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
    public partial class BioConsole : Form
    {
        public BioConsole()
        {
            InitializeComponent();
        }
        public static bool onTab = false;
        public static bool useBioformats = false;
        private void runBut_Click(object sender, EventArgs e)
        {
            object o = Scripting.Script.RunString(textBox.Text);
            consoleBox.Text += o.ToString() + Environment.NewLine;
            textBox.Text = "";
        }

        private void imagejBut_Click(object sender, EventArgs e)
        {
            ImageJ.RunOnImage(textBox.Text, headlessBox.Checked, tabRadioBut.Checked, biofBox.Checked);
            consoleBox.Text += textBox.Text + Environment.NewLine;
            textBox.Text = "";
            string filename = "";
            if (ImageView.SelectedImage.ID.EndsWith(".ome.tif"))
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(ImageView.SelectedImage.ID);
                filename = filename.Remove(filename.Length - 4, 4);
            }
            else
                filename = System.IO.Path.GetFileNameWithoutExtension(ImageView.SelectedImage.ID);
            string file = System.IO.Path.GetDirectoryName(ImageView.SelectedImage.ID) + "/" + filename + ".ome.tif";
            if (ImageView.SelectedImage.ID.EndsWith(".ome.tif"))
                ImageView.SelectedImage.Update();
            else
                App.tabsView.AddTab(BioImage.OpenOME(file));
        }

        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = topMostBox.Checked;
        }

        private void BioConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        int line = 0;
        private void BioConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                line++;
                textBox.Text = consoleBox.Lines[consoleBox.Lines.Length - 1 - line];
            }
            if (e.KeyCode == Keys.Down)
            {
                line--;
                textBox.Text = consoleBox.Lines[consoleBox.Lines.Length - 1 - line];
            }
        }

        private void tabRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            onTab = tabRadioBut.Checked;
        }

        private void biofBox_CheckedChanged(object sender, EventArgs e)
        {
            useBioformats = biofBox.Checked;
        }
    }
}
