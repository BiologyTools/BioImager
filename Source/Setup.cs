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
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        /// If the user has entered a path to the image file and a path to the library file (or has
        /// chosen to use Python), then the program will save the paths and close the dialog
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        /// 
        /// @return The DialogResult is being returned.
        private void okBut_Click(object sender, EventArgs e)
        {
            if (imgPathBox.Text != "")
            {
                Properties.Settings.Default.AppPath = imgPathBox.Text;
            }
            else if(!pythonRadioBut.Checked)
                return;
            if (libraryPathBox.Text != "" && libRadioBut.Checked)
            {
                Properties.Settings.Default.LibPath = libraryPathBox.Text;
            }
            Properties.Settings.Default.Setup = true;
            DialogResult = DialogResult.OK;
            Properties.Settings.Default.Save();
            this.Close();
        }

        /// If the user clicks the cancel button, the program will close and exit
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Application.Exit();
        }

        /// If the user selects the Python radio button, then the PMicroscope variable is set to true
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void pythonRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PMicroscope = pythonRadioBut.Checked;
            Properties.Settings.Default.PycroManager = false;
        }

        /// If the user clicks on the libRadioBut radio button, then the PMicroscope variable in the
        /// Settings.settings file is set to true
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void libRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PMicroscope = false;
            Properties.Settings.Default.PycroManager = false;
        }

        /// It opens a file dialog, and if the user selects a file, it sets the text of the imgPathBox to
        /// the path of the file
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The file path of the executable.
        private void setImagingBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Select Imaging Application Executable";
            if (d.ShowDialog() != DialogResult.OK)
                return;
            imgPathBox.Text = d.FileName;
        }

        /// The user clicks a button, a dialog box opens, the user selects a file, the file path is
        /// displayed in a text box, and a boolean value is set to false
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        /// 
        /// @return The file path of the selected file.
        private void setLibraryBut_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "Select Imaging Library";
            if (d.ShowDialog() != DialogResult.OK)
                return;
            libraryPathBox.Text = d.FileName;
            Properties.Settings.Default.PMicroscope = false;
        }

        private void micromanRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PycroManager = micromanRadioBut.Checked;
            Properties.Settings.Default.PMicroscope = false;
        }
    }
}
