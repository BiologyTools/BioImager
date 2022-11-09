using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Configuration;

namespace Bio
{
    public partial class MicroscopeSetup : Form
    {
        public static BioImage simImage = null;
        public MicroscopeSetup()
        {
            InitializeComponent();
            if (!System.IO.Directory.Exists(Application.StartupPath + "//" + "Config"))
                System.IO.Directory.CreateDirectory(Application.StartupPath + "//" + "Config");
            Objectives_Load();

            simCameraBox.Checked = Properties.Settings.Default.SimulateCamera;
            if (simCameraBox.Checked)
            {
                imageLabel.Text = Properties.Settings.Default.SimulatedImage;
                if(File.Exists(Properties.Settings.Default.SimulatedImage))
                simImage = BioImage.OpenFile(Properties.Settings.Default.SimulatedImage);
            }
        }
        private void Objectives_Load()
        {
            string st = Application.StartupPath + "/Config/MicroscopeObjectives.json";
            if (File.Exists(st))
            {
                List<Objectives.Objective> list = JsonConvert.DeserializeObject<List<Objectives.Objective>>(File.ReadAllText(st));
                if (list.Count == 0)
                {
                    //We initialize with default values.
                    UpdateObjectives();
                }
                obj1Name.Text = list[0].Name;
                obj2Name.Text = list[1].Name;
                obj3Name.Text = list[2].Name;
                obj4Name.Text = list[3].Name;
                obj5Name.Text = list[4].Name;
                obj6Name.Text = list[5].Name;
                if (Microscope.Objectives.List.Count == 7)
                    obj7Name.Text = list[6].Name;

                objectiveA1Box.Value = (decimal)list[0].AcquisitionExposure;
                objectiveA2Box.Value = (decimal)list[1].AcquisitionExposure;
                objectiveA3Box.Value = (decimal)list[2].AcquisitionExposure;
                objectiveA4Box.Value = (decimal)list[3].AcquisitionExposure;
                objectiveA5Box.Value = (decimal)list[4].AcquisitionExposure;
                objectiveA6Box.Value = (decimal)list[5].AcquisitionExposure;
                if (Microscope.Objectives.List.Count == 7)
                    objectiveA7Box.Value = (decimal)list[6].AcquisitionExposure;

                objectiveL1Box.Value = (decimal)list[0].LocateExposure;
                objectiveL2Box.Value = (decimal)list[1].LocateExposure;
                objectiveL3Box.Value = (decimal)list[2].LocateExposure;
                objectiveL4Box.Value = (decimal)list[3].LocateExposure;
                objectiveL5Box.Value = (decimal)list[4].LocateExposure;
                objectiveL6Box.Value = (decimal)list[5].LocateExposure;
                if (Microscope.Objectives.List.Count == 7)
                    objectiveL7Box.Value = (decimal)list[6].LocateExposure;

                Obj1LMove.Value = (decimal)list[0].MoveAmountL;
                Obj2LMove.Value = (decimal)list[1].MoveAmountL;
                Obj3LMove.Value = (decimal)list[2].MoveAmountL;
                Obj4LMove.Value = (decimal)list[3].MoveAmountL;
                Obj5LMove.Value = (decimal)list[4].MoveAmountL;
                Obj6LMove.Value = (decimal)list[5].MoveAmountL;
                if (Microscope.Objectives.List.Count == 7)
                    Obj7LMove.Value = (decimal)list[6].MoveAmountL;

                Obj1RMove.Value = (decimal)list[0].MoveAmountR;
                Obj2RMove.Value = (decimal)list[1].MoveAmountR;
                Obj3RMove.Value = (decimal)list[2].MoveAmountR;
                Obj4RMove.Value = (decimal)list[3].MoveAmountR;
                Obj5RMove.Value = (decimal)list[4].MoveAmountR;
                Obj6RMove.Value = (decimal)list[5].MoveAmountR;
                if (Microscope.Objectives.List.Count == 7)
                    Obj7RMove.Value = (decimal)list[6].MoveAmountR;

                Obj1Focus.Value = (decimal)list[0].FocusMoveAmount;
                Obj2Focus.Value = (decimal)list[1].FocusMoveAmount;
                Obj3Focus.Value = (decimal)list[2].FocusMoveAmount;
                Obj4Focus.Value = (decimal)list[3].FocusMoveAmount;
                Obj5Focus.Value = (decimal)list[4].FocusMoveAmount;
                Obj6Focus.Value = (decimal)list[5].FocusMoveAmount;
                if (Microscope.Objectives.List.Count == 7)
                    Obj7Focus.Value = (decimal)list[6].FocusMoveAmount;


                Obj1Width.Value = (decimal)list[0].ViewWidth;
                Obj2Width.Value = (decimal)list[1].ViewWidth;
                Obj3Width.Value = (decimal)list[2].ViewWidth;
                Obj4Width.Value = (decimal)list[3].ViewWidth;
                Obj5Width.Value = (decimal)list[4].ViewWidth;
                Obj6Width.Value = (decimal)list[5].ViewWidth;
                if (Microscope.Objectives.List.Count == 7)
                    Obj7Width.Value = (decimal)list[6].ViewWidth;

                Obj1Height.Value = (decimal)list[0].ViewHeight;
                Obj2Height.Value = (decimal)list[1].ViewHeight;
                Obj3Height.Value = (decimal)list[2].ViewHeight;
                Obj4Height.Value = (decimal)list[3].ViewHeight;
                Obj5Height.Value = (decimal)list[4].ViewHeight;
                Obj6Height.Value = (decimal)list[5].ViewHeight;
                if (Microscope.Objectives.List.Count == 7)
                    Obj7Height.Value = (decimal)list[6].ViewHeight;
            }
            UpdateObjectives();
        }

