using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bio
{
    public partial class StageTool : Form
    {
        public MicroscopeSetup objectiveSetup = null;
        public StageTool()
        {
            InitializeComponent();
            Microscope.Initialize();
            //panel.Controls.Add(App.stageView);
            this.Show();
            objectiveSetup = App.setup;
            objBox.Items.AddRange(Microscope.Objectives.List.ToArray());
            folderBox.Text = Properties.Settings.Default.ImagingPath;
            dockBox.Checked = Properties.Settings.Default.DockMicro;
            moveXBox.Value = (decimal)Microscope.GetObjectiveViewRectangle().W;
            moveYBox.Value = (decimal)Microscope.GetObjectiveViewRectangle().H;
            Microscope.viewSize = new PointD((double)moveXBox.Value, (double)moveYBox.Value);

            timer.Start();
        }
        public string ImagingFolder
        {
            get
            {
                return folderBox.Text;
            }
            set
            {
                Properties.Settings.Default.ImagingPath = folderBox.Text;
                Properties.Settings.Default.Save();
            }
        }
        public string ImageName
        {
            get
            {
                return nameBox.Text;
            }
            set
            {
                nameBox.Text = value;
                Properties.Settings.Default.ImageName = nameBox.Text;
                Properties.Settings.Default.Save();
            }
        }
        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            //panel.Width = Width;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel2.Show();
            //panel.Width -= panel2.Width;
        }
        private void hideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            //panel.Width = Width;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Show();
            //panel.Width -= panel2.Width;
        }
        private void upBut_Click(object sender, EventArgs e)
        {
            Microscope.MoveUp((double)moveYBox.Value);
            App.viewer.UpdateOverlay();
        }

        private void rightBut_Click(object sender, EventArgs e)
        {
            Microscope.MoveRight((double)moveXBox.Value);
            App.viewer.UpdateOverlay();
        }

        private void downBut_Click(object sender, EventArgs e)
        {
            Microscope.MoveDown((double)moveYBox.Value);
            App.viewer.UpdateOverlay();
        }

        private void leftBut_Click(object sender, EventArgs e)
        {
            Microscope.MoveLeft((double)moveXBox.Value);
            App.viewer.UpdateOverlay();
        }

        private void objToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.setup.Show();
        }

        private void imagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.imager.Show();
        }

        private void objBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            moveXBox.Value = (decimal)Microscope.Objectives.List[objBox.SelectedIndex].ViewWidth;
            moveYBox.Value = (decimal)Microscope.Objectives.List[objBox.SelectedIndex].ViewHeight;
        }
        private void setObjBut_Click(object sender, EventArgs e)
        {
            if (objBox.SelectedItem == null)
                return;
            Objectives.Objective o = (Objectives.Objective)objBox.SelectedItem;
            Microscope.Objectives.SetPosition(o.Index);
        }

        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostBox.Checked;
        }

        private void takeImageBut_Click(object sender, EventArgs e)
        {
            Microscope.TakeImage();
        }

        private void stackBut_Click(object sender, EventArgs e)
        {
            Microscope.UpperLimit = (double)upperLimBox.Value;
            Microscope.LowerLimit = (double)lowerLimBox.Value;
            Microscope.fInterVal = (double)fIntervalBox.Value;
            Microscope.SetFolder(folderBox.Text);
            BioImage bm = Microscope.TakeImageStack();
            App.viewer.AddImage(bm);
        }
        private Point p;
        /// If the dockBox checkbox is checked, then we set the window location based on the imaging app
        /// location
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        private void timer_Tick(object sender, EventArgs e)
        {
            if (dockBox.Checked)
            {
                //We set window location based on imaging app location.
                Win32.Rect r = new Win32.Rect();
                Win32.GetWindowRect(Imager.apph, ref r);
                Point pp = new Point(r.Right - Width, r.Bottom - Height - App.imager.Height - 30);
                this.Location = pp;
                string s = Win32.GetActiveWindowTitle();
                if (s != null)
                    if (s.Contains(Properties.Settings.Default.AppName))
                    {
                        if (dockBox.Checked)
                            Show();
                        else
                        {
                            if (!s.Contains("Imager"))
                            {
                                if (dockBox.Checked)
                                    Hide();
                            }
                        }
                    }
            }

        }

        /// If the checkbox is checked, set the value of the "DockMicro" setting to true
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void dockBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DockMicro = dockBox.Checked;
#if DEBUG
            this.Location = new Point(0, 0);
#endif
        }

        /// This function sets the upper limit of the focus range to the current focus position
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void setUpperBut_Click(object sender, EventArgs e)
        {
            upperLimBox.Value = (decimal)Microscope.Focus.GetFocus(true);
        }

        /// This function sets the lower limit of the focus range to the current focus position
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        private void setLowerBut_Click(object sender, EventArgs e)
        {
            lowerLimBox.Value = (decimal)Microscope.Focus.GetFocus(true);
        }

        /// When the user changes the value of the fIntervalBox, the value of the fIntervalBox is
        /// assigned to the fInterVal variable in the Microscope class
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void fIntervalBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.fInterVal = (double)fIntervalBox.Value;
            UpdateSlices();
        }
        /// The function UpdateSlices() updates the number of slices in the sliceBox.Value based on the
        /// upperLimBox.Value, lowerLimBox.Value, and fIntervalBox.Value
        private void UpdateSlices()
        {
            double d = (double)upperLimBox.Value - (double)lowerLimBox.Value;
            sliceBox.Value = (decimal)(d / (double)fIntervalBox.Value);
        }
        /// When the user changes the number of slices, the function changes the interval between slices
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        private void sliceBox_ValueChanged(object sender, EventArgs e)
        {
            double d = (double)upperLimBox.Value - (double)lowerLimBox.Value;
            double interval = d / (double)sliceBox.Value;
            double dd = interval / (double)fIntervalBox.Value;
            fIntervalBox.Value = (decimal)interval;
        }

        /// When the value of the upper limit box is changed, the upper limit of the microscope is set
        /// to the value of the upper limit box
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        private void upperLimBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.UpperLimit = (double)upperLimBox.Value;
        }

        /// When the value of the lower limit box is changed, the lower limit of the microscope is set
        /// to the value of the lower limit box.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        private void lowerLimBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.LowerLimit = (double)lowerLimBox.Value;
        }

        /// When the text in the textbox changes, the text is saved to the settings file
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ImageName = nameBox.Text;
            Properties.Settings.Default.Save();
        }

        /// The user clicks the "Tiles" button, which opens a dialog box to get the number of tiles in
        /// the X and Y directions. The dialog box returns the number of tiles in the X and Y
        /// directions. The microscope takes the tiles
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The dialog result.
        private void tilesBut_Click(object sender, EventArgs e)
        {
            ImageTiles im = new ImageTiles();
            if (im.ShowDialog() != DialogResult.OK)
                return;
            Microscope.TakeTiles(im.SizeX, im.SizeY);
        }

        /// The user clicks a button, a dialog box opens, the user selects a folder, the folder path is
        /// saved in a text box, the folder path is saved in a class, and the folder path is saved in the
        /// application settings
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        private void setFolderBut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fs = new FolderBrowserDialog();
            fs.Description = "Select Imaging Folder";
            if (fs.ShowDialog() != DialogResult.OK)
            {
                fs.Dispose();
            }
            else
            {
                fs.Dispose();
                folderBox.Text = fs.SelectedPath;
                Microscope.SetFolder(fs.SelectedPath);
                Properties.Settings.Default.ImagingPath = fs.SelectedPath;
            }
        }

        /// When the value of the moveXBox changes, the viewSize of the microscope is changed to the
        /// value of the moveXBox
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void moveXBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.viewSize = new PointD((double)moveXBox.Value, (double)moveYBox.Value);
            if (App.viewer != null)
                App.viewer.UpdateView();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /// When the user changes the value of the moveYBox, the viewSize of the microscope is changed
        /// to the new value
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void moveYBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.viewSize = new PointD((double)moveXBox.Value, (double)moveYBox.Value);
            if (App.viewer != null)
                App.viewer.UpdateView();
        }

        private void goUpperBut_Click(object sender, EventArgs e)
        {
            Microscope.SetFocus((double)upperLimBox.Value);
        }

        private void goLowerBut_Click(object sender, EventArgs e)
        {
            Microscope.SetFocus((double)lowerLimBox.Value);
        }
    }
}
