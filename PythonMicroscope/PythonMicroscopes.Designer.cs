
namespace Bio.PythonMicroscope
{
    partial class PythonMicroscopes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PythonMicroscopes));
            this.saveBut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.filterWheelBox = new System.Windows.Forms.TextBox();
            this.stageBox = new System.Windows.Forms.TextBox();
            this.startBut = new System.Windows.Forms.Button();
            this.editConfigBut = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.funcsBox = new System.Windows.Forms.ComboBox();
            this.pxBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cameraBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // saveBut
            // 
            this.saveBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.saveBut.ForeColor = System.Drawing.Color.White;
            this.saveBut.Location = new System.Drawing.Point(385, 442);
            this.saveBut.Name = "saveBut";
            this.saveBut.Size = new System.Drawing.Size(56, 23);
            this.saveBut.TabIndex = 22;
            this.saveBut.Text = "Save";
            this.saveBut.UseVisualStyleBackColor = false;
            this.saveBut.Click += new System.EventHandler(this.saveBut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Filterwheel URI:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Stage URI:";
            // 
            // filterWheelBox
            // 
            this.filterWheelBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterWheelBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.filterWheelBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterWheelBox.ForeColor = System.Drawing.Color.White;
            this.filterWheelBox.Location = new System.Drawing.Point(124, 62);
            this.filterWheelBox.Name = "filterWheelBox";
            this.filterWheelBox.Size = new System.Drawing.Size(317, 20);
            this.filterWheelBox.TabIndex = 28;
            this.filterWheelBox.Text = "PYRO:SimulatedFilterWheel@127.0.0.1:8001";
            this.filterWheelBox.TextChanged += new System.EventHandler(this.filterWheelBox_TextChanged);
            // 
            // stageBox
            // 
            this.stageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.stageBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stageBox.ForeColor = System.Drawing.Color.White;
            this.stageBox.Location = new System.Drawing.Point(124, 84);
            this.stageBox.Name = "stageBox";
            this.stageBox.Size = new System.Drawing.Size(317, 20);
            this.stageBox.TabIndex = 29;
            this.stageBox.Text = "PYRO:SimulatedStage@127.0.0.1:8002";
            this.stageBox.TextChanged += new System.EventHandler(this.stageBox_TextChanged);
            // 
            // startBut
            // 
            this.startBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.startBut.ForeColor = System.Drawing.Color.White;
            this.startBut.Location = new System.Drawing.Point(137, 442);
            this.startBut.Name = "startBut";
            this.startBut.Size = new System.Drawing.Size(77, 23);
            this.startBut.TabIndex = 30;
            this.startBut.Text = "Start Server";
            this.startBut.UseVisualStyleBackColor = false;
            this.startBut.Click += new System.EventHandler(this.initBut_Click);
            // 
            // editConfigBut
            // 
            this.editConfigBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editConfigBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.editConfigBut.ForeColor = System.Drawing.Color.White;
            this.editConfigBut.Location = new System.Drawing.Point(12, 442);
            this.editConfigBut.Name = "editConfigBut";
            this.editConfigBut.Size = new System.Drawing.Size(119, 23);
            this.editConfigBut.TabIndex = 31;
            this.editConfigBut.Text = "Device Server Config.";
            this.editConfigBut.UseVisualStyleBackColor = false;
            this.editConfigBut.Click += new System.EventHandler(this.editConfigBut_Click);
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(15, 137);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(426, 299);
            this.panel.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Python Functions:";
            // 
            // funcsBox
            // 
            this.funcsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.funcsBox.ForeColor = System.Drawing.Color.White;
            this.funcsBox.FormattingEnabled = true;
            this.funcsBox.Items.AddRange(new object[] {
            "getexposure.py",
            "getfilterwheel.py",
            "getstagexyz.py",
            "getstagexy.py",
            "takeimage.py",
            "setexposure.py",
            "setfilterwheel.py",
            "setstagexyz.py",
            "setstagexy.py",
            "initialize.py"});
            this.funcsBox.Location = new System.Drawing.Point(124, 106);
            this.funcsBox.Name = "funcsBox";
            this.funcsBox.Size = new System.Drawing.Size(317, 21);
            this.funcsBox.TabIndex = 36;
            this.funcsBox.SelectedIndexChanged += new System.EventHandler(this.funcsBox_SelectedIndexChanged);
            // 
            // pxBox
            // 
            this.pxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.pxBox.ForeColor = System.Drawing.Color.White;
            this.pxBox.FormattingEnabled = true;
            this.pxBox.Items.AddRange(new object[] {
            "Format8bppIndexed",
            "Format16bppGrayScale",
            "Format24bppRgb",
            "Format32bppArgb",
            "Format48bppRgb"});
            this.pxBox.Location = new System.Drawing.Point(124, 9);
            this.pxBox.Name = "pxBox";
            this.pxBox.Size = new System.Drawing.Size(317, 21);
            this.pxBox.TabIndex = 37;
            this.pxBox.SelectedIndexChanged += new System.EventHandler(this.pxBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Camera Pixel Format:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Camera URI:";
            // 
            // cameraBox
            // 
            this.cameraBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(91)))), ((int)(((byte)(138)))));
            this.cameraBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraBox.ForeColor = System.Drawing.Color.White;
            this.cameraBox.Location = new System.Drawing.Point(124, 41);
            this.cameraBox.Name = "cameraBox";
            this.cameraBox.Size = new System.Drawing.Size(317, 20);
            this.cameraBox.TabIndex = 40;
            this.cameraBox.Text = "PYRO:SimulatedCamera@127.0.0.1:8000";
            // 
            // PythonMicroscopes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(449, 475);
            this.Controls.Add(this.cameraBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pxBox);
            this.Controls.Add(this.funcsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.editConfigBut);
            this.Controls.Add(this.startBut);
            this.Controls.Add(this.stageBox);
            this.Controls.Add(this.filterWheelBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saveBut);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PythonMicroscopes";
            this.Text = "Python Microscope Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox filterWheelBox;
        private System.Windows.Forms.TextBox stageBox;
        private System.Windows.Forms.Button startBut;
        private System.Windows.Forms.Button editConfigBut;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox funcsBox;
        private System.Windows.Forms.ComboBox pxBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox cameraBox;
    }
}