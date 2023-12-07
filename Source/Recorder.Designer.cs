namespace BioImager
{
    partial class Recorder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recorder));
            this.clearBut = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.delLineBut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.topMostBox = new System.Windows.Forms.CheckBox();
            this.microRecBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // clearBut
            // 
            this.clearBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.clearBut.Location = new System.Drawing.Point(368, 271);
            this.clearBut.Name = "clearBut";
            this.clearBut.Size = new System.Drawing.Size(75, 23);
            this.clearBut.TabIndex = 3;
            this.clearBut.TabStop = false;
            this.clearBut.Text = "Clear";
            this.clearBut.UseVisualStyleBackColor = false;
            this.clearBut.Click += new System.EventHandler(this.clearBut_Click);
            // 
            // textBox
            // 
            this.textBox.AcceptsTab = true;
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(12, 28);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(431, 237);
            this.textBox.TabIndex = 4;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // delLineBut
            // 
            this.delLineBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delLineBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.delLineBut.Location = new System.Drawing.Point(287, 271);
            this.delLineBut.Name = "delLineBut";
            this.delLineBut.Size = new System.Drawing.Size(75, 23);
            this.delLineBut.TabIndex = 5;
            this.delLineBut.TabStop = false;
            this.delLineBut.Text = "Delete Line";
            this.delLineBut.UseVisualStyleBackColor = false;
            this.delLineBut.Click += new System.EventHandler(this.delLineBut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Use these lines in a script by pasting these lines in script editor\'s \"Load\" meth" +
    "od.";
            // 
            // topMostBox
            // 
            this.topMostBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.topMostBox.AutoSize = true;
            this.topMostBox.Checked = true;
            this.topMostBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topMostBox.ForeColor = System.Drawing.Color.White;
            this.topMostBox.Location = new System.Drawing.Point(15, 271);
            this.topMostBox.Name = "topMostBox";
            this.topMostBox.Size = new System.Drawing.Size(71, 17);
            this.topMostBox.TabIndex = 7;
            this.topMostBox.Text = "Top Most";
            this.topMostBox.UseVisualStyleBackColor = true;
            this.topMostBox.CheckedChanged += new System.EventHandler(this.topMostBox_CheckedChanged);
            // 
            // microRecBox
            // 
            this.microRecBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.microRecBox.AutoSize = true;
            this.microRecBox.Checked = true;
            this.microRecBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.microRecBox.ForeColor = System.Drawing.Color.White;
            this.microRecBox.Location = new System.Drawing.Point(92, 271);
            this.microRecBox.Name = "microRecBox";
            this.microRecBox.Size = new System.Drawing.Size(119, 17);
            this.microRecBox.TabIndex = 8;
            this.microRecBox.Text = "Record Microscope";
            this.microRecBox.UseVisualStyleBackColor = true;
            this.microRecBox.CheckedChanged += new System.EventHandler(this.microRecBox_CheckedChanged);
            // 
            // Recorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(455, 306);
            this.Controls.Add(this.microRecBox);
            this.Controls.Add(this.topMostBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delLineBut);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.clearBut);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Recorder";
            this.Text = "Recorder";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.Recorder_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Recorder_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button clearBut;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button delLineBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox topMostBox;
        private System.Windows.Forms.CheckBox microRecBox;
    }
}