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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            #if DEBUG
            MessageBox.Show("Application is running in Debug mode.");
            #endif
            versionLabel.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BiologyTools/BioImager");
        }
    }
}
