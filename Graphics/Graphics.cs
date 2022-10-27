using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Bio.Graphics
{
    public struct Pen : IDisposable
    {
        public ColorS color;
        public ushort width;
        public byte bitsPerPixel;
        public Pen(ColorS col, ushort w, int bitsPerPixel)
        {
            color = col;
            width = w;
            this.bitsPerPixel = (byte)bitsPerPixel;
        }
        public Pen(ColorS col, int w, int bitsPerPixel)
        {
            color = col;
            width = (ushort)w;
            this.bitsPerPixel = (byte)bitsPerPixel;
        }
        public void Dispose()
        {
            color.Dispose();
        }
    }
    public class Graphics : IDisposable
    {
        public BufferInfo buf;
        public Pen pen;
        private AbstractFloodFiller filler;
        public static Graphics FromImage(BufferInfo b)
        {
            Graphics g = new Graphics();
            g.buf = b;
            g.pen = new Pen(new ColorS(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue),1,b.BitsPerPixel);
            return g;
        }
        public void DrawLine(int x, int y, int x2, int y2)
        {
            //Bresenham's algorithm for line drawing.
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                FillEllipse(x, y, pen.width, pen.width, pen.color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
        private void DrawLine(int x, int y, int x2, int y2, BufferInfo bf, ColorS c)
        {
            //Bresenham's algorithm for line drawing.
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                bf.SetPixel(x, y, c);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
        public void DrawLine(PointF p, PointF p2)
        {
            DrawLine((int)p.X, (int)p.Y, (int)p2.X, (int)p2.Y);
        }
        public void FillRectangle(Rectangle r, ColorS col)
        {
            for (int x = r.X; x < r.Width + r.X; x++)
            {
                for (int y = r.Y; y < r.Height + r.Y; y++)
                {
                    buf.SetPixel(x,y, col);
                }
            }
        }
        public void FillRectangle(RectangleF r, ColorS col)
        {
            FillRectangle(new Rectangle((int)Math.Ceiling(r.X), (int)Math.Ceiling(r.Y), (int)Math.Ceiling(r.Width), (int)Math.Ceiling(r.Height)), col);
        }
        public void DrawRectangle(Rectangle r)
        {
            for (int x = r.X; x < r.Width + r.X; x++)
            {
                for (int i = 0; i < pen.width; i++)
                {
                    buf.SetPixel(x + i, r.Y, pen.color);
                    buf.SetPixel(x + i, r.Y + r.Height, pen.color);
                }
            }
            for (int y = r.Y; y < r.Height + r.Y; y++)
            {
                for (int i = 0; i < pen.width; i++)
                {
                    buf.SetPixel(r.X, y + i, pen.color);
                    buf.SetPixel(r.X + r.Width, y + i, pen.color);
                }
            }
        }
        public void DrawRectangle(RectangleF r)
        {
            DrawRectangle(new Rectangle((int)Math.Ceiling(r.X), (int)Math.Ceiling(r.Y), (int)Math.Ceiling(r.Width), (int)Math.Ceiling(r.Height)));
        }

        public void DrawEllipse(Rectangle r)
        {
            if (r.Width == 1 && r.Height == 1)
                buf.SetPixel(r.X, r.Y, pen.color);
            double radiusx = r.Width / 2;
            double radiusy = r.Height / 2;
            int x, y;
            for (double a = 0.0; a < 360.0; a += 0.1)
            {
                double angle = a * System.Math.PI / 180;
                for (int i = 0; i < pen.width; i++)
                {
                    x = (int)(radiusx * System.Math.Cos(angle) + radiusx + r.X);
                    y = (int)(radiusy * System.Math.Sin(angle) + radiusy + r.Y);
                    buf.SetPixel(x+i, y+i, pen.color);
                }
            }
        }
        public void DrawEllipse(RectangleF r)
        {
            DrawEllipse(new Rectangle((int)Math.Ceiling(r.X), (int)Math.Ceiling(r.Y), (int)Math.Ceiling(r.Width), (int)Math.Ceiling(r.Height)));
        }
        public void FillEllipse(float xx, float yy, int w, int h, ColorS c)
        {
            if (w <= 1 && w <= 1)
            {
                buf.SetPixel((int)xx,(int)yy, c);
                return;
            }
            double radiusx = w / 2;
            double radiusy = h / 2;
            int x, y;
            for (double a = 90; a < 270; a += 0.1)
            {
                double angle = a * System.Math.PI / 180;
                x = (int)(radiusx * System.Math.Cos(angle) + radiusx + xx);
                y = (int)(radiusy * System.Math.Sin(angle) + radiusy + yy);
                double angle2 = (a+180) * System.Math.PI / 180;
                int x2 = (int)(radiusx * System.Math.Cos(angle2) + radiusx + xx);
                DrawScanline(x,x2,y,c);
            }
        }
        public void FillEllipse(RectangleF r, ColorS c)
        {
            FillEllipse(new Rectangle((int)Math.Ceiling(r.X), (int)Math.Ceiling(r.Y), (int)Math.Ceiling(r.Width), (int)Math.Ceiling(r.Height)), c);
        }
        public void FillEllipse(Rectangle r, ColorS c)
        {
            FillEllipse(r.X, r.Y, r.Width, r.Height, c);
        }
        public void FillPolygon(PointF[] pfs, Rectangle r)
        {
            //We will use the flood fill algorithm to fill the polygon.
            //First we need to create a new Buffer incase the current Buffer contains filled pixels that could prevent flood fill from filling the whole area.
            BufferInfo bf = buf.CopyInfo();
            bf.Bytes = new byte[buf.Bytes.Length];

            DrawPolygon(pfs, bf, pen.color);
            
            filler = new QueueLinearFloodFiller(filler);
            filler.FillColor = pen.color;
            filler.Tolerance = new ColorS(0, 0, 0);
            filler.Bitmap = bf;
            //Next we need to find a point inside the polygon from where to start the flood fill.
            //We use the center points x-line till we find a point inside.
            Point p = new Point(r.X + (r.Width / 2), r.Y + (r.Height / 2));
            Point? pp = null;
            polygon = pfs;
            for (int x = r.X; x < r.Width + r.X; x++)
            {
                if (PointInPolygon(x, p.Y))
                {
                    pp = new Point(x, p.Y);
                    break;
                }
            }
            filler.FloodFill(pp.Value);
            //Now that we have a filled shape we draw it onto the original bitmap
            for (int x = 0; x < bf.SizeX; x++)
            {
                for (int y = 0; y < bf.SizeY; y++)
                {
                    if(bf.GetPixel(x,y) == pen.color)
                    {
                        buf.SetPixel(x, y, pen.color);
                    }
                }
            }
            bf.Dispose();
        }
        public void FillPolygon(PointF[] pfs, RectangleF r)
        {
            FillPolygon(pfs, new Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height));
        }
        private static PointF[] polygon;
        public bool PointInPolygon(int x, int y)
        {
            int j = polygon.Length - 1;
            bool c = false;
            for (int i = 0; i < polygon.Length; j = i++) c ^= polygon[i].Y > y ^ polygon[j].Y > y && x < (polygon[j].X - polygon[i].X) * (y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X;
            return c;
        }
        
        public void FillPolygon(PointF[] pfs, Rectangle r, ColorS c)
        {
            pen.color = c;
            FillPolygon(pfs, r);
        }
        public void FillPolygon(PointF[] pfs, RectangleF r, ColorS c)
        {
            pen.color = c;
            FillPolygon(pfs, r);
        }
        private void DrawPolygon(PointF[] pfs, BufferInfo bf, ColorS s)
        {
            for (int i = 0; i < pfs.Length - 1; i++)
            {
                DrawLine((int)pfs[i].X, (int)pfs[i].Y, (int)pfs[i + 1].X, (int)pfs[i + 1].Y, bf, s);
            }
            DrawLine((int)pfs[0].X, (int)pfs[0].Y, (int)pfs[pfs.Length - 1].X, (int)pfs[pfs.Length - 1].Y, bf, s);
        }
        public void DrawScanline(int x, int x2, int line, ColorS col)
        {
            for (int xx = x; xx < x2; xx++)
            {
                buf.SetPixel(xx, line, col);
            }
        }
        public void Dispose()
        {
            pen.Dispose();
        }
    }
}
