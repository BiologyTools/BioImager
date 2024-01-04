using com.sun.source.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ucar.nc2.@internal.iosp.netcdf3;
using static loci.poi.hssf.util.HSSFColor;

namespace BioImager
{
    public partial class BioConsole : Form
    {
        public BioConsole()
        {
            InitializeComponent();
        }
        public static bool onTab = false;
        public static bool useBioformats = false;
        public static bool headless = false;
        public static bool newTab = false;
        /// It runs the code in the textbox and outputs the result to the console
        /// 
        /// @param sender The object that called the event.
        /// @param EventArgs The event arguments.
        private void runBut_Click(object sender, EventArgs e)
        {
            object o = Scripting.Script.RunString(textBox.Text);
            consoleBox.Text += textBox.Text + Environment.NewLine + o.ToString() + Environment.NewLine;
            textBox.Text = "";
        }

        /// The function takes the text from the textbox, and runs it as a macro in ImageJ.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The ImageJ.RunOnImage method returns a string.
        private void imagejBut_Click(object sender, EventArgs e)
        {
            if (ImageView.SelectedImage == null)
                return;
            ImageJ.RunOnImage(textBox.Text, headlessBox.Checked, tabRadioBut.Checked, biofBox.Checked, newTabBox.Checked);
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
                App.tabsView.AddTab(BioImage.OpenOME(file, false));
        }

        /// If the topMostBox checkbox is checked, then the form will be topmost
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = topMostBox.Checked;
        }

        /// When the user tries to close the form, cancel the closing event and hide the form instead.
        /// 
        /// @param sender The object that raised the event.
        /// @param FormClosingEventArgs The event data generated from the form closing.
        private void BioConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        int line = 0;
        /// If the user presses the up arrow key, the line variable is incremented and the textbox is
        /// set to the line in the consolebox that is the length of the consolebox minus 1 minus the
        /// line variable.
        /// If the user presses the down arrow key, the line variable is decremented and the textbox is
        /// set to the line in the consolebox that is the length of the consolebox minus 1 minus the
        /// line variable
        /// 
        /// @param sender The object that raised the event.
        /// @param KeyEventArgs The event arguments for the key press.
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

        /// If the tabRadioBut is checked, then onTab is true
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void tabRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            onTab = tabRadioBut.Checked;
        }

        /// If the checkbox is checked, then the variable useBioformats is set to true
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void biofBox_CheckedChanged(object sender, EventArgs e)
        {
            useBioformats = biofBox.Checked;
        }

        private void headlessBox_CheckedChanged(object sender, EventArgs e)
        {
            headless = headlessBox.Checked;
        }
        bool skip = false;
        string chs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private string pred = "";
        private List<string> preds = new List<string>();
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Text.Length == 0)
                return;
            if (skip)
            {
                skip = false;
                return;
            }
            pred = pred.Replace("\t", "").Replace("\r", "").Replace("\n", "");
            if (pred.Contains(' '))
                pred = textBox.Text.Remove(0, textBox.Text.LastIndexOf(' '));
            else
                pred = textBox.Text.Replace("\t", "").Replace("\r", "").Replace("\n", "");
            bool ends = false;
            char ch = ' ';
            int i = 0;
            int ind = -1;
            foreach (Char cs in pred)
            {
                ends = false;
                foreach (Char c in chs)
                {
                    if (cs == c)
                    {
                        ends = true;
                        ch = c;
                    }
                }
                if (!ends)
                    ind = i;
                i++;
            }
            if (ind != -1)
                pred = pred.Remove(0, ind + 1);

            preds.Clear();
            i = 0;
            ind = -1;
            foreach (var cs in ImageJ.Macro.Commands)
            {
                if (cs.Value.Name.StartsWith(pred))
                    preds.Add(cs.Value.Name);
            }
            foreach (var cs in ImageJ.Macro.Functions)
            {
                if (cs.Value[0].Name.StartsWith(pred))
                    preds.Add(cs.Value[0].Name);
            }
            predLabel.Text = "";
            foreach (var item in preds)
            {
                if (this.Width > TextRenderer.MeasureText(predLabel.Text, this.Font).Width)
                {
                    predLabel.Text += item + ", ";
                }
                else break;
            }
            if (textBox.Text.EndsWith('\t'))
            {
                string s = textBox.Text.TrimEnd('\t');
                skip = true;
                textBox.Text = s.Remove(s.Length - pred.Length, pred.Length) + preds[0];
            }
            textBox.SelectionStart = textBox.Text.Length;
        }

        private void newTabBox_CheckedChanged(object sender, EventArgs e)
        {
            newTab = newTabBox.Checked;
        }
    }
}
