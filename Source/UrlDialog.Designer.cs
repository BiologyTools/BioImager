namespace BioImager.Source
{
    partial class UrlDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UrlDialog));
            textBox1 = new TextBox();
            goBut = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(49, 91, 138);
            textBox1.ForeColor = SystemColors.Window;
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(327, 23);
            textBox1.TabIndex = 1;
            // 
            // goBut
            // 
            goBut.BackColor = Color.FromArgb(49, 91, 138);
            goBut.ForeColor = Color.White;
            goBut.Location = new Point(345, 12);
            goBut.Name = "goBut";
            goBut.Size = new Size(75, 23);
            goBut.TabIndex = 0;
            goBut.Text = "Go";
            goBut.UseVisualStyleBackColor = false;
            goBut.Click += goBut_Click;
            // 
            // UrlDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            ClientSize = new Size(432, 49);
            Controls.Add(textBox1);
            Controls.Add(goBut);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UrlDialog";
            Text = "Go To URL";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private Button goBut;
    }
}