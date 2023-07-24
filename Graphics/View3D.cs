using AForge;
using Bio.Graphics;
using SharpDX;

namespace Bio
{
    public partial class View3D : Form
    {
        DSystem sys = null;
        List<BufferInfo> Buffers = new List<BufferInfo>();
        private static Vector3 origin = new Vector3(0f, -1f, -2f);
        Vector3 Origin = origin;
        Vector3 r = new Vector3(0, (float)Math.PI, (float)Math.PI);
        SizeF Scale = new SizeF(1f, 1f);
        Matrix rot = Matrix.Identity;
        bool update = true;
        bool fullScreen = false;

        public static IntRange RRange { get; set; }
        public static IntRange GRange { get; set; }
        public static IntRange BRange { get; set; }
        public static bool Ctrl
        {
            get
            {
                return Win32.GetKeyState(Keys.LControlKey);
            }
        }
        public View3D(BioImage im)
        {
            InitializeComponent();
            Initialize();           
            MouseWheel += new System.Windows.Forms.MouseEventHandler(ImageView_MouseWheel);
            RRange = im.RRange;
            GRange = im.GRange;
            BRange = im.BRange;
            
            rMinBox.Value = im.RRange.Min;
            gMinBox.Value = im.GRange.Min;
            bMinBox.Value = im.BRange.Min;
            rMaxBox.Value = im.RRange.Max;
            gMaxBox.Value = im.GRange.Max;
            bMaxBox.Value = im.BRange.Max;
            UpdateView();
            UpdateStatus();
        }
        private void UpdateStatus()
        {
            toolStripStatusLabel.Text = Origin.ToString() + " Zoom:" + Scale.Width;
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
            world = rot;// * Matrix.Scaling(Scale.Width, Scale.Height, 1);
            sys.Graphics.D3D.WorldMatrix = world;
            sys.Graphics.Initialize(sys.Configuration, dxPanel.Handle, ImageView.SelectedImage);
            sys.Graphics.Frame(RRange, GRange, BRange, (float)frameIntervalBox.Value, (float)transparencyBox.Value);
        }
        public void UpdateView()
        {
            if (sys == null)
                return;
            sys.Configuration.Width = dxPanel.Width;
            sys.Configuration.Height = dxPanel.Height;
            rot = Matrix.RotationX(r.X) * Matrix.RotationY(r.Y) * Matrix.RotationZ(r.Z);
            world = rot * Matrix.Scaling(Scale.Width, Scale.Height, 1);
            sys.Graphics.D3D.WorldMatrix = world;
            sys.Graphics.Camera.SetPosition(Origin.X, Origin.Y, Origin.Z);
            sys.Graphics.Frame(RRange, GRange, BRange, (float)frameIntervalBox.Value, (float)transparencyBox.Value);
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
            if (e.Button == MouseButtons.Middle && !Ctrl)
            {
                Origin.X -= (e.X - mouseDown.X) * 0.01f;
                Origin.Y += (e.Y - mouseDown.Y) * 0.01f;
                mouseDown = e.Location;
                UpdateView();
            }
            if (e.Button == MouseButtons.Middle && Ctrl)
            {
                r.X += (float)((e.Y - mouseDown.Y) * (Math.PI / 180));
                r.Y += (float)((e.X - mouseDown.X) * (Math.PI / 180));
                mouseDown = e.Location;
                UpdateView();
            }
        }
        private void dxPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp = e.Location;
        }

        private void View3D_KeyDown(object sender, KeyEventArgs e)
        {
            dxPanel.Focus();
            float moveAmount = 0.1f;
            if (e.KeyCode == Keys.Up)
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

        private void trackBarRMin_ValueChanged(object sender, EventArgs e)
        {
            RRange = new IntRange((int)rMinBox.Value, (int)rMaxBox.Value);
            GRange = new IntRange((int)gMinBox.Value, (int)gMaxBox.Value);
            BRange = new IntRange((int)bMinBox.Value, (int)bMaxBox.Value);
            UpdateView();
        }

        private void View3D_FormClosing(object sender, FormClosingEventArgs e)
        {
            sys.ShutDown();
        }

        private void frameIntervalBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void transparencyBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void resetCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Origin = origin;
            UpdateView();
        }
    }
}
