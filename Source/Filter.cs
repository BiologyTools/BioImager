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
    public partial class Filter : Form
    {
        /* The constructor of the class. */
        public Filter()
        {
            InitializeComponent();
            Init();
        }
        /// It updates the status of the viewer
        private void UpdateView()
        {
            App.viewer.UpdateStatus();
        }
        /* The Node class is a wrapper class for the TreeNode class. It allows us to add a Filt object
        to a TreeNode object */
        public class Node
        {
            public TreeNode node;
            public Filt filt;
            public Node(TreeNode nod, Filt f)
            {
                node = nod;
                filt = f;
            }
            public override string ToString()
            {
                return filt.name.ToString();
            }
        }
        /// It takes a list of filters and adds them to a treeview
        public void Init()
        {
            foreach (Filt.Type t in (Filt.Type[])Enum.GetValues(typeof(Filt.Type)))
            {
                TreeNode gr = new TreeNode();
                gr.Text = t.ToString();
                filterView.Nodes.Add(gr);
            }
            foreach (Filt f in Filters.filters.Values)
            {
                TreeNode nod = new TreeNode();
                nod.Text = f.name;
                Node node = new Node(nod, f);
                nod.Tag = node;
                filterView.Nodes[(int)f.type].Nodes.Add(nod);
            }
        }

        /// It takes the selected node from the treeview, and then applies the filter to the selected
        /// image
        /// 
        /// @param inPlace true if the filter is applied to the original image, false if it's applied to
        /// a copy of the image.
        /// 
        /// @return The return value is the result of the function.
        private void ApplyFilter(bool inPlace)
        {
            if (filterView.SelectedNode==null)
                return;
            Node n = (Node)filterView.SelectedNode.Tag;
            if (n.filt.type == Filt.Type.Base)
            {
                Filters.Base(ImageView.SelectedImage.ID, n.filt.name, false);
                
            }
            if (n.filt.type == Filt.Type.Base2)
            {
                ApplyFilter two = new ApplyFilter(true);
                if (two.ShowDialog() != DialogResult.OK)
                    return;
                Filters.Base2(two.ImageA.ID, two.ImageB.ID, n.filt.name, false);
                
            }
            else
            if (n.filt.type == Filt.Type.InPlace)
            {
                Filters.InPlace(ImageView.SelectedImage.ID, n.filt.name, false);
            }
            else
            if (n.filt.type == Filt.Type.InPlace2)
            {
                ApplyFilter two = new ApplyFilter(true);
                if (two.ShowDialog() != DialogResult.OK)
                    return;
                Filters.InPlace2(two.ImageA.ID, two.ImageB.ID, n.filt.name, false);
                
            }
            else
            if (n.filt.type == Filt.Type.InPlacePartial)
            {
                Filters.InPlacePartial(ImageView.SelectedImage.ID, n.filt.name, false);
                
            }
            else
            if (n.filt.type == Filt.Type.Resize)
            {
                ApplyFilter two = new ApplyFilter(false);
                if (two.ShowDialog() != DialogResult.OK)
                    return;
                Filters.Resize(ImageView.SelectedImage.ID, n.filt.name, false, two.W,two.H);
            }
            else
            if (n.filt.type == Filt.Type.Rotate)
            {
                ApplyFilter two = new ApplyFilter(false);
                if (two.ShowDialog() != DialogResult.OK)
                    return;
                Filters.Rotate(ImageView.SelectedImage.ID, n.filt.name, false, two.Angle, two.Color.A, two.Color.R, two.Color.G, two.Color.B);
            }
            else
            if (n.filt.type == Filt.Type.Transformation)
            {
                ApplyFilter two = new ApplyFilter(false);
                if (two.ShowDialog() != DialogResult.OK)
                    return;
                if (n.filt.name == "Crop")
                {
                    Filters.Crop(two.ImageA.ID, two.Rectangle);
                }
                else
                {
                    Filters.Transformation(ImageView.SelectedImage.ID, n.filt.name, false, two.Angle);
                }
            }
            else
            if (n.filt.type == Filt.Type.Copy)
            {
                Filters.Copy(ImageView.SelectedImage.ID, n.filt.name, false);
            }
            UpdateView();
        }

       /// It applies the filter to the current image
       /// 
       /// @param sender The object that raised the event.
       /// @param EventArgs The event arguments.
        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(false);
        }

/// It takes the current image, applies the filter, and then displays the new image
/// 
/// @param sender The object that raised the event.
/// @param EventArgs The event arguments.
        private void applyRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }

        /// If the topMostBox checkbox is checked, then the form will be topmost
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostBox.Checked;
        }

        /// If the user double clicks on a filter, apply the filter to the current view
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void filterView_DoubleClick(object sender, EventArgs e)
        {
            ApplyFilter(false);
        }
    }
}
