using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using loci.common.services;
using loci.formats;
using loci.formats.services;
using ome.xml.model.primitives;
using loci.formats.meta;

namespace BioImager
{
    public partial class Resolutions : Form
    {
        private int res;
        public int Resolution
        {
            get{return res;}
        }

        public Resolutions(List<BioLib.Resolution> res)
        {
            InitializeComponent();
            foreach (var item in res)
            {
                resBox.Items.Add(item);
            }
        }

        private void resBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            res = resBox.SelectedIndex;
        }

        private void okBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
