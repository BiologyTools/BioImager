namespace BioImager
{
    partial class Layers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Layers));
            layersBox = new ListView();
            SuspendLayout();
            // 
            // layersBox
            // 
            layersBox.BackColor = Color.FromArgb(49, 91, 138);
            layersBox.Dock = DockStyle.Fill;
            layersBox.ForeColor = Color.White;
            layersBox.Location = new Point(0, 0);
            layersBox.Name = "layersBox";
            layersBox.Size = new Size(168, 197);
            layersBox.TabIndex = 0;
            layersBox.UseCompatibleStateImageBehavior = false;
            layersBox.ItemChecked += layersBox_ItemChecked;
            // 
            // Layers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(168, 197);
            Controls.Add(layersBox);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Layers";
            Text = "Layers";
            Activated += Layers_Activated;
            ResumeLayout(false);
        }

        #endregion

        private ListView layersBox;
    }
}