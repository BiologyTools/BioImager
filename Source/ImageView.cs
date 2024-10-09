using AForge;
using AForge.Imaging.Filters;
using OpenSlideGTK;
using SharpDX.Mathematics.Interop;
using System.ComponentModel;
using Bitmap = AForge.Bitmap;
using Point = AForge.Point;
using PointF = AForge.PointF;
using Rectangle = AForge.Rectangle;
using RectangleD = AForge.RectangleD;
using RectangleF = AForge.RectangleF;
using SizeF = AForge.SizeF;
using BioLib;
using CSScripting;
namespace BioImager
{
    /// <summary>
    /// ImageView control for image stacks, pyramidal and whole-slide-images.
    /// </summary>
    public partial class ImageView : UserControl, IDisposable
    {
        ///Initializing the image viewer. */
        public ImageView(BioImage im, bool OpenWithOpenSlide = true)
        {
            string file = im.ID.Replace("\\", "/");
            InitializeComponent();
            if (im.isPyramidal)
            {
                dxPanel.Size = new System.Drawing.Size(800, 600);
                if (im.openSlideImage != null && OpenWithOpenSlide)
                {
                    openSlide = true;
                    Initialize(im.file, im);
                }
                else
                    Initialize(im.file, im);
                hScrollBar.Maximum = im.Resolutions[Level].SizeX;
                vScrollBar.Maximum = im.Resolutions[Level].SizeY;
                hScrollBar.Visible = true;
                vScrollBar.Visible = true;
            }
            else
            {
                hScrollBar.Visible = false;
                vScrollBar.Visible = false;
                pictureBox.Width += 18;
                pictureBox.Height += 18;
                overlayPictureBox.Width += 18;
                overlayPictureBox.Height += 18;
                Tools.selectBoxSize = ROI.selectBoxSize;
            }
            App.viewer = this;
            if (im.SlideBase != null && im.isPyramidal && OpenSlide)
                openSlide = true;
            serie = im.series;

            Dock = DockStyle.Fill;
            AddImage(im);
            App.viewer = this;
            if (file == "" || file == null)
                return;
            SetCoordinate(0, 0, 0);
            InitGUI();
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
            overlayPictureBox.Location = new System.Drawing.Point(0, 0);
            resolutions = im.Resolutions;

            if (HardwareAcceleration)
            {
                dx = new Direct2D();
                dx.Initialize(new Configuration("BioImager", dxPanel.Width, dxPanel.Height), dxPanel.Handle);
            }
            update = true;
            InitPreview();
            GoToImage();
            UpdateImages(true);
            Mode = ViewMode.Filtered;
            UpdateView();

        }
        ///Initializing the ImageView class. */
        public ImageView()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            App.viewer = this;
            SetCoordinate(0, 0, 0);
            InitGUI();
            //Buf = image.GetBufByCoord(GetCoordinate());
            MouseWheel += new System.Windows.Forms.MouseEventHandler(ImageView_MouseWheel);
            zBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(ZTrackBar_MouseWheel);
            cBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(CTrackBar_MouseWheel);
            tBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(TimeTrackBar_MouseWheel);
            this.SizeChanged += ImageView_SizeChanged;
            //We set the trackbar event to handled so that it only scrolls one tick not the default multiple.
            zBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            tBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            cBar.MouseWheel += (sender, e) => ((HandledMouseEventArgs)e).Handled = true;
            TimeFps = 60;
            ZFps = 60;
            CFps = 1;
            // Change parent for overlay PictureBox.
            overlayPictureBox.Parent = pictureBox;
            overlayPictureBox.Location = new System.Drawing.Point(0, 0);

            update = true;
            UpdateImages(true);
            GoToImage();
            UpdateView();
            if (HardwareAcceleration)
            {
                dx = new Direct2D();
                dx.Initialize(new Configuration("BioImager", dxPanel.Width, dxPanel.Height), dxPanel.Handle);
            }
        }

        private void ImageView_SizeChanged(object? sender, EventArgs e)
        {
            if(SelectedImage!=null)
            SelectedImage.PyramidalSize = new AForge.Size(this.Width, this.Height);
        }

