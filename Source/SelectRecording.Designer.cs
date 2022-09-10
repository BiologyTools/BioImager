
namespace Bio
{
    partial class SelectRecording
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectRecording));
            this.okBut = new System.Windows.Forms.Button();
            this.ObjectiveLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.recsBox = new System.Windows.Forms.ComboBox();
            this.propsBox = new System.Windows.Forms.ComboBox();
            this.cancelBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // okBut
            // 
            this.okBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.okBut.ForeColor = System.Drawing.Color.White;
            this.okBut.Location = new System.Drawing.Point(259, 60);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(65, 23);
            this.okBut.TabIndex = 194;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = false;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // ObjectiveLabel
            // 
            this.ObjectiveLabel.AutoSize = true;
            this.ObjectiveLabel.ForeColor = System.Drawing.Color.White;
            this.ObjectiveLabel.Location = new System.Drawing.Point(12, 9);
            this.ObjectiveLabel.Name = "ObjectiveLabel";
            this.ObjectiveLabel.Size = new System.Drawing.Size(61, 13);
            this.ObjectiveLabel.TabIndex = 195;
            this.ObjectiveLabel.Text = "Recordings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 196;
            this.label1.Text = "Properties";
            // 
            // recsBox
            // 
            this.recsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.recsBox.ForeColor = System.Drawing.Color.White;
            this.recsBox.FormattingEnabled = true;
            this.recsBox.Location = new System.Drawing.Point(79, 5);
            this.recsBox.Name = "recsBox";
            this.recsBox.Size = new System.Drawing.Size(245, 21);
            this.recsBox.TabIndex = 197;
            this.recsBox.SelectedIndexChanged += new System.EventHandler(this.recsBox_SelectedIndexChanged);
            // 
            // propsBox
            // 
            this.propsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.propsBox.ForeColor = System.Drawing.Color.White;
            this.propsBox.FormattingEnabled = true;
            this.propsBox.Location = new System.Drawing.Point(79, 32);
            this.propsBox.Name = "propsBox";
            this.propsBox.Size = new System.Drawing.Size(245, 21);
            this.propsBox.TabIndex = 198;
            this.propsBox.SelectedIndexChanged += new System.EventHandler(this.propsBox_SelectedIndexChanged);
            // 
            // cancelBut
            // 
            this.cancelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.ForeColor = System.Drawing.Color.White;
            this.cancelBut.Location = new System.Drawing.Point(188, 60);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(65, 23);
            this.cancelBut.TabIndex = 199;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = false;
            // 
            // SelectRecording
            // 
            this.AcceptButton = this.okBut;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(336, 91);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.propsBox);
            this.Controls.Add(this.recsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ObjectiveLabel);
            this.Controls.Add(this.okBut);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SelectRecording";
            this.Text = "Select Recording or Property";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.Label ObjectiveLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox recsBox;
        private System.Windows.Forms.ComboBox propsBox;
        private System.Windows.Forms.Button cancelBut;
    }
}