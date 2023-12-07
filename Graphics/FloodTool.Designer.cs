namespace BioImager.Graphics
{
    partial class FloodTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FloodTool));
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
            this.cancelBut = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.tolBBox = new System.Windows.Forms.NumericUpDown();
            this.tolGBox = new System.Windows.Forms.NumericUpDown();
            this.tolRBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bBar = new System.Windows.Forms.TrackBar();
            this.rBar = new System.Windows.Forms.TrackBar();
            this.gBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.redBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tolBBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tolGBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tolRBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gBar)).BeginInit();
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
            this.redBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
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
            this.greenBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
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
            this.blueBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
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
            // cancelBut
            // 
            this.cancelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.ForeColor = System.Drawing.Color.White;
            this.cancelBut.Location = new System.Drawing.Point(52, 270);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(75, 23);
            this.cancelBut.TabIndex = 168;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = false;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Location = new System.Drawing.Point(133, 270);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 167;
            this.applyButton.Text = "OK";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // tolBBox
            // 
            this.tolBBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tolBBox.ForeColor = System.Drawing.Color.White;
            this.tolBBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tolBBox.Location = new System.Drawing.Point(35, 242);
            this.tolBBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.tolBBox.Name = "tolBBox";
            this.tolBBox.Size = new System.Drawing.Size(80, 20);
            this.tolBBox.TabIndex = 178;
            this.tolBBox.ValueChanged += new System.EventHandler(this.tolBBox_ValueChanged);
            // 
            // tolGBox
            // 
            this.tolGBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tolGBox.ForeColor = System.Drawing.Color.White;
            this.tolGBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tolGBox.Location = new System.Drawing.Point(36, 215);
            this.tolGBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.tolGBox.Name = "tolGBox";
            this.tolGBox.Size = new System.Drawing.Size(80, 20);
            this.tolGBox.TabIndex = 177;
            this.tolGBox.ValueChanged += new System.EventHandler(this.tolGBox_ValueChanged);
            // 
            // tolRBox
            // 
            this.tolRBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tolRBox.ForeColor = System.Drawing.Color.White;
            this.tolRBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tolRBox.Location = new System.Drawing.Point(36, 189);
            this.tolRBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.tolRBox.Name = "tolRBox";
            this.tolRBox.Size = new System.Drawing.Size(80, 20);
            this.tolRBox.TabIndex = 176;
            this.tolRBox.ValueChanged += new System.EventHandler(this.tolRBox_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 175;
            this.label5.Text = "B:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 174;
            this.label6.Text = "G:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 173;
            this.label7.Text = "R:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(13, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 179;
            this.label8.Text = "Tolerance";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 185;
            this.label4.Text = "B:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 184;
            this.label9.Text = "G:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(12, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 183;
            this.label10.Text = "R:";
            // 
            // bBar
            // 
            this.bBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bBar.AutoSize = false;
            this.bBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.bBar.LargeChange = 1000;
            this.bBar.Location = new System.Drawing.Point(32, 133);
            this.bBar.Margin = new System.Windows.Forms.Padding(0);
            this.bBar.Maximum = 65535;
            this.bBar.Name = "bBar";
            this.bBar.Size = new System.Drawing.Size(176, 25);
            this.bBar.TabIndex = 181;
            this.bBar.TickFrequency = 2000;
            this.bBar.ValueChanged += new System.EventHandler(this.rBar_ValueChanged);
            // 
            // rBar
            // 
            this.rBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rBar.AutoSize = false;
            this.rBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.rBar.LargeChange = 1000;
            this.rBar.Location = new System.Drawing.Point(32, 83);
            this.rBar.Margin = new System.Windows.Forms.Padding(0);
            this.rBar.Maximum = 65535;
            this.rBar.Name = "rBar";
            this.rBar.Size = new System.Drawing.Size(176, 25);
            this.rBar.TabIndex = 180;
            this.rBar.TickFrequency = 2000;
            this.rBar.ValueChanged += new System.EventHandler(this.rBar_ValueChanged);
            // 
            // gBar
            // 
            this.gBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBar.AutoSize = false;
            this.gBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.gBar.LargeChange = 1000;
            this.gBar.Location = new System.Drawing.Point(32, 108);
            this.gBar.Margin = new System.Windows.Forms.Padding(0);
            this.gBar.Maximum = 65535;
            this.gBar.Name = "gBar";
            this.gBar.Size = new System.Drawing.Size(176, 25);
            this.gBar.TabIndex = 182;
            this.gBar.TickFrequency = 2000;
            this.gBar.ValueChanged += new System.EventHandler(this.rBar_ValueChanged);
            // 
            // FloodTool
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(217, 300);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bBar);
            this.Controls.Add(this.rBar);
            this.Controls.Add(this.gBar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tolBBox);
            this.Controls.Add(this.tolGBox);
            this.Controls.Add(this.tolRBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.applyButton);
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
            this.Name = "FloodTool";
            this.Text = "Flood Tool";
            ((System.ComponentModel.ISupportInitialize)(this.redBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tolBBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tolGBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tolRBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gBar)).EndInit();
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
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.NumericUpDown tolBBox;
        private System.Windows.Forms.NumericUpDown tolGBox;
        private System.Windows.Forms.NumericUpDown tolRBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar bBar;
        private System.Windows.Forms.TrackBar rBar;
        private System.Windows.Forms.TrackBar gBar;
    }
}