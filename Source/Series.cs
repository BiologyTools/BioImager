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
    public partial class Series : Form
    {
        public Series()
        {
            InitializeComponent();
            UpdateItems();
        }

        public void UpdateItems()
        {
           imagesBox.Items.Clear();
            foreach (BioImage item in Images.images)
            {
                imagesBox.Items.Add(item);
            }
        }

        private void addBut_Click(object sender, EventArgs e)
        {
            if (imagesBox.SelectedIndices.Count == 0)
                return;
            foreach (BioImage item in imagesBox.SelectedItems)
            {
                seriesBox.Items.Add(item);
            }
        }

        private void removeBut_Click(object sender, EventArgs e)
        {
            if (seriesBox.SelectedIndices.Count == 0)
                return;
            foreach (BioImage item in imagesBox.SelectedItems)
            {
                seriesBox.Items.Remove(item);
            }
        }

        private void Series_Activated(object sender, EventArgs e)
        {
            UpdateItems();
        }

        private void saveOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            int i = 0;
            List<string> sts = new List<string>();
            foreach (BioImage item in seriesBox.Items)
            {
                sts.Add(item.ID);
                item.series = i;
                i++;
            }
            BioImage.SaveOMESeries(sts.ToArray() , saveFileDialog.FileName, Properties.Settings.Default.Planes);
        }

        private void addAllBut_Click(object sender, EventArgs e)
        {
            foreach (BioImage item in imagesBox.Items)
            {
                seriesBox.Items.Add(item);
            }
        }

        private void removeAllBut_Click(object sender, EventArgs e)
        {
            seriesBox.Items.Clear();
        }

        private void Series_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
