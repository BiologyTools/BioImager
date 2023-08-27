
namespace Bio
{
    partial class FunctionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FunctionForm));
            this.stateBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.label12 = new System.Windows.Forms.Label();
            this.valBox = new System.Windows.Forms.NumericUpDown();
            this.performBut = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.imageJRadioBut = new System.Windows.Forms.RadioButton();
            this.bioRadioBut = new System.Windows.Forms.RadioButton();
            this.menuPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.funcsBox = new System.Windows.Forms.ComboBox();
            this.microBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.contextMenuPath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.valBox)).BeginInit();
            this.SuspendLayout();
            // 
            // stateBox
            // 
            this.stateBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.stateBox.ForeColor = System.Drawing.Color.White;
            this.stateBox.FormattingEnabled = true;
            this.stateBox.Location = new System.Drawing.Point(66, 126);
            this.stateBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(101, 23);
            this.stateBox.TabIndex = 24;
            this.stateBox.SelectedIndexChanged += new System.EventHandler(this.stateBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(-1, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "State:";
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Location = new System.Drawing.Point(274, 453);
            this.applyButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(88, 27);
            this.applyButton.TabIndex = 22;
            this.applyButton.Text = "OK";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Key:";
            // 
            // modifierBox
            // 
            this.modifierBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.modifierBox.ForeColor = System.Drawing.Color.White;
            this.modifierBox.FormattingEnabled = true;
            this.modifierBox.Location = new System.Drawing.Point(239, 126);
            this.modifierBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.modifierBox.Name = "modifierBox";
            this.modifierBox.Size = new System.Drawing.Size(119, 23);
            this.modifierBox.TabIndex = 16;
            this.modifierBox.SelectedIndexChanged += new System.EventHandler(this.modifierBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(177, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Modifier:";
            // 
            // keysBox
            // 
            this.keysBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.keysBox.ForeColor = System.Drawing.Color.White;
            this.keysBox.FormattingEnabled = true;
            this.keysBox.Location = new System.Drawing.Point(66, 95);
            this.keysBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.keysBox.Name = "keysBox";
            this.keysBox.Size = new System.Drawing.Size(292, 23);
            this.keysBox.TabIndex = 14;
            this.keysBox.SelectedIndexChanged += new System.EventHandler(this.keysBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 160);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "Property:";
            // 
            // propBox
            // 
            this.propBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.propBox.ForeColor = System.Drawing.Color.White;
            this.propBox.FormattingEnabled = true;
            this.propBox.Location = new System.Drawing.Point(66, 157);
            this.propBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.propBox.Name = "propBox";
            this.propBox.Size = new System.Drawing.Size(292, 23);
            this.propBox.TabIndex = 26;
            this.propBox.SelectedIndexChanged += new System.EventHandler(this.propBox_SelectedIndexChanged);
            // 
            // recBox
            // 
            this.recBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.recBox.ForeColor = System.Drawing.Color.White;
            this.recBox.FormattingEnabled = true;
            this.recBox.Location = new System.Drawing.Point(66, 186);
            this.recBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.recBox.Name = "recBox";
            this.recBox.Size = new System.Drawing.Size(292, 23);
            this.recBox.TabIndex = 28;
            this.recBox.SelectedIndexChanged += new System.EventHandler(this.recBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 189);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 27;
            this.label7.Text = "Recording:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(2, 428);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 15);
            this.label8.TabIndex = 29;
            this.label8.Text = "ImageJ Macro:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(2, 459);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "Bio Script:";
            // 
            // setMacroFileBut
            // 
            this.setMacroFileBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setMacroFileBut.ForeColor = System.Drawing.Color.White;
            this.setMacroFileBut.Location = new System.Drawing.Point(92, 422);
            this.setMacroFileBut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.setMacroFileBut.Name = "setMacroFileBut";
            this.setMacroFileBut.Size = new System.Drawing.Size(84, 27);
            this.setMacroFileBut.TabIndex = 32;
            this.setMacroFileBut.Text = "Set File";
            this.setMacroFileBut.UseVisualStyleBackColor = false;
            this.setMacroFileBut.Click += new System.EventHandler(this.setMacroFileBut_Click);
            // 
            // setScriptFileBut
            // 
            this.setScriptFileBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.setScriptFileBut.ForeColor = System.Drawing.Color.White;
            this.setScriptFileBut.Location = new System.Drawing.Point(92, 453);
            this.setScriptFileBut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.setScriptFileBut.Name = "setScriptFileBut";
            this.setScriptFileBut.Size = new System.Drawing.Size(84, 27);
            this.setScriptFileBut.TabIndex = 33;
            this.setScriptFileBut.Text = "Set File";
            this.setScriptFileBut.UseVisualStyleBackColor = false;
            this.setScriptFileBut.Click += new System.EventHandler(this.setScriptFileBut_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 15);
            this.label10.TabIndex = 34;
            this.label10.Text = "Name:";
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.nameBox.ForeColor = System.Drawing.Color.White;
            this.nameBox.Location = new System.Drawing.Point(66, 36);
            this.nameBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(292, 23);
            this.nameBox.TabIndex = 165;
            this.nameBox.Text = "Function1";
            // 
            // cancelBut
            // 
            this.cancelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.ForeColor = System.Drawing.Color.White;
            this.cancelBut.Location = new System.Drawing.Point(183, 453);
            this.cancelBut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(88, 27);
            this.cancelBut.TabIndex = 166;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = false;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(0, 219);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 15);
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
            this.valBox.Location = new System.Drawing.Point(66, 217);
            this.valBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.valBox.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.valBox.Name = "valBox";
            this.valBox.Size = new System.Drawing.Size(293, 23);
            this.valBox.TabIndex = 171;
            this.valBox.ValueChanged += new System.EventHandler(this.valBox_ValueChanged);
            // 
            // performBut
            // 
            this.performBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.performBut.ForeColor = System.Drawing.Color.White;
            this.performBut.Location = new System.Drawing.Point(274, 422);
            this.performBut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.performBut.Name = "performBut";
            this.performBut.Size = new System.Drawing.Size(88, 27);
            this.performBut.TabIndex = 172;
            this.performBut.Text = "Perform";
            this.performBut.UseVisualStyleBackColor = false;
            this.performBut.Click += new System.EventHandler(this.performBut_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-1, 252);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 173;
            this.label3.Text = "Script:";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.textBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(66, 252);
            this.textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(292, 86);
            this.textBox.TabIndex = 174;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // imageJRadioBut
            // 
            this.imageJRadioBut.AutoSize = true;
            this.imageJRadioBut.Checked = true;
            this.imageJRadioBut.Location = new System.Drawing.Point(239, 342);
            this.imageJRadioBut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.imageJRadioBut.Name = "imageJRadioBut";
            this.imageJRadioBut.Size = new System.Drawing.Size(62, 19);
            this.imageJRadioBut.TabIndex = 175;
            this.imageJRadioBut.TabStop = true;
            this.imageJRadioBut.Text = "ImageJ";
            this.imageJRadioBut.UseVisualStyleBackColor = true;
            this.imageJRadioBut.CheckedChanged += new System.EventHandler(this.imageJRadioBut_CheckedChanged);
            // 
            // bioRadioBut
            // 
            this.bioRadioBut.AutoSize = true;
            this.bioRadioBut.Location = new System.Drawing.Point(315, 342);
            this.bioRadioBut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bioRadioBut.Name = "bioRadioBut";
            this.bioRadioBut.Size = new System.Drawing.Size(42, 19);
            this.bioRadioBut.TabIndex = 176;
            this.bioRadioBut.Text = "Bio";
            this.bioRadioBut.UseVisualStyleBackColor = true;
            this.bioRadioBut.CheckedChanged += new System.EventHandler(this.bioRadioBut_CheckedChanged);
            // 
            // menuPath
            // 
            this.menuPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.menuPath.ForeColor = System.Drawing.Color.White;
            this.menuPath.Location = new System.Drawing.Point(128, 368);
            this.menuPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.menuPath.Name = "menuPath";
            this.menuPath.Size = new System.Drawing.Size(233, 23);
            this.menuPath.TabIndex = 178;
            this.menuPath.TextChanged += new System.EventHandler(this.menuPath_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(2, 372);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 177;
            this.label4.Text = "Menu Path /:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(2, 10);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 15);
            this.label11.TabIndex = 179;
            this.label11.Text = "Functions:";
            // 
            // funcsBox
            // 
            this.funcsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.funcsBox.ForeColor = System.Drawing.Color.White;
            this.funcsBox.FormattingEnabled = true;
            this.funcsBox.Location = new System.Drawing.Point(66, 7);
            this.funcsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.funcsBox.Name = "funcsBox";
            this.funcsBox.Size = new System.Drawing.Size(292, 23);
            this.funcsBox.TabIndex = 180;
            this.funcsBox.SelectedIndexChanged += new System.EventHandler(this.funcsBox_SelectedIndexChanged);
            // 
            // microBox
            // 
            this.microBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.microBox.ForeColor = System.Drawing.Color.White;
            this.microBox.FormattingEnabled = true;
            this.microBox.Location = new System.Drawing.Point(66, 63);
            this.microBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.microBox.Name = "microBox";
            this.microBox.Size = new System.Drawing.Size(293, 23);
            this.microBox.TabIndex = 182;
            this.microBox.SelectedIndexChanged += new System.EventHandler(this.microBox_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, 67);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 15);
            this.label13.TabIndex = 181;
            this.label13.Text = "Type:";
            // 
            // contextMenuPath
            // 
            this.contextMenuPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.contextMenuPath.ForeColor = System.Drawing.Color.White;
            this.contextMenuPath.Location = new System.Drawing.Point(128, 396);
            this.contextMenuPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.contextMenuPath.Name = "contextMenuPath";
            this.contextMenuPath.Size = new System.Drawing.Size(233, 23);
            this.contextMenuPath.TabIndex = 184;
            this.contextMenuPath.TextChanged += new System.EventHandler(this.contextMenuPath_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(2, 399);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 15);
            this.label14.TabIndex = 183;
            this.label14.Text = "Context Menu Path /:";
            // 
            // FunctionForm
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(370, 489);
            this.Controls.Add(this.contextMenuPath);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.microBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.funcsBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.menuPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bioRadioBut);
            this.Controls.Add(this.imageJRadioBut);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.performBut);
            this.Controls.Add(this.valBox);
            this.Controls.Add(this.label12);
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
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modifierBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.keysBox);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FunctionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Functions";
            this.Activated += new System.EventHandler(this.FunctionForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FunctionForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.valBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox stateBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown valBox;
        private System.Windows.Forms.Button performBut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.RadioButton imageJRadioBut;
        private System.Windows.Forms.RadioButton bioRadioBut;
        private System.Windows.Forms.TextBox menuPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox funcsBox;
        private System.Windows.Forms.ComboBox microBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox contextMenuPath;
        private System.Windows.Forms.Label label14;
    }
}