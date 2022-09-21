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
            timer.Start();
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

        private void overlayBox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.viewer.UpdateOverlay();
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
            Microscope.TakeImageStack();
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
            upperLimBox.Value = (decimal)Microscope.Focus.GetFocus();
        }

        private void setLowerBut_Click(object sender, EventArgs e)
        {
            lowerLimBox.Value = (decimal)Microscope.Focus.GetFocus();
        }

        private void fIntervalBox_ValueChanged(object sender, EventArgs e)
        {
            Microscope.fInterVal = (double)fIntervalBox.Value;
        }

        private void sliceBox_ValueChanged(object sender, EventArgs e)
        {
            double d = Microscope.UpperLimit - Microscope.LowerLimit;
            double dd = d / Microscope.fInterVal;
            fIntervalBox.Value = (decimal)dd;
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
        }
    }
}
