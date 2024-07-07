using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioLib;
namespace BioImager
{
    public partial class StackTools : Form
    {
        public StackTools()
        {
            InitializeComponent();
            UpdateStacks();
        }

        /// It clears the contents of two combo boxes, then adds all the images in the Images class to
        /// the combo boxes
        /// 
        /// @return The method is returning the number of items in the stackABox.Items collection.
        public void UpdateStacks()
        {
            if (stackABox.Items.Count == Images.images.Count)
                return;
            stackABox.Items.Clear();
            stackBBox.Items.Clear();    
            foreach (BioImage b in Images.images)
            {
                stackABox.Items.Add(b);
                stackBBox.Items.Add(b);
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
        /// It takes the image in the first stack, and creates a new image from a subset of the original
        /// image's series, z-slices, channels, and time points
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return A BioImage object.
        private void substackBut_Click(object sender, EventArgs e)
        {
            if (stackABox.SelectedIndex == -1)
                return;
            BioImage b = BioImage.Substack(ImageA, ((BioImage)stackABox.SelectedItem).series, (int)zStartBox.Value, (int)zEndBox.Value, (int)cStartBox.Value, (int)cEndBox.Value, (int)tStartBox.Value, (int)tEndBox.Value);
            App.tabsView.AddTab(b);
            UpdateStacks();
        }

        /// If the user selects the same image for both A and B, then the program will display a message
        /// box telling the user to select a different image for either A or B
        /// 
        /// @param sender The control that raised the event.
        /// @param EventArgs System.EventArgs
        /// 
        /// @return The selected index of the stackABox.
        private void stackABox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stackABox.SelectedIndex == -1)
                return;
            if (stackABox.SelectedItem == stackBBox.SelectedItem)
            {
                //Same image selected for A & B
                MessageBox.Show("Same image selected for A & B. Change either A stack or B stack.");
                stackABox.SelectedIndex = -1;
            }
            zEndBox.Maximum = ImageA.SizeZ;
            cEndBox.Maximum = ImageA.SizeC;
            tEndBox.Maximum = ImageA.SizeT;
            zEndBox.Value = ImageA.SizeZ;
            cEndBox.Value = ImageA.SizeC;
            tEndBox.Value = ImageA.SizeT;
        }

       /// If the user selects the same image for both A and B, then display a message box
       /// 
       /// @param sender The object that raised the event.
       /// @param EventArgs 
       /// 
       /// @return The selected index of the stackBBox ComboBox.
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

        /// If the user has selected an image from each stack, then merge the two images and display the
        /// result
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The return type is void.
        private void mergeBut_Click(object sender, EventArgs e)
        {
            if (stackABox.SelectedIndex == -1)
                return;
            if (stackBBox.SelectedIndex == -1)
                return;
            BioImage b = BioImage.MergeChannels(ImageA, ImageB);
            TabsView iv = new TabsView(b);
            iv.Show();
            UpdateStacks();
        }

        /// It takes the image in the first stack, splits it into its component channels, and adds each
        /// channel to a new tab
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The image is being split into its RGB channels and returned as an array of
        /// BioImages.
        private void splitChannelsBut_Click(object sender, EventArgs e)
        {
            if (stackABox.SelectedIndex == -1)
                return;
            BioImage[] bms = ImageA.SplitChannels();
            for (int i = 0; i < bms.Length; i++)
            {
                App.tabsView.AddTab(bms[i]);
            }
            UpdateStacks();
        }

        private void StackTools_Activated(object sender, EventArgs e)
        {
            UpdateStacks();
        }

        private void setMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ImageA!=null)
            zEndBox.Value = ImageA.SizeZ;
        }

        private void setMaxCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImageA != null)
                cEndBox.Value = ImageA.SizeC;
        }

        private void setMaxTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImageA != null)
                tEndBox.Value = ImageA.SizeT;
        }

        private void StackTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void mergeZBut_Click(object sender, EventArgs e)
        {
            if (ImageA != null)
                App.tabsView.AddTab(BioImage.MergeZ(ImageA));
        }

        private void mergeTBut_Click(object sender, EventArgs e)
        {
            if (ImageA != null)
                App.tabsView.AddTab(BioImage.MergeT(ImageA));
        }
    }
}
