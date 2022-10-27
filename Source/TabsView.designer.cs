
namespace Bio
{
    partial class TabsView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabsView));
            this.openFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveOMEFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOMEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveCSVFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openCSVFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveTiffFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOMEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOMESeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImagesToTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImagesOMEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOMEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTabTiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTabViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filteredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOIManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportROIsOfFolderOfImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoThresholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsToolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stackToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bit8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bit16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.to24BitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.to36BitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.to48BitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptRunnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptRecorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.stackToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel.SuspendLayout();
            this.tabContextMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // openFilesDialog
            // 
            this.openFilesDialog.Multiselect = true;
            this.openFilesDialog.Title = "Open Images";
            // 
            // saveOMEFileDialog
            // 
            this.saveOMEFileDialog.DefaultExt = "ome.tif";
            this.saveOMEFileDialog.Filter = "OME TIFF Files (*.ome.tif)|*.ome.tif|All files (*.*)|*.*";
            this.saveOMEFileDialog.SupportMultiDottedExtensions = true;
            this.saveOMEFileDialog.Title = "Save Image";
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(156)))));
            this.panel.Controls.Add(this.tabControl);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 24);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(584, 417);
            this.panel.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.ContextMenuStrip = this.tabContextMenuStrip;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(584, 417);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabContextMenuStrip
            // 
            this.tabContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.saveOMEToolStripMenuItem1,
            this.toWindowToolStripMenuItem});
            this.tabContextMenuStrip.Name = "tabContextMenuStrip";
            this.tabContextMenuStrip.Size = new System.Drawing.Size(165, 92);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click_1);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveOMEToolStripMenuItem1
            // 
            this.saveOMEToolStripMenuItem1.Name = "saveOMEToolStripMenuItem1";
            this.saveOMEToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.saveOMEToolStripMenuItem1.Text = "Save OME";
            this.saveOMEToolStripMenuItem1.Click += new System.EventHandler(this.saveOMEToolStripMenuItem1_Click);
            // 
            // toWindowToolStripMenuItem
            // 
            this.toWindowToolStripMenuItem.Name = "toWindowToolStripMenuItem";
            this.toWindowToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.toWindowToolStripMenuItem.Text = "Open as Window";
            this.toWindowToolStripMenuItem.Click += new System.EventHandler(this.toWindowToolStripMenuItem_Click);
            // 
            // saveCSVFileDialog
            // 
            this.saveCSVFileDialog.DefaultExt = "csv";
            this.saveCSVFileDialog.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            this.saveCSVFileDialog.Title = "Save ROIs to CSV";
            // 
            // openCSVFileDialog
            // 
            this.openCSVFileDialog.DefaultExt = "csv";
            this.openCSVFileDialog.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            this.openCSVFileDialog.Title = "Import ROI from CSV";
            // 
            // saveTiffFileDialog
            // 
            this.saveTiffFileDialog.DefaultExt = "tif";
            this.saveTiffFileDialog.Filter = "TIFF Files (*.tif)|*.tif | PNG Files (*.png)|*.png | BMP Files (*.bmp)|*.bmp | JP" +
    "G Files (*.jpg)|*.jpg | Gif Files (*.gif)|*.gif";
            this.saveTiffFileDialog.SupportMultiDottedExtensions = true;
            this.saveTiffFileDialog.Title = "Save Image";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openRecentToolStripMenuItem,
            this.openOMEToolStripMenuItem,
            this.openOMESeriesToolStripMenuItem,
            this.openSeriesToolStripMenuItem,
            this.addImagesToTabToolStripMenuItem,
            this.addImagesOMEToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveOMEToolStripMenuItem,
            this.saveTabToolStripMenuItem,
            this.saveTabTiffToolStripMenuItem,
            this.saveSeriesToolStripMenuItem,
            this.newTabViewToolStripMenuItem,
            this.nodeViewToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openToolStripMenuItem.Text = "Open Images";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openRecentToolStripMenuItem
            // 
            this.openRecentToolStripMenuItem.Name = "openRecentToolStripMenuItem";
            this.openRecentToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openRecentToolStripMenuItem.Text = "Open Recent";
            this.openRecentToolStripMenuItem.DropDownOpening += new System.EventHandler(this.openRecentToolStripMenuItem_DropDownOpening);
            // 
            // openOMEToolStripMenuItem
            // 
            this.openOMEToolStripMenuItem.Name = "openOMEToolStripMenuItem";
            this.openOMEToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openOMEToolStripMenuItem.Text = "Open OME Images";
            this.openOMEToolStripMenuItem.Click += new System.EventHandler(this.openOMEToolStripMenuItem_Click_1);
            // 
            // openOMESeriesToolStripMenuItem
            // 
            this.openOMESeriesToolStripMenuItem.Name = "openOMESeriesToolStripMenuItem";
            this.openOMESeriesToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openOMESeriesToolStripMenuItem.Text = "Open OME Series";
            this.openOMESeriesToolStripMenuItem.Click += new System.EventHandler(this.openSeriesToolStripMenuItem_Click);
            // 
            // openSeriesToolStripMenuItem
            // 
            this.openSeriesToolStripMenuItem.Name = "openSeriesToolStripMenuItem";
            this.openSeriesToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openSeriesToolStripMenuItem.Text = "Open Series";
            this.openSeriesToolStripMenuItem.Click += new System.EventHandler(this.openSeriesToolStripMenuItem_Click_1);
            // 
            // addImagesToTabToolStripMenuItem
            // 
            this.addImagesToTabToolStripMenuItem.Name = "addImagesToTabToolStripMenuItem";
            this.addImagesToTabToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addImagesToTabToolStripMenuItem.Text = "Add Images To Tab";
            this.addImagesToTabToolStripMenuItem.Click += new System.EventHandler(this.addImagesToTabToolStripMenuItem_Click);
            // 
            // addImagesOMEToolStripMenuItem
            // 
            this.addImagesOMEToolStripMenuItem.Name = "addImagesOMEToolStripMenuItem";
            this.addImagesOMEToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addImagesOMEToolStripMenuItem.Text = "Add OME Images To Tab";
            this.addImagesOMEToolStripMenuItem.Click += new System.EventHandler(this.addImagesOMEToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveToolStripMenuItem.Text = "Save Selected Tiff";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveOMEToolStripMenuItem
            // 
            this.saveOMEToolStripMenuItem.Name = "saveOMEToolStripMenuItem";
            this.saveOMEToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveOMEToolStripMenuItem.Text = "Save Selected OME";
            this.saveOMEToolStripMenuItem.Click += new System.EventHandler(this.saveOMEToolStripMenuItem_Click);
            // 
            // saveTabToolStripMenuItem
            // 
            this.saveTabToolStripMenuItem.Name = "saveTabToolStripMenuItem";
            this.saveTabToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveTabToolStripMenuItem.Text = "Save Tab OME";
            this.saveTabToolStripMenuItem.Click += new System.EventHandler(this.saveTabToolStripMenuItem_Click);
            // 
            // saveTabTiffToolStripMenuItem
            // 
            this.saveTabTiffToolStripMenuItem.Name = "saveTabTiffToolStripMenuItem";
            this.saveTabTiffToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveTabTiffToolStripMenuItem.Text = "Save Tab Tiff";
            this.saveTabTiffToolStripMenuItem.Click += new System.EventHandler(this.saveTabTiffToolStripMenuItem_Click);
            // 
            // saveSeriesToolStripMenuItem
            // 
            this.saveSeriesToolStripMenuItem.Name = "saveSeriesToolStripMenuItem";
            this.saveSeriesToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveSeriesToolStripMenuItem.Text = "Save Series";
            this.saveSeriesToolStripMenuItem.Click += new System.EventHandler(this.saveSeriesToolStripMenuItem_Click);
            // 
            // newTabViewToolStripMenuItem
            // 
            this.newTabViewToolStripMenuItem.Name = "newTabViewToolStripMenuItem";
            this.newTabViewToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.newTabViewToolStripMenuItem.Text = "New Process";
            this.newTabViewToolStripMenuItem.Click += new System.EventHandler(this.newTabViewToolStripMenuItem_Click);
            // 
            // nodeViewToolStripMenuItem
            // 
            this.nodeViewToolStripMenuItem.Name = "nodeViewToolStripMenuItem";
            this.nodeViewToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.nodeViewToolStripMenuItem.Text = "Node View";
            this.nodeViewToolStripMenuItem.Click += new System.EventHandler(this.nodeViewToolStripMenuItem_Click);
            // 
            // sizeModeToolStripMenuItem
            // 
            this.sizeModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBToolStripMenuItem,
            this.filteredToolStripMenuItem,
            this.rawToolStripMenuItem});
            this.sizeModeToolStripMenuItem.Name = "sizeModeToolStripMenuItem";
            this.sizeModeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.sizeModeToolStripMenuItem.Text = "View";
            // 
            // rGBToolStripMenuItem
            // 
            this.rGBToolStripMenuItem.CheckOnClick = true;
            this.rGBToolStripMenuItem.Name = "rGBToolStripMenuItem";
            this.rGBToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.rGBToolStripMenuItem.Text = "RGB";
            this.rGBToolStripMenuItem.Click += new System.EventHandler(this.rGBToolStripMenuItem_Click);
            // 
            // filteredToolStripMenuItem
            // 
            this.filteredToolStripMenuItem.Checked = true;
            this.filteredToolStripMenuItem.CheckOnClick = true;
            this.filteredToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filteredToolStripMenuItem.Name = "filteredToolStripMenuItem";
            this.filteredToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.filteredToolStripMenuItem.Text = "Filtered";
            this.filteredToolStripMenuItem.Click += new System.EventHandler(this.filteredToolStripMenuItem_Click);
            // 
            // rawToolStripMenuItem
            // 
            this.rawToolStripMenuItem.CheckOnClick = true;
            this.rawToolStripMenuItem.Name = "rawToolStripMenuItem";
            this.rawToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.rawToolStripMenuItem.Text = "Raw";
            this.rawToolStripMenuItem.Click += new System.EventHandler(this.rawToolStripMenuItem_Click);
            // 
            // toolboxToolStripMenuItem
            // 
            this.toolboxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setToolToolStripMenuItem});
            this.toolboxToolStripMenuItem.Name = "toolboxToolStripMenuItem";
            this.toolboxToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolboxToolStripMenuItem.Text = "Tools";
            this.toolboxToolStripMenuItem.Click += new System.EventHandler(this.toolboxToolStripMenuItem_Click);
            // 
            // setToolToolStripMenuItem
            // 
            this.setToolToolStripMenuItem.Name = "setToolToolStripMenuItem";
            this.setToolToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.setToolToolStripMenuItem.Text = "Set Tool";
            this.setToolToolStripMenuItem.Click += new System.EventHandler(this.setToolToolStripMenuItem_Click);
            // 
            // rOIToolStripMenuItem
            // 
            this.rOIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rOIManagerToolStripMenuItem,
            this.exportCSVToolStripMenuItem,
            this.importCSVToolStripMenuItem,
            this.exportROIsOfFolderOfImagesToolStripMenuItem});
            this.rOIToolStripMenuItem.Name = "rOIToolStripMenuItem";
            this.rOIToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.rOIToolStripMenuItem.Text = "ROI";
            // 
            // rOIManagerToolStripMenuItem
            // 
            this.rOIManagerToolStripMenuItem.Name = "rOIManagerToolStripMenuItem";
            this.rOIManagerToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.rOIManagerToolStripMenuItem.Text = "ROI Manager";
            this.rOIManagerToolStripMenuItem.Click += new System.EventHandler(this.rOIManagerToolStripMenuItem_Click);
            // 
            // exportCSVToolStripMenuItem
            // 
            this.exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            this.exportCSVToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.exportCSVToolStripMenuItem.Text = "Export ROI\'s to CSV";
            this.exportCSVToolStripMenuItem.Click += new System.EventHandler(this.exportCSVToolStripMenuItem_Click);
            // 
            // importCSVToolStripMenuItem
            // 
            this.importCSVToolStripMenuItem.Name = "importCSVToolStripMenuItem";
            this.importCSVToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.importCSVToolStripMenuItem.Text = "Import ROI\'s from CSV";
            this.importCSVToolStripMenuItem.Click += new System.EventHandler(this.importCSVToolStripMenuItem_Click);
            // 
            // exportROIsOfFolderOfImagesToolStripMenuItem
            // 
            this.exportROIsOfFolderOfImagesToolStripMenuItem.Name = "exportROIsOfFolderOfImagesToolStripMenuItem";
            this.exportROIsOfFolderOfImagesToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.exportROIsOfFolderOfImagesToolStripMenuItem.Text = "Export ROI\'s of Folder of Images";
            this.exportROIsOfFolderOfImagesToolStripMenuItem.Click += new System.EventHandler(this.exportROIsOfFolderOfImagesToolStripMenuItem_Click);
            // 
            // channelsToolToolStripMenuItem
            // 
            this.channelsToolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoThresholdToolStripMenuItem,
            this.channelsToolToolStripMenuItem1});
            this.channelsToolToolStripMenuItem.Name = "channelsToolToolStripMenuItem";
            this.channelsToolToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.channelsToolToolStripMenuItem.Text = "Channels";
            // 
            // autoThresholdToolStripMenuItem
            // 
            this.autoThresholdToolStripMenuItem.Name = "autoThresholdToolStripMenuItem";
            this.autoThresholdToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.autoThresholdToolStripMenuItem.Text = "Auto Threshold All";
            this.autoThresholdToolStripMenuItem.Click += new System.EventHandler(this.autoThresholdToolStripMenuItem_Click);
            // 
            // channelsToolToolStripMenuItem1
            // 
            this.channelsToolToolStripMenuItem1.Name = "channelsToolToolStripMenuItem1";
            this.channelsToolToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.channelsToolToolStripMenuItem1.Text = "Channels Tool";
            this.channelsToolToolStripMenuItem1.Click += new System.EventHandler(this.channelsToolToolStripMenuItem_Click);
            // 
            // stackToolsToolStripMenuItem
            // 
            this.stackToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stackToolToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.rotateFlipToolStripMenuItem});
            this.stackToolsToolStripMenuItem.Name = "stackToolsToolStripMenuItem";
            this.stackToolsToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.stackToolsToolStripMenuItem.Text = "Stacks";
            this.stackToolsToolStripMenuItem.Click += new System.EventHandler(this.stackToolsToolStripMenuItem_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sizeModeToolStripMenuItem,
            this.toolboxToolStripMenuItem,
            this.rOIToolStripMenuItem,
            this.channelsToolToolStripMenuItem,
            this.stackToolsToolStripMenuItem,
            this.formatToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.scriptToolStripMenuItem,
            this.automationToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bit8ToolStripMenuItem,
            this.bit16ToolStripMenuItem,
            this.to24BitToolStripMenuItem,
            this.to36BitToolStripMenuItem,
            this.to48BitToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // bit8ToolStripMenuItem
            // 
            this.bit8ToolStripMenuItem.Name = "bit8ToolStripMenuItem";
            this.bit8ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bit8ToolStripMenuItem.Text = "To 8 Bit";
            this.bit8ToolStripMenuItem.Click += new System.EventHandler(this.bit8ToolStripMenuItem_Click);
            // 
            // bit16ToolStripMenuItem
            // 
            this.bit16ToolStripMenuItem.Name = "bit16ToolStripMenuItem";
            this.bit16ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bit16ToolStripMenuItem.Text = "To 16 Bit";
            this.bit16ToolStripMenuItem.Click += new System.EventHandler(this.bit16ToolStripMenuItem_Click);
            // 
            // to24BitToolStripMenuItem
            // 
            this.to24BitToolStripMenuItem.Name = "to24BitToolStripMenuItem";
            this.to24BitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.to24BitToolStripMenuItem.Text = "To 24 Bit";
            this.to24BitToolStripMenuItem.Click += new System.EventHandler(this.to24BitToolStripMenuItem_Click);
            // 
            // to36BitToolStripMenuItem
            // 
            this.to36BitToolStripMenuItem.Name = "to36BitToolStripMenuItem";
            this.to36BitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.to36BitToolStripMenuItem.Text = "To 32 Bit";
            this.to36BitToolStripMenuItem.Click += new System.EventHandler(this.to32BitToolStripMenuItem_Click);
            // 
            // to48BitToolStripMenuItem
            // 
            this.to48BitToolStripMenuItem.Name = "to48BitToolStripMenuItem";
            this.to48BitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.to48BitToolStripMenuItem.Text = "To 48 Bit";
            this.to48BitToolStripMenuItem.Click += new System.EventHandler(this.to48BitToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            this.filtersToolStripMenuItem.Click += new System.EventHandler(this.filtersToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptRunnerToolStripMenuItem,
            this.scriptRecorderToolStripMenuItem});
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.scriptToolStripMenuItem.Text = "Script";
            // 
            // scriptRunnerToolStripMenuItem
            // 
            this.scriptRunnerToolStripMenuItem.Name = "scriptRunnerToolStripMenuItem";
            this.scriptRunnerToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.scriptRunnerToolStripMenuItem.Text = "Script Runner";
            this.scriptRunnerToolStripMenuItem.Click += new System.EventHandler(this.scriptRunnerToolStripMenuItem_Click_1);
            // 
            // scriptRecorderToolStripMenuItem
            // 
            this.scriptRecorderToolStripMenuItem.Name = "scriptRecorderToolStripMenuItem";
            this.scriptRecorderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.scriptRecorderToolStripMenuItem.Text = "Script Recorder";
            this.scriptRecorderToolStripMenuItem.Click += new System.EventHandler(this.scriptRecorderToolStripMenuItem_Click_1);
            // 
            // automationToolStripMenuItem
            // 
            this.automationToolStripMenuItem.Name = "automationToolStripMenuItem";
            this.automationToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.automationToolStripMenuItem.Text = "Automation";
            this.automationToolStripMenuItem.Click += new System.EventHandler(this.automationToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Created);
            // 
            // stackToolToolStripMenuItem
            // 
            this.stackToolToolStripMenuItem.Name = "stackToolToolStripMenuItem";
            this.stackToolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stackToolToolStripMenuItem.Text = "Stack Tool";
            this.stackToolToolStripMenuItem.Click += new System.EventHandler(this.stackToolsToolStripMenuItem_Click);
            // 
            // rotateFlipToolStripMenuItem
            // 
            this.rotateFlipToolStripMenuItem.Name = "rotateFlipToolStripMenuItem";
            this.rotateFlipToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rotateFlipToolStripMenuItem.Text = "Rotate Flip";
            this.rotateFlipToolStripMenuItem.DropDownOpening += new System.EventHandler(this.rotateFlipToolStripMenuItem_DropDownOpening);
            this.rotateFlipToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.rotateFlipToolStripMenuItem_DropDownItemClicked);
            // 
            // TabsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(77)))), ((int)(((byte)(98)))));
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(200, 64);
            this.Name = "TabsView";
            this.Text = "BioImager";
            this.Activated += new System.EventHandler(this.ImageViewer_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabsView_FormClosing);
            this.Load += new System.EventHandler(this.TabsView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TabsView_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ImageViewer_PreviewKeyDown);
            this.panel.ResumeLayout(false);
            this.tabContextMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFilesDialog;
        private System.Windows.Forms.SaveFileDialog saveOMEFileDialog;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.SaveFileDialog saveCSVFileDialog;
        private System.Windows.Forms.OpenFileDialog openCSVFileDialog;
        private System.Windows.Forms.SaveFileDialog saveTiffFileDialog;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveOMEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filteredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOIManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportROIsOfFolderOfImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelsToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoThresholdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelsToolToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stackToolsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bit8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bit16ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem to24BitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem to36BitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem to48BitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ContextMenuStrip tabContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptRunnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptRecorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOMEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTabViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveOMEToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openRecentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automationToolStripMenuItem;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.ToolStripMenuItem addImagesToTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addImagesOMEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOMESeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTabTiffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stackToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateFlipToolStripMenuItem;
    }
}