        private void objectiveA1Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].AcquisitionExposure = (int)objectiveA1Box.Value;
        }
        private void objectiveA2Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].AcquisitionExposure = (int)objectiveA2Box.Value;
        }
        private void objectiveA3Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].AcquisitionExposure = (int)objectiveA3Box.Value;
        }
        private void objectiveA4Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].AcquisitionExposure = (int)objectiveA4Box.Value;
        }
        private void objectiveA5Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].AcquisitionExposure = (int)objectiveA5Box.Value;
        }
        private void objectiveA6Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].AcquisitionExposure = (int)objectiveA6Box.Value;
        }
        private void objectiveA7Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].AcquisitionExposure = (int)objectiveA7Box.Value;
        }
        private void objectiveL1Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].LocateExposure = (int)objectiveL1Box.Value;
        }
        private void objectiveL2Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].LocateExposure = (int)objectiveL2Box.Value;
        }
        private void objectiveL3Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].LocateExposure = (int)objectiveL3Box.Value;
        }
        private void objectiveL4Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].LocateExposure = (int)objectiveL4Box.Value;
        }
        private void objectiveL5Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].LocateExposure = (int)objectiveL5Box.Value;
        }
        private void objectiveL6Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].LocateExposure = (int)objectiveL6Box.Value;
        }
        private void objectiveL7Box_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].LocateExposure = (int)objectiveL7Box.Value;
        }
        private void Obj1RMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].MoveAmountR = (double)Obj1RMove.Value;
        }
        private void Obj2RMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].MoveAmountR = (double)Obj2RMove.Value;
        }
        private void Obj3RMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].MoveAmountR = (double)Obj3RMove.Value;
        }
        private void Obj4RMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].MoveAmountR = (double)Obj4RMove.Value;
        }
        private void Obj5RMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].MoveAmountR = (double)Obj5RMove.Value;
        }
        private void Obj6RMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].MoveAmountR = (double)Obj6RMove.Value;
        }
        private void Obj7RMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].MoveAmountR = (double)Obj7RMove.Value;
        }
        private void Obj1LMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].MoveAmountL = (double)Obj1LMove.Value;
        }
        private void Obj2LMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].MoveAmountL = (double)Obj2LMove.Value;
        }
        private void Obj3LMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].MoveAmountL = (double)Obj3LMove.Value;
        }
        private void Obj4LMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].MoveAmountL = (double)Obj4LMove.Value;
        }
        private void Obj5LMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].MoveAmountL = (double)Obj5LMove.Value;
        }
        private void Obj6LMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].MoveAmountL = (double)Obj6LMove.Value;
        }
        private void Obj7LMove_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].MoveAmountL = (double)Obj7LMove.Value;
        }
        private void Obj1Focus_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].FocusMoveAmount = (double)Obj1Focus.Value;
        }
        private void Obj2Focus_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].FocusMoveAmount = (double)Obj2Focus.Value;
        }
        private void Obj3Focus_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].FocusMoveAmount = (double)Obj3Focus.Value;
        }
        private void Obj4Focus_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].FocusMoveAmount = (double)Obj4Focus.Value;
        }
        private void Obj5Focus_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].FocusMoveAmount = (double)Obj5Focus.Value;
        }
        private void Obj6Focus_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].FocusMoveAmount = (double)Obj6Focus.Value;
        }
        private void Obj7Focus_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].FocusMoveAmount = (double)Obj7Focus.Value;
        }
        private void Obj1Width_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].ViewWidth = (double)Obj1Width.Value;
        }
        private void Obj2Width_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].ViewWidth = (double)Obj2Width.Value;
        }
        private void Obj3Width_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].ViewWidth = (double)Obj3Width.Value;
        }
        private void Obj4Width_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].ViewWidth = (double)Obj4Width.Value;
        }
        private void Obj5Width_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].ViewWidth = (double)Obj5Width.Value;
        }
        private void Obj6Width_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].ViewWidth = (double)Obj6Width.Value;
        }
        private void Obj7Width_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].ViewWidth = (double)Obj7Width.Value;
        }
        private void Obj1Height_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].ViewHeight = (double)Obj1Height.Value;
        }
        private void Obj2Height_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].ViewHeight = (double)Obj2Height.Value;
        }
        private void Obj3Height_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].ViewHeight = (double)Obj3Height.Value;
        }
        private void Obj4Height_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].ViewHeight = (double)Obj4Height.Value;
        }
        private void Obj5Height_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].ViewHeight = (double)Obj5Height.Value;
        }
        private void Obj6Height_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].ViewHeight = (double)Obj6Height.Value;
        }
        private void Obj7Height_ValueChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].ViewHeight = (double)Obj7Height.Value;
        }
        private void obj1Name_TextChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[0].Name = obj1Name.Text;
        }
        private void obj2Name_TextChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[1].Name = obj2Name.Text;
        }
        private void obj3Name_TextChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[2].Name = obj3Name.Text;
        }
        private void obj4Name_TextChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[3].Name = obj4Name.Text;
        }
        private void obj5Name_TextChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[4].Name = obj5Name.Text;
        }
        private void obj6Name_TextChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[5].Name = obj6Name.Text;
        }
        private void obj7Name_TextChanged(object sender, EventArgs e)
        {
            Microscope.Objectives.List[6].Name = obj7Name.Text;
        }

        private void UpdateObjectives()
        {

            Microscope.Objectives.List[0].Name = obj1Name.Text;
            Microscope.Objectives.List[1].Name = obj2Name.Text;
            Microscope.Objectives.List[2].Name = obj3Name.Text;
            Microscope.Objectives.List[3].Name = obj4Name.Text;
            Microscope.Objectives.List[4].Name = obj5Name.Text;
            Microscope.Objectives.List[5].Name = obj6Name.Text;
            if (Microscope.Objectives.List.Count == 7)
                Microscope.Objectives.List[6].Name = obj7Name.Text;

            Microscope.Objectives.List[0].AcquisitionExposure = (double)objectiveA1Box.Value;
            Microscope.Objectives.List[1].AcquisitionExposure = (double)objectiveA2Box.Value;
            Microscope.Objectives.List[2].AcquisitionExposure = (double)objectiveA3Box.Value;
            Microscope.Objectives.List[3].AcquisitionExposure = (double)objectiveA4Box.Value;
            Microscope.Objectives.List[4].AcquisitionExposure = (double)objectiveA5Box.Value;
            Microscope.Objectives.List[5].AcquisitionExposure = (double)objectiveA6Box.Value;

            Microscope.Objectives.List[0].LocateExposure = (double)objectiveL1Box.Value;
            Microscope.Objectives.List[1].LocateExposure = (double)objectiveL2Box.Value;
            Microscope.Objectives.List[2].LocateExposure = (double)objectiveL3Box.Value;
            Microscope.Objectives.List[3].LocateExposure = (double)objectiveL4Box.Value;
            Microscope.Objectives.List[4].LocateExposure = (double)objectiveL5Box.Value;
            Microscope.Objectives.List[5].LocateExposure = (double)objectiveL6Box.Value;
            if (Microscope.Objectives.List.Count == 7)
                Microscope.Objectives.List[6].LocateExposure = (double)objectiveL7Box.Value;

            Microscope.Objectives.List[0].ViewWidth = (double)Obj1Width.Value;
            Microscope.Objectives.List[1].ViewWidth = (double)Obj2Width.Value;
            Microscope.Objectives.List[2].ViewWidth = (double)Obj3Width.Value;
            Microscope.Objectives.List[3].ViewWidth = (double)Obj4Width.Value;
            Microscope.Objectives.List[4].ViewWidth = (double)Obj5Width.Value;
            Microscope.Objectives.List[5].ViewWidth = (double)Obj6Width.Value;
            if (Microscope.Objectives.List.Count == 7)
                Microscope.Objectives.List[6].ViewWidth = (double)Obj7Width.Value;

            Microscope.Objectives.List[0].ViewHeight = (double)Obj1Height.Value;
            Microscope.Objectives.List[1].ViewHeight = (double)Obj2Height.Value;
            Microscope.Objectives.List[2].ViewHeight = (double)Obj3Height.Value;
            Microscope.Objectives.List[3].ViewHeight = (double)Obj4Height.Value;
            Microscope.Objectives.List[4].ViewHeight = (double)Obj5Height.Value;
            Microscope.Objectives.List[5].ViewHeight = (double)Obj6Height.Value;
            if (Microscope.Objectives.List.Count == 7)
                Microscope.Objectives.List[6].ViewWidth = (double)Obj7Width.Value;

            Microscope.Objectives.List[0].MoveAmountL = (double)Obj1LMove.Value;
            Microscope.Objectives.List[1].MoveAmountL = (double)Obj2LMove.Value;
            Microscope.Objectives.List[2].MoveAmountL = (double)Obj3LMove.Value;
            Microscope.Objectives.List[3].MoveAmountL = (double)Obj4LMove.Value;
            Microscope.Objectives.List[4].MoveAmountL = (double)Obj5LMove.Value;
            Microscope.Objectives.List[5].MoveAmountL = (double)Obj6LMove.Value;
            if (Microscope.Objectives.List.Count == 7)
                Microscope.Objectives.List[5].MoveAmountL = (double)Obj6LMove.Value;

            Microscope.Objectives.List[0].MoveAmountR = (double)Obj1RMove.Value;
            Microscope.Objectives.List[1].MoveAmountR = (double)Obj2RMove.Value;
            Microscope.Objectives.List[2].MoveAmountR = (double)Obj3RMove.Value;
            Microscope.Objectives.List[3].MoveAmountR = (double)Obj4RMove.Value;
            Microscope.Objectives.List[4].MoveAmountR = (double)Obj5RMove.Value;
            Microscope.Objectives.List[5].MoveAmountR = (double)Obj6RMove.Value;
            if (Microscope.Objectives.List.Count == 7)
                Microscope.Objectives.List[5].MoveAmountR = (double)Obj6RMove.Value;

            Microscope.Objectives.List[0].FocusMoveAmount = (double)Obj1Focus.Value;
            Microscope.Objectives.List[1].FocusMoveAmount = (double)Obj2Focus.Value;
            Microscope.Objectives.List[2].FocusMoveAmount = (double)Obj3Focus.Value;
            Microscope.Objectives.List[3].FocusMoveAmount = (double)Obj4Focus.Value;
            Microscope.Objectives.List[4].FocusMoveAmount = (double)Obj5Focus.Value;
            Microscope.Objectives.List[5].FocusMoveAmount = (double)Obj6Focus.Value;
            if (Microscope.Objectives.List.Count == 7)
                Microscope.Objectives.List[6].FocusMoveAmount = (double)Obj7Focus.Value;
        }
        private void MicroscopeSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            string j = JsonConvert.SerializeObject(Microscope.Objectives.List, Formatting.None);
            System.IO.File.WriteAllText(Application.StartupPath + "/Config/MicroscopeObjectives.json", j);
            Properties.Settings.Default.Save();
            e.Cancel = true;
            this.Hide();
            UpdateObjectives();
        }

        private void setImageBut_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Properties.Settings.Default.SimulatedImage = openFileDialog.FileName;
        }

        private void simCameraBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SimulateCamera = simCameraBox.Checked;
        }
        public static bool SettingExist(string settingName)
        {
            return Properties.Settings.Default.Properties.Cast<SettingsProperty>().Any(prop => prop.Name == settingName);
        }

        private void SetGetProperty(object sender, EventArgs e)
        {
            string name = (string)((Button)sender).Tag;
            Function f = new Function();
            f.Name = "Get" + name;
            FunctionForm form = new FunctionForm(f);
            if (form.ShowDialog() != DialogResult.OK)
                return;
            if (!SettingExist(name))
            {
                SettingsProperty sp = new SettingsProperty(name);
                sp.DefaultValue = "Set" + form.Func.File;
                Properties.Settings.Default.Properties.Add(sp);
            }
            else
                Properties.Settings.Default[name] = form.Func.File;
            form.Dispose();
        }

        private void SetProperty(object sender, EventArgs e)
        {
            string name = (string)((Button)sender).Tag;
            Function f = new Function();
            f.Name = "Set" + name;
            FunctionForm form = new FunctionForm(f);
            if (form.ShowDialog() != DialogResult.OK)
                return;
            if (!SettingExist(name))
            {
                SettingsProperty sp = new SettingsProperty(name);
                sp.DefaultValue = "Get" + form.Func.File;
                Properties.Settings.Default.Properties.Add(sp);
            }
            else
                Properties.Settings.Default[name] = form.Func.File;
            form.Dispose();
        }

        private void useLibBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void setLibBut_Click(object sender, EventArgs e)
        {
            App.lib.Show();
        }
    }
}
