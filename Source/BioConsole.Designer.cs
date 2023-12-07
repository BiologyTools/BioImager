
namespace BioImager
{
    partial class BioConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BioConsole));
            this.textBox = new System.Windows.Forms.TextBox();
            this.runBut = new System.Windows.Forms.Button();
            this.consoleBox = new System.Windows.Forms.TextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.imagejBut = new System.Windows.Forms.Button();
            this.headlessBox = new System.Windows.Forms.CheckBox();
            this.topMostBox = new System.Windows.Forms.CheckBox();
            this.selRadioBut = new System.Windows.Forms.RadioButton();
            this.tabRadioBut = new System.Windows.Forms.RadioButton();
            this.biofBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(429, 69);
            this.textBox.TabIndex = 18;
            // 
            // runBut
            // 
            this.runBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.runBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.runBut.ForeColor = System.Drawing.Color.White;
            this.runBut.Location = new System.Drawing.Point(378, 258);
            this.runBut.Name = "runBut";
            this.runBut.Size = new System.Drawing.Size(56, 23);
            this.runBut.TabIndex = 21;
            this.runBut.Text = "Run";
            this.runBut.UseVisualStyleBackColor = false;
            this.runBut.Click += new System.EventHandler(this.runBut_Click);
            // 
            // consoleBox
            // 
            this.consoleBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.consoleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleBox.ForeColor = System.Drawing.Color.White;
            this.consoleBox.Location = new System.Drawing.Point(0, 0);
            this.consoleBox.Multiline = true;
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.Size = new System.Drawing.Size(429, 159);
            this.consoleBox.TabIndex = 22;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(6, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.consoleBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.textBox);
            this.splitContainer.Size = new System.Drawing.Size(429, 232);
            this.splitContainer.SplitterDistance = 159;
            this.splitContainer.TabIndex = 23;
            // 
            // imagejBut
            // 
            this.imagejBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.imagejBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.imagejBut.ForeColor = System.Drawing.Color.White;
            this.imagejBut.Location = new System.Drawing.Point(299, 258);
            this.imagejBut.Name = "imagejBut";
            this.imagejBut.Size = new System.Drawing.Size(73, 23);
            this.imagejBut.TabIndex = 24;
            this.imagejBut.Text = "Run ImageJ";
            this.imagejBut.UseVisualStyleBackColor = false;
            this.imagejBut.Click += new System.EventHandler(this.imagejBut_Click);
            // 
            // headlessBox
            // 
            this.headlessBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.headlessBox.AutoSize = true;
            this.headlessBox.Location = new System.Drawing.Point(88, 262);
            this.headlessBox.Name = "headlessBox";
            this.headlessBox.Size = new System.Drawing.Size(70, 17);
            this.headlessBox.TabIndex = 25;
            this.headlessBox.Text = "Headless";
            this.headlessBox.UseVisualStyleBackColor = true;
            // 
            // topMostBox
            // 
            this.topMostBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.topMostBox.AutoSize = true;
            this.topMostBox.Location = new System.Drawing.Point(11, 261);
            this.topMostBox.Name = "topMostBox";
            this.topMostBox.Size = new System.Drawing.Size(71, 17);
            this.topMostBox.TabIndex = 26;
            this.topMostBox.Text = "Top Most";
            this.topMostBox.UseVisualStyleBackColor = true;
            this.topMostBox.CheckedChanged += new System.EventHandler(this.topMostBox_CheckedChanged);
            // 
            // selRadioBut
            // 
            this.selRadioBut.AutoSize = true;
            this.selRadioBut.Checked = true;
            this.selRadioBut.Location = new System.Drawing.Point(164, 261);
            this.selRadioBut.Name = "selRadioBut";
            this.selRadioBut.Size = new System.Drawing.Size(67, 17);
            this.selRadioBut.TabIndex = 27;
            this.selRadioBut.TabStop = true;
            this.selRadioBut.Text = "Selected";
            this.selRadioBut.UseVisualStyleBackColor = true;
            // 
            // tabRadioBut
            // 
            this.tabRadioBut.AutoSize = true;
            this.tabRadioBut.Location = new System.Drawing.Point(241, 261);
            this.tabRadioBut.Name = "tabRadioBut";
            this.tabRadioBut.Size = new System.Drawing.Size(44, 17);
            this.tabRadioBut.TabIndex = 28;
            this.tabRadioBut.Text = "Tab";
            this.tabRadioBut.UseVisualStyleBackColor = true;
            this.tabRadioBut.CheckedChanged += new System.EventHandler(this.tabRadioBut_CheckedChanged);
            // 
            // biofBox
            // 
            this.biofBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.biofBox.AutoSize = true;
            this.biofBox.Checked = true;
            this.biofBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.biofBox.Location = new System.Drawing.Point(88, 241);
            this.biofBox.Name = "biofBox";
            this.biofBox.Size = new System.Drawing.Size(97, 17);
            this.biofBox.TabIndex = 29;
            this.biofBox.Text = "Use Bioformats";
            this.biofBox.UseVisualStyleBackColor = true;
            this.biofBox.CheckedChanged += new System.EventHandler(this.biofBox_CheckedChanged);
            // 
            // BioConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(439, 283);
            this.Controls.Add(this.biofBox);
            this.Controls.Add(this.tabRadioBut);
            this.Controls.Add(this.selRadioBut);
            this.Controls.Add(this.topMostBox);
            this.Controls.Add(this.headlessBox);
            this.Controls.Add(this.imagejBut);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.runBut);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BioConsole";
            this.Text = "Bio & ImageJ Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BioConsole_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BioConsole_KeyDown);
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

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button runBut;
        private System.Windows.Forms.TextBox consoleBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button imagejBut;
        private System.Windows.Forms.CheckBox headlessBox;
        private System.Windows.Forms.CheckBox topMostBox;
        private System.Windows.Forms.RadioButton selRadioBut;
        private System.Windows.Forms.RadioButton tabRadioBut;
        private System.Windows.Forms.CheckBox biofBox;
    }
}