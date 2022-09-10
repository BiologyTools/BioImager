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
                    channel.Min = (int)value;
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
                    channel.Max = (int)value;
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
        private Graphics g;

        public void UpdateChannel(Channel c)
        {
            channel = c;
        }
        public void UpdateView()
        {
            this.Invalidate();
        }
        private void HistogramControl_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(Color.LightGray);
            g.TranslateTransform(-graphMin, 0);
            //g.ScaleTransform(scale.Width, scale.Height);
            if (channel.statistics == null)
                return;
            fy = ((float)this.Height) / (float)channel.statistics.StackMedian;
            fx = ((float)this.Width) / ((float)graphMax);
            Pen black = new Pen(Color.FromArgb(150,0,0,0), bin * fx);
            Pen blue = new Pen(Color.FromArgb(150,0, 0, 255), bin * fx);
            float sumbins = 0;
            float sumbin = 0;
            int binind = 0;
            int bininds = 0;
            g.DrawString("(" + (mouseX / fx).ToString() + ", " + (Math.Abs(graphMin - ((Height - mouseY) / fy))).ToString() + ")"
                , SystemFonts.DefaultFont, Brushes.Black, mouseX + 10, mouseY -20);
            int gmax = graphMax;
            if (App.Image.bitsPerPixel <= 8)
                gmax = 255;
             
            for (float x = 0; x < gmax; x++)
            {
                if (StackHistogram)
                {
                    //Lets draw the stack histogram
                    float val = (float)ImageView.SelectedImage.Statistics.StackValues[(int)x];
                    sumbin += val;
                    if (binind == bin)
                    {
                        float v = sumbin / binind;
                        float yy = this.Height - (fy * v);
                        g.DrawLine(black, new PointF(fx * x, this.Height), new PointF(fx * x, yy));
                        binind = 0;
                        sumbin = 0;
                    }
                }
                //Lets draw the channel histogram on top of the stack histogram.
                float vals = (float)channel.statistics.StackValues[(int)x];
                sumbins += vals;
                if (bininds == bin)
                {
                    float v = sumbins / bininds;
                    float yy = this.Height - (fy * v);
                    g.DrawLine(blue, new PointF(fx * x, this.Height), new PointF(fx * x, yy));
                    bininds = 0;
                    sumbins = 0;
                }
                binind++;
                bininds++;
                
            }
            g.DrawLine(Pens.Red, new PointF((fx * channel.Max), 0), new PointF((fx * channel.Max), this.Height));
            g.DrawLine(Pens.Red, new PointF(fx * channel.Min, 0), new PointF(fx * channel.Min, this.Height));

            float tick = 6;
            if (axisTicks)
            {
                if (axisNumbers)
                for (float x = 0; x < graphMax; x += 1000)
                {
                    g.DrawString(x.ToString(), SystemFonts.DefaultFont, Brushes.Black, fx * x, tick + 2);
                }
                for (float x = 0; x < graphMax; x += 100)
                {
                    g.DrawLine(Pens.Black, new PointF((fx * x), 0), new PointF((fx * x), tick));
                }
                for (float x = 0; x < graphMax; x += 50)
                {
                    g.DrawLine(Pens.Black, new PointF((fx * x), 0), new PointF((fx * x), 3));
                }
            }
            blue.Dispose();
            black.Dispose();
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void HistogramControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            Invalidate();
        }

        private void HistogramControl_MouseMove(object sender, MouseEventArgs e)
        {
            mouseValX = e.X / fx;
            mouseValY = e.Y / fy;
        }

        private void HistogramControl_SizeChanged(object sender, EventArgs e)
        {
            InitGraphics();
        }
        private void InitGraphics()
        {
            if (Width == 0 || Height == 0)
                return;
            bm = new Bitmap(Width, Height);
            if (g != null)
                g.Dispose();
            g = Graphics.FromImage(bm);
        }

        private void copyViewToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(bm);
        }

        private void setMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.channelsTool.SelectedChannel.Max = (int)MouseValX;
            Invalidate();
            App.viewer.UpdateImage();
        }

        private void setMinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.channelsTool.SelectedChannel.Min = (int)MouseValX;
            Invalidate();
            App.viewer.UpdateImage();
        }

        private void setMaxAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Channel c in ImageView.SelectedImage.Channels)
            {
                c.Max = (int)MouseValX;
            }
        }

        private void setMinAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Channel c in ImageView.SelectedImage.Channels)
            {
                c.Min = (int)MouseValX;
            }
        }
    }
}
