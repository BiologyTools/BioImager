
namespace Bio
{
    partial class StageTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StageTool));
            menuStrip = new MenuStrip();
            objToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            showControlsMenuItem = new ToolStripMenuItem();
            contextMenuStrip = new ContextMenuStrip(components);
            hideToolStripMenuItem = new ToolStripMenuItem();
            moveYBox = new NumericUpDown();
            label1 = new Label();
            moveXBox = new NumericUpDown();
            objBox = new ComboBox();
            rightBut = new Button();
            downBut = new Button();
            leftBut = new Button();
            upBut = new Button();
            panel2 = new Panel();
            setFolderBut = new Button();
            folderBox = new TextBox();
            label8 = new Label();
            tilesBut = new Button();
            label6 = new Label();
            nameBox = new TextBox();
            label7 = new Label();
            fIntervalBox = new NumericUpDown();
            setLowerBut = new Button();
            setUpperBut = new Button();
            dockBox = new CheckBox();
            label5 = new Label();
            label4 = new Label();
            sliceBox = new NumericUpDown();
            label3 = new Label();
            label11 = new Label();
            stackBut = new Button();
            upperLimBox = new NumericUpDown();
            lowerLimBox = new NumericUpDown();
            takeImageBut = new Button();
            topMostBox = new CheckBox();
            setObjBut = new Button();
            label2 = new Label();
            timer = new System.Windows.Forms.Timer(components);
            goLowerBut = new Button();
            goUpperBut = new Button();
            menuStrip.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)moveYBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)moveXBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fIntervalBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliceBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)upperLimBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lowerLimBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { objToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(331, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // objToolStripMenuItem
            // 
            objToolStripMenuItem.Name = "objToolStripMenuItem";
            objToolStripMenuItem.Size = new Size(114, 20);
            objToolStripMenuItem.Text = "Microscope Setup";
            objToolStripMenuItem.Click += objToolStripMenuItem_Click;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { showControlsMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip";
            contextMenuStrip2.Size = new Size(152, 26);
            // 
            // showControlsMenuItem
            // 
            showControlsMenuItem.Name = "showControlsMenuItem";
            showControlsMenuItem.Size = new Size(151, 22);
            showControlsMenuItem.Text = "Show Controls";
            showControlsMenuItem.Click += toolStripMenuItem1_Click;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { hideToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(100, 26);
            // 
            // hideToolStripMenuItem
            // 
            hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            hideToolStripMenuItem.Size = new Size(99, 22);
            hideToolStripMenuItem.Text = "Hide";
            hideToolStripMenuItem.Click += hideToolStripMenuItem_Click;
            // 
            // moveYBox
            // 
            moveYBox.BackColor = Color.FromArgb(49, 91, 138);
            moveYBox.DecimalPlaces = 4;
            moveYBox.ForeColor = Color.White;
            moveYBox.Location = new Point(239, 102);
            moveYBox.Margin = new Padding(4, 3, 4, 3);
            moveYBox.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            moveYBox.Name = "moveYBox";
            moveYBox.Size = new Size(89, 23);
            moveYBox.TabIndex = 7;
            moveYBox.Value = new decimal(new int[] { 1002000, 0, 0, 262144 });
            moveYBox.ValueChanged += moveYBox_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(199, 74);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 6;
            label1.Text = "SizeX";
            // 
            // moveXBox
            // 
            moveXBox.BackColor = Color.FromArgb(49, 91, 138);
            moveXBox.DecimalPlaces = 4;
            moveXBox.ForeColor = Color.White;
            moveXBox.Location = new Point(239, 72);
            moveXBox.Margin = new Padding(4, 3, 4, 3);
            moveXBox.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            moveXBox.Name = "moveXBox";
            moveXBox.Size = new Size(89, 23);
            moveXBox.TabIndex = 5;
            moveXBox.Value = new decimal(new int[] { 1249000, 0, 0, 262144 });
            moveXBox.ValueChanged += moveXBox_ValueChanged;
            // 
            // objBox
            // 
            objBox.BackColor = Color.FromArgb(49, 91, 138);
            objBox.ForeColor = Color.White;
            objBox.FormattingEnabled = true;
            objBox.Location = new Point(205, 6);
            objBox.Margin = new Padding(4, 3, 4, 3);
            objBox.Name = "objBox";
            objBox.Size = new Size(122, 23);
            objBox.TabIndex = 4;
            objBox.SelectedIndexChanged += objBox_SelectedIndexChanged;
            // 
            // rightBut
            // 
            rightBut.BackColor = Color.FromArgb(49, 91, 138);
            rightBut.ForeColor = Color.White;
            rightBut.Location = new Point(104, 53);
            rightBut.Margin = new Padding(4, 3, 4, 3);
            rightBut.Name = "rightBut";
            rightBut.Size = new Size(52, 52);
            rightBut.TabIndex = 3;
            rightBut.Text = "Right";
            rightBut.UseVisualStyleBackColor = false;
            rightBut.Click += rightBut_Click;
            // 
            // downBut
            // 
            downBut.BackColor = Color.FromArgb(49, 91, 138);
            downBut.ForeColor = Color.White;
            downBut.Location = new Point(54, 103);
            downBut.Margin = new Padding(4, 3, 4, 3);
            downBut.Name = "downBut";
            downBut.Size = new Size(52, 52);
            downBut.TabIndex = 2;
            downBut.Text = "Down";
            downBut.UseVisualStyleBackColor = false;
            downBut.Click += downBut_Click;
            // 
            // leftBut
            // 
            leftBut.BackColor = Color.FromArgb(49, 91, 138);
            leftBut.ForeColor = Color.White;
            leftBut.Location = new Point(4, 53);
            leftBut.Margin = new Padding(4, 3, 4, 3);
            leftBut.Name = "leftBut";
            leftBut.Size = new Size(52, 52);
            leftBut.TabIndex = 1;
            leftBut.Text = "Left";
            leftBut.UseVisualStyleBackColor = false;
            leftBut.Click += leftBut_Click;
            // 
            // upBut
            // 
            upBut.BackColor = Color.FromArgb(49, 91, 138);
            upBut.ForeColor = Color.White;
            upBut.Location = new Point(54, 3);
            upBut.Margin = new Padding(4, 3, 4, 3);
            upBut.Name = "upBut";
            upBut.Size = new Size(52, 52);
            upBut.TabIndex = 0;
            upBut.Text = "Up";
            upBut.UseVisualStyleBackColor = false;
            upBut.Click += upBut_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(49, 91, 138);
            panel2.ContextMenuStrip = contextMenuStrip;
            panel2.Controls.Add(goLowerBut);
            panel2.Controls.Add(goUpperBut);
            panel2.Controls.Add(setFolderBut);
            panel2.Controls.Add(folderBox);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(tilesBut);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(nameBox);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(fIntervalBox);
            panel2.Controls.Add(setLowerBut);
            panel2.Controls.Add(setUpperBut);
            panel2.Controls.Add(dockBox);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(sliceBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(stackBut);
            panel2.Controls.Add(upperLimBox);
            panel2.Controls.Add(lowerLimBox);
            panel2.Controls.Add(takeImageBut);
            panel2.Controls.Add(topMostBox);
            panel2.Controls.Add(setObjBut);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(upBut);
            panel2.Controls.Add(rightBut);
            panel2.Controls.Add(leftBut);
            panel2.Controls.Add(moveXBox);
            panel2.Controls.Add(moveYBox);
            panel2.Controls.Add(objBox);
            panel2.Controls.Add(downBut);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(0, 28);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(331, 330);
            panel2.TabIndex = 8;
            // 
            // setFolderBut
            // 
            setFolderBut.BackColor = Color.FromArgb(49, 91, 138);
            setFolderBut.ForeColor = Color.White;
            setFolderBut.Location = new Point(259, 300);
            setFolderBut.Margin = new Padding(4, 3, 4, 3);
            setFolderBut.Name = "setFolderBut";
            setFolderBut.Size = new Size(52, 29);
            setFolderBut.TabIndex = 171;
            setFolderBut.Text = "Set";
            setFolderBut.UseVisualStyleBackColor = false;
            setFolderBut.Click += setFolderBut_Click;
            // 
            // folderBox
            // 
            folderBox.BackColor = Color.FromArgb(49, 91, 138);
            folderBox.Enabled = false;
            folderBox.ForeColor = Color.White;
            folderBox.Location = new Point(104, 303);
            folderBox.Margin = new Padding(4, 3, 4, 3);
            folderBox.Name = "folderBox";
            folderBox.Size = new Size(148, 23);
            folderBox.TabIndex = 170;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(10, 306);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 169;
            label8.Text = "Imaging Folder";
            // 
            // tilesBut
            // 
            tilesBut.BackColor = Color.FromArgb(49, 91, 138);
            tilesBut.ForeColor = Color.White;
            tilesBut.Location = new Point(205, 193);
            tilesBut.Margin = new Padding(4, 3, 4, 3);
            tilesBut.Name = "tilesBut";
            tilesBut.Size = new Size(122, 29);
            tilesBut.TabIndex = 167;
            tilesBut.Text = "Take Image Tiles";
            tilesBut.UseVisualStyleBackColor = false;
            tilesBut.Click += tilesBut_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(10, 279);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 166;
            label6.Text = "Image Name";
            // 
            // nameBox
            // 
            nameBox.BackColor = Color.FromArgb(49, 91, 138);
            nameBox.ForeColor = Color.White;
            nameBox.Location = new Point(104, 276);
            nameBox.Margin = new Padding(4, 3, 4, 3);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(207, 23);
            nameBox.TabIndex = 165;
            nameBox.Text = "Image";
            nameBox.TextChanged += nameBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(110, 248);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(46, 15);
            label7.TabIndex = 29;
            label7.Text = "Interval";
            // 
            // fIntervalBox
            // 
            fIntervalBox.BackColor = Color.FromArgb(49, 91, 138);
            fIntervalBox.DecimalPlaces = 6;
            fIntervalBox.ForeColor = Color.White;
            fIntervalBox.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            fIntervalBox.Location = new Point(14, 246);
            fIntervalBox.Margin = new Padding(4, 3, 4, 3);
            fIntervalBox.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            fIntervalBox.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            fIntervalBox.Name = "fIntervalBox";
            fIntervalBox.Size = new Size(92, 23);
            fIntervalBox.TabIndex = 27;
            fIntervalBox.Value = new decimal(new int[] { 5, 0, 0, 0 });
            fIntervalBox.ValueChanged += fIntervalBox_ValueChanged;
            // 
            // setLowerBut
            // 
            setLowerBut.BackColor = Color.FromArgb(49, 91, 138);
            setLowerBut.ForeColor = Color.White;
            setLowerBut.Location = new Point(129, 189);
            setLowerBut.Margin = new Padding(4, 3, 4, 3);
            setLowerBut.Name = "setLowerBut";
            setLowerBut.Size = new Size(43, 23);
            setLowerBut.TabIndex = 26;
            setLowerBut.Text = "Set";
            setLowerBut.UseVisualStyleBackColor = false;
            setLowerBut.Click += setLowerBut_Click;
            // 
            // setUpperBut
            // 
            setUpperBut.BackColor = Color.FromArgb(49, 91, 138);
            setUpperBut.ForeColor = Color.White;
            setUpperBut.Location = new Point(129, 159);
            setUpperBut.Margin = new Padding(4, 3, 4, 3);
            setUpperBut.Name = "setUpperBut";
            setUpperBut.Size = new Size(43, 23);
            setUpperBut.TabIndex = 25;
            setUpperBut.Text = "Set";
            setUpperBut.UseVisualStyleBackColor = false;
            setUpperBut.Click += setUpperBut_Click;
            // 
            // dockBox
            // 
            dockBox.AutoSize = true;
            dockBox.Location = new Point(268, 252);
            dockBox.Margin = new Padding(4, 3, 4, 3);
            dockBox.Name = "dockBox";
            dockBox.Size = new Size(53, 19);
            dockBox.TabIndex = 24;
            dockBox.Text = "Dock";
            dockBox.UseVisualStyleBackColor = true;
            dockBox.CheckedChanged += dockBox_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(115, 9);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 23;
            label5.Text = "Objectives";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(110, 222);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 22;
            label4.Text = "Slices";
            // 
            // sliceBox
            // 
            sliceBox.BackColor = Color.FromArgb(49, 91, 138);
            sliceBox.ForeColor = Color.White;
            sliceBox.Location = new Point(14, 218);
            sliceBox.Margin = new Padding(4, 3, 4, 3);
            sliceBox.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            sliceBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            sliceBox.Name = "sliceBox";
            sliceBox.Size = new Size(92, 23);
            sliceBox.TabIndex = 21;
            sliceBox.Value = new decimal(new int[] { 5, 0, 0, 0 });
            sliceBox.ValueChanged += sliceBox_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(107, 193);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(25, 15);
            label3.TabIndex = 20;
            label3.Text = "μm";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(106, 163);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(25, 15);
            label11.TabIndex = 19;
            label11.Text = "μm";
            // 
            // stackBut
            // 
            stackBut.BackColor = Color.FromArgb(49, 91, 138);
            stackBut.ForeColor = Color.White;
            stackBut.Location = new Point(205, 163);
            stackBut.Margin = new Padding(4, 3, 4, 3);
            stackBut.Name = "stackBut";
            stackBut.Size = new Size(122, 29);
            stackBut.TabIndex = 16;
            stackBut.Text = "Take Image Stack";
            stackBut.UseVisualStyleBackColor = false;
            stackBut.Click += stackBut_Click;
            // 
            // upperLimBox
            // 
            upperLimBox.BackColor = Color.FromArgb(49, 91, 138);
            upperLimBox.DecimalPlaces = 4;
            upperLimBox.ForeColor = Color.White;
            upperLimBox.Location = new Point(14, 159);
            upperLimBox.Margin = new Padding(4, 3, 4, 3);
            upperLimBox.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            upperLimBox.Name = "upperLimBox";
            upperLimBox.Size = new Size(92, 23);
            upperLimBox.TabIndex = 12;
            upperLimBox.Value = new decimal(new int[] { 125, 0, 0, 0 });
            upperLimBox.ValueChanged += upperLimBox_ValueChanged;
            // 
            // lowerLimBox
            // 
            lowerLimBox.BackColor = Color.FromArgb(49, 91, 138);
            lowerLimBox.DecimalPlaces = 4;
            lowerLimBox.ForeColor = Color.White;
            lowerLimBox.Location = new Point(14, 189);
            lowerLimBox.Margin = new Padding(4, 3, 4, 3);
            lowerLimBox.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            lowerLimBox.Name = "lowerLimBox";
            lowerLimBox.Size = new Size(92, 23);
            lowerLimBox.TabIndex = 14;
            lowerLimBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            lowerLimBox.ValueChanged += lowerLimBox_ValueChanged;
            // 
            // takeImageBut
            // 
            takeImageBut.BackColor = Color.FromArgb(49, 91, 138);
            takeImageBut.ForeColor = Color.White;
            takeImageBut.Location = new Point(205, 132);
            takeImageBut.Margin = new Padding(4, 3, 4, 3);
            takeImageBut.Name = "takeImageBut";
            takeImageBut.Size = new Size(122, 29);
            takeImageBut.TabIndex = 11;
            takeImageBut.Text = "Take Image";
            takeImageBut.UseVisualStyleBackColor = false;
            takeImageBut.Click += takeImageBut_Click;
            // 
            // topMostBox
            // 
            topMostBox.AutoSize = true;
            topMostBox.Checked = true;
            topMostBox.CheckState = CheckState.Checked;
            topMostBox.Location = new Point(190, 252);
            topMostBox.Margin = new Padding(4, 3, 4, 3);
            topMostBox.Name = "topMostBox";
            topMostBox.Size = new Size(75, 19);
            topMostBox.TabIndex = 10;
            topMostBox.Text = "Top Most";
            topMostBox.UseVisualStyleBackColor = true;
            topMostBox.CheckedChanged += topMostBox_CheckedChanged;
            // 
            // setObjBut
            // 
            setObjBut.BackColor = Color.FromArgb(49, 91, 138);
            setObjBut.ForeColor = Color.White;
            setObjBut.Location = new Point(205, 35);
            setObjBut.Margin = new Padding(4, 3, 4, 3);
            setObjBut.Name = "setObjBut";
            setObjBut.Size = new Size(122, 29);
            setObjBut.TabIndex = 9;
            setObjBut.Text = "Set Objective";
            setObjBut.UseVisualStyleBackColor = false;
            setObjBut.Click += setObjBut_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(199, 104);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 8;
            label2.Text = "SizeY";
            label2.Click += label2_Click;
            // 
            // timer
            // 
            timer.Interval = 500;
            timer.Tick += timer_Tick;
            // 
            // goLowerBut
            // 
            goLowerBut.BackColor = Color.FromArgb(49, 91, 138);
            goLowerBut.ForeColor = Color.White;
            goLowerBut.Location = new Point(173, 189);
            goLowerBut.Margin = new Padding(4, 3, 4, 3);
            goLowerBut.Name = "goLowerBut";
            goLowerBut.Size = new Size(32, 23);
            goLowerBut.TabIndex = 173;
            goLowerBut.Text = "Go";
            goLowerBut.UseVisualStyleBackColor = false;
            goLowerBut.Click += goLowerBut_Click;
            // 
            // goUpperBut
            // 
            goUpperBut.BackColor = Color.FromArgb(49, 91, 138);
            goUpperBut.ForeColor = Color.White;
            goUpperBut.Location = new Point(173, 159);
            goUpperBut.Margin = new Padding(4, 3, 4, 3);
            goUpperBut.Name = "goUpperBut";
            goUpperBut.Size = new Size(32, 23);
            goUpperBut.TabIndex = 172;
            goUpperBut.Text = "Go";
            goUpperBut.UseVisualStyleBackColor = false;
            goUpperBut.Click += goUpperBut_Click;
            // 
            // StageTool
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            ClientSize = new Size(331, 359);
            Controls.Add(panel2);
            Controls.Add(menuStrip);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            MaximumSize = new Size(347, 398);
            Name = "StageTool";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Stage Tool";
            TopMost = true;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            contextMenuStrip2.ResumeLayout(false);
            contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)moveYBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)moveXBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fIntervalBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliceBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)upperLimBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)lowerLimBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem objToolStripMenuItem;
        private ComboBox objBox;
        private Button rightBut;
        private Button downBut;
        private Button leftBut;
        private Button upBut;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem hideToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem showControlsMenuItem;
        private NumericUpDown moveXBox;
        private NumericUpDown moveYBox;
        private Label label1;
        private Panel panel2;
        private Label label2;
        private Button setObjBut;
        private CheckBox topMostBox;
        private Button takeImageBut;
        private Button stackBut;
        private NumericUpDown upperLimBox;
        private NumericUpDown lowerLimBox;
        private NumericUpDown sliceBox;
        private Label label3;
        private Label label11;
        private Label label4;
        private CheckBox dockBox;
        private Label label5;
        private System.Windows.Forms.Timer timer;
        private Button setLowerBut;
        private Button setUpperBut;
        private NumericUpDown fIntervalBox;
        private Label label7;
        private Label label6;
        private TextBox nameBox;
        private Button tilesBut;
        private Button setFolderBut;
        private TextBox folderBox;
        private Label label8;
        private Button goLowerBut;
        private Button goUpperBut;
    }
}