using AForge;
using NetVips;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioLib;
namespace BioImager
{
    public partial class SlideImager : Form
    {
        private static Stopwatch Stopwatch = new Stopwatch();
        /// Convert a point in the stage coordinate system to a point in the image coordinate system
        /// 
        /// @param PointD 
        /// 
        /// @return A PointF object.
        public static PointD ToImageSpace(PointD p, PointD stage, double physX, double physY)
        {
            PointD pp = new PointD();
            pp.X = (float)((p.X - stage.X) / physX);
            pp.Y = (float)((p.Y - stage.Y) / physY);
            return pp;
        }
        public class BaseLevel
        {
            public double Focus { get; set; }
            public int Objective { get; set; }
            public BaseLevel(double focus, int objective)
            {
                Focus = focus;
                Objective = objective;
            }
            public override string ToString()
            {
                return "Focus:" + Focus + ", Objective:" + Objective;
            }
        }
        public class Level
        {
            public List<Level> Levels = new List<Level>();
            public Level Parent { get; set; } = null;
            public Point3D Location { get; set; }
            public List<BioImage> BioImages { get; set; } = new List<BioImage>();
            public int Objective { get; set; }
            public double Focus { get; set; }
            public NetVips.Image VipsImage { get; set; }
            public PixelFormat PixelFormat { get; set; }
            public double PhysicalX { get; set; }
            public double PhysicalY { get; set; }
            public AForge.Bitmap Image { get; set; } = null;
            public double Width { get; set; }
            public double Height { get; set; }
            public int Index {  get; set; }
            public double WidthPx { get { return Width / PhysicalX; } }
            public double HeightPx { get { return Height / PhysicalY; } }
            public List<Channel> Channels = new List<Channel>();
            public Enums.ForeignTiffCompression Compression { get; set; }
            public int CompressionLevel { get; set; }
            public void Init(double w, double h, PixelFormat pf, int index)
            {
                RectangleD r = Microscope.GetObjectiveViewRectangle();
                int wi = (int)(w / r.W);
                int hi = (int)(h / r.H);
                double xx = 2 * index;
                if(index==0)
                    Image = new AForge.Bitmap((int)(w / PhysicalX), (int)(h / PhysicalY), pf);
                else
                    Image = new AForge.Bitmap((int)(w / PhysicalX / xx), (int)(h / PhysicalY / xx), pf);
                int bands = BioImage.GetBands(pf);
                if (Image.BitsPerPixel > 8)
                    VipsImage = NetVips.Image.NewFromMemory(Image.Data, (ulong)Image.Length, Image.Width, Image.Height, bands, Enums.BandFormat.Ushort);
                else
                    VipsImage = NetVips.Image.NewFromMemory(Image.Data, (ulong)Image.Length, Image.Width, Image.Height, bands, Enums.BandFormat.Uchar);
            }
            public Level(double w, double h, PixelFormat pf, int levels, Enums.ForeignTiffCompression comp, double physX, double physY)
            {
                PhysicalX = physX; PhysicalY = physY;
                Compression = comp;
                for (int i = 0; i < levels; i++)
                {
                    Levels.Add(new Level(w, h, pf));
                    Levels[i].Parent = this;
                }
            }
            public Level(double w, double h, PixelFormat pf)
            {
                Width = w; Height = h; PixelFormat = pf;
            }
            public void SaveLevel(string file)
            {
                string met = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" +
                "<OME xmlns=\"http://www.openmicroscopy.org/Schemas/OME/2016-06\" " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xsi:schemaLocation=\"http://www.openmicroscopy.org/Schemas/OME/2016-06 http://www.openmicroscopy.org/Schemas/OME/2016-06/ome.xsd\">";
                int c = Parent.Image.RGBChannelsCount;
                string endian = Parent.Image.LittleEndian.ToString().ToLower();
                int ib = 0;
                met +=
                "<Image ID=\"Image:" + ib + "\">" +
                "<Pixels BigEndian=\"" + endian + "\" DimensionOrder= \"XYCZT\" ID= \"Pixels:0\" Interleaved=\"true\" " +
                "PhysicalSizeX=\"" + PhysicalX + "\" PhysicalSizeXUnit=\"µm\" PhysicalSizeY=\"" + PhysicalY + "\" PhysicalSizeYUnit=\"µm\" SignificantBits=\"" + Parent.Image.BitsPerPixel + "\" " +
                "SizeC = \"" + c + "\" SizeT = \"" + 1 + "\" SizeX =\"" + Parent.Image.Width +
                "\" SizeY= \"" + Parent.Image.Height + "\" SizeZ=\"" + 1;
                if (Parent.Image.BitsPerPixel > 8) met += "\" Type= \"uint16\">";
                else met += "\" Type= \"uint8\">";
                int i = 0;
                foreach (Channel ch in Channels)
                {
                    met += "<Channel ID=\"Channel:" + ib + ":" + i + "\" SamplesPerPixel=\"1\"></Channel>";
                    i++;
                }
                met += "</Pixels></Image></OME>";
                A:
                try
                {
                    using var mutated = Parent.VipsImage.Mutate(mutable =>
                    {
                        // Set the ImageDescription tag
                        mutable.Set(GValue.GStrType, "image-description", met);
                        mutable.Set(GValue.GIntType, "page-height", Parent.Image.Height);
                    });
                    if (Parent.Image.BitsPerPixel > 8)
                        mutated.Tiffsave(file, Compression, 1, Enums.ForeignTiffPredictor.None, true, (int)512, (int)512, true, false, 16,
                        Enums.ForeignTiffResunit.Cm, 1000 * PhysicalX, 1000 * PhysicalY, true, null, Enums.RegionShrink.Nearest,
                        CompressionLevel, true, Enums.ForeignDzDepth.Onetile, true, false, null, null, (int)Parent.Image.Height);
                    else
                        mutated.Tiffsave(file, Compression, 1, Enums.ForeignTiffPredictor.None, true, (int)512, (int)512, true, false, 8,
                        Enums.ForeignTiffResunit.Cm, 1000 * PhysicalX, 1000 * PhysicalY, true, null, Enums.RegionShrink.Nearest,
                        CompressionLevel, true, Enums.ForeignDzDepth.Onetile, true, false, null, null, (int)Parent.Image.Height);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    goto A;
                }
                

            }
            public void SaveLevels(string file)
            {
                string met = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" +
                "<OME xmlns=\"http://www.openmicroscopy.org/Schemas/OME/2016-06\" " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xsi:schemaLocation=\"http://www.openmicroscopy.org/Schemas/OME/2016-06 http://www.openmicroscopy.org/Schemas/OME/2016-06/ome.xsd\">";
                int c = Image.RGBChannelsCount;
                string endian = Image.LittleEndian.ToString().ToLower();
                int ib = 0;
                foreach (Level l in Levels)
                {
                    met +=
                    "<Image ID=\"Image:" + ib + "\">" +
                    "<Pixels BigEndian=\"" + endian + "\" DimensionOrder= \"XYCZT\" ID= \"Pixels:0\" Interleaved=\"true\" " +
                    "PhysicalSizeX=\"" + PhysicalX + "\" PhysicalSizeXUnit=\"µm\" PhysicalSizeY=\"" + PhysicalY + "\" PhysicalSizeYUnit=\"µm\" SignificantBits=\"" + Image.BitsPerPixel + "\" " +
                    "SizeC = \"" + c + "\" SizeT = \"" + 1 + "\" SizeX =\"" + Image.Width +
                    "\" SizeY= \"" + Image.Height + "\" SizeZ=\"" + 1;
                    if (Image.BitsPerPixel > 8) met += "\" Type= \"uint16\">";
                    else met += "\" Type= \"uint8\">";
                    int i = 0;
                    foreach (Channel ch in l.Channels)
                    {
                        met += "<Channel ID=\"Channel:" + ib + ":" + i + "\" SamplesPerPixel=\"1\"></Channel>";
                        i++;
                    }
                    met += "</Pixels></Image>";
                    ib++;
                }
                met += "</OME>";
                int li = 0;
                foreach (Level l in Levels)
                {
                    string fs = file.Replace(".ome.tif", "");
                    NetVips.Image im = NetVips.Image.NewFromFile(fs + "-" + li + "ome.tif");
                    using var mutated = im.Mutate(mutable =>
                    {
                        // Set the ImageDescription tag
                        mutable.Set(GValue.GStrType, "image-description", met);
                        mutable.Set(GValue.GIntType, "page-height", l.Height * l.PhysicalY);
                    });
                    if (l.Image.BitsPerPixel > 8)
                        mutated.Tiffsave(file, Compression, 1, Enums.ForeignTiffPredictor.None, true, (int)l.WidthPx, (int)l.HeightPx, true, false, 16,
                        Enums.ForeignTiffResunit.Cm, 1000 * l.PhysicalX, 1000 * l.PhysicalY, true, null, Enums.RegionShrink.Nearest,
                        CompressionLevel, true, Enums.ForeignDzDepth.One, true, false, null, null, (int)l.HeightPx);
                    else
                        mutated.Tiffsave(file, Compression, 1, Enums.ForeignTiffPredictor.None, true, (int)l.WidthPx, (int)l.HeightPx, true, false, 8,
                        Enums.ForeignTiffResunit.Cm, 1000 * l.PhysicalX, 1000 * l.PhysicalY, true, null, Enums.RegionShrink.Nearest,
                        CompressionLevel, true, Enums.ForeignDzDepth.One, true, false, null, null, (int)l.HeightPx);
                    li++;
                }
            }
            public void ImageLevel(int level)
            {
                Level l = Levels[level];
                if (l.Image == null)
                    Init(l.Width, l.Height, l.PixelFormat, l.Index);
                Microscope.SetPosition(new Point3D(l.Location.X, l.Location.Y, l.Focus));
                Microscope.Objectives.SetPosition(l.Objective);
                RectangleD r = Microscope.GetObjectiveViewRectangle();
                int w = (int)Math.Ceiling(l.Width / r.W);
                int h = (int)Math.Ceiling(l.Height / r.H);
                count = w * h;
                bool leftright = false;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Point3D pd = Microscope.GetPosition();
                        l.ImageTile(new PointD(pd.X, pd.Y),(w * h) - ((y * w) + x));
                        SlideImager.progress = (int)((double)((y * w) + x) / (double)(w * h));
                        if (leftright)
                            Microscope.MoveFieldLeft();
                        else
                            Microscope.MoveFieldRight();
                    }
                    Point3D p = Microscope.GetPosition();
                    l.ImageTile(new PointD(p.X, p.Y), (w * h) - ((y * w) + w));
                    leftright = !leftright;
                    Microscope.MoveFieldDown();
                }
            }
            private void ImageTile(PointD loc, int tiles)
            {
                Stopwatch.Restart();
            A:
                BioImage b = Microscope.TakeImage(false, false, false);
                if (b == null)
                    goto A;
                PhysicalX = b.PhysicalSizeX;
                PhysicalY = b.PhysicalSizeY;
                Channels = b.Channels;
                PointD pd = ToImageSpace(loc, new PointD(Location.X, Location.Y), b.PhysicalSizeX, b.PhysicalSizeY);
                int bands = BioImage.GetBands(PixelFormat);
                NetVips.Image im;
                if (b.bitsPerPixel > 8)
                    im = NetVips.Image.NewFromMemory(b.Buffers[0].Data, (ulong)b.Buffers[0].Length, b.Buffers[0].Width, b.Buffers[0].Height, bands, Enums.BandFormat.Ushort);
                else
                    im = NetVips.Image.NewFromMemory(b.Buffers[0].Data, (ulong)b.Buffers[0].Length, b.Buffers[0].Width, b.Buffers[0].Height, bands, Enums.BandFormat.Uchar);
                Parent.VipsImage = Parent.VipsImage.Insert(im, (int)pd.X, (int)pd.Y);
                im.Dispose();
                b.Dispose();
                Stopwatch.Stop();
                status = "Time Left:" + (Stopwatch.Elapsed * tiles).ToString();
            }
        }
        private Level SlideLevel { get; set; } = null;
        private List<BaseLevel> Levels { get; set; } = new List<BaseLevel>();
        public SlideImager()
        {
            InitializeComponent();
            foreach (Objectives.Objective o in Microscope.Objectives.List)
            {
                objBox.Items.Add(o);
            }
            foreach (Enums.ForeignTiffCompression o in (Enums.ForeignTiffCompression[])Enum.GetValues(typeof(Enums.ForeignTiffCompression)))
            {
                compBox.Items.Add(o);
            }
        }


        private void setToStageBut_Click(object sender, EventArgs e)
        {
            Point3D p = Microscope.GetPosition();
            xBox.Value = (decimal)p.X;
            yBox.Value = (decimal)p.Y;
        }

        private void setSizeBut_Click(object sender, EventArgs e)
        {
            Point3D p = Microscope.GetPosition();
            wBox.Value = (decimal)(p.X - (double)xBox.Value);
            hBox.Value = (decimal)(p.Y - (double)yBox.Value);
        }

        private void startBut_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Microscope.Objectives.SetPosition(Levels[0].Objective);
            BioImage b = Microscope.TakeImage(false, false, false);
            SlideLevel = new Level((double)wBox.Value, (double)hBox.Value, b.Buffers[0].PixelFormat, Levels.Count, (Enums.ForeignTiffCompression)compBox.SelectedItem, b.PhysicalSizeX, b.PhysicalSizeY);
            for (int i = 0; i < Levels.Count; i++)
            {
                SlideLevel.Levels[i].Location = b.Volume.Location;
                SlideLevel.Levels[i].Focus = Levels[i].Focus;
                SlideLevel.Levels[i].Objective = Levels[i].Objective;
            }
            SlideLevel.PixelFormat = b.Buffers[0].PixelFormat;
            SlideLevel.PhysicalX = b.PhysicalSizeX;
            SlideLevel.PhysicalY = b.PhysicalSizeY;
            timer.Start();
            RunImaging(saveFileDialog.FileName, SlideLevel, Levels);
        }
        static Level level = null;
        static string ffile = null;
        static List<BaseLevel> levels;
        static int prog;
        static string status = "";
        static bool imaging = false;
        static int count = 0;
        private static int progress { get { return prog; } set { prog = value; } }
        private static void Image()
        {
            for (int i = 0; i < levels.Count; i++)
            {
                string f = ffile.Replace(".ome.tif", "");
                A:
                try
                {
                    level.ImageLevel(i);
                    level.Levels[i].SaveLevel(f + "-" + i + ".ome.tif");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    goto A;
                }
            }
            imaging = false;
            level.SaveLevels(ffile);
        }
        private static void RunImaging(string file, Level lev, List<BaseLevel> lvs)
        {
            imaging = true;
            level = lev;
            levels = lvs;
            ffile = file;
            Thread th = new Thread(Image);
            th.Start();
        }

        private void addBut_Click(object sender, EventArgs e)
        {
            BaseLevel bl = new BaseLevel((double)focBox.Value, objBox.SelectedIndex);
            Levels.Add(bl);
            listBox.Items.Add(bl);
        }

        private void SlideImager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void focBut_Click(object sender, EventArgs e)
        {
            focBox.Value = (decimal)Microscope.GetFocus();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem == null)
                return;
            BaseLevel bl = (BaseLevel)listBox.SelectedItem;
            focBox.Value = (decimal)bl.Focus;
            objBox.SelectedIndex = bl.Objective;
        }

        private void objBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItems.Count > 0)
            {
                BaseLevel bl = (BaseLevel)listBox.SelectedItem;
                bl.Objective = objBox.SelectedIndex;
            }
        }

        private void focBox_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItems.Count > 0)
            {
                BaseLevel bl = (BaseLevel)listBox.SelectedItem;
                bl.Focus = (double)focBox.Value;
            }
        }

        private void remBut_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedItems.Count > 0)
                listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!imaging)
                timer.Stop();
            toolStripProgressBar.Value = progress * 100;
            toolStripStatusLabel.Text = status;
        }
    }
}