        ~ImageView()
        {

        }
        public struct Layer
        {
            public double Resolution
            {
                get; set;
            }
            public bool Enabled { get; set; }
            public Layer(double d)
            {
                Enabled = true;
                Resolution = d;
            }
            public Layer(double d, bool enabled)
            {
                Enabled = enabled;
                Resolution = d;
            }
        }
        List<Layer> layers = new List<Layer>();
        public List<Layer> Layers
        {
            get { return layers; }
            set { layers = value; }
        }
        private Layers layersTool = new Layers();
        private bool IsLayerEnabled(double d)
        {
            foreach (Layer item in layers)
            {
                if (d == item.Resolution && item.Enabled)
                    return true;
            }
            return false;
        }
        List<Bitmap> Bitmaps = new List<Bitmap>();
        List<Resolution> resolutions = new List<Resolution>();
        public static List<ROI> selectedAnnotations = new List<ROI>();
        private static int selectedIndex = 0;
        public Direct2D dx = null;
        public static BioImage SelectedImage
        {
            get
            {
                if (App.viewer != null)
                {
                    if (App.viewer.Images.Count > 0)
                        return App.viewer.Images[selectedIndex];
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        public static Bitmap SelectedBuffer
        {
            get
            {
                int ind = SelectedImage.Coords[SelectedImage.Coordinate.Z, SelectedImage.Coordinate.C, SelectedImage.Coordinate.T];
                return SelectedImage.Buffers[ind];
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
        List<SharpDX.Direct2D1.Bitmap> dBitmaps = new List<SharpDX.Direct2D1.Bitmap>();
        private int selIndex = 0;
        ///A property that is used to set the selected index of the image. */
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
                    selectedIndex = selIndex;
                    InitGUI();
                }
            }
        }
        public Tools tools { get { return App.tools; } }
        public string filepath = "";
        public int serie = 0;
        public int minSizeX = 50;
        public int minSizeY = 20;
        public bool loopZ = true;
        public bool loopT = true;
        public bool loopC = true;
        private AForge.Rectangle overview;
        private Bitmap overviewBitmap;
        public bool ShowStage { get; set; }
        SharpDX.Direct2D1.Bitmap db;
        private double pxWmicron = 0.004;
        private double pxHmicron = 0.004;
        AForge.SizeF scale = new AForge.SizeF(1, 1);
        /// > Sets the coordinate of the image to the specified Z, C, and T values
        /// 
        /// @param z the z-coordinate of the image
        /// @param c channel
        /// @param t time
        /// 
        /// @return The method is returning the value of the zBar.Value, cBar.Value, and tBar.Value.
        public void SetCoordinate(int z, int c, int t)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    App.viewer.Invoke((MethodInvoker)delegate
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
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            });
        }
        /// It returns the coordinate of the selected image
        /// 
        /// @return The Coordinate property of the SelectedImage object.
        public ZCT GetCoordinate()
        {
            return SelectedImage.Coordinate;
        }
        /// This function adds an image to the list of images, and then updates the GUI to reflect the
        /// new image
        /// 
        /// @param BioImage This is a class that contains the image data, and some other information
        /// about the image.
        public void AddImage(BioImage im)
        {
            Images.Add(im);
            SelectedIndex = Images.Count - 1;
            bool c = false;
            foreach (var item in layers)
            {
                if (item.Resolution == im.PhysicalSizeX)
                    c = true;
            }
            if (!c)
                Layers.Add(new Layer(im.PhysicalSizeX));
            if (im.isPyramidal)
            {
                hScrollBar.Maximum = im.Resolutions[Level].SizeX;
                vScrollBar.Maximum = im.Resolutions[Level].SizeY;
                hScrollBar.Visible = true;
                vScrollBar.Visible = true;
            }
            else
            {
                hScrollBar.Visible = false;
                vScrollBar.Visible = false;
                pictureBox.Width += 18;
                pictureBox.Height += 18;
                overlayPictureBox.Width += 18;
                overlayPictureBox.Height += 18;
                Tools.selectBoxSize = ROI.selectBoxSize;
            }
            InitGUI();
            UpdateImages(true);

            GoToImage(Images.Count - 1);
        }
        /// <summary>
        /// It takes a large image, resizes it to a small image, and then displays it.
        /// </summary>
        private void InitPreview()
        {
            if (!SelectedImage.isPyramidal)
                return;
            overview = new Rectangle(0, 0, 120, 120);
            if (SelectedImage.Resolutions.Count == 1)
            {
                ShowOverview = false;
                return;
            }
            if (MacroResolution.HasValue)
            {
                double aspx = (double)SelectedImage.Resolutions[MacroResolution.Value - 2].SizeX / (double)SelectedImage.Resolutions[MacroResolution.Value - 2].SizeY;
                double aspy = (double)SelectedImage.Resolutions[MacroResolution.Value - 2].SizeY / (double)SelectedImage.Resolutions[MacroResolution.Value - 2].SizeX;
                overview = new Rectangle(0, 0, (int)(aspx * 120), (int)(aspy * 120));
                Bitmap bm = BioImage.GetTile(SelectedImage, GetCoordinate(), MacroResolution.Value - 2, 0, 0, SelectedImage.Resolutions[MacroResolution.Value - 2].SizeX, SelectedImage.Resolutions[MacroResolution.Value - 2].SizeY);
                ResizeNearestNeighbor re = new ResizeNearestNeighbor(overview.Width, overview.Height);
                Bitmap bmp = re.Apply(bm);
                overviewBitmap = bmp;
            }
            else
            {
                int lev = SelectedImage.Resolutions.Count - 1;
                double aspx = (double)SelectedImage.Resolutions[lev].SizeX / (double)SelectedImage.Resolutions[lev].SizeY;
                double aspy = (double)SelectedImage.Resolutions[lev].SizeY / (double)SelectedImage.Resolutions[lev].SizeX;
                overview = new Rectangle(0, 0, (int)(aspx * 120), (int)(aspy * 120));
                Bitmap bm = BioImage.GetTile(SelectedImage, GetCoordinate(), lev, 0, 0, SelectedImage.Resolutions[lev].SizeX, SelectedImage.Resolutions[lev].SizeY);
                ResizeNearestNeighbor re = new ResizeNearestNeighbor(overview.Width, overview.Height);
                Bitmap bmp = re.Apply(bm);
                overviewBitmap = bmp;
            }
            ShowOverview = true;
            Console.WriteLine("Preview Initialized.");
        }
        public bool ShowOverview { get; set; }
        ///Declaring a variable called showControls and setting it to true. */
        private bool showControls = true;
        ///Setting the visibility of the trackBarPanel. */
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
        ///Declaring a variable called showStatus and setting it to true. */
        private bool showStatus = true;
        ///Setting the value of the property ShowStatus. */
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
        /// <summary>
        /// Only used when opening stacks.
        /// </summary>
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
        /// <summary>
        /// Only used when opening stacks.
        /// </summary>
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
        ///Setting the view mode of the application. */
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
                UpdateImages(false);
                App.tabsView.UpdateViewMode(viewMode);
                UpdateView();
                UpdateOverlay();
                if (viewMode == ViewMode.RGBImage)
                {
                    cBar.Value = 0;
                    rgbBoxsPanel.BringToFront();
                    cBar.SendToBack();
                    cLabel.SendToBack();
                    if (HardwareAcceleration)
                    {
                        dxPanel.Visible = HardwareAcceleration;
                        dxPanel.BringToFront();
                    }
                    else
                    {
                        pictureBox.BringToFront();
                        overlayPictureBox.BringToFront();
                    }
                }
                else
                if (viewMode == ViewMode.Filtered)
                {
                    rgbBoxsPanel.SendToBack();
                    cBar.BringToFront();
                    cLabel.BringToFront();
                    if (HardwareAcceleration)
                    {
                        dxPanel.Visible = HardwareAcceleration;
                        dxPanel.BringToFront();
                    }
                    else
                    {
                        pictureBox.BringToFront();
                        overlayPictureBox.BringToFront();
                    }
                }
                else
                if (viewMode == ViewMode.Raw)
                {
                    rgbBoxsPanel.SendToBack();
                    cBar.BringToFront();
                    cLabel.BringToFront();
                    if (HardwareAcceleration)
                    {
                        dxPanel.Visible = HardwareAcceleration;
                        dxPanel.BringToFront();
                    }
                    else
                    {
                        pictureBox.BringToFront();
                        overlayPictureBox.BringToFront();
                    }
                }
                else
                {
                    cBar.Value = 0;
                    rgbBoxsPanel.BringToFront();
                    cBar.SendToBack();
                    cLabel.SendToBack();
                    if (HardwareAcceleration)
                    {
                        dxPanel.Visible = HardwareAcceleration;
                        dxPanel.BringToFront();
                    }
                    else
                    {
                        pictureBox.BringToFront();
                        overlayPictureBox.BringToFront();
                    }
                }
            }
        }
        ///A property that returns the filepath. */
        public string Path
        {
            get
            {
                return filepath;
            }
        }
        ///A property that returns the R channel of the selected image. */
        public Channel RChannel
        {
            get
            {
                return SelectedImage.Channels[SelectedImage.rgbChannels[0]];
            }
        }
        ///A property that returns the G channel of the image. */
        public Channel GChannel
        {
            get
            {
                return SelectedImage.Channels[SelectedImage.rgbChannels[1]];
            }
        }
        ///Getting the B channel of the image. */
        public Channel BChannel
        {
            get
            {
                return SelectedImage.Channels[SelectedImage.rgbChannels[2]];
            }
        }
        PointD origin = new PointD(0, 0);
        bool hardwareAcceleration = true;
        bool openSlide = false;
        public bool OpenSlide
        {
            get { return openSlide; }
        }
        public int? MacroResolution { get { return SelectedImage.MacroResolution; } }
        public int? LabelResolution { get { return SelectedImage.LabelResolution; } }
        public int ViewWidth
        {
            get
            {
                if (HardwareAcceleration)
                    return dxPanel.Width;
                else
                    return pictureBox.Width;
            }
        }
        public int ViewHeight
        {
            get
            {
                if (HardwareAcceleration)
                    return dxPanel.Height;
                else
                    return pictureBox.Height;
            }
        }
        ///A property of the class PointD. */
        public PointD Origin
        {
            get { return origin; }
            set
            {
                origin = value;
                update = true;
            }
        }
        ///Setting the value of the pyramidalOrigin variable. */
        public PointD PyramidalOrigin
        {
            get
            {
                return SelectedImage.PyramidalOrigin;
            }
            set
            {
                double dsx = 0;
                if (SelectedImage.OpenSlideBase != null)
                    dsx = Resolution / SelectedImage.OpenSlideBase.Schema.Resolutions[Level].UnitsPerPixel;
                else
                    dsx = Resolution / SelectedImage.SlideBase.Schema.Resolutions[Level].UnitsPerPixel;
                int h = (int)(SelectedImage.Resolutions[Level].SizeY * dsx);
                int w = (int)(SelectedImage.Resolutions[Level].SizeX * dsx);
                if (value.X < 0 || value.Y < 0 || value.X > w || value.Y > h)
                    return;
                hScrollBar.Maximum = (int)(SelectedImage.Resolutions[Level].SizeX * dsx);
                vScrollBar.Maximum = (int)(SelectedImage.Resolutions[Level].SizeY * dsx);
                hScrollBar.Value = (int)value.X;
                vScrollBar.Value = (int)value.Y;
                SelectedImage.PyramidalOrigin = value;
                UpdateImages(true);
                UpdateView();
            }
        }
        ///A property of the class ImageViewer. It is a getter and setter for the resolution of the image.
        public double Resolution
        {
            get
            {
                if (SelectedImage == null)
                    return 1;
                return SelectedImage.Resolution;
            }
            set
            {
                double dp = Resolution / value;
                SelectedImage.PyramidalOrigin = new PointD((dp * PyramidalOrigin.X), (dp * PyramidalOrigin.Y));
                SelectedImage.Resolution = value;
                if (SelectedImage.Type == BioImage.ImageType.well)
                    SelectedImage.Level = (int)value;
                UpdateImage();
                UpdateView();
            }
        }
        private void UpdateScrollBars()
        {
            double dsx = SelectedImage.OpenSlideBase.Schema.Resolutions[Level].UnitsPerPixel / Resolution;
            hScrollBar.Maximum = (int)(SelectedImage.Resolutions[Level].SizeX * dsx);
            vScrollBar.Maximum = (int)(SelectedImage.Resolutions[Level].SizeY * dsx);
            if (PyramidalOrigin.X >= hScrollBar.Maximum)
                hScrollBar.Maximum = (int)PyramidalOrigin.X;
            if (PyramidalOrigin.Y >= vScrollBar.Maximum)
                vScrollBar.Maximum = (int)PyramidalOrigin.Y;
            try
            {
                hScrollBar.Value = (int)PyramidalOrigin.X;
                vScrollBar.Value = (int)PyramidalOrigin.Y;
                hScrollBar.Update();
                vScrollBar.Update();
            }
            catch (Exception)
            {

            }
            
        }
        ///Setting the Level of the image. */
        private int LevelFromResolution(double resolution)
        {
            int lev;
            if (MacroResolution.HasValue)
            {
                if (resolution >= MacroResolution.Value)
                {
                    int r = 0;
                    for (int i = 0; i < SelectedImage.Resolutions.Count; i++)
                    {
                        if (i <= resolution - 1)
                            r = i;
                    }
                    if (r - 1 <= MacroResolution.Value)
                        lev = MacroResolution.Value - 1;
                    else
                        lev = r - 1;
                }
                else
                    return (int)resolution;
            }
            else
            {
                int r = 0;
                for (int i = 0; i < SelectedImage.Resolutions.Count; i++)
                {
                    if (i <= resolution - 1)
                        r = i;
                }
                lev = r;
            }
            if (!OpenSlide)
            {
                return lev;
            }
            else
            {
                if (MacroResolution.HasValue)
                {
                    if (lev >= MacroResolution.Value - 1)
                        return lev - 1;
                    else
                        return lev;
                }
                else
                {
                    return lev - 1;
                }
            }
        }
        /// <summary>
        /// Current level when viewing whole-slide and pyramidal images.
        /// </summary>
        public int Level
        {
            get
            {
                int l = 0;
                if (!openSlide)
                    l = TileUtil.GetLevel(_slideBase.Schema.Resolutions, Resolution);
                else
                    l = OpenSlideGTK.TileUtil.GetLevel(_openSlideBase.Schema.Resolutions, Resolution);
                return l;
            }
        }
        /// <summary>
        /// Scale when opening image stacks.
        /// </summary>
        public new AForge.SizeF Scale
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
                if (SelectedImage != null)
                    if (SelectedImage.isPyramidal)
                    {
                        Tools.selectBoxSize = (float)(ROI.selectBoxSize * Resolution);
                    }
                    else
                        Tools.selectBoxSize = ROI.selectBoxSize;
            }
        }
        /// <summary>
        /// Whether or not Hardware Acceleration should be used for rendering.
        /// </summary>
        public bool HardwareAcceleration
        {
            get
            {
                if (Images.Count > 0)
                    if (Images[0].isPyramidal)
                        return false;
                return hardwareAcceleration;
            }
            set
            {
                hardwareAcceleration = value;
                if (value)
                {
                    GoToImage(0);
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
                    GoToImage(0);
                    rgbBoxsPanel.SendToBack();
                    cBar.BringToFront();
                    cLabel.BringToFront();
                    dxPanel.Visible = false;
                    pictureBox.BringToFront();
                    overlayPictureBox.BringToFront();
                }
            }
        }
        ///A property that returns a ContextMenuStrip object. */
        public ContextMenuStrip ViewContextMenu
        {
            get
            {
                return contextMenuStrip;
            }
        }
        /// "If the user has selected a channel, then set the channel to the selected index, otherwise
        /// set it to 0."
        /// 
        /// The above function is called when the user changes the channel selection
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
        ///Setting the interval of the timer to the value of the fps. */
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
        ///Setting the interval of the timer to the value of the timefps variable. */
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
        ///Setting the interval of the timer to the inverse of the fps. */
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
        /// It initializes the GUI
        /// 
        /// @return The image is being returned.
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
        /// This function is called from the Unity Editor when the user changes the size of the select
        /// box
        /// 
        /// @param size The size of the box that will be drawn around the ROI.
        public void UpdateSelectBoxSize(float size)
        {
            ROI.selectBoxSize = size;
        }
        /// If hardware acceleration is enabled, render the frame. Otherwise, set a flag to update the
        /// overlay and invalidate the overlay picture box
        /// 
        /// @return The return value is the value of the last expression evaluated in the method.
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
        /// It updates the status bar of the image viewer
        /// 
        /// @return The statusLabel.Text is being returned.
        public void UpdateStatus()
        {
            if (SelectedImage == null)
                return;
            string res = "";
            if (SelectedImage.Type == BioImage.ImageType.well)
                res = "Well:" + SelectedImage.Level;
            else if (SelectedImage.Type == BioImage.ImageType.pyramidal)
            {
                res = "Res.:" + Resolution;
            }
            if (Mode == ViewMode.RGBImage)
            {
                if (timeEnabled)
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (zBar.Maximum + 1) + ", " + (tBar.Value + 1) + "/" + (tBar.Maximum + 1) + ", " +
                        mousePoint + mouseColor + ", " + SelectedImage.Buffers[0].PixelFormat.ToString() + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") " + res;
                }
                else
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (cBar.Maximum + 1) + ", " + mousePoint + mouseColor + ", " + SelectedImage.Buffers[0].PixelFormat.ToString()
                        + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") " + res;
                }

            }
            else
            {
                if (timeEnabled)
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (zBar.Maximum + 1) + ", " + (cBar.Value + 1) + "/" + (cBar.Maximum + 1) + ", " + (tBar.Value + 1) + "/" + (tBar.Maximum + 1) + ", " +
                        mousePoint + mouseColor + ", " + SelectedImage.Buffers[0].PixelFormat.ToString() + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") " + res;
                }
                else
                {
                    statusLabel.Text = (zBar.Value + 1) + "/" + (zBar.Maximum + 1) + ", " + (cBar.Value + 1) + "/" + (cBar.Maximum + 1) + ", " + mousePoint + mouseColor + ", " +
                        SelectedImage.Buffers[0].PixelFormat.ToString() + ", (" + -Origin.X + ", " + -Origin.Y + "), (" + SelectedImage.Volume.Location.X + ", " + SelectedImage.Volume.Location.Y + ") " + res;
                }
            }
        }
        /// If hardware acceleration is enabled, render the frame. Otherwise, invalidate the picture
        /// box.
        /// 
        /// @return The method is returning void.
        public void UpdateView()
        {
            UpdateStatus();
            if (HardwareAcceleration)
            {
                RenderFrame();
                return;
            }
            pictureBox.Invalidate();
            overlayPictureBox.Invalidate();
        }
        /// It draws the images and annotations to the screen
        /// 
        /// @return A Bitmap
        public void RenderFrame()
        {
            drawing = true;
            if (HardwareAcceleration && dx != null)
            {
                Plugins.Render(this, dx);
                dx.BeginDraw();
                dx.RenderTarget2D.Clear(new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
                RectangleF rg = ToScreenRectF(PointD.MinX, PointD.MinY, PointD.MaxX - PointD.MinX, PointD.MaxY - PointD.MinY);
                //dx.RenderTarget2D.Transform = SharpDX.Matrix3x2.Rotation((float)Math.PI);
                SharpDX.Direct2D1.SolidColorBrush pen = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 0.0f, 0.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush red = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 0.0f, 0.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush green = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(0.0f, 1.0f, 0.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush mag = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 0.0f, 1.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush blue = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(0.0f, 0.0f, 1.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush b = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(1.0f, 1.0f, 0.0f, 1.0f));
                SharpDX.Direct2D1.SolidColorBrush gray = new SharpDX.Direct2D1.SolidColorBrush(dx.RenderTarget2D, new RawColor4(0.5f, 0.5f, 0.5f, 1.0f));

                dx.RenderTarget2D.FillRectangle(ToRawRectF(rg.X, rg.Y, rg.Width, rg.Height), gray);
                for (int x = 0; x < Images.Count; x++)
                {
                    if (!IsLayerEnabled(Images[x].PhysicalSizeX) && !Images[x].isPyramidal)
                        continue;
                    RectangleF r = ToScreenRectF(Images[x].Volume.Location.X, Images[x].Volume.Location.Y, Images[x].Volume.Width, Images[x].Volume.Height);
                    double w = ToViewW(ViewWidth);
                    double h = ToViewH(ViewHeight);
                    RectangleF rge = new RectangleF((float)((-Origin.X) - (w / 2)), (float)((-Origin.Y) - (h / 2)), (float)(Math.Abs(w)), (float)(Math.Abs(h)));
                    RectangleF rec = new RectangleF((float)Images[x].Volume.Location.X, (float)Images[x].Volume.Location.Y, (float)Images[x].Volume.Width, (float)Images[x].Volume.Height);
                    //if (rge.IntersectsWith(rec))
                    dx.RenderTarget2D.DrawBitmap(dBitmaps[x], ToRawRectF(r.X, r.Y, r.Width, r.Height), 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    if (x == selectedIndex)
                        dx.RenderTarget2D.DrawRectangle(ToRawRectF(r.X, r.Y, r.Width, r.Height), blue);
                }
                bool bounds = showBounds;
                bool labels = showText;
                foreach (BioImage bi in Images)
                {
                    if (!IsLayerEnabled(bi.PhysicalSizeX) && !bi.isPyramidal)
                        continue;
                    foreach (ROI an in bi.Annotations)
                    {
                        if (zBar.Value != an.coord.Z || cBar.Value != an.coord.C || tBar.Value != an.coord.T)
                            continue;
                        float w = Math.Abs(Scale.Width);
                        Font fo = new Font(new FontFamily(an.family), (float)((an.strokeWidth / w) * an.fontSize));
                        PointF pc = new PointF((float)(an.BoundingBox.X + (an.BoundingBox.W / 2)), (float)(an.BoundingBox.Y + (an.BoundingBox.H / 2)));
                        float width = 1;
                        dx.RenderTarget2D.StrokeWidth = width;
                        if (an.type == ROI.Type.Point)
                        {
                            PointF pf = ToScreenSpace(new PointF((float)an.Point.X + 1, (float)an.Point.Y + 1));
                            dx.RenderTarget2D.DrawLine(new RawVector2((float)an.Point.X, (float)an.Point.Y), new RawVector2(pf.X, pf.Y), b);
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        else
                        if (an.type == ROI.Type.Line)
                        {
                            PointD pf = ToScreenSpace(an.GetPoint(0));
                            PointD pf2 = ToScreenSpace(an.GetPoint(1));
                            dx.RenderTarget2D.DrawLine(new RawVector2((float)pf.X, (float)pf.Y), new RawVector2((float)pf2.X, (float)pf2.Y), b);
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize));
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
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        else
                        if (an.type == ROI.Type.Ellipse)
                        {
                            RectangleF r = ToScreenSpace(an.BoundingBox);
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize));
                            RawRectangleF rf = ToRawRectF(rfs[0].X, rfs[0].Y, rfs[0].Width, rfs[0].Height);
                            SharpDX.Direct2D1.Ellipse e = new SharpDX.Direct2D1.Ellipse(new RawVector2(rf.Left + (Math.Abs(r.Width) / 2) - (rfs[0].Width / 2), rf.Top + (Math.Abs(r.Height) / 2) - (rfs[0].Height / 2)), -r.Width / 2, -r.Height / 2);
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
                            for (int i = 0; i < an.PointsD.Count - 1; i++)
                            {
                                pf = new RawVector2((float)ToScreenSpace(an.GetPoint(i).X, an.GetPoint(i).Y).X, (float)ToScreenSpace(an.GetPoint(i).X, an.GetPoint(i).Y).Y);
                                pf2 = new RawVector2((float)ToScreenSpace(an.GetPoint(i + 1).X, an.GetPoint(i + 1).Y).X, (float)ToScreenSpace(an.GetPoint(i + 1).X, an.GetPoint(i + 1).Y).Y);
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                            }
                            pf = new RawVector2((float)ToScreenSpace(an.GetPoint(0)).X, (float)ToScreenSpace(an.GetPoint(0)).Y);
                            pf2 = new RawVector2((float)ToScreenSpace(an.GetPoint(an.PointsD.Count - 1)).X, (float)ToScreenSpace(an.GetPoint(an.PointsD.Count - 1)).Y);
                            dx.RenderTarget2D.DrawLine(pf, pf2, b);

                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize));
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
                                pf = new RawVector2((float)ToScreenSpace(an.GetPoint(i).X, an.GetPoint(i).Y).X, (float)ToScreenSpace(an.GetPoint(i).X, an.GetPoint(i).Y).Y);
                                pf2 = new RawVector2((float)ToScreenSpace(an.GetPoint(i + 1).X, an.GetPoint(i + 1).Y).X, (float)ToScreenSpace(an.GetPoint(i + 1).X, an.GetPoint(i + 1).Y).Y);
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                            }

                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize));
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
                                pf = new RawVector2((float)ToScreenSpace(an.GetPoint(i).X, an.GetPoint(i).Y).X, (float)ToScreenSpace(an.GetPoint(i).X, an.GetPoint(i).Y).Y);
                                pf2 = new RawVector2((float)ToScreenSpace(an.GetPoint(i + 1).X, an.GetPoint(i + 1).Y).X, (float)ToScreenSpace(an.GetPoint(i + 1).X, an.GetPoint(i + 1).Y).Y);
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                                dx.RenderTarget2D.DrawLine(pf, pf2, b);
                            }
                            pf = new RawVector2((float)ToScreenSpace(an.GetPoint(0)).X, (float)ToScreenSpace(an.GetPoint(0)).Y);
                            pf2 = new RawVector2((float)ToScreenSpace(an.GetPoint(an.PointsD.Count - 1)).X, (float)ToScreenSpace(an.GetPoint(an.PointsD.Count - 1)).Y);
                            dx.RenderTarget2D.DrawLine(pf, pf2, b);
                        }
                        if (an.type == ROI.Type.Label)
                        {
                            RectangleF rec = ToScreenSpace(an.Rect);
                            RawRectangleF r = ToRawRectF(rec.X, rec.Y, rec.Width, rec.Height);
                            SharpDX.DirectWrite.TextFormat tex = new SharpDX.DirectWrite.TextFormat(dx.FactoryDWrite, an.family.ToString(), an.fontSize);
                            dx.RenderTarget2D.DrawText(an.Text, tex, r, b);
                            tex.Dispose();
                            RectangleF[] rfs = ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize));
                            for (int i = 0; i < rfs.Length; i++)
                            {
                                dx.RenderTarget2D.DrawRectangle(ToRawRectF(rfs[i].X, rfs[i].Y, rfs[i].Width, rfs[i].Height), red);
                            }
                        }
                        if (labels)
                        {
                            if (an.Text != null)
                            {
                                System.Drawing.Size s = TextRenderer.MeasureText(an.Text, new Font(an.family, an.fontSize));
                                //Lets draw the text of this ROI in the middle of the ROI.
                                float fw = ((float)an.Rect.X + ((float)an.Rect.W / 2)) - ((float)s.Width / 2);
                                float fh = ((float)an.Rect.Y + ((float)an.Rect.H / 2)) - ((float)s.Height / 2);
                                RawRectangleF r = ToRawRectF(fw, fh, s.Width, s.Height);
                                SharpDX.DirectWrite.TextFormat tex = new SharpDX.DirectWrite.TextFormat(dx.FactoryDWrite, an.family.ToString(), an.fontSize);
                                dx.RenderTarget2D.DrawText(an.Text, tex, r, b);
                                tex.Dispose();
                            }
                        }
                        if (bounds)
                        {
                            RectangleF r = ToScreenSpace(an.Rect);
                            dx.RenderTarget2D.DrawRectangle(ToRawRectF(r.X, r.Y, r.Width, r.Height), green);
                        }
                        if (an.Selected)
                        {
                            //Lets draw the selected bounding box.
                            RectangleF r = ToScreenSpace(an.Rect);
                            dx.RenderTarget2D.DrawRectangle(ToRawRectF(r.X, r.Y, r.Width, r.Height), mag);

                            //Lets draw the selectBoxes.
                            List<RectangleD> rects = new List<RectangleD>();
                            RectangleD[] sels = an.GetSelectBoxes(Tools.selectBoxSize);
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
                                    dx.RenderTarget2D.DrawRectangle(ToRawRectF(ri.X, ri.Y, ri.Width, ri.Height), blue);
                                }
                            }
                            rects.Clear();
                        }

                    }
                }
                if (ShowStage)
                {
                    AForge.RectangleD re = Microscope.GetViewRectangle(false);
                    RectangleF vr = ToScreenRectF(re.X, re.Y, re.W, re.H);
                    RawRectangleF rr = ToRawRectF(vr.X, vr.Y, vr.Width, vr.Height);
                    dx.RenderTarget2D.DrawRectangle(rr, red);
                }

                pen.Dispose();
                red.Dispose();
                mag.Dispose();
                green.Dispose();
                blue.Dispose();
                gray.Dispose();
                b.Dispose();
                dx.EndDraw();
            }
            drawing = false;
        }
        /// > Update the status of the application and then render the frame
        /// 
        /// @param refresh boolean
        public void UpdateView(bool refresh)
        {
            UpdateStatus();
            update = refresh;
            if (HardwareAcceleration)
            {
                RenderFrame();
            }
            if (update)
            {
                pictureBox.Invalidate();
                overlayPictureBox.Invalidate();
            }
        }
        bool updating = false;
        /// It takes a list of images, and for each image, it gets the image at the current Z, C, T
        /// coordinates, and then converts it to a bitmap
        /// 
        /// @return A Bitmap
        public async void UpdateImages(bool updatePyramidal = false)
        {
            if (SelectedImage == null)
            {
                if (Images.Count > 0)
                    SelectedIndex = 0;
                else
                    return;
            }
            for (int i = 0; i < Bitmaps.Count; i++)
            {
                Bitmaps[i] = null;
            }
            Bitmaps.Clear();
            if (zBar.Maximum != SelectedImage.SizeZ - 1 || tBar.Maximum != SelectedImage.SizeT - 1)
            {
                InitGUI();
            }

            for (int i = 0; i < Images.Count; i++)
            {
                if (dBitmaps != null)
                {
                    if (dBitmaps.Count > i)
                        if (dBitmaps[i] != null)
                        {
                            dBitmaps[i].Dispose();
                            dBitmaps[i] = null;
                        }
                }
            }
            dBitmaps = new List<SharpDX.Direct2D1.Bitmap>();

            int bi = 0;
            foreach (BioImage b in Images)
            {
                b.PyramidalOrigin = PyramidalOrigin;
                b.PyramidalSize = new AForge.Size(dxPanel.Width, dxPanel.Height);
                if(updatePyramidal)
                await b.UpdateBuffersPyramidal();
                ZCT coords = new ZCT(zBar.Value, cBar.Value, tBar.Value);
                Bitmap bitmap = null;

                int index = b.Coords[zBar.Value, cBar.Value, tBar.Value];
                if (Mode == ViewMode.Filtered)
                {
                    bitmap = b.GetFiltered(coords, b.RChannel.RangeR, b.GChannel.RangeG, b.BChannel.RangeB);
                }
                else if (Mode == ViewMode.RGBImage)
                {
                    bitmap = b.GetRGBBitmap(coords, b.RChannel.RangeR, b.GChannel.RangeG, b.BChannel.RangeB);
                }
                else if (Mode == ViewMode.Raw)
                {
                    bitmap = (Bitmap)b.Buffers[index].ImageRGB;
                }
                else
                {
                    bitmap = b.GetEmission(coords, b.RChannel.RangeR, b.GChannel.RangeG, b.BChannel.RangeB);
                }
                if (bitmap != null)
                    if (bitmap.PixelFormat == PixelFormat.Format16bppGrayScale || bitmap.PixelFormat == PixelFormat.Format48bppRgb)
                        bitmap = AForge.Imaging.Image.Convert16bppTo8bpp((Bitmap)bitmap);

                if (HardwareAcceleration && dx != null)
                {
                    if (dBitmaps.Count > bi)
                        if (dBitmaps[bi] != null)
                        {
                            dBitmaps[bi].Dispose();
                            dBitmaps[bi] = null;
                        }
                    Bitmap bf = new Bitmap("", bitmap, new ZCT(), 0);
                    dBitmaps.Add(DBitmap.FromImage(dx.RenderTarget2D, (bf)));
                }
                else
                    Bitmaps.Add(bitmap);
                bi++;
            }
            update = true;
        }
        bool drawing = false;
        public void RemoveImage(int i)
        {
            do
            {
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
            } while (drawing || updating);
            Images[i].Dispose();
            Images.RemoveAt(i);
            UpdateImages(false);
        }
        public void RemoveImages()
        {
            do
            {
                Thread.Sleep(50);
                Application.DoEvents();
            } while (drawing || updating);
            for (int i = 0; i < Images.Count; i++)
            {
                Images[i].Dispose();
            }
            Images.Clear();
            UpdateImages(false);
        }
        Bitmap bitmap;
        /// It takes a 16-bit image, converts it to 8-bit, and then converts it to a DirectX texture
        /// 
        /// @return A bitmap
        public void UpdateImage()
        {
            UpdateImages(true);
        }
        /// When the user selects a new channel from the dropdown menu, the program updates the image to
        /// display the new channel
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        /// 
        /// @return The index of the selected item in the ComboBox.
        private void channelBoxR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelBoxR.SelectedIndex == -1)
                return;
            SelectedImage.rgbChannels[0] = channelBoxR.SelectedIndex;
            update = true;
            UpdateView();
        }
        /// When the user selects a new channel from the dropdown menu, the program updates the image to
        /// reflect the new channel
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        /// 
        /// @return The index of the selected item in the ComboBox.
        private void channelBoxG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelBoxG.SelectedIndex == -1)
                return;
            SelectedImage.rgbChannels[1] = channelBoxG.SelectedIndex;
            update = true;
            UpdateView();
        }
        /// When the user selects a channel from the dropdown menu, the program updates the image to
        /// display the selected channel
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The selected index of the channelBoxB ComboBox.
        private void channelBoxB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelBoxB.SelectedIndex == -1)
                return;
            SelectedImage.rgbChannels[2] = channelBoxB.SelectedIndex;
            update = true;
            UpdateView();
        }
        /// If the trackBarPanel is visible, hide it, set its height to 0, and set the pictureBox height
        /// to 75. 
        /// If the trackBarPanel is not visible, show it, set the pictureBox height to 75, and set the
        /// trackBarPanel height to 75
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
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
        /// If the playZToolStripMenuItem is checked, we stop the timer, otherwise we start the timer
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
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
        /// If the stopZToolStripMenuItem is checked, we start the zTimer, otherwise we stop it
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments that are passed to the event handler.
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
        /// If the playTimeToolStripMenu is checked, we stop the timer, otherwise we start the timer
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
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
        /// This function stops the timer and unchecks the playTimeToolStripMenu and checks the
        /// stopTimeToolStripMenu
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void stopTimeToolStripMenu_Click(object sender, EventArgs e)
        {
            playTimeToolStripMenu.Checked = false;
            stopTimeToolStripMenu.Checked = true;
            timelineTimer.Stop();
        }
        /// If the play button is checked, we stop the timer, otherwise we start the timer
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
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
        /// If the user clicks the stop button, then the stop button is unchecked and the play button is
        /// checked, and the timer is stopped
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void stopCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopCToolStripMenuItem.Checked = false;
            playCToolStripMenuItem.Checked = true;
            cTimer.Stop();
        }
        private string mousePoint = "";
        private string mouseColor = "";
        /// The function is called when the user clicks on the "Play Speed" menu item
        /// 
        /// @param sender System.Object
        /// @param EventArgs e
        /// 
        /// @return The return value is a DialogResult.
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
        /// The function is called when the user clicks on the "Play Speed" menu item in the "View" menu
        /// 
        /// @param sender System.Object
        /// @param EventArgs System.EventArgs
        /// 
        /// @return The DialogResult.OK is being returned.
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
        /// The function is called when the user clicks on the "Play Speed" menu item. It opens a dialog
        /// box that allows the user to change the speed at which the images are played
        /// 
        /// @param sender System.Object
        /// @param EventArgs e
        /// 
        /// @return The DialogResult.OK is being returned.
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
        /// It creates a new RangeTool object, which is a form that allows the user to set the range of
        /// the data to be displayed. 
        /// 
        /// The RangeTool object is created with the following parameters: 
        /// 
        /// - timeEnabled: a boolean that indicates whether the data has a time component. 
        /// - Mode: a ViewMode enum that indicates whether the data is filtered or not. 
        /// - zBar.Minimum: the minimum value of the zBar. 
        /// - zBar.Maximum: the maximum value of the zBar. 
        /// - tBar.Minimum: the minimum value of the tBar. 
        /// - tBar.Maximum: the maximum value of the tBar. 
        /// - cBar.Minimum: the minimum value of the cBar. 
        /// - cBar.Maximum: the maximum value of the cBar. 
        /// 
        /// The RangeTool object is then shown to the user. 
        /// 
        /// If the user clicks the
        /// 
        /// @return A RangeTool object.
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
        /// It's a function that handles the mouse wheel event for the image view
        /// 
        /// @param sender The object that raised the event.
        /// @param e the mouse event
        private void ImageView_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Plugins.ScrollEvent(sender, e);
            if (SelectedImage != null)
                if (SelectedImage.isPyramidal)
                {
                    if (Ctrl)
                        if (e.Delta > 0)
                        {
                            Resolution *= 0.95;
                        }
                        else
                        {
                            Resolution *= 1.05;
                        }
                }
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

        }
        /// If the mouse wheel is scrolled up, increase the value of the trackbar by 1, if the mouse
        /// wheel is scrolled down, decrease the value of the trackbar by 1
        /// 
        /// @param sender The object that raised the event.
        /// @param e The mouse event that was triggered
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
        /// If the mouse wheel is scrolled up, the time trackbar value is increased by 1, and if the
        /// mouse wheel is scrolled down, the time trackbar value is decreased by 1
        /// 
        /// @param sender The object that raised the event.
        /// @param e The mouse event that was triggered
        /// 
        /// @return The value of the time trackbar.
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
        /// If the mouse wheel is scrolled up, increase the value of the trackbar by 1, if the mouse
        /// wheel is scrolled down, decrease the value of the trackbar by 1
        /// 
        /// @param sender The object that raised the event.
        /// @param e The mouse event arguments.
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

        /// If the user has checked the "Play C" checkbox, and the C bar is not at its maximum value,
        /// then increment the C bar by 1. If the C bar is at its maximum value, and the user has
        /// checked the "Loop C" checkbox, then reset the C bar to its minimum value
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
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
        /// If the user has checked the "play" checkbox, then if the maximum value of the slider is
        /// greater than the current value of the slider, then increment the slider by one. Otherwise,
        /// if the user has checked the "loop" checkbox, then set the slider to its minimum value
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
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
        /// If the playTimeToolStripMenu is checked, then if the maximum value of the trackbar is
        /// greater than or equal to the current value of the trackbar plus 1, then the value of the
        /// trackbar is incremented by 1. Otherwise, if the loopT variable is true, then the value of
        /// the trackbar is set to the minimum value of the trackbar.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
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
        /// When the value of the zBar is changed, the z coordinate of the selected image is changed to
        /// the value of the zBar
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The value of the ZBar.Value is being returned.
        private void zBar_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            SelectedImage.Coordinate = new ZCT(zBar.Value, SelectedImage.Coordinate.C, SelectedImage.Coordinate.T);
            update = true;
            UpdateImage();
            UpdateView();
        }
        /// When the time bar is changed, the image's coordinate is changed to the new time value
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        /// 
        /// @return The image is being returned.
        private void timeBar_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            SelectedImage.Coordinate = new ZCT(SelectedImage.Coordinate.Z, SelectedImage.Coordinate.C, tBar.Value);
            update = true;
            UpdateImage();
            UpdateView();
        }
        /// When the value of the cBar is changed, the coordinate of the selected image is changed to the
        /// new value of the cBar
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        /// 
        /// @return The value of the slider.
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
        private System.Drawing.Point ToPoint(Point p)
        {
            return new System.Drawing.Point((int)p.X, (int)p.Y);
        }
        private System.Drawing.PointF ToPointF(PointD p)
        {
            return new System.Drawing.PointF((float)p.X, (float)p.Y);
        }
        /// This function draws the ROIs on the image
        /// 
        /// @param g Graphics object
        /// 
        /// @return A list of ROI objects.
        private void DrawOverlay(System.Drawing.Graphics g)
        {
            if (SelectedImage == null)
                return;
            if (!updateOverlay)
                return;
            if (HardwareAcceleration)
            {
                RenderFrame();
                return;
            }
            SetCoordinate(zBar.Value, cBar.Value, tBar.Value);
            System.Drawing.Pen pen = null;
            System.Drawing.Pen red = null;
            System.Drawing.Pen green = null;
            System.Drawing.Pen mag = null;
            System.Drawing.Pen blue = null;
            Brush b = null;
            bool bounds = showBounds;
            bool labels = showText;
            ZCT cor = GetCoordinate();
            foreach (BioImage bi in Images)
            {
                if (!IsLayerEnabled(bi.PhysicalSizeX))
                    continue;
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
                    pen = new System.Drawing.Pen(new SolidBrush(System.Drawing.Color.FromArgb(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B)), (float)an.strokeWidth / w);
                    red = new System.Drawing.Pen(Brushes.Red, (float)an.strokeWidth / w);
                    mag = new System.Drawing.Pen(Brushes.Magenta, (float)an.strokeWidth / w);
                    green = new System.Drawing.Pen(Brushes.Green, (float)an.strokeWidth / w);
                    blue = new System.Drawing.Pen(Brushes.Blue, (float)an.strokeWidth / w);
                    Font fo = new Font(an.family, (float)(an.strokeWidth / w) * an.fontSize);
                    if (an.Selected)
                    {
                        b = new SolidBrush(System.Drawing.Color.Magenta);
                    }
                    else
                        b = new SolidBrush(System.Drawing.Color.FromArgb(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B));
                    PointF pc = new PointF((float)(an.BoundingBox.X + (an.BoundingBox.W / 2)), (float)(an.BoundingBox.Y + (an.BoundingBox.H / 2)));
                    float width = (float)ToViewSizeW(ROI.selectBoxSize / w);
                    if (SelectedImage.isPyramidal)
                    {
                        width = (float)(ROI.selectBoxSize * Resolution);
                    }
                    if (an.type == ROI.Type.Point)
                    {
                        g.DrawLine(pen, ToPointF(ToScreenSpace(an.Point)), ToPointF(ToScreenSpace(an.Point.X + 1, an.Point.Y + 1)));
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }
                    else
                    if (an.type == ROI.Type.Line)
                    {
                        g.DrawLine(pen, ToPointF(ToScreenSpace(an.Point)), ToPointF(ToScreenSpace(an.GetPoint(1))));
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }
                    else
                    if (an.type == ROI.Type.Rectangle && an.Rect.W > 0 && an.Rect.H > 0)
                    {
                        g.DrawRectangle(pen, new System.Drawing.RectangleF((float)an.Rect.X, (float)an.Rect.Y, (float)an.W, (float)an.H));
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }
                    else
                    if (an.type == ROI.Type.Ellipse)
                    {
                        RectangleF rec = ToScreenSpace(an.Rect.ToRectangleF());
                        g.DrawEllipse(pen, rec.X, rec.Y, rec.Width, rec.Height);
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }
                    else
                    if (an.type == ROI.Type.Polygon && an.closed)
                    {
                        if (an.PointsD.Count > 1)
                        {
                            System.Drawing.Point[] pts = new System.Drawing.Point[an.PointsD.Count];
                            for (int i = 0; i < an.PointsD.Count; i++)
                            {
                                var p = ToScreenSpace(an.PointsD[i].X, an.PointsD[i].Y);
                                pts[i] = new System.Drawing.Point((int)p.X, (int)p.Y);
                            }
                            g.DrawLines(pen, pts);
                        }
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }
                    else
                    if (an.type == ROI.Type.Polygon && !an.closed)
                    {
                        if (an.PointsD.Count > 1)
                        {
                            System.Drawing.Point[] pts = new System.Drawing.Point[an.PointsD.Count - 1];
                            for (int i = 0; i < an.PointsD.Count - 1; i++)
                            {
                                var p = ToScreenSpace(an.PointsD[i].X, an.PointsD[i].Y);
                                pts[i] = new System.Drawing.Point((int)p.X, (int)p.Y);
                            }
                            g.DrawLines(pen, pts);
                        }
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }
                    else
                    if (an.type == ROI.Type.Polyline)
                    {
                        if (an.PointsD.Count > 1)
                        {
                            System.Drawing.Point[] pts = new System.Drawing.Point[an.PointsD.Count - 1];
                            for (int i = 0; i < an.PointsD.Count - 1; i++)
                            {
                                var p = ToScreenSpace(an.PointsD[i].X, an.PointsD[i].Y);
                                pts[i] = new System.Drawing.Point((int)p.X, (int)p.Y);
                            }
                            g.DrawLines(pen, pts);
                        }
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }

                    else
                    if (an.type == ROI.Type.Freeform && an.closed)
                    {
                        if (an.PointsD.Count > 1)
                        {
                            System.Drawing.Point[] pts = new System.Drawing.Point[an.PointsD.Count];
                            for (int i = 0; i < an.PointsD.Count; i++)
                            {
                                var p = ToScreenSpace(an.PointsD[i].X, an.PointsD[i].Y);
                                pts[i] = new System.Drawing.Point((int)p.X, (int)p.Y);
                            }
                            g.DrawLines(pen, pts);
                        }
                    }
                    else
                    if (an.type == ROI.Type.Freeform && !an.closed)
                    {
                        if (an.PointsD.Count > 1)
                        {
                            System.Drawing.Point[] pts = new System.Drawing.Point[an.PointsD.Count - 1];
                            for (int i = 0; i < an.PointsD.Count - 1; i++)
                            {
                                var p = ToScreenSpace(an.PointsD[i].X, an.PointsD[i].Y);
                                pts[i] = new System.Drawing.Point((int)p.X, (int)p.Y);
                            }
                            g.DrawLines(pen, pts);
                        }
                    }
                    if (an.type == ROI.Type.Label)
                    {
                        var v = ToScreenSpace(an.Point);
                        g.DrawString(an.Text, fo, b, (float)v.X, (float)v.Y);
                        foreach (var item in ToScreenSpace(an.GetSelectBoxes(Tools.selectBoxSize)))
                        {
                            g.DrawRectangle(red, new System.Drawing.RectangleF(item.X, item.Y, item.Width, item.Height));
                        }
                    }
                    if (labels)
                    {
                        System.Drawing.SizeF s = TextRenderer.MeasureText(an.Text, new Font(an.family, an.fontSize));
                        //Lets draw the text of this ROI in the middle of the RO
                        float fw = ((float)an.Rect.X + ((float)an.Rect.W / 2)) - ((float)s.Width / 2);
                        float fh = ((float)an.Rect.Y + ((float)an.Rect.H / 2)) - ((float)s.Height / 2);
                        var v = ToScreenSpace(new PointF(fw, fh));
                        g.DrawString(an.Text, fo, b, v.X, v.Y);
                    }
                    if (bounds)
                    {
                        var v = ToScreenSpace(an.BoundingBox);
                        g.DrawRectangles(green, new System.Drawing.RectangleF[1] { new System.Drawing.RectangleF(v.X, v.Y, v.Width, v.Height) });
                    }
                    if (an.Selected)
                    {
                        //Lets draw the bounding box.
                        var v = ToScreenSpace(an.BoundingBox);
                        g.DrawRectangles(mag, new System.Drawing.RectangleF[1] { new System.Drawing.RectangleF(v.X, v.Y, v.Width, v.Height) });
                        //Lets draw the selectBoxes.
                        List<RectangleD> rects = new List<RectangleD>();
                        RectangleD[] sels = an.GetSelectBoxes(Tools.selectBoxSize);
                        for (int i = 0; i < an.selectedPoints.Count; i++)
                        {
                            if (an.selectedPoints[i] < an.GetPointCount())
                            {
                                rects.Add(sels[an.selectedPoints[i]]);
                            }
                        }
                        for (int i = 0; i < rects.Count; i++)
                        {
                            var va = ToScreenSpace(rects.ToArray());
                            g.DrawRectangle(blue, new System.Drawing.RectangleF(va[0].X, va[0].Y, va[0].Width, va[0].Height));
                        }

                        rects.Clear();
                        //Lets draw the text of this ROI in the middle of the ROI
                        System.Drawing.Size s = TextRenderer.MeasureText(an.Text, new Font(an.family, an.fontSize));
                        float fw = ((float)an.Rect.X + ((float)an.Rect.W / 2)) - ((float)s.Width / 2);
                        float fh = ((float)an.Rect.Y + ((float)an.Rect.H / 2)) - ((float)s.Height / 2);
                        var vv = ToScreenSpace(new PointF(fw, fh));
                        g.DrawString(an.Text, fo, b, vv.X, vv.Y);
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
        /// It draws the overlay on the picturebox
        /// 
        /// @param sender The object that raised the event.
        /// @param PaintEventArgs e
        private void overlayPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (SelectedImage == null)
                return;
            System.Drawing.Graphics g = e.Graphics;
            if (!SelectedImage.isPyramidal)
            {
                g.TranslateTransform(ViewWidth / 2, ViewHeight / 2);
                if (Scale.Width == 0)
                    Scale = new SizeF(0.00001f, 0.00001f);
                g.ScaleTransform(Scale.Width, Scale.Height);
            }
            DrawOverlay(g);
            TabsView.graphics = g;
            if ((Tools.currentTool.type == Tools.Tool.Type.rectSel && down) || (Tools.currentTool.type == Tools.Tool.Type.magic && down))
            {
                System.Drawing.Pen mag = new System.Drawing.Pen(Brushes.Magenta, (float)1 / Scale.Width);
                RectangleF[] fs = new RectangleF[1];
                fs[0] = Tools.GetTool(Tools.Tool.Type.rectSel).RectangleF;
                var v = ToScreenSpace(fs);
                g.DrawRectangle(mag, new System.Drawing.RectangleF(v[0].X, v[0].Y, v[0].Width, v[0].Height));
                mag.Dispose();
            }
            else
                Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle = new RectangleD(0, 0, 0, 0);
        }


        AForge.SizeF dSize = new AForge.SizeF(1, 1);
        /// <summary>
        /// Draws the viewport when Hardware Acceleration is off.
        /// </summary>
        /// <param name="g"></param>
        public void DrawView(System.Drawing.Graphics g)
        {
            if (HardwareAcceleration)
            {
                RenderFrame();
                return;
            }
            drawing = true;
            g.TranslateTransform(ViewWidth / 2, ViewHeight / 2);
            if (Scale.Width == 0 || float.IsInfinity(Scale.Width))
                Scale = new SizeF(1, 1);
            g.ScaleTransform(Scale.Width, Scale.Height);
            if (Bitmaps.Count == 0)
                return;
            System.Drawing.RectangleF[] rf = new System.Drawing.RectangleF[1];
            System.Drawing.Pen blue = new System.Drawing.Pen(Brushes.Blue, 1 / Scale.Width);
            int i = 0;
            foreach (BioImage im in Images)
            {
                if (!IsLayerEnabled(im.PhysicalSizeX) && !im.isPyramidal)
                    continue;
                RectangleF r = ToScreenRectF(im.Volume.Location.X, im.Volume.Location.Y, im.Volume.Width, im.Volume.Height);
                double w = ToViewW(ViewWidth);
                double h = ToViewH(ViewHeight);
                RectangleF rg = new RectangleF((float)((-Origin.X) - (w / 2)), (float)((-Origin.Y) - (h / 2)), (float)(w), (float)(h));
                RectangleF rec = new RectangleF((float)im.Volume.Location.X, (float)im.Volume.Location.Y, (float)im.Volume.Width, (float)im.Volume.Height);
                if (SelectedImage.isPyramidal)
                {
                    var tr = g.Transform;
                    g.ResetTransform();
                    g.DrawImage(ToSysBitmap(Bitmaps[i]), 0, 0);
                    g.Transform = tr;
                }
                else
                if (rg.IntersectsWith(rec))
                {
                    g.DrawImage(ToSysBitmap(Bitmaps[i]), r.X, r.Y, r.Width, r.Height);
                }
                if (i == SelectedIndex && !SelectedImage.isPyramidal)
                {
                    rf[0] = new System.Drawing.RectangleF(r.X, r.Y, r.Width, r.Height);
                    g.DrawRectangles(blue, rf);
                }
                i++;
            }
            if (ShowOverview)
            {
                g.ResetTransform();
                g.DrawImage(ToSysBitmap(overviewBitmap), new System.Drawing.Point(0, 0));
                g.DrawRectangle(Pens.Gray, overview.X, overview.Y, overview.Width, overview.Height);
                if (!openSlide)
                {
                    double dsx = _slideBase.Schema.Resolutions[Level].UnitsPerPixel / Resolution;
                    Resolution rs = SelectedImage.Resolutions[Level];
                    double dx = ((double)PyramidalOrigin.X / (rs.SizeX * dsx)) * overview.Width;
                    double dy = ((double)PyramidalOrigin.Y / (rs.SizeY * dsx)) * overview.Height;
                    double dw = ((double)ViewWidth / (rs.SizeX * dsx)) * overview.Width;
                    double dh = ((double)ViewHeight / (rs.SizeY * dsx)) * overview.Height;
                    g.DrawRectangle(Pens.Red, (int)dx, (int)dy, (int)dw, (int)dh);
                }
                else
                {
                    double dsx = _openSlideBase.Schema.Resolutions[Level].UnitsPerPixel / Resolution;
                    var rs = _openSlideBase.Schema.Resolutions.Last();
                    Resolution rss = SelectedImage.Resolutions[Level];
                    double dx = ((double)PyramidalOrigin.X / (rss.SizeX * dsx)) * overview.Width;
                    double dy = ((double)PyramidalOrigin.Y / (rss.SizeY * dsx)) * overview.Height;
                    double dw = ((double)ViewWidth / (rss.SizeX * dsx)) * overview.Width;
                    double dh = ((double)ViewHeight / (rss.SizeY * dsx)) * overview.Height;
                    g.DrawRectangle(Pens.Red, (int)dx, (int)dy, (int)dw, (int)dh);
                }
            }
            if (ShowStage)
            {
                RectangleD d = Microscope.GetViewRectangle(false);
                RectangleF[] rfs = new RectangleF[1] { new RectangleF((float)d.X, (float)d.Y, (float)d.W, (float)d.H) };
                var v = ToScreenSpace(rfs);
                System.Drawing.Pen red = new System.Drawing.Pen(Brushes.Red, 1 / Scale.Width);
                g.DrawRectangle(red, v[0].X, v[0].Y, v[0].Width, v[0].Height);
            }
            blue.Dispose();
            update = false;
            drawing = false;
        }
        private double scaleorig = 0;
        /// It draws the view.
        /// 
        /// @param sender The object that raised the event.
        /// @param PaintEventArgs The PaintEventArgs object that contains the event data.
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            update = true;
            DrawView(e.Graphics);
            Plugins.Paint(sender, e);
        }
        /// GetScale() returns the scale of the image in the viewport.
        /// 
        /// @return The scale of the image.
        public double GetScale()
        {
            return ToViewSizeW(ROI.selectBoxSize / Scale.Width);
        }
        private System.Drawing.Bitmap ToSysBitmap(Bitmap bm)
        {
            return new System.Drawing.Bitmap(bm.Width, bm.Height, bm.Width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, bm.RGBData);
        }
        Point mouseD = new Point(0, 0);
        PointD prevMove = new PointD();
        /// The function is called when the mouse is moved over the image. It updates the mouse
        /// position, and if the mouse is clicked, it updates the image
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs e.Location.X, e.Location.Y
        /// 
        /// @return The point in the image space.
        private void rgbPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PointD p = ImageToViewSpace(e.Location.X, e.Location.Y);
            tools.ToolMove(p, e.Button);
            mouseMove = p;
            MouseMoveInt = new PointD(e.X, e.Y);
            if (SelectedImage != null)
            {
                PointD ip = SelectedImage.ToImageSpace(p);
                mousePoint = "(" + (p.X) + ", " + (p.Y) + ")";
            }
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
                    PointD ip = SelectedImage.ToImageSpace(p);
                    Tools.Tool tool = Tools.currentTool;
                    Graphics.Graphics g = Graphics.Graphics.FromImage(SelectedBuffer);
                    Graphics.Pen pen = new Graphics.Pen(Tools.DrawColor, (int)Tools.StrokeWidth, ImageView.SelectedImage.bitsPerPixel);
                    g.FillEllipse(new Rectangle((int)ip.X, (int)ip.Y, (int)Tools.StrokeWidth, (int)Tools.StrokeWidth), pen.color);
                    UpdateImage();
                }


            UpdateStatus();
            prevMove = p;
            pd = p;
        }
        /// The function is called when the mouse is released
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs e.Location.X, e.Location.Y
        /// 
        /// @return The mouse location in the picture box.
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PointD p = ImageToViewSpace(e.Location.X, e.Location.Y);
            mouseUpButtons = e.Button;
            mouseDownButtons = MouseButtons.None;
            mouseUp = p;
            down = false;
            up = true;
            if (SelectedImage == null)
                return;
            App.viewer = this;
            if (e.Button == MouseButtons.Middle)
            {
                PointD pd = new PointD(p.X - mouseDown.X, p.Y - mouseDown.Y);
                origin = new PointD(origin.X + pd.X, origin.Y + pd.Y);
                if (SelectedImage.isPyramidal)
                {
                    Point pf = new Point(e.X - mouseD.X, e.Y - mouseD.Y);
                    PyramidalOrigin = new PointD(PyramidalOrigin.X - pf.X, PyramidalOrigin.Y - pf.Y);
                }
            }
            tools.ToolUp(p, e.Button);
        }
        /// The function is called when the mouse is pressed down on the picturebox
        /// 
        /// @param sender the object that raised the event
        /// @param MouseEventArgs e
        /// 
        /// @return The mouseDownButtons is being returned.
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            App.viewer = this;
            MouseDownInt = new PointD(e.X, e.Y);
            mouseDownButtons = e.Button;
            mouseUpButtons = MouseButtons.None;
            PointD p = ImageToViewSpace(e.Location.X, e.Location.Y);
            pd = new PointD(p.X, p.Y);
            mouseDown = pd;
            mouseD = new Point(e.Location.X, e.Location.Y);
            down = true;
            up = false;
            PointD ip;
            if (SelectedImage == null)
                return;
            if (HardwareAcceleration)
                ip = SelectedImage.ToImageSpace(new PointD(SelectedImage.Volume.Width - p.X, SelectedImage.Volume.Height - p.Y));
            else
                ip = SelectedImage.ToImageSpace(p);
            tools.BringToFront();
            int ind = 0;
            if (ShowOverview && overview.IntersectsWith(new RectangleD(e.X, e.Y, 1, 1)))
            {
                if (!OpenSlide)
                {
                    double dsx = _slideBase.Schema.Resolutions[Level].UnitsPerPixel / Resolution;
                    Resolution rs = SelectedImage.Resolutions[(int)Level];
                    double dx = ((double)e.X / overview.Width) * (rs.SizeX * dsx);
                    double dy = ((double)e.Y / overview.Height) * (rs.SizeY * dsx);
                    double w = (double)ViewWidth / 2;
                    double h = (double)ViewHeight / 2;
                    PyramidalOrigin = new PointD(dx - w, dy - h);
                }
                else
                {
                    double dsx = _openSlideBase.Schema.Resolutions[Level].UnitsPerPixel / Resolution;
                    Resolution rs = SelectedImage.Resolutions[(int)Level];
                    double dx = ((double)e.X / overview.Width) * (rs.SizeX * dsx);
                    double dy = ((double)e.Y / overview.Height) * (rs.SizeY * dsx);
                    PyramidalOrigin = new PointD(dx, dy);
                }
                return;
            }
            foreach (BioImage b in Images)
            {
                RectangleD r = new RectangleD(b.Volume.Location.X, b.Volume.Location.Y, b.Volume.Width, b.Volume.Height);
                if (r.IntersectsWith(p))
                {
                    b.selected = true;
                    SelectedIndex = ind;
                }
                else
                    b.selected = false;
                ind++;
            }
            if (!Ctrl && e.Button == MouseButtons.Left)
            {
                foreach (ROI item in selectedAnnotations)
                {
                    if (item.Selected)
                        item.selectedPoints.Clear();
                }
                selectedAnnotations.Clear();
            }

            if (Tools.currentTool.type == Tools.Tool.Type.move && e.Button == MouseButtons.Left)
            {
                float width = (float)ToViewSizeW(ROI.selectBoxSize / Scale.Width);
                float height = (float)ToViewSizeH(ROI.selectBoxSize / Scale.Height);
                if (openSlide)
                {
                    width = Tools.selectBoxSize;
                    height = Tools.selectBoxSize;
                }
                foreach (BioImage bi in Images)
                {
                    foreach (ROI an in bi.Annotations)
                    {
                        if (an.GetSelectBound(width, height).IntersectsWith(p.X, p.Y))
                        {
                            selectedAnnotations.Add(an);
                            an.Selected = true;
                            RectangleD[] sels = an.GetSelectBoxes(Tools.selectBoxSize);
                            RectangleD r = new RectangleD(p.X, p.Y, sels[0].W, sels[0].H);
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
                            an.Selected = false;
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
                            int r = (int)SelectedImage.GetValueRGB(zc, RChannel.Index, tc, e.X, e.Y, 0);
                            int g = (int)SelectedImage.GetValueRGB(zc, GChannel.Index, tc, e.X, e.Y, 1);
                            int b = (int)SelectedImage.GetValueRGB(zc, BChannel.Index, tc, e.X, e.Y, 2);
                            mouseColor = ", " + r + "," + g + "," + b;
                        }
                        else
                        {
                            int r = (int)SelectedImage.GetValueRGB(zc, 0, tc, e.X, e.Y, 0);
                            mouseColor = ", " + r;
                        }
                    }
                    else
                    {
                        if (SelectedImage.isRGB)
                        {
                            int r = (int)SelectedImage.GetValueRGB(zc, RChannel.Index, tc, (int)ip.X, (int)ip.Y, 0);
                            int g = (int)SelectedImage.GetValueRGB(zc, GChannel.Index, tc, (int)ip.X, (int)ip.Y, 1);
                            int b = (int)SelectedImage.GetValueRGB(zc, BChannel.Index, tc, (int)ip.X, (int)ip.Y, 2);
                            mouseColor = ", " + r + "," + g + "," + b;
                        }
                        else
                        {
                            int r = (int)SelectedImage.GetValueRGB(zc, 0, tc, (int)ip.X, (int)ip.Y, 0);
                            mouseColor = ", " + r;
                        }
                    }
                }
            }
            UpdateStatus();
            tools.ToolDown(mouseDown, e.Button);
        }
        /// > If the user double clicks on the picture box, then the selected image is set to the image
        /// that the user double clicked on
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs e.Location.X, e.Location.Y
        /// 
        /// @return The return value is a PointD object.
        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedImage == null)
                return;
            App.viewer = this;
            PointD p = ToViewSpace(e.Location.X, e.Location.Y);
            tools.ToolDown(p, e.Button);
            if (e.Button != MouseButtons.XButton1 && e.Button != MouseButtons.XButton2)
                Origin = new PointD(-p.X, -p.Y);
        }
        /// If the user has selected a ROI, then delete it
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The selected ROI is being returned.
        private void deleteROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            foreach (ROI item in AnnotationsRGB)
            {
                if (item.Selected && (item.selectedPoints.Count == 0 || item.selectedPoints.Count == item.GetPointCount() || item.type == ROI.Type.Ellipse || item.type == ROI.Type.Rectangle))
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
        /// This function is used to set the text of the selected ROI
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The DialogResult.OK is being returned.
        private void setTextSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null)
                return;
            foreach (ROI item in AnnotationsRGB)
            {
                if (item.Selected)
                {
                    TextInput input = new TextInput(item.Text);
                    if (input.ShowDialog() != DialogResult.OK)
                        return;
                    item.Text = input.TextValue;
                    item.family = input.font.FontFamily.Name;
                    item.fontSize = input.font.Size;
                    item.strokeColor = AForge.Color.FromArgb(input.color.R, input.color.G, input.color.B);
                    UpdateOverlay();
                }
            }
        }

        /// It creates a bitmap, draws the view and overlay to it, and then copies it to the clipboard
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void copyViewToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ViewWidth, ViewHeight);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                DrawView(g);
                DrawOverlay(g);
            }
            Clipboard.SetImage(bmp);
        }
        List<ROI> copys = new List<ROI>();

        /// It takes the selected ROIs and copies them to the clipboard
        public void CopySelection()
        {
            copys.Clear();
            string s = "";
            foreach (ROI item in AnnotationsRGB)
            {
                if (item.Selected)
                {
                    copys.Add(item);
                    s += BioImage.ROIToString(item);
                }
            }
            Clipboard.SetText(s);
        }
        /// We get the text from the clipboard, split it into lines, and then for each line, if it's
        /// longer than 8 characters, we convert it to an ROI and add it to the image
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
        /// CopySelection() copies the selected region of the image to the clipboard
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void copyROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelection();
        }

        /// PasteSelection() is a function that takes the current selection and pastes it into the
        /// current image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event data.
        private void pasteROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        /// If the ShowControls variable is true, set it to false. Otherwise, set it to true
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void hideControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowControls)
                ShowControls = false;
            else
                ShowControls = true;
        }

        /// If the ShowControls variable is true, set it to false. Otherwise, set it to true
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void showControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowControls)
                ShowControls = false;
            else
                ShowControls = true;
        }

        /// It sets the value of the ShowStatus variable to false
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void HideStatusMenuItem_Click(object sender, EventArgs e)
        {
            ShowStatus = false;
        }
        /// When the user clicks on the RGB menu item, set the mode to RGB.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = ViewMode.RGBImage;
        }

        /// The function is called when the user clicks on the "Filtered" menu item
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void filteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = ViewMode.Filtered;
        }

        /// If the user clicks on the "Raw" menu item, then set the Mode property to ViewMode.Raw
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void rawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = ViewMode.Raw;
        }
        /// It takes a point in the image space and returns the point in the view space
        /// 
        /// @param x the x coordinate of the point in the image
        /// @param y the y coordinate of the point in the image
        /// 
        /// @return The point in the image space that corresponds to the point in the view space.
        public PointD ImageToViewSpace(double x, double y)
        {
            if (SelectedImage == null)
                return ToViewSpace(x, y);
            if (SelectedImage.isPyramidal)
            {
                return new PointD((PyramidalOrigin.X + x) * Resolution, (PyramidalOrigin.Y + y) * Resolution);
            }
            else
                return ToViewSpace(x, y);
        }
        /// Convert a point from world space to view space
        /// 
        /// @param PointF The point to convert
        /// 
        /// @return A PointD object.
        public PointF ToViewSpace(PointF p)
        {
            PointD d = ToViewSpace(p.X, p.Y);
            return new PointF((float)d.X, (float)d.Y);
        }
        /// > Converts a point from world space to view space
        /// 
        /// @param PointD A class that contains an X and Y value.
        /// 
        /// @return A PointD object.
        public PointD ToViewSpace(PointD p)
        {
            return ToViewSpace(p.X, p.Y); ;
        }
        /// > ToViewSpace(x, y) = (ToViewSizeW(x - (ViewWidth / 2)) / Scale.Width) - Origin.X;
        /// 
        /// @param x The x coordinate of the point to convert
        /// @param y The y coordinate of the point to convert.
        /// 
        /// @return A PointD object.
        public PointD ToViewSpace(double x, double y)
        {
            if (SelectedImage != null)
                if (SelectedImage.isPyramidal)
                {
                    double ddx = x / Resolution;
                    double ddy = y / Resolution;
                    return new PointD(ddx, ddy);
                }
            double dx, dy;
            if (HardwareAcceleration)
            {
                dx = (ToViewSizeW(x - (dxPanel.Width / 2)) / Scale.Width) - Origin.X;
                dy = (ToViewSizeH(y - (dxPanel.Height / 2)) / Scale.Height) - Origin.Y;
            }
            else
            {
                dx = (ToViewSizeW(x - (ViewWidth / 2)) / Scale.Width) - Origin.X;
                dy = (ToViewSizeH(y - (ViewHeight / 2)) / Scale.Height) - Origin.Y;
            }
            return new PointD(dx, dy);
        }
        /// Convert a value in microns to a value in pixels
        /// 
        /// @param d the size in microns
        /// 
        /// @return The return value is the size of the object in pixels.
        private double ToViewSizeW(double d)
        {
            if (SelectedImage != null)
                if (SelectedImage.isPyramidal)
                {
                    return d / Resolution;
                }
            double x = (double)(d / PxWmicron);
            return x;
        }
        /// > Convert a value in microns to a value in pixels
        /// 
        /// @param d the size in microns
        /// 
        /// @return The return value is the size of the object in pixels.
        public double ToViewSizeH(double d)
        {
            if (SelectedImage != null)
                if (SelectedImage.isPyramidal)
                {
                    return d / Resolution;
                }
            double y = (double)(d / PxHmicron);
            return y;
        }
        /// Convert a distance in microns to a distance in pixels on the screen
        /// 
        /// @param d the distance in microns
        /// 
        /// @return The width of the image in pixels.
        public double ToViewW(double d)
        {
            double x = (double)(d / PxWmicron) / scale.Width;
            return x;
        }
        /// > Convert a distance in microns to a distance in pixels
        /// 
        /// @param d the distance in microns
        /// 
        /// @return The return value is the y-coordinate of the point in the view.
        public double ToViewH(double d)
        {
            double y = (double)(d / PxHmicron) / scale.Height;
            return y;
        }
        /// > It converts a point in world space to a point in screen space
        /// 
        /// @param x The x coordinate of the point to convert.
        /// @param y The y coordinate of the point to transform.
        /// 
        /// @return A PointD object.
        public PointD ToScreenSpace(double x, double y)
        {
            if (HardwareAcceleration)
            {
                RectangleF f = ToScreenRectF(x, y, 1, 1);
                RawRectangleF rf = ToRawRectF(f.X, f.Y, f.Width, f.Height);
                return new PointD(rf.Left, rf.Top);
            }

            double fx = ToScreenScaleW(Origin.X + x);
            double fy = ToScreenScaleH(Origin.Y + y);
            return new PointD(fx, fy);
        }
        /// > Converts a point from world space to screen space
        /// 
        /// @param PointD A class that contains an X and Y value.
        /// 
        /// @return A PointD object.
        public PointD ToScreenSpace(PointD p)
        {
            return ToScreenSpace(p.X, p.Y);
        }
        /// Convert a point in the world coordinate system to the screen coordinate system
        /// 
        /// @param PointF The point you want to convert to screen space.
        /// 
        /// @return A PointD object.
        public PointF ToScreenSpace(PointF p)
        {
            PointD pd = ToScreenSpace(p.X, p.Y);
            return new PointF((float)pd.X, (float)pd.Y);
        }
        /// > It takes an array of points and returns an array of points
        /// 
        /// @param p The point to convert
        /// 
        /// @return A PointF array.
        public PointF[] ToScreenSpace(PointF[] p)
        {
            PointF[] pf = new PointF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                pf[i] = ToScreenSpace(p[i]);
            }
            return pf;
        }
        /// > It converts a 3D point to a 2D point
        /// 
        /// @param Point3D 
        /// 
        /// @return A PointF object.
        public PointF ToScreenSpace(Point3D p)
        {
            PointD pd = ToScreenSpace(p.X, p.Y);
            return new PointF((float)pd.X, (float)pd.Y);
        }
        /// ToScreenScaleW() returns the number of pixels that correspond to the given number of microns
        /// 
        /// @param x the x coordinate of the point to be converted
        /// 
        /// @return The return value is a float.
        public float ToScreenScaleW(double x)
        {
            if (HardwareAcceleration)
            {
                return (float)(-x * PxWmicron * Scale.Width);
            }
            return (float)(x * PxWmicron);
        }
        /// > Convert a value in microns to a value in pixels
        /// 
        /// @param y the y coordinate of the point to be converted
        /// 
        /// @return The return value is a float.
        public float ToScreenScaleH(double y)
        {
            if (HardwareAcceleration)
            {
                return (float)(-y * PxHmicron * Scale.Height);
            }
            return (float)(y * PxHmicron);
        }
        /// > Convert a point in the world coordinate system to a point in the screen coordinate system
        /// 
        /// @param PointD 
        /// 
        /// @return A PointF object.
        public PointF ToScreenScale(PointD p)
        {
            float x = ToScreenScaleW((float)p.X);
            float y = ToScreenScaleH((float)p.Y);
            return new PointF(x, y);
        }
        /// It converts a rectangle in microns to a rectangle in pixels
        /// 
        /// @param x The x coordinate of the rectangle
        /// @param y -0.0015
        /// @param w width of the image in microns
        /// @param h height of the rectangle
        /// 
        /// @return A RectangleF object.
        public RectangleF ToScreenRectF(double x, double y, double w, double h)
        {
            if (SelectedImage == null)
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
            if (SelectedImage.isPyramidal)
            {
                PointD d = ToViewSpace(x, y);
                double dw = ToViewSizeW(w);
                double dh = ToViewSizeH(h);
                return new RectangleF((float)(d.X - PyramidalOrigin.X), (float)(d.Y - PyramidalOrigin.Y), (float)dw, (float)dh);
            }
            else
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
        }
        /// > It takes a rectangle in the coordinate system of the stage and returns a rectangle in the
        /// coordinate system of the picturebox
        /// 
        /// @param x The x coordinate of the upper-left corner of the rectangle.
        /// @param y The y-coordinate of the upper-left corner of the rectangle.
        /// @param w width of the rectangle
        /// @param h height of the rectangle
        /// 
        /// @return A RawRectangleF object.
        public RawRectangleF ToRawRectF(double x, double y, double w, double h)
        {
            double xd = dxPanel.Width / 2;
            double yd = dxPanel.Height / 2;
            return new RawRectangleF((float)(xd - x), (float)(yd - y), (float)(xd - (x + w)), (float)(yd - (y + h)));
        }
        /// > It converts a rectangle from world space to screen space
        /// 
        /// @param RectangleD The rectangle to convert.
        /// 
        /// @return A RectangleF object.
        public RectangleF ToScreenSpace(RectangleD p)
        {
            return ToScreenRectF(p.X, p.Y, p.W, p.H);
        }
        /// > It converts a rectangle from world space to screen space
        /// 
        /// @param RectangleF The rectangle to convert.
        /// 
        /// @return A RectangleF object.
        public RectangleF ToScreenSpace(RectangleF p)
        {
            return ToScreenRectF(p.X, p.Y, p.Width, p.Height);
        }
        /// It takes an array of RectangleD objects and returns an array of RectangleF objects
        /// 
        /// @param p The rectangle to convert
        /// 
        /// @return A RectangleF[]
        public RectangleF[] ToScreenSpace(RectangleD[] p)
        {
            RectangleF[] rs = new RectangleF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                rs[i] = ToScreenSpace(p[i]);
            }
            return rs;
        }
        /// It takes an array of RectangleF objects and returns an array of RectangleF objects
        /// 
        /// @param p The rectangle to convert
        /// 
        /// @return A RectangleF[]
        public RectangleF[] ToScreenSpace(RectangleF[] p)
        {
            RectangleF[] rs = new RectangleF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                rs[i] = ToScreenSpace(p[i]);
            }
            return rs;
        }
        /// > Convert a list of points from world space to screen space
        /// 
        /// @param p The point to convert
        /// 
        /// @return A PointF[] array of points.
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
        /// ToScreenW(x) = x * PxWmicron
        /// 
        /// @param x the x coordinate of the point to be converted
        /// 
        /// @return The return value is a float.
        public float ToScreenW(double x)
        {
            if (HardwareAcceleration)
            {
                return (float)(x * PxWmicron);
            }
            return (float)(x * PxWmicron);
        }
        /// > Convert a value in microns to a value in pixels
        /// 
        /// @param y the y coordinate of the point to be converted
        /// 
        /// @return The return value is a float.
        public float ToScreenH(double y)
        {
            if (HardwareAcceleration)
            {
                return (float)(y * PxHmicron);
            }
            return (float)(y * PxHmicron);
        }
        /// If the user presses the "C" key while holding down the "Control" key, then the function
        /// "CopySelection" is called
        /// 
        /// @param sender The object that raised the event.
        /// @param KeyEventArgs The event arguments for the key press.
        /// 
        /// @return The return value is a PointD object.
        public void ImageView_KeyDown(object sender, KeyEventArgs e)
        {
            double moveAmount = 5 * Scale.Width;
            Plugins.KeyDownEvent(sender, e);
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

        private void ImageView_KeyUp(object sender, KeyEventArgs e)
        {
            Plugins.KeyUpEvent(sender, e);
        }

        private void ImageView_KeyPress(object sender, KeyPressEventArgs e)
        {
            Plugins.KeyPressEvent(sender, e);
        }
        /// If the selected image is not null, set the origin to the center of the image, and set the
        /// scale to the height of the image.
        /// 
        /// @return The method is returning the value of the variable "Scale"
        public void GoToImage()
        {
            GoToImage(0);
        }
        public PointD PyramidalOriginTransformed
        {
            get { return new PointD(PyramidalOrigin.X * Resolution, PyramidalOrigin.Y * Resolution); }
            set { PyramidalOrigin = new PointD(value.X / Resolution, value.Y / Resolution); }
        }
        PointD mouseDownInt = new PointD(0, 0);
        PointD mouseMoveInt = new PointD(0, 0);
        PointD mouseMove = new PointD(0, 0);
        /* A property that returns the value of the mouseDownInt variable. */
        public PointD MouseDownInt
        {
            get { return mouseDownInt; }
            set { mouseDownInt = value; }
        }
        public PointD MouseMoveInt
        {
            get { return mouseMoveInt; }
            set { mouseMoveInt = value; }
        }
        /// It takes an image index and centers the image in the viewport
        /// 
        /// @param i the index of the image to go to
        /// 
        /// @return The method is returning the value of the variable "i"
        public void GoToImage(int i)
        {
            if (Images.Count <= i)
                return;
            if (SelectedImage.Type == BioImage.ImageType.pyramidal)
            {
                if (SelectedImage.OpenSlideBase != null)
                {
                    if (MacroResolution.HasValue)
                    {
                        int lev = MacroResolution.Value - 2;
                        Resolution = _openSlideBase.Schema.Resolutions[lev].UnitsPerPixel * 0.98;
                    }
                    else
                    {
                        int lev = 0;
                        Resolution = _openSlideBase.Schema.Resolutions[lev].UnitsPerPixel * 0.98;
                    }
                }
                else
                {
                    if (MacroResolution.HasValue)
                    {
                        int lev = MacroResolution.Value - 1;
                        Resolution = SelectedImage.SlideBase.Schema.Resolutions[lev].UnitsPerPixel * 0.98;
                        PyramidalOrigin = new PointD(0, 0);
                    }
                    else
                    {
                        Resolution = SelectedImage.GetUnitPerPixel(SelectedImage.Resolutions.Count - 1) * 0.98;
                    }
                }
            }
            double dx = Images[i].Volume.Width / 2;
            double dy = Images[i].Volume.Height / 2;
            Origin = new PointD(-(Images[i].Volume.Location.X + dx), -(Images[i].Volume.Location.Y + dy));
            double wx, wy;
            if (HardwareAcceleration)
            {
                wx = ViewWidth / ToScreenW(SelectedImage.Volume.Width);
                wy = ViewHeight / ToScreenH(SelectedImage.Volume.Height);
            }
            else
            {
                wx = ViewWidth / ToScreenScaleW(SelectedImage.Volume.Width);
                wy = ViewHeight / ToScreenScaleH(SelectedImage.Volume.Height);
            }
            Scale = new SizeF((float)wy, (float)wy);
            UpdateView();
        }

        /// It opens a new form, and then it opens a new image in that form.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void goToImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToImage();
        }
        /// The function is called when the user clicks on the "Go To" menu item. It sets the origin to
        /// the point where the user clicked
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void goToToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Origin = new PointD(mouseDown.X, mouseDown.Y);
        }
        /// > When the user clicks on the "Go to Image" menu item, the program will clear the menu
        /// item's drop down menu and then add a menu item for each image in the Images list
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
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
        /// When the user clicks on an item in the dropdown menu, the function finds the index of the
        /// image in the list of images and then calls the GoToImage function with that index
        /// 
        /// @param sender System.Object
        /// @param ToolStripItemClickedEventArgs 
        /// 
        /// @return The filename of the image that was clicked on.
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
        public new void Dispose()
        {
            for (int i = 0; i < Bitmaps.Count; i++)
            {
                if (Bitmaps[i] != null)
                    Bitmaps[i].Dispose();
            }
            for (int i = 0; i < dBitmaps.Count; i++)
            {
                if (dBitmaps[i] != null)
                    dBitmaps[i].Dispose();
            }
        }

        /// The function is called when the user clicks on the "Go To" menu item in the context menu.
        /// The function sets the stage position to the position of the mouse click
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void goToToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Microscope.Stage.SetPosition(mouseDown.X, mouseDown.Y);
        }

        /// It draws the selected ROI's on the image
        /// 
        /// @param sender
        /// @param EventArgs e
        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics.Graphics g = Graphics.Graphics.FromImage(SelectedBuffer);
            foreach (ROI item in AnnotationsRGB)
            {
                Graphics.Pen p = new Graphics.Pen(Tools.DrawColor, (int)Tools.StrokeWidth, SelectedBuffer.BitsPerPixel);
                g.pen = p;
                if (item.Selected)
                {
                    if (item.type == ROI.Type.Line)
                    {
                        g.DrawLine(new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(0)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(0)).Y), new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(1)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(1)).Y));
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
                                g.DrawLine(new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(i)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(i)).Y), new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(i + 1)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(i + 1)).Y));
                            }
                            g.DrawLine(new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(0)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(0)).Y), new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(item.GetPointCount() - 1)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(item.GetPointCount() - 1)).Y));

                        }
                        else
                        {
                            for (int i = 0; i < item.GetPointCount() - 1; i++)
                            {
                                g.DrawLine(new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(i)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(i)).Y), new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(i + 1)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(i + 1)).Y));
                            }
                        }
                    }
                }
            }
            update = true;
            UpdateImage();
        }

        /// It takes the selected ROI's and draws them on the image
        /// 
        /// @param sender
        /// @param EventArgs e
        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics.Graphics g = Graphics.Graphics.FromImage(SelectedBuffer);
            foreach (ROI item in AnnotationsRGB)
            {
                Graphics.Pen p = new Graphics.Pen(Tools.DrawColor, (int)Tools.StrokeWidth, SelectedBuffer.BitsPerPixel);
                if (item.Selected)
                {
                    if (item.type == ROI.Type.Line)
                    {
                        g.DrawLine(new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(0)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(0)).Y), new PointF((float)SelectedImage.ToImageSpace(item.GetPoint(1)).X, (float)SelectedImage.ToImageSpace(item.GetPoint(1)).Y));
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

        public void GoToStage()
        {
            RectangleD d = Microscope.GetViewRectangle(false);
            double dx = d.W / 2;
            double dy = d.H / 2;
            Origin = new PointD(-(d.X + dx), -(d.Y + dy));
            double wx, wy;
            if (HardwareAcceleration)
            {
                wx = Math.Abs(ViewWidth / ToScreenW(d.W));
                wy = Math.Abs(ViewHeight / ToScreenH(d.H));
            }
            else
            {
                wx = Math.Abs(ViewWidth / ToScreenScaleW(d.W));
                wy = Math.Abs(ViewHeight / ToScreenScaleH(d.H));
            }
            scale.Width = (float)wy;
            scale.Height = (float)wy;
            update = true;
            UpdateView();
        }
        /// Move the stage to the location of the selected image
        /// 
        /// @return The method is returning the value of the SelectedImage property.
        public void MoveStageToImage()
        {
            if (SelectedImage == null)
                return;
            Microscope.Stage.SetPosition(SelectedImage.Volume.Location.X, SelectedImage.Volume.Location.Y);
            update = true;
            UpdateView();
        }
        /// Move the stage to the location of the image at index i
        /// 
        /// @param i the index of the image in the list of images
        public void MoveStageToImage(int i)
        {
            update = true;
            Microscope.Stage.SetPosition(Images[i].Volume.Location.X, Images[i].Volume.Location.Y);
            UpdateView();
        }
        /// It's a function that is called when the user clicks on the "Go To Stage" button in the menu
        /// bar.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void goToStageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = true;
            GoToStage();
        }
        /// It moves the stage to the image that was clicked on in the drop down menu
        /// 
        /// @param sender System.Windows.Forms.ToolStripMenuItem
        /// @param ToolStripItemClickedEventArgs 
        /// 
        /// @return The index of the image in the list.
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

        /// Move the stage to the center of the image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void moveStageToImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveStageToImage();
        }
        private void layersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (layersTool == null)
                layersTool = new Layers();
            layersTool.Show();
        }
        /// If the image is pyramidal, update the image and update the view
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (ViewWidth < 2 || ViewHeight < 2) return;
            if (SelectedImage.isPyramidal)
            {
                UpdateImages(true);
            }
        }
        Configuration conf = new Configuration();
        /// If the panel is resized, update the width and height of the configuration object, update the
        /// device, and update the view
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void dxPanel_SizeChanged(object sender, EventArgs e)
        {
            if (HardwareAcceleration && dx != null)
            {
                conf.Width = dxPanel.Width;
                conf.Height = dxPanel.Height;
                dx.Update(conf, dxPanel.Handle);
                UpdateView();
            }
        }
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Function.Initialize();
        }
        private void hideOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hideOverViewToolStripMenuItem.Text == "Hide Overview")
            {
                ShowOverview = false;
                hideOverViewToolStripMenuItem.Text = "Show Overview";
            }
            else
            {
                ShowOverview = true;
                hideOverViewToolStripMenuItem.Text = "Hide Overview";
            }
        }
        private void removeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveImage(SelectedIndex);
        }

        private void removeImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveImages();
        }
        #region OpenSlide
        private OpenSlideBase _openSlideBase;
        private SlideBase _slideBase;
        /// <summary>
        /// Open slide file
        /// </summary>
        private void Initialize(string file, BioImage bm)
        {
            if (bm.Type != BioImage.ImageType.pyramidal)
                return;
            if (bm.OpenSlideBase == null)
            {
                _slideBase = bm.SlideBase;
            }
            else
            {
                _openSlideBase = bm.OpenSlideBase;
            }
        }

        #endregion

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            SelectedImage.PyramidalOrigin = new PointD(vScrollBar.Value, hScrollBar.Value);
        }
    }
}
