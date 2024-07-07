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
    public partial class ImageWindow : Form
    {
        /* Creating a new window with the image. */
        public ImageWindow(BioImage im)
        {
            InitializeComponent();
            ImageView iv = new ImageView(im);
            iv.Dock = DockStyle.Fill;
            this.Controls.Add(iv);
            this.Text = System.IO.Path.GetFileName(im.ID);
            this.Size = new Size(im.SizeX, im.SizeY);
            this.Show();
        }
    }
}
