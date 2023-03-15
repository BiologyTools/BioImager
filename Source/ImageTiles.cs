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

        /// The function is called when the user clicks the OK button
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void okBut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        /// The function cancelBut_Click is a private function that takes in two parameters, an object
        /// sender and an EventArgs e. The function sets the DialogResult to DialogResult.Cancel and
        /// closes the form
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
