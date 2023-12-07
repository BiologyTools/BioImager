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
    public partial class RangeTool : Form
    {
        public RangeTool(bool timeEnabled,bool cEnabled,int zmin,int zmax,int timeMin, int timeMax,int cmin,int cmax)
        {
            InitializeComponent();
            zMinBox.Value = zmin;
            zMaxBox.Value = zmax;
            if (!timeEnabled)
            {
                timeMinBox.Enabled = false;
                timeMaxBox.Enabled = false;
                return;
            }
            if(!cEnabled)
            {
                cMinBox.Enabled = false;
                cMaxBox.Enabled = false;
            }
            timeMinBox.Value = timeMin;
            timeMaxBox.Value = timeMax;

            cMinBox.Value = cmin;
            cMaxBox.Value = cmax;
        }

        public int ZMin
        {
            get
            {
                return (int)zMinBox.Value;
            }
        }

        public int ZMax
        {
            get
            {
                return (int)zMaxBox.Value;
            }
        }

        public int TimeMin
        {
            get
            {
                return (int)timeMinBox.Value;
            }
        }

        public int TimeMax
        {
            get
            {
                return (int)timeMaxBox.Value;
            }
        }

        public int CMin
        {
            get
            {
                return (int)cMinBox.Value;
            }
        }

        public int CMax
        {
            get
            {
                return (int)cMaxBox.Value;
            }
        }

        private void RangeTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

    }
}
