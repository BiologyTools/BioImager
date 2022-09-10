namespace Bio
{
    partial class Recordings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recordings));
            this.view = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.performToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startBut = new System.Windows.Forms.Button();
            this.stopBut = new System.Windows.Forms.Button();
            this.recordStatusLabel = new System.Windows.Forms.Label();
            this.playBut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.saveRecDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecDialog = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.propView = new System.Windows.Forms.TreeView();
            this.propMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePropMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.propRecStatusLabel = new System.Windows.Forms.Label();
            this.stopPropBut = new System.Windows.Forms.Button();
            this.startPropBut = new System.Windows.Forms.Button();
            this.getPropBut = new System.Windows.Forms.Button();
            this.savePropDialog = new System.Windows.Forms.SaveFileDialog();
            this.openPropDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.propBox = new System.Windows.Forms.ComboBox();
            this.setPropBut = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.propMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // view
            // 
            this.view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.view.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.view.ContextMenuStrip = this.contextMenuStrip;
            this.view.ForeColor = System.Drawing.Color.White;
            this.view.Location = new System.Drawing.Point(3, 18);
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(366, 113);
            this.view.TabIndex = 3;
            this.view.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.view_BeforeExpand);
            this.view.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.view_AfterSelect);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.performToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(139, 92);
            // 
            // performToolStripMenuItem
            // 
            this.performToolStripMenuItem.Name = "performToolStripMenuItem";
            this.performToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.performToolStripMenuItem.Text = "Perform";
            this.performToolStripMenuItem.Click += new System.EventHandler(this.performToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // startBut
            // 
            this.startBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.startBut.ForeColor = System.Drawing.Color.White;
            this.startBut.Location = new System.Drawing.Point(81, 150);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(75, 23);
            this.startBut.TabIndex = 5;
            this.startBut.Text = "Start";
            this.startBut.UseVisualStyleBackColor = false;
            this.startBut.Click += new System.EventHandler(this.startBut_Click);
            // 
            // stopBut
            // 
            this.stopBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.stopBut.ForeColor = System.Drawing.Color.White;
            this.stopBut.Location = new System.Drawing.Point(3, 150);
            this.stopBut.Name = "stopBut";
            this.stopBut.Size = new System.Drawing.Size(75, 23);
            this.stopBut.TabIndex = 6;
            this.stopBut.Text = "Stop";
            this.stopBut.UseVisualStyleBackColor = false;
            this.stopBut.Click += new System.EventHandler(this.stopBut_Click);
            // 
            // recordStatusLabel
            // 
            this.recordStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recordStatusLabel.AutoSize = true;
            this.recordStatusLabel.ForeColor = System.Drawing.Color.White;
            this.recordStatusLabel.Location = new System.Drawing.Point(3, 134);
            this.recordStatusLabel.Name = "recordStatusLabel";
            this.recordStatusLabel.Size = new System.Drawing.Size(102, 13);
            this.recordStatusLabel.TabIndex = 7;
            this.recordStatusLabel.Text = "Recording: Stopped";
            // 
            // playBut
            // 
            this.playBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.playBut.ForeColor = System.Drawing.Color.White;
            this.playBut.Location = new System.Drawing.Point(158, 150);
            this.playBut.Name = "playBut";
            this.playBut.Size = new System.Drawing.Size(105, 23);
            this.playBut.TabIndex = 8;
            this.playBut.Text = "Run Selected";
            this.playBut.UseVisualStyleBackColor = false;
            this.playBut.Click += new System.EventHandler(this.playBut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Recordings";
            // 
            // saveRecDialog
            // 
            this.saveRecDialog.Filter = "Recording Files (*.reco)|*.reco";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(369, 24);
            this.menuStrip.TabIndex = 13;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openPropertyToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveSelectedValueToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openToolStripMenuItem.Text = "Open Recording";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openPropertyToolStripMenuItem
            // 
            this.openPropertyToolStripMenuItem.Name = "openPropertyToolStripMenuItem";
            this.openPropertyToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openPropertyToolStripMenuItem.Text = "Open Property";
            this.openPropertyToolStripMenuItem.Click += new System.EventHandler(this.openPropertyToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveToolStripMenuItem.Text = "Save Selected Recording";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveSelectedValueToolStripMenuItem
            // 
            this.saveSelectedValueToolStripMenuItem.Name = "saveSelectedValueToolStripMenuItem";
            this.saveSelectedValueToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveSelectedValueToolStripMenuItem.Text = "Save Selected Property";
            this.saveSelectedValueToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedValueToolStripMenuItem_Click);
            // 
            // openRecDialog
            // 
            this.openRecDialog.Filter = "Recording Files (*.reco)|*.reco";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Properties";
            // 
            // propView
            // 
            this.propView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.propView.ContextMenuStrip = this.propMenuStrip;
            this.propView.ForeColor = System.Drawing.Color.White;
            this.propView.Location = new System.Drawing.Point(0, 19);
            this.propView.Name = "propView";
            this.propView.Size = new System.Drawing.Size(369, 187);
            this.propView.TabIndex = 14;
            this.propView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.propView_AfterSelect);
            // 
            // propMenuStrip
            // 
            this.propMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getMenuItem,
            this.deletePropMenuItem,
            this.moveUpToolStripMenuItem1,
            this.moveDownToolStripMenuItem1});
            this.propMenuStrip.Name = "contextMenuStrip";
            this.propMenuStrip.Size = new System.Drawing.Size(139, 92);
            // 
            // getMenuItem
            // 
            this.getMenuItem.Name = "getMenuItem";
            this.getMenuItem.Size = new System.Drawing.Size(138, 22);
            this.getMenuItem.Text = "Get";
            this.getMenuItem.Click += new System.EventHandler(this.getMenuItem_Click);
            // 
            // deletePropMenuItem
            // 
            this.deletePropMenuItem.Name = "deletePropMenuItem";
            this.deletePropMenuItem.Size = new System.Drawing.Size(138, 22);
            this.deletePropMenuItem.Text = "Delete";
            this.deletePropMenuItem.Click += new System.EventHandler(this.deletePropMenuItem_Click);
            // 
            // moveUpToolStripMenuItem1
            // 
            this.moveUpToolStripMenuItem1.Name = "moveUpToolStripMenuItem1";
            this.moveUpToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.moveUpToolStripMenuItem1.Text = "Move Up";
            this.moveUpToolStripMenuItem1.Click += new System.EventHandler(this.moveUpToolStripMenuItem1_Click);
            // 
            // moveDownToolStripMenuItem1
            // 
            this.moveDownToolStripMenuItem1.Name = "moveDownToolStripMenuItem1";
            this.moveDownToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.moveDownToolStripMenuItem1.Text = "Move Down";
            this.moveDownToolStripMenuItem1.Click += new System.EventHandler(this.moveDownToolStripMenuItem1_Click);
            // 
            // propRecStatusLabel
            // 
            this.propRecStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.propRecStatusLabel.AutoSize = true;
            this.propRecStatusLabel.ForeColor = System.Drawing.Color.White;
            this.propRecStatusLabel.Location = new System.Drawing.Point(0, 211);
            this.propRecStatusLabel.Name = "propRecStatusLabel";
            this.propRecStatusLabel.Size = new System.Drawing.Size(144, 13);
            this.propRecStatusLabel.TabIndex = 18;
            this.propRecStatusLabel.Text = "Property Recording: Stopped";
            // 
            // stopPropBut
            // 
            this.stopPropBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopPropBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.stopPropBut.ForeColor = System.Drawing.Color.White;
            this.stopPropBut.Location = new System.Drawing.Point(3, 229);
            this.stopPropBut.Name = "stopPropBut";
            this.stopPropBut.Size = new System.Drawing.Size(75, 23);
            this.stopPropBut.TabIndex = 17;
            this.stopPropBut.Text = "Stop";
            this.stopPropBut.UseVisualStyleBackColor = false;
            this.stopPropBut.Click += new System.EventHandler(this.stopPropBut_Click);
            // 
            // startPropBut
            // 
            this.startPropBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startPropBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.startPropBut.ForeColor = System.Drawing.Color.White;
            this.startPropBut.Location = new System.Drawing.Point(81, 229);
            this.startPropBut.Name = "startPropBut";
            this.startPropBut.Size = new System.Drawing.Size(75, 23);
            this.startPropBut.TabIndex = 16;
            this.startPropBut.Text = "Start";
            this.startPropBut.UseVisualStyleBackColor = false;
            this.startPropBut.Click += new System.EventHandler(this.startPropBut_Click);
            // 
            // getPropBut
            // 
            this.getPropBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.getPropBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.getPropBut.ForeColor = System.Drawing.Color.White;
            this.getPropBut.Location = new System.Drawing.Point(158, 229);
            this.getPropBut.Name = "getPropBut";
            this.getPropBut.Size = new System.Drawing.Size(101, 23);
            this.getPropBut.TabIndex = 19;
            this.getPropBut.Text = "Get Property";
            this.getPropBut.UseVisualStyleBackColor = false;
            this.getPropBut.Click += new System.EventHandler(this.getPropBut_Click);
            // 
            // savePropDialog
            // 
            this.savePropDialog.Filter = "Property Files (*.pro)|*.pro";
            this.savePropDialog.InitialDirectory = "/Recordings";
            // 
            // openPropDialog
            // 
            this.openPropDialog.Filter = "Property Files (*.pro)|*.pro";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.view);
            this.splitContainer.Panel1.Controls.Add(this.startBut);
            this.splitContainer.Panel1.Controls.Add(this.stopBut);
            this.splitContainer.Panel1.Controls.Add(this.recordStatusLabel);
            this.splitContainer.Panel1.Controls.Add(this.playBut);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.label3);
            this.splitContainer.Panel2.Controls.Add(this.propBox);
            this.splitContainer.Panel2.Controls.Add(this.setPropBut);
            this.splitContainer.Panel2.Controls.Add(this.label2);
            this.splitContainer.Panel2.Controls.Add(this.propView);
            this.splitContainer.Panel2.Controls.Add(this.startPropBut);
            this.splitContainer.Panel2.Controls.Add(this.getPropBut);
            this.splitContainer.Panel2.Controls.Add(this.stopPropBut);
            this.splitContainer.Panel2.Controls.Add(this.propRecStatusLabel);
            this.splitContainer.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainer.Size = new System.Drawing.Size(369, 477);
            this.splitContainer.SplitterDistance = 186;
            this.splitContainer.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Property Type:";
            // 
            // propBox
            // 
            this.propBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.propBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.propBox.ForeColor = System.Drawing.Color.White;
            this.propBox.FormattingEnabled = true;
            this.propBox.Location = new System.Drawing.Point(81, 257);
            this.propBox.Name = "propBox";
            this.propBox.Size = new System.Drawing.Size(105, 21);
            this.propBox.TabIndex = 21;
            this.propBox.SelectedIndexChanged += new System.EventHandler(this.propBox_SelectedIndexChanged);
            // 
            // setPropBut
            // 
            this.setPropBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.setPropBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setPropBut.ForeColor = System.Drawing.Color.White;
            this.setPropBut.Location = new System.Drawing.Point(261, 229);
            this.setPropBut.Name = "setPropBut";
            this.setPropBut.Size = new System.Drawing.Size(105, 23);
            this.setPropBut.TabIndex = 20;
            this.setPropBut.Text = "Set Property";
            this.setPropBut.UseVisualStyleBackColor = false;
            this.setPropBut.Click += new System.EventHandler(this.setPropBut_Click);
            // 
            // Recordings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(369, 501);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Recordings";
            this.Text = "GUI Recordings & Properties";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Elements_FormClosing);
            this.contextMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.propMenuStrip.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView view;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Button startBut;
        private System.Windows.Forms.Button stopBut;
        private System.Windows.Forms.Label recordStatusLabel;
        private System.Windows.Forms.Button playBut;
        private System.Windows.Forms.ToolStripMenuItem performToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog saveRecDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openRecDialog;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedValueToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView propView;
        private System.Windows.Forms.Label propRecStatusLabel;
        private System.Windows.Forms.Button stopPropBut;
        private System.Windows.Forms.Button startPropBut;
        private System.Windows.Forms.Button getPropBut;
        private System.Windows.Forms.ToolStripMenuItem openPropertyToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog savePropDialog;
        private System.Windows.Forms.OpenFileDialog openPropDialog;
        private System.Windows.Forms.ContextMenuStrip propMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem getMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePropMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button setPropBut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox propBox;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem1;
    }
}