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
    public partial class TextInput : Form
    {
        public string textInput = "";
        public Font font = DefaultFont;
        public Color color = Color.Yellow;
        public TextInput(string text)
        {
            InitializeComponent();
            textBox.Text = text;
        }

        private void okBut_Click(object sender, EventArgs e)
        {
            textInput = textBox.Text;
            DialogResult = DialogResult.OK;
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void fontBut_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() != DialogResult.OK)
                return;
            font = fontDialog.Font;
        }

        private void colorBut_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;
            color = colorDialog.Color;
        }
    }
}
