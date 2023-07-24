
namespace Bio
{
    partial class View3D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View3D));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dxPanel = new System.Windows.Forms.Panel();
            this.rMinBox = new System.Windows.Forms.NumericUpDown();
            this.rMaxBox = new System.Windows.Forms.NumericUpDown();
            this.gMaxBox = new System.Windows.Forms.NumericUpDown();
            this.gMinBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bMaxBox = new System.Windows.Forms.NumericUpDown();
            this.bMinBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.frameIntervalBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.transparencyBox = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rMinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rMaxBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gMaxBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gMinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bMaxBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bMinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameIntervalBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 452);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(678, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "R Min:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 407);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "R Max:";
            // 
            // dxPanel
            // 
            this.dxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dxPanel.ContextMenuStrip = this.contextMenuStrip;
            this.dxPanel.Location = new System.Drawing.Point(0, 0);
            this.dxPanel.Name = "dxPanel";
            this.dxPanel.Size = new System.Drawing.Size(678, 399);
            this.dxPanel.TabIndex = 14;
            this.dxPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dxPanel_MouseDown);
            this.dxPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dxPanel_MouseMove);
            this.dxPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dxPanel_MouseUp);
            // 
            // rMinBox
            // 
            this.rMinBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.rMinBox.ForeColor = System.Drawing.Color.White;
            this.rMinBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rMinBox.InterceptArrowKeys = false;
            this.rMinBox.Location = new System.Drawing.Point(46, 405);
            this.rMinBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.rMinBox.Name = "rMinBox";
            this.rMinBox.Size = new System.Drawing.Size(61, 20);
            this.rMinBox.TabIndex = 15;
            this.rMinBox.ValueChanged += new System.EventHandler(this.trackBarRMin_ValueChanged);
            // 
            // rMaxBox
            // 
            this.rMaxBox.AccessibleName = "rMaxBox";
            this.rMaxBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.rMaxBox.ForeColor = System.Drawing.Color.White;
            this.rMaxBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rMaxBox.InterceptArrowKeys = false;
            this.rMaxBox.Location = new System.Drawing.Point(160, 405);
            this.rMaxBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.rMaxBox.Name = "rMaxBox";
            this.rMaxBox.Size = new System.Drawing.Size(61, 20);
            this.rMaxBox.TabIndex = 16;
            this.rMaxBox.ValueChanged += new System.EventHandler(this.trackBarRMin_ValueChanged);
            // 
            // gMaxBox
            // 
            this.gMaxBox.AccessibleName = "rMaxBox";
            this.gMaxBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.gMaxBox.ForeColor = System.Drawing.Color.White;
            this.gMaxBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.gMaxBox.InterceptArrowKeys = false;
            this.gMaxBox.Location = new System.Drawing.Point(387, 405);
            this.gMaxBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.gMaxBox.Name = "gMaxBox";
            this.gMaxBox.Size = new System.Drawing.Size(61, 20);
            this.gMaxBox.TabIndex = 20;
            this.gMaxBox.ValueChanged += new System.EventHandler(this.trackBarRMin_ValueChanged);
            // 
            // gMinBox
            // 
            this.gMinBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.gMinBox.ForeColor = System.Drawing.Color.White;
            this.gMinBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.gMinBox.InterceptArrowKeys = false;
            this.gMinBox.Location = new System.Drawing.Point(273, 405);
            this.gMinBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.gMinBox.Name = "gMinBox";
            this.gMinBox.Size = new System.Drawing.Size(61, 20);
            this.gMinBox.TabIndex = 19;
            this.gMinBox.ValueChanged += new System.EventHandler(this.trackBarRMin_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 407);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "G Max:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 407);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "G Min:";
            // 
            // bMaxBox
            // 
            this.bMaxBox.AccessibleName = "rMaxBox";
            this.bMaxBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.bMaxBox.ForeColor = System.Drawing.Color.White;
            this.bMaxBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.bMaxBox.InterceptArrowKeys = false;
            this.bMaxBox.Location = new System.Drawing.Point(611, 405);
            this.bMaxBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.bMaxBox.Name = "bMaxBox";
            this.bMaxBox.Size = new System.Drawing.Size(61, 20);
            this.bMaxBox.TabIndex = 24;
            this.bMaxBox.ValueChanged += new System.EventHandler(this.trackBarRMin_ValueChanged);
            // 
            // bMinBox
            // 
            this.bMinBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.bMinBox.ForeColor = System.Drawing.Color.White;
            this.bMinBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.bMinBox.InterceptArrowKeys = false;
            this.bMinBox.Location = new System.Drawing.Point(497, 405);
            this.bMinBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.bMinBox.Name = "bMinBox";
            this.bMinBox.Size = new System.Drawing.Size(61, 20);
            this.bMinBox.TabIndex = 23;
            this.bMinBox.ValueChanged += new System.EventHandler(this.trackBarRMin_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(564, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "B Max:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(453, 407);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "B Min:";
            // 
            // frameIntervalBox
            // 
            this.frameIntervalBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.frameIntervalBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.frameIntervalBox.DecimalPlaces = 4;
            this.frameIntervalBox.ForeColor = System.Drawing.Color.White;
            this.frameIntervalBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.frameIntervalBox.InterceptArrowKeys = false;
            this.frameIntervalBox.Location = new System.Drawing.Point(82, 429);
            this.frameIntervalBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.frameIntervalBox.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.frameIntervalBox.Name = "frameIntervalBox";
            this.frameIntervalBox.Size = new System.Drawing.Size(61, 20);
            this.frameIntervalBox.TabIndex = 26;
            this.frameIntervalBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameIntervalBox.ValueChanged += new System.EventHandler(this.frameIntervalBox_ValueChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 431);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Frame Interval:";
            // 
            // transparencyBox
            // 
            this.transparencyBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.transparencyBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.transparencyBox.DecimalPlaces = 4;
            this.transparencyBox.ForeColor = System.Drawing.Color.White;
            this.transparencyBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.transparencyBox.InterceptArrowKeys = false;
            this.transparencyBox.Location = new System.Drawing.Point(237, 429);
            this.transparencyBox.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.transparencyBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.transparencyBox.Name = "transparencyBox";
            this.transparencyBox.Size = new System.Drawing.Size(61, 20);
            this.transparencyBox.TabIndex = 28;
            this.transparencyBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.transparencyBox.ValueChanged += new System.EventHandler(this.transparencyBox_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(157, 431);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Transparency";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetCameraToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(147, 26);
            // 
            // resetCameraToolStripMenuItem
            // 
            this.resetCameraToolStripMenuItem.Name = "resetCameraToolStripMenuItem";
            this.resetCameraToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.resetCameraToolStripMenuItem.Text = "Reset Camera";
            this.resetCameraToolStripMenuItem.Click += new System.EventHandler(this.resetCameraToolStripMenuItem_Click);
            // 
            // View3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(678, 474);
            this.Controls.Add(this.transparencyBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.frameIntervalBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bMaxBox);
            this.Controls.Add(this.bMinBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gMaxBox);
            this.Controls.Add(this.gMinBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rMaxBox);
            this.Controls.Add(this.rMinBox);
            this.Controls.Add(this.dxPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "View3D";
            this.Text = "3D View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.View3D_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.View3D_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View3D_KeyDown);
            this.Resize += new System.EventHandler(this.View3D_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rMinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rMaxBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gMaxBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gMinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bMaxBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bMinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameIntervalBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel dxPanel;
        private System.Windows.Forms.NumericUpDown rMinBox;
        private System.Windows.Forms.NumericUpDown rMaxBox;
        private System.Windows.Forms.NumericUpDown gMaxBox;
        private System.Windows.Forms.NumericUpDown gMinBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown bMaxBox;
        private System.Windows.Forms.NumericUpDown bMinBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown frameIntervalBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown transparencyBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem resetCameraToolStripMenuItem;
    }
}