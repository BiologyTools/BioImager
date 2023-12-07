using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge;
using BioImager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioImager
{
    public partial class CellImager : Form
    {
        ImageView viewer;
        List<Blob> blobs = new List<Blob>();
        List<BioImage> images = new List<BioImage>();
        BioImage b;
        System.Drawing.Bitmap bitmap;
        public CellImager()
        {
            InitializeComponent();
            viewer = new ImageView();
            viewer.ShowControls = false;
            imageSplitContainer.Panel2.Controls.Add(viewer);
            foreach (var item in Microscope.Objectives.List)
            {
                objectivesBox.Items.Add(item.Name);
            }
            objectivesBox.SelectedIndex = Microscope.Objectives.Index;
            maxWidthLabel.Text = "Max Width:" + maxWidthBar.Value;
            maxHeightLabel.Text = "Max Height:" + maxHeightBar.Value;
            minHeightLabel.Text = "Min Height:" + minHeightBar.Value;
            minWidthLabel.Text = "Min Width:" + minWidthBar.Value;
            foreach (Scripting.Script item in Scripting.Scripts.Values)
            {
                comboBox.Items.Add(item);
            }
        }
        
        private void maxWidthBar_Scroll(object sender, EventArgs e)
        {
            maxWidthLabel.Text = "Max Width:" + maxWidthBar.Value;
        }

        private void maxHeightBar_Scroll(object sender, EventArgs e)
        {
            maxHeightLabel.Text = "Max Height:" + maxHeightBar.Value;
        }

        private void minHeightBar_Scroll(object sender, EventArgs e)
        {
            minHeightLabel.Text = "Min Height:" + minHeightBar.Value;
        }

        private void sizeBar_Scroll(object sender, EventArgs e)
        {
            minWidthLabel.Text = "Min Width:" + minWidthBar.Value;
        }

        private void thresholdBar_Scroll(object sender, EventArgs e)
        {
            thresholdLabel.Text = "Threshold:" + thresholdBar.Value;
        }

        

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cell Imager allows for performing imaging routines on individual cells based on blob detection." +
                " Set threshold and size range for detection. Also allows for an imaging script to be run on each cell.");
        }

        private void detectBut_Click(object sender, EventArgs e)
        {
            b = Microscope.TakeImage(false,false,false);
            bitmap = new System.Drawing.Bitmap(b.SizeX, b.SizeY);
            
            Statistics[] sts = Statistics.FromBytes(b.Buffers[0]);
            Statistics st = sts[0];
            Threshold th;
            if (numericRBut.Checked)
            {
                th = new Threshold(thresholdBar.Value);
            }
            else
                th = new Threshold((int)(st.Median));
            AForge.Bitmap bf = b.GetBitmap(0, (int)chanBox.Value-1, 0);
            if (bf.RGBChannelsCount > 1)
            {
                ExtractChannel ch = new ExtractChannel((short)(rgbBox.Value-1));
                bf = ch.Apply(bf);
            }
            th.ApplyInPlace(bf);
            Invert inv = new Invert();
            inv.ApplyInPlace(bf);
            AForge.Bitmap det;
            if (bf.BitsPerPixel > 8)
                det = AForge.Imaging.Image.Convert16bppTo8bpp(bf);
            else
                det = bf;
            pictureBox.Image = new System.Drawing.Bitmap(det.Width, det.Height, det.Width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, det.RGBData);
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(det);
            Blob[] bls = blobCounter.GetObjectsInformation();
            blobs.Clear();
            foreach (Blob blob in bls)
            {
                if (blob.Rectangle.Width < minWidthBar.Value || blob.Rectangle.Height < minHeightBar.Value || blob.Rectangle.Width > maxWidthBar.Value || blob.Rectangle.Height > maxHeightBar.Value)
                    continue;
                blobs.Add(blob);
            }
            UpdateItems();
        }

        private void UpdateItems()
        {
            listView.Clear();
            foreach (Blob blob in blobs)
            {
                ListViewItem item = new ListViewItem(blob.Rectangle.Width + "," + blob.Rectangle.Height);
                item.Tag = blob;
                listView.Items.Add(item);
            }
        }

        public void Image(Blob[] blobs)
        {
            Point3D p = Microscope.GetPosition();
            int o = Microscope.Objectives.Index;
            foreach (Blob blob in blobs)
            {
                if (blob.Rectangle.Width < minWidthBar.Value || blob.Rectangle.Height < minHeightBar.Value || blob.Rectangle.Width > maxWidthBar.Value || blob.Rectangle.Height > maxHeightBar.Value)
                    continue;
                PointD loc = new PointD(b.StageSizeX + (blob.Rectangle.X * b.PhysicalSizeX), b.StageSizeY + (blob.Rectangle.Y * b.PhysicalSizeY));
                Microscope.Objectives.SetPosition(objectivesBox.SelectedIndex);
                Microscope.SetPosition(loc);
                if(tileBox.Checked)
                {
                    for (int y = 0; y < tileYBox.Value; y++)
                    {
                        for (int x = 0; x < tileXBox.Value; x++)
                        {
                            if (comboBox.SelectedIndex != -1)
                                ((Scripting.Script)comboBox.SelectedItem).Run();
                            else
                            images.AddRange(Microscope.TakeTiles((int)tileXBox.Value,(int)tileYBox.Value));
                        }
                    }
                }
                else
                {
                    if (comboBox.SelectedIndex != -1)
                        ((Scripting.Script)comboBox.SelectedItem).Run();
                    else
                        images.Add(Microscope.TakeImage(false,false,false));
                }
            }
            Microscope.Objectives.SetPosition(o);
            Microscope.SetPosition(p);
            viewer.Images.Clear();
            viewer.Images.AddRange(images);
            viewer.UpdateView();
        }

        private void clearBut_Click(object sender, EventArgs e)
        {
            viewer.RemoveImages();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if(bitmap==null)
                return;
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            int i = 0;
            Pen bb = new Pen(Brushes.Blue, 2);
            Pen r = new Pen(Brushes.Red, 2);
            foreach (Blob b in blobs)
            {
                if (listView.Items[i].Selected)
                    g.DrawRectangle(bb, new System.Drawing.Rectangle(b.Rectangle.X, b.Rectangle.Y, b.Rectangle.Width, b.Rectangle.Height));
                else
                    g.DrawRectangle(r, new System.Drawing.Rectangle(b.Rectangle.X, b.Rectangle.Y, b.Rectangle.Width, b.Rectangle.Height));
                i++;
            }
            bb.Dispose();
            r.Dispose();
            g.Dispose();
            e.Graphics.DrawImage(bitmap, 0, 0,pictureBox.Width,pictureBox.Height);
        }

        private void imageAllBut_Click(object sender, EventArgs e)
        {
            Image(blobs.ToArray());
        }

        private void imageCurBut_Click(object sender, EventArgs e)
        {
            List<Blob> blobs = new List<Blob>();
            foreach (ListViewItem blob in listView.Items)
            {
                if(blob.Selected)
                blobs.Add((Blob)blob.Tag);
            }
            Image(blobs.ToArray());
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }
    }
}
