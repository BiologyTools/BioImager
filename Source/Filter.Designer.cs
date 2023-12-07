namespace BioImager
{
    partial class Filter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Filter));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.applyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyRGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterView = new System.Windows.Forms.TreeView();
            this.topMostBox = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyToolStripMenuItem,
            this.applyRGBToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(150, 48);
            // 
            // applyToolStripMenuItem
            // 
            this.applyToolStripMenuItem.Name = "applyToolStripMenuItem";
            this.applyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.applyToolStripMenuItem.Text = "Apply";
            this.applyToolStripMenuItem.Click += new System.EventHandler(this.applyToolStripMenuItem_Click);
            // 
            // applyRGBToolStripMenuItem
            // 
            this.applyRGBToolStripMenuItem.Name = "applyRGBToolStripMenuItem";
            this.applyRGBToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.applyRGBToolStripMenuItem.Text = "Apply In Place";
            this.applyRGBToolStripMenuItem.Click += new System.EventHandler(this.applyRGBToolStripMenuItem_Click);
            // 
            // filterView
            // 
            this.filterView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.filterView.ContextMenuStrip = this.contextMenuStrip;
            this.filterView.ForeColor = System.Drawing.Color.White;
            this.filterView.Location = new System.Drawing.Point(0, 0);
            this.filterView.Name = "filterView";
            this.filterView.Size = new System.Drawing.Size(201, 213);
            this.filterView.TabIndex = 8;
            this.filterView.DoubleClick += new System.EventHandler(this.filterView_DoubleClick);
            // 
            // topMostBox
            // 
            this.topMostBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.topMostBox.AutoSize = true;
            this.topMostBox.Location = new System.Drawing.Point(12, 219);
            this.topMostBox.Name = "topMostBox";
            this.topMostBox.Size = new System.Drawing.Size(71, 17);
            this.topMostBox.TabIndex = 9;
            this.topMostBox.Text = "Top Most";
            this.topMostBox.UseVisualStyleBackColor = true;
            this.topMostBox.CheckedChanged += new System.EventHandler(this.topMostBox_CheckedChanged);
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(201, 242);
            this.Controls.Add(this.topMostBox);
            this.Controls.Add(this.filterView);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Filter";
            this.Text = "Filters";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem applyToolStripMenuItem;
        private System.Windows.Forms.TreeView filterView;
        private System.Windows.Forms.ToolStripMenuItem applyRGBToolStripMenuItem;
        private System.Windows.Forms.CheckBox topMostBox;
    }
}