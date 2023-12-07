
namespace BioImager
{
    partial class Resolutions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resolutions));
            this.resBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.okBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resBox
            // 
            this.resBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.resBox.ForeColor = System.Drawing.Color.White;
            this.resBox.FormattingEnabled = true;
            this.resBox.Location = new System.Drawing.Point(75, 12);
            this.resBox.Name = "resBox";
            this.resBox.Size = new System.Drawing.Size(229, 21);
            this.resBox.TabIndex = 7;
            this.resBox.SelectedIndexChanged += new System.EventHandler(this.resBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Resolution:";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(46, 42);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(0, 13);
            this.infoLabel.TabIndex = 10;
            // 
            // okBut
            // 
            this.okBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.okBut.ForeColor = System.Drawing.Color.White;
            this.okBut.Location = new System.Drawing.Point(227, 42);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(77, 23);
            this.okBut.TabIndex = 21;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = false;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // Resolutions
            // 
            this.AcceptButton = this.okBut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(316, 76);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.resBox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Resolutions";
            this.Text = "Pick Resolution";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox resBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button okBut;
    }
}