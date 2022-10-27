using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Automation;
using SharpDX.XInput;
using WindowsInput;
using WindowsInput.Native;
using System.Configuration;
using Bio.Properties;
using System.Reflection;


namespace Bio
{
    public partial class Imager : Form
    {
        private static Process AppP = null;
        public static IntPtr apph;
        private Point mainWinDefLocation = new Point(1473,509);
        public static string AppPath = "";
        public static List<BioImage> StoredImages = new List<BioImage>();
        public Controller controller;
        public Gamepad gamepad, previousGamepadState;
        public bool prevState = false;
        public bool connected = false;
        public float leftTrigger, rightTrigger;
        public int deadzone = 5;

        public string m_ID;
        public string ID => this.m_ID;
        public static Process ZenP
        {
            get
            {
                return AppP;
            }
            set
            {
                AppP = value;
            }
        }
        public uint ImagesPerSlice
        {
            get
            {
                return (uint)imagesPerSliceBox.Value;
            }
            set
            {
                imagesPerSliceBox.Value = (decimal)value;
            }
        }
        public uint SliceIncrement
        {
            get
            {
                return (uint)sliceIncrementBox.Value;
            }
            set
            {
                sliceIncrementBox.Value = (decimal)value;
            }
        }
        public uint SlicesPerSlide
        {
            get
            {
                return (uint)slicesPerSlideBox.Value;
            }
            set
            {
                slicesPerSlideBox.Value = (decimal)value;
            }
        }

        public Profile CurrentProfile = new Profile("Default");
        public class Profile
        {
            public string FilePath = "";
            public string Name = "";
            public string DirName = "";
            public void Save(string path)
            {
                List<string> sts = new List<string>();
                foreach (SettingsProperty currentProperty in Settings.Default.Properties)
                {
                    string s = currentProperty.Name + "=" + Settings.Default[currentProperty.Name].ToString();
                    sts.Add(s);
                }
                File.WriteAllLines(path,sts.ToArray());
            }

            public Profile(string path)
            {
                FilePath = path;
                if (path != "Default")
                {
                    Name = Path.GetFileNameWithoutExtension(FilePath);
                    DirName = Path.GetDirectoryName(FilePath);
                }
            }

            public void Load()
            {
                string[] sts = File.ReadAllLines(FilePath);
                foreach (string s in sts)
                {
                    string[] ss = s.Split('=');
                    if (Settings.Default[ss[0]].GetType() == typeof(double))
                    {
                        Settings.Default[ss[0]] = double.Parse(ss[1]);
                    }
                    else
                        if (Settings.Default[ss[0]].GetType() == typeof(string))
                    {
                        Settings.Default[ss[0]] = ss[1];
                    }
                    else
                        throw new ArgumentException();
                }
            }

