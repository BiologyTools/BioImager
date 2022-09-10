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
using Newtonsoft.Json;

namespace Bio
{
    public partial class ControllerFunc : Form
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
        public ControllerFunc(Function func)
        {
            InitializeComponent();
            Func = func;
            Init();
        }
        public ControllerFunc(Function func, string name)
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
            foreach (Microscope.Actions val in Enum.GetValues(typeof(Microscope.Actions)))
            {
                if (func.Microscope == val.ToString())
                    sel = ind;
                microBox.Items.Add(val);
                ind++;
            }
            microBox.SelectedIndex = sel;
            if(func.FuncType == Function.FunctionType.Microscope)
            {
                for (int i = 0; i < microBox.Items.Count; i++)
                {
                    if (microBox.Items[i].ToString() == func.Name)
                    {
                        microBox.SelectedIndex = i;
                        func.FuncType = Function.FunctionType.Microscope;
                        break;
                    }
                }
            }
            nameBox.Text = func.Name;
            //We add button states to buttonState box.
            foreach (Function.ButtonState val in Enum.GetValues(typeof(Function.ButtonState)))
            {
                stateBox.Items.Add(val);
            }
            stateBox.SelectedItem = func.State;

            //We add button states to buttonState box.
            ind = 0;
            foreach (VirtualKeyCode val in Enum.GetValues(typeof(VirtualKeyCode)))
            {
                keysBox.Items.Add(val);
                if (val == func.Key)
                    keysBox.SelectedIndex = ind;
                ind++;
            }
            //We add the modifiers to modifierBox
            modifierBox.Items.Add(VirtualKeyCode.NONAME);
            modifierBox.Items.Add(VirtualKeyCode.RCONTROL);
            modifierBox.Items.Add(VirtualKeyCode.LCONTROL);
            modifierBox.Items.Add(VirtualKeyCode.RSHIFT);
            modifierBox.Items.Add(VirtualKeyCode.LSHIFT);
            modifierBox.Items.Add(VirtualKeyCode.MENU);
            modifierBox.Items.Add(VirtualKeyCode.RWIN);
            modifierBox.Items.Add(VirtualKeyCode.LWIN);
            modifierBox.SelectedItem = func.Modifier;

            //We add the objectives to objectivesBox;
            objectivesBox.Items.AddRange(Microscope.Objectives.List.ToArray());
            if (func.FuncType == Function.FunctionType.Objective)
                objectivesBox.SelectedItem = func.Objective;
            ind = 0;
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
        }
        private void objectivesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectivesBox.SelectedIndex == 0)
                return;
            Func.Objective = ((Objectives.Objective)objectivesBox.SelectedItem).Name;
            Func.FuncType = Function.FunctionType.Objective;
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
            this.Close();
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
        private void microBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func.FuncType = Function.FunctionType.Microscope;
            Func.Microscope = microBox.SelectedItem.ToString();
        }
        private void valBox_ValueChanged(object sender, EventArgs e)
        {
            Func.Value = (double)valBox.Value;
        }
    }

    public class Function
    {
        public static Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public enum FunctionType
        {
            Key,
            Objective,
            StoreCoordinate,
            NextCoordinate,
            PreviousCoordinate,
            NextSnapCoordinate,
            PreviousSnapCoordinate,
            Recording,
            Property,
            ImageJ,
            Microscope,
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

        private string objective;
        public string Objective
        {
            get
            {
                return objective;
            }
            set
            {
                FuncType = FunctionType.Objective;
                objective = value;
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

        private string micro;
        public string Microscope
        { 
            get
            {
                return micro;
            }
            set
            {
                micro = value;
            }
        }
        public Objectives.Objective GetObjective()
        {
            if (objective == null)
                return null;
            foreach (Objectives.Objective obj in Bio.Microscope.Objectives.List)
            {
                if (objective.Contains(obj.NumericAperture.ToString()) && objective.Contains(obj.Magnification.ToString()))
                {
                    return obj;
                }
            }
            return null;
        }

        public override string ToString()
        {
            string s = JsonConvert.SerializeObject(this);
            return s;
        }

        public static Function Parse(string s)
        {
            if (s == "")
                return new Function();
            return JsonConvert.DeserializeObject<Function>(s);
        }

    }

}
