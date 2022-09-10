
namespace Bio
{
    partial class ControllerFunc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControllerFunc));
            this.stateBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.objectivesBox = new System.Windows.Forms.ComboBox();
            this.modifierBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.keysBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.propBox = new System.Windows.Forms.ComboBox();
            this.recBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.setMacroFileBut = new System.Windows.Forms.Button();
            this.setScriptFileBut = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label10 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.cancelBut = new System.Windows.Forms.Button();
            this.microBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.valBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.valBox)).BeginInit();
            this.SuspendLayout();
            // 
            // stateBox
            // 
            this.stateBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.stateBox.ForeColor = System.Drawing.Color.White;
            this.stateBox.FormattingEnabled = true;
            this.stateBox.Location = new System.Drawing.Point(47, 61);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(103, 21);
            this.stateBox.TabIndex = 24;
            this.stateBox.SelectedIndexChanged += new System.EventHandler(this.stateBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(5, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "State:";
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Location = new System.Drawing.Point(239, 245);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 22;
            this.applyButton.Text = "OK";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Objective:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Key:";
            // 
            // objectivesBox
            // 
            this.objectivesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.objectivesBox.ForeColor = System.Drawing.Color.White;
            this.objectivesBox.FormattingEnabled = true;
            this.objectivesBox.Location = new System.Drawing.Point(83, 88);
            this.objectivesBox.Name = "objectivesBox";
            this.objectivesBox.Size = new System.Drawing.Size(231, 21);
            this.objectivesBox.TabIndex = 17;
            this.objectivesBox.SelectedIndexChanged += new System.EventHandler(this.objectivesBox_SelectedIndexChanged);
            // 
            // modifierBox
            // 
            this.modifierBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.modifierBox.ForeColor = System.Drawing.Color.White;
            this.modifierBox.FormattingEnabled = true;
            this.modifierBox.Location = new System.Drawing.Point(211, 61);
            this.modifierBox.Name = "modifierBox";
            this.modifierBox.Size = new System.Drawing.Size(103, 21);
            this.modifierBox.TabIndex = 16;
            this.modifierBox.SelectedIndexChanged += new System.EventHandler(this.modifierBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(158, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Modifier:";
            // 
            // keysBox
            // 
            this.keysBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.keysBox.ForeColor = System.Drawing.Color.White;
            this.keysBox.FormattingEnabled = true;
            this.keysBox.Location = new System.Drawing.Point(47, 34);
            this.keysBox.Name = "keysBox";
            this.keysBox.Size = new System.Drawing.Size(267, 21);
            this.keysBox.TabIndex = 14;
            this.keysBox.SelectedIndexChanged += new System.EventHandler(this.keysBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(6, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Property:";
            // 
            // propBox
            // 
            this.propBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.propBox.ForeColor = System.Drawing.Color.White;
            this.propBox.FormattingEnabled = true;
            this.propBox.Location = new System.Drawing.Point(83, 113);
            this.propBox.Name = "propBox";
            this.propBox.Size = new System.Drawing.Size(231, 21);
            this.propBox.TabIndex = 26;
            this.propBox.SelectedIndexChanged += new System.EventHandler(this.propBox_SelectedIndexChanged);
            // 
            // recBox
            // 
            this.recBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.recBox.ForeColor = System.Drawing.Color.White;
            this.recBox.FormattingEnabled = true;
            this.recBox.Location = new System.Drawing.Point(83, 138);
            this.recBox.Name = "recBox";
            this.recBox.Size = new System.Drawing.Size(231, 21);
            this.recBox.TabIndex = 28;
            this.recBox.SelectedIndexChanged += new System.EventHandler(this.recBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(6, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Recording:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(6, 223);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "ImageJ Macro:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(6, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Bio Script:";
            // 
            // setMacroFileBut
            // 
            this.setMacroFileBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setMacroFileBut.ForeColor = System.Drawing.Color.White;
            this.setMacroFileBut.Location = new System.Drawing.Point(83, 218);
            this.setMacroFileBut.Name = "setMacroFileBut";
            this.setMacroFileBut.Size = new System.Drawing.Size(72, 23);
            this.setMacroFileBut.TabIndex = 32;
            this.setMacroFileBut.Text = "Set File";
            this.setMacroFileBut.UseVisualStyleBackColor = false;
            this.setMacroFileBut.Click += new System.EventHandler(this.setMacroFileBut_Click);
            // 
            // setScriptFileBut
            // 
            this.setScriptFileBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setScriptFileBut.ForeColor = System.Drawing.Color.White;
            this.setScriptFileBut.Location = new System.Drawing.Point(83, 245);
            this.setScriptFileBut.Name = "setScriptFileBut";
            this.setScriptFileBut.Size = new System.Drawing.Size(72, 23);
            this.setScriptFileBut.TabIndex = 33;
            this.setScriptFileBut.Text = "Set File";
            this.setScriptFileBut.UseVisualStyleBackColor = false;
            this.setScriptFileBut.Click += new System.EventHandler(this.setScriptFileBut_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(6, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Name:";
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.nameBox.ForeColor = System.Drawing.Color.White;
            this.nameBox.Location = new System.Drawing.Point(47, 6);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(267, 20);
            this.nameBox.TabIndex = 165;
            this.nameBox.Text = "Function1";
            // 
            // cancelBut
            // 
            this.cancelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.ForeColor = System.Drawing.Color.White;
            this.cancelBut.Location = new System.Drawing.Point(161, 245);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(75, 23);
            this.cancelBut.TabIndex = 166;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = false;
            // 
            // microBox
            // 
            this.microBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.microBox.ForeColor = System.Drawing.Color.White;
            this.microBox.FormattingEnabled = true;
            this.microBox.Location = new System.Drawing.Point(83, 164);
            this.microBox.Name = "microBox";
            this.microBox.Size = new System.Drawing.Size(231, 21);
            this.microBox.TabIndex = 169;
            this.microBox.SelectedIndexChanged += new System.EventHandler(this.microBox_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(6, 167);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 168;
            this.label11.Text = "Microscope:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(6, 194);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 170;
            this.label12.Text = "Value:";
            // 
            // valBox
            // 
            this.valBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.valBox.DecimalPlaces = 4;
            this.valBox.ForeColor = System.Drawing.Color.White;
            this.valBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.valBox.Location = new System.Drawing.Point(83, 192);
            this.valBox.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.valBox.Name = "valBox";
            this.valBox.Size = new System.Drawing.Size(231, 20);
            this.valBox.TabIndex = 171;
            this.valBox.ValueChanged += new System.EventHandler(this.valBox_ValueChanged);
            // 
            // ControllerFunc
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(320, 274);
            this.Controls.Add(this.valBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.microBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.setScriptFileBut);
            this.Controls.Add(this.setMacroFileBut);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.recBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.propBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.stateBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.objectivesBox);
            this.Controls.Add(this.modifierBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.keysBox);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ControllerFunc";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Function";
            ((System.ComponentModel.ISupportInitialize)(this.valBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox stateBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox objectivesBox;
        private System.Windows.Forms.ComboBox modifierBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox keysBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox propBox;
        private System.Windows.Forms.ComboBox recBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button setMacroFileBut;
        private System.Windows.Forms.Button setScriptFileBut;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.ComboBox microBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown valBox;
    }
}