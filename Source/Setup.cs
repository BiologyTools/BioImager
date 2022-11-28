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
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void okBut_Click(object sender, EventArgs e)
        {
            if (imgPathBox.Text != "")
            {
                Properties.Settings.Default.AppPath = imgPathBox.Text;
            }
            else
                return;
            if (libraryPathBox.Text != "" && libRadioBut.Checked)
            {
                Properties.Settings.Default.LibPath = libraryPathBox.Text;
            }
            else if (!pythonRadioBut.Checked)
                return;
            Properties.Settings.Default.Setup = true;
            DialogResult = DialogResult.OK;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Application.Exit();
        }

        private void pythonRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PMicroscope = pythonRadioBut.Checked;
        }

        private void libRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PMicroscope = libRadioBut.Checked;
        }

        private void setImagingBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Select Imaging Application Executable";
            if (d.ShowDialog() != DialogResult.OK)
                return;
            imgPathBox.Text = d.FileName;
        }

        private void setLibraryBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Select Imaging Library";
            if (d.ShowDialog() != DialogResult.OK)
                return;
            libraryPathBox.Text = d.FileName;
           
        }
    }
}
