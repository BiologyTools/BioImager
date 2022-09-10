namespace Bio
{
    partial class Scripting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scripting));
            this.scriptView = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.outputTab = new System.Windows.Forms.TabPage();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.error = new System.Windows.Forms.TabPage();
            this.errorView = new System.Windows.Forms.ListView();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scriptLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.scriptLoadBut = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.stopBut = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.topMostBox = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.outputTab.SuspendLayout();
            this.error.SuspendLayout();
            this.logTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // scriptView
            // 
            this.scriptView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.scriptView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.scriptView.ContextMenuStrip = this.contextMenuStrip;
            this.scriptView.ForeColor = System.Drawing.Color.White;
            this.scriptView.HideSelection = false;
            this.scriptView.Location = new System.Drawing.Point(0, 0);
            this.scriptView.MultiSelect = false;
            this.scriptView.Name = "scriptView";
            this.scriptView.Size = new System.Drawing.Size(179, 429);
            this.scriptView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.scriptView.TabIndex = 1;
            this.scriptView.TabStop = false;
            this.scriptView.UseCompatibleStateImageBehavior = false;
            this.scriptView.View = System.Windows.Forms.View.List;
            this.scriptView.SelectedIndexChanged += new System.EventHandler(this.scriptView_SelectedIndexChanged);
            this.scriptView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.scriptView_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.openScriptFolderToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(173, 70);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // openScriptFolderToolStripMenuItem
            // 
            this.openScriptFolderToolStripMenuItem.Name = "openScriptFolderToolStripMenuItem";
            this.openScriptFolderToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openScriptFolderToolStripMenuItem.Text = "Open Script Folder";
            this.openScriptFolderToolStripMenuItem.Click += new System.EventHandler(this.openScriptFolderToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.outputTab);
            this.tabControl.Controls.Add(this.error);
            this.tabControl.Controls.Add(this.logTabPage);
            this.tabControl.Location = new System.Drawing.Point(3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(355, 147);
            this.tabControl.TabIndex = 8;
            this.tabControl.TabStop = false;
            // 
            // outputTab
            // 
            this.outputTab.Controls.Add(this.outputBox);
            this.outputTab.Location = new System.Drawing.Point(4, 22);
            this.outputTab.Name = "outputTab";
            this.outputTab.Padding = new System.Windows.Forms.Padding(3);
            this.outputTab.Size = new System.Drawing.Size(347, 121);
            this.outputTab.TabIndex = 1;
            this.outputTab.Text = "Output";
            this.outputTab.UseVisualStyleBackColor = true;
            // 
            // outputBox
            // 
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBox.Location = new System.Drawing.Point(3, 3);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(341, 115);
            this.outputBox.TabIndex = 2;
            this.outputBox.TabStop = false;
            // 
            // error
            // 
            this.error.Controls.Add(this.errorView);
            this.error.Location = new System.Drawing.Point(4, 22);
            this.error.Name = "error";
            this.error.Padding = new System.Windows.Forms.Padding(3);
            this.error.Size = new System.Drawing.Size(347, 121);
            this.error.TabIndex = 0;
            this.error.Text = "Error";
            this.error.UseVisualStyleBackColor = true;
            // 
            // errorView
            // 
            this.errorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorView.HideSelection = false;
            this.errorView.Location = new System.Drawing.Point(3, 3);
            this.errorView.Name = "errorView";
            this.errorView.Size = new System.Drawing.Size(341, 115);
            this.errorView.TabIndex = 0;
            this.errorView.UseCompatibleStateImageBehavior = false;
            this.errorView.View = System.Windows.Forms.View.List;
            this.errorView.SelectedIndexChanged += new System.EventHandler(this.errorView_SelectedIndexChanged);
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.logBox);
            this.logTabPage.Location = new System.Drawing.Point(4, 22);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Size = new System.Drawing.Size(347, 121);
            this.logTabPage.TabIndex = 2;
            this.logTabPage.Text = "Log";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(0, 0);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(347, 121);
            this.logBox.TabIndex = 4;
            this.logBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(189, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Script:";
            // 
            // scriptLabel
            // 
            this.scriptLabel.AutoSize = true;
            this.scriptLabel.ForeColor = System.Drawing.Color.White;
            this.scriptLabel.Location = new System.Drawing.Point(225, 9);
            this.scriptLabel.Name = "scriptLabel";
            this.scriptLabel.Size = new System.Drawing.Size(0, 13);
            this.scriptLabel.TabIndex = 10;
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.runButton.Location = new System.Drawing.Point(426, 398);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(57, 23);
            this.runButton.TabIndex = 11;
            this.runButton.TabStop = false;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(345, 398);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 13;
            this.saveButton.TabStop = false;
            this.saveButton.Text = "Save Script";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // scriptLoadBut
            // 
            this.scriptLoadBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptLoadBut.Location = new System.Drawing.Point(263, 398);
            this.scriptLoadBut.Name = "scriptLoadBut";
            this.scriptLoadBut.Size = new System.Drawing.Size(75, 23);
            this.scriptLoadBut.TabIndex = 12;
            this.scriptLoadBut.TabStop = false;
            this.scriptLoadBut.Text = "Load Script";
            this.scriptLoadBut.UseVisualStyleBackColor = true;
            this.scriptLoadBut.Click += new System.EventHandler(this.scriptLoadBut_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "\"C# Files (*.cs)|*.cs| IJM Files (*.ijm)|*.ijm| All files (*.*)|*.*\"";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "\"C# Files (*.cs)|*.cs| Text Files (*.txt)|*.txt| All files (*.*)|*.*\"";
            // 
            // stopBut
            // 
            this.stopBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopBut.Location = new System.Drawing.Point(489, 398);
            this.stopBut.Name = "stopBut";
            this.stopBut.Size = new System.Drawing.Size(57, 23);
            this.stopBut.TabIndex = 14;
            this.stopBut.TabStop = false;
            this.stopBut.Text = "Stop";
            this.stopBut.UseVisualStyleBackColor = true;
            this.stopBut.Click += new System.EventHandler(this.stopBut_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(185, 25);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(361, 367);
            this.splitContainer.SplitterDistance = 212;
            this.splitContainer.TabIndex = 15;
            // 
            // topMostBox
            // 
            this.topMostBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.topMostBox.AutoSize = true;
            this.topMostBox.Checked = true;
            this.topMostBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topMostBox.ForeColor = System.Drawing.Color.White;
            this.topMostBox.Location = new System.Drawing.Point(188, 402);
            this.topMostBox.Name = "topMostBox";
            this.topMostBox.Size = new System.Drawing.Size(71, 17);
            this.topMostBox.TabIndex = 3;
            this.topMostBox.Text = "Top Most";
            this.topMostBox.UseVisualStyleBackColor = true;
            this.topMostBox.CheckedChanged += new System.EventHandler(this.topMostBox_CheckedChanged);
            // 
            // Scripting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(552, 428);
            this.Controls.Add(this.topMostBox);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.stopBut);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.scriptLoadBut);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.scriptLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scriptView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Scripting";
            this.Text = "Scripting";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptRunner_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Scripting_KeyDown);
            this.contextMenuStrip.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.outputTab.ResumeLayout(false);
            this.outputTab.PerformLayout();
            this.error.ResumeLayout(false);
            this.logTabPage.ResumeLayout(false);
            this.logTabPage.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView scriptView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openScriptFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage outputTab;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.TabPage error;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label scriptLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button scriptLoadBut;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button stopBut;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.CheckBox topMostBox;
        private System.Windows.Forms.ListView errorView;
    }
}