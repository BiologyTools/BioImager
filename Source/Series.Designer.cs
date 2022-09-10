namespace Bio
{
    partial class Series
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Series));
            this.imagesBox = new System.Windows.Forms.ListBox();
            this.seriesBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addBut = new System.Windows.Forms.Button();
            this.removeBut = new System.Windows.Forms.Button();
            this.saveTiffFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOMEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllBut = new System.Windows.Forms.Button();
            this.removeAllBut = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imagesBox
            // 
            this.imagesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.imagesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.imagesBox.ForeColor = System.Drawing.Color.White;
            this.imagesBox.FormattingEnabled = true;
            this.imagesBox.Location = new System.Drawing.Point(9, 48);
            this.imagesBox.Name = "imagesBox";
            this.imagesBox.Size = new System.Drawing.Size(162, 173);
            this.imagesBox.TabIndex = 0;
            // 
            // seriesBox
            // 
            this.seriesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.seriesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.seriesBox.ForeColor = System.Drawing.Color.White;
            this.seriesBox.FormattingEnabled = true;
            this.seriesBox.Location = new System.Drawing.Point(219, 48);
            this.seriesBox.Name = "seriesBox";
            this.seriesBox.Size = new System.Drawing.Size(162, 173);
            this.seriesBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Images";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(219, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Series";
            // 
            // addBut
            // 
            this.addBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.addBut.ForeColor = System.Drawing.Color.White;
            this.addBut.Location = new System.Drawing.Point(177, 48);
            this.addBut.Name = "addBut";
            this.addBut.Size = new System.Drawing.Size(38, 23);
            this.addBut.TabIndex = 23;
            this.addBut.Text = ">";
            this.addBut.UseVisualStyleBackColor = false;
            this.addBut.Click += new System.EventHandler(this.addBut_Click);
            // 
            // removeBut
            // 
            this.removeBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.removeBut.ForeColor = System.Drawing.Color.White;
            this.removeBut.Location = new System.Drawing.Point(177, 77);
            this.removeBut.Name = "removeBut";
            this.removeBut.Size = new System.Drawing.Size(38, 23);
            this.removeBut.TabIndex = 24;
            this.removeBut.Text = "<";
            this.removeBut.UseVisualStyleBackColor = false;
            this.removeBut.Click += new System.EventHandler(this.removeBut_Click);
            // 
            // saveTiffFileDialog
            // 
            this.saveTiffFileDialog.DefaultExt = "tif";
            this.saveTiffFileDialog.Filter = "TIFF Files (*.tif)|*.tif";
            this.saveTiffFileDialog.SupportMultiDottedExtensions = true;
            this.saveTiffFileDialog.Title = "Save Image";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(387, 24);
            this.menuStrip.TabIndex = 25;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveOMEToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveOMEToolStripMenuItem
            // 
            this.saveOMEToolStripMenuItem.Name = "saveOMEToolStripMenuItem";
            this.saveOMEToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.saveOMEToolStripMenuItem.Text = "Save OME";
            this.saveOMEToolStripMenuItem.Click += new System.EventHandler(this.saveOMEToolStripMenuItem_Click);
            // 
            // addAllBut
            // 
            this.addAllBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.addAllBut.ForeColor = System.Drawing.Color.White;
            this.addAllBut.Location = new System.Drawing.Point(177, 106);
            this.addAllBut.Name = "addAllBut";
            this.addAllBut.Size = new System.Drawing.Size(38, 23);
            this.addAllBut.TabIndex = 26;
            this.addAllBut.Text = ">>";
            this.addAllBut.UseVisualStyleBackColor = false;
            this.addAllBut.Click += new System.EventHandler(this.addAllBut_Click);
            // 
            // removeAllBut
            // 
            this.removeAllBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.removeAllBut.ForeColor = System.Drawing.Color.White;
            this.removeAllBut.Location = new System.Drawing.Point(177, 135);
            this.removeAllBut.Name = "removeAllBut";
            this.removeAllBut.Size = new System.Drawing.Size(38, 23);
            this.removeAllBut.TabIndex = 27;
            this.removeAllBut.Text = "<<";
            this.removeAllBut.UseVisualStyleBackColor = false;
            this.removeAllBut.Click += new System.EventHandler(this.removeAllBut_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "tif";
            this.saveFileDialog.Filter = "OME TIFF Files (*.ome.tif)|*.ome.tif";
            this.saveFileDialog.SupportMultiDottedExtensions = true;
            this.saveFileDialog.Title = "Save Image";
            // 
            // Series
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(387, 235);
            this.Controls.Add(this.removeAllBut);
            this.Controls.Add(this.addAllBut);
            this.Controls.Add(this.removeBut);
            this.Controls.Add(this.addBut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.seriesBox);
            this.Controls.Add(this.imagesBox);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Series";
            this.Text = "Save Series";
            this.Activated += new System.EventHandler(this.Series_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Series_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox imagesBox;
        private System.Windows.Forms.ListBox seriesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addBut;
        private System.Windows.Forms.Button removeBut;
        private System.Windows.Forms.SaveFileDialog saveTiffFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveOMEToolStripMenuItem;
        private System.Windows.Forms.Button addAllBut;
        private System.Windows.Forms.Button removeAllBut;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}