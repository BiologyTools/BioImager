namespace Bio
{
    partial class ColorTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorTool));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.redBox = new System.Windows.Forms.NumericUpDown();
            this.greenBox = new System.Windows.Forms.NumericUpDown();
            this.blueBox = new System.Windows.Forms.NumericUpDown();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.rEnbaled = new System.Windows.Forms.CheckBox();
            this.gEnabled = new System.Windows.Forms.CheckBox();
            this.bEnabled = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.redBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "R:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "G:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "B:";
            // 
            // redBox
            // 
            this.redBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.redBox.ForeColor = System.Drawing.Color.White;
            this.redBox.Location = new System.Drawing.Point(36, 7);
            this.redBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.redBox.Name = "redBox";
            this.redBox.Size = new System.Drawing.Size(80, 20);
            this.redBox.TabIndex = 3;
            this.redBox.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.redBox.ValueChanged += new System.EventHandler(this.redBox_ValueChanged);
            // 
            // greenBox
            // 
            this.greenBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.greenBox.ForeColor = System.Drawing.Color.White;
            this.greenBox.Location = new System.Drawing.Point(36, 33);
            this.greenBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.greenBox.Name = "greenBox";
            this.greenBox.Size = new System.Drawing.Size(80, 20);
            this.greenBox.TabIndex = 4;
            this.greenBox.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.greenBox.ValueChanged += new System.EventHandler(this.greenBox_ValueChanged);
            // 
            // blueBox
            // 
            this.blueBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.blueBox.ForeColor = System.Drawing.Color.White;
            this.blueBox.Location = new System.Drawing.Point(35, 60);
            this.blueBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.blueBox.Name = "blueBox";
            this.blueBox.Size = new System.Drawing.Size(80, 20);
            this.blueBox.TabIndex = 5;
            this.blueBox.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.blueBox.ValueChanged += new System.EventHandler(this.blueBox_ValueChanged);
            // 
            // colorPanel
            // 
            this.colorPanel.Location = new System.Drawing.Point(143, 12);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(64, 64);
            this.colorPanel.TabIndex = 6;
            // 
            // rEnbaled
            // 
            this.rEnbaled.AutoSize = true;
            this.rEnbaled.Checked = true;
            this.rEnbaled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rEnbaled.Location = new System.Drawing.Point(122, 9);
            this.rEnbaled.Name = "rEnbaled";
            this.rEnbaled.Size = new System.Drawing.Size(15, 14);
            this.rEnbaled.TabIndex = 7;
            this.rEnbaled.UseVisualStyleBackColor = true;
            this.rEnbaled.CheckedChanged += new System.EventHandler(this.rEnbaled_CheckedChanged);
            // 
            // gEnabled
            // 
            this.gEnabled.AutoSize = true;
            this.gEnabled.Checked = true;
            this.gEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gEnabled.Location = new System.Drawing.Point(122, 35);
            this.gEnabled.Name = "gEnabled";
            this.gEnabled.Size = new System.Drawing.Size(15, 14);
            this.gEnabled.TabIndex = 8;
            this.gEnabled.UseVisualStyleBackColor = true;
            this.gEnabled.CheckedChanged += new System.EventHandler(this.gEnabled_CheckedChanged);
            // 
            // bEnabled
            // 
            this.bEnabled.AutoSize = true;
            this.bEnabled.Checked = true;
            this.bEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bEnabled.Location = new System.Drawing.Point(121, 62);
            this.bEnabled.Name = "bEnabled";
            this.bEnabled.Size = new System.Drawing.Size(15, 14);
            this.bEnabled.TabIndex = 9;
            this.bEnabled.UseVisualStyleBackColor = true;
            this.bEnabled.CheckedChanged += new System.EventHandler(this.bEnabled_CheckedChanged);
            // 
            // ColorTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(217, 88);
            this.Controls.Add(this.bEnabled);
            this.Controls.Add(this.gEnabled);
            this.Controls.Add(this.rEnbaled);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.blueBox);
            this.Controls.Add(this.greenBox);
            this.Controls.Add(this.redBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColorTool";
            this.Text = "Color Tool";
            ((System.ComponentModel.ISupportInitialize)(this.redBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown redBox;
        private System.Windows.Forms.NumericUpDown greenBox;
        private System.Windows.Forms.NumericUpDown blueBox;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.CheckBox rEnbaled;
        private System.Windows.Forms.CheckBox gEnabled;
        private System.Windows.Forms.CheckBox bEnabled;
    }
}