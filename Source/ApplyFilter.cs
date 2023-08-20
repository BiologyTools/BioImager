using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
namespace Bio
{
    public partial class ApplyFilter : Form
    {
        /* This is the constructor for the ApplyFilter class. It is called when the ApplyFilter class
        is instantiated. */
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
        /// If the number of images in the list of images is not equal to the number of items in the
        /// stackABox and stackBBox, then clear the items in the stackABox and stackBBox, add the images
        /// to the stackABox and stackBBox, and if there is only one image in the list of images, then
        /// select that image
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
        public RectangleD Rectangle
        {
            get
            {
                if (roiBox.SelectedIndex != -1)
                    return ((ROI)roiBox.SelectedItem).BoundingBox;
                else
                    return new RectangleD((double)xBox.Value, (double)yBox.Value, (double)wBox.Value, (double)hBox.Value);
            }
        }
        public int Angle
        {
            get
            {
                return (int)angleBox.Value;
            }
        }
        public System.Drawing.Color Color
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

        /// If the user selects the same image for both A and B, then the program will display a message
        /// box telling the user to select a different image for either A or B
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The selected item in the stackABox.
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
        /// If the user selects the same image for both A and B, then display a message box
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The selected index of the stackBBox.
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
        /// When the form is activated, update the stacks
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void StackTools_Activated(object sender, EventArgs e)
        {
            UpdateStacks();
        }
        /// The function is called when the user clicks the OK button
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void okBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
       /// If the user clicks the cancel button, the dialog result is set to cancel
       /// 
       /// @param sender The object that raised the event.
       /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        /// If the user clicks the "Set Color" button, then the color dialog box will appear and the
        /// user can select a color. If the user clicks "OK", then the color will be set to the color
        /// the user selected. If the user clicks "Cancel", then the color will not be changed
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The color that was selected in the color dialog.
        private void setColorBut_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;
            fillPanel.BackColor = colorDialog.Color;
        }
        /// This function clears the ROI box when the user clicks on the "Clear" button in the ROI menu
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event data.
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roiBox.SelectedIndex = -1;
        }
    }
}
