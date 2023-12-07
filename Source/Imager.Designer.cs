﻿
namespace BioImager
{
    partial class Imager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Imager));
            statusTimer = new System.Windows.Forms.Timer(components);
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label22 = new Label();
            RJoystickStageMoveAmountBox = new NumericUpDown();
            label24 = new Label();
            label15 = new Label();
            triggerFocusBox = new NumericUpDown();
            label14 = new Label();
            label11 = new Label();
            LJoystickStageMoveAmountBox = new NumericUpDown();
            label10 = new Label();
            label5 = new Label();
            label4 = new Label();
            deadzoneBox = new NumericUpDown();
            controllerJoystickUpdate = new System.Windows.Forms.Timer(components);
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            LJoystickLabel = new Label();
            RJoystickLabel = new Label();
            label9 = new Label();
            leftTriggerLabel = new Label();
            rightTriggerLabel = new Label();
            label12 = new Label();
            YPressedLabel = new Label();
            BPressedLabel = new Label();
            APressedLabel = new Label();
            XPressedLabel = new Label();
            label17 = new Label();
            DPadLeftLabel = new Label();
            DPadDownLabel = new Label();
            DPadRightLabel = new Label();
            DPadUpLabel = new Label();
            label13 = new Label();
            shoulderRightLabel = new Label();
            shoulderLeftLabel = new Label();
            BackPressedLabel = new Label();
            StartPressedLabel = new Label();
            storedCoordsBox = new ListBox();
            contextMenuStrip2 = new ContextMenuStrip(components);
            goToToolStripMenuItem1 = new ToolStripMenuItem();
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            clearToolStripMenuItem1 = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            snapContextMenuStrip = new ContextMenuStrip(components);
            goToToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem1 = new ToolStripMenuItem();
            openFileToolStripMenuItem = new ToolStripMenuItem();
            openInImageViewToolStripMenuItem = new ToolStripMenuItem();
            label20 = new Label();
            addCoordBut = new Button();
            label21 = new Label();
            lastCoordDistanceLabel = new Label();
            lastSelectedCoordsLabel = new Label();
            label23 = new Label();
            clearBut = new Button();
            label25 = new Label();
            l3PressedLabel = new Label();
            r3PressedLabel = new Label();
            lastSelectedSnapDistanceLabel = new Label();
            label27 = new Label();
            lastSnapDistanceLabel = new Label();
            label29 = new Label();
            storedImageBox = new ListBox();
            controllerButtonUpdate = new System.Windows.Forms.Timer(components);
            topMostCheckBox = new CheckBox();
            mainTabControl = new TabControl();
            ImagingTab = new TabPage();
            dockToApp = new CheckBox();
            label26 = new Label();
            goToLocBox = new Button();
            label35 = new Label();
            zCoordBox = new NumericUpDown();
            label34 = new Label();
            yCoordBox = new NumericUpDown();
            label32 = new Label();
            xCoordBox = new NumericUpDown();
            splitContainer = new SplitContainer();
            clearImagesBut = new Button();
            SettingsTab = new TabPage();
            label36 = new Label();
            label3 = new Label();
            slicesPerSlideBox = new NumericUpDown();
            label30 = new Label();
            sliceIncrementBox = new NumericUpDown();
            label31 = new Label();
            imagesPerSliceBox = new NumericUpDown();
            openProfileBut = new Button();
            saveProfileBut = new Button();
            label1 = new Label();
            profilesBox = new ComboBox();
            tabContextMenu = new ContextMenuStrip(components);
            undockToWindowToolStripMenuItem = new ToolStripMenuItem();
            openProfileFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            openSnapsFileDialog = new OpenFileDialog();
            openExeFileDialog = new OpenFileDialog();
            mainWinContextMenuStrip = new ContextMenuStrip(components);
            hideToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            borderToolStripMenuItem = new ToolStripMenuItem();
            toolTip = new ToolTip(components);
            openImagesDialog = new OpenFileDialog();
            saveImageDialog = new SaveFileDialog();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RJoystickStageMoveAmountBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)triggerFocusBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LJoystickStageMoveAmountBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)deadzoneBox).BeginInit();
            contextMenuStrip2.SuspendLayout();
            snapContextMenuStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            ImagingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)zCoordBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)yCoordBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xCoordBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SettingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)slicesPerSlideBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliceIncrementBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imagesPerSliceBox).BeginInit();
            tabContextMenu.SuspendLayout();
            mainWinContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusTimer
            // 
            statusTimer.Interval = 1000;
            statusTimer.Tick += statusTimer_Tick;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(49, 91, 138);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, toolStripStatusLabel2, toolStripStatusLabel1 });
            statusStrip.Location = new Point(0, 559);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(335, 22);
            statusStrip.TabIndex = 7;
            statusStrip.Text = "statusStrip1";
            statusStrip.Click += ImagingTab_Click;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(253, 447);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(25, 15);
            label22.TabIndex = 28;
            label22.Text = "μm";
            // 
            // RJoystickStageMoveAmountBox
            // 
            RJoystickStageMoveAmountBox.BackColor = Color.FromArgb(49, 91, 138);
            RJoystickStageMoveAmountBox.DecimalPlaces = 5;
            RJoystickStageMoveAmountBox.ForeColor = Color.White;
            RJoystickStageMoveAmountBox.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            RJoystickStageMoveAmountBox.InterceptArrowKeys = false;
            RJoystickStageMoveAmountBox.Location = new Point(149, 444);
            RJoystickStageMoveAmountBox.Margin = new Padding(4, 3, 4, 3);
            RJoystickStageMoveAmountBox.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            RJoystickStageMoveAmountBox.Name = "RJoystickStageMoveAmountBox";
            RJoystickStageMoveAmountBox.Size = new Size(97, 23);
            RJoystickStageMoveAmountBox.TabIndex = 27;
            toolTip.SetToolTip(RJoystickStageMoveAmountBox, "This will override the move amount set in objectives. If objective is changed this will revert to the value set for the objective.");
            RJoystickStageMoveAmountBox.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(146, 422);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(79, 15);
            label24.TabIndex = 26;
            label24.Text = "Right Joystick";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(107, 492);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(25, 15);
            label15.TabIndex = 21;
            label15.Text = "μm";
            // 
            // triggerFocusBox
            // 
            triggerFocusBox.BackColor = Color.FromArgb(49, 91, 138);
            triggerFocusBox.DecimalPlaces = 5;
            triggerFocusBox.ForeColor = Color.White;
            triggerFocusBox.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            triggerFocusBox.InterceptArrowKeys = false;
            triggerFocusBox.Location = new Point(7, 488);
            triggerFocusBox.Margin = new Padding(4, 3, 4, 3);
            triggerFocusBox.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            triggerFocusBox.Name = "triggerFocusBox";
            triggerFocusBox.Size = new Size(97, 23);
            triggerFocusBox.TabIndex = 20;
            toolTip.SetToolTip(triggerFocusBox, "This will override the move amount set in objectives. If objective is changed this will revert to the value set for the objective.");
            triggerFocusBox.Value = new decimal(new int[] { 1, 0, 0, 196608 });
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(4, 471);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(77, 15);
            label14.TabIndex = 19;
            label14.Text = "Trigger Focus";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(107, 447);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(25, 15);
            label11.TabIndex = 18;
            label11.Text = "μm";
            // 
            // LJoystickStageMoveAmountBox
            // 
            LJoystickStageMoveAmountBox.BackColor = Color.FromArgb(49, 91, 138);
            LJoystickStageMoveAmountBox.DecimalPlaces = 5;
            LJoystickStageMoveAmountBox.ForeColor = Color.White;
            LJoystickStageMoveAmountBox.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            LJoystickStageMoveAmountBox.InterceptArrowKeys = false;
            LJoystickStageMoveAmountBox.Location = new Point(7, 444);
            LJoystickStageMoveAmountBox.Margin = new Padding(4, 3, 4, 3);
            LJoystickStageMoveAmountBox.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            LJoystickStageMoveAmountBox.Name = "LJoystickStageMoveAmountBox";
            LJoystickStageMoveAmountBox.Size = new Size(97, 23);
            LJoystickStageMoveAmountBox.TabIndex = 17;
            toolTip.SetToolTip(LJoystickStageMoveAmountBox, "This will override the move amount set in objectives. If objective is changed this will revert to the value set for the objective.");
            LJoystickStageMoveAmountBox.Value = new decimal(new int[] { 40, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(4, 422);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(71, 15);
            label10.TabIndex = 16;
            label10.Text = "Left Joystick";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(-1, 333);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(147, 471);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 14;
            label4.Text = "Deadzone";
            // 
            // deadzoneBox
            // 
            deadzoneBox.BackColor = Color.FromArgb(49, 91, 138);
            deadzoneBox.ForeColor = Color.White;
            deadzoneBox.InterceptArrowKeys = false;
            deadzoneBox.Location = new Point(150, 489);
            deadzoneBox.Margin = new Padding(4, 3, 4, 3);
            deadzoneBox.Name = "deadzoneBox";
            deadzoneBox.Size = new Size(97, 23);
            deadzoneBox.TabIndex = 13;
            deadzoneBox.Value = new decimal(new int[] { 5, 0, 0, 0 });
            deadzoneBox.ValueChanged += deadzoneBox_ValueChanged;
            // 
            // controllerJoystickUpdate
            // 
            controllerJoystickUpdate.Tick += controllerJoystickUpdate_Tick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(2, 31);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(92, 13);
            label6.TabIndex = 10;
            label6.Text = "Left Joystick %";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(119, 31);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(100, 13);
            label7.TabIndex = 11;
            label7.Text = "Right Joystick %";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(118, 182);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(94, 13);
            label8.TabIndex = 12;
            label8.Text = "Right Trigger %";
            // 
            // LJoystickLabel
            // 
            LJoystickLabel.AutoSize = true;
            LJoystickLabel.Location = new Point(2, 57);
            LJoystickLabel.Margin = new Padding(4, 0, 4, 0);
            LJoystickLabel.Name = "LJoystickLabel";
            LJoystickLabel.Size = new Size(24, 15);
            LJoystickLabel.TabIndex = 13;
            LJoystickLabel.Text = "X,Y";
            // 
            // RJoystickLabel
            // 
            RJoystickLabel.AutoSize = true;
            RJoystickLabel.Location = new Point(119, 57);
            RJoystickLabel.Margin = new Padding(4, 0, 4, 0);
            RJoystickLabel.Name = "RJoystickLabel";
            RJoystickLabel.Size = new Size(24, 15);
            RJoystickLabel.TabIndex = 14;
            RJoystickLabel.Text = "X,Y";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(2, 182);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(86, 13);
            label9.TabIndex = 15;
            label9.Text = "Left Trigger %";
            // 
            // leftTriggerLabel
            // 
            leftTriggerLabel.AutoSize = true;
            leftTriggerLabel.Location = new Point(8, 207);
            leftTriggerLabel.Margin = new Padding(4, 0, 4, 0);
            leftTriggerLabel.Name = "leftTriggerLabel";
            leftTriggerLabel.Size = new Size(0, 15);
            leftTriggerLabel.TabIndex = 16;
            // 
            // rightTriggerLabel
            // 
            rightTriggerLabel.AutoSize = true;
            rightTriggerLabel.Location = new Point(126, 208);
            rightTriggerLabel.Margin = new Padding(4, 0, 4, 0);
            rightTriggerLabel.Name = "rightTriggerLabel";
            rightTriggerLabel.Size = new Size(0, 15);
            rightTriggerLabel.TabIndex = 17;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(2, 80);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(50, 13);
            label12.TabIndex = 18;
            label12.Text = "Buttons";
            // 
            // YPressedLabel
            // 
            YPressedLabel.AutoSize = true;
            YPressedLabel.Location = new Point(2, 100);
            YPressedLabel.Margin = new Padding(4, 0, 4, 0);
            YPressedLabel.Name = "YPressedLabel";
            YPressedLabel.Size = new Size(81, 15);
            YPressedLabel.TabIndex = 19;
            YPressedLabel.Text = "Y: not pressed";
            toolTip.SetToolTip(YPressedLabel, "Double click to change button function.");
            YPressedLabel.MouseDoubleClick += YPressedLabel_MouseDoubleClick;
            // 
            // BPressedLabel
            // 
            BPressedLabel.AutoSize = true;
            BPressedLabel.Location = new Point(2, 119);
            BPressedLabel.Margin = new Padding(4, 0, 4, 0);
            BPressedLabel.Name = "BPressedLabel";
            BPressedLabel.Size = new Size(81, 15);
            BPressedLabel.TabIndex = 20;
            BPressedLabel.Text = "B: not pressed";
            toolTip.SetToolTip(BPressedLabel, "Double click to change button function.");
            BPressedLabel.MouseDoubleClick += BPressedLabel_MouseDoubleClick;
            // 
            // APressedLabel
            // 
            APressedLabel.AutoSize = true;
            APressedLabel.Location = new Point(2, 137);
            APressedLabel.Margin = new Padding(4, 0, 4, 0);
            APressedLabel.Name = "APressedLabel";
            APressedLabel.Size = new Size(82, 15);
            APressedLabel.TabIndex = 21;
            APressedLabel.Text = "A: not pressed";
            toolTip.SetToolTip(APressedLabel, "Double click to change button function.");
            APressedLabel.MouseDoubleClick += APressedLabel_MouseDoubleClick;
            // 
            // XPressedLabel
            // 
            XPressedLabel.AutoSize = true;
            XPressedLabel.Location = new Point(2, 156);
            XPressedLabel.Margin = new Padding(4, 0, 4, 0);
            XPressedLabel.Name = "XPressedLabel";
            XPressedLabel.Size = new Size(81, 15);
            XPressedLabel.TabIndex = 22;
            XPressedLabel.Text = "X: not pressed";
            toolTip.SetToolTip(XPressedLabel, "Double click to change button function.");
            XPressedLabel.MouseDoubleClick += XPressedLabel_MouseDoubleClick;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label17.Location = new Point(118, 77);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(38, 13);
            label17.TabIndex = 23;
            label17.Text = "DPad";
            // 
            // DPadLeftLabel
            // 
            DPadLeftLabel.AutoSize = true;
            DPadLeftLabel.Location = new Point(117, 156);
            DPadLeftLabel.Margin = new Padding(4, 0, 4, 0);
            DPadLeftLabel.Name = "DPadLeftLabel";
            DPadLeftLabel.Size = new Size(94, 15);
            DPadLeftLabel.TabIndex = 27;
            DPadLeftLabel.Text = "Left: not pressed";
            toolTip.SetToolTip(DPadLeftLabel, "Double click to change button function.");
            DPadLeftLabel.MouseDoubleClick += DPadLeftLabel_MouseDoubleClick;
            // 
            // DPadDownLabel
            // 
            DPadDownLabel.AutoSize = true;
            DPadDownLabel.Location = new Point(117, 137);
            DPadDownLabel.Margin = new Padding(4, 0, 4, 0);
            DPadDownLabel.Name = "DPadDownLabel";
            DPadDownLabel.Size = new Size(105, 15);
            DPadDownLabel.TabIndex = 26;
            DPadDownLabel.Text = "Down: not pressed";
            toolTip.SetToolTip(DPadDownLabel, "Double click to change button function.");
            DPadDownLabel.MouseDoubleClick += DPadDownLabel_MouseDoubleClick;
            // 
            // DPadRightLabel
            // 
            DPadRightLabel.AutoSize = true;
            DPadRightLabel.Location = new Point(117, 119);
            DPadRightLabel.Margin = new Padding(4, 0, 4, 0);
            DPadRightLabel.Name = "DPadRightLabel";
            DPadRightLabel.Size = new Size(102, 15);
            DPadRightLabel.TabIndex = 25;
            DPadRightLabel.Text = "Right: not pressed";
            toolTip.SetToolTip(DPadRightLabel, "Double click to change button function.");
            DPadRightLabel.MouseDoubleClick += DPadRightLabel_MouseDoubleClick;
            // 
            // DPadUpLabel
            // 
            DPadUpLabel.AutoSize = true;
            DPadUpLabel.Location = new Point(117, 100);
            DPadUpLabel.Margin = new Padding(4, 0, 4, 0);
            DPadUpLabel.Name = "DPadUpLabel";
            DPadUpLabel.Size = new Size(89, 15);
            DPadUpLabel.TabIndex = 24;
            DPadUpLabel.Text = "Up: not pressed";
            toolTip.SetToolTip(DPadUpLabel, "Double click to change button function.");
            DPadUpLabel.MouseDoubleClick += DPadUpLabel_MouseDoubleClick;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(6, 228);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(63, 13);
            label13.TabIndex = 28;
            label13.Text = "Shoulders";
            // 
            // shoulderRightLabel
            // 
            shoulderRightLabel.AutoSize = true;
            shoulderRightLabel.Location = new Point(6, 270);
            shoulderRightLabel.Margin = new Padding(4, 0, 4, 0);
            shoulderRightLabel.Name = "shoulderRightLabel";
            shoulderRightLabel.Size = new Size(102, 15);
            shoulderRightLabel.TabIndex = 30;
            shoulderRightLabel.Text = "Right: not pressed";
            toolTip.SetToolTip(shoulderRightLabel, "Double click to change button function.");
            shoulderRightLabel.MouseDoubleClick += shoulderRightLabel_MouseDoubleClick;
            // 
            // shoulderLeftLabel
            // 
            shoulderLeftLabel.AutoSize = true;
            shoulderLeftLabel.Location = new Point(6, 252);
            shoulderLeftLabel.Margin = new Padding(4, 0, 4, 0);
            shoulderLeftLabel.Name = "shoulderLeftLabel";
            shoulderLeftLabel.Size = new Size(94, 15);
            shoulderLeftLabel.TabIndex = 29;
            shoulderLeftLabel.Text = "Left: not pressed";
            toolTip.SetToolTip(shoulderLeftLabel, "Double click to change button function.");
            shoulderLeftLabel.MouseDoubleClick += shoulderLeftLabel_MouseDoubleClick;
            // 
            // BackPressedLabel
            // 
            BackPressedLabel.AutoSize = true;
            BackPressedLabel.Location = new Point(120, 231);
            BackPressedLabel.Margin = new Padding(4, 0, 4, 0);
            BackPressedLabel.Name = "BackPressedLabel";
            BackPressedLabel.Size = new Size(99, 15);
            BackPressedLabel.TabIndex = 31;
            BackPressedLabel.Text = "Back: not pressed";
            toolTip.SetToolTip(BackPressedLabel, "Double click to change button function.");
            BackPressedLabel.MouseDoubleClick += BackPressedLabel_MouseDoubleClick;
            // 
            // StartPressedLabel
            // 
            StartPressedLabel.AutoSize = true;
            StartPressedLabel.Location = new Point(120, 248);
            StartPressedLabel.Margin = new Padding(4, 0, 4, 0);
            StartPressedLabel.Name = "StartPressedLabel";
            StartPressedLabel.Size = new Size(98, 15);
            StartPressedLabel.TabIndex = 32;
            StartPressedLabel.Text = "Start: not pressed";
            toolTip.SetToolTip(StartPressedLabel, "Double click to change button function.");
            StartPressedLabel.MouseDoubleClick += StartPressedLabel_MouseDoubleClick;
            // 
            // storedCoordsBox
            // 
            storedCoordsBox.BackColor = Color.FromArgb(49, 91, 138);
            storedCoordsBox.ContextMenuStrip = contextMenuStrip2;
            storedCoordsBox.Dock = DockStyle.Fill;
            storedCoordsBox.ForeColor = Color.White;
            storedCoordsBox.FormattingEnabled = true;
            storedCoordsBox.ItemHeight = 15;
            storedCoordsBox.Location = new Point(0, 0);
            storedCoordsBox.Margin = new Padding(4, 3, 4, 3);
            storedCoordsBox.Name = "storedCoordsBox";
            storedCoordsBox.Size = new Size(151, 272);
            storedCoordsBox.TabIndex = 33;
            storedCoordsBox.SelectedIndexChanged += storedCoordsBox_SelectedIndexChanged;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { goToToolStripMenuItem1, deleteToolStripMenuItem1, clearToolStripMenuItem1, copyToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(108, 92);
            // 
            // goToToolStripMenuItem1
            // 
            goToToolStripMenuItem1.Name = "goToToolStripMenuItem1";
            goToToolStripMenuItem1.Size = new Size(107, 22);
            goToToolStripMenuItem1.Text = "Go To";
            goToToolStripMenuItem1.Click += goToToolStripMenuItem1_Click;
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(107, 22);
            deleteToolStripMenuItem1.Text = "Delete";
            deleteToolStripMenuItem1.Click += deleteToolStripMenuItem1_Click;
            // 
            // clearToolStripMenuItem1
            // 
            clearToolStripMenuItem1.Name = "clearToolStripMenuItem1";
            clearToolStripMenuItem1.Size = new Size(107, 22);
            clearToolStripMenuItem1.Text = "Clear";
            clearToolStripMenuItem1.Click += clearToolStripMenuItem1_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(107, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // snapContextMenuStrip
            // 
            snapContextMenuStrip.Items.AddRange(new ToolStripItem[] { goToToolStripMenuItem, deleteToolStripMenuItem, clearToolStripMenuItem, copyToolStripMenuItem1, openFileToolStripMenuItem, openInImageViewToolStripMenuItem });
            snapContextMenuStrip.Name = "contextMenuStrip1";
            snapContextMenuStrip.Size = new Size(243, 136);
            // 
            // goToToolStripMenuItem
            // 
            goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            goToToolStripMenuItem.Size = new Size(242, 22);
            goToToolStripMenuItem.Text = "Go To";
            goToToolStripMenuItem.Click += goToToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(242, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(242, 22);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem1
            // 
            copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            copyToolStripMenuItem1.Size = new Size(242, 22);
            copyToolStripMenuItem1.Text = "Copy";
            copyToolStripMenuItem1.Click += copyToolStripMenuItem1_Click;
            // 
            // openFileToolStripMenuItem
            // 
            openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            openFileToolStripMenuItem.Size = new Size(242, 22);
            openFileToolStripMenuItem.Text = "Open Image in Default Program";
            openFileToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
            // 
            // openInImageViewToolStripMenuItem
            // 
            openInImageViewToolStripMenuItem.Name = "openInImageViewToolStripMenuItem";
            openInImageViewToolStripMenuItem.Size = new Size(242, 22);
            openInImageViewToolStripMenuItem.Text = "Open in Image View";
            openInImageViewToolStripMenuItem.Click += openInImageViewToolStripMenuItem_Click;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.ForeColor = Color.White;
            label20.Location = new Point(5, 164);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(108, 15);
            label20.TabIndex = 34;
            label20.Text = "Stored Coordinates";
            // 
            // addCoordBut
            // 
            addCoordBut.BackColor = Color.FromArgb(49, 91, 138);
            addCoordBut.ForeColor = Color.White;
            addCoordBut.Location = new Point(6, 182);
            addCoordBut.Margin = new Padding(4, 3, 4, 3);
            addCoordBut.Name = "addCoordBut";
            addCoordBut.Size = new Size(56, 27);
            addCoordBut.TabIndex = 35;
            addCoordBut.Text = "Add";
            addCoordBut.UseVisualStyleBackColor = false;
            addCoordBut.Click += addCoordBut_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.ForeColor = Color.White;
            label21.Location = new Point(7, 52);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(201, 15);
            label21.TabIndex = 36;
            label21.Text = "Distance from last stored coordinate.";
            // 
            // lastCoordDistanceLabel
            // 
            lastCoordDistanceLabel.AutoSize = true;
            lastCoordDistanceLabel.ForeColor = Color.White;
            lastCoordDistanceLabel.Location = new Point(15, 76);
            lastCoordDistanceLabel.Margin = new Padding(4, 0, 4, 0);
            lastCoordDistanceLabel.Name = "lastCoordDistanceLabel";
            lastCoordDistanceLabel.Size = new Size(0, 15);
            lastCoordDistanceLabel.TabIndex = 37;
            // 
            // lastSelectedCoordsLabel
            // 
            lastSelectedCoordsLabel.AutoSize = true;
            lastSelectedCoordsLabel.ForeColor = Color.White;
            lastSelectedCoordsLabel.Location = new Point(15, 31);
            lastSelectedCoordsLabel.Margin = new Padding(4, 0, 4, 0);
            lastSelectedCoordsLabel.Name = "lastSelectedCoordsLabel";
            lastSelectedCoordsLabel.Size = new Size(0, 15);
            lastSelectedCoordsLabel.TabIndex = 39;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.ForeColor = Color.White;
            label23.Location = new Point(6, 7);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(190, 15);
            label23.TabIndex = 38;
            label23.Text = "Distance from selected coordinate.";
            // 
            // clearBut
            // 
            clearBut.BackColor = Color.FromArgb(49, 91, 138);
            clearBut.ForeColor = Color.White;
            clearBut.Location = new Point(62, 182);
            clearBut.Margin = new Padding(4, 3, 4, 3);
            clearBut.Name = "clearBut";
            clearBut.Size = new Size(56, 27);
            clearBut.TabIndex = 40;
            clearBut.Text = "Clear";
            clearBut.UseVisualStyleBackColor = false;
            clearBut.Click += clearBut_Click;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label25.Location = new Point(119, 270);
            label25.Margin = new Padding(4, 0, 4, 0);
            label25.Name = "label25";
            label25.Size = new Size(66, 13);
            label25.TabIndex = 41;
            label25.Text = "R3 and L3";
            // 
            // l3PressedLabel
            // 
            l3PressedLabel.AutoSize = true;
            l3PressedLabel.Location = new Point(120, 309);
            l3PressedLabel.Margin = new Padding(4, 0, 4, 0);
            l3PressedLabel.Name = "l3PressedLabel";
            l3PressedLabel.Size = new Size(86, 15);
            l3PressedLabel.TabIndex = 43;
            l3PressedLabel.Text = "L3: not pressed";
            toolTip.SetToolTip(l3PressedLabel, "Double click to change button function.");
            l3PressedLabel.MouseDoubleClick += l3PressedLabel_MouseDoubleClick;
            // 
            // r3PressedLabel
            // 
            r3PressedLabel.AutoSize = true;
            r3PressedLabel.Location = new Point(120, 291);
            r3PressedLabel.Margin = new Padding(4, 0, 4, 0);
            r3PressedLabel.Name = "r3PressedLabel";
            r3PressedLabel.Size = new Size(87, 15);
            r3PressedLabel.TabIndex = 42;
            r3PressedLabel.Text = "R3: not pressed";
            toolTip.SetToolTip(r3PressedLabel, "Double click to change button function.");
            r3PressedLabel.MouseDoubleClick += r3PressedLabel_MouseDoubleClick;
            // 
            // lastSelectedSnapDistanceLabel
            // 
            lastSelectedSnapDistanceLabel.AutoSize = true;
            lastSelectedSnapDistanceLabel.ForeColor = Color.White;
            lastSelectedSnapDistanceLabel.Location = new Point(15, 115);
            lastSelectedSnapDistanceLabel.Margin = new Padding(4, 0, 4, 0);
            lastSelectedSnapDistanceLabel.Name = "lastSelectedSnapDistanceLabel";
            lastSelectedSnapDistanceLabel.Size = new Size(0, 15);
            lastSelectedSnapDistanceLabel.TabIndex = 49;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.ForeColor = Color.White;
            label27.Location = new Point(7, 91);
            label27.Margin = new Padding(4, 0, 4, 0);
            label27.Name = "label27";
            label27.Size = new Size(166, 15);
            label27.TabIndex = 48;
            label27.Text = "Distance from selected image.";
            // 
            // lastSnapDistanceLabel
            // 
            lastSnapDistanceLabel.AutoSize = true;
            lastSnapDistanceLabel.ForeColor = Color.White;
            lastSnapDistanceLabel.Location = new Point(229, 140);
            lastSnapDistanceLabel.Margin = new Padding(4, 0, 4, 0);
            lastSnapDistanceLabel.Name = "lastSnapDistanceLabel";
            lastSnapDistanceLabel.Size = new Size(0, 15);
            lastSnapDistanceLabel.TabIndex = 47;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.ForeColor = Color.White;
            label29.Location = new Point(7, 136);
            label29.Margin = new Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.Size = new Size(177, 15);
            label29.TabIndex = 46;
            label29.Text = "Distance from last stored image.";
            // 
            // storedImageBox
            // 
            storedImageBox.BackColor = Color.FromArgb(49, 91, 138);
            storedImageBox.ContextMenuStrip = snapContextMenuStrip;
            storedImageBox.Dock = DockStyle.Fill;
            storedImageBox.ForeColor = Color.White;
            storedImageBox.FormattingEnabled = true;
            storedImageBox.ItemHeight = 15;
            storedImageBox.Location = new Point(0, 0);
            storedImageBox.Margin = new Padding(4, 3, 4, 3);
            storedImageBox.Name = "storedImageBox";
            storedImageBox.Size = new Size(160, 272);
            storedImageBox.TabIndex = 45;
            storedImageBox.SelectedIndexChanged += storedSnapBox_SelectedIndexChanged;
            // 
            // controllerButtonUpdate
            // 
            controllerButtonUpdate.Interval = 250;
            controllerButtonUpdate.Tick += controllerButtonUpdate_Tick;
            // 
            // topMostCheckBox
            // 
            topMostCheckBox.AutoSize = true;
            topMostCheckBox.Checked = true;
            topMostCheckBox.CheckState = CheckState.Checked;
            topMostCheckBox.ForeColor = Color.White;
            topMostCheckBox.Location = new Point(217, 7);
            topMostCheckBox.Margin = new Padding(4, 3, 4, 3);
            topMostCheckBox.Name = "topMostCheckBox";
            topMostCheckBox.Size = new Size(75, 19);
            topMostCheckBox.TabIndex = 60;
            topMostCheckBox.Text = "Top Most";
            topMostCheckBox.UseVisualStyleBackColor = true;
            topMostCheckBox.CheckedChanged += topMostCheckBox_CheckedChanged;
            // 
            // mainTabControl
            // 
            mainTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTabControl.Controls.Add(ImagingTab);
            mainTabControl.Controls.Add(SettingsTab);
            mainTabControl.HotTrack = true;
            mainTabControl.Location = new Point(4, 2);
            mainTabControl.Margin = new Padding(4, 3, 4, 3);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(331, 550);
            mainTabControl.TabIndex = 62;
            // 
            // ImagingTab
            // 
            ImagingTab.BackColor = Color.FromArgb(95, 122, 156);
            ImagingTab.Controls.Add(dockToApp);
            ImagingTab.Controls.Add(label26);
            ImagingTab.Controls.Add(goToLocBox);
            ImagingTab.Controls.Add(label35);
            ImagingTab.Controls.Add(zCoordBox);
            ImagingTab.Controls.Add(label34);
            ImagingTab.Controls.Add(yCoordBox);
            ImagingTab.Controls.Add(label32);
            ImagingTab.Controls.Add(xCoordBox);
            ImagingTab.Controls.Add(splitContainer);
            ImagingTab.Controls.Add(clearImagesBut);
            ImagingTab.Controls.Add(label20);
            ImagingTab.Controls.Add(addCoordBut);
            ImagingTab.Controls.Add(topMostCheckBox);
            ImagingTab.Controls.Add(label21);
            ImagingTab.Controls.Add(lastSelectedSnapDistanceLabel);
            ImagingTab.Controls.Add(lastCoordDistanceLabel);
            ImagingTab.Controls.Add(label27);
            ImagingTab.Controls.Add(label23);
            ImagingTab.Controls.Add(lastSnapDistanceLabel);
            ImagingTab.Controls.Add(lastSelectedCoordsLabel);
            ImagingTab.Controls.Add(label29);
            ImagingTab.Controls.Add(clearBut);
            ImagingTab.Location = new Point(4, 24);
            ImagingTab.Margin = new Padding(4, 3, 4, 3);
            ImagingTab.Name = "ImagingTab";
            ImagingTab.Padding = new Padding(4, 3, 4, 3);
            ImagingTab.Size = new Size(323, 522);
            ImagingTab.TabIndex = 0;
            ImagingTab.Text = "Imaging";
            ImagingTab.Click += ImagingTab_Click;
            // 
            // dockToApp
            // 
            dockToApp.AutoSize = true;
            dockToApp.Checked = true;
            dockToApp.CheckState = CheckState.Checked;
            dockToApp.Location = new Point(217, 33);
            dockToApp.Margin = new Padding(4, 3, 4, 3);
            dockToApp.Name = "dockToApp";
            dockToApp.Size = new Size(93, 19);
            dockToApp.TabIndex = 71;
            dockToApp.Text = "Dock To App";
            dockToApp.UseVisualStyleBackColor = true;
            dockToApp.CheckedChanged += dockToApp_CheckedChanged;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.ForeColor = Color.White;
            label26.Location = new Point(156, 164);
            label26.Margin = new Padding(4, 0, 4, 0);
            label26.Name = "label26";
            label26.Size = new Size(82, 15);
            label26.TabIndex = 71;
            label26.Text = "Stored Images";
            // 
            // goToLocBox
            // 
            goToLocBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            goToLocBox.BackColor = Color.FromArgb(49, 91, 138);
            goToLocBox.ForeColor = Color.White;
            goToLocBox.Location = new Point(284, 493);
            goToLocBox.Margin = new Padding(4, 3, 4, 3);
            goToLocBox.Name = "goToLocBox";
            goToLocBox.Size = new Size(38, 27);
            goToLocBox.TabIndex = 70;
            goToLocBox.Text = "Go";
            goToLocBox.UseVisualStyleBackColor = false;
            goToLocBox.Click += goToLocBox_Click;
            // 
            // label35
            // 
            label35.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label35.AutoSize = true;
            label35.ForeColor = Color.White;
            label35.Location = new Point(191, 499);
            label35.Margin = new Padding(4, 0, 4, 0);
            label35.Name = "label35";
            label35.Size = new Size(14, 15);
            label35.TabIndex = 69;
            label35.Text = "Z";
            // 
            // zCoordBox
            // 
            zCoordBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            zCoordBox.BackColor = Color.FromArgb(49, 91, 138);
            zCoordBox.DecimalPlaces = 5;
            zCoordBox.ForeColor = Color.White;
            zCoordBox.InterceptArrowKeys = false;
            zCoordBox.Location = new Point(208, 495);
            zCoordBox.Margin = new Padding(4, 3, 4, 3);
            zCoordBox.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            zCoordBox.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            zCoordBox.Name = "zCoordBox";
            zCoordBox.Size = new Size(75, 23);
            zCoordBox.TabIndex = 68;
            // 
            // label34
            // 
            label34.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label34.AutoSize = true;
            label34.ForeColor = Color.White;
            label34.Location = new Point(97, 499);
            label34.Margin = new Padding(4, 0, 4, 0);
            label34.Name = "label34";
            label34.Size = new Size(14, 15);
            label34.TabIndex = 67;
            label34.Text = "Y";
            // 
            // yCoordBox
            // 
            yCoordBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            yCoordBox.BackColor = Color.FromArgb(49, 91, 138);
            yCoordBox.DecimalPlaces = 5;
            yCoordBox.ForeColor = Color.White;
            yCoordBox.InterceptArrowKeys = false;
            yCoordBox.Location = new Point(113, 495);
            yCoordBox.Margin = new Padding(4, 3, 4, 3);
            yCoordBox.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            yCoordBox.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            yCoordBox.Name = "yCoordBox";
            yCoordBox.Size = new Size(75, 23);
            yCoordBox.TabIndex = 66;
            // 
            // label32
            // 
            label32.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label32.AutoSize = true;
            label32.ForeColor = Color.White;
            label32.Location = new Point(2, 499);
            label32.Margin = new Padding(4, 0, 4, 0);
            label32.Name = "label32";
            label32.Size = new Size(14, 15);
            label32.TabIndex = 65;
            label32.Text = "X";
            // 
            // xCoordBox
            // 
            xCoordBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            xCoordBox.BackColor = Color.FromArgb(49, 91, 138);
            xCoordBox.DecimalPlaces = 5;
            xCoordBox.ForeColor = Color.White;
            xCoordBox.InterceptArrowKeys = false;
            xCoordBox.Location = new Point(20, 495);
            xCoordBox.Margin = new Padding(4, 3, 4, 3);
            xCoordBox.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            xCoordBox.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            xCoordBox.Name = "xCoordBox";
            xCoordBox.Size = new Size(75, 23);
            xCoordBox.TabIndex = 64;
            // 
            // splitContainer
            // 
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer.Location = new Point(3, 215);
            splitContainer.Margin = new Padding(4, 3, 4, 3);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(storedCoordsBox);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(storedImageBox);
            splitContainer.Size = new Size(316, 272);
            splitContainer.SplitterDistance = 151;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 63;
            // 
            // clearImagesBut
            // 
            clearImagesBut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            clearImagesBut.BackColor = Color.FromArgb(49, 91, 138);
            clearImagesBut.ForeColor = Color.White;
            clearImagesBut.Location = new Point(160, 182);
            clearImagesBut.Margin = new Padding(4, 3, 4, 3);
            clearImagesBut.Name = "clearImagesBut";
            clearImagesBut.Size = new Size(61, 27);
            clearImagesBut.TabIndex = 62;
            clearImagesBut.Text = "Clear";
            clearImagesBut.UseVisualStyleBackColor = false;
            clearImagesBut.Click += clearSnapsBut_Click;
            // 
            // SettingsTab
            // 
            SettingsTab.BackColor = Color.FromArgb(95, 122, 156);
            SettingsTab.Controls.Add(label36);
            SettingsTab.Controls.Add(label22);
            SettingsTab.Controls.Add(RJoystickStageMoveAmountBox);
            SettingsTab.Controls.Add(label24);
            SettingsTab.Controls.Add(label15);
            SettingsTab.Controls.Add(label3);
            SettingsTab.Controls.Add(triggerFocusBox);
            SettingsTab.Controls.Add(label14);
            SettingsTab.Controls.Add(slicesPerSlideBox);
            SettingsTab.Controls.Add(label30);
            SettingsTab.Controls.Add(sliceIncrementBox);
            SettingsTab.Controls.Add(label31);
            SettingsTab.Controls.Add(imagesPerSliceBox);
            SettingsTab.Controls.Add(openProfileBut);
            SettingsTab.Controls.Add(saveProfileBut);
            SettingsTab.Controls.Add(label11);
            SettingsTab.Controls.Add(label1);
            SettingsTab.Controls.Add(LJoystickStageMoveAmountBox);
            SettingsTab.Controls.Add(label10);
            SettingsTab.Controls.Add(profilesBox);
            SettingsTab.Controls.Add(label6);
            SettingsTab.Controls.Add(label5);
            SettingsTab.Controls.Add(label7);
            SettingsTab.Controls.Add(label4);
            SettingsTab.Controls.Add(label8);
            SettingsTab.Controls.Add(deadzoneBox);
            SettingsTab.Controls.Add(LJoystickLabel);
            SettingsTab.Controls.Add(RJoystickLabel);
            SettingsTab.Controls.Add(label9);
            SettingsTab.Controls.Add(leftTriggerLabel);
            SettingsTab.Controls.Add(rightTriggerLabel);
            SettingsTab.Controls.Add(label12);
            SettingsTab.Controls.Add(YPressedLabel);
            SettingsTab.Controls.Add(BPressedLabel);
            SettingsTab.Controls.Add(APressedLabel);
            SettingsTab.Controls.Add(XPressedLabel);
            SettingsTab.Controls.Add(label17);
            SettingsTab.Controls.Add(DPadUpLabel);
            SettingsTab.Controls.Add(DPadRightLabel);
            SettingsTab.Controls.Add(l3PressedLabel);
            SettingsTab.Controls.Add(DPadDownLabel);
            SettingsTab.Controls.Add(r3PressedLabel);
            SettingsTab.Controls.Add(DPadLeftLabel);
            SettingsTab.Controls.Add(label25);
            SettingsTab.Controls.Add(label13);
            SettingsTab.Controls.Add(shoulderLeftLabel);
            SettingsTab.Controls.Add(shoulderRightLabel);
            SettingsTab.Controls.Add(BackPressedLabel);
            SettingsTab.Controls.Add(StartPressedLabel);
            SettingsTab.Location = new Point(4, 24);
            SettingsTab.Margin = new Padding(4, 3, 4, 3);
            SettingsTab.Name = "SettingsTab";
            SettingsTab.Padding = new Padding(4, 3, 4, 3);
            SettingsTab.Size = new Size(323, 522);
            SettingsTab.TabIndex = 1;
            SettingsTab.Text = "Controller Settings";
            toolTip.SetToolTip(SettingsTab, "Double click to change button function.");
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Location = new Point(4, 8);
            label36.Margin = new Padding(4, 0, 4, 0);
            label36.Name = "label36";
            label36.Size = new Size(222, 15);
            label36.TabIndex = 72;
            label36.Text = "Double click label to set button function.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 396);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(83, 15);
            label3.TabIndex = 69;
            label3.Text = "Slices per slide";
            // 
            // slicesPerSlideBox
            // 
            slicesPerSlideBox.BackColor = Color.FromArgb(49, 91, 138);
            slicesPerSlideBox.ForeColor = Color.White;
            slicesPerSlideBox.InterceptArrowKeys = false;
            slicesPerSlideBox.Location = new Point(103, 392);
            slicesPerSlideBox.Margin = new Padding(4, 3, 4, 3);
            slicesPerSlideBox.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            slicesPerSlideBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            slicesPerSlideBox.Name = "slicesPerSlideBox";
            slicesPerSlideBox.Size = new Size(56, 23);
            slicesPerSlideBox.TabIndex = 68;
            slicesPerSlideBox.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(166, 367);
            label30.Margin = new Padding(4, 0, 4, 0);
            label30.Name = "label30";
            label30.Size = new Size(88, 15);
            label30.TabIndex = 67;
            label30.Text = "Slice increment";
            // 
            // sliceIncrementBox
            // 
            sliceIncrementBox.BackColor = Color.FromArgb(49, 91, 138);
            sliceIncrementBox.ForeColor = Color.White;
            sliceIncrementBox.InterceptArrowKeys = false;
            sliceIncrementBox.Location = new Point(169, 392);
            sliceIncrementBox.Margin = new Padding(4, 3, 4, 3);
            sliceIncrementBox.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            sliceIncrementBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            sliceIncrementBox.Name = "sliceIncrementBox";
            sliceIncrementBox.Size = new Size(56, 23);
            sliceIncrementBox.TabIndex = 66;
            sliceIncrementBox.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(6, 367);
            label31.Margin = new Padding(4, 0, 4, 0);
            label31.Name = "label31";
            label31.Size = new Size(91, 15);
            label31.TabIndex = 65;
            label31.Text = "Images per slice";
            // 
            // imagesPerSliceBox
            // 
            imagesPerSliceBox.BackColor = Color.FromArgb(49, 91, 138);
            imagesPerSliceBox.ForeColor = Color.White;
            imagesPerSliceBox.InterceptArrowKeys = false;
            imagesPerSliceBox.Location = new Point(103, 365);
            imagesPerSliceBox.Margin = new Padding(4, 3, 4, 3);
            imagesPerSliceBox.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            imagesPerSliceBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            imagesPerSliceBox.Name = "imagesPerSliceBox";
            imagesPerSliceBox.Size = new Size(56, 23);
            imagesPerSliceBox.TabIndex = 64;
            imagesPerSliceBox.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // openProfileBut
            // 
            openProfileBut.BackColor = Color.FromArgb(49, 91, 138);
            openProfileBut.ForeColor = Color.White;
            openProfileBut.Location = new Point(220, 333);
            openProfileBut.Margin = new Padding(4, 3, 4, 3);
            openProfileBut.Name = "openProfileBut";
            openProfileBut.Size = new Size(56, 24);
            openProfileBut.TabIndex = 63;
            openProfileBut.Text = "Open";
            openProfileBut.UseVisualStyleBackColor = false;
            openProfileBut.Click += openProfileBut_Click;
            // 
            // saveProfileBut
            // 
            saveProfileBut.BackColor = Color.FromArgb(49, 91, 138);
            saveProfileBut.ForeColor = Color.White;
            saveProfileBut.Location = new Point(163, 333);
            saveProfileBut.Margin = new Padding(4, 3, 4, 3);
            saveProfileBut.Name = "saveProfileBut";
            saveProfileBut.Size = new Size(56, 24);
            saveProfileBut.TabIndex = 62;
            saveProfileBut.Text = "Save";
            saveProfileBut.UseVisualStyleBackColor = false;
            saveProfileBut.Click += saveProfileBut_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(8, 309);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 13);
            label1.TabIndex = 61;
            label1.Text = "User Profile";
            // 
            // profilesBox
            // 
            profilesBox.BackColor = Color.FromArgb(49, 91, 138);
            profilesBox.ForeColor = Color.White;
            profilesBox.FormattingEnabled = true;
            profilesBox.Location = new Point(12, 333);
            profilesBox.Margin = new Padding(4, 3, 4, 3);
            profilesBox.Name = "profilesBox";
            profilesBox.Size = new Size(144, 23);
            profilesBox.TabIndex = 60;
            profilesBox.Text = "Default";
            profilesBox.SelectedIndexChanged += profilesBox_SelectedIndexChanged;
            // 
            // tabContextMenu
            // 
            tabContextMenu.Items.AddRange(new ToolStripItem[] { undockToWindowToolStripMenuItem });
            tabContextMenu.Name = "tabContextMenu";
            tabContextMenu.Size = new Size(189, 26);
            tabContextMenu.Opening += tabContextMenu_Opening;
            // 
            // undockToWindowToolStripMenuItem
            // 
            undockToWindowToolStripMenuItem.Name = "undockToWindowToolStripMenuItem";
            undockToWindowToolStripMenuItem.Size = new Size(188, 22);
            undockToWindowToolStripMenuItem.Text = "Open In Tool Window";
            // 
            // saveFileDialog
            // 
            saveFileDialog.DefaultExt = "profile";
            saveFileDialog.Filter = "Profile File (.profile)|*.profile";
            // 
            // openSnapsFileDialog
            // 
            openSnapsFileDialog.Filter = "Image Files|*.czi;*.tif;*.tiff;*.csv";
            openSnapsFileDialog.Multiselect = true;
            openSnapsFileDialog.SupportMultiDottedExtensions = true;
            openSnapsFileDialog.Title = "Open Image File";
            // 
            // openExeFileDialog
            // 
            openExeFileDialog.Filter = "Executable files (*.exe)|*.exe";
            // 
            // mainWinContextMenuStrip
            // 
            mainWinContextMenuStrip.Items.AddRange(new ToolStripItem[] { hideToolStripMenuItem, closeToolStripMenuItem, borderToolStripMenuItem });
            mainWinContextMenuStrip.Name = "mainWinContextMenuStrip";
            mainWinContextMenuStrip.Size = new Size(189, 70);
            // 
            // hideToolStripMenuItem
            // 
            hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            hideToolStripMenuItem.Size = new Size(188, 22);
            hideToolStripMenuItem.Text = "Minimize";
            hideToolStripMenuItem.Click += hideToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(188, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // borderToolStripMenuItem
            // 
            borderToolStripMenuItem.Name = "borderToolStripMenuItem";
            borderToolStripMenuItem.Size = new Size(188, 22);
            borderToolStripMenuItem.Text = "Window Border None";
            borderToolStripMenuItem.Click += borderToolStripMenuItem_Click;
            // 
            // openImagesDialog
            // 
            openImagesDialog.Filter = "Image Files|*.czi;*.tif;*.tiff;";
            openImagesDialog.SupportMultiDottedExtensions = true;
            openImagesDialog.Title = "Open Image File";
            // 
            // saveImageDialog
            // 
            saveImageDialog.DefaultExt = "tif";
            saveImageDialog.SupportMultiDottedExtensions = true;
            // 
            // Imager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 91, 138);
            ClientSize = new Size(335, 581);
            ContextMenuStrip = mainWinContextMenuStrip;
            Controls.Add(mainTabControl);
            Controls.Add(statusStrip);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Imager";
            StartPosition = FormStartPosition.Manual;
            Text = "Imager";
            FormClosing += MainForm_FormClosing;
            KeyDown += MainForm_KeyDown;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RJoystickStageMoveAmountBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)triggerFocusBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)LJoystickStageMoveAmountBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)deadzoneBox).EndInit();
            contextMenuStrip2.ResumeLayout(false);
            snapContextMenuStrip.ResumeLayout(false);
            mainTabControl.ResumeLayout(false);
            ImagingTab.ResumeLayout(false);
            ImagingTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)zCoordBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)yCoordBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)xCoordBox).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            SettingsTab.ResumeLayout(false);
            SettingsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)slicesPerSlideBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliceIncrementBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)imagesPerSliceBox).EndInit();
            tabContextMenu.ResumeLayout(false);
            mainWinContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer statusTimer;
        private StatusStrip statusStrip;
        private System.Windows.Forms.Timer controllerJoystickUpdate;
        private Label label5;
        private Label label4;
        private NumericUpDown deadzoneBox;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label LJoystickLabel;
        private Label RJoystickLabel;
        private Label label9;
        private Label leftTriggerLabel;
        private Label rightTriggerLabel;
        private Label label11;
        private NumericUpDown LJoystickStageMoveAmountBox;
        private Label label10;
        private Label label12;
        private Label YPressedLabel;
        private Label BPressedLabel;
        private Label APressedLabel;
        private Label XPressedLabel;
        private Label label17;
        private Label DPadLeftLabel;
        private Label DPadDownLabel;
        private Label DPadRightLabel;
        private Label DPadUpLabel;
        private Label label13;
        private Label shoulderRightLabel;
        private Label shoulderLeftLabel;
        private Label BackPressedLabel;
        private Label StartPressedLabel;
        private Label label15;
        private NumericUpDown triggerFocusBox;
        private Label label14;
        private ListBox storedCoordsBox;
        private Label label20;
        private Button addCoordBut;
        private ContextMenuStrip snapContextMenuStrip;
        private ToolStripMenuItem goToToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Label label21;
        private Label lastCoordDistanceLabel;
        private Label lastSelectedCoordsLabel;
        private Label label23;
        private Label label22;
        private NumericUpDown RJoystickStageMoveAmountBox;
        private Label label24;
        private Button clearBut;
        private Label label25;
        private Label l3PressedLabel;
        private Label r3PressedLabel;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Label lastSelectedSnapDistanceLabel;
        private Label label27;
        private Label lastSnapDistanceLabel;
        private Label label29;
        private ListBox storedImageBox;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer controllerButtonUpdate;
        private ToolStripMenuItem goToToolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem1;
        private CheckBox topMostCheckBox;
        private TabControl mainTabControl;
        private TabPage ImagingTab;
        private TabPage SettingsTab;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private Button openProfileBut;
        private Button saveProfileBut;
        private Label label1;
        private ComboBox profilesBox;
        private Label label3;
        private NumericUpDown slicesPerSlideBox;
        private Label label30;
        private NumericUpDown sliceIncrementBox;
        private Label label31;
        private NumericUpDown imagesPerSliceBox;
        private OpenFileDialog openProfileFileDialog;
        private SaveFileDialog saveFileDialog;
        private Button clearImagesBut;
        private SplitContainer splitContainer;
        private Label label32;
        private NumericUpDown xCoordBox;
        private Button goToLocBox;
        private Label label35;
        private NumericUpDown zCoordBox;
        private Label label34;
        private NumericUpDown yCoordBox;
        private OpenFileDialog openSnapsFileDialog;
        private CheckBox dockToApp;
        private ContextMenuStrip tabContextMenu;
        private ToolStripMenuItem undockToWindowToolStripMenuItem;
        private OpenFileDialog openExeFileDialog;
        private ContextMenuStrip mainWinContextMenuStrip;
        private ToolStripMenuItem hideToolStripMenuItem;
        private ToolStripMenuItem borderToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolTip toolTip;
        private OpenFileDialog openImagesDialog;
        private ToolStripMenuItem openInImageViewToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem1;
        private SaveFileDialog saveImageDialog;
        private Label label36;
        private Label label26;
    }
}

