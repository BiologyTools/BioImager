
namespace BioImager
{
    partial class BioConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BioConsole));
            textBox = new TextBox();
            runBut = new Button();
            consoleBox = new TextBox();
            splitContainer = new SplitContainer();
            imagejBut = new Button();
            headlessBox = new CheckBox();
            topMostBox = new CheckBox();
            selRadioBut = new RadioButton();
            tabRadioBut = new RadioButton();
            biofBox = new CheckBox();
            predLabel = new Label();
            newTabBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.AcceptsTab = true;
            textBox.BackColor = Color.FromArgb(49, 91, 138);
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBox.ForeColor = Color.White;
            textBox.Location = new Point(0, 0);
            textBox.Margin = new Padding(4, 3, 4, 3);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.Size = new Size(644, 84);
            textBox.TabIndex = 18;
            textBox.TextChanged += textBox_TextChanged;
            // 
            // runBut
            // 
            runBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            runBut.BackColor = Color.FromArgb(49, 91, 138);
            runBut.ForeColor = Color.White;
            runBut.Location = new Point(585, 308);
            runBut.Margin = new Padding(4, 3, 4, 3);
            runBut.Name = "runBut";
            runBut.Size = new Size(65, 27);
            runBut.TabIndex = 21;
            runBut.Text = "Run";
            runBut.UseVisualStyleBackColor = false;
            runBut.Click += runBut_Click;
            // 
            // consoleBox
            // 
            consoleBox.BackColor = Color.FromArgb(49, 91, 138);
            consoleBox.Dock = DockStyle.Fill;
            consoleBox.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            consoleBox.ForeColor = Color.White;
            consoleBox.Location = new Point(0, 0);
            consoleBox.Margin = new Padding(4, 3, 4, 3);
            consoleBox.Multiline = true;
            consoleBox.Name = "consoleBox";
            consoleBox.Size = new Size(644, 189);
            consoleBox.TabIndex = 22;
            // 
            // splitContainer
            // 
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer.Location = new Point(7, 3);
            splitContainer.Margin = new Padding(4, 3, 4, 3);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(consoleBox);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(textBox);
            splitContainer.Size = new Size(644, 278);
            splitContainer.SplitterDistance = 189;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 23;
            // 
            // imagejBut
            // 
            imagejBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            imagejBut.BackColor = Color.FromArgb(49, 91, 138);
            imagejBut.ForeColor = Color.White;
            imagejBut.Location = new Point(493, 308);
            imagejBut.Margin = new Padding(4, 3, 4, 3);
            imagejBut.Name = "imagejBut";
            imagejBut.Size = new Size(85, 27);
            imagejBut.TabIndex = 24;
            imagejBut.Text = "Run ImageJ";
            imagejBut.UseVisualStyleBackColor = false;
            imagejBut.Click += imagejBut_Click;
            // 
            // headlessBox
            // 
            headlessBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            headlessBox.AutoSize = true;
            headlessBox.Location = new Point(90, 313);
            headlessBox.Margin = new Padding(4, 3, 4, 3);
            headlessBox.Name = "headlessBox";
            headlessBox.Size = new Size(73, 19);
            headlessBox.TabIndex = 25;
            headlessBox.Text = "Headless";
            headlessBox.UseVisualStyleBackColor = true;
            headlessBox.CheckedChanged += headlessBox_CheckedChanged;
            // 
            // topMostBox
            // 
            topMostBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            topMostBox.AutoSize = true;
            topMostBox.Location = new Point(7, 313);
            topMostBox.Margin = new Padding(4, 3, 4, 3);
            topMostBox.Name = "topMostBox";
            topMostBox.Size = new Size(75, 19);
            topMostBox.TabIndex = 26;
            topMostBox.Text = "Top Most";
            topMostBox.UseVisualStyleBackColor = true;
            topMostBox.CheckedChanged += topMostBox_CheckedChanged;
            // 
            // selRadioBut
            // 
            selRadioBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            selRadioBut.AutoSize = true;
            selRadioBut.Checked = true;
            selRadioBut.Location = new Point(366, 312);
            selRadioBut.Margin = new Padding(4, 3, 4, 3);
            selRadioBut.Name = "selRadioBut";
            selRadioBut.Size = new Size(69, 19);
            selRadioBut.TabIndex = 27;
            selRadioBut.TabStop = true;
            selRadioBut.Text = "Selected";
            selRadioBut.UseVisualStyleBackColor = true;
            // 
            // tabRadioBut
            // 
            tabRadioBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tabRadioBut.AutoSize = true;
            tabRadioBut.Location = new Point(443, 312);
            tabRadioBut.Margin = new Padding(4, 3, 4, 3);
            tabRadioBut.Name = "tabRadioBut";
            tabRadioBut.Size = new Size(43, 19);
            tabRadioBut.TabIndex = 28;
            tabRadioBut.Text = "Tab";
            tabRadioBut.UseVisualStyleBackColor = true;
            tabRadioBut.CheckedChanged += tabRadioBut_CheckedChanged;
            // 
            // biofBox
            // 
            biofBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            biofBox.AutoSize = true;
            biofBox.Checked = true;
            biofBox.CheckState = CheckState.Checked;
            biofBox.Location = new Point(256, 313);
            biofBox.Margin = new Padding(4, 3, 4, 3);
            biofBox.Name = "biofBox";
            biofBox.Size = new Size(106, 19);
            biofBox.TabIndex = 29;
            biofBox.Text = "Use Bioformats";
            biofBox.UseVisualStyleBackColor = true;
            biofBox.CheckedChanged += biofBox_CheckedChanged;
            // 
            // predLabel
            // 
            predLabel.AutoSize = true;
            predLabel.Location = new Point(7, 280);
            predLabel.Name = "predLabel";
            predLabel.Size = new Size(0, 15);
            predLabel.TabIndex = 30;
            // 
            // newTabBox
            // 
            newTabBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            newTabBox.AutoSize = true;
            newTabBox.Location = new Point(171, 313);
            newTabBox.Margin = new Padding(4, 3, 4, 3);
            newTabBox.Name = "newTabBox";
            newTabBox.Size = new Size(71, 19);
            newTabBox.TabIndex = 31;
            newTabBox.Text = "New Tab";
            newTabBox.UseVisualStyleBackColor = true;
            newTabBox.CheckedChanged += newTabBox_CheckedChanged;
            // 
            // BioConsole
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            ClientSize = new Size(656, 337);
            Controls.Add(newTabBox);
            Controls.Add(predLabel);
            Controls.Add(biofBox);
            Controls.Add(tabRadioBut);
            Controls.Add(selRadioBut);
            Controls.Add(topMostBox);
            Controls.Add(headlessBox);
            Controls.Add(imagejBut);
            Controls.Add(splitContainer);
            Controls.Add(runBut);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "BioConsole";
            Text = "Bio & ImageJ Console";
            FormClosing += BioConsole_FormClosing;
            KeyDown += BioConsole_KeyDown;
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button runBut;
        private System.Windows.Forms.TextBox consoleBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button imagejBut;
        private System.Windows.Forms.CheckBox headlessBox;
        private System.Windows.Forms.CheckBox topMostBox;
        private System.Windows.Forms.RadioButton selRadioBut;
        private System.Windows.Forms.RadioButton tabRadioBut;
        private System.Windows.Forms.CheckBox biofBox;
        private Label predLabel;
        private CheckBox newTabBox;
    }
}