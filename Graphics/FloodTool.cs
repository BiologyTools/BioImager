using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bio.Graphics
{
    public partial class FloodTool : Form
    {
        private Pen pen = new Pen(new ColorS(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue), 1,16);
        private int bitsPerPixel = 16;
        public Pen Pen
        {
            get
            {
                return pen;
            }
            set
            {
                pen = value;
            }
        }

        private ColorS tolerance;
        public ColorS Tolerance
        {
            get
            {
                return tolerance;
            }
        }

        public void UpdateGUI()
        {
            pen.color = new ColorS((ushort)redBox.Value, (ushort)greenBox.Value, (ushort)blueBox.Value);
            colorPanel.BackColor = ColorS.ToColor(pen.color,pen.bitsPerPixel);
            if (rBar.Value != redBox.Value)
                redBox.Value = rBar.Value;
            if (gBar.Value != greenBox.Value)
                greenBox.Value = gBar.Value;
            if (bBar.Value != blueBox.Value)
                blueBox.Value = bBar.Value;
        }

        public void SetColor()
        {
            rBar.Value = pen.color.R;
            gBar.Value = pen.color.G;
            bBar.Value = pen.color.B;
        }
        public FloodTool()
        {
            InitializeComponent();
            UpdateGUI();
        }
        public FloodTool(Pen p, ColorS tolerance, int bitPerPixel)
        {
            InitializeComponent();
            pen = p;
            bitsPerPixel = bitPerPixel;
            if (bitsPerPixel == 8)
            {
                rBar.Maximum = 255;
                gBar.Maximum = 255;
                bBar.Maximum = 255;
                redBox.Maximum = 255;
                greenBox.Maximum = 255;
                blueBox.Maximum = 255;
            }
            if (rBar.Maximum <= p.color.R)
                rBar.Value = rBar.Maximum;
            if (gBar.Maximum <= p.color.G)
                gBar.Value = gBar.Maximum;
            if (bBar.Maximum <= p.color.B)
                bBar.Value = bBar.Maximum;
            this.tolerance = tolerance;
            tolRBox.Value = (decimal)tolerance.R;
            tolGBox.Value = (decimal)tolerance.G;
            tolBBox.Value = (decimal)tolerance.B;
            SetColor();
            UpdateGUI();
        }

        private void redBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void greenBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void blueBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateGUI();
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

        private void applyButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tolRBox_ValueChanged(object sender, EventArgs e)
        {
            tolerance.R = (ushort)tolRBox.Value;
        }

        private void tolGBox_ValueChanged(object sender, EventArgs e)
        {
            tolerance.G = (ushort)tolGBox.Value;
        }

        private void tolBBox_ValueChanged(object sender, EventArgs e)
        {
            tolerance.B = (ushort)tolBBox.Value;
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void rBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateGUI();
        }
    }
}
