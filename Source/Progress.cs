using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioImager
{
    public partial class Progress : Form
    {
        Stopwatch watch = new Stopwatch();
        public Progress(string file, string status)
        {
            InitializeComponent();
            watch.Start();
            statusLabel.Text = status;
            fileLabel.Text = file;
            timer.Start();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status
        {
            get { return statusLabel.Text; }
            set 
            {
                // Use Invoke to update the label on the UI thread
                if (statusLabel.InvokeRequired)
                {
                    statusLabel.Invoke(() => statusLabel.Text = value);
                }
                else
                {
                    statusLabel.Text = value;
                }
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float ProgressValue
        {
            get { return progressBar.Value; }
            set 
            {
                if (progressBar.InvokeRequired)
                {
                    if (progressBar.Maximum < value)
                    {
                        progressBar.Invoke(() => progressBar.Value = (int)(value / 100));
                    }
                    else
                        progressBar.Invoke(() => progressBar.Value = (int)value);
                    return;
                }
                if (progressBar.Maximum < value)
                    progressBar.Value = (int)(value * 100);
                else
                    progressBar.Value = (int)(value);
            }
        }

        public void UpdateProgress(int p)
        {
            progressBar.Value = p;
        }
        public void UpdateProgressF(float p)
        {
            if (p * 100 > progressBar.Maximum)
                return;
            progressBar.Value = (int)(p * 100);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = watch.Elapsed.Seconds + "." + watch.Elapsed.Milliseconds + "s";
        }
    }
}
