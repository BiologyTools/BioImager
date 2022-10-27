
namespace Bio
{
    partial class ChannelsTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelsTool));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.minBox = new System.Windows.Forms.NumericUpDown();
            this.minContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxBox = new System.Windows.Forms.NumericUpDown();
            this.maxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.channelsBox = new System.Windows.Forms.ComboBox();
            this.maxUintBox = new System.Windows.Forms.ComboBox();
            this.setMaxAllBut = new System.Windows.Forms.Button();
            this.setMinAllBut = new System.Windows.Forms.Button();
            this.maxUintBox2 = new System.Windows.Forms.ComboBox();
            this.maxGraphBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.minGraphBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.binBox = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.stackHistoBox = new System.Windows.Forms.CheckBox();
            this.statsPanel = new System.Windows.Forms.Panel();
            this.applyBut = new System.Windows.Forms.Button();
            this.resetBut = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.sampleBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.minBox)).BeginInit();
            this.minContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxBox)).BeginInit();
            this.maxContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxGraphBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minGraphBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Channel Value Range";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Min";
            // 
            // minBox
            // 
            this.minBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.minBox.ContextMenuStrip = this.minContextMenuStrip;
            this.minBox.ForeColor = System.Drawing.Color.White;
            this.minBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minBox.Location = new System.Drawing.Point(42, 74);
            this.minBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.minBox.Name = "minBox";
            this.minBox.Size = new System.Drawing.Size(77, 20);
            this.minBox.TabIndex = 2;
            this.minBox.ValueChanged += new System.EventHandler(this.minBox_ValueChanged);
            // 
            // minContextMenuStrip
            // 
            this.minContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minToolStripMenuItem,
            this.maxToolStripMenuItem,
            this.medianToolStripMenuItem,
            this.meanToolStripMenuItem});
            this.minContextMenuStrip.Name = "minContextMenuStrip";
            this.minContextMenuStrip.Size = new System.Drawing.Size(115, 92);
            // 
            // minToolStripMenuItem
            // 
            this.minToolStripMenuItem.Name = "minToolStripMenuItem";
            this.minToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.minToolStripMenuItem.Text = "Min";
            this.minToolStripMenuItem.Click += new System.EventHandler(this.minToolStripMenuItem_Click);
            // 
            // maxToolStripMenuItem
            // 
            this.maxToolStripMenuItem.Name = "maxToolStripMenuItem";
            this.maxToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.maxToolStripMenuItem.Text = "Max";
            this.maxToolStripMenuItem.Click += new System.EventHandler(this.maxToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.medianToolStripMenuItem.Text = "Median";
            this.medianToolStripMenuItem.Click += new System.EventHandler(this.medianToolStripMenuItem_Click);
            // 
            // meanToolStripMenuItem
            // 
            this.meanToolStripMenuItem.Name = "meanToolStripMenuItem";
            this.meanToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.meanToolStripMenuItem.Text = "Mean";
            this.meanToolStripMenuItem.Click += new System.EventHandler(this.meanToolStripMenuItem_Click);
            // 
            // maxBox
            // 
            this.maxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.maxBox.ContextMenuStrip = this.maxContextMenuStrip;
            this.maxBox.ForeColor = System.Drawing.Color.White;
            this.maxBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxBox.Location = new System.Drawing.Point(178, 74);
            this.maxBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.maxBox.Name = "maxBox";
            this.maxBox.Size = new System.Drawing.Size(77, 20);
            this.maxBox.TabIndex = 4;
            this.maxBox.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.maxBox.ValueChanged += new System.EventHandler(this.maxBox_ValueChanged);
            // 
            // maxContextMenuStrip
            // 
            this.maxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.maxContextMenuStrip.Name = "minContextMenuStrip";
            this.maxContextMenuStrip.Size = new System.Drawing.Size(115, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem1.Text = "Min";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem2.Text = "Max";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem3.Text = "Median";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem4.Text = "Mean";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(145, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Channel";
            // 
            // channelsBox
            // 
            this.channelsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.channelsBox.ForeColor = System.Drawing.Color.White;
            this.channelsBox.FormattingEnabled = true;
            this.channelsBox.Location = new System.Drawing.Point(60, 4);
            this.channelsBox.Name = "channelsBox";
            this.channelsBox.Size = new System.Drawing.Size(195, 21);
            this.channelsBox.TabIndex = 6;
            this.channelsBox.SelectedIndexChanged += new System.EventHandler(this.channelsBox_SelectedIndexChanged);
            // 
            // maxUintBox
            // 
            this.maxUintBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.maxUintBox.ForeColor = System.Drawing.Color.White;
            this.maxUintBox.FormattingEnabled = true;
            this.maxUintBox.Items.AddRange(new object[] {
            "255",
            "1023",
            "4096",
            "16383",
            "65535"});
            this.maxUintBox.Location = new System.Drawing.Point(178, 51);
            this.maxUintBox.Name = "maxUintBox";
            this.maxUintBox.Size = new System.Drawing.Size(77, 21);
            this.maxUintBox.TabIndex = 7;
            this.maxUintBox.Text = "65535";
            this.maxUintBox.SelectedIndexChanged += new System.EventHandler(this.maxUintBox_SelectedIndexChanged);
            // 
            // setMaxAllBut
            // 
            this.setMaxAllBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setMaxAllBut.ForeColor = System.Drawing.Color.White;
            this.setMaxAllBut.Location = new System.Drawing.Point(178, 100);
            this.setMaxAllBut.Name = "setMaxAllBut";
            this.setMaxAllBut.Size = new System.Drawing.Size(77, 23);
            this.setMaxAllBut.TabIndex = 8;
            this.setMaxAllBut.Text = "Set Max All";
            this.setMaxAllBut.UseVisualStyleBackColor = false;
            this.setMaxAllBut.Click += new System.EventHandler(this.setMaxAllBut_Click);
            // 
            // setMinAllBut
            // 
            this.setMinAllBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setMinAllBut.ForeColor = System.Drawing.Color.White;
            this.setMinAllBut.Location = new System.Drawing.Point(42, 100);
            this.setMinAllBut.Name = "setMinAllBut";
            this.setMinAllBut.Size = new System.Drawing.Size(77, 23);
            this.setMinAllBut.TabIndex = 9;
            this.setMinAllBut.Text = "Set Min All";
            this.setMinAllBut.UseVisualStyleBackColor = false;
            this.setMinAllBut.Click += new System.EventHandler(this.setMinAllBut_Click);
            // 
            // maxUintBox2
            // 
            this.maxUintBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxUintBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.maxUintBox2.ForeColor = System.Drawing.Color.White;
            this.maxUintBox2.FormattingEnabled = true;
            this.maxUintBox2.Items.AddRange(new object[] {
            "255",
            "1023",
            "4096",
            "16383",
            "65535"});
            this.maxUintBox2.Location = new System.Drawing.Point(175, 313);
            this.maxUintBox2.Name = "maxUintBox2";
            this.maxUintBox2.Size = new System.Drawing.Size(77, 21);
            this.maxUintBox2.TabIndex = 16;
            this.maxUintBox2.Text = "65535";
            this.maxUintBox2.SelectedIndexChanged += new System.EventHandler(this.maxUintBox2_SelectedIndexChanged);
            // 
            // maxGraphBox
            // 
            this.maxGraphBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxGraphBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.maxGraphBox.ForeColor = System.Drawing.Color.White;
            this.maxGraphBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxGraphBox.Location = new System.Drawing.Point(175, 338);
            this.maxGraphBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.maxGraphBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxGraphBox.Name = "maxGraphBox";
            this.maxGraphBox.Size = new System.Drawing.Size(77, 20);
            this.maxGraphBox.TabIndex = 15;
            this.maxGraphBox.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.maxGraphBox.ValueChanged += new System.EventHandler(this.maxGraphBox_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(142, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Max";
            // 
            // minGraphBox
            // 
            this.minGraphBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.minGraphBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.minGraphBox.ForeColor = System.Drawing.Color.White;
            this.minGraphBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minGraphBox.Location = new System.Drawing.Point(39, 338);
            this.minGraphBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.minGraphBox.Name = "minGraphBox";
            this.minGraphBox.Size = new System.Drawing.Size(77, 20);
            this.minGraphBox.TabIndex = 13;
            this.minGraphBox.ValueChanged += new System.EventHandler(this.minGraphBox_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(10, 340);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Min";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(8, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Graph Value Range";
            // 
            // binBox
            // 
            this.binBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.binBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.binBox.ForeColor = System.Drawing.Color.White;
            this.binBox.Location = new System.Drawing.Point(39, 314);
            this.binBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.binBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.binBox.Name = "binBox";
            this.binBox.Size = new System.Drawing.Size(77, 20);
            this.binBox.TabIndex = 18;
            this.binBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.binBox.ValueChanged += new System.EventHandler(this.binBox_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(10, 316);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Bin";
            // 
            // stackHistoBox
            // 
            this.stackHistoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stackHistoBox.AutoSize = true;
            this.stackHistoBox.Checked = true;
            this.stackHistoBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stackHistoBox.Location = new System.Drawing.Point(132, 291);
            this.stackHistoBox.Name = "stackHistoBox";
            this.stackHistoBox.Size = new System.Drawing.Size(134, 17);
            this.stackHistoBox.TabIndex = 19;
            this.stackHistoBox.Text = "Mean Stack Histogram";
            this.stackHistoBox.UseVisualStyleBackColor = true;
            this.stackHistoBox.CheckedChanged += new System.EventHandler(this.stackHistoBox_CheckedChanged);
            // 
            // statsPanel
            // 
            this.statsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statsPanel.Location = new System.Drawing.Point(12, 132);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Size = new System.Drawing.Size(243, 152);
            this.statsPanel.TabIndex = 10;
            // 
            // applyBut
            // 
            this.applyBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.applyBut.ForeColor = System.Drawing.Color.White;
            this.applyBut.Location = new System.Drawing.Point(92, 364);
            this.applyBut.Name = "applyBut";
            this.applyBut.Size = new System.Drawing.Size(77, 23);
            this.applyBut.TabIndex = 20;
            this.applyBut.Text = "Apply";
            this.applyBut.UseVisualStyleBackColor = false;
            this.applyBut.Click += new System.EventHandler(this.applyBut_Click);
            // 
            // resetBut
            // 
            this.resetBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.resetBut.ForeColor = System.Drawing.Color.White;
            this.resetBut.Location = new System.Drawing.Point(175, 364);
            this.resetBut.Name = "resetBut";
            this.resetBut.Size = new System.Drawing.Size(77, 23);
            this.resetBut.TabIndex = 21;
            this.resetBut.Text = "Reset";
            this.resetBut.UseVisualStyleBackColor = false;
            this.resetBut.Click += new System.EventHandler(this.updateBut_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(8, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Sample";
            // 
            // sampleBox
            // 
            this.sampleBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.sampleBox.ContextMenuStrip = this.maxContextMenuStrip;
            this.sampleBox.ForeColor = System.Drawing.Color.White;
            this.sampleBox.Location = new System.Drawing.Point(60, 28);
            this.sampleBox.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.sampleBox.Name = "sampleBox";
            this.sampleBox.Size = new System.Drawing.Size(195, 20);
            this.sampleBox.TabIndex = 23;
            this.sampleBox.ValueChanged += new System.EventHandler(this.sampleBox_ValueChanged);
            // 
            // ChannelsTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(263, 396);
            this.Controls.Add(this.sampleBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.resetBut);
            this.Controls.Add(this.applyBut);
            this.Controls.Add(this.stackHistoBox);
            this.Controls.Add(this.binBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.maxUintBox2);
            this.Controls.Add(this.maxGraphBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.minGraphBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.statsPanel);
            this.Controls.Add(this.setMinAllBut);
            this.Controls.Add(this.setMaxAllBut);
            this.Controls.Add(this.maxUintBox);
            this.Controls.Add(this.channelsBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maxBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.minBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(279, 408);
            this.Name = "ChannelsTool";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Channels Tool";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.ChannelsTool_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChannelsTool_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.ChannelsTool_ResizeEnd);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChannelsTool_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChannelsTool_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.minBox)).EndInit();
            this.minContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxBox)).EndInit();
            this.maxContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxGraphBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minGraphBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minBox;
        private System.Windows.Forms.NumericUpDown maxBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox channelsBox;
        private System.Windows.Forms.ComboBox maxUintBox;
        private System.Windows.Forms.Button setMaxAllBut;
        private System.Windows.Forms.Button setMinAllBut;
        private System.Windows.Forms.ComboBox maxUintBox2;
        private System.Windows.Forms.NumericUpDown maxGraphBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown minGraphBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown binBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox stackHistoBox;
        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.Button applyBut;
        private System.Windows.Forms.ContextMenuStrip minContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem minToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meanToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip maxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.Button resetBut;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown sampleBox;
    }
}