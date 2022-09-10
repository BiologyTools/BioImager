namespace Bio
{
    partial class Tools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tools));
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.textPanel = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.movePanel = new System.Windows.Forms.Panel();
            this.freeformPanel = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectPanel = new System.Windows.Forms.Panel();
            this.ellipsePanel = new System.Windows.Forms.Panel();
            this.linePanel = new System.Windows.Forms.Panel();
            this.polyPanel = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.pointPanel = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.deletePanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rectSelPanel = new System.Windows.Forms.Panel();
            this.panPanel = new System.Windows.Forms.Panel();
            this.magicPanel = new System.Windows.Forms.Panel();
            this.movePanel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.polyPanel.SuspendLayout();
            this.pointPanel.SuspendLayout();
            this.deletePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.White;
            this.colorDialog.SolidColorOnly = true;
            // 
            // textPanel
            // 
            this.textPanel.BackColor = System.Drawing.Color.White;
            this.textPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textPanel.BackgroundImage")));
            this.textPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textPanel.Location = new System.Drawing.Point(0, 120);
            this.textPanel.Name = "textPanel";
            this.textPanel.Size = new System.Drawing.Size(30, 30);
            this.textPanel.TabIndex = 3;
            this.textPanel.Click += new System.EventHandler(this.textPanel_Click);
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Location = new System.Drawing.Point(0, 33);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(30, 30);
            this.panel7.TabIndex = 3;
            // 
            // movePanel
            // 
            this.movePanel.BackColor = System.Drawing.Color.LightGray;
            this.movePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("movePanel.BackgroundImage")));
            this.movePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.movePanel.Controls.Add(this.panel7);
            this.movePanel.Location = new System.Drawing.Point(30, 0);
            this.movePanel.Name = "movePanel";
            this.movePanel.Size = new System.Drawing.Size(30, 30);
            this.movePanel.TabIndex = 6;
            this.movePanel.Click += new System.EventHandler(this.movePanel_Click);
            // 
            // freeformPanel
            // 
            this.freeformPanel.BackColor = System.Drawing.Color.White;
            this.freeformPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("freeformPanel.BackgroundImage")));
            this.freeformPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.freeformPanel.Location = new System.Drawing.Point(30, 150);
            this.freeformPanel.Name = "freeformPanel";
            this.freeformPanel.Size = new System.Drawing.Size(30, 30);
            this.freeformPanel.TabIndex = 7;
            this.freeformPanel.Click += new System.EventHandler(this.freeformPanel_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(149, 26);
            // 
            // addNewToolToolStripMenuItem
            // 
            this.addNewToolToolStripMenuItem.Name = "addNewToolToolStripMenuItem";
            this.addNewToolToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addNewToolToolStripMenuItem.Text = "Add New Tool";
            // 
            // rectPanel
            // 
            this.rectPanel.BackColor = System.Drawing.Color.White;
            this.rectPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rectPanel.BackgroundImage")));
            this.rectPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rectPanel.Location = new System.Drawing.Point(30, 60);
            this.rectPanel.Name = "rectPanel";
            this.rectPanel.Size = new System.Drawing.Size(30, 30);
            this.rectPanel.TabIndex = 2;
            this.rectPanel.Click += new System.EventHandler(this.rectPanel_Click);
            // 
            // ellipsePanel
            // 
            this.ellipsePanel.BackColor = System.Drawing.Color.White;
            this.ellipsePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ellipsePanel.BackgroundImage")));
            this.ellipsePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ellipsePanel.Location = new System.Drawing.Point(0, 30);
            this.ellipsePanel.Name = "ellipsePanel";
            this.ellipsePanel.Size = new System.Drawing.Size(30, 30);
            this.ellipsePanel.TabIndex = 8;
            this.ellipsePanel.Click += new System.EventHandler(this.ellipsePanel_Click);
            // 
            // linePanel
            // 
            this.linePanel.BackColor = System.Drawing.Color.White;
            this.linePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("linePanel.BackgroundImage")));
            this.linePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.linePanel.Location = new System.Drawing.Point(30, 90);
            this.linePanel.Name = "linePanel";
            this.linePanel.Size = new System.Drawing.Size(30, 30);
            this.linePanel.TabIndex = 3;
            this.linePanel.Click += new System.EventHandler(this.linePanel_Click);
            // 
            // polyPanel
            // 
            this.polyPanel.BackColor = System.Drawing.Color.White;
            this.polyPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("polyPanel.BackgroundImage")));
            this.polyPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.polyPanel.Controls.Add(this.panel11);
            this.polyPanel.Location = new System.Drawing.Point(0, 90);
            this.polyPanel.Name = "polyPanel";
            this.polyPanel.Size = new System.Drawing.Size(30, 30);
            this.polyPanel.TabIndex = 4;
            this.polyPanel.Click += new System.EventHandler(this.polyPanel_Click);
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Location = new System.Drawing.Point(0, 33);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(30, 30);
            this.panel11.TabIndex = 3;
            // 
            // pointPanel
            // 
            this.pointPanel.BackColor = System.Drawing.Color.White;
            this.pointPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pointPanel.BackgroundImage")));
            this.pointPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pointPanel.Controls.Add(this.panel13);
            this.pointPanel.Location = new System.Drawing.Point(0, 60);
            this.pointPanel.Name = "pointPanel";
            this.pointPanel.Size = new System.Drawing.Size(30, 30);
            this.pointPanel.TabIndex = 5;
            this.pointPanel.Click += new System.EventHandler(this.pointPanel_Click);
            // 
            // panel13
            // 
            this.panel13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel13.BackgroundImage")));
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.Location = new System.Drawing.Point(0, 33);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(30, 30);
            this.panel13.TabIndex = 3;
            // 
            // deletePanel
            // 
            this.deletePanel.BackColor = System.Drawing.Color.White;
            this.deletePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deletePanel.BackgroundImage")));
            this.deletePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deletePanel.Controls.Add(this.panel2);
            this.deletePanel.Location = new System.Drawing.Point(30, 120);
            this.deletePanel.Name = "deletePanel";
            this.deletePanel.Size = new System.Drawing.Size(30, 30);
            this.deletePanel.TabIndex = 5;
            this.deletePanel.Click += new System.EventHandler(this.deletePanel_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 30);
            this.panel2.TabIndex = 3;
            // 
            // rectSelPanel
            // 
            this.rectSelPanel.BackColor = System.Drawing.Color.White;
            this.rectSelPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rectSelPanel.BackgroundImage")));
            this.rectSelPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rectSelPanel.Location = new System.Drawing.Point(-1, 0);
            this.rectSelPanel.Name = "rectSelPanel";
            this.rectSelPanel.Size = new System.Drawing.Size(30, 30);
            this.rectSelPanel.TabIndex = 9;
            this.rectSelPanel.Click += new System.EventHandler(this.rectSelPanel_Click);
            // 
            // panPanel
            // 
            this.panPanel.BackColor = System.Drawing.Color.White;
            this.panPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panPanel.BackgroundImage")));
            this.panPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panPanel.Location = new System.Drawing.Point(30, 30);
            this.panPanel.Name = "panPanel";
            this.panPanel.Size = new System.Drawing.Size(30, 30);
            this.panPanel.TabIndex = 3;
            this.panPanel.Click += new System.EventHandler(this.panPanel_Click);
            // 
            // magicPanel
            // 
            this.magicPanel.BackColor = System.Drawing.Color.White;
            this.magicPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("magicPanel.BackgroundImage")));
            this.magicPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.magicPanel.Location = new System.Drawing.Point(0, 150);
            this.magicPanel.Name = "magicPanel";
            this.magicPanel.Size = new System.Drawing.Size(30, 30);
            this.magicPanel.TabIndex = 8;
            this.magicPanel.Click += new System.EventHandler(this.magicPanel_Click);
            this.magicPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.magicPanel_MouseDoubleClick);
            // 
            // Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(61, 181);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.magicPanel);
            this.Controls.Add(this.panPanel);
            this.Controls.Add(this.rectPanel);
            this.Controls.Add(this.deletePanel);
            this.Controls.Add(this.rectSelPanel);
            this.Controls.Add(this.freeformPanel);
            this.Controls.Add(this.textPanel);
            this.Controls.Add(this.linePanel);
            this.Controls.Add(this.pointPanel);
            this.Controls.Add(this.polyPanel);
            this.Controls.Add(this.ellipsePanel);
            this.Controls.Add(this.movePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tools";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Tools";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tools_FormClosing);
            this.movePanel.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.polyPanel.ResumeLayout(false);
            this.pointPanel.ResumeLayout(false);
            this.deletePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Panel textPanel;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel movePanel;
        private System.Windows.Forms.Panel freeformPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewToolToolStripMenuItem;
        private System.Windows.Forms.Panel rectPanel;
        private System.Windows.Forms.Panel ellipsePanel;
        private System.Windows.Forms.Panel linePanel;
        private System.Windows.Forms.Panel polyPanel;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel pointPanel;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel deletePanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel rectSelPanel;
        private System.Windows.Forms.Panel panPanel;
        private System.Windows.Forms.Panel magicPanel;
    }
}