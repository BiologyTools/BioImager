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

namespace Bio
{
    public partial class Library : Form
    {
        public static Lib dll = null;
        public Library()
        {
            InitializeComponent();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            dll = new Lib(openFileDialog.FileName);
            typeBox.Items.AddRange(dll.Types.Values.ToArray());

        }

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
        public class ObjectInfo
        {
            public TypeInfo type;
            
        }
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
        public object Invoke(Type type, string name, object o, object[] args)
        {
            return type.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }
        public object Invoke(string st, string name, object o, object[] args)
        {
            Type t = Types[st].type;
            return t.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }
        public object GetProperty(string type, string name, object obj)
        {
            Type myType = Types[type].type;
            PropertyInfo p = myType.GetProperty(name);
            return p.GetValue(obj);
        }
        private void GetEnums(TypeInfo type)
        {
            Array ar = Enum.GetValues(type.type);
            foreach (var item in ar)
            {
                if(!type.Enums.ContainsKey(item.ToString()))
                type.Enums.Add(item.ToString(), (Enum)item);
            }
        }
        private void GetInterfaces(TypeInfo type)
        {
            Type[] ts = type.type.GetInterfaces();
            foreach (Type item in ts)
            {
                type.Interfaces.Add(item.ToString(), item);
            }
        }
        private void GetConstructors(TypeInfo type)
        {
            ConstructorInfo[] ts = type.type.GetConstructors();
            foreach (ConstructorInfo item in ts)
            {
                type.Constructors.Add(item.ToString(), item);
            }
        }
        private void GetMembers(TypeInfo type)
        {
            MemberInfo[] ts = type.type.GetMembers();
            foreach (MemberInfo item in ts)
            {
                type.AddMember(item);
            }
        }
        private void GetProperties(TypeInfo type)
        {
            PropertyInfo[] ts = type.type.GetProperties();
            foreach (PropertyInfo item in ts)
            {
                type.AddProperty(item);
            }
        }
        private void GetFields(TypeInfo type)
        {
            FieldInfo[] ts = type.type.GetFields();
            foreach (FieldInfo item in ts)
            {
                type.AddField(item);
            }
        }
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
