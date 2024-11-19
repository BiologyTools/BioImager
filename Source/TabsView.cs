using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;
using AForge;
using RotateFlipType = AForge.RotateFlipType;
using BioLib;
using BioImage = BioLib.BioImage;
namespace BioImager
{
    public partial class TabsView : Form
    {
        public static bool init = false;
        public Filter filters = null;
        public static System.Drawing.Graphics graphics = null;
        /* Returning the ImageView object that is currently selected in the tabControl. */
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
                return App.tabsView.Viewer;
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
        /* A constructor for a class called TabsView. It is initializing the component and loading
        properties. It is also initializing the class. */
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
                    if (arg[i].EndsWith(".ijm"))
                    {
                        ImageJ.RunMacro(arg[i], "");
                        return;
                    }
                    else
                    {
                        BioImage.OpenFile(arg[i], true);
                    }
                }
            }
        }
        /// It creates a new tab, adds an image to it, and then resizes the window to fit the image
        /// 
        /// @param BioImage This is a class that contains the image data, and some other information
        /// about the image.
        /// 
        /// @return The method is returning a void.
        public void AddTab(BioImage b)
        {
            if (b == null)
                return;
            if (b.filename.Contains("/") || b.filename.Contains("\\"))
                b.filename = Path.GetFileName(b.filename);
            if(tabControl.IsHandleCreated)
            tabControl.Invoke((MethodInvoker)delegate {
                TabPage t = new TabPage(b.filename);
                ImageView v = new ImageView(b);
                v.ShowStage = false;
                v.Dock = DockStyle.Fill;
                t.Controls.Add(v);
                tabControl.TabPages.Add(t);
                tabControl.SelectedIndex = tabControl.TabCount - 1;
                ResizeView();
            });
        }
        public bool HasTab(string name)
        {
            foreach (TabPage item in tabControl.TabPages)
            {
                if(item.Text == name)
                {
                    return true;
                }
            }
            return false;
        }
        private void Init()
        {
            TabPage t = new TabPage("Viewer");
            ImageView v = new ImageView();
            v.ShowStage = true;
            v.Dock = DockStyle.Fill;
            t.Controls.Add(v);
            tabControl.TabPages.Add(t);
            filters = new Filter();
            App.tabsView = this;
            Plugins.Initialize();
            string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (char c in a)
            {
                ToolStripMenuItem me = new ToolStripMenuItem(c.ToString());
                foreach (ImageJ.Macro.Command command in ImageJ.Macro.Commands.Values)
                {
                    if (command.Name.StartsWith(c))
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem(command.Name.ToString());
                        menuItem.Click += MenuItem_Click;
                        me.DropDownItems.Add(menuItem);
                    }
                }
                commandToolStripMenuItem.DropDownItems.Add(me);
            }
            foreach (Scripting.Script s in Scripting.Scripts.Values)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(s.name);
                mi.Click += Mi_Click;
                runToolStripMenuItem.DropDownItems.Add(mi);
            }
            foreach (ImageJ.Macro.Command c in ImageJ.Macros)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(c.Name);
                mi.Click += Mi_Click;
                runToolStripMenuItem.DropDownItems.Add(mi);
            }
            ML.ML.Initialize();
            init = true;
        }

        private void Mi_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem m = (ToolStripMenuItem)sender;
            if (m.Text.EndsWith(".ijm") || m.Text.EndsWith(".txt") && !m.Text.EndsWith(".cs"))
            {
                string ma = File.ReadAllText(m.Text);
                ImageJ.RunOnImage(ma, BioConsole.headless, BioConsole.onTab, BioConsole.useBioformats, BioConsole.newTab);
            }
            else
                Scripting.RunByName(m.Text);
        }

        private void MenuItem_Click(object? sender, EventArgs e)
        {
            if (ImageView.SelectedImage == null) return;
            ToolStripMenuItem m = (ToolStripMenuItem)sender;
            ImageJ.RunOnImage("run(\"" + m.Text + "\");", BioConsole.headless, BioConsole.onTab, BioConsole.useBioformats, BioConsole.newTab);
            ToolStripMenuItem mi = new ToolStripMenuItem(m.Text);
            mi.Click += MenuItem_Click;
            bool con = false;
            foreach (ToolStripMenuItem item in recentToolStripMenuItem.DropDownItems)
            {
                if (item.Text == m.Text)
                    con = true;
            }
            if (!con)
                recentToolStripMenuItem.DropDownItems.Add(mi);
        }

        /// If the image is pyramidal, add 42 to the width and 206 to the height. Otherwise, add 20 to
        /// the width and 180 to the height
        /// 
        /// @return The size of the image.
        public void ResizeView()
        {
            if (Image == null)
                return;
            System.Drawing.Size s;
            if (SelectedImage.isPyramidal)
                s = new System.Drawing.Size(ImageView.SelectedImage.Resolutions[Viewer.Level].SizeX + 42, ImageView.SelectedImage.Resolutions[Viewer.Level].SizeY + 206);
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

        /* A property that returns the selected image from the image viewer. */
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

        /* A property that returns the number of tabs in the tab control. */
        public int TabCount
        {
            get
            {
                return tabControl.TabPages.Count;
            }
        }

        /// It creates a new process, sets the file name to the current executable, and sets the
        /// arguments to the file name
        /// 
        /// @param file The file to open
        public void OpenInNewProcess(string file)
        {
            Process p = new Process();
            p.StartInfo.FileName = Application.ExecutablePath;
            p.StartInfo.Arguments = file;
            p.Start();
        }

        /// It opens a file dialog, and if the user selects a file, it opens the file and adds it to the
        /// tab control
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        /// 
        /// @return The return value is the result of the ShowDialog method of the openFilesDialog
        /// object.
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string item in openFilesDialog.FileNames)
            {
                StartProgress("Opening selected image.", "Opening");
                BioImage im = BioImage.OpenFile(item, 0, false, false);
                StopProgress();
                if (im == null)
                    return;
                AddTab(im);
                Images.images.Add(im);
                if (!App.recent.Contains(im.ID))
                    App.recent.Add(im.ID);
            }
            foreach (string item in App.recent)
            {
                openRecentToolStripMenuItem.DropDownItems.Add(item, null, ItemClicked);
            }
        }

        /// It saves the recent list to the settings file
        private void SaveProperties()
        {
            string s = "";
            for (int i = 0; i < App.recent.Count; i++)
            {
                s += "$" + App.recent[i];
            }
            Properties.Settings.Default["Recent"] = s;
            Properties.Settings.Default.Save();
        }

        /// It loads the recent files from the settings file and adds them to the recent files list.
        private void LoadProperties()
        {
            App.recent.Clear();
            string s = (string)Properties.Settings.Default["Recent"];
            string[] sts = s.Split('$');
            foreach (string item in sts)
            {
                if (item != "")
                    App.recent.Add(item);
            }
        }

        private static bool done = false;
        private static Progress progress = new Progress("", "");
        private static string title;
        private static void ProgressUpdate()
        {
            do
            {
                progress.Text = title;
                progress.Status = BioImage.Status;
                progress.ProgressValue = BioImage.Progress;
                Thread.Sleep(250);
            } while (!done);
        }
        private static void StartProgress(string titl, string status)
        {
            done = false;
            title = titl;
            BioImage.Progress = 0;
            progress.Status = status;
            progress.Text = title;
            if (BioImage.Status == "" || BioImage.Status == null)
                progress.Status = status;
            else
                progress.Status = BioImage.Status;
            progress.ProgressValue = BioImage.Progress;
            progress.Show();
            Application.DoEvents();
            Thread th = new Thread(ProgressUpdate);
            th.Start();
        }
        private static void StopProgress()
        {
            done = true;
            progress.Hide();
        }

        /// This function removes the selected image from the list of images
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Viewer != null)
            {
                Images.RemoveImage(SelectedImage);
            }
        }

        /// If the user selects a file to save to, then save the image to that file
        /// 
        /// @param sender System.Object
        /// @param EventArgs 
        /// 
        /// @return The image is being returned.
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Image == null)
                return;
            saveTiffFileDialog.FileName = Path.GetFileNameWithoutExtension(Image.Filename);
            if (saveTiffFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string[] sts = new string[1];
            sts[0] = ImageView.SelectedImage.ID;
            StartProgress("Saving selected Tiff.", "Saving");
            BioImage.SaveSeries(sts, saveTiffFileDialog.FileName);
            StopProgress();
        }

        /// If the user presses the "S" key while holding down the "Control" key, then the "Save" menu
        /// item is clicked
        /// 
        /// @param sender The object that raised the event.
        /// @param PreviewKeyDownEventArgs The event data.
        private void ImageViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                saveToolStripMenuItem.PerformClick();
            }
            else
            if (e.KeyCode == Keys.O && e.Control)
            {
                openToolStripMenuItem.PerformClick();
            }
        }

        /// It shows the toolbox when the user clicks on the toolbox menu item
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void toolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.tools.Show();
        }

        /// If the user clicks the "Export CSV" menu item, then show the save file dialog, and if the
        /// user clicks OK, then export the ROIs to a CSV file
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The file name of the file that was selected.
        private void exportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveCSVFileDialog.ShowDialog() != DialogResult.OK)
                return;
            BioImage.ExportROIsCSV(saveCSVFileDialog.FileName, ImageView.SelectedImage.Annotations);
        }

        /// This function opens a file dialog to select a CSV file, then adds the ROIs in the CSV file
        /// to the current image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The return value is a list of ROIs.
        private void importCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openCSVFileDialog.ShowDialog() != DialogResult.OK)
                return;
            ImageView.SelectedImage.Annotations.AddRange(BioImage.ImportROIsCSV(openCSVFileDialog.FileName));
        }

        /// This function exports the ROIs of all the images in a folder to a CSV file
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The return value is the path of the selected folder.
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

        /// It opens the ROI Manager window
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void rOIManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.manager.Show();
        }

        /// When the ImageViewer is activated, the tabsView is set to the current ImageViewer
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void ImageViewer_Activated(object sender, EventArgs e)
        {
            App.tabsView = this;
            //App.Image = SelectedImage;
        }

        /// If the user clicks on the Channels Tool menu item, then if the Channels Tool window is not
        /// open, open it
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The image that is selected in the ImageView.
        private void channelsToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImageView.SelectedImage == null)
                return;
            if (App.channelsTool == null)
                App.channelsTool = new ChannelsTool(App.Channels);
            App.channelsTool.Show();
        }

        /// This function is called when the user clicks on the RGB menu item. It sets the Viewer.Mode
        /// to ImageView.ViewMode.RGBImage
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The image is being returned.
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

        /// If the Viewer is not null, set the Viewer's mode to filtered, uncheck the other modes, and
        /// update the status
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        /// 
        /// @return The image is being returned.
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

        /// If the Viewer is not null, set the Viewer's mode to Raw, uncheck the other menu items, and
        /// update the status
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The image is being returned.
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

        /// This function is called when the user clicks on the "Auto Threshold" menu item. It calls the
        /// BioImage.AutoThreshold function to automatically threshold the image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        /// 
        /// @return The image is being returned.
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


        /// It updates the view mode of the image.
        /// 
        /// @param v The view mode that the user has selected.
        public void UpdateViewMode(ImageView.ViewMode v)
        {
            if (v == ImageView.ViewMode.RGBImage)
                rGBToolStripMenuItem.Checked = true;
            if (v == ImageView.ViewMode.Filtered)
                filteredToolStripMenuItem.Checked = true;
            if (v == ImageView.ViewMode.Raw)
                rawToolStripMenuItem.Checked = true;
        }

        /// It opens the script runner window
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void scriptRunnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.runner.WindowState = FormWindowState.Normal;
            App.runner.Show();
        }

        /// It opens the stackTools form when the user clicks on the stackToolsToolStripMenuItem
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void stackToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.stackTools.Show();
        }

        /// It opens the recorder window
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void scriptRecorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recorder.recorder.WindowState = FormWindowState.Normal;
            Recorder.recorder.Show();
        }

        /// If the user has selected an image, then save the image as an OME-TIFF file
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The file name of the image.
        private void saveOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Image == null)
                return;
            saveOMEFileDialog.FileName = Path.GetFileNameWithoutExtension(ImageView.SelectedImage.Filename);
            if (saveOMEFileDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string file in saveOMEFileDialog.FileNames)
            {
                StartProgress("Saving selected OME.", "Saving OME");
                BioImage.SaveOME(file, Image.ID);
                StopProgress();
            }
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// It opens a new window
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
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

        /// It converts the image to 8 bit.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs This is the event arguments.
        private void to8BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To8Bit();
        }

        /// It converts the image to 16 bit
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data,
        /// and provides a value to use with events that do not include event data.
        private void to16BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To16Bit();
        }

        /// It opens a new form called "filters" when the user clicks on the "Filters" menu item
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters = new Filter();
            filters.Show();
        }

        /// It converts the image to 8 bit, then updates the GUI and the image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data,
        /// and provides a value to use for events that do not include event data.
        private void bit8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To8Bit();
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        /// It converts the image to 16 bit, then updates the GUI and the image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data,
        /// and provides a value to use for events that do not include event data.
        private void bit16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To16Bit();
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        /// Converts the image to 24 bit color.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void to24BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To24Bit();
            //We update the viewer gui since there are less planes now.
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        /// It converts the image to 48 bit color
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void to48BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To48Bit();
            //We update the viewer gui since there are less planes now.
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        /// It converts the image to 32 bit color
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void to32BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image.To32Bit();
            Viewer.InitGUI();
            Viewer.UpdateImages();
        }

        /// If the user clicks on the "To Window" menu item, then if there is a selected image, create a
        /// new ImageWindow and show it
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The image that is selected in the tab control.
        private void toWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == -1)
                return;
            BioImage im = ImageView.SelectedImage;
            ImageWindow vi = new ImageWindow(im);
            vi.Show();
        }

        /// We update the view status based on tab
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        /// 
        /// @return The selected index of the tab control.
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
            Viewer.SelectedIndex = 0;
        }

        /// It removes the selected tab from the tab control, disposes of the image view, removes the
        /// images from the image list, and disposes of the images
        /// 
        /// @param sender System.Object
        /// @param EventArgs e
        /// 
        /// @return The ImageView object is being returned.
        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == -1)
                return;
            ImageView v = (ImageView)tabControl.SelectedTab.Controls[0];
            tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);
            v.Dispose();
            foreach (BioImage item in v.Images)
            {
                Images.RemoveImage(item);
                item.Dispose();
            }
            GC.Collect();
        }

        /// It saves all the images in the list of images
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs System.EventArgs
        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            List<string> sts = new List<string>();
            foreach (BioImage item in Images.images)
            {
                StartProgress("Save Selected Tiff", "Saving");
                BioImage.SaveFile(Image.ID, Image.ID);
                StopProgress();
            }
        }

        /// When the user clicks on the "About" menu item, a new About form is created and shown
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        /// It opens a new window called "runner" when the user clicks on the "Script Runner" menu item
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void scriptRunnerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            App.runner.Show();
        }

        /// It opens the script recorder window
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void scriptRecorderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            App.recorder.Show();
        }

        /// This function opens an OME file and displays it in a new tab
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The return value is a string.
        private void openOMEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string item in openFilesDialog.FileNames)
            {
                StartProgress("Opening selected OME", "Opening OME");
                BioImage im = BioImage.OpenOME(item, 0, false, false, false, 0, 0, 0, 0);
                if (im == null)
                    return;
                AddTab(im);
                Images.images.Add(im);
                if (!App.recent.Contains(im.ID))
                    App.recent.Add(im.ID);
            }
            foreach (string item in App.recent)
            {
                openRecentToolStripMenuItem.DropDownItems.Add(item, null, ItemClicked);
            }
        }

        /// It opens a new instance of the application
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void newTabViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        /// It shows the nodeView form
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void nodeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.nodeView.Show();
            App.nodeView.ShowInTaskbar = true;
        }
        /// The saveToolStripMenuItem1_Click function is called when the user clicks the "Save" button
        /// in the menu bar. If the user selects a file name and location, the BioImage.Save function is
        /// called to save the image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveTiffFileDialog.ShowDialog() == DialogResult.OK)
            {
                StartProgress("Saving selected Tiff", "Saving");
                BioImage.SaveFile(ImageView.SelectedImage.ID, saveTiffFileDialog.FileName);
                StopProgress();
            }
        }

        /// If the user clicks the "Save OME" menu item, then show the save file dialog and if the user
        /// clicks "OK", then save the image to the file name the user specified
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void saveOMEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveOMEFileDialog.ShowDialog() == DialogResult.OK)
                BioImage.SaveOME(ImageView.SelectedImage.ID, saveOMEFileDialog.FileName);
        }
        /// It clears the dropdown menu, then adds each item in the recent list to the dropdown menu.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void openRecentToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            openRecentToolStripMenuItem.DropDownItems.Clear();
            foreach (string item in App.recent)
            {
                openRecentToolStripMenuItem.DropDownItems.Add(item, null, ItemClicked);
            }
        }
        /// If the file is not an OME file, open the file as a series. If it is an OME file, open the
        /// file as a series and ask the user if they want to open the series in the same tab or in a
        /// new tab
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        private void ItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (!BioImage.isOME(ts.Text))
            {
                BioImage[] bs = BioImage.OpenSeries(ts.Text, true);
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
                BioImage[] bs = BioImage.OpenOMESeries(ts.Text, true, true);
            }
            App.viewer.GoToImage();
            SaveProperties();
        }
        /// If the user presses a key, and the key is one of the arrow keys, then move the microscope in
        /// the direction of the arrow key
        /// 
        /// @param sender The object that raised the event.
        /// @param KeyEventArgs e
        /// 
        /// @return The return value is a string.
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
                App.viewer.Scale = new AForge.SizeF(App.viewer.Scale.Width - 0.1f, App.viewer.Scale.Height - 0.1f);
            }
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.NumPad9)
            {
                App.viewer.Scale = new AForge.SizeF(App.viewer.Scale.Width - 0.1f, App.viewer.Scale.Height - 0.1f);
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
                Microscope.MoveUp(Microscope.viewSize.Y / 2);
            if (e.KeyCode == Keys.Down)
                Microscope.MoveDown(Microscope.viewSize.Y / 2);
            if (e.KeyCode == Keys.Left)
                Microscope.MoveRight(-(Microscope.viewSize.X / 2));
            if (e.KeyCode == Keys.Right)
                Microscope.MoveRight((Microscope.viewSize.X / 2) / 2);
            Viewer.UpdateStatus();
            Viewer.UpdateView();
        }

        /// This function is called when the user clicks on the "Duplicate" menu item in the "Image"
        /// menu. It creates a copy of the current image and adds a new tab with the copy
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BioImage im = Image.Copy();
            AddTab(im);
        }

        /// It opens a new window
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void saveSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.seriesTool.Show();
        }

        /// The function saves the properties of the application, closes the microscope, closes the
        /// imager, closes the stage, closes the node view, and exits the application
        /// 
        /// @param sender The object that raised the event.
        /// @param FormClosingEventArgs 
        private void TabsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProperties();
            Microscope.Close();
            App.imager.Close();
            App.stage.Close();
            App.nodeView.Close();
            Application.Exit();
        }

        /// It opens a new window when the user clicks on the "Automation" menu item
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void automationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.recordings.Show();
        }

        private void stageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.stage.Show();
        }

        /// If the user selects a file, open it as an OME series, and add each image to the current tab
        /// 
        /// @param sender System.Object
        /// @param EventArgs e
        /// 
        /// @return The return value is a BioImage array.
        private void addImagesToTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            for (int i = 0; i < openFilesDialog.FileNames.Length; i++)
            {
                if (i == 0 && tabControl.TabPages.Count == 0)
                {
                    BioImage b = BioImage.OpenFile(openFilesDialog.FileNames[i], 0, true, true);
                }
                else
                {
                    BioImage b = BioImage.OpenFile(openFilesDialog.FileNames[i], 0, true, false);
                    Viewer.AddImage(b);
                }
            }
            App.viewer.GoToImage();
        }

        /// If the user selects a file, open it and add it to the viewer
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The return value is a string array of the file names.
        private void addImagesOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            for (int i = 0; i < openFilesDialog.FileNames.Length; i++)
            {
                if (i == 0 && tabControl.TabPages.Count == 0)
                {
                    BioImage b = BioImage.OpenOME(openFilesDialog.FileNames[i], 0, true, true, false, 0, 0, 0, 0);
                }
                else
                {
                    BioImage b = BioImage.OpenOME(openFilesDialog.FileNames[i], 0, true, true, false, 0, 0, 0, 0);
                    Viewer.AddImage(b);
                }
            }
            App.viewer.GoToImage();
        }

        /// It opens a dialog box to select a file, then opens the file and adds it to the viewer
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The BioImage[] bs is being returned.
        private void openSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            BioImage[] bs = BioImage.OpenOMESeries(openFilesDialog.FileName, false, true);
            if (bs == null)
                return;
            AddTab(bs[0]);
            for (int i = 1; i < bs.Length; i++)
            {
                Viewer.AddImage(bs[i]);
            }
            App.viewer.GoToImage();
        }

        /// If the user selects a file to save to, and the images are not 8 or 16 bit, ask the user if
        /// they want to convert them to 8 or 16 bit, and if they do, convert them and save them
        /// 
        /// @param sender System.Object
        /// @param EventArgs e
        /// 
        /// @return The return value is a string array of the image IDs.
        private void saveTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveOMEFileDialog.ShowDialog() != DialogResult.OK)
                return;
            bool convert = false;
            foreach (BioImage b in Viewer.Images)
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
                if (ImageView.SelectedImage.bitsPerPixel > 8)
                    mes = "Saving Series as OME only supports 8 bit & 16 bit images. Convert 16 bit?";
                else
                    mes = "Saving Series as OME only supports 8 bit & 16 bit images. Convert 8 bit?";
                if (MessageBox.Show(this, mes, "Convert to supported format?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (BioImage b in Viewer.Images)
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

            BioImage[] sts = new BioImage[App.viewer.Images.Count];
            for (int i = 0; i < sts.Length; i++)
            {
                sts[i] = App.viewer.Images[i];
            }
            BioImage.SaveOMESeries(sts, saveOMEFileDialog.FileName, Properties.Settings.Default.Planes);
        }

        /// It takes the filenames of all the images in the viewer and saves them as a multi-page tiff
        /// file
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs 
        /// 
        /// @return The file name of the file that was selected.
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

        /// It opens a series of images and adds them to the viewer
        /// 
        /// @param sender System.Object
        /// @param EventArgs 
        /// 
        /// @return The return type is void.
        private void openSeriesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            BioImage[] bms = null;
            foreach (string item in openFilesDialog.FileNames)
            {
                bms = BioImage.OpenSeries(openFilesDialog.FileName, false);
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

        /// The function is called when the form is loaded. It checks if the viewer is not null, and if
        /// it isn't, it calls the GoToImage function
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data,
        /// and provides a value to use for events that do not include event data.
        private void TabsView_Load(object sender, EventArgs e)
        {
            if (App.viewer != null)
                App.viewer.GoToImage();

            Function.Initialize();
        }

        /// It clears the recent files list
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void clearRecentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Recent = "";
            Properties.Settings.Default.Save();
            openRecentToolStripMenuItem.DropDownItems.Clear();
            App.recent.Clear();
        }

        /// If the user clicks on a menu item in the "Rotate" menu, then rotate the selected image by
        /// the amount specified in the menu item
        /// 
        /// @param sender The object that raised the event.
        /// @param ToolStripItemClickedEventArgs 
        /// 
        /// @return The name of the clicked item.
        private void rotateToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ImageView.SelectedImage == null)
                return;
            string st = e.ClickedItem.Text;
            RotateFlipType rot = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), st);
            ImageView.SelectedImage.RotateFlip(rot);
            Viewer.UpdateImage();
            Viewer.UpdateView();
        }

        /// If the dropdown menu has no items, add the items from the RotateFlipType enum
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The names of the enumeration values.
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

        /// If the Viewer is not null, set the Viewer's mode to Emission, uncheck all the other modes,
        /// check the Emission mode, and update the status
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The Viewer.Mode is being returned.
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

        /// It opens the console window when the user clicks on the console menu item
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.console.Show();
        }

        /// It loops through all the buffers in the selected image, and calls the SwitchRedBlue()
        /// function on each buffer
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void switchRedBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (AForge.Bitmap bf in ImageView.SelectedImage.Buffers)
            {
                bf.SwitchRedBlue();
            }
            Viewer.UpdateImages();
        }
        /// This function creates a new function form and shows it
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void createFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FunctionForm f = new FunctionForm(new Function());
            f.Show();
        }
        /// When a user clicks on a dropdown item, it will call the function that is associated with
        /// that item
        /// 
        /// @param sender The object that triggered the event.
        /// @param EventArgs The event arguments.
        private void DropDownItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            Function.Functions[ts.Text].PerformFunction(true);
        }
        /// It clears the dropdown menu, then adds a new item for each function in the Function class.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void runToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            runToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in Function.Functions)
            {
                runToolStripMenuItem.DropDownItems.Add(item.Value.Name, null, DropDownItemClicked);
            }
        }

        /// It reloads the image in the picturebox
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageView.SelectedImage.Update();
        }

        /// It opens an XML file, and displays it in a new window
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLView v = new XMLView(BioImage.OpenXML(ImageView.SelectedImage.file));
            v.Show();
        }
        /// When the user clicks on the menu item, the viewer's hardware acceleration is set to the value
        /// of the menu item's checked property, and the viewer is updated.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Viewer.HardwareAcceleration = dToolStripMenuItem.Checked;
            Properties.Settings.Default.HardwareAcceleration = dToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            Viewer.UpdateView();
        }

        /// It opens a file dialog, and if the user selects a bunch of images, it creates a new BioImage
        /// object that is a stack of the images, and adds a new tab to the main form with the new
        /// BioImage object
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return A BioImage object.
        private void imagesToStackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Images to Stack";
            fd.Multiselect = true;
            if (fd.ShowDialog() != DialogResult.OK)
                return;
            BioImage b = BioImage.ImagesToStack(fd.FileNames, false);
            AddTab(b);
        }

        /// It creates a new instance of the View3D class, and passes the selected image to the
        /// constructor
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void _3dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View3D d = new View3D(ImageView.SelectedImage);
            d.Show();
        }

        /// It opens a dialog box to select a file, then it adds the ROI to the selected image
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The return value is a list of annotations.
        private void importImageJROIToSelectedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openImageJROI.ShowDialog() != DialogResult.OK)
                return;
            foreach (string item in openImageJROI.FileNames)
            {
                ImageView.SelectedImage.Annotations.Add(ImageJ.RoiDecoder.open(item));
            }
            App.viewer.UpdateView();
        }

        /// This function exports all ROI's from the currently selected image to a folder of your choice
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The ROI object is being returned.
        private void exportImageJROIFromSelectedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImageJROI.Title = "Set Filename for exported ROI's.";
            saveImageJROI.FileName = ImageView.SelectedImage.Filename;
            if (saveImageJROI.ShowDialog() != DialogResult.OK)
                return;
            int i = 1;
            foreach (ROI roi in ImageView.SelectedImage.Annotations)
            {
                string s = Path.GetDirectoryName(saveImageJROI.FileName) + "//" + Path.GetFileNameWithoutExtension(saveImageJROI.FileName) + "-" + i + ".roi";
                ImageJ.RoiEncoder.save(roi, s);
                i++;
            }
            App.viewer.UpdateView();
        }

        /// It opens the lightTool form when the lightToolToolStripMenuItem is clicked
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void lightToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.lightTool.Show();
        }

        private void savePyramidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveOMEFileDialog.ShowDialog() != DialogResult.OK)
                return;
            BioImage.SaveOMEPyramidal(App.viewer.Images.ToArray(), saveOMEFileDialog.FileName, NetVips.Enums.ForeignTiffCompression.None, 0);
        }

        private void findFocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImageView.SelectedImage == null) return;
            ZCT co = SelectedViewer.GetCoordinate();
            int f = BioImage.FindFocus(ImageView.SelectedImage, co.C, co.T);
            ZCT z = ImageView.SelectedImage.Buffers[f].Coordinate;
            SelectedViewer.SetCoordinate(z.Z, z.C, z.T);
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.setup.Show();
        }

        private void imagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.imager.Show();
        }

        private void stageToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.stage.Show();
        }

        private void cellImagerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            App.cellImager.Show();
        }

        private void slideImagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.slideImager.Show();
        }

        private void importGeoJSONROIToSelectedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFilesDialog.ShowDialog() != DialogResult.OK)
                return;
            ImageView.SelectedImage.Annotations.AddRange(QuPath.ReadROI(openFilesDialog.FileName,ImageView.SelectedImage));
        }

        private void exportGeoJSONROIFromSelectedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveOMEFileDialog.ShowDialog() != DialogResult.OK)
                return;
            QuPath.Save(saveOMEFileDialog.FileName, ImageView.SelectedImage);
        }
    }
}
