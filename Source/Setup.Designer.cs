
namespace BioImager
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
            label1 = new Label();
            imgPathBox = new TextBox();
            setImagingBut = new Button();
            setLibraryBut = new Button();
            libraryPathBox = new TextBox();
            label2 = new Label();
            okBut = new Button();
            pythonRadioBut = new RadioButton();
            libRadioBut = new RadioButton();
            cancelBut = new Button();
            micromanRadioBut = new RadioButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 13);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(115, 15);
            label1.TabIndex = 0;
            label1.Text = "Imaging Application";
            // 
            // imgPathBox
            // 
            imgPathBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            imgPathBox.BackColor = Color.FromArgb(49, 91, 138);
            imgPathBox.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            imgPathBox.ForeColor = Color.White;
            imgPathBox.Location = new Point(126, 8);
            imgPathBox.Margin = new Padding(4, 3, 4, 3);
            imgPathBox.Name = "imgPathBox";
            imgPathBox.Size = new Size(268, 20);
            imgPathBox.TabIndex = 29;
            // 
            // setImagingBut
            // 
            setImagingBut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            setImagingBut.BackColor = Color.FromArgb(49, 91, 138);
            setImagingBut.ForeColor = Color.White;
            setImagingBut.Location = new Point(401, 8);
            setImagingBut.Margin = new Padding(4, 3, 4, 3);
            setImagingBut.Name = "setImagingBut";
            setImagingBut.Size = new Size(59, 23);
            setImagingBut.TabIndex = 30;
            setImagingBut.Text = "Set";
            setImagingBut.UseVisualStyleBackColor = false;
            setImagingBut.Click += setImagingBut_Click;
            // 
            // setLibraryBut
            // 
            setLibraryBut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            setLibraryBut.BackColor = Color.FromArgb(49, 91, 138);
            setLibraryBut.ForeColor = Color.White;
            setLibraryBut.Location = new Point(401, 38);
            setLibraryBut.Margin = new Padding(4, 3, 4, 3);
            setLibraryBut.Name = "setLibraryBut";
            setLibraryBut.Size = new Size(59, 23);
            setLibraryBut.TabIndex = 33;
            setLibraryBut.Text = "Set";
            setLibraryBut.UseVisualStyleBackColor = false;
            setLibraryBut.Click += setLibraryBut_Click;
            // 
            // libraryPathBox
            // 
            libraryPathBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            libraryPathBox.BackColor = Color.FromArgb(49, 91, 138);
            libraryPathBox.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            libraryPathBox.ForeColor = Color.White;
            libraryPathBox.Location = new Point(126, 38);
            libraryPathBox.Margin = new Padding(4, 3, 4, 3);
            libraryPathBox.Name = "libraryPathBox";
            libraryPathBox.Size = new Size(268, 20);
            libraryPathBox.TabIndex = 32;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 43);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 31;
            label2.Text = "Library Path";
            // 
            // okBut
            // 
            okBut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            okBut.BackColor = Color.FromArgb(49, 91, 138);
            okBut.ForeColor = Color.White;
            okBut.Location = new Point(383, 110);
            okBut.Margin = new Padding(4, 3, 4, 3);
            okBut.Name = "okBut";
            okBut.Size = new Size(78, 27);
            okBut.TabIndex = 34;
            okBut.Text = "OK";
            okBut.UseVisualStyleBackColor = false;
            okBut.Click += okBut_Click;
            // 
            // pythonRadioBut
            // 
            pythonRadioBut.AutoSize = true;
            pythonRadioBut.Location = new Point(7, 72);
            pythonRadioBut.Margin = new Padding(4, 3, 4, 3);
            pythonRadioBut.Name = "pythonRadioBut";
            pythonRadioBut.Size = new Size(150, 19);
            pythonRadioBut.TabIndex = 35;
            pythonRadioBut.Text = "Use Python Microscope";
            pythonRadioBut.UseVisualStyleBackColor = true;
            pythonRadioBut.CheckedChanged += pythonRadioBut_CheckedChanged;
            // 
            // libRadioBut
            // 
            libRadioBut.AutoSize = true;
            libRadioBut.Checked = true;
            libRadioBut.Location = new Point(298, 72);
            libRadioBut.Margin = new Padding(4, 3, 4, 3);
            libRadioBut.Name = "libRadioBut";
            libRadioBut.Size = new Size(83, 19);
            libRadioBut.TabIndex = 36;
            libRadioBut.TabStop = true;
            libRadioBut.Text = "Use Library";
            libRadioBut.UseVisualStyleBackColor = true;
            libRadioBut.CheckedChanged += libRadioBut_CheckedChanged;
            // 
            // cancelBut
            // 
            cancelBut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cancelBut.BackColor = Color.FromArgb(49, 91, 138);
            cancelBut.DialogResult = DialogResult.Cancel;
            cancelBut.ForeColor = Color.White;
            cancelBut.Location = new Point(298, 110);
            cancelBut.Margin = new Padding(4, 3, 4, 3);
            cancelBut.Name = "cancelBut";
            cancelBut.Size = new Size(78, 27);
            cancelBut.TabIndex = 37;
            cancelBut.Text = "Cancel";
            cancelBut.UseVisualStyleBackColor = false;
            cancelBut.Click += cancelBut_Click;
            // 
            // micromanRadioBut
            // 
            micromanRadioBut.AutoSize = true;
            micromanRadioBut.Location = new Point(165, 72);
            micromanRadioBut.Margin = new Padding(4, 3, 4, 3);
            micromanRadioBut.Name = "micromanRadioBut";
            micromanRadioBut.Size = new Size(130, 19);
            micromanRadioBut.TabIndex = 38;
            micromanRadioBut.Text = "Use Micro-Manager";
            micromanRadioBut.UseVisualStyleBackColor = true;
            micromanRadioBut.CheckedChanged += micromanRadioBut_CheckedChanged;
            // 
            // Setup
            // 
            AcceptButton = okBut;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            CancelButton = cancelBut;
            ClientSize = new Size(465, 144);
            Controls.Add(micromanRadioBut);
            Controls.Add(cancelBut);
            Controls.Add(libRadioBut);
            Controls.Add(pythonRadioBut);
            Controls.Add(okBut);
            Controls.Add(setLibraryBut);
            Controls.Add(libraryPathBox);
            Controls.Add(label2);
            Controls.Add(setImagingBut);
            Controls.Add(imgPathBox);
            Controls.Add(label1);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Setup";
            Text = "Setup";
            ResumeLayout(false);
            PerformLayout();
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
        private RadioButton micromanRadioBut;
    }
}