            public override string ToString()
            {
                return Name + " " + DirName;
            }

        }
        public Imager()
        {
            InitializeComponent();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            TimeSpan st1 = timer.Elapsed;
            m_ID = "";

            //We start the imaging application making sure the we have the right path to the executable.
            AppPath = Settings.Default.AppPath;
            if (!File.Exists(AppPath))
            {
                MessageBox.Show("Error!","Imaging appplication not found. Please locate the executable.");
                //path is not default. Let the user find it.
                if (openExeFileDialog.ShowDialog() != DialogResult.OK)
                    this.Close();
                Settings.Default.AppPath = openExeFileDialog.FileName;
            }
            string pn = Path.GetFileNameWithoutExtension(AppPath);
            Process[] prs = Process.GetProcessesByName(pn);
            if(prs.Length==0)
            {
                AppP = Process.Start(AppPath);
            }
            else
                AppP = prs[0];
            apph = AppP.MainWindowHandle;
            AppP.WaitForInputIdle();
            statusTimer.Start();
           
            for (UserIndex i = UserIndex.One; i <= UserIndex.Four; i++)
            {
                controller = new Controller(i);
                if (controller.IsConnected)
                {
                    connected = true;
                    break;
                }
                else controller = null;
            }

            ControllerFuncs.LoadSettings();

            controllerJoystickUpdate.Start();

            controllerButtonUpdate.Start();

            
            if (!Directory.Exists(Application.StartupPath + "\\Profiles"))
                Directory.CreateDirectory(Application.StartupPath + "\\Profiles");
            string[] sts = Directory.GetFiles(Application.StartupPath + "\\Profiles");
            foreach (string s in sts)
            {
                Profile p = new Profile(s);
                profilesBox.Items.Add(p);
            }

            if (Settings.Default.PreviousProfile != "Default")
            {
                CurrentProfile = new Profile(Settings.Default.PreviousProfile);
                CurrentProfile.Load();
                int index = 0;
                foreach (Profile item in profilesBox.Items)
                {
                    if (item.FilePath == Settings.Default.PreviousProfile)
                    {
                        profilesBox.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
            this.TopMost = topMostCheckBox.Checked;

            if (dockToApp.Checked)
            {
                //We set window location based on ZEN®
                Win32.Rect r = new Win32.Rect();
                Win32.GetWindowRect(apph, ref r);
                Point pp = new Point(r.Right - this.Width, r.Bottom - this.Height - 23);
                this.Location = pp;
            }
            this.Show();
        }

        public void UpdateStatus()
        {
            //GetLiveStatus();
            if (AppP.HasExited)
                return;
            Point3D p = Microscope.GetPosition();
            double f = Microscope.GetFocus();
            Point3D point = new Point3D(p.X, p.Y, f);
            statusLabel1.Text = "Stage: " + p.X + " , " + p.Y + " Focus: " + f;
            if(StoredImages.Count > storedImageBox.Items.Count)
            {
                int dif = StoredImages.Count - storedImageBox.Items.Count;
                for (int i = 0; i < dif; i++)
                {
                    int index = (StoredImages.Count - 1) + dif;
                    storedImageBox.Items.Add(StoredImages[index-1]);
                }
            }
        }

        public void StoreCoordinate()
        {
            Point3D p3 = Microscope.GetPosition();
            storedCoordsBox.Items.Add(p3);
            storedCoordsBox.SelectedIndex = storedCoordsBox.Items.Count - 1;
        }
 
        public static bool? liveStatus = null;
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(App.viewer != null)
            Properties.Settings.Default.Origin = App.viewer.Origin.ToString();
            Settings.Default.PreviousProfile = CurrentProfile.FilePath;
            Settings.Default.Save();
            if(CurrentProfile.FilePath!="Default")
            CurrentProfile.Save(CurrentProfile.FilePath);

            if (AppP.HasExited)
            {
                //Old Zen process has exited meaning we need to restart to initialize GUI automation elements.
                Application.Restart();
            }
            else
            Application.Exit();
        }
        private void deadzoneBox_ValueChanged(object sender, EventArgs e)
        {
            deadzone = (int)deadzoneBox.Value;
        }

        private void storedCoordsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Point3D p = (Point3D)storedCoordsBox.SelectedItem;
            xCoordBox.Value = (decimal)p.X;
            yCoordBox.Value = (decimal)p.Y;
            zCoordBox.Value = (decimal)p.Z;
        }

        private void addCoordBut_Click(object sender, EventArgs e)
        {
            StoreCoordinate();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storedImageBox.SelectedIndex == -1)
                return;
            BioImage s = (BioImage)storedImageBox.SelectedItem;
            Microscope.SetPosition(s.Volume.Location);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storedImageBox.SelectedIndex == -1)
                return;
            storedImageBox.Items.RemoveAt(storedImageBox.SelectedIndex);
            storedImageBox.SelectedIndex = storedImageBox.Items.Count-1;
        }

        private void clearBut_Click(object sender, EventArgs e)
        {
            //We stop the controller update time so we can clear the list it access without causing problems
            storedCoordsBox.Items.Clear();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            storedImageBox.Items.Clear();
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            storedCoordsBox.Items.Clear();
        }

