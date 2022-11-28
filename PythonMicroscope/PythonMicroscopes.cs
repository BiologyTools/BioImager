using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bio.PythonMicroscope
{
    public partial class PythonMicroscopes : Form
    {
        CodeView view = new CodeView();
        public PythonMicroscopes()
        {
            InitializeComponent();
            panel.Controls.Add(view);
            view.Dock = DockStyle.Fill;
            int i = 0;
            foreach (System.Drawing.Imaging.PixelFormat p in (System.Drawing.Imaging.PixelFormat[])Enum.GetValues(typeof(System.Drawing.Imaging.PixelFormat)))
            {
                if (Properties.Settings.Default.PCameraFormat == p.ToString())
                    pxBox.SelectedItem = p.ToString();
                i++;
            }
            funcsBox.SelectedIndex = 4;
        }
        private void filterWheelBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PFilterWheel = filterWheelBox.Text;
        }

        private void stageBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PStage = stageBox.Text;
        }

        private void editConfigBut_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "/PythonMicroscope/microscope/config.txt");
        }

        private void initBut_Click(object sender, EventArgs e)
        {
            if(startBut.Text == "Start Server")
            {
                Microscope.pMicroscope.Initialize(Properties.Settings.Default.PFilterWheel, Properties.Settings.Default.PStage);
                PMicroscope.Start();
                startBut.Text = "Stop Server";
            }
            else
            {
                PMicroscope.Stop();
                startBut.Text = "Start Server";
            }
            
        }

        private void funcsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            view.TextBox.Text = System.IO.File.ReadAllText(Application.StartupPath + "/PythonMicroscope/microscope/" + funcsBox.SelectedItem.ToString());
        }

        private void saveBut_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(Application.StartupPath + "/PythonMicroscope/microscope/" + funcsBox.SelectedItem.ToString(), view.TextBox.Text);
        }

        private void pxBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PCameraFormat = pxBox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
