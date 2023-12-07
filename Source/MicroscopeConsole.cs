using BioImager;
using Newtonsoft.Json;

using System.Diagnostics;
using PointD = AForge.PointD;
namespace BioImager
{
    public static class MicroscopeConsole
    {
        public static bool synchronous = true;
        public static Process console = null;
        [Serializable]
        public struct Command
        {
            public enum Type
            {
                SetStage,
                SetFocus,
                SetObjective,
                SetFilterWheel,
                SetHXP,
                SetRLHalogen,
                SetTLHalogen,
                SetHXPShutter,
                SetRLShutter,
                SetTLShutter,
                SetStageSWLimit,
                SetFocusSWLimit,
                SetCalibration,
                GetStage,
                GetFocus,
                GetObjective,
                GetFilterWheel,
                GetHXP,
                GetRLHalogen,
                GetTLHalogen,
                GetHXPShutter,
                GetRLShutter,
                GetTLShutter,
                GetStageSWLimit,
                GetFocusSWLimit,
                GetCalibration,
            }
            public double[] doubles;
            public Command.Type type;
            public Command()
            {
                doubles = null;
                type = Type.SetStage;
            }
            public Command(Type t, double[] ds)
            {
                doubles = ds;
                type = t;
            }
        }
        public static void SetStagePosition(PointD p)
        {
            RunCommand(new Command(Command.Type.SetStage, new double[] { p.X, p.Y }));
        }
        static void Start()
        {
            ProcessStartInfo ps = new ProcessStartInfo();
#if DEBUG
            ps.CreateNoWindow = false;
#else
            ps.CreateNoWindow = true;
#endif
            ps.Arguments = Properties.Settings.Default.LibPath;
            ps.Arguments = ps.Arguments.Replace(' ', '_');
            ps.FileName = Application.StartupPath + "MicroscopeConsole\\MicroscopeConsole.exe";
            ps.UseShellExecute = false;
            ps.RedirectStandardError = true;
            ps.RedirectStandardOutput = true;
            ps.RedirectStandardInput = true;
            console = Process.Start(ps);
        }
        public static Command RunCommand(Command c)
        {
            Process[] prs = Process.GetProcessesByName("MicroscopeConsole.exe");
            if (prs.Length == 1)
                console = prs[0];
            if (console == null)
            {
                Start();
            }
            if (c.type.ToString().StartsWith("Get"))
            {
                string arg = JsonConvert.SerializeObject(c);
                console.StandardInput.WriteLine("Command:" + arg);
                A:
                try
                {
                    do
                    {
                        if (console.HasExited)
                            break;
                        string st = console.StandardOutput.ReadLine();
                        if(st != null)
                        if (st.Length > 0)
                        {
                            return JsonConvert.DeserializeObject<Command>(st);
                        }
                        Thread.Sleep(20);
                    } while (true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto A;
                }
                
                string er = console.StandardError.ReadToEnd();
                string o = console.StandardOutput.ReadToEnd();
                Console.WriteLine(er);
                console = null;
                return new Command();
            }
            else
            {
                string arg = JsonConvert.SerializeObject(c);
                console.StandardInput.WriteLine("Command:" + arg);
                if (synchronous)
                {
                    //Now we need to wait till the command is finished.
                    do
                    {
                        if (console.HasExited)
                            break;
                        string st = console.StandardOutput.ReadLine();
                        if (st != null)
                            if (st.Length > 0)
                            {
                                return JsonConvert.DeserializeObject<Command>(st);
                            }
                        Application.DoEvents();
                    } while (true);
                }
                return new Command();
            }
        }
    }
}
