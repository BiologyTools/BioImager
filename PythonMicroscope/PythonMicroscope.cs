using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Bio
{
    public class PMicroscope
    {
        public static Process deviceServer;
        public static string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.WorkingDirectory = Application.StartupPath + "/PythonMicroscope/microscope";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            string res;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    res = reader.ReadToEnd();
                    Console.Write(res);
                }
            }
            return res;
        }
        public static void Start()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("{0} {1}", "device_server.py", "config.txt");
            start.WorkingDirectory = Application.StartupPath + "/PythonMicroscope/microscope";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = false;
            start.CreateNoWindow = false;
            deviceServer = Process.Start(start);
        }
        public static void Stop()
        {
            deviceServer.Kill();
        }
        public bool Initialize(string filterwheel, string stage)
        {
            //We start the device server
            Start();
            string s = run_cmd("initialize.py", Properties.Settings.Default.PFilterWheel + " " + Properties.Settings.Default.PStage);
            if (s.Contains("OK"))
                return true;
            else
                return false;
        }

        public bool SetPosition(Point3D p)
        {
            string s = run_cmd("setstagexyz.py", Properties.Settings.Default.PStage + " " + p.X + " " + p.Y + " " + p.Z);
            if (s.Contains("OK"))
                return true;
            else
                return false;
        }
        public bool SetPosition(PointD p)
        {
            string s = run_cmd("setstagexy.py", Properties.Settings.Default.PStage + " " + p.X + " " + p.Y);
            if (s.Contains("OK"))
                return true;
            else
                return false;
        }
        public bool GetPosition3D(out Point3D p)
        {
            string s = run_cmd("getstagexyz.py", Properties.Settings.Default.PStage);
            if (s.Contains("OK"))
            {
                string[] sts = s.Split();
                p = new Point3D(double.Parse(sts[0]), double.Parse(sts[2]), double.Parse(sts[4]));
                return true;
            }
            else
            {
                p = new Point3D();
                return false;
            }
        }
        public bool GetPosition(out PointD p)
        {
            string s = run_cmd("getstagexy.py", Properties.Settings.Default.PStage);
            if (s.Contains("OK"))
            {
                string[] sts = s.Split();
                p = new PointD(double.Parse(sts[0]), double.Parse(sts[2]));
                return true;
            }
            else
            {
                p = new PointD();
                return false;
            }
        }
        public bool SetFilterWheelPosition(int p)
        {
            string s = run_cmd("setfilterwheel.py", Properties.Settings.Default.PFilterWheel + " " + p);
            if (s.Contains("OK"))
                return true;
            else
                return false;
        }
        public bool GetFilterWheelPosition(out int p)
        {
            string s = run_cmd("getfilterwheel.py", Properties.Settings.Default.PFilterWheel);
            if (s.Contains("OK"))
            {
                string[] sts = s.Split();
                p = int.Parse(sts[2]);
                return true;
            }
            else
            {
                p = -1;
                return false;
            }
        }
        public bool TakeImage(string path)
        {
           string s = run_cmd("takeimage.py", Properties.Settings.Default.PCamera + " " + path);
           if (s.Contains("OK"))
                return true;
            else
                return false;
        }
        public bool SetExposure(float f)
        {
            string s = run_cmd("setexposure.py", Properties.Settings.Default.PCamera + " " + f);
            if (s.Contains("OK"))
                return true;
            else
                return false;
        }
    }
}
