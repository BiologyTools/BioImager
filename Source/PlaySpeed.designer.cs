
namespace Bio
{
    partial class PlaySpeed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaySpeed));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zPlayspeed = new System.Windows.Forms.NumericUpDown();
            this.tPlayspeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cPlayspeed = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.zPlayspeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPlayspeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPlayspeed)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // zPlayspeed
            // 
            this.zPlayspeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.zPlayspeed.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.zPlayspeed, "zPlayspeed");
            this.zPlayspeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.zPlayspeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zPlayspeed.Name = "zPlayspeed";
            this.zPlayspeed.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // tPlayspeed
            // 
            this.tPlayspeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tPlayspeed.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tPlayspeed, "tPlayspeed");
            this.tPlayspeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tPlayspeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tPlayspeed.Name = "tPlayspeed";
            this.tPlayspeed.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // cPlayspeed
            // 
            this.cPlayspeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cPlayspeed.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cPlayspeed, "cPlayspeed");
            this.cPlayspeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cPlayspeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cPlayspeed.Name = "cPlayspeed";
            this.cPlayspeed.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Name = "label9";
            // 
            // PlaySpeed
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cPlayspeed);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tPlayspeed);
            this.Controls.Add(this.zPlayspeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PlaySpeed";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlaySpeed_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.zPlayspeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPlayspeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPlayspeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown zPlayspeed;
        private System.Windows.Forms.NumericUpDown tPlayspeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown cPlayspeed;
        private System.Windows.Forms.Label label9;
    }
}