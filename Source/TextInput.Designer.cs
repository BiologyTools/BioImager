namespace BioImager
{
    partial class TextInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextInput));
            this.textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelBut = new System.Windows.Forms.Button();
            this.okBut = new System.Windows.Forms.Button();
            this.fontBut = new System.Windows.Forms.Button();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.colorBut = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(46, 6);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(135, 20);
            this.textBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // cancelBut
            // 
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.ForeColor = System.Drawing.Color.Black;
            this.cancelBut.Location = new System.Drawing.Point(94, 58);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(80, 26);
            this.cancelBut.TabIndex = 2;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // okBut
            // 
            this.okBut.ForeColor = System.Drawing.Color.Black;
            this.okBut.Location = new System.Drawing.Point(187, 59);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(80, 26);
            this.okBut.TabIndex = 3;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = true;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // fontBut
            // 
            this.fontBut.ForeColor = System.Drawing.Color.Black;
            this.fontBut.Location = new System.Drawing.Point(187, 6);
            this.fontBut.Name = "fontBut";
            this.fontBut.Size = new System.Drawing.Size(80, 20);
            this.fontBut.TabIndex = 4;
            this.fontBut.Text = "Set Font";
            this.fontBut.UseVisualStyleBackColor = true;
            this.fontBut.Click += new System.EventHandler(this.fontBut_Click);
            // 
            // colorBut
            // 
            this.colorBut.ForeColor = System.Drawing.Color.Black;
            this.colorBut.Location = new System.Drawing.Point(187, 32);
            this.colorBut.Name = "colorBut";
            this.colorBut.Size = new System.Drawing.Size(80, 20);
            this.colorBut.TabIndex = 5;
            this.colorBut.Text = "Set Color";
            this.colorBut.UseVisualStyleBackColor = true;
            this.colorBut.Click += new System.EventHandler(this.colorBut_Click);
            // 
            // TextInput
            // 
            this.AcceptButton = this.okBut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(272, 97);
            this.Controls.Add(this.colorBut);
            this.Controls.Add(this.fontBut);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextInput";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Text Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.Button fontBut;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Button colorBut;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}