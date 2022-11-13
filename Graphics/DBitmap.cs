using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Bio.Graphics
{
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration() : this("BioImager")
        {
            WaitVerticalBlanking = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration(string title) : this(title, 800, 600)
        {
            WaitVerticalBlanking = true;
        }

        public Configuration(string title, int width, int height)
        {
            Title = title;
            Width = width;
            Height = height;
            WaitVerticalBlanking = true;
        }

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [wait vertical blanking].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [wait vertical blanking]; otherwise, <c>false</c>.
        /// </value>
        public bool WaitVerticalBlanking
        {
            get; set;
        }
    }
    class DBitmap
    {
        private Bitmap _bitmap;

        /// <summary>
        /// Loads a Direct2D Bitmap from a file using System.Drawing.Image.FromFile(...)
        /// </summary>
        /// <param name="renderTarget">The render target.</param>
        /// <param name="file">The file.</param>
        /// <returns>A D2D1 Bitmap</returns>
        public static Bitmap FromImage(RenderTarget renderTarget, BufferInfo image)
        {
            // Loads from file using System.Drawing.Image
            using (var bitmap = (System.Drawing.Bitmap)image.ImageRGB)
            {
                var bitmapProperties = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore));
                var size = new Size2(bitmap.Width, bitmap.Height);
                return new Bitmap(renderTarget, size, new DataPointer(image.RGBData,bitmap.Width * 4 * bitmap.Height), bitmap.Width * 4, bitmapProperties);
            }
        }
        public unsafe static Bitmap FromImage(RenderTarget renderTarget, System.Drawing.Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;
            Bitmap b = null;
            // Loads from file using System.Drawing.Image
            BitmapData d = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            var bitmap = image;
            var bitmapProperties = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore));
            var size = new Size2(bitmap.Width, bitmap.Height);


            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                b = new Bitmap(renderTarget, size, new DataPointer(d.Scan0, bitmap.Width * 4 * bitmap.Height), bitmap.Width * 4, bitmapProperties);
            }
            else if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                //opening a 8 bit per pixel jpg image
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //iterating through all the pixels in y direction
                for (int y = 0; y < h; y++)
                {
                    //getting the pixels of current row
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                    byte* rowOrig = (byte*)d.Scan0 + (y * d.Stride);
                    int rowRGB = y * w * 3;
                    //iterating through all the pixels in x direction
                    for (int x = 0; x < w; x++)
                    {
                        int indexRGB = x * 3;
                        int indexRGBA = x * 4;
                        row[indexRGBA + 3] = byte.MaxValue;//byte A
                        row[indexRGBA + 2] = rowOrig[indexRGB + 2];//byte R
                        row[indexRGBA + 1] = rowOrig[indexRGB + 1];//byte G
                        row[indexRGBA] = rowOrig[indexRGB];//byte B
                    }
                }
                //unlocking bits and disposing image
                bmp.UnlockBits(bmd);
                var bitmapProp = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore));
                return new Bitmap(renderTarget, size, new DataPointer(bmd.Scan0, bitmap.Width * 4 * bitmap.Height), bitmap.Width * 4, bitmapProp);
            }
            else
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format48bppRgb)
            {
                //opening a 8 bit per pixel jpg image
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                byte[] bts = new byte[6];
                unsafe
                {
                    //iterating through all the pixels in y direction
                    for (int y = 0; y < h; y++)
                    {
                        //getting the pixels of current row
                        byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                        byte* rowOrig = (byte*)d.Scan0 + (y * d.Stride);
                        int rowRGB = y * w * 6;
                        //iterating through all the pixels in x direction
                        for (int x = 0; x < w; x++)
                        {
                            int indexRGB = x * 6;
                            int indexRGBA = x * 4;
                            bts[0] = rowOrig[indexRGB + 1];
                            bts[1] = rowOrig[indexRGB + 0];
                            bts[2] = rowOrig[indexRGB + 3];
                            bts[3] = rowOrig[indexRGB + 2];
                            bts[2] = rowOrig[indexRGB + 5];
                            bts[3] = rowOrig[indexRGB + 4];
                            int bb = (int)((float)BitConverter.ToUInt16(bts, 0) / 255);
                            int bg = (int)((float)BitConverter.ToUInt16(bts, 2) / 255);
                            int br = (int)((float)BitConverter.ToUInt16(bts, 4) / 255);
                            row[indexRGBA + 3] = 255;//byte A
                            row[indexRGBA + 2] = (byte)(bb);//byte R
                            row[indexRGBA + 1] = (byte)(bg);//byte G
                            row[indexRGBA] = (byte)(br);//byte B
                        }
                    }
                }
                bmp.UnlockBits(bmd);
                var bitmapProp = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore));
                return new Bitmap(renderTarget, size, new DataPointer(bmd.Scan0, bitmap.Width * 4 * bitmap.Height), bitmap.Width * 4, bitmapProp);
            }
            else
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                
                //opening a 8 bit per pixel jpg image
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //iterating through all the pixels in y direction
                for (int y = 0; y < h; y++)
                {
                    //getting the pixels of current row
                    byte* row = (byte*)bmd.Scan0 + (y * w * 4);
                    byte* rowOrig = (byte*)d.Scan0 + (y * w);
                    //iterating through all the pixels in x direction
                    for (int x = 0; x < w; x++)
                    {
                        int indexRGB = x;
                        int indexRGBA = x * 4;
                        row[indexRGBA + 3] = byte.MaxValue;//byte A
                        row[indexRGBA + 2] = rowOrig[indexRGB];//byte R
                        row[indexRGBA + 1] = rowOrig[indexRGB];//byte G
                        row[indexRGBA] = rowOrig[indexRGB];//byte B
                    }
                }
                //unlocking bits and disposing image
                bmp.UnlockBits(bmd);
                var bitmapProp = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore));
                return new Bitmap(renderTarget, size, new DataPointer(bmd.Scan0, bitmap.Width * 4 * bitmap.Height), bitmap.Width * 4, bitmapProp);

            }
            else
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
            {
                //opening a 8 bit per pixel jpg image
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //creating the bitmapdata and lock bits
                System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, w, h);
                BitmapData bmd = bmp.LockBits(rec, ImageLockMode.ReadWrite, image.PixelFormat);
                byte[] bts = new byte[2];
                unsafe
                {
                    //iterating through all the pixels in y direction
                    for (int y = 0; y < h; y++)
                    {
                        //getting the pixels of current row
                        byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                        byte* rowOrig = (byte*)d.Scan0 + (y * d.Stride);
                        int rowRGB = y * w * 2;
                        //iterating through all the pixels in x direction
                        for (int x = 0; x < w; x++)
                        {
                            int indexRGB = x * 2;
                            int indexRGBA = x * 4;
                            bts[0] = rowOrig[indexRGB + 1];
                            bts[1] = rowOrig[indexRGB];
                            ushort bs = (ushort)((float)BitConverter.ToUInt16(bts, 0) / ushort.MaxValue);
                            row[indexRGBA + 3] = 255;//byte A
                            row[indexRGBA + 2] = (byte)(bs);//byte R
                            row[indexRGBA + 1] = (byte)(bs);//byte G
                            row[indexRGBA] = (byte)(bs);//byte B
                        }
                    }
                }
                bmp.UnlockBits(bmd);
                var bitmapProp = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore));
                return new Bitmap(renderTarget, size, new DataPointer(bmd.Scan0, bitmap.Width * 4 * bitmap.Height), bitmap.Width * 4, bitmapProp);
            }
            image.UnlockBits(d);
            return b;
        }

        public void Initialize(Configuration configuration, RenderTarget renderTarget2, BufferInfo bf)
        {
            _bitmap = FromImage(renderTarget2, bf);
        }
    }
}
