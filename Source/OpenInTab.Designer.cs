
namespace Bio
{
    partial class OpenInTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenInTab));
            this.label1 = new System.Windows.Forms.Label();
            this.noBut = new System.Windows.Forms.Button();
            this.yesBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Open series in single tab?";
            // 
            // noBut
            // 
            this.noBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.noBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.noBut.ForeColor = System.Drawing.Color.White;
            this.noBut.Location = new System.Drawing.Point(57, 34);
            this.noBut.Name = "noBut";
            this.noBut.Size = new System.Drawing.Size(77, 23);
            this.noBut.TabIndex = 23;
            this.noBut.Text = "No";
            this.noBut.UseVisualStyleBackColor = false;
            this.noBut.Click += new System.EventHandler(this.noBut_Click);
            // 
            // yesBut
            // 
            this.yesBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yesBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.yesBut.ForeColor = System.Drawing.Color.White;
            this.yesBut.Location = new System.Drawing.Point(140, 34);
            this.yesBut.Name = "yesBut";
            this.yesBut.Size = new System.Drawing.Size(77, 23);
            this.yesBut.TabIndex = 22;
            this.yesBut.Text = "Yes";
            this.yesBut.UseVisualStyleBackColor = false;
            this.yesBut.Click += new System.EventHandler(this.yesBut_Click);
            // 
            // OpenInTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(229, 69);
            this.Controls.Add(this.noBut);
            this.Controls.Add(this.yesBut);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenInTab";
            this.Text = "Open In Tab";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button noBut;
        private System.Windows.Forms.Button yesBut;
    }
}