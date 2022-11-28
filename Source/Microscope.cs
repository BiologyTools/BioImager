﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.IO;

namespace Bio
{
    public class Stage
    {
        public static object stage;
        public static Type stageType;
        public static Type axisType;
        public static object xAxis;
        public static object yAxis;
        public static double minX;
        public static double maxX;
        public static double minY;
        public static double maxY;
        public Stage(object st)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                stageType = Microscope.Types["IMTBStage"];
                stage = st;
                axisType = Microscope.Types["IMTBAxis"];
                xAxis = Microscope.GetProperty("IMTBStage", "XAxis", Stage.stage);
                yAxis = Microscope.GetProperty("IMTBStage", "YAxis", Stage.stage);
                PointD d = GetPosition();
                x = GetPositionX();
                y = GetPositionY();
                UpdateSWLimit();
            }
        }
        public Stage()
        {

        }

        private double x;
        private double y;
        public double X
        {
            get
            {
                return x;
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
        }
        public int moveWait = 250;
        private void MoveWait()
        {
            Thread.Sleep(moveWait);
        }
        public void SetPosition(double px, double py)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Stage.SetPosition(" + px + "," + py + ");");
            x = px;
            y = py;
            if(Properties.Settings.Default.PMicroscope)
            {
                Microscope.pMicroscope.SetPosition(new PointD(px, py));
            }
            else
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            { 
                object[] setPosArgs = new object[5];
                setPosArgs[0] = px;
                setPosArgs[1] = py;
                setPosArgs[2] = "µm";
                setPosArgs[3] = Microscope.CmdSetMode;
                setPosArgs[4] = 10000;
                bool resStage = (bool)stageType.InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, stage, setPosArgs);
            }
            else if (Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                Microscope.sdk.SetPosition(new PointD(px, py));
            }
            if (App.viewer != null)
                App.viewer.UpdateView(true);
        }
        public void SetPositionX(double px)
        {
            x = px;
            SetPosition(px, y);
        }
        public void SetPositionY(double py)
        {
            y = py;
            SetPosition(x, py);
        }
        public double GetPositionX()
        {
            x = GetPosition().X;
            return x;
        }
        public double GetPositionY()
        {
            y = GetPosition().Y;
            return y;
        }
        public PointD GetPosition()
        {
            if (Properties.Settings.Default.PMicroscope)
            {
                PointD pd;
                Microscope.pMicroscope.GetPosition(out pd);
                return pd;
            }
            else
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[3];
                args[2] = "µm";
                Microscope.Types["IMTBStage"].InvokeMember("GetPosition", BindingFlags.InvokeMethod, null, stage, args);
                x = (double)args[0];
                y = (double)args[1];
                return new PointD((double)args[0], (double)args[1]);
            }
            else if (Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                return Microscope.sdk.GetPosition();
            }
            return new PointD(x, y);
        }
        public void MoveUp(double m)
        {
            double y = GetPositionY() - m;
            SetPositionY(y);
        }
        public void MoveDown(double m)
        {
            double y = GetPositionY() + m;
            SetPositionY(y);
        }
        public void MoveRight(double m)
        {
            double x = GetPositionX() + m;
            SetPositionX(x);
        }
        public void MoveLeft(double m)
        {
            double x = GetPositionX() - m;
            SetPositionX(x);
        }
        public void UpdateSWLimit()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[2];
                args[0] = true;
                args[1] = "µm";
                maxX = (double)axisType.InvokeMember("GetSWLimit", BindingFlags.InvokeMethod, null, xAxis, args);
                args[0] = false;
                minX = (double)axisType.InvokeMember("GetSWLimit", BindingFlags.InvokeMethod, null, xAxis, args);
                args[0] = true;
                maxY = (double)axisType.InvokeMember("GetSWLimit", BindingFlags.InvokeMethod, null, yAxis, args);
                args[0] = false;
                minY = (double)axisType.InvokeMember("GetSWLimit", BindingFlags.InvokeMethod, null, yAxis, args);
                if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Focus.SetSWLimit(" + minX + "," + maxX + "," + minY + "," + maxY + ");");
            }
           
        }
        public void SetSWLimit(double xmin, double xmax, double ymin, double ymax)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[3];
                args[0] = true;
                args[1] = xmax;
                args[2] = "µm";
                axisType.InvokeMember("SetSWLimit", BindingFlags.InvokeMethod, null, xAxis, args);
                args[0] = false;
                args[1] = xmin;
                axisType.InvokeMember("SetSWLimit", BindingFlags.InvokeMethod, null, xAxis, args);

                args[0] = true;
                args[1] = ymax;
                args[2] = "µm";
                axisType.InvokeMember("SetSWLimit", BindingFlags.InvokeMethod, null, yAxis, args);
                args[0] = false;
                args[1] = ymin;
                axisType.InvokeMember("SetSWLimit", BindingFlags.InvokeMethod, null, yAxis, args);
            }
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Focus.SetSWLimit(" + xmin + "," + xmax + "," + ymin + "," + ymax + ");");
        }
    }

    public class Focus
    {
        public static Type focusType;
        public static Type axisType;
        public static object focus;
        public static double upperLimit;
        public static double lowerLimit;
        private static double z;
        public Focus(object foc)
        {
            focus = foc;
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                axisType = Microscope.Types["IMTBAxis"];
            }
        }
        public Focus()
        {

        }
        public void SetFocus(double f)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Focus.SetFocus(" + f + ");");
            if (Properties.Settings.Default.PMicroscope)
            {
                PointD p = Microscope.Stage.GetPosition();
                Microscope.pMicroscope.SetPosition(new Point3D(p.X,p.Y,f));
            }
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[3];
                args[0] = f;
                args[1] = "µm";
                args[2] = Microscope.CmdSetMode;
                bool resFoc = (bool)Microscope.Types["IMTBContinual"].InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, focus, args);
                z = f;
            }
            else if (Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                Microscope.sdk.SetZ(f);
            }
        }
        public double GetFocus()
        {
            if (Properties.Settings.Default.PMicroscope)
            {
                Point3D p;
                Microscope.pMicroscope.GetPosition3D(out p);
                return p.Z;
            }
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] getPosFocArgs = new object[1];
                getPosFocArgs[0] = "µm";
                z = (double)Microscope.Types["IMTBContinual"].InvokeMember("GetPosition", BindingFlags.InvokeMethod, null, focus, getPosFocArgs);
                return z;
            }
            if (Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                return Microscope.sdk.GetZ();
            }
            return z;
        }
        public PointD GetSWLimit()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                PointD d = new PointD();
                object[] args = new object[2];
                args[0] = true;
                args[1] = "µm";
                d.X = (double)axisType.InvokeMember("GetSWLimit", BindingFlags.InvokeMethod, null, focus, args);
                args[0] = false;
                d.Y = (double)axisType.InvokeMember("GetSWLimit", BindingFlags.InvokeMethod, null, focus, args);
                upperLimit = d.X;
                lowerLimit = d.Y;
                return d;
            }
            return new PointD(lowerLimit, upperLimit);
        }
        public void SetSWLimit(double xd, double yd)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[3];
                args[0] = true;
                args[1] = xd;
                args[2] = "µm";
                axisType.InvokeMember("SetSWLimit", BindingFlags.InvokeMethod, null, focus, args);
                args[0] = false;
                args[1] = yd;
                axisType.InvokeMember("SetSWLimit", BindingFlags.InvokeMethod, null, focus, args);
            }
            upperLimit = xd;
            lowerLimit = yd;
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Focus.SetSWLimit(" + xd + "," + yd + ");");
        }
    }

    public class Objectives
    {
        public List<Objective> List = new List<Objective>();
        public static Type changerType = null;
        public static object changer;

        public Objectives(object objs)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                changerType = Microscope.Types["IMTBChanger"];
                changer = objs;

                int count = (int)changerType.InvokeMember("GetElementCount", BindingFlags.InvokeMethod, null, objs, null);
                object[] args3 = new object[1];

                for (int i = 0; i < count; i++)
                {
                    args3[0] = i;
                    object o = changerType.InvokeMember("GetElement", BindingFlags.InvokeMethod, null, objs, args3);
                    if (o != null)
                        List.Add(new Objective(o, i));
                }
            }
        }
        public Objectives(int count)
        {
            for (int i = 0; i < count; i++)
            {
                List.Add(new Objective(null, i));
            }
        }
        public class Objective
        {
            public Dictionary<string, object> config = new Dictionary<string, object>();
            public string Name = "";
            public string UniqueName = "";
            public string ElementType = "";
            public bool Oil = false;
            public int Magnification = 0;
            public float NumericAperture = 0;
            public int Index;
            public string Modes = "";
            public string Features = "";
            public double LocateExposure = 50;
            public int WorkingDistance = 0;
            public double AcquisitionExposure = 50;
            public string Configuration = "";
            public double MoveAmountL = 40;
            public double MoveAmountR = 10;
            public double FocusMoveAmount = 0.02;
            public double ViewWidth;
            public double ViewHeight;
            public Objective(object o,int index)
            {
                if (Properties.Settings.Default.LibPath.Contains("MTB"))
                {
                    Type t = Microscope.Types["IMTBChangerElement"];
                    Type id = Microscope.Types["IMTBIdent"];
                    Name = (string)id.InvokeMember("get_Name", BindingFlags.InvokeMethod, null, o, null);
                    UniqueName = (string)id.InvokeMember("get_UniqueName", BindingFlags.InvokeMethod, null, o, null);
                    Configuration = (string)id.InvokeMember("GetConfiguration", BindingFlags.InvokeMethod, null, o, null);
                    string[] sts = Configuration.Split('>');
                    for (int i = 0; i < sts.Length; i++)
                    {
                        string item = sts[i];
                        int ind = item.IndexOf("</");
                        if (item.StartsWith("<") || item.Length == 0)
                            continue;
                        string s = item.Remove(ind, item.Length - ind);
                        if (i == 2)
                            Magnification = int.Parse(s);
                        else
                        if (i == 4)
                            NumericAperture = float.Parse(s, CultureInfo.InvariantCulture);
                        else
                        if (i == 6)
                        {
                            if (s.Contains("Air"))
                                Oil = false;
                            else
                                Oil = true;
                        }
                        else
                        if (i == 8)
                        {
                            Modes = s;
                        }
                        else
                        if (i == 10)
                        {
                            Features = s;
                        }
                        else
                        if (i == 12)
                        {
                            WorkingDistance = int.Parse(s);
                        }
                    }
                }
                Index = index;
            }
            public Objective()
            {
            }
            public override string ToString()
            {
                return Name.ToString() + " " + Index;
            }
        }
        private int index;
        public int moveWait = 1000;
        private void MoveWait()
        {
            Thread.Sleep(moveWait);
        }
        public int Index
        {
            get
            {
                return GetPosition();
            }
            set
            {
                SetPosition(value);
            }
        }
        public void SetPosition(int index)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Objectives.SetObjective(" + index + ");");
            this.index = index;
            if (!Properties.Settings.Default.LibPath.Contains("MTB") && !Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                Function f = Function.Functions["SetO" + index];
                App.imager.PerformFunction(f);
                return;
            }
            else if(Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] setObjPosArgs = new object[3];
                setObjPosArgs[0] = (short)index;
                setObjPosArgs[1] = Microscope.CmdSetMode;
                setObjPosArgs[2] = 10000;
                bool resObj = (bool)changerType.InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, changer, setObjPosArgs);
            }
            
        }
        public int GetPosition()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                return List[(short)changerType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, changer, null)].Index;
            }
            else
            {
                return Microscope.sdk.GetNosePiece();
            }
        }
        public Objective GetObjective()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                return List[(short)changerType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, changer, null)];
            }
            else
                return List[index];
        }
    }

    public class TLShutter
    {
        public static Type tlType;
        public static object tlShutter = null;
        public static int position;
        public TLShutter(object tlShut)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                tlShutter = tlShut;
                tlType = Microscope.Types["IMTBChanger"];
            }
        }
        public short GetPosition()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
                return (short)tlType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, tlShutter, null);
            else
            return (short)position;
        }
        public void SetPosition(int p)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.TLShutter.SetPosition(" + p + ");");
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[2];
                args[0] = (short)0;
                args[1] = Activator.CreateInstance(Microscope.Types["MTBCmdSetModes"]);
                bool res = (bool)tlType.InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, tlShutter, args);
                position = p;
            }
            if (Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                if (p == 0)
                    Microscope.sdk.shutterClose();
                else
                    Microscope.sdk.shutterOpen();
            }
        }
    }

    public class RLShutter
    {
        public static Type rlType;
        public static object rlShutter = null;
        public static int position;
        public RLShutter(object rlShut)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                rlShutter = rlShut;
                rlType = Microscope.Types["IMTBChanger"];
            }

        }
        public short GetPosition()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
                return (short)rlType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, rlShutter, null);
            else
                return (short)position;
        }
        public void SetPosition(int p)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[2];
                args[0] = (short)0;
                args[1] = Activator.CreateInstance(Microscope.Types["MTBCmdSetModes"]);
                bool res = (bool)rlType.InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, rlShutter, args);
            }
            position = p;
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.RLShutter.SetPosition(" + p + ");");
        }
    }

    public class FilterWheel
    {
        public int GetPosition()
        {
            if (Properties.Settings.Default.PMicroscope)
            {
                int pos;
                Microscope.pMicroscope.GetFilterWheelPosition(out pos);
                return pos;
            }
            return -1;
        }
        public void SetPosition(int i)
        {
            if (Properties.Settings.Default.PMicroscope)
            {
                int pos;
                Microscope.pMicroscope.SetFilterWheelPosition(i);
                if (Recorder.recordMicroscope)
                    Recorder.AddLine("Microscope.FilterWheel.SetPosition(" + i + ");");
            }
        }
    }

    public static class Microscope
    {
        public enum Actions
        {
            StageUp,
            StageRight,
            StageDown,
            StageLeft,
            StageFieldUp,
            StageFieldRight,
            StageFieldDown,
            StageFieldLeft,
            FocusUp,
            FocusDown,
            TL,
            RL,
            TakeImage,
            TakeImageStack,
            Acquisition,
            Locate,
        }
        public static bool redraw = false;
        public static Focus Focus = null;
        public static Stage Stage = null;
        public static Objectives Objectives = null;
        public static TLShutter TLShutter = null;
        public static RLShutter RLShutter = null;
        public static FilterWheel FilterWheel = new FilterWheel();
        public static double UpperLimit, LowerLimit, fInterVal;
        public static object CmdSetMode = null;
        public static bool initialized = false;
        public static bool ArrowKeysEnabled = true;
        public static Point3D defaultPos = new Point3D(30000, 30000, 23900);
        public static Assembly dll = null;
        public static Dictionary<string, Type> Types = new Dictionary<string, Type>();
        public static object root = null;
        public static StringBuilder dllVersion = new StringBuilder();
        public static SDK sdk;
        public static int sessionID = -1;
        public static string userRx = "";
        public static PointD viewSize;
        public static PMicroscope pMicroscope = null;
        public static int ImageCount = 0;
        public static Objectives.Objective Objective
        {
            get
            {
                return Objectives.GetObjective();
            }
        }
        public static void Initialize()
        {
            if (initialized)
                return;
            int err;
            folder = Properties.Settings.Default.ImagingPath;
            if (Properties.Settings.Default.PMicroscope)
            {
                pMicroscope = new PMicroscope();
                pMicroscope.Initialize(Properties.Settings.Default.PFilterWheel, Properties.Settings.Default.PStage);
                bool b = pMicroscope.SetPosition(new Point3D(100, 100, 100));
                Point3D p;
                bool res = pMicroscope.GetPosition3D(out p);
                bool bb = pMicroscope.TakeImage(Application.StartupPath + "\\Image");
                Objectives = new Objectives(6);
                Stage = new Stage();
                Focus = new Focus();
            }
            else
            if (Properties.Settings.Default.AppPath == "")
            {
                OpenFileDialog fl = new OpenFileDialog();
                MessageBox.Show("Imaging application path is not set. Please set executable location.");
                fl.Title = "Set imaging application location.";
                if (fl.ShowDialog() != DialogResult.OK)
                    Application.Exit();
                Properties.Settings.Default.AppPath = fl.FileName;
                fl.Dispose();
            }
            else
            if (Properties.Settings.Default.LibPath == "")
            {
                OpenFileDialog fl = new OpenFileDialog();
                MessageBox.Show("Imaging library path is not set. Please set imaging library.");
                fl.Title = "Set imaging library location.";
                if (fl.ShowDialog() != DialogResult.OK)
                    Application.Exit();
                Properties.Settings.Default.LibPath = fl.FileName;
                fl.Dispose();
            }

            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                //We dynamically load the dll installed on the system.
                string app = Path.GetDirectoryName(Properties.Settings.Default.AppPath);
                dll = Assembly.LoadFile(app + "\\" + "MTBApi.dll");
                Type[] tps = dll.GetExportedTypes();
                foreach (Type type in tps)
                {
                    string s = type.ToString();
                    s = s.Remove(0, s.LastIndexOf('.') + 1);
                    Types.Add(s, type);
                }
                CmdSetMode = GetEnum("MTBCmdSetModes", "Synchronous");
                Type con = Types["MTBConnection"];
                var c = Activator.CreateInstance(con);
                object[] args = new object[2];
                args[0] = "en";
                args[1] = "";
                con.InvokeMember("Login", BindingFlags.InvokeMethod, null, c, args);
                object[] args2 = new object[1];
                args2[0] = args[1];
                con.InvokeMember("Init", BindingFlags.InvokeMethod, null, c, args2);
                root = con.InvokeMember("GetRoot", BindingFlags.InvokeMethod, null, c, args2);
                Type r = Types["IMTBRoot"];
                object[] st = new object[1];
                st[0] = "MTBStage";
                var sta = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBFocus";
                var foc = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBTLShutter";
                var tlShutter = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBRLShutter";
                var rlShutter = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBObjectiveChanger";
                var objs = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);

                Stage = new Stage(sta);
                Focus = new Focus(foc);
                Objectives = new Objectives(objs);
                TLShutter = new TLShutter(tlShutter);
                RLShutter = new RLShutter(rlShutter);
                Point3D.SetLimits(Stage.minX, Stage.maxX, Stage.minY, Stage.maxY, Focus.lowerLimit, Focus.upperLimit);
                PointD.SetLimits(Stage.minX, Stage.maxX, Stage.minY, Stage.maxY);
                //We calibrate the stage and focus, so that images are taken always with same calibration
                CalibrateXYZ("OnLowerLimit");
            }
            else if (Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                sdk = new SDK();
                /* get the version number of the dll */
                if ((err = sdk.GetVersion(dllVersion)) != Prior.PRIOR_OK)
                {
                    MessageBox.Show("Error getting Prior SDK version (" + err.ToString() + ")");
                    return;
                }

                /* SDK must be initialised before any real use
                 */
                if ((err = sdk.Initialise()) != Prior.PRIOR_OK)
                {
                    MessageBox.Show("Error initialising Prior SDK (" + err.ToString() + ")");
                    return;
                }

                /* create a session in the DLL, this gives us one controller and currently an ODS and SL160 robot loader. 
                 * Multiple connections allow control of multiple stage/loaders but is outside the brief for this demo
                 */
                if ((sessionID = sdk.OpenSession()) < 0)
                {
                    MessageBox.Show("Error (" + sessionID.ToString() + ") Creating session in SDK " + dllVersion);
                    return;
                }
                int open = 0;
                try
                {
                    for (int port = 0; port < 10; port++)
                    {
                        //specify path name or PriorSDK.log is written to working directory
                        //priorSDK.Cmd(sessionID, "dll.log.on",  ref userRx);
                        /* my controller identifies on COM1, yours will probably be different.
                         */
                        /* try to connect to the ps3 */
                        open = sdk.Cmd(sessionID, "controller.connect " + port.ToString(), ref userRx, false);
                    }
                }
                catch (Exception)
                {

                }
                
                if (open != Prior.PRIOR_OK)
                {
                    MessageBox.Show("Error (" + open.ToString() + ")  connecting to stage controller ", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                err = sdk.Cmd(sessionID, "controller.stage.hostdirection.set 1 1", ref userRx);
                Objectives = new Objectives(sdk.GetNosePiecePositions());
                Stage = new Stage();
                Focus = new Focus();
            }

            RectangleD rec = GetObjectiveViewRectangle();
            viewSize = new PointD(rec.W, rec.H);
            Watcher();
            initialized = true;
        }
        public static object Invoke(Type type, string name, object o, object[] args)
        {
            return type.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }
        public static object Invoke(string type, string name, object o, object[] args)
        {
            Type t = Types[type];
            return t.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }
        public static object GetProperty(string type, string name, object obj)
        {
            Type myType = Types[type];
            PropertyInfo p = myType.GetProperty(name);
            return p.GetValue(obj);
        }
        public static object GetEnum(string type, string name)
        {
            Array ar = Enum.GetValues(Types[type]);
            foreach (var item in ar)
            {
                if (item.ToString() == name)
                    return item;
            }
            return null;
        }

        public static void CalibrateXYZ(string calibMode)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                //We check to see if focus & stage are correctly calibrated on lower limit and perform calibration if necessary
                PointD cur = Stage.GetPosition();
                double z = Focus.GetFocus();
                Type mt = Types["MTBCalibrationModes"];

                object xaxis = GetProperty("IMTBStage", "XAxis", Stage.stage);
                object xmode = GetProperty("IMTBAxis", "CalibrationMode", xaxis);

                object yaxis = GetProperty("IMTBStage", "YAxis", Stage.stage);
                object ymode = GetProperty("IMTBAxis", "CalibrationMode", yaxis);

                object zmode = GetProperty("IMTBAxis", "CalibrationMode", Focus.focus);

                object[] args = new object[3];
                args[0] = GetEnum("MTBCalibrationModes", calibMode);
                args[1] = CmdSetMode;
                if (xmode.ToString() != "OnLowerLimit")
                {
                    Invoke("IMTBAxis", "Calibrate", xaxis, args);
                }
                if (ymode.ToString() != "OnLowerLimit")
                {
                    Invoke("IMTBAxis", "Calibrate", yaxis, args);
                }
                if (zmode.ToString() != "OnLowerLimit")
                {
                    Invoke("IMTBAxis", "Calibrate", Focus.focus, args);
                }
                //After calibration we return to the position before calibration.
                SetPosition(cur);
                Focus.SetFocus(z);
            }
        }

        public static Point3D GetPosition()
        {
            PointD p = Stage.GetPosition();
            double f = Focus.GetFocus();
            return new Point3D(p.X, p.Y, f);
        }

        public static void SetPosition(Point3D p)
        {
            Stage.SetPosition(p.X,p.Y);
            Focus.SetFocus(p.Z);
            Microscope.redraw = true;
        }

        public static void SetPosition(PointD p)
        {
            Stage.SetPosition(p.X, p.Y);
            Microscope.redraw = true;
        }
        public static void OpenRL()
        {
            //If shutter is closed we open it.
            if (RLShutter.GetPosition() == 0)
                RLShutter.SetPosition(1);
        }

        public static void OpenTL()
        {
            //If shutter is closed we open it.
            if (TLShutter.GetPosition() == 0)
                TLShutter.SetPosition(1);
        }

        public static void CloseRL()
        {
            //If shutter is open then we close it.
            if (RLShutter.GetPosition() == 0)
                RLShutter.SetPosition(1);
        }

        public static void CloseTL()
        {
            //If shutter is open then we close it.
            if (TLShutter.GetPosition() == 0)
                TLShutter.SetPosition(1);
        }

        public static void SetTL(uint tl)
        {
            TLShutter.SetPosition((short)tl);
        }

        public static void SetRL(uint tr)
        {
            RLShutter.SetPosition((short)tr);
        }

        public static int GetTL()
        {
            return TLShutter.GetPosition();
        }

        public static int GetRL()
        {
            return RLShutter.GetPosition();
        }

        public static void MoveUp(double d)
        {
            Stage.MoveUp(d);
        }

        public static void MoveRight(double d)
        {
            Stage.MoveRight(d);
        }

        public static void MoveDown(double d)
        {
            Stage.MoveDown(d);
        }

        public static void MoveLeft(double d)
        {
            Stage.MoveLeft(d);
        }
        public static void MoveFieldUp()
        {
            Stage.MoveUp(viewSize.Y);
        }

        public static void MoveFieldRight()
        {
            Stage.MoveRight(viewSize.X);
        }

        public static void MoveFieldDown()
        {
            Stage.MoveDown(viewSize.Y);
        }

        public static void MoveFieldLeft()
        {
            Stage.MoveLeft(viewSize.X);
        }

        public static void SetFocus(double d)
        {
            Focus.SetFocus(d);
        }

        public static double GetFocus()
        {
            return Focus.GetFocus();
        }
        private static string folder = "";
        public static string GetFolder()
        {

            object o = Recordings.GetProperty(Automation.Action.ValueType.ValuePattern, "GetFolder");
            string s = null;
            if (o != null)
                s = o.ToString();
            if (s == null && folder == "" && Properties.Settings.Default.ImagingPath == "")
            {
                FolderBrowserDialog fs = new FolderBrowserDialog();
                fs.Description = "Select Imaging Folder";
                if (fs.ShowDialog() != DialogResult.OK)
                {
                    fs.Dispose();
                    return null;
                }
                else
                {
                    fs.Dispose();
                    return fs.SelectedPath;
                }
            }
            else
            {
                folder = Properties.Settings.Default.ImagingPath;
                return folder;
            }
        }
        public static void SetFolder(string fol)
        {
            if (!Directory.Exists(fol))
                Directory.CreateDirectory(fol);
            if(Automation.Properties.ContainsKey("SetFolder"))
                Automation.SetProperty("SetFolder",fol);
            folder = fol;
            Properties.Settings.Default.ImagingPath = folder;
            Properties.Settings.Default.Save();
        }

        public static void TakeImage()
        {
            TakeImage(0);
            App.viewer.AddImage(bi);
        }
        public static void TakeImage(int i)
        {
            if (Properties.Settings.Default.SimulateCamera && !Properties.Settings.Default.PMicroscope)
            {
                BioImage b = MicroscopeSetup.simImage.Copy();
                b.Volume.Location = GetPosition();
                b.stageSizeX = b.Volume.Location.X;
                b.stageSizeY = b.Volume.Location.Y;
                b.stageSizeZ = b.Volume.Location.Z;
                b.Filename = Images.GetImageName(App.stage.ImageName);
                if(i==0)
                {
                    if (bi == null)
                    {
                        bi = MicroscopeSetup.simImage;
                    }
                    bi.Volume.Location = b.Volume.Location;
                    bi.Channels = MicroscopeSetup.simImage.Channels;
                    bi.stageSizeX = b.Volume.Location.X;
                    bi.stageSizeY = b.Volume.Location.Y;
                    bi.stageSizeZ = b.Volume.Location.Z;
                    bi.physicalSizeX = b.physicalSizeX;
                    bi.physicalSizeY = b.physicalSizeY;
                    bi.physicalSizeZ = b.physicalSizeZ;
                }
                bi.Buffers[i].Plane.Coordinate = new ZCT(i, 0, 0);
                bi.Buffers[i].Plane.Location = GetPosition();

                watcher.Path = Properties.Settings.Default.ImagingPath;
                watcher.EnableRaisingEvents = true;
                string file;
                if (folder == "" || folder == null)
                    file = Properties.Settings.Default.ImageName + i;
                else
                    file = folder + "/" + Properties.Settings.Default.ImageName + i;
                BioImage.SaveOME(file, b.ID);
                Application.DoEvents();
            }
            else
            {
                if(Properties.Settings.Default.PMicroscope)
                {
                    folder = GetFolder();
                    string file;
                    if (folder == "" || folder == null)
                        file = Properties.Settings.Default.ImageName + (ImageCount + 1).ToString();
                    else
                        file = folder + "/" + Properties.Settings.Default.ImageName + (ImageCount + 1);
                    pMicroscope.TakeImage(file);
                    ImageCount++;
                    byte[] bts = File.ReadAllBytes(file);
                    int w, h;
                    System.Drawing.Imaging.PixelFormat px;
                    Enum.TryParse<System.Drawing.Imaging.PixelFormat>(Properties.Settings.Default.PCameraFormat, out px);
                    pMicroscope.GetSize(out w, out h);
                    BufferInfo bf = new BufferInfo(file, w, h, px,bts, new ZCT(0, 0, 0),0);
                    Statistics.CalcStatistics(bf);
                    BioImage bm = new BioImage(file);
                    bm.Buffers.Add(bf);
                    //Set the physical size to be 96 dpi
                    bm.physicalSizeX = 96f / 2540f;
                    bm.physicalSizeY = 96f / 2540f;
                    bm.physicalSizeZ = 1;
                    PointD p = Stage.GetPosition();
                    double f = Focus.GetFocus();
                    bm.bitsPerPixel = bf.BitsPerPixel;
                    bm.stageSizeX = p.X;
                    bm.stageSizeY = p.Y;
                    bm.stageSizeZ = f;
                    bm.UpdateCoords(1,1,1);
                    for (int c = 0; c < bf.RGBChannelsCount; c++)
                    {
                        bm.Channels.Add(new Channel(i, bf.BitsPerPixel, bf.RGBChannelsCount));
                    }
                    Images.AddImage(bm);
                    BioImage.SaveOME(file + ".ome.tif", bm.ID);
                    currentImage = bm;
                }
                else
                App.imager.PerformFunction(Function.Functions["TakeImage"]);
            } 
        }
        public static BioImage TakeImageStack()
        {
            bi = new BioImage(Properties.Settings.Default.ImageName);
            watcher.Path = GetFolder();
            Focus.SetFocus(UpperLimit);
            double d = Math.Abs(UpperLimit - LowerLimit);
            double dd = d / fInterVal;
            for (int i = 0; i < dd+1; i++)
            {
                TakeImage(i);
                //If this is the last image we don't need to move the focus. 
                if(i < dd)
                Focus.SetFocus(Focus.GetFocus() - fInterVal);
                Application.DoEvents();
            }
            bi.UpdateCoords(bi.Buffers.Count, 1, 1);
            bi.Volume = new VolumeD(new Point3D(bi.stageSizeX, bi.stageSizeY, bi.stageSizeZ), new Point3D(bi.physicalSizeX * bi.SizeX, bi.physicalSizeY * bi.SizeY, bi.physicalSizeZ * bi.SizeZ));
            if (bi.bitsPerPixel > 8)
                bi.StackThreshold(true);
            else
                bi.StackThreshold(false);
            Images.AddImage(bi);
            return bi;
        }
        static FileSystemWatcher watcher = new FileSystemWatcher();
        public static void Watcher()
        {
            string s = GetFolder();
            watcher.Created += fileSystemWatcher1_Created;
            watcher.Path = s;
            watcher.BeginInit();
            watcher.EnableRaisingEvents = true;
            watcher.EndInit();
        }
        static BioImage bi;
        static BioImage currentImage;
        static bool imaging = false;
        private static void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            //We wait till the new file is no longer written into.
            do
            {
                List<Process> pr = FileUtil.Locking(e.FullPath);
                FileInfo fi = new FileInfo(e.FullPath);
                if (pr.Count == 0 && fi.Length > 0)
                    break;
                fi = null;
            } while (true);
            currentImage = BioImage.OpenOME(e.FullPath,0,false,false,0,0,0,0);
            bi.Buffers.AddRange(currentImage.Buffers);
        }
        public static BioImage TakeImageStack(double UpperLimit, double LowerLimit, double interval)
        {
            bi = new BioImage(Properties.Settings.Default.ImageName);
            watcher.Path = GetFolder();
            Focus.SetFocus(UpperLimit);
            double d = Math.Abs(UpperLimit - LowerLimit);
            double dd = d / fInterVal;
            for (int i = 0; i < dd + 1; i++)
            {
                TakeImage(i);
                //If this is the last image we don't need to move the focus. 
                if (i < dd)
                    Focus.SetFocus(Focus.GetFocus() - fInterVal);
                Application.DoEvents();
            }
            bi.UpdateCoords(bi.Buffers.Count, 1, 1);
            bi.Volume = new VolumeD(new Point3D(bi.stageSizeX, bi.stageSizeY, bi.stageSizeZ), new Point3D(bi.physicalSizeX * bi.SizeX, bi.physicalSizeY * bi.SizeY, bi.physicalSizeZ * bi.SizeZ));
            if (bi.bitsPerPixel > 8)
                bi.StackThreshold(true);
            else
                bi.StackThreshold(false);
            Images.AddImage(bi);
            return bi;
        }
        public static void TakeTiles(int width, int height)
        {
            bool leftright = true;
            for (int y = 0; y < height; y++)
            {
                if (y != 0)
                    Microscope.MoveFieldDown();
                leftright = !leftright;
                Microscope.TakeImage();
                for (int x = 0; x < width; x++)
                {
                    if (leftright)
                        Microscope.MoveFieldRight();
                    else
                        Microscope.MoveFieldLeft();
                    Microscope.TakeImage();
                }
            }
        }
        public static void TakeTilesStack(int width, int height, double UpperLimit, double LowerLimit, double interval)
        {
            bool leftright = true;
            for (int y = 0; y < height; y++)
            {
                if (y != 0)
                    Microscope.MoveFieldDown();
                leftright = !leftright;
                Microscope.TakeImageStack(UpperLimit, LowerLimit, interval);
                for (int x = 0; x < width; x++)
                {
                    if(x != 0)
                    if (leftright)
                        Microscope.MoveFieldRight();
                    else
                        Microscope.MoveFieldLeft();
                    Microscope.TakeImageStack(UpperLimit, LowerLimit, interval);
                }
            }
        }
        public static RectangleD GetObjectiveViewRectangle()
        {
            Objectives.Objective o = Objectives.GetObjective();
            PointD d = Stage.GetPosition();
            return new RectangleD(d.X, d.Y, o.ViewWidth, o.ViewHeight);
        }
        public static RectangleD GetViewRectangle()
        {
            Objectives.Objective o = Objectives.GetObjective();
            PointD d = Stage.GetPosition();
            return new RectangleD(d.X, d.Y, viewSize.X, viewSize.Y);
        }
        public static void Close()
        {
            PMicroscope.Stop();
        }
    }

}
