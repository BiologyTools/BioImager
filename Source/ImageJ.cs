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
            if(ImageJPath == "")
            {
                if (!App.SetImageJPath())
                    return;
            }
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

            if (ImageJPath == "")
            {
                if (!App.SetImageJPath())
                    return;
            }
            Process pr = new Process();
            pr.StartInfo.FileName = ImageJPath;
            pr.StartInfo.CreateNoWindow = true;
            string te = rng.Next(0, 9999999).ToString();
            string p = Environment.CurrentDirectory + "\\" + te + ".txt";
            p.Replace("/", "\\");
            File.WriteAllText(p,con);
            if(headless)
                pr.StartInfo.Arguments = "--headless -macro " + p + " " + param;
            else
                pr.StartInfo.Arguments = "-macro " + p + " " + param;
            pr.Start();
            File.Delete(Path.GetDirectoryName(ImageJPath) + "/done.txt");
            processes.Add(pr);
            do
            {
                if (File.Exists(Path.GetDirectoryName(ImageJPath) + "/done.txt"))
                {
                    do
                    {
                        try
                        {
                            File.Delete(Path.GetDirectoryName(ImageJPath) + "/done.txt");
                        }
                        catch (Exception)
                        {
                        
                        }
                    } while (File.Exists(Path.GetDirectoryName(ImageJPath) + "/done.txt"));
                    pr.Kill();
                    break;
                }
            } while (!pr.HasExited);
            File.Delete(p);
        }
        public static void RunOnImage(string con, bool headless, bool onTab, bool bioformats)
        {
            if (ImageJPath == "" || ImageJPath == null)
            {
                if (!App.SetImageJPath())
                    return;
            }
            string filename = "";
            string dir = Path.GetDirectoryName(ImageView.SelectedImage.file);

            if (ImageView.SelectedImage.ID.EndsWith(".ome.tif"))
            {
                filename = Path.GetFileNameWithoutExtension(ImageView.SelectedImage.ID);
                filename = filename.Remove(filename.Length - 4, 4);
            }
            else
                filename = Path.GetFileNameWithoutExtension(ImageView.SelectedImage.ID);
            string file = dir + "\\" + filename + "-temp" + ".ome.tif";
            file = file.Replace("\\", "/");
            string st =
            "run(\"Bio-Formats Importer\", \"open=\" + getArgument + \" autoscale color_mode=Default open_all_series display_rois rois_import=[ROI manager] view=Hyperstack stack_order=XYCZT\"); " + con +
            "run(\"Bio-Formats Exporter\", \"save=" + file + " export compression=Uncompressed\"); " +
            "dir = getDir(\"startup\"); " +
            "File.saveString(\"done\", dir + \"/done.txt\");";
            if(bioformats)
                st =
                "open(getArgument); " + con +
                "run(\"Bio-Formats Exporter\", \"save=" + file + " export compression=Uncompressed\"); " +
                "dir = getDir(\"startup\"); " +
                "File.saveString(\"done\", dir + \"/done.txt\");";
            //We save the image as a temp image as otherwise imagej won't export due to file access error.
            RunString(st, ImageView.SelectedImage.file, headless);

            if (!File.Exists(file))
                return;

            string ffile = dir + "/" + filename + ".ome.tif";
            File.Delete(ffile);
            File.Copy(file, ffile);
            File.Delete(file);
            App.tabsView.AddTab(BioImage.OpenFile(ffile));
            App.viewer.UpdateImage();
            App.viewer.UpdateView();
            
            Recorder.AddLine("RunOnImage(\"" + con + "\"," + headless + "," + onTab + ");");
        }

        /*
        public static void RunOnImage(string con, bool headless, bool onTab)
        {
            string filename = "";
            string dir = Path.GetDirectoryName(ImageView.SelectedImage.file);
            if(App.viewer.Images.Count == 1)
            {
                if (ImageView.SelectedImage.ID.EndsWith(".ome.tif"))
                {
                    filename = Path.GetFileNameWithoutExtension(ImageView.SelectedImage.ID);
                    filename = filename.Remove(filename.Length - 4, 4);
                }
                else
                    filename = Path.GetFileNameWithoutExtension(ImageView.SelectedImage.ID);
            }
            else
            {
                string f = App.viewer.Images[0].ID;
                if (f.EndsWith(".ome.tif"))
                {
                    filename = Path.GetFileNameWithoutExtension(f);
                    filename = filename.Remove(filename.Length - 4, 4);
                }
                else
                    filename = Path.GetFileNameWithoutExtension(f);
            }

            string temp;
            if(dir == "")
                temp = filename + "-temp" + ".ome.tif";
            else
                temp = dir + "\\" + filename  + "-temp" + ".ome.tif";
            temp = temp.Replace("\\", "/");
            string st;
            if (!onTab)
            {
                st =
                "run(\"Bio-Formats Importer\", \"open=\" + getArgument + \" autoscale color_mode=Default open_all_series display_rois rois_import=[ROI manager] view=Hyperstack stack_order=XYCZT\"); " + con +
                "run(\"Bio-Formats Exporter\", \"save=" + temp + " export compression=Uncompressed\"); " +
                "dir = getDir(\"startup\"); " +
                "File.saveString(\"done\", dir + \"/done.txt\");";
            }
            else
            {
                //Currently v.2.4.1 some OME series written by Bio open up only correctly in ImageJ when opened with ImageJ open() rather than BioFormats.
                st =
                "open(getArgument); " + con +
                "run(\"Bio-Formats Exporter\", \"save=" + temp + " export compression=Uncompressed\"); " +
                "dir = getDir(\"startup\"); " +
                "File.saveString(\"done\", dir + \"/done.txt\");";
            }
            if (App.viewer.Images.Count == 1)
            {
                //First we save to a temp file.
                BioImage.SaveOME(temp, ImageView.SelectedImage.file);
            }
            else
            {
                //First we save to a temp file.
                List<string> sts = new List<string>();
                foreach (BioImage item in App.viewer.Images)
                {
                    sts.Add(item.ID);
                }
                BioImage.SaveOMESeries(sts.ToArray(), temp);
            }
            //We save the image as a temp image as otherwise imagej won't export due to file access error.
            RunString(st, temp, headless);
            string ffile = dir + "/" + filename + ".ome.tif";
            File.Delete(ImageView.SelectedImage.file);
            File.Copy(temp, ImageView.SelectedImage.file);
            File.Delete(temp);
            if (ImageView.SelectedImage.ID.EndsWith(".ome.tif"))
            {
                if(ImageView.SelectedImage.seriesCount > 1)
                foreach (BioImage item in App.viewer.Images)
                {
                    item.Update();
                }
                else
                    ImageView.SelectedImage.Update();
            }
            else
            {
                App.tabsView.AddTab(BioImage.OpenFile(ImageView.SelectedImage.file));
            }
            Recorder.AddLine("RunOnImage(\"" + con + "," + headless + "," + onTab + ");");
        }
        */
        public static void Initialize(string path)
        {
            ImageJPath = path;
        }
    }
}
