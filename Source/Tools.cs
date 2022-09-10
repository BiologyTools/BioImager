using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bio;
using System.IO;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge;

namespace Bio
{
    public partial class Tools : Form
    {
        public static bool applyToStack = false;
        public static ColorTool colorTool;
        public static bool rEnabled = true;
        public static bool gEnabled = true;
        public static bool bEnabled = true;
        public static Rectangle selectionRectangle;
        public static Hashtable tools = new Hashtable();
        public class Tool
        {
            public enum ToolType
            {
                color,
                annotation,
                select,
                function
            }
            public enum Type
            {
                pencil,
                brush,
                bucket,
                eraser,
                move,
                point,
                line,
                rect,
                ellipse,
                polyline,
                polygon,
                text,
                delete,
                freeform,
                rectSel,
                pointSel,
                pan,
                magic,
                script,
            }

            public static void Init()
            {
                if (tools.Count == 0)
                {
                    foreach (Tool.Type tool in (Tool.Type[])Enum.GetValues(typeof(Tool.Type)))
                    {
                        tools.Add(tool.ToString(), new Tool(tool, new ColorS(0, 0, 0), 1));
                    }
                }
            }

            public ColorS Color;
            public List<System.Drawing.Point> Points;
            public ToolType toolType;
            private RectangleD rect;
            public RectangleD Rectangle
            {
                get { return rect; }
                set { rect = value; }
            }
            public RectangleF RectangleF
            {
                get { return new RectangleF((float)rect.X, (float)rect.Y, (float)rect.W, (float)rect.H); }
            }
            public double width = 1;
            public string script;
            public Type type;
            public Tool()
            {
            }
            public Tool(Type t)
            {
                type = t;
            }
            public Tool(Type t, ColorS col)
            {
                type = t;
                Color = col;
            }
            public Tool(Type t, ColorS col, double w)
            {
                type = t;
                Color = col;
                width = w;
            }
            public Tool(Type t, RectangleD r)
            {
                type = t;
                rect = r;
            }
            public Tool(Type t, string sc)
            {
                type = t;
                script = sc;
            }

            public ushort R
            {
                get { return Color.R; }
                set { Color.R = value; }
            }
            public ushort G
            {
                get { return Color.G; }
                set { Color.G = value; }
            }
            public ushort B
            {
                get { return Color.B; }
                set { Color.B = value; }
            }
            public override string ToString()
            {
                return type.ToString();
            }
        }
        public static Tool currentTool;
        
        public static RectangleD selectionRect;
        public Font font;
        public Tools()
        {
            InitializeComponent();
            Tool.Init();
            ColorS col = new ColorS(ushort.MaxValue);
            //We initialize the tools
            currentTool = GetTool(Tool.Type.move);
        }

        public static Tool GetTool(string name)
        {
            return (Tool)tools[name];
        }
        public static Tool GetTool(Tool.Type typ)
        {
            return (Tool)tools[typ.ToString()];
        }
        public void UpdateOverlay()
        {
            App.viewer.UpdateOverlay();
        }
        public void UpdateView()
        {
            App.viewer.UpdateStatus();
        }
        public void UpdateSelected()
        {
            foreach (Control item in this.Controls)
            {
                item.BackColor = Color.White;
            }
        }

