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
using System.IO;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge;
using BioImager.Graphics;
using BioImager;
using Rectangle = AForge.Rectangle;
using Bitmap = AForge.Bitmap;
using BioLib;
using Gdk;
namespace BioImager
{
    public partial class Tools : Form
    {
        public static bool applyToStack = false;
        public static ColorTool colorTool;
        public static bool rEnabled = true;
        public static bool gEnabled = true;
        public static bool bEnabled = true;
        private static ColorS drawColor = new ColorS(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue);
        private static ColorS eraseColor = new ColorS(0, 0, 0);
        private static int width = 1;

        public static System.Drawing.Rectangle selectionRectangle;
        public static Hashtable tools = new Hashtable();
        private AbstractFloodFiller floodFiller = null;
        /* It's a class that contains a list of tools, and each tool has a type, a rectangle, a script,
        and a tolerance. */
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
                dropper
            }

            public static void Init()
            {
                if (tools.Count == 0)
                {
                    foreach (Tool.Type tool in (Tool.Type[])Enum.GetValues(typeof(Tool.Type)))
                    {
                        tools.Add(tool.ToString(), new Tool(tool, new ColorS(0, 0, 0)));
                    }
                }
            }
            public List<System.Drawing.Point> Points;
            public ToolType toolType;
            private RectangleD rect;
            public RectangleD Rectangle
            {
                get { return rect; }
                set { rect = value; }
            }
            public AForge.RectangleF RectangleF
            {
                get { return new AForge.RectangleF((float)rect.X, (float)rect.Y, (float)rect.W, (float)rect.H); }
            }
            public string script;
            public Type type;
            public ColorS tolerance;
            public Tool()
            {
                tolerance = new ColorS(0, 0, 0);
            }
            public Tool(Type t)
            {
                type = t;
                tolerance = new ColorS(0, 0, 0);
            }
            public Tool(Type t, ColorS col)
            {
                type = t;
                tolerance = new ColorS(0, 0, 0);
            }
            public Tool(Type t, RectangleD r)
            {
                type = t;
                rect = r;
                tolerance = new ColorS(0, 0, 0);
            }
            public Tool(Type t, string sc)
            {
                type = t;
                script = sc;
                tolerance = new ColorS(0, 0, 0);
            }
            public override string ToString()
            {
                return type.ToString();
            }
        }
        public static Tool currentTool;
        public static ColorS DrawColor
        {
            get
            {
                return drawColor;
            }
            set
            {
                drawColor = value;
            }
        }
        public static ColorS EraseColor
        {
            get
            {
                return eraseColor;
            }
            set
            {
                eraseColor = value;
            }
        }

        public static int StrokeWidth
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                App.tools.UpdateGUI();
            }
        }

        public static RectangleD selectionRect;
        public Font font;
        /* Initializing the tools. */
        public Tools()
        {
            InitializeComponent();
            Tool.Init();
            ColorS col = new ColorS(ushort.MaxValue);
            //We initialize the tools
            currentTool = GetTool(Tool.Type.move);

            floodFiller = null;
            floodFiller = new QueueLinearFloodFiller(floodFiller);
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
            App.viewer.UpdateView();
        }
        /// It loops through all the controls in the form and if the control has a tag of "tool" it sets
        /// the background color to white
        public void UpdateSelected()
        {
            foreach (Control item in this.Controls)
            {
                if (item.Tag != null)
                    if (item.Tag.ToString() == "tool")
                        item.BackColor = System.Drawing.Color.White;
            }
        }
        /// It updates the GUI
        private void UpdateGUI()
        {
            if (ImageView.SelectedImage != null)
            {
                color1Box.BackColor = System.Drawing.Color.FromArgb(DrawColor.R / ushort.MaxValue, DrawColor.G / ushort.MaxValue, DrawColor.B / ushort.MaxValue);
                color2Box.BackColor = System.Drawing.Color.FromArgb(EraseColor.R / ushort.MaxValue, EraseColor.G / ushort.MaxValue, EraseColor.B / ushort.MaxValue);
            }
            widthBox.Value = width;
        }
        public static float selectBoxSize = ROI.selectBoxSize;
        ROI anno = new ROI();
        /// The function is called when the user clicks the mouse button. 
        /// 
        /// The function checks if the user is using the line, polygon, freeform, rectangle, ellipse,
        /// delete, or text tool. 
        /// 
        /// If the user is using the line tool, the function creates a new ROI object and adds it to the
        /// image's annotations. 
        /// 
        /// If the user is using the polygon tool, the function creates a new ROI object and adds it to
        /// the image's annotations. 
        /// 
        /// If the user is using the freeform tool, the function creates a new ROI object and adds it to
        /// the image's annotations. 
        /// 
        /// If the user is using the rectangle tool, the function creates a new ROI object and adds it to
        /// the image's annotations. 
        /// 
        /// If the user is using the ellipse tool, the function creates a new ROI object and adds it to
        /// the image's annotations. 
        /// 
        /// If
        /// 
        /// @param PointD A point with double precision
        /// @param MouseButtons A set of values that indicate which mouse button was pressed.
        /// 
        /// @return The return type is void.
        public void ToolDown(PointD e, MouseButtons buts)
        {
            if (App.viewer == null || currentTool == null || ImageView.SelectedImage == null)
                return;
            Scripting.UpdateState(Scripting.State.GetDown(e, buts));
            PointD p;
            if(ImageView.SelectedImage!=null)
            if (App.viewer.HardwareAcceleration)
                p = ImageView.SelectedImage.ToImageSpace(new PointD(ImageView.SelectedImage.Volume.Width - e.X, ImageView.SelectedImage.Volume.Height - e.Y));
            else
                p = ImageView.SelectedImage.ToImageSpace(e);
            if (currentTool.type == Tool.Type.line && buts == MouseButtons.Left)
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
            if (currentTool.type == Tool.Type.polygon && buts == MouseButtons.Left)
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
                    RectangleD d = new RectangleD(e.X, e.Y, ROI.selectBoxSize, ROI.selectBoxSize);
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
            if (currentTool.type == Tool.Type.freeform && buts == MouseButtons.Left)
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
            if (currentTool.type == Tool.Type.rect && buts == MouseButtons.Left)
            {
                anno.type = ROI.Type.Rectangle;
                anno.Rect = new RectangleD(e.X, e.Y, 1, 1);
                anno.coord = App.viewer.GetCoordinate();
                ImageView.SelectedImage.Annotations.Add(anno);
            }
            else
            if (currentTool.type == Tool.Type.ellipse && buts == MouseButtons.Left)
            {
                anno.type = ROI.Type.Ellipse;
                anno.Rect = new RectangleD(e.X, e.Y, 1, 1);
                anno.coord = App.viewer.GetCoordinate();
                ImageView.SelectedImage.Annotations.Add(anno);
            }
            else
            if (currentTool.type == Tool.Type.delete && buts == MouseButtons.Left)
            {
                for (int i = 0; i < ImageView.SelectedImage.Annotations.Count; i++)
                {
                    ROI an = ImageView.SelectedImage.Annotations[i];
                    if (an.BoundingBox.IntersectsWith(e.X, e.Y))
                    {
                        if (an.selectedPoints.Count == 0)
                        {
                            ImageView.SelectedImage.Annotations.Remove(an);
                            break;
                        }
                        else
                        if (an.selectedPoints.Count == 1 && !(an.type == ROI.Type.Polygon || an.type == ROI.Type.Polyline || an.type == ROI.Type.Freeform))
                        {
                            ImageView.SelectedImage.Annotations.Remove(an);
                            break;
                        }
                        else
                        {
                            if (an.type == ROI.Type.Polygon ||
                                an.type == ROI.Type.Polyline ||
                                an.type == ROI.Type.Freeform)
                            {
                                an.closed = false;
                                an.RemovePoints(an.selectedPoints.ToArray());
                                break;
                            }
                        }
                    }
                }
                UpdateOverlay();
            }
            else
            if (currentTool.type == Tool.Type.text && buts == MouseButtons.Left)
            {
                ROI an = new ROI();
                an.type = ROI.Type.Label;
                an.AddPoint(new PointD(e.X, e.Y));
                an.coord = App.viewer.GetCoordinate();
                TextInput ti = new TextInput("");
                if (ti.ShowDialog() != DialogResult.OK)
                    return;
                an.family = font.FontFamily.Name;
                an.fontSize = ti.font.Size;
                an.strokeColor = AForge.Color.FromArgb(ti.color.A, ti.color.R, ti.color.G, ti.color.B);
                an.Text = ti.TextValue;
                ImageView.SelectedImage.Annotations.Add(an);
            }
            else
            if (buts == MouseButtons.Middle || currentTool.type == Tool.Type.pan)
            {
                currentTool = GetTool(Tool.Type.pan);
                UpdateSelected();
                panPanel.BackColor = System.Drawing.Color.LightGray;
            }

            UpdateOverlay();
        }
        /// The function ToolUp() is called when the mouse button is released
        /// 
        /// @param PointD X,Y
        /// @param MouseButtons Left, Right, Middle, XButton1, XButton2
        /// 
        /// @return a RectangleF.
        public void ToolUp(PointD e, MouseButtons buts)
        {
            PointD p = new PointD();
            if(ImageView.SelectedImage != null)
            if (App.viewer.HardwareAcceleration)
                p = ImageView.SelectedImage.ToImageSpace(new PointD(ImageView.SelectedImage.Volume.Width - e.X, ImageView.SelectedImage.Volume.Height - e.Y));
            else
                p = ImageView.SelectedImage.ToImageSpace(e);
            if (App.viewer == null || currentTool == null || ImageView.SelectedImage == null || anno == null)
                return;
            Scripting.UpdateState(Scripting.State.GetUp(e, buts));
            if (currentTool.type == Tool.Type.point && buts == MouseButtons.Left)
            {
                ROI an = new ROI();
                an.AddPoint(new PointD(e.X, e.Y));
                an.type = ROI.Type.Point;
                an.coord = App.viewer.GetCoordinate();
                ImageView.SelectedImage.Annotations.Add(an);
            }
            else
            if (currentTool.type == Tool.Type.line && anno.type == ROI.Type.Line && buts == MouseButtons.Left)
            {
                if (anno.GetPointCount() > 0)
                {
                    anno.UpdatePoint(new PointD(e.X, e.Y), 1);
                    anno = new ROI();
                }
            }
            else
            if (currentTool.type == Tool.Type.rect && anno.type == ROI.Type.Rectangle && buts == MouseButtons.Left)
            {
                if (anno.GetPointCount() == 4)
                {
                    anno = new ROI();
                }
            }
            else
            if (currentTool.type == Tool.Type.ellipse && anno.type == ROI.Type.Ellipse && buts == MouseButtons.Left)
            {
                if (anno.GetPointCount() == 4)
                {
                    anno = new ROI();
                }
            }
            else
            if (currentTool.type == Tool.Type.freeform && anno.type == ROI.Type.Freeform && buts == MouseButtons.Left)
            {
                anno = new ROI();
            }
            else
            if (currentTool.type == Tool.Type.rectSel && buts == MouseButtons.Left)
            {
                ImageView.selectedAnnotations.Clear();
                RectangleD r = GetTool(Tool.Type.rectSel).Rectangle;
                foreach (ROI an in App.viewer.AnnotationsRGB)
                {
                    if (an.GetSelectBound(App.viewer.GetScale(), App.viewer.GetScale()).ToRectangleF().IntersectsWith(r.ToRectangleF()))
                    {
                        an.selectedPoints.Clear();
                        ImageView.selectedAnnotations.Add(an);
                        an.Selected = true;
                        RectangleD[] sels = an.GetSelectBoxes(selectBoxSize);
                        for (int i = 0; i < sels.Length; i++)
                        {
                            if (sels[i].IntersectsWith(r))
                            {
                                an.selectedPoints.Add(i);
                            }
                        }
                    }
                    else
                        an.Selected = false;
                }
                Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle = new RectangleD(0, 0, 0, 0);
            }
            else
            if (Tools.currentTool.type == Tools.Tool.Type.magic && buts == MouseButtons.Left)
            {
                PointD pf = new PointD(ImageView.mouseUp.X - ImageView.mouseDown.X, ImageView.mouseUp.Y - ImageView.mouseDown.Y);
                ZCT coord = App.viewer.GetCoordinate();

                Rectangle r = new Rectangle((int)ImageView.mouseDown.X, (int)ImageView.mouseDown.Y, (int)(ImageView.mouseUp.X - ImageView.mouseDown.X), (int)(ImageView.mouseUp.Y - ImageView.mouseDown.Y));
                if (r.Width <= 2 || r.Height <= 2)
                    return;
                Bitmap bf = ImageView.SelectedImage.Buffers[ImageView.SelectedImage.Coords[coord.Z, coord.C, coord.T]].GetCropBuffer(r);
                Statistics[] sts = Statistics.FromBytes(bf);
                Statistics st = sts[0];
                Threshold th;
                if (magicSel.Numeric)
                {
                    th = new Threshold((int)magicSel.Threshold);
                }
                else
                if (magicSel.Index == 2)
                    th = new Threshold((int)(st.Min + st.Mean));
                else
                if (magicSel.Index == 1)
                    th = new Threshold((int)st.Median);
                else
                    th = new Threshold((int)st.Min);
                th.ApplyInPlace((Bitmap)bf.Image);
                Invert inv = new Invert();
                Bitmap det;
                if (bf.BitsPerPixel > 8)
                    det = AForge.Imaging.Image.Convert16bppTo8bpp((Bitmap)bf.Image);
                else
                    det = (Bitmap)bf.Image;
                BlobCounter blobCounter = new BlobCounter();
                blobCounter.ProcessImage(det);
                Blob[] blobs = blobCounter.GetObjectsInformation();
                // create convex hull searching algorithm
                GrahamConvexHull hullFinder = new GrahamConvexHull();
                // lock image to draw on it
                // process each blob
                foreach (Blob blob in blobs)
                {
                    if (blob.Rectangle.Width < magicSel.Max && blob.Rectangle.Height < magicSel.Max)
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
                        pfs[i] = new PointD(r.X + hull[i].X, r.Y + hull[i].Y);
                    }
                    ROI an = ROI.CreateFreeform(coord, pfs);
                    ImageView.SelectedImage.Annotations.Add(an);
                }
            }
            else
            if (Tools.currentTool.type == Tools.Tool.Type.bucket && buts == MouseButtons.Left)
            {
                ZCT coord = App.viewer.GetCoordinate();
                floodFiller.FillColor = DrawColor;
                floodFiller.Tolerance = currentTool.tolerance;
                floodFiller.Bitmap = ImageView.SelectedImage.Buffers[ImageView.SelectedImage.Coords[coord.C, coord.Z, coord.T]];
                floodFiller.FloodFill(new AForge.Point((int)p.X, (int)p.Y));
                App.viewer.UpdateImages();
            }
            else
            if (Tools.currentTool.type == Tools.Tool.Type.dropper && buts == MouseButtons.Left)
            {
                DrawColor = ImageView.SelectedBuffer.GetPixel((int)p.X, (int)p.Y);
                UpdateGUI();
            }
            UpdateOverlay();
        }

        public void ToolMove(PointD e, MouseButtons buts)
        {
            if (App.viewer == null)
                return;
            if (Tools.currentTool.type == Tools.Tool.Type.pan && (buts.HasFlag(MouseButtons.Left) || buts.HasFlag(MouseButtons.Middle)))
            {
                if (ImageView.SelectedImage.isPyramidal)
                {
                    if (App.viewer.MouseMoveInt.X != 0 || App.viewer.MouseMoveInt.Y != 0)
                    {
                        App.viewer.PyramidalOriginTransformed = new PointD(App.viewer.PyramidalOriginTransformed.X + (ImageView.mouseDown.X - e.X), App.viewer.PyramidalOriginTransformed.Y + (ImageView.mouseDown.Y - e.Y));
                    }
                }
                else
                {
                    PointD pf = new PointD(e.X - ImageView.mouseDown.X, e.Y - ImageView.mouseDown.Y);
                    App.viewer.Origin = new PointD(App.viewer.Origin.X + pf.X, App.viewer.Origin.Y + pf.Y);
                }
                App.viewer.UpdateImages();
                UpdateView();
            }
            if (ImageView.SelectedImage == null)
                return;
            PointD p;
            if (App.viewer.HardwareAcceleration)
                p = ImageView.SelectedImage.ToImageSpace(new PointD(ImageView.SelectedImage.Volume.Width - e.X, ImageView.SelectedImage.Volume.Height - e.Y));
            else
                p = ImageView.SelectedImage.ToImageSpace(e);
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
                RectangleD r = Tools.GetTool(Tools.Tool.Type.rectSel).Rectangle;
                foreach (ROI an in App.viewer.AnnotationsRGB)
                {
                    if (an.GetSelectBound(App.viewer.GetScale(), App.viewer.GetScale()).IntersectsWith(r))
                    {
                        an.selectedPoints.Clear();
                        ImageView.selectedAnnotations.Add(an);
                        an.Selected = true;
                        RectangleD[] sels = an.GetSelectBoxes(selectBoxSize);
                        for (int i = 0; i < sels.Length; i++)
                        {
                            if (sels[i].IntersectsWith(r))
                            {
                                an.selectedPoints.Add(i);
                            }
                        }
                    }
                    else
                        an.Selected = false;
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

            if (buts == MouseButtons.Left && currentTool.type == Tool.Type.eraser)
            {
                Graphics.Graphics g = Graphics.Graphics.FromImage(ImageView.SelectedBuffer);
                Graphics.Pen pen = new Graphics.Pen(Tools.EraseColor, (int)Tools.StrokeWidth, ImageView.SelectedBuffer.BitsPerPixel);
                g.FillEllipse(new Rectangle((int)p.X, (int)p.Y, (int)width, (int)Tools.StrokeWidth), pen.color);
                pen.Dispose();
                App.viewer.UpdateImage();
            }
        }

        /// When the movePanel is clicked, the currentTool is set to the move tool, the movePanel's
        /// background color is set to light gray, and the cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void movePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.move);
            UpdateSelected();
            movePanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the user clicks on the textPanel, the currentTool is set to the text tool, the
        /// textPanel's background color is set to light gray, and the cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void textPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.text);
            UpdateSelected();
            textPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// This function is called when the user double clicks on the text panel. It sets the current
        /// tool to the text tool, updates the selected tool, and sets the text panel's background color
        /// to light gray. Then, it opens the font dialog box and sets the font to the font selected by
        /// the user
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The font that was selected in the font dialog.
        private void textPanel_DoubleClick(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.text);
            UpdateSelected();
            textPanel.BackColor = System.Drawing.Color.LightGray;

            if (fontDialog.ShowDialog() != DialogResult.OK)
                return;
            font = fontDialog.Font;
        }
        /// When the user clicks on the pointPanel, the currentTool is set to the point tool, the
        /// pointPanel's background color is set to light gray, and the cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void pointPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.point);
            UpdateSelected();
            pointPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the user clicks on the linePanel, the currentTool is set to the line tool, the
        /// linePanel's background color is set to light gray, and the cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void linePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.line);
            UpdateSelected();
            linePanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the user clicks on the rectangle panel, the current tool is set to the rectangle tool,
        /// the selected tool is updated, the rectangle panel's background color is set to light gray,
        /// and the cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void rectPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.rect);
            UpdateSelected();
            rectPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the ellipse button is clicked, the current tool is set to the ellipse tool, the
        /// selected tool is updated, the ellipse button's background color is set to light gray, and
        /// the cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void ellipsePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.ellipse);
            UpdateSelected();
            ellipsePanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the user clicks on the polygon button, the current tool is set to the polygon tool, the
        /// selected tool is updated, the polygon button is highlighted, and the cursor is set to an
        /// arrow
        /// 
        /// @param sender The control that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void polyPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.polygon);
            UpdateSelected();
            polyPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the delete button is clicked, the current tool is set to the delete tool, the selected
        /// tool is updated, the delete button's background color is set to light gray, and the cursor
        /// is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void deletePanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.delete);
            UpdateSelected();
            deletePanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the user clicks on the freeformPanel, the currentTool is set to the freeform tool, the
        /// freeformPanel's background color is set to light gray, and the cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void freeformPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.freeform);
            UpdateSelected();
            freeformPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the user clicks on the rectangle selection panel, the current tool is set to the
        /// rectangle selection tool, the panel's background color is set to light gray, and the cursor
        /// is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void rectSelPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.rectSel);
            UpdateSelected();
            rectSelPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the user clicks on the panPanel, the currentTool is set to the pan tool, the selected
        /// tool is updated, the panPanel's background color is set to light gray, and the cursor is set
        /// to the hand cursor
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void panPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.pan);
            UpdateSelected();
            panPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the magicPanel is clicked, the currentTool is set to the magic tool, the selected tool
        /// is updated, the magicPanel's background color is set to light gray, and the cursor is set to
        /// the arrow cursor
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void magicPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.magic);
            UpdateSelected();
            magicPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the bucketPanel is clicked, the currentTool is set to the bucket tool, the selected
        /// tool is updated, the bucketPanel's background color is set to light gray, and the cursor is
        /// set to an arrow
        /// 
        /// @param sender The control that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void bucketPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.bucket);
            UpdateSelected();
            bucketPanel.BackColor = System.Drawing.Color.LightGray;
        }
        /// When the pencilPanel is clicked, the currentTool is set to the pencil tool, the
        /// pencilPanel's background color is set to light gray, and the cursor is set to the arrow
        /// cursor
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void pencilPanel_Click(object sender, EventArgs e)
        {
            currentTool = GetTool(Tool.Type.pencil);
            UpdateSelected();
            pencilPanel.BackColor = System.Drawing.Color.LightGray;
        }
        MagicSelect magicSel = new MagicSelect(2);
        /// If the user double clicks on the magicPanel, then the magicSel dialog box will appear
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs The event data for the MouseDoubleClick event.
        /// 
        /// @return The result of the ShowDialog() method.
        private void magicPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (magicSel.ShowDialog() != DialogResult.OK)
                return;
        }
        /// When the form is closing, cancel the closing event and hide the form instead.
        /// 
        /// @param sender The object that raised the event.
        /// @param FormClosingEventArgs The FormClosingEventArgs class contains the event data for the
        /// FormClosing event.
        private void Tools_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// It's a function that is called when the user double clicks on the pencil icon in the
        /// toolbar.
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs e
        /// 
        /// @return The PenTool class is being returned.
        private void pencilPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            currentTool = GetTool(Tool.Type.pencil);
            UpdateSelected();
            pencilPanel.BackColor = System.Drawing.Color.LightGray;
            PenTool pt = new PenTool(new Graphics.Pen(DrawColor, (int)Tools.StrokeWidth, ImageView.SelectedBuffer.BitsPerPixel));
            if (pt.ShowDialog() != DialogResult.OK)
                return;
            Tools.StrokeWidth = pt.Pen.width;
            DrawColor = pt.Pen.color;
        }

        /// The function is called when the user double clicks on the bucket tool. It sets the current
        /// tool to the bucket tool, updates the selected tool, sets the bucket tool's background color
        /// to light gray, sets the cursor to an arrow, and then creates a new flood tool with the
        /// selected color and width
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs e
        /// 
        /// @return The currentTool is being returned.
        private void bucketPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            currentTool = GetTool(Tool.Type.bucket);
            UpdateSelected();
            bucketPanel.BackColor = System.Drawing.Color.LightGray;
            FloodTool pt = new FloodTool(new Graphics.Pen(DrawColor, (int)Tools.StrokeWidth, ImageView.SelectedBuffer.BitsPerPixel), currentTool.tolerance, ImageView.SelectedBuffer.BitsPerPixel);
            if (pt.ShowDialog() != DialogResult.OK)
                return;
            Tools.StrokeWidth = pt.Pen.width;
            DrawColor = pt.Pen.color;
            currentTool.tolerance = pt.Tolerance;
        }

        /// When the dropper panel is clicked, the current tool is set to the dropper tool, the selected
        /// tool is updated, the dropper panel's background color is set to light gray, and the cursor
        /// is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs The event data generated by a mouse event.
        private void dropperPanel_MouseClick(object sender, MouseEventArgs e)
        {
            currentTool = GetTool(Tool.Type.dropper);
            UpdateSelected();
            dropperPanel.BackColor = System.Drawing.Color.LightGray;
        }

        /// When the user clicks on the color1Box, a new ColorTool is created, and if the user clicks
        /// OK, the DrawColor is set to the color the user selected.
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs The mouse event arguments.
        /// 
        /// @return The color that was selected.
        private void color1Box_MouseClick(object sender, MouseEventArgs e)
        {
            ColorTool t = new ColorTool(DrawColor, ImageView.SelectedBuffer.BitsPerPixel);
            if (t.ShowDialog() != DialogResult.OK)
                return;
            DrawColor = t.Color;
            UpdateGUI();
        }

        /// It opens a color dialog box and sets the color of the erase tool to the color selected in
        /// the dialog box.
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs The mouse event arguments.
        /// 
        /// @return The color that was selected.
        private void color2Box_MouseClick(object sender, MouseEventArgs e)
        {
            ColorTool t = new ColorTool(EraseColor, ImageView.SelectedBuffer.BitsPerPixel);
            if (t.ShowDialog() != DialogResult.OK)
                return;
            EraseColor = t.Color;
            UpdateGUI();
        }

        /// When the value of the widthBox is changed, the width variable is set to the value of the
        /// widthBox
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void widthBox_ValueChanged(object sender, EventArgs e)
        {
            width = (int)widthBox.Value;
        }

        /// When the user clicks on the eraser panel, the current tool is set to the eraser tool, the
        /// selected tool is updated, the eraser panel's background color is set to light gray, and the
        /// cursor is set to an arrow
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs The event data generated by a mouse event.
        private void eraserPanel_MouseClick(object sender, MouseEventArgs e)
        {
            currentTool = GetTool(Tool.Type.eraser);
            UpdateSelected();
            eraserPanel.BackColor = System.Drawing.Color.LightGray;
        }

        /// When the user clicks on the switchBox, the program swaps the DrawColor and EraseColor
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs The event arguments for the mouse click event.
        private void switchBox_MouseClick(object sender, MouseEventArgs e)
        {
            ColorS s = DrawColor;
            DrawColor = eraseColor;
            EraseColor = s;
            UpdateGUI();
        }

        /// When the Tools menu is activated, update the GUI
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void Tools_Activated(object sender, EventArgs e)
        {
            UpdateGUI();
        }
    }
}
