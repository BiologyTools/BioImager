
namespace Bio
{
    partial class Setup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setup));
            this.label1 = new System.Windows.Forms.Label();
            this.imgPathBox = new System.Windows.Forms.TextBox();
            this.setImagingBut = new System.Windows.Forms.Button();
            this.setLibraryBut = new System.Windows.Forms.Button();
            this.libraryPathBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.okBut = new System.Windows.Forms.Button();
            this.pythonRadioBut = new System.Windows.Forms.RadioButton();
            this.libRadioBut = new System.Windows.Forms.RadioButton();
            this.cancelBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Imaging Application";
            // 
            // imgPathBox
            // 
            this.imgPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgPathBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.imgPathBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgPathBox.ForeColor = System.Drawing.Color.White;
            this.imgPathBox.Location = new System.Drawing.Point(108, 7);
            this.imgPathBox.Name = "imgPathBox";
            this.imgPathBox.Size = new System.Drawing.Size(230, 20);
            this.imgPathBox.TabIndex = 29;
            // 
            // setImagingBut
            // 
            this.setImagingBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setImagingBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setImagingBut.ForeColor = System.Drawing.Color.White;
            this.setImagingBut.Location = new System.Drawing.Point(344, 7);
            this.setImagingBut.Name = "setImagingBut";
            this.setImagingBut.Size = new System.Drawing.Size(51, 20);
            this.setImagingBut.TabIndex = 30;
            this.setImagingBut.Text = "Set";
            this.setImagingBut.UseVisualStyleBackColor = false;
            this.setImagingBut.Click += new System.EventHandler(this.setImagingBut_Click);
            // 
            // setLibraryBut
            // 
            this.setLibraryBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setLibraryBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setLibraryBut.ForeColor = System.Drawing.Color.White;
            this.setLibraryBut.Location = new System.Drawing.Point(344, 33);
            this.setLibraryBut.Name = "setLibraryBut";
            this.setLibraryBut.Size = new System.Drawing.Size(51, 20);
            this.setLibraryBut.TabIndex = 33;
            this.setLibraryBut.Text = "Set";
            this.setLibraryBut.UseVisualStyleBackColor = false;
            this.setLibraryBut.Click += new System.EventHandler(this.setLibraryBut_Click);
            // 
            // libraryPathBox
            // 
            this.libraryPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.libraryPathBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.libraryPathBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.libraryPathBox.ForeColor = System.Drawing.Color.White;
            this.libraryPathBox.Location = new System.Drawing.Point(108, 33);
            this.libraryPathBox.Name = "libraryPathBox";
            this.libraryPathBox.Size = new System.Drawing.Size(230, 20);
            this.libraryPathBox.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Library Path";
            // 
            // okBut
            // 
            this.okBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.okBut.ForeColor = System.Drawing.Color.White;
            this.okBut.Location = new System.Drawing.Point(328, 95);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(67, 23);
            this.okBut.TabIndex = 34;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = false;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // pythonRadioBut
            // 
            this.pythonRadioBut.AutoSize = true;
            this.pythonRadioBut.Location = new System.Drawing.Point(6, 62);
            this.pythonRadioBut.Name = "pythonRadioBut";
            this.pythonRadioBut.Size = new System.Drawing.Size(138, 17);
            this.pythonRadioBut.TabIndex = 35;
            this.pythonRadioBut.Text = "Use Python Microscope";
            this.pythonRadioBut.UseVisualStyleBackColor = true;
            this.pythonRadioBut.CheckedChanged += new System.EventHandler(this.pythonRadioBut_CheckedChanged);
            // 
            // libRadioBut
            // 
            this.libRadioBut.AutoSize = true;
            this.libRadioBut.Checked = true;
            this.libRadioBut.Location = new System.Drawing.Point(150, 62);
            this.libRadioBut.Name = "libRadioBut";
            this.libRadioBut.Size = new System.Drawing.Size(78, 17);
            this.libRadioBut.TabIndex = 36;
            this.libRadioBut.TabStop = true;
            this.libRadioBut.Text = "Use Library";
            this.libRadioBut.UseVisualStyleBackColor = true;
            this.libRadioBut.CheckedChanged += new System.EventHandler(this.libRadioBut_CheckedChanged);
            // 
            // cancelBut
            // 
            this.cancelBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.ForeColor = System.Drawing.Color.White;
            this.cancelBut.Location = new System.Drawing.Point(255, 95);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(67, 23);
            this.cancelBut.TabIndex = 37;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = false;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // Setup
            // 
            this.AcceptButton = this.okBut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(399, 125);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.libRadioBut);
            this.Controls.Add(this.pythonRadioBut);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.setLibraryBut);
            this.Controls.Add(this.libraryPathBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.setImagingBut);
            this.Controls.Add(this.imgPathBox);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setup";
            this.Text = "Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox imgPathBox;
        private System.Windows.Forms.Button setImagingBut;
        private System.Windows.Forms.Button setLibraryBut;
        private System.Windows.Forms.TextBox libraryPathBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.RadioButton pythonRadioBut;
        private System.Windows.Forms.RadioButton libRadioBut;
        private System.Windows.Forms.Button cancelBut;
    }
}