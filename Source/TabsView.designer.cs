
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabsView));
            openFilesDialog = new OpenFileDialog();
            saveOMEFileDialog = new SaveFileDialog();
            panel = new Panel();
            tabControl = new TabControl();
            tabContextMenuStrip = new ContextMenuStrip(components);
            closeToolStripMenuItem = new ToolStripMenuItem();
            reloadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem1 = new ToolStripMenuItem();
            saveOMEToolStripMenuItem1 = new ToolStripMenuItem();
            toWindowToolStripMenuItem = new ToolStripMenuItem();
            folderBrowserDialog = new FolderBrowserDialog();
            saveCSVFileDialog = new SaveFileDialog();
            openCSVFileDialog = new OpenFileDialog();
            saveTiffFileDialog = new SaveFileDialog();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            openRecentToolStripMenuItem = new ToolStripMenuItem();
            openOMEToolStripMenuItem = new ToolStripMenuItem();
            openOMESeriesToolStripMenuItem = new ToolStripMenuItem();
            openSeriesToolStripMenuItem = new ToolStripMenuItem();
            sepToolStripMenuItem1 = new ToolStripSeparator();
            addImagesToTabToolStripMenuItem = new ToolStripMenuItem();
            addImagesOMEToolStripMenuItem = new ToolStripMenuItem();
            sepToolStripMenuItem2 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveOMEToolStripMenuItem = new ToolStripMenuItem();
            saveTabToolStripMenuItem = new ToolStripMenuItem();
            saveTabTiffToolStripMenuItem = new ToolStripMenuItem();
            saveSeriesToolStripMenuItem = new ToolStripMenuItem();
            savePyramidalToolStripMenuItem = new ToolStripMenuItem();
            sepToolStripMenuItem3 = new ToolStripSeparator();
            imagesToStackToolStripMenuItem = new ToolStripMenuItem();
            newTabViewToolStripMenuItem = new ToolStripMenuItem();
            nodeViewToolStripMenuItem = new ToolStripMenuItem();
            clearRecentToolStripMenuItem = new ToolStripMenuItem();
            sizeModeToolStripMenuItem = new ToolStripMenuItem();
            rGBToolStripMenuItem = new ToolStripMenuItem();
            filteredToolStripMenuItem = new ToolStripMenuItem();
            rawToolStripMenuItem = new ToolStripMenuItem();
            emissionToolStripMenuItem = new ToolStripMenuItem();
            _3dToolStripMenuItem = new ToolStripMenuItem();
            sepToolStripMenuItem = new ToolStripSeparator();
            dToolStripMenuItem = new ToolStripMenuItem();
            xMLToolStripMenuItem = new ToolStripMenuItem();
            toolboxToolStripMenuItem = new ToolStripMenuItem();
            setToolToolStripMenuItem = new ToolStripMenuItem();
            rOIToolStripMenuItem = new ToolStripMenuItem();
            rOIManagerToolStripMenuItem = new ToolStripMenuItem();
            exportCSVToolStripMenuItem = new ToolStripMenuItem();
            importCSVToolStripMenuItem = new ToolStripMenuItem();
            exportROIsOfFolderOfImagesToolStripMenuItem = new ToolStripMenuItem();
            importImageJROIToSelectedImageToolStripMenuItem = new ToolStripMenuItem();
            exportImageJROIFromSelectedImageToolStripMenuItem = new ToolStripMenuItem();
            channelsToolToolStripMenuItem = new ToolStripMenuItem();
            autoThresholdToolStripMenuItem = new ToolStripMenuItem();
            channelsToolToolStripMenuItem1 = new ToolStripMenuItem();
            switchRedBlueToolStripMenuItem = new ToolStripMenuItem();
            stackToolsToolStripMenuItem = new ToolStripMenuItem();
            stackToolsToolStripMenuItem1 = new ToolStripMenuItem();
            duplicateToolStripMenuItem = new ToolStripMenuItem();
            rotateToolStripMenuItem = new ToolStripMenuItem();
            findFocusToolStripMenuItem = new ToolStripMenuItem();
            menuStrip = new MenuStrip();
            formatToolStripMenuItem = new ToolStripMenuItem();
            bit8ToolStripMenuItem = new ToolStripMenuItem();
            bit16ToolStripMenuItem = new ToolStripMenuItem();
            to24BitToolStripMenuItem = new ToolStripMenuItem();
            to36BitToolStripMenuItem = new ToolStripMenuItem();
            to48BitToolStripMenuItem = new ToolStripMenuItem();
            filtersToolStripMenuItem = new ToolStripMenuItem();
            scriptToolStripMenuItem = new ToolStripMenuItem();
            runToolStripMenuItem = new ToolStripMenuItem();
            createFunctionToolStripMenuItem = new ToolStripMenuItem();
            consoleToolStripMenuItem = new ToolStripMenuItem();
            scriptRunnerToolStripMenuItem = new ToolStripMenuItem();
            scriptRecorderToolStripMenuItem = new ToolStripMenuItem();
            automationToolStripMenuItem = new ToolStripMenuItem();
            microscopeToolStripMenuItem = new ToolStripMenuItem();
            lightToolToolStripMenuItem = new ToolStripMenuItem();
            setupToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            openImageJROI = new OpenFileDialog();
            saveImageJROI = new SaveFileDialog();
            imagerToolStripMenuItem = new ToolStripMenuItem();
            stageToolToolStripMenuItem = new ToolStripMenuItem();
            panel.SuspendLayout();
            tabContextMenuStrip.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // openFilesDialog
            // 
            openFilesDialog.Multiselect = true;
            openFilesDialog.Title = "Open Images";
            // 
            // saveOMEFileDialog
            // 
            saveOMEFileDialog.DefaultExt = "ome.tif";
            saveOMEFileDialog.Filter = "OME TIFF Files (*.ome.tif)|*.ome.tif|All files (*.*)|*.*";
            saveOMEFileDialog.SupportMultiDottedExtensions = true;
            saveOMEFileDialog.Title = "Save Image";
            // 
            // panel
            // 
            panel.BackColor = Color.FromArgb(95, 122, 156);
            panel.Controls.Add(tabControl);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 24);
            panel.Margin = new Padding(4, 3, 4, 3);
            panel.Name = "panel";
            panel.Size = new Size(774, 487);
            panel.TabIndex = 1;
            // 
            // tabControl
            // 
            tabControl.ContextMenuStrip = tabContextMenuStrip;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(774, 487);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabContextMenuStrip
            // 
            tabContextMenuStrip.Items.AddRange(new ToolStripItem[] { closeToolStripMenuItem, reloadToolStripMenuItem, saveToolStripMenuItem1, saveOMEToolStripMenuItem1, toWindowToolStripMenuItem });
            tabContextMenuStrip.Name = "tabContextMenuStrip";
            tabContextMenuStrip.Size = new Size(165, 114);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(164, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click_1;
            // 
            // reloadToolStripMenuItem
            // 
            reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            reloadToolStripMenuItem.Size = new Size(164, 22);
            reloadToolStripMenuItem.Text = "Reload";
            reloadToolStripMenuItem.Click += reloadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem1
            // 
            saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            saveToolStripMenuItem1.Size = new Size(164, 22);
            saveToolStripMenuItem1.Text = "Save";
            saveToolStripMenuItem1.Click += saveToolStripMenuItem1_Click;
            // 
            // saveOMEToolStripMenuItem1
            // 
            saveOMEToolStripMenuItem1.Name = "saveOMEToolStripMenuItem1";
            saveOMEToolStripMenuItem1.Size = new Size(164, 22);
            saveOMEToolStripMenuItem1.Text = "Save OME";
            saveOMEToolStripMenuItem1.Click += saveOMEToolStripMenuItem1_Click;
            // 
            // toWindowToolStripMenuItem
            // 
            toWindowToolStripMenuItem.Name = "toWindowToolStripMenuItem";
            toWindowToolStripMenuItem.Size = new Size(164, 22);
            toWindowToolStripMenuItem.Text = "Open as Window";
            toWindowToolStripMenuItem.Click += toWindowToolStripMenuItem_Click;
            // 
            // saveCSVFileDialog
            // 
            saveCSVFileDialog.DefaultExt = "csv";
            saveCSVFileDialog.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            saveCSVFileDialog.Title = "Save ROIs to CSV";
            // 
            // openCSVFileDialog
            // 
            openCSVFileDialog.DefaultExt = "csv";
            openCSVFileDialog.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            openCSVFileDialog.Title = "Import ROI from CSV";
            // 
            // saveTiffFileDialog
            // 
            saveTiffFileDialog.DefaultExt = "tif";
            saveTiffFileDialog.Filter = "TIFF Files (*.tif)|*.tif | PNG Files (*.png)|*.png | BMP Files (*.bmp)|*.bmp | JPG Files (*.jpg)|*.jpg | Gif Files (*.gif)|*.gif";
            saveTiffFileDialog.SupportMultiDottedExtensions = true;
            saveTiffFileDialog.Title = "Save Image";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, openRecentToolStripMenuItem, openOMEToolStripMenuItem, openOMESeriesToolStripMenuItem, openSeriesToolStripMenuItem, sepToolStripMenuItem1, addImagesToTabToolStripMenuItem, addImagesOMEToolStripMenuItem, sepToolStripMenuItem2, saveToolStripMenuItem, saveOMEToolStripMenuItem, saveTabToolStripMenuItem, saveTabTiffToolStripMenuItem, saveSeriesToolStripMenuItem, savePyramidalToolStripMenuItem, sepToolStripMenuItem3, imagesToStackToolStripMenuItem, newTabViewToolStripMenuItem, nodeViewToolStripMenuItem, clearRecentToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(202, 22);
            openToolStripMenuItem.Text = "Open Images";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // openRecentToolStripMenuItem
            // 
            openRecentToolStripMenuItem.Name = "openRecentToolStripMenuItem";
            openRecentToolStripMenuItem.Size = new Size(202, 22);
            openRecentToolStripMenuItem.Text = "Open Recent";
            openRecentToolStripMenuItem.DropDownOpening += openRecentToolStripMenuItem_DropDownOpening;
            // 
            // openOMEToolStripMenuItem
            // 
            openOMEToolStripMenuItem.Name = "openOMEToolStripMenuItem";
            openOMEToolStripMenuItem.Size = new Size(202, 22);
            openOMEToolStripMenuItem.Text = "Open OME Images";
            openOMEToolStripMenuItem.Click += openOMEToolStripMenuItem_Click_1;
            // 
            // openOMESeriesToolStripMenuItem
            // 
            openOMESeriesToolStripMenuItem.Name = "openOMESeriesToolStripMenuItem";
            openOMESeriesToolStripMenuItem.Size = new Size(202, 22);
            openOMESeriesToolStripMenuItem.Text = "Open OME Series";
            openOMESeriesToolStripMenuItem.Click += openSeriesToolStripMenuItem_Click;
            // 
            // openSeriesToolStripMenuItem
            // 
            openSeriesToolStripMenuItem.Name = "openSeriesToolStripMenuItem";
            openSeriesToolStripMenuItem.Size = new Size(202, 22);
            openSeriesToolStripMenuItem.Text = "Open Series";
            openSeriesToolStripMenuItem.Click += openSeriesToolStripMenuItem_Click_1;
            // 
            // sepToolStripMenuItem1
            // 
            sepToolStripMenuItem1.Name = "sepToolStripMenuItem1";
            sepToolStripMenuItem1.Size = new Size(199, 6);
            // 
            // addImagesToTabToolStripMenuItem
            // 
            addImagesToTabToolStripMenuItem.Name = "addImagesToTabToolStripMenuItem";
            addImagesToTabToolStripMenuItem.Size = new Size(202, 22);
            addImagesToTabToolStripMenuItem.Text = "Add Images To Tab";
            addImagesToTabToolStripMenuItem.Click += addImagesToTabToolStripMenuItem_Click;
            // 
            // addImagesOMEToolStripMenuItem
            // 
            addImagesOMEToolStripMenuItem.Name = "addImagesOMEToolStripMenuItem";
            addImagesOMEToolStripMenuItem.Size = new Size(202, 22);
            addImagesOMEToolStripMenuItem.Text = "Add OME Images To Tab";
            addImagesOMEToolStripMenuItem.Click += addImagesOMEToolStripMenuItem_Click;
            // 
            // sepToolStripMenuItem2
            // 
            sepToolStripMenuItem2.Name = "sepToolStripMenuItem2";
            sepToolStripMenuItem2.Size = new Size(199, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(202, 22);
            saveToolStripMenuItem.Text = "Save Selected Tiff";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveOMEToolStripMenuItem
            // 
            saveOMEToolStripMenuItem.Name = "saveOMEToolStripMenuItem";
            saveOMEToolStripMenuItem.Size = new Size(202, 22);
            saveOMEToolStripMenuItem.Text = "Save Selected OME";
            saveOMEToolStripMenuItem.Click += saveOMEToolStripMenuItem_Click;
            // 
            // saveTabToolStripMenuItem
            // 
            saveTabToolStripMenuItem.Name = "saveTabToolStripMenuItem";
            saveTabToolStripMenuItem.Size = new Size(202, 22);
            saveTabToolStripMenuItem.Text = "Save Tab OME";
            saveTabToolStripMenuItem.Click += saveTabToolStripMenuItem_Click;
            // 
            // saveTabTiffToolStripMenuItem
            // 
            saveTabTiffToolStripMenuItem.Name = "saveTabTiffToolStripMenuItem";
            saveTabTiffToolStripMenuItem.Size = new Size(202, 22);
            saveTabTiffToolStripMenuItem.Text = "Save Tab Tiff";
            saveTabTiffToolStripMenuItem.Click += saveTabTiffToolStripMenuItem_Click;
            // 
            // saveSeriesToolStripMenuItem
            // 
            saveSeriesToolStripMenuItem.Name = "saveSeriesToolStripMenuItem";
            saveSeriesToolStripMenuItem.Size = new Size(202, 22);
            saveSeriesToolStripMenuItem.Text = "Save Series";
            saveSeriesToolStripMenuItem.Click += saveSeriesToolStripMenuItem_Click;
            // 
            // savePyramidalToolStripMenuItem
            // 
            savePyramidalToolStripMenuItem.Name = "savePyramidalToolStripMenuItem";
            savePyramidalToolStripMenuItem.Size = new Size(202, 22);
            savePyramidalToolStripMenuItem.Text = "Save Pyramidal";
            savePyramidalToolStripMenuItem.Click += savePyramidalToolStripMenuItem_Click;
            // 
            // sepToolStripMenuItem3
            // 
            sepToolStripMenuItem3.Name = "sepToolStripMenuItem3";
            sepToolStripMenuItem3.Size = new Size(199, 6);
            // 
            // imagesToStackToolStripMenuItem
            // 
            imagesToStackToolStripMenuItem.Name = "imagesToStackToolStripMenuItem";
            imagesToStackToolStripMenuItem.Size = new Size(202, 22);
            imagesToStackToolStripMenuItem.Text = "Images To Stack";
            imagesToStackToolStripMenuItem.Click += imagesToStackToolStripMenuItem_Click;
            // 
            // newTabViewToolStripMenuItem
            // 
            newTabViewToolStripMenuItem.Name = "newTabViewToolStripMenuItem";
            newTabViewToolStripMenuItem.Size = new Size(202, 22);
            newTabViewToolStripMenuItem.Text = "New Process";
            newTabViewToolStripMenuItem.Click += newTabViewToolStripMenuItem_Click;
            // 
            // nodeViewToolStripMenuItem
            // 
            nodeViewToolStripMenuItem.Name = "nodeViewToolStripMenuItem";
            nodeViewToolStripMenuItem.Size = new Size(202, 22);
            nodeViewToolStripMenuItem.Text = "Node View";
            nodeViewToolStripMenuItem.Click += nodeViewToolStripMenuItem_Click;
            // 
            // clearRecentToolStripMenuItem
            // 
            clearRecentToolStripMenuItem.Name = "clearRecentToolStripMenuItem";
            clearRecentToolStripMenuItem.Size = new Size(202, 22);
            clearRecentToolStripMenuItem.Text = "Clear Recent";
            clearRecentToolStripMenuItem.Click += clearRecentToolStripMenuItem_Click;
            // 
            // sizeModeToolStripMenuItem
            // 
            sizeModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rGBToolStripMenuItem, filteredToolStripMenuItem, rawToolStripMenuItem, emissionToolStripMenuItem, _3dToolStripMenuItem, sepToolStripMenuItem, dToolStripMenuItem, xMLToolStripMenuItem });
            sizeModeToolStripMenuItem.Name = "sizeModeToolStripMenuItem";
            sizeModeToolStripMenuItem.Size = new Size(44, 20);
            sizeModeToolStripMenuItem.Text = "View";
            // 
            // rGBToolStripMenuItem
            // 
            rGBToolStripMenuItem.CheckOnClick = true;
            rGBToolStripMenuItem.Name = "rGBToolStripMenuItem";
            rGBToolStripMenuItem.Size = new Size(194, 22);
            rGBToolStripMenuItem.Text = "RGB";
            rGBToolStripMenuItem.Click += rGBToolStripMenuItem_Click;
            // 
            // filteredToolStripMenuItem
            // 
            filteredToolStripMenuItem.Checked = true;
            filteredToolStripMenuItem.CheckOnClick = true;
            filteredToolStripMenuItem.CheckState = CheckState.Checked;
            filteredToolStripMenuItem.Name = "filteredToolStripMenuItem";
            filteredToolStripMenuItem.Size = new Size(194, 22);
            filteredToolStripMenuItem.Text = "Filtered";
            filteredToolStripMenuItem.Click += filteredToolStripMenuItem_Click;
            // 
            // rawToolStripMenuItem
            // 
            rawToolStripMenuItem.CheckOnClick = true;
            rawToolStripMenuItem.Name = "rawToolStripMenuItem";
            rawToolStripMenuItem.Size = new Size(194, 22);
            rawToolStripMenuItem.Text = "Raw";
            rawToolStripMenuItem.Click += rawToolStripMenuItem_Click;
            // 
            // emissionToolStripMenuItem
            // 
            emissionToolStripMenuItem.Name = "emissionToolStripMenuItem";
            emissionToolStripMenuItem.Size = new Size(194, 22);
            emissionToolStripMenuItem.Text = "Emission";
            emissionToolStripMenuItem.Click += emissionToolStripMenuItem_Click;
            // 
            // _3dToolStripMenuItem
            // 
            _3dToolStripMenuItem.Name = "_3dToolStripMenuItem";
            _3dToolStripMenuItem.Size = new Size(194, 22);
            _3dToolStripMenuItem.Text = "3D";
            _3dToolStripMenuItem.Click += _3dToolStripMenuItem_Click;
            // 
            // sepToolStripMenuItem
            // 
            sepToolStripMenuItem.Name = "sepToolStripMenuItem";
            sepToolStripMenuItem.Size = new Size(191, 6);
            // 
            // dToolStripMenuItem
            // 
            dToolStripMenuItem.Checked = true;
            dToolStripMenuItem.CheckOnClick = true;
            dToolStripMenuItem.CheckState = CheckState.Checked;
            dToolStripMenuItem.Name = "dToolStripMenuItem";
            dToolStripMenuItem.Size = new Size(194, 22);
            dToolStripMenuItem.Text = "Hardware Acceleration";
            dToolStripMenuItem.Click += dToolStripMenuItem_Click;
            // 
            // xMLToolStripMenuItem
            // 
            xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            xMLToolStripMenuItem.Size = new Size(194, 22);
            xMLToolStripMenuItem.Text = "XML";
            xMLToolStripMenuItem.Click += xMLToolStripMenuItem_Click;
            // 
            // toolboxToolStripMenuItem
            // 
            toolboxToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setToolToolStripMenuItem });
            toolboxToolStripMenuItem.Name = "toolboxToolStripMenuItem";
            toolboxToolStripMenuItem.Size = new Size(46, 20);
            toolboxToolStripMenuItem.Text = "Tools";
            toolboxToolStripMenuItem.Click += toolboxToolStripMenuItem_Click;
            // 
            // setToolToolStripMenuItem
            // 
            setToolToolStripMenuItem.Name = "setToolToolStripMenuItem";
            setToolToolStripMenuItem.Size = new Size(115, 22);
            setToolToolStripMenuItem.Text = "Set Tool";
            setToolToolStripMenuItem.Click += setToolToolStripMenuItem_Click;
            // 
            // rOIToolStripMenuItem
            // 
            rOIToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rOIManagerToolStripMenuItem, exportCSVToolStripMenuItem, importCSVToolStripMenuItem, exportROIsOfFolderOfImagesToolStripMenuItem, importImageJROIToSelectedImageToolStripMenuItem, exportImageJROIFromSelectedImageToolStripMenuItem });
            rOIToolStripMenuItem.Name = "rOIToolStripMenuItem";
            rOIToolStripMenuItem.Size = new Size(38, 20);
            rOIToolStripMenuItem.Text = "ROI";
            // 
            // rOIManagerToolStripMenuItem
            // 
            rOIManagerToolStripMenuItem.Name = "rOIManagerToolStripMenuItem";
            rOIManagerToolStripMenuItem.Size = new Size(282, 22);
            rOIManagerToolStripMenuItem.Text = "ROI Manager";
            rOIManagerToolStripMenuItem.Click += rOIManagerToolStripMenuItem_Click;
            // 
            // exportCSVToolStripMenuItem
            // 
            exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            exportCSVToolStripMenuItem.Size = new Size(282, 22);
            exportCSVToolStripMenuItem.Text = "Export ROI's to CSV";
            exportCSVToolStripMenuItem.Click += exportCSVToolStripMenuItem_Click;
            // 
            // importCSVToolStripMenuItem
            // 
            importCSVToolStripMenuItem.Name = "importCSVToolStripMenuItem";
            importCSVToolStripMenuItem.Size = new Size(282, 22);
            importCSVToolStripMenuItem.Text = "Import ROI's from CSV";
            importCSVToolStripMenuItem.Click += importCSVToolStripMenuItem_Click;
            // 
            // exportROIsOfFolderOfImagesToolStripMenuItem
            // 
            exportROIsOfFolderOfImagesToolStripMenuItem.Name = "exportROIsOfFolderOfImagesToolStripMenuItem";
            exportROIsOfFolderOfImagesToolStripMenuItem.Size = new Size(282, 22);
            exportROIsOfFolderOfImagesToolStripMenuItem.Text = "Export ROI's of Folder of Images";
            exportROIsOfFolderOfImagesToolStripMenuItem.Click += exportROIsOfFolderOfImagesToolStripMenuItem_Click;
            // 
            // importImageJROIToSelectedImageToolStripMenuItem
            // 
            importImageJROIToSelectedImageToolStripMenuItem.Name = "importImageJROIToSelectedImageToolStripMenuItem";
            importImageJROIToSelectedImageToolStripMenuItem.Size = new Size(282, 22);
            importImageJROIToSelectedImageToolStripMenuItem.Text = "Import ImageJ ROI to Selected Image";
            importImageJROIToSelectedImageToolStripMenuItem.Click += importImageJROIToSelectedImageToolStripMenuItem_Click;
            // 
            // exportImageJROIFromSelectedImageToolStripMenuItem
            // 
            exportImageJROIFromSelectedImageToolStripMenuItem.Name = "exportImageJROIFromSelectedImageToolStripMenuItem";
            exportImageJROIFromSelectedImageToolStripMenuItem.Size = new Size(282, 22);
            exportImageJROIFromSelectedImageToolStripMenuItem.Text = "Export ImageJ ROI from Selected Image";
            exportImageJROIFromSelectedImageToolStripMenuItem.Click += exportImageJROIFromSelectedImageToolStripMenuItem_Click;
            // 
            // channelsToolToolStripMenuItem
            // 
            channelsToolToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { autoThresholdToolStripMenuItem, channelsToolToolStripMenuItem1, switchRedBlueToolStripMenuItem });
            channelsToolToolStripMenuItem.Name = "channelsToolToolStripMenuItem";
            channelsToolToolStripMenuItem.Size = new Size(68, 20);
            channelsToolToolStripMenuItem.Text = "Channels";
            // 
            // autoThresholdToolStripMenuItem
            // 
            autoThresholdToolStripMenuItem.Name = "autoThresholdToolStripMenuItem";
            autoThresholdToolStripMenuItem.Size = new Size(172, 22);
            autoThresholdToolStripMenuItem.Text = "Auto Threshold All";
            autoThresholdToolStripMenuItem.Click += autoThresholdToolStripMenuItem_Click;
            // 
            // channelsToolToolStripMenuItem1
            // 
            channelsToolToolStripMenuItem1.Name = "channelsToolToolStripMenuItem1";
            channelsToolToolStripMenuItem1.Size = new Size(172, 22);
            channelsToolToolStripMenuItem1.Text = "Channels Tool";
            channelsToolToolStripMenuItem1.Click += channelsToolToolStripMenuItem_Click;
            // 
            // switchRedBlueToolStripMenuItem
            // 
            switchRedBlueToolStripMenuItem.Name = "switchRedBlueToolStripMenuItem";
            switchRedBlueToolStripMenuItem.Size = new Size(172, 22);
            switchRedBlueToolStripMenuItem.Text = "Switch Red Blue";
            switchRedBlueToolStripMenuItem.Click += switchRedBlueToolStripMenuItem_Click;
            // 
            // stackToolsToolStripMenuItem
            // 
            stackToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { stackToolsToolStripMenuItem1, duplicateToolStripMenuItem, rotateToolStripMenuItem, findFocusToolStripMenuItem });
            stackToolsToolStripMenuItem.Name = "stackToolsToolStripMenuItem";
            stackToolsToolStripMenuItem.Size = new Size(52, 20);
            stackToolsToolStripMenuItem.Text = "Stacks";
            // 
            // stackToolsToolStripMenuItem1
            // 
            stackToolsToolStripMenuItem1.Name = "stackToolsToolStripMenuItem1";
            stackToolsToolStripMenuItem1.Size = new Size(131, 22);
            stackToolsToolStripMenuItem1.Text = "Stack Tool";
            stackToolsToolStripMenuItem1.Click += stackToolsToolStripMenuItem_Click;
            // 
            // duplicateToolStripMenuItem
            // 
            duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            duplicateToolStripMenuItem.Size = new Size(131, 22);
            duplicateToolStripMenuItem.Text = "Duplicate";
            duplicateToolStripMenuItem.Click += duplicateToolStripMenuItem_Click;
            // 
            // rotateToolStripMenuItem
            // 
            rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            rotateToolStripMenuItem.Size = new Size(131, 22);
            rotateToolStripMenuItem.Text = "Rotate Flip";
            rotateToolStripMenuItem.DropDownOpening += rotateToolStripMenuItem_DropDownOpening;
            rotateToolStripMenuItem.DropDownItemClicked += rotateToolStripMenuItem_DropDownItemClicked;
            // 
            // findFocusToolStripMenuItem
            // 
            findFocusToolStripMenuItem.Name = "findFocusToolStripMenuItem";
            findFocusToolStripMenuItem.Size = new Size(131, 22);
            findFocusToolStripMenuItem.Text = "Find Focus";
            findFocusToolStripMenuItem.Click += findFocusToolStripMenuItem_Click;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, sizeModeToolStripMenuItem, toolboxToolStripMenuItem, rOIToolStripMenuItem, channelsToolToolStripMenuItem, stackToolsToolStripMenuItem, formatToolStripMenuItem, filtersToolStripMenuItem, scriptToolStripMenuItem, automationToolStripMenuItem, microscopeToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(774, 24);
            menuStrip.TabIndex = 0;
            // 
            // formatToolStripMenuItem
            // 
            formatToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bit8ToolStripMenuItem, bit16ToolStripMenuItem, to24BitToolStripMenuItem, to36BitToolStripMenuItem, to48BitToolStripMenuItem });
            formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            formatToolStripMenuItem.Size = new Size(57, 20);
            formatToolStripMenuItem.Text = "Format";
            // 
            // bit8ToolStripMenuItem
            // 
            bit8ToolStripMenuItem.Name = "bit8ToolStripMenuItem";
            bit8ToolStripMenuItem.Size = new Size(118, 22);
            bit8ToolStripMenuItem.Text = "To 8 Bit";
            bit8ToolStripMenuItem.Click += bit8ToolStripMenuItem_Click;
            // 
            // bit16ToolStripMenuItem
            // 
            bit16ToolStripMenuItem.Name = "bit16ToolStripMenuItem";
            bit16ToolStripMenuItem.Size = new Size(118, 22);
            bit16ToolStripMenuItem.Text = "To 16 Bit";
            bit16ToolStripMenuItem.Click += bit16ToolStripMenuItem_Click;
            // 
            // to24BitToolStripMenuItem
            // 
            to24BitToolStripMenuItem.Name = "to24BitToolStripMenuItem";
            to24BitToolStripMenuItem.Size = new Size(118, 22);
            to24BitToolStripMenuItem.Text = "To 24 Bit";
            to24BitToolStripMenuItem.Click += to24BitToolStripMenuItem_Click;
            // 
            // to36BitToolStripMenuItem
            // 
            to36BitToolStripMenuItem.Name = "to36BitToolStripMenuItem";
            to36BitToolStripMenuItem.Size = new Size(118, 22);
            to36BitToolStripMenuItem.Text = "To 32 Bit";
            to36BitToolStripMenuItem.Click += to32BitToolStripMenuItem_Click;
            // 
            // to48BitToolStripMenuItem
            // 
            to48BitToolStripMenuItem.Name = "to48BitToolStripMenuItem";
            to48BitToolStripMenuItem.Size = new Size(118, 22);
            to48BitToolStripMenuItem.Text = "To 48 Bit";
            to48BitToolStripMenuItem.Click += to48BitToolStripMenuItem_Click;
            // 
            // filtersToolStripMenuItem
            // 
            filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            filtersToolStripMenuItem.Size = new Size(50, 20);
            filtersToolStripMenuItem.Text = "Filters";
            filtersToolStripMenuItem.Click += filtersToolStripMenuItem_Click;
            // 
            // scriptToolStripMenuItem
            // 
            scriptToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { runToolStripMenuItem, createFunctionToolStripMenuItem, consoleToolStripMenuItem, scriptRunnerToolStripMenuItem, scriptRecorderToolStripMenuItem });
            scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            scriptToolStripMenuItem.Size = new Size(49, 20);
            scriptToolStripMenuItem.Text = "Script";
            // 
            // runToolStripMenuItem
            // 
            runToolStripMenuItem.Name = "runToolStripMenuItem";
            runToolStripMenuItem.Size = new Size(154, 22);
            runToolStripMenuItem.Text = "Run";
            runToolStripMenuItem.DropDownOpening += runToolStripMenuItem_DropDownOpening;
            // 
            // createFunctionToolStripMenuItem
            // 
            createFunctionToolStripMenuItem.Name = "createFunctionToolStripMenuItem";
            createFunctionToolStripMenuItem.Size = new Size(154, 22);
            createFunctionToolStripMenuItem.Text = "Functions Tool";
            createFunctionToolStripMenuItem.Click += createFunctionToolStripMenuItem_Click;
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(154, 22);
            consoleToolStripMenuItem.Text = "Console";
            consoleToolStripMenuItem.Click += consoleToolStripMenuItem_Click;
            // 
            // scriptRunnerToolStripMenuItem
            // 
            scriptRunnerToolStripMenuItem.Name = "scriptRunnerToolStripMenuItem";
            scriptRunnerToolStripMenuItem.Size = new Size(154, 22);
            scriptRunnerToolStripMenuItem.Text = "Script Runner";
            scriptRunnerToolStripMenuItem.Click += scriptRunnerToolStripMenuItem_Click_1;
            // 
            // scriptRecorderToolStripMenuItem
            // 
            scriptRecorderToolStripMenuItem.Name = "scriptRecorderToolStripMenuItem";
            scriptRecorderToolStripMenuItem.Size = new Size(154, 22);
            scriptRecorderToolStripMenuItem.Text = "Script Recorder";
            scriptRecorderToolStripMenuItem.Click += scriptRecorderToolStripMenuItem_Click_1;
            // 
            // automationToolStripMenuItem
            // 
            automationToolStripMenuItem.Name = "automationToolStripMenuItem";
            automationToolStripMenuItem.Size = new Size(83, 20);
            automationToolStripMenuItem.Text = "Automation";
            automationToolStripMenuItem.Click += automationToolStripMenuItem_Click;
            // 
            // microscopeToolStripMenuItem
            // 
            microscopeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lightToolToolStripMenuItem, setupToolStripMenuItem, imagerToolStripMenuItem, stageToolToolStripMenuItem });
            microscopeToolStripMenuItem.Name = "microscopeToolStripMenuItem";
            microscopeToolStripMenuItem.Size = new Size(81, 20);
            microscopeToolStripMenuItem.Text = "Microscope";
            // 
            // lightToolToolStripMenuItem
            // 
            lightToolToolStripMenuItem.Name = "lightToolToolStripMenuItem";
            lightToolToolStripMenuItem.Size = new Size(180, 22);
            lightToolToolStripMenuItem.Text = "Light Path";
            lightToolToolStripMenuItem.Click += lightToolToolStripMenuItem_Click;
            // 
            // setupToolStripMenuItem
            // 
            setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            setupToolStripMenuItem.Size = new Size(180, 22);
            setupToolStripMenuItem.Text = "Setup";
            setupToolStripMenuItem.Click += setupToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // openImageJROI
            // 
            openImageJROI.DefaultExt = "csv";
            openImageJROI.Filter = "ROI Files (*.roi)|*.roi|All files (*.*)|*.*";
            openImageJROI.Title = "Import ImageJ ROI";
            // 
            // saveImageJROI
            // 
            saveImageJROI.DefaultExt = "csv";
            saveImageJROI.Filter = "ROI Files (*.roi)|*.roi|All files (*.*)|*.*";
            saveImageJROI.Title = "Save ROIs to CSV";
            // 
            // imagerToolStripMenuItem
            // 
            imagerToolStripMenuItem.Name = "imagerToolStripMenuItem";
            imagerToolStripMenuItem.Size = new Size(180, 22);
            imagerToolStripMenuItem.Text = "Imager";
            imagerToolStripMenuItem.Click += imagerToolStripMenuItem_Click;
            // 
            // stageToolToolStripMenuItem
            // 
            stageToolToolStripMenuItem.Name = "stageToolToolStripMenuItem";
            stageToolToolStripMenuItem.Size = new Size(180, 22);
            stageToolToolStripMenuItem.Text = "Stage Tool";
            stageToolToolStripMenuItem.Click += stageToolToolStripMenuItem_Click;
            // 
            // TabsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(69, 77, 98);
            ClientSize = new Size(774, 511);
            Controls.Add(panel);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(231, 68);
            Name = "TabsView";
            Text = "BioImager";
            Activated += ImageViewer_Activated;
            FormClosing += TabsView_FormClosing;
            Load += TabsView_Load;
            KeyDown += TabsView_KeyDown;
            PreviewKeyDown += ImageViewer_PreviewKeyDown;
            panel.ResumeLayout(false);
            tabContextMenuStrip.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFilesDialog;
        private SaveFileDialog saveOMEFileDialog;
        private Panel panel;
        private FolderBrowserDialog folderBrowserDialog;
        private SaveFileDialog saveCSVFileDialog;
        private OpenFileDialog openCSVFileDialog;
        private SaveFileDialog saveTiffFileDialog;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveOMEToolStripMenuItem;
        private ToolStripMenuItem sizeModeToolStripMenuItem;
        private ToolStripMenuItem rGBToolStripMenuItem;
        private ToolStripMenuItem filteredToolStripMenuItem;
        private ToolStripMenuItem rawToolStripMenuItem;
        private ToolStripMenuItem toolboxToolStripMenuItem;
        private ToolStripMenuItem setToolToolStripMenuItem;
        private ToolStripMenuItem rOIToolStripMenuItem;
        private ToolStripMenuItem rOIManagerToolStripMenuItem;
        private ToolStripMenuItem exportCSVToolStripMenuItem;
        private ToolStripMenuItem importCSVToolStripMenuItem;
        private ToolStripMenuItem exportROIsOfFolderOfImagesToolStripMenuItem;
        private ToolStripMenuItem channelsToolToolStripMenuItem;
        private ToolStripMenuItem autoThresholdToolStripMenuItem;
        private ToolStripMenuItem channelsToolToolStripMenuItem1;
        private ToolStripMenuItem stackToolsToolStripMenuItem;
        private MenuStrip menuStrip;
        private ToolStripMenuItem filtersToolStripMenuItem;
        private ToolStripMenuItem formatToolStripMenuItem;
        private ToolStripMenuItem bit8ToolStripMenuItem;
        private ToolStripMenuItem bit16ToolStripMenuItem;
        private ToolStripMenuItem to24BitToolStripMenuItem;
        private ToolStripMenuItem to36BitToolStripMenuItem;
        private ToolStripMenuItem to48BitToolStripMenuItem;
        private TabControl tabControl;
        private ContextMenuStrip tabContextMenuStrip;
        private ToolStripMenuItem toWindowToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem scriptToolStripMenuItem;
        private ToolStripMenuItem scriptRunnerToolStripMenuItem;
        private ToolStripMenuItem scriptRecorderToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem openOMEToolStripMenuItem;
        private ToolStripMenuItem newTabViewToolStripMenuItem;
        private ToolStripMenuItem nodeViewToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem1;
        private ToolStripMenuItem saveOMEToolStripMenuItem1;
        private ToolStripMenuItem openRecentToolStripMenuItem;
        private ToolStripMenuItem duplicateToolStripMenuItem;
        private ToolStripMenuItem saveSeriesToolStripMenuItem;
        private ToolStripMenuItem automationToolStripMenuItem;
        private ToolStripMenuItem addImagesToTabToolStripMenuItem;
        private ToolStripMenuItem addImagesOMEToolStripMenuItem;
        private ToolStripMenuItem openOMESeriesToolStripMenuItem;
        private ToolStripMenuItem saveTabToolStripMenuItem;
        private ToolStripMenuItem saveTabTiffToolStripMenuItem;
        private ToolStripMenuItem openSeriesToolStripMenuItem;
        private ToolStripMenuItem clearRecentToolStripMenuItem;
        private ToolStripMenuItem rotateToolStripMenuItem;
        private ToolStripMenuItem stackToolsToolStripMenuItem1;
        private ToolStripMenuItem emissionToolStripMenuItem;
        private ToolStripMenuItem consoleToolStripMenuItem;
        private ToolStripMenuItem switchRedBlueToolStripMenuItem;
        private ToolStripMenuItem createFunctionToolStripMenuItem;
        private ToolStripMenuItem runToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem xMLToolStripMenuItem;
        private ToolStripMenuItem dToolStripMenuItem;
        private ToolStripMenuItem imagesToStackToolStripMenuItem;
        private ToolStripSeparator sepToolStripMenuItem;
        private ToolStripSeparator sepToolStripMenuItem1;
        private ToolStripSeparator sepToolStripMenuItem2;
        private ToolStripSeparator sepToolStripMenuItem3;
        private ToolStripMenuItem _3dToolStripMenuItem;
        private ToolStripMenuItem importImageJROIToSelectedImageToolStripMenuItem;
        private ToolStripMenuItem exportImageJROIFromSelectedImageToolStripMenuItem;
        private OpenFileDialog openImageJROI;
        private SaveFileDialog saveImageJROI;
        private ToolStripMenuItem microscopeToolStripMenuItem;
        private ToolStripMenuItem lightToolToolStripMenuItem;
        private ToolStripMenuItem savePyramidalToolStripMenuItem;
        private ToolStripMenuItem findFocusToolStripMenuItem;
        private ToolStripMenuItem setupToolStripMenuItem;
        private ToolStripMenuItem imagerToolStripMenuItem;
        private ToolStripMenuItem stageToolToolStripMenuItem;
    }
}