
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StageTool));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.objToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showControlsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveYBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.moveXBox = new System.Windows.Forms.NumericUpDown();
            this.objBox = new System.Windows.Forms.ComboBox();
            this.rightBut = new System.Windows.Forms.Button();
            this.downBut = new System.Windows.Forms.Button();
            this.leftBut = new System.Windows.Forms.Button();
            this.upBut = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.setFolderBut = new System.Windows.Forms.Button();
            this.folderBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tilesBut = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.fIntervalBox = new System.Windows.Forms.NumericUpDown();
            this.setLowerBut = new System.Windows.Forms.Button();
            this.setUpperBut = new System.Windows.Forms.Button();
            this.dockBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sliceBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.stackBut = new System.Windows.Forms.Button();
            this.upperLimBox = new System.Windows.Forms.NumericUpDown();
            this.lowerLimBox = new System.Windows.Forms.NumericUpDown();
            this.takeImageBut = new System.Windows.Forms.Button();
            this.topMostBox = new System.Windows.Forms.CheckBox();
            this.setObjBut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveXBox)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fIntervalBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliceBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperLimBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerLimBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(284, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // objToolStripMenuItem
            // 
            this.objToolStripMenuItem.Name = "objToolStripMenuItem";
            this.objToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.objToolStripMenuItem.Text = "Microscope Setup";
            this.objToolStripMenuItem.Click += new System.EventHandler(this.objToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showControlsMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip";
            this.contextMenuStrip2.Size = new System.Drawing.Size(152, 26);
            // 
            // showControlsMenuItem
            // 
            this.showControlsMenuItem.Name = "showControlsMenuItem";
            this.showControlsMenuItem.Size = new System.Drawing.Size(151, 22);
            this.showControlsMenuItem.Text = "Show Controls";
            this.showControlsMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(100, 26);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // moveYBox
            // 
            this.moveYBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.moveYBox.DecimalPlaces = 4;
            this.moveYBox.ForeColor = System.Drawing.Color.White;
            this.moveYBox.Location = new System.Drawing.Point(191, 88);
            this.moveYBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.moveYBox.Name = "moveYBox";
            this.moveYBox.Size = new System.Drawing.Size(76, 20);
            this.moveYBox.TabIndex = 7;
            this.moveYBox.Value = new decimal(new int[] {
            1002000,
            0,
            0,
            262144});
            this.moveYBox.ValueChanged += new System.EventHandler(this.moveYBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(157, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "SizeX";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // moveXBox
            // 
            this.moveXBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.moveXBox.DecimalPlaces = 4;
            this.moveXBox.ForeColor = System.Drawing.Color.White;
            this.moveXBox.Location = new System.Drawing.Point(191, 62);
            this.moveXBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.moveXBox.Name = "moveXBox";
            this.moveXBox.Size = new System.Drawing.Size(76, 20);
            this.moveXBox.TabIndex = 5;
            this.moveXBox.Value = new decimal(new int[] {
            1249000,
            0,
            0,
            262144});
            this.moveXBox.ValueChanged += new System.EventHandler(this.moveXBox_ValueChanged);
            // 
            // objBox
            // 
            this.objBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.objBox.ForeColor = System.Drawing.Color.White;
            this.objBox.FormattingEnabled = true;
            this.objBox.Location = new System.Drawing.Point(162, 5);
            this.objBox.Name = "objBox";
            this.objBox.Size = new System.Drawing.Size(105, 21);
            this.objBox.TabIndex = 4;
            this.objBox.SelectedIndexChanged += new System.EventHandler(this.objBox_SelectedIndexChanged);
            // 
            // rightBut
            // 
            this.rightBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.rightBut.ForeColor = System.Drawing.Color.White;
            this.rightBut.Location = new System.Drawing.Point(89, 46);
            this.rightBut.Name = "rightBut";
            this.rightBut.Size = new System.Drawing.Size(45, 45);
            this.rightBut.TabIndex = 3;
            this.rightBut.Text = "Right";
            this.rightBut.UseVisualStyleBackColor = false;
            this.rightBut.Click += new System.EventHandler(this.rightBut_Click);
            // 
            // downBut
            // 
            this.downBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.downBut.ForeColor = System.Drawing.Color.White;
            this.downBut.Location = new System.Drawing.Point(46, 89);
            this.downBut.Name = "downBut";
            this.downBut.Size = new System.Drawing.Size(45, 45);
            this.downBut.TabIndex = 2;
            this.downBut.Text = "Down";
            this.downBut.UseVisualStyleBackColor = false;
            this.downBut.Click += new System.EventHandler(this.downBut_Click);
            // 
            // leftBut
            // 
            this.leftBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.leftBut.ForeColor = System.Drawing.Color.White;
            this.leftBut.Location = new System.Drawing.Point(3, 46);
            this.leftBut.Name = "leftBut";
            this.leftBut.Size = new System.Drawing.Size(45, 45);
            this.leftBut.TabIndex = 1;
            this.leftBut.Text = "Left";
            this.leftBut.UseVisualStyleBackColor = false;
            this.leftBut.Click += new System.EventHandler(this.leftBut_Click);
            // 
            // upBut
            // 
            this.upBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.upBut.ForeColor = System.Drawing.Color.White;
            this.upBut.Location = new System.Drawing.Point(46, 3);
            this.upBut.Name = "upBut";
            this.upBut.Size = new System.Drawing.Size(45, 45);
            this.upBut.TabIndex = 0;
            this.upBut.Text = "Up";
            this.upBut.UseVisualStyleBackColor = false;
            this.upBut.Click += new System.EventHandler(this.upBut_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.panel2.ContextMenuStrip = this.contextMenuStrip;
            this.panel2.Controls.Add(this.setFolderBut);
            this.panel2.Controls.Add(this.folderBox);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tilesBut);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.nameBox);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.fIntervalBox);
            this.panel2.Controls.Add(this.setLowerBut);
            this.panel2.Controls.Add(this.setUpperBut);
            this.panel2.Controls.Add(this.dockBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.sliceBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.stackBut);
            this.panel2.Controls.Add(this.upperLimBox);
            this.panel2.Controls.Add(this.lowerLimBox);
            this.panel2.Controls.Add(this.takeImageBut);
            this.panel2.Controls.Add(this.topMostBox);
            this.panel2.Controls.Add(this.setObjBut);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.upBut);
            this.panel2.Controls.Add(this.rightBut);
            this.panel2.Controls.Add(this.leftBut);
            this.panel2.Controls.Add(this.moveXBox);
            this.panel2.Controls.Add(this.moveYBox);
            this.panel2.Controls.Add(this.objBox);
            this.panel2.Controls.Add(this.downBut);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 286);
            this.panel2.TabIndex = 8;
            // 
            // setFolderBut
            // 
            this.setFolderBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setFolderBut.ForeColor = System.Drawing.Color.White;
            this.setFolderBut.Location = new System.Drawing.Point(222, 260);
            this.setFolderBut.Name = "setFolderBut";
            this.setFolderBut.Size = new System.Drawing.Size(45, 25);
            this.setFolderBut.TabIndex = 171;
            this.setFolderBut.Text = "Set";
            this.setFolderBut.UseVisualStyleBackColor = false;
            this.setFolderBut.Click += new System.EventHandler(this.setFolderBut_Click);
            // 
            // folderBox
            // 
            this.folderBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.folderBox.Enabled = false;
            this.folderBox.ForeColor = System.Drawing.Color.White;
            this.folderBox.Location = new System.Drawing.Point(89, 263);
            this.folderBox.Name = "folderBox";
            this.folderBox.Size = new System.Drawing.Size(127, 20);
            this.folderBox.TabIndex = 170;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(9, 265);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 169;
            this.label8.Text = "Imaging Folder";
            // 
            // tilesBut
            // 
            this.tilesBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tilesBut.ForeColor = System.Drawing.Color.White;
            this.tilesBut.Location = new System.Drawing.Point(162, 167);
            this.tilesBut.Name = "tilesBut";
            this.tilesBut.Size = new System.Drawing.Size(105, 25);
            this.tilesBut.TabIndex = 167;
            this.tilesBut.Text = "Take Image Tiles";
            this.tilesBut.UseVisualStyleBackColor = false;
            this.tilesBut.Click += new System.EventHandler(this.tilesBut_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(9, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 166;
            this.label6.Text = "Image Name";
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.nameBox.ForeColor = System.Drawing.Color.White;
            this.nameBox.Location = new System.Drawing.Point(89, 239);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(178, 20);
            this.nameBox.TabIndex = 165;
            this.nameBox.Text = "Image";
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(94, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Interval";
            // 
            // fIntervalBox
            // 
            this.fIntervalBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.fIntervalBox.DecimalPlaces = 6;
            this.fIntervalBox.ForeColor = System.Drawing.Color.White;
            this.fIntervalBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.fIntervalBox.Location = new System.Drawing.Point(12, 213);
            this.fIntervalBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.fIntervalBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.fIntervalBox.Name = "fIntervalBox";
            this.fIntervalBox.Size = new System.Drawing.Size(79, 20);
            this.fIntervalBox.TabIndex = 27;
            this.fIntervalBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.fIntervalBox.ValueChanged += new System.EventHandler(this.fIntervalBox_ValueChanged);
            // 
            // setLowerBut
            // 
            this.setLowerBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setLowerBut.ForeColor = System.Drawing.Color.White;
            this.setLowerBut.Location = new System.Drawing.Point(97, 164);
            this.setLowerBut.Name = "setLowerBut";
            this.setLowerBut.Size = new System.Drawing.Size(37, 20);
            this.setLowerBut.TabIndex = 26;
            this.setLowerBut.Text = "Set";
            this.setLowerBut.UseVisualStyleBackColor = false;
            this.setLowerBut.Click += new System.EventHandler(this.setLowerBut_Click);
            // 
            // setUpperBut
            // 
            this.setUpperBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setUpperBut.ForeColor = System.Drawing.Color.White;
            this.setUpperBut.Location = new System.Drawing.Point(97, 138);
            this.setUpperBut.Name = "setUpperBut";
            this.setUpperBut.Size = new System.Drawing.Size(37, 20);
            this.setUpperBut.TabIndex = 25;
            this.setUpperBut.Text = "Set";
            this.setUpperBut.UseVisualStyleBackColor = false;
            this.setUpperBut.Click += new System.EventHandler(this.setUpperBut_Click);
            // 
            // dockBox
            // 
            this.dockBox.AutoSize = true;
            this.dockBox.Location = new System.Drawing.Point(230, 218);
            this.dockBox.Name = "dockBox";
            this.dockBox.Size = new System.Drawing.Size(52, 17);
            this.dockBox.TabIndex = 24;
            this.dockBox.Text = "Dock";
            this.dockBox.UseVisualStyleBackColor = true;
            this.dockBox.CheckedChanged += new System.EventHandler(this.dockBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(99, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Objectives";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(94, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Slices";
            // 
            // sliceBox
            // 
            this.sliceBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.sliceBox.ForeColor = System.Drawing.Color.White;
            this.sliceBox.Location = new System.Drawing.Point(12, 189);
            this.sliceBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.sliceBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sliceBox.Name = "sliceBox";
            this.sliceBox.Size = new System.Drawing.Size(79, 20);
            this.sliceBox.TabIndex = 21;
            this.sliceBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.sliceBox.ValueChanged += new System.EventHandler(this.sliceBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "μm";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(135, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "μm";
            // 
            // stackBut
            // 
            this.stackBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.stackBut.ForeColor = System.Drawing.Color.White;
            this.stackBut.Location = new System.Drawing.Point(162, 141);
            this.stackBut.Name = "stackBut";
            this.stackBut.Size = new System.Drawing.Size(105, 25);
            this.stackBut.TabIndex = 16;
            this.stackBut.Text = "Take Image Stack";
            this.stackBut.UseVisualStyleBackColor = false;
            this.stackBut.Click += new System.EventHandler(this.stackBut_Click);
            // 
            // upperLimBox
            // 
            this.upperLimBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.upperLimBox.DecimalPlaces = 4;
            this.upperLimBox.ForeColor = System.Drawing.Color.White;
            this.upperLimBox.Location = new System.Drawing.Point(12, 138);
            this.upperLimBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.upperLimBox.Name = "upperLimBox";
            this.upperLimBox.Size = new System.Drawing.Size(79, 20);
            this.upperLimBox.TabIndex = 12;
            this.upperLimBox.Value = new decimal(new int[] {
            125,
            0,
            0,
            0});
            this.upperLimBox.ValueChanged += new System.EventHandler(this.upperLimBox_ValueChanged);
            // 
            // lowerLimBox
            // 
            this.lowerLimBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.lowerLimBox.DecimalPlaces = 4;
            this.lowerLimBox.ForeColor = System.Drawing.Color.White;
            this.lowerLimBox.Location = new System.Drawing.Point(12, 164);
            this.lowerLimBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.lowerLimBox.Name = "lowerLimBox";
            this.lowerLimBox.Size = new System.Drawing.Size(79, 20);
            this.lowerLimBox.TabIndex = 14;
            this.lowerLimBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.lowerLimBox.ValueChanged += new System.EventHandler(this.lowerLimBox_ValueChanged);
            // 
            // takeImageBut
            // 
            this.takeImageBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.takeImageBut.ForeColor = System.Drawing.Color.White;
            this.takeImageBut.Location = new System.Drawing.Point(162, 114);
            this.takeImageBut.Name = "takeImageBut";
            this.takeImageBut.Size = new System.Drawing.Size(105, 25);
            this.takeImageBut.TabIndex = 11;
            this.takeImageBut.Text = "Take Image";
            this.takeImageBut.UseVisualStyleBackColor = false;
            this.takeImageBut.Click += new System.EventHandler(this.takeImageBut_Click);
            // 
            // topMostBox
            // 
            this.topMostBox.AutoSize = true;
            this.topMostBox.Checked = true;
            this.topMostBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topMostBox.Location = new System.Drawing.Point(163, 218);
            this.topMostBox.Name = "topMostBox";
            this.topMostBox.Size = new System.Drawing.Size(71, 17);
            this.topMostBox.TabIndex = 10;
            this.topMostBox.Text = "Top Most";
            this.topMostBox.UseVisualStyleBackColor = true;
            this.topMostBox.CheckedChanged += new System.EventHandler(this.topMostBox_CheckedChanged);
            // 
            // setObjBut
            // 
            this.setObjBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setObjBut.ForeColor = System.Drawing.Color.White;
            this.setObjBut.Location = new System.Drawing.Point(162, 30);
            this.setObjBut.Name = "setObjBut";
            this.setObjBut.Size = new System.Drawing.Size(105, 25);
            this.setObjBut.TabIndex = 9;
            this.setObjBut.Text = "Set Objective";
            this.setObjBut.UseVisualStyleBackColor = false;
            this.setObjBut.Click += new System.EventHandler(this.setObjBut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(157, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "SizeY";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // StageTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(284, 311);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximumSize = new System.Drawing.Size(300, 350);
            this.Name = "StageTool";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Stage Tool";
            this.TopMost = true;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moveYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveXBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fIntervalBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliceBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperLimBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerLimBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem objToolStripMenuItem;
        private System.Windows.Forms.ComboBox objBox;
        private System.Windows.Forms.Button rightBut;
        private System.Windows.Forms.Button downBut;
        private System.Windows.Forms.Button leftBut;
        private System.Windows.Forms.Button upBut;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem showControlsMenuItem;
        private System.Windows.Forms.NumericUpDown moveXBox;
        private System.Windows.Forms.NumericUpDown moveYBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button setObjBut;
        private System.Windows.Forms.CheckBox topMostBox;
        private System.Windows.Forms.Button takeImageBut;
        private System.Windows.Forms.Button stackBut;
        private System.Windows.Forms.NumericUpDown upperLimBox;
        private System.Windows.Forms.NumericUpDown lowerLimBox;
        private System.Windows.Forms.NumericUpDown sliceBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox dockBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button setLowerBut;
        private System.Windows.Forms.Button setUpperBut;
        private System.Windows.Forms.NumericUpDown fIntervalBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button tilesBut;
        private System.Windows.Forms.Button setFolderBut;
        private System.Windows.Forms.TextBox folderBox;
        private System.Windows.Forms.Label label8;
    }
}