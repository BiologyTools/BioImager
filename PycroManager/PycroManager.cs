using AForge;
using loci.plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioImager
{
    public static class PycroManager
    {
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
            string[] sts = File.ReadAllLines(config);
            foreach (string s in sts)
            {
                if(!s.Replace(" ","").StartsWith("#"))
                {
                    string[] vals = s.Split(',');
                    if (vals.Length == 1)
                        continue;
                    if (!Config.ContainsKey(vals[0]))
                    Config.Add(vals[0],new List<Conf>());
                    List<string> vs = new List<string>();
                    for (int j = 2; j < vals.Length; j++)
                    {
                        vs.Add(vals[j]);
                    }
                    Config[vals[0]].Add(new Conf(vals[0], vals[1], vs.ToArray()));
                }
            }
            if(Config.ContainsKey("Device"))
            {
                //We get the name of the objective turret required for changing objectives
                foreach (Conf item in Config["Device"])
                {
                    if(item.Type == "Objective")
                    {
                        //We set the 
                        TurretName = "Objective";
                        initialized = true;
                    }
                }
            }
            Objectives.Initialize();
            if(!initialized)
            {
                Console.WriteLine("Unable to find Objective Turret (DObjective) Label in MMConfig.");
                return false;
            }
            Shutters.Initialize();
            Point3D loc = new Point3D();
            GetPosition3D(out loc, true);
            focus = loc.Z;
            location = new PointD(loc.X, loc.Y);
            return true;
        }
        public static string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.WorkingDirectory = Application.StartupPath + "PycroManager";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            string res;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    res = reader.ReadToEnd();
#if DEBUG
                    Console.Write(res);
#endif
                }
            }
            return res;
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
            string s = run_cmd("TakeImage.py",fil);
            if (s.Contains("OK"))
            {
                string[] sts = s.Split();
                width = int.Parse(sts[0]);
                height = int.Parse(sts[2]);
                depth = int.Parse(sts[4]);
                bits = int.Parse(sts[6]);
                channels = int.Parse(sts[8]);

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
                for (int i = 0; i < channels; i++)
                {
                    //Each channel is saved as a seperate file
                    bm[i] = new AForge.Bitmap(width,height,format,File.ReadAllBytes(Application.StartupPath + "/PycroManager/" + fil + i),new ZCT(), "");
                    //We delete the temporary file.
                    File.Delete(file + i);
                }
                return true;
            }
            else
            {
                width = 0;height = 0; depth = 0;bits = 0;channels = 0;bm = null;
                return false;
            }
        }
        public static bool SetExposure(float f)
        {
            string s = run_cmd("SetExposure.py", f.ToString());
            if (s.Contains("OK"))
                return true;
            else
                return false;
        }
        public static bool SetPosition(Point3D p)
        {
            string s = run_cmd("SetStageXYZ.py", p.X + " " + p.Y + " " + p.Z);
            if (s.Contains("OK"))
            {
                focus = p.Z;
                location = new PointD(p.X, p.Y);
                return true;
            }
            else
                return false;
        }
        public static bool SetPosition(PointD p)
        {
            string s = run_cmd("SetStageXY.py", p.X + " " + p.Y);
            if (s.Contains("OK"))
            {
                location = p;
                return true;
            }
            else
                return false;
        }
        public static bool GetPosition3D(out Point3D p, bool update)
        {
            if (!update)
            {
                p = new Point3D(location.X, location.Y, focus);
                return true;
            }
            string s = run_cmd("GetStageXYZ.py", "");
            if (s.Contains("OK"))
            {
                string[] sts = s.Split();
                p = new Point3D(double.Parse(sts[0]), double.Parse(sts[2]), double.Parse(sts[4]));
                focus = p.Z;
                return true;
            }
            else
            {
                p = new Point3D();
                return false;
            }
        }
        public static bool GetPosition(out PointD p, bool update)
        {
            if (!update)
            {
                p = location;
                return true;
            }
            string s = run_cmd("GetStageXY.py", "");
            if (s.Contains("OK"))
            {
                string[] sts = s.Split();
                p = new PointD(double.Parse(sts[0]), double.Parse(sts[2]));
                location = p;
                return true;
            }
            else
            {
                p = new PointD();
                return false;
            }
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
                int i = 0;
                foreach (Conf obj in GetConfigs("ConfigGroup", "Objective"))
                {
                    List.Add(new Objective(obj.Values[0], int.Parse(obj.Values[3]), int.Parse(obj.Values[0].Replace("X", ""))));
                }
            }

            public static Conf GetObjective(int index)
            {
                return GetConfigs("ConfigGroup", "Objective")[index];
            }
            public static bool SetPosition(int i)
            {
                string s = run_cmd("SetObjective.py", TurretName + " " + GetObjective(i).Values[0]);
                if (s.Contains("OK"))
                {
                    return true;
                }
                else
                    return false;
            }
            public static int GetPosition()
            {
                string s = run_cmd("GetObjective.py", TurretName);
                if (s.Contains("OK"))
                {
                    string[] sts = s.Split();
                    foreach (Objective o in List)
                    {
                        if(o.Name == sts[0])
                            return o.Index; 
                    }
                    return 0;
                }
                else
                    throw new Exception("Unable to get objective position. " + s);
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
                string s = run_cmd("GetShutter.py", shutterName);
                if (s.Contains("OK"))
                {
                    string[] sts = s.Split();
                    string st = sts[0].ToLower();
                    if (bool.Parse(st))
                        return 1;
                    else
                        return 0;
                }
                else
                    throw new Exception("Unable to get shutter position. " + s);
            }
            public static bool SetPosition(string shutterName, int state)
            {
                string s;
                if(state == 0)
                    s = run_cmd("SetShutter.py", shutterName + " 0");
                else
                    s = run_cmd("SetShutter.py", shutterName + " 1");
                if (s.Contains("OK"))
                {
                    return true;
                }
                else
                    return false;
            }
        }
        
        
    }
}
