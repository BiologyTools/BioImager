using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Bio
{
    public class ImageJ
    {
        public static string ImageJPath;
        public static List<Process> processes = new List<Process>();
        private static Random rng = new Random();
        public static void RunMacro(string file, string param)
        {
            file.Replace("/", "\\");
            Process pr = new Process();
            pr.StartInfo.FileName = ImageJPath;
            pr.StartInfo.Arguments = "-macro " + file + " " + param;
            pr.Start();
            processes.Add(pr);
            Recorder.AddLine("ImageJ.RunMacro(" + file + "," + '"' + param + '"' + ");");
        }
        public static void RunString(string con, string param, bool headless)
        {
            Process pr = new Process();
            pr.StartInfo.FileName = ImageJPath;
            string te = rng.Next(0, 9999999).ToString();
            string p = Environment.CurrentDirectory + "\\" + te + ".txt";
            p.Replace("/", "\\");
            File.WriteAllText(p,con);
            if(headless)
                pr.StartInfo.Arguments = "--headless -macro " + p + " " + param;
            else
                pr.StartInfo.Arguments = "-macro " + p + " " + param;
            pr.Start();
            processes.Add(pr);
            do
            {
                pr.WaitForExit();
            } while (!pr.HasExited);
            //TODO
            if (pr.StandardError.BaseStream.Length > 0)
            {
                string stder = pr.StandardError.ReadToEnd();
            }
            if (pr.StandardOutput.BaseStream.Length > 0)
            {
                string stdout = pr.StandardOutput.ReadToEnd();
            }
            File.Delete(p);
        }

        public static void Initialize(string path)
        {
            ImageJPath = path;
        }
    }
}
