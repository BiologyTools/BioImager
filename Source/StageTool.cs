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

        private void dockBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DockMicro = dockBox.Checked;
        }

        private void setUpperBut_Click(object sender, EventArgs e)
        {
            upperLimBox.Value = (decimal)Microscope.Focus.GetFocus(true);
        }

        private void setLowerBut_Click(object sender, EventArgs e)
        {
            lowerLimBox.Value = (decimal)Microscope.Focus.GetFocus(true);
        }

        private void fIntervalBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.fInterVal = (double)fIntervalBox.Value;
            UpdateSlices();
        }
        private void UpdateSlices()
        {
            double d = (double)upperLimBox.Value - (double)lowerLimBox.Value;
            sliceBox.Value = (decimal)(d / (double)fIntervalBox.Value);
        }
        private void sliceBox_ValueChanged(object sender, EventArgs e)
        {
            double d = (double)upperLimBox.Value - (double)lowerLimBox.Value;
            double interval = d / (double)sliceBox.Value;
            double dd = interval / (double)fIntervalBox.Value;
            fIntervalBox.Value = (decimal)interval;
        }

        private void upperLimBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.UpperLimit = (double)upperLimBox.Value;
        }

        private void lowerLimBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.LowerLimit = (double)lowerLimBox.Value;
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ImageName = nameBox.Text;
            Properties.Settings.Default.Save();
        }

        private void tilesBut_Click(object sender, EventArgs e)
        {
            ImageTiles im = new ImageTiles();
            if (im.ShowDialog() != DialogResult.OK)
                return;
            Microscope.TakeTiles(im.SizeX, im.SizeY);
        }

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

        private void moveXBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.viewSize = new PointD((double)moveXBox.Value, (double)moveYBox.Value);
            if(App.viewer!=null)
            App.viewer.UpdateView();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void moveYBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.viewSize = new PointD((double)moveXBox.Value, (double)moveYBox.Value);
            if (App.viewer != null)
                App.viewer.UpdateView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
