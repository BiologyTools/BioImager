using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bio.Graphics;
using SharpDX;

namespace Bio
{
    public partial class View3D : Form
    {
        DSystem sys = null;
        List<BufferInfo> Buffers = new List<BufferInfo>();
        Point3D Origin = new Point3D(0.01f, 0.01f, -1);
        Vector3 r = new Vector3(0, (float)Math.PI, (float)Math.PI);
        SizeF Scale = new SizeF(60, 60);
        Matrix rot = Matrix.Identity;
        bool update = true;
        bool fullScreen = false;
        public View3D()
        {
            InitializeComponent();
            MouseWheel += new System.Windows.Forms.MouseEventHandler(ImageView_MouseWheel);
            Initialize();
        }
        private void UpdateStatus()
        {
            toolStripStatusLabel.Text = Origin.ToString() + " Zoom:" + Scale.Width;
        }
        private void UpdateImage()
        {
            sys.Graphics.Initialize(sys.Configuration, dxPanel.Handle, ImageView.SelectedImage);

        }
        Matrix world = Matrix.Identity;
        private void Initialize()
        {
            sys = new DSystem();
            sys.Initialize("3D View", dxPanel.Width, dxPanel.Height, false, fullScreen, dxPanel.Handle);
            //sys.Graphics.Camera.SetPosition((float)Origin.X, (float)Origin.Y, -10);
            sys.Configuration.Title = "3D View";
            sys.Configuration.Width = dxPanel.Width;
            sys.Configuration.Height = dxPanel.Height;
            rot = Matrix.RotationX(r.X) * Matrix.RotationY(r.Y) * Matrix.RotationZ(r.Z);
            world = rot * Matrix.Scaling(Scale.Width, Scale.Height, 1);
            sys.Graphics.D3D.WorldMatrix = world;
            sys.Graphics.Initialize(sys.Configuration, dxPanel.Handle, ImageView.SelectedImage);
            //sys.Graphics.Frame((float)App.viewer.Origin.X, (float)App.viewer.Origin.Y, dxPanel.Width, dxPanel.Height);
            sys.Graphics.Frame();
        }
        public void UpdateView()
        {
            sys.Configuration.Width = dxPanel.Width;
            sys.Configuration.Height = dxPanel.Height;
            rot = Matrix.RotationX(r.X) * Matrix.RotationY(r.Y) * Matrix.RotationZ(r.Z);
            world = rot * Matrix.Scaling(Scale.Width, Scale.Height, 1);
            sys.Graphics.D3D.WorldMatrix = world;
            //sys.Graphics.Camera.SetPosition((float)-Origin.X, (float)-Origin.Y,-10);
            //sys.Graphics.Frame((float)App.viewer.Origin.X, (float)App.viewer.Origin.Y, dxPanel.Width, dxPanel.Height);
            sys.Graphics.Frame();
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
        private void dxPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = e.Location;
        }

        private void dxPanel_MouseMove(object sender, MouseEventArgs e)
        {
        }
        private void dxPanel_MouseUp(object sender, MouseEventArgs e)
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
            float moveAmount = 0.1f;
            if(e.KeyCode == Keys.Up)
            {
                Origin.Y += moveAmount;
            }
            if (e.KeyCode == Keys.Down)
            {
                Origin.Y -= moveAmount;
            }
            if (e.KeyCode == Keys.Left)
            {
                Origin.X -= moveAmount;
            }
            if (e.KeyCode == Keys.Right)
            {
                Origin.X += moveAmount;
            }
            if (e.KeyCode == Keys.W)
            {
                r.X += 5.0f * ((float)Math.PI / 180.0f);
            }
            if (e.KeyCode == Keys.S)
            {
                r.X -= 5.0f * ((float)Math.PI / 180.0f);
            }
            if (e.KeyCode == Keys.A)
            {
                r.Y += 5.0f * ((float)Math.PI / 180.0f);
            }
            if (e.KeyCode == Keys.D)
            {
                r.Y -= 5.0f * ((float)Math.PI / 180.0f);
            }
            if (e.KeyCode == Keys.Q)
            {
                r.Z += 5.0f * ((float)Math.PI / 180.0f);
            }
            if (e.KeyCode == Keys.E)
            {
                r.Z -= 5.0f * ((float)Math.PI / 180.0f);
            }
            rot = Matrix.RotationX(r.X) * Matrix.RotationY(r.Y) * Matrix.RotationZ(r.Z);
            sys.Graphics.Camera.SetPosition((float)Origin.X, (float)Origin.Y, (float)Origin.Z);
            UpdateStatus();
            UpdateView();
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
            UpdateStatus();
        }
    }
}