        private void goToToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Point3D p = (Point3D)storedCoordsBox.SelectedItem;
            Microscope.SetPosition(p);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (storedCoordsBox.SelectedIndex == -1)
                return;
            storedCoordsBox.Items.RemoveAt(storedCoordsBox.SelectedIndex);
            storedCoordsBox.SelectedIndex = storedCoordsBox.Items.Count - 1;
        }

        private void controllerJoystickUpdate_Tick(object sender, EventArgs e)
        {
            if (AppP.HasExited)
            {
                return;
            }
            //Here we check the joysticks etc. which should have a high refresh rate to ensure smoothness.
            if (!connected)
                return;

            gamepad = controller.GetState().Gamepad;
            
            //we set the triggers to be percent
            leftTrigger  = ((float)(gamepad.LeftTrigger)/255)*100;
            rightTrigger = ((float)(gamepad.RightTrigger)/255)*100;

            #region//TRIGGERS
            if (rightTrigger > 0)
            {
                //We use MTBCmdSetModes.Relative | MTBCmdSetModes.Synchronous so that we wait till the operation finishes
                double curpos = Microscope.GetFocus();
                Microscope.SetFocus((((double)triggerFocusBox.Value) * rightTrigger) + curpos);
            }

            if (leftTrigger > 0)
            {
                double curpos = Microscope.GetFocus();
                Microscope.SetFocus((-1 * (((double)triggerFocusBox.Value) * leftTrigger)) + curpos);
            }
            #endregion

            #region//JOYSTICKS
            //LEFT JOYSTICKS
            double maxVal = ushort.MaxValue / 2;
            double minVal = maxVal * -1;
            if (gamepad.LeftThumbX > deadzone)
                Microscope.MoveRight((((float)gamepad.LeftThumbX) / maxVal) * (float)LJoystickStageMoveAmountBox.Value);
            if (gamepad.LeftThumbX < -deadzone)
                Microscope.MoveLeft((((float)gamepad.LeftThumbX) / minVal) * (float)LJoystickStageMoveAmountBox.Value);
            if (gamepad.LeftThumbY < -deadzone)
                Microscope.MoveUp((((float)gamepad.LeftThumbY) / maxVal) * (float)LJoystickStageMoveAmountBox.Value);
            if (gamepad.LeftThumbY > deadzone)
                Microscope.MoveDown((((float)gamepad.LeftThumbY) / minVal) * (float)LJoystickStageMoveAmountBox.Value);

            //RIGHT JOYSTICK
            if (gamepad.RightThumbX > deadzone)
                Microscope.MoveRight(((float)gamepad.RightThumbX / maxVal) * (float)RJoystickStageMoveAmountBox.Value);
            if (gamepad.RightThumbX < -deadzone)
                Microscope.MoveLeft((((float)gamepad.RightThumbX) / minVal) * (float)RJoystickStageMoveAmountBox.Value);
            if (gamepad.RightThumbY < -deadzone)
                Microscope.MoveUp((((float)gamepad.RightThumbY) / maxVal) * (float)RJoystickStageMoveAmountBox.Value);
            if (gamepad.RightThumbY > deadzone)
                Microscope.MoveDown((((float)gamepad.RightThumbY) / minVal) * (float)RJoystickStageMoveAmountBox.Value);
            #endregion

            LJoystickLabel.Text = "X: " + gamepad.LeftThumbX.ToString("0.000") + " Y:" + gamepad.LeftThumbY.ToString("0.000");
            RJoystickLabel.Text = "X: " + gamepad.RightThumbX.ToString("0.000") + " Y:" + gamepad.RightThumbY.ToString("0.000");

            leftTriggerLabel.Text = leftTrigger.ToString();
            rightTriggerLabel.Text = rightTrigger.ToString();
            
            //We update distance labels
            if(storedCoordsBox.Items.Count>0 && storedCoordsBox.SelectedIndex!=-1)
            {
                Point3D p0 = (Point3D)storedCoordsBox.Items[storedCoordsBox.Items.Count - 1];
                Point3D p1 = Microscope.GetPosition();
                lastCoordDistanceLabel.Text = Point3D.Distance(p0, p1).ToString();

                Point3D pp0 = (Point3D)storedCoordsBox.Items[storedCoordsBox.SelectedIndex];
                Point3D pp1 = Microscope.GetPosition();
                lastSelectedCoordsLabel.Text = Point3D.Distance(pp0, pp1).ToString();
            }

            if (storedImageBox.Items.Count > 0 && storedImageBox.SelectedIndex != -1)
            {
                BioImage s = (BioImage)storedImageBox.Items[storedImageBox.Items.Count - 1];
                Point3D p0 = s.Volume.Location;
                Point3D p1 = Microscope.GetPosition();
                lastSnapDistanceLabel.Text = Point3D.Distance(p0, p1).ToString();

                BioImage ss = (BioImage)storedImageBox.Items[storedImageBox.SelectedIndex];
                Point3D pp0 = ss.Volume.Location;
                Point3D pp1 = Microscope.GetPosition();
                lastSelectedSnapDistanceLabel.Text = Point3D.Distance(pp0, pp1).ToString();
            }
            if(App.viewer!=null)
            App.viewer.UpdateView();
        }

        private void controllerButtonUpdate_Tick(object sender, EventArgs e)
        {
            if (AppP.HasExited)
                return;
            //AppP.WaitForInputIdle();
            //Here we check buttons etc. which will have a lower refresh rate to ensure that we do not repeat commands
            if (!connected)
                return;
            
            gamepad = controller.GetState().Gamepad;

            if (prevState)
                CheckGamepadButtons(gamepad, previousGamepadState);

            previousGamepadState = gamepad;
            prevState = true;
        }

        public static int DigitsInString(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0') && (str[i] <= '9'))
                {
                    count++;
                }
            }
            return count;
        }

        bool init = false;
        Process[] prs;
        private void statusTimer_Tick(object sender, EventArgs e)
        {            
            if (dockToApp.Checked)
            {
                //We set window location based on imaging app location.
                Win32.Rect r = new Win32.Rect();
                Win32.GetWindowRect(apph, ref r);
                Point pp = new Point((r.Right) - this.Width,(r.Bottom) - this.Height - 30);
                this.Location = pp;
            }
            string s = Win32.GetActiveWindowTitle();
            if (s != null)
            if (s.Contains(Settings.Default.AppName))
            {
                if (dockToApp.Checked)
                    Show();
                else
                {
                    if (!s.Contains("Imager"))
                    {
                        if (dockToApp.Checked)
                            Hide();
                    }
                }
            }

        }
        
        #region Labels Double Clicked

        private void YPressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.Y,"YButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.Y = form.Func;
            Settings.Default.Y = ControllerFuncs.Y.ToString();
            Settings.Default.Save();
        }

        private void BPressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.B, "BButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.B = form.Func;
            Settings.Default.B = ControllerFuncs.B.ToString();
            Settings.Default.Save();
        }

        private void APressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.A, "AButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.A = form.Func;
            Settings.Default.A = ControllerFuncs.A.ToString();
            Settings.Default.Save();
        }

        private void XPressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.X, "XButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.X = form.Func;
            Settings.Default.X = ControllerFuncs.X.ToString();
            Settings.Default.Save();
        }

        private void BackPressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.Back, "BackButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.Back = form.Func;
            Settings.Default.Back = ControllerFuncs.Back.ToString();
            Settings.Default.Save();
        }

        private void StartPressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.Start, "StartButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            Settings.Default.Start = ControllerFuncs.Start.ToString();
            Settings.Default.Save();
        }

        private void DPadUpLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.DPadUp, "DPadUpButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.DPadUp = form.Func;
            Settings.Default.DPadUp = ControllerFuncs.DPadUp.ToString();
            Settings.Default.Save();
        }

        private void DPadRightLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.DPadRight, "DPadRightButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.DPadRight = form.Func;
            Settings.Default.DPadRight = ControllerFuncs.DPadRight.ToString();
            Settings.Default.Save();
        }

        private void DPadDownLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.DPadDown, "DPadDownButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.DPadDown = form.Func;
            Settings.Default.DPadDown = ControllerFuncs.DPadDown.ToString();
            Settings.Default.Save();
        }

        private void DPadLeftLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.DPadLeft, "DPadLeftButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.DPadLeft = form.Func;
            Settings.Default.DPadLeft = ControllerFuncs.DPadLeft.ToString();
            Settings.Default.Save();
        }

        private void shoulderLeftLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.LShoulder, "ShoulderLeftButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.LShoulder = form.Func;
            Settings.Default.LShoulder = ControllerFuncs.LShoulder.ToString();
            Settings.Default.Save();
        }

        private void shoulderRightLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.RShoulder, "ShoulderRightButton");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.RShoulder = form.Func;
            Settings.Default.RShoulder = ControllerFuncs.RShoulder.ToString();
            Settings.Default.Save();
        }

        private void r3PressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.R3, "R3");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            ControllerFuncs.R3 = form.Func;
            Settings.Default.Right3 = ControllerFuncs.R3.ToString();
            Settings.Default.Save();
        }

        private void l3PressedLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FunctionForm form = new FunctionForm(ControllerFuncs.L3, "L3");
            if (form.ShowDialog() != DialogResult.OK)
                return;
            Settings.Default.Left3 = ControllerFuncs.L3.ToString();
            Settings.Default.Save();
        }

        #endregion

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storedImageBox.SelectedItem == null)
                return;
            BioImage s = (BioImage)storedImageBox.SelectedItem;
            if (s.Filename == "")
                return;
            else
                Process.Start(s.Filename);
        }

        private void openProfileBut_Click(object sender, EventArgs e)
        {
            openProfileFileDialog.InitialDirectory = Application.StartupPath + "\\Profiles";
            if (openProfileFileDialog.ShowDialog() != DialogResult.OK)
                return;
            CurrentProfile = new Profile(openProfileFileDialog.FileName);
            CurrentProfile.Load();
            profilesBox.Items.Add(CurrentProfile);
            int index = 0;
            foreach (Profile item in profilesBox.Items)
            {
                if (item.FilePath == CurrentProfile.FilePath)
                {
                    profilesBox.SelectedIndex = index;
                    break;
                }
                index++;
            }
        }

        private void profilesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (profilesBox.SelectedIndex == -1)
                return;
            if (profilesBox.Text == "Default" || profilesBox.SelectedItem.ToString() == "Default")
                return;
            CurrentProfile = (Profile)profilesBox.SelectedItem;
        }

        private void saveProfileBut_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Profiles\\";
            if (profilesBox.Text != "")
                saveFileDialog.FileName = profilesBox.Text;

            if(profilesBox.SelectedItem != null)
            if (profilesBox.SelectedItem.ToString() == "Default" || profilesBox.Text == "Default")
            {
                MessageBox.Show("You are saving the default profile, are you sure you want to do this?", "NOTICE!");
                Settings.Default.Save();
                return;
            }
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            CurrentProfile.Save(saveFileDialog.FileName);
        }

        private void clearSnapsBut_Click(object sender, EventArgs e)
        {
            storedImageBox.Items.Clear();
        }

        private void storedSnapBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (storedImageBox.SelectedItem == null)
                return;
            BioImage s = (BioImage)storedImageBox.SelectedItem;
            xCoordBox.Value = (decimal)s.Volume.Location.X;
            yCoordBox.Value = (decimal)s.Volume.Location.Y;
            zCoordBox.Value = (decimal)s.Volume.Location.Z;

        }
        private void goToLocBox_Click(object sender, EventArgs e)
        {
            Point3D p = new Point3D((double)xCoordBox.Value, (double)yCoordBox.Value, (double)zCoordBox.Value);
            Microscope.SetPosition(p);
        }
        private void tabContextMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void borderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.FormBorderStyle == FormBorderStyle.Sizable)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                borderToolStripMenuItem.Text = "Window Border Normal";
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                borderToolStripMenuItem.Text = "Window Border None";
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openInImageViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (storedImageBox.SelectedIndex == -1)
                return;
            BioImage s = (BioImage)storedImageBox.SelectedItem;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control && mainTabControl.SelectedIndex == 3)
            {
                if (saveImageDialog.ShowDialog() != DialogResult.OK)
                    return;
                //imageview.image.Save(saveImageDialog.FileName);
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(storedImageBox.SelectedItem.ToString());
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(storedCoordsBox.SelectedItem.ToString());
        }

        private void dockToApp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void topMostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = topMostCheckBox.Checked;
        }

        public void CheckGamepadButtons(Gamepad state, Gamepad prevState)
        {
            #region YBAX Keys
            if (ControllerFuncs.Y.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.Y && state.Buttons != GamepadButtonFlags.Y)
                {
                    PerformFunction(ControllerFuncs.Y);
                    prevState.Buttons = GamepadButtonFlags.None;
                    YPressedLabel.Text = "Y: released";
                }
                else
                    YPressedLabel.Text = "Y: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.Y)
                {
                    PerformFunction(ControllerFuncs.Y);
                    prevState.Buttons = GamepadButtonFlags.None;
                    YPressedLabel.Text = "Y: pressed";
                }
                else
                    YPressedLabel.Text = "Y: not pressed";
            }
            if (ControllerFuncs.B.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.B && state.Buttons != GamepadButtonFlags.B)
                {
                    PerformFunction(ControllerFuncs.B);
                    prevState.Buttons = GamepadButtonFlags.None;
                    state.Buttons = GamepadButtonFlags.None;
                    BPressedLabel.Text = "B: released";
                }
                else
                    BPressedLabel.Text = "B: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.B)
                {
                    PerformFunction(ControllerFuncs.B);
                    prevState.Buttons = GamepadButtonFlags.None;
                    BPressedLabel.Text = "B: pressed";
                }
                else
                    BPressedLabel.Text = "B: not pressed";
            }
            if (ControllerFuncs.A.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.A && state.Buttons != GamepadButtonFlags.A)
                {
                    PerformFunction(ControllerFuncs.A);
                    prevState.Buttons = GamepadButtonFlags.None;
                    APressedLabel.Text = "A: released";
                }
                else
                    APressedLabel.Text = "A: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.A)
                {
                    PerformFunction(ControllerFuncs.A);
                    prevState.Buttons = GamepadButtonFlags.None;
                    APressedLabel.Text = "A: pressed";
                }
                else
                    APressedLabel.Text = "A: not pressed";
            }
            if (ControllerFuncs.X.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.X && state.Buttons != GamepadButtonFlags.X)
                {
                    PerformFunction(ControllerFuncs.X);
                    prevState.Buttons = GamepadButtonFlags.None;
                    XPressedLabel.Text = "X: released";
                }
                else
                    XPressedLabel.Text = "X: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.X)
                {
                    PerformFunction(ControllerFuncs.X);
                    prevState.Buttons = GamepadButtonFlags.None;
                    XPressedLabel.Text = "X: pressed";
                }
                else
                    XPressedLabel.Text = "X: not pressed";
            }
            #endregion

            #region Back & Start Keys
            if (ControllerFuncs.Start.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.Start && state.Buttons != GamepadButtonFlags.Start)
                {
                    PerformFunction(ControllerFuncs.Start);
                    prevState.Buttons = GamepadButtonFlags.None;
                    StartPressedLabel.Text = "Start: released";
                }
                else
                    StartPressedLabel.Text = "Start: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.Start)
                {
                    PerformFunction(ControllerFuncs.Start);
                    prevState.Buttons = GamepadButtonFlags.None;
                    StartPressedLabel.Text = "Start: pressed";
                }
                else
                    StartPressedLabel.Text = "Start: not pressed";
            }
            if (ControllerFuncs.Back.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.Back && state.Buttons != GamepadButtonFlags.Back)
                {
                    PerformFunction(ControllerFuncs.Back);
                    prevState.Buttons = GamepadButtonFlags.None;
                    BackPressedLabel.Text = "Back: released";
                }
                else
                    BackPressedLabel.Text = "Back: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.Back)
                {
                    PerformFunction(ControllerFuncs.Back);
                    prevState.Buttons = GamepadButtonFlags.None;
                    BackPressedLabel.Text = "Back: pressed";
                }
                else
                    BackPressedLabel.Text = "Back: not pressed";
            }
            #endregion

            #region Shoulders
            if (ControllerFuncs.RShoulder.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.RightShoulder && state.Buttons != GamepadButtonFlags.RightShoulder)
                {
                    PerformFunction(ControllerFuncs.RShoulder);
                    prevState.Buttons = GamepadButtonFlags.None;
                    shoulderRightLabel.Text = "Right: released";
                }
                else
                    shoulderRightLabel.Text = "Right: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.RightShoulder)
                {
                    PerformFunction(ControllerFuncs.RShoulder);
                    prevState.Buttons = GamepadButtonFlags.None;
                    shoulderRightLabel.Text = "Right: pressed";
                }
                else
                    shoulderRightLabel.Text = "Right: not pressed";
            }
            if (ControllerFuncs.LShoulder.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.LeftShoulder && state.Buttons != GamepadButtonFlags.LeftShoulder)
                {
                    PerformFunction(ControllerFuncs.RShoulder);
                    prevState.Buttons = GamepadButtonFlags.None;
                    shoulderLeftLabel.Text = "Left: released";
                }
                else
                    shoulderLeftLabel.Text = "Left: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.LeftShoulder)
                {
                    PerformFunction(ControllerFuncs.LShoulder);
                    prevState.Buttons = GamepadButtonFlags.None;
                    shoulderLeftLabel.Text = "Left: pressed";
                }
                else
                    shoulderLeftLabel.Text = "Left: not pressed";
            }

            #endregion

            #region L3 & R3
            if (ControllerFuncs.R3.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.RightThumb && state.Buttons != GamepadButtonFlags.RightThumb)
                {
                    PerformFunction(ControllerFuncs.R3);
                    prevState.Buttons = GamepadButtonFlags.None;
                    r3PressedLabel.Text = "R3: released";
                }
                else
                    r3PressedLabel.Text = "R3: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.RightThumb)
                {
                    PerformFunction(ControllerFuncs.R3);
                    prevState.Buttons = GamepadButtonFlags.None;
                    r3PressedLabel.Text = "R3: pressed";
                }
                else
                    r3PressedLabel.Text = "R3: not pressed";
            }

            if (ControllerFuncs.L3.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.LeftThumb && state.Buttons != GamepadButtonFlags.LeftThumb)
                {
                    PerformFunction(ControllerFuncs.L3);
                    prevState.Buttons = GamepadButtonFlags.None;
                    l3PressedLabel.Text = "L3: released";
                }
                else
                    l3PressedLabel.Text = "L3: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.LeftThumb)
                {
                    PerformFunction(ControllerFuncs.L3);
                    prevState.Buttons = GamepadButtonFlags.None;
                    l3PressedLabel.Text = "L3: pressed";
                }
                else
                    l3PressedLabel.Text = "L3: not pressed";
            }
            #endregion

            #region DPAD BUTTONS
            if (ControllerFuncs.DPadUp.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.DPadUp && state.Buttons != GamepadButtonFlags.DPadUp)
                {
                    PerformFunction(ControllerFuncs.DPadUp);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadUpLabel.Text = "Up: released";
                }
                else
                    DPadUpLabel.Text = "Up: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.DPadUp)
                {
                    PerformFunction(ControllerFuncs.DPadUp);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadUpLabel.Text = "Up: pressed";
                }
                else
                    DPadUpLabel.Text = "Up: not pressed";
            }

            if (ControllerFuncs.DPadRight.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.DPadRight && state.Buttons != GamepadButtonFlags.DPadRight)
                {
                    PerformFunction(ControllerFuncs.DPadRight);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadRightLabel.Text = "Right: released";
                }
                else
                    DPadRightLabel.Text = "Right: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.DPadRight)
                {
                    PerformFunction(ControllerFuncs.DPadRight);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadRightLabel.Text = "Right: pressed";
                }
                else
                    DPadRightLabel.Text = "Right: not pressed";
            }

            if (ControllerFuncs.DPadDown.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.DPadDown && state.Buttons != GamepadButtonFlags.DPadDown)
                {
                    PerformFunction(ControllerFuncs.DPadDown);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadDownLabel.Text = "Down: released";
                }
                else
                    DPadDownLabel.Text = "Down: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.DPadDown)
                {
                    PerformFunction(ControllerFuncs.DPadDown);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadDownLabel.Text = "Down: pressed";
                }
                else
                    DPadDownLabel.Text = "Down: not pressed";
            }

            if (ControllerFuncs.DPadLeft.State == Function.ButtonState.Released)
            {
                if (prevState.Buttons == GamepadButtonFlags.DPadLeft && state.Buttons != GamepadButtonFlags.DPadLeft)
                {
                    PerformFunction(ControllerFuncs.DPadLeft);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadLeftLabel.Text = "Left: released";
                }
                else
                    DPadLeftLabel.Text = "Left: not pressed";
            }
            else
            {
                if (state.Buttons == GamepadButtonFlags.DPadLeft)
                {
                    PerformFunction(ControllerFuncs.DPadLeft);
                    prevState.Buttons = GamepadButtonFlags.None;
                    DPadLeftLabel.Text = "Left: pressed";
                }
                else
                    DPadLeftLabel.Text = "Left: not pressed";
            }

            #endregion

        }

        public static InputSimulator input = new InputSimulator();

        private void label34_Click(object sender, EventArgs e)
        {

        }

        public object PerformFunction(Function f)
        {
            if (f.FuncType == Function.FunctionType.Key)
            {
                if (AppP.HasExited)
                    return null;
                AppP.WaitForInputIdle();
                Win32.SetForegroundWindow(apph);
                if (f.Modifier != VirtualKeyCode.NONAME)
                {
                    input.Keyboard.ModifiedKeyStroke(f.Modifier, f.Key);
                }
                else
                {
                    input.Keyboard.KeyPress(f.Key);
                }
                return null;
            }
            if (f.FuncType == Function.FunctionType.Objective)
            {
                Objectives.Objective obj = Bio.Microscope.Objectives.GetObjective();
                Microscope.Objectives.SetPosition(obj.Magnification);
                //We set locate exposure as the locate tab is the only one that updates lightpath automatically after objective change. 
                Automation.SetProperty("LocateExposure", obj.LocateExposure.ToString());
                LJoystickStageMoveAmountBox.Value = (decimal)obj.MoveAmountL;
                RJoystickStageMoveAmountBox.Value = (decimal)obj.MoveAmountR;
                triggerFocusBox.Value = (decimal)obj.FocusMoveAmount;
                return null;
            }
            if (f.FuncType == Function.FunctionType.StoreCoordinate)
            {
                StoreCoordinate();
                return null;
            }
            if (f.FuncType == Function.FunctionType.NextCoordinate)
            {
                //If stored coordinates selected index is -1 then nothing is selected.
                //so we can't go to next stored coordinate
                if (storedCoordsBox.SelectedIndex != -1)
                {
                    //We go to the next stored coordinate
                    if (storedCoordsBox.SelectedIndex + 1 < storedCoordsBox.Items.Count)
                    {
                        Point3D p = (Point3D)storedCoordsBox.Items[storedCoordsBox.SelectedIndex + 1];
                        Microscope.SetPosition(p);
                        storedCoordsBox.SelectedIndex = storedCoordsBox.SelectedIndex + 1;
                    }
                    if (storedCoordsBox.SelectedIndex == 0)
                    {
                        Point3D p = (Point3D)storedCoordsBox.Items[storedCoordsBox.SelectedIndex];
                        Microscope.SetPosition(p);
                    }
                }
                return null;
            }
            if (f.FuncType == Function.FunctionType.PreviousCoordinate)
            {
                //We go to the previous stored coordinate unless selected index is 0
                //in which case we go to coordinate of selected point.

                //If stored coordinates selected index is -1 then nothing is selected.
                //so we can't go to next stored coordinate
                if (storedCoordsBox.SelectedIndex != -1)
                {
                    if (storedCoordsBox.SelectedIndex == 0)
                    {
                        Point3D p = (Point3D)storedCoordsBox.Items[storedCoordsBox.SelectedIndex];
                        Microscope.SetPosition(p);
                    }
                    if (storedCoordsBox.SelectedIndex > 0)
                    {
                        Point3D p = (Point3D)storedCoordsBox.Items[storedCoordsBox.SelectedIndex - 1];
                        Microscope.SetPosition(p);
                        storedCoordsBox.SelectedIndex = storedCoordsBox.SelectedIndex - 1;
                    }
                }
                return null;
            }
            if (f.FuncType == Function.FunctionType.NextSnapCoordinate)
            {
                //If stored coordinates selected index is -1 then nothing is selected.
                //so we can't go to next stored coordinate
                if (storedImageBox.SelectedIndex != -1)
                {
                    //We go to the next stored coordinate
                    if (storedImageBox.SelectedIndex + 1 < storedImageBox.Items.Count)
                    {
                        Point3D p = (Point3D)storedImageBox.Items[storedImageBox.SelectedIndex + 1];
                        Microscope.SetPosition(p);
                        storedImageBox.SelectedIndex = storedImageBox.SelectedIndex + 1;
                    }
                    if (storedImageBox.SelectedIndex == 0)
                    {
                        Point3D p = (Point3D)storedImageBox.Items[storedImageBox.SelectedIndex];
                        Microscope.SetPosition(p);
                    }
                }
                return null;
            }
            if (f.FuncType == Function.FunctionType.PreviousSnapCoordinate)
            {
                //We go to the previous stored coordinate unless selected index is 0
                //in which case we go to coordinate of selected point.

                //If stored coordinates selected index is -1 then nothing is selected.
                //so we can't go to next stored coordinate
                if (storedImageBox.SelectedIndex != -1)
                {
                    if (storedImageBox.SelectedIndex == 0)
                    {
                        Point3D p = (Point3D)storedImageBox.Items[storedImageBox.SelectedIndex];
                        Microscope.SetPosition(p);
                    }
                    if (storedImageBox.SelectedIndex > 0)
                    {
                        Point3D p = (Point3D)storedImageBox.Items[storedImageBox.SelectedIndex - 1];
                        Microscope.SetPosition(p);
                        //We stop the controller update timer so we can update the stored coordinates list it operates on safely
                        storedImageBox.SelectedIndex = storedImageBox.SelectedIndex - 1;
                    }
                }
                return null;
            }
            if(f.FuncType == Function.FunctionType.Script)
            {
                Scripting.RunScript(f.File);
            }
            if (f.FuncType == Function.FunctionType.ImageJ)
            {
                ImageJ.RunMacro(f.File, "");
            }
            if(f.FuncType == Function.FunctionType.Microscope)
            {
                if(f.Microscope == "StageUp")
                {
                    Microscope.MoveUp(f.Value);
                }
                else
                if (f.Microscope == "StageRight")
                {
                    Microscope.MoveRight(f.Value);
                }
                else
                if (f.Microscope == "StageDown")
                {
                    Microscope.MoveDown(f.Value);
                }
                else
                if (f.Microscope == "StageLeft")
                {
                    Microscope.MoveLeft(f.Value);
                }
                else
                if (f.Microscope == "FocusUp")
                {
                    Microscope.SetFocus(f.Value);
                }
                else
                if (f.Microscope == "FocusDown")
                {
                    Microscope.SetFocus(-f.Value);
                }
                else
                if (f.Microscope == "StageFieldUp")
                {
                    Microscope.MoveFieldUp();
                }
                else
                if (f.Microscope == "StageFieldRight")
                {
                    Microscope.MoveFieldRight();
                }
                else
                if (f.Microscope == "StageFieldDown")
                {
                    Microscope.MoveFieldDown();
                }
                else
                if (f.Microscope == "StageFieldLeft")
                {
                    Microscope.MoveFieldLeft();
                }
                else
                if (f.Microscope == "TakeImage")
                {
                    Microscope.TakeImage();
                }
                else
                if (f.Microscope == "TakeImageStack")
                {
                    Microscope.TakeImageStack();
                }
                else
                if (f.Name == "RL")
                {
                    Microscope.RLShutter.SetPosition((int)f.Value);
                }
                else
                if (f.Name == "TL")
                {
                    Microscope.TLShutter.SetPosition((int)f.Value);
                }
            }
            if(f.FuncType == Function.FunctionType.Property)
            {
                if (f.Name.StartsWith("Get"))
                {
                    Automation.Recording r = (Automation.Recording)Automation.Properties[Path.GetFileNameWithoutExtension(f.File)];
                    return r.Get();
                }
                else if (f.Name.StartsWith("Set"))
                {
                    Automation.Recording r = (Automation.Recording)Automation.Properties[Path.GetFileNameWithoutExtension(f.File)];
                    r.Set(f.Value.ToString());
                }
                
            }
            if (f.FuncType == Function.FunctionType.Recording)
            {
                Automation.Recording r = (Automation.Recording)Automation.Recordings[Path.GetFileNameWithoutExtension(f.File)];
                r.Run();
            }
            return null;
        }

        public class ControllerFuncs
        {
            public static Function Y, B, A, X, DPadUp, DPadRight, DPadDown, DPadLeft, Start, Back, RShoulder, LShoulder, R3, L3;
            public static bool Initialized = false;
            public static void LoadSettings()
            {
                Y = Function.Parse(Settings.Default.Y.ToString());
                B = Function.Parse(Settings.Default.B.ToString());
                A = Function.Parse(Settings.Default.A.ToString());
                X = Function.Parse(Settings.Default.X.ToString());
                DPadUp = Function.Parse(Settings.Default.DPadUp.ToString());
                DPadRight = Function.Parse(Settings.Default.DPadRight.ToString());
                DPadDown = Function.Parse(Settings.Default.DPadDown.ToString());
                DPadLeft = Function.Parse(Settings.Default.DPadLeft.ToString());
                Start = Function.Parse(Settings.Default.Start.ToString());
                Back = Function.Parse(Settings.Default.Back.ToString());
                RShoulder = Function.Parse(Settings.Default.RShoulder.ToString());
                LShoulder = Function.Parse(Settings.Default.LShoulder.ToString());
                R3 = Function.Parse(Settings.Default.Right3.ToString());
                L3 = Function.Parse(Settings.Default.Left3.ToString());
                Initialized = true;
                if (Settings.Default.Y.ToString() == "")
                    Initialized = false;
            }
        }

    }

}