        ROI anno = new ROI();
        public void ToolDown(PointD e, MouseButtons buts)
        {
            if (App.viewer == null)
                return;
            if (currentTool == null)
                return;
            Scripting.UpdateState(Scripting.State.GetDown(e, buts));
            if (currentTool.type == Tool.Type.line)
            {
                if (anno.GetPointCount() == 0)
                {
                    anno = new ROI();
                    anno.type = ROI.Type.Line;
                    anno.AddPoint(new PointD(e.X, e.Y));
                    anno.AddPoint(new PointD(e.X, e.Y));
                    anno.coord = App.viewer.GetCoordinate();
                    ImageView.SelectedImage.Annotations.Add(anno);
                }
            }
            else
            if (currentTool.type == Tool.Type.polygon)
            {
                if (anno.GetPointCount() == 0)
                {
                    anno = new ROI();
                    anno.type = ROI.Type.Polygon;
                    anno.AddPoint(new PointD(e.X, e.Y));
                    anno.coord = App.viewer.GetCoordinate();
                    ImageView.SelectedImage.Annotations.Add(anno);
                }
                else
                {
                    //If we click on a point 1 we close this polygon
                    RectangleD d = new RectangleD(e.X, e.Y, anno.selectBoxSize, anno.selectBoxSize);
                    if (d.IntersectsWith(anno.Point))
                    {
                        anno.closed = true;
                        anno = new ROI();
                    }
                    else
                    {
                        anno.AddPoint(new PointD(e.X, e.Y));
                    }
                }
            }
            else
            if (currentTool.type == Tool.Type.freeform)
            {
                if (anno.GetPointCount() == 0)
                {
                    anno = new ROI();
                    anno.type = ROI.Type.Freeform;
                    anno.AddPoint(new PointD(e.X, e.Y));
                    anno.coord = App.viewer.GetCoordinate();
                    anno.closed = true;
                    ImageView.SelectedImage.Annotations.Add(anno);
                }
                else
                {
                    anno.AddPoint(new PointD(e.X, e.Y));
                }
            }
            else
            if (currentTool.type == Tool.Type.rect)
            {
                anno.type = ROI.Type.Rectangle;
                anno.Rect = new RectangleD(e.X, e.Y, 1, 1);
                anno.coord = App.viewer.GetCoordinate();
                ImageView.SelectedImage.Annotations.Add(anno);
            }
            else
            if (currentTool.type == Tool.Type.ellipse)
            {
                anno.type = ROI.Type.Ellipse;
                anno.Rect = new RectangleD(e.X, e.Y, 1, 1);
                anno.coord = App.viewer.GetCoordinate();
                ImageView.SelectedImage.Annotations.Add(anno);
            }
            else
            if(currentTool.type == Tool.Type.delete)
            {
                foreach (ROI an in ImageView.selectedAnnotations)
                {
                    if (an != null)
                    {
                        if (an.selectedPoints.Count == 0)
                        {
                            ImageView.SelectedImage.Annotations.Remove(an);
                        }
                        else
                        if (an.selectedPoints.Count == 1 && !(an.type == ROI.Type.Polygon || an.type == ROI.Type.Polyline || an.type == ROI.Type.Freeform))
                        {
                            ImageView.SelectedImage.Annotations.Remove(an);
                        }
                        else
                        {
                            if (an.type == ROI.Type.Polygon ||
                                an.type == ROI.Type.Polyline ||
                                an.type == ROI.Type.Freeform)
                            {
                                an.closed = false;
                                an.RemovePoints(an.selectedPoints.ToArray());
                            }
                        }
                    }
                }
            }
            else
            if (currentTool.type == Tool.Type.text)
            {
                ROI an = new ROI();
                an.type = ROI.Type.Label;
                an.AddPoint(new PointD(e.X, e.Y));
                an.coord = App.viewer.GetCoordinate();
                TextInput ti = new TextInput("");
                if (ti.ShowDialog() != DialogResult.OK)
                    return;
                an.font = ti.font;
                an.strokeColor = ti.color;
                an.Text = ti.textInput;
                ImageView.SelectedImage.Annotations.Add(an);
            }
            if (buts == MouseButtons.Middle || currentTool.type == Tool.Type.pan)
            {
                currentTool = GetTool(Tool.Type.pan);
                UpdateSelected();
                panPanel.BackColor = Color.LightGray;
                Cursor.Current = Cursors.Hand;
            }
            UpdateOverlay();
        }        
        public void ToolUp(PointD e, MouseButtons buts)
        {
            if (App.viewer == null)
                return;
            if (currentTool == null)
                return;
            if (anno == null)
                return;
            Scripting.UpdateState(Scripting.State.GetUp(e, buts));
            if (currentTool.type == Tool.Type.point)    
            {
                ROI an = new ROI();
                an.AddPoint(new PointD(e.X, e.Y));
                an.type = ROI.Type.Point;
                an.coord = App.viewer.GetCoordinate();
                ImageView.SelectedImage.Annotations.Add(an);
            }
            else
            if (currentTool.type == Tool.Type.line && anno.type == ROI.Type.Line)
            {
                if (anno.GetPointCount() > 0)
                {
                    anno.UpdatePoint(new PointD(e.X, e.Y), 1);
                    anno = new ROI();
                }
            }
            else
            if (currentTool.type == Tool.Type.rect && anno.type == ROI.Type.Rectangle)
            {
                if (anno.GetPointCount() == 4)
                {
                    anno = new ROI();
                }
            }
            else
            if (currentTool.type == Tool.Type.ellipse && anno.type == ROI.Type.Ellipse)
            {
                if (anno.GetPointCount() == 4)
                {
                    anno = new ROI();
                }
            }
            else
            if (currentTool.type == Tool.Type.freeform && anno.type == ROI.Type.Freeform)
            {
                anno = new ROI();
            }
            else
            if (currentTool.type == Tool.Type.rectSel)
            {
                ImageView.selectedAnnotations.Clear();
                RectangleF r = GetTool(Tool.Type.rectSel).RectangleF;
                foreach (ROI an in App.viewer.AnnotationsRGB)
                {
                    if (an.GetSelectBound().ToRectangleF().IntersectsWith(r))
                    {
                        an.selectedPoints.Clear();
                        ImageView.selectedAnnotations.Add(an);
                        an.selected = true;
                        RectangleF[] sels = an.GetSelectBoxes(ImageView.scale.Width);
                        for (int i = 0; i < sels.Length; i++)
                        {
                            if (sels[i].IntersectsWith(r))
                            {
                                an.selectedPoints.Add(i);
                            }
                        }
                    }
                    else
                        an.selected = false;
                }
                Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle = new RectangleD(0, 0, 0, 0);
            }
            if (Tools.currentTool.type == Tools.Tool.Type.magic)
            {
                PointD pf = new PointD(ImageView.mouseUp.X - ImageView.mouseDown.X, ImageView.mouseUp.Y - ImageView.mouseDown.Y);
                ZCT coord = App.viewer.GetCoordinate();
               
                Rectangle r = new Rectangle((int)ImageView.mouseDown.X, (int)ImageView.mouseDown.Y, (int)(ImageView.mouseUp.X - ImageView.mouseDown.X), (int)(ImageView.mouseUp.Y - ImageView.mouseDown.Y));
                if (r.Width <= 2 || r.Height <= 2)
                    return;
                BufferInfo bf = ImageView.SelectedImage.Buffers[ImageView.SelectedImage.Coords[coord.Z, coord.C, coord.T]].GetCropBuffer(r);
                Statistics st = Statistics.FromBytes(bf);
                Bitmap crop = (Bitmap)bf.Image;
                Threshold th;
                if(magicSel.Numeric)
                {
                    th = new Threshold((int)magicSel.Threshold);
                }
                else
                if(magicSel.Index == 2)
                    th = new Threshold((int)(st.Min + st.Mean));
                else
                if(magicSel.Index == 1)
                    th = new Threshold((int)st.Median);
                else
                    th = new Threshold(st.Min);
                th.ApplyInPlace(crop);
                Invert inv = new Invert();
                Bitmap det;
                if (bf.BitsPerPixel > 8)
                    det = AForge.Imaging.Image.Convert16bppTo8bpp((crop));
                else
                    det = crop;
                BlobCounter blobCounter = new BlobCounter();
                blobCounter.ProcessImage(det);
                Blob[] blobs = blobCounter.GetObjectsInformation();
                // create convex hull searching algorithm
                GrahamConvexHull hullFinder = new GrahamConvexHull();
                // lock image to draw on it
                // process each blob
                foreach (Blob blob in blobs)
                {
                    if(blob.Rectangle.Width < magicSel.Max  && blob.Rectangle.Height < magicSel.Max)
                        continue;
                    List<IntPoint> leftPoints = new List<IntPoint>();
                    List<IntPoint> rightPoints = new List<IntPoint>();
                    List<IntPoint> edgePoints = new List<IntPoint>();
                    List<IntPoint> hull = new List<IntPoint>();
                    // get blob's edge points
                    blobCounter.GetBlobsLeftAndRightEdges(blob,
                        out leftPoints, out rightPoints);
                    edgePoints.AddRange(leftPoints);
                    edgePoints.AddRange(rightPoints);
                    // blob's convex hull
                    hull = hullFinder.FindHull(edgePoints);
                    PointD[] pfs = new PointD[hull.Count];
                    for (int i = 0; i < hull.Count; i++)
                    {
                        pfs[i] = new PointD(r.X + hull[i].X,r.Y + hull[i].Y);
                    }
                    ROI an = ROI.CreateFreeform(coord, pfs);
                    ImageView.SelectedImage.Annotations.Add(an); 
                }
            }
            UpdateOverlay();
        }
        public void ToolMove(PointD e, MouseButtons buts)
        {
            if (App.viewer == null)
                return;
            Scripting.UpdateState(Scripting.State.GetMove(e, buts));
            if (currentTool.type == Tool.Type.line && ImageView.down)
            {
                anno.UpdatePoint(new PointD(e.X, e.Y), 1);
                UpdateOverlay();
                
            }
            else
            if (currentTool.type == Tool.Type.freeform && buts == MouseButtons.Left && ImageView.down)
            {
                if (anno.GetPointCount() == 0)
                {
                    anno.type = ROI.Type.Freeform;
                    anno.AddPoint(new PointD(e.X, e.Y));
                    anno.coord = App.viewer.GetCoordinate();
                    anno.closed = true;
                    ImageView.SelectedImage.Annotations.Add(anno);
                }
                else
                {
                    anno.AddPoint(new PointD(e.X, e.Y));
                }
                UpdateOverlay();
            }
            else
            if (currentTool.type == Tool.Type.rect && anno.type == ROI.Type.Rectangle)
            {
                if (anno.GetPointCount() == 4)
                {
                    anno.Rect = new RectangleD(anno.X, anno.Y, e.X - anno.X, e.Y - anno.Y);
                    UpdateOverlay();
                }
            }
            else
            if (currentTool.type == Tool.Type.ellipse && anno.type == ROI.Type.Ellipse)
            {
                if (anno.GetPointCount() == 4)
                {
                    anno.Rect = new RectangleD(anno.X, anno.Y, e.X - anno.X, e.Y - anno.Y);
                    UpdateOverlay();
                }
            }
            else
            if (currentTool.type == Tool.Type.rectSel && buts == MouseButtons.Left && ImageView.down)
            {
                PointD d = new PointD(e.X - ImageView.mouseDown.X, e.Y - ImageView.mouseDown.Y);
                Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle = new RectangleD(ImageView.mouseDown.X, ImageView.mouseDown.Y, d.X, d.Y);
                RectangleF r = Tools.GetTool(Tools.Tool.Type.rectSel).RectangleF;
                foreach (ROI an in App.viewer.AnnotationsRGB)
                {
                    if (an.GetSelectBound().ToRectangleF().IntersectsWith(r))
                    {
                        an.selectedPoints.Clear();
                        ImageView.selectedAnnotations.Add(an);
                        an.selected = true;
                        RectangleF[] sels = an.GetSelectBoxes(ImageView.scale.Width);
                        for (int i = 0; i < sels.Length; i++)
                        {
                            if (sels[i].IntersectsWith(r))
                            {
                                an.selectedPoints.Add(i);
                            }
                        }
                    }
                    else
                        an.selected = false;
                }
                UpdateOverlay();
            }
            else
            if (currentTool.type == Tool.Type.rectSel && ImageView.up)
            {
                Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle = new RectangleD(0, 0, 0, 0);
            }
            else
            if (Win32.GetKeyState(Keys.Delete))
            {
                foreach (ROI an in ImageView.selectedAnnotations)
                {
                    if (an != null)
                    {
                        if (an.selectedPoints.Count == 0)
                        {
                            ImageView.SelectedImage.Annotations.Remove(an);
                        }
                        else
                        {
                            if (an.type == ROI.Type.Polygon ||
                                an.type == ROI.Type.Polyline ||
                                an.type == ROI.Type.Freeform)
                            {
                                an.closed = false;
                                an.RemovePoints(an.selectedPoints.ToArray());
                                
                            }
                        }
                    }
                }
                UpdateOverlay();
            }

            if (Tools.currentTool.type == Tools.Tool.Type.magic && buts == MouseButtons.Left)
            {
                //First we draw the selection rectangle
                PointD d = new PointD(e.X - ImageView.mouseDown.X, e.Y - ImageView.mouseDown.Y);
                Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle = new RectangleD(ImageView.mouseDown.X, ImageView.mouseDown.Y, d.X, d.Y);
                UpdateOverlay();
            }
            if (Tools.currentTool.type == Tools.Tool.Type.pan && (buts == MouseButtons.Middle || buts == MouseButtons.Left))
            {
                PointD pf = new PointD(e.X - ImageView.mouseDown.X, e.Y - ImageView.mouseDown.Y);
                if (pf.X > 50 && pf.Y > 50)
                    return;
                //App.viewer.Origin = new PointD(App.viewer.Origin.X + pf.X, App.viewer.Origin.Y + pf.Y);
                UpdateView();
            }

        }

