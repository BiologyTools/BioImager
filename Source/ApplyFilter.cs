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
    public partial class ApplyFilter : Form
    {
        public ApplyFilter(bool two)
        {
            InitializeComponent();
            UpdateStacks();
            if (!two)
            { 
                stackBBox.Enabled = false;
                if (stackBBox.Items.Count > 1)
                    stackBBox.SelectedIndex = 1;
            }
            if (stackABox.Items.Count > 0)
                stackABox.SelectedIndex = 0;
        }
        public void UpdateStacks()
        {
            if (Images.images.Count != stackABox.Items.Count)
            {
                stackABox.Items.Clear();
                stackBBox.Items.Clear();
                foreach (BioImage b in Images.images)
                {
                    stackABox.Items.Add(b);
                    stackBBox.Items.Add(b);
                }
                if (stackABox.Items.Count == 1)
                    stackABox.SelectedIndex = 0;
            }
        }
        public BioImage ImageA
        {
            get { return (BioImage)stackABox.SelectedItem; }
        }
        public BioImage ImageB
        {
            get { return (BioImage)stackBBox.SelectedItem; }
        }
        public Rectangle Rectangle
        {
            get
            {
                if (roiBox.SelectedIndex != -1)
                    return ((ROI)roiBox.SelectedItem).BoundingBox.ToRectangleInt();
                else
                    return new Rectangle((int)xBox.Value, (int)yBox.Value, (int)wBox.Value, (int)hBox.Value);
            }
        }
        public int Angle
        {
            get
            {
                return (int)angleBox.Value;
            }
        }
        public Color Color
        {
            get
            {
                return colorDialog.Color;
            }
        }
        public int W
        {
            get
            {
                return (int)wBox.Value;
            }
        }
        public int H
        {
            get
            {
                return (int)hBox.Value;
            }
        }

        private void stackABox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stackABox.SelectedIndex == -1)
                return;
            roiBox.Items.Clear();
            roiBox.Items.AddRange(ImageA.Annotations.ToArray());
            if (stackBBox.SelectedIndex == -1)
                return;
            if(stackABox.SelectedItem == stackBBox.SelectedItem)
            {
                //Same image selected for A & B
                MessageBox.Show("Same image selected for A & B. Change either A stack or B stack.");
                stackABox.SelectedIndex = -1;
            }
        }
        private void stackBBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stackABox.SelectedIndex == -1)
                return;
            if (stackBBox.SelectedIndex == -1)
                return;
            if (stackABox.SelectedItem == stackBBox.SelectedItem)
            {
                //Same image selected for A & B
                MessageBox.Show("Same image selected for A & B. Change either A stack or B stack.");
                stackBBox.SelectedIndex = -1;
            }
        }
        private void StackTools_Activated(object sender, EventArgs e)
        {
            UpdateStacks();
        }
        private void okBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void setColorBut_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;
            fillPanel.BackColor = colorDialog.Color;
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roiBox.SelectedIndex = -1;
        }
    }
}
