using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace Bio
{
    public class App
    {
        public static ROIManager manager = null;
        public static ChannelsTool channelsTool = null;
        public static TabsView tabsView = null;
        public static NodeView nodeView = null;
        public static Scripting runner = null;
        public static Recorder recorder = null;
        public static Imager imager = null;
        public static Tools tools = null;
        public static StackTools stackTools = null;
        public static ImageView viewer = null;
        public static StageTool stage = null;
        public static Series seriesTool = null;
        public static Recordings recordings = null;
        public static Automation automation = null;
        public static MicroscopeSetup setup = null;
        public static BioConsole console = null;
        public static Library lib = null;
        public static List<string> recent = new List<string>();

        public static BioImage Image
        {
            get 
            {
                if (ImageView.SelectedImage == null)
                    return tabsView.Image;
                return ImageView.SelectedImage;
            }
        }
        public static List<Channel> Channels
        {
            get { return Image.Channels; }
        }

        public static List<ROI> Annotations
        {
            get { return Image.Annotations; }
        }
        public static void Initialize()
        {
            BioImage.Initialize();
            Microscope.Initialize();
            setup = new MicroscopeSetup();
            tools = new Tools();
            stackTools = new StackTools();
            manager = new ROIManager();
            runner = new Scripting();
            recorder = new Recorder();
            seriesTool = new Series();
            recordings = new Recordings();
            automation = new Automation();
            stage = new StageTool();
            lib = new Library();
            imager = new Imager();
            console = new BioConsole();
            imager.Show();
            //ImageJ.Initialize(Properties.Settings.Default.ImageJPath);
        }
        public static void Hide()
        {
            tools.Hide();
            stackTools.Hide();
            manager.Hide();
            runner.Hide();
            recorder.Hide();
            seriesTool.Hide();
            recordings.Hide();
            console.Hide();
        }
        public static bool SetImageJPath()
        {
            MessageBox.Show("ImageJ path not set. Set the ImageJ executable location.");
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Set the ImageJ executable location.";
            if (file.ShowDialog() != DialogResult.OK)
                return false;
            Properties.Settings.Default.ImageJPath = file.FileName;
            Properties.Settings.Default.Save();
            file.Dispose();
            ImageJ.Initialize(file.FileName);
            return true;
        }
        private static IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in GetItems(dropDownItem))
                        yield return subItem;
                }
                yield return dropDownItem;
            }
        }
        public static List<ToolStripMenuItem> GetMenuItems()
        {
            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem toolItem in tabsView.MainMenuStrip.Items)
            {
                allItems.Add(toolItem);
                allItems.AddRange(GetItems(toolItem));
            }
            return allItems;
        }
        public static bool Contains(string s)
        {
            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem toolItem in tabsView.MainMenuStrip.Items)
            {
                allItems.Add(toolItem);
                allItems.AddRange(GetItems(toolItem));
            }
            for (int i = 0; i < allItems.Count; i++)
            {
                if (allItems[i].Text == s)
                    return true;
            }
            return false;
        }
        public static ToolStripItem GetMenuItemFromPath(string s, Function f)
        {
            string[] sts = s.Split('/');
            List<ToolStripMenuItem> allItems = GetMenuItems();


            //Path not found lets create it.
            ToolStripItem item = null;
            for (int t = 0; t < sts.Length; t++)
            {
                for (int i = 0; i < allItems.Count; i++)
                {
                    if (allItems[i].Text == sts[t])
                    {
                        if (!allItems[i].DropDownItems.ContainsKey(sts[t]))
                        {
                            if (t == sts.Length - 1)
                            {
                                if (!Contains(f.Name))
                                {
                                    allItems[i].DropDownItems.Add(f.Name, null, ItemClicked);
                                    item = allItems[i].DropDownItems[allItems[i].DropDownItems.Count - 1];
                                    item.Tag = f;
                                    return item;
                                }
                            }
                            else
                            {
                                if (!Contains(sts[t + 1]))
                                    allItems[i].DropDownItems.Add(sts[t + 1]);
                            }
                            allItems = GetMenuItems();
                            break;
                        }
                    }

                }
            }

            return item;
        }
        public static void AddMenu(string menu, Function f)
        {
            GetMenuItemFromPath(menu, f);
        }
        private static void ItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            Function f = (Function)ts.Tag;
            f.PerformFunction(true);
        }
        public static void AddROI(string an)
        {
            Annotations.Add(BioImage.StringToROI(an));
            Recorder.AddLine("App.AddROI(" + '"' + an + "'" + ");");
        }
    }
}
