using Bio.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.XInput;
using WindowsInput;
using WindowsInput.Native;
using System.IO;
using Newtonsoft.Json;

namespace Bio
{
    public partial class FunctionForm : Form
    {
        public static InputSimulator input = new InputSimulator();
        private Function func;
        public Function Func
        {
            get
            {
                return func;
            }
            set
            {
                func = value;
            }
        }
        public FunctionForm(Function func)
        {
            InitializeComponent();
            Func = func;
            Init();
        }
        public FunctionForm(Function func, string name)
        {
            InitializeComponent();
            Func = func;
            func.Name = name;
            Init();
        }
        private void Init()
        {
            int ind = 0;
            int sel = 0;

            textBox.Text = func.Script;

            funcsBox.Items.Clear();
            foreach (Function f in Function.Functions.Values)
            {
                funcsBox.Items.Add(f);
            }

            nameBox.Text = func.Name;
            //We add button states to buttonState box.
            stateBox.Items.Clear();
            foreach (Function.ButtonState val in Enum.GetValues(typeof(Function.ButtonState)))
            {
                stateBox.Items.Add(val);
            }
            stateBox.SelectedItem = func.State;

            //We add button states to buttonState box.
            ind = 0;
            keysBox.Items.Clear();
            foreach (VirtualKeyCode val in Enum.GetValues(typeof(VirtualKeyCode)))
            {
                keysBox.Items.Add(val);
                if (val == func.Key)
                    keysBox.SelectedIndex = ind;
                ind++;
            }
            //We add the modifiers to modifierBox
            modifierBox.Items.Clear();
            modifierBox.Items.Add(VirtualKeyCode.NONAME);
            modifierBox.Items.Add(VirtualKeyCode.RCONTROL);
            modifierBox.Items.Add(VirtualKeyCode.LCONTROL);
            modifierBox.Items.Add(VirtualKeyCode.RSHIFT);
            modifierBox.Items.Add(VirtualKeyCode.LSHIFT);
            modifierBox.Items.Add(VirtualKeyCode.MENU);
            modifierBox.Items.Add(VirtualKeyCode.RWIN);
            modifierBox.Items.Add(VirtualKeyCode.LWIN);
            modifierBox.SelectedItem = func.Modifier;

            microBox.Items.Clear();
            microBox.Items.Add(Function.FunctionType.NextCoordinate);
            microBox.Items.Add(Function.FunctionType.NextSnapCoordinate);
            microBox.Items.Add(Function.FunctionType.PreviousCoordinate);
            microBox.Items.Add(Function.FunctionType.PreviousSnapCoordinate);
            microBox.Items.Add(Function.FunctionType.Objective);
            microBox.Items.Add(Function.FunctionType.NextCoordinate);
            microBox.Items.Add(Function.FunctionType.StoreCoordinate);
            microBox.SelectedItem = func.Microscope;

            ind = 0;
            recBox.Items.Clear();
            foreach (Automation.Recording rec in Automation.Recordings.Values)
            {
                recBox.Items.Add(rec);
                string s = System.IO.Path.GetFileNameWithoutExtension(rec.File);
                if(s == func.File)
                {
                    recBox.SelectedIndex = ind;
                }
                ind++;
            }
            
            ind = 0;
            propBox.Items.Clear();
            foreach (Automation.Recording rec in Automation.Properties.Values)
            {
                propBox.Items.Add(rec);
                string s = System.IO.Path.GetFileNameWithoutExtension(rec.File);
                if (s == func.File)
                {
                    propBox.SelectedIndex = ind;
                }
                ind++;
            }
            valBox.Value = (decimal)func.Value;
            menuPath.Text = func.MenuPath;
            contextMenuPath.Text = func.ContextPath;
        }
        private void UpdateItems()
        {
            int ind = 0;
            textBox.Text = func.Script;
            nameBox.Text = func.Name;
            stateBox.SelectedItem = func.State;
            keysBox.SelectedItem = func.Key;
            modifierBox.SelectedItem = func.Modifier;
            microBox.SelectedItem = func.Microscope;
            foreach (Automation.Recording rec in Automation.Recordings.Values)
            {
                string s = System.IO.Path.GetFileNameWithoutExtension(rec.File);
                if (s == func.File)
                {
                    recBox.SelectedIndex = ind;
                }
                ind++;
            }
            ind = 0;
            foreach (Automation.Recording rec in Automation.Properties.Values)
            {
                string s = System.IO.Path.GetFileNameWithoutExtension(rec.File);
                if (s == func.File)
                {
                    propBox.SelectedIndex = ind;
                }
                ind++;
            }
            valBox.Value = (decimal)func.Value;
            if (func.FuncType == Function.FunctionType.ImageJ)
                imageJRadioBut.Checked = true;
            else
                imageJRadioBut.Checked = false;
            menuPath.Text = func.MenuPath;
            contextMenuPath.Text = func.ContextPath;
        }
        private void keysBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.Key = (VirtualKeyCode)keysBox.SelectedItem;
            Func.FuncType = Function.FunctionType.Key;
        }
        private void modifierBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.Modifier = (VirtualKeyCode)modifierBox.SelectedItem;
            Func.FuncType = Function.FunctionType.Key;
        }
        private void stateBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.State = (Function.ButtonState)stateBox.SelectedItem;
            Func.FuncType = Function.FunctionType.Key;
        }
        private void applyButton_Click(object sender, EventArgs e)
        {
            func.Name = nameBox.Text;
            this.DialogResult = DialogResult.OK;
            if(!Function.Functions.ContainsKey(func.Name))
            {
                Function.Functions.Add(func.Name, func);
            }
            else
            {
                Function.Functions[func.Name] = func;
            }
            if (func.MenuPath != null && func.MenuPath != "")
            {
                if (func.MenuPath.EndsWith("/"))
                    func.MenuPath = func.MenuPath.TrimEnd('/');
                if (func.MenuPath.EndsWith(func.Name) && func.MenuPath.Contains("/"))
                    func.MenuPath.Remove(func.MenuPath.IndexOf('/'), func.MenuPath.Length - func.MenuPath.IndexOf('/'));
                App.AddMenu(func.MenuPath, func);
            }
            if (func.ContextPath != null && func.ContextPath != "")
            {
                if (func.ContextPath.EndsWith("/"))
                    func.ContextPath = func.ContextPath.TrimEnd('/');
                if (func.ContextPath.EndsWith(func.Name) && func.ContextPath.Contains("/"))
                    func.ContextPath.Remove(func.ContextPath.IndexOf('/'), func.ContextPath.Length - func.ContextPath.IndexOf('/'));
                App.AddContextMenu(func.ContextPath, func);
            }
            func.Save();
            Init();
        }
        private void propBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.File = propBox.Text;
            Func.FuncType = Function.FunctionType.Property;
        }
        private void recBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.File = recBox.Text;
            Func.FuncType = Function.FunctionType.Recording;
        }
        private void setMacroFileBut_Click(object sender, EventArgs e)
        {
            Func.File = propBox.Text;
            Func.FuncType = Function.FunctionType.ImageJ;
        }
        private void setScriptFileBut_Click(object sender, EventArgs e)
        {
            Func.File = propBox.Text;
            Func.FuncType = Function.FunctionType.Script;
        }
        private void valBox_ValueChanged(object sender, EventArgs e)
        {
            Func.Value = (double)valBox.Value;
        }
        private void performBut_Click(object sender, EventArgs e)
        {
            MessageBox.Show(func.PerformFunction(imageJRadioBut.Checked).ToString());
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            func.Script = textBox.Text;
            if (bioRadioBut.Checked)
                func.FuncType = Function.FunctionType.Script;
            else
                func.FuncType = Function.FunctionType.ImageJ;
        }

        private void bioRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            if(bioRadioBut.Checked)
            func.FuncType = Function.FunctionType.Script;
        }

        private void imageJRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            if (imageJRadioBut.Checked)
                func.FuncType = Function.FunctionType.ImageJ;
        }

        private void menuPath_TextChanged(object sender, EventArgs e)
        {
            func.MenuPath = menuPath.Text;
        }

        private void FunctionForm_Activated(object sender, EventArgs e)
        {

        }

        private void funcsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (funcsBox.SelectedIndex == -1)
                return;
            func = (Function)funcsBox.SelectedItem;
            UpdateItems();
        }

        private void FunctionForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void contextMenuPath_TextChanged(object sender, EventArgs e)
        {
            func.ContextPath = contextMenuPath.Text;
        }

        private void microBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            func.FuncType = (Function.FunctionType)microBox.SelectedItem;
        }
    }

    public class Function
    {
        public static Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public enum FunctionType
        {
            Key,
            Microscope,
            Objective,
            StoreCoordinate,
            NextCoordinate,
            PreviousCoordinate,
            NextSnapCoordinate,
            PreviousSnapCoordinate,
            Recording,
            Property,
            ImageJ,
            Script,
            None
        }
        public enum ButtonState
        {
            Pressed,
            Released
        }

        private ButtonState buttonState = ButtonState.Pressed;
        public ButtonState State
        {
            get
            {
                return buttonState;
            }
            set
            {
                buttonState = value;
            }
        }

        private VirtualKeyCode key;
        public VirtualKeyCode Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
                FuncType = FunctionType.Key;
            }
        }

        private VirtualKeyCode modifier = VirtualKeyCode.NONAME;
        public VirtualKeyCode Modifier
        {
            get
            {
                return modifier;
            }
            set
            {
                FuncType = FunctionType.Key;
                modifier = value;
            }
        }

        private FunctionType functionType;
        public FunctionType FuncType
        {
            get
            {
                return functionType;
            }
            set
            {
                functionType = value;
            }
        }

        private string file;
        public string File
        {
            get { return file; }
            set { file = value; }
        }

        private string script;
        public string Script
        {
            get { return script; }
            set 
            { 
                script = value;
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private string menuPath;
        public string MenuPath
        {
            get
            {
                return menuPath;
            }
            set
            {
                menuPath = value;
            }
        }
        private string contextPath;
        public string ContextPath
        {
            get
            {
                return menuPath;
            }
            set
            {
                menuPath = value;
            }
        }

        private double val;
        public double Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }
        private string microscope;
        public string Microscope
        {
            get
            {
                return microscope;
            }
            set
            {
                microscope = value;
            }
        }
        public override string ToString()
        {
            return name + ", " + MenuPath;
        }
        public static Function Parse(string s)
        {
            if (s == "")
                return new Function();
            return JsonConvert.DeserializeObject<Function>(s);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static InputSimulator input = new InputSimulator();
        public object PerformFunction(bool imagej)
        {
            if(FuncType == FunctionType.Key && Script!="")
            if(imagej)
            {
                FuncType = FunctionType.ImageJ;
            }
            else
                FuncType = FunctionType.Script;
            if (FuncType == Function.FunctionType.Script)
            {
                Scripting.RunString(script);
            }
            if (FuncType == Function.FunctionType.ImageJ)
            {
                ImageJ.RunOnImage(script, false, BioConsole.onTab, BioConsole.useBioformats);
            }
            if (FuncType == Function.FunctionType.Property)
            {
                if (Name.StartsWith("Get"))
                {
                    Automation.Recording r = (Automation.Recording)Automation.Properties[System.IO.Path.GetFileNameWithoutExtension(File)];
                    return r.Get();
                }
                else if (Name.StartsWith("Set"))
                {
                    Automation.Recording r = (Automation.Recording)Automation.Properties[System.IO.Path.GetFileNameWithoutExtension(File)];
                    r.Set(Value.ToString());
                }

            }
            if (FuncType == Function.FunctionType.Key)
            {
                if (Modifier != VirtualKeyCode.NONAME)
                {
                    input.Keyboard.ModifiedKeyStroke(Modifier, Key);
                }
                else
                {
                    input.Keyboard.KeyPress(Key);
                }
                return null;
            }
            if (FuncType == Function.FunctionType.Recording)
            {
                Automation.Recording r = (Automation.Recording)Automation.Recordings[System.IO.Path.GetFileNameWithoutExtension(File)];
                r.Run();
            }
            if (FuncType == Function.FunctionType.Objective)
            {
                App.imager.PerformFunction(this);
            }
            if (FuncType == Function.FunctionType.StoreCoordinate)
            {
                App.imager.PerformFunction(this);
            }
            if (FuncType == Function.FunctionType.NextCoordinate)
            {
                App.imager.PerformFunction(this);
            }
            if (FuncType == Function.FunctionType.PreviousCoordinate)
            {
                App.imager.PerformFunction(this);
            }
            if (FuncType == Function.FunctionType.NextSnapCoordinate)
            {
                App.imager.PerformFunction(this);
            }
            if (FuncType == Function.FunctionType.PreviousSnapCoordinate)
            {
                App.imager.PerformFunction(this);
            }
            if (FuncType == Function.FunctionType.Microscope)
            {
                App.imager.PerformFunction(this);
            }
            return null;
        }
        public static void Initialize()
        {
            string st = Application.StartupPath + "/Functions";
            if (!Directory.Exists(st))
                Directory.CreateDirectory(st);
            string[] sts = Directory.GetFiles(st);
            for (int i = 0; i < sts.Length; i++)
            {
                string fs = System.IO.File.ReadAllText(sts[i]);
                Function f = Function.Parse(fs);
                if(!Functions.ContainsKey(f.Name))
                Functions.Add(f.Name, f);
                App.AddMenu(f.MenuPath, f);
                App.AddContextMenu(f.ContextPath, f);
            }
        }
        public void Save()
        {
            string st = Application.StartupPath;
            
            if(!Directory.Exists(st + "/Functions"))
            {
                Directory.CreateDirectory(st + "/Functions");
            }
            foreach (Function f in Functions.Values)
            {
                System.IO.File.WriteAllText(st + "/Functions/" + f.Name + ".func",f.Serialize());
            }
        }
    }

}
