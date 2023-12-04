namespace Bio
{
    partial class CellImager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellImager));
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clearBut = new System.Windows.Forms.Button();
            this.tileYBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tileXBox = new System.Windows.Forms.NumericUpDown();
            this.tileBox = new System.Windows.Forms.CheckBox();
            this.numericRBut = new System.Windows.Forms.RadioButton();
            this.medianRBut = new System.Windows.Forms.RadioButton();
            this.maxHeightLabel = new System.Windows.Forms.Label();
            this.maxHeightBar = new System.Windows.Forms.TrackBar();
            this.maxWidthLabel = new System.Windows.Forms.Label();
            this.maxWidthBar = new System.Windows.Forms.TrackBar();
            this.detectBut = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.minHeightLabel = new System.Windows.Forms.Label();
            this.minHeightBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.imageAllBut = new System.Windows.Forms.Button();
            this.thresholdBar = new System.Windows.Forms.TrackBar();
            this.imageCurBut = new System.Windows.Forms.Button();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.minWidthLabel = new System.Windows.Forms.Label();
            this.minWidthBar = new System.Windows.Forms.TrackBar();
            this.imageSplitContainer = new System.Windows.Forms.SplitContainer();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.chanBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rgbBox = new System.Windows.Forms.NumericUpDown();
            this.objectivesBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileXBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxHeightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxWidthBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minHeightBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWidthBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSplitContainer)).BeginInit();
            this.imageSplitContainer.Panel1.SuspendLayout();
            this.imageSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chanBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbBox)).BeginInit();
            this.SuspendLayout();
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clearBut
            // 
            this.clearBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.clearBut.Location = new System.Drawing.Point(482, 443);
            this.clearBut.Name = "clearBut";
            this.clearBut.Size = new System.Drawing.Size(96, 23);
            this.clearBut.TabIndex = 24;
            this.clearBut.Text = "Clear Viewport";
            this.clearBut.UseVisualStyleBackColor = false;
            this.clearBut.Click += new System.EventHandler(this.clearBut_Click);
            // 
            // tileYBox
            // 
            this.tileYBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tileYBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tileYBox.ForeColor = System.Drawing.Color.White;
            this.tileYBox.Location = new System.Drawing.Point(428, 512);
            this.tileYBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileYBox.Name = "tileYBox";
            this.tileYBox.Size = new System.Drawing.Size(46, 23);
            this.tileYBox.TabIndex = 23;
            this.tileYBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 517);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "X";
            // 
            // tileXBox
            // 
            this.tileXBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tileXBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tileXBox.ForeColor = System.Drawing.Color.White;
            this.tileXBox.Location = new System.Drawing.Point(356, 512);
            this.tileXBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileXBox.Name = "tileXBox";
            this.tileXBox.Size = new System.Drawing.Size(46, 23);
            this.tileXBox.TabIndex = 21;
            this.tileXBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // tileBox
            // 
            this.tileBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tileBox.AutoSize = true;
            this.tileBox.Location = new System.Drawing.Point(306, 513);
            this.tileBox.Name = "tileBox";
            this.tileBox.Size = new System.Drawing.Size(44, 19);
            this.tileBox.TabIndex = 20;
            this.tileBox.Text = "Tile";
            this.tileBox.UseVisualStyleBackColor = true;
            // 
            // numericRBut
            // 
            this.numericRBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericRBut.AutoSize = true;
            this.numericRBut.Location = new System.Drawing.Point(305, 389);
            this.numericRBut.Name = "numericRBut";
            this.numericRBut.Size = new System.Drawing.Size(71, 19);
            this.numericRBut.TabIndex = 19;
            this.numericRBut.Text = "Numeric";
            this.numericRBut.UseVisualStyleBackColor = true;
            // 
            // medianRBut
            // 
            this.medianRBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.medianRBut.AutoSize = true;
            this.medianRBut.Checked = true;
            this.medianRBut.ForeColor = System.Drawing.Color.White;
            this.medianRBut.Location = new System.Drawing.Point(305, 360);
            this.medianRBut.Name = "medianRBut";
            this.medianRBut.Size = new System.Drawing.Size(114, 19);
            this.medianRBut.TabIndex = 16;
            this.medianRBut.TabStop = true;
            this.medianRBut.Text = "Statistics Median";
            this.medianRBut.UseVisualStyleBackColor = true;
            // 
            // maxHeightLabel
            // 
            this.maxHeightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxHeightLabel.AutoSize = true;
            this.maxHeightLabel.Location = new System.Drawing.Point(212, 537);
            this.maxHeightLabel.Name = "maxHeightLabel";
            this.maxHeightLabel.Size = new System.Drawing.Size(72, 15);
            this.maxHeightLabel.TabIndex = 15;
            this.maxHeightLabel.Text = "Max Height:";
            // 
            // maxHeightBar
            // 
            this.maxHeightBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxHeightBar.LargeChange = 1;
            this.maxHeightBar.Location = new System.Drawing.Point(0, 541);
            this.maxHeightBar.Maximum = 5000;
            this.maxHeightBar.Minimum = 1;
            this.maxHeightBar.Name = "maxHeightBar";
            this.maxHeightBar.Size = new System.Drawing.Size(203, 45);
            this.maxHeightBar.TabIndex = 14;
            this.maxHeightBar.Value = 500;
            this.maxHeightBar.Scroll += new System.EventHandler(this.maxHeightBar_Scroll);
            // 
            // maxWidthLabel
            // 
            this.maxWidthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxWidthLabel.AutoSize = true;
            this.maxWidthLabel.Location = new System.Drawing.Point(212, 500);
            this.maxWidthLabel.Name = "maxWidthLabel";
            this.maxWidthLabel.Size = new System.Drawing.Size(68, 15);
            this.maxWidthLabel.TabIndex = 13;
            this.maxWidthLabel.Text = "Max Width:";
            // 
            // maxWidthBar
            // 
            this.maxWidthBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxWidthBar.LargeChange = 1;
            this.maxWidthBar.Location = new System.Drawing.Point(0, 500);
            this.maxWidthBar.Maximum = 5000;
            this.maxWidthBar.Minimum = 1;
            this.maxWidthBar.Name = "maxWidthBar";
            this.maxWidthBar.Size = new System.Drawing.Size(203, 45);
            this.maxWidthBar.TabIndex = 12;
            this.maxWidthBar.Value = 500;
            this.maxWidthBar.Scroll += new System.EventHandler(this.maxWidthBar_Scroll);
            // 
            // detectBut
            // 
            this.detectBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detectBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.detectBut.Location = new System.Drawing.Point(482, 356);
            this.detectBut.Name = "detectBut";
            this.detectBut.Size = new System.Drawing.Size(96, 23);
            this.detectBut.TabIndex = 11;
            this.detectBut.Text = "Detect";
            this.detectBut.UseVisualStyleBackColor = false;
            this.detectBut.Click += new System.EventHandler(this.detectBut_Click);
            // 
            // comboBox
            // 
            this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.comboBox.ForeColor = System.Drawing.Color.White;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(305, 484);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(273, 23);
            this.comboBox.TabIndex = 10;
            // 
            // minHeightLabel
            // 
            this.minHeightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.minHeightLabel.AutoSize = true;
            this.minHeightLabel.Location = new System.Drawing.Point(212, 465);
            this.minHeightLabel.Name = "minHeightLabel";
            this.minHeightLabel.Size = new System.Drawing.Size(70, 15);
            this.minHeightLabel.TabIndex = 9;
            this.minHeightLabel.Text = "Min Height:";
            // 
            // minHeightBar
            // 
            this.minHeightBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.minHeightBar.LargeChange = 1;
            this.minHeightBar.Location = new System.Drawing.Point(0, 462);
            this.minHeightBar.Maximum = 5000;
            this.minHeightBar.Name = "minHeightBar";
            this.minHeightBar.Size = new System.Drawing.Size(203, 45);
            this.minHeightBar.TabIndex = 8;
            this.minHeightBar.Value = 2;
            this.minHeightBar.Scroll += new System.EventHandler(this.minHeightBar_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 466);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Imaging Script";
            // 
            // imageAllBut
            // 
            this.imageAllBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.imageAllBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.imageAllBut.Location = new System.Drawing.Point(482, 414);
            this.imageAllBut.Name = "imageAllBut";
            this.imageAllBut.Size = new System.Drawing.Size(96, 23);
            this.imageAllBut.TabIndex = 6;
            this.imageAllBut.Text = "Image All";
            this.imageAllBut.UseVisualStyleBackColor = false;
            this.imageAllBut.Click += new System.EventHandler(this.imageAllBut_Click);
            // 
            // thresholdBar
            // 
            this.thresholdBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.thresholdBar.Location = new System.Drawing.Point(0, 385);
            this.thresholdBar.Maximum = 65535;
            this.thresholdBar.Name = "thresholdBar";
            this.thresholdBar.Size = new System.Drawing.Size(203, 45);
            this.thresholdBar.TabIndex = 0;
            this.thresholdBar.Scroll += new System.EventHandler(this.thresholdBar_Scroll);
            // 
            // imageCurBut
            // 
            this.imageCurBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.imageCurBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.imageCurBut.Location = new System.Drawing.Point(482, 385);
            this.imageCurBut.Name = "imageCurBut";
            this.imageCurBut.Size = new System.Drawing.Size(96, 23);
            this.imageCurBut.TabIndex = 5;
            this.imageCurBut.Text = "Image Selected";
            this.imageCurBut.UseVisualStyleBackColor = false;
            this.imageCurBut.Click += new System.EventHandler(this.imageCurBut_Click);
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.thresholdLabel.AutoSize = true;
            this.thresholdLabel.Location = new System.Drawing.Point(212, 389);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(62, 15);
            this.thresholdLabel.TabIndex = 1;
            this.thresholdLabel.Text = "Threshold:";
            // 
            // minWidthLabel
            // 
            this.minWidthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.minWidthLabel.AutoSize = true;
            this.minWidthLabel.Location = new System.Drawing.Point(212, 428);
            this.minWidthLabel.Name = "minWidthLabel";
            this.minWidthLabel.Size = new System.Drawing.Size(66, 15);
            this.minWidthLabel.TabIndex = 4;
            this.minWidthLabel.Text = "Min Width:";
            // 
            // minWidthBar
            // 
            this.minWidthBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.minWidthBar.LargeChange = 1;
            this.minWidthBar.Location = new System.Drawing.Point(0, 421);
            this.minWidthBar.Maximum = 5000;
            this.minWidthBar.Name = "minWidthBar";
            this.minWidthBar.Size = new System.Drawing.Size(203, 45);
            this.minWidthBar.TabIndex = 3;
            this.minWidthBar.Value = 2;
            this.minWidthBar.Scroll += new System.EventHandler(this.sizeBar_Scroll);
            // 
            // imageSplitContainer
            // 
            this.imageSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSplitContainer.Location = new System.Drawing.Point(0, 27);
            this.imageSplitContainer.Name = "imageSplitContainer";
            // 
            // imageSplitContainer.Panel1
            // 
            this.imageSplitContainer.Panel1.Controls.Add(this.pictureBox);
            this.imageSplitContainer.Size = new System.Drawing.Size(800, 319);
            this.imageSplitContainer.SplitterDistance = 387;
            this.imageSplitContainer.TabIndex = 8;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(387, 319);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // chanBox
            // 
            this.chanBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chanBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.chanBox.ForeColor = System.Drawing.Color.White;
            this.chanBox.Location = new System.Drawing.Point(69, 355);
            this.chanBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.chanBox.Name = "chanBox";
            this.chanBox.Size = new System.Drawing.Size(46, 23);
            this.chanBox.TabIndex = 25;
            this.chanBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Channel";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 28;
            this.label4.Text = "RGB Channel";
            // 
            // rgbBox
            // 
            this.rgbBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rgbBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.rgbBox.ForeColor = System.Drawing.Color.White;
            this.rgbBox.Location = new System.Drawing.Point(203, 355);
            this.rgbBox.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.rgbBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rgbBox.Name = "rgbBox";
            this.rgbBox.Size = new System.Drawing.Size(46, 23);
            this.rgbBox.TabIndex = 27;
            this.rgbBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // objectivesBox
            // 
            this.objectivesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.objectivesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.objectivesBox.ForeColor = System.Drawing.Color.White;
            this.objectivesBox.FormattingEnabled = true;
            this.objectivesBox.Location = new System.Drawing.Point(368, 541);
            this.objectivesBox.Name = "objectivesBox";
            this.objectivesBox.Size = new System.Drawing.Size(121, 23);
            this.objectivesBox.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 544);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 30;
            this.label5.Text = "Objective";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.listView.ForeColor = System.Drawing.Color.White;
            this.listView.Location = new System.Drawing.Point(584, 355);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(204, 214);
            this.listView.TabIndex = 31;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // CellImager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(800, 581);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.objectivesBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rgbBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chanBox);
            this.Controls.Add(this.clearBut);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tileYBox);
            this.Controls.Add(this.imageSplitContainer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxWidthBar);
            this.Controls.Add(this.tileXBox);
            this.Controls.Add(this.minWidthBar);
            this.Controls.Add(this.tileBox);
            this.Controls.Add(this.minWidthLabel);
            this.Controls.Add(this.numericRBut);
            this.Controls.Add(this.thresholdLabel);
            this.Controls.Add(this.imageCurBut);
            this.Controls.Add(this.thresholdBar);
            this.Controls.Add(this.medianRBut);
            this.Controls.Add(this.imageAllBut);
            this.Controls.Add(this.maxHeightLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxHeightBar);
            this.Controls.Add(this.minHeightBar);
            this.Controls.Add(this.maxWidthLabel);
            this.Controls.Add(this.minHeightLabel);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.detectBut);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CellImager";
            this.Text = "Cell Imager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileXBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxHeightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxWidthBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minHeightBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWidthBar)).EndInit();
            this.imageSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageSplitContainer)).EndInit();
            this.imageSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chanBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripMenuItem helpToolStripMenuItem;
        private MenuStrip menuStrip1;
        private Button clearBut;
        private NumericUpDown tileYBox;
        private Label label2;
        private NumericUpDown tileXBox;
        private CheckBox tileBox;
        private RadioButton numericRBut;
        private RadioButton medianRBut;
        private Label maxHeightLabel;
        private TrackBar maxHeightBar;
        private Label maxWidthLabel;
        private TrackBar maxWidthBar;
        private Button detectBut;
        private ComboBox comboBox;
        private Label minHeightLabel;
        private TrackBar minHeightBar;
        private Label label1;
        private Button imageAllBut;
        private TrackBar thresholdBar;
        private Button imageCurBut;
        private Label thresholdLabel;
        private Label minWidthLabel;
        private TrackBar minWidthBar;
        private SplitContainer imageSplitContainer;
        private PictureBox pictureBox;
        private NumericUpDown chanBox;
        private Label label3;
        private Label label4;
        private NumericUpDown rgbBox;
        private ComboBox objectivesBox;
        private Label label5;
        private ListView listView;
    }
}