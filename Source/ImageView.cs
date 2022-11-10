using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Bio.Graphics;
using Pen = System.Drawing.Pen;
using System.Threading;
using SharpDX.Mathematics.Interop;

namespace Bio
{
    public partial class ImageView : UserControl, IDisposable
    {
        public ImageView(BioImage im)
        {
            string file = im.ID.Replace("\\", "/");
            InitializeComponent();
            serie = im.series;
            selectedImage = im;
            tools = new Tools();
            Dock = DockStyle.Fill;
            Images.Add(im);
            App.viewer = this;
            if (file == "" || file == null)
                return;
            SetCoordinate(0, 0, 0);
            InitGUI();
            //Buf = image.GetBufByCoord(GetCoordinate());
            MouseWheel += new System.Windows.Forms.MouseEventHandler(ImageView_MouseWheel);
            zBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(ZTrackBar_MouseWheel);
            cBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(CTrackBar_MouseWheel);
            tBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(TimeTrackBar_MouseWheel);
            //We set the trackbar event to handled so that it only scrolls one tick not the default multiple.
            zBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            tBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            cBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            TimeFps = 60;
            ZFps = 60;
            CFps = 1;
            // Change parent for overlay PictureBox.
            overlayPictureBox.Parent = pictureBox;
            overlayPictureBox.Location = new Point(0, 0);
            resolutions = im.Resolutions;
            if (im.isPyramidal)
            {
                hScrollBar.Maximum = im.Resolutions[resolution].SizeX;
                vScrollBar.Maximum = im.Resolutions[resolution].SizeY;
                hScrollBar.Visible = true;
                vScrollBar.Visible = true;
            }
            else
            {
                hScrollBar.Visible = false;
                vScrollBar.Visible = false;
            }
            update = true;
            UpdateImages();
            GoToImage();
            Mode = ViewMode.Filtered;
            Resolution = im.series;
            UpdateView();
            dx = new Direct2D();
            dx.Initialize(new Configuration("BioImager", dxPanel.Width, dxPanel.Height), dxPanel.Handle);
            
        }
        public ImageView()
        {
            InitializeComponent();
            tools = new Tools();
            Dock = DockStyle.Fill;
            App.viewer = this;
            SetCoordinate(0, 0, 0);
            InitGUI();
            //Buf = image.GetBufByCoord(GetCoordinate());
            MouseWheel += new System.Windows.Forms.MouseEventHandler(ImageView_MouseWheel);
            zBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(ZTrackBar_MouseWheel);
            cBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(CTrackBar_MouseWheel);
            tBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(TimeTrackBar_MouseWheel);
            //We set the trackbar event to handled so that it only scrolls one tick not the default multiple.
            zBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            tBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            cBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            TimeFps = 60;
            ZFps = 60;
            CFps = 1;
            // Change parent for overlay PictureBox.
            overlayPictureBox.Parent = pictureBox;
            overlayPictureBox.Location = new Point(0, 0);

            update = true;
            UpdateImages();
            GoToImage();
            UpdateView();
            dx = new Direct2D();
            dx.Initialize(new Configuration("BioImager", dxPanel.Width, dxPanel.Height), dxPanel.Handle);
        }
        ~ImageView()
        {

        }

        List<Bitmap> Bitmaps = new List<Bitmap>();
        List<Resolution> resolutions = new List<Resolution>();
        public static List<ROI> selectedAnnotations = new List<ROI>();
        private static BioImage selectedImage = null;
        private static int selectedIndex = 0;
        public Direct2D dx = null;
        public static BioImage SelectedImage
        {
            get
            {
                return selectedImage;
            }
        }
        public static BufferInfo SelectedBuffer
        {
            get
            {
                int ind = SelectedImage.Coords[SelectedImage.Coordinate.Z, SelectedImage.Coordinate.C, SelectedImage.Coordinate.T];
                return selectedImage.Buffers[ind];
            }
        }
        public static PointD mouseDown;
        public static bool down;
        public static PointD mouseUp;
        public static bool up;
        public static bool Ctrl
        {
            get
            {
                return Win32.GetKeyState(Keys.LControlKey);
            }
        }
        private bool x1State = false;
        private bool x2State = false;
        public static MouseButtons mouseUpButtons;
        public static MouseButtons mouseDownButtons;
        private PointD pd;
        public static bool showBounds = true;
        public static bool showText = true;
        public Image Buf = null;
        public bool init = false;
        private bool update = false;
        private bool updateOverlay = false;
        public List<BioImage> Images = new List<BioImage>();
        SharpDX.Direct2D1.Bitmap[] dBitmaps;
        private static int selIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return selIndex;
            }
            set
            {
                if (value >= 0 && Images.Count > 0)
                {
                    selIndex = value;
                    selectedImage = Images[selIndex];
                    InitGUI();
                }
            }
        }
        public Tools tools;
        public string filepath = "";
        public int serie = 0;
        public int minSizeX = 50;
        public int minSizeY = 20;
        public bool loopZ = true;
        public bool loopT = true;
        public bool loopC = true;
        private double pxWmicron = 0.004;
        private double pxHmicron = 0.004;
        SizeF scale = new SizeF(1, 1);
        public void SetCoordinate(int z, int c, int t)
        {
            if (SelectedImage == null)
                return;
            if (z >= SelectedImage.SizeZ)
                zBar.Value = zBar.Maximum;
            if (c >= SelectedImage.SizeC)
                cBar.Value = cBar.Maximum;
            if (t >= SelectedImage.SizeT)
                tBar.Value = tBar.Maximum;
            zBar.Value = z;
            cBar.Value = c;
            tBar.Value = t;
        }
        public ZCT GetCoordinate()
        {
            return SelectedImage.Coordinate;
        }
        public void AddImage(BioImage im)
        {
            Images.Add(im);
            SelectedIndex = Images.Count - 1;
            InitGUI();
            UpdateImages();
            GoToImage(Images.Count - 1);
        }
        private bool showControls = true;
        public bool ShowControls
        {
            get { return trackBarPanel.Visible; }
            set
            {
                showControls = value;
                if (!value)
                {
                    trackBarPanel.Hide();
                    if (ShowStatus)
                    {
                        pictureBox.Top = 25;
                        overlayPictureBox.Top = 25;
                        dxPanel.Top = 25;
                        pictureBox.Height += 75;
                        overlayPictureBox.Height += 75;
                        dxPanel.Height += 75;
                    }
                    else
                    {
                        pictureBox.Top = 0;
                        overlayPictureBox.Top = 0;
                        dxPanel.Top = 0;
                        pictureBox.Height += 75;
                        overlayPictureBox.Height += 75;
                        dxPanel.Height += 75;
                    }
                    hideControlsToolStripMenuItem1.Text = "Show Controls";
                }
                else
                {
                    trackBarPanel.Show();
                    if (ShowStatus)
                    {
                        pictureBox.Top = 25;
                        overlayPictureBox.Top = 25;
                        dxPanel.Top = 25;
                        pictureBox.Height -= 75;
                        overlayPictureBox.Height -= 75;
                        dxPanel.Height -= 75;
                    }
                    else
                    {
                        pictureBox.Top = 0;
                        overlayPictureBox.Top = 0;
                        dxPanel.Top = 0;
                        pictureBox.Height += 75;
                        overlayPictureBox.Height += 75;
                        dxPanel.Height += 75;
                    }

                    //panel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    hideControlsToolStripMenuItem1.Text = "Hide Controls";
                }
                UpdateView();
            }
        }
        private bool showStatus = true;
        public bool ShowStatus
        {
            get { return showStatus; }
            set
            {
                showStatus = true;
                if (!value)
                {
                    statusPanel.Hide();
                    //panel.Top = 0;
                    pictureBox.Top = 0;
                    overlayPictureBox.Top = 0;
                    dxPanel.Top = 0;
                    //panel.Height += 25;
                    pictureBox.Height += 25;
                    overlayPictureBox.Height += 25;
                    dxPanel.Height += 25;

                    showControlsToolStripMenuItem.Visible = true;
                    hideControlsToolStripMenuItem.Text = "Show Status";
                }
                else
                {
                    statusPanel.Show();
                    statusPanel.Visible = value;
                    pictureBox.Top = 25;
                    overlayPictureBox.Top = 25;
                    dxPanel.Top = 25;
                    showControlsToolStripMenuItem.Visible = false;
                    hideControlsToolStripMenuItem.Text = "Hide Status";
                }
            }
        }
        public double PxWmicron
        {
            get
            {
                return pxWmicron;
            }
            set
            {
                pxWmicron = value;
            }
        }
        public double PxHmicron
        {
            get
            {
                return pxHmicron;
            }
            set
            {
                pxHmicron = value;
            }
        }
        public enum ViewMode
        {
            Raw,
            Filtered,
            RGBImage,
            Emission,
        }
        private ViewMode viewMode = ViewMode.Filtered;
        public ViewMode Mode
        {
            get
            {
                return viewMode;
            }
            set
            {
                viewMode = value;
                //If view mode is changed we update.
                update = true;
                UpdateImages();
                App.tabsView.UpdateViewMode(viewMode);
                UpdateView();
                UpdateOverlay();
                if (viewMode == ViewMode.RGBImage)
                {
                    cBar.Value = 0;
                    rgbBoxsPanel.BringToFront();
                    cBar.SendToBack();
                    cLabel.SendToBack();
                    dxPanel.Visible = false;
                    pictureBox.BringToFront();
                    overlayPictureBox.BringToFront();
                }
                else
                if (viewMode == ViewMode.Filtered)
                {
                    rgbBoxsPanel.SendToBack();
                    cBar.BringToFront();
                    cLabel.BringToFront();
                    dxPanel.Visible = false;
                    pictureBox.BringToFront();
                    overlayPictureBox.BringToFront();
                }
                else
                if (viewMode == ViewMode.Raw)
                {
                    rgbBoxsPanel.SendToBack();
                    cBar.BringToFront();
                    cLabel.BringToFront();
                    dxPanel.Visible = false;
                    pictureBox.BringToFront();
                    overlayPictureBox.BringToFront();
                }
                else
                {
                    cBar.Value = 0;
                    rgbBoxsPanel.BringToFront();
                    cBar.SendToBack();
                    cLabel.SendToBack();
                    dxPanel.Visible = false;
                    //dxPanel.Visible = false;
                    pictureBox.BringToFront();
                    overlayPictureBox.BringToFront();
                }
            }
        }
        public string Path
        {
            get
            {
                return filepath;
            }
        }
        public Channel RChannel
        {
            get
            {
                return SelectedImage.Channels[SelectedImage.rgbChannels[0]];
            }
        }
        public Channel GChannel
        {
            get
            {
                return SelectedImage.Channels[SelectedImage.rgbChannels[1]];
            }
        }
        public Channel BChannel
        {
            get
            {
                return SelectedImage.Channels[SelectedImage.rgbChannels[2]];
            }
        }
        PointD origin = new PointD(0, 0);
        Point pyramidalOrigin = new Point(0, 0);
        public PointD Origin
        {
            get { return origin; }
            set
            {
                origin = value;
                update = true;
            }
        }
        public Point PyramidalOrigin
        {
            get { return pyramidalOrigin; }
            set
            {
                if (hScrollBar.Maximum > value.X && value.X > -1)
                    hScrollBar.Value = value.X;
                if (vScrollBar.Maximum > value.Y && value.Y > -1)
                    vScrollBar.Value = value.Y;
                pyramidalOrigin = value;
                UpdateImage();
                UpdateView();
                //PointF p = ToScreenSpace(pyramidalOrigin);
                //origin = new PointD(p.X, p.Y);
            }
        }
        public int Resolution
        {
            get { return resolution; }
            set
            {
                if (SelectedImage.Resolutions.Count <= value || value < 0)
                    return;
                double x = PyramidalOrigin.X * ((double)SelectedImage.Resolutions[resolution].SizeX / (double)SelectedImage.Resolutions[value].SizeX);
                double y = PyramidalOrigin.Y * ((double)SelectedImage.Resolutions[resolution].SizeY / (double)SelectedImage.Resolutions[value].SizeY);
                hScrollBar.Maximum = SelectedImage.Resolutions[value].SizeX;
                vScrollBar.Maximum = SelectedImage.Resolutions[value].SizeY;
                PyramidalOrigin = new Point((int)x, (int)y);
                resolution = value;
                UpdateImage();
                UpdateView();
            }
        }
        public new SizeF Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                update = true;
                UpdateView();
            }
        }
        public bool HardwareAcceleration
        {
            get
            {
                return dxPanel.Visible;
            }
            set
            {
                if (value)
                {
                    cBar.Value = 0;
                    rgbBoxsPanel.SendToBack();
                    pictureBox.BringToFront();
                    cBar.BringToFront();
                    cLabel.BringToFront();
                    dxPanel.Visible = true;
                    dxPanel.BringToFront();
                }
                else
                {
                    rgbBoxsPanel.SendToBack();
                    cBar.BringToFront();
                    cLabel.BringToFront();
                    dxPanel.Visible = false;
                    pictureBox.BringToFront();
                    overlayPictureBox.BringToFront();
                }
            }
        }
        public void UpdateRGBChannels()
        {
            //Buf = image.GetBufByCoord(GetCoordinate());
            if (channelBoxR.SelectedIndex == -1)
                SelectedImage.rgbChannels[0] = 0;
            else
                SelectedImage.rgbChannels[1] = channelBoxR.SelectedIndex;
            if (channelBoxG.SelectedIndex == -1)
                SelectedImage.rgbChannels[1] = 0;
            else
                SelectedImage.rgbChannels[1] = channelBoxG.SelectedIndex;
            if (channelBoxB.SelectedIndex == -1)
                SelectedImage.rgbChannels[2] = 0;
            else
                SelectedImage.rgbChannels[2] = channelBoxB.SelectedIndex;

        }
        private bool timeEnabled = false;
        private int zfps;
        public int ZFps
        {
            get
            {
                return zfps;
            }
            set
            {
                zfps = value;
                float f = value;
                zTimer.Interval = (int)Math.Floor(1000 / f);
            }
        }
        private int timefps;
        public int TimeFps
        {
            get
            {
                return timefps;
            }
            set
            {
                timefps = value;
                float f = value;
                timelineTimer.Interval = (int)Math.Floor(1000 / f);
            }
        }
        private int cfps;
        public int CFps
        {
            get
            {
                return cfps;
            }
            set
            {
                cfps = value;
                float f = value;
                cTimer.Interval = (int)Math.Floor(1000 / f);
            }
        }
        public void InitGUI()
        {
            if (SelectedImage == null)
                return;
            zBar.Maximum = SelectedImage.SizeZ - 1;
            if (SelectedImage.Buffers[0].RGBChannelsCount == 3)
                cBar.Maximum = 0;
            else
                cBar.Maximum = SelectedImage.SizeC - 1;
            if (SelectedImage.SizeT > 1)
            {
                tBar.Maximum = SelectedImage.SizeT - 1;
                timeEnabled = true;
            }
            else
            {
                tBar.Enabled = false;
                timeEnabled = false;
                tBar.Maximum = SelectedImage.SizeT - 1;
            }
            //rgbPictureBox.Image = image.plane.GetBitmap();
            //we clear the channel comboboxes incase we have channels from previous loaded image.
            channelBoxR.Items.Clear();
            channelBoxG.Items.Clear();
            channelBoxB.Items.Clear();

            foreach (Channel ch in SelectedImage.Channels)
            {
                channelBoxR.Items.Add(ch);
                channelBoxG.Items.Add(ch);
                channelBoxB.Items.Add(ch);
            }
            if (SelectedImage.Channels.Count > 2)
            {
                channelBoxR.SelectedIndex = 0;
                channelBoxG.SelectedIndex = 1;
                channelBoxB.SelectedIndex = 2;
            }
            else
            if (SelectedImage.Channels.Count == 2)
            {
                channelBoxR.SelectedIndex = 0;
                channelBoxG.SelectedIndex = 1;
            }
            UpdateRGBChannels();
            init = true;
        }
        public void UpdateSelectBoxSize(float size)
        {
            ROI.selectBoxSize = size;
        }
        public void UpdateOverlay()
        {
            if (HardwareAcceleration)
            {
                RenderFrame();
                return;
            }
            updateOverlay = true;
            overlayPictureBox.Invalidate();
        }
        public void UpdateStatus()
        {
            if (SelectedImage == null)
                return;
            if (Mode == ViewMode.RGBImage)
            {
                if (timeEnabled)
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (zBar.Maximum + 1) + ", " + (tBar.Value + 1) + "/" + (tBar.Maximum + 1) + ", " +
                        mousePoint + mouseColor + ", " + SelectedImage.Buffers[0].PixelFormat.ToString() + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") ";
                }
                else
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (cBar.Maximum + 1) + ", " + mousePoint + mouseColor + ", " + SelectedImage.Buffers[0].PixelFormat.ToString()
                        + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") ";
                }

            }
            else
            {
                if (timeEnabled)
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (zBar.Maximum + 1) + ", " + (cBar.Value + 1) + "/" + (cBar.Maximum + 1) + ", " + (tBar.Value + 1) + "/" + (tBar.Maximum + 1) + ", " +
                        mousePoint + mouseColor + ", " + SelectedImage.Buffers[0].PixelFormat.ToString() + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") ";
                }
                else
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (zBar.Maximum + 1) + ", " + (cBar.Value + 1) + "/" + (cBar.Maximum + 1) + ", " + mousePoint + mouseColor + ", " +
                        SelectedImage.Buffers[0].PixelFormat.ToString() + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") ";
                }
            }
        }
        public void UpdateView()
        {
            UpdateStatus();
            if(HardwareAcceleration)
            {
                RenderFrame();
                return;
            }
            pictureBox.Invalidate();
            overlayPictureBox.Invalidate();
        }
        public void RenderFrame()
        {
            if (HardwareAcceleration && dx!=null)
            {
                dx.BeginDraw();
                dx.RenderTarget2D.Clear(new SharpDX.Mathematics.Interop.RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
                RectangleF rg = ToScreenRectF(PointD.MinX, PointD.MinY, PointD.MaxX - PointD.MinX, PointD.MaxY - PointD.MinY);
                //dx.RenderTarget2D.Transform = SharpDX.Matrix3x2.Rotation((float)Math.PI);
                dx.RenderTarget2D.FillRectangle(ToRawRectF(rg.X, rg.Y, rg.Width, rg.Height), new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(0.5f, 0.5f, 0.5f, 1.0f)));
                for (int x = 0; x < Images.Count; x++)
                {
                    if (dBitmaps[x] == null)
                        UpdateImages();
                    RectangleF r = ToScreenRectF(Images[x].Volume.Location.X, Images[x].Volume.Location.Y, Images[x].Volume.Width, Images[x].Volume.Height);
                    
                    dx.RenderTarget2D.DrawBitmap(dBitmaps[x], ToRawRectF(r.X, r.Y, r.Width, r.Height), 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    if(Images[x].selected)
                    {
                        dx.RenderTarget2D.DrawRectangle(ToRawRectF(r.X, r.Y, r.Width, r.Height), new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(0.0f, 0.0f, 1.0f, 1.0f)));
                    }
                }
                RectangleD re = Microscope.GetViewRectangle();
                RectangleF vr = ToScreenRectF(re.X, re.Y, re.W, re.H);
                RawRectangleF rr = ToRawRectF(vr.X, vr.Y, vr.Width, vr.Height);
                dx.RenderTarget2D.DrawRectangle(rr, new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 0.0f, 0.0f, 1.0f)));

                SetCoordinate(zBar.Value, cBar.Value, tBar.Value);
                SharpDX.Direct2D1.SolidColorBrush pen = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 0.0f, 0.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush red = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 0.0f, 0.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush green = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(0.0f, 1.0f, 0.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush mag = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 0.0f, 1.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush blue = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(0.0f, 0.0f, 1.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush b = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 1.0f, 0.0f, 1.0f));
                bool bounds = showBounds;
                bool labels = showText;
                foreach (BioImage bi in Images)
                {
                    foreach (ROI an in bi.Annotations)
                    {
                        float w = Math.Abs(Scale.Width);

                        Font fo = new Font(an.font.FontFamily, (float)(an.strokeWidth / w) * an.font.Size);
                        PointF pc = new PointF((float)(an.BoundingBox.X + (an.BoundingBox.W / 2)), (float)(an.BoundingBox.Y + (an.BoundingBox.H / 2)));
                        float width = (float)ToViewSizeW(ROI.selectBoxSize / w);
                        if (an.type == ROI.Type.Point)
                        {
                            PointF pf = ToScreenSpace(new PointF((float)an.Point.X + 1, (float)an.Point.Y + 1));
                            dx.RenderTarget2D.DrawLine(an.Point.ToRaw(), new RawVector2(pf.X,pf.Y), b);
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(width));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height),red);
                            }
                        }
                        else
                        if (an.type == ROI.Type.Line)
                        {
                            RawVector2 pf = ToScreenSpace(an.GetPoint(0)).ToRaw();
                            RawVector2 pf2 = ToScreenSpace(an.GetPoint(1)).ToRaw();
                            dx.RenderTarget2D.DrawLine(pf,pf2, b);
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(width));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        else
                        if (an.type == ROI.Type.Rectangle && an.Rect.W > 0 && an.Rect.H > 0)
                        {
                            RectangleF rec = ToScreenSpace(an.Rect);
                            dx.RenderTarget2D.DrawRectangle(ToRawRectF(rec.X, rec.Y, rec.Width, rec.Height), red);
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(width));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        else
                        if (an.type == ROI.Type.Ellipse)
                        {
                            RectangleF r = ToScreenSpace(an.BoundingBox);
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(width));
                            RawRectangleF rf = ToRawRectF(rfs[0].X, rfs[0].Y, rfs[0].Width, rfs[0].Height);
                            SharpDX.Direct2D1.Ellipse e = new SharpDX.Direct2D1.Ellipse(new RawVector2(rf.Left + (Math.Abs(r.Width) / 2) - (rfs[0].Width/2), rf.Top + (Math.Abs(r.Height) / 2) - (rfs[0].Height/2)), -r.Width / 2 , -r.Height / 2);
                            dx.RenderTarget2D.DrawEllipse(e, b);
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        else
                        if (an.type == ROI.Type.Polygon && an.closed)
                        {
                            RawVector2 pf;
                            RawVector2 pf2;
                            for (int i = 0; i < an.PointsD.Count-1; i++)
                            {
                                pf = ToScreenSpace(an.GetPoint(i)).ToRaw();
                                pf2 = ToScreenSpace(an.GetPoint(i+1)).ToRaw();
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                            }
                            pf = ToScreenSpace(an.GetPoint(0)).ToRaw();
                            pf2 = ToScreenSpace(an.GetPoint(an.PointsD.Count - 1)).ToRaw();
                            dx.RenderTarget2D.DrawLine(pf, pf2, b);

                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(width));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        else
                        if (an.type == ROI.Type.Polygon && !an.closed || an.type == ROI.Type.Polyline)
                        {
                            RawVector2 pf;
                            RawVector2 pf2;
                            for (int i = 0; i < an.PointsD.Count - 1; i++)
                            {
                                pf = ToScreenSpace(an.GetPoint(i)).ToRaw();
                                pf2 = ToScreenSpace(an.GetPoint(i + 1)).ToRaw();
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                            }

                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(width));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }

                        }
                        else
                        if (an.type == ROI.Type.Freeform && an.closed)
                        {
                            RawVector2 pf;
                            RawVector2 pf2;
                            for (int i = 0; i < an.PointsD.Count - 1; i++)
                            {
                                pf = ToScreenSpace(an.GetPoint(i)).ToRaw();
                                pf2 = ToScreenSpace(an.GetPoint(i + 1)).ToRaw();
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                            }
                            pf = ToScreenSpace(an.GetPoint(0)).ToRaw();
                            pf2 = ToScreenSpace(an.GetPoint(an.PointsD.Count - 1)).ToRaw();
                            dx.RenderTarget2D.DrawLine(pf, pf2, b);
                        }
                        if (an.type == ROI.Type.Label)
                        {
                            RectangleF rec = ToScreenSpace(an.Rect);
                            RawRectangleF r =ToRawRectF(rec.X,rec.Y,rec.Width,rec.Height);
                            SharpDX.DirectWrite.TextFormat tex = new SharpDX.DirectWrite.TextFormat(dx.FactoryDWrite, an.font.FontFamily.ToString(), an.font.Size);
                            dx.RenderTarget2D.DrawText(an.Text, tex, r, b);
                            tex.Dispose();
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(width));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        if (labels)
                        {
                            //Lets draw the text of this ROI in the middle of the RO
                            float fw = ((float)an.Rect.X + ((float)an.Rect.W / 2)) - ((float)an.TextSize.Width / 2);
                            float fh = ((float)an.Rect.Y + ((float)an.Rect.H / 2)) - ((float)an.TextSize.Height / 2);
                            RawRectangleF r = ToRawRectF(fw, fh, an.TextSize.Width, an.TextSize.Height);
                            SharpDX.DirectWrite.TextFormat tex = new SharpDX.DirectWrite.TextFormat(dx.FactoryDWrite, an.font.FontFamily.ToString(), an.font.Size);
                            dx.RenderTarget2D.DrawText(an.Text, tex, r, b);
                            tex.Dispose();
                        }
                        if (bounds)
                        {
                            RectangleF r = ToScreenSpace(an.Rect);
                            dx.RenderTarget2D.DrawRectangle(ToRawRectF(r.X, r.Y, r.Width, r.Height), green);
                        }
                        if (an.selected)
                        {
                            //Lets draw the selected bounding box.
                            RectangleF r = ToScreenSpace(an.Rect);
                            dx.RenderTarget2D.DrawRectangle(ToRawRectF(r.X, r.Y, r.Width, r.Height), mag);

                            //Lets draw the selectBoxes.
                            List<RectangleF> rects = new List<RectangleF>();
                            RectangleF[] sels = an.GetSelectBoxes(width);
                            for (int i = 0; i < an.selectedPoints.Count; i++)
                            {
                                if (an.selectedPoints[i] < an.GetPointCount())
                                {
                                    rects.Add(sels[an.selectedPoints[i]]);
                                }
                            }
                            if (rects.Count > 0)
                            {
                                for (int i = 0; i < rects.Count; i++)
                                {
                                    RectangleF ri = ToScreenSpace(rects[i]);
                                    dx.RenderTarget2D.DrawRectangle(ToRawRectF(ri.X,ri.Y,ri.Width,ri.Height),blue);
                                }
                            }
                            rects.Clear();
                        }
                        
                    }
                }
                pen.Dispose();
                red.Dispose();
                mag.Dispose();
                green.Dispose();
                blue.Dispose();
                b.Dispose();
                dx.EndDraw();
                return;
            }
        }
        public void UpdateView(bool refresh)
        {
            UpdateStatus();
            update = refresh;
            if (HardwareAcceleration)
            {
                //dxPanel.Invalidate();
            }
            if (update)
            {
                pictureBox.Invalidate();
                overlayPictureBox.Invalidate();
            }
        }
        public void UpdateImages()
        {
            if (SelectedImage == null)
                return;
            for (int i = 0; i < Bitmaps.Count; i++)
            {
                Bitmaps[i] = null;
            }
            GC.Collect();
            Bitmaps.Clear();
            if (zBar.Maximum != SelectedImage.SizeZ - 1 || tBar.Maximum != SelectedImage.SizeT - 1)
            {
                InitGUI();
            }

            for (int i = 0; i < Images.Count; i++)
            {
                if (dBitmaps != null)
                {
                    if(dBitmaps.Length > i)
                    if (dBitmaps[i] != null)
                    {
                        dBitmaps[i].Dispose();
                        dBitmaps[i] = null;
                    }
                }
                else
                    dBitmaps = new SharpDX.Direct2D1.Bitmap[Images.Count];
            }
            GC.Collect();
            dBitmaps = new SharpDX.Direct2D1.Bitmap[Images.Count];

            int bi = 0;
            foreach (BioImage b in Images)
            {
                ZCT coords = new ZCT(zBar.Value, cBar.Value, tBar.Value);
                Bitmap bitmap = null;

                int index = b.Coords[zBar.Value, cBar.Value, tBar.Value];
                if (Mode == ViewMode.Filtered)
                {
                    if (SelectedImage.isPyramidal)
                    {
                        ZCT c = GetCoordinate();
                        BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                        bitmap = (Bitmap)bf.ImageRGB;
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                    }
                    else
                        bitmap = b.GetFiltered(coords, b.RChannel.RangeR, b.GChannel.RangeG, b.BChannel.RangeB);
                }
                else if (Mode == ViewMode.RGBImage)
                {
                    if (SelectedImage.isPyramidal)
                    {
                        ZCT c = GetCoordinate();
                        BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                        bitmap = (Bitmap)bf.ImageRGB;
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                    }
                    else
                        bitmap = b.GetRGBBitmap(coords, b.RChannel.RangeR, b.GChannel.RangeG, b.BChannel.RangeB);
                }
                else if (Mode == ViewMode.Raw)
                {
                    if (SelectedImage.isPyramidal)
                    {
                        ZCT c = GetCoordinate();
                        BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                        bitmap = (Bitmap)bf.ImageRGB;
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                    }
                    else
                        bitmap = (Bitmap)b.Buffers[index].ImageRGB;
                }
                else
                {
                    if (SelectedImage.isPyramidal)
                    {
                        ZCT c = GetCoordinate();
                        BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                        bitmap = (Bitmap)bf.ImageRGB;
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                        SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                    }
                    else
                        bitmap = b.GetEmission(coords, b.RChannel.RangeR, b.GChannel.RangeG, b.BChannel.RangeB);
                }
                if(bitmap!=null)
                if (bitmap.PixelFormat == PixelFormat.Format16bppGrayScale || bitmap.PixelFormat == PixelFormat.Format48bppRgb)
                    bitmap = AForge.Imaging.Image.Convert16bppTo8bpp((Bitmap)bitmap);

                if (HardwareAcceleration && dx != null)
                {
                    if (dBitmaps[bi] != null)
                    {
                        dBitmaps[bi].Dispose();
                        dBitmaps[bi] = null;
                    }
                    dBitmaps[bi] = DBitmap.FromImage(dx.RenderTarget2D, bitmap);
                }
                else
                    Bitmaps.Add(bitmap);
                bi++;
            }
            update = true;
            UpdateView();
        }
        Bitmap bitmap;
        public void UpdateImage()
        {
            if (SelectedImage == null)
                return;
            if (Bitmaps.Count == 0)
                return;
            
            ZCT coords = new ZCT(zBar.Value, cBar.Value, tBar.Value);
            bitmap = null;
            GC.Collect();
            if (zBar.Maximum != SelectedImage.SizeZ - 1 || tBar.Maximum != SelectedImage.SizeT - 1)
            {
                zBar.Value = 0;
                cBar.Value = 0;
                tBar.Value = 0;
                InitGUI();
            }
            int index = SelectedImage.Coords[zBar.Value, cBar.Value, tBar.Value];
            if (Mode == ViewMode.Filtered)
            {
                if (SelectedImage.isPyramidal)
                {
                    ZCT c = GetCoordinate();
                    BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                    bitmap = (Bitmap)bf.ImageRGB;
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                }
                else
                    bitmap = SelectedImage.GetFiltered(coords, RChannel.RangeR, GChannel.RangeG, BChannel.RangeB);
            }
            else if (Mode == ViewMode.RGBImage)
            {
                if (SelectedImage.isPyramidal)
                {
                    ZCT c = GetCoordinate();
                    BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                    bitmap = (Bitmap)bf.ImageRGB;
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                }
                else
                    bitmap = SelectedImage.GetRGBBitmap(coords, RChannel.RangeR, GChannel.RangeG, BChannel.RangeB);
            }
            else if (Mode == ViewMode.Raw)
            {
                if (SelectedImage.isPyramidal)
                {
                    ZCT c = GetCoordinate();
                    BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                    bitmap = (Bitmap)bf.ImageRGB;
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                }
                else
                    bitmap = (Bitmap)SelectedImage.Buffers[index].ImageRGB;
            }
            else if(Mode == ViewMode.Emission)
            {
                if (SelectedImage.isPyramidal)
                {
                    ZCT c = GetCoordinate();
                    BufferInfo bf = BioImage.GetTile(SelectedImage, c, resolution, PyramidalOrigin.X, PyramidalOrigin.Y, pictureBox.Width, pictureBox.Height);
                    bitmap = (Bitmap)bf.ImageRGB;
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]].Dispose();
                    SelectedImage.Buffers[SelectedImage.Coords[c.Z, c.C, c.T]] = bf;
                }
                else
                    bitmap = SelectedImage.GetEmission(coords, RChannel.RangeR, GChannel.RangeG, BChannel.RangeB);
            }

            if (bitmap != null)
            if (bitmap.PixelFormat == PixelFormat.Format16bppGrayScale || bitmap.PixelFormat == PixelFormat.Format48bppRgb)
                bitmap = AForge.Imaging.Image.Convert16bppTo8bpp((Bitmap)bitmap);

            if (HardwareAcceleration)
            {
                if (dBitmaps[SelectedIndex] != null)
                {
                    dBitmaps[SelectedIndex].Dispose();
                    dBitmaps[SelectedIndex] = null;
                }
                dBitmaps[SelectedIndex] = DBitmap.FromImage(dx.RenderTarget2D, bitmap);
            }

           
            if (SelectedIndex < Bitmaps.Count)
                Bitmaps[SelectedIndex] = bitmap;
            else
                Bitmaps.Add(bitmap);

            update = false;
            UpdateView();
        }
        private void channelBoxR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelBoxR.SelectedIndex == -1)
                return;
            SelectedImage.rgbChannels[0] = channelBoxR.SelectedIndex;
            update = true;
            UpdateView();
        }
        private void channelBoxG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelBoxG.SelectedIndex == -1)
                return;
            SelectedImage.rgbChannels[1] = channelBoxG.SelectedIndex;
            update = true;
            UpdateView();
        }
        private void channelBoxB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelBoxB.SelectedIndex == -1)
                return;
            SelectedImage.rgbChannels[2] = channelBoxB.SelectedIndex;
            update = true;
            UpdateView();
        }
        private void showControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (trackBarPanel.Visible)
            {
                trackBarPanel.Hide();
                trackBarPanel.Height = 0;
                Application.DoEvents();
                pictureBox.Height += 75;
                overlayPictureBox.Height += 75;
                showControlsToolStripMenuItem.Text = "Show Controls";
                pictureBox.Dock = DockStyle.Fill;
            }
            else
            {
                trackBarPanel.Show();
                pictureBox.Height -= 75;
                Application.DoEvents();
                trackBarPanel.Height = 75;
                overlayPictureBox.Height += trackBarPanel.Height;
                showControlsToolStripMenuItem.Text = "Hide Controls";
                pictureBox.Dock = DockStyle.Fill;
            }

        }
        private void playZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playZToolStripMenuItem.Checked)
            {
                //We stop
                playZToolStripMenuItem.Checked = false;
                stopZToolStripMenuItem.Checked = true;
                zTimer.Stop();
            }
            else
            {
                //We start
                playZToolStripMenuItem.Checked = true;
                stopZToolStripMenuItem.Checked = false;
                zTimer.Start();
            }
        }
        private void stopZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stopZToolStripMenuItem.Checked)
            {
                //We start
                playZToolStripMenuItem.Checked = true;
                stopZToolStripMenuItem.Checked = false;
                zTimer.Start();

            }
            else
            {
                //We stop
                playZToolStripMenuItem.Checked = false;
                stopZToolStripMenuItem.Checked = true;
                zTimer.Stop();
            }
        }
        private void playTimeToolStripMenu_Click(object sender, EventArgs e)
        {
            if (playTimeToolStripMenu.Checked)
            {
                //We stop
                playTimeToolStripMenu.Checked = false;
                stopTimeToolStripMenu.Checked = true;
                timelineTimer.Stop();
            }
            else
            {
                //We start
                playTimeToolStripMenu.Checked = true;
                stopTimeToolStripMenu.Checked = false;
                timelineTimer.Start();
            }
        }
        private void stopTimeToolStripMenu_Click(object sender, EventArgs e)
        {
            playTimeToolStripMenu.Checked = false;
            stopTimeToolStripMenu.Checked = true;
            timelineTimer.Stop();
        }
        private void playCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playCToolStripMenuItem.Checked)
            {
                //We stop
                playCToolStripMenuItem.Checked = false;
                stopCToolStripMenuItem.Checked = true;
                cTimer.Stop();
            }
            else
            {
                //We start
                playCToolStripMenuItem.Checked = true;
                stopCToolStripMenuItem.Checked = false;
                cTimer.Start();
            }
        }
        private void stopCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopCToolStripMenuItem.Checked = false;
            playCToolStripMenuItem.Checked = true;
            cTimer.Stop();
        }
        private string mousePoint = "";
        private string mouseColor = "";
        private void playSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySpeed sp = null;
            if (Mode == ViewMode.RGBImage)
                sp = new PlaySpeed(timeEnabled, false, ZFps, TimeFps, CFps);
            else
                sp = new PlaySpeed(timeEnabled, true, ZFps, TimeFps, CFps);
            if (sp.ShowDialog() != DialogResult.OK)
                return;
            zTimer.Interval = sp.TimePlayspeed;
            cTimer.Interval = sp.CPlayspeed;
            timelineTimer.Interval = sp.TimePlayspeed;
        }
        private void playSpeedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PlaySpeed sp = null;
            if (Mode == ViewMode.RGBImage)
                sp = new PlaySpeed(timeEnabled, false, ZFps, TimeFps, CFps);
            else
                sp = new PlaySpeed(timeEnabled, true, ZFps, TimeFps, CFps);
            if (sp.ShowDialog() != DialogResult.OK)
                return;
            zTimer.Interval = sp.TimePlayspeed;
            cTimer.Interval = sp.CPlayspeed;
            timelineTimer.Interval = sp.TimePlayspeed;
        }
        private void CPlaySpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySpeed sp = null;
            if (Mode == ViewMode.RGBImage)
                sp = new PlaySpeed(timeEnabled, false, ZFps, TimeFps, CFps);
            else
                sp = new PlaySpeed(timeEnabled, true, ZFps, TimeFps, CFps);
            if (sp.ShowDialog() != DialogResult.OK)
                return;
            zTimer.Interval = sp.TimePlayspeed;
            cTimer.Interval = sp.CPlayspeed;
            timelineTimer.Interval = sp.TimePlayspeed;
        }
        public void GetRange()
        {
            RangeTool t;
            if (Mode == ViewMode.Filtered)
                t = new RangeTool(timeEnabled, true, zBar.Minimum, zBar.Maximum, tBar.Minimum, tBar.Maximum, cBar.Minimum, cBar.Maximum);
            else
                t = new RangeTool(timeEnabled, false, zBar.Minimum, zBar.Maximum, tBar.Minimum, tBar.Maximum, cBar.Minimum, cBar.Maximum);
            if (t.ShowDialog() != DialogResult.OK)
                return;
            zBar.Minimum = t.ZMin;
            zBar.Maximum = t.ZMax;
            tBar.Minimum = t.TimeMin;
            tBar.Maximum = t.TimeMax;
            cBar.Minimum = t.CMin;
            cBar.Maximum = t.CMax;
        }
        private void setValueRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRange();
        }
        private void setValueRangeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetRange();
        }
        private void setCValueRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRange();
        }
        private void ImageView_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            float dx = Scale.Width / 50;
            float dy = Scale.Height / 50;
            if (Ctrl)
            {
                float dsx = dSize.Width / 50;
                float dsy = dSize.Height / 50;
                if (e.Delta > 0)
                {
                    Scale = new SizeF(Scale.Width + dx, Scale.Height + dy);
                    dSize.Width += dsx;
                    dSize.Height += dsy;
                }
                else
                {
                    Scale = new SizeF(Scale.Width - dx, Scale.Height - dy);
                    dSize.Width -= dsx;
                    dSize.Height -= dsy;
                }
                UpdateView();
            }
            else
            if (e.Delta > 0)
            {
                if (zBar.Value + 1 <= zBar.Maximum)
                    zBar.Value += 1;
            }
            else
            {
                if (zBar.Value - 1 >= zBar.Minimum)
                    zBar.Value -= 1;
            }
            if(SelectedImage!=null)
            if (Ctrl && SelectedImage.isPyramidal)
                if (e.Delta > 0)
                {
                    if (resolution - 1 > 0)
                        Resolution--;
                }
                else
                {
                    if (resolution + 1 < SelectedImage.Resolutions.Count)
                        Resolution++;
                }
        }
        private void ZTrackBar_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (zBar.Value + 1 <= zBar.Maximum)
                    zBar.Value += 1;
            }
            else
            {
                if (zBar.Value - 1 >= zBar.Minimum)
                    zBar.Value -= 1;
            }

        }
        private void TimeTrackBar_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!timeEnabled)
                return;
            if (e.Delta > 0)
            {
                if (tBar.Value + 1 <= tBar.Maximum)
                    tBar.Value += 1;
            }
            else
            {
                if (tBar.Value - 1 >= tBar.Minimum)
                    tBar.Value -= 1;
            }
        }
        private void CTrackBar_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (cBar.Value + 1 <= cBar.Maximum)
                    cBar.Value += 1;
            }
            else
            {
                if (cBar.Value - 1 >= cBar.Minimum)
                    cBar.Value -= 1;
            }
        }

        private void cTimer_Tick(object sender, EventArgs e)
        {
            if (playCToolStripMenuItem.Checked)
            {
                if (cBar.Maximum >= cBar.Value + 1)
                    cBar.Value++;
                else
                {
                    if (loopC)
                        cBar.Value = cBar.Minimum;
                }
            }
        }
        private void zTimer_Tick(object sender, EventArgs e)
        {
            if (playZToolStripMenuItem.Checked)
            {
                if (zBar.Maximum >= zBar.Value + 1)
                    zBar.Value++;
                else
                {
                    if (loopZ)
                        zBar.Value = zBar.Minimum;
                }
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (playTimeToolStripMenu.Checked)
            {
                if (tBar.Maximum >= tBar.Value + 1)
                    tBar.Value++;
                else
                {
                    if (loopT)
                        tBar.Value = tBar.Minimum;
                }
            }
        }
        private void zBar_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            SelectedImage.Coordinate = new ZCT(zBar.Value, SelectedImage.Coordinate.C, SelectedImage.Coordinate.T);
            update = true;
            UpdateImage();
            UpdateView();
        }
        private void timeBar_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            SelectedImage.Coordinate = new ZCT(SelectedImage.Coordinate.Z, SelectedImage.Coordinate.C, tBar.Value);
            update = true;
            UpdateImage();
            UpdateView();
        }
        private void cBar_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            SelectedImage.Coordinate = new ZCT(SelectedImage.Coordinate.Z, cBar.Value, SelectedImage.Coordinate.T);
            update = true;
            UpdateImage();
            UpdateView();
        }

        private void loopTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loopT = loopTimeToolStripMenuItem.Checked;
        }
        private void loopZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loopZ = loopZToolStripMenuItem.Checked;
        }
        private void loopCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loopC = loopCToolStripMenuItem.Checked;
        }

        public bool showRROIs = true;
        public bool showGROIs = true;
        public bool showBROIs = true;

        private List<ROI> annotationsR = new List<ROI>();
        public List<ROI> AnnotationsR
        {
            get
            {
                return SelectedImage.GetAnnotations(SelectedImage.Coordinate.Z, SelectedImage.RChannel.Index, SelectedImage.Coordinate.T);
            }
        }
        private List<ROI> annotationsG = new List<ROI>();
        public List<ROI> AnnotationsG
        {
            get
            {
                return SelectedImage.GetAnnotations(SelectedImage.Coordinate.Z, SelectedImage.GChannel.Index, SelectedImage.Coordinate.T);
            }
        }
        private List<ROI> annotationsB = new List<ROI>();
        public List<ROI> AnnotationsB
        {
            get
            {
                return SelectedImage.GetAnnotations(SelectedImage.Coordinate.Z, SelectedImage.BChannel.Index, SelectedImage.Coordinate.T);
            }
        }
        public List<ROI> AnnotationsRGB
        {
            get
            {
                if (SelectedImage == null)
                    return null;
                List<ROI> ans = new List<ROI>();
                if (Mode == ViewMode.RGBImage)
                {
                    if (showRROIs)
                        ans.AddRange(AnnotationsR);
                    if (showGROIs)
                        ans.AddRange(AnnotationsG);
                    if (showBROIs)
                        ans.AddRange(AnnotationsB);
                }
                else
                {
                    ans.AddRange(SelectedImage.GetAnnotations(SelectedImage.Coordinate));
                }
                return ans;
            }
        }
        private void DrawOverlay(System.Drawing.Graphics g)
        {
            if (SelectedImage == null)
                return;
            if (!updateOverlay)
                return;
            SetCoordinate(zBar.Value, cBar.Value, tBar.Value);
            Pen pen = null;
            Pen red = null;
            Pen green = null;
            Pen mag = null;
            Pen blue = null;
            Brush b = null;
            bool bounds = showBounds;
            bool labels = showText;
            ZCT cor = GetCoordinate();
            foreach (BioImage bi in Images)
            {
                foreach (ROI an in bi.Annotations)
                {
                    if (Mode == ViewMode.RGBImage)
                    {
                        if (!showRROIs && an.coord.C == 0)
                            continue;
                        if (!showGROIs && an.coord.C == 1)
                            continue;
                        if (!showBROIs && an.coord.C == 2)
                            continue;
                    }
                    else if (zBar.Value != an.coord.Z || cBar.Value != an.coord.C || tBar.Value != an.coord.T)
                        continue;
                    float w = Math.Abs(Scale.Width);
                    pen = new Pen(an.strokeColor, (float)an.strokeWidth / w);
                    red = new Pen(Brushes.Red, (float)an.strokeWidth / w);
                    mag = new Pen(Brushes.Magenta, (float)an.strokeWidth / w);
                    green = new Pen(Brushes.Green, (float)an.strokeWidth / w);
                    blue = new Pen(Brushes.Blue, (float)an.strokeWidth / w);
                    Font fo = new Font(an.font.FontFamily, (float)(an.strokeWidth / w) * an.font.Size);
                    if (an.selected)
                    {
                        b = new SolidBrush(Color.Magenta);
                    }
                    else
                        b = new SolidBrush(an.strokeColor);
                    PointF pc = new PointF((float)(an.BoundingBox.X + (an.BoundingBox.W / 2)), (float)(an.BoundingBox.Y + (an.BoundingBox.H / 2)));
                    float width = (float)ToViewSizeW(ROI.selectBoxSize / w);
                    if (an.type == ROI.Type.Point)
                    {
                        g.DrawLine(pen, ToScreenSpace(an.Point.ToPointF()), ToScreenSpace(new PointF((float)an.Point.X + 1, (float)an.Point.Y + 1)));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(width)));
                    }
                    else
                    if (an.type == ROI.Type.Line)
                    {
                        g.DrawLine(pen, ToScreenSpace(an.GetPoint(0).ToPointF()), ToScreenSpace(an.GetPoint(1).ToPointF()));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(width)));
                    }
                    else
                    if (an.type == ROI.Type.Rectangle && an.Rect.W > 0 && an.Rect.H > 0)
                    {
                        RectangleF[] rects = new RectangleF[1];
                        rects[0] = an.Rect.ToRectangleF();
                        g.DrawRectangles(pen, ToScreenSpace(rects));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(width)));
                    }
                    else
                    if (an.type == ROI.Type.Ellipse)
                    {
                        g.DrawEllipse(pen, ToScreenSpace(an.Rect.ToRectangleF()));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(width)));
                    }
                    else
                    if (an.type == ROI.Type.Polygon && an.closed)
                    {
                        if (an.PointsD.Count > 1)
                            g.DrawPolygon(pen, ToScreenSpace(an.GetPointsF()));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(width)));
                    }
                    else
                    if (an.type == ROI.Type.Polygon && !an.closed)
                    {
                        PointF[] points = an.GetPointsF();
                        if (points.Length == 1)
                        {
                            g.DrawLine(pen, ToScreenSpace(an.Point.ToPointF()), ToScreenSpace(new PointF((float)an.Point.X + 1, (float)an.Point.Y + 1)));
                        }
                        else
                            g.DrawLines(pen, ToScreenSpace(points));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(width)));
                    }
                    else
                    if (an.type == ROI.Type.Polyline)
                    {
                        g.DrawLines(pen, ToScreenSpace(an.GetPointsF()));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(width)));
                    }

                    else
                    if (an.type == ROI.Type.Freeform && an.closed)
                    {
                        PointF[] points = an.GetPointsF();
                        if (points.Length > 1)
                            if (points.Length == 1)
                            {
                                g.DrawLine(pen, ToScreenSpace(an.Point.ToPointF()), ToScreenSpace(new PointF((float)an.Point.X + 1, (float)an.Point.Y + 1)));
                            }
                            else
                                g.DrawPolygon(pen, ToScreenSpace(an.GetPointsF()));
                    }
                    else
                    if (an.type == ROI.Type.Freeform && !an.closed)
                    {
                        PointF[] points = an.GetPointsF();
                        if (points.Length > 1)
                            if (points.Length == 1)
                            {
                                g.DrawLine(pen, ToScreenSpace(an.Point.ToPointF()), ToScreenSpace(new PointF((float)an.Point.X + 1, (float)an.Point.Y + 1)));
                            }
                            else
                                g.DrawLines(pen, ToScreenSpace(points));
                    }
                    if (an.type == ROI.Type.Label)
                    {
                        g.DrawString(an.Text, fo, b, ToScreenSpace(an.Point.ToPointF()));
                        g.DrawRectangles(red, ToScreenSpace(an.GetSelectBoxes(Scale.Width)));
                    }
                    if (labels)
                    {
                        //Lets draw the text of this ROI in the middle of the RO
                        float fw = ((float)an.Rect.X + ((float)an.Rect.W / 2)) - ((float)an.TextSize.Width / 2);
                        float fh = ((float)an.Rect.Y + ((float)an.Rect.H / 2)) - ((float)an.TextSize.Height / 2);

                        g.DrawString(an.Text, fo, b, ToScreenSpace(new PointF(fw, fh)));
                    }
                    if (bounds)
                    {
                        RectangleF[] rects = new RectangleF[1];
                        rects[0] = an.BoundingBox.ToRectangleF();
                        g.DrawRectangles(green, ToScreenSpace(rects));
                    }
                    if (an.selected)
                    {
                        //Lets draw the bounding box.
                        RectangleF[] bo = new RectangleF[1];
                        bo[0] = an.BoundingBox.ToRectangleF();
                        g.DrawRectangles(mag, ToScreenSpace(bo));
                        //Lets draw the selectBoxes.
                        List<RectangleF> rects = new List<RectangleF>();
                        RectangleF[] sels = an.GetSelectBoxes(width);
                        for (int i = 0; i < an.selectedPoints.Count; i++)
                        {
                            if (an.selectedPoints[i] < an.GetPointCount())
                            {
                                rects.Add(sels[an.selectedPoints[i]]);
                            }
                        }
                        if (rects.Count > 0)
                            g.DrawRectangles(blue, ToScreenSpace(rects.ToArray()));
                        rects.Clear();
                        //Lets draw the text of this ROI in the middle of the ROI
                        float fw = ((float)an.Rect.X + ((float)an.Rect.W / 2)) - ((float)an.TextSize.Width / 2);
                        float fh = ((float)an.Rect.Y + ((float)an.Rect.H / 2)) - ((float)an.TextSize.Height / 2);
                        g.DrawString(an.Text, fo, b, ToScreenSpace(new PointF(fw, fh)));
                    }
                    pen.Dispose();
                    red.Dispose();
                    mag.Dispose();
                    green.Dispose();
                    blue.Dispose();
                    b.Dispose();
                }
            }
        }
        private void overlayPictureBox_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            g.TranslateTransform(pictureBox.Width / 2, pictureBox.Height / 2);
            if (Scale.Width == 0)
                Scale = new SizeF(0.00001f, 0.00001f);
            g.ScaleTransform(Scale.Width, Scale.Height);
            DrawOverlay(g);
            TabsView.graphics = g;
            Point3D p = Microscope.GetPosition();

            if ((Tools.currentTool.type == Tools.Tool.Type.rectSel && down) || (Tools.currentTool.type == Tools.Tool.Type.magic && down))
            {
                Pen mag = new Pen(Brushes.Magenta, (float)1 / Scale.Width);
                RectangleF[] fs = new RectangleF[1];
                fs[0] = Tools.GetTool(Tools.Tool.Type.rectSel).RectangleF;
                g.DrawRectangles(mag, ToScreenSpace(fs));
                mag.Dispose();
            }
            else
                Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle = new RectangleD(0, 0, 0, 0);
        }

        SizeF dSize = new SizeF(1, 1);
        private void DrawView(System.Drawing.Graphics g)
        {
            if(HardwareAcceleration)
            {
                RenderFrame();
                return;
            }
            if (Bitmaps.Count == 0 || Bitmaps.Count != Images.Count)
                UpdateImages();
            g.TranslateTransform(pictureBox.Width / 2, pictureBox.Height / 2);
            if (Scale.Width == 0 || float.IsInfinity(Scale.Width))
                Scale = new SizeF(1, 1);
            g.ScaleTransform(Scale.Width, Scale.Height);
            g.FillRectangle(Brushes.LightGray, ToScreenRectF(PointD.MinX, PointD.MinY, PointD.MaxX - PointD.MinX, PointD.MaxY - PointD.MinY));
            RectangleF[] rfs = new RectangleF[1] { Microscope.GetViewRectangle().ToRectangleF() };
            rfs[0] = ToScreenRectF(rfs[0].X, rfs[0].Y, rfs[0].Width, rfs[0].Height);
            Pen red = new Pen(Brushes.Red, 1 / Scale.Width);
            g.DrawRectangles(red, rfs);
            if (Bitmaps.Count == 0)
                return;
            RectangleF[] rf = new RectangleF[1];
            Pen blue = new Pen(Brushes.Blue, 1 / Scale.Width);
            int i = 0;
            foreach (BioImage im in Images)
            {
                if (Bitmaps[i] == null)
                    UpdateImages();
                RectangleF r = ToScreenRectF(im.Volume.Location.X, im.Volume.Location.Y, im.Volume.Width, im.Volume.Height);
                double w = ToViewW(pictureBox.Width);
                double h = ToViewH(pictureBox.Height);
                RectangleF rg = new RectangleF((float)((-Origin.X) - (w / 2)), (float)((-Origin.Y) - (h / 2)), (float)(w), (float)(h));
                RectangleF rec = new RectangleF((float)im.Volume.Location.X, (float)im.Volume.Location.Y, (float)im.Volume.Width, (float)im.Volume.Height);
                if (rg.IntersectsWith(rec))
                    if (SelectedImage.isPyramidal)
                    {
                        g.ResetTransform();
                        g.DrawImage(Bitmaps[i], 0, 0, Bitmaps[i].Width, Bitmaps[i].Height);
                    }
                    else
                    {
                        g.DrawImage(Bitmaps[i], r.X, r.Y, r.Width, r.Height);
                    }
                if (i == SelectedIndex && !SelectedImage.isPyramidal)
                {
                    rf[0] = r;
                    g.DrawRectangles(blue, rf);
                }
                i++;
            }
            blue.Dispose();
            red.Dispose();
            update = false;
        }

        private int resolution;
        private double scaleorig = 0;
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            update = true;
            DrawView(e.Graphics);
        }
        public double GetScale()
        {
            return ToViewSizeW(ROI.selectBoxSize / Scale.Width);
        }

        Point mouseD = new Point(0, 0);
        private void rgbPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedImage == null)
                return;
            selectedImage = SelectedImage;
            PointD p = ToViewSpace(e.Location.X, e.Location.Y);
            PointF ip = SelectedImage.ToImageSpace(p);
            mousePoint = "(" + p.X + ", " + p.Y + ")";
            tools.ToolMove(p, mouseDownButtons);

            if (e.Button == MouseButtons.XButton1 && !x1State && !Ctrl && Mode != ViewMode.RGBImage)
            {
                if (cBar.Value < cBar.Maximum)
                    cBar.Value++;
                x1State = true;
            }
            else if (e.Button == MouseButtons.XButton1 && !x1State && Ctrl)
            {
                if (tBar.Value < tBar.Maximum)
                    tBar.Value++;
                x1State = true;
            }
            if (e.Button != MouseButtons.XButton1)
                x1State = false;

            if (e.Button == MouseButtons.XButton2 && !x2State && !Ctrl && Mode != ViewMode.RGBImage)
            {
                if (cBar.Value > cBar.Minimum)
                    cBar.Value--;
                x2State = true;
            }
            else if (e.Button == MouseButtons.XButton2 && !x2State && Ctrl)
            {
                if (tBar.Value > tBar.Minimum)
                    tBar.Value--;
                x2State = true;
            }
            if (e.Button != MouseButtons.XButton2)
                x2State = false;

            if (Tools.currentTool.type == Tools.Tool.Type.move && e.Button == MouseButtons.Left)
            {
                foreach (ROI an in selectedAnnotations)
                {
                    if (an.selectedPoints.Count > 0 && an.selectedPoints.Count < an.GetPointCount())
                    {
                        if (an.type == ROI.Type.Rectangle || an.type == ROI.Type.Ellipse)
                        {
                            RectangleD d = an.Rect;
                            if (an.selectedPoints[0] == 0)
                            {
                                double dw = d.X - p.X;
                                double dh = d.Y - p.Y;
                                d.X = p.X;
                                d.Y = p.Y;
                                d.W += dw;
                                d.H += dh;
                            }
                            else
                            if (an.selectedPoints[0] == 1)
                            {
                                double dw = p.X - (d.W + d.X);
                                double dh = d.Y - p.Y;
                                d.W += dw;
                                d.H += dh;
                                d.Y -= dh;
                            }
                            else
                            if (an.selectedPoints[0] == 2)
                            {
                                double dw = d.X - p.X;
                                double dh = p.Y - (d.Y + d.H);
                                d.W += dw;
                                d.H += dh;
                                d.X -= dw;
                            }
                            else
                            if (an.selectedPoints[0] == 3)
                            {
                                double dw = d.X - p.X;
                                double dh = d.Y - p.Y;
                                d.W = p.X - an.X;
                                d.H = p.Y - an.Y;
                            }
                            an.Rect = d;
                        }
                        else
                        {
                            PointD pod = new PointD(p.X - pd.X, p.Y - pd.Y);
                            for (int i = 0; i < an.selectedPoints.Count; i++)
                            {
                                PointD poid = an.GetPoint(an.selectedPoints[i]);
                                an.UpdatePoint(new PointD(poid.X + pod.X, poid.Y + pod.Y), an.selectedPoints[i]);
                            }
                        }
                    }
                    else
                    {
                        PointD pod = new PointD(p.X - pd.X, p.Y - pd.Y);
                        for (int i = 0; i < an.GetPointCount(); i++)
                        {
                            PointD poid = an.PointsD[i];
                            an.UpdatePoint(new PointD(poid.X + pod.X, poid.Y + pod.Y), i);
                        }
                    }
                }
                UpdateOverlay();
            }

            if (Tools.currentTool != null)
                if (Tools.currentTool.type == Tools.Tool.Type.pencil && e.Button == MouseButtons.Left)
                {
                    Tools.Tool tool = Tools.currentTool;
                    Graphics.Graphics g = Graphics.Graphics.FromImage(SelectedBuffer);
                    Graphics.Pen pen = new Graphics.Pen(Tools.DrawColor, (int)Tools.StrokeWidth, ImageView.SelectedImage.bitsPerPixel);
                    g.FillEllipse(new Rectangle((int)ip.X, (int)ip.Y, (int)Tools.StrokeWidth, (int)Tools.StrokeWidth), pen.color);
                    update = true;
                    UpdateImage();
                }

            UpdateStatus();
            
            pd = p;
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedImage == null)
                return;
            App.viewer = this;
            PointD p = ToViewSpace(e.Location.X, e.Location.Y);
            if (e.Button == MouseButtons.Middle)
            {
                PointD pd = new PointD(p.X - mouseDown.X, p.Y - mouseDown.Y);
                origin = new PointD(origin.X + pd.X, origin.Y + pd.Y);
                if (SelectedImage.isPyramidal)
                {
                    Point pf = new Point(e.X - mouseD.X, e.Y - mouseD.Y);
                    PyramidalOrigin = new Point(PyramidalOrigin.X - pf.X, PyramidalOrigin.Y - pf.Y);
                    UpdateImage();
                    UpdateView();
                }
            }
            mouseUpButtons = e.Button;
            mouseDownButtons = MouseButtons.None;
            mouseUp = p;
            down = false;
            up = true;
            tools.ToolUp(p, e.Button);

        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedImage == null)
                return;
            App.viewer = this;
            selectedImage = SelectedImage;
            mouseDownButtons = e.Button;
            mouseUpButtons = MouseButtons.None;
            PointD p = ToViewSpace(e.Location.X, e.Location.Y);
            PointF ip = SelectedImage.ToImageSpace(p);
            pd = new PointD(p.X, p.Y);
            mouseDown = pd;
            mouseD = e.Location;
            down = true;
            up = false;
            tools.BringToFront();
            int ind = 0;
            foreach (BioImage b in Images)
            {
                RectangleD r = new RectangleD(b.Volume.Location.X, b.Volume.Location.Y, b.Volume.Width, b.Volume.Height);
                if (r.IntersectsWith(p))
                {
                    SelectedIndex = ind;
                    break;
                }
                ind++;
            }
            if (!Ctrl && e.Button == MouseButtons.Left)
            {
                foreach (ROI item in selectedAnnotations)
                {
                    if (item.selected)
                        item.selectedPoints.Clear();
                }
                selectedAnnotations.Clear();
            }

            if (Tools.currentTool.type == Tools.Tool.Type.move && e.Button == MouseButtons.Left)
            {
                float width = (float)ToViewSizeW(ROI.selectBoxSize / Scale.Width);
                foreach (BioImage bi in Images)
                {
                    foreach (ROI an in bi.Annotations)
                    {
                        if (an.GetSelectBound(width).IntersectsWith(p.X, p.Y))
                        {
                            selectedAnnotations.Add(an);
                            an.selected = true;
                            RectangleF[] sels = an.GetSelectBoxes(width);
                            RectangleF r = new RectangleF((float)p.X, (float)p.Y, sels[0].Width, sels[0].Width);
                            for (int i = 0; i < sels.Length; i++)
                            {
                                if (sels[i].IntersectsWith(r))
                                {
                                    an.selectedPoints.Add(i);
                                }
                            }
                        }
                        else
                            if (!Ctrl)
                            an.selected = false;
                    }
                }
                UpdateOverlay();
            }

            if (e.Button == MouseButtons.Left)
            {
                Point s = new Point(SelectedImage.SizeX, SelectedImage.SizeY);
                if ((ip.X < s.X && ip.Y < s.Y) || (ip.X >= 0 && ip.Y >= 0))
                {
                    int zc = SelectedImage.Coordinate.Z;
                    int cc = SelectedImage.Coordinate.C;
                    int tc = SelectedImage.Coordinate.T;
                    if (SelectedImage.isPyramidal)
                    {
                        if (SelectedImage.isRGB)
                        {
                            int r = SelectedImage.GetValueRGB(zc, RChannel.Index, tc, e.X, e.Y, 0);
                            int g = SelectedImage.GetValueRGB(zc, GChannel.Index, tc, e.X, e.Y, 1);
                            int b = SelectedImage.GetValueRGB(zc, BChannel.Index, tc, e.X, e.Y, 2);
                            mouseColor = ", " + r + "," + g + "," + b;
                        }
                        else
                        {
                            int r = SelectedImage.GetValueRGB(zc, 0, tc, e.X, e.Y, 0);
                            mouseColor = ", " + r;
                        }
                    }
                    else
                    {
                        if (SelectedImage.isRGB)
                        {
                            int r = SelectedImage.GetValueRGB(zc, RChannel.Index, tc, (int)ip.X, (int)ip.Y, 0);
                            int g = SelectedImage.GetValueRGB(zc, GChannel.Index, tc, (int)ip.X, (int)ip.Y, 1);
                            int b = SelectedImage.GetValueRGB(zc, BChannel.Index, tc, (int)ip.X, (int)ip.Y, 2);
                            mouseColor = ", " + r + "," + g + "," + b;
                        }
                        else
                        {
                            int r = SelectedImage.GetValueRGB(zc, 0, tc, (int)ip.X, (int)ip.Y, 0);
                            mouseColor = ", " + r;
                        }
                    }
                }
            }
            UpdateStatus();
            tools.ToolDown(mouseDown, e.Button);
        }
        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedImage == null)
                return;
            App.viewer = this;
            selectedImage = SelectedImage;
            PointD p = ToViewSpace(e.Location.X, e.Location.Y);
            tools.ToolDown(p, e.Button);
            if (e.Button != MouseButtons.XButton1 && e.Button != MouseButtons.XButton2)
                Origin = new PointD(-p.X, -p.Y);
        }
        private void deleteROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            foreach (ROI item in AnnotationsRGB)
            {
                if (item.selected && (item.selectedPoints.Count == 0 || item.selectedPoints.Count == item.GetPointCount() || item.type == ROI.Type.Ellipse || item.type == ROI.Type.Rectangle))
                {
                    SelectedImage.Annotations.Remove(item);
                }
                else
                {
                    if ((item.type == ROI.Type.Polygon || item.type == ROI.Type.Freeform || item.type == ROI.Type.Polyline) && item.selectedPoints.Count > 0)
                    {
                        item.RemovePoints(item.selectedPoints.ToArray());

                    }
                    item.selectedPoints.Clear();
                }
            }
            UpdateOverlay();
        }
        private void setTextSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            foreach (ROI item in AnnotationsRGB)
            {
                if (item.selected)
                {
                    TextInput input = new TextInput(item.Text);
                    if (input.ShowDialog() != DialogResult.OK)
                        return;
                    item.Text = input.textInput;
                    item.font = input.font;
                    item.strokeColor = input.color;
                    UpdateOverlay();
                }
            }
        }

        private void copyViewToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                DrawView(g);
                DrawOverlay(g);
            }
            Clipboard.SetImage(bmp);
        }
        List<ROI> copys = new List<ROI>();

        public void CopySelection()
        {
            copys.Clear();
            string s = "";
            foreach (ROI item in AnnotationsRGB)
            {
                if (item.selected)
                {
                    copys.Add(item);
                    s += BioImage.ROIToString(item);
                }
            }
            Clipboard.SetText(s);
        }
        public void PasteSelection()
        {
            string[] sts = Clipboard.GetText().Split(BioImage.NewLine);
            foreach (string line in sts)
            {
                if (line.Length > 8)
                {
                    ROI an = BioImage.StringToROI(line);
                    //We set the coordinates of the ROI's we are pasting
                    an.coord = GetCoordinate();
                    SelectedImage.Annotations.Add(an);
                }
            }
            UpdateOverlay();
        }
        private void copyROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelection();
        }

        private void pasteROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        private void hideControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowControls)
                ShowControls = false;
            else
                ShowControls = true;
        }

        private void showControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowControls)
                ShowControls = false;
            else
                ShowControls = true;
        }

        private void HideStatusMenuItem_Click(object sender, EventArgs e)
        {
            ShowStatus = false;
        }
        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = ViewMode.RGBImage;
        }

        private void filteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = ViewMode.Filtered;
        }

        private void rawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = ViewMode.Raw;
        }

        public PointF ToViewSpace(Point p)
        {
            PointD d = ToViewSpace(p.X, p.Y);
            return new PointF((float)d.X, (float)d.Y);
        }
        public PointF ToViewSpace(PointF p)
        {
            PointD d = ToViewSpace(p.X, p.Y);
            return new PointF((float)d.X, (float)d.Y);
        }
        public PointD ToViewSpace(PointD p)
        {
            return ToViewSpace(p.X, p.Y); ;
        }
        public PointD ToViewSpace(double x, double y)
        {
            double dx = (ToViewSizeW(x - (pictureBox.Width / 2)) / Scale.Width) - Origin.X;
            double dy = (ToViewSizeH(y - (pictureBox.Height / 2)) / Scale.Height) - Origin.Y;
            return new PointD(dx, dy);
        }
        private double ToViewSizeW(double d)
        {
            double x = (double)(d / PxWmicron);
            return x;
        }
        public double ToViewSizeH(double d)
        {
            double y = (double)(d / PxHmicron);
            return y;
        }
        public double ToViewW(double d)
        {
            double x = (double)(d / PxWmicron) / scale.Width;
            return x;
        }
        public double ToViewH(double d)
        {
            double y = (double)(d / PxHmicron) / scale.Height;
            return y;
        }
        public PointD ToScreenSpace(double x, double y)
        {
            if (HardwareAcceleration)
            {
                RectangleF f = ToScreenRectF(x, y, 1, 1);
                RawRectangleF rf = ToRawRectF(f.X,f.Y,f.Width,f.Height);
                return new PointD(rf.Left, rf.Top);
            }
            
            double fx = ToScreenScaleW(Origin.X + x);
            double fy = ToScreenScaleH(Origin.Y + y);
            return new PointD(fx, fy);
        }
        public PointD ToScreenSpace(PointD p)
        {
            return ToScreenSpace(p.X, p.Y);
        }
        public PointF ToScreenSpace(PointF p)
        {
            PointD pd = ToScreenSpace(p.X, p.Y);
            return new PointF((float)pd.X, (float)pd.Y);
        }
        public PointF[] ToScreenSpace(PointF[] p)
        {
            PointF[] pf = new PointF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                pf[i] = ToScreenSpace(p[i]);
            }
            return pf;
        }
        public PointF ToScreenSpace(Point3D p)
        {
            PointD pd = ToScreenSpace(p.X, p.Y);
            return new PointF((float)pd.X, (float)pd.Y);
        }
        public float ToScreenScaleW(double x)
        {
            if(HardwareAcceleration)
            {
                return (float)(-x * PxWmicron * Scale.Width);
            }
            return (float)(x * PxWmicron);
        }
        public float ToScreenScaleH(double y)
        {
            if (HardwareAcceleration)
            {
                return (float)(-y * PxHmicron * Scale.Height);
            }
            return (float)(y * PxHmicron);
        }
        public PointF ToScreenScale(PointD p)
        {
            float x = ToScreenScaleW((float)p.X);
            float y = ToScreenScaleH((float)p.Y);
            return new PointF(x, y);
        }
        public RectangleF ToScreenRectF(double x, double y, double w, double h)
        {
            PointD pf;
            if (HardwareAcceleration)
            {
                double dx = (pxWmicron * (-Origin.X)) * Scale.Width;
                double dy = (pxHmicron * (-Origin.Y)) * Scale.Height;
                RectangleF rf = new RectangleF((float)(PxWmicron * -x * Scale.Width + dx), (float)(PxHmicron * -y * Scale.Height + dy), (float)(PxWmicron * -w * Scale.Width), (float)(PxHmicron * -h * Scale.Height));
                return rf;
            }
            pf = ToScreenSpace(x, y);
            return new RectangleF((float)pf.X, (float)pf.Y, ToScreenScaleW(w), ToScreenScaleH(h));
        }
        public RawRectangleF ToRawRectF(double x, double y, double w, double h)
        {
            double xd = dxPanel.Width / 2;
            double yd = dxPanel.Height / 2;
            return new RawRectangleF((float)(xd - x),(float)(yd - y), (float)(xd - (x + w)),(float)(yd - (y + h)));
        }
        public RectangleF ToScreenSpace(RectangleD p)
        {
            return ToScreenRectF(p.X, p.Y, p.W, p.H);
        }
        public RectangleF ToScreenSpace(RectangleF p)
        {
            return ToScreenRectF(p.X, p.Y, p.Width, p.Height);
        }
        public RectangleF[] ToScreenSpace(RectangleD[] p)
        {
            RectangleF[] rs = new RectangleF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                rs[i] = ToScreenSpace(p[i]);
            }
            return rs;
        }
        public RectangleF[] ToScreenSpace(RectangleF[] p)
        {
            RectangleF[] rs = new RectangleF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                rs[i] = ToScreenSpace(p[i]);
            }
            return rs;
        }
        public PointF[] ToScreenSpace(PointD[] p)
        {
            PointF[] rs = new PointF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                PointD pd = ToScreenSpace(p[i]);
                rs[i] = new PointF((float)pd.X, (float)pd.Y);
            }
            return rs;
        }

        private void ImageView_KeyDown(object sender, KeyEventArgs e)
        {
            double moveAmount = 5 * Scale.Width;
            if (e.KeyCode == Keys.C && e.Control)
            {
                CopySelection();
                return;
            }
            if (e.KeyCode == Keys.V && e.Control)
            {
                PasteSelection();
                return;
            }
            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.NumPad7)
            {
                Scale = new SizeF(Scale.Width - 0.1f, Scale.Height - 0.1f);
                UpdateOverlay();
            }
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.NumPad9)
            {
                Scale = new SizeF(Scale.Width + 0.1f, Scale.Height + 0.1f);
                UpdateOverlay();
            }
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.NumPad8)
            {
                Origin = new PointD(Origin.X, Origin.Y + moveAmount);
                UpdateView();
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.NumPad2)
            {
                Origin = new PointD(Origin.X, Origin.Y - moveAmount);
                UpdateView();
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.NumPad4)
            {
                Origin = new PointD(Origin.X + moveAmount, Origin.Y);
                UpdateView();
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.NumPad6)
            {
                Origin = new PointD(Origin.X - moveAmount, Origin.Y);
                UpdateView();
            }
        }

        public void GoToImage()
        {
            if (SelectedImage == null)
                return;
            double dx = SelectedImage.Volume.Width / 2;
            double dy = SelectedImage.Volume.Height / 2;
            Origin = new PointD(-(SelectedImage.Volume.Location.X + dx), -(SelectedImage.Volume.Location.Y + dy));
            double wx = pictureBox.Width / ToScreenScaleW(SelectedImage.Volume.Width);
            double wy = pictureBox.Height / ToScreenScaleH(SelectedImage.Volume.Height);
            Scale = new SizeF((float)wy,(float)wy);
            UpdateView();
        }
        public void GoToImage(int i)
        {
            double dx = Images[i].Volume.Width / 2;
            double dy = Images[i].Volume.Height / 2;
            Origin = new PointD(-(Images[i].Volume.Location.X + dx), -(Images[i].Volume.Location.Y + dy));
            double wx = pictureBox.Width / ToScreenScaleW(SelectedImage.Volume.Width);
            double wy = pictureBox.Height / ToScreenScaleH(SelectedImage.Volume.Height);
            Scale = new SizeF((float)wy, (float)wy);
            UpdateView();
        }
        private void goToImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToImage();
        }
        private void goToToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Origin = new PointD(mouseDown.X, mouseDown.Y);
        }

        private void goToImageToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.DropDownItems.Clear();
            foreach (BioImage im in Images)
            {
                ToolStripMenuItem it = new ToolStripMenuItem();
                it.Text = im.filename;
                item.DropDownItems.Add(it);
            }
        }

        private void goToImageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = 0;
            foreach (var item in Images)
            {
                if (item.filename == e.ClickedItem.Text)
                {
                    GoToImage(i);
                    return;
                }
                i++;
            }
        }
        public void GoToStage()
        {
            RectangleD d = Microscope.GetViewRectangle();
            double dx = d.W / 2;
            double dy = d.H / 2;
            Origin = new PointD(-(d.X + dx), -(d.Y + dy));
            double wx = pictureBox.Width / ToScreenScaleW(d.W);
            double wy = pictureBox.Height / ToScreenScaleH(d.H);
            scale.Width = (float)wy;
            scale.Height = (float)wy;
            update = true;
            UpdateView();
        }
        public void MoveStageToImage()
        {
            if (SelectedImage == null)
                return;
            Microscope.Stage.SetPosition(SelectedImage.Volume.Location.X, SelectedImage.Volume.Location.Y);
            update = true;
            UpdateView();
        }
        public void MoveStageToImage(int i)
        {
            update = true;
            Microscope.Stage.SetPosition(Images[i].Volume.Location.X, Images[i].Volume.Location.Y);
            UpdateView();
        }
        private void goToStageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = true;
            GoToStage();
        }
        private void moveStageToImageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = 0;
            foreach (var item in Images)
            {
                if (item.ID.Contains(e.ClickedItem.Text))
                {
                    MoveStageToImage(i);
                    return;
                }
                i++;
            }
        }

        private void moveStageToImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveStageToImage();
        }

        public new void Dispose()
        {
            for (int i = 0; i < Bitmaps.Count; i++)
            {
                if(Bitmaps[i]!=null)
                Bitmaps[i].Dispose();
            }
            foreach (BioImage item in Images)
            {
                Bio.Images.RemoveImage(item);
            }
        }

        private void goToToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Microscope.Stage.SetPosition(mouseDown.X, mouseDown.Y);
        }

        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bio.Graphics.Graphics g = Bio.Graphics.Graphics.FromImage(SelectedBuffer);
            foreach (ROI item in AnnotationsRGB)
            {
                Bio.Graphics.Pen p = new Graphics.Pen(Tools.DrawColor, (int)Tools.StrokeWidth, SelectedBuffer.BitsPerPixel);
                g.pen = p;
                if (item.selected)
                {
                    if (item.type == ROI.Type.Line)
                    {
                        g.DrawLine(SelectedImage.ToImageSpace(item.GetPoint(0)), SelectedImage.ToImageSpace(item.GetPoint(1)));
                    }
                    else
                    if (item.type == ROI.Type.Rectangle)
                    {
                        g.DrawRectangle(SelectedImage.ToImageSpace(item.Rect));
                    }
                    else
                    if (item.type == ROI.Type.Ellipse)
                    {
                        g.DrawEllipse(SelectedImage.ToImageSpace(item.Rect));
                    }
                    else
                    if (item.type == ROI.Type.Freeform || item.type == ROI.Type.Polygon || item.type == ROI.Type.Polyline)
                    {
                        if (item.closed)
                        {
                            for (int i = 0; i < item.GetPointCount() - 1; i++)
                            {
                                g.DrawLine(SelectedImage.ToImageSpace(item.GetPoint(i)), SelectedImage.ToImageSpace(item.GetPoint(i + 1)));
                            }
                            g.DrawLine(SelectedImage.ToImageSpace(item.GetPoint(0)), SelectedImage.ToImageSpace(item.GetPoint(item.GetPointCount() - 1)));
                        }
                        else
                        {
                            for (int i = 0; i < item.GetPointCount() - 1; i++)
                            {
                                g.DrawLine(SelectedImage.ToImageSpace(item.GetPoint(i)), SelectedImage.ToImageSpace(item.GetPoint(i + 1)));
                            }
                        }
                    }
                }
            }
            update = true;
            UpdateImage();
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bio.Graphics.Graphics g = Bio.Graphics.Graphics.FromImage(SelectedBuffer);
            foreach (ROI item in AnnotationsRGB)
            {
                Bio.Graphics.Pen p = new Graphics.Pen(Tools.DrawColor, (int)Tools.StrokeWidth, SelectedBuffer.BitsPerPixel);
                if (item.selected)
                {
                    if (item.type == ROI.Type.Line)
                    {
                        g.DrawLine(SelectedImage.ToImageSpace(item.GetPoint(0)), SelectedImage.ToImageSpace(item.GetPoint(1)));
                    }
                    else
                    if (item.type == ROI.Type.Rectangle)
                    {
                        g.FillRectangle(SelectedImage.ToImageSpace(item.Rect), p.color);
                    }
                    else
                    if (item.type == ROI.Type.Ellipse)
                    {
                        g.FillEllipse(SelectedImage.ToImageSpace(item.Rect), p.color);
                    }
                    else
                    if (item.type == ROI.Type.Freeform || item.type == ROI.Type.Polygon || item.type == ROI.Type.Polyline)
                    {
                        g.FillPolygon(SelectedImage.ToImageSpace(item.GetPointsF()), SelectedImage.ToImageSpace(item.Rect), p.color);
                    }
                }
            }
            update = true;
            UpdateImage();
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            PyramidalOrigin = new Point(hScrollBar.Value, vScrollBar.Value);
        }
        private void overlayPictureBox_Resize(object sender, EventArgs e)
        {
            if (SelectedImage.isPyramidal)
            {
                UpdateImage();
                UpdateView();
            }
        }

        Configuration conf = new Configuration();
        private void dxPanel_SizeChanged(object sender, EventArgs e)
        {
            conf.Width = dxPanel.Width;
            conf.Height = dxPanel.Height;
            dx.Update(conf, dxPanel.Handle);
            UpdateView();
        }

    }
}
