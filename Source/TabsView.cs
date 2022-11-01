using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Bio
{
    public partial class TabsView : Form
    {
        public static bool init = false;
        public Filter filters = null;
        public static System.Drawing.Graphics graphics = null;
        public ImageView Viewer
        {
            get
            {
                if (tabControl.TabPages.Count == 0)
                    return null;
                if (tabControl.SelectedIndex == -1)
                    return null;
                if (tabControl.SelectedTab.Controls.Count == 0)
                    return null;
                return (ImageView)tabControl.SelectedTab.Controls[0];
            }
        }
        public static BioImage SelectedImage
        {
            get
            {
                return App.tabsView.Image;
            }
        }
        public static ImageView SelectedViewer
        {
            get
            {
                return App.tabsView.ImageView;
            }
        }
        public TabsView(BioImage arg)
        {
            InitializeComponent();
            LoadProperties();
            ResizeView();
            Init();
        }
        public TabsView()
        {
            InitializeComponent();
            LoadProperties();
            ResizeView();
            Init();
        }
        public TabsView(string[] arg)
        {
            InitializeComponent();
            LoadProperties();
            Init();
            if (arg.Length == 0)
                return;
            else
            {             
                for (int i = 0; i < arg.Length; i++)
                {
                    if (arg[i].EndsWith(".cs"))
                    {
                        App.runner.RunScriptFile(arg[0]);
                        return;
                    }
                    if(arg[i].EndsWith(".ijm"))
                    {
                        ImageJ.RunMacro(arg[i], "");
                        return;
                    }
                    else
                    {
                        AddTab(BioImage.OpenFile(arg[i]));
                    }
                }
            }
        }
        public void AddTab(BioImage b)
        {
            if (b == null)
                return;
            if (b.filename.Contains("/") || b.filename.Contains("\\"))
                b.filename = Path.GetFileName(b.filename);
            TabPage t = new TabPage(b.filename);
            ImageView v = new ImageView(b);
            v.Dock = DockStyle.Fill;
            t.Controls.Add(v);
            if(Width < b.SizeX || Height < b.SizeY)
            {
                Width = b.SizeX;
                Height = b.SizeY + 190;
            }
            tabControl.TabPages.Add(t);
            tabControl.Dock = DockStyle.Fill;
            tabControl.SelectedIndex = tabControl.TabCount - 1;
            ResizeView();
        }
        private void Init()
        {
            TabPage t = new TabPage("Viewer");
            ImageView v = new ImageView();
            v.Dock = DockStyle.Fill;
            t.Controls.Add(v);
            tabControl.TabPages.Add(t);
            filters = new Filter();
            init = true;
        }

        public void ResizeView()
        {
            if (Image == null)
                return;
            System.Drawing.Size s;
            if(SelectedImage.isPyramidal)
                s = new System.Drawing.Size(ImageView.SelectedImage.Resolutions[ImageView.Resolution].SizeX + 42, ImageView.SelectedImage.Resolutions[ImageView.Resolution].SizeY + 206);
            else
                s = new System.Drawing.Size(ImageView.SelectedImage.SizeX + 20, ImageView.SelectedImage.SizeY + 180);
            if (s.Width > Screen.PrimaryScreen.Bounds.Width || s.Height > Screen.PrimaryScreen.Bounds.Height)
            {
                this.WindowState = FormWindowState.Maximized;
                Viewer.GoToImage();
            }
            else
            {
                Size = s;
                Viewer.GoToImage();
            }
        }

        public BioImage Image
        {
            get 
            {
                if (Viewer == null)
                    return null;
                if (ImageView.SelectedImage == null)
                    return null;
                return ImageView.SelectedImage; 
            }
        }

        public ImageView ImageView
        {
            get
            {
                return Viewer;
            }
        }

        public int TabCount
        {
            get
            {
                return tabControl.TabPages.Count;
            }
        }

        public void OpenInNewProcess(string file)
        {
            Process p = new Process();
            p.StartInfo.FileName = Application.ExecutablePath;
            p.StartInfo.Arguments = file;
            p.Start();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            int img = Images.images.Count;
            foreach (string item in openFilesDialog.FileNames)
            {
                BioImage im = BioImage.OpenFile(item);
                if (im == null)
                    return;
                AddTab(im);
                if (!App.recent.Contains(im.ID))
                    App.recent.Add(im.ID);
            }
            foreach (string item in App.recent)
            {
                openRecentToolStripMenuItem.DropDownItems.Add(item, null, ItemClicked);
            }
        }

        private void SaveProperties()
        {
            string s = "";
            for (int i = 0; i < App.recent.Count; i++)
            {
                 s += "$"+App.recent[i];
            }
            Properties.Settings.Default["Recent"] = s;
            Properties.Settings.Default.Save();
        }

        private void LoadProperties()
        {
            App.recent.Clear();
            string s = (string)Properties.Settings.Default["Recent"];
            string[] sts = s.Split('$');
            foreach (string item in sts)
            {
                if(item!="")
                    App.recent.Add(item);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Viewer != null)
            {
                Images.RemoveImage(SelectedImage);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Image == null)
                return;
            saveTiffFileDialog.FileName = Path.GetFileNameWithoutExtension(Image.Filename);
            if (saveTiffFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string[] sts = new string[1];
            sts[0] = ImageView.SelectedImage.ID;
            BioImage.SaveSeries(sts, saveTiffFileDialog.FileName);
        }

        private void ImageViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.S && e.Control)
            {
                saveToolStripMenuItem.PerformClick();
            }
            else
            if (e.KeyCode == Keys.O && e.Control)
            {
                openToolStripMenuItem.PerformClick();
            }
        }

        private void toolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.tools.Show();
        }

        private void exportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveCSVFileDialog.ShowDialog() != DialogResult.OK)
                return;
            BioImage.ExportROIsCSV(saveCSVFileDialog.FileName, ImageView.SelectedImage.Annotations);
        }

        private void importCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openCSVFileDialog.ShowDialog() != DialogResult.OK)
                return;
            ImageView.SelectedImage.Annotations.AddRange(BioImage.ImportROIsCSV(openCSVFileDialog.FileName));
        }

        private void exportROIsOfFolderOfImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;
            saveOMEFileDialog.InitialDirectory = folderBrowserDialog.SelectedPath;
            if (saveCSVFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string f = Path.GetFileName(saveCSVFileDialog.FileName);

            BioImage.ExportROIFolder(folderBrowserDialog.SelectedPath, f);
        }

        private void rOIManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.manager.Show();
        }

        private void ImageViewer_Activated(object sender, EventArgs e)
        {
            App.tabsView = this;
            //App.Image = SelectedImage;
        }

        private void channelsToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImageView.SelectedImage == null)
                return;
            if (App.channelsTool == null)
                App.channelsTool = new ChannelsTool(App.Channels);
            App.channelsTool.Show();
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Viewer == null)
                return;
            Viewer.Mode = ImageView.ViewMode.RGBImage;
            filteredToolStripMenuItem.Checked = false;
            rawToolStripMenuItem.Checked = false;
            emissionToolStripMenuItem.Checked = false;
            Viewer.UpdateStatus();
        }

        private void filteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Viewer == null)
                return;
            Viewer.Mode = ImageView.ViewMode.Filtered;
            rGBToolStripMenuItem.Checked = false;
            rawToolStripMenuItem.Checked = false;
            emissionToolStripMenuItem.Checked = false;
            Viewer.UpdateStatus();
        }

        private void rawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Viewer == null)
                return;
            Viewer.Mode = ImageView.ViewMode.Raw;
            rGBToolStripMenuItem.Checked = false;
            filteredToolStripMenuItem.Checked = false;
            emissionToolStripMenuItem.Checked = false;
            Viewer.UpdateStatus();
        }

        private void autoThresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Viewer == null)
                return;
            BioImage.AutoThreshold(ImageView.SelectedImage, true);
            if (ImageView.SelectedImage.bitsPerPixel > 8)
                ImageView.SelectedImage.StackThreshold(true);
            else
                ImageView.SelectedImage.StackThreshold(false);
        }

        public void UpdateViewMode(ImageView.ViewMode v)
        {
            if (v == ImageView.ViewMode.RGBImage)
                rGBToolStripMenuItem.Checked = true;
            if (v == ImageView.ViewMode.Filtered)
                filteredToolStripMenuItem.Checked = true;
            if(v == ImageView.ViewMode.Raw)
                rawToolStripMenuItem.Checked = true;
        }

        private void scriptRunnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.runner.WindowState = FormWindowState.Normal;
            App.runner.Show();
        }

        private void stackToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.stackTools.Show();
        }

        private void scriptRecorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recorder.recorder.WindowState = FormWindowState.Normal;
            Recorder.recorder.Show();
        }

        private void saveOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Image == null)
                return;
            saveOMEFileDialog.FileName = Path.GetFileNameWithoutExtension(ImageView.SelectedImage.Filename);
            if (saveOMEFileDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string file in saveOMEFileDialog.FileNames)
            {
                BioImage.SaveOME(file,Image.ID);
            }
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTool tool = new SetTool();
            tool.Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Here we update the tabs based on images in the table.

        }

        private void filtersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        }

        private void to8BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To8Bit();
        }

        private void to16BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To16Bit();
        }

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters = new Filter();
            filters.Show();
        }

        private void bit8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To8Bit();
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        private void bit16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To16Bit();
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        private void to24BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To24Bit();
            //We update the viewer gui since there are less planes now.
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        private void to48BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To48Bit();
            //We update the viewer gui since there are less planes now.
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        private void to32BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To32Bit();
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        private void toWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == -1)
                return;
            BioImage im = ImageView.SelectedImage;
            ImageWindow vi = new ImageWindow(im);
            vi.Show();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == -1)
                return;
            //We update the view status based on tab.
            if (Viewer.Mode == ImageView.ViewMode.Raw)
                rawToolStripMenuItem.Checked = true;
            else
                rawToolStripMenuItem.Checked = false;
            if (Viewer.Mode == ImageView.ViewMode.Filtered)
                filteredToolStripMenuItem.Checked = true;
            else
                filteredToolStripMenuItem.Checked = false;
            if (Viewer.Mode == ImageView.ViewMode.RGBImage)
                rGBToolStripMenuItem.Checked = true;
            else
                rGBToolStripMenuItem.Checked = false;
            //We update the active Viewer.
            App.viewer = Viewer;
            ImageView.SelectedIndex = 0;
        }

        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == -1)
                return;
            ImageView v = (ImageView)tabControl.SelectedTab.Controls[0];
            tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);
            v.Dispose();

        }

        private void openOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> sts = new List<string>();
            foreach (BioImage item in Images.images)
            {
                BioImage.Save(Image.ID, Image.ID);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void scriptRunnerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            App.runner.Show();
        }

        private void scriptRecorderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            App.recorder.Show();
        }

        private void openOMEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string sts in openFilesDialog.FileNames)
            {
                BioImage im = BioImage.OpenOME(sts);
                if (im == null)
                    return;
                AddTab(im);
            }
        }

        private void newTabViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void nodeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.nodeView.Show();
            App.nodeView.ShowInTaskbar = true;
        }
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveTiffFileDialog.ShowDialog() == DialogResult.OK)
                Bio.BioImage.Save(ImageView.SelectedImage.ID, saveTiffFileDialog.FileName);
        }

        private void saveOMEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveOMEFileDialog.ShowDialog() == DialogResult.OK)
                Bio.BioImage.Save(ImageView.SelectedImage.ID, saveOMEFileDialog.FileName);
        }
        private void openRecentToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            openRecentToolStripMenuItem.DropDownItems.Clear();
            foreach (string item in App.recent)
            {
                openRecentToolStripMenuItem.DropDownItems.Add(item, null, ItemClicked);
            }
        }
        private void ItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (!BioImage.isOME(ts.Text))
            {
                BioImage[] bs = BioImage.OpenSeries(ts.Text);
                for (int i = 0; i < bs.Length; i++)
                {
                    if (i == 0)
                        AddTab(bs[i]);
                    else
                        Viewer.AddImage(bs[i]);
                }
            }
            else
            {
                BioImage[] bs = BioImage.OpenOMESeries(ts.Text);
                if (bs.Length > 0)
                {
                    if (bs.Length == 1)
                        AddTab(bs[0]);
                    else
                    {
                        OpenInTab tb = new OpenInTab();
                        if (tb.ShowDialog() == DialogResult.Yes)
                        {
                            for (int i = 0; i < bs.Length; i++)
                            {
                                if (i == 0)
                                    AddTab(bs[i]);
                                else
                                    Viewer.AddImage(bs[i]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < bs.Length; i++)
                            {
                                AddTab(bs[i]);
                            }
                        }
                    }
                }
            }
            App.viewer.GoToImage();
            SaveProperties();
        }
        private void TabsView_KeyDown(object sender, KeyEventArgs e)
        {
            if (Viewer == null)
                return;
            double moveAmount = 1500 * (1 / App.viewer.Scale.Width);
            if (e.KeyCode == Keys.C && e.Control)
            {
                Viewer.CopySelection();
                return;
            }
            if (e.KeyCode == Keys.V && e.Control)
            {
                Viewer.PasteSelection();
                return;
            }
            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.NumPad7)
            {
                App.viewer.Scale = new SizeF(App.viewer.Scale.Width - 0.1f, App.viewer.Scale.Height - 0.1f);
            }
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.NumPad9)
            {
                App.viewer.Scale = new SizeF(App.viewer.Scale.Width - 0.1f, App.viewer.Scale.Height - 0.1f);
            }
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.NumPad8)
            {
                Viewer.Origin = new PointD(Viewer.Origin.X, Viewer.Origin.Y - moveAmount);
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.NumPad2)
            {
                Viewer.Origin = new PointD(Viewer.Origin.X, Viewer.Origin.Y + moveAmount);
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.NumPad4)
            {
                Viewer.Origin = new PointD(Viewer.Origin.X - moveAmount, Viewer.Origin.Y);
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.NumPad6)
            {
                Viewer.Origin = new PointD(Viewer.Origin.X + moveAmount, Viewer.Origin.Y);
            }
            if (e.KeyCode == Keys.Up)
                Microscope.MoveUp(Microscope.Objective.ViewHeight / 2);
            if (e.KeyCode == Keys.Down)
                Microscope.MoveDown(Microscope.Objective.ViewHeight / 2);
            if (e.KeyCode == Keys.Left)
                Microscope.MoveRight(-(Microscope.Objective.ViewWidth / 2));
            if (e.KeyCode == Keys.Right)
                Microscope.MoveRight(Microscope.Objective.ViewWidth / 2);
            Viewer.UpdateStatus();
            Viewer.UpdateView();
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BioImage im = Image.Copy();
            AddTab(im);
        }

        private void saveSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.seriesTool.Show();
        }

        private void TabsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProperties();
            App.imager.Close();
            App.stage.Close();
            App.nodeView.Close();
            Application.Exit();
        }

        private void automationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.recordings.Show();
        }

        private void stageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            App.viewer.AddImage(BioImage.OpenFile(e.FullPath));
        }

        private void addImagesToTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            for (int i = 0; i < openFilesDialog.FileNames.Length; i++)
            {
                if(i == 0 && tabControl.TabPages.Count == 0)
                {
                    AddTab(BioImage.OpenFile(openFilesDialog.FileNames[0]));
                }
                else
                    App.viewer.AddImage(BioImage.OpenFile(openFilesDialog.FileNames[i]));
            }
            App.viewer.GoToImage();
        }

        private void addImagesOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            for (int i = 0; i < openFilesDialog.FileNames.Length; i++)
            {
                if (i == 0 && tabControl.TabPages.Count == 0)
                {
                    AddTab(BioImage.OpenOME(openFilesDialog.FileNames[0]));
                }
                else
                App.viewer.AddImage(BioImage.OpenOME(openFilesDialog.FileNames[i]));
            }
            App.viewer.GoToImage();
        }

        private void openSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            BioImage[] bs = BioImage.OpenOMESeries(openFilesDialog.FileName);
            if (bs == null)
                return;
            AddTab(bs[0]);
            for (int i = 1; i < bs.Length; i++)
            {
                Viewer.AddImage(bs[i]);
            }
            App.viewer.GoToImage();
        }

        private void saveTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool convert = false;
            foreach (BioImage b in ImageView.Images)
            {
                if (b.isRGB)
                {
                    convert = true;
                    break;
                }
            }
            if (convert)
            {
                string mes;
                if(ImageView.SelectedImage.bitsPerPixel > 8)
                    mes = "Saving Series as OME only supports 8 bit & 16 bit images. Convert 16 bit?";
                else
                    mes = "Saving Series as OME only supports 8 bit & 16 bit images. Convert 8 bit?";
                if (MessageBox.Show(this, mes,"Convert to supported format?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (BioImage b in ImageView.Images)
                    {
                        if (b.bitsPerPixel > 8)
                            b.To16Bit();
                        else
                            b.To8Bit();
                    }
                }
                else
                    return;
            }
            if (saveOMEFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string[] sts = new string[App.viewer.Images.Count];
            for (int i = 0; i < sts.Length; i++)
            {
                sts[i] = App.viewer.Images[i].ID;
            }
            BioImage.SaveOMESeries(sts, saveOMEFileDialog.FileName, Properties.Settings.Default.Planes);
        }

        private void saveTabTiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveTiffFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string[] sts = new string[App.viewer.Images.Count];
            for (int i = 0; i < sts.Length; i++)
            {
                sts[i] = App.viewer.Images[i].Filename;
            }
            BioImage.SaveSeries(sts, saveTiffFileDialog.FileName);
        }

        private void openSeriesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            BioImage[] bms = null;
            foreach (string item in openFilesDialog.FileNames)
            {
                bms = BioImage.OpenSeries(openFilesDialog.FileName);
                for (int i = 0; i < bms.Length; i++)
                {
                    if (i == 0)
                    {
                        AddTab(bms[i]);
                    }
                    else
                    App.viewer.AddImage(bms[i]);
                }
            }
            App.viewer.GoToImage();
        }

        private void TabsView_Load(object sender, EventArgs e)
        {
            if(App.viewer != null)
            App.viewer.GoToImage();
            Function.Initialize();
        }

        private void clearRecentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Recent = "";
            Properties.Settings.Default.Save();
            openRecentToolStripMenuItem.DropDownItems.Clear();
            App.recent.Clear();
        }

        private void rotateToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ImageView.SelectedImage == null)
                return;
            string st = e.ClickedItem.Text;
            RotateFlipType rot = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), st);
            ImageView.SelectedImage.RotateFlip(rot);
            ImageView.UpdateImage();
            ImageView.UpdateView();
        }

        private void rotateToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (rotateToolStripMenuItem.DropDownItems.Count > 0)
                return;
            string[] sts = Enum.GetNames(typeof(RotateFlipType));
            foreach (string item in sts)
            {
                rotateToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void emissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Viewer == null)
                return;
            Viewer.Mode = ImageView.ViewMode.Emission;
            rGBToolStripMenuItem.Checked = false;
            rawToolStripMenuItem.Checked = false;
            filteredToolStripMenuItem.Checked = false;
            emissionToolStripMenuItem.Checked = true;
            Viewer.UpdateStatus();
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.console.Show();
        }

        private void switchRedBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (BufferInfo bf in ImageView.SelectedImage.Buffers)
            {
                bf.SwitchRedBlue();
            }
            ImageView.UpdateImages();
        }
        private void createFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FunctionForm f = new FunctionForm(new Function());
            f.Show();
        }
        private void DropDownItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            Function.Functions[ts.Text].PerformFunction(true);
        }
        private void runToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            runToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in Function.Functions)
            {
                runToolStripMenuItem.DropDownItems.Add(item.Value.Name,null, DropDownItemClicked);
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageView.SelectedImage.Update();
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLView v = new XMLView(BioImage.OpenXML(ImageView.SelectedImage.file));
            v.Show();
        }
    }
}
