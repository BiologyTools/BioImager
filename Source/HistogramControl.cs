using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bio
{
    public partial class HistogramControl : UserControl
    {
        public HistogramControl(Channel c)
        {
            channel = c;
            this.Dock = DockStyle.Fill;
            InitializeComponent();
            if (c.BitsPerPixel == 8)
            {
                graphMax = 255;
                Bin = 1;
            }
            else
            {
                graphMax = ushort.MaxValue;
                Bin = 10;
            }

        }
        private Channel channel = null;
        private float bin = 10;
        public float Bin
        {
            get
            {
                return bin;
            }
            set
            {
                bin = value;
            }
        }
        private int min = 0;
        public float Min
        {
            get { return min; }
            set 
            { 
                if(channel!=null)
                    channel.range[App.channelsTool.SelectedSample].Min = (int)value;
                min = (int)value;
            }
        }
        private int max = 0;
        public float Max
        {
            get { return max; }
            set
            {
                if (channel != null)
                    channel.range[App.channelsTool.SelectedSample].Max = (int)value;
                max = (int)value;
            }
        }
        private int graphMax = ushort.MaxValue;
        public int GraphMax
        {
            get { return graphMax; }
            set { graphMax = value; }
        }
        private int graphMin = 0;
        public int GraphMin
        {
            get { return graphMin; }
            set { graphMin = value; }
        }
        private bool stackHistogram = true;
        public bool StackHistogram
        {
            get
            {
                return stackHistogram;
            }
            set
            {
                stackHistogram = value;
            }
        }
        private int mouseX = 0;
        private int mouseY = 0;
        public int MouseX
        {
            get
            {
                return mouseX;
            }
        }
        public int MouseY
        {
            get
            {
                return mouseX;
            }
        }
        private float mouseValX = 0;
        private float mouseValY = 0;
        public float MouseValX
        {
            get
            {
                return mouseValX;
            }
        }
        public float MouseValY
        {
            get
            {
                return mouseValY;
            }
        }

        private bool axisNumbers = true;
        public bool AxisNumbers
        {
            get { return axisNumbers;}
            set { axisNumbers = value; }
        }

        private bool axisTicks = true;
        public bool AxisTicks
        {
            get { return axisTicks; }
            set { axisTicks = value; }
        }

        private float fx = 0;
        private float fy = 0;
        private Bitmap bm;
        private System.Drawing.Graphics g;

        /// This function updates the channel variable with the channel that is passed in
        /// 
        /// @param Channel The channel to update
        public void UpdateChannel(Channel c)
        {
            channel = c;
        }
        /// > This function is called when the view needs to be updated
        public void UpdateView()
        {
            this.Invalidate();
        }
        /// It draws a histogram of the image
        /// 
        /// @param sender The object that raised the event.
        /// @param PaintEventArgs e
        private void HistogramControl_Paint(object sender, PaintEventArgs e)
        {
            if (graphMax == 0)
                graphMax = ushort.MaxValue;
            g.Clear(Color.LightGray);
            g.ResetTransform();
            g.TranslateTransform(-graphMin, 0);
            string st = "";
            fx = ((float)this.Width) / ((float)graphMax);
            float maxmedian = 0;
            int maxChan = 0;
            for (int cc = 0; cc < ImageView.SelectedImage.Channels.Count; cc++)
            {
                for (int i = 0; i < ImageView.SelectedImage.Channels[cc].stats.Length; i++)
                {
                    float f = ImageView.SelectedImage.Channels[cc].stats[i].StackMedian;
                    if (maxmedian < f)
                    {
                        maxmedian = f;
                        maxChan = cc;
                    }
                }
            }
            fy = ((float)this.Height) / maxmedian;
            for (int c = 0; c < ImageView.SelectedImage.Channels.Count; c++)
            {
                Channel channel = ImageView.SelectedImage.Channels[c];
                for (int i = 0; i < channel.range.Length; i++)
                {
                    Statistics stat;
                    if (ImageView.SelectedImage.RGBChannelCount == 1)
                        stat = channel.stats[0];
                    else
                        stat = channel.stats[i];

                    Pen black = new Pen(Color.FromArgb(35, 0, 0, 0), bin * fx);
                    Pen blackd = new Pen(Color.FromArgb(150, 0, 0, 0), bin * fx);
                    Pen pen = null;
                    Pen pend = null;
                    int dark = 200;
                    int light = 50;
                    if (channel.Emission != 0)
                    {
                        pen = new Pen(SpectralColor(channel.Emission), bin * fx);
                        pen.Color = Color.FromArgb(light, pen.Color);
                        pend = new Pen(SpectralColor(channel.Emission), bin * fx);
                        pend.Color = Color.FromArgb(dark, pen.Color);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            pen = new Pen(Color.FromArgb(light, 255, 0, 0), bin * fx);
                            pend = new Pen(Color.FromArgb(dark, 255, 0, 0), bin * fx);
                        }
                        else if (i == 1)
                        {
                            pen = new Pen(Color.FromArgb(light, 0, 255, 0), bin * fx);
                            pend = new Pen(Color.FromArgb(dark, 0, 255, 0), bin * fx);
                        }
                        else
                        {
                            pen = new Pen(Color.FromArgb(light, 0, 0, 255), bin * fx);
                            pend = new Pen(Color.FromArgb(dark, 0, 0, 255), bin * fx);
                        }
                    }

                    g.DrawLine(Pens.Black, new PointF(mouseX, 0), new PointF(mouseX, this.Height));
                    int gmax = graphMax;
                    if (App.Image.bitsPerPixel <= 8)
                        gmax = 255;


                    float sumbins = 0;
                    float sumbin = 0;
                    int binind = 0;
                    int bininds = 0;
                    PointF? prevs = null;
                    PointF? prev = null;
                    for (float x = 0; x < gmax; x++)
                    {
                        if (StackHistogram && c == ImageView.SelectedImage.Channels.Count - 1)
                        {
                            //Lets draw the stack histogram.
                            float val = (float)ImageView.SelectedImage.Statistics.StackValues[(int)x];
                            sumbin += val;
                            if (binind == bin)
                            {
                                float v = sumbin / binind;
                                float yy = this.Height - (fy * v);
                                if (prevs != null)
                                {
                                    g.DrawLine(blackd, prevs.Value, new PointF(fx * x, yy));
                                }
                                g.DrawLine(black, new PointF(fx * x, this.Height), new PointF(fx * x, yy));
                                prevs = new PointF(fx * x, yy);
                                binind = 0;
                                sumbin = 0;
                            }
                        }
                        //Lets draw the channel histogram on top of the stack histogram.
                        float rv = stat.StackValues[(int)x];
                        sumbins += rv;
                        if (bininds == bin)
                        {
                            g.DrawLine(pen, new PointF(fx * x, this.Height), new PointF(fx * x, this.Height - (fy * (sumbins / bininds))));
                            if (prev != null)
                            {
                                g.DrawLine(pend, prev.Value, new PointF(fx * x, this.Height - (fy * (sumbins / bininds))));
                            }
                            prev = new PointF(fx * x, this.Height - (fy * (sumbins / bininds)));
                            bininds = 0;
                            sumbins = 0;
                        }
                        binind++;
                        bininds++;

                    }
                    
                    g.DrawLine(pend, new PointF((fx * channel.range[i].Max), 0), new PointF((fx * channel.range[i].Max), this.Height));
                    g.DrawLine(pend, new PointF(fx * channel.range[i].Min, 0), new PointF(fx * channel.range[i].Min, this.Height));

                    black.Dispose();
                    blackd.Dispose();
                    pen.Dispose();
                    pend.Dispose();
                }
                
            }

            float tick = 6;
            if (axisTicks)
            {
                if (axisNumbers)
                {
                    for (float x = 0; x < graphMax; x += 2000)
                    {
                        SizeF s = g.MeasureString(x.ToString(), SystemFonts.DefaultFont);
                        g.DrawString(x.ToString(), SystemFonts.DefaultFont, Brushes.Black, (fx * x) - (s.Width/2), tick + 6);
                        g.DrawLine(Pens.Black, new PointF((fx * x), 0), new PointF((fx * x), tick + 3));
                    }
                    for (float x = 0; x < graphMax; x += 1000)
                    {
                        g.DrawLine(Pens.Black, new PointF((fx * x), 0), new PointF((fx * x), tick));
                    }
                }
                if (graphMax <= 16383)
                for (float x = 0; x < graphMax; x += 100)
                {
                    g.DrawLine(Pens.Black, new PointF((fx * x), 0), new PointF((fx * x), tick));
                }
                if (graphMax <= 255)
                {
                    for (float x = 0; x < graphMax; x += 50)
                    {
                        g.DrawLine(Pens.Black, new PointF((fx * x), 0), new PointF((fx * x), 4));
                    }
                    for (float x = 0; x < graphMax; x += 10)
                    {
                        g.DrawLine(Pens.Black, new PointF((fx * x), 0), new PointF((fx * x), 2));
                    }
                }
            }

            if (ImageView.SelectedImage.RGBChannelCount == 3)
            {
                if (ImageView.SelectedImage.Channels.Count > 2)
                {
                    st =
                        "(" + (mouseX / fx).ToString() +
                        ",R:" + ImageView.SelectedImage.Channels[2].stats[2].StackValues[(int)(mouseX / fx)].ToString() +
                        ",G:" + ImageView.SelectedImage.Channels[1].stats[1].StackValues[(int)(mouseX / fx)].ToString() +
                        ",B:" + ImageView.SelectedImage.Channels[0].stats[0].StackValues[(int)(mouseX / fx)].ToString() + ")";
                }
                else
                {
                    st =
                        "(" + (mouseX / fx).ToString() +
                        ",R:" + ImageView.SelectedImage.Channels[0].stats[2].StackValues[(int)(mouseX / fx)].ToString() +
                        ",G:" + ImageView.SelectedImage.Channels[0].stats[1].StackValues[(int)(mouseX / fx)].ToString() +
                        ",B:" + ImageView.SelectedImage.Channels[0].stats[0].StackValues[(int)(mouseX / fx)].ToString() + ")";
                }
            }
            else
            {
                int x = (int)(mouseX / fx);
                if (ImageView.SelectedImage.bitsPerPixel < 8 && ImageView.SelectedImage.Channels[0].stats[0].StackValues.Length < 255)
                    st = "(" + (mouseX / fx).ToString() + "," + ImageView.SelectedImage.Channels[0].stats[0].StackValues[x].ToString() + ")";

            }
            SizeF sf = g.MeasureString(st, SystemFonts.DefaultFont);
            g.DrawString(st, SystemFonts.DefaultFont, Brushes.Black, mouseX, mouseY + sf.Height);

            e.Graphics.DrawImage(bm, 0, 0);
        }

        /// The function is called when the user clicks the mouse on the histogram control
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs 
        private void HistogramControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            Invalidate();
        }

        /// The function takes the mouse position and divides it by the scaling factor to get the actual
        /// value of the mouse position
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs e.X and e.Y are the mouse coordinates.
        private void HistogramControl_MouseMove(object sender, MouseEventArgs e)
        {
            mouseValX = e.X / fx;
            mouseValY = e.Y / fy;
        }

        /// > When the size of the control changes, we need to re-initialize the graphics
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        private void HistogramControl_SizeChanged(object sender, EventArgs e)
        {
            InitGraphics();
        }
        /// If the width or height of the control is zero, then return. Otherwise, create a new bitmap
        /// with the width and height of the control, and create a new graphics object from the bitmap
        /// 
        /// @return A bitmap object.
        private void InitGraphics()
        {
            if (Width == 0 || Height == 0)
                return;
            bm = new Bitmap(Width, Height);
            if (g != null)
                g.Dispose();
            g = System.Drawing.Graphics.FromImage(bm);
        }

        /// It takes the image from the picturebox and puts it on the clipboard
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void copyViewToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(bm);
        }

        /// When the user clicks on the "Set Max" menu item, the selected channel's range's max value is
        /// set to the current mouse X value
        /// 
        /// @param sender System.Object
        /// @param EventArgs e
        private void setMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.channelsTool.SelectedChannel.range[App.channelsTool.SelectedSample].Max = (int)MouseValX;
            Invalidate();
            App.viewer.UpdateImage();
        }

        /// When the user clicks on the "Set Min" menu item, the selected channel's range's minimum
        /// value is set to the current mouse position
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void setMinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.channelsTool.SelectedChannel.range[App.channelsTool.SelectedSample].Min = (int)MouseValX;
            Invalidate();
            App.viewer.UpdateImage();
        }

        /// It sets the maximum value of the selected channel to the current mouse position.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void setMaxAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Channel c in ImageView.SelectedImage.Channels)
            {
                for (int i = 0; i < c.range.Length; i++)
                {
                    c.range[i].Max = (int)MouseValX;
                }
            }
        }

        /// This function sets the minimum value of the selected channel to the current mouse position
        /// 
        /// @param sender
        /// @param EventArgs e
        private void setMinAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Channel c in ImageView.SelectedImage.Channels)
            {
                for (int i = 0; i < c.range.Length; i++)
                {
                    c.range[i].Min = (int)MouseValX;
                }
            }
        }
        /// > The function takes a wavelength in nanometers and returns a color
        /// 
        /// @param l the wavelength of the light in nanometers
        /// 
        /// @return A color.
        Color SpectralColor(double l) // RGB <0,1> <- lambda l <400,700> [nm]
        {
            double t;
            double r = 0;
            double g = 0;
            double b = 0;
            if ((l >= 400.0) && (l < 410.0)) { t = (l - 400.0) / (410.0 - 400.0); r = +(0.33 * t) - (0.20 * t * t); }
            else if ((l >= 410.0) && (l < 475.0)) { t = (l - 410.0) / (475.0 - 410.0); r = 0.14 - (0.13 * t * t); }
            else if ((l >= 545.0) && (l < 595.0)) { t = (l - 545.0) / (595.0 - 545.0); r = +(1.98 * t) - (t * t); }
            else if ((l >= 595.0) && (l < 650.0)) { t = (l - 595.0) / (650.0 - 595.0); r = 0.98 + (0.06 * t) - (0.40 * t * t); }
            else if ((l >= 650.0) && (l < 700.0)) { t = (l - 650.0) / (700.0 - 650.0); r = 0.65 - (0.84 * t) + (0.20 * t * t); }
            if ((l >= 415.0) && (l < 475.0)) { t = (l - 415.0) / (475.0 - 415.0); g = +(0.80 * t * t); }
            else if ((l >= 475.0) && (l < 590.0)) { t = (l - 475.0) / (590.0 - 475.0); g = 0.8 + (0.76 * t) - (0.80 * t * t); }
            else if ((l >= 585.0) && (l < 639.0)) { t = (l - 585.0) / (639.0 - 585.0); g = 0.84 - (0.84 * t); }
            if ((l >= 400.0) && (l < 475.0)) { t = (l - 400.0) / (475.0 - 400.0); b = +(2.20 * t) - (1.50 * t * t); }
            else if ((l >= 475.0) && (l < 560.0)) { t = (l - 475.0) / (560.0 - 475.0); b = 0.7 - (t) + (0.30 * t * t); }
            r *= 255;
            g *= 255;
            b *= 255;
            return Color.FromArgb(255, (int)r, (int)g, (int)b);
        }
    }
}
