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
        string textValue = "";
        public Font font = DefaultFont;
        public Color color = Color.Yellow;
        public string TextValue
        {
            get
            {
                return textValue;
            }
        }
        public TextInput(string text)
        {
            InitializeComponent();
            textBox.Text = text;
        }

        /// The function is called when the user clicks the OK button. It sets the textValue variable to
        /// the text in the text box and then sets the DialogResult to OK
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void okBut_Click(object sender, EventArgs e)
        {
            textValue = textBox.Text;
            DialogResult = DialogResult.OK;
        }

        /// The cancel button is clicked, the dialog result is set to cancel, and the form is closed
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        /// If the user clicks the font button, then the font dialog box will appear and the user can
        /// change the font
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The font that was selected in the font dialog.
        private void fontBut_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() != DialogResult.OK)
                return;
            font = fontDialog.Font;
        }

        /// If the user clicks the color button, open the color dialog and if the user clicks OK, set
        /// the color to the color the user selected
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The color that the user selected.
        private void colorBut_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;
            color = colorDialog.Color;
        }
    }
}
