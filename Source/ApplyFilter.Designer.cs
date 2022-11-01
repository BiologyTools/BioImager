namespace Bio
{
    partial class ApplyFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplyFilter));
            this.stackABox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.stackBBox = new System.Windows.Forms.ComboBox();
            this.okBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.wBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.angleBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.yBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.xBox = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.roiBox = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.setColorBut = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.hBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.wBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hBox)).BeginInit();
            this.SuspendLayout();
            // 
            // stackABox
            // 
            this.stackABox.FormattingEnabled = true;
            this.stackABox.Location = new System.Drawing.Point(68, 12);
            this.stackABox.Name = "stackABox";
            this.stackABox.Size = new System.Drawing.Size(140, 21);
            this.stackABox.TabIndex = 19;
            this.stackABox.SelectedIndexChanged += new System.EventHandler(this.stackABox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Stack A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Stack B";
            // 
            // stackBBox
            // 
            this.stackBBox.FormattingEnabled = true;
            this.stackBBox.Location = new System.Drawing.Point(68, 39);
            this.stackBBox.Name = "stackBBox";
            this.stackBBox.Size = new System.Drawing.Size(140, 21);
            this.stackBBox.TabIndex = 21;
            this.stackBBox.SelectedIndexChanged += new System.EventHandler(this.stackBBox_SelectedIndexChanged);
            // 
            // okBut
            // 
            this.okBut.Location = new System.Drawing.Point(136, 237);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(75, 23);
            this.okBut.TabIndex = 23;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = true;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // cancelBut
            // 
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.Location = new System.Drawing.Point(55, 237);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(75, 23);
            this.cancelBut.TabIndex = 24;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Resize";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(115, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "H";
            // 
            // wBox
            // 
            this.wBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.wBox.ForeColor = System.Drawing.Color.White;
            this.wBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.wBox.Location = new System.Drawing.Point(32, 89);
            this.wBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.wBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.wBox.Name = "wBox";
            this.wBox.Size = new System.Drawing.Size(77, 20);
            this.wBox.TabIndex = 27;
            this.wBox.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "W";
            // 
            // angleBox
            // 
            this.angleBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.angleBox.DecimalPlaces = 1;
            this.angleBox.ForeColor = System.Drawing.Color.White;
            this.angleBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.angleBox.Location = new System.Drawing.Point(11, 154);
            this.angleBox.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.angleBox.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.angleBox.Name = "angleBox";
            this.angleBox.Size = new System.Drawing.Size(77, 20);
            this.angleBox.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(8, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Angle";
            // 
            // yBox
            // 
            this.yBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.yBox.ForeColor = System.Drawing.Color.White;
            this.yBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.yBox.Location = new System.Drawing.Point(136, 115);
            this.yBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(77, 20);
            this.yBox.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(115, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Y";
            // 
            // xBox
            // 
            this.xBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.xBox.ForeColor = System.Drawing.Color.White;
            this.xBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.xBox.Location = new System.Drawing.Point(32, 115);
            this.xBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(77, 20);
            this.xBox.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(8, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "X";
            // 
            // roiBox
            // 
            this.roiBox.ContextMenuStrip = this.contextMenuStrip;
            this.roiBox.FormattingEnabled = true;
            this.roiBox.Location = new System.Drawing.Point(94, 154);
            this.roiBox.Name = "roiBox";
            this.roiBox.Size = new System.Drawing.Size(117, 21);
            this.roiBox.TabIndex = 37;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(102, 26);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(96, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "ROI";
            // 
            // setColorBut
            // 
            this.setColorBut.Location = new System.Drawing.Point(136, 208);
            this.setColorBut.Name = "setColorBut";
            this.setColorBut.Size = new System.Drawing.Size(75, 23);
            this.setColorBut.TabIndex = 39;
            this.setColorBut.Text = "Set Fill Color";
            this.setColorBut.UseVisualStyleBackColor = true;
            this.setColorBut.Click += new System.EventHandler(this.setColorBut_Click);
            // 
            // fillPanel
            // 
            this.fillPanel.BackColor = System.Drawing.Color.Black;
            this.fillPanel.Location = new System.Drawing.Point(55, 208);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Size = new System.Drawing.Size(75, 23);
            this.fillPanel.TabIndex = 41;
            // 
            // hBox
            // 
            this.hBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.hBox.ForeColor = System.Drawing.Color.White;
            this.hBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.hBox.Location = new System.Drawing.Point(136, 89);
            this.hBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.hBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hBox.Name = "hBox";
            this.hBox.Size = new System.Drawing.Size(77, 20);
            this.hBox.TabIndex = 29;
            this.hBox.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            // 
            // ApplyFilter
            // 
            this.AcceptButton = this.okBut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(224, 272);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.setColorBut);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.roiBox);
            this.Controls.Add(this.yBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.angleBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.hBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.wBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stackBBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stackABox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ApplyFilter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Apply Filter";
            this.Activated += new System.EventHandler(this.StackTools_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.wBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox stackABox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox stackBBox;
        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown wBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown angleBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown yBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown xBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox roiBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button setColorBut;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.NumericUpDown hBox;
    }
}