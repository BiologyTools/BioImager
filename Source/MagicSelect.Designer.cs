namespace BioImager
{
    partial class MagicSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagicSelect));
            this.label1 = new System.Windows.Forms.Label();
            this.thBox = new System.Windows.Forms.ComboBox();
            this.numBox = new System.Windows.Forms.NumericUpDown();
            this.numericBox = new System.Windows.Forms.CheckBox();
            this.minBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maxBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Threshold";
            // 
            // thBox
            // 
            this.thBox.FormattingEnabled = true;
            this.thBox.Items.AddRange(new object[] {
            "Min",
            "Median",
            "Median - Min"});
            this.thBox.Location = new System.Drawing.Point(95, 6);
            this.thBox.Name = "thBox";
            this.thBox.Size = new System.Drawing.Size(121, 21);
            this.thBox.TabIndex = 1;
            // 
            // numBox
            // 
            this.numBox.Location = new System.Drawing.Point(95, 33);
            this.numBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numBox.Name = "numBox";
            this.numBox.Size = new System.Drawing.Size(120, 20);
            this.numBox.TabIndex = 2;
            // 
            // numericBox
            // 
            this.numericBox.AutoSize = true;
            this.numericBox.ForeColor = System.Drawing.Color.White;
            this.numericBox.Location = new System.Drawing.Point(12, 34);
            this.numericBox.Name = "numericBox";
            this.numericBox.Size = new System.Drawing.Size(65, 17);
            this.numericBox.TabIndex = 3;
            this.numericBox.Text = "Numeric";
            this.numericBox.UseVisualStyleBackColor = true;
            this.numericBox.CheckedChanged += new System.EventHandler(this.numericBox_CheckedChanged);
            // 
            // minBox
            // 
            this.minBox.Location = new System.Drawing.Point(95, 59);
            this.minBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.minBox.Name = "minBox";
            this.minBox.Size = new System.Drawing.Size(120, 20);
            this.minBox.TabIndex = 4;
            this.minBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Min";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Max";
            // 
            // maxBox
            // 
            this.maxBox.Location = new System.Drawing.Point(95, 85);
            this.maxBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.maxBox.Name = "maxBox";
            this.maxBox.Size = new System.Drawing.Size(120, 20);
            this.maxBox.TabIndex = 6;
            this.maxBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // MagicSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(224, 110);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minBox);
            this.Controls.Add(this.numericBox);
            this.Controls.Add(this.numBox);
            this.Controls.Add(this.thBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MagicSelect";
            this.ShowInTaskbar = false;
            this.Text = "Magic Select";
            ((System.ComponentModel.ISupportInitialize)(this.numBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox thBox;
        private System.Windows.Forms.NumericUpDown numBox;
        private System.Windows.Forms.CheckBox numericBox;
        private System.Windows.Forms.NumericUpDown minBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown maxBox;
    }
}