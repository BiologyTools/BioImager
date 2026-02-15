using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioImager
{
    public partial class Plot : Form
    {
        ScottPlot.Plot model;
        string file;
        string name;
        Bitmap bitmap;
        List<double[]> data = new List<double[]>();
        static Dictionary<string, Plot> plots = new Dictionary<string, Plot>();
        public Plot(double[] vals, string name, PlotType typ)
        {
            InitializeComponent();
            model = new ScottPlot.Plot();
            data.Add(vals);
            if (typ == PlotType.Scatter)
                InitScatter();
            else
            if (typ == PlotType.Bar)
                InitBars();
        }
        public enum PlotType
        {
            Bar,
            Scatter
        }
        PlotType type;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PlotType Type
        {
            get { return type; }
            set { type = value; }
        }
        public static Dictionary<string, Plot> Plots
        {
            get { return plots; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<double[]> Data
        {
            get { return data; }
            set { data = value; UpdateImage(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Bitmap Image
        {
            get { return bitmap; }
            set { bitmap = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScottPlot.Plot Model
        {
            get { return model; }
            set { model = value; UpdateImage(); }
        }
        /// The function updates an image by saving it as a PNG file, setting the window title, creating
        /// a new Pixbuf object from the file, and redrawing the image.
        public void UpdateImage()
        {
            model.SavePng(file, Width, Height);
            this.Text = name;
            bitmap = (Bitmap)Bitmap.FromFile(file);
        }
        /// The function initializes a scatter plot by adding data points to the model.
        private void InitScatter()
        {
            model.Clear();
            List<Coordinates> cs = new List<Coordinates>();
            for (int i = 0; i < data.Count; i++)
            {
                for (int x = 0; x < data[i].Length; x++)
                {
                    cs.Add(new ScottPlot.Coordinates(x, data[i][x]));
                }
            }
            model.Add.Scatter(cs);
        }
        /// The function initializes a bar chart by creating a list of bar series and adding them to the
        /// model.
        private void InitBars()
        {
            model.Clear();
            foreach (double[] items in data)
            {
                int i = 0;
                List<Bar> bars = new List<Bar>();
                foreach (double item in items)
                {
                    Bar b = new Bar();
                    b.Value = item;
                    b.Position = i;
                    bars.Add(b);
                    i++;
                }
                model.Add.Bars(bars);
            }
        }

        private void Plot_ResizeEnd(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void Plot_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Image, 0, 0);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            Image.Save(saveFileDialog.FileName);
        }

        private void saveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveCSVFileDialog.ShowDialog() != DialogResult.OK) return;
            string s = "";
            foreach (double[] item in data)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    s += i + ",";
                }
                s += Environment.NewLine;
                foreach (double d in item)
                {
                    s += d + "," + Environment.NewLine;
                }
            }
            File.WriteAllText(saveCSVFileDialog.FileName, s);
        }
    }
}
