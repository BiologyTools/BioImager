using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace BioImager
{
    public partial class Library : Form
    {
        public static Lib dll = null;
        public Library()
        {
            InitializeComponent();
        }

        /// It opens a file dialog, and if the user selects a file, it creates a new Lib object with the
        /// file name, and adds all the types in the Lib object to the typeBox
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event data.
        /// 
        /// @return The file name of the file that was selected.
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            dll = new Lib(openFileDialog.FileName);
            typeBox.Items.AddRange(dll.Types.Values.ToArray());

        }

        /// It takes the selected item from the typeBox, and then adds the interfaces, methods, and
        /// enums to their respective boxes
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event data.
        private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lib.TypeInfo item = (Lib.TypeInfo)typeBox.SelectedItem;
            interBox.Items.Clear();
            interBox.Items.AddRange(item.Interfaces.Values.ToArray());
            methodsBox.Items.Clear();
            foreach (List<MethodInfo> me in item.Methods.Values)
            {
                foreach (MethodInfo meitem in me)
                {
                methodsBox.Items.Add(meitem);
                }
                
            }
            enumsBox.Items.Clear();
            enumsBox.Items.AddRange(item.Enums.Values.ToArray());
        }
    }
    public class Lib
    {
        public Assembly dll;
        public Dictionary<string, TypeInfo> Types = new Dictionary<string, TypeInfo>();
        public Dictionary<string, TypeInfo> Objects = new Dictionary<string, TypeInfo>();
        public Dictionary<string, Module> Modules = new Dictionary<string, Module>();
        public Dictionary<string, ConstructorInfo> Constructors = new Dictionary<string, ConstructorInfo>();
        public Dictionary<string, InterfaceMapping> Interfaces = new Dictionary<string, InterfaceMapping>();
        /* It's a wrapper for a type that contains all the information about the type */
        public class TypeInfo
        {
            public enum Kind
            {
                Interface,
                Member,
                Field,
                Enum,
                Property,
                Constructor,
            }
            public Kind kind;
            public Dictionary<string, Enum> Enums = new Dictionary<string, Enum>();
            public Dictionary<string, Type> Interfaces = new Dictionary<string, Type>();
            public Dictionary<string, ConstructorInfo> Constructors = new Dictionary<string, ConstructorInfo>();
            public Dictionary<string, List<MemberInfo>> Members = new Dictionary<string, List<MemberInfo>>();
            public Dictionary<string, FieldInfo> Fields = new Dictionary<string, FieldInfo>();
            public Dictionary<string, PropertyInfo> Properties = new Dictionary<string, PropertyInfo>();
            public Dictionary<string, List<MethodInfo>> Methods = new Dictionary<string, List<MethodInfo>>();
            public Type type;
            public string GUID;
            public void AddMember(MemberInfo inf)
            {
                if(Members.ContainsKey(inf.ToString()))
                {
                    Members[inf.ToString()].Add(inf);
                }
                else
                {
                    List<MemberInfo> l = new List<MemberInfo>();
                    l.Add(inf);
                    Members.Add(inf.ToString(), l);
                }
            }
            public void AddMethod(MethodInfo inf)
            {
                if (Methods.ContainsKey(inf.ToString()))
                {
                    Methods[inf.ToString()].Add(inf);
                }
                else
                {
                    List<MethodInfo> l = new List<MethodInfo>();
                    l.Add(inf);
                    Methods.Add(inf.ToString(), l);
                }
            }
            public void AddField(FieldInfo inf)
            {
                Fields.Add(inf.ToString(),inf);
            }
            public void AddProperty(PropertyInfo inf)
            {
                Properties.Add(inf.ToString(),inf);
            }
            public object Invoke(string name, object o, object[] args)
            {
                return type.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
            }
            public object GetProperty(string name, object obj)
            {
                PropertyInfo p = type.GetProperty(name);
                return p.GetValue(obj);
            }
            public object Instance()
            {
                return Activator.CreateInstance(type);
            }
            public override string ToString()
            {
                return type.Name;
            }
        }
        /* The ObjectInfo class contains a TypeInfo object */
        public class ObjectInfo
        {
            public TypeInfo type;
            
        }
        /* It's loading the dll, and then it's getting all the types, modules, interfaces,
        constructors, properties, fields, methods, and members. */
        public Lib(string file)
        {
            dll = Assembly.LoadFile(file);
            //Assembly.ReflectionOnlyLoad
            Type[] tps = dll.GetExportedTypes();
            foreach (Type type in tps)
            {
                TypeInfo t = new TypeInfo();
                t.type = type;
                string s = type.ToString();
                s = s.Remove(0, s.LastIndexOf('.') + 1);
                if (type.IsEnum)
                {
                    GetEnums(t);
                }
                GetInterfaces(t);
                GetConstructors(t);
                GetProperties(t);
                GetFields(t);
                GetMethods(t);
                GetMembers(t);
                Types.Add(s,t);
            }
            Module[] mds = dll.GetModules();
            foreach (Module m in mds)
            {
                string s = m.ToString();
                s = s.Remove(0, s.LastIndexOf('.') + 1);
                Modules.Add(s, m);
            }
        }
        /// It invokes a method on an object
        /// 
        /// @param Type The type of the object you want to invoke the method on.
        /// @param name The name of the method to invoke.
        /// @param o The object to invoke the method on.
        /// @param args The arguments to pass to the method. This array of arguments must match in
        /// number, order, and type the parameters of the method to be invoked. If there are no
        /// parameters, args must be null.
        /// 
        /// @return The return value of the method.
        public object Invoke(Type type, string name, object o, object[] args)
        {
            return type.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }
        /// It takes a string, a name, an object, and an array of objects, and returns an object
        /// 
        /// @param st The name of the type.
        /// @param name The name of the method to invoke.
        /// @param o The object to invoke the method on.
        /// @param args The arguments to pass to the method. This array of arguments must match in
        /// number, order, and type the parameters of the method to be invoked. If there are no
        /// parameters, args must be null.
        /// 
        /// @return The return value of the method.
        public object Invoke(string st, string name, object o, object[] args)
        {
            Type t = Types[st].type;
            return t.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }
        /// Get the property of the object with the given name
        /// 
        /// @param type The type of the object you want to get the property from.
        /// @param name The name of the property
        /// @param obj The object you want to get the property from
        /// 
        /// @return The value of the property.
        public object GetProperty(string type, string name, object obj)
        {
            Type myType = Types[type].type;
            PropertyInfo p = myType.GetProperty(name);
            return p.GetValue(obj);
        }
        /// It gets all the values of an enum and adds them to a dictionary.
        /// 
        /// @param TypeInfo This is a class that contains the type, the name of the type, and a
        /// dictionary of enums.
        private void GetEnums(TypeInfo type)
        {
            Array ar = Enum.GetValues(type.type);
            foreach (var item in ar)
            {
                if(!type.Enums.ContainsKey(item.ToString()))
                type.Enums.Add(item.ToString(), (Enum)item);
            }
        }
        /// > Get all the interfaces that the type implements and add them to the type's Interfaces
        /// dictionary
        /// 
        /// @param TypeInfo This is a class that contains the type, the type's name, the type's
        /// namespace, the type's assembly, the type's base type, the type's interfaces, and the type's
        /// methods.
        private void GetInterfaces(TypeInfo type)
        {
            Type[] ts = type.type.GetInterfaces();
            foreach (Type item in ts)
            {
                type.Interfaces.Add(item.ToString(), item);
            }
        }
        /// It gets the constructors of a type and adds them to a dictionary
        /// 
        /// @param TypeInfo This is a class that I created to hold information about the type.
        private void GetConstructors(TypeInfo type)
        {
            ConstructorInfo[] ts = type.type.GetConstructors();
            foreach (ConstructorInfo item in ts)
            {
                type.Constructors.Add(item.ToString(), item);
            }
        }
        /// > Get all the members of a type and add them to the type's member list
        /// 
        /// @param TypeInfo This is a class that contains the type and the members of the type.
        private void GetMembers(TypeInfo type)
        {
            MemberInfo[] ts = type.type.GetMembers();
            foreach (MemberInfo item in ts)
            {
                type.AddMember(item);
            }
        }
        /// > Get all the properties of the type and add them to the type
        /// 
        /// @param TypeInfo This is a class that contains the type, the properties, and the methods.
        private void GetProperties(TypeInfo type)
        {
            PropertyInfo[] ts = type.type.GetProperties();
            foreach (PropertyInfo item in ts)
            {
                type.AddProperty(item);
            }
        }
        /// > Get all the fields of the type and add them to the type's field list
        /// 
        /// @param TypeInfo This is a class that contains the type, the fields, and the methods.
        private void GetFields(TypeInfo type)
        {
            FieldInfo[] ts = type.type.GetFields();
            foreach (FieldInfo item in ts)
            {
                type.AddField(item);
            }
        }
        /// It takes a TypeInfo object and adds all the methods of the type to the TypeInfo object
        /// 
        /// @param TypeInfo This is a class that I created to store the information about the type.
        private void GetMethods(TypeInfo type)
        {
            MethodInfo[] ts = type.type.GetMethods();
            foreach (MethodInfo item in ts)
            {
                type.AddMethod(item);
            }
        }
    }
}
