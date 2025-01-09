namespace BioImager
{
    partial class OMERO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OMERO));
            listView1 = new ListView();
            comboBox1 = new ComboBox();
            searchBox = new TextBox();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            loadIconsBox = new CheckBox();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.BackColor = Color.FromArgb(49, 91, 138);
            listView1.Location = new Point(0, 54);
            listView1.Name = "listView1";
            listView1.Size = new Size(800, 396);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.SmallIcon;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.BackColor = Color.FromArgb(49, 91, 138);
            comboBox1.ForeColor = Color.White;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(0, 28);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(800, 23);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // searchBox
            // 
            searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchBox.BackColor = Color.FromArgb(49, 91, 138);
            searchBox.ForeColor = Color.White;
            searchBox.Location = new Point(2, 3);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(690, 23);
            searchBox.TabIndex = 2;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel, progressBar });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 17);
            // 
            // progressBar
            // 
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(100, 16);
            // 
            // loadIconsBox
            // 
            loadIconsBox.AutoSize = true;
            loadIconsBox.Checked = true;
            loadIconsBox.CheckState = CheckState.Checked;
            loadIconsBox.ForeColor = Color.White;
            loadIconsBox.Location = new Point(698, 5);
            loadIconsBox.Name = "loadIconsBox";
            loadIconsBox.Size = new Size(83, 19);
            loadIconsBox.TabIndex = 4;
            loadIconsBox.Text = "Load Icons";
            loadIconsBox.UseVisualStyleBackColor = true;
            // 
            // OMERO
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(loadIconsBox);
            Controls.Add(statusStrip1);
            Controls.Add(searchBox);
            Controls.Add(comboBox1);
            Controls.Add(listView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OMERO";
            Text = "OMERO";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ComboBox comboBox1;
        private TextBox searchBox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar progressBar;
        private CheckBox loadIconsBox;
    }
}