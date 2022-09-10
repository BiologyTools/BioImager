using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Bio
{
    public partial class ChannelsTool : Form
    {
        private HistogramControl hist = null;
        public List<Channel> Channels
        {
            get
            {
                return App.Channels;
            }
        }
        public Channel SelectedChannel
        {
            get
            {
                if (channelsBox.SelectedIndex != -1)
                    return Channels[channelsBox.SelectedIndex];
                else
                    return Channels[0];
            }
        }

        public void UpdateItems()
        {
            channelsBox.Items.Clear();
            foreach (Channel item in Channels)
            {
                channelsBox.Items.Add(item);
            }
        }
        public ChannelsTool(List<Channel> Channels)
        {
            InitializeComponent();
            foreach (Channel item in Channels)
            {
                channelsBox.Items.Add(item);
            }
            channelsBox.SelectedIndex = 0;
            minBox.Value = Channels[0].Min;
            maxBox.Value = Channels[0].Max;
            hist = new HistogramControl(Channels[channelsBox.SelectedIndex]);
            hist.GraphMax = (int)maxBox.Value;
            MouseWheel += new System.Windows.Forms.MouseEventHandler(ChannelsTool_MouseWheel);
            statsPanel.Controls.Add(hist);
        }

        private void minBox_ValueChanged(object sender, EventArgs e)
        {
            if (channelsBox.SelectedIndex == -1)
                return;
            if (hist != null)
            {
                hist.UpdateChannel(Channels[channelsBox.SelectedIndex]);
                hist.Invalidate();
                hist.Min = (int)minBox.Value;
            }
            
            App.Channels[channelsBox.SelectedIndex].Min = (int)minBox.Value;
            App.viewer.UpdateView();
        }
        private void maxBox_ValueChanged(object sender, EventArgs e)
        {
            if (channelsBox.SelectedIndex == -1)
                return;
            if (hist != null)
            {
                hist.UpdateChannel(Channels[channelsBox.SelectedIndex]);
                hist.Invalidate();
                hist.Max = (int)maxBox.Value;
            }
            
            App.Channels[channelsBox.SelectedIndex].Max = (int)maxBox.Value;
            App.viewer.UpdateView();
        }
        private void channelsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelsBox.SelectedIndex == -1)
                return;
            minBox.Value = Channels[channelsBox.SelectedIndex].Min;
            maxBox.Value = Channels[channelsBox.SelectedIndex].Max;
            if (hist != null)
            {
                //hist.Statistics = Channels[channelsBox.SelectedIndex].statistics;
                hist.UpdateChannel(SelectedChannel);
                hist.Invalidate();
            }
            App.viewer.UpdateView();
        }

        private void maxUintBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = int.Parse((string)maxUintBox.SelectedItem,System.Globalization.CultureInfo.InvariantCulture);
            if(i<=maxBox.Maximum)
            maxBox.Value = i;

        }

        private void ChannelsTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void setMaxAllBut_Click(object sender, EventArgs e)
        {
            foreach (Channel c in Channels)
            {
                c.Max = (int)maxBox.Value;
            }
            App.viewer.UpdateView();
        }

        private void setMinAllBut_Click(object sender, EventArgs e)
        {
            foreach (Channel c in Channels)
            {
                c.Min = (int)minBox.Value;
            }
            App.viewer.UpdateView();
        }

        private void ChannelsTool_Activated(object sender, EventArgs e)
        {
            minBox.Value = SelectedChannel.Min;
            maxBox.Value = SelectedChannel.Max;
            if (App.Image.bitsPerPixel == 8)
                maxGraphBox.Value = 255;
            UpdateItems();
            hist.UpdateView();
        }

        private void ChannelsTool_ResizeEnd(object sender, EventArgs e)
        {
            hist.Invalidate();
        }

        private void maxUintBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = int.Parse((string)maxUintBox2.SelectedItem, System.Globalization.CultureInfo.InvariantCulture);
            if(i <= maxGraphBox.Maximum)
            maxGraphBox.Value = i;
        }

        private void minGraphBox_ValueChanged(object sender, EventArgs e)
        {
            if (hist != null)
            {
                hist.GraphMin = (int)minGraphBox.Value;
                hist.Invalidate();
            }
        }

        private void maxGraphBox_ValueChanged(object sender, EventArgs e)
        {
            if (hist != null)
            {
                hist.GraphMax = (int)maxGraphBox.Value;
                hist.Invalidate();
            }
        }
        private void binBox_ValueChanged(object sender, EventArgs e)
        {
            if (hist != null)
            {
                hist.Bin = (int)binBox.Value;
                hist.Invalidate();
            }
        }

        private void ChannelsTool_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
        private bool pressedX1 = false;
        private bool pressedX2 = false;
        private void ChannelsTool_MouseMove(object sender, MouseEventArgs e)
        {
            minBox.Value = SelectedChannel.Min;
            maxBox.Value = SelectedChannel.Max;
            if (e.Button == MouseButtons.XButton1)
            {
                if (channelsBox.SelectedIndex < channelsBox.Items.Count-1)
                {
                    if (pressedX1 == false)
                    {
                        channelsBox.SelectedIndex++;
                        pressedX1 = true;
                        return;
                    }
                }
            }
            pressedX1 = false;
            if (e.Button == MouseButtons.XButton2)
            {
                if (channelsBox.SelectedIndex > 0)
                {
                    if (pressedX2 == false)
                    {
                        channelsBox.SelectedIndex--;
                        pressedX2 = true;
                        return;
                    }
                }
            }
            pressedX2 = false;
        }

        private void ChannelsTool_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta == 0)
                return;
            int i = 10;
            if (e.Delta > 0)
            if (maxGraphBox.Value + i < maxGraphBox.Maximum)
            {
                maxGraphBox.Value += i;
                hist.Invalidate();
                return;
            }
            if (e.Delta < 0)
            if (maxGraphBox.Value - i > maxGraphBox.Minimum)
            {
                maxGraphBox.Value -= i;
                hist.Invalidate();
                return;
            }
        }

        private void stackHistoBox_CheckedChanged(object sender, EventArgs e)
        {
            hist.StackHistogram = stackHistoBox.Checked;
            hist.Invalidate();
        }

        private void setMinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = (int)hist.MouseValX;
        }

        private void setMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maxBox.Value = (int)hist.MouseValX;
        }

        private void applyBut_Click(object sender, EventArgs e)
        {
            //App.Image.Bake(App.Image.RChannel.range, App.Image.GChannel.range, App.Image.BChannel.range);
        }

        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = SelectedChannel.statistics.Min;
        }

        private void maxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = SelectedChannel.statistics.Max;
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = (decimal)SelectedChannel.statistics.Median;
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = (decimal)SelectedChannel.statistics.Mean;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            maxBox.Value = SelectedChannel.statistics.Min;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            maxBox.Value = SelectedChannel.statistics.Max;
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            maxBox.Value = (decimal)SelectedChannel.statistics.Median;
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            maxBox.Value = (decimal)SelectedChannel.statistics.Mean;
        }

    }
}
