namespace BioImager
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
            helpToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            clearBut = new Button();
            tileYBox = new NumericUpDown();
            label2 = new Label();
            tileXBox = new NumericUpDown();
            tileBox = new CheckBox();
            numericRBut = new RadioButton();
            medianRBut = new RadioButton();
            maxHeightLabel = new Label();
            maxHeightBar = new TrackBar();
            maxWidthLabel = new Label();
            maxWidthBar = new TrackBar();
            detectBut = new Button();
            comboBox = new ComboBox();
            minHeightLabel = new Label();
            minHeightBar = new TrackBar();
            label1 = new Label();
            imageAllBut = new Button();
            thresholdBar = new TrackBar();
            imageCurBut = new Button();
            thresholdLabel = new Label();
            minWidthLabel = new Label();
            minWidthBar = new TrackBar();
            imageSplitContainer = new SplitContainer();
            pictureBox = new PictureBox();
            chanBox = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            rgbBox = new NumericUpDown();
            objectivesBox = new ComboBox();
            label5 = new Label();
            listView = new ListView();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tileYBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tileXBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxHeightBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxWidthBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minHeightBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thresholdBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minWidthBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageSplitContainer).BeginInit();
            imageSplitContainer.Panel1.SuspendLayout();
            imageSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chanBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rgbBox).BeginInit();
            SuspendLayout();
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // clearBut
            // 
            clearBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            clearBut.BackColor = Color.FromArgb(49, 91, 138);
            clearBut.Location = new Point(482, 443);
            clearBut.Name = "clearBut";
            clearBut.Size = new Size(96, 23);
            clearBut.TabIndex = 24;
            clearBut.Text = "Clear Viewport";
            clearBut.UseVisualStyleBackColor = false;
            clearBut.Click += clearBut_Click;
            // 
            // tileYBox
            // 
            tileYBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tileYBox.BackColor = Color.FromArgb(49, 91, 138);
            tileYBox.ForeColor = Color.White;
            tileYBox.Location = new Point(428, 512);
            tileYBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            tileYBox.Name = "tileYBox";
            tileYBox.Size = new Size(46, 23);
            tileYBox.TabIndex = 23;
            tileYBox.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(408, 517);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 22;
            label2.Text = "X";
            // 
            // tileXBox
            // 
            tileXBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tileXBox.BackColor = Color.FromArgb(49, 91, 138);
            tileXBox.ForeColor = Color.White;
            tileXBox.Location = new Point(356, 512);
            tileXBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            tileXBox.Name = "tileXBox";
            tileXBox.Size = new Size(46, 23);
            tileXBox.TabIndex = 21;
            tileXBox.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // tileBox
            // 
            tileBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tileBox.AutoSize = true;
            tileBox.Location = new Point(306, 513);
            tileBox.Name = "tileBox";
            tileBox.Size = new Size(44, 19);
            tileBox.TabIndex = 20;
            tileBox.Text = "Tile";
            tileBox.UseVisualStyleBackColor = true;
            // 
            // numericRBut
            // 
            numericRBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            numericRBut.AutoSize = true;
            numericRBut.Location = new Point(305, 389);
            numericRBut.Name = "numericRBut";
            numericRBut.Size = new Size(71, 19);
            numericRBut.TabIndex = 19;
            numericRBut.Text = "Numeric";
            numericRBut.UseVisualStyleBackColor = true;
            // 
            // medianRBut
            // 
            medianRBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            medianRBut.AutoSize = true;
            medianRBut.Checked = true;
            medianRBut.ForeColor = Color.White;
            medianRBut.Location = new Point(305, 360);
            medianRBut.Name = "medianRBut";
            medianRBut.Size = new Size(114, 19);
            medianRBut.TabIndex = 16;
            medianRBut.TabStop = true;
            medianRBut.Text = "Statistics Median";
            medianRBut.UseVisualStyleBackColor = true;
            // 
            // maxHeightLabel
            // 
            maxHeightLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            maxHeightLabel.AutoSize = true;
            maxHeightLabel.Location = new Point(212, 537);
            maxHeightLabel.Name = "maxHeightLabel";
            maxHeightLabel.Size = new Size(72, 15);
            maxHeightLabel.TabIndex = 15;
            maxHeightLabel.Text = "Max Height:";
            // 
            // maxHeightBar
            // 
            maxHeightBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            maxHeightBar.LargeChange = 1;
            maxHeightBar.Location = new Point(0, 541);
            maxHeightBar.Maximum = 5000;
            maxHeightBar.Minimum = 1;
            maxHeightBar.Name = "maxHeightBar";
            maxHeightBar.Size = new Size(203, 45);
            maxHeightBar.TabIndex = 14;
            maxHeightBar.Value = 500;
            maxHeightBar.Scroll += maxHeightBar_Scroll;
            // 
            // maxWidthLabel
            // 
            maxWidthLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            maxWidthLabel.AutoSize = true;
            maxWidthLabel.Location = new Point(212, 500);
            maxWidthLabel.Name = "maxWidthLabel";
            maxWidthLabel.Size = new Size(68, 15);
            maxWidthLabel.TabIndex = 13;
            maxWidthLabel.Text = "Max Width:";
            // 
            // maxWidthBar
            // 
            maxWidthBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            maxWidthBar.LargeChange = 1;
            maxWidthBar.Location = new Point(0, 500);
            maxWidthBar.Maximum = 5000;
            maxWidthBar.Minimum = 1;
            maxWidthBar.Name = "maxWidthBar";
            maxWidthBar.Size = new Size(203, 45);
            maxWidthBar.TabIndex = 12;
            maxWidthBar.Value = 500;
            maxWidthBar.Scroll += maxWidthBar_Scroll;
            // 
            // detectBut
            // 
            detectBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            detectBut.BackColor = Color.FromArgb(49, 91, 138);
            detectBut.Location = new Point(482, 356);
            detectBut.Name = "detectBut";
            detectBut.Size = new Size(96, 23);
            detectBut.TabIndex = 11;
            detectBut.Text = "Detect";
            detectBut.UseVisualStyleBackColor = false;
            detectBut.Click += detectBut_Click;
            // 
            // comboBox
            // 
            comboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            comboBox.BackColor = Color.FromArgb(49, 91, 138);
            comboBox.ForeColor = Color.White;
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(305, 484);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(273, 23);
            comboBox.TabIndex = 10;
            // 
            // minHeightLabel
            // 
            minHeightLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            minHeightLabel.AutoSize = true;
            minHeightLabel.Location = new Point(212, 465);
            minHeightLabel.Name = "minHeightLabel";
            minHeightLabel.Size = new Size(70, 15);
            minHeightLabel.TabIndex = 9;
            minHeightLabel.Text = "Min Height:";
            // 
            // minHeightBar
            // 
            minHeightBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            minHeightBar.LargeChange = 1;
            minHeightBar.Location = new Point(0, 462);
            minHeightBar.Maximum = 5000;
            minHeightBar.Name = "minHeightBar";
            minHeightBar.Size = new Size(203, 45);
            minHeightBar.TabIndex = 8;
            minHeightBar.Value = 2;
            minHeightBar.Scroll += minHeightBar_Scroll;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(305, 466);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 7;
            label1.Text = "Imaging Script";
            // 
            // imageAllBut
            // 
            imageAllBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            imageAllBut.BackColor = Color.FromArgb(49, 91, 138);
            imageAllBut.Location = new Point(482, 414);
            imageAllBut.Name = "imageAllBut";
            imageAllBut.Size = new Size(96, 23);
            imageAllBut.TabIndex = 6;
            imageAllBut.Text = "Image All";
            imageAllBut.UseVisualStyleBackColor = false;
            imageAllBut.Click += imageAllBut_Click;
            // 
            // thresholdBar
            // 
            thresholdBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            thresholdBar.Location = new Point(0, 385);
            thresholdBar.Maximum = 65535;
            thresholdBar.Name = "thresholdBar";
            thresholdBar.Size = new Size(203, 45);
            thresholdBar.TabIndex = 0;
            thresholdBar.Scroll += thresholdBar_Scroll;
            // 
            // imageCurBut
            // 
            imageCurBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            imageCurBut.BackColor = Color.FromArgb(49, 91, 138);
            imageCurBut.Location = new Point(482, 385);
            imageCurBut.Name = "imageCurBut";
            imageCurBut.Size = new Size(96, 23);
            imageCurBut.TabIndex = 5;
            imageCurBut.Text = "Image Selected";
            imageCurBut.UseVisualStyleBackColor = false;
            imageCurBut.Click += imageCurBut_Click;
            // 
            // thresholdLabel
            // 
            thresholdLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            thresholdLabel.AutoSize = true;
            thresholdLabel.Location = new Point(212, 389);
            thresholdLabel.Name = "thresholdLabel";
            thresholdLabel.Size = new Size(62, 15);
            thresholdLabel.TabIndex = 1;
            thresholdLabel.Text = "Threshold:";
            // 
            // minWidthLabel
            // 
            minWidthLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            minWidthLabel.AutoSize = true;
            minWidthLabel.Location = new Point(212, 428);
            minWidthLabel.Name = "minWidthLabel";
            minWidthLabel.Size = new Size(66, 15);
            minWidthLabel.TabIndex = 4;
            minWidthLabel.Text = "Min Width:";
            // 
            // minWidthBar
            // 
            minWidthBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            minWidthBar.LargeChange = 1;
            minWidthBar.Location = new Point(0, 421);
            minWidthBar.Maximum = 5000;
            minWidthBar.Name = "minWidthBar";
            minWidthBar.Size = new Size(203, 45);
            minWidthBar.TabIndex = 3;
            minWidthBar.Value = 2;
            minWidthBar.Scroll += sizeBar_Scroll;
            // 
            // imageSplitContainer
            // 
            imageSplitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            imageSplitContainer.Location = new Point(0, 27);
            imageSplitContainer.Name = "imageSplitContainer";
            // 
            // imageSplitContainer.Panel1
            // 
            imageSplitContainer.Panel1.Controls.Add(pictureBox);
            imageSplitContainer.Size = new Size(800, 319);
            imageSplitContainer.SplitterDistance = 387;
            imageSplitContainer.TabIndex = 8;
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(387, 319);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            // 
            // chanBox
            // 
            chanBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chanBox.BackColor = Color.FromArgb(49, 91, 138);
            chanBox.ForeColor = Color.White;
            chanBox.Location = new Point(69, 355);
            chanBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            chanBox.Name = "chanBox";
            chanBox.Size = new Size(46, 23);
            chanBox.TabIndex = 25;
            chanBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(12, 357);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 26;
            label3.Text = "Channel";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(121, 357);
            label4.Name = "label4";
            label4.Size = new Size(76, 15);
            label4.TabIndex = 28;
            label4.Text = "RGB Channel";
            // 
            // rgbBox
            // 
            rgbBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rgbBox.BackColor = Color.FromArgb(49, 91, 138);
            rgbBox.ForeColor = Color.White;
            rgbBox.Location = new Point(203, 355);
            rgbBox.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            rgbBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            rgbBox.Name = "rgbBox";
            rgbBox.Size = new Size(46, 23);
            rgbBox.TabIndex = 27;
            rgbBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // objectivesBox
            // 
            objectivesBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            objectivesBox.BackColor = Color.FromArgb(49, 91, 138);
            objectivesBox.ForeColor = Color.White;
            objectivesBox.FormattingEnabled = true;
            objectivesBox.Location = new Point(368, 541);
            objectivesBox.Name = "objectivesBox";
            objectivesBox.Size = new Size(121, 23);
            objectivesBox.TabIndex = 29;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(305, 544);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 30;
            label5.Text = "Objective";
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView.BackColor = Color.FromArgb(49, 91, 138);
            listView.ForeColor = Color.White;
            listView.Location = new Point(584, 355);
            listView.Name = "listView";
            listView.Size = new Size(204, 214);
            listView.TabIndex = 31;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.List;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            // 
            // CellImager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            ClientSize = new Size(800, 581);
            Controls.Add(listView);
            Controls.Add(label5);
            Controls.Add(objectivesBox);
            Controls.Add(label4);
            Controls.Add(rgbBox);
            Controls.Add(label3);
            Controls.Add(chanBox);
            Controls.Add(clearBut);
            Controls.Add(menuStrip1);
            Controls.Add(tileYBox);
            Controls.Add(imageSplitContainer);
            Controls.Add(label2);
            Controls.Add(maxWidthBar);
            Controls.Add(tileXBox);
            Controls.Add(minWidthBar);
            Controls.Add(tileBox);
            Controls.Add(minWidthLabel);
            Controls.Add(numericRBut);
            Controls.Add(thresholdLabel);
            Controls.Add(imageCurBut);
            Controls.Add(thresholdBar);
            Controls.Add(medianRBut);
            Controls.Add(imageAllBut);
            Controls.Add(maxHeightLabel);
            Controls.Add(label1);
            Controls.Add(maxHeightBar);
            Controls.Add(minHeightBar);
            Controls.Add(maxWidthLabel);
            Controls.Add(minHeightLabel);
            Controls.Add(comboBox);
            Controls.Add(detectBut);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CellImager";
            Text = "Cell Imager";
            FormClosing += CellImager_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tileYBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)tileXBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxHeightBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxWidthBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)minHeightBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)thresholdBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)minWidthBar).EndInit();
            imageSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)imageSplitContainer).EndInit();
            imageSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)chanBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)rgbBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
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