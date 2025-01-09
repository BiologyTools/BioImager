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
    public partial class Login : Form
    {
        public static string host, username, password;
        public static int port;
        public Login()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            BioLib.Settings.Load();
            string h = BioLib.Settings.GetSettings("host");
            string p = BioLib.Settings.GetSettings("port");
            string u = BioLib.Settings.GetSettings("username");
            if (h != "")
            {
                hostBox.Text = h;
                host = h;
            }
            if (p != "")
            {
                portBox.Text = int.Parse(p).ToString();
                port = int.Parse(p);
            }
            if(u != "")
            {
                usernameBox.Text = u;
                username = u;
            }
            BioLib.Settings.AddSettings("username", usernameBox.Text);
            BioLib.Settings.AddSettings("host", hostBox.Text);
            BioLib.Settings.AddSettings("port", ((int)portBox.Value).ToString());
            
        }

        private void loginBut_Click(object sender, EventArgs e)
        {
            host = hostBox.Text;
            username = usernameBox.Text;
            password = passBox.Text;
            port = (int)portBox.Value;
            BioLib.Settings.AddSettings("host", hostBox.Text);
            BioLib.Settings.AddSettings("port", ((int)portBox.Value).ToString());
            BioLib.Settings.AddSettings("username", usernameBox.Text);
            BioLib.Settings.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
