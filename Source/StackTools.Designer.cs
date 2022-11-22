namespace Bio
{
    partial class StackTools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StackTools));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.zEndBox = new System.Windows.Forms.NumericUpDown();
            this.zContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setMaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.zStartBox = new System.Windows.Forms.NumericUpDown();
            this.cEndBox = new System.Windows.Forms.NumericUpDown();
            this.cContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setMaxCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.cStartBox = new System.Windows.Forms.NumericUpDown();
            this.tEndBox = new System.Windows.Forms.NumericUpDown();
            this.tContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setMaxTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.tStartBox = new System.Windows.Forms.NumericUpDown();
            this.substackBut = new System.Windows.Forms.Button();
            this.splitChannelsBut = new System.Windows.Forms.Button();
            this.mergeBut = new System.Windows.Forms.Button();
            this.stackABox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.stackBBox = new System.Windows.Forms.ComboBox();
            this.mergeZBut = new System.Windows.Forms.Button();
            this.mergeTBut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.zEndBox)).BeginInit();
            this.zContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zStartBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEndBox)).BeginInit();
            this.cContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cStartBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tEndBox)).BeginInit();
            this.tContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tStartBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(42, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Start";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(109, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "End";
            // 
            // zEndBox
            // 
            this.zEndBox.ContextMenuStrip = this.zContextMenuStrip;
            this.zEndBox.Location = new System.Drawing.Point(112, 54);
            this.zEndBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.zEndBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zEndBox.Name = "zEndBox";
            this.zEndBox.Size = new System.Drawing.Size(71, 20);
            this.zEndBox.TabIndex = 9;
            this.zEndBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // zContextMenuStrip
            // 
            this.zContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setMaxToolStripMenuItem});
            this.zContextMenuStrip.Name = "zContextMenuStrip";
            this.zContextMenuStrip.Size = new System.Drawing.Size(117, 26);
            // 
            // setMaxToolStripMenuItem
            // 
            this.setMaxToolStripMenuItem.Name = "setMaxToolStripMenuItem";
            this.setMaxToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.setMaxToolStripMenuItem.Text = "Set Max";
            this.setMaxToolStripMenuItem.Click += new System.EventHandler(this.setMaxToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(17, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Z";
            // 
            // zStartBox
            // 
            this.zStartBox.Location = new System.Drawing.Point(37, 54);
            this.zStartBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.zStartBox.Name = "zStartBox";
            this.zStartBox.Size = new System.Drawing.Size(69, 20);
            this.zStartBox.TabIndex = 7;
            // 
            // cEndBox
            // 
            this.cEndBox.ContextMenuStrip = this.cContextMenuStrip;
            this.cEndBox.Location = new System.Drawing.Point(112, 81);
            this.cEndBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cEndBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cEndBox.Name = "cEndBox";
            this.cEndBox.Size = new System.Drawing.Size(71, 20);
            this.cEndBox.TabIndex = 12;
            this.cEndBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cContextMenuStrip
            // 
            this.cContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setMaxCToolStripMenuItem});
            this.cContextMenuStrip.Name = "zContextMenuStrip";
            this.cContextMenuStrip.Size = new System.Drawing.Size(117, 26);
            // 
            // setMaxCToolStripMenuItem
            // 
            this.setMaxCToolStripMenuItem.Name = "setMaxCToolStripMenuItem";
            this.setMaxCToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.setMaxCToolStripMenuItem.Text = "Set Max";
            this.setMaxCToolStripMenuItem.Click += new System.EventHandler(this.setMaxCToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "C";
            // 
            // cStartBox
            // 
            this.cStartBox.Location = new System.Drawing.Point(37, 81);
            this.cStartBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cStartBox.Name = "cStartBox";
            this.cStartBox.Size = new System.Drawing.Size(69, 20);
            this.cStartBox.TabIndex = 10;
            // 
            // tEndBox
            // 
            this.tEndBox.ContextMenuStrip = this.tContextMenuStrip;
            this.tEndBox.Location = new System.Drawing.Point(112, 107);
            this.tEndBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tEndBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tEndBox.Name = "tEndBox";
            this.tEndBox.Size = new System.Drawing.Size(71, 20);
            this.tEndBox.TabIndex = 15;
            this.tEndBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tContextMenuStrip
            // 
            this.tContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setMaxTToolStripMenuItem});
            this.tContextMenuStrip.Name = "zContextMenuStrip";
            this.tContextMenuStrip.Size = new System.Drawing.Size(117, 26);
            // 
            // setMaxTToolStripMenuItem
            // 
            this.setMaxTToolStripMenuItem.Name = "setMaxTToolStripMenuItem";
            this.setMaxTToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.setMaxTToolStripMenuItem.Text = "Set Max";
            this.setMaxTToolStripMenuItem.Click += new System.EventHandler(this.setMaxTToolStripMenuItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(17, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "T";
            // 
            // tStartBox
            // 
            this.tStartBox.Location = new System.Drawing.Point(37, 107);
            this.tStartBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tStartBox.Name = "tStartBox";
            this.tStartBox.Size = new System.Drawing.Size(69, 20);
            this.tStartBox.TabIndex = 13;
            // 
            // substackBut
            // 
            this.substackBut.Location = new System.Drawing.Point(112, 133);
            this.substackBut.Name = "substackBut";
            this.substackBut.Size = new System.Drawing.Size(96, 23);
            this.substackBut.TabIndex = 16;
            this.substackBut.Text = "Create Substack";
            this.substackBut.UseVisualStyleBackColor = true;
            this.substackBut.Click += new System.EventHandler(this.substackBut_Click);
            // 
            // splitChannelsBut
            // 
            this.splitChannelsBut.Location = new System.Drawing.Point(12, 133);
            this.splitChannelsBut.Name = "splitChannelsBut";
            this.splitChannelsBut.Size = new System.Drawing.Size(96, 23);
            this.splitChannelsBut.TabIndex = 17;
            this.splitChannelsBut.Text = "Split Channels";
            this.splitChannelsBut.UseVisualStyleBackColor = true;
            this.splitChannelsBut.Click += new System.EventHandler(this.splitChannelsBut_Click);
            // 
            // mergeBut
            // 
            this.mergeBut.Location = new System.Drawing.Point(112, 191);
            this.mergeBut.Name = "mergeBut";
            this.mergeBut.Size = new System.Drawing.Size(96, 23);
            this.mergeBut.TabIndex = 18;
            this.mergeBut.Text = "Merge Channels";
            this.mergeBut.UseVisualStyleBackColor = true;
            this.mergeBut.Click += new System.EventHandler(this.mergeBut_Click);
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
            this.label2.Location = new System.Drawing.Point(17, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Stack B";
            // 
            // stackBBox
            // 
            this.stackBBox.FormattingEnabled = true;
            this.stackBBox.Location = new System.Drawing.Point(68, 164);
            this.stackBBox.Name = "stackBBox";
            this.stackBBox.Size = new System.Drawing.Size(140, 21);
            this.stackBBox.TabIndex = 21;
            this.stackBBox.SelectedIndexChanged += new System.EventHandler(this.stackBBox_SelectedIndexChanged);
            // 
            // mergeZBut
            // 
            this.mergeZBut.Location = new System.Drawing.Point(12, 191);
            this.mergeZBut.Name = "mergeZBut";
            this.mergeZBut.Size = new System.Drawing.Size(96, 23);
            this.mergeZBut.TabIndex = 23;
            this.mergeZBut.Text = "Merge Z";
            this.mergeZBut.UseVisualStyleBackColor = true;
            this.mergeZBut.Click += new System.EventHandler(this.mergeZBut_Click);
            // 
            // mergeTBut
            // 
            this.mergeTBut.Location = new System.Drawing.Point(12, 220);
            this.mergeTBut.Name = "mergeTBut";
            this.mergeTBut.Size = new System.Drawing.Size(96, 23);
            this.mergeTBut.TabIndex = 24;
            this.mergeTBut.Text = "Merge T";
            this.mergeTBut.UseVisualStyleBackColor = true;
            this.mergeTBut.Click += new System.EventHandler(this.mergeTBut_Click);
            // 
            // StackTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(224, 251);
            this.Controls.Add(this.mergeTBut);
            this.Controls.Add(this.mergeZBut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stackBBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stackABox);
            this.Controls.Add(this.mergeBut);
            this.Controls.Add(this.splitChannelsBut);
            this.Controls.Add(this.substackBut);
            this.Controls.Add(this.tEndBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tStartBox);
            this.Controls.Add(this.cEndBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cStartBox);
            this.Controls.Add(this.zEndBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.zStartBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StackTools";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Stack Tools";
            this.Activated += new System.EventHandler(this.StackTools_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StackTools_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.zEndBox)).EndInit();
            this.zContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zStartBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cEndBox)).EndInit();
            this.cContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cStartBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tEndBox)).EndInit();
            this.tContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tStartBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown zEndBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown zStartBox;
        private System.Windows.Forms.NumericUpDown cEndBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown cStartBox;
        private System.Windows.Forms.NumericUpDown tEndBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown tStartBox;
        private System.Windows.Forms.Button substackBut;
        private System.Windows.Forms.Button splitChannelsBut;
        private System.Windows.Forms.Button mergeBut;
        private System.Windows.Forms.ComboBox stackABox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox stackBBox;
        private System.Windows.Forms.ContextMenuStrip zContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setMaxToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setMaxCToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip tContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setMaxTToolStripMenuItem;
        private System.Windows.Forms.Button mergeZBut;
        private System.Windows.Forms.Button mergeTBut;
    }
}