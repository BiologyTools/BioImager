﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bio
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
        public void UpdateProgress(int p)
        {
            progressBar.Value = p;
        }
        public void UpdateProgressF(float p)
        {
            progressBar.Value = (int)(p * 100);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = watch.Elapsed.Seconds + "." + watch.Elapsed.Milliseconds + "s";
        }
    }
}
