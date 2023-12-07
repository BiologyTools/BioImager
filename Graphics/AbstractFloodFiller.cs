using System.Diagnostics;
using AForge;
using Bitmap = AForge.Bitmap;
using Color = AForge.Color;
using PointF = AForge.PointF;
using RectangleF = AForge.RectangleF;
using RectangleD = AForge.RectangleD;
using Rectangle = AForge.Rectangle;
using Point = AForge.Point;
namespace BioImager
{
    /// <summary>
    /// The base class that the flood fill algorithms inherit from. Implements the
    /// basic flood filler functionality that is the same across all algorithms.
    /// </summary>
    public abstract class AbstractFloodFiller
    {

        protected Bitmap bitmap;
        protected ColorS tolerance = new ColorS(25, 25, 25);
        protected ColorS fillColor = ColorS.FromColor(Color.Black);
        protected bool fillDiagonally = false;
        protected bool slow = false;

        //cached bitmap properties
        protected int bitmapWidth = 0;
        protected int bitmapHeight = 0;
        protected int bitmapStride = 0;
        protected int bitmapPixelFormatSize = 0;
        protected byte[] bitmapBits = null;
        protected PixelFormat pixelFormat;

        //internal int timeBenchmark = 0;
        internal Stopwatch watch = new Stopwatch();

        //internal, initialized per fill
        //protected BitArray pixelsChecked;
        protected bool[] pixelsChecked;
        protected ColorS byteFillColor;
        protected ColorS startColor;
        //protected int stride;

        public AbstractFloodFiller()
        {

        }

        public AbstractFloodFiller(AbstractFloodFiller configSource)
        {
            if (configSource != null)
            {
                this.Bitmap = configSource.Bitmap;
                this.FillColor = configSource.FillColor;
                this.FillDiagonally = configSource.FillDiagonally; ;
                this.Tolerance = configSource.Tolerance;
            }
        }

        public ColorS FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public bool FillDiagonally
        {
            get { return fillDiagonally; }
            set { fillDiagonally = value; }
        }

        public ColorS Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public Bitmap Bitmap
        {
            get { return bitmap; }
            set
            {
                bitmap = value;
            }
        }

        public abstract void FloodFill(Point pt);
        protected void PrepareForFloodFill(Point pt)
        {
            startColor = bitmap.GetPixel((int)pt.X, (int)pt.Y);
            byteFillColor = new ColorS(fillColor.B, fillColor.G, fillColor.R);
            bitmapStride = bitmap.Stride;
            bitmapPixelFormatSize = bitmap.PixelFormatSize;
            pixelFormat = bitmap.PixelFormat;
            bitmapBits = bitmap.Bytes;
            bitmapWidth = bitmap.SizeX;
            bitmapHeight = bitmap.SizeY;
            pixelsChecked = new bool[bitmapBits.Length / bitmapPixelFormatSize];
        }
    }
}
