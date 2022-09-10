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
        public static Library lib = null;
        public static List<string> recent = new List<string>();

        public static BioImage Image
        {
            get {
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
            ImageJ.Initialize(Properties.Settings.Default.ImageJPath);
            //channelsTool = new ChannelsTool();
        }
        public static void AddROI(string an)
        {
            Annotations.Add(BioImage.StringToROI(an));
            Recorder.AddLine("App.AddROI(" + '"' + an + "'" + ");");
        }
    }
}
