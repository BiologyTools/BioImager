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
    public partial class SetTool : Form
    {

        public SetTool()
        {
            InitializeComponent();
            timer.Start();
            foreach (Scripting.Script tool in Scripting.Scripts.Values)
            {
                if (tool.type == Scripting.ScriptType.tool)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = tool.name;
                    item.Tag = tool;
                    toolView.Items.Add(item);
                }
            }
        }
        public Scripting.Script Script
        {
            get { return (Scripting.Script)toolView.SelectedItems[0].Tag;}
        }
        private void setToolBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

        }
        private void toolView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Script.thread != null)
                Script.Stop();
            Script.Run();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in toolView.Items)
            {
                item.Text = ((Scripting.Script)item.Tag).ToString();
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Script.thread != null)
                Script.Stop();
        }
    }
}
