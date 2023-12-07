
namespace BioImager
{
    partial class ImageTiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageTiles));
            this.yBox = new System.Windows.Forms.NumericUpDown();
            this.xBox = new System.Windows.Forms.NumericUpDown();
            this.cancelBut = new System.Windows.Forms.Button();
            this.okBut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).BeginInit();
            this.SuspendLayout();
            // 
            // yBox
            // 
            this.yBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.yBox.ForeColor = System.Drawing.Color.White;
            this.yBox.Location = new System.Drawing.Point(130, 12);
            this.yBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.yBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(77, 20);
            this.yBox.TabIndex = 30;
            this.yBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // xBox
            // 
            this.xBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.xBox.ForeColor = System.Drawing.Color.White;
            this.xBox.Location = new System.Drawing.Point(27, 12);
            this.xBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.xBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(77, 20);
            this.xBox.TabIndex = 31;
            this.xBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cancelBut
            // 
            this.cancelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.Location = new System.Drawing.Point(53, 40);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(75, 23);
            this.cancelBut.TabIndex = 33;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = false;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // okBut
            // 
            this.okBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.okBut.Location = new System.Drawing.Point(134, 40);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(75, 23);
            this.okBut.TabIndex = 32;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = false;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "X";
            // 
            // ImageTiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(217, 75);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.yBox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImageTiles";
            this.Text = "Image Tiles";
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown yBox;
        private System.Windows.Forms.NumericUpDown xBox;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}