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
    public partial class ColorTool : Form
    {
        private ColorS color = new ColorS(65535, 65535, 65535);
        public ColorS Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public void UpdateColor()
        {
            color = new ColorS((ushort)redBox.Value, (ushort)greenBox.Value, (ushort)blueBox.Value);
            colorPanel.BackColor = ColorS.ToColor(color);
            Tools.currentTool.Color = color;
        }

        public void SetColor()
        {
            redBox.Value = (decimal)color.R;
            greenBox.Value = (decimal)color.G;
            blueBox.Value = (decimal)color.B;
        }
        public ColorTool()
        {
            InitializeComponent();
            UpdateColor();
        }
        public ColorTool(ColorS col)
        {
            InitializeComponent();
            color = col;
            SetColor();
            UpdateColor();
        }

        private void redBox_ValueChanged(object sender, EventArgs e)
        {
            color.R = (ushort)redBox.Value;
            UpdateColor();
        }

        private void greenBox_ValueChanged(object sender, EventArgs e)
        {
            color.G = (ushort)greenBox.Value;
            UpdateColor();
        }

        private void blueBox_ValueChanged(object sender, EventArgs e)
        {
            color.B = (ushort)blueBox.Value;
            UpdateColor();
        }

        private void rEnbaled_CheckedChanged(object sender, EventArgs e)
        {
            Tools.rEnabled = rEnbaled.Checked;
        }

        private void gEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Tools.gEnabled = gEnabled.Checked;
        }

        private void bEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Tools.bEnabled = bEnabled.Checked;
        }
    }
}
