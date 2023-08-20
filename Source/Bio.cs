using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using BitMiracle.LibTiff.Classic;
using loci.common.services;
using loci.formats;
using loci.formats.services;
using ome.xml.model.primitives;
using loci.formats.meta;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Runtime.Serialization;
using System.Text;
using Bitmap = AForge.Bitmap;
using Color = AForge.Color;
using PointF = AForge.PointF;
using RectangleF = AForge.RectangleF;
using RectangleD = AForge.RectangleD;
using Rectangle = AForge.Rectangle;
using Point = AForge.Point;
using loci.formats.@in;
using Gtk;
using System.Linq;
using NetVips;

namespace Bio
{
    /* A class declaration. */
    public static class Images
    {
        public static List<BioImage> images = new List<BioImage>();
        /// 
        /// @param ids The id of the image you want to get.
        public static BioImage GetImage(string ids)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (images[i].ID == ids || images[i].file == ids)
                    return images[i];
            }
            return null;
        }
        /// It adds an image to the list of images
        /// 
        /// @param BioImage A class that contains the image data and other information.
        public static void AddImage(BioImage im, bool newtab)
        {
            if (images.Contains(im)) return;
            im.Filename = GetImageName(im.ID);
            im.ID = im.Filename;
            images.Add(im);
            App.nodeView.UpdateNodes();
            if (newtab)
                App.tabsView.AddTab(im);
            //App.Image = im;
            //NodeView.viewer.AddTab(im);
        }
        /// It takes a string as an argument, and returns the number of times that string appears in the
        /// list of images
        /// 
        /// @param s The name of the image
        /// 
        /// @return The number of images that contain the name of the image.
        public static int GetImageCountByName(string s)
        {
            int i = 0;
            for (int im = 0; im < images.Count; im++)
            {
                string name = images[im].ID;
                int sti = name.LastIndexOf("-");
                string st = name.Remove(sti);
                if (images[im].ID.Contains(st))
                    i++;
            }
            return i;
        }
        /// It takes a string, and returns a string
        /// 
        /// @param s The path to the image.
        /// 
        /// @return The name of the image.
        public static string GetImageName(string s)
        {
            //Here we create a unique ID for an image.
            int i = Images.GetImageCountByName(s);
            if (i == 0)
                return Path.GetFileName(s);
            string name = Path.GetFileNameWithoutExtension(s);
            string ext = Path.GetExtension(s);
            int sti = name.LastIndexOf("-");
            if (sti == -1)
            {
                return name + "-" + (i) + ext;
            }
            else
            {
                string stb = name.Substring(0, sti);
                string sta = name.Substring(sti + 1, name.Length - sti - 1);
                int ind;
                if (int.TryParse(sta, out ind))
                {
                    return stb + "-" + (i + 1).ToString() + ext;
                }
                else
                    return name + "-" + (i + 1) + ext;
            }
            //
        }
        /// This function removes an image from the table
        /// 
        /// @param BioImage This is the image that you want to remove.
        public static void RemoveImage(BioImage im)
        {
            RemoveImage(im.ID);
        }
        /// It removes an image from the table
        /// 
        /// @param id The id of the image to remove.
        /// 
        /// @return The image is being returned.
        public static void RemoveImage(string id)
        {
            BioImage im = GetImage(id);
            if (im == null)
                return;
            images.Remove(im);
            //im.Dispose();
            im = null;
            Recorder.AddLine("Bio.Table.RemoveImage(" + '"' + id + '"' + ");");
        }

        /// It updates an image from the table
        /// 
        /// @param id The id of the image to update.
        /// @param im The BioImage to update with.
        /// @return The image is being returned.
        public static void UpdateImage(BioImage im)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (images[i].ID == im.ID)
                {
                    images[i] = im;
                    return;
                }
            }
        }
    }
    /* A struct that is used to store the resolution of an image. */
    /* A struct that is used to store the resolution of an image. */
    public struct Resolution
    {
        int x;
        int y;
        PixelFormat format;
        double px, py, pz, sx, sy, sz;
        public int SizeX
        {
            get { return x; }
            set { x = value; }
        }
        public int SizeY
        {
            get { return y; }
            set { y = value; }
        }
        public double PhysicalSizeX
        {
            get { return px; }
            set { px = value; }
        }
        public double PhysicalSizeY
        {
            get { return py; }
            set { py = value; }
        }
        public double PhysicalSizeZ
        {
            get { return pz; }
            set { pz = value; }
        }
        public double StageSizeX
        {
            get { return sx; }
            set { sx = value; }
        }
        public double StageSizeY
        {
            get { return sy; }
            set { sy = value; }
        }
        public double StageSizeZ
        {
            get { return sz; }
            set { sz = value; }
        }
        public double VolumeWidth
        {
            get
            {
                return PhysicalSizeX * SizeX;
            }
        }
        public double VolumeHeight
        {
            get
            {
                return PhysicalSizeY * SizeY;
            }
        }
        public PixelFormat PixelFormat
        {
            get { return format; }
            set { format = value; }
        }
        public int RGBChannelsCount
        {
            get
            {
                if (PixelFormat == PixelFormat.Format8bppIndexed || PixelFormat == PixelFormat.Format16bppGrayScale)
                    return 1;
                else if (PixelFormat == PixelFormat.Format24bppRgb || PixelFormat == PixelFormat.Format48bppRgb)
                    return 3;
                else
                    return 4;
            }
        }
        public long SizeInBytes
        {
            get
            {
                if (format == PixelFormat.Format8bppIndexed)
                    return (long)y * (long)x;
                else if (format == PixelFormat.Format16bppGrayScale)
                    return (long)y * (long)x * 2;
                else if (format == PixelFormat.Format24bppRgb)
                    return (long)y * (long)x * 3;
                else if (format == PixelFormat.Format32bppRgb || format == PixelFormat.Format32bppArgb)
                    return (long)y * (long)x * 4;
                else if (format == PixelFormat.Format48bppRgb || format == PixelFormat.Format48bppRgb)
                    return (long)y * (long)x * 6;
                throw new NotSupportedException(format + " is not supported.");
            }
        }
        public Resolution(int w, int h, int omePx, int bitsPerPixel, double physX, double physY, double physZ, double stageX, double stageY, double stageZ)
        {
            x = w;
            y = h;
            sx = stageX;
            sy = stageY;
            sz = stageZ;
            format = BioImage.GetPixelFormat(omePx, bitsPerPixel);
            px = physX;
            py = physY;
            pz = physZ;
        }
        public Resolution(int w, int h, PixelFormat f, double physX, double physY, double physZ, double stageX, double stageY, double stageZ)
        {
            x = w;
            y = h;
            format = f;
            sx = stageX;
            sy = stageY;
            sz = stageZ;
            px = physX;
            py = physY;
            pz = physZ;
        }
        public Resolution Copy()
        {
            return new Resolution(x, y, format, px, py, pz, sx, sy, sz);
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ") " + format.ToString() + " " + (SizeInBytes / 1000) + " KB";
        }
    }
    /* The ROI class is a class that contains a list of points, a bounding box, and a type */
    public class ROI
    {
        /* Defining an enum. */
        public enum Type
        {
            Rectangle,
            Point,
            Line,
            Polygon,
            Polyline,
            Freeform,
            Ellipse,
            Label,
            Mask
        }
        /* A property of a class. */
        public PointD Point
        {
            get
            {
                if (Points.Count == 0)
                    return new PointD(0, 0);
                if (type == Type.Line || type == Type.Ellipse || type == Type.Label || type == Type.Freeform)
                    return new PointD(BoundingBox.X, BoundingBox.Y);
                return Points[0];
            }
            set
            {
                if (Points.Count == 0)
                {
                    AddPoint(value);
                }
                else
                    UpdatePoint(value, 0);
                UpdateBoundingBox();
            }
        }
        public RectangleD Rect
        {
            get
            {
                if (Points.Count == 0)
                    return new RectangleD(0, 0, 0, 0);
                if (type == Type.Line || type == Type.Polyline || type == Type.Polygon || type == Type.Freeform || type == Type.Label)
                    return BoundingBox;
                if (type == Type.Rectangle || type == Type.Ellipse)
                    return new RectangleD(Points[0].X, Points[0].Y, Points[1].X - Points[0].X, Points[2].Y - Points[0].Y);
                else
                    return new RectangleD(Points[0].X, Points[0].Y, 1, 1);
            }
            set
            {
                if (type == Type.Line || type == Type.Polyline || type == Type.Polygon || type == Type.Freeform)
                {
                    BoundingBox = value;
                }
                else
                if (Points.Count < 4 && (type == Type.Rectangle || type == Type.Ellipse))
                {
                    AddPoint(new PointD(value.X, value.Y));
                    AddPoint(new PointD(value.X + value.W, value.Y));
                    AddPoint(new PointD(value.X, value.Y + value.H));
                    AddPoint(new PointD(value.X + value.W, value.Y + value.H));
                }
                else
                if (type == Type.Rectangle || type == Type.Ellipse)
                {
                    Points[0] = new PointD(value.X, value.Y);
                    Points[1] = new PointD(value.X + value.W, value.Y);
                    Points[2] = new PointD(value.X, value.Y + value.H);
                    Points[3] = new PointD(value.X + value.W, value.Y + value.H);
                }
                UpdateBoundingBox();
            }
        }
        public double X
        {
            get
            {
                return Point.X;
            }
            set
            {
                Rect = new RectangleD(value, Y, W, H);
                Recorder.AddLine("App.AddROI(" + BioImage.ROIToString(this) + ");");
            }
        }
        public double Y
        {
            get
            {
                return Point.Y;
            }
            set
            {
                Rect = new RectangleD(X, value, W, H);
                Recorder.AddLine("App.AddROI(" + BioImage.ROIToString(this) + ");");
            }
        }
        public double W
        {
            get
            {
                if (type == Type.Point)
                    return strokeWidth;
                else
                    return BoundingBox.W;
            }
            set
            {
                Rect = new RectangleD(X, Y, value, H);
                Recorder.AddLine("App.AddROI(" + BioImage.ROIToString(this) + ");");
            }
        }
        public double H
        {
            get
            {
                if (type == Type.Point)
                    return strokeWidth;
                else
                    return BoundingBox.H;
            }
            set
            {
                Rect = new RectangleD(X, Y, W, value);
                Recorder.AddLine("App.AddROI(" + BioImage.ROIToString(this) + ");");
            }
        }
        public int Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }
        public enum CoordinateSystem
        {
            pixel,
            micron
        }
        public Type type;
        public Gdk.Pixbuf mask;
        public static float selectBoxSize = 8f;
        private List<PointD> Points = new List<PointD>();
        public List<PointD> PointsD
        {
            get
            {
                return Points;
            }
        }
        public PointD[] PointsImage
        {
            get
            {
                return ImageView.SelectedImage.ToImageSpace(PointsD);
            }
        }
        private List<RectangleD> selectBoxs = new List<RectangleD>();
        public List<int> selectedPoints = new List<int>();
        public RectangleD BoundingBox;
        public float fontSize = 12;
        public Cairo.FontSlant slant;
        public Cairo.FontWeight weight;
        public string family = "Times New Roman";
        public ZCT coord;
        public Color strokeColor;
        public Color fillColor;
        public bool isFilled = false;
        public string id = "";
        public string roiID = "";
        public string roiName = "";
        public string properties = "";
        public int serie = 0;
        private string text = "";
        private int resolution = 0;
        public double strokeWidth = 1;
        public int shapeIndex = 0;
        public bool closed = false;
        public bool selected = false;
        public bool subPixel = false;

        /*
        public Size TextSize
        {
            get
            {
                return TextRenderer.MeasureText(text, font);
            }
        }
        */
        public ROI Copy()
        {
            ROI copy = new ROI();
            copy.id = id;
            copy.roiID = roiID;
            copy.roiName = roiName;
            copy.text = text;
            copy.strokeWidth = strokeWidth;
            copy.strokeColor = strokeColor;
            copy.fillColor = fillColor;
            copy.Points = Points;
            copy.selected = selected;
            copy.shapeIndex = shapeIndex;
            copy.closed = closed;
            copy.family = family;
            copy.fontSize = fontSize;
            copy.slant = slant;
            copy.selectBoxs = selectBoxs;
            copy.BoundingBox = BoundingBox;
            copy.isFilled = isFilled;
            copy.coord = coord;
            copy.selectedPoints = selectedPoints;

            return copy;
        }
        public ROI Copy(ZCT cord)
        {
            ROI copy = new ROI();
            copy.type = type;
            copy.id = id;
            copy.roiID = roiID;
            copy.roiName = roiName;
            copy.text = text;
            copy.strokeWidth = strokeWidth;
            copy.strokeColor = strokeColor;
            copy.fillColor = fillColor;
            copy.Points.AddRange(Points);
            copy.selected = selected;
            copy.shapeIndex = shapeIndex;
            copy.closed = closed;
            copy.family = family;
            copy.fontSize = fontSize;
            copy.slant = slant;
            copy.selectBoxs.AddRange(selectBoxs);
            copy.BoundingBox = BoundingBox;
            copy.isFilled = isFilled;
            copy.coord = cord;
            copy.selectedPoints = selectedPoints;
            return copy;
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                if (type == Type.Label)
                {
                    UpdateBoundingBox();
                }
            }
        }
        /// > This function returns a rectangle that is the bounding box of the object, but with a
        /// border of half the scale
        /// 
        /// @param scale the scale of the image
        /// 
        /// @return A rectangle with the following properties:
        public RectangleD GetSelectBound(double scaleX, double scaleY)
        {
            double fx = scaleX / 2;
            double fy = scaleY / 2;
            return new RectangleD(BoundingBox.X - fx, BoundingBox.Y - fy, BoundingBox.W + scaleX, BoundingBox.H + scaleY);
        }
        /* Creating a new ROI object. */
        public ROI()
        {
            coord = new ZCT(0, 0, 0);
            strokeColor = Color.Yellow;
            BoundingBox = new RectangleD(0, 0, 1, 1);
        }

        /// It returns an array of RectangleF objects that are used to draw the selection boxes around
        /// the points of the polygon
        /// 
        /// @param s the size of the select box
        /// 
        /// @return A list of RectangleF objects.
        public RectangleD[] GetSelectBoxes(double s)
        {
            double f = s / 2;
            selectBoxs.Clear();
            for (int i = 0; i < Points.Count; i++)
            {
                selectBoxs.Add(new RectangleD((float)(Points[i].X - f), (float)(Points[i].Y - f), (float)s, (float)s));
            }
            return selectBoxs.ToArray();
        }
        /// Create a new ROI object, add a point to it, and return it
        /// 
        /// @param ZCT a class that contains the Z, C, and T coordinates of the image.
        /// @param x x coordinate of the point
        /// @param y The y coordinate of the point
        /// 
        /// @return A new ROI object
        public static ROI CreatePoint(ZCT coord, double x, double y)
        {
            ROI an = new ROI();
            an.coord = coord;
            an.AddPoint(new PointD(x, y));
            an.type = Type.Point;
            Recorder.AddLine("ROI.CreatePoint(new ZCT(" + coord.Z + "," + coord.C + "," + coord.T + "), " + x + "," + y + ");");
            return an;
        }
        /// Create a new ROI object, set its type to Line, add two points to it, and return it
        /// 
        /// @param ZCT Z is the Z-axis, C is the color channel, and T is the time frame.
        /// @param PointD X,Y
        /// @param PointD X,Y
        /// 
        /// @return A new ROI object.

        public static ROI CreateLine(ZCT coord, PointD x1, PointD x2)
        {
            ROI an = new ROI();
            an.coord = coord;
            an.type = Type.Line;
            an.AddPoint(x1);
            an.AddPoint(x2);
            Recorder.AddLine("ROI.CreateLine(new ZCT(" + coord.Z + "," + coord.C + "," + coord.T + "), new PointD(" + x1.X + "," + x1.Y + "), new PointD(" + x2.X + "," + x2.Y + "));");
            return an;
        }
        /// Create a new ROI object with a rectangle shape, and add a line to the recorder
        /// 
        /// @param ZCT The ZCT coordinates of the image you want to create the ROI on.
        /// @param x x coordinate of the top left corner of the rectangle
        /// @param y y-coordinate of the top-left corner of the rectangle
        /// @param w width
        /// @param h height
        /// 
        /// @return A new ROI object.
        public static ROI CreateRectangle(ZCT coord, double x, double y, double w, double h)
        {
            ROI an = new ROI();
            an.coord = coord;
            an.type = Type.Rectangle;
            an.Rect = new RectangleD(x, y, w, h);
            Recorder.AddLine("ROI.CreateRectangle(new ZCT(" + coord.Z + "," + coord.C + "," + coord.T + "), new RectangleD(" + x + "," + y + "," + w + "," + h + ");");
            return an;
        }
        /// Create an ellipse ROI at the specified ZCT coordinate with the specified width and height
        /// 
        /// @param ZCT The ZCT coordinates of the image you want to create the ROI on.
        /// @param x x-coordinate of the top-left corner of the rectangle
        /// @param y The y-coordinate of the upper-left corner of the rectangle to create.
        /// @param w width
        /// @param h height
        /// 
        /// @return A new ROI object.
        public static ROI CreateEllipse(ZCT coord, double x, double y, double w, double h)
        {
            ROI an = new ROI();
            an.coord = coord;
            an.type = Type.Ellipse;
            an.Rect = new RectangleD(x, y, w, h);
            Recorder.AddLine("ROI.CreateEllipse(new ZCT(" + coord.Z + "," + coord.C + "," + coord.T + "), new RectangleD(" + x + "," + y + "," + w + "," + h + ");");
            return an;
        }
        /// > Create a new ROI object of type Polygon, with the given coordinate system and points
        /// 
        /// @param ZCT The ZCT coordinate of the ROI.
        /// @param pts an array of PointD objects, which are just a pair of doubles (x,y)
        /// 
        /// @return A ROI object
        public static ROI CreatePolygon(ZCT coord, PointD[] pts)
        {
            ROI an = new ROI();
            an.coord = coord;
            an.type = Type.Polygon;
            an.AddPoints(pts);
            an.closed = true;
            return an;
        }
        /// > Create a new ROI object of type Freeform, with the specified ZCT coordinate and points
        /// 
        /// @param ZCT A class that contains the Z, C, and T coordinates of the ROI.
        /// @param pts an array of PointD objects, which are just a pair of doubles (x,y)
        /// 
        /// @return A new ROI object.
        public static ROI CreateFreeform(ZCT coord, PointD[] pts)
        {
            ROI an = new ROI();
            an.coord = coord;
            an.type = Type.Freeform;
            an.AddPoints(pts);
            an.closed = true;
            return an;
        }

        /// This function updates the point at the specified index
        /// 
        /// @param PointD A class that contains an X and Y coordinate.
        /// @param i The index of the point to update
        public void UpdatePoint(PointD p, int i)
        {
            if (i < Points.Count)
            {
                Points[i] = p;
            }
            UpdateBoundingBox();
        }
        /// This function returns the point at the specified index
        /// 
        /// @param i The index of the point to get.
        /// 
        /// @return The point at index i in the Points array.
        public PointD GetPoint(int i)
        {
            return Points[i];
        }
        /// It returns an array of PointD objects
        /// 
        /// @return An array of PointD objects.
        public PointD[] GetPoints()
        {
            return Points.ToArray();
        }
        /// It converts a list of points to an array of points
        /// 
        /// @return A PointF array.
        public PointF[] GetPointsF()
        {
            PointF[] pfs = new PointF[Points.Count];
            for (int i = 0; i < Points.Count; i++)
            {
                pfs[i].X = (float)Points[i].X;
                pfs[i].Y = (float)Points[i].Y;
            }
            return pfs;
        }
        /// > Adds a point to the list of points and updates the bounding box
        /// 
        /// @param PointD 
        public void AddPoint(PointD p)
        {
            Points.Add(p);
            UpdateBoundingBox();
        }
        /// > Adds a range of points to the Points collection and updates the bounding box
        /// 
        /// @param p The points to add to the polygon
        public void AddPoints(PointD[] p)
        {
            Points.AddRange(p);
            UpdateBoundingBox();
        }
        /// > Adds a range of integer points to the Points collection and updates the bounding box
        /// 
        /// @param p The points to add to the polygon
        public void AddPoints(int[] xp, int[] yp)
        {
            for (int i = 0; i < xp.Length; i++)
            {
                Points.Add(new PointD(xp[i], yp[i]));
            }
            UpdateBoundingBox();
        }
        /// > Adds a range of float points to the Points collection and updates the bounding box
        /// 
        /// @param p The points to add to the polygon
        public void AddPoints(float[] xp, float[] yp)
        {
            for (int i = 0; i < xp.Length; i++)
            {
                Points.Add(new PointD(xp[i], yp[i]));
            }
            UpdateBoundingBox();
        }
        /// It removes points from a list of points based on an array of indexes
        /// 
        /// @param indexs an array of integers that represent the indexes of the points to be removed
        public void RemovePoints(int[] indexs)
        {
            List<PointD> inds = new List<PointD>();
            for (int i = 0; i < Points.Count; i++)
            {
                bool found = false;
                for (int ind = 0; ind < indexs.Length; ind++)
                {
                    if (indexs[ind] == i)
                        found = true;
                }
                if (!found)
                    inds.Add(Points[i]);
            }
            Points = inds;
            UpdateBoundingBox();
        }

        /// This function returns the number of points in the polygon
        /// 
        /// @return The number of points in the list.
        public int GetPointCount()
        {
            return Points.Count;
        }
        /// It takes a string of points and returns an array of points
        /// 
        /// @param s The string to convert to points.
        /// 
        /// @return A list of points.
        public PointD[] stringToPoints(string s)
        {
            List<PointD> pts = new List<PointD>();
            string[] ints = s.Split(' ');
            for (int i = 0; i < ints.Length; i++)
            {
                string[] sints;
                if (s.Contains("\t"))
                    sints = ints[i].Split('\t');
                else
                    sints = ints[i].Split(',');
                double x = double.Parse(sints[0], CultureInfo.InvariantCulture);
                double y = double.Parse(sints[1], CultureInfo.InvariantCulture);
                pts.Add(new PointD(x, y));
            }
            return pts.ToArray();
        }

        /// This function takes a BioImage object and returns a string of the points in the image space
        /// 
        /// @param BioImage The image that the ROI is on
        /// 
        /// @return The points of the polygon in the image space.
        public string PointsToString(BioImage b)
        {
            string pts = "";
            PointD[] ps = b.ToImageSpace(Points);
            for (int j = 0; j < ps.Length; j++)
            {
                if (j == ps.Length - 1)
                    pts += ps[j].X.ToString(CultureInfo.InvariantCulture) + "," + ps[j].Y.ToString(CultureInfo.InvariantCulture);
                else
                    pts += ps[j].X.ToString(CultureInfo.InvariantCulture) + "," + ps[j].Y.ToString(CultureInfo.InvariantCulture) + " ";
            }
            return pts;
        }
        /// It takes a list of points and returns a rectangle that contains all of the points
        /// 
        public void UpdateBoundingBox()
        {
            PointD min = new PointD(double.MaxValue, double.MaxValue);
            PointD max = new PointD(double.MinValue, double.MinValue);
            foreach (PointD p in Points)
            {
                if (min.X > p.X)
                    min.X = p.X;
                if (min.Y > p.Y)
                    min.Y = p.Y;

                if (max.X < p.X)
                    max.X = p.X;
                if (max.Y < p.Y)
                    max.Y = p.Y;
            }
            RectangleD r = new RectangleD();
            r.X = min.X;
            r.Y = min.Y;
            r.W = max.X - min.X;
            r.H = max.Y - min.Y;
            if (r.W == 0)
                r.W = 1;
            if (r.H == 0)
                r.H = 1;
            BoundingBox = r;
        }

        public override string ToString()
        {
            double w = Math.Round(W, 2, MidpointRounding.ToZero);
            double h = Math.Round(H, 2, MidpointRounding.ToZero);
            double x = Math.Round(Point.X, 2, MidpointRounding.ToZero);
            double y = Math.Round(Point.Y, 2, MidpointRounding.ToZero);
            return type.ToString() + ", " + Text + " (" + w + ", " + h + ") " + " (" + x + ", " + y + ") " + coord.ToString();
        }
    }
    /* It's a class that holds a string, an IFilter, and a Type */
    public class Filt
    {
        /* Defining an enum. */
        public enum Type
        {
            Base = 0,
            Base2 = 1,
            InPlace = 2,
            InPlace2 = 3,
            InPlacePartial = 4,
            Resize = 5,
            Rotate = 6,
            Transformation = 7,
            Copy = 8
        }
        public string name;
        public IFilter filt;
        public Type type;
        public Filt(string s, IFilter f, Type t)
        {
            name = s;
            filt = f;
            type = t;
        }
    }
    public static class Filters
    {
        public static int[,] indexs;
        /// It takes a string as an argument and returns a filter object
        /// 
        /// @param name The name of the filter.
        /// 
        /// @return The value of the key "name" in the dictionary "filters"
        public static Filt GetFilter(string name)
        {
            foreach (Filt item in filters)
            {
                if (item.name == name)
                    return item;
            }
            return null;
        }
        /// It returns a filter from the filters dictionary, using the indexs array to find the index of
        /// the filter in the dictionary
        /// 
        /// @param type The type of filter you want to get.
        /// @param index The index of the filter in the list of filters.
        /// 
        /// @return The value of the filter at the index of the type and index.
        public static Filt GetFilter(int type, int index)
        {
            return filters[indexs[type, index]];
        }
        public static List<Filt> filters = new List<Filt>();
        /// It takes an image, applies a filter to it, and returns the filtered image
        /// 
        /// @param id The id of the image to apply the filter to.
        /// @param name The name of the filter.
        /// @param inPlace If true, the image will be modified in place. If false, a new image will be
        /// created.
        /// 
        /// @return The image that was filtered.
        public static BioImage Base(string id, string name, bool inPlace)
        {
            BioImage img = Images.GetImage(id);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseFilter fi = (BaseFilter)f.filt;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    Bitmap bm = fi.Apply(img.Buffers[i]);
                    img.Buffers[i] = bm;
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.Base(" + '"' + id +
                    '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + ");");
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// It takes two images, applies a filter to the first image, and then applies the second image
        /// to the first image
        /// 
        /// @param id the image id
        /// @param id2 The image to be filtered
        /// @param name The name of the filter.
        /// @param inPlace If true, the image will be modified in place. If false, a new image will be
        /// created.
        /// 
        /// @return The image that was filtered.
        public static BioImage Base2(string id, string id2, string name, bool inPlace)
        {
            BioImage c2 = Images.GetImage(id);
            BioImage img = Images.GetImage(id2);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseFilter2 fi = (BaseFilter2)f.filt;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    fi.OverlayImage = c2.Buffers[i];
                    img.Buffers[i] = fi.Apply(img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.Base2(" + '"' + id + '"' + "," +
                   '"' + id2 + '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + ");");
                return img;
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// It takes an image, applies a filter to it, and returns the image
        /// 
        /// @param id The id of the image to apply the filter to.
        /// @param name The name of the filter
        /// @param inPlace If true, the image will be modified in place. If false, a copy of the image
        /// will be created and modified.
        /// 
        /// @return The image that was passed in.
        public static BioImage InPlace(string id, string name, bool inPlace)
        {
            BioImage img = Images.GetImage(id);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseInPlaceFilter fi = (BaseInPlaceFilter)f.filt;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    img.Buffers[i] = fi.Apply((Bitmap)img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.InPlace(" + '"' + id +
                    '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + ");");
                return img;
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// This function takes an image, applies a filter to it, and returns the filtered image
        /// 
        /// @param id the image id
        /// @param id2 the image to be filtered
        /// @param name The name of the filter
        /// @param inPlace true or false
        /// 
        /// @return The image that was passed in.
        public static BioImage InPlace2(string id, string id2, string name, bool inPlace)
        {
            BioImage c2 = Images.GetImage(id);
            BioImage img = Images.GetImage(id2);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseInPlaceFilter2 fi = (BaseInPlaceFilter2)f.filt;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    fi.OverlayImage = c2.Buffers[i];
                    img.Buffers[i] = fi.Apply(img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.InPlace2(" + '"' + id + '"' + "," +
                   '"' + id2 + '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + ");");
                return img;
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// It takes an image, applies a filter to it, and returns the filtered image
        /// 
        /// @param id the id of the image to be filtered
        /// @param name The name of the filter
        /// @param inPlace If true, the image is modified in place. If false, a copy of the image is
        /// created and modified.
        /// 
        /// @return The image that was passed in.
        public static BioImage InPlacePartial(string id, string name, bool inPlace)
        {
            BioImage img = Images.GetImage(id);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseInPlacePartialFilter fi = (BaseInPlacePartialFilter)f.filt;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    img.Buffers[i] = fi.Apply(img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.InPlacePartial(" + '"' + id +
                    '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + ");");
                return img;
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// This function takes an image, resizes it, and returns the resized image
        /// 
        /// @param id the id of the image to be resized
        /// @param name The name of the filter to use.
        /// @param inPlace If true, the image will be modified in place. If false, a new image will be
        /// created.
        /// @param w width
        /// @param h height
        /// 
        /// @return The image that was resized.
        public static BioImage Resize(string id, string name, bool inPlace, int w, int h)
        {
            BioImage img = Images.GetImage(id);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseResizeFilter fi = (BaseResizeFilter)f.filt;
                fi.NewHeight = h;
                fi.NewWidth = w;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    img.Buffers[i] = fi.Apply(img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.Resize(" + '"' + id +
                    '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + "," + w + "," + h + ");");
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// It takes an image, rotates it, and returns the rotated image
        /// 
        /// @param id the id of the image to be rotated
        /// @param name The name of the filter
        /// @param inPlace whether to apply the filter to the original image or to a copy of it
        /// @param angle the angle to rotate the image
        /// @param a alpha
        /// @param r red
        /// @param g the image to be rotated
        /// @param b blue
        /// 
        /// @return The image that was rotated.
        public static BioImage Rotate(string id, string name, bool inPlace, float angle, int a, int r, int g, int b)
        {
            BioImage img = Images.GetImage(id);
            if (!inPlace)
                img = BioImage.Copy(Images.GetImage(id));
            try
            {
                Filt f = GetFilter(name);
                BaseRotateFilter fi = (BaseRotateFilter)f.filt;
                fi.Angle = angle;
                fi.FillColor = Color.FromArgb(a, r, g, b);
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    img.Buffers[i] = fi.Apply(img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.Rotate(" + '"' + id +
                    '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + "," + angle.ToString() + "," +
                    a + "," + r + "," + g + "," + b + ");");
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;

        }
        /// This function takes an image, applies a transformation filter to it, and returns the
        /// transformed image
        /// 
        /// @param id the id of the image to be transformed
        /// @param name The name of the filter.
        /// @param inPlace If true, the image will be modified in place. If false, a new image will be
        /// created.
        /// @param angle the angle of rotation
        /// 
        /// @return The image that was transformed.
        public static BioImage Transformation(string id, string name, bool inPlace, float angle)
        {
            BioImage img = Images.GetImage(id);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseTransformationFilter fi = (BaseTransformationFilter)f.filt;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    img.Buffers[i] = fi.Apply(img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.Transformation(" + '"' + id +
                        '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + "," + angle + ");");
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// It takes an image, applies a filter to it, and returns the filtered image
        /// 
        /// @param id the id of the image to be filtered
        /// @param name The name of the filter to apply
        /// @param inPlace If true, the image will be modified in place. If false, a copy of the image
        /// will be created and the copy will be modified.
        /// 
        /// @return The image that was copied.
        public static BioImage Copy(string id, string name, bool inPlace)
        {
            BioImage img = Images.GetImage(id);
            if (!inPlace)
                img = BioImage.Copy(img);
            try
            {
                Filt f = GetFilter(name);
                BaseUsingCopyPartialFilter fi = (BaseUsingCopyPartialFilter)f.filt;
                for (int i = 0; i < img.Buffers.Count; i++)
                {
                    img.Buffers[i] = fi.Apply((Bitmap)img.Buffers[i]);
                }
                if (!inPlace)
                {
                    Images.AddImage(img, true);
                }
                Recorder.AddLine("Filters.Copy(" + '"' + id +
                        '"' + "," + '"' + name + '"' + "," + inPlace.ToString().ToLower() + ");");
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, e.Message);
                dialog.Show();
            }
            return img;
        }
        /// It takes an image, crops it, and returns the cropped image
        /// 
        /// @param id the id of the image to crop
        /// @param x x coordinate of the top left corner of the rectangle
        /// @param y y-coordinate of the top-left corner of the rectangle
        /// @param w width
        /// @param h height
        /// 
        /// @return The cropped image.
        public static BioImage Crop(string id, double x, double y, double w, double h)
        {
            BioImage c = Images.GetImage(id);
            RectangleF r = c.ToImageSpace(new RectangleD(x, y, w, h));
            Rectangle rec = new Rectangle((int)r.X, (int)r.Y, (int)Math.Abs(r.Width), (int)Math.Abs(r.Height));
            BioImage img = BioImage.Copy(c, false);
            for (int i = 0; i < img.Buffers.Count; i++)
            {
                img.Buffers[i].Crop(rec);
            }
            BioImage.AutoThreshold(img, true);
            if (img.bitsPerPixel > 8)
                img.StackThreshold(true);
            else
                img.StackThreshold(false);
            Recorder.AddLine("Filters.Crop(" + '"' + id + '"' + "," + x + "," + y + "," + w + "," + h + ");");
            //App.tabsView.AddTab(img);
            return img;
        }
        /// This function takes a string and a rectangle and returns a BioImage
        /// 
        /// @param id the id of the image to crop
        /// @param RectangleD 
        /// 
        /// @return A BioImage object.
        public static BioImage Crop(string id, RectangleD r)
        {
            return Crop(id, r.X, r.Y, r.W, r.H);
        }

        /// It creates a dictionary of filters and their names
        public static void Init()
        {
            //Base Filters
            indexs = new int[9, 32];
            indexs[0, 0] = filters.Count;
            Filt f = new Filt("AdaptiveSmoothing", new AdaptiveSmoothing(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 1] = filters.Count;
            f = new Filt("BayerFilter", new BayerFilter(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 2] = filters.Count;
            f = new Filt("BayerFilterOptimized", new BayerFilterOptimized(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 3] = filters.Count;
            f = new Filt("BayerDithering", new BayerDithering(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 4] = filters.Count;
            f = new Filt("ConnectedComponentsLabeling", new ConnectedComponentsLabeling(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 5] = filters.Count;
            f = new Filt("ExtractChannel", new ExtractChannel(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 6] = filters.Count;
            f = new Filt("ExtractNormalizedRGBChannel", new ExtractNormalizedRGBChannel(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 7] = filters.Count;
            f = new Filt("Grayscale", new Grayscale(0.2125, 0.7154, 0.0721), Filt.Type.Base);
            filters.Add(f);
            //f = new Filt("TexturedFilter", new TexturedFilter());
            //filters.Add(f);
            indexs[0, 8] = filters.Count;
            f = new Filt("WaterWave", new WaterWave(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 9] = filters.Count;
            f = new Filt("YCbCrExtractChannel", new YCbCrExtractChannel(), Filt.Type.Base);
            filters.Add(f);
            indexs[0, 10] = filters.Count;
            //BaseFilter2

            f = new Filt("ThresholdedDifference", new ThresholdedDifference(), Filt.Type.Base2);
            filters.Add(f);
            indexs[1, 0] = filters.Count;
            f = new Filt("ThresholdedEuclideanDifference", new ThresholdedEuclideanDifference(), Filt.Type.Base2);
            filters.Add(f);
            indexs[1, 1] = filters.Count;

            //BaseInPlaceFilter
            indexs[2, 0] = filters.Count;
            f = new Filt("BackwardQuadrilateralTransformation", new BackwardQuadrilateralTransformation(), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 1] = filters.Count;
            f = new Filt("BlobsFiltering", new BlobsFiltering(), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 2] = filters.Count;
            f = new Filt("BottomHat", new BottomHat(), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 3] = filters.Count;
            f = new Filt("BradleyLocalThresholding", new BradleyLocalThresholding(), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 4] = filters.Count;
            f = new Filt("CanvasCrop", new CanvasCrop(new Rectangle(0, 0, 0, 0)), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 5] = filters.Count;
            f = new Filt("CanvasFill", new CanvasFill(new Rectangle(0, 0, 0, 0)), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 6] = filters.Count;
            f = new Filt("CanvasMove", new CanvasMove(new IntPoint()), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 7] = filters.Count;
            f = new Filt("FillHoles", new FillHoles(), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 8] = filters.Count;
            f = new Filt("FlatFieldCorrection", new FlatFieldCorrection(), Filt.Type.InPlace);
            filters.Add(f);
            indexs[2, 9] = filters.Count;
            f = new Filt("TopHat", new TopHat(), Filt.Type.InPlace);
            filters.Add(f);

            //BaseInPlaceFilter2
            indexs[3, 0] = filters.Count;
            f = new Filt("Add", new Add(), Filt.Type.InPlace2);
            filters.Add(f);
            indexs[3, 1] = filters.Count;
            f = new Filt("Difference", new Difference(), Filt.Type.InPlace2);
            filters.Add(f);
            indexs[3, 2] = filters.Count;
            f = new Filt("Intersect", new Intersect(), Filt.Type.InPlace2);
            filters.Add(f);
            indexs[3, 3] = filters.Count;
            f = new Filt("Merge", new Merge(), Filt.Type.InPlace2);
            filters.Add(f);
            indexs[3, 4] = filters.Count;
            f = new Filt("Morph", new Morph(), Filt.Type.InPlace2);
            filters.Add(f);
            indexs[3, 5] = filters.Count;
            f = new Filt("MoveTowards", new MoveTowards(), Filt.Type.InPlace2);
            filters.Add(f);
            indexs[3, 6] = filters.Count;
            f = new Filt("StereoAnaglyph", new StereoAnaglyph(), Filt.Type.InPlace2);
            filters.Add(f);
            indexs[3, 7] = filters.Count;
            f = new Filt("Subtract", new Subtract(), Filt.Type.InPlace2);
            filters.Add(f);
            //f = new Filt("Add", new TexturedMerge(), Filt.Type.InPlace2);
            //filters.Add(f);

            //BaseInPlacePartialFilter
            indexs[4, 0] = filters.Count;
            f = new Filt("AdditiveNoise", new AdditiveNoise(), Filt.Type.InPlacePartial);
            filters.Add(f);
            //f = new Filt("ApplyMask", new ApplyMask(), Filt.Type.InPlacePartial2);
            //filters.Add(f);
            indexs[4, 1] = filters.Count;
            f = new Filt("BrightnessCorrection", new BrightnessCorrection(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 2] = filters.Count;
            f = new Filt("ChannelFiltering", new ChannelFiltering(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 3] = filters.Count;
            f = new Filt("ColorFiltering", new ColorFiltering(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 4] = filters.Count;
            f = new Filt("ColorRemapping", new ColorRemapping(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 5] = filters.Count;
            f = new Filt("ContrastCorrection", new ContrastCorrection(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 6] = filters.Count;
            f = new Filt("ContrastStretch", new ContrastStretch(), Filt.Type.InPlacePartial);
            filters.Add(f);

            //f = new Filt("ErrorDiffusionDithering", new ErrorDiffusionDithering(), Filt.Type.InPlacePartial);
            //filters.Add(f);
            indexs[4, 7] = filters.Count;
            f = new Filt("EuclideanColorFiltering", new EuclideanColorFiltering(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 8] = filters.Count;
            f = new Filt("GammaCorrection", new GammaCorrection(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 9] = filters.Count;
            f = new Filt("HistogramEqualization", new HistogramEqualization(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 10] = filters.Count;
            f = new Filt("HorizontalRunLengthSmoothing", new HorizontalRunLengthSmoothing(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 11] = filters.Count;
            f = new Filt("HSLFiltering", new HSLFiltering(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 12] = filters.Count;
            f = new Filt("HueModifier", new HueModifier(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 13] = filters.Count;
            f = new Filt("Invert", new Invert(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 14] = filters.Count;
            f = new Filt("LevelsLinear", new LevelsLinear(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 15] = filters.Count;
            f = new Filt("LevelsLinear16bpp", new LevelsLinear16bpp(), Filt.Type.InPlacePartial);
            filters.Add(f);
            //indexs[4, 16] = 46;
            //f = new Filt("MaskedFilter", new MaskedFilter(), Filt.Type.InPlacePartial);
            //filters.Add(f);
            //f = new Filt("Mirror", new Mirror(), Filt.Type.InPlacePartial);
            //filters.Add(f);
            indexs[4, 16] = filters.Count;
            f = new Filt("OrderedDithering", new OrderedDithering(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 17] = filters.Count;
            f = new Filt("OtsuThreshold", new OtsuThreshold(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 19] = filters.Count;
            f = new Filt("Pixellate", new Pixellate(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 20] = filters.Count;
            f = new Filt("PointedColorFloodFill", new PointedColorFloodFill(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 21] = filters.Count;
            f = new Filt("PointedMeanFloodFill", new PointedMeanFloodFill(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 22] = filters.Count;
            f = new Filt("ReplaceChannel", new Invert(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 23] = filters.Count;
            f = new Filt("RotateChannels", new LevelsLinear(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 24] = filters.Count;
            f = new Filt("SaltAndPepperNoise", new LevelsLinear16bpp(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 25] = filters.Count;
            f = new Filt("SaturationCorrection", new SaturationCorrection(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 26] = filters.Count;
            f = new Filt("Sepia", new Sepia(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 26] = filters.Count;
            f = new Filt("SimplePosterization", new SimplePosterization(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 27] = filters.Count;
            f = new Filt("SISThreshold", new SISThreshold(), Filt.Type.InPlacePartial);
            filters.Add(f);
            //f = new Filt("Texturer", new Texturer(), Filt.Type.InPlacePartial);
            //filters.Add(f);
            //f = new Filt("Threshold", new Threshold(), Filt.Type.InPlacePartial);
            //filters.Add(f);
            indexs[4, 28] = filters.Count;
            f = new Filt("ThresholdWithCarry", new ThresholdWithCarry(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 29] = filters.Count;
            f = new Filt("VerticalRunLengthSmoothing", new VerticalRunLengthSmoothing(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 30] = filters.Count;
            f = new Filt("YCbCrFiltering", new YCbCrFiltering(), Filt.Type.InPlacePartial);
            filters.Add(f);
            indexs[4, 31] = filters.Count;
            f = new Filt("YCbCrLinear", new YCbCrLinear(), Filt.Type.InPlacePartial);
            filters.Add(f);
            //f = new Filt("YCbCrReplaceChannel", new YCbCrReplaceChannel(), Filt.Type.InPlacePartial);
            //filters.Add(f);

            //BaseResizeFilter
            indexs[5, 0] = filters.Count;
            f = new Filt("ResizeBicubic", new ResizeBicubic(0, 0), Filt.Type.Resize);
            filters.Add(f);
            indexs[5, 1] = filters.Count;
            f = new Filt("ResizeBilinear", new ResizeBilinear(0, 0), Filt.Type.Resize);
            filters.Add(f);
            indexs[5, 2] = filters.Count;
            f = new Filt("ResizeNearestNeighbor", new ResizeNearestNeighbor(0, 0), Filt.Type.Resize);
            filters.Add(f);

            //BaseRotateFilter
            indexs[6, 0] = filters.Count;
            f = new Filt("RotateBicubic", new RotateBicubic(0), Filt.Type.Rotate);
            filters.Add(f);
            indexs[6, 1] = filters.Count;
            f = new Filt("RotateBilinear", new RotateBilinear(0), Filt.Type.Rotate);
            filters.Add(f);
            indexs[6, 2] = filters.Count;
            f = new Filt("RotateNearestNeighbor", new RotateNearestNeighbor(0), Filt.Type.Rotate);
            filters.Add(f);

            //Transformation
            indexs[7, 0] = filters.Count;
            f = new Filt("Crop", new Crop(new Rectangle(0, 0, 0, 0)), Filt.Type.Transformation);
            filters.Add(f);
            indexs[7, 1] = filters.Count;
            f = new Filt("QuadrilateralTransformation", new QuadrilateralTransformation(), Filt.Type.Transformation);
            filters.Add(f);
            //f = new Filt("QuadrilateralTransformationBilinear", new QuadrilateralTransformationBilinear(), Filt.Type.Transformation);
            //filters.Add(f);
            //f = new Filt("QuadrilateralTransformationNearestNeighbor", new QuadrilateralTransformationNearestNeighbor(), Filt.Type.Transformation);
            //filters.Add(f);
            indexs[7, 2] = filters.Count;
            f = new Filt("Shrink", new Shrink(), Filt.Type.Transformation);
            filters.Add(f);
            indexs[7, 3] = filters.Count;
            f = new Filt("SimpleQuadrilateralTransformation", new SimpleQuadrilateralTransformation(), Filt.Type.Transformation);
            filters.Add(f);
            indexs[7, 4] = filters.Count;
            f = new Filt("TransformFromPolar", new TransformFromPolar(), Filt.Type.Transformation);
            filters.Add(f);
            indexs[7, 5] = filters.Count;
            f = new Filt("TransformToPolar", new TransformToPolar(), Filt.Type.Transformation);
            filters.Add(f);

            //BaseUsingCopyPartialFilter
            indexs[8, 0] = filters.Count;
            f = new Filt("BinaryDilatation3x3", new BinaryDilatation3x3(), Filt.Type.Copy);
            filters.Add(f);
            indexs[8, 1] = filters.Count;
            f = new Filt("BilateralSmoothing ", new BilateralSmoothing(), Filt.Type.Copy);
            filters.Add(f);
            indexs[8, 2] = filters.Count;
            f = new Filt("BinaryErosion3x3 ", new BinaryErosion3x3(), Filt.Type.Copy);
            filters.Add(f);

        }
    }
    public class ImageInfo
    {
        bool HasPhysicalXY = false;
        bool HasPhysicalXYZ = false;
        private double physicalSizeX = -1;
        private double physicalSizeY = -1;
        private double physicalSizeZ = -1;
        public double PhysicalSizeX
        {
            get { return physicalSizeX; }
            set
            {
                physicalSizeX = value;
                HasPhysicalXY = true;
            }
        }
        public double PhysicalSizeY
        {
            get { return physicalSizeY; }
            set
            {
                physicalSizeY = value;
                HasPhysicalXY = true;
            }
        }
        public double PhysicalSizeZ
        {
            get { return physicalSizeZ; }
            set
            {
                physicalSizeZ = value;
                HasPhysicalXYZ = true;
            }
        }

        bool HasStageXY = false;
        bool HasStageXYZ = false;
        public double stageSizeX = -1;
        public double stageSizeY = -1;
        public double stageSizeZ = -1;
        public double StageSizeX
        {
            get { return stageSizeX; }
            set
            {
                stageSizeX = value;
                HasStageXY = true;
            }
        }
        public double StageSizeY
        {
            get { return stageSizeY; }
            set
            {
                stageSizeY = value;
                HasStageXY = true;
            }
        }
        public double StageSizeZ
        {
            get { return stageSizeZ; }
            set
            {
                stageSizeZ = value;
                HasStageXYZ = true;
            }
        }

        private int series = 0;
        public int Series
        {
            get { return series; }
            set { series = value; }
        }

        /// Copy() is a function that copies the values of the ImageInfo class to a new ImageInfo class
        /// 
        /// @return A copy of the ImageInfo object.
        public ImageInfo Copy()
        {
            ImageInfo inf = new ImageInfo();
            inf.PhysicalSizeX = PhysicalSizeX;
            inf.PhysicalSizeY = PhysicalSizeY;
            inf.PhysicalSizeZ = PhysicalSizeZ;
            inf.StageSizeX = StageSizeX;
            inf.StageSizeY = StageSizeY;
            inf.StageSizeZ = StageSizeZ;
            inf.HasPhysicalXY = HasPhysicalXY;
            inf.HasPhysicalXYZ = HasPhysicalXYZ;
            inf.StageSizeX = StageSizeX;
            inf.StageSizeY = StageSizeY;
            inf.StageSizeZ = StageSizeZ;
            inf.HasStageXY = HasStageXY;
            inf.HasStageXYZ = HasStageXYZ;
            return inf;
        }

    }
    public class BioImage : IDisposable
    {
        public int[,,] Coords;
        private ZCT coordinate;
        public ZCT Coordinate
        {
            get
            {
                return coordinate;
            }
            set
            {
                coordinate = value;
            }
        }

        private string id;
        public List<Channel> Channels = new List<Channel>();
        public List<Resolution> Resolutions = new List<Resolution>();
        public List<AForge.Bitmap> Buffers = new List<AForge.Bitmap>();
        public List<NetVips.Image> vipPages = new List<NetVips.Image>();
        public int Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }
        public VolumeD Volume;
        public List<ROI> Annotations = new List<ROI>();
        public string filename = "";
        public string script = "";
        public string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }
        public int[] rgbChannels = new int[3];
        public int RGBChannelCount
        {
            get
            {
                return Buffers[0].RGBChannelsCount;
            }
        }
        public int bitsPerPixel;
        public int imagesPerSeries = 0;
        public int seriesCount = 1;
        public double frameInterval = 0;
        public bool littleEndian = false;
        public bool isGroup = false;
        public long loadTimeMS = 0;
        public long loadTimeTicks = 0;
        public bool selected = false;
        private bool ispyramidal = false;
        public Statistics Statistics
        {
            get
            {
                return statistics;
            }
            set
            {
                statistics = value;
            }
        }
        private int sizeZ, sizeC, sizeT;
        private Statistics statistics;
        private int resolution = 0;
        public static float progressValue = 0;
        ImageInfo imageInfo = new ImageInfo();
        /// It copies the BioImage b and returns a new BioImage object.
        /// 
        /// @param BioImage The BioImage object to copy
        /// @param rois If true, the ROIs will be copied. If false, the ROIs will be ignored.
        public static BioImage Copy(BioImage b, bool rois)
        {
            BioImage bi = new BioImage(b.ID);
            if (rois)
                foreach (ROI an in b.Annotations)
                {
                    bi.Annotations.Add(an);
                }
            foreach (Bitmap bf in b.Buffers)
            {
                bi.Buffers.Add(bf.Copy());
            }
            foreach (Channel c in b.Channels)
            {
                bi.Channels.Add(c);
            }
            bi.Volume = b.Volume;
            bi.Coords = b.Coords;
            bi.sizeZ = b.sizeZ;
            bi.sizeC = b.sizeC;
            bi.sizeT = b.sizeT;
            bi.series = b.series;
            bi.seriesCount = b.seriesCount;
            bi.frameInterval = b.frameInterval;
            bi.littleEndian = b.littleEndian;
            bi.isGroup = b.isGroup;
            bi.imageInfo = b.imageInfo;
            bi.bitsPerPixel = b.bitsPerPixel;
            bi.file = b.file;
            bi.filename = b.filename;
            bi.Resolutions = b.Resolutions;
            bi.statistics = b.statistics;
            return bi;
        }
        /// Copy a BioImage object.
        /// 
        /// @param BioImage The image to copy
        /// 
        /// @return A copy of the BioImage object.
        public static BioImage Copy(BioImage b)
        {
            return Copy(b, true);
        }
        /// Copy the image and optionally the ROIs
        /// 
        /// @param rois Boolean value indicating whether to copy the ROIs or not.
        /// 
        /// @return A copy of the BioImage object.
        public BioImage Copy(bool rois)
        {
            return BioImage.Copy(this, rois);
        }
        /// > This function copies the current BioImage object and returns a new BioImage object
        /// 
        /// @return A copy of the BioImage object.
        public BioImage Copy()
        {
            return BioImage.Copy(this, true);
        }
        /// CopyInfo() copies the information from one BioImage to another
        /// 
        /// @param BioImage the image to copy
        /// @param copyAnnotations true
        /// @param copyChannels true
        /// 
        /// @return A new BioImage object.
        public static BioImage CopyInfo(BioImage b, bool copyAnnotations, bool copyChannels)
        {
            BioImage bi = new BioImage(b.ID);
            if (copyAnnotations)
                foreach (ROI an in b.Annotations)
                {
                    bi.Annotations.Add(an);
                }
            if (copyChannels)
                foreach (Channel c in b.Channels)
                {
                    bi.Channels.Add(c.Copy());
                }

            bi.Coords = b.Coords;
            bi.Volume = b.Volume;
            bi.sizeZ = b.sizeZ;
            bi.sizeC = b.sizeC;
            bi.sizeT = b.sizeT;
            bi.series = b.series;
            bi.seriesCount = b.seriesCount;
            bi.frameInterval = b.frameInterval;
            bi.littleEndian = b.littleEndian;
            bi.isGroup = b.isGroup;
            bi.imageInfo = b.imageInfo;
            bi.bitsPerPixel = b.bitsPerPixel;
            bi.Resolutions = b.Resolutions;
            bi.Coordinate = b.Coordinate;
            bi.file = b.file;
            bi.Filename = b.Filename;
            bi.ID = Images.GetImageName(b.file);
            bi.statistics = b.statistics;
            return bi;
        }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public int ImageCount
        {
            get
            {
                return Buffers.Count;
            }
        }
        public double PhysicalSizeX
        {
            get { return Resolutions[Resolution].PhysicalSizeX; }
        }
        public double PhysicalSizeY
        {
            get { return Resolutions[Resolution].PhysicalSizeY; }
        }
        public double PhysicalSizeZ
        {
            get { return Resolutions[Resolution].PhysicalSizeZ; }
        }
        public double StageSizeX
        {
            get
            {
                return Resolutions[Resolution].StageSizeX;
            }
            set { imageInfo.StageSizeX = value; }
        }
        public double StageSizeY
        {
            get { return Resolutions[Resolution].StageSizeX; }
            set { imageInfo.StageSizeY = value; }
        }
        public double StageSizeZ
        {
            get { return Resolutions[Resolution].StageSizeX; }
            set { imageInfo.StageSizeZ = value; }
        }

        public int series
        {
            get
            {
                return imageInfo.Series;
            }
            set
            {
                imageInfo.Series = value;
            }
        }

        static bool initialized = false;
        public Channel RChannel
        {
            get
            {
                if (Channels[0].range.Length == 1)
                    return Channels[rgbChannels[0]];
                else
                    return Channels[0];
            }
        }
        public Channel GChannel
        {
            get
            {
                if (Channels[0].range.Length == 1)
                    return Channels[rgbChannels[1]];
                else
                    return Channels[0];
            }
        }
        public Channel BChannel
        {
            get
            {
                if (Channels[0].range.Length == 1)
                    return Channels[rgbChannels[2]];
                else
                    return Channels[0];
            }
        }

        public class ImageJDesc
        {
            public string ImageJ;
            public int images = 0;
            public int channels = 0;
            public int slices = 0;
            public int frames = 0;
            public bool hyperstack;
            public string mode;
            public string unit;
            public double finterval = 0;
            public double spacing = 0;
            public bool loop;
            public double min = 0;
            public double max = 0;
            public int count;
            public bool bit8color = false;

            /// The function "FromImage" takes a BioImage object and sets the properties of an
            /// ImageJDesc object based on the properties of the BioImage object.
            /// 
            /// @param BioImage The BioImage parameter represents an object that contains information
            /// about a biological image, such as the number of images, channels, slices, and frames, as
            /// well as other properties like frame interval, physical size, and channel ranges.
            /// 
            /// @return The method is returning an instance of the ImageJDesc class.
            public ImageJDesc FromImage(BioImage b)
            {
                ImageJ = "";
                images = b.ImageCount;
                channels = b.SizeC;
                slices = b.SizeZ;
                frames = b.SizeT;
                hyperstack = true;
                mode = "grayscale";
                unit = "micron";
                finterval = b.frameInterval;
                spacing = b.PhysicalSizeZ;
                loop = false;
                /*
                double dmax = double.MinValue;
                double dmin = double.MaxValue;
                foreach (Channel c in b.Channels)
                {
                    if(dmax < c.Max)
                        dmax = c.Max;
                    if(dmin > c.Min)
                        dmin = c.Min;
                }
                min = dmin;
                max = dmax;
                */
                min = b.Channels[0].RangeR.Min;
                max = b.Channels[0].RangeR.Max;
                return this;
            }
            /// It returns a string that contains the values of all the variables in the imagej image
            /// 
            /// @return A string representation of the imagej image data.
            public string GetString()
            {
                string s = "";
                s += "ImageJ=" + ImageJ + "\n";
                s += "images=" + images + "\n";
                s += "channels=" + channels.ToString() + "\n";
                s += "slices=" + slices.ToString() + "\n";
                s += "frames=" + frames.ToString() + "\n";
                s += "hyperstack=" + hyperstack.ToString() + "\n";
                s += "mode=" + mode.ToString() + "\n";
                s += "unit=" + unit.ToString() + "\n";
                s += "finterval=" + finterval.ToString() + "\n";
                s += "spacing=" + spacing.ToString() + "\n";
                s += "loop=" + loop.ToString() + "\n";
                s += "min=" + min.ToString() + "\n";
                s += "max=" + max.ToString() + "\n";
                return s;
            }
            /// It sets the string to the value of the parameter.
            /// 
            /// @param desc The description of the string.
            public void SetString(string desc)
            {
                string[] lines = desc.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int maxlen = 20;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i < maxlen)
                    {
                        string[] sp = lines[i].Split('=');
                        if (sp[0] == "ImageJ")
                            ImageJ = sp[1];
                        if (sp[0] == "images")
                            images = int.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "channels")
                            channels = int.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "slices")
                            slices = int.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "frames")
                            frames = int.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "hyperstack")
                            hyperstack = bool.Parse(sp[1]);
                        if (sp[0] == "mode")
                            mode = sp[1];
                        if (sp[0] == "unit")
                            unit = sp[1];
                        if (sp[0] == "finterval")
                            finterval = double.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "spacing")
                            spacing = double.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "loop")
                            loop = bool.Parse(sp[1]);
                        if (sp[0] == "min")
                            min = double.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "max")
                            max = double.Parse(sp[1], CultureInfo.InvariantCulture);
                        if (sp[0] == "8bitcolor")
                            bit8color = bool.Parse(sp[1]);
                    }
                    else
                        return;
                }

            }
        }
        public int SizeX
        {
            get
            {
                if (Buffers.Count > 0)
                    return Buffers[0].SizeX;
                else return 0;
            }
        }
        public int SizeY
        {
            get
            {
                if (Buffers.Count > 0)
                    return Buffers[0].SizeY;
                else return 0;
            }
        }
        public int SizeZ
        {
            get { return sizeZ; }
        }
        public int SizeC
        {
            get { return sizeC; }
        }
        public int SizeT
        {
            get { return sizeT; }
        }
        public static bool Planes = false;
        public IntRange RRange
        {
            get
            {
                return RChannel.RangeR;
            }
        }
        public IntRange GRange
        {
            get
            {
                return GChannel.RangeG;
            }
        }
        public IntRange BRange
        {
            get
            {
                return BChannel.RangeB;
            }
        }
        public Bitmap SelectedBuffer
        {
            get
            {
                return Buffers[Coords[Coordinate.Z, Coordinate.C, Coordinate.T]];
            }
        }
        public Stopwatch watch = new Stopwatch();
        public bool isRGB
        {
            get
            {
                if (RGBChannelCount == 3 || RGBChannelCount == 4)
                    return true;
                else
                    return false;
            }
        }
        public bool isTime
        {
            get
            {
                if (SizeT > 1)
                    return true;
                else
                    return false;
            }
        }
        public bool isSeries
        {
            get
            {
                if (seriesCount > 1)
                    return true;
                else
                    return false;
            }
        }
        public bool isPyramidal
        {
            get
            {
                return ispyramidal;
            }
            set
            {
                ispyramidal = value;
            }
        }
        public string file;
        public static string status;
        public static string progFile;
        public static bool Initialized
        {
            get
            {
                return initialized;
            }
        }
        /// Converts a 16-bit image to an 8-bit image
        public void To8Bit()
        {
            if (Buffers[0].RGBChannelsCount == 4)
                To24Bit();
            PixelFormat px = Buffers[0].PixelFormat;
            if (px == PixelFormat.Format8bppIndexed)
                return;
            if (px == PixelFormat.Format48bppRgb)
            {
                To24Bit();
                List<AForge.Bitmap> bfs = new List<AForge.Bitmap>();
                int index = 0;
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Bitmap[] bs = Bitmap.RGB24To8(Buffers[i]);
                    Bitmap br = new Bitmap(ID, bs[2], new ZCT(Buffers[i].Coordinate.Z, 0, Buffers[i].Coordinate.T), index, Buffers[i].Plane);
                    Bitmap bg = new Bitmap(ID, bs[1], new ZCT(Buffers[i].Coordinate.Z, 1, Buffers[i].Coordinate.T), index + 1, Buffers[i].Plane);
                    Bitmap bb = new Bitmap(ID, bs[0], new ZCT(Buffers[i].Coordinate.Z, 2, Buffers[i].Coordinate.T), index + 2, Buffers[i].Plane);
                    for (int b = 0; b < 3; b++)
                    {
                        bs[b].Dispose();
                    }
                    bs = null;
                    GC.Collect();
                    Statistics.CalcStatistics(br);
                    Statistics.CalcStatistics(bg);
                    Statistics.CalcStatistics(bb);
                    bfs.Add(br);
                    bfs.Add(bg);
                    bfs.Add(bb);
                    index += 3;
                }
                Buffers = bfs;
                UpdateCoords(SizeZ, 3, SizeT);
            }
            else if (px == PixelFormat.Format24bppRgb)
            {
                List<Bitmap> bfs = new List<Bitmap>();
                int index = 0;
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Bitmap[] bs = Bitmap.RGB24To8(Buffers[i]);
                    Bitmap br = new Bitmap(ID, bs[2], new ZCT(Buffers[i].Coordinate.Z, 0, Buffers[i].Coordinate.T), index, Buffers[i].Plane);
                    Bitmap bg = new Bitmap(ID, bs[1], new ZCT(Buffers[i].Coordinate.Z, 1, Buffers[i].Coordinate.T), index + 1, Buffers[i].Plane);
                    Bitmap bb = new Bitmap(ID, bs[0], new ZCT(Buffers[i].Coordinate.Z, 2, Buffers[i].Coordinate.T), index + 2, Buffers[i].Plane);
                    for (int b = 0; b < 3; b++)
                    {
                        bs[b].Dispose();
                        bs[b] = null;
                    }
                    bs = null;
                    GC.Collect();
                    Statistics.CalcStatistics(br);
                    Statistics.CalcStatistics(bg);
                    Statistics.CalcStatistics(bb);
                    bfs.Add(br);
                    bfs.Add(bg);
                    bfs.Add(bb);
                    index += 3;
                }
                Buffers = bfs;
                UpdateCoords(SizeZ, 3, SizeT);
            }
            else
            {
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Bitmap b = AForge.Imaging.Image.Convert16bppTo8bpp(Buffers[i]);
                    Buffers[i] = b;
                    Statistics.CalcStatistics(Buffers[i]);
                }
                for (int c = 0; c < Channels.Count; c++)
                {
                    Channels[c].BitsPerPixel = 8;
                    for (int i = 0; i < Channels[c].range.Length; i++)
                    {
                        Channels[c].range[i].Min = (int)(((float)Channels[c].range[i].Min / (float)ushort.MaxValue) * byte.MaxValue);
                        Channels[c].range[i].Max = (int)(((float)Channels[c].range[i].Max / (float)ushort.MaxValue) * byte.MaxValue);
                    }
                }

            }
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(100);
            } while (Buffers[Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            AutoThreshold(this, false);
            bitsPerPixel = 8;
            App.viewer.UpdateImages();
            App.viewer.UpdateView();
            Recorder.AddLine("Bio.Table.GetImage(" + '"' + ID + '"' + ")" + "." + "To8Bit();");
        }
        /// Converts the image to 16 bit.
        public void To16Bit()
        {
            if (Buffers[0].RGBChannelsCount == 4)
                To24Bit();
            if (Buffers[0].PixelFormat == PixelFormat.Format16bppGrayScale)
                return;
            bitsPerPixel = 16;
            if (Buffers[0].PixelFormat == PixelFormat.Format48bppRgb)
            {
                List<Bitmap> bfs = new List<Bitmap>();
                int index = 0;
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Array.Reverse(Buffers[i].Bytes);
                    Bitmap[] bs = Bitmap.RGB48To16(ID, SizeX, SizeY, Buffers[i].Stride, Buffers[i].Bytes, Buffers[i].Coordinate, index, Buffers[i].Plane);
                    bfs.AddRange(bs);
                    index += 3;
                }
                Buffers = bfs;
                UpdateCoords(SizeZ, SizeC * 3, SizeT);
                if (Channels[0].SamplesPerPixel == 3)
                {
                    Channel c = Channels[0].Copy();
                    c.SamplesPerPixel = 1;
                    c.range = new IntRange[1];
                    Channels.Clear();
                    Channels.Add(c);
                    Channels.Add(c.Copy());
                    Channels.Add(c.Copy());
                    Channels[1].Index = 1;
                    Channels[2].Index = 2;
                }
            }
            else if (Buffers[0].PixelFormat == PixelFormat.Format8bppIndexed)
            {
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Buffers[i].Image = AForge.Imaging.Image.Convert8bppTo16bpp(Buffers[i]);
                    Statistics.CalcStatistics(Buffers[i]);
                }
                for (int c = 0; c < Channels.Count; c++)
                {
                    for (int i = 0; i < Channels[c].range.Length; i++)
                    {
                        Channels[c].range[i].Min = (int)(((float)Channels[c].range[i].Min / (float)byte.MaxValue) * ushort.MaxValue);
                        Channels[c].range[i].Max = (int)(((float)Channels[c].range[i].Max / (float)byte.MaxValue) * ushort.MaxValue);
                    }
                    Channels[c].BitsPerPixel = 16;
                }
            }
            else if (Buffers[0].PixelFormat == PixelFormat.Format24bppRgb)
            {
                List<Bitmap> bfs = new List<Bitmap>();
                int index = 0;
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Bitmap[] bs = Bitmap.RGB24To8(Buffers[i]);
                    Bitmap br = new Bitmap(ID, bs[2].Image, new ZCT(Buffers[i].Coordinate.Z, 0, Buffers[i].Coordinate.T), index, Buffers[i].Plane);
                    Bitmap bg = new Bitmap(ID, bs[1].Image, new ZCT(Buffers[i].Coordinate.Z, 1, Buffers[i].Coordinate.T), index + 1, Buffers[i].Plane);
                    Bitmap bb = new Bitmap(ID, bs[0].Image, new ZCT(Buffers[i].Coordinate.Z, 2, Buffers[i].Coordinate.T), index + 2, Buffers[i].Plane);
                    for (int b = 0; b < 3; b++)
                    {
                        bs[b].Dispose();
                        bs[b] = null;
                    }
                    bs = null;
                    GC.Collect();
                    br.To16Bit();
                    bg.To16Bit();
                    bb.To16Bit();
                    Statistics.CalcStatistics(br);
                    Statistics.CalcStatistics(bg);
                    Statistics.CalcStatistics(bb);
                    bfs.Add(br);
                    bfs.Add(bg);
                    bfs.Add(bb);
                    index += 3;
                }
                Buffers = bfs;
                UpdateCoords(SizeZ, 3, SizeT);
                for (int c = 0; c < Channels.Count; c++)
                {
                    for (int i = 0; i < Channels[c].range.Length; i++)
                    {
                        Channels[c].range[i].Min = (int)(((float)Channels[c].range[i].Min / (float)byte.MaxValue) * ushort.MaxValue);
                        Channels[c].range[i].Max = (int)(((float)Channels[c].range[i].Max / (float)byte.MaxValue) * ushort.MaxValue);
                    }
                    Channels[c].BitsPerPixel = 16;
                }
            }
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(100);
            } while (Buffers[Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            AutoThreshold(this, false);
            StackThreshold(true);
            App.viewer.UpdateImages();
            App.viewer.UpdateView();
            Recorder.AddLine("Bio.Images.GetImage(" + '"' + ID + '"' + ")" + "." + "To16Bit();");
        }
        /// Converts the image to 24 bit.
        public void To24Bit()
        {
            if (Buffers[0].PixelFormat == PixelFormat.Format24bppRgb)
                return;
            bitsPerPixel = 8;
            if (Buffers[0].PixelFormat == PixelFormat.Format32bppArgb || Buffers[0].PixelFormat == PixelFormat.Format32bppRgb)
            {
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Buffers[i] = Bitmap.To24Bit(Buffers[i]);
                    Buffers[i].SwitchRedBlue();
                }
                if (Channels.Count == 4)
                {
                    Channels.RemoveAt(0);
                }
                else
                {
                    Channels[0].SamplesPerPixel = 3;
                }
            }
            else
            if (Buffers[0].PixelFormat == PixelFormat.Format48bppRgb)
            {
                //We run 8bit so we get 24 bit rgb.
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Buffers[i].Image = AForge.Imaging.Image.Convert16bppTo8bpp(Buffers[i]);
                }
            }
            else
            if (Buffers[0].PixelFormat == PixelFormat.Format16bppGrayScale || Buffers[0].PixelFormat == PixelFormat.Format8bppIndexed)
            {
                if (Buffers[0].PixelFormat == PixelFormat.Format16bppGrayScale)
                {
                    for (int i = 0; i < Buffers.Count; i++)
                    {
                        Buffers[i].Image = AForge.Imaging.Image.Convert16bppTo8bpp(Buffers[i]);
                    }
                    for (int c = 0; c < Channels.Count; c++)
                    {
                        for (int i = 0; i < Channels[c].range.Length; i++)
                        {
                            Channels[c].range[i].Min = (int)(((float)Channels[c].range[i].Min / (float)ushort.MaxValue) * byte.MaxValue);
                            Channels[c].range[i].Max = (int)(((float)Channels[c].range[i].Max / (float)ushort.MaxValue) * byte.MaxValue);
                        }
                        Channels[c].BitsPerPixel = 8;
                    }
                }
                List<Bitmap> bfs = new List<Bitmap>();
                if (Buffers.Count % 3 != 0 && Buffers.Count % 2 != 0)
                    for (int i = 0; i < Buffers.Count; i++)
                    {
                        Bitmap bs = new Bitmap(ID, SizeX, SizeY, Buffers[i].PixelFormat, Buffers[i].Bytes, new ZCT(Buffers[i].Coordinate.Z, 0, Buffers[i].Coordinate.T), i, Buffers[i].Plane);
                        Bitmap bbs = Bitmap.RGB16To48(bs);
                        bs.Dispose();
                        bs = null;
                        bfs.Add(bbs);
                    }
                else
                    for (int i = 0; i < Buffers.Count; i += Channels.Count)
                    {
                        Bitmap[] bs = new Bitmap[3];
                        bs[2] = new Bitmap(ID, SizeX, SizeY, Buffers[i].PixelFormat, Buffers[i].Bytes, new ZCT(Buffers[i].Coordinate.Z, 0, Buffers[i].Coordinate.T), i, Buffers[i].Plane);
                        bs[1] = new Bitmap(ID, SizeX, SizeY, Buffers[i + 1].PixelFormat, Buffers[i + 1].Bytes, new ZCT(Buffers[i + 1].Coordinate.Z, 0, Buffers[i + 1].Coordinate.T), i + 1, Buffers[i + 1].Plane);
                        if (Channels.Count > 2)
                            bs[0] = new Bitmap(ID, SizeX, SizeY, Buffers[i + 2].PixelFormat, Buffers[i + 2].Bytes, new ZCT(Buffers[i + 2].Coordinate.Z, 0, Buffers[i + 2].Coordinate.T), i + 2, Buffers[i + 2].Plane);
                        Bitmap bbs = Bitmap.RGB8To24(bs);
                        for (int b = 0; b < 3; b++)
                        {
                            if (bs[b] != null)
                                bs[b].Dispose();
                            bs[b] = null;
                        }
                        bfs.Add(bbs);
                    }
                Buffers = bfs;
                UpdateCoords(SizeZ, 1, SizeT);
            }
            Recorder.AddLine("Bio.Images.GetImage(" + '"' + ID + '"' + ")" + "." + "To24Bit();");
            AutoThreshold(this, true);
            StackThreshold(false);
            App.viewer.UpdateImages();
            App.viewer.UpdateView();
        }
        /// Converts the image to 32 bit.
        public void To32Bit()
        {
            if (Buffers[0].PixelFormat == PixelFormat.Format32bppArgb)
                return;
            if (Buffers[0].PixelFormat != PixelFormat.Format24bppRgb)
            {
                To24Bit();
            }
            for (int i = 0; i < Buffers.Count; i++)
            {
                UnmanagedImage b = Bitmap.To32Bit(Buffers[i]);
                Buffers[i].Image = b;
            }
            AutoThreshold(this, true);
            App.viewer.UpdateImages();
            App.viewer.UpdateView();
            Recorder.AddLine("Bio.Images.GetImage(" + '"' + ID + '"' + ")" + "." + "To32Bit();");
        }
        /// It converts a 16 bit image to a 48 bit image
        /// 
        /// @return A list of Bitmaps.
        public void To48Bit()
        {
            if (Buffers[0].RGBChannelsCount == 4)
                To24Bit();
            if (Buffers[0].PixelFormat == PixelFormat.Format48bppRgb)
                return;
            if (Buffers[0].PixelFormat == PixelFormat.Format8bppIndexed || Buffers[0].PixelFormat == PixelFormat.Format16bppGrayScale)
            {
                if (Buffers[0].PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    for (int i = 0; i < Buffers.Count; i++)
                    {
                        Buffers[i].Image = AForge.Imaging.Image.Convert8bppTo16bpp(Buffers[i]);
                    }
                }
                List<Bitmap> bfs = new List<Bitmap>();
                if (Buffers.Count % 3 != 0 && Buffers.Count % 2 != 0)
                    for (int i = 0; i < Buffers.Count; i++)
                    {
                        Bitmap bs = new Bitmap(ID, SizeX, SizeY, Buffers[i].PixelFormat, Buffers[i].Bytes, new ZCT(Buffers[i].Coordinate.Z, 0, Buffers[i].Coordinate.T), i, Buffers[i].Plane);
                        Bitmap bbs = Bitmap.RGB16To48(bs);
                        Statistics.CalcStatistics(bbs);
                        bs.Dispose();
                        bs = null;
                        bfs.Add(bbs);
                    }
                else
                    for (int i = 0; i < Buffers.Count; i += Channels.Count)
                    {
                        Bitmap[] bs = new Bitmap[3];
                        bs[0] = new Bitmap(ID, SizeX, SizeY, Buffers[i + 2].PixelFormat, Buffers[i + 2].Bytes, new ZCT(Buffers[i + 2].Coordinate.Z, 0, Buffers[i + 2].Coordinate.T), i + 2, Buffers[i + 2].Plane);
                        bs[1] = new Bitmap(ID, SizeX, SizeY, Buffers[i + 1].PixelFormat, Buffers[i + 1].Bytes, new ZCT(Buffers[i + 1].Coordinate.Z, 0, Buffers[i + 1].Coordinate.T), i + 1, Buffers[i + 1].Plane);
                        if (Channels.Count > 2)
                            bs[2] = new Bitmap(ID, SizeX, SizeY, Buffers[i].PixelFormat, Buffers[i].Bytes, new ZCT(Buffers[i].Coordinate.Z, 0, Buffers[i].Coordinate.T), i, Buffers[i].Plane);
                        Bitmap bbs = Bitmap.RGB16To48(bs);
                        for (int b = 0; b < 3; b++)
                        {
                            if (bs[b] != null)
                                bs[b].Dispose();
                            bs[b] = null;
                        }
                        Statistics.CalcStatistics(bbs);
                        bfs.Add(bbs);
                    }
                GC.Collect();
                Buffers = bfs;
                UpdateCoords(SizeZ, 1, SizeT);
                Channel c = Channels[0].Copy();
                c.SamplesPerPixel = 3;
                rgbChannels[0] = 0;
                rgbChannels[1] = 0;
                rgbChannels[2] = 0;
                Channels.Clear();
                Channels.Add(c);
            }
            else
            if (Buffers[0].PixelFormat == PixelFormat.Format24bppRgb)
            {
                for (int i = 0; i < Buffers.Count; i++)
                {
                    Buffers[i].Image = AForge.Imaging.Image.Convert8bppTo16bpp(Buffers[i]);
                    Statistics.CalcStatistics(Buffers[i]);
                }
                for (int c = 0; c < Channels.Count; c++)
                {
                    for (int i = 0; i < Channels[c].range.Length; i++)
                    {
                        Channels[c].range[i].Min = (int)(((float)Channels[c].range[i].Min / (float)byte.MaxValue) * ushort.MaxValue);
                        Channels[c].range[i].Max = (int)(((float)Channels[c].range[i].Max / (float)byte.MaxValue) * ushort.MaxValue);
                    }
                    Channels[c].BitsPerPixel = 16;
                }
            }
            else
            {
                int index = 0;
                List<Bitmap> buffers = new List<Bitmap>();
                for (int i = 0; i < Buffers.Count; i += 3)
                {
                    Bitmap[] bf = new Bitmap[3];
                    bf[0] = Buffers[i];
                    bf[1] = Buffers[i + 1];
                    bf[2] = Buffers[i + 2];
                    Bitmap inf = Bitmap.RGB16To48(bf);
                    buffers.Add(inf);
                    Statistics.CalcStatistics(inf);
                    for (int b = 0; b < 3; b++)
                    {
                        bf[b].Dispose();
                    }
                    index++;
                }
                Buffers = buffers;
                UpdateCoords(SizeZ, 1, SizeT);
            }
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(50);
            } while (Buffers[Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            bitsPerPixel = 16;
            AutoThreshold(this, false);
            StackThreshold(true);
            App.viewer.UpdateImages();
            App.viewer.UpdateView();
            Recorder.AddLine("Bio.Images.GetImage(" + '"' + ID + '"' + ")" + "." + "To48Bit();");
        }
        /// Rotates the image by specified degrees or flips it.
        /// 
        /// @param rot The type of rotation to perform.
        public void RotateFlip(AForge.RotateFlipType rot)
        {
            for (int i = 0; i < Buffers.Count; i++)
            {
                Buffers[i].RotateFlip(rot);
            }
            Volume = new VolumeD(new Point3D(StageSizeX, StageSizeY, StageSizeZ), new Point3D(PhysicalSizeX * SizeX, PhysicalSizeY * SizeY, PhysicalSizeZ * SizeZ));
        }
        /// Bake(int rmin, int rmax, int gmin, int gmax, int bmin, int bmax)
        /// 
        /// @param rmin The minimum value of the red channel.
        /// @param rmax The maximum value of the red channel.
        /// @param gmin The minimum value of the green channel.
        /// @param gmax The maximum value of the green channel.
        /// @param bmin The minimum value of the blue channel.
        /// @param bmax The maximum value of the blue channel.
        public void Bake(int rmin, int rmax, int gmin, int gmax, int bmin, int bmax)
        {
            Bake(new IntRange(rmin, rmax), new IntRange(gmin, gmax), new IntRange(bmin, bmax));
        }
        /// It takes a range of values for each channel, and creates a new image with the filtered
        /// values
        /// 
        /// @param IntRange 
        /// @param IntRange 
        /// @param IntRange 
        public void Bake(IntRange rf, IntRange gf, IntRange bf)
        {
            BioImage bm = new BioImage(Images.GetImageName(ID));
            bm = CopyInfo(this, true, true);
            for (int i = 0; i < Buffers.Count; i++)
            {
                ZCT co = Buffers[i].Coordinate;
                UnmanagedImage b = GetFiltered(i, rf, gf, bf);
                Bitmap inf = new Bitmap(bm.ID, b, co, i);
                Statistics.CalcStatistics(inf);
                bm.Coords[co.Z, co.C, co.T] = i;
                bm.Buffers.Add(inf);
            }
            foreach (Channel item in bm.Channels)
            {
                for (int i = 0; i < item.range.Length; i++)
                {
                    item.range[i].Min = 0;
                    if (bm.bitsPerPixel > 8)
                        item.range[i].Max = ushort.MaxValue;
                    else
                        item.range[i].Max = 255;
                }
            }
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(50);
            } while (Buffers[Buffers.Count - 1].Stats == null);
            AutoThreshold(bm, false);
            Statistics.ClearCalcBuffer();
            Images.AddImage(bm, true);
            App.tabsView.AddTab(bm);
            Recorder.AddLine("ImageView.SelectedImage.Bake(" + rf.Min + "," + rf.Max + "," + gf.Min + "," + gf.Max + "," + bf.Min + "," + bf.Max + ");");
        }
        /// It takes a list of images and assigns them to a 3D array of coordinates
        public void UpdateCoords()
        {
            int z = 0;
            int c = 0;
            int t = 0;
            Coords = new int[SizeZ, SizeC, SizeT];
            for (int im = 0; im < Buffers.Count; im++)
            {
                ZCT co = new ZCT(z, c, t);
                Coords[co.Z, co.C, co.T] = im;
                Buffers[im].Coordinate = co;
                if (c < SizeC - 1)
                    c++;
                else
                {
                    c = 0;
                    if (z < SizeZ - 1)
                        z++;
                    else
                    {
                        z = 0;
                        if (t < SizeT - 1)
                            t++;
                        else
                            t = 0;
                    }
                }
            }
        }
        /// It takes the number of Z, C, and T planes in the image and then assigns each image buffer a
        /// coordinate in the ZCT space
        /// 
        /// @param sz size of the Z dimension
        /// @param sc number of channels
        /// @param st number of time points
        public void UpdateCoords(int sz, int sc, int st)
        {
            int z = 0;
            int c = 0;
            int t = 0;
            sizeZ = sz;
            sizeC = sc;
            sizeT = st;
            Coords = new int[sz, sc, st];
            for (int im = 0; im < Buffers.Count; im++)
            {
                ZCT co = new ZCT(z, c, t);
                Coords[co.Z, co.C, co.T] = im;
                Buffers[im].Coordinate = co;
                if (c < SizeC - 1)
                    c++;
                else
                {
                    c = 0;
                    if (z < SizeZ - 1)
                        z++;
                    else
                    {
                        z = 0;
                        if (t < SizeT - 1)
                            t++;
                        else
                            t = 0;
                    }
                }
            }
        }
        /// It takes a list of images and assigns them to a 3D array of coordinates
        /// 
        /// @param sz size of the Z dimension
        /// @param sc number of channels
        /// @param st number of time points
        /// @param order XYCZT or XYZCT
        public void UpdateCoords(int sz, int sc, int st, string order)
        {
            int z = 0;
            int c = 0;
            int t = 0;
            sizeZ = sz;
            sizeC = sc;
            sizeT = st;
            Coords = new int[sz, sc, st];
            if (order == "XYCZT")
            {
                for (int im = 0; im < Buffers.Count; im++)
                {
                    ZCT co = new ZCT(z, c, t);
                    Coords[co.Z, co.C, co.T] = im;
                    Buffers[im].Coordinate = co;
                    if (c < SizeC - 1)
                        c++;
                    else
                    {
                        c = 0;
                        if (z < SizeZ - 1)
                            z++;
                        else
                        {
                            z = 0;
                            if (t < SizeT - 1)
                                t++;
                            else
                                t = 0;
                        }
                    }
                }
            }
            else if (order == "XYZCT")
            {
                for (int im = 0; im < Buffers.Count; im++)
                {
                    ZCT co = new ZCT(z, c, t);
                    Coords[co.Z, co.C, co.T] = im;
                    Buffers[im].Coordinate = co;
                    if (z < SizeZ - 1)
                        z++;
                    else
                    {
                        z = 0;
                        if (c < SizeC - 1)
                            c++;
                        else
                        {
                            c = 0;
                            if (t < SizeT - 1)
                                t++;
                            else
                                t = 0;
                        }
                    }
                }
            }
        }
        /// Convert a physical size to an image size
        /// 
        /// @param d the distance in microns
        /// 
        /// @return The value of d divided by the physicalSizeX.
        public double ToImageSizeX(double d)
        {
            return d / PhysicalSizeX;
        }
        /// Convert a physical size in Y direction to an image size in Y direction
        /// 
        /// @param d the distance in microns
        /// 
        /// @return The return value is the value of the parameter d divided by the value of the
        /// physicalSizeY field.
        public double ToImageSizeY(double d)
        {
            return d / PhysicalSizeY;
        }
        /// > Convert a stage coordinate to an image coordinate
        /// 
        /// @param x the x coordinate of the point in the image
        /// 
        /// @return The return value is a double.
        public double ToImageSpaceX(double x)
        {
            if (isPyramidal)
                return x;
            return (float)((x - StageSizeX) / PhysicalSizeX);
        }
        /// > Convert a Y coordinate from stage space to image space
        /// 
        /// @param y the y coordinate of the point in the image
        /// 
        /// @return The return value is the y-coordinate of the image.
        public double ToImageSpaceY(double y)
        {
            if (isPyramidal)
                return y;
            return (float)((y - StageSizeY) / PhysicalSizeY);
        }
        /// Convert a point in the stage coordinate system to a point in the image coordinate system
        /// 
        /// @param PointD 
        /// 
        /// @return A PointF object.
        public PointD ToImageSpace(PointD p)
        {
            PointD pp = new PointD();
            pp.X = (float)((p.X - StageSizeX) / PhysicalSizeX);
            pp.Y = (float)((p.Y - StageSizeY) / PhysicalSizeY);
            return pp;
        }
        /// Convert a list of points from stage space to image space
        /// 
        /// @param p List of points in stage space
        /// 
        /// @return A PointD array.
        public PointD[] ToImageSpace(List<PointD> p)
        {
            PointD[] ps = new PointD[p.Count];
            for (int i = 0; i < p.Count; i++)
            {
                PointD pp = new PointD();
                pp.X = ((p[i].X - StageSizeX) / PhysicalSizeX);
                pp.Y = ((p[i].Y - StageSizeY) / PhysicalSizeY);
                ps[i] = pp;
            }
            return ps;
        }
        /// > The function takes a list of points in the stage coordinate system and returns a list of
        /// points in the image coordinate system
        /// 
        /// @param p the points to be converted
        /// 
        /// @return A PointF[]
        public PointF[] ToImageSpace(PointF[] p)
        {
            PointF[] ps = new PointF[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                PointF pp = new PointF();
                pp.X = (float)((p[i].X - StageSizeX) / PhysicalSizeX);
                pp.Y = (float)((p[i].Y - StageSizeY) / PhysicalSizeY);
                ps[i] = pp;
            }
            return ps;
        }
        /// > Convert a rectangle in physical space to a rectangle in image space
        /// 
        /// @param RectangleD 
        /// 
        /// @return A RectangleF object.
        public RectangleF ToImageSpace(RectangleD p)
        {
            RectangleF r = new RectangleF();
            Point pp = new Point();
            r.X = (int)((p.X - StageSizeX) / PhysicalSizeX);
            r.Y = (int)((p.Y - StageSizeY) / PhysicalSizeY);
            r.Width = (int)(p.W / PhysicalSizeX);
            r.Height = (int)(p.H / PhysicalSizeY);
            return r;
        }
        /// > This function converts a point in the image space to a point in the stage space
        /// 
        /// @param PointD A class that contains an X and Y coordinate.
        /// 
        /// @return A PointD object.
        public PointD ToStageSpace(PointD p)
        {
            PointD pp = new PointD();
            if (isPyramidal)
            {
                pp.X = ((p.X * Resolutions[resolution].PhysicalSizeX) + Volume.Location.X);
                pp.Y = ((p.Y * Resolutions[resolution].PhysicalSizeY) + Volume.Location.Y);
                return pp;
            }
            else
            {
                pp.X = ((p.X * PhysicalSizeX) + Volume.Location.X);
                pp.Y = ((p.Y * PhysicalSizeY) + Volume.Location.Y);
                return pp;
            }
        }
        /// Convert a point in the image space to a point in the stage space
        /// 
        /// @param PointD A point in the image space
        /// @param resolution the resolution of the image (0, 1, 2, 3, 4)
        /// 
        /// @return A PointD object.
        public PointD ToStageSpace(PointD p, int resolution)
        {
            PointD pp = new PointD();
            pp.X = ((p.X * Resolutions[resolution].PhysicalSizeX) + Volume.Location.X);
            pp.Y = ((p.Y * Resolutions[resolution].PhysicalSizeY) + Volume.Location.Y);
            return pp;
        }
        /// > The function takes a point in the volume space and converts it to a point in the stage
        /// space
        /// 
        /// @param PointD A custom class that holds an X and Y coordinate.
        /// @param physicalSizeX The width of the stage in mm
        /// @param physicalSizeY The height of the stage in mm
        /// @param volumeX The X coordinate of the top left corner of the volume in stage space.
        /// @param volumeY The Y position of the top left corner of the volume in stage space.
        /// 
        /// @return A PointD object.
        public static PointD ToStageSpace(PointD p, double physicalSizeX, double physicalSizeY, double volumeX, double volumeY)
        {
            PointD pp = new PointD();
            pp.X = ((p.X * physicalSizeX) + volumeX);
            pp.Y = ((p.Y * physicalSizeY) + volumeY);
            return pp;
        }
        /// > Convert a rectangle from the coordinate space of the image to the coordinate space of the
        /// stage
        /// 
        /// @param RectangleD A rectangle with double precision coordinates.
        /// 
        /// @return A RectangleD object.
        public RectangleD ToStageSpace(RectangleD p)
        {
            RectangleD r = new RectangleD();
            r.X = ((p.X * PhysicalSizeX) + Volume.Location.X);
            r.Y = ((p.Y * PhysicalSizeY) + Volume.Location.Y);
            r.W = (p.W * PhysicalSizeX);
            r.H = (p.H * PhysicalSizeY);
            return r;
        }
        /// > This function takes a rectangle in the coordinate space of the image and converts it to
        /// the coordinate space of the stage
        /// 
        /// @param RectangleD A rectangle with double precision.
        /// @param physicalSizeX The width of the physical screen in pixels
        /// @param physicalSizeY The height of the stage in pixels
        /// @param volumeX The X position of the volume in stage space.
        /// @param volumeY The Y position of the top of the volume in stage space.
        /// 
        /// @return A RectangleD object.
        public static RectangleD ToStageSpace(RectangleD p, double physicalSizeX, double physicalSizeY, double volumeX, double volumeY)
        {
            RectangleD r = new RectangleD();
            r.X = ((p.X * physicalSizeX) + volumeX);
            r.Y = ((p.Y * physicalSizeY) + volumeY);
            r.W = (p.W * physicalSizeX);
            r.H = (p.H * physicalSizeY);
            return r;
        }
        /// > This function takes a list of points in the coordinate system of the image and returns a
        /// list of points in the coordinate system of the stage
        /// 
        /// @param p The array of points to convert
        /// 
        /// @return A PointD[] array.
        public PointD[] ToStageSpace(PointD[] p)
        {
            PointD[] ps = new PointD[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                PointD pp = new PointD();
                pp.X = ((p[i].X * PhysicalSizeX) + Volume.Location.X);
                pp.Y = ((p[i].Y * PhysicalSizeY) + Volume.Location.Y);
                ps[i] = pp;
            }
            return ps;
        }
        /// It takes a list of points, and converts them from a coordinate system where the origin is in
        /// the center of the image, to a coordinate system where the origin is in the top left corner
        /// of the image
        /// 
        /// @param p the array of points to convert
        /// @param physicalSizeX The width of the image in microns
        /// @param physicalSizeY The height of the image in microns
        /// @param volumeX The X position of the volume in stage space.
        /// @param volumeY The Y position of the top left corner of the volume in stage space.
        /// 
        /// @return A PointD array.
        public static PointD[] ToStageSpace(PointD[] p, double physicalSizeX, double physicalSizeY, double volumeX, double volumeY)
        {
            PointD[] ps = new PointD[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                PointD pp = new PointD();
                pp.X = ((p[i].X * physicalSizeX) + volumeX);
                pp.Y = ((p[i].Y * physicalSizeY) + volumeY);
                ps[i] = pp;
            }
            return ps;
        }
        /* Creating a new BioImage object. */
        public BioImage(string file)
        {
            id = file;
            this.file = file;
            filename = Images.GetImageName(id);
            Coordinate = new ZCT();
            rgbChannels[0] = 0;
            rgbChannels[1] = 0;
            rgbChannels[2] = 0;
        }
        /// It takes a BioImage object, and returns a new BioImage object that is a subset of the
        /// original
        /// 
        /// @param BioImage the image to be processed
        /// @param ser series number
        /// @param zs starting z-plane
        /// @param ze end of z-stack
        /// @param cs channel start
        /// @param ce channel end
        /// @param ts time start
        /// @param te time end
        /// 
        /// @return A new BioImage object.
        public static BioImage Substack(BioImage orig, int ser, int zs, int ze, int cs, int ce, int ts, int te)
        {
            BioImage b = CopyInfo(orig, false, false);
            b.ID = Images.GetImageName(orig.ID);
            int i = 0;
            b.Coords = new int[ze - zs, ce - cs, te - ts];
            b.sizeZ = ze - zs;
            b.sizeC = ce - cs;
            b.sizeT = te - ts;
            for (int ti = 0; ti < b.SizeT; ti++)
            {
                for (int zi = 0; zi < b.SizeZ; zi++)
                {
                    for (int ci = 0; ci < b.SizeC; ci++)
                    {
                        int ind = orig.Coords[zs + zi, cs + ci, ts + ti];
                        Bitmap bf = new Bitmap(Images.GetImageName(orig.id), orig.SizeX, orig.SizeY, orig.Buffers[0].PixelFormat, orig.Buffers[ind].Bytes, new ZCT(zi, ci, ti), i);
                        Statistics.CalcStatistics(bf);
                        b.Buffers.Add(bf);
                        b.Coords[zi, ci, ti] = i;
                        i++;
                    }
                }
            }
            for (int ci = cs; ci < ce; ci++)
            {
                b.Channels.Add(orig.Channels[ci]);
            }
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(100);
            } while (b.Buffers[b.Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            b.Resolutions.Add(new Resolution(b.Buffers[0].SizeX, b.Buffers[0].SizeY, b.Buffers[0].PixelFormat, b.PhysicalSizeX, b.PhysicalSizeY, b.PhysicalSizeZ, b.StageSizeX, b.StageSizeY, b.StageSizeZ));
            AutoThreshold(b, false);
            if (b.bitsPerPixel > 8)
                b.StackThreshold(true);
            else
                b.StackThreshold(false);
            Images.AddImage(b, true);
            Recorder.AddLine("Bio.BioImage.Substack(" + '"' + orig.Filename + '"' + "," + ser + "," + zs + "," + ze + "," + cs + "," + ce + "," + ts + "," + te + ");");
            return b;
        }
        /// This function takes two images and merges them together
        /// 
        /// @param BioImage The image to be merged
        /// @param BioImage The image to be merged
        /// 
        /// @return A new BioImage object.
        public static BioImage MergeChannels(BioImage b2, BioImage b)
        {
            BioImage res = new BioImage(b2.ID);
            res.ID = Images.GetImageName(b2.ID);
            res.series = b2.series;
            res.sizeZ = b2.SizeZ;
            int cOrig = b2.SizeC;
            res.sizeC = b2.SizeC + b.SizeC;
            res.sizeT = b2.SizeT;
            res.bitsPerPixel = b2.bitsPerPixel;
            res.imageInfo = b2.imageInfo;
            res.littleEndian = b2.littleEndian;
            res.seriesCount = b2.seriesCount;
            res.imagesPerSeries = res.ImageCount / res.seriesCount;
            res.Coords = new int[res.SizeZ, res.SizeC, res.SizeT];

            int i = 0;
            int cc = 0;
            for (int ti = 0; ti < res.SizeT; ti++)
            {
                for (int zi = 0; zi < res.SizeZ; zi++)
                {
                    for (int ci = 0; ci < res.SizeC; ci++)
                    {
                        ZCT co = new ZCT(zi, ci, ti);
                        if (ci < cOrig)
                        {
                            //If this channel is part of the image b1 we add planes from it.
                            Bitmap copy = new Bitmap(b2.id, b2.SizeX, b2.SizeY, b2.Buffers[0].PixelFormat, b2.Buffers[i].Bytes, co, i);
                            if (b2.littleEndian)
                                copy.RotateFlip(AForge.RotateFlipType.Rotate180FlipNone);
                            res.Coords[zi, ci, ti] = i;
                            res.Buffers.Add(b2.Buffers[i]);
                            res.Buffers.Add(copy);
                            //Lets copy the ROI's from the original image.
                            List<ROI> anns = b2.GetAnnotations(zi, ci, ti);
                            if (anns.Count > 0)
                                res.Annotations.AddRange(anns);
                        }
                        else
                        {
                            //This plane is not part of b1 so we add the planes from b2 channels.
                            Bitmap copy = new Bitmap(b.id, b.SizeX, b.SizeY, b.Buffers[0].PixelFormat, b.Buffers[i].Bytes, co, i);
                            if (b2.littleEndian)
                                copy.RotateFlip(AForge.RotateFlipType.Rotate180FlipNone);
                            res.Coords[zi, ci, ti] = i;
                            res.Buffers.Add(b.Buffers[i]);
                            res.Buffers.Add(copy);

                            //Lets copy the ROI's from the original image.
                            List<ROI> anns = b.GetAnnotations(zi, cc, ti);
                            if (anns.Count > 0)
                                res.Annotations.AddRange(anns);
                        }
                        i++;
                    }
                }
            }
            for (int ci = 0; ci < res.SizeC; ci++)
            {
                if (ci < cOrig)
                {
                    res.Channels.Add(b2.Channels[ci].Copy());
                }
                else
                {
                    res.Channels.Add(b.Channels[cc].Copy());
                    res.Channels[cOrig + cc].Index = ci;
                    cc++;
                }
            }
            Images.AddImage(res, true);
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(100);
            } while (res.Buffers[res.Buffers.Count - 1].Stats == null);
            AutoThreshold(res, false);
            if (res.bitsPerPixel > 8)
                res.StackThreshold(true);
            else
                res.StackThreshold(false);
            Recorder.AddLine("Bio.BioImage.MergeChannels(" + '"' + b.ID + '"' + "," + '"' + b2.ID + '"' + ");");
            return res;
        }
        /// MergeChannels(b, b2) takes two images, b and b2, and merges the channels of b2 into b
        /// 
        /// @param bname The name of the first image
        /// @param b2name The name of the image to merge with the first image.
        /// 
        /// @return A BioImage object.
        public static BioImage MergeChannels(string bname, string b2name)
        {
            BioImage b = Images.GetImage(bname);
            BioImage b2 = Images.GetImage(b2name);
            return MergeChannels(b, b2);
        }
        /// It takes a 3D image and merges the Z-stack into a single 2D image
        /// 
        /// @param BioImage The image to be merged
        /// 
        /// @return A new BioImage object.
        public static BioImage MergeZ(BioImage b)
        {
            BioImage bi = BioImage.CopyInfo(b, true, true);
            int ind = 0;
            for (int c = 0; c < b.SizeC; c++)
            {
                for (int t = 0; t < b.sizeT; t++)
                {
                    Merge m = new Merge(b.Buffers[b.Coords[0, c, t]]);
                    Bitmap bm = new Bitmap(b.SizeX, b.SizeY, b.Buffers[0].PixelFormat);
                    for (int i = 1; i < b.sizeZ; i++)
                    {
                        m.OverlayImage = bm;
                        bm = m.Apply(b.Buffers[b.Coords[i, c, t]]);
                    }
                    Bitmap bf = new Bitmap(b.file, bm, new ZCT(0, c, t), ind);
                    bi.Buffers.Add(bf);
                    Statistics.CalcStatistics(bf);
                    ind++;
                }
            }
            Images.AddImage(bi, true);
            bi.UpdateCoords(1, b.SizeC, b.SizeT);
            bi.Coordinate = new ZCT(0, 0, 0);
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(100);
            } while (bi.Buffers[bi.Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            bi.Resolutions.Add(new Resolution(b.Buffers[0].SizeX, b.Buffers[0].SizeY, b.Buffers[0].PixelFormat, b.PhysicalSizeX, b.PhysicalSizeY, b.PhysicalSizeZ, b.StageSizeX, b.StageSizeY, b.StageSizeZ));

            AutoThreshold(bi, false);
            if (bi.bitsPerPixel > 8)
                bi.StackThreshold(true);
            else
                bi.StackThreshold(false);
            Recorder.AddLine("Bio.BioImage.MergeZ(" + '"' + b.ID + '"' + ");");
            return bi;
        }
        /// It takes a 3D image and merges the time dimension into a single image
        /// 
        /// @param BioImage The image to be processed
        /// 
        /// @return A new BioImage object.
        public static BioImage MergeT(BioImage b)
        {
            BioImage bi = BioImage.CopyInfo(b, true, true);
            int ind = 0;
            for (int c = 0; c < b.SizeC; c++)
            {
                for (int z = 0; z < b.sizeZ; z++)
                {
                    Merge m = new Merge(b.Buffers[b.Coords[z, c, 0]]);
                    Bitmap bm = new Bitmap(b.SizeX, b.SizeY, b.Buffers[0].PixelFormat);
                    for (int i = 1; i < b.sizeT; i++)
                    {
                        m.OverlayImage = bm;
                        bm = m.Apply(b.Buffers[b.Coords[z, c, i]]);
                    }
                    Bitmap bf = new Bitmap(b.file, bm, new ZCT(z, c, 0), ind);
                    bi.Buffers.Add(bf);
                    Statistics.CalcStatistics(bf);
                    ind++;
                }
            }
            Images.AddImage(bi, true);
            bi.UpdateCoords(1, b.SizeC, b.SizeT);
            bi.Coordinate = new ZCT(0, 0, 0);
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(100);
            } while (bi.Buffers[bi.Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            bi.Resolutions.Add(new Resolution(b.Buffers[0].SizeX, b.Buffers[0].SizeY, b.Buffers[0].PixelFormat, b.PhysicalSizeX, b.PhysicalSizeY, b.PhysicalSizeZ, b.StageSizeX, b.StageSizeY, b.StageSizeZ));
            AutoThreshold(bi, false);
            if (bi.bitsPerPixel > 8)
                bi.StackThreshold(true);
            else
                bi.StackThreshold(false);
            Recorder.AddLine("Bio.BioImage.MergeT(" + '"' + b.ID + '"' + ");");
            return bi;
        }
        /// It takes a single image and splits it into three images, one for each channel
        /// 
        /// @return A list of BioImages
        public BioImage[] SplitChannels()
        {
            BioImage[] bms;
            if (isRGB)
            {
                bms = new BioImage[3];
                BioImage ri = new BioImage(Path.GetFileNameWithoutExtension(ID) + "-1" + Path.GetExtension(ID));
                BioImage gi = new BioImage(Path.GetFileNameWithoutExtension(ID) + "-2" + Path.GetExtension(ID));
                BioImage bi = new BioImage(Path.GetFileNameWithoutExtension(ID) + "-3" + Path.GetExtension(ID));
                ri.sizeC = 1;
                gi.sizeC = 1;
                bi.sizeC = 1;
                ri.sizeZ = SizeZ;
                gi.sizeZ = SizeZ;
                bi.sizeZ = SizeZ;
                ri.sizeT = SizeT;
                gi.sizeT = SizeT;
                bi.sizeT = SizeT;

                ri.Coords = new int[SizeZ, 1, SizeT];
                gi.Coords = new int[SizeZ, 1, SizeT];
                bi.Coords = new int[SizeZ, 1, SizeT];
                int ind = 0;
                for (int i = 0; i < ImageCount; i++)
                {
                    if (Buffers[i].PixelFormat == PixelFormat.Format48bppRgb)
                    {
                        //For 48bit images we need to use our own function as AForge won't give us a proper image.
                        Bitmap[] bfs = Bitmap.RGB48To16(ID, SizeX, SizeY, Buffers[i].Stride, Buffers[i].Bytes, Buffers[i].Coordinate, ind, Buffers[i].Plane);
                        ind += 3;
                        ri.Buffers.Add(bfs[0]);
                        gi.Buffers.Add(bfs[1]);
                        bi.Buffers.Add(bfs[2]);
                        Statistics.CalcStatistics(bfs[0]);
                        Statistics.CalcStatistics(bfs[1]);
                        Statistics.CalcStatistics(bfs[2]);
                        ri.Coords[Buffers[i].Coordinate.Z, Buffers[i].Coordinate.C, Buffers[i].Coordinate.T] = i;
                        gi.Coords[Buffers[i].Coordinate.Z, Buffers[i].Coordinate.C, Buffers[i].Coordinate.T] = i;
                        bi.Coords[Buffers[i].Coordinate.Z, Buffers[i].Coordinate.C, Buffers[i].Coordinate.T] = i;
                    }
                    else
                    {

                        Bitmap rImage = extractR.Apply(Buffers[i]);
                        Bitmap rbf = new Bitmap(ri.ID, rImage, Buffers[i].Coordinate, ind++);
                        Statistics.CalcStatistics(rbf);
                        ri.Buffers.Add(rbf);
                        ri.Coords[Buffers[i].Coordinate.Z, Buffers[i].Coordinate.C, Buffers[i].Coordinate.T] = i;

                        Bitmap gImage = extractG.Apply(Buffers[i]);
                        Bitmap gbf = new Bitmap(gi.ID, gImage, Buffers[i].Coordinate, ind++);
                        Statistics.CalcStatistics(gbf);
                        gi.Buffers.Add(gbf);
                        gi.Coords[Buffers[i].Coordinate.Z, Buffers[i].Coordinate.C, Buffers[i].Coordinate.T] = i;

                        Bitmap bImage = extractB.Apply(Buffers[i]);
                        //Clipboard.SetImage(bImage);
                        Bitmap bbf = new Bitmap(bi.ID, bImage, Buffers[i].Coordinate, ind++);
                        Statistics.CalcStatistics(bbf);
                        bi.Buffers.Add(bbf);
                        bi.Coords[Buffers[i].Coordinate.Z, Buffers[i].Coordinate.C, Buffers[i].Coordinate.T] = i;

                    }
                }
                //We wait for threshold image statistics calculation
                do
                {
                    Thread.Sleep(100);
                } while (bi.Buffers[bi.Buffers.Count - 1].Stats == null);
                ri.Resolutions.Add(new Resolution(Buffers[0].SizeX, Buffers[0].SizeY, Buffers[0].PixelFormat, PhysicalSizeX, PhysicalSizeY, PhysicalSizeZ, StageSizeX, StageSizeY, StageSizeZ));
                gi.Resolutions.Add(new Resolution(Buffers[0].SizeX, Buffers[0].SizeY, Buffers[0].PixelFormat, PhysicalSizeX, PhysicalSizeY, PhysicalSizeZ, StageSizeX, StageSizeY, StageSizeZ));
                bi.Resolutions.Add(new Resolution(Buffers[0].SizeX, Buffers[0].SizeY, Buffers[0].PixelFormat, PhysicalSizeX, PhysicalSizeY, PhysicalSizeZ, StageSizeX, StageSizeY, StageSizeZ));
                ri.Channels.Add(Channels[0].Copy());
                gi.Channels.Add(Channels[0].Copy());
                bi.Channels.Add(Channels[0].Copy());
                AutoThreshold(ri, false);
                AutoThreshold(gi, false);
                AutoThreshold(bi, false);
                Images.AddImage(ri, true);
                Images.AddImage(gi, true);
                Images.AddImage(bi, true);
                Statistics.ClearCalcBuffer();
                bms[0] = ri;
                bms[1] = gi;
                bms[2] = bi;
            }
            else
            {
                bms = new BioImage[SizeC];
                for (int c = 0; c < SizeC; c++)
                {
                    BioImage b = BioImage.Substack(this, 0, 0, SizeZ, c, c + 1, 0, SizeT);
                    bms[c] = b;
                }
            }

            Statistics.ClearCalcBuffer();
            Recorder.AddLine("Bio.BioImage.SplitChannels(" + '"' + Filename + '"' + ");");
            return bms;
        }
        /// > SplitChannels splits a BioImage into its constituent channels
        /// 
        /// @param BioImage The image to split
        /// 
        /// @return An array of BioImages
        public static BioImage[] SplitChannels(BioImage bb)
        {
            return bb.SplitChannels();
        }
        /// This function takes an image and splits it into its individual channels
        /// 
        /// @param name The name of the image to split.
        /// 
        /// @return An array of BioImage objects.
        public static BioImage[] SplitChannels(string name)
        {
            return SplitChannels(Images.GetImage(name));
        }

        /* Creating a new instance of the LevelsLinear class. */
        public static LevelsLinear filter8 = new LevelsLinear();
        public static LevelsLinear16bpp filter16 = new LevelsLinear16bpp();
        private static ExtractChannel extractR = new ExtractChannel(AForge.Imaging.RGB.R);
        private static ExtractChannel extractG = new ExtractChannel(AForge.Imaging.RGB.G);
        private static ExtractChannel extractB = new ExtractChannel(AForge.Imaging.RGB.B);

        /// > Get the image at the specified coordinates
        /// 
        /// @param z the z-stack index
        /// @param c channel
        /// @param t time
        /// 
        /// @return A Bitmap object.
        public Bitmap GetImageByCoord(int z, int c, int t)
        {
            return Buffers[Coords[z, c, t]];
        }
        /// "Given a z, c, t coordinate, return the bitmap at that coordinate."
        /// 
        /// The function is called by the following code:
        /// 
        /// @param z the z-stack index
        /// @param c channel
        /// @param t time
        /// 
        /// @return A bitmap.
        public Bitmap GetBitmap(int z, int c, int t)
        {
            return Buffers[Coords[z, c, t]];
        }
        /// > GetIndex(x,y) = (y * stridex + x) * 2
        /// 
        /// The stridex is the width of the image in bytes. 
        /// 
        /// The stridey is the height of the image in bytes. 
        /// 
        /// The stridex and stridey are the same for a square image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The stridex and stridey are different for a rectangular image. 
        /// 
        /// The
        /// 
        /// @param ix x coordinate of the pixel
        /// @param iy The y coordinate of the pixel
        /// 
        /// @return The index of the pixel in the array.
        public int GetIndex(int ix, int iy)
        {
            if (ix > SizeX || iy > SizeY || ix < 0 || iy < 0)
                return 0;
            int stridex = SizeX;
            int x = ix;
            int y = iy;
            if (bitsPerPixel > 8)
            {
                return (y * stridex + x) * 2;
            }
            else
            {
                return (y * stridex + x);
            }
        }
        /// > The function returns the index of the pixel in the buffer
        /// 
        /// @param ix x coordinate of the pixel
        /// @param iy The y coordinate of the pixel
        /// @param index 0 = Red, 1 = Green, 2 = Blue
        /// 
        /// @return The index of the pixel in the buffer.
        public int GetIndexRGB(int ix, int iy, int index)
        {
            int stridex = SizeX;
            //For 16bit (2*8bit) images we multiply buffer index by 2
            int x = ix;
            int y = iy;
            if (bitsPerPixel > 8)
            {
                return (y * stridex + x) * 2 * index;
            }
            else
            {
                return (y * stridex + x) * index;
            }
        }
        /// If the coordinate is within the bounds of the image, then return the value of the pixel at
        /// that coordinate
        /// 
        /// @param ZCTXY a struct that contains the X, Y, Z, C, and T coordinates of the pixel.
        /// 
        /// @return The value of the pixel at the given coordinate.
        public ushort GetValue(ZCTXY coord)
        {
            if (coord.X < 0 || coord.Y < 0 || coord.X > SizeX || coord.Y > SizeY)
            {
                return 0;
            }
            if (isRGB)
            {
                if (coord.C == 0)
                    return GetValueRGB(coord, 0);
                else if (coord.C == 1)
                    return GetValueRGB(coord, 1);
                else if (coord.C == 2)
                    return GetValueRGB(coord, 2);
            }
            else
                return GetValueRGB(coord, 0);
            return 0;
        }
        /// It takes a coordinate and an index and returns the value of the pixel at that coordinate
        /// 
        /// @param ZCTXY a struct that contains the Z, C, T, X, and Y coordinates of the pixel.
        /// @param index 0, 1, 2
        /// 
        /// @return A ushort value.
        public ushort GetValueRGB(ZCTXY coord, int index)
        {
            int ind = 0;
            if (coord.C >= SizeC)
            {
                coord.C = 0;
            }
            ind = Coords[coord.Z, coord.C, coord.T];
            ColorS c = Buffers[ind].GetPixel(coord.X, coord.Y);
            if (index == 0)
                return c.R;
            else
            if (index == 1)
                return c.G;
            else
            if (index == 2)
                return c.B;
            throw new IndexOutOfRangeException();
        }
        /// > Get the value of the pixel at the given coordinates
        /// 
        /// @param ZCT Z is the Z-plane, C is the channel, T is the timepoint
        /// @param x x coordinate of the pixel
        /// @param y The y coordinate of the pixel
        /// 
        /// @return The value of the pixel at the given coordinates.
        public ushort GetValue(ZCT coord, int x, int y)
        {
            return GetValueRGB(new ZCTXY(coord.Z, coord.C, coord.T, x, y), 0);
        }
        /// > This function returns the value of the pixel at the specified ZCTXY coordinates
        /// 
        /// @param z The Z-plane of the image.
        /// @param c channel
        /// @param t time
        /// @param x x coordinate of the pixel
        /// @param y the y coordinate of the pixel
        /// 
        /// @return The value of the pixel at the given coordinates.
        public ushort GetValue(int z, int c, int t, int x, int y)
        {
            return GetValue(new ZCTXY(z, c, t, x, y));
        }
        /// > Get the value of a pixel at a given coordinate, x, y, and RGB index
        /// 
        /// @param ZCT The ZCT coordinate of the image.
        /// @param x x coordinate of the pixel
        /// @param y the y coordinate of the pixel
        /// @param RGBindex 0 = Red, 1 = Green, 2 = Blue
        /// 
        /// @return The value of the pixel at the given coordinates.
        public ushort GetValueRGB(ZCT coord, int x, int y, int RGBindex)
        {
            ZCTXY c = new ZCTXY(coord.Z, coord.C, coord.T, x, y);
            if (isRGB)
            {
                return GetValueRGB(c, RGBindex);
            }
            else
                return GetValue(coord, x, y);
        }
        /// This function returns the value of the pixel at the specified coordinates in the specified
        /// channel, frame, and RGB index
        /// 
        /// @param z The Z-plane index
        /// @param c channel
        /// @param t time index
        /// @param x x coordinate of the pixel
        /// @param y The y coordinate of the pixel
        /// @param RGBindex 0 = Red, 1 = Green, 2 = Blue
        /// 
        /// @return The value of the pixel at the given coordinates.
        public ushort GetValueRGB(int z, int c, int t, int x, int y, int RGBindex)
        {
            return GetValueRGB(new ZCT(z, c, t), x, y, RGBindex);
        }
        /// It takes a coordinate and a value, and sets the value at that coordinate
        /// 
        /// @param ZCTXY a struct that contains the Z, C, T, X, and Y coordinates of the pixel
        /// @param value the value to be set
        public void SetValue(ZCTXY coord, ushort value)
        {
            int i = Coords[coord.Z, coord.C, coord.T];
            Buffers[i].SetValue(coord.X, coord.Y, value);
        }
        /// It sets the value of a pixel in a buffer
        /// 
        /// @param x The x coordinate of the pixel to set.
        /// @param y The y coordinate of the pixel to set.
        /// @param ind The index of the buffer to set the value in.
        /// @param value The value to set the pixel to.
        public void SetValue(int x, int y, int ind, ushort value)
        {
            Buffers[ind].SetValue(x, y, value);
        }
        /// This function sets the value of a pixel at a given x,y coordinate in a given image plane
        /// 
        /// @param x x coordinate of the pixel
        /// @param y The y coordinate of the pixel to set.
        /// @param ZCT a struct that contains the Z, C, and T coordinates of the pixel
        /// @param value the value to set
        public void SetValue(int x, int y, ZCT coord, ushort value)
        {
            SetValue(x, y, Coords[coord.Z, coord.C, coord.T], value);
        }
        /// It takes a coordinate, an RGB index, and a value, and sets the value of the pixel at that
        /// coordinate to the value
        /// 
        /// @param ZCTXY a struct that contains the Z, C, T, X, and Y coordinates of the pixel
        /// @param RGBindex 0 = Red, 1 = Green, 2 = Blue
        /// @param value the value to be set
        public void SetValueRGB(ZCTXY coord, int RGBindex, ushort value)
        {
            int ind = Coords[coord.Z, coord.C, coord.T];
            Buffers[ind].SetValueRGB(coord.X, coord.Y, RGBindex, value);
        }
        /// > This function returns a Bitmap object from the image data stored in the OME-TIFF file
        /// 
        /// @param ZCT Z = Z-stack, C = channel, T = timepoint
        /// 
        /// @return A Bitmap object.
        public Bitmap GetBitmap(ZCT coord)
        {
            return (Bitmap)GetImageByCoord(coord.Z, coord.C, coord.T);
        }
        /// > Get the image at the specified ZCT coordinate, and return a filtered version of it
        /// 
        /// @param ZCT a 3-tuple of integers (z, c, t)
        /// @param IntRange 
        /// @param IntRange 
        /// @param IntRange 
        /// 
        /// @return An UnmanagedImage object.
        public Bitmap GetFiltered(ZCT coord, IntRange r, IntRange g, IntRange b)
        {
            int index = Coords[coord.Z, coord.C, coord.T];
            return GetFiltered(index, r, g, b);
        }
        /// It takes an image, and returns a filtered version of that image
        /// 
        /// @param ind the index of the buffer to be filtered
        /// @param IntRange 
        /// @param IntRange 
        /// @param IntRange 
        /// 
        /// @return A filtered image.
        public Bitmap GetFiltered(int ind, IntRange r, IntRange g, IntRange b)
        {
            if (Buffers[ind].BitsPerPixel > 8)
            {
                BioImage.filter16.InRed = r;
                BioImage.filter16.InGreen = g;
                BioImage.filter16.InBlue = b;
                return BioImage.filter16.Apply(Buffers[ind]);
            }
            else
            {
                // set ranges
                BioImage.filter8.InRed = r;
                BioImage.filter8.InGreen = g;
                BioImage.filter8.InBlue = b;
                return BioImage.filter8.Apply(Buffers[ind]);
            }
        }
        /// It takes an image, and returns a channel of that image
        /// 
        /// @param ind the index of the buffer
        /// @param s 0, 1, 2
        public UnmanagedImage GetChannelImage(int ind, short s)
        {
            Bitmap bf = Buffers[ind];
            if (bf.isRGB)
            {
                if (s == 0)
                    return extractR.Apply(Buffers[ind].Image);
                else
                if (s == 1)
                    return extractG.Apply(Buffers[ind].Image);
                else
                    return extractB.Apply(Buffers[ind].Image);
            }
            else
                throw new InvalidOperationException();
        }
        /// > GetEmission() returns an UnmanagedImage object that is a composite of the emission
        /// channels
        /// 
        /// @param ZCT Z, C, T coordinates
        /// @param IntRange 
        /// @param IntRange 
        /// @param IntRange 
        /// 
        /// @return A Bitmap or an UnmanagedImage.
        public Bitmap GetEmission(ZCT coord, IntRange rf, IntRange gf, IntRange bf)
        {
            if (RGBChannelCount == 1)
            {
                Bitmap[] bs = new Bitmap[Channels.Count];
                for (int c = 0; c < Channels.Count; c++)
                {
                    int index = Coords[coord.Z, c, coord.T];
                    bs[c] = Buffers[index];
                }
                Bitmap bm = (Bitmap)Bitmap.GetEmissionBitmap(bs, Channels.ToArray());
                return bm;
            }
            else
            {
                int index = Coords[coord.Z, coord.C, coord.T];
                return Buffers[index];
            }
        }
        /// > Get the RGB bitmap for the specified ZCT coordinate
        /// 
        /// @param ZCT Z, C, T coordinates
        /// @param IntRange 
        /// @param IntRange 
        /// @param IntRange 
        /// 
        /// @return A Bitmap object.
        public Bitmap GetRGBBitmap(ZCT coord, IntRange rf, IntRange gf, IntRange bf)
        {
            int index = Coords[coord.Z, coord.C, coord.T];
            if (Buffers[0].RGBChannelsCount == 1)
            {
                if (Channels.Count >= 3)
                {
                    Bitmap[] bs = new Bitmap[3];
                    bs[2] = Buffers[index + RChannel.Index];
                    bs[1] = Buffers[index + GChannel.Index];
                    bs[0] = Buffers[index + BChannel.Index];
                    return Bitmap.GetRGBBitmap(bs, rf, gf, bf);
                }
                else
                {
                    Bitmap[] bs = new Bitmap[3];
                    bs[2] = Buffers[index + RChannel.Index];
                    bs[1] = Buffers[index + RChannel.Index + 1];
                    bs[0] = Buffers[index + RChannel.Index + 2];
                    return Bitmap.GetRGBBitmap(bs, rf, gf, bf);
                }
            }
            else
                return Buffers[index];
        }
        /// It takes a byte array of RGB or RGBA data and converts it to a Bitmap
        /// 
        /// @param w width of the image
        /// @param h height of the image
        /// @param PixelFormat The pixel format of the image.
        /// @param bts the byte array of the image
        /// 
        /// @return A Bitmap object.
        public static unsafe Bitmap GetBitmapRGB(int w, int h, PixelFormat px, byte[] bts)
        {
            if (px == PixelFormat.Format32bppArgb)
            {
                //opening a 8 bit per pixel jpg image
                Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                AForge.Rectangle rec = new AForge.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, bmp.PixelFormat);
                //iterating through all the pixels in y direction
                for (int y = 0; y < h; y++)
                {
                    //getting the pixels of current row
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    int rowRGB = y * w * 4;
                    //iterating through all the pixels in x direction
                    for (int x = 0; x < w; x++)
                    {
                        int indexRGB = x * 4;
                        int indexRGBA = x * 4;
                        row[indexRGBA + 3] = bts[rowRGB + indexRGB + 3];//byte A
                        row[indexRGBA + 2] = bts[rowRGB + indexRGB + 2];//byte R
                        row[indexRGBA + 1] = bts[rowRGB + indexRGB + 1];//byte G
                        row[indexRGBA] = bts[rowRGB + indexRGB];//byte B
                    }
                }
                //unlocking bits and disposing image
                bmp.UnlockBits(bmd);
                return bmp;
            }
            else if (px == PixelFormat.Format24bppRgb)
            {
                //opening a 8 bit per pixel jpg image
                Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                AForge.Rectangle rec = new AForge.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, bmp.PixelFormat);
                //iterating through all the pixels in y direction
                for (int y = 0; y < h; y++)
                {
                    //getting the pixels of current row
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    int rowRGB = y * w * 3;
                    //iterating through all the pixels in x direction
                    for (int x = 0; x < w; x++)
                    {
                        int indexRGB = x * 3;
                        int indexRGBA = x * 4;
                        row[indexRGBA + 3] = byte.MaxValue;//byte A
                        row[indexRGBA + 2] = bts[rowRGB + indexRGB + 2];//byte R
                        row[indexRGBA + 1] = bts[rowRGB + indexRGB + 1];//byte G
                        row[indexRGBA] = bts[rowRGB + indexRGB];//byte B
                    }
                }
                //unlocking bits and disposing image
                bmp.UnlockBits(bmd);
                return bmp;
            }
            else
            if (px == PixelFormat.Format48bppRgb)
            {
                //opening a 8 bit per pixel jpg image
                Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                AForge.Rectangle rec = new AForge.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, bmp.PixelFormat);
                unsafe
                {
                    //iterating through all the pixels in y direction
                    for (int y = 0; y < h; y++)
                    {
                        //getting the pixels of current row
                        byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                        int rowRGB = y * w * 6;
                        //iterating through all the pixels in x direction
                        for (int x = 0; x < w; x++)
                        {
                            int indexRGB = x * 6;
                            int indexRGBA = x * 4;
                            int b = (int)((float)BitConverter.ToUInt16(bts, rowRGB + indexRGB) / 255);
                            int g = (int)((float)BitConverter.ToUInt16(bts, rowRGB + indexRGB + 2) / 255);
                            int r = (int)((float)BitConverter.ToUInt16(bts, rowRGB + indexRGB + 4) / 255);
                            row[indexRGBA + 3] = 255;//byte A
                            row[indexRGBA + 2] = (byte)(b);//byte R
                            row[indexRGBA + 1] = (byte)(g);//byte G
                            row[indexRGBA] = (byte)(r);//byte B
                        }
                    }
                }
                bmp.UnlockBits(bmd);
                return bmp;
            }
            else
            if (px == PixelFormat.Format8bppIndexed)
            {
                //opening a 8 bit per pixel jpg image
                Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                AForge.Rectangle rec = new AForge.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, bmp.PixelFormat);
                unsafe
                {
                    //iterating through all the pixels in y direction
                    for (int y = 0; y < h; y++)
                    {
                        //getting the pixels of current row
                        byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                        int rowRGB = y * w;
                        //iterating through all the pixels in x direction
                        for (int x = 0; x < w; x++)
                        {
                            int indexRGB = x;
                            int indexRGBA = x * 4;
                            byte b = bts[rowRGB + indexRGB];
                            row[indexRGBA + 3] = 255;//byte A
                            row[indexRGBA + 2] = (byte)(b);//byte R
                            row[indexRGBA + 1] = (byte)(b);//byte G
                            row[indexRGBA] = (byte)(b);//byte B
                        }
                    }
                }
                bmp.UnlockBits(bmd);
                return bmp;
            }
            else
            if (px == PixelFormat.Format16bppGrayScale)
            {
                //opening a 8 bit per pixel jpg image
                Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                AForge.Rectangle rec = new AForge.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, bmp.PixelFormat);
                unsafe
                {
                    //iterating through all the pixels in y direction
                    for (int y = 0; y < h; y++)
                    {
                        //getting the pixels of current row
                        byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                        int rowRGB = y * w * 2;
                        //iterating through all the pixels in x direction
                        for (int x = 0; x < w; x++)
                        {
                            int indexRGB = x * 2;
                            int indexRGBA = x * 4;
                            ushort b = (ushort)((float)BitConverter.ToUInt16(bts, rowRGB + indexRGB) / 255);
                            row[indexRGBA + 3] = 255;//byte A
                            row[indexRGBA + 2] = (byte)(b);//byte R
                            row[indexRGBA + 1] = (byte)(b);//byte G
                            row[indexRGBA] = (byte)(b);//byte B
                        }
                    }
                }
                bmp.UnlockBits(bmd);
                return bmp;
            }

            throw new NotSupportedException("Pixelformat " + px + " is not supported.");
        }
        public static Stopwatch swatch = new Stopwatch();
        /// > GetAnnotations() returns a list of ROI objects that are associated with the ZCT coordinate
        /// passed in as a parameter
        /// 
        /// @param ZCT a 3D coordinate (Z, C, T)
        /// 
        /// @return A list of ROI objects.
        public List<ROI> GetAnnotations(ZCT coord)
        {
            List<ROI> annotations = new List<ROI>();
            foreach (ROI an in Annotations)
            {
                if (an == null)
                    continue;
                if (an.coord == coord)
                    annotations.Add(an);
            }
            return annotations;
        }
        /// This function returns a list of ROI objects that are associated with the specified Z, C, and
        /// T coordinates
        /// 
        /// @param Z The Z-stack index
        /// @param C Channel
        /// @param T Time
        /// 
        /// @return A list of ROI objects.
        public List<ROI> GetAnnotations(int Z, int C, int T)
        {
            List<ROI> annotations = new List<ROI>();
            foreach (ROI an in Annotations)
            {
                if (an.coord.Z == Z && an.coord.Z == Z && an.coord.C == C && an.coord.T == T)
                    annotations.Add(an);
            }
            return annotations;
        }

        public bool Loading = false;
        /// We initialize OME on a seperate thread so the user doesn't have to wait for initialization
        /// to view images.
        public static void Initialize()
        {
            if (!OMESupport())
                return;
            //We initialize OME on a seperate thread so the user doesn't have to wait for initialization to
            //view images. 
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(InitOME));
            t.Start();
        }
        /// > Initialize the OME-XML library
        private static void InitOME()
        {
            factory = new ServiceFactory();
            service = (OMEXMLService)factory.getInstance(typeof(OMEXMLService));
            reader = new ImageReader();
            writer = new ImageWriter();
            initialized = true;
        }
        /// This function takes a string array of file names and a string ID and saves the files to the
        /// database
        /// 
        /// @param file The file path to the file you want to save.
        /// @param ID The ID of the series you want to save.
        public static void SaveFile(string file, string ID)
        {
            string[] sts = new string[1];
            sts[0] = ID;
            SaveSeries(sts, file);
        }
        /// It takes a list of image IDs, and saves them as a single multi-page TIFF file.
        /// 
        /// @param An array of IDs of the images to save
        /// @param The path to the file to save to.
        public static void SaveSeries(string[] IDs, string file)
        {
            string desc = "";
            int stride = 0;
            ImageJDesc j = new ImageJDesc();
            BioImage bi = Images.GetImage(IDs[0]);
            j.FromImage(bi);
            desc = j.GetString();
            for (int fi = 0; fi < IDs.Length; fi++)
            {
                string id = IDs[fi];
                BioImage b = Images.GetImage(id);
                string fn = Path.GetFileNameWithoutExtension(id);
                string dir = Path.GetDirectoryName(file);
                stride = b.Buffers[0].Stride;

                //Save ROIs to CSV file.
                if (b.Annotations.Count > 0)
                {
                    string f = dir + "//" + fn + ".csv";
                    ExportROIsCSV(f, b.Annotations);
                }

                //Embed ROI's to image description.
                for (int i = 0; i < b.Annotations.Count; i++)
                {
                    desc += "-ROI:" + b.series + ":" + ROIToString(b.Annotations[i]) + NewLine;
                }
                foreach (Channel c in b.Channels)
                {
                    string cj = JsonConvert.SerializeObject(c.info, Formatting.None);
                    desc += "-Channel:" + fi + ":" + cj + NewLine;
                }
                string json = JsonConvert.SerializeObject(b.imageInfo, Formatting.None);
                desc += "-ImageInfo:" + fi + ":" + json + NewLine;
            }

            Tiff image = Tiff.Open(file, "w");
            for (int fi = 0; fi < IDs.Length; fi++)
            {
                int im = 0;
                string id = IDs[fi];
                //Progress pr = new //Progress(file, "Saving");
                //pr.Show();
                //Application.DoEvents();
                BioImage b = Images.GetImage(id);
                int sizec = 1;
                if (!b.isRGB)
                {
                    sizec = b.SizeC;
                }
                byte[] buffer;
                for (int c = 0; c < sizec; c++)
                {
                    for (int z = 0; z < b.SizeZ; z++)
                    {
                        for (int t = 0; t < b.SizeT; t++)
                        {
                            image.SetDirectory((short)(im + (b.Buffers.Count * fi)));
                            image.SetField(TiffTag.IMAGEWIDTH, b.SizeX);
                            image.SetField(TiffTag.IMAGEDESCRIPTION, desc);
                            image.SetField(TiffTag.IMAGELENGTH, b.SizeY);
                            image.SetField(TiffTag.BITSPERSAMPLE, b.bitsPerPixel);
                            image.SetField(TiffTag.SAMPLESPERPIXEL, b.RGBChannelCount);
                            image.SetField(TiffTag.ROWSPERSTRIP, b.SizeY);
                            image.SetField(TiffTag.ORIENTATION, BitMiracle.LibTiff.Classic.Orientation.TOPLEFT);
                            image.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
                            image.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
                            image.SetField(TiffTag.ROWSPERSTRIP, image.DefaultStripSize(0));
                            if (b.PhysicalSizeX != -1 && b.PhysicalSizeY != -1)
                            {
                                image.SetField(TiffTag.XRESOLUTION, (b.PhysicalSizeX * b.SizeX) / ((b.PhysicalSizeX * b.SizeX) * b.PhysicalSizeX));
                                image.SetField(TiffTag.YRESOLUTION, (b.PhysicalSizeY * b.SizeY) / ((b.PhysicalSizeY * b.SizeY) * b.PhysicalSizeY));
                                image.SetField(TiffTag.RESOLUTIONUNIT, ResUnit.NONE);
                            }
                            else
                            {
                                image.SetField(TiffTag.XRESOLUTION, 100.0);
                                image.SetField(TiffTag.YRESOLUTION, 100.0);
                                image.SetField(TiffTag.RESOLUTIONUNIT, ResUnit.INCH);
                            }
                            // specify that it's a page within the multipage file
                            image.SetField(TiffTag.SUBFILETYPE, FileType.PAGE);
                            // specify the page number
                            buffer = b.Buffers[im].GetSaveBytes(true);
                            image.SetField(TiffTag.PAGENUMBER, im + (b.Buffers.Count * fi), b.Buffers.Count * IDs.Length);
                            for (int i = 0, offset = 0; i < b.SizeY; i++)
                            {
                                image.WriteScanline(buffer, offset, i, 0);
                                offset += stride;
                            }
                            image.WriteDirectory();
                            //pr.Update//ProgressF((float)im / (float)b.ImageCount);
                            //Application.DoEvents();
                            im++;
                        }
                    }
                }
                //pr.Close();
            }
            image.Dispose();

        }
        /// It opens a tiff file, reads the number of pages, reads the number of channels, and then
        /// reads each page into a BioImage object.
        /// 
        /// @param file the path to the file
        /// @param tab open image in new tab.
        /// @return An array of BioImage objects.
        public static BioImage[] OpenSeries(string file, bool tab)
        {
            Tiff image = Tiff.Open(file, "r");
            int pages = image.NumberOfDirectories();
            FieldValue[] f = image.GetField(TiffTag.IMAGEDESCRIPTION);
            int sp = image.GetField(TiffTag.SAMPLESPERPIXEL)[0].ToInt();
            ImageJDesc imDesc = new ImageJDesc();
            int count = 1;
            if (f != null)
            {
                string desc = f[0].ToString();
                if (desc.StartsWith("ImageJ"))
                {
                    imDesc.SetString(desc);
                    if (imDesc.channels != 0)
                        count = imDesc.channels;
                }
            }
            int scount = (pages * sp) / count;
            BioImage[] bs = new BioImage[pages];
            image.Close();
            for (int i = 0; i < pages; i++)
            {
                bs[i] = OpenFile(file, i, tab, true);
            }
            return bs;
        }
        /// This function opens a file and returns a BioImage object
        /// 
        /// @param file The path to the file to open.
        /// 
        /// @return A BioImage object.
        public static BioImage OpenFile(string file)
        {
            return OpenFile(file, 0, false, true);
        }
        public static BioImage OpenFile(string file, bool tab)
        {
            return OpenFile(file, 0, tab, true);
        }
        /// It opens a TIFF file and returns a BioImage object
        /// 
        /// @param file the file path
        /// @param series the series number of the image to open
        /// 
        /// @return A BioImage object.
        public static BioImage OpenFile(string file, int series, bool tab, bool addToImages)
        {
            return OpenFile(file, series, tab, addToImages, false, 0, 0, 0, 0);
        }
        static bool IsTiffTiled(string imagePath)
        {
            if (imagePath.EndsWith(".tif") || imagePath.EndsWith(".tiff"))
            {
                using (Tiff tiff = Tiff.Open(imagePath, "r"))
                {
                    if (tiff == null)
                    {
                        throw new Exception("Failed to open TIFF image.");
                    }
                    bool t = tiff.IsTiled();
                    tiff.Close();
                    return t;
                }
            }
            else return false;
        }
        /// The function `InitDirectoryResolution` initializes the resolution properties of a `BioImage`
        /// object based on the resolution information stored in a TIFF image.
        /// 
        /// @param BioImage BioImage is a class representing a bioimage, which contains information
        /// about the image resolution, size, and pixel format.
        /// @param Tiff The "Tiff" parameter is an object of the Tiff class. It represents a TIFF image
        /// file and provides methods and properties to access and manipulate the image data.
        /// @param ImageJDesc ImageJDesc is a class that contains information about the image dimensions
        /// and resolution in ImageJ format. It has the following properties:
        static void InitDirectoryResolution(BioImage b, Tiff image, ImageJDesc jdesc = null)
        {
            Resolution res = new Resolution();
            FieldValue[] rs = image.GetField(TiffTag.RESOLUTIONUNIT);
            string unit = "NONE";
            if (rs != null)
                unit = rs[0].ToString();
            if (jdesc != null)
                res.PhysicalSizeZ = jdesc.finterval;
            else
                res.PhysicalSizeZ = 2.54 / 96;
            if (unit == "CENTIMETER")
            {
                if (image.GetField(TiffTag.XRESOLUTION) != null)
                {
                    double x = image.GetField(TiffTag.XRESOLUTION)[0].ToDouble();
                    res.PhysicalSizeX = (1000 / x);
                }
                if (image.GetField(TiffTag.YRESOLUTION) != null)
                {
                    double y = image.GetField(TiffTag.YRESOLUTION)[0].ToDouble();
                    res.PhysicalSizeY = (1000 / y);
                }
            }
            else
            if (unit == "INCH")
            {
                if (image.GetField(TiffTag.XRESOLUTION) != null)
                {
                    double x = image.GetField(TiffTag.XRESOLUTION)[0].ToDouble();
                    res.PhysicalSizeX = (2.54 / x) / 2.54;
                }
                if (image.GetField(TiffTag.YRESOLUTION) != null)
                {
                    double y = image.GetField(TiffTag.YRESOLUTION)[0].ToDouble();
                    res.PhysicalSizeY = (2.54 / y) / 2.54;
                }
            }
            else
            if (unit == "NONE")
            {
                if (jdesc != null)
                {
                    if (jdesc.unit == "micron")
                    {
                        if (image.GetField(TiffTag.XRESOLUTION) != null)
                        {
                            double x = image.GetField(TiffTag.XRESOLUTION)[0].ToDouble();
                            res.PhysicalSizeX = (2.54 / x) / 2.54;
                        }
                        if (image.GetField(TiffTag.YRESOLUTION) != null)
                        {
                            double y = image.GetField(TiffTag.YRESOLUTION)[0].ToDouble();
                            res.PhysicalSizeY = (2.54 / y) / 2.54;
                        }
                    }
                }
                else
                {
                    res.PhysicalSizeX = 2.54 / 96;
                    res.PhysicalSizeY = 2.54 / 96;
                    res.PhysicalSizeZ = 2.54 / 96;
                }
            }
            res.SizeX = image.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
            res.SizeY = image.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
            int bitsPerPixel = image.GetField(TiffTag.BITSPERSAMPLE)[0].ToInt();
            int RGBChannelCount = image.GetField(TiffTag.SAMPLESPERPIXEL)[0].ToInt();
            res.PixelFormat = GetPixelFormat(RGBChannelCount, bitsPerPixel);
            b.Resolutions.Add(res);
        }

        /// The OpenFile function in C# opens a BioImage file, reads its metadata, and loads the image
        /// data into a BioImage object.
        /// 
        /// @param file The file path of the bioimage to be opened.
        /// @param series The series parameter is an integer that specifies the series number of the
        /// image to open.
        /// @param tab The "tab" parameter is a boolean value that determines whether the BioImage
        /// should be opened in a new tab or not. If set to true, the BioImage will be opened in a new
        /// tab. If set to false, the BioImage will be opened in the current tab.
        /// @param addToImages A boolean value indicating whether the BioImage should be added to the
        /// Images collection.
        /// @param tile The "tile" parameter is a boolean value that determines whether the image should
        /// be opened as a tiled image or not. If set to true, the image will be opened as a tiled
        /// image. If set to false, the image will be opened as a regular image.
        /// @param tileX The `tileX` parameter is an integer that represents the starting X coordinate
        /// of the tile. It is used when opening a tiled image to specify the position of the tile
        /// within the image.
        /// @param tileY The `tileY` parameter is used to specify the starting Y coordinate of the tile
        /// when opening a tiled image. It determines the position of the tile within the image.
        /// @param tileSizeX The tileSizeX parameter is the width of each tile in pixels. It is used
        /// when opening a tiled TIFF image to specify the size of the tiles.
        /// @param tileSizeY The tileSizeY parameter is the height of each tile in pixels when the image
        /// is tiled.
        /// 
        /// @return The method is returning a BioImage object.
        public static BioImage OpenFile(string file, int series, bool tab, bool addToImages, bool tile, int tileX, int tileY, int tileSizeX, int tileSizeY)
        {
            string fs = file.Replace("\\", "/");
            vips = VipsSupport(file);
            Console.WriteLine("Opening BioImage: " + file);
            bool ome = isOME(file);
            if (ome) return OpenOME(file, series, tab, addToImages, tile, tileX, tileY, tileSizeX, tileSizeY);
            bool tiled = IsTiffTiled(file);
            Console.WriteLine("IsTiled=" + tiled.ToString());
            tile = tiled;

            Stopwatch st = new Stopwatch();
            st.Start();
            status = "Opening Image";
            progFile = file;
            progressValue = 0;
            BioImage b = new BioImage(file);
            if (tiled && file.EndsWith(".tif") && !file.EndsWith(".ome.tif"))
            {
                //To open this we need libvips
                vips = VipsSupport(b.file);
            }
            b.series = series;
            b.file = file;
            b.ispyramidal = tiled;
            string fn = Path.GetFileNameWithoutExtension(file);
            string dir = Path.GetDirectoryName(file);
            if (File.Exists(fn + ".csv"))
            {
                string f = dir + "//" + fn + ".csv";
                b.Annotations = BioImage.ImportROIsCSV(f);
            }
            if (file.EndsWith("tif") || file.EndsWith("tiff") || file.EndsWith("TIF") || file.EndsWith("TIFF"))
            {
                Tiff image = Tiff.Open(file, "r");
                b.tifRead = image;
                int SizeX = image.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
                int SizeY = image.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
                b.bitsPerPixel = image.GetField(TiffTag.BITSPERSAMPLE)[0].ToInt();
                b.littleEndian = !image.IsBigEndian();
                int RGBChannelCount = image.GetField(TiffTag.SAMPLESPERPIXEL)[0].ToInt();
                string desc = "";

                FieldValue[] f = image.GetField(TiffTag.IMAGEDESCRIPTION);
                desc = f[0].ToString();
                ImageJDesc imDesc = null;
                b.sizeC = 1;
                b.sizeT = 1;
                b.sizeZ = 1;
                if (f != null && !tile)
                {
                    imDesc = new ImageJDesc();
                    desc = f[0].ToString();
                    if (desc.StartsWith("ImageJ"))
                    {
                        imDesc.SetString(desc);
                        if (imDesc.channels != 0)
                            b.sizeC = imDesc.channels;
                        else
                            b.sizeC = 1;
                        if (imDesc.slices != 0)
                            b.sizeZ = imDesc.slices;
                        else
                            b.sizeZ = 1;
                        if (imDesc.frames != 0)
                            b.sizeT = imDesc.frames;
                        else
                            b.sizeT = 1;
                        if (imDesc.finterval != 0)
                            b.frameInterval = imDesc.finterval;
                        else
                            b.frameInterval = 1;
                        if (imDesc.spacing != 0)
                            b.imageInfo.PhysicalSizeZ = imDesc.spacing;
                        else
                            b.imageInfo.PhysicalSizeZ = 1;
                    }
                }
                int stride = 0;
                PixelFormat PixelFormat;
                if (RGBChannelCount == 1)
                {
                    if (b.bitsPerPixel > 8)
                    {
                        PixelFormat = PixelFormat.Format16bppGrayScale;
                        stride = SizeX * 2;
                    }
                    else
                    {
                        PixelFormat = PixelFormat.Format8bppIndexed;
                        stride = SizeX;
                    }
                }
                else
                if (RGBChannelCount == 3)
                {
                    b.sizeC = 1;
                    if (b.bitsPerPixel > 8)
                    {
                        PixelFormat = PixelFormat.Format48bppRgb;
                        stride = SizeX * 2 * 3;
                    }
                    else
                    {
                        PixelFormat = PixelFormat.Format24bppRgb;
                        stride = SizeX * 3;
                    }
                }
                else
                {
                    PixelFormat = PixelFormat.Format32bppArgb;
                    stride = SizeX * 4;
                }

                string[] sts = desc.Split('\n');
                int index = 0;
                for (int i = 0; i < sts.Length; i++)
                {
                    if (sts[i].StartsWith("-Channel"))
                    {
                        string val = sts[i].Substring(9);
                        val = val.Substring(0, val.IndexOf(':'));
                        int serie = int.Parse(val);
                        if (serie == series && sts[i].Length > 7)
                        {
                            string cht = sts[i].Substring(sts[i].IndexOf('{'), sts[i].Length - sts[i].IndexOf('{'));
                            Channel.ChannelInfo info = JsonConvert.DeserializeObject<Channel.ChannelInfo>(cht);
                            Channel ch = new Channel(index, b.bitsPerPixel, info.SamplesPerPixel);
                            ch.info = info;
                            b.Channels.Add(ch);
                            if (index == 0)
                            {
                                b.rgbChannels[0] = 0;
                            }
                            else
                            if (index == 1)
                            {
                                b.rgbChannels[1] = 1;
                            }
                            else
                            if (index == 2)
                            {
                                b.rgbChannels[2] = 2;
                            }
                            index++;
                        }
                    }
                    else
                    if (sts[i].StartsWith("-ROI"))
                    {
                        string val = sts[i].Substring(5);
                        val = val.Substring(0, val.IndexOf(':'));
                        int serie = int.Parse(val);
                        if (serie == series && sts[i].Length > 7)
                        {
                            string s = sts[i].Substring(sts[i].IndexOf("ROI:") + 4, sts[i].Length - (sts[i].IndexOf("ROI:") + 4));
                            string ro = s.Substring(s.IndexOf(":") + 1, s.Length - (s.IndexOf(':') + 1));
                            ROI roi = StringToROI(ro);
                            b.Annotations.Add(roi);
                        }
                    }
                    else
                    if (sts[i].StartsWith("-ImageInfo"))
                    {
                        string val = sts[i].Substring(11);
                        val = val.Substring(0, val.IndexOf(':'));
                        int serie = int.Parse(val);
                        if (serie == series && sts[i].Length > 10)
                        {
                            string cht = sts[i].Substring(sts[i].IndexOf('{'), sts[i].Length - sts[i].IndexOf('{'));
                            b.imageInfo = JsonConvert.DeserializeObject<ImageInfo>(cht);
                        }
                    }
                }
                b.Coords = new int[b.SizeZ, b.SizeC, b.SizeT];
                if (tiled && tileSizeX == 0 && tileSizeY == 0)
                {
                    tileSizeX = 1920;
                    tileSizeY = 1080;
                }

                //If this is a tiff file not made by Bio we init channels based on RGBChannels.
                if (b.Channels.Count == 0)
                    b.Channels.Add(new Channel(0, b.bitsPerPixel, RGBChannelCount));

                //Lets check to see the channels are correctly defined in this file
                for (int ch = 0; ch < b.Channels.Count; ch++)
                {
                    if (b.Channels[ch].SamplesPerPixel != RGBChannelCount)
                    {
                        b.Channels[ch].SamplesPerPixel = RGBChannelCount;
                    }
                }

                b.Buffers = new List<Bitmap>();
                int pages = image.NumberOfDirectories() / b.seriesCount;
                int str = image.ScanlineSize();
                bool inter = true;
                if (stride != str)
                    inter = false;
                InitDirectoryResolution(b, image, imDesc);
                if (tiled)
                {
                    Console.WriteLine("Opening tiles.");
                    if (vips)
                        OpenVips(b, b.Resolutions.Count);
                    for (int t = 0; t < b.SizeT; t++)
                    {
                        for (int c = 0; c < b.SizeC; c++)
                        {
                            for (int z = 0; z < b.SizeZ; z++)
                            {
                                Bitmap bmp = GetTile(b, new ZCT(z, c, t), series, tileX, tileY, tileSizeX, tileSizeY);
                                b.Buffers.Add(bmp);
                                Statistics.CalcStatistics(bmp);
                            }
                        }
                    }
                    Console.WriteLine("Calculating statisitics.");
                }
                else
                {
                    for (int p = series * pages; p < (series + 1) * pages; p++)
                    {
                        image.SetDirectory((short)p);

                        byte[] bytes = new byte[stride * SizeY];
                        for (int im = 0, offset = 0; im < SizeY; im++)
                        {
                            image.ReadScanline(bytes, offset, im, 0);
                            offset += stride;
                        }
                        Bitmap inf = new Bitmap(file, SizeX, SizeY, b.Resolutions[series].PixelFormat, bytes, new ZCT(0, 0, 0), p, null, b.littleEndian, inter);
                        b.Buffers.Add(inf);
                        Statistics.CalcStatistics(inf);
                        progressValue = (float)p / (float)(series + 1) * pages;
                    }
                }
                image.Close();
                b.UpdateCoords();
            }
            if (b.StageSizeX == -1)
            {
                b.imageInfo.Series = 0;
                b.StageSizeX = 0;
                b.StageSizeY = 0;
                b.StageSizeZ = 0;
            }
            b.Volume = new VolumeD(new Point3D(b.StageSizeX, b.StageSizeY, b.StageSizeZ), new Point3D(b.PhysicalSizeX * b.SizeX, b.PhysicalSizeY * b.SizeY, b.PhysicalSizeZ * b.SizeZ));

            //If file is ome and we have OME support then check for annotation in metadata.
            if (ome && OmeSupport)
            {
                b.Annotations.AddRange(OpenOMEROIs(file, series));
            }

            //We wait for histogram image statistics calculation
            do
            {
                Thread.Sleep(50);
            } while (b.Buffers[b.Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            AutoThreshold(b, false);
            if (b.bitsPerPixel > 8)
                b.StackThreshold(true);
            else
                b.StackThreshold(false);
            Recorder.AddLine("Bio.BioImage.Open(" + '"' + file + '"' + ");");
            if (addToImages)
                Images.AddImage(b, tab);
            //pr.Close();
            //pr.Dispose();
            st.Stop();
            b.loadTimeMS = st.ElapsedMilliseconds;
            Console.WriteLine("BioImage loaded " + b.ToString());
            return b;
        }
        /// > The function checks if the image is a Tiff image and if it is, it checks if the image is a
        /// series of images
        /// 
        /// @param file the path to the file
        /// 
        /// @return a boolean value.
        public static bool isTiffSeries(string file)
        {
            Tiff image = Tiff.Open(file, "r");
            string desc = "";
            FieldValue[] f = image.GetField(TiffTag.IMAGEDESCRIPTION);
            image.Close();
            string[] sts = desc.Split('\n');
            int index = 0;
            for (int i = 0; i < sts.Length; i++)
            {
                if (sts[i].StartsWith("-ImageInfo"))
                {
                    string val = sts[i].Substring(11);
                    val = val.Substring(0, val.IndexOf(':'));
                    int serie = int.Parse(val);
                    if (sts[i].Length > 10)
                    {
                        string cht = sts[i].Substring(sts[i].IndexOf('{'), sts[i].Length - sts[i].IndexOf('{'));
                        ImageInfo info = JsonConvert.DeserializeObject<ImageInfo>(cht);
                        if (info.Series > 1)
                            return true;
                        else
                            return false;
                    }
                }
            }
            return false;
        }
        /// If the file is a TIFF, check the ImageDescription tag for the string "OME-XML". If it's
        /// there, return true. If it's not a TIFF, return true. If it's a PNG, JPG, JPEG, or BMP,
        /// return false
        /// 
        /// @param file the path to the image file
        /// 
        /// @return A boolean value.
        public static bool isOME(string file)
        {
            if (file.EndsWith("ome.tif") || file.EndsWith("ome.tiff"))
            {
                return true;
            }
            if (file.EndsWith(".tif") || file.EndsWith(".TIF") || file.EndsWith("tiff") || file.EndsWith("TIFF"))
            {
                Tiff image = Tiff.Open(file, "r");
                string desc = "";
                FieldValue[] f = image.GetField(TiffTag.IMAGEDESCRIPTION);
                desc = f[0].ToString();
                image.Close();
                if (desc.Contains("OME-XML"))
                    return true;
                else
                    return false;
            }
            if (!(file.EndsWith("png") || file.EndsWith("PNG") || file.EndsWith("jpg") || file.EndsWith("JPG") ||
                file.EndsWith("jpeg") || file.EndsWith("JPEG") || file.EndsWith("bmp") || file.EndsWith("BMP")))
            {
                return true;
            }
            else return false;
        }
        /// > If the file is an OME file and has more than one series, return true
        /// 
        /// @param file the file to be checked
        /// 
        /// @return A boolean value.
        public static bool isOMESeries(string file)
        {
            if (!isOME(file))
                return false;
            ImageReader reader = new ImageReader();
            var meta = (IMetadata)((OMEXMLService)new ServiceFactory().getInstance(typeof(OMEXMLService))).createOMEXMLMetadata();
            reader.setMetadataStore((MetadataStore)meta);
            file = file.Replace("\\", "/");
            reader.setId(file);
            bool ser = false;
            if (reader.getSeriesCount() > 1)
                ser = true;
            reader.close();
            reader = null;
            return ser;
        }
        /// This function takes a string array of image IDs, a file name, and a number of planes. It
        /// then saves the images to the file name
        /// 
        /// @param file the file name to save the image to
        /// @param ID The ID of the image you want to save
        public static void SaveOME(string file, string ID)
        {
            if (!OMESupport())
                return;
            string[] sts = new string[1];
            sts[0] = ID;
            SaveOMESeries(sts, file, BioImage.Planes);
        }
        /// This function takes a list of image files and saves them as a single OME-TIFF file
        /// 
        /// @param files an array of file paths to the images to be saved
        /// @param f the file name to save to
        /// @param planes if true, the planes will be saved as well.
        public static void SaveOMESeries(string[] files, string f, bool planes)
        {
            if (!OMESupport())
                return;
            if (File.Exists(f))
                File.Delete(f);
            loci.formats.meta.IMetadata omexml = service.createOMEXMLMetadata();
            status = "Saving OME Image Metadata.";
            for (int fi = 0; fi < files.Length; fi++)
            {
                progFile = files[fi];
                int serie = fi;
                string file = files[fi];

                BioImage b = Images.GetImage(file);
                if (b.isPyramidal)
                {
                    b = OpenOME(b.file, b.resolution, false, false, true, (int)App.viewer.PyramidalOrigin.X, (int)App.viewer.PyramidalOrigin.Y, App.viewer.Width, App.viewer.Height);
                }
                // create OME-XML metadata store

                omexml.setImageID("Image:" + serie, serie);
                omexml.setPixelsID("Pixels:" + serie, serie);
                omexml.setPixelsInterleaved(java.lang.Boolean.TRUE, serie);
                omexml.setPixelsDimensionOrder(ome.xml.model.enums.DimensionOrder.XYCZT, serie);
                if (b.bitsPerPixel > 8)
                    omexml.setPixelsType(ome.xml.model.enums.PixelType.UINT16, serie);
                else
                    omexml.setPixelsType(ome.xml.model.enums.PixelType.UINT8, serie);
                omexml.setPixelsSizeX(new PositiveInteger(java.lang.Integer.valueOf(b.SizeX)), serie);
                omexml.setPixelsSizeY(new PositiveInteger(java.lang.Integer.valueOf(b.SizeY)), serie);
                omexml.setPixelsSizeZ(new PositiveInteger(java.lang.Integer.valueOf(b.SizeZ)), serie);
                int samples = 1;
                if (b.isRGB)
                    samples = 3;
                omexml.setPixelsSizeC(new PositiveInteger(java.lang.Integer.valueOf(b.SizeC)), serie);
                omexml.setPixelsSizeT(new PositiveInteger(java.lang.Integer.valueOf(b.SizeT)), serie);
                if (BitConverter.IsLittleEndian)
                    omexml.setPixelsBigEndian(java.lang.Boolean.FALSE, serie);
                else
                    omexml.setPixelsBigEndian(java.lang.Boolean.TRUE, serie);
                ome.units.quantity.Length p1 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.PhysicalSizeX), ome.units.UNITS.MICROMETER);
                omexml.setPixelsPhysicalSizeX(p1, serie);
                ome.units.quantity.Length p2 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.PhysicalSizeY), ome.units.UNITS.MICROMETER);
                omexml.setPixelsPhysicalSizeY(p2, serie);
                ome.units.quantity.Length p3 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.PhysicalSizeZ), ome.units.UNITS.MICROMETER);
                omexml.setPixelsPhysicalSizeZ(p3, serie);
                ome.units.quantity.Length s1 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Volume.Location.X), ome.units.UNITS.MICROMETER);
                omexml.setStageLabelX(s1, serie);
                ome.units.quantity.Length s2 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Volume.Location.Y), ome.units.UNITS.MICROMETER);
                omexml.setStageLabelY(s2, serie);
                ome.units.quantity.Length s3 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Volume.Location.Z), ome.units.UNITS.MICROMETER);
                omexml.setStageLabelZ(s3, serie);
                omexml.setStageLabelName("StageLabel:" + serie, serie);

                for (int channel = 0; channel < b.Channels.Count; channel++)
                {
                    Channel c = b.Channels[channel];
                    for (int r = 0; r < c.range.Length; r++)
                    {
                        omexml.setChannelID("Channel:" + channel + ":" + serie, serie, channel + r);
                        omexml.setChannelSamplesPerPixel(new PositiveInteger(java.lang.Integer.valueOf(1)), serie, channel + r);
                        if (c.LightSourceWavelength != 0)
                        {
                            omexml.setChannelLightSourceSettingsID("LightSourceSettings:" + channel, serie, channel + r);
                            ome.units.quantity.Length lw = new ome.units.quantity.Length(java.lang.Double.valueOf(c.LightSourceWavelength), ome.units.UNITS.NANOMETER);
                            omexml.setChannelLightSourceSettingsWavelength(lw, serie, channel + r);
                            omexml.setChannelLightSourceSettingsAttenuation(PercentFraction.valueOf(c.LightSourceAttenuation), serie, channel + r);
                        }
                        omexml.setChannelName(c.Name, serie, channel + r);
                        if (c.Color != null)
                        {
                            ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(c.Color.Value.R, c.Color.Value.G, c.Color.Value.B, c.Color.Value.A);
                            omexml.setChannelColor(col, serie, channel + r);
                        }
                        if (c.Emission != 0)
                        {
                            ome.units.quantity.Length em = new ome.units.quantity.Length(java.lang.Double.valueOf(c.Emission), ome.units.UNITS.NANOMETER);
                            omexml.setChannelEmissionWavelength(em, serie, channel + r);
                            ome.units.quantity.Length ex = new ome.units.quantity.Length(java.lang.Double.valueOf(c.Excitation), ome.units.UNITS.NANOMETER);
                            omexml.setChannelExcitationWavelength(ex, serie, channel + r);
                        }
                        /*
                        if (c.ContrastMethod != null)
                        {
                            ome.xml.model.enums.ContrastMethod cm = (ome.xml.model.enums.ContrastMethod)Enum.Parse(typeof(ome.xml.model.enums.ContrastMethod), c.ContrastMethod);
                            omexml.setChannelContrastMethod(cm, serie, channel + r);
                        }
                        if (c.IlluminationType != null)
                        {
                            ome.xml.model.enums.IlluminationType il = (ome.xml.model.enums.IlluminationType)Enum.Parse(typeof(ome.xml.model.enums.IlluminationType), c.IlluminationType.ToUpper());
                            omexml.setChannelIlluminationType(il, serie, channel + r);
                        }
                        if (c.AcquisitionMode != null)
                        {
                            ome.xml.model.enums.AcquisitionMode am = (ome.xml.model.enums.AcquisitionMode)Enum.Parse(typeof(ome.xml.model.enums.AcquisitionMode), c.AcquisitionMode.ToUpper());
                            omexml.setChannelAcquisitionMode(am, serie, channel + r);
                        }
                        */
                        omexml.setChannelFluor(c.Fluor, serie, channel + r);
                        if (c.LightSourceIntensity != 0)
                        {
                            ome.units.quantity.Power pw = new ome.units.quantity.Power(java.lang.Double.valueOf(c.LightSourceIntensity), ome.units.UNITS.VOLT);
                            omexml.setLightEmittingDiodePower(pw, serie, channel + r);
                            omexml.setLightEmittingDiodeID(c.DiodeName, serie, channel + r);
                        }
                    }
                }

                int i = 0;
                foreach (ROI an in b.Annotations)
                {
                    if (an.roiID == "")
                        omexml.setROIID("ROI:" + i.ToString() + ":" + serie, i);
                    else
                        omexml.setROIID(an.roiID, i);
                    omexml.setROIName(an.roiName, i);
                    if (an.type == ROI.Type.Point)
                    {
                        if (an.id != "")
                            omexml.setPointID(an.id, i, serie);
                        else
                            omexml.setPointID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setPointX(java.lang.Double.valueOf(b.ToImageSpaceX(an.X)), i, serie);
                        omexml.setPointY(java.lang.Double.valueOf(b.ToImageSpaceY(an.Y)), i, serie);
                        omexml.setPointTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setPointTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setPointTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setPointText(an.Text, i, serie);
                        else
                            omexml.setPointText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setPointFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setPointStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setPointStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setPointFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Polygon || an.type == ROI.Type.Freeform)
                    {
                        if (an.id != "")
                            omexml.setPolygonID(an.id, i, serie);
                        else
                            omexml.setPolygonID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setPolygonPoints(an.PointsToString(b), i, serie);
                        omexml.setPolygonTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setPolygonTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setPolygonTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setPolygonText(an.Text, i, serie);
                        else
                            omexml.setPolygonText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setPolygonFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setPolygonStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setPolygonStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setPolygonFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Rectangle)
                    {
                        if (an.id != "")
                            omexml.setRectangleID(an.id, i, serie);
                        else
                            omexml.setRectangleID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setRectangleWidth(java.lang.Double.valueOf(b.ToImageSizeX(an.W)), i, serie);
                        omexml.setRectangleHeight(java.lang.Double.valueOf(b.ToImageSizeY(an.H)), i, serie);
                        omexml.setRectangleX(java.lang.Double.valueOf(b.ToImageSpaceX(an.Rect.X)), i, serie);
                        omexml.setRectangleY(java.lang.Double.valueOf(b.ToImageSpaceY(an.Rect.Y)), i, serie);
                        omexml.setRectangleTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setRectangleTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setRectangleTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        omexml.setRectangleText(i.ToString(), i, serie);
                        if (an.Text != "")
                            omexml.setRectangleText(an.Text, i, serie);
                        else
                            omexml.setRectangleText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setRectangleFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setRectangleStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setRectangleStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setRectangleFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Line)
                    {
                        if (an.id != "")
                            omexml.setLineID(an.id, i, serie);
                        else
                            omexml.setLineID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setLineX1(java.lang.Double.valueOf(b.ToImageSpaceX(an.GetPoint(0).X)), i, serie);
                        omexml.setLineY1(java.lang.Double.valueOf(b.ToImageSpaceY(an.GetPoint(0).Y)), i, serie);
                        omexml.setLineX2(java.lang.Double.valueOf(b.ToImageSpaceX(an.GetPoint(1).X)), i, serie);
                        omexml.setLineY2(java.lang.Double.valueOf(b.ToImageSpaceY(an.GetPoint(1).Y)), i, serie);
                        omexml.setLineTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setLineTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setLineTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setLineText(an.Text, i, serie);
                        else
                            omexml.setLineText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setLineFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setLineStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setLineStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setLineFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Ellipse)
                    {

                        if (an.id != "")
                            omexml.setEllipseID(an.id, i, serie);
                        else
                            omexml.setEllipseID("Shape:" + i + ":" + serie, i, serie);
                        //We need to change Rectangle to ellipse radius;
                        double w = (double)an.W / 2;
                        double h = (double)an.H / 2;
                        omexml.setEllipseRadiusX(java.lang.Double.valueOf(b.ToImageSizeX(w)), i, serie);
                        omexml.setEllipseRadiusY(java.lang.Double.valueOf(b.ToImageSizeY(h)), i, serie);

                        double x = an.Point.X + w;
                        double y = an.Point.Y + h;
                        omexml.setEllipseX(java.lang.Double.valueOf(b.ToImageSpaceX(x)), i, serie);
                        omexml.setEllipseY(java.lang.Double.valueOf(b.ToImageSpaceX(y)), i, serie);
                        omexml.setEllipseTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setEllipseTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setEllipseTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setEllipseText(an.Text, i, serie);
                        else
                            omexml.setEllipseText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setEllipseFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setEllipseStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setEllipseStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setEllipseFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Label)
                    {
                        if (an.id != "")
                            omexml.setLabelID(an.id, i, serie);
                        else
                            omexml.setLabelID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setLabelX(java.lang.Double.valueOf(b.ToImageSpaceX(an.Rect.X)), i, serie);
                        omexml.setLabelY(java.lang.Double.valueOf(b.ToImageSpaceY(an.Rect.Y)), i, serie);
                        omexml.setLabelTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setLabelTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setLabelTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        omexml.setLabelText(i.ToString(), i, serie);
                        if (an.Text != "")
                            omexml.setLabelText(an.Text, i, serie);
                        else
                            omexml.setLabelText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setLabelFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setLabelStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setLabelStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setLabelFillColor(colf, i, serie);
                    }
                    i++;
                }

                if (b.Buffers[0].Plane != null && planes)
                    for (int bu = 0; bu < b.Buffers.Count; bu++)
                    {
                        //Correct order of parameters.
                        if (b.Buffers[bu].Plane.Delta != 0)
                        {
                            ome.units.quantity.Time t = new ome.units.quantity.Time(java.lang.Double.valueOf(b.Buffers[bu].Plane.Delta), ome.units.UNITS.MILLISECOND);
                            omexml.setPlaneDeltaT(t, serie, bu);
                        }
                        if (b.Buffers[bu].Plane.Exposure != 0)
                        {
                            ome.units.quantity.Time et = new ome.units.quantity.Time(java.lang.Double.valueOf(b.Buffers[bu].Plane.Exposure), ome.units.UNITS.MILLISECOND);
                            omexml.setPlaneExposureTime(et, serie, bu);
                        }
                        ome.units.quantity.Length lx = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Buffers[bu].Plane.Location.X), ome.units.UNITS.MICROMETER);
                        ome.units.quantity.Length ly = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Buffers[bu].Plane.Location.Y), ome.units.UNITS.MICROMETER);
                        ome.units.quantity.Length lz = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Buffers[bu].Plane.Location.Z), ome.units.UNITS.MICROMETER);
                        omexml.setPlanePositionX(lx, serie, bu);
                        omexml.setPlanePositionY(ly, serie, bu);
                        omexml.setPlanePositionZ(lz, serie, bu);
                        omexml.setPlaneTheC(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.C)), serie, bu);
                        omexml.setPlaneTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.Z)), serie, bu);
                        omexml.setPlaneTheT(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.T)), serie, bu);

                        omexml.setTiffDataPlaneCount(new NonNegativeInteger(java.lang.Integer.valueOf(1)), serie, bu);
                        omexml.setTiffDataIFD(new NonNegativeInteger(java.lang.Integer.valueOf(bu)), serie, bu);
                        omexml.setTiffDataFirstC(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.C)), serie, bu);
                        omexml.setTiffDataFirstZ(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.Z)), serie, bu);
                        omexml.setTiffDataFirstT(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.T)), serie, bu);

                    }

            }
            writer.setMetadataRetrieve(omexml);
            f = f.Replace("\\", "/");
            writer.setId(f);
            status = "Saving OME Image Planes.";
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                ////Progress pr = new //Progress(file, "Saving");
                //pr.Show();
                BioImage b = Images.GetImage(files[i]);
                writer.setSeries(i);
                for (int bu = 0; bu < b.Buffers.Count; bu++)
                {
                    progressValue = (float)bu / (float)b.Buffers.Count;
                    writer.saveBytes(bu, b.Buffers[bu].GetSaveBytes(BitConverter.IsLittleEndian));
                    //pr.Update//ProgressF((float)bu / b.Buffers.Count);
                    //Application.DoEvents();
                }
                //pr.Close();
                //pr.Dispose();
            }
            bool stop = false;
            do
            {
                try
                {
                    writer.close();
                    stop = true;
                }
                catch (Exception e)
                {
                    //Scripting.LogLine(e.Message);
                }

            } while (!stop);
        }
        public static void SaveOMEPyramidal(BioImage[] bms, string file, string compression)
        {
            if (File.Exists(file))
                File.Delete(file);
            loci.formats.meta.IMetadata omexml = service.createOMEXMLMetadata();
            //We need to go through the images and find the ones belonging to each resolution.
            //As well we need to determine the dimensions of the tiles.
            Dictionary<double, List<BioImage>> bis = new Dictionary<double, List<BioImage>>();
            Dictionary<double, Point3D> min = new Dictionary<double, Point3D>();
            Dictionary<double, Point3D> max = new Dictionary<double, Point3D>();
            for (int i = 0; i < bms.Length; i++)
            {
                Resolution res = bms[i].Resolutions[bms[i].Resolution];
                if (bis.ContainsKey(res.PhysicalSizeX))
                {
                    bis[res.PhysicalSizeX].Add(bms[i]);
                    if (bms[i].StageSizeX < min[res.PhysicalSizeX].X || bms[i].StageSizeY < min[res.PhysicalSizeX].Y)
                    {
                        min[res.PhysicalSizeX] = bms[i].Volume.Location;
                    }
                    if (bms[i].StageSizeX > max[res.PhysicalSizeX].X || bms[i].StageSizeY > max[res.PhysicalSizeX].Y)
                    {
                        max[res.PhysicalSizeX] = bms[i].Volume.Location;
                    }
                }
                else
                {
                    bis.Add(res.PhysicalSizeX, new List<BioImage>());
                    min.Add(res.PhysicalSizeX, new Point3D(res.StageSizeX, res.StageSizeY, res.StageSizeZ));
                    max.Add(res.PhysicalSizeX, new Point3D(res.StageSizeX, res.StageSizeY, res.StageSizeZ));
                    bis[res.PhysicalSizeX].Add(bms[i]);
                }
            }
            int s = 0;
            foreach (double px in bis.Keys)
            {
                int xi = 1 + (int)Math.Ceiling((max[px].X - min[px].X) / bis[px][0].Resolutions[bis[px][0].Resolution].VolumeWidth);
                int yi = 1 + (int)Math.Ceiling((max[px].Y - min[px].Y) / bis[px][0].Resolutions[bis[px][0].Resolution].VolumeHeight);
                BioImage b = bis[px][0];
                int serie = s;
                // create OME-XML metadata store.
                omexml.setImageID("Image:" + serie, serie);
                omexml.setPixelsID("Pixels:" + serie, serie);
                omexml.setPixelsInterleaved(java.lang.Boolean.TRUE, serie);
                omexml.setPixelsDimensionOrder(ome.xml.model.enums.DimensionOrder.XYCZT, serie);
                if (b.bitsPerPixel > 8)
                    omexml.setPixelsType(ome.xml.model.enums.PixelType.UINT16, serie);
                else
                    omexml.setPixelsType(ome.xml.model.enums.PixelType.UINT8, serie);
                omexml.setPixelsSizeX(new PositiveInteger(java.lang.Integer.valueOf(b.SizeX * xi)), serie);
                omexml.setPixelsSizeY(new PositiveInteger(java.lang.Integer.valueOf(b.SizeY * yi)), serie);
                omexml.setPixelsSizeZ(new PositiveInteger(java.lang.Integer.valueOf(b.SizeZ)), serie);
                omexml.setPixelsSizeC(new PositiveInteger(java.lang.Integer.valueOf(b.SizeC)), serie);
                omexml.setPixelsSizeT(new PositiveInteger(java.lang.Integer.valueOf(b.SizeT)), serie);
                if (BitConverter.IsLittleEndian)
                    omexml.setPixelsBigEndian(java.lang.Boolean.FALSE, serie);
                else
                    omexml.setPixelsBigEndian(java.lang.Boolean.TRUE, serie);
                ome.units.quantity.Length p1 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.PhysicalSizeX), ome.units.UNITS.MICROMETER);
                omexml.setPixelsPhysicalSizeX(p1, serie);
                ome.units.quantity.Length p2 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.PhysicalSizeY), ome.units.UNITS.MICROMETER);
                omexml.setPixelsPhysicalSizeY(p2, serie);
                ome.units.quantity.Length p3 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.PhysicalSizeZ), ome.units.UNITS.MICROMETER);
                omexml.setPixelsPhysicalSizeZ(p3, serie);
                ome.units.quantity.Length s1 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Volume.Location.X), ome.units.UNITS.MICROMETER);
                omexml.setStageLabelX(s1, serie);
                ome.units.quantity.Length s2 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Volume.Location.Y), ome.units.UNITS.MICROMETER);
                omexml.setStageLabelY(s2, serie);
                ome.units.quantity.Length s3 = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Volume.Location.Z), ome.units.UNITS.MICROMETER);
                omexml.setStageLabelZ(s3, serie);
                omexml.setStageLabelName("StageLabel:" + serie, serie);

                for (int channel = 0; channel < b.Channels.Count; channel++)
                {
                    Channel c = b.Channels[channel];
                    for (int r = 0; r < c.range.Length; r++)
                    {
                        omexml.setChannelID("Channel:" + channel + ":" + serie, serie, channel + r);
                        omexml.setChannelSamplesPerPixel(new PositiveInteger(java.lang.Integer.valueOf(1)), serie, channel + r);
                        if (c.LightSourceWavelength != 0)
                        {
                            omexml.setChannelLightSourceSettingsID("LightSourceSettings:" + channel, serie, channel + r);
                            ome.units.quantity.Length lw = new ome.units.quantity.Length(java.lang.Double.valueOf(c.LightSourceWavelength), ome.units.UNITS.NANOMETER);
                            omexml.setChannelLightSourceSettingsWavelength(lw, serie, channel + r);
                            omexml.setChannelLightSourceSettingsAttenuation(PercentFraction.valueOf(c.LightSourceAttenuation), serie, channel + r);
                        }
                        omexml.setChannelName(c.Name, serie, channel + r);
                        if (c.Color != null)
                        {
                            ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(c.Color.Value.R, c.Color.Value.G, c.Color.Value.B, c.Color.Value.A);
                            omexml.setChannelColor(col, serie, channel + r);
                        }
                        if (c.Emission != 0)
                        {
                            ome.units.quantity.Length em = new ome.units.quantity.Length(java.lang.Double.valueOf(c.Emission), ome.units.UNITS.NANOMETER);
                            omexml.setChannelEmissionWavelength(em, serie, channel + r);
                            ome.units.quantity.Length ex = new ome.units.quantity.Length(java.lang.Double.valueOf(c.Excitation), ome.units.UNITS.NANOMETER);
                            omexml.setChannelExcitationWavelength(ex, serie, channel + r);
                        }
                        omexml.setChannelContrastMethod((ome.xml.model.enums.ContrastMethod)Enum.Parse(typeof(ome.xml.model.enums.ContrastMethod), c.ContrastMethod), serie, channel + r);
                        omexml.setChannelFluor(c.Fluor, serie, channel + r);
                        omexml.setChannelIlluminationType((ome.xml.model.enums.IlluminationType)Enum.Parse(typeof(ome.xml.model.enums.IlluminationType), c.IlluminationType), serie, channel + r);

                        if (c.LightSourceIntensity != 0)
                        {
                            ome.units.quantity.Power pw = new ome.units.quantity.Power(java.lang.Double.valueOf(c.LightSourceIntensity), ome.units.UNITS.VOLT);
                            omexml.setLightEmittingDiodePower(pw, serie, channel + r);
                            omexml.setLightEmittingDiodeID(c.DiodeName, serie, channel + r);
                        }
                        if (c.AcquisitionMode != null)
                            omexml.setChannelAcquisitionMode((ome.xml.model.enums.AcquisitionMode)Enum.Parse(typeof(ome.xml.model.enums.AcquisitionMode), c.AcquisitionMode), serie, channel + r);
                    }
                }

                int i = 0;
                foreach (ROI an in b.Annotations)
                {
                    if (an.roiID == "")
                        omexml.setROIID("ROI:" + i.ToString() + ":" + serie, i);
                    else
                        omexml.setROIID(an.roiID, i);
                    omexml.setROIName(an.roiName, i);
                    if (an.type == ROI.Type.Point)
                    {
                        if (an.id != "")
                            omexml.setPointID(an.id, i, serie);
                        else
                            omexml.setPointID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setPointX(java.lang.Double.valueOf(b.ToImageSpaceX(an.X)), i, serie);
                        omexml.setPointY(java.lang.Double.valueOf(b.ToImageSpaceY(an.Y)), i, serie);
                        omexml.setPointTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setPointTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setPointTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setPointText(an.Text, i, serie);
                        else
                            omexml.setPointText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setPointFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setPointStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setPointStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setPointFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Polygon || an.type == ROI.Type.Freeform)
                    {
                        if (an.id != "")
                            omexml.setPolygonID(an.id, i, serie);
                        else
                            omexml.setPolygonID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setPolygonPoints(an.PointsToString(b), i, serie);
                        omexml.setPolygonTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setPolygonTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setPolygonTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setPolygonText(an.Text, i, serie);
                        else
                            omexml.setPolygonText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setPolygonFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setPolygonStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setPolygonStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setPolygonFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Rectangle)
                    {
                        if (an.id != "")
                            omexml.setRectangleID(an.id, i, serie);
                        else
                            omexml.setRectangleID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setRectangleWidth(java.lang.Double.valueOf(b.ToImageSizeX(an.W)), i, serie);
                        omexml.setRectangleHeight(java.lang.Double.valueOf(b.ToImageSizeY(an.H)), i, serie);
                        omexml.setRectangleX(java.lang.Double.valueOf(b.ToImageSpaceX(an.Rect.X)), i, serie);
                        omexml.setRectangleY(java.lang.Double.valueOf(b.ToImageSpaceY(an.Rect.Y)), i, serie);
                        omexml.setRectangleTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setRectangleTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setRectangleTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        omexml.setRectangleText(i.ToString(), i, serie);
                        if (an.Text != "")
                            omexml.setRectangleText(an.Text, i, serie);
                        else
                            omexml.setRectangleText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setRectangleFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setRectangleStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setRectangleStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setRectangleFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Line)
                    {
                        if (an.id != "")
                            omexml.setLineID(an.id, i, serie);
                        else
                            omexml.setLineID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setLineX1(java.lang.Double.valueOf(b.ToImageSpaceX(an.GetPoint(0).X)), i, serie);
                        omexml.setLineY1(java.lang.Double.valueOf(b.ToImageSpaceY(an.GetPoint(0).Y)), i, serie);
                        omexml.setLineX2(java.lang.Double.valueOf(b.ToImageSpaceX(an.GetPoint(1).X)), i, serie);
                        omexml.setLineY2(java.lang.Double.valueOf(b.ToImageSpaceY(an.GetPoint(1).Y)), i, serie);
                        omexml.setLineTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setLineTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setLineTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setLineText(an.Text, i, serie);
                        else
                            omexml.setLineText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setLineFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setLineStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setLineStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setLineFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Ellipse)
                    {

                        if (an.id != "")
                            omexml.setEllipseID(an.id, i, serie);
                        else
                            omexml.setEllipseID("Shape:" + i + ":" + serie, i, serie);
                        //We need to change System.Drawing.Rectangle to ellipse radius;
                        double w = (double)an.W / 2;
                        double h = (double)an.H / 2;
                        omexml.setEllipseRadiusX(java.lang.Double.valueOf(b.ToImageSizeX(w)), i, serie);
                        omexml.setEllipseRadiusY(java.lang.Double.valueOf(b.ToImageSizeY(h)), i, serie);

                        double x = an.Point.X + w;
                        double y = an.Point.Y + h;
                        omexml.setEllipseX(java.lang.Double.valueOf(b.ToImageSpaceX(x)), i, serie);
                        omexml.setEllipseY(java.lang.Double.valueOf(b.ToImageSpaceX(y)), i, serie);
                        omexml.setEllipseTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setEllipseTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setEllipseTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        if (an.Text != "")
                            omexml.setEllipseText(an.Text, i, serie);
                        else
                            omexml.setEllipseText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setEllipseFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setEllipseStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setEllipseStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setEllipseFillColor(colf, i, serie);
                    }
                    else
                    if (an.type == ROI.Type.Label)
                    {
                        if (an.id != "")
                            omexml.setLabelID(an.id, i, serie);
                        else
                            omexml.setLabelID("Shape:" + i + ":" + serie, i, serie);
                        omexml.setLabelX(java.lang.Double.valueOf(b.ToImageSpaceX(an.Rect.X)), i, serie);
                        omexml.setLabelY(java.lang.Double.valueOf(b.ToImageSpaceY(an.Rect.Y)), i, serie);
                        omexml.setLabelTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.Z)), i, serie);
                        omexml.setLabelTheC(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.C)), i, serie);
                        omexml.setLabelTheT(new NonNegativeInteger(java.lang.Integer.valueOf(an.coord.T)), i, serie);
                        omexml.setLabelText(i.ToString(), i, serie);
                        if (an.Text != "")
                            omexml.setLabelText(an.Text, i, serie);
                        else
                            omexml.setLabelText(i.ToString(), i, serie);
                        ome.units.quantity.Length fl = new ome.units.quantity.Length(java.lang.Double.valueOf(an.fontSize), ome.units.UNITS.PIXEL);
                        omexml.setLabelFontSize(fl, i, serie);
                        ome.xml.model.primitives.Color col = new ome.xml.model.primitives.Color(an.strokeColor.R, an.strokeColor.G, an.strokeColor.B, an.strokeColor.A);
                        omexml.setLabelStrokeColor(col, i, serie);
                        ome.units.quantity.Length sw = new ome.units.quantity.Length(java.lang.Double.valueOf(an.strokeWidth), ome.units.UNITS.PIXEL);
                        omexml.setLabelStrokeWidth(sw, i, serie);
                        ome.xml.model.primitives.Color colf = new ome.xml.model.primitives.Color(an.fillColor.R, an.fillColor.G, an.fillColor.B, an.fillColor.A);
                        omexml.setLabelFillColor(colf, i, serie);
                    }
                    i++;
                }
                /*
                if (b.Buffers[0].Plane != null)
                    for (int bu = 0; bu < b.Buffers.Count; bu++)
                    {
                        //Correct order of parameters.
                        if (b.Buffers[bu].Plane.Delta != 0)
                        {
                            ome.units.quantity.Time t = new ome.units.quantity.Time(java.lang.Double.valueOf(b.Buffers[bu].Plane.Delta), ome.units.UNITS.MILLISECOND);
                            omexml.setPlaneDeltaT(t, bu, serie);
                        }
                        if (b.Buffers[bu].Plane.Exposure != 0)
                        {
                            ome.units.quantity.Time et = new ome.units.quantity.Time(java.lang.Double.valueOf(b.Buffers[bu].Plane.Exposure), ome.units.UNITS.MILLISECOND);
                            omexml.setPlaneExposureTime(et, bu, serie);
                        }
                        ome.units.quantity.Length lx = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Buffers[bu].Plane.Location.X), ome.units.UNITS.MICROMETER);
                        ome.units.quantity.Length ly = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Buffers[bu].Plane.Location.Y), ome.units.UNITS.MICROMETER);
                        ome.units.quantity.Length lz = new ome.units.quantity.Length(java.lang.Double.valueOf(b.Buffers[bu].Plane.Location.Z), ome.units.UNITS.MICROMETER);
                        omexml.setPlanePositionX(lx, bu, serie);
                        omexml.setPlanePositionY(ly, bu, serie);
                        omexml.setPlanePositionZ(lz, bu, serie);
                        omexml.setPlaneTheC(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.C)), bu, serie);
                        omexml.setPlaneTheZ(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.Z)), bu, serie);
                        omexml.setPlaneTheT(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.T)), bu, serie);

                        omexml.setTiffDataPlaneCount(new NonNegativeInteger(java.lang.Integer.valueOf(1)), bu, serie);
                        omexml.setTiffDataIFD(new NonNegativeInteger(java.lang.Integer.valueOf(bu)), bu, serie);
                        omexml.setTiffDataFirstC(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.C)), bu, serie);
                        omexml.setTiffDataFirstZ(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.Z)), bu, serie);
                        omexml.setTiffDataFirstT(new NonNegativeInteger(java.lang.Integer.valueOf(b.Buffers[bu].Plane.Coordinate.T)), bu, serie);

                    }
                */
                s++;
            }
            writer.setMetadataRetrieve(omexml);
            file = file.Replace("\\", "/");
            writer.setId(file);
            writer.setCompression(compression);
            s = 0;
            foreach (double px in bis.Keys)
            {
                writer.setSeries(s);
                PointD p = new PointD(max[px].X - min[px].X, max[px].Y - min[px].Y);
                for (int i = 0; i < bis[px].Count; i++)
                {
                    BioImage b = bis[px][i];
                    writer.setTileSizeX(b.SizeX);
                    writer.setTileSizeY(b.SizeY);
                    double dx = Math.Ceiling((bis[px][i].StageSizeX - min[px].X) / bis[px][i].Resolutions[bis[px][i].Resolution].VolumeWidth);
                    double dy = Math.Ceiling((bis[px][i].StageSizeY - min[px].Y) / bis[px][i].Resolutions[bis[px][i].Resolution].VolumeHeight);
                    for (int bu = 0; bu < b.Buffers.Count; bu++)
                    {
                        byte[] bt = b.Buffers[bu].GetSaveBytes(BitConverter.IsLittleEndian);
                        writer.saveBytes(bu, bt, (int)dx * b.SizeX, (int)dy * b.SizeY, b.SizeX, b.SizeY);
                    }
                    progress = (int)((float)i / bis[px].Count) * 100;
                }
            }
            bool stop = false;
            do
            {
                try
                {
                    writer.close();
                    stop = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (!stop);
        }

        /// The function "OpenOME" opens a bioimage file in the OME format and returns the first image
        /// in the series.
        /// 
        /// @param file The "file" parameter is a string that represents the file path or name of the
        /// OME file that you want to open.
        /// @param tab The "tab" parameter is a boolean value that determines whether the image is opened in a new tab.
        /// 
        /// @return The method is returning a BioImage object.
        public static BioImage OpenOME(string file, bool tab)
        {
            if (!OMESupport())
                return null;
            return OpenOMESeries(file, tab, true)[0];
        }
        /// > OpenOME(string file, int serie)
        /// 
        /// The first parameter is a string, the second is an integer
        /// 
        /// @param file the path to the file
        /// @param serie the image series to open
        /// 
        /// @return A BioImage object.
        public static BioImage OpenOME(string file, int serie)
        {
            if (!OMESupport())
                return null;
            Recorder.AddLine("Bio.BioImage.OpenOME(\"" + file + "\"," + serie + ");");
            return OpenOME(file, serie, true, false, false, 0, 0, 0, 0);
        }
        /// It takes a list of files, and creates a new BioImage object with the first file in the list.
        /// Then it loops through the rest of the files, adding the buffers from each file to the new
        /// BioImage object. Finally, it updates the coordinates of the new BioImage object, and adds it
        /// to the Images list
        /// 
        /// @param files an array of file paths
        /// @param sizeZ number of slices in the stack
        /// @param sizeC number of channels
        /// @param sizeT number of time points
        /// 
        /// @return A BioImage object.
        public static BioImage FilesToStack(string[] files, int sizeZ, int sizeC, int sizeT)
        {
            BioImage b = new BioImage(files[0]);
            for (int i = 0; i < files.Length; i++)
            {
                BioImage bb = OpenFile(files[i], false);
                b.Buffers.AddRange(bb.Buffers);
            }
            b.UpdateCoords(sizeZ, sizeC, sizeT);
            Images.AddImage(b, true);
            return b;
        }
        /// It takes a folder of images and creates a stack from them
        /// 
        /// @param path the path to the folder containing the images
        /// 
        /// @return A BioImage object.
        public static BioImage FolderToStack(string path, bool tab)
        {
            string[] files = Directory.GetFiles(path);
            BioImage b = new BioImage(files[0]);
            int z = 0;
            int c = 0;
            int t = 0;
            BioImage bb = null;
            for (int i = 0; i < files.Length; i++)
            {
                string[] st = files[i].Split('_');
                if (st.Length > 3)
                {
                    z = int.Parse(st[1].Replace("Z", ""));
                    c = int.Parse(st[2].Replace("C", ""));
                    t = int.Parse(st[3].Replace("T", ""));
                }
                bb = OpenFile(files[i], tab);
                b.Buffers.AddRange(bb.Buffers);
            }
            if (z == 0)
            {
                /*TO DO
                ImagesToStack im = new ImagesToStack();
                if (im.ShowDialog() != DialogResult.OK)
                    return null;
                b.UpdateCoords(im.SizeZ, im.SizeC, im.SizeT);
                */
            }
            else
                b.UpdateCoords(z + 1, c + 1, t + 1);
            Images.AddImage(b, tab);
            Recorder.AddLine("BioImage.FolderToStack(\"" + path + "\");");
            return b;
        }
        static int progress;
        static bool vips = false;
        /// The function "OpenVips" takes a BioImage object and an integer representing the number of
        /// pages, and adds each page of the image file to the BioImage's vipPages list using the
        /// NetVips library.
        /// 
        /// @param BioImage The BioImage parameter is an object that represents a bio image. It likely
        /// contains information about the image file, such as the file path and other metadata.
        /// @param pagecount The parameter "pagecount" represents the number of pages in the TIFF file
        /// that needs to be loaded into the "vipPages" list of the "BioImage" object.
        public static void OpenVips(BioImage b, int pagecount)
        {
            try
            {
                for (int i = 0; i < pagecount; i++)
                {
                    b.vipPages.Add(NetVips.Image.Tiffload(b.file, i));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        /// The function ExtractRegionFromTiledTiff takes a BioImage object, coordinates, width, height,
        /// and resolution as input, and returns a Bitmap object representing the extracted region from
        /// the tiled TIFF image.
        /// 
        /// @param BioImage The BioImage object represents an image file that contains multiple pages or
        /// resolutions. It contains information about the image file, such as the file path, the number
        /// of pages, and the format of each page.
        /// @param x The x-coordinate of the top-left corner of the region to extract from the tiled
        /// TIFF image.
        /// @param y The parameter "y" represents the starting y-coordinate of the region to be
        /// extracted from the tiled TIFF image.
        /// @param width The width parameter represents the width of the region to be extracted from the
        /// tiled TIFF image.
        /// @param height The height parameter represents the height of the region to be extracted from
        /// the tiled TIFF image.
        /// @param res The parameter "res" represents the resolution level of the tiled TIFF image. It
        /// is used to specify the level of detail or zoom level at which the image is being extracted.
        /// 
        /// @return The method is returning a Bitmap object.
        public static Bitmap ExtractRegionFromTiledTiff(BioImage b, int x, int y, int width, int height, int res)
        {
            try
            {
                NetVips.Image subImage = b.vipPages[res].Crop(x, y, width, height);
                if (b.vipPages[res].Format == Enums.BandFormat.Uchar)
                {
                    Bitmap bm;
                    byte[] imageData = subImage.WriteToMemory();
                    if (b.Resolutions[res].RGBChannelsCount == 3)
                        bm = new Bitmap(width, height, PixelFormat.Format24bppRgb, imageData, new ZCT(), b.file);
                    else
                        bm = new Bitmap(width, height, PixelFormat.Format8bppIndexed, imageData, new ZCT(), b.file);
                    return bm;

                }
                else if (b.vipPages[res].Format == Enums.BandFormat.Ushort)
                {
                    Bitmap bm;
                    byte[] imageData = subImage.WriteToMemory();
                    if (b.Resolutions[res].RGBChannelsCount == 3)
                        bm = new Bitmap(width, height, PixelFormat.Format24bppRgb, imageData, new ZCT(), b.file);
                    else
                        bm = new Bitmap(width, height, PixelFormat.Format8bppIndexed, imageData, new ZCT(), b.file);
                    return bm;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            return null;
        }
        /// The function "OpenOME" opens a bioimage file, with options to specify the series, whether to
        /// display it in a tab, whether to add it to existing images, whether to tile the image, and
        /// the tile size.
        /// 
        /// @param file The file parameter is a string that represents the file path or name of the OME
        /// (Open Microscopy Environment) file that you want to open.
        /// @param serie The "serie" parameter is an integer that represents the series number of the
        /// image to be opened.
        /// @param tab The "tab" parameter is a boolean value that determines whether the image should
        /// be opened in a new tab or not. If set to true, the image will be opened in a new tab. If set
        /// to false, the image will be opened in the current tab.
        /// @param addToImages The "addToImages" parameter is a boolean value that determines whether
        /// the opened image should be added to a collection of images. If set to true, the image will
        /// be added to the collection. If set to false, the image will not be added.
        /// @param tile The "tile" parameter is a boolean value that determines whether or not to tile
        /// the images. If set to true, the images will be tiled according to the specified tilex,
        /// tiley, tileSizeX, and tileSizeY parameters. If set to false, the images will not be tiled.
        /// @param tilex The parameter "tilex" is an integer that represents the starting x-coordinate
        /// of the tile.
        /// @param tiley The parameter "tiley" is used to specify the number of tiles in the y-direction
        /// when tiling the image.
        /// @param tileSizeX The tileSizeX parameter specifies the width of each tile in pixels when
        /// tiling the images.
        /// @param tileSizeY The tileSizeY parameter is the height of each tile in pixels when tiling
        /// the images. <summary>
        /// The function "OpenOME" opens a bioimage file, with options to specify the series, whether to
        public static BioImage OpenOME(string file, int serie, bool tab, bool addToImages, bool tile, int tilex, int tiley, int tileSizeX, int tileSizeY)
        {
            if (tileSizeX == 0)
                tileSizeX = 1920;
            if (tileSizeY == 0)
                tileSizeY = 1080;
            if (file == null || file == "")
                throw new InvalidDataException("File is empty or null");
            progressValue = 0;
            progFile = file;
            BioImage b = new BioImage(file);
            b.Loading = true;
            if (b.meta == null)
                b.meta = (IMetadata)((OMEXMLService)new ServiceFactory().getInstance(typeof(OMEXMLService))).createOMEXMLMetadata();
            string f = file.Replace("\\", "/");
            string cf = reader.getCurrentFile();
            if (cf != null)
                cf = cf.Replace("\\", "/");
            if (cf != f)
            {
                //status = "Opening OME Image.";
                reader.close();
                reader.setMetadataStore(b.meta);
                reader.setId(f);
            }
            //status = "Reading OME Metadata.";
            reader.setSeries(serie);
            int RGBChannelCount = reader.getRGBChannelCount();
            if (reader.getOptimalTileWidth() != reader.getSizeX())
            {
                b.ispyramidal = true;
                tile = true;
            }
            //OME reader.getBitsPerPixel(); sometimes returns incorrect bits per pixel, like when opening ImageJ images.
            //So we check the pixel type from xml metadata and if it fails we use the readers value.
            PixelFormat PixelFormat;
            try
            {
                PixelFormat = GetPixelFormat(RGBChannelCount, b.meta.getPixelsType(serie));
            }
            catch (Exception)
            {
                PixelFormat = GetPixelFormat(RGBChannelCount, reader.getBitsPerPixel());
            }

            b.id = file;
            b.file = file;
            int SizeX, SizeY;
            SizeX = reader.getSizeX();
            SizeY = reader.getSizeY();
            int SizeZ = reader.getSizeZ();
            b.sizeC = reader.getSizeC();
            b.sizeZ = reader.getSizeZ();
            b.sizeT = reader.getSizeT();
            b.littleEndian = reader.isLittleEndian();
            b.seriesCount = reader.getSeriesCount();
            b.imagesPerSeries = reader.getImageCount();
            b.bitsPerPixel = reader.getBitsPerPixel();
            b.series = serie;
            string order = reader.getDimensionOrder();

            //Lets get the channels and initialize them
            int i = 0;
            while (true)
            {
                Channel ch = new Channel(i, b.bitsPerPixel, 1);
                bool def = false;
                try
                {
                    if (b.meta.getChannelSamplesPerPixel(serie, i) != null)
                    {
                        int s = b.meta.getChannelSamplesPerPixel(serie, i).getNumberValue().intValue();
                        ch.SamplesPerPixel = s;
                        def = true;
                    }
                    if (b.meta.getChannelName(serie, i) != null)
                        ch.Name = b.meta.getChannelName(serie, i);
                    if (b.meta.getChannelAcquisitionMode(serie, i) != null)
                        ch.AcquisitionMode = b.meta.getChannelAcquisitionMode(serie, i).ToString();
                    if (b.meta.getChannelID(serie, i) != null)
                        ch.info.ID = b.meta.getChannelID(serie, i);
                    if (b.meta.getChannelFluor(serie, i) != null)
                        ch.Fluor = b.meta.getChannelFluor(serie, i);
                    if (b.meta.getChannelColor(serie, i) != null)
                    {
                        ome.xml.model.primitives.Color cc = b.meta.getChannelColor(serie, i);
                        ch.Color = Color.FromArgb(cc.getRed(), cc.getGreen(), cc.getBlue());
                    }
                    if (b.meta.getChannelIlluminationType(serie, i) != null)
                        ch.IlluminationType = b.meta.getChannelIlluminationType(serie, i).ToString();
                    if (b.meta.getChannelContrastMethod(serie, i) != null)
                        ch.ContrastMethod = b.meta.getChannelContrastMethod(serie, i).ToString();
                    if (b.meta.getChannelEmissionWavelength(serie, i) != null)
                        ch.Emission = b.meta.getChannelEmissionWavelength(serie, i).value().intValue();
                    if (b.meta.getChannelExcitationWavelength(serie, i) != null)
                        ch.Excitation = b.meta.getChannelExcitationWavelength(serie, i).value().intValue();
                    if (b.meta.getLightEmittingDiodePower(serie, i) != null)
                        ch.LightSourceIntensity = b.meta.getLightEmittingDiodePower(serie, i).value().doubleValue();
                    if (b.meta.getLightEmittingDiodeID(serie, i) != null)
                        ch.DiodeName = b.meta.getLightEmittingDiodeID(serie, i);
                    if (b.meta.getChannelLightSourceSettingsAttenuation(serie, i) != null)
                        ch.LightSourceAttenuation = b.meta.getChannelLightSourceSettingsAttenuation(serie, i).toString();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //If this channel is not defined we have loaded all the channels in the file.
                if (!def)
                    break;
                else
                    b.Channels.Add(ch);
                if (i == 0)
                {
                    b.rgbChannels[0] = 0;
                }
                else
                if (i == 1)
                {
                    b.rgbChannels[1] = 1;
                }
                else
                if (i == 2)
                {
                    b.rgbChannels[2] = 2;
                }
                i++;
            }

            //If the file doens't have channels we initialize them.
            if (b.Channels.Count == 0)
            {
                b.Channels.Add(new Channel(0, b.bitsPerPixel, RGBChannelCount));
            }

            //Bioformats gives a size of 3 for C when saved in ImageJ as RGB. We need to correct for this as C should be 1 for RGB.
            if ((PixelFormat == PixelFormat.Format24bppRgb || PixelFormat == PixelFormat.Format32bppArgb || PixelFormat == PixelFormat.Format48bppRgb) && b.SizeC == 3)
            {
                b.sizeC = 1;
            }
            b.Coords = new int[b.SizeZ, b.SizeC, b.SizeT];

            int resc = reader.getResolutionCount();
            for (int s = 0; s < b.seriesCount; s++)
            {
                reader.setSeries(s);
                for (int r = 0; r < reader.getResolutionCount(); r++)
                {
                    Resolution res = new Resolution();
                    try
                    {
                        int rgbc = reader.getRGBChannelCount();
                        int bps = reader.getBitsPerPixel();
                        PixelFormat px;
                        try
                        {
                            px = GetPixelFormat(rgbc, b.meta.getPixelsType(s));
                        }
                        catch (Exception)
                        {
                            px = GetPixelFormat(rgbc, bps);
                        }
                        res.PixelFormat = px;
                        res.SizeX = reader.getSizeX();
                        res.SizeY = reader.getSizeY();
                        if (b.meta.getPixelsPhysicalSizeX(s) != null)
                        {
                            res.PhysicalSizeX = b.meta.getPixelsPhysicalSizeX(s).value().doubleValue();
                        }
                        else
                            res.PhysicalSizeX = (96 / 2.54) / 1000;
                        if (b.meta.getPixelsPhysicalSizeY(s) != null)
                        {
                            res.PhysicalSizeY = b.meta.getPixelsPhysicalSizeY(s).value().doubleValue();
                        }
                        else
                            res.PhysicalSizeY = (96 / 2.54) / 1000;

                        if (b.meta.getStageLabelX(s) != null)
                            res.StageSizeX = b.meta.getStageLabelX(s).value().doubleValue();
                        if (b.meta.getStageLabelY(s) != null)
                            res.StageSizeY = b.meta.getStageLabelY(s).value().doubleValue();
                        if (b.meta.getStageLabelZ(s) != null)
                            res.StageSizeZ = b.meta.getStageLabelZ(s).value().doubleValue();
                        else
                            res.StageSizeZ = 1;
                        if (b.meta.getPixelsPhysicalSizeZ(s) != null)
                        {
                            res.PhysicalSizeZ = b.meta.getPixelsPhysicalSizeZ(s).value().doubleValue();
                        }
                        else
                        {
                            res.PhysicalSizeZ = 1;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Stage Coordinates. PhysicalSize:(" + res.PhysicalSizeX + "," + res.PhysicalSizeY + "," + res.PhysicalSizeZ + ")");
                    }
                    b.Resolutions.Add(res);
                }
            }
            b.Volume = new VolumeD(new Point3D(b.StageSizeX, b.StageSizeY, b.StageSizeZ), new Point3D(b.PhysicalSizeX * SizeX, b.PhysicalSizeY * SizeY, b.PhysicalSizeZ * SizeZ));

            int rc = b.meta.getROICount();
            for (int im = 0; im < rc; im++)
            {
                string roiID = b.meta.getROIID(im);
                string roiName = b.meta.getROIName(im);
                ZCT co = new ZCT(0, 0, 0);
                int scount = 1;
                try
                {
                    scount = b.meta.getShapeCount(im);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
                for (int sc = 0; sc < scount; sc++)
                {
                    string type = b.meta.getShapeType(im, sc);
                    ROI an = new ROI();
                    an.roiID = roiID;
                    an.roiName = roiName;
                    an.shapeIndex = sc;
                    if (type == "Point")
                    {
                        an.type = ROI.Type.Point;
                        an.id = b.meta.getPointID(im, sc);
                        double dx = b.meta.getPointX(im, sc).doubleValue();
                        double dy = b.meta.getPointY(im, sc).doubleValue();
                        an.AddPoint(b.ToStageSpace(new PointD(dx, dy)));
                        an.coord = new ZCT();
                        ome.xml.model.primitives.NonNegativeInteger nz = b.meta.getPointTheZ(im, sc);
                        if (nz != null)
                            an.coord.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = b.meta.getPointTheC(im, sc);
                        if (nc != null)
                            an.coord.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = b.meta.getPointTheT(im, sc);
                        if (nt != null)
                            an.coord.T = nt.getNumberValue().intValue();
                        an.Text = b.meta.getPointText(im, sc);
                        ome.units.quantity.Length fl = b.meta.getPointFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        ome.xml.model.enums.FontFamily ff = b.meta.getPointFontFamily(im, sc);
                        if (ff != null)
                            an.family = ff.name();
                        ome.xml.model.primitives.Color col = b.meta.getPointStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = b.meta.getPointStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = b.meta.getPointStrokeColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                        continue;
                    }
                    else
                    if (type == "Line")
                    {
                        an.type = ROI.Type.Line;
                        an.id = b.meta.getLineID(im, sc);
                        double px1 = b.meta.getLineX1(im, sc).doubleValue();
                        double py1 = b.meta.getLineY1(im, sc).doubleValue();
                        double px2 = b.meta.getLineX2(im, sc).doubleValue();
                        double py2 = b.meta.getLineY2(im, sc).doubleValue();
                        an.AddPoint(b.ToStageSpace(new PointD(px1, py1)));
                        an.AddPoint(b.ToStageSpace(new PointD(px2, py2)));
                        ome.xml.model.primitives.NonNegativeInteger nz = b.meta.getLineTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = b.meta.getLineTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = b.meta.getLineTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = b.meta.getLineText(im, sc);
                        ome.units.quantity.Length fl = b.meta.getLineFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        ome.xml.model.enums.FontFamily ff = b.meta.getLineFontFamily(im, sc);
                        if (ff != null)
                            an.family = ff.name();
                        ome.xml.model.primitives.Color col = b.meta.getLineStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = b.meta.getLineStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = b.meta.getLineFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                        continue;
                    }
                    else
                    if (type == "Rectangle")
                    {
                        an.type = ROI.Type.Rectangle;
                        an.id = b.meta.getRectangleID(im, sc);
                        double px = b.meta.getRectangleX(im, sc).doubleValue();
                        double py = b.meta.getRectangleY(im, sc).doubleValue();
                        double pw = b.meta.getRectangleWidth(im, sc).doubleValue();
                        double ph = b.meta.getRectangleHeight(im, sc).doubleValue();
                        an.Rect = b.ToStageSpace(new RectangleD(px, py, pw, ph));
                        ome.xml.model.primitives.NonNegativeInteger nz = b.meta.getRectangleTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = b.meta.getRectangleTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = b.meta.getRectangleTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;

                        an.Text = b.meta.getRectangleText(im, sc);
                        ome.units.quantity.Length fl = b.meta.getRectangleFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        ome.xml.model.enums.FontFamily ff = b.meta.getRectangleFontFamily(im, sc);
                        if (ff != null)
                            an.family = ff.name();
                        ome.xml.model.primitives.Color col = b.meta.getRectangleStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = b.meta.getRectangleStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = b.meta.getRectangleFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                        ome.xml.model.enums.FillRule fr = b.meta.getRectangleFillRule(im, sc);
                        continue;
                    }
                    else
                    if (type == "Ellipse")
                    {
                        an.type = ROI.Type.Ellipse;
                        an.id = b.meta.getEllipseID(im, sc);
                        double px = b.meta.getEllipseX(im, sc).doubleValue();
                        double py = b.meta.getEllipseY(im, sc).doubleValue();
                        double ew = b.meta.getEllipseRadiusX(im, sc).doubleValue();
                        double eh = b.meta.getEllipseRadiusY(im, sc).doubleValue();
                        //We convert the ellipse radius to Rectangle
                        double w = ew * 2;
                        double h = eh * 2;
                        double x = px - ew;
                        double y = py - eh;
                        an.Rect = b.ToStageSpace(new RectangleD(x, y, w, h));
                        ome.xml.model.primitives.NonNegativeInteger nz = b.meta.getEllipseTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = b.meta.getEllipseTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = b.meta.getEllipseTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = b.meta.getEllipseText(im, sc);
                        ome.units.quantity.Length fl = b.meta.getEllipseFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        ome.xml.model.enums.FontFamily ff = b.meta.getEllipseFontFamily(im, sc);
                        if (ff != null)
                            an.family = ff.name();
                        ome.xml.model.primitives.Color col = b.meta.getEllipseStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = b.meta.getEllipseStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = b.meta.getEllipseFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Polygon")
                    {
                        an.type = ROI.Type.Polygon;
                        an.id = b.meta.getPolygonID(im, sc);
                        an.closed = true;
                        string pxs = b.meta.getPolygonPoints(im, sc);
                        PointD[] pts = an.stringToPoints(pxs);
                        pts = b.ToStageSpace(pts);
                        if (pts.Length > 100)
                        {
                            an.type = ROI.Type.Freeform;
                        }
                        an.AddPoints(pts);
                        ome.xml.model.primitives.NonNegativeInteger nz = b.meta.getPolygonTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = b.meta.getPolygonTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = b.meta.getPolygonTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = b.meta.getPolygonText(im, sc);
                        ome.units.quantity.Length fl = b.meta.getPolygonFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        ome.xml.model.enums.FontFamily ff = b.meta.getPolygonFontFamily(im, sc);
                        if (ff != null)
                            an.family = ff.name();
                        ome.xml.model.primitives.Color col = b.meta.getPolygonStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = b.meta.getPolygonStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = b.meta.getPolygonFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Polyline")
                    {
                        an.type = ROI.Type.Polyline;
                        an.id = b.meta.getPolylineID(im, sc);
                        string pxs = b.meta.getPolylinePoints(im, sc);
                        PointD[] pts = an.stringToPoints(pxs);
                        for (int pi = 0; pi < pts.Length; pi++)
                        {
                            pts[pi] = b.ToStageSpace(pts[pi]);
                        }
                        an.AddPoints(an.stringToPoints(pxs));
                        ome.xml.model.primitives.NonNegativeInteger nz = b.meta.getPolylineTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = b.meta.getPolylineTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = b.meta.getPolylineTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = b.meta.getPolylineText(im, sc);
                        ome.units.quantity.Length fl = b.meta.getPolylineFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        ome.xml.model.enums.FontFamily ff = b.meta.getPolylineFontFamily(im, sc);
                        if (ff != null)
                            an.family = ff.name();
                        ome.xml.model.primitives.Color col = b.meta.getPolylineStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = b.meta.getPolylineStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = b.meta.getPolylineFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Label")
                    {
                        an.type = ROI.Type.Label;
                        an.id = b.meta.getLabelID(im, sc);

                        ome.xml.model.primitives.NonNegativeInteger nz = b.meta.getLabelTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = b.meta.getLabelTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = b.meta.getLabelTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;

                        ome.units.quantity.Length fl = b.meta.getLabelFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        ome.xml.model.enums.FontFamily ff = b.meta.getLabelFontFamily(im, sc);
                        if (ff != null)
                            an.family = ff.name();
                        ome.xml.model.primitives.Color col = b.meta.getLabelStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = b.meta.getLabelStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = b.meta.getLabelFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                        PointD p = new PointD(b.meta.getLabelX(im, sc).doubleValue(), b.meta.getLabelY(im, sc).doubleValue());
                        an.AddPoint(b.ToStageSpace(p));
                        an.Text = b.meta.getLabelText(im, sc);
                    }
                    if (b.Volume.Intersects(new PointD(an.BoundingBox.X, an.BoundingBox.Y)))
                        b.Annotations.Add(an);
                }
            }

            List<string> serFiles = new List<string>();
            serFiles.AddRange(reader.getSeriesUsedFiles());

            b.Buffers = new List<Bitmap>();
            // read the image data bytes
            int pages = reader.getImageCount();
            bool inter = reader.isInterleaved();
            int z = 0;
            int c = 0;
            int t = 0;
            //status = "Reading OME Image Planes";
            if (!tile)
                for (int p = 0; p < pages; p++)
                {
                    Bitmap bf;
                    progressValue = (float)p / (float)pages;
                    byte[] bytes = reader.openBytes(p);
                    bf = new Bitmap(file, SizeX, SizeY, PixelFormat, bytes, new ZCT(z, c, t), p, null, b.littleEndian, inter);
                    if (bf.isRGB && !inter && b.littleEndian)
                        bf.SwitchRedBlue();
                    b.Buffers.Add(bf);
                }
            else
            {
                b.Buffers.Add(GetTile(b, new ZCT(z, c, t), serie, tilex, tiley, tileSizeX, tileSizeY));
                Statistics.CalcStatistics(b.Buffers.Last());
            }
            int pls;
            try
            {
                pls = b.meta.getPlaneCount(serie);
            }
            catch (Exception)
            {
                pls = 0;
            }
            if (pls == b.Buffers.Count)
                for (int bi = 0; bi < b.Buffers.Count; bi++)
                {
                    Plane pl = new Plane();
                    pl.Coordinate = new ZCT();
                    double px = 0; double py = 0; double pz = 0;
                    if (b.meta.getPlanePositionX(serie, bi) != null)
                        px = b.meta.getPlanePositionX(serie, bi).value().doubleValue();
                    if (b.meta.getPlanePositionY(serie, bi) != null)
                        py = b.meta.getPlanePositionY(serie, bi).value().doubleValue();
                    if (b.meta.getPlanePositionZ(serie, bi) != null)
                        pz = b.meta.getPlanePositionZ(serie, bi).value().doubleValue();
                    pl.Location = new AForge.Point3D(px, py, pz);
                    int cc = 0; int zc = 0; int tc = 0;
                    if (b.meta.getPlaneTheC(serie, bi) != null)
                        cc = b.meta.getPlaneTheC(serie, bi).getNumberValue().intValue();
                    if (b.meta.getPlaneTheC(serie, bi) != null)
                        zc = b.meta.getPlaneTheZ(serie, bi).getNumberValue().intValue();
                    if (b.meta.getPlaneTheC(serie, bi) != null)
                        tc = b.meta.getPlaneTheT(serie, bi).getNumberValue().intValue();
                    pl.Coordinate = new ZCT(zc, cc, tc);
                    if (b.meta.getPlaneDeltaT(serie, bi) != null)
                        pl.Delta = b.meta.getPlaneDeltaT(serie, bi).value().doubleValue();
                    if (b.meta.getPlaneExposureTime(serie, bi) != null)
                        pl.Exposure = b.meta.getPlaneExposureTime(serie, bi).value().doubleValue();
                    b.Buffers[bi].Plane = pl;
                }

            b.UpdateCoords(b.SizeZ, b.SizeC, b.SizeT, order);
            //We wait for histogram image statistics calculation
            do
            {
                Thread.Sleep(50);
            } while (b.Buffers[b.Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            AutoThreshold(b, true);
            if (b.bitsPerPixel > 8)
                b.StackThreshold(true);
            else
                b.StackThreshold(false);
            if (addToImages)
                Images.AddImage(b, tab);
            //We use a try block to close the reader as sometimes this will cause an error.
            bool stop = false;
            do
            {
                try
                {
                    reader.close();
                    stop = true;
                }
                catch (Exception)
                {

                }
            } while (!stop);
            b.Loading = false;
            return b;
        }
        public ImageReader imRead;
        public Tiff tifRead;
        static Bitmap bm;
        /// It reads a tile from a file, and returns a bitmap
        /// 
        /// @param BioImage This is a class that contains the image file name, the image reader, and the
        /// coordinates of the image.
        /// @param ZCT Z, C, T
        /// @param serie the series number (0-based)
        /// @param tilex the x coordinate of the tile
        /// @param tiley the y coordinate of the tile
        /// @param tileSizeX the width of the tile
        /// @param tileSizeY the height of the tile
        /// 
        /// @return A Bitmap object.
        public static Bitmap GetTile(BioImage b, ZCT coord, int serie, int tilex, int tiley, int tileSizeX, int tileSizeY)
        {
            if ((!OMESupport() && b.file.EndsWith("ome.tif") && vips) || (b.file.EndsWith(".tif") && vips))
            {
                //We can get a tile faster with libvips rather than bioformats.
                //and incase we are on mac we can't use bioformats due to IKVM not supporting mac.
                return ExtractRegionFromTiledTiff(b, tilex, tiley, tileSizeX, tileSizeY, serie);
            }
            string curfile = reader.getCurrentFile();
            if (curfile == null)
            {
                b.meta = (IMetadata)((OMEXMLService)factory.getInstance(typeof(OMEXMLService))).createOMEXMLMetadata();
                reader.close();
                reader.setMetadataStore(b.meta);
                Console.WriteLine(b.file);
                reader.setId(b.file);
            }
            else
            {
                string fi = b.file.Replace("\\", "/");
                string cf = curfile.Replace("\\", "/");
                if (cf != fi)
                {
                    reader.close();
                    b.meta = (IMetadata)((OMEXMLService)factory.getInstance(typeof(OMEXMLService))).createOMEXMLMetadata();
                    reader.setMetadataStore(b.meta);
                    reader.setId(b.file);
                }
            }
            if (reader.getSeries() != serie)
                reader.setSeries(serie);
            int SizeX = reader.getSizeX();
            int SizeY = reader.getSizeY();
            int p = b.Coords[coord.Z, coord.C, coord.T];
            bool littleEndian = reader.isLittleEndian();
            PixelFormat PixelFormat = b.Resolutions[serie].PixelFormat;
            if (tilex < 0)
                tilex = 0;
            if (tiley < 0)
                tiley = 0;
            if (tilex >= SizeX)
                tilex = SizeX;
            if (tiley >= SizeY)
                tiley = SizeY;
            int sx = tileSizeX;
            if (tilex + tileSizeX > SizeX)
                sx -= (tilex + tileSizeX) - (SizeX);
            int sy = tileSizeY;
            if (tiley + tileSizeY > SizeY)
                sy -= (tiley + tileSizeY) - (SizeY);
            //For some reason calling openBytes with 1x1px area causes an exception. 
            if (sx <= 1)
                return null;
            if (sy <= 1)
                return null;
            byte[] bytesr = reader.openBytes(b.Coords[coord.Z, coord.C, coord.T], tilex, tiley, sx, sy);
            bool interleaved = reader.isInterleaved();
            if (bm != null)
                bm.Dispose();
            bm = new Bitmap(b.file, sx, sy, PixelFormat, bytesr, coord, p, null, littleEndian, interleaved);
            if (bm.isRGB && !interleaved && littleEndian)
                bm.SwitchRedBlue();
            return bm;
        }
        /// This function sets the minimum and maximum values of the image to the minimum and maximum
        /// values of the stack
        /// 
        /// @param bit16 true = 16 bit, false = 8 bit
        public void StackThreshold(bool bit16)
        {
            if (bit16)
            {
                for (int ch = 0; ch < Channels.Count; ch++)
                {
                    for (int i = 0; i < Channels[ch].range.Length; i++)
                    {
                        Channels[ch].range[i].Min = (int)Channels[ch].stats[i].StackMin;
                        Channels[ch].range[i].Max = (int)Channels[ch].stats[i].StackMax;
                    }
                    Channels[ch].BitsPerPixel = 16;
                }
                bitsPerPixel = 16;
            }
            else
            {
                for (int ch = 0; ch < Channels.Count; ch++)
                {
                    for (int i = 0; i < Channels[ch].range.Length; i++)
                    {
                        Channels[ch].range[i].Min = (int)Channels[ch].stats[i].StackMin;
                        Channels[ch].range[i].Max = (int)Channels[ch].stats[i].StackMax;
                    }
                    Channels[ch].BitsPerPixel = 8;
                }
                bitsPerPixel = 8;
            }
        }
        /// > If the number is less than or equal to 255, then it's 8 bits. If it's less than or equal
        /// to 512, then it's 9 bits. If it's less than or equal to 1023, then it's 10 bits. If it's
        /// less than or equal to 2047, then it's 11 bits. If it's less than or equal to 4095, then it's
        /// 12 bits. If it's less than or equal to 8191, then it's 13 bits. If it's less than or equal
        /// to 16383, then it's 14 bits. If it's less than or equal to 32767, then it's 15 bits. If it's
        /// less than or equal to 65535, then it's 16 bits
        /// 
        /// @param bt The number of bits per pixel.
        /// 
        /// @return The number of bits per pixel.
        public static int GetBitsPerPixel(int bt)
        {
            if (bt <= 255)
                return 8;
            if (bt <= 512)
                return 9;
            else if (bt <= 1023)
                return 10;
            else if (bt <= 2047)
                return 11;
            else if (bt <= 4095)
                return 12;
            else if (bt <= 8191)
                return 13;
            else if (bt <= 16383)
                return 14;
            else if (bt <= 32767)
                return 15;
            else
                return 16;
        }
        /// It returns the maximum value of a bit.
        /// 
        /// @param bt bit depth
        /// 
        /// @return The maximum value of a bit.
        public static int GetBitMaxValue(int bt)
        {
            if (bt == 8)
                return 255;
            if (bt == 9)
                return 512;
            else if (bt == 10)
                return 1023;
            else if (bt == 11)
                return 2047;
            else if (bt == 12)
                return 4095;
            else if (bt == 13)
                return 8191;
            else if (bt == 14)
                return 16383;
            else if (bt == 15)
                return 32767;
            else
                return 65535;
        }
        /// If the bits per pixel is 8, then the pixel format is either 8bppIndexed, 24bppRgb, or
        /// 32bppArgb. If the bits per pixel is 16, then the pixel format is either 16bppGrayScale or
        /// 48bppRgb
        /// 
        /// @param rgbChannelCount The number of channels in the image. For example, a grayscale image
        /// has 1 channel, a color image has 3 channels (red, green, blue).
        /// @param bitsPerPixel 8 or 16
        /// 
        /// @return The PixelFormat of the image.
        public static PixelFormat GetPixelFormat(int rgbChannelCount, int bitsPerPixel)
        {
            if (bitsPerPixel == 8)
            {
                if (rgbChannelCount == 1)
                    return PixelFormat.Format8bppIndexed;
                else if (rgbChannelCount == 3)
                    return PixelFormat.Format24bppRgb;
                else if (rgbChannelCount == 4)
                    return PixelFormat.Format32bppArgb;
            }
            else
            {
                if (rgbChannelCount == 1)
                    return PixelFormat.Format16bppGrayScale;
                if (rgbChannelCount == 3)
                    return PixelFormat.Format48bppRgb;
            }
            throw new NotSupportedException("Not supported pixel format.");
        }
        /// The function returns the appropriate PixelFormat based on the number of RGB channels and the
        /// pixel type.
        /// 
        /// @param rgbChannelCount The `rgbChannelCount` parameter represents the number of channels in
        /// the RGB color model. It can have a value of either 1 (for grayscale images) or 3 (for RGB
        /// color images).
        /// @param px The parameter "px" is of type ome.xml.model.enums.PixelType. It represents the
        /// pixel type of the image, such as INT8, UINT8, INT16, or UINT16.
        /// 
        /// @return The method returns a PixelFormat value based on the input parameters.
        public static PixelFormat GetPixelFormat(int rgbChannelCount, ome.xml.model.enums.PixelType px)
        {
            if (rgbChannelCount == 1)
            {
                if (px == ome.xml.model.enums.PixelType.INT8 || px == ome.xml.model.enums.PixelType.UINT8)
                    return PixelFormat.Format8bppIndexed;
                else if (px == ome.xml.model.enums.PixelType.INT16 || px == ome.xml.model.enums.PixelType.UINT16)
                    return PixelFormat.Format16bppGrayScale;
            }
            else
            {
                if (px == ome.xml.model.enums.PixelType.INT8 || px == ome.xml.model.enums.PixelType.UINT8)
                    return PixelFormat.Format24bppRgb;
                else if (px == ome.xml.model.enums.PixelType.INT16 || px == ome.xml.model.enums.PixelType.UINT16)
                    return PixelFormat.Format48bppRgb;
            }
            throw new NotSupportedException("Not supported pixel format.");
        }
        /// It opens a file, checks if it's tiled, if it is, it opens it as a tiled image, if not, it
        /// opens it as a normal image
        /// 
        /// @param file the file path
        /// @param tab open in new tab
        /// @param addToImages add to images list.
        /// @return An array of BioImage objects.
        public static BioImage[] OpenOMESeries(string file, bool tab, bool addToImages)
        {
            //We wait incase OME has not initialized yet.
            if (!initialized)
                do
                {
                    Thread.Sleep(100);
                    //Application.DoEvents();
                } while (!Initialized);
            var meta = (IMetadata)((OMEXMLService)new ServiceFactory().getInstance(typeof(OMEXMLService))).createOMEXMLMetadata();
            reader.setMetadataStore((MetadataStore)meta);
            file = file.Replace("\\", "/");
            try
            {
                if (reader.getCurrentFile() != file)
                {
                    status = "Opening OME Image.";
                    file = file.Replace("\\", "/");
                    reader.setId(file);
                }
            }
            catch (Exception e)
            {
                Scripting.LogLine(e.Message);
                return null;
            }

            bool tile = false;
            if (reader.getOptimalTileWidth() != reader.getSizeX())
                tile = true;
            int count = reader.getSeriesCount();
            BioImage[] bs = null;
            if (tile)
            {
                bs = new BioImage[1];
                bs[0] = OpenOME(file, 0, tab, addToImages, true, 0, 0, 1920, 1080);
                bs[0].isPyramidal = true;
                Images.AddImage(bs[0], tab);
                return bs;
            }
            else
                bs = new BioImage[count];
            reader.close();
            for (int i = 0; i < count; i++)
            {
                bs[i] = OpenOME(file, i, tab, addToImages, false, 0, 0, 0, 0);
                if (bs[i] == null)
                    return null;
            }
            return bs;
        }
        /// It opens a file in a new thread.
        /// 
        /// @param file The file to open
        public static void OpenAsync(string file)
        {
            //Due to a GTKSharp error we can't open async on windows.
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                Thread t = new Thread(OpenThread);
                t.Name = file;
                t.Start();
            }
            else
            {
                OpenFile(file);
            }
        }
        /// It opens a file asynchronously
        /// 
        /// @param files The file(s) to open.
        public static void OpenAsync(string[] files)
        {
            foreach (string file in files)
            {
                OpenAsync(file);
            }
        }
        /// It opens a file in a new thread.
        /// 
        /// @param file The file to open
        public static void AddAsync(string file)
        {
            Thread t = new Thread(OpenThread);
            t.Name = file;
            t.Start();
        }
        /// It opens a file asynchronously
        /// 
        /// @param files The file(s) to open.
        public static void AddAsync(string[] files)
        {
            foreach (string file in files)
            {
                OpenAsync(file);
            }
        }
        /// It opens a file
        /// 
        /// @param file The file to open.
        public static void Open(string file)
        {
            OpenFile(file);
        }
        /// It opens a file
        /// 
        /// @param files The files to open.
        public static void Open(string[] files)
        {
            foreach (string file in files)
            {
                Open(file);
            }
        }
        /// It takes a list of files, opens them, and then combines them into a single BioImage object
        /// 
        /// @param files an array of file paths
        /// 
        /// @return A BioImage object.
        public static BioImage ImagesToStack(string[] files, bool tab)
        {
            BioImage[] bs = new BioImage[files.Length];
            int z = 0;
            int c = 0;
            int t = 0;
            for (int i = 0; i < files.Length; i++)
            {
                string str = Path.GetFileNameWithoutExtension(files[i]);
                str = str.Replace(".ome", "");
                string[] st = str.Split('_');
                if (st.Length > 3)
                {
                    z = int.Parse(st[1].Replace("Z", ""));
                    c = int.Parse(st[2].Replace("C", ""));
                    t = int.Parse(st[3].Replace("T", ""));
                }
                if (i == 0)
                    bs[0] = OpenOME(files[i], tab);
                else
                {
                    bs[i] = OpenFile(files[i], 0, tab, false);
                }
            }
            BioImage b = BioImage.CopyInfo(bs[0], true, true);
            for (int i = 0; i < files.Length; i++)
            {
                for (int bc = 0; bc < bs[i].Buffers.Count; bc++)
                {
                    b.Buffers.Add(bs[i].Buffers[bc]);
                }
            }
            b.UpdateCoords(z + 1, c + 1, t + 1);
            b.Volume = new VolumeD(bs[0].Volume.Location, new Point3D(bs[0].SizeX * bs[0].PhysicalSizeX, bs[0].SizeY * bs[0].PhysicalSizeY, (z + 1) * bs[0].PhysicalSizeZ));
            return b;
        }
        /// The function takes a BioImage object, opens the file, and returns a updated BioImage object
        /// 
        /// @param BioImage This is the class that contains the image data.
        public static void Update(BioImage b)
        {
            b = OpenFile(b.file);
        }
        /// > Update() is a function that calls the Update() function of the parent class
        public void Update()
        {
            Update(this);
        }
        /* Creating a list of strings called openfile. */
        private static List<string> openfile = new List<string>();
        /// The function OpenThread() is a private static function that takes no parameters. It creates
        /// a string variable called file and assigns it the value of the current thread's name. It then
        /// calls the function OpenFile() and passes it the file variable
        private static void OpenThread()
        {
            OpenFile(Thread.CurrentThread.Name);
        }
        /// The function "AddThread" opens a file with the name of the current thread.
        private static void AddThread()
        {
            OpenFile(Thread.CurrentThread.Name, false);
        }
        /// It adds the file and ID to a list, then starts a new thread to save the file
        /// 
        /// @param file The file to save to
        /// @param ID The ID of the file
        public static void SaveAsync(string file, string ID)
        {
            saveid.Add(file);
            savefile.Add(ID);
            Thread t = new Thread(Save);
            t.Start();
        }
        /// It takes a file and an ID and saves the file to the ID
        /// 
        /// @param file The file to save the data to.
        /// @param ID The ID of the user
        public static void Save(string file, string ID)
        {
            SaveFile(file, ID);
        }
        private static List<string> savefile = new List<string>();
        private static List<string> saveid = new List<string>();
        /// It saves all the files in the savefile list to the saveid list.
        private static void Save()
        {
            List<string> sts = new List<string>();
            for (int i = 0; i < savefile.Count; i++)
            {
                SaveAsync(savefile[i], saveid[i]);
                sts.Add(savefile[i]);
            }
            for (int i = 0; i < sts.Count; i++)
            {
                savefile.Remove(sts[i]);
                saveid.Remove(sts[i]);
            }
        }

        private static List<string> openOMEfile = new List<string>();
        /// It takes a string array of file paths, adds them to a list, and starts a new thread to open
        /// the files
        /// 
        /// @param file The file path to the OME file.
        public static void OpenOMEThread(string[] file)
        {
            if (!OMESupport())
                return;
            openOMEfile.AddRange(file);
            Thread t = new Thread(OpenOME);
            t.Start();
        }
        /// It opens the OME file.
        private static void OpenOME()
        {
            if (!OMESupport())
                return;
            foreach (string f in openOMEfile)
            {
                OpenOME(f, true);
            }
            openOMEfile.Clear();
        }
        /// It creates a new thread and starts it. 
        /// 
        /// The thread is a function called SaveOME. 
        /// 
        /// The function SaveOME uses the global variables saveOMEID and saveOMEfile. 
        /// 
        /// The global variables saveOMEID and saveOMEfile are set in the function SaveOMEThread. 
        /// 
        /// The function SaveOMEThread takes two parameters: file and ID. 
        /// 
        /// The function SaveOMEThread sets the global variables saveOMEID and saveOMEfile to the values
        /// of the parameters file and ID. 
        /// 
        /// The function SaveOMEThread then creates a new thread and starts it. 
        /// 
        /// @param file The file to save to
        /// @param ID The ID of the OME file to save.
        public static void SaveOMEThread(string file, string ID)
        {
            if (!OMESupport())
                return;
            saveOMEID = ID;
            saveOMEfile = file;
            Thread t = new Thread(SaveOME);
            t.Start();
        }
        private static string saveOMEfile;
        private static string saveOMEID;
        /// It takes the file name and the ID of the OME and saves it to the file
        private static void SaveOME()
        {
            SaveOME(saveOMEfile, saveOMEID);
        }
        public static bool OmeSupport = false;
        static bool supportDialog = false;
        /// The function checks if the operating system is macOS and displays a message if OME images
        /// are not supported, otherwise it sets OmeSupport to true.
        /// 
        /// @return The method is returning a boolean value. If the operating system is MacOS and the
        /// supportDialog flag is false, it will display a message dialog and return false. Otherwise,
        /// it will set the OmeSupport flag to true and return true.
        public static bool OMESupport()
        {
            bool isMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            if (RuntimeInformation.OSArchitecture == Architecture.Arm64 && isMacOS)
            {
                if (!supportDialog)
                {
                    MessageDialog md = new MessageDialog(
                    null,
                    DialogFlags.DestroyWithParent,
                    MessageType.Info,
                    ButtonsType.Ok, "BioGTK currently doens't support OME images on Arm64 MacOS due to dependency IKVM 8.6.2 not yet supporting Mac Arm64." +
                    "On MacOS ImageJ Tiff files, LibVips supported whole-slide images, and BioGTK Tiff files are supported.");
                    md.Run();
                }
                supportDialog = true;
                return false;
            }
            else
            {
                OmeSupport = true;
                return true;
            }
        }
        static NetVips.Image netim;
        /// The function checks if a given file is supported by the Vips library and returns true if it
        /// is, false otherwise.
        /// 
        /// @param file The "file" parameter is a string that represents the file path of the TIFF image
        /// that you want to load using the NetVips library.
        /// 
        /// @return The method is returning a boolean value. If the try block is executed successfully,
        /// it will return true. If an exception is caught, it will return false.
        public static bool VipsSupport(string file)
        {
            //Currently GTKSharp and LibVips causes an error so on windows no netvips support.
            if (OperatingSystem.IsWindows())
                return false;
            try
            {
                netim = NetVips.Image.Tiffload(file);
                //Settings.AddSettings("VipsSupport", "true");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            netim.Close();
            netim.Dispose();
            return true;
        }
        private static Stopwatch st = new Stopwatch();
        private static ServiceFactory factory;
        private static OMEXMLService service;
        private static ImageReader reader;
        private static ImageWriter writer;
        private loci.formats.meta.IMetadata meta;

        //We use UNIX type line endings since they are supported by ImageJ & Bio.
        public const char NewLine = '\n';
        public const string columns = "ROIID,ROINAME,TYPE,ID,SHAPEINDEX,TEXT,S,C,Z,T,X,Y,W,H,POINTS,STROKECOLOR,STROKECOLORW,FILLCOLOR,FONTSIZE\n";

        /// > Open the file, get the image description field, and return it as a string
        /// 
        /// @param file the path to the file
        /// 
        /// @return The image description of the tiff file.
        public static string OpenXML(string file)
        {
            if (!file.EndsWith(".tif"))
                return null;
            Tiff image = Tiff.Open(file, "r");
            FieldValue[] f = image.GetField(TiffTag.IMAGEDESCRIPTION);
            return f[0].ToString();
        }

        /// It reads the OME-XML file and converts the ROIs to a list of ROI objects
        /// 
        /// @param file the path to the OME-TIFF file
        /// @param series the series number of the image you want to open
        /// 
        /// @return A list of ROI objects.
        public static List<ROI> OpenOMEROIs(string file, int series)
        {
            if (!OMESupport())
                return null;
            List<ROI> Annotations = new List<ROI>();
            // create OME-XML metadata store
            ServiceFactory factory = new ServiceFactory();
            OMEXMLService service = (OMEXMLService)factory.getInstance(typeof(OMEXMLService));
            loci.formats.ome.OMEXMLMetadata meta = service.createOMEXMLMetadata();
            // create format reader
            ImageReader imageReader = new ImageReader();
            imageReader.setMetadataStore(meta);
            // initialize file
            file = file.Replace("\\", "/");
            imageReader.setId(file);
            int imageCount = imageReader.getImageCount();
            int seriesCount = imageReader.getSeriesCount();
            double physicalSizeX = 0;
            double physicalSizeY = 0;
            double physicalSizeZ = 0;
            double stageSizeX = 0;
            double stageSizeY = 0;
            double stageSizeZ = 0;
            int SizeX = imageReader.getSizeX();
            int SizeY = imageReader.getSizeY();
            int SizeZ = imageReader.getSizeY();
            try
            {
                bool hasPhysical = false;
                if (meta.getPixelsPhysicalSizeX(series) != null)
                {
                    physicalSizeX = meta.getPixelsPhysicalSizeX(series).value().doubleValue();
                    hasPhysical = true;
                }
                if (meta.getPixelsPhysicalSizeY(series) != null)
                {
                    physicalSizeY = meta.getPixelsPhysicalSizeY(series).value().doubleValue();
                }
                if (meta.getPixelsPhysicalSizeZ(series) != null)
                {
                    physicalSizeZ = meta.getPixelsPhysicalSizeZ(series).value().doubleValue();
                }
                else
                {
                    physicalSizeZ = 1;
                }
                if (meta.getStageLabelX(series) != null)
                    stageSizeX = meta.getStageLabelX(series).value().doubleValue();
                if (meta.getStageLabelY(series) != null)
                    stageSizeY = meta.getStageLabelY(series).value().doubleValue();
                if (meta.getStageLabelZ(series) != null)
                    stageSizeZ = meta.getStageLabelZ(series).value().doubleValue();
                else
                    stageSizeZ = 1;
            }
            catch (Exception e)
            {
                stageSizeX = 0;
                stageSizeY = 0;
                stageSizeZ = 1;
            }
            VolumeD volume = new VolumeD(new Point3D(stageSizeX, stageSizeY, stageSizeZ), new Point3D(physicalSizeX * SizeX, physicalSizeY * SizeY, physicalSizeZ * SizeZ));
            int rc = meta.getROICount();
            for (int im = 0; im < rc; im++)
            {
                string roiID = meta.getROIID(im);
                string roiName = meta.getROIName(im);
                ZCT co = new ZCT(0, 0, 0);
                int scount = 1;
                try
                {
                    scount = meta.getShapeCount(im);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
                for (int sc = 0; sc < scount; sc++)
                {
                    string type = meta.getShapeType(im, sc);
                    ROI an = new ROI();
                    an.roiID = roiID;
                    an.roiName = roiName;
                    an.shapeIndex = sc;
                    if (type == "Point")
                    {
                        an.type = ROI.Type.Point;
                        an.id = meta.getPointID(im, sc);
                        double dx = meta.getPointX(im, sc).doubleValue();
                        double dy = meta.getPointY(im, sc).doubleValue();
                        an.AddPoint(ToStageSpace(new PointD(dx, dy), physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y));
                        an.coord = new ZCT();
                        ome.xml.model.primitives.NonNegativeInteger nz = meta.getPointTheZ(im, sc);
                        if (nz != null)
                            an.coord.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = meta.getPointTheC(im, sc);
                        if (nc != null)
                            an.coord.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = meta.getPointTheT(im, sc);
                        if (nt != null)
                            an.coord.T = nt.getNumberValue().intValue();
                        an.Text = meta.getPointText(im, sc);
                        ome.units.quantity.Length fl = meta.getPointFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        an.family = meta.getPointFontFamily(im, sc).name();
                        ome.xml.model.primitives.Color col = meta.getPointStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = meta.getPointStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = meta.getPointStrokeColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Line")
                    {
                        an.type = ROI.Type.Line;
                        an.id = meta.getLineID(im, sc);
                        double px1 = meta.getLineX1(im, sc).doubleValue();
                        double py1 = meta.getLineY1(im, sc).doubleValue();
                        double px2 = meta.getLineX2(im, sc).doubleValue();
                        double py2 = meta.getLineY2(im, sc).doubleValue();
                        an.AddPoint(ToStageSpace(new PointD(px1, py1), physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y));
                        an.AddPoint(ToStageSpace(new PointD(px2, py2), physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y));
                        ome.xml.model.primitives.NonNegativeInteger nz = meta.getLineTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = meta.getLineTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = meta.getLineTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = meta.getLineText(im, sc);
                        ome.units.quantity.Length fl = meta.getLineFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        an.family = meta.getPointFontFamily(im, sc).name();
                        ome.xml.model.primitives.Color col = meta.getLineStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = meta.getLineStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = meta.getLineFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Rectangle")
                    {
                        an.type = ROI.Type.Rectangle;
                        an.id = meta.getRectangleID(im, sc);
                        double px = meta.getRectangleX(im, sc).doubleValue();
                        double py = meta.getRectangleY(im, sc).doubleValue();
                        double pw = meta.getRectangleWidth(im, sc).doubleValue();
                        double ph = meta.getRectangleHeight(im, sc).doubleValue();
                        an.Rect = ToStageSpace(new RectangleD(px, py, pw, ph), physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y);
                        ome.xml.model.primitives.NonNegativeInteger nz = meta.getRectangleTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = meta.getRectangleTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = meta.getRectangleTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;

                        an.Text = meta.getRectangleText(im, sc);
                        ome.units.quantity.Length fl = meta.getRectangleFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        an.family = meta.getPointFontFamily(im, sc).name();
                        ome.xml.model.primitives.Color col = meta.getRectangleStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = meta.getRectangleStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = meta.getRectangleFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                        ome.xml.model.enums.FillRule fr = meta.getRectangleFillRule(im, sc);
                    }
                    else
                    if (type == "Ellipse")
                    {
                        an.type = ROI.Type.Ellipse;
                        an.id = meta.getEllipseID(im, sc);
                        double px = meta.getEllipseX(im, sc).doubleValue();
                        double py = meta.getEllipseY(im, sc).doubleValue();
                        double ew = meta.getEllipseRadiusX(im, sc).doubleValue();
                        double eh = meta.getEllipseRadiusY(im, sc).doubleValue();
                        //We convert the ellipse radius to Rectangle
                        double w = ew * 2;
                        double h = eh * 2;
                        double x = px - ew;
                        double y = py - eh;
                        an.Rect = ToStageSpace(new RectangleD(px, py, w, h), physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y);
                        ome.xml.model.primitives.NonNegativeInteger nz = meta.getEllipseTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = meta.getEllipseTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = meta.getEllipseTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = meta.getEllipseText(im, sc);
                        ome.units.quantity.Length fl = meta.getEllipseFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        an.family = meta.getPointFontFamily(im, sc).name();
                        ome.xml.model.primitives.Color col = meta.getEllipseStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = meta.getEllipseStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = meta.getEllipseFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Polygon")
                    {
                        an.type = ROI.Type.Polygon;
                        an.id = meta.getPolygonID(im, sc);
                        an.closed = true;
                        string pxs = meta.getPolygonPoints(im, sc);
                        PointD[] pts = an.stringToPoints(pxs);
                        pts = ToStageSpace(pts, physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y);
                        if (pts.Length > 100)
                        {
                            an.type = ROI.Type.Freeform;
                        }
                        an.AddPoints(pts);
                        ome.xml.model.primitives.NonNegativeInteger nz = meta.getPolygonTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = meta.getPolygonTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = meta.getPolygonTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = meta.getPolygonText(im, sc);
                        ome.units.quantity.Length fl = meta.getPolygonFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        an.family = meta.getPointFontFamily(im, sc).name();
                        ome.xml.model.primitives.Color col = meta.getPolygonStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = meta.getPolygonStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = meta.getPolygonFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Polyline")
                    {
                        an.type = ROI.Type.Polyline;
                        an.id = meta.getPolylineID(im, sc);
                        string pxs = meta.getPolylinePoints(im, sc);
                        PointD[] pts = an.stringToPoints(pxs);
                        for (int pi = 0; pi < pts.Length; pi++)
                        {
                            pts[pi] = ToStageSpace(pts[pi], physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y);
                        }
                        an.AddPoints(an.stringToPoints(pxs));
                        ome.xml.model.primitives.NonNegativeInteger nz = meta.getPolylineTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = meta.getPolylineTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = meta.getPolylineTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;
                        an.Text = meta.getPolylineText(im, sc);
                        ome.units.quantity.Length fl = meta.getPolylineFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        an.family = meta.getPointFontFamily(im, sc).name();
                        ome.xml.model.primitives.Color col = meta.getPolylineStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = meta.getPolylineStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = meta.getPolylineFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                    }
                    else
                    if (type == "Label")
                    {
                        an.type = ROI.Type.Label;
                        an.id = meta.getLabelID(im, sc);

                        ome.xml.model.primitives.NonNegativeInteger nz = meta.getLabelTheZ(im, sc);
                        if (nz != null)
                            co.Z = nz.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nc = meta.getLabelTheC(im, sc);
                        if (nc != null)
                            co.C = nc.getNumberValue().intValue();
                        ome.xml.model.primitives.NonNegativeInteger nt = meta.getLabelTheT(im, sc);
                        if (nt != null)
                            co.T = nt.getNumberValue().intValue();
                        an.coord = co;

                        ome.units.quantity.Length fl = meta.getLabelFontSize(im, sc);
                        if (fl != null)
                            an.fontSize = fl.value().intValue();
                        an.family = meta.getPointFontFamily(im, sc).name();
                        ome.xml.model.primitives.Color col = meta.getLabelStrokeColor(im, sc);
                        if (col != null)
                            an.strokeColor = Color.FromArgb(col.getAlpha(), col.getRed(), col.getGreen(), col.getBlue());
                        ome.units.quantity.Length fw = meta.getLabelStrokeWidth(im, sc);
                        if (fw != null)
                            an.strokeWidth = (float)fw.value().floatValue();
                        ome.xml.model.primitives.Color colf = meta.getLabelFillColor(im, sc);
                        if (colf != null)
                            an.fillColor = Color.FromArgb(colf.getAlpha(), colf.getRed(), colf.getGreen(), colf.getBlue());
                        PointD p = new PointD(meta.getLabelX(im, sc).doubleValue(), meta.getLabelY(im, sc).doubleValue());
                        an.AddPoint(ToStageSpace(p, physicalSizeX, physicalSizeY, volume.Location.X, volume.Location.Y));
                        an.Text = meta.getLabelText(im, sc);
                    }
                }
            }

            imageReader.close();
            return Annotations;
        }
        /// It takes a list of ROI objects and returns a string of all the ROI objects in the list
        /// 
        /// @param Annotations List of ROI objects
        /// 
        /// @return A string of the ROI's
        public static string ROIsToString(List<ROI> Annotations)
        {
            string s = "";
            for (int i = 0; i < Annotations.Count; i++)
            {
                s += ROIToString(Annotations[i]);
            }
            return s;
        }
        /// This function takes an ROI object and returns a string that contains all the information
        /// about the ROI
        /// 
        /// @param ROI The ROI object
        /// 
        /// @return A string
        public static string ROIToString(ROI an)
        {
            PointD[] points = an.GetPoints();
            string pts = "";
            for (int j = 0; j < points.Length; j++)
            {
                if (j == points.Length - 1)
                    pts += points[j].X.ToString(CultureInfo.InvariantCulture) + "," + points[j].Y.ToString(CultureInfo.InvariantCulture);
                else
                    pts += points[j].X.ToString(CultureInfo.InvariantCulture) + "," + points[j].Y.ToString(CultureInfo.InvariantCulture) + " ";
            }
            char sep = (char)34;
            string sColor = sep.ToString() + an.strokeColor.A.ToString() + ',' + an.strokeColor.R.ToString() + ',' + an.strokeColor.G.ToString() + ',' + an.strokeColor.B.ToString() + sep.ToString();
            string bColor = sep.ToString() + an.fillColor.A.ToString() + ',' + an.fillColor.R.ToString() + ',' + an.fillColor.G.ToString() + ',' + an.fillColor.B.ToString() + sep.ToString();

            string line = an.roiID + ',' + an.roiName + ',' + an.type.ToString() + ',' + an.id + ',' + an.shapeIndex.ToString() + ',' +
                an.Text + ',' + an.serie + ',' + an.coord.Z.ToString() + ',' + an.coord.C.ToString() + ',' + an.coord.T.ToString() + ',' + an.X.ToString(CultureInfo.InvariantCulture) + ',' + an.Y.ToString(CultureInfo.InvariantCulture) + ',' +
                an.W.ToString(CultureInfo.InvariantCulture) + ',' + an.H.ToString(CultureInfo.InvariantCulture) + ',' + sep.ToString() + pts + sep.ToString() + ',' + sColor + ',' + an.strokeWidth.ToString(CultureInfo.InvariantCulture) + ',' + bColor + ',' + an.fontSize.ToString(CultureInfo.InvariantCulture) + ',' + NewLine;
            return line;
        }
        /// It takes a string and returns an ROI object
        /// 
        /// @param sts the string that contains the ROI data
        /// 
        /// @return A ROI object.
        public static ROI StringToROI(string sts)
        {
            //Works with either comma or tab separated values.
            if (sts.StartsWith("<?xml") || sts.StartsWith("{"))
                return null;
            ROI an = new ROI();
            string val = "";
            bool inSep = false;
            int col = 0;
            double x = 0;
            double y = 0;
            double w = 0;
            double h = 0;
            string line = sts;
            bool points = false;
            char sep = '"';
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == sep)
                {
                    if (!inSep)
                    {
                        inSep = true;
                    }
                    else
                        inSep = false;
                    continue;
                }

                if ((c == ',' || c == '\t') && (!inSep || points))
                {
                    //ROIID,ROINAME,TYPE,ID,SHAPEINDEX,TEXT,C,Z,T,X,Y,W,H,POINTS,STROKECOLOR,STROKECOLORW,FILLCOLOR,FONTSIZE
                    if (col == 0)
                    {
                        //ROIID
                        an.roiID = val;
                    }
                    else
                    if (col == 1)
                    {
                        //ROINAME
                        an.roiName = val;
                    }
                    else
                    if (col == 2)
                    {
                        //TYPE
                        an.type = (ROI.Type)Enum.Parse(typeof(ROI.Type), val);
                        if (an.type == ROI.Type.Freeform || an.type == ROI.Type.Polygon)
                            an.closed = true;
                    }
                    else
                    if (col == 3)
                    {
                        //ID
                        an.id = val;
                    }
                    else
                    if (col == 4)
                    {
                        //SHAPEINDEX/
                        an.shapeIndex = int.Parse(val);
                    }
                    else
                    if (col == 5)
                    {
                        //TEXT/
                        an.Text = val;
                    }
                    else
                    if (col == 6)
                    {
                        an.serie = int.Parse(val);
                    }
                    else
                    if (col == 7)
                    {
                        an.coord.Z = int.Parse(val);
                    }
                    else
                    if (col == 8)
                    {
                        an.coord.C = int.Parse(val);
                    }
                    else
                    if (col == 9)
                    {
                        an.coord.T = int.Parse(val);
                    }
                    else
                    if (col == 10)
                    {
                        x = double.Parse(val, CultureInfo.InvariantCulture);
                    }
                    else
                    if (col == 11)
                    {
                        y = double.Parse(val, CultureInfo.InvariantCulture);
                    }
                    else
                    if (col == 12)
                    {
                        w = double.Parse(val, CultureInfo.InvariantCulture);
                    }
                    else
                    if (col == 13)
                    {
                        h = double.Parse(val, CultureInfo.InvariantCulture);
                    }
                    else
                    if (col == 14)
                    {
                        //POINTS
                        an.AddPoints(an.stringToPoints(val));
                        points = false;
                        an.Rect = new RectangleD(x, y, w, h);
                    }
                    else
                    if (col == 15)
                    {
                        //STROKECOLOR
                        string[] st = val.Split(',');
                        an.strokeColor = Color.FromArgb(int.Parse(st[0]), int.Parse(st[1]), int.Parse(st[2]), int.Parse(st[3]));
                    }
                    else
                    if (col == 16)
                    {
                        //STROKECOLORW
                        an.strokeWidth = double.Parse(val, CultureInfo.InvariantCulture);
                    }
                    else
                    if (col == 17)
                    {
                        //FILLCOLOR
                        string[] st = val.Split(',');
                        an.fillColor = Color.FromArgb(int.Parse(st[0]), int.Parse(st[1]), int.Parse(st[2]), int.Parse(st[3]));
                    }
                    else
                    if (col == 18)
                    {
                        //FONTSIZE
                        double s = double.Parse(val, CultureInfo.InvariantCulture);
                        an.fontSize = (float)s;
                        an.family = "Times New Roman";
                    }
                    col++;
                    val = "";
                }
                else
                    val += c;
            }

            return an;
        }
        /// This function takes a list of ROIs and writes them to a CSV file
        /// 
        /// @param filename the name of the file to be saved
        /// @param Annotations List of ROI objects
        public static void ExportROIsCSV(string filename, List<ROI> Annotations)
        {
            string con = columns;
            con += ROIsToString(Annotations);
            File.WriteAllText(filename, con);
        }
        /// It reads the CSV file and converts each line into a ROI object
        /// 
        /// @param filename The path to the CSV file.
        /// 
        /// @return A list of ROI objects.
        public static List<ROI> ImportROIsCSV(string filename)
        {
            List<ROI> list = new List<ROI>();
            if (!File.Exists(filename))
                return list;
            string[] sts = File.ReadAllLines(filename);
            //We start reading from line 1.
            for (int i = 1; i < sts.Length; i++)
            {
                list.Add(StringToROI(sts[i]));
            }
            return list;
        }
        /// ExportROIFolder(path, filename)
        /// 
        /// This function takes a folder path and a filename as input and exports all the ROIs in the
        /// folder as CSV files
        /// 
        /// @param path the path to the folder containing the OMERO ROI files
        /// @param filename the name of the file you want to export
        public static void ExportROIFolder(string path, string filename)
        {
            if (!OMESupport())
                return;
            string[] fs = Directory.GetFiles(path);
            int i = 0;
            foreach (string f in fs)
            {
                List<ROI> annotations = OpenOMEROIs(f, 0);
                string ff = Path.GetFileNameWithoutExtension(f);
                ExportROIsCSV(path + "//" + ff + "-" + i.ToString() + ".csv", annotations);
                i++;
            }
        }

        private static BioImage bstats = null;
        private static bool update = false;
        /// It takes a BioImage object, and calculates the mean histogram for each channel, and for the
        /// entire image. 
        /// 
        /// The BioImage object is a class that contains a list of buffers, each of which contains a
        /// byte array of pixel data. 
        /// 
        /// The function also calculates the mean histogram for each buffer, and stores it in the
        /// buffer's stats property. 
        /// 
        /// The function also calculates the mean histogram for each channel, and stores it in the
        /// channel's stats property. 
        /// 
        /// The function also calculates the mean histogram for the entire image, and stores it in the
        /// image's statistics property. 
        /// 
        /// The mean histogram is calculated by adding up the histograms of each buffer, and then
        /// dividing by the number of buffers. 
        /// 
        /// The histogram is calculated by looping through the byte array of pixel data, and
        /// incrementing the value of the histogram at the index of the pixel value. 
        /// 
        /// The histogram
        /// 
        /// @param BioImage This is the image object that contains the image data.
        /// @param updateImageStats if true, the image stats will be updated.
        public static void AutoThreshold(BioImage b, bool updateImageStats)
        {
            bstats = b;
            Statistics statistics = null;
            if (b.bitsPerPixel > 8)
                statistics = new Statistics(true);
            else
                statistics = new Statistics(false);
            for (int i = 0; i < b.Buffers.Count; i++)
            {
                if (b.Buffers[i].Stats == null || updateImageStats)
                    b.Buffers[i].Stats = Statistics.FromBytes(b.Buffers[i]);
                if (b.Buffers[i].RGBChannelsCount == 1)
                    statistics.AddStatistics(b.Buffers[i].Stats[0]);
                else
                {
                    for (int r = 0; r < b.Buffers[i].RGBChannelsCount; r++)
                    {
                        statistics.AddStatistics(b.Buffers[i].Stats[r]);
                    }
                }
            }
            for (int c = 0; c < b.Channels.Count; c++)
            {
                Statistics[] sts = new Statistics[b.Buffers[0].RGBChannelsCount];
                for (int i = 0; i < b.Buffers[0].RGBChannelsCount; i++)
                {
                    if (b.bitsPerPixel > 8)
                    {
                        sts[i] = new Statistics(true);
                    }
                    else
                        sts[i] = new Statistics(false);
                }
                for (int z = 0; z < b.SizeZ; z++)
                {
                    for (int t = 0; t < b.SizeT; t++)
                    {
                        int ind;
                        if (b.Channels.Count > b.SizeC)
                        {
                            ind = b.Coords[z, 0, t];
                        }
                        else
                            ind = b.Coords[z, c, t];
                        if (b.Buffers[ind].RGBChannelsCount == 1)
                            sts[0].AddStatistics(b.Buffers[ind].Stats[0]);
                        else
                        {
                            sts[0].AddStatistics(b.Buffers[ind].Stats[0]);
                            sts[1].AddStatistics(b.Buffers[ind].Stats[1]);
                            sts[2].AddStatistics(b.Buffers[ind].Stats[2]);
                            if (b.Buffers[ind].RGBChannelsCount == 4)
                                sts[3].AddStatistics(b.Buffers[ind].Stats[3]);
                        }
                    }
                }
                if (b.RGBChannelCount == 1)
                    sts[0].MeanHistogram();
                else
                {
                    sts[0].MeanHistogram();
                    sts[1].MeanHistogram();
                    sts[2].MeanHistogram();
                    if (b.Buffers[0].RGBChannelsCount == 4)
                        sts[3].MeanHistogram();
                }
                b.Channels[c].stats = sts;
            }
            statistics.MeanHistogram();
            b.statistics = statistics;

        }
        /// It takes the current image, and finds the best threshold value for it
        public static void AutoThreshold()
        {
            AutoThreshold(bstats, update);
        }
        /// It creates a new thread that calls the AutoThreshold function
        /// 
        /// @param BioImage This is a class that holds the image data and some other information.
        public static void AutoThresholdThread(BioImage b)
        {
            bstats = b;
            Thread th = new Thread(AutoThreshold);
            th.Start();
        }
        /// The function finds the focus of a given BioImage at a specific channel and time by
        /// calculating the focus quality of each Z-plane and returning the coordinate with the highest
        /// focus quality.
        /// 
        /// @param BioImage A BioImage object that contains the image data.
        /// @param Channel The channel of the BioImage to analyze. A BioImage can have multiple
        /// channels, each representing a different fluorescent label or imaging modality.
        /// @param Time The time point at which the focus is being calculated.
        /// 
        /// @return an integer value which represents the coordinate of the image with the highest focus
        /// quality in a given channel and time.
        public static int FindFocus(BioImage im, int Channel, int Time)
        {
            long mf = 0;
            int fr = 0;
            List<double> dt = new List<double>();
            ZCT c = new ZCT(0, 0, 0);
            for (int i = 0; i < im.SizeZ; i++)
            {
                long f = CalculateFocusQuality(im.Buffers[im.Coords[i, Channel, Time]]);
                dt.Add(f);
                if (f > mf)
                {
                    mf = f;
                    fr = im.Coords[i, Channel, Time];
                }
            }
            //Plot pl = Plot.Create(dt.ToArray(), "Focus");
            //pl.Show();
            return fr;
        }
        /// The function calculates the focus quality of a given bitmap image.
        /// 
        /// @param Bitmap A class representing a bitmap image, which contains information about the
        /// image's size, pixel data, and color channels.
        /// 
        /// @return The method is returning a long value which represents the calculated focus quality
        /// of the input Bitmap image.
        public static long CalculateFocusQuality(Bitmap b)
        {
            if (b.RGBChannelsCount == 1)
            {
                long sum = 0;
                long sumOfSquaresR = 0;
                for (int y = 0; y < b.SizeY; y++)
                    for (int x = 0; x < b.SizeX; x++)
                    {
                        ColorS pixel = b.GetPixel(x, y);
                        sum += pixel.R;
                        sumOfSquaresR += pixel.R * pixel.R;
                    }
                return sumOfSquaresR * b.SizeX * b.SizeY - sum * sum;
            }
            else
            {
                long sum = 0;
                long sumOfSquares = 0;
                for (int y = 0; y < b.SizeY; y++)
                    for (int x = 0; x < b.SizeX; x++)
                    {
                        ColorS pixel = b.GetPixel(x, y);
                        int p = (pixel.R + pixel.G + pixel.B) / 3;
                        sum += p;
                        sumOfSquares += p * p;
                    }
                return sumOfSquares * b.SizeX * b.SizeY - sum * sum;
            }
        }
        /// It disposes of all the buffers and channels in the image, removes the image from the Images
        /// list, and then forces the garbage collector to run
        public void Dispose()
        {
            for (int i = 0; i < Buffers.Count; i++)
            {
                Buffers[i].Dispose();
            }
            for (int i = 0; i < Channels.Count; i++)
            {
                Channels[i].Dispose();
            }
        }
        /// This function returns the filename of the object, and the location of the object in the 3D
        /// space
        /// 
        /// @return The filename, and the location of the volume.
        public override string ToString()
        {
            return Filename.ToString() + ", (" + Volume.Location.X + ", " + Volume.Location.Y + ", " + Volume.Location.Z + ")";
        }

        /// This function divides each pixel in the image by a constant value
        /// 
        /// @param BioImage a class that contains a list of buffers (which are 2D arrays of floats)
        /// @param b the value to divide by
        /// 
        /// @return The image is being returned.
        public static BioImage operator /(BioImage a, float b)
        {
            for (int i = 0; i < a.Buffers.Count; i++)
            {
                a.Buffers[i] = a.Buffers[i] / b;
            }
            return a;
        }

        /// This function adds a constant value to each pixel in the image
        /// 
        /// @param BioImage a class that contains a list of buffers (float[])
        /// @param b the value to add to the image
        /// 
        /// @return The image itself.
        public static BioImage operator +(BioImage a, float b)
        {
            for (int i = 0; i < a.Buffers.Count; i++)
            {
                a.Buffers[i] = a.Buffers[i] + b;
            }
            return a;
        }
        /// Subtracts a scalar value from each pixel in the image
        /// 
        /// @param BioImage a class that contains a list of buffers (which are 2D arrays of floats)
        /// @param b the value to subtract from the image
        /// 
        /// @return The BioImage object is being returned.
        public static BioImage operator -(BioImage a, float b)
        {
            for (int i = 0; i < a.Buffers.Count; i++)
            {
                a.Buffers[i] = a.Buffers[i] - b;
            }
            return a;
        }

        /// This function divides each pixel in the image by the value of the color
        /// 
        /// @param BioImage a class that contains a list of ColorS objects.
        /// @param ColorS a struct that contains a byte for each color channel (R, G, B, A)
        /// 
        /// @return A BioImage object.
        public static BioImage operator /(BioImage a, ColorS b)
        {
            for (int i = 0; i < a.Buffers.Count; i++)
            {
                a.Buffers[i] = a.Buffers[i] / b;
            }
            return a;
        }
        /// This function takes a BioImage object and a ColorS object and returns a BioImage object
        /// 
        /// @param BioImage a class that contains a list of ColorS objects.
        /// @param ColorS a struct that contains a byte for each color channel (R, G, B, A)
        /// 
        /// @return A BioImage object
        public static BioImage operator *(BioImage a, ColorS b)
        {
            for (int i = 0; i < a.Buffers.Count; i++)
            {
                a.Buffers[i] = a.Buffers[i] * b;
            }
            return a;
        }
        /// It takes a BioImage object and a ColorS object and adds the ColorS object to each buffer in
        /// the BioImage object
        /// 
        /// @param BioImage a class that contains a list of ColorS objects.
        /// @param ColorS a struct that contains a byte for each color channel (R, G, B, A)
        /// 
        /// @return A BioImage object
        public static BioImage operator +(BioImage a, ColorS b)
        {
            for (int i = 0; i < a.Buffers.Count; i++)
            {
                a.Buffers[i] = a.Buffers[i] + b;
            }
            return a;
        }
        /// The function subtracts a color from each pixel in the image
        /// 
        /// @param BioImage a class that contains a list of ColorS objects.
        /// @param ColorS a struct that contains a byte for each color channel (R, G, B, A)
        /// 
        /// @return The image itself.
        public static BioImage operator -(BioImage a, ColorS b)
        {
            for (int i = 0; i < a.Buffers.Count; i++)
            {
                a.Buffers[i] = a.Buffers[i] - b;
            }
            return a;
        }
    }
}
