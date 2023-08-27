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
        /// It initializes the form with the values of the function
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
            foreach (Function.FunctionType f in (Function.FunctionType[])Enum.GetValues(typeof(Function.FunctionType)))
            {
                microBox.Items.Add(f);
            }
            microBox.SelectedItem = func.FuncType;

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
        /// It updates the items in the form
        private void UpdateItems()
        {
            int ind = 0;
            textBox.Text = func.Script;
            nameBox.Text = func.Name;
            stateBox.SelectedItem = func.State;
            keysBox.SelectedItem = func.Key;
            modifierBox.SelectedItem = func.Modifier;
            microBox.SelectedItem = func.FuncType;
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
        /// When the user selects a key from the dropdown menu, the function's key is set to the
        /// selected key
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void keysBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.Key = (VirtualKeyCode)keysBox.SelectedItem;
        }
        /// When the user selects a modifier key from the dropdown menu, the function's modifier is set
        /// to the selected key
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void modifierBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.Modifier = (VirtualKeyCode)modifierBox.SelectedItem;
        }
        /// When the user selects a new item in the stateBox, the state of the function is set to the
        /// selected item and the function type is set to key
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void stateBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.State = (Function.ButtonState)stateBox.SelectedItem;
        }
        /// It saves the function
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
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
            func.FuncType = (Function.FunctionType)microBox.SelectedItem;
            func.Save();
            this.Close();
        }
        /// When the user selects a property from the dropdown list, the function will set the File
        /// property of the Func object to the name of the property selected, and the FuncType property
        /// to Property
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void propBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.File = propBox.Text;
            Func.FuncType = Function.FunctionType.Property;
        }
        /// When the user selects a recording from the dropdown box, the program will set the file name
        /// to the selected recording and set the function type to recording.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void recBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.File = recBox.Text;
            Func.FuncType = Function.FunctionType.Recording;
        }
        /// This function is called when the user clicks the "Set Macro File" button. It sets the macro
        /// file to the file specified in the text box
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void setMacroFileBut_Click(object sender, EventArgs e)
        {
            Func.File = propBox.Text;
            Func.FuncType = Function.FunctionType.ImageJ;
        }
        /// It sets the file path of the script file to the text in the textbox.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void setScriptFileBut_Click(object sender, EventArgs e)
        {
            Func.File = propBox.Text;
            Func.FuncType = Function.FunctionType.Script;
        }
        /// When the value of the numeric up/down control changes, the value of the function is set to
        /// the value of the numeric up/down control
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void valBox_ValueChanged(object sender, EventArgs e)
        {
            Func.Value = (double)valBox.Value;
        }
        /// If the imageJRadioBut is checked, then the PerformFunction function is called with the
        /// argument true. Otherwise, the PerformFunction function is called with the argument false
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void performBut_Click(object sender, EventArgs e)
        {
            MessageBox.Show(func.PerformFunction(imageJRadioBut.Checked).ToString());
        }

        /// The function cancelBut_Click is a private function that takes two parameters, sender and e,
        /// and returns nothing
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// The text in the textbox is set to the script property of the function object
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            func.Script = textBox.Text;
            if (bioRadioBut.Checked)
                func.FuncType = Function.FunctionType.Script;
            else
                func.FuncType = Function.FunctionType.ImageJ;
        }

        /// If the radio button is checked, set the function type to script
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void bioRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            if(bioRadioBut.Checked)
            func.FuncType = Function.FunctionType.Script;
        }

        /// If the imageJRadioBut is checked, then set the function type to imageJ
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void imageJRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            if (imageJRadioBut.Checked)
                func.FuncType = Function.FunctionType.ImageJ;
        }

        /// The function is called when the text in the menuPath textbox is changed
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void menuPath_TextChanged(object sender, EventArgs e)
        {
            func.MenuPath = menuPath.Text;
        }

        /// This function is called when the form is activated
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void FunctionForm_Activated(object sender, EventArgs e)
        {

        }

        /// If the user selects a function from the dropdown menu, the function is set to the selected
        /// function and the items are updated
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        /// 
        /// @return The function that is selected in the combo box.
        private void funcsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (funcsBox.SelectedIndex == -1)
                return;
            func = (Function)funcsBox.SelectedItem;
            UpdateItems();
        }

        /// This function is called when the form is closing
        /// 
        /// @param sender The object that raised the event.
        /// @param FormClosingEventArgs 
        private void FunctionForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// When the text in the contextMenuPath textbox changes, the ContextPath variable in the func
        /// class is set to the text in the contextMenuPath textbox
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void contextMenuPath_TextChanged(object sender, EventArgs e)
        {
            func.ContextPath = contextMenuPath.Text;
        }

        /// When the user selects a new function from the dropdown menu, the function type is changed to
        /// the selected function
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void microBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //func.FuncType = (Function.FunctionType)microBox.SelectedItem;
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class Function
    {
        public static Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        /* Defining an enum. */
        public enum FunctionType
        {
            Key,
            StageUp,
            StageRight,
            StageDown,
            StageLeft,
            FocusUp,
            FocusDown,
            StageFieldUp,
            StageFieldRight,
            StageFieldDown,
            StageFieldLeft,
            TakeImage,
            TakeImageStack,
            RL,
            TL,
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
        /* Defining an enumeration. */
        public enum ButtonState
        {
            Pressed,
            Released
        }

        private ButtonState buttonState = ButtonState.Pressed;
        /* A property. */
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
        /* A property of a class. */
        public VirtualKeyCode Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
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
        public override string ToString()
        {
            return name + ", " + MenuPath;
        }
        public static Function Parse(string s)
        {
            if (s == "")
                return new Function();
            try
            {
                return JsonConvert.DeserializeObject<Function>(s);
            }
            catch (Exception)
            {
                return new Function();
            }
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static InputSimulator input = new InputSimulator();
        /// It runs a function based on the type of function it is
        /// 
        /// @param imagej boolean
        /// 
        /// @return The return value is the object that is being returned.
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
            if (FuncType == Function.FunctionType.StageUp)
            {
                Microscope.MoveUp(Value);
            }
            if (FuncType == Function.FunctionType.StageRight)
            {
                Microscope.MoveRight(Value);
            }
            if (FuncType == Function.FunctionType.StageDown)
            {
                Microscope.MoveDown(Value);
            }
            if (FuncType == Function.FunctionType.StageLeft)
            {
                Microscope.MoveLeft(Value);
            }
            if (FuncType == Function.FunctionType.FocusUp)
            {
                Microscope.SetFocus(Value);
            }
            if (FuncType == Function.FunctionType.FocusDown)
            {
                Microscope.SetFocus(-Value);
            }
            if (FuncType == Function.FunctionType.StageFieldUp)
            {
                Microscope.MoveFieldUp();
            }
            if (FuncType == Function.FunctionType.StageFieldRight)
            {
                Microscope.MoveFieldRight();
            }
            if (FuncType == Function.FunctionType.StageFieldDown)
            {
                Microscope.MoveFieldDown();
            }
            if (FuncType == Function.FunctionType.StageFieldLeft)
            {
                Microscope.MoveFieldLeft();
            }
            if (FuncType == Function.FunctionType.TakeImage)
            {
                Microscope.TakeImage();
            }
            if (FuncType == Function.FunctionType.TakeImageStack)
            {
                Microscope.TakeImageStack();
            }
            if (FuncType == Function.FunctionType.RL)
            {
                Microscope.RLShutter.SetPosition((int)Value);
            }
            if (FuncType == Function.FunctionType.TL)
            {
                Microscope.TLShutter.SetPosition((int)Value);
            }
            return null;
        }
        /// It reads all the files in the Functions folder, parses them into Function objects, and adds
        /// them to the Functions dictionary
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
        /// It saves all the functions in the Functions dictionary to a file
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
