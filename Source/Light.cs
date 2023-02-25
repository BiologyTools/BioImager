using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bio.Source
{
    public partial class Light : Form
    {
        public Light()
        {
            InitializeComponent();
            lightBox.Items.Add(Microscope.TLHalogen);
            lightBox.Items.Add(Microscope.RLHalogen);
            lightBox.Items.Add(Microscope.HXP);
            lightBox.SelectedIndex = 0;
            UpdateGUI();
        }
        public void UpdateGUI()
        {
            int tl = Microscope.TLShutter.GetPosition();
            int rl = Microscope.RLShutter.GetPosition();
            int hxp = Microscope.HXPShutter.GetPosition();
            if (tl == 2)
                tlShutterBox.Checked = true;
            else if (tl == 1)
                tlShutterBox.Checked = false;
            if (rl == 2)
                rlShutterBox.Checked = true;
            else if (rl == 1)
                rlShutterBox.Checked = false;
            if (hxp == 2)
                hxpShutterBox.Checked = true;
            else if (hxp == 1)
                hxpShutterBox.Checked = false;
            percentLabel.Text = trackBar.Value + "%";
        }
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            percentLabel.Text = trackBar.Value + "%";
            LightSource l = (LightSource)lightBox.SelectedItem;
            l.SetPosition(trackBar.Value);
        }

        private void tlShutterBox_CheckedChanged(object sender, EventArgs e)
        {
            if(tlShutterBox.Checked)
                Microscope.TLShutter.SetPosition(2);
            else
                Microscope.TLShutter.SetPosition(1);
        }

        private void rlShutterBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rlShutterBox.Checked)
                Microscope.RLShutter.SetPosition(2);
            else
                Microscope.RLShutter.SetPosition(1);
        }

        private void hxpShutterBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hxpShutterBox.Checked)
                Microscope.HXPShutter.SetPosition(2);
            else
                Microscope.HXPShutter.SetPosition(1);
        }

        private void lightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LightSource l = (LightSource)lightBox.SelectedItem;
            int i = (int)l.GetPosition();
            if (i != -1)
                trackBar.Value = i;
        }

        private void Light_Activated(object sender, EventArgs e)
        {
            UpdateGUI();
        }
    }
}
