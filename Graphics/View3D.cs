using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bio.Graphics.DX;
using SharpDX;

namespace Bio
{
    public partial class View2D : Form
    {
        DSystem sys = null;
        List<BioImage> Images = new List<BioImage>();
        List<BufferInfo> Buffers = new List<BufferInfo>();
        PointD Origin = new PointD();
        SizeF Scale = new SizeF(1, 1);
        bool update = true;
        float zoom = 10;
        bool fullScreen = false;
        public View2D(List<BioImage> b)
        {
            InitializeComponent();
            MouseWheel += new System.Windows.Forms.MouseEventHandler(ImageView_MouseWheel);
            Images = b;
            for (int i = 0; i < b.Count; i++)
            {
                Buffers.Add(Images[i].SelectedBuffer);
            }
            Initialize();
            Origin = App.viewer.Origin;
            // Change parent for overlay PictureBox.
            /*
            overlayPictureBox.Parent = pictureBox;
            overlayPictureBox.Location = new Point(0, 0);
            */
        }
        private void UpdateImage()
        {
            sys.Graphics.Initialize(sys.Configuration, pictureBox.Handle, Images, zoom);
            for (int i = 0; i < sys.Graphics.Bitmaps.Count; i++)
            {
                sys.Graphics.Bitmaps[i].Texture.TextureResource.Dispose();
                sys.Graphics.Bitmaps[i].Texture.ShutDown();
                sys.Graphics.Bitmaps[i].Texture = null;
                sys.Graphics.Bitmaps[i].Shutdown();
                sys.Graphics.Bitmaps[i] = null;
                GC.Collect();
                sys.Graphics.Bitmaps[i] = new Graphics.DX.Graphics.DBitmap();
                sys.Graphics.Bitmaps[i].Initialize(sys.Graphics.Device, pictureBox.Width, pictureBox.Height, Images[i].SelectedBuffer);
            }
        }
        Matrix world = Matrix.Identity;
        private void Initialize()
        {
            sys = new DSystem();
            sys.Initialize("3D View", pictureBox.Width, pictureBox.Height, false, fullScreen, Images, pictureBox.Handle);
            sys.Graphics.Camera.SetPosition((float)Origin.X, (float)Origin.Y, -10);
            sys.Configuration.Title = "3D View";
            sys.Configuration.Width = pictureBox.Width;
            sys.Configuration.Height = pictureBox.Height;
            world = Matrix.Scaling(Scale.Width, Scale.Height, 1);
            sys.Graphics.Initialize(sys.Configuration, pictureBox.Handle, Images, zoom);
            //sys.Graphics.Frame((float)App.viewer.Origin.X, (float)App.viewer.Origin.Y, pictureBox.Width, pictureBox.Height);
            sys.Graphics.Frame(0, 0, pictureBox.Width, pictureBox.Height, world);
        }
        public void UpdateView()
        {
            sys.Configuration.Width = pictureBox.Width;
            sys.Configuration.Height = pictureBox.Height;
            world = Matrix.Scaling(Scale.Width, Scale.Height, 1);
            sys.Graphics.Camera.SetPosition((float)-Origin.X, (float)-Origin.Y,-10);
            //sys.Graphics.Frame((float)App.viewer.Origin.X, (float)App.viewer.Origin.Y, pictureBox.Width, pictureBox.Height);
            sys.Graphics.Frame(0, 0, pictureBox.Width, pictureBox.Height,world);
            if (update)
            UpdateImage();
            update = false;
        }

        private void View3D_Resize(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void View3D_Paint(object sender, PaintEventArgs e)
        {
            UpdateView();
        }
        System.Drawing.Point mouseDown = new System.Drawing.Point();
        System.Drawing.Point mouseUp = new System.Drawing.Point();
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp = e.Location;
            if (e.Button == MouseButtons.Middle)
            {
                Origin.X = -(mouseUp.X - mouseDown.X);
                Origin.Y = -(mouseUp.Y - mouseDown.Y);
                UpdateView();
            }
        }

        private void View3D_KeyDown(object sender, KeyEventArgs e)
        {
            float moveAmount = 20;
            if(e.KeyCode == Keys.Up)
            {
                Origin.Y += moveAmount;
                UpdateView();
            }
            if (e.KeyCode == Keys.Down)
            {
                Origin.Y -= moveAmount;
                UpdateView();
            }
            if (e.KeyCode == Keys.Left)
            {
                Origin.X -= moveAmount;
                UpdateView();
            }
            if (e.KeyCode == Keys.Right)
            {
                Origin.X += moveAmount;
                UpdateView();
            }
            
        }
        private void ImageView_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            float dx = Scale.Width / 50;
            float dy = Scale.Height / 50;
            if (e.Delta > 0)
            {
                Scale = new SizeF(Scale.Width + dx, Scale.Height + dy);
            }
            else
            {
                Scale = new SizeF(Scale.Width - dx, Scale.Height - dy);
            }
            UpdateView();
        }

        
    }
}
