
namespace BioImager
{
    partial class ImageView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timePlayMenuStrip = new ContextMenuStrip(components);
            playTimeToolStripMenu = new ToolStripMenuItem();
            stopTimeToolStripMenu = new ToolStripMenuItem();
            playSpeedToolStripMenuItem1 = new ToolStripMenuItem();
            setValueRangeToolStripMenuItem1 = new ToolStripMenuItem();
            loopTimeToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip = new ContextMenuStrip(components);
            rOIToolStripMenuItem = new ToolStripMenuItem();
            setROITextToolStripMenuItem = new ToolStripMenuItem();
            copyROIToolStripMenuItem1 = new ToolStripMenuItem();
            pasteROIToolStripMenuItem1 = new ToolStripMenuItem();
            deleteROIToolStripMenuItem1 = new ToolStripMenuItem();
            drawToolStripMenuItem = new ToolStripMenuItem();
            fillToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            rGBToolStripMenuItem = new ToolStripMenuItem();
            filteredToolStripMenuItem = new ToolStripMenuItem();
            rawToolStripMenuItem = new ToolStripMenuItem();
            moveStageToImageToolStripMenuItem = new ToolStripMenuItem();
            goToToolStripMenuItem = new ToolStripMenuItem();
            goToImageToolStripMenuItem = new ToolStripMenuItem();
            goToStageToolStripMenuItem = new ToolStripMenuItem();
            controlsToolStripMenuItem = new ToolStripMenuItem();
            hideControlsToolStripMenuItem1 = new ToolStripMenuItem();
            hideStatusToolStripMenuItem = new ToolStripMenuItem();
            hideOverViewToolStripMenuItem = new ToolStripMenuItem();
            copyViewToClipboardToolStripMenuItem = new ToolStripMenuItem();
            layersToolStripMenuItem = new ToolStripMenuItem();
            removeImageToolStripMenuItem = new ToolStripMenuItem();
            removeImagesToolStripMenuItem = new ToolStripMenuItem();
            zPlayMenuStrip = new ContextMenuStrip(components);
            playZToolStripMenuItem = new ToolStripMenuItem();
            stopZToolStripMenuItem = new ToolStripMenuItem();
            playSpeedToolStripMenuItem = new ToolStripMenuItem();
            setValueRangeToolStripMenuItem = new ToolStripMenuItem();
            loopZToolStripMenuItem = new ToolStripMenuItem();
            channelBoxB = new ComboBox();
            channelBoxG = new ComboBox();
            labelRGB = new Label();
            channelBoxR = new ComboBox();
            rgbBoxsPanel = new Panel();
            statusLabel = new Label();
            tLabel = new Label();
            tBar = new TrackBar();
            controlsMenuStrip = new ContextMenuStrip(components);
            hideControlsToolStripMenuItem = new ToolStripMenuItem();
            cPlayMenuStrip = new ContextMenuStrip(components);
            playCToolStripMenuItem = new ToolStripMenuItem();
            stopCToolStripMenuItem = new ToolStripMenuItem();
            CPlaySpeedToolStripMenuItem = new ToolStripMenuItem();
            setCValueRangeToolStripMenuItem = new ToolStripMenuItem();
            loopCToolStripMenuItem = new ToolStripMenuItem();
            zBar = new TrackBar();
            zLabel = new Label();
            cBar = new TrackBar();
            cLabel = new Label();
            timelineTimer = new System.Windows.Forms.Timer(components);
            zTimer = new System.Windows.Forms.Timer(components);
            cTimer = new System.Windows.Forms.Timer(components);
            trackBarPanel = new Panel();
            statusPanel = new Panel();
            ticksLabel = new Label();
            showControlsToolStripMenuItem = new ToolStripMenuItem();
            statusMenuStrip = new ContextMenuStrip(components);
            HideStatusMenuItem = new ToolStripMenuItem();
            pictureBox = new PictureBox();
            overlayPictureBox = new PictureBox();
            hScrollBar = new HScrollBar();
            vScrollBar = new VScrollBar();
            saveCSVFileDialog = new SaveFileDialog();
            timePlayMenuStrip.SuspendLayout();
            contextMenuStrip.SuspendLayout();
            zPlayMenuStrip.SuspendLayout();
            rgbBoxsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tBar).BeginInit();
            controlsMenuStrip.SuspendLayout();
            cPlayMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)zBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cBar).BeginInit();
            trackBarPanel.SuspendLayout();
            statusPanel.SuspendLayout();
            statusMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)overlayPictureBox).BeginInit();
            SuspendLayout();
            // 
            // timePlayMenuStrip
            // 
            timePlayMenuStrip.ImageScalingSize = new Size(32, 32);
            timePlayMenuStrip.Items.AddRange(new ToolStripItem[] { playTimeToolStripMenu, stopTimeToolStripMenu, playSpeedToolStripMenuItem1, setValueRangeToolStripMenuItem1, loopTimeToolStripMenuItem });
            timePlayMenuStrip.Name = "zPlayMenuStrip";
            timePlayMenuStrip.Size = new Size(262, 204);
            // 
            // playTimeToolStripMenu
            // 
            playTimeToolStripMenu.Name = "playTimeToolStripMenu";
            playTimeToolStripMenu.Size = new Size(261, 40);
            playTimeToolStripMenu.Text = "Play";
            playTimeToolStripMenu.Click += playTimeToolStripMenu_Click;
            // 
            // stopTimeToolStripMenu
            // 
            stopTimeToolStripMenu.Checked = true;
            stopTimeToolStripMenu.CheckState = CheckState.Checked;
            stopTimeToolStripMenu.Name = "stopTimeToolStripMenu";
            stopTimeToolStripMenu.Size = new Size(261, 40);
            stopTimeToolStripMenu.Text = "Stop";
            stopTimeToolStripMenu.Click += stopTimeToolStripMenu_Click;
            // 
            // playSpeedToolStripMenuItem1
            // 
            playSpeedToolStripMenuItem1.Name = "playSpeedToolStripMenuItem1";
            playSpeedToolStripMenuItem1.Size = new Size(261, 40);
            playSpeedToolStripMenuItem1.Text = "Play Speed";
            playSpeedToolStripMenuItem1.Click += playSpeedToolStripMenuItem1_Click;
            // 
            // setValueRangeToolStripMenuItem1
            // 
            setValueRangeToolStripMenuItem1.Name = "setValueRangeToolStripMenuItem1";
            setValueRangeToolStripMenuItem1.Size = new Size(261, 40);
            setValueRangeToolStripMenuItem1.Text = "Set Value Range";
            setValueRangeToolStripMenuItem1.Click += setValueRangeToolStripMenuItem1_Click;
            // 
            // loopTimeToolStripMenuItem
            // 
            loopTimeToolStripMenuItem.Checked = true;
            loopTimeToolStripMenuItem.CheckOnClick = true;
            loopTimeToolStripMenuItem.CheckState = CheckState.Checked;
            loopTimeToolStripMenuItem.Name = "loopTimeToolStripMenuItem";
            loopTimeToolStripMenuItem.ShowShortcutKeys = false;
            loopTimeToolStripMenuItem.Size = new Size(261, 40);
            loopTimeToolStripMenuItem.Text = "Loop";
            loopTimeToolStripMenuItem.Click += loopTimeToolStripMenuItem_Click;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(32, 32);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { rOIToolStripMenuItem, viewToolStripMenuItem, moveStageToImageToolStripMenuItem, goToToolStripMenuItem, goToImageToolStripMenuItem, goToStageToolStripMenuItem, controlsToolStripMenuItem, copyViewToClipboardToolStripMenuItem, layersToolStripMenuItem, removeImageToolStripMenuItem, removeImagesToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(341, 422);
            // 
            // rOIToolStripMenuItem
            // 
            rOIToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setROITextToolStripMenuItem, copyROIToolStripMenuItem1, pasteROIToolStripMenuItem1, deleteROIToolStripMenuItem1, drawToolStripMenuItem, fillToolStripMenuItem });
            rOIToolStripMenuItem.Name = "rOIToolStripMenuItem";
            rOIToolStripMenuItem.Size = new Size(340, 38);
            rOIToolStripMenuItem.Text = "ROI";
            // 
            // setROITextToolStripMenuItem
            // 
            setROITextToolStripMenuItem.Name = "setROITextToolStripMenuItem";
            setROITextToolStripMenuItem.Size = new Size(231, 44);
            setROITextToolStripMenuItem.Text = "Set Text";
            setROITextToolStripMenuItem.Click += setTextSelectionToolStripMenuItem_Click;
            // 
            // copyROIToolStripMenuItem1
            // 
            copyROIToolStripMenuItem1.Name = "copyROIToolStripMenuItem1";
            copyROIToolStripMenuItem1.Size = new Size(231, 44);
            copyROIToolStripMenuItem1.Text = "Copy";
            copyROIToolStripMenuItem1.Click += copyROIToolStripMenuItem_Click;
            // 
            // pasteROIToolStripMenuItem1
            // 
            pasteROIToolStripMenuItem1.Name = "pasteROIToolStripMenuItem1";
            pasteROIToolStripMenuItem1.Size = new Size(231, 44);
            pasteROIToolStripMenuItem1.Text = "Paste";
            pasteROIToolStripMenuItem1.Click += pasteROIToolStripMenuItem_Click;
            // 
            // deleteROIToolStripMenuItem1
            // 
            deleteROIToolStripMenuItem1.Name = "deleteROIToolStripMenuItem1";
            deleteROIToolStripMenuItem1.Size = new Size(231, 44);
            deleteROIToolStripMenuItem1.Text = "Delete";
            deleteROIToolStripMenuItem1.Click += deleteROIToolStripMenuItem_Click;
            // 
            // drawToolStripMenuItem
            // 
            drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            drawToolStripMenuItem.Size = new Size(231, 44);
            drawToolStripMenuItem.Text = "Draw";
            drawToolStripMenuItem.Click += drawToolStripMenuItem_Click;
            // 
            // fillToolStripMenuItem
            // 
            fillToolStripMenuItem.Name = "fillToolStripMenuItem";
            fillToolStripMenuItem.Size = new Size(231, 44);
            fillToolStripMenuItem.Text = "Fill";
            fillToolStripMenuItem.Click += fillToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rGBToolStripMenuItem, filteredToolStripMenuItem, rawToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(340, 38);
            viewToolStripMenuItem.Text = "View";
            // 
            // rGBToolStripMenuItem
            // 
            rGBToolStripMenuItem.Name = "rGBToolStripMenuItem";
            rGBToolStripMenuItem.Size = new Size(227, 44);
            rGBToolStripMenuItem.Text = "RGB";
            rGBToolStripMenuItem.Click += rGBToolStripMenuItem_Click;
            // 
            // filteredToolStripMenuItem
            // 
            filteredToolStripMenuItem.Name = "filteredToolStripMenuItem";
            filteredToolStripMenuItem.Size = new Size(227, 44);
            filteredToolStripMenuItem.Text = "Filtered";
            filteredToolStripMenuItem.Click += filteredToolStripMenuItem_Click;
            // 
            // rawToolStripMenuItem
            // 
            rawToolStripMenuItem.Name = "rawToolStripMenuItem";
            rawToolStripMenuItem.Size = new Size(227, 44);
            rawToolStripMenuItem.Text = "Raw";
            rawToolStripMenuItem.Click += rawToolStripMenuItem_Click;
            // 
            // moveStageToImageToolStripMenuItem
            // 
            moveStageToImageToolStripMenuItem.Name = "moveStageToImageToolStripMenuItem";
            moveStageToImageToolStripMenuItem.Size = new Size(340, 38);
            moveStageToImageToolStripMenuItem.Text = "Move Stage To Image";
            moveStageToImageToolStripMenuItem.DropDownOpening += goToImageToolStripMenuItem_DropDownOpening;
            moveStageToImageToolStripMenuItem.DropDownItemClicked += moveStageToImageToolStripMenuItem_DropDownItemClicked;
            moveStageToImageToolStripMenuItem.Click += moveStageToImageToolStripMenuItem_Click;
            // 
            // goToToolStripMenuItem
            // 
            goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            goToToolStripMenuItem.Size = new Size(340, 38);
            goToToolStripMenuItem.Text = "Go To";
            goToToolStripMenuItem.Click += goToToolStripMenuItem_Click_1;
            // 
            // goToImageToolStripMenuItem
            // 
            goToImageToolStripMenuItem.Name = "goToImageToolStripMenuItem";
            goToImageToolStripMenuItem.Size = new Size(340, 38);
            goToImageToolStripMenuItem.Text = "Go To Image";
            goToImageToolStripMenuItem.DropDownOpening += goToImageToolStripMenuItem_DropDownOpening;
            goToImageToolStripMenuItem.DropDownItemClicked += goToImageToolStripMenuItem_DropDownItemClicked;
            goToImageToolStripMenuItem.Click += goToImageToolStripMenuItem_Click;
            // 
            // goToStageToolStripMenuItem
            // 
            goToStageToolStripMenuItem.Name = "goToStageToolStripMenuItem";
            goToStageToolStripMenuItem.Size = new Size(340, 38);
            goToStageToolStripMenuItem.Text = "Go To Stage";
            goToStageToolStripMenuItem.Click += goToStageToolStripMenuItem_Click;
            // 
            // controlsToolStripMenuItem
            // 
            controlsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hideControlsToolStripMenuItem1, hideStatusToolStripMenuItem, hideOverViewToolStripMenuItem });
            controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            controlsToolStripMenuItem.Size = new Size(340, 38);
            controlsToolStripMenuItem.Text = "Controls";
            // 
            // hideControlsToolStripMenuItem1
            // 
            hideControlsToolStripMenuItem1.Name = "hideControlsToolStripMenuItem1";
            hideControlsToolStripMenuItem1.Size = new Size(306, 44);
            hideControlsToolStripMenuItem1.Text = "Hide Controls";
            hideControlsToolStripMenuItem1.Click += hideControlsToolStripMenuItem_Click;
            // 
            // hideStatusToolStripMenuItem
            // 
            hideStatusToolStripMenuItem.Name = "hideStatusToolStripMenuItem";
            hideStatusToolStripMenuItem.Size = new Size(306, 44);
            hideStatusToolStripMenuItem.Text = "Hide Status";
            hideStatusToolStripMenuItem.Click += HideStatusMenuItem_Click;
            // 
            // hideOverViewToolStripMenuItem
            // 
            hideOverViewToolStripMenuItem.Name = "hideOverViewToolStripMenuItem";
            hideOverViewToolStripMenuItem.Size = new Size(306, 44);
            hideOverViewToolStripMenuItem.Text = "Hide OverView";
            hideOverViewToolStripMenuItem.Click += hideOverviewToolStripMenuItem_Click;
            // 
            // copyViewToClipboardToolStripMenuItem
            // 
            copyViewToClipboardToolStripMenuItem.Name = "copyViewToClipboardToolStripMenuItem";
            copyViewToClipboardToolStripMenuItem.Size = new Size(340, 38);
            copyViewToClipboardToolStripMenuItem.Text = "Copy View to Clipboard";
            copyViewToClipboardToolStripMenuItem.Click += copyViewToClipboardToolStripMenuItem_Click;
            // 
            // layersToolStripMenuItem
            // 
            layersToolStripMenuItem.Name = "layersToolStripMenuItem";
            layersToolStripMenuItem.Size = new Size(340, 38);
            layersToolStripMenuItem.Text = "Layers";
            layersToolStripMenuItem.Click += layersToolStripMenuItem_Click;
            // 
            // removeImageToolStripMenuItem
            // 
            removeImageToolStripMenuItem.Name = "removeImageToolStripMenuItem";
            removeImageToolStripMenuItem.Size = new Size(340, 38);
            removeImageToolStripMenuItem.Text = "Remove Image";
            removeImageToolStripMenuItem.Click += removeImageToolStripMenuItem_Click;
            // 
            // removeImagesToolStripMenuItem
            // 
            removeImagesToolStripMenuItem.Name = "removeImagesToolStripMenuItem";
            removeImagesToolStripMenuItem.Size = new Size(340, 38);
            removeImagesToolStripMenuItem.Text = "Remove Images";
            removeImagesToolStripMenuItem.Click += removeImagesToolStripMenuItem_Click;
            // 
            // zPlayMenuStrip
            // 
            zPlayMenuStrip.ImageScalingSize = new Size(32, 32);
            zPlayMenuStrip.Items.AddRange(new ToolStripItem[] { playZToolStripMenuItem, stopZToolStripMenuItem, playSpeedToolStripMenuItem, setValueRangeToolStripMenuItem, loopZToolStripMenuItem });
            zPlayMenuStrip.Name = "zPlayMenuStrip";
            zPlayMenuStrip.Size = new Size(262, 204);
            // 
            // playZToolStripMenuItem
            // 
            playZToolStripMenuItem.Name = "playZToolStripMenuItem";
            playZToolStripMenuItem.Size = new Size(261, 40);
            playZToolStripMenuItem.Text = "Play";
            playZToolStripMenuItem.Click += playZToolStripMenuItem_Click;
            // 
            // stopZToolStripMenuItem
            // 
            stopZToolStripMenuItem.Checked = true;
            stopZToolStripMenuItem.CheckState = CheckState.Checked;
            stopZToolStripMenuItem.Name = "stopZToolStripMenuItem";
            stopZToolStripMenuItem.Size = new Size(261, 40);
            stopZToolStripMenuItem.Text = "Stop";
            stopZToolStripMenuItem.Click += stopZToolStripMenuItem_Click;
            // 
            // playSpeedToolStripMenuItem
            // 
            playSpeedToolStripMenuItem.Name = "playSpeedToolStripMenuItem";
            playSpeedToolStripMenuItem.Size = new Size(261, 40);
            playSpeedToolStripMenuItem.Text = "Play Speed";
            playSpeedToolStripMenuItem.Click += playSpeedToolStripMenuItem_Click;
            // 
            // setValueRangeToolStripMenuItem
            // 
            setValueRangeToolStripMenuItem.Name = "setValueRangeToolStripMenuItem";
            setValueRangeToolStripMenuItem.Size = new Size(261, 40);
            setValueRangeToolStripMenuItem.Text = "Set Value Range";
            setValueRangeToolStripMenuItem.Click += setValueRangeToolStripMenuItem_Click;
            // 
            // loopZToolStripMenuItem
            // 
            loopZToolStripMenuItem.Checked = true;
            loopZToolStripMenuItem.CheckOnClick = true;
            loopZToolStripMenuItem.CheckState = CheckState.Checked;
            loopZToolStripMenuItem.Name = "loopZToolStripMenuItem";
            loopZToolStripMenuItem.ShowShortcutKeys = false;
            loopZToolStripMenuItem.Size = new Size(261, 40);
            loopZToolStripMenuItem.Text = "Loop";
            loopZToolStripMenuItem.Click += loopZToolStripMenuItem_Click;
            // 
            // channelBoxB
            // 
            channelBoxB.BackColor = Color.FromArgb(39, 73, 112);
            channelBoxB.DropDownWidth = 150;
            channelBoxB.ForeColor = Color.White;
            channelBoxB.FormattingEnabled = true;
            channelBoxB.Location = new Point(633, 4);
            channelBoxB.Margin = new Padding(7, 6, 7, 6);
            channelBoxB.Name = "channelBoxB";
            channelBoxB.Size = new Size(255, 40);
            channelBoxB.TabIndex = 8;
            channelBoxB.SelectedIndexChanged += channelBoxB_SelectedIndexChanged;
            // 
            // channelBoxG
            // 
            channelBoxG.BackColor = Color.FromArgb(39, 73, 112);
            channelBoxG.DropDownWidth = 150;
            channelBoxG.ForeColor = Color.White;
            channelBoxG.FormattingEnabled = true;
            channelBoxG.Location = new Point(360, 4);
            channelBoxG.Margin = new Padding(7, 6, 7, 6);
            channelBoxG.Name = "channelBoxG";
            channelBoxG.Size = new Size(255, 40);
            channelBoxG.TabIndex = 6;
            channelBoxG.SelectedIndexChanged += channelBoxG_SelectedIndexChanged;
            // 
            // labelRGB
            // 
            labelRGB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelRGB.AutoSize = true;
            labelRGB.ForeColor = Color.White;
            labelRGB.Location = new Point(9, -137);
            labelRGB.Margin = new Padding(7, 0, 7, 0);
            labelRGB.Name = "labelRGB";
            labelRGB.Size = new Size(58, 32);
            labelRGB.TabIndex = 5;
            labelRGB.Text = "RGB";
            // 
            // channelBoxR
            // 
            channelBoxR.BackColor = Color.FromArgb(39, 73, 112);
            channelBoxR.DropDownWidth = 150;
            channelBoxR.ForeColor = Color.White;
            channelBoxR.FormattingEnabled = true;
            channelBoxR.Location = new Point(87, 4);
            channelBoxR.Margin = new Padding(7, 6, 7, 6);
            channelBoxR.Name = "channelBoxR";
            channelBoxR.Size = new Size(255, 40);
            channelBoxR.TabIndex = 4;
            channelBoxR.SelectedIndexChanged += channelBoxR_SelectedIndexChanged;
            // 
            // rgbBoxsPanel
            // 
            rgbBoxsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rgbBoxsPanel.BackColor = Color.FromArgb(49, 91, 138);
            rgbBoxsPanel.Controls.Add(channelBoxR);
            rgbBoxsPanel.Controls.Add(channelBoxB);
            rgbBoxsPanel.Controls.Add(labelRGB);
            rgbBoxsPanel.Controls.Add(channelBoxG);
            rgbBoxsPanel.ForeColor = Color.White;
            rgbBoxsPanel.Location = new Point(0, 124);
            rgbBoxsPanel.Margin = new Padding(7, 6, 7, 6);
            rgbBoxsPanel.Name = "rgbBoxsPanel";
            rgbBoxsPanel.Size = new Size(927, 62);
            rgbBoxsPanel.TabIndex = 13;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.ForeColor = Color.White;
            statusLabel.Location = new Point(17, 15);
            statusLabel.Margin = new Padding(7, 0, 7, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 32);
            statusLabel.TabIndex = 3;
            // 
            // tLabel
            // 
            tLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tLabel.AutoSize = true;
            tLabel.BackColor = Color.FromArgb(49, 91, 138);
            tLabel.ForeColor = Color.White;
            tLabel.Location = new Point(9, 77);
            tLabel.Margin = new Padding(7, 0, 7, 0);
            tLabel.Name = "tLabel";
            tLabel.Size = new Size(27, 32);
            tLabel.TabIndex = 13;
            tLabel.Text = "T";
            // 
            // tBar
            // 
            tBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tBar.AutoSize = false;
            tBar.BackColor = Color.FromArgb(49, 91, 138);
            tBar.ContextMenuStrip = timePlayMenuStrip;
            tBar.LargeChange = 1;
            tBar.Location = new Point(33, 62);
            tBar.Margin = new Padding(0);
            tBar.Name = "tBar";
            tBar.Size = new Size(895, 62);
            tBar.TabIndex = 16;
            tBar.ValueChanged += timeBar_ValueChanged;
            // 
            // controlsMenuStrip
            // 
            controlsMenuStrip.ImageScalingSize = new Size(32, 32);
            controlsMenuStrip.Items.AddRange(new ToolStripItem[] { hideControlsToolStripMenuItem });
            controlsMenuStrip.Name = "controlsMenuStrip";
            controlsMenuStrip.Size = new Size(235, 42);
            // 
            // hideControlsToolStripMenuItem
            // 
            hideControlsToolStripMenuItem.Name = "hideControlsToolStripMenuItem";
            hideControlsToolStripMenuItem.Size = new Size(234, 38);
            hideControlsToolStripMenuItem.Text = "Hide Controls";
            hideControlsToolStripMenuItem.Click += hideControlsToolStripMenuItem_Click;
            // 
            // cPlayMenuStrip
            // 
            cPlayMenuStrip.ImageScalingSize = new Size(32, 32);
            cPlayMenuStrip.Items.AddRange(new ToolStripItem[] { playCToolStripMenuItem, stopCToolStripMenuItem, CPlaySpeedToolStripMenuItem, setCValueRangeToolStripMenuItem, loopCToolStripMenuItem });
            cPlayMenuStrip.Name = "zPlayMenuStrip";
            cPlayMenuStrip.Size = new Size(262, 204);
            // 
            // playCToolStripMenuItem
            // 
            playCToolStripMenuItem.Name = "playCToolStripMenuItem";
            playCToolStripMenuItem.Size = new Size(261, 40);
            playCToolStripMenuItem.Text = "Play";
            playCToolStripMenuItem.Click += playCToolStripMenuItem_Click;
            // 
            // stopCToolStripMenuItem
            // 
            stopCToolStripMenuItem.Checked = true;
            stopCToolStripMenuItem.CheckState = CheckState.Checked;
            stopCToolStripMenuItem.Name = "stopCToolStripMenuItem";
            stopCToolStripMenuItem.Size = new Size(261, 40);
            stopCToolStripMenuItem.Text = "Stop";
            stopCToolStripMenuItem.Click += stopCToolStripMenuItem_Click;
            // 
            // CPlaySpeedToolStripMenuItem
            // 
            CPlaySpeedToolStripMenuItem.Name = "CPlaySpeedToolStripMenuItem";
            CPlaySpeedToolStripMenuItem.Size = new Size(261, 40);
            CPlaySpeedToolStripMenuItem.Text = "Play Speed";
            CPlaySpeedToolStripMenuItem.Click += CPlaySpeedToolStripMenuItem_Click;
            // 
            // setCValueRangeToolStripMenuItem
            // 
            setCValueRangeToolStripMenuItem.Name = "setCValueRangeToolStripMenuItem";
            setCValueRangeToolStripMenuItem.Size = new Size(261, 40);
            setCValueRangeToolStripMenuItem.Text = "Set Value Range";
            setCValueRangeToolStripMenuItem.Click += setCValueRangeToolStripMenuItem_Click;
            // 
            // loopCToolStripMenuItem
            // 
            loopCToolStripMenuItem.Checked = true;
            loopCToolStripMenuItem.CheckOnClick = true;
            loopCToolStripMenuItem.CheckState = CheckState.Checked;
            loopCToolStripMenuItem.Name = "loopCToolStripMenuItem";
            loopCToolStripMenuItem.ShowShortcutKeys = false;
            loopCToolStripMenuItem.Size = new Size(261, 40);
            loopCToolStripMenuItem.Text = "Loop";
            loopCToolStripMenuItem.Click += loopCToolStripMenuItem_Click;
            // 
            // zBar
            // 
            zBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            zBar.AutoSize = false;
            zBar.BackColor = Color.FromArgb(49, 91, 138);
            zBar.ContextMenuStrip = zPlayMenuStrip;
            zBar.LargeChange = 1;
            zBar.Location = new Point(33, 2);
            zBar.Margin = new Padding(0);
            zBar.Name = "zBar";
            zBar.Size = new Size(895, 62);
            zBar.TabIndex = 12;
            zBar.ValueChanged += zBar_ValueChanged;
            // 
            // zLabel
            // 
            zLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            zLabel.AutoSize = true;
            zLabel.BackColor = Color.FromArgb(49, 91, 138);
            zLabel.ForeColor = Color.White;
            zLabel.Location = new Point(9, 11);
            zLabel.Margin = new Padding(7, 0, 7, 0);
            zLabel.Name = "zLabel";
            zLabel.Size = new Size(28, 32);
            zLabel.TabIndex = 9;
            zLabel.Text = "Z";
            // 
            // cBar
            // 
            cBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cBar.AutoSize = false;
            cBar.BackColor = Color.FromArgb(49, 91, 138);
            cBar.ContextMenuStrip = cPlayMenuStrip;
            cBar.LargeChange = 1;
            cBar.Location = new Point(33, 124);
            cBar.Margin = new Padding(0);
            cBar.Name = "cBar";
            cBar.Size = new Size(895, 62);
            cBar.TabIndex = 15;
            cBar.ValueChanged += cBar_ValueChanged;
            // 
            // cLabel
            // 
            cLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cLabel.AutoSize = true;
            cLabel.BackColor = Color.FromArgb(49, 91, 138);
            cLabel.ForeColor = Color.White;
            cLabel.Location = new Point(9, 134);
            cLabel.Margin = new Padding(7, 0, 7, 0);
            cLabel.Name = "cLabel";
            cLabel.Size = new Size(29, 32);
            cLabel.TabIndex = 15;
            cLabel.Text = "C";
            // 
            // timelineTimer
            // 
            timelineTimer.Interval = 32;
            timelineTimer.Tick += timer_Tick;
            // 
            // zTimer
            // 
            zTimer.Interval = 32;
            zTimer.Tick += zTimer_Tick;
            // 
            // cTimer
            // 
            cTimer.Interval = 32;
            cTimer.Tick += cTimer_Tick;
            // 
            // trackBarPanel
            // 
            trackBarPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBarPanel.BackColor = Color.FromArgb(49, 91, 138);
            trackBarPanel.ContextMenuStrip = controlsMenuStrip;
            trackBarPanel.Controls.Add(cBar);
            trackBarPanel.Controls.Add(zBar);
            trackBarPanel.Controls.Add(tBar);
            trackBarPanel.Controls.Add(tLabel);
            trackBarPanel.Controls.Add(zLabel);
            trackBarPanel.Controls.Add(cLabel);
            trackBarPanel.Controls.Add(rgbBoxsPanel);
            trackBarPanel.Location = new Point(0, 721);
            trackBarPanel.Margin = new Padding(7, 6, 7, 6);
            trackBarPanel.Name = "trackBarPanel";
            trackBarPanel.Size = new Size(927, 186);
            trackBarPanel.TabIndex = 17;
            // 
            // statusPanel
            // 
            statusPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusPanel.BackColor = Color.FromArgb(49, 91, 138);
            statusPanel.ContextMenuStrip = contextMenuStrip;
            statusPanel.Controls.Add(ticksLabel);
            statusPanel.Controls.Add(statusLabel);
            statusPanel.Location = new Point(0, 0);
            statusPanel.Margin = new Padding(7, 6, 7, 6);
            statusPanel.Name = "statusPanel";
            statusPanel.Size = new Size(927, 62);
            statusPanel.TabIndex = 18;
            // 
            // ticksLabel
            // 
            ticksLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ticksLabel.AutoSize = true;
            ticksLabel.ForeColor = Color.White;
            ticksLabel.Location = new Point(726, 15);
            ticksLabel.Margin = new Padding(7, 0, 7, 0);
            ticksLabel.Name = "ticksLabel";
            ticksLabel.Size = new Size(0, 32);
            ticksLabel.TabIndex = 4;
            // 
            // showControlsToolStripMenuItem
            // 
            showControlsToolStripMenuItem.Name = "showControlsToolStripMenuItem";
            showControlsToolStripMenuItem.Size = new Size(199, 22);
            showControlsToolStripMenuItem.Text = "Hide Controls";
            showControlsToolStripMenuItem.Click += showControlsToolStripMenuItem_Click;
            // 
            // statusMenuStrip
            // 
            statusMenuStrip.ImageScalingSize = new Size(32, 32);
            statusMenuStrip.Items.AddRange(new ToolStripItem[] { HideStatusMenuItem });
            statusMenuStrip.Name = "controlsMenuStrip";
            statusMenuStrip.Size = new Size(210, 42);
            // 
            // HideStatusMenuItem
            // 
            HideStatusMenuItem.Name = "HideStatusMenuItem";
            HideStatusMenuItem.Size = new Size(209, 38);
            HideStatusMenuItem.Text = "Hide Status";
            HideStatusMenuItem.Click += HideStatusMenuItem_Click;
            // 
            // pictureBox
            // 
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.ContextMenuStrip = contextMenuStrip;
            pictureBox.Location = new Point(0, 62);
            pictureBox.Margin = new Padding(0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(893, 619);
            pictureBox.TabIndex = 20;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += rgbPictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // overlayPictureBox
            // 
            overlayPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            overlayPictureBox.BackColor = Color.Transparent;
            overlayPictureBox.ContextMenuStrip = contextMenuStrip;
            overlayPictureBox.Location = new Point(0, 62);
            overlayPictureBox.Margin = new Padding(0);
            overlayPictureBox.Name = "overlayPictureBox";
            overlayPictureBox.Size = new Size(888, 619);
            overlayPictureBox.TabIndex = 19;
            overlayPictureBox.TabStop = false;
            overlayPictureBox.Paint += overlayPictureBox_Paint;
            overlayPictureBox.MouseDoubleClick += pictureBox_MouseDoubleClick;
            overlayPictureBox.MouseDown += pictureBox_MouseDown;
            overlayPictureBox.MouseMove += rgbPictureBox_MouseMove;
            overlayPictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // hScrollBar
            // 
            hScrollBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            hScrollBar.Location = new Point(0, 681);
            hScrollBar.Name = "hScrollBar";
            hScrollBar.Size = new Size(929, 34);
            hScrollBar.SmallChange = 10;
            hScrollBar.TabIndex = 24;
            hScrollBar.Visible = false;
            hScrollBar.ValueChanged += vScrollBar_ValueChanged;
            // 
            // vScrollBar
            // 
            vScrollBar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            vScrollBar.Location = new Point(888, 62);
            vScrollBar.Name = "vScrollBar";
            vScrollBar.Size = new Size(39, 619);
            vScrollBar.SmallChange = 10;
            vScrollBar.TabIndex = 23;
            vScrollBar.Visible = false;
            vScrollBar.ValueChanged += vScrollBar_ValueChanged;
            // 
            // saveCSVFileDialog
            // 
            saveCSVFileDialog.DefaultExt = "csv";
            saveCSVFileDialog.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            saveCSVFileDialog.Title = "Save ROIs to CSV";
            // 
            // ImageView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(95, 122, 156);
            Controls.Add(vScrollBar);
            Controls.Add(hScrollBar);
            Controls.Add(trackBarPanel);
            Controls.Add(statusPanel);
            Controls.Add(overlayPictureBox);
            Controls.Add(pictureBox);
            Margin = new Padding(0);
            MinimumSize = new Size(217, 245);
            Name = "ImageView";
            Size = new Size(927, 907);
            KeyDown += ImageView_KeyDown;
            KeyPress += ImageView_KeyPress;
            KeyUp += ImageView_KeyUp;
            timePlayMenuStrip.ResumeLayout(false);
            contextMenuStrip.ResumeLayout(false);
            zPlayMenuStrip.ResumeLayout(false);
            rgbBoxsPanel.ResumeLayout(false);
            rgbBoxsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tBar).EndInit();
            controlsMenuStrip.ResumeLayout(false);
            cPlayMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)zBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)cBar).EndInit();
            trackBarPanel.ResumeLayout(false);
            trackBarPanel.PerformLayout();
            statusPanel.ResumeLayout(false);
            statusPanel.PerformLayout();
            statusMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)overlayPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ComboBox channelBoxR;
        private System.Windows.Forms.ComboBox channelBoxB;
        private System.Windows.Forms.ComboBox channelBoxG;
        private System.Windows.Forms.Label labelRGB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Panel rgbBoxsPanel;
        private System.Windows.Forms.Timer timelineTimer;
        private System.Windows.Forms.ContextMenuStrip zPlayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem playZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopZToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip timePlayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem playTimeToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem stopTimeToolStripMenu;
        private System.Windows.Forms.Timer zTimer;
        private System.Windows.Forms.ToolStripMenuItem playSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playSpeedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setValueRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setValueRangeToolStripMenuItem1;
        private System.Windows.Forms.Timer cTimer;
        private System.Windows.Forms.ContextMenuStrip cPlayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem playCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CPlaySpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCValueRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loopZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loopTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loopCToolStripMenuItem;
        private System.Windows.Forms.TrackBar cBar;
        private System.Windows.Forms.TrackBar zBar;
        private System.Windows.Forms.Label cLabel;
        private System.Windows.Forms.Label zLabel;
        private System.Windows.Forms.Label tLabel;
        private System.Windows.Forms.TrackBar tBar;
        private System.Windows.Forms.Panel trackBarPanel;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label ticksLabel;
        private System.Windows.Forms.ToolStripMenuItem copyViewToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showControlsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip controlsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem hideControlsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip statusMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HideStatusMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox overlayPictureBox;
        private System.Windows.Forms.SaveFileDialog saveCSVFileDialog;
        private System.Windows.Forms.ToolStripMenuItem rOIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setROITextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyROIToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteROIToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteROIToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filteredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideControlsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hideStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToStageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveStageToImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillToolStripMenuItem;
        private ToolStripMenuItem layersToolStripMenuItem;
        private ToolStripMenuItem hideOverViewToolStripMenuItem;
        private ToolStripMenuItem removeImageToolStripMenuItem;
        private ToolStripMenuItem removeImagesToolStripMenuItem;
    }
}
