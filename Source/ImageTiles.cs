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
    public partial class ImageTiles : Form
    {
        public ImageTiles()
        {
            InitializeComponent();
        }
        public int SizeX
        {
            get
            {
                return (int)xBox.Value;
            }
        }
        public int SizeY
        {
            get
            {
                return (int)yBox.Value;
            }
        }

        private void okBut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
