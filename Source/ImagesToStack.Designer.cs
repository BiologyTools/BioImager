
namespace BioImager
{
    partial class ImagesToStack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagesToStack));
            this.okBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.zBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cBox = new System.Windows.Forms.NumericUpDown();
            this.tBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.zBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okBut
            // 
            this.okBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.okBut.ForeColor = System.Drawing.Color.White;
            this.okBut.Location = new System.Drawing.Point(113, 86);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(74, 25);
            this.okBut.TabIndex = 18;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = false;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // cancelBut
            // 
            this.cancelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cancelBut.ForeColor = System.Drawing.Color.White;
            this.cancelBut.Location = new System.Drawing.Point(33, 86);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(74, 25);
            this.cancelBut.TabIndex = 17;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = false;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(57, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Size C";
            // 
            // zBox
            // 
            this.zBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.zBox.ForeColor = System.Drawing.Color.White;
            this.zBox.Location = new System.Drawing.Point(100, 12);
            this.zBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.zBox.Name = "zBox";
            this.zBox.Size = new System.Drawing.Size(76, 20);
            this.zBox.TabIndex = 19;
            this.zBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(57, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Size Z";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(57, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Size T";
            // 
            // cBox
            // 
            this.cBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cBox.ForeColor = System.Drawing.Color.White;
            this.cBox.Location = new System.Drawing.Point(100, 38);
            this.cBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.cBox.Name = "cBox";
            this.cBox.Size = new System.Drawing.Size(76, 20);
            this.cBox.TabIndex = 24;
            this.cBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tBox
            // 
            this.tBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.tBox.ForeColor = System.Drawing.Color.White;
            this.tBox.Location = new System.Drawing.Point(100, 60);
            this.tBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.tBox.Name = "tBox";
            this.tBox.Size = new System.Drawing.Size(76, 20);
            this.tBox.TabIndex = 25;
            this.tBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ImagesToStack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(199, 122);
            this.Controls.Add(this.tBox);
            this.Controls.Add(this.cBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.zBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.cancelBut);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImagesToStack";
            this.Text = "Images To Stack";
            ((System.ComponentModel.ISupportInitialize)(this.zBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown zBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown cBox;
        private System.Windows.Forms.NumericUpDown tBox;
    }
}