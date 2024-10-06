using AForge;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RectangleD = AForge.RectangleD;
using javax.swing;
using BioLib;
using System.Runtime.InteropServices;
using mmcorej;
using org.micromanager.@internal;

namespace BioImager
{
    public static class MicroManager
    {
        public static mmcorej.CMMCore core;
        public static MMStudio studio;
        public static PointD location;
        public static double focus;
        public static Dictionary<string, List<Conf>> Config = new Dictionary<string, List<Conf>>();
        public struct Conf
        {
            public string Class {  get; set; }
            public string Type { get; set; }
            public string[] Values { get; set; }
            public Conf(string Class, string Type, string[] Values)
            {
                this.Class = Class;
                this.Type = Type;
                this.Values = Values;
            }
            public override string ToString()
            {
                string s = Class + "," + Type + ",";
                foreach (string item in Values)
                {
                    s += item + ",";
                }
                return s;
            }
        }
        public static Conf[] GetConfigs(string Class, string Type)
        {
            List<Conf> configs = new List<Conf>();
            foreach (var conf in Config[Class]) 
            {
                if(conf.Type == Type) configs.Add(conf);
            }
            return configs.ToArray();
        }
        public static bool initialized = false;
        public static string TurretName = "";
        public static bool Initialize(string config)
        {
            try
            {
                Directory.SetCurrentDirectory("C:/Program Files/Micro-Manager-2.0/");
                java.lang.System.setProperty("force.annotation.index", "true");
                // Set the library path (adjust the path as needed)
                java.lang.System.setProperty("org.micromanager.corej.path", "C:/Program Files/Micro-Manager-2.0");
                java.lang.System.setProperty("user.dir", "C:/Program Files/Micro-Manager-2.0/");
                MMStudio.main(new string[] { });
                studio = MMStudio.getInstance();
                core = (CMMCore)studio.core();
                Objectives.Initialize();
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Environment.ProcessPath));
                return true;
            }
            catch (Exception e)
            {
                // Log the exception message
                Console.WriteLine("Error during Micro-Manager initialization: " + e.Message);
                return false; // Return false on failure
            }
        }

        public static BioImage TakeImage(string name)
        {
            int width, height, depth, bits, channels = 0;
            AForge.Bitmap[] bm;
            TakeImage(name,out width,out height,out depth,out bits,out channels, out bm);
            BioImage im = new BioImage(name);
            foreach (AForge.Bitmap item in bm)
            {
                Statistics.CalcStatistics(item);
            }
            im.Buffers.AddRange(bm);
            //Set the physical size based on objective view
            RectangleD rec = Microscope.GetViewRectangle(false);
            Point3D p = Microscope.GetPosition();
            Resolution res = new Resolution(width, height, im.Buffers[0].PixelFormat, rec.W / im.Buffers[0].SizeX, rec.H / im.Buffers[0].SizeY, 1, p.X, p.Y, p.Z);
            im.Volume.Location = p;
            im.Volume.Width = rec.W;
            im.Volume.Height = rec.H;
            im.bitsPerPixel = im.Buffers[0].BitsPerPixel;
            im.UpdateCoords(1, 1, 1);
            im.Resolutions.Add(res);
            for (int c = 0; c < bm.Length; c++)
            {
                im.Channels.Add(new Channel(c, bm[c].BitsPerPixel, bm[c].RGBChannelsCount));
            }
            //We wait for threshold image statistics calculation
            do
            {
                Thread.Sleep(50);
            } while (im.Buffers[im.Buffers.Count - 1].Stats == null);
            Statistics.ClearCalcBuffer();
            BioImage.AutoThreshold(im, false);
            return im;
        }
        private static bool TakeImage(string file, out int width, out int height, out int depth, out int bits, out int channels, out AForge.Bitmap[] bm)
        {
            string fil = Path.GetFileNameWithoutExtension(file);
            width = (int)core.getImageWidth();
            height = (int)core.getImageHeight();
            depth = (int)core.getImageBitDepth();
            bits = (int)core.getBytesPerPixel();
            channels = (int)core.getNumberOfCameraChannels();
            PixelFormat format = PixelFormat.Format8bppIndexed;
            if (depth > 8 && bits == 2)
                format = PixelFormat.Format16bppGrayScale;
            else if(depth > 8 && bits == 6)
                format = PixelFormat.Format48bppRgb;
            else if (depth == 8 && bits == 3)
                format = PixelFormat.Format24bppRgb;
            else if (depth == 8 && bits == 4)
                format = PixelFormat.Format32bppArgb;
            bm = new AForge.Bitmap[channels];
            core.snapImage();
            for (int i = 0; i < channels; i++)
            {
                if(depth <= 8)
                {
                    byte[] bytes = (byte[])core.getImage(i);
                    bm[i] = new AForge.Bitmap(width, height, format, bytes , new ZCT(), "");
                }
                else
                {
                    ushort[] buf = (ushort[])core.getImage(i);
                    byte[] bts = new byte[buf.Length * 2];
                    for (int b = 0; b < buf.Length; b++)
                    {
                        byte[] pl = BitConverter.GetBytes(buf[b]);
                        bts[b] = pl[0];
                        bts[b+1] = pl[1];
                    }
                    bm[i] = new AForge.Bitmap(width, height, format, bts, new ZCT(), "");
                }
            }
            return true;
        }
        public static void SetExposure(float f)
        {
            core.setExposure(f);
        }
        public static void SetPosition(Point3D p)
        {
            core.setXYPosition(p.X, p.Y);
            core.setPosition(p.Z);
        }
        public static void SetPosition(PointD p)
        {
            core.setXYPosition(p.X, p.Y);
        }
        public static void GetPosition3D(out Point3D p, bool update)
        {
            var pos = core.getXYStagePosition();
            var f = core.getPosition();
            p = new Point3D(pos.x, pos.y, f);
        }
        public static void GetPosition(out PointD p, bool update)
        {
            var pos = core.getXYStagePosition();
            p = new PointD(pos.x, pos.y);
        }
        public static class Objectives
        {
            /// <summary>
            /// Number of Installed Objectives
            /// </summary>
            public static int InstalledCount
            {
                get
                {
                    return GetConfigs("ConfigGroup", "Objective").Length;
                }
            }
            public static List<Objective> List = new List<Objective>();
            public class Objective
            {
                public string Name { get; set; }
                public int Index {  get; set; }
                public int Magnification { get; set; }
                public Objective(string name, int index, int mag)
                {
                    Name = name;
                    Index = index;
                    Magnification = mag;
                }
            }
            internal static void Initialize()
            {
                foreach (Conf obj in GetConfigs("ConfigGroup", "Objective"))
                {
                    List.Add(new Objective(obj.Values[0], int.Parse(obj.Values[3]), int.Parse(obj.Values[0].Replace("X", ""))));
                }
            }

            public static Conf GetObjective(int index)
            {
                Conf[] cfs = GetConfigs("ConfigGroup", "Objective");
                return cfs[index];
            }
            public static void SetPosition(int i)
            {
                core.setConfig(TurretName, List[i].Name);
            }
            public static int GetPosition()
            {
                string s = core.getCurrentConfig(TurretName);
                int i = 0;
                foreach(Objective obj in Objectives.List)
                {
                    if (obj.Name == s)
                        return i;
                }
                return -1;
            }
        }
        public static class Shutters
        {
            public class Shutter
            {
                public string Name { get; set; }
                public Shutter(string name)
                {
                    Name = name;
                }
            }
            public static List<Shutter> List = new List<Shutter>();
            public static void Initialize()
            {
                if (Config.ContainsKey("Device"))
                {
                    //We get the name of the shutters.
                    foreach (Conf item in Config["Device"])
                    {
                        if (item.Values.Last().Contains("Shutter"))
                        {
                            List.Add(new Shutter(item.Type));
                        }
                    }
                }
            }
            public static int GetPosition(string shutterName)
            {
                if (core.getShutterOpen())
                    return 1;
                else
                    return 0;
            }
            public static void SetPosition(string shutterName, int state)
            {
                if(state == 1)
                    core.setShutterOpen(shutterName, true);
                else
                    core.setShutterOpen(shutterName, false);
            }
        }
        
        
    }
}
