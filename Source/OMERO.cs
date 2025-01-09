using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BioLib;
using Gdk;
using Graphics = System.Drawing.Graphics;
using Bitmap = System.Drawing.Bitmap;
using OMERO = BioLib.OMERO;
using Rectangle = System.Drawing.Rectangle;
namespace BioImager
{
    public partial class OMERO : Form
    {
        public List<omero.gateway.model.DatasetData> datas = new List<omero.gateway.model.DatasetData>();
        public int selectedIndex = 0;
        public OMERO()
        {
            InitializeComponent();
            Login log = new Login();
            if (log.ShowDialog() == DialogResult.OK)
            {
                BioLib.OMERO.Connect(Login.host, Login.port, Login.username, Login.password);
                Init();
            }
        }
        private void Init()
        {
            BioLib.OMERO.ReConnect();
            var dst = BioLib.OMERO.GetDatasetsData();
            datas.AddRange(dst);
            foreach (var src in dst)
            {
                if (src != null)
                {
                    comboBox1.Items.Add(src.getName() + " " + src.getId());
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string[] sts = comboBox1.SelectedItem.ToString().Split(" ");
            foreach (omero.gateway.model.DatasetData s in datas)
            {
                if (s.getId().ToString() == sts[1].ToString())
                {
                    List<string> fs = BioLib.OMERO.GetDatasetFiles(s.getId());
                    ImageList ims = new ImageList();
                    Dictionary<long, Pixbuf> dict = BioLib.OMERO.GetThumbnails(fs.ToArray(), 32,32);

                    foreach (Pixbuf img in dict.Values)
                    {
                        Bitmap bm = PixbufToBitmap(img);
                        ims.Images.Add(bm);
                    }
                    listView1.SmallImageList = ims;
                    listView1.LargeImageList = ims;
                    int i = 0;
                    foreach (var item in fs)
                    {
                        ListViewItem li = new ListViewItem();
                        li.Text = item;
                        li.ImageIndex = i;
                        listView1.Items.Add(li);
                        i++;
                    }
                    listView1.View = View.LargeIcon;
                    return;
                }
            }
            selectedIndex = listView1.SelectedIndices[0];
        }
        public static Bitmap PixbufToBitmap(Pixbuf pixbuf)
        {
            if (pixbuf == null)
                throw new ArgumentNullException(nameof(pixbuf), "Pixbuf cannot be null.");

            // Get Pixbuf properties
            int width = pixbuf.Width;
            int height = pixbuf.Height;
            int rowstride = pixbuf.Rowstride;
            bool hasAlpha = pixbuf.HasAlpha;
            byte[] pixelData = pixbuf.PixelBytes.Data;

            // Create a Bitmap
            PixelFormat pixelFormat = hasAlpha ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb;
            Bitmap bitmap = new Bitmap(width, height, pixelFormat);

            // Lock the Bitmap's data
            BitmapData bmpData = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                pixelFormat);

            // Copy pixel data
            IntPtr bmpPtr = bmpData.Scan0;
            unsafe
            {
                byte* srcPtr = (byte*)pixbuf.Pixels;
                byte* destPtr = (byte*)bmpPtr;

                for (int y = 0; y < height; y++)
                {
                    byte* srcRow = srcPtr + y * rowstride;
                    byte* destRow = destPtr + y * bmpData.Stride;

                    if (hasAlpha)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            destRow[x * 4 + 0] = srcRow[x * 4 + 2]; // B
                            destRow[x * 4 + 1] = srcRow[x * 4 + 1]; // G
                            destRow[x * 4 + 2] = srcRow[x * 4 + 0]; // R
                            destRow[x * 4 + 3] = srcRow[x * 4 + 3]; // A
                        }
                    }
                    else
                    {
                        for (int x = 0; x < width; x++)
                        {
                            destRow[x * 3 + 0] = srcRow[x * 3 + 2]; // B
                            destRow[x * 3 + 1] = srcRow[x * 3 + 1]; // G
                            destRow[x * 3 + 2] = srcRow[x * 3 + 0]; // R
                        }
                    }
                }
            }

            // Unlock and return the Bitmap
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
                return;
            string[] f = listView1.SelectedItems[0].Text.Split(' ');
            BioImage b = BioLib.OMERO.GetImage(f[0], datas[selectedIndex].getId());
            App.tabsView.AddTab(b);
        }
    }
}