        private void movePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.move);
            UpdateSelected();
            movePanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void textPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.text);
            UpdateSelected();
            textPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void textPanel_DoubleClick(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.text);
            UpdateSelected();
            textPanel.BackColor = Color.LightGray;

            if (fontDialog.ShowDialog() != DialogResult.OK)
                return;
            font = fontDialog.Font;
            Cursor.Current = Cursors.Arrow;
        }
        private void pointPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.point);
            UpdateSelected();
            pointPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void linePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.line);
            UpdateSelected();
            linePanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void rectPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.rect);
            UpdateSelected();
            rectPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void ellipsePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.ellipse);
            UpdateSelected();
            ellipsePanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void polyPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.polygon);
            UpdateSelected();
            polyPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void deletePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.delete);
            UpdateSelected();
            deletePanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void freeformPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.freeform);
            UpdateSelected();
            freeformPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void rectSelPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.rectSel);
            UpdateSelected();
            rectSelPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }
        private void panPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.pan);
            UpdateSelected();
            panPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Hand;
        }
        private void magicPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.magic);
            UpdateSelected();
            magicPanel.BackColor = Color.LightGray;
            Cursor.Current = Cursors.Arrow;
        }

        MagicSelect magicSel = new MagicSelect(2);
        private void magicPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (magicSel.ShowDialog() != DialogResult.OK)
                return;
        }
        private void Tools_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
