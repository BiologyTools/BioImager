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
        public int SelectedSample
        {
            get
            {
                return (int)sampleBox.Value;
            }
            set
            {
                sampleBox.Value = (decimal)value;
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
            minBox.Value = (int)Channels[0].stats[0].StackMin;
            maxBox.Value = (int)Channels[0].stats[0].StackMax;
            hist = new HistogramControl(Channels[channelsBox.SelectedIndex]);
            hist.GraphMax = (int)maxBox.Value;
            maxGraphBox.Value = (int)maxBox.Value;
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
            App.Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Min = (int)minBox.Value;
            App.viewer.UpdateImage();
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
            App.Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Min = (int)minBox.Value;
            App.viewer.UpdateImage();
            App.viewer.UpdateView();
        }
        private void channelsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelsBox.SelectedIndex == -1)
                return;
            sampleBox.Value = 0;
            sampleBox.Maximum = ((Channel)channelsBox.SelectedItem).range.Length - 1;
            if (minBox.Maximum < Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Min || maxBox.Maximum < Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Max)
            {
                minBox.Value = 0;
                maxBox.Value = ushort.MaxValue;
            }
            else
            {
                minBox.Value = Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Min;
                maxBox.Value = Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Max;
            }
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
                for (int i = 0; i < c.range.Length; i++)
                {
                    c.range[i].Max = (int)maxBox.Value;
                }
            }
            App.viewer.UpdateView();
        }

        private void setMinAllBut_Click(object sender, EventArgs e)
        {
            foreach (Channel c in Channels)
            {
                for (int i = 0; i < c.range.Length; i++)
                {
                    c.range[i].Min = (int)minBox.Value;
                }
            }
            App.viewer.UpdateView();
        }

        private void ChannelsTool_Activated(object sender, EventArgs e)
        {
            sampleBox.Maximum = SelectedChannel.SamplesPerPixel - 1;
            for (int i = 0; i < SelectedChannel.range.Length; i++)
            {
                SelectedChannel.range[i].Min = (int)minBox.Value;
            }
            minBox.Value = SelectedChannel.range[(int)sampleBox.Value].Min;
            maxBox.Value = SelectedChannel.range[(int)sampleBox.Value].Max;
            UpdateItems();
            hist.UpdateView();

        }

        private void ChannelsTool_ResizeEnd(object sender, EventArgs e)
        {
            hist.UpdateView();
        }

        private void maxUintBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = int.Parse((string)maxUintBox2.SelectedItem, System.Globalization.CultureInfo.InvariantCulture);
            if(i <= maxGraphBox.Maximum)
            maxGraphBox.Value = i;
            if (i == 255)
            {
                maxGraphBox.Value = 255;
                binBox.Value = 1;
            }
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
            int i = 100;
            if (maxGraphBox.Value < 255)
                i = 10;
            if (e.Delta > 0)
            {
                if (maxGraphBox.Value + i < maxGraphBox.Maximum)
                {
                    maxGraphBox.Value += i;
                    hist.Invalidate();
                    return;
                }
            }
            else
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
            ImageView.SelectedImage.Bake(ImageView.SelectedImage.RChannel.RangeR, ImageView.SelectedImage.GChannel.RangeG, ImageView.SelectedImage.BChannel.RangeB);
        }

        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMin;
        }

        private void maxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMax;
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMedian;
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMean;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            maxBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMin;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            maxBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMax;
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            maxBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMedian;
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            maxBox.Value = (decimal)SelectedChannel.stats[channelsBox.SelectedIndex].StackMean;
        }

        private void updateBut_Click(object sender, EventArgs e)
        {
            if (ImageView.SelectedImage.bitsPerPixel > 8)
            {
                ImageView.SelectedImage.StackThreshold(true);
                if (ImageView.SelectedImage.RGBChannelCount == 1)
                {
                    maxBox.Value = (decimal)ImageView.SelectedImage.Channels[channelsBox.SelectedIndex].stats[0].StackMax;
                    minBox.Value = (decimal)ImageView.SelectedImage.Channels[channelsBox.SelectedIndex].stats[0].StackMin;
                }
                else
                {
                    maxBox.Value = (decimal)ImageView.SelectedImage.Channels[channelsBox.SelectedIndex].stats[channelsBox.SelectedIndex].StackMax;
                    minBox.Value = (decimal)ImageView.SelectedImage.Channels[channelsBox.SelectedIndex].stats[channelsBox.SelectedIndex].StackMin;
                }
            }
            else
            {
                ImageView.SelectedImage.StackThreshold(false);
            }
            App.viewer.UpdateImage();
        }

        private void sampleBox_ValueChanged(object sender, EventArgs e)
        {
            if (channelsBox.SelectedIndex == -1)
                channelsBox.SelectedIndex = 0;
            minBox.Value = Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Min;
            maxBox.Value = Channels[channelsBox.SelectedIndex].range[(int)sampleBox.Value].Max;
            if (hist != null)
            {
                //hist.Statistics = Channels[channelsBox.SelectedIndex].statistics;
                hist.UpdateChannel(SelectedChannel);
                hist.Invalidate();
            }
            App.viewer.UpdateView();
        }
    }
}
