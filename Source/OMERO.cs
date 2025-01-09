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
        private List<ListViewItem> items = new List<ListViewItem>();
        public int selectedIndex = 0;
        public int Progress
        {
            get { return progressBar.Value; }
            set 
            { 
                if(value < 100)
                progressBar.Value = value;

            }
        }
        public string Status
        {
            get { return statusLabel.Text; }
            set { statusLabel.Text = value; }
        }
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
            items.Clear();
            string[] sts = comboBox1.SelectedItem.ToString().Split(" ");
            List<ListViewItem> lss = new List<ListViewItem>();
            foreach (omero.gateway.model.DatasetData s in datas)
            {
                if (s.getId().ToString() == sts[1].ToString())
                {
                    Status = "Getting dataset information.";
                    List<string> fs = new List<string>();
                    List<long> ids = BioLib.OMERO.GetDatasetIds(s.getId());
                    foreach (long id in ids)
                    {
                        fs.Add(BioLib.OMERO.GetNameFromID(id));
                    }
                    if (loadIconsBox.Checked)
                    {
                        Status = "Loading icons...";
                        // Create ImageList
                        ImageList ims = new ImageList
                        {
                            ImageSize = new System.Drawing.Size(64, 64), // Set icon size (width x height)
                            ColorDepth = ColorDepth.Depth32Bit // Set color depth
                        };
                        Dictionary<long, Pixbuf> dict = BioLib.OMERO.GetThumbnails(fs.ToArray(), 64, 64);
                        int i = 0;
                        Status = "Loading files...";
                        foreach (var item in ids)
                        {
                            foreach (var itm in dict)
                            {
                                if(item == itm.Key)
                                {
                                    Bitmap bm = PixbufToBitmap(itm.Value);
                                    ims.Images.Add(bm);
                                    ListViewItem li = new ListViewItem();
                                    li.Text = fs[i];
                                    li.ImageIndex = i;
                                    items.Add(li);
                                    Progress = (int)(((float)(i + 1) / (float)(ids.Count)) * 100);
                                    i++;
                                }
                            }
                        }
                        listView1.SmallImageList = ims;
                        listView1.LargeImageList = ims;
                        listView1.View = View.LargeIcon;
                        listView1.Items.AddRange(items.ToArray());
                        Status = "";
                        Progress = 0;
                        return;
                    }
                    else
                    {
                        Status = "Loading files...";
                        int i = 0;
                        foreach (var item in ids)
                        {
                            ListViewItem li = new ListViewItem();
                            li.Text = fs[i];
                            items.Add(li);
                            Progress = (int)(((float)(i + 1) / (float)(ids.Count)) * 100);
                            i++;
                        }
                        listView1.View = View.List;
                        listView1.Items.AddRange(items.ToArray());
                        Status = "";
                        Progress = 0;
                        return;
                    }
                }
            }
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

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (ListViewItem item in items)
            {
                if(item.Text.Contains(searchBox.Text))
                    list.Add(item);
            }
            listView1.Items.AddRange(list.ToArray());
        }
    }
}
