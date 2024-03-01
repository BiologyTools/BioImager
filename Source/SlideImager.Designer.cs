namespace BioImager
{
    partial class SlideImager
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlideImager));
            xBox = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            yBox = new NumericUpDown();
            label5 = new Label();
            hBox = new NumericUpDown();
            label6 = new Label();
            wBox = new NumericUpDown();
            setToStageBut = new Button();
            setSizeBut = new Button();
            addBut = new Button();
            label1 = new Label();
            focBox = new NumericUpDown();
            objBox = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            listBox = new ListBox();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            startBut = new Button();
            saveFileDialog = new SaveFileDialog();
            compBox = new ComboBox();
            label9 = new Label();
            focBut = new Button();
            remBut = new Button();
            timer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)xBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)yBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)focBox).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // xBox
            // 
            xBox.BackColor = Color.FromArgb(49, 91, 138);
            xBox.DecimalPlaces = 3;
            xBox.ForeColor = SystemColors.Window;
            xBox.Location = new Point(42, 31);
            xBox.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            xBox.Minimum = new decimal(new int[] { 1000000000, 0, 0, int.MinValue });
            xBox.Name = "xBox";
            xBox.Size = new Size(120, 23);
            xBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 33);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 3;
            label2.Text = "X";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(209, 15);
            label3.TabIndex = 4;
            label3.Text = "Define the area of slide to image. (µm)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(166, 33);
            label4.Name = "label4";
            label4.Size = new Size(14, 15);
            label4.TabIndex = 6;
            label4.Text = "Y";
            // 
            // yBox
            // 
            yBox.BackColor = Color.FromArgb(49, 91, 138);
            yBox.DecimalPlaces = 3;
            yBox.ForeColor = SystemColors.Window;
            yBox.Location = new Point(187, 31);
            yBox.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            yBox.Minimum = new decimal(new int[] { 1000000000, 0, 0, int.MinValue });
            yBox.Name = "yBox";
            yBox.Size = new Size(120, 23);
            yBox.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(457, 33);
            label5.Name = "label5";
            label5.Size = new Size(16, 15);
            label5.TabIndex = 10;
            label5.Text = "H";
            // 
            // hBox
            // 
            hBox.BackColor = Color.FromArgb(49, 91, 138);
            hBox.ForeColor = SystemColors.Window;
            hBox.Location = new Point(478, 31);
            hBox.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            hBox.Name = "hBox";
            hBox.Size = new Size(120, 23);
            hBox.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(312, 33);
            label6.Name = "label6";
            label6.Size = new Size(18, 15);
            label6.TabIndex = 8;
            label6.Text = "W";
            // 
            // wBox
            // 
            wBox.BackColor = Color.FromArgb(49, 91, 138);
            wBox.ForeColor = SystemColors.Window;
            wBox.Location = new Point(333, 31);
            wBox.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            wBox.Name = "wBox";
            wBox.Size = new Size(120, 23);
            wBox.TabIndex = 7;
            // 
            // setToStageBut
            // 
            setToStageBut.ForeColor = Color.Black;
            setToStageBut.Location = new Point(42, 60);
            setToStageBut.Name = "setToStageBut";
            setToStageBut.Size = new Size(265, 23);
            setToStageBut.TabIndex = 11;
            setToStageBut.Text = "Set to Area Location to Stage.";
            setToStageBut.UseVisualStyleBackColor = true;
            setToStageBut.Click += setToStageBut_Click;
            // 
            // setSizeBut
            // 
            setSizeBut.ForeColor = Color.Black;
            setSizeBut.Location = new Point(333, 60);
            setSizeBut.Name = "setSizeBut";
            setSizeBut.Size = new Size(265, 23);
            setSizeBut.TabIndex = 12;
            setSizeBut.Text = "Set Area Size based on stage coordinates.";
            setSizeBut.UseVisualStyleBackColor = true;
            setSizeBut.Click += setSizeBut_Click;
            // 
            // addBut
            // 
            addBut.ForeColor = Color.Black;
            addBut.Location = new Point(523, 114);
            addBut.Name = "addBut";
            addBut.Size = new Size(75, 23);
            addBut.TabIndex = 13;
            addBut.Text = "Add Level";
            addBut.UseVisualStyleBackColor = true;
            addBut.Click += addBut_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(353, 118);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 15;
            label1.Text = "Focus";
            // 
            // focBox
            // 
            focBox.BackColor = Color.FromArgb(49, 91, 138);
            focBox.DecimalPlaces = 3;
            focBox.ForeColor = SystemColors.Window;
            focBox.Location = new Point(397, 114);
            focBox.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            focBox.Minimum = new decimal(new int[] { 1000000000, 0, 0, int.MinValue });
            focBox.Name = "focBox";
            focBox.Size = new Size(120, 23);
            focBox.TabIndex = 14;
            focBox.ValueChanged += focBox_ValueChanged;
            // 
            // objBox
            // 
            objBox.BackColor = Color.FromArgb(49, 91, 138);
            objBox.FormattingEnabled = true;
            objBox.Location = new Point(255, 115);
            objBox.Name = "objBox";
            objBox.Size = new Size(92, 23);
            objBox.TabIndex = 16;
            objBox.SelectedIndexChanged += objBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(192, 118);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 17;
            label7.Text = "Objective";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 92);
            label8.Name = "label8";
            label8.Size = new Size(407, 15);
            label8.TabIndex = 18;
            label8.Text = "Add pyramidal levels. Setting the objective and the focus to use for the area.";
            // 
            // listBox
            // 
            listBox.BackColor = Color.FromArgb(49, 91, 138);
            listBox.ForeColor = Color.White;
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(12, 142);
            listBox.Name = "listBox";
            listBox.Size = new Size(586, 94);
            listBox.TabIndex = 19;
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel, toolStripProgressBar });
            statusStrip.Location = new Point(0, 273);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(609, 22);
            statusStrip.TabIndex = 20;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(71, 17);
            toolStripStatusLabel.Text = "Imaging 0/0";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new Size(100, 16);
            // 
            // startBut
            // 
            startBut.ForeColor = Color.Black;
            startBut.Location = new Point(523, 242);
            startBut.Name = "startBut";
            startBut.Size = new Size(75, 23);
            startBut.TabIndex = 21;
            startBut.Text = "Start";
            startBut.UseVisualStyleBackColor = true;
            startBut.Click += startBut_Click;
            // 
            // saveFileDialog
            // 
            saveFileDialog.SupportMultiDottedExtensions = true;
            saveFileDialog.Title = "Save Whole Slide Image as.";
            // 
            // compBox
            // 
            compBox.BackColor = Color.FromArgb(49, 91, 138);
            compBox.FormattingEnabled = true;
            compBox.Location = new Point(95, 114);
            compBox.Name = "compBox";
            compBox.Size = new Size(91, 23);
            compBox.TabIndex = 22;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 118);
            label9.Name = "label9";
            label9.Size = new Size(77, 15);
            label9.TabIndex = 23;
            label9.Text = "Compression";
            // 
            // focBut
            // 
            focBut.ForeColor = Color.Black;
            focBut.Location = new Point(442, 88);
            focBut.Name = "focBut";
            focBut.Size = new Size(75, 23);
            focBut.TabIndex = 24;
            focBut.Text = "Set Focus";
            focBut.UseVisualStyleBackColor = true;
            focBut.Click += focBut_Click;
            // 
            // remBut
            // 
            remBut.ForeColor = Color.Black;
            remBut.Location = new Point(429, 242);
            remBut.Name = "remBut";
            remBut.Size = new Size(88, 23);
            remBut.TabIndex = 25;
            remBut.Text = "Remove Level";
            remBut.UseVisualStyleBackColor = true;
            remBut.Click += remBut_Click;
            // 
            // timer
            // 
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // SlideImager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            ClientSize = new Size(609, 295);
            Controls.Add(remBut);
            Controls.Add(focBut);
            Controls.Add(label9);
            Controls.Add(compBox);
            Controls.Add(startBut);
            Controls.Add(statusStrip);
            Controls.Add(listBox);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(objBox);
            Controls.Add(label1);
            Controls.Add(focBox);
            Controls.Add(addBut);
            Controls.Add(setSizeBut);
            Controls.Add(setToStageBut);
            Controls.Add(label5);
            Controls.Add(hBox);
            Controls.Add(label6);
            Controls.Add(wBox);
            Controls.Add(label4);
            Controls.Add(yBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(xBox);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SlideImager";
            Text = "Slide Imager";
            FormClosing += SlideImager_FormClosing;
            ((System.ComponentModel.ISupportInitialize)xBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)yBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)hBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)wBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)focBox).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private NumericUpDown xBox;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown yBox;
        private Label label5;
        private NumericUpDown hBox;
        private Label label6;
        private NumericUpDown wBox;
        private Button setToStageBut;
        private Button setSizeBut;
        private Button addBut;
        private Label label1;
        private NumericUpDown focBox;
        private ComboBox objBox;
        private Label label7;
        private Label label8;
        private ListBox listBox;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripProgressBar toolStripProgressBar;
        private Button startBut;
        private SaveFileDialog saveFileDialog;
        private ComboBox compBox;
        private Label label9;
        private Button focBut;
        private Button remBut;
        private System.Windows.Forms.Timer timer;
    }
}