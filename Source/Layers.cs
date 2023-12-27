using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BioImager.ImageView;

namespace BioImager
{
    public partial class Layers : Form
    {
        public Layers()
        {
            InitializeComponent();
        }

        private void Layers_Activated(object sender, EventArgs e)
        {
            layersBox.Items.Clear();
            foreach (Layer d in App.viewer.Layers)
            {
                ListViewItem l = new ListViewItem(d.Resolution.ToString());
                l.Checked = d.Enabled;
                layersBox.Items.Add(l);
            }
        }

        private void layersBox_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            double r = double.Parse(e.Item.Text);
            for (int i = 0; i < layersBox.Items.Count; i++)
            {
                if (r == App.viewer.Layers[i].Resolution)
                    App.viewer.Layers[i] = new Layer(App.viewer.Layers[i].Resolution,e.Item.Checked);
            }
        }
    }
}
