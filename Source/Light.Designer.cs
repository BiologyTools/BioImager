namespace Bio.Source
{
    partial class Light
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Light));
            this.lightBox = new System.Windows.Forms.ComboBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.percentLabel = new System.Windows.Forms.Label();
            this.tlShutterBox = new System.Windows.Forms.CheckBox();
            this.rlShutterBox = new System.Windows.Forms.CheckBox();
            this.hxpShutterBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.filterWheelBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // lightBox
            // 
            this.lightBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.lightBox.FormattingEnabled = true;
            this.lightBox.Location = new System.Drawing.Point(85, 12);
            this.lightBox.Name = "lightBox";
            this.lightBox.Size = new System.Drawing.Size(244, 21);
            this.lightBox.TabIndex = 0;
            this.lightBox.SelectedIndexChanged += new System.EventHandler(this.lightBox_SelectedIndexChanged);
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(2, 49);
            this.trackBar.Maximum = 100;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(305, 45);
            this.trackBar.TabIndex = 1;
            this.trackBar.TickFrequency = 10;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Light Source";
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Location = new System.Drawing.Point(308, 53);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(21, 13);
            this.percentLabel.TabIndex = 3;
            this.percentLabel.Text = "0%";
            // 
            // tlShutterBox
            // 
            this.tlShutterBox.AutoSize = true;
            this.tlShutterBox.Location = new System.Drawing.Point(12, 88);
            this.tlShutterBox.Name = "tlShutterBox";
            this.tlShutterBox.Size = new System.Drawing.Size(76, 17);
            this.tlShutterBox.TabIndex = 4;
            this.tlShutterBox.Text = "TL Shutter";
            this.tlShutterBox.UseVisualStyleBackColor = true;
            this.tlShutterBox.CheckedChanged += new System.EventHandler(this.tlShutterBox_CheckedChanged);
            // 
            // rlShutterBox
            // 
            this.rlShutterBox.AutoSize = true;
            this.rlShutterBox.Location = new System.Drawing.Point(94, 88);
            this.rlShutterBox.Name = "rlShutterBox";
            this.rlShutterBox.Size = new System.Drawing.Size(77, 17);
            this.rlShutterBox.TabIndex = 5;
            this.rlShutterBox.Text = "RL Shutter";
            this.rlShutterBox.UseVisualStyleBackColor = true;
            this.rlShutterBox.CheckedChanged += new System.EventHandler(this.rlShutterBox_CheckedChanged);
            // 
            // hxpShutterBox
            // 
            this.hxpShutterBox.AutoSize = true;
            this.hxpShutterBox.Location = new System.Drawing.Point(177, 88);
            this.hxpShutterBox.Name = "hxpShutterBox";
            this.hxpShutterBox.Size = new System.Drawing.Size(85, 17);
            this.hxpShutterBox.TabIndex = 6;
            this.hxpShutterBox.Text = "HXP Shutter";
            this.hxpShutterBox.UseVisualStyleBackColor = true;
            this.hxpShutterBox.CheckedChanged += new System.EventHandler(this.hxpShutterBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Filter Wheel";
            // 
            // filterWheelBox
            // 
            this.filterWheelBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.filterWheelBox.FormattingEnabled = true;
            this.filterWheelBox.Location = new System.Drawing.Point(85, 118);
            this.filterWheelBox.Name = "filterWheelBox";
            this.filterWheelBox.Size = new System.Drawing.Size(244, 21);
            this.filterWheelBox.TabIndex = 8;
            // 
            // Light
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(341, 149);
            this.Controls.Add(this.filterWheelBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hxpShutterBox);
            this.Controls.Add(this.rlShutterBox);
            this.Controls.Add(this.tlShutterBox);
            this.Controls.Add(this.percentLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.lightBox);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Light";
            this.Text = "Light Path";
            this.Activated += new System.EventHandler(this.Light_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox lightBox;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label percentLabel;
        private System.Windows.Forms.CheckBox tlShutterBox;
        private System.Windows.Forms.CheckBox rlShutterBox;
        private System.Windows.Forms.CheckBox hxpShutterBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox filterWheelBox;
    }
}