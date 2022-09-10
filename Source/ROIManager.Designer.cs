namespace Bio
{
    partial class ROIManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ROIManager));
            this.roiView = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.rBox = new System.Windows.Forms.NumericUpDown();
            this.gBox = new System.Windows.Forms.NumericUpDown();
            this.bBox = new System.Windows.Forms.NumericUpDown();
            this.tBox = new System.Windows.Forms.NumericUpDown();
            this.cBox = new System.Windows.Forms.NumericUpDown();
            this.zBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.yBox = new System.Windows.Forms.NumericUpDown();
            this.xBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.hBox = new System.Windows.Forms.NumericUpDown();
            this.wBox = new System.Windows.Forms.NumericUpDown();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.showBoundsBox = new System.Windows.Forms.CheckBox();
            this.showTextBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.imageNameLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.TextBox();
            this.pointYBox = new System.Windows.Forms.NumericUpDown();
            this.pointXBox = new System.Windows.Forms.NumericUpDown();
            this.pointIndexBox = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.duplicateBut = new System.Windows.Forms.Button();
            this.fontBut = new System.Windows.Forms.Button();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.strokeWBox = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.rChBox = new System.Windows.Forms.CheckBox();
            this.gChBox = new System.Windows.Forms.CheckBox();
            this.bChBox = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.selectBoxSize = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointXBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointIndexBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.strokeWBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectBoxSize)).BeginInit();
            this.SuspendLayout();
            // 
            // roiView
            // 
            this.roiView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roiView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.roiView.ContextMenuStrip = this.contextMenuStrip;
            this.roiView.ForeColor = System.Drawing.Color.White;
            this.roiView.HideSelection = false;
            this.roiView.Location = new System.Drawing.Point(-1, 0);
            this.roiView.MultiSelect = false;
            this.roiView.Name = "roiView";
            this.roiView.Size = new System.Drawing.Size(237, 450);
            this.roiView.TabIndex = 0;
            this.roiView.UseCompatibleStateImageBehavior = false;
            this.roiView.View = System.Windows.Forms.View.List;
            this.roiView.SelectedIndexChanged += new System.EventHandler(this.roiView_SelectedIndexChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(108, 70);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(242, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stroke Color RGB";
            // 
            // rBox
            // 
            this.rBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.rBox.ForeColor = System.Drawing.Color.White;
            this.rBox.Location = new System.Drawing.Point(245, 155);
            this.rBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rBox.Name = "rBox";
            this.rBox.Size = new System.Drawing.Size(48, 20);
            this.rBox.TabIndex = 2;
            this.rBox.ValueChanged += new System.EventHandler(this.rBox_ValueChanged);
            // 
            // gBox
            // 
            this.gBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.gBox.ForeColor = System.Drawing.Color.White;
            this.gBox.Location = new System.Drawing.Point(299, 155);
            this.gBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.gBox.Name = "gBox";
            this.gBox.Size = new System.Drawing.Size(48, 20);
            this.gBox.TabIndex = 3;
            this.gBox.ValueChanged += new System.EventHandler(this.gBox_ValueChanged);
            // 
            // bBox
            // 
            this.bBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.bBox.ForeColor = System.Drawing.Color.White;
            this.bBox.Location = new System.Drawing.Point(353, 155);
            this.bBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.bBox.Name = "bBox";
            this.bBox.Size = new System.Drawing.Size(48, 20);
            this.bBox.TabIndex = 4;
            this.bBox.ValueChanged += new System.EventHandler(this.bBox_ValueChanged);
            // 
            // tBox
            // 
            this.tBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tBox.ForeColor = System.Drawing.Color.White;
            this.tBox.Location = new System.Drawing.Point(353, 115);
            this.tBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.tBox.Name = "tBox";
            this.tBox.Size = new System.Drawing.Size(48, 20);
            this.tBox.TabIndex = 8;
            this.tBox.ValueChanged += new System.EventHandler(this.tBox_ValueChanged);
            // 
            // cBox
            // 
            this.cBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cBox.ForeColor = System.Drawing.Color.White;
            this.cBox.Location = new System.Drawing.Point(299, 115);
            this.cBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.cBox.Name = "cBox";
            this.cBox.Size = new System.Drawing.Size(48, 20);
            this.cBox.TabIndex = 7;
            this.cBox.ValueChanged += new System.EventHandler(this.cBox_ValueChanged);
            // 
            // zBox
            // 
            this.zBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.zBox.ForeColor = System.Drawing.Color.White;
            this.zBox.Location = new System.Drawing.Point(245, 115);
            this.zBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.zBox.Name = "zBox";
            this.zBox.Size = new System.Drawing.Size(48, 20);
            this.zBox.TabIndex = 6;
            this.zBox.ValueChanged += new System.EventHandler(this.zBox_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(242, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ZCT";
            // 
            // yBox
            // 
            this.yBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.yBox.ForeColor = System.Drawing.Color.White;
            this.yBox.Location = new System.Drawing.Point(353, 48);
            this.yBox.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.yBox.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(67, 20);
            this.yBox.TabIndex = 10;
            this.yBox.ValueChanged += new System.EventHandler(this.yBox_ValueChanged);
            // 
            // xBox
            // 
            this.xBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.xBox.ForeColor = System.Drawing.Color.White;
            this.xBox.Location = new System.Drawing.Point(278, 48);
            this.xBox.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.xBox.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(67, 20);
            this.xBox.TabIndex = 9;
            this.xBox.ValueChanged += new System.EventHandler(this.xBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(245, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "X, Y:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(240, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "W, H:";
            // 
            // hBox
            // 
            this.hBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.hBox.ForeColor = System.Drawing.Color.White;
            this.hBox.Location = new System.Drawing.Point(353, 74);
            this.hBox.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.hBox.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.hBox.Name = "hBox";
            this.hBox.Size = new System.Drawing.Size(67, 20);
            this.hBox.TabIndex = 13;
            this.hBox.ValueChanged += new System.EventHandler(this.hBox_ValueChanged);
            // 
            // wBox
            // 
            this.wBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.wBox.ForeColor = System.Drawing.Color.White;
            this.wBox.Location = new System.Drawing.Point(278, 74);
            this.wBox.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.wBox.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.wBox.Name = "wBox";
            this.wBox.Size = new System.Drawing.Size(67, 20);
            this.wBox.TabIndex = 12;
            this.wBox.ValueChanged += new System.EventHandler(this.wBox_ValueChanged);
            // 
            // typeBox
            // 
            this.typeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.typeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.typeBox.Enabled = false;
            this.typeBox.ForeColor = System.Drawing.Color.White;
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Location = new System.Drawing.Point(281, 226);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(132, 21);
            this.typeBox.TabIndex = 15;
            this.typeBox.SelectedIndexChanged += new System.EventHandler(this.typeBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(245, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Type:";
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(282, 251);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(132, 20);
            this.textBox.TabIndex = 17;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(245, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Text:";
            // 
            // showBoundsBox
            // 
            this.showBoundsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showBoundsBox.AutoSize = true;
            this.showBoundsBox.Checked = true;
            this.showBoundsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBoundsBox.ForeColor = System.Drawing.Color.White;
            this.showBoundsBox.Location = new System.Drawing.Point(258, 340);
            this.showBoundsBox.Name = "showBoundsBox";
            this.showBoundsBox.Size = new System.Drawing.Size(92, 17);
            this.showBoundsBox.TabIndex = 19;
            this.showBoundsBox.Text = "Show Bounds";
            this.showBoundsBox.UseVisualStyleBackColor = true;
            this.showBoundsBox.CheckedChanged += new System.EventHandler(this.showBoundsBox_CheckedChanged);
            // 
            // showTextBox
            // 
            this.showTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showTextBox.AutoSize = true;
            this.showTextBox.Checked = true;
            this.showTextBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showTextBox.ForeColor = System.Drawing.Color.White;
            this.showTextBox.Location = new System.Drawing.Point(347, 340);
            this.showTextBox.Name = "showTextBox";
            this.showTextBox.Size = new System.Drawing.Size(77, 17);
            this.showTextBox.TabIndex = 20;
            this.showTextBox.Text = "Show Text";
            this.showTextBox.UseVisualStyleBackColor = true;
            this.showTextBox.CheckedChanged += new System.EventHandler(this.showTextBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(261, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Image Name";
            // 
            // imageNameLabel
            // 
            this.imageNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageNameLabel.AutoSize = true;
            this.imageNameLabel.ForeColor = System.Drawing.Color.White;
            this.imageNameLabel.Location = new System.Drawing.Point(261, 27);
            this.imageNameLabel.Name = "imageNameLabel";
            this.imageNameLabel.Size = new System.Drawing.Size(0, 13);
            this.imageNameLabel.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(245, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "ID:";
            // 
            // idBox
            // 
            this.idBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.idBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.idBox.ForeColor = System.Drawing.Color.White;
            this.idBox.Location = new System.Drawing.Point(282, 274);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(132, 20);
            this.idBox.TabIndex = 25;
            this.idBox.TextChanged += new System.EventHandler(this.idBox_TextChanged);
            // 
            // pointYBox
            // 
            this.pointYBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pointYBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.pointYBox.ForeColor = System.Drawing.Color.White;
            this.pointYBox.Location = new System.Drawing.Point(362, 314);
            this.pointYBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.pointYBox.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.pointYBox.Name = "pointYBox";
            this.pointYBox.Size = new System.Drawing.Size(62, 20);
            this.pointYBox.TabIndex = 32;
            this.pointYBox.ValueChanged += new System.EventHandler(this.pointYBox_ValueChanged);
            // 
            // pointXBox
            // 
            this.pointXBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pointXBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.pointXBox.ForeColor = System.Drawing.Color.White;
            this.pointXBox.Location = new System.Drawing.Point(299, 314);
            this.pointXBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.pointXBox.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.pointXBox.Name = "pointXBox";
            this.pointXBox.Size = new System.Drawing.Size(63, 20);
            this.pointXBox.TabIndex = 31;
            this.pointXBox.ValueChanged += new System.EventHandler(this.pointXBox_ValueChanged);
            // 
            // pointIndexBox
            // 
            this.pointIndexBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pointIndexBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.pointIndexBox.ForeColor = System.Drawing.Color.White;
            this.pointIndexBox.Location = new System.Drawing.Point(246, 314);
            this.pointIndexBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.pointIndexBox.Name = "pointIndexBox";
            this.pointIndexBox.Size = new System.Drawing.Size(48, 20);
            this.pointIndexBox.TabIndex = 33;
            this.pointIndexBox.ValueChanged += new System.EventHandler(this.pointIndexBox_ValueChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(246, 298);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Point";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(300, 298);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "X";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(365, 298);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Y";
            // 
            // duplicateBut
            // 
            this.duplicateBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.duplicateBut.Location = new System.Drawing.Point(259, 417);
            this.duplicateBut.Name = "duplicateBut";
            this.duplicateBut.Size = new System.Drawing.Size(75, 25);
            this.duplicateBut.TabIndex = 29;
            this.duplicateBut.Text = "Add";
            this.duplicateBut.UseVisualStyleBackColor = true;
            this.duplicateBut.Click += new System.EventHandler(this.addButton_Click);
            // 
            // fontBut
            // 
            this.fontBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fontBut.Location = new System.Drawing.Point(333, 196);
            this.fontBut.Name = "fontBut";
            this.fontBut.Size = new System.Drawing.Size(82, 20);
            this.fontBut.TabIndex = 37;
            this.fontBut.Text = "Set Font";
            this.fontBut.UseVisualStyleBackColor = true;
            this.fontBut.Click += new System.EventHandler(this.fontBut_Click);
            // 
            // strokeWBox
            // 
            this.strokeWBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.strokeWBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.strokeWBox.ForeColor = System.Drawing.Color.White;
            this.strokeWBox.Location = new System.Drawing.Point(245, 196);
            this.strokeWBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.strokeWBox.Name = "strokeWBox";
            this.strokeWBox.Size = new System.Drawing.Size(69, 20);
            this.strokeWBox.TabIndex = 38;
            this.strokeWBox.ValueChanged += new System.EventHandler(this.strokeWBox_ValueChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(244, 180);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Stroke Width";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(253, 360);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 13);
            this.label15.TabIndex = 42;
            this.label15.Text = "Show ROI\'s in RGB View";
            // 
            // rChBox
            // 
            this.rChBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rChBox.AutoSize = true;
            this.rChBox.Checked = true;
            this.rChBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rChBox.ForeColor = System.Drawing.Color.White;
            this.rChBox.Location = new System.Drawing.Point(258, 381);
            this.rChBox.Name = "rChBox";
            this.rChBox.Size = new System.Drawing.Size(52, 17);
            this.rChBox.TabIndex = 43;
            this.rChBox.Text = "R/C1";
            this.rChBox.UseVisualStyleBackColor = true;
            this.rChBox.CheckedChanged += new System.EventHandler(this.rChBox_CheckedChanged);
            // 
            // gChBox
            // 
            this.gChBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gChBox.AutoSize = true;
            this.gChBox.Checked = true;
            this.gChBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gChBox.ForeColor = System.Drawing.Color.White;
            this.gChBox.Location = new System.Drawing.Point(310, 381);
            this.gChBox.Name = "gChBox";
            this.gChBox.Size = new System.Drawing.Size(52, 17);
            this.gChBox.TabIndex = 44;
            this.gChBox.Text = "G/C2";
            this.gChBox.UseVisualStyleBackColor = true;
            this.gChBox.CheckedChanged += new System.EventHandler(this.gChBox_CheckedChanged);
            // 
            // bChBox
            // 
            this.bChBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bChBox.AutoSize = true;
            this.bChBox.Checked = true;
            this.bChBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bChBox.ForeColor = System.Drawing.Color.White;
            this.bChBox.Location = new System.Drawing.Point(361, 381);
            this.bChBox.Name = "bChBox";
            this.bChBox.Size = new System.Drawing.Size(51, 17);
            this.bChBox.TabIndex = 45;
            this.bChBox.Text = "B/C3";
            this.bChBox.UseVisualStyleBackColor = true;
            this.bChBox.CheckedChanged += new System.EventHandler(this.bChBox_CheckedChanged);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(341, 401);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 40;
            this.label14.Text = "Select Box Size";
            // 
            // selectBoxSize
            // 
            this.selectBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBoxSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.selectBoxSize.ForeColor = System.Drawing.Color.White;
            this.selectBoxSize.Location = new System.Drawing.Point(344, 420);
            this.selectBoxSize.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.selectBoxSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.selectBoxSize.Name = "selectBoxSize";
            this.selectBoxSize.Size = new System.Drawing.Size(48, 20);
            this.selectBoxSize.TabIndex = 41;
            this.selectBoxSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.selectBoxSize.ValueChanged += new System.EventHandler(this.selectBoxSize_ValueChanged);
            // 
            // ROIManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(425, 451);
            this.Controls.Add(this.bChBox);
            this.Controls.Add(this.gChBox);
            this.Controls.Add(this.rChBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.selectBoxSize);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.strokeWBox);
            this.Controls.Add(this.fontBut);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pointIndexBox);
            this.Controls.Add(this.pointYBox);
            this.Controls.Add(this.pointXBox);
            this.Controls.Add(this.duplicateBut);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.imageNameLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.showTextBox);
            this.Controls.Add(this.showBoundsBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hBox);
            this.Controls.Add(this.wBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.yBox);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.tBox);
            this.Controls.Add(this.cBox);
            this.Controls.Add(this.zBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bBox);
            this.Controls.Add(this.gBox);
            this.Controls.Add(this.rBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.roiView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ROIManager";
            this.Text = "ROI Manager";
            this.Activated += new System.EventHandler(this.ROIManager_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ROIManager_FormClosing);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointXBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointIndexBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.strokeWBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectBoxSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView roiView;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown rBox;
        private System.Windows.Forms.NumericUpDown gBox;
        private System.Windows.Forms.NumericUpDown bBox;
        private System.Windows.Forms.NumericUpDown tBox;
        private System.Windows.Forms.NumericUpDown cBox;
        private System.Windows.Forms.NumericUpDown zBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown yBox;
        private System.Windows.Forms.NumericUpDown xBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown hBox;
        private System.Windows.Forms.NumericUpDown wBox;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox showBoundsBox;
        private System.Windows.Forms.CheckBox showTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label imageNameLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.NumericUpDown pointYBox;
        private System.Windows.Forms.NumericUpDown pointXBox;
        private System.Windows.Forms.NumericUpDown pointIndexBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button duplicateBut;
        private System.Windows.Forms.Button fontBut;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.NumericUpDown strokeWBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox rChBox;
        private System.Windows.Forms.CheckBox gChBox;
        private System.Windows.Forms.CheckBox bChBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown selectBoxSize;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}