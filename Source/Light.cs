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
        /* The constructor for the Light class. It initializes the GUI components, adds the light
        sources to the lightBox, and sets the selected index to 0. It then calls the UpdateGUI
        method. */
        public Light()
        {
            InitializeComponent();
            lightBox.Items.Add(Microscope.TLHalogen);
            lightBox.Items.Add(Microscope.RLHalogen);
            lightBox.Items.Add(Microscope.HXP);
            lightBox.SelectedIndex = 0;
            UpdateGUI();
        }
        /// It updates the GUI to reflect the current state of the shutters
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
        /// When the trackbar is scrolled, the percentLabel is updated to reflect the new value of the
        /// trackbar, and the position of the selected light source is updated to reflect the new value
        /// of the trackbar
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            percentLabel.Text = trackBar.Value + "%";
            LightSource l = (LightSource)lightBox.SelectedItem;
            l.SetPosition(trackBar.Value);
        }

/// If the checkbox is checked, set the position of the shutter to 2. If the checkbox is not checked,
/// set the position of the shutter to 1
/// 
/// @param sender The object that raised the event.
/// @param EventArgs System.EventArgs
        private void tlShutterBox_CheckedChanged(object sender, EventArgs e)
        {
            if(tlShutterBox.Checked)
                Microscope.TLShutter.SetPosition(2);
            else
                Microscope.TLShutter.SetPosition(1);
        }

        /// If the checkbox is checked, set the position of the RLShutter to 2, otherwise set the
        /// position of the RLShutter to 1
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        private void rlShutterBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rlShutterBox.Checked)
                Microscope.RLShutter.SetPosition(2);
            else
                Microscope.RLShutter.SetPosition(1);
        }

        /// If the checkbox is checked, set the HXP shutter to the open position. If the checkbox is not
        /// checked, set the HXP shutter to the closed position
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        private void hxpShutterBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hxpShutterBox.Checked)
                Microscope.HXPShutter.SetPosition(2);
            else
                Microscope.HXPShutter.SetPosition(1);
        }

        /// When the user selects a light source from the drop down list, the track bar is set to the
        /// position of the light source
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void lightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LightSource l = (LightSource)lightBox.SelectedItem;
            if (l == null)
                return;
            int i = (int)l.GetPosition();
            if (i != -1)
                trackBar.Value = i;
        }

        /// When the light is activated, update the GUI
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void Light_Activated(object sender, EventArgs e)
        {
            UpdateGUI();
        }
    }
}
