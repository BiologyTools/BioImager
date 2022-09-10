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
    public partial class PlaySpeed : Form
    {
        public PlaySpeed(bool timeEnabled, bool cEnabled, int zFps, int timeFps, int cFps)
        {
            InitializeComponent();
            if (!timeEnabled)
            {
                tPlayspeed.Enabled = false;
            }
            if (!cEnabled)
            {
                cPlayspeed.Enabled = false;
            }
        }

        public int TimePlayspeed
        {
            get
            {
                return (int)tPlayspeed.Value;
            }
        }

        public int ZPlayspeed
        {
            get
            {
                return (int)zPlayspeed.Value;
            }
        }

        public int CPlayspeed
        {
            get
            {
                return (int)cPlayspeed.Value;
            }
        }

        private void PlaySpeed_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
