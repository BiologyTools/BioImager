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
    public partial class Recorder : Form
    {
        public static string log;
        public static void AddLine(string s)
        {
            log += s + Environment.NewLine;
        }
        public static Recorder recorder = null;
        public static bool recordMicroscope = true;
        public Recorder()
        {
            InitializeComponent();
        }

        private void clearBut_Click(object sender, EventArgs e)
        {
            textBox.Clear();
            log = "";
        }

        private void delLineBut_Click(object sender, EventArgs e)
        {
            string[] sts = textBox.Lines;
            string[] st = new string[sts.Length - 1];
            for (int i = 0; i < st.Length; i++)
            {
                st[i] = sts[i];
            }
            textBox.Lines = st;
        }

        private void Recorder_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
           // update = true;
           // log = textBox.Text;
        }

        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = topMostBox.Checked;
        }

        private void Recorder_Activated(object sender, EventArgs e)
        {
            textBox.Text = log;
        }

        private void microRecBox_CheckedChanged(object sender, EventArgs e)
        {
            recordMicroscope = microRecBox.Checked;
        }
    }
}
