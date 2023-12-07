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
    public partial class SelectRecording : Form
    {
        public SelectRecording()
        {
            InitializeComponent();
            foreach (var item in Automation.Properties.Values)
            {
                propsBox.Items.Add(item);
            }
            foreach (var item in Automation.Recordings.Values)
            {
                recsBox.Items.Add(item);
            }
        }
        private Automation.Recording rec = null;
        public Automation.Recording Recording
        {
            get
            {
                return rec;
            }
        }

        private void recsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            rec = (Automation.Recording)recsBox.SelectedItem;
        }

        private void propsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            rec = (Automation.Recording)propsBox.SelectedItem;
        }

        private void okBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
