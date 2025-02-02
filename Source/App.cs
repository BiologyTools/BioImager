﻿using AForge;
using BioImager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioLib;
namespace BioImager
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
        public static CellImager cellImager = null;
        public static SlideImager slideImager = null;
        public static BioConsole console = null;
        public static Light lightTool = null;
        public static Library lib = null;
        public static List<string> recent = new List<string>();


        public static void UpdateImages()
        {
            do
            {
                Thread.Sleep(1000);
                foreach (var item in Images.images)
                {
                    if(App.tabsView!=null)
                    if (App.tabsView.HasTab(item.Filename))
                        continue;
                    else
                        App.tabsView.AddTab(item);
                }
            } while (true);
        }

        /* A property that returns the current image. */
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
        /// Initialize() is a function that initializes the BioImager UI
        public static void Initialize()
        {
            BioImage.Initialize(ImageJ.ImageJPath);
            Thread th = new Thread(UpdateImages);
            th.Start();
            do
            {
                Thread.Sleep(100);
            } while (!BioImage.Initialized);
            imager = new Imager();
            Microscope.Initialize();
            ImageJ.Initialize();
            setup = new MicroscopeSetup();
            stage = new StageTool();
            cellImager = new CellImager();
            stackTools = new StackTools();
            tools = new Tools();            
            manager = new ROIManager();
            runner = new Scripting();
            recorder = new Recorder();
            seriesTool = new Series();
            recordings = new Recordings();
            automation = new Automation();
            slideImager = new SlideImager();
            if (!Properties.Settings.Default.PycroManager && !Properties.Settings.Default.PMicroscope)
            {
                lightTool = new Light();
            }
            lib = new Library();
            
            console = new BioConsole();
            imager.Show();
            
            //ImageJ.Initialize(Properties.Settings.Default.ImageJPath);
        }
        /// Hide() hides all the tools
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
        
        /// It returns a list of all the items in a menu, including submenus
        /// 
        /// @param ToolStripMenuItem The menu item you want to get the sub items from.
        private static IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            foreach (var dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.GetType() == typeof(ToolStripMenuItem))
                {
                    if (((ToolStripMenuItem)dropDownItem).HasDropDownItems)
                    {
                        foreach (ToolStripMenuItem subItem in GetItems((ToolStripMenuItem)dropDownItem))
                            yield return subItem;
                    }
                    yield return (ToolStripMenuItem)dropDownItem;
                }
            }
        }
        /// It takes a menu strip and returns a list of all the menu items in the menu strip
        /// 
        /// @return A list of ToolStripMenuItems
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
        /// It takes the context menu of the viewer and returns a list of all the items in the menu
        /// 
        /// @return A list of ToolStripMenuItems
        public static List<ToolStripMenuItem> GetContextItems()
        {
            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem toolItem in viewer.ViewContextMenu.Items)
            {
                allItems.Add(toolItem);
                allItems.AddRange(GetItems(toolItem));
            }
            return allItems;
        }
        /// It takes a string and a function, and adds the function to the menu item specified by the
        /// string
        /// 
        /// @param s The path to the menu item.
        /// @param Function 
        /// 
        /// @return A ToolStripItem
        public static ToolStripItem GetMenuItemFromPath(string s, Function f)
        {
            if (s == "" || s == null)
                return null;
            string[] sts = s.Split('/');
        start:
            List<ToolStripMenuItem> allItems = GetMenuItems();
            //Find path or create it.
            bool found = false;
            ToolStripMenuItem item = null;
            
            for (int t = 0; t < sts.Length; t++)
            {
                found = false;
                for (int i = 0; i < allItems.Count; i++)
                {
                    if (allItems[i].Text == sts[t])
                    {
                        item = allItems[i];
                        found = true;
                        if (t == sts.Length - 1)
                            return allItems[i];
                    }
                }
                if (!found)
                {
                    if(t == 0)
                    {
                        tabsView.MainMenuStrip.Items.Add(sts[t]);
                        goto start;
                    }
                    else if(t > 0 && t < sts.Length)
                    {
                        ToolStripMenuItem itm = new ToolStripMenuItem();
                        itm.Tag = f;
                        itm.Name = f.Name;
                        item.DropDownItems.Add(f.Name, null, ItemClicked);
                        return item;
                    }
                    else
                    {
                        item.DropDownItems.Add(sts[t]);
                    }
                }
            }
            return item;
        }
        /// It takes a string and a function, and adds the function to the context menu at the location
        /// specified by the string
        /// 
        /// @param s The path to the item.
        /// @param Function 
        /// 
        /// @return A ToolStripItem
        public static ToolStripItem GetContextMenuItemFromPath(string s, Function f)
        {
            if (s == "" || s == null)
                return null;
            string[] sts = s.Split('/');
        start:
            List<ToolStripMenuItem> allItems = GetContextItems();
            //Find path or create it.
            bool found = false;
            ToolStripMenuItem item = null;

            for (int t = 0; t < sts.Length; t++)
            {
                found = false;
                for (int i = 0; i < allItems.Count; i++)
                {
                    if (allItems[i].Text == sts[t])
                    {
                        item = allItems[i];
                        found = true;
                        if (t == sts.Length - 1)
                            return allItems[i];
                    }
                }
                if (!found)
                {
                    if (t == 0)
                    {
                        viewer.ViewContextMenu.Items.Add(sts[t]);
                        goto start;
                    }
                    else if (t > 0 && t < sts.Length)
                    {
                        ToolStripMenuItem itm = new ToolStripMenuItem();
                        itm.Tag = f;
                        itm.Name = f.Name;
                        item.DropDownItems.Add(f.Name, null, ItemClicked);
                        return item;
                    }
                    else
                    {
                        item.DropDownItems.Add(sts[t]);
                    }
                }
            }
            return item;
        }

        /// It takes a string and a function, and adds the function to the menu item specified by the
        /// string
        /// 
        /// @param s The path to the menu item.
        /// @param Function 
        /// 
        /// @return A ToolStripItem
        public static ToolStripItem GetMenuItemFromPath(string s)
        {
            if (s == "" || s == null)
                return null;
            string[] sts = s.Split('/');
        start:
            List<ToolStripMenuItem> allItems = GetMenuItems();
            //Find path or create it.
            bool found = false;
            ToolStripMenuItem item = null;

            for (int t = 0; t < sts.Length; t++)
            {
                found = false;
                for (int i = 0; i < allItems.Count; i++)
                {
                    if (allItems[i].Text == sts[t])
                    {
                        item = allItems[i];
                        found = true;
                        if (t == sts.Length - 1)
                            return allItems[i];
                    }
                }
                if (!found)
                {
                    if (t == 0)
                    {
                        tabsView.MainMenuStrip.Items.Add(sts[t]);
                        goto start;
                    }
                    else if (t > 0 && t < sts.Length)
                    {
                        ToolStripMenuItem itm = new ToolStripMenuItem();
                        itm.Text = Path.GetFileName(s);
                        item.DropDownItems.Add(Path.GetFileName(s), null, ItemClicked);
                        return item;
                    }
                    else
                    {
                        item.DropDownItems.Add(sts[t]);
                    }
                }
            }
            return item;
        }
        /// It takes a string and a function, and adds the function to the context menu at the location
        /// specified by the string
        /// 
        /// @param s The path to the item.
        /// @param Function 
        /// 
        /// @return A ToolStripItem
        public static ToolStripItem GetContextMenuItemFromPath(string s)
        {
            if (s == "" || s == null)
                return null;
            string[] sts = s.Split('/');
        start:
            List<ToolStripMenuItem> allItems = GetContextItems();
            //Find path or create it.
            bool found = false;
            ToolStripMenuItem item = null;

            for (int t = 0; t < sts.Length; t++)
            {
                found = false;
                for (int i = 0; i < allItems.Count; i++)
                {
                    if (allItems[i].Text == sts[t])
                    {
                        item = allItems[i];
                        found = true;
                        if (t == sts.Length - 1)
                            return allItems[i];
                    }
                }
                if (!found)
                {
                    if (t == 0)
                    {
                        viewer.ViewContextMenu.Items.Add(sts[t]);
                        goto start;
                    }
                    else if (t > 0 && t < sts.Length)
                    {
                        ToolStripMenuItem itm = new ToolStripMenuItem();
                        itm.Name = Path.GetFileName(s);
                        item.DropDownItems.Add(Path.GetFileName(s), null, ItemClicked);
                        return item;
                    }
                    else
                    {
                        item.DropDownItems.Add(sts[t]);
                    }
                }
            }
            return item;
        }

        /// It takes a string and a function and adds the function to the menu item specified by the
        /// string
        /// 
        /// @param menu The menu path to add the menu item to.
        /// @param Function The function that will be called when the menu item is clicked.
        public static void AddMenu(string menu, Function f)
        {
            GetMenuItemFromPath(menu, f);
        }
        /// It takes a string and a function and adds a context menu item to the context menu
        /// 
        /// @param menu The path to the menu item.
        /// @param Function The function that will be called when the menu item is clicked.
        public static void AddContextMenu(string menu, Function f)
        {
            GetContextMenuItemFromPath(menu, f);
        }

        /// It takes a string and a function and adds the function to the menu item specified by the
        /// string
        /// 
        /// @param menu The menu path to add the menu item to.
        /// @param Function The function that will be called when the menu item is clicked.
        public static void AddMenu(string menu)
        {
            GetMenuItemFromPath(menu);
        }
        /// It takes a string and a function and adds a context menu item to the context menu
        /// 
        /// @param menu The path to the menu item.
        /// @param Function The function that will be called when the menu item is clicked.
        public static void AddContextMenu(string menu)
        {
            GetContextMenuItemFromPath(menu);
        }


        /// It takes a function, adds it to the menu, and then adds an event handler to the menu item
        /// that calls the function when the menu item is clicked
        /// 
        /// @param sender The object that sent the event.
        /// @param EventArgs The event arguments.
        private static void ItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if(ts.Text.EndsWith(".dll"))
            {
                try
                {
                    Plugin.Plugins[ts.Text].Execute(new string[] { });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
            }
            else if(ts.Text.EndsWith(".ijm"))
            {
                string st = File.ReadAllText(Path.GetDirectoryName(ImageJ.ImageJPath) + "/macros/" + ts.Text);
                ImageJ.RunOnImage(st, BioConsole.headless, BioConsole.onTab, BioConsole.useBioformats, BioConsole.newTab);
            }
            else if(ts.Text.EndsWith(".pt") || ts.Text.EndsWith(".onnx"))
            {
                try
                {
                    ML.ML.Run(ts.Text, ImageView.SelectedImage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            else
            {
                Function f = (Function)ts.Tag;
                f.PerformFunction(true);
            }
        }
        /// It takes a string, converts it to a ROI, and adds it to the list of ROIs
        /// 
        /// @param an the string representation of the ROI
        public static void AddROI(string an)
        {
            Annotations.Add(BioImage.StringToROI(an));
            Recorder.AddLine("App.AddROI(" + '"' + an + "'" + ");");
        }
    }
}
