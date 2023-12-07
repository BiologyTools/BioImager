
namespace BioImager
{
    partial class RangeTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RangeTool));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeMinBox = new System.Windows.Forms.NumericUpDown();
            this.zMinBox = new System.Windows.Forms.NumericUpDown();
            this.zMaxBox = new System.Windows.Forms.NumericUpDown();
            this.timeMaxBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cMaxBox = new System.Windows.Forms.NumericUpDown();
            this.cMinBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timeMinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zMinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zMaxBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeMaxBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cMaxBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cMinBox)).BeginInit();
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
            // timeMinBox
            // 
            this.timeMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.timeMinBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.timeMinBox, "timeMinBox");
            this.timeMinBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.timeMinBox.Name = "timeMinBox";
            // 
            // zMinBox
            // 
            this.zMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.zMinBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.zMinBox, "zMinBox");
            this.zMinBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.zMinBox.Name = "zMinBox";
            // 
            // zMaxBox
            // 
            this.zMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.zMaxBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.zMaxBox, "zMaxBox");
            this.zMaxBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.zMaxBox.Name = "zMaxBox";
            // 
            // timeMaxBox
            // 
            this.timeMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.timeMaxBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.timeMaxBox, "timeMaxBox");
            this.timeMaxBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.timeMaxBox.Name = "timeMaxBox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // cMaxBox
            // 
            this.cMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cMaxBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cMaxBox, "cMaxBox");
            this.cMaxBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cMaxBox.Name = "cMaxBox";
            // 
            // cMinBox
            // 
            this.cMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cMinBox.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cMinBox, "cMinBox");
            this.cMinBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cMinBox.Name = "cMinBox";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Name = "label5";
            // 
            // RangeTool
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.Controls.Add(this.cMaxBox);
            this.Controls.Add(this.cMinBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.zMaxBox);
            this.Controls.Add(this.timeMaxBox);
            this.Controls.Add(this.zMinBox);
            this.Controls.Add(this.timeMinBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "RangeTool";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RangeTool_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.timeMinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zMinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zMaxBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeMaxBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cMaxBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cMinBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown timeMinBox;
        private System.Windows.Forms.NumericUpDown zMinBox;
        private System.Windows.Forms.NumericUpDown zMaxBox;
        private System.Windows.Forms.NumericUpDown timeMaxBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown cMaxBox;
        private System.Windows.Forms.NumericUpDown cMinBox;
        private System.Windows.Forms.Label label5;
    }
}