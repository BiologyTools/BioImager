namespace BioImager
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            passBox = new MaskedTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            portBox = new NumericUpDown();
            usernameBox = new TextBox();
            hostBox = new TextBox();
            label4 = new Label();
            loginBut = new Button();
            cancelBut = new Button();
            ((System.ComponentModel.ISupportInitialize)portBox).BeginInit();
            SuspendLayout();
            // 
            // passBox
            // 
            passBox.BackColor = Color.FromArgb(49, 91, 138);
            passBox.ForeColor = Color.White;
            passBox.Location = new Point(72, 99);
            passBox.Name = "passBox";
            passBox.Size = new Size(199, 23);
            passBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(6, 102);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 1;
            label1.Text = "Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 73);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 2;
            label2.Text = "Username:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(6, 45);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 3;
            label3.Text = "Port:";
            // 
            // portBox
            // 
            portBox.BackColor = Color.FromArgb(49, 91, 138);
            portBox.ForeColor = Color.White;
            portBox.Location = new Point(72, 43);
            portBox.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            portBox.Name = "portBox";
            portBox.Size = new Size(199, 23);
            portBox.TabIndex = 4;
            // 
            // usernameBox
            // 
            usernameBox.BackColor = Color.FromArgb(49, 91, 138);
            usernameBox.ForeColor = Color.White;
            usernameBox.Location = new Point(72, 70);
            usernameBox.Name = "usernameBox";
            usernameBox.Size = new Size(199, 23);
            usernameBox.TabIndex = 5;
            // 
            // hostBox
            // 
            hostBox.BackColor = Color.FromArgb(49, 91, 138);
            hostBox.ForeColor = Color.White;
            hostBox.Location = new Point(72, 12);
            hostBox.Name = "hostBox";
            hostBox.Size = new Size(199, 23);
            hostBox.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(3, 15);
            label4.Name = "label4";
            label4.Size = new Size(35, 15);
            label4.TabIndex = 6;
            label4.Text = "Host:";
            // 
            // loginBut
            // 
            loginBut.BackColor = Color.FromArgb(49, 91, 138);
            loginBut.ForeColor = Color.White;
            loginBut.Location = new Point(196, 133);
            loginBut.Name = "loginBut";
            loginBut.Size = new Size(75, 23);
            loginBut.TabIndex = 8;
            loginBut.Text = "Login";
            loginBut.UseVisualStyleBackColor = false;
            loginBut.Click += loginBut_Click;
            // 
            // cancelBut
            // 
            cancelBut.BackColor = Color.FromArgb(49, 91, 138);
            cancelBut.ForeColor = Color.White;
            cancelBut.Location = new Point(115, 133);
            cancelBut.Name = "cancelBut";
            cancelBut.Size = new Size(75, 23);
            cancelBut.TabIndex = 9;
            cancelBut.Text = "Cancel";
            cancelBut.UseVisualStyleBackColor = false;
            // 
            // Login
            // 
            AcceptButton = loginBut;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            CancelButton = cancelBut;
            ClientSize = new Size(283, 168);
            Controls.Add(cancelBut);
            Controls.Add(loginBut);
            Controls.Add(hostBox);
            Controls.Add(label4);
            Controls.Add(usernameBox);
            Controls.Add(portBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Login";
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)portBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox passBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown portBox;
        private TextBox usernameBox;
        private TextBox hostBox;
        private Label label4;
        private Button loginBut;
        private Button cancelBut;
    }
}