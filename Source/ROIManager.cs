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
    public partial class ROIManager : Form
    {
        public ROIManager()
        {
            InitializeComponent();
            foreach (ROI.Type item in Enum.GetValues(typeof(ROI.Type)))
            {
                typeBox.Items.Add(item);
            }
        }
        public ROI anno = new ROI();
        public void UpdateAnnotationList()
        {
            if (ImageView.SelectedImage == null)
                return;
            roiView.Items.Clear();
            foreach (ROI an in ImageView.SelectedImage.Annotations)
            {
                ListViewItem it = new ListViewItem();
                it.Tag = an;
                it.Text = an.ToString();
                roiView.Items.Add(it);
            }
        }
        public void UpdateOverlay()
        {
            if(App.viewer != null)
                App.viewer.UpdateOverlay();
        }
        public void updateROI(int index, ROI an)
        {
            if (ImageView.SelectedImage == null)
                return;
            ImageView.SelectedImage.Annotations[index] = an;
            UpdateOverlay();
        }
        private void xBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.X = (double)xBox.Value;
            UpdateOverlay();
        }
        private void yBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.Y = (double)yBox.Value;
            UpdateOverlay();
        }
        private void wBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            if(anno.type == ROI.Type.Rectangle || anno.type == ROI.Type.Ellipse)
                anno.W = (double)wBox.Value;
            UpdateOverlay();
        }
        private void hBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            if (anno.type == ROI.Type.Rectangle || anno.type == ROI.Type.Ellipse)
                anno.H = (double)hBox.Value;
            UpdateAnnotationList();
            UpdateOverlay();
        }
        private void sBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            UpdateOverlay();
        }
        private void zBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.coord.Z = (int)zBox.Value;
            UpdateOverlay();
        }
        private void cBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.coord.C = (int)cBox.Value;
            UpdateOverlay();
        }
        private void tBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.coord.T = (int)cBox.Value;
            UpdateOverlay();
        }
        private void rBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.strokeColor = Color.FromArgb((byte)rBox.Value, anno.strokeColor.G, anno.strokeColor.B);
            UpdateOverlay();
        }
        private void gBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.strokeColor = Color.FromArgb(anno.strokeColor.R, (byte)gBox.Value, anno.strokeColor.B);
            UpdateOverlay();
        }
        private void bBox_ValueChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.strokeColor = Color.FromArgb(anno.strokeColor.R, anno.strokeColor.G, (byte)bBox.Value);
            UpdateOverlay();
        }
        private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.type = (ROI.Type)typeBox.SelectedItem;
            UpdateOverlay();
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.Text = textBox.Text;
            UpdateOverlay();
        }
        private void idBox_TextChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            anno.id = idBox.Text;
            UpdateOverlay();
        }
        private void ROIManager_Activated(object sender, EventArgs e)
        {
            if (ImageView.SelectedImage == null)
                return;
            string n = System.IO.Path.GetFileName(ImageView.SelectedImage.ID);
            if (imageNameLabel.Text != n)
                imageNameLabel.Text = n;
            UpdateAnnotationList();
        }
        
        private void roiView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            ListViewItem it = roiView.SelectedItems[0];
            anno = (ROI)it.Tag;
            if(App.viewer!=null)
            App.viewer.SetCoordinate(anno.coord.Z, anno.coord.C, anno.coord.T);
            if(anno.type == ROI.Type.Line || anno.type == ROI.Type.Polygon ||
               anno.type == ROI.Type.Polyline)
            {
                xBox.Enabled = false;
                yBox.Enabled = false;
                wBox.Enabled = false;
                hBox.Enabled = false;
            }
            else
            {
                xBox.Enabled = true;
                yBox.Enabled = true;
                wBox.Enabled = true;
                hBox.Enabled = true;
            }
            if(anno.type == ROI.Type.Rectangle || anno.type == ROI.Type.Ellipse)
            {
                pointIndexBox.Enabled = false;
                pointXBox.Enabled = false;
                pointYBox.Enabled = false;
            }
            else
            {
                pointIndexBox.Enabled = true;
                pointXBox.Enabled = true;
                pointYBox.Enabled = true;
            }
            xBox.Value = (decimal)anno.X;
            yBox.Value = (decimal)anno.Y;
            wBox.Value = (decimal)anno.W;
            hBox.Value = (decimal)anno.H;
            zBox.Value = anno.coord.Z;
            cBox.Value = anno.coord.C;
            tBox.Value = anno.coord.T;
            rBox.Value = anno.strokeColor.R;
            gBox.Value = anno.strokeColor.G;
            bBox.Value = anno.strokeColor.B;
            strokeWBox.Value = (decimal)anno.strokeWidth;
            idBox.Text = anno.id;
            textBox.Text = anno.Text;
            typeBox.SelectedIndex = (int)anno.type;
            UpdatePointBox();
        }
        private void updateBut_Click(object sender, EventArgs e)
        {
            if (roiView.SelectedItems.Count == 0)
                return;
            if (ImageView.SelectedImage == null)
                return;
            ImageView.SelectedImage.Annotations[roiView.SelectedIndices[0]] = anno;
            UpdateOverlay();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            ImageView.SelectedImage.Annotations.Add(anno);
            UpdateOverlay();
        }
        private void showBoundsBox_CheckedChanged(object sender, EventArgs e)
        {
            ImageView.showBounds = showBoundsBox.Checked;
            UpdateOverlay();
        }
        private void showTextBox_CheckedChanged(object sender, EventArgs e)
        {
            ImageView.showText = showTextBox.Checked;
            UpdateOverlay();
        }
        private void pointXBox_ValueChanged(object sender, EventArgs e)
        {
            if (anno == null)
                return;
            if (anno.type == ROI.Type.Rectangle || anno.type == ROI.Type.Ellipse)
                return;
            anno.UpdatePoint(new PointD((double)pointXBox.Value, (double)pointYBox.Value),(int)pointIndexBox.Value);
            UpdateOverlay();
        }
        private void pointYBox_ValueChanged(object sender, EventArgs e)
        {
            if (anno == null)
                return;
            if (anno.type == ROI.Type.Rectangle || anno.type == ROI.Type.Ellipse)
                return;
            anno.UpdatePoint(new PointD((double)pointXBox.Value, (double)pointYBox.Value), (int)pointIndexBox.Value);
            UpdateOverlay();
        }
        public bool autoUpdate = true;
        public void UpdatePointBox()
        {
            if (anno == null)
                return;
            PointD d = anno.GetPoint((int)pointIndexBox.Value);
            pointXBox.Value = (int)d.X;
            pointYBox.Value = (int)d.Y;
        }
        private void pointIndexBox_ValueChanged(object sender, EventArgs e)
        {
            UpdatePointBox();
        }
        private void fontBut_Click(object sender, EventArgs e)
        {
            if (anno == null)
                return;
            if (fontDialog.ShowDialog() != DialogResult.OK)
                return;
            anno.font = fontDialog.Font;
        }
        private void strokeWBox_ValueChanged(object sender, EventArgs e)
        {
            if (anno == null)
                return;
            anno.strokeWidth = (int)strokeWBox.Value;
            UpdateOverlay();
        }
        private void selectBoxSize_ValueChanged(object sender, EventArgs e)
        {
            App.viewer.UpdateSelectBoxSize((float)selectBoxSize.Value);
            UpdateOverlay();
        }
        private void rChBox_CheckedChanged(object sender, EventArgs e)
        {
            if (App.viewer == null)
                return;
            App.viewer.showRROIs = rChBox.Checked;
            UpdateOverlay();
        }
        private void gChBox_CheckedChanged(object sender, EventArgs e)
        {
            if (App.viewer == null)
                return;
            App.viewer.showGROIs = gChBox.Checked;
            UpdateOverlay();
        }
        private void bChBox_CheckedChanged(object sender, EventArgs e)
        {
            if (App.viewer == null)
                return;
            App.viewer.showBROIs = bChBox.Checked;
            UpdateOverlay();
        }
        private void ROIManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < roiView.SelectedItems.Count; i++)
            {
                ImageView.SelectedImage.Annotations.Remove((ROI)roiView.SelectedItems[i].Tag);
            }
            UpdateAnnotationList();
            UpdateOverlay();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ROI> annotations = new List<ROI>();
            ROI an = (ROI)roiView.SelectedItems[0].Tag;
            Clipboard.SetText(BioImage.ROIToString(an));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostBox.Checked;
        }
    }
}
