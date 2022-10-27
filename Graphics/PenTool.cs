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
    public partial class PenTool : Form
    {
        private Pen pen = new Pen(new ColorS(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue), 1,16);
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
        public PenTool()
        {
            InitializeComponent();
            UpdateGUI();
        }
        public PenTool(Pen p)
        {
            InitializeComponent();
            pen = p;
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

        private void widthBox_ValueChanged(object sender, EventArgs e)
        {
            pen.width = (ushort)widthBox.Value;
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void rBar_Scroll(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void gBar_Scroll(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void bBar_Scroll(object sender, EventArgs e)
        {
            UpdateGUI();
        }
    }
}
