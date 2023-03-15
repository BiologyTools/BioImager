using System;
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
    /* It's a wrapper for the stage*/
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
        /* Creating a new instance of the Stage class. */
        public Stage(object st)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                stageType = Microscope.Types["IMTBStage"];
                stage = st;
                axisType = Microscope.Types["IMTBAxis"];
                xAxis = Microscope.GetProperty("IMTBStage", "XAxis", Stage.stage);
                yAxis = Microscope.GetProperty("IMTBStage", "YAxis", Stage.stage);
                PointD d = GetPosition(true);
                x = d.X;
                y = d.Y;
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

        /// The function sets the position of the stage to the given coordinates
        /// 
        /// @param px x position in microns
        /// @param py y-coordinate
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
       
        /// This function sets the position of the object to the given x and y coordinates
        /// 
        /// @param px The new x position of the object
        public void SetPositionX(double px)
        {
            x = px;
            SetPosition(px, y);
        }
        
       /// This function sets the position of the object to the given x and y coordinates
       /// 
       /// @param py The y position of the object
        public void SetPositionY(double py)
        {
            y = py;
            SetPosition(x, py);
        }
        
        /// This function returns the X position of the mouse cursor
        /// 
        /// @param update If true, the position will be updated before returning the value.
        /// 
        /// @return The position of the object in the X axis.
        public double GetPositionX(bool update)
        {
            x = GetPosition(update).X;
            return x;
        }
        
        /// GetPositionY() returns the Y coordinate of the current position of the mouse cursor
        /// 
        /// @param update If true, the position will be updated before returning the value.
        /// 
        /// @return The position of the object in the Y axis.
        public double GetPositionY(bool update)
        {
            y = GetPosition(update).Y;
            return y;
        }
        
       /// If the microscope is a Prior, get the position from the Prior SDK. If the microscope is a
       /// MTB, get the position from the MTB SDK. If the microscope is a Piezosystem, get the position
       /// from the Piezosystem SDK
       /// 
       /// @param update true if the position should be updated from the microscope, false if the
       /// position should be returned from the cache.
       /// 
       /// @return A PointD object.
        public PointD GetPosition(bool update)
        {
            if (Properties.Settings.Default.PMicroscope)
            {
                PointD pd;
                Microscope.pMicroscope.GetPosition(out pd,false);
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
        
       /// Move the stage up by the specified  amount of microns
       /// 
       /// @param m The amount of pixels to move the object up by.
        public void MoveUp(double m)
        {
            double y = GetPositionY(true) - m;
            SetPositionY(y);
        }
       
        /// Move the stage down by the specified amount of microns
        /// 
        /// @param m The amount to move the object by.
        public void MoveDown(double m)
        {
            double y = GetPositionY(true) + m;
            SetPositionY(y);
        }
        
        /// Move the stage right by the specified amount of microns
        /// 
        /// @param m The amount to move the object by.
        public void MoveRight(double m)
        {
            double x = GetPositionX(true) + m;
            SetPositionX(x);
        }
        
        /// Move the stage left by the specified amount of microns
        /// 
        /// @param m The amount to move the object by.
        public void MoveLeft(double m)
        {
            double x = GetPositionX(true) - m;
            SetPositionX(x);
        }
        
        /// It gets the software limits of the microscope stage
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
       
        /// It sets the limits of the stage movement
        /// 
        /// @param xmin minimum x value
        /// @param xmax the maximum x value
        /// @param ymin -0.0025
        /// @param ymax -0.0015
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

    /* The Focus class is used to set and get the focus of the microscope */
    public class Focus
    {
        public static Type focusType;
        public static Type axisType;
        public static object focus;
        public static double upperLimit;
        public static double lowerLimit;
        private static double z;
        /* Creating a new instance of the Focus class. */
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

        /// Sets the z-axis based on microscope configuration.
        /// 
        /// @param f the focus value
        public void SetFocus(double f)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Focus.SetFocus(" + f + ");");
            if (Properties.Settings.Default.PMicroscope)
            {
                PointD p = Microscope.Stage.GetPosition(true);
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
            if (Automation.Properties.ContainsKey("SetFocus"))
            {
                Automation.SetProperty("SetFocus", f.ToString());
            }
        }

       /// This function returns the z-axis position
       /// 
       /// @return The z-axis position of the microscope.
        public double GetFocus()
        {
            return GetFocus(false);
        }

        /// If the microscope is a Prior, get the Z position from the SDK. If it's a MTB, get the Z
        /// position from the MTB SDK. If it's a PMicroscope, get the Z position from the PMicroscope
        /// SDK. If it's a custom microscope, get the Z position from the custom SDK
        /// 
        /// @param update boolean, if true, the focus is updated from the microscope
        /// 
        /// @return The z position of the microscope.
        public double GetFocus(bool update)
        {
            if (Properties.Settings.Default.PMicroscope)
            {
                Point3D p;
                Microscope.pMicroscope.GetPosition3D(out p, update);
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
            if(Automation.Properties.ContainsKey("GetFocus"))
            {
                return (double)Automation.GetProperty("GetFocus");
            }
            return z;
        }

        /// It gets the lower and upper limits of the focus axis
        /// 
        /// @return The return value is a PointD object.
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

        /// The function takes two double values, xd and yd, and sets the upper and lower limits of the
        /// focus axis
        /// 
        /// @param xd the upper limit of the focus
        /// @param yd the lower limit of the focus
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

    /* The Objectives class is a class that contains a list of objectives */
    public class Objectives
    {
        public List<Objective> List = new List<Objective>();
        public static Type changerType = null;
        public static object changer;

        /* Creating a list of objectives. */
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
       /* Creating a list of objectives. */
        public Objectives(int count)
        {
            for (int i = 0; i < count; i++)
            {
                List.Add(new Objective(null, i));
            }
        }
        /* The Objective class is a class that contains all the information about the objective */
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
        
        /// It waits for a second to give time for stage movements. 
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
        
        /// The function is called with an integer argument, and it sets the microscope objective to
        /// that integer
        /// 
        /// @param index the index of the objective to be set
        /// 
        /// @return The return value is a boolean.
        public void SetPosition(int index)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.Objectives.SetObjective(" + index + ");");
            this.index = index;
            if (!Properties.Settings.Default.LibPath.Contains("MTB") && !Properties.Settings.Default.LibPath.Contains("Prio") && Function.Functions.ContainsKey("SetO" + index))
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
            else if (Properties.Settings.Default.LibPath.Contains("Prio"))
            {
                return Microscope.sdk.GetNosePiece();
            }
            else if(!Properties.Settings.Default.PMicroscope && Automation.Properties.ContainsKey("GetO1"))
            {
                if (Function.Functions.ContainsKey("GetO7"))
                {
                    bool b;
                    //We check each objective button state to determine which objective we currently are in.
                    b = (bool)Automation.GetProperty("Get01");
                    if (b)
                        return 0;
                    b = (bool)Automation.GetProperty("Get02");
                    if (b)
                        return 1;
                    b = (bool)Automation.GetProperty("Get03");
                    if (b)
                        return 2;
                    b = (bool)Automation.GetProperty("Get04");
                    if (b)
                        return 3;
                    b = (bool)Automation.GetProperty("Get05");
                    if (b)
                        return 4;
                    b = (bool)Automation.GetProperty("Get06");
                    if (b)
                        return 5;
                    b = (bool)Automation.GetProperty("Get07");
                    if (b)
                        return 6;
                }
                else
                {
                    bool b;
                    //We check each objective button state to determine which objective we currently are in.
                    b = (bool)Automation.GetProperty("Get01");
                    if (b)
                        return 0;
                    b = (bool)Automation.GetProperty("Get02");
                    if (b)
                        return 1;
                    b = (bool)Automation.GetProperty("Get03");
                    if (b)
                        return 2;
                    b = (bool)Automation.GetProperty("Get04");
                    if (b)
                        return 3;
                    b = (bool)Automation.GetProperty("Get05");
                    if (b)
                        return 4;
                    b = (bool)Automation.GetProperty("Get06");
                    if (b)
                        return 5;
                }
            }
            return 0;
        }
        /// If the library path contains "MTB", then return the list at the position of the
        /// changerType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, changer, null)
        /// 
        /// If the library path does not contain "MTB", then return the list at the index
        /// 
        /// @return The Objective object.
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

   /* The TLShutter class is a wrapper class for the shutter object */
    public class TLShutter
    {
        public static Type tlType;
        public static object tlShutter = null;
        public static int position;
        /* Creating a new instance of the TLShutter class. */
        public TLShutter(object tlShut)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                tlShutter = tlShut;
                tlType = Microscope.Types["IMTBChanger"];
            }
        }
        
        /// If the library path contains "MTB", then invoke the get_Position method on the tlShutter
        /// object. Otherwise, return the position variable
        /// 
        /// @return The position of the shutter.
        public short GetPosition()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
                return (short)tlType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, tlShutter, null);
            else
            return (short)position;
        }
        
       /// The function takes an integer as an argument and sets the position of the shutter to the
       /// value of the integer. 
       /// 
       /// The function is called by the following line of code:
       /// 
       /// @param p 0 or 1
        public void SetPosition(int p)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.TLShutter.SetPosition(" + p + ");");
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[2];
                args[0] = (short)p;
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
   /* The HXPShutter class is a wrapper class for the TLShutter class */
    public class HXPShutter
    {
        public static Type tlType;
        public static object tlShutter = null;
        public static int position;
        /* Creating a new instance of the TLShutter class. */
        public HXPShutter(object tlShut)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                tlShutter = tlShut;
                tlType = Microscope.Types["IMTBChanger"];
            }
        }

        /// If the library path contains "MTB", then invoke the get_Position method on the tlShutter
        /// object. Otherwise, return the position variable
        /// 
        /// @return The position of the shutter.
        public short GetPosition()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
                return (short)tlType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, tlShutter, null);
            else
                return (short)position;
        }

        /// The function takes an integer as an argument and sets the position of the shutter to the
        /// value of the integer. 
        /// 
        /// The function is called by the following line of code:
        /// 
        /// @param p 0 or 1
        public void SetPosition(int p)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.TLShutter.SetPosition(" + p + ");");
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[2];
                args[0] = (short)p;
                args[1] = Activator.CreateInstance(Microscope.Types["MTBCmdSetModes"]);
                bool res = (bool)tlType.InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, tlShutter, args);
                position = p;
            }
        }
    }

    /* The RLShutter class is a wrapper class for the IMTBChanger interface */
    public class RLShutter
    {
        public static Type rlType;
        public static object rlShutter = null;
        public static int position;
        
        /* Creating a new instance of the RLShutter class. */
        public RLShutter(object rlShut)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                rlShutter = rlShut;
                rlType = Microscope.Types["IMTBChanger"];
            }

        }
        
        /// If the library path contains "MTB", then return the position of the shutter, otherwise
        /// return the position of the shutter
        /// 
        /// @return The position of the shutter.
        public short GetPosition()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
                return (short)rlType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, rlShutter, null);
            else
                return (short)position;
        }
       
        /// The function takes an integer as an argument, and if the microscope is an MTB, it sets the
        /// position of the RLShutter to the value of the integer
        /// 
        /// @param p
        public void SetPosition(int p)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[2];
                args[0] = (short)p;
                args[1] = Activator.CreateInstance(Microscope.Types["MTBCmdSetModes"]);
                bool res = (bool)rlType.InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, rlShutter, args);
            }
            position = p;
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.RLShutter.SetPosition(" + p + ");");
        }
    }
    /* The LightSource class is a wrapper class for the IMTBContinual interface. */
    public class LightSource
    {
        public Type continualType;
        public object light = null;
        public double position;
        string name;
        public LightSource()
        {
        }
        /* Creating a new instance of the LightSource class. */
        public LightSource(object tlShut,string name)
        {
            this.name = name;
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                light = tlShut;
                continualType = Microscope.Types["IMTBContinual"];
            }
        }
        /// If the microscope is a MTB, then get the position of the light source.
        /// 
        /// @return The position of the light source.
        public double GetPosition()
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                if (light == null)
                    return -1;
                object[] getPosFocArgs = new object[1];
                getPosFocArgs[0] = "%";
                object o = Microscope.Types["IMTBContinual"].InvokeMember("GetPosition", BindingFlags.InvokeMethod, null, light, getPosFocArgs);
                return (double)o;
            }
            return position;
        }
        /// The function takes a double as an argument and then calls the SetPosition function of the
        /// light source object
        /// 
        /// @param f the position of the light source
        public void SetPosition(double f)
        {
            if (Recorder.recordMicroscope)
                Recorder.AddLine("Microscope.LightSource.SetPosition(" + f + ");");
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[3];
                args[0] = f;
                args[1] = "%";
                args[2] = Microscope.CmdSetMode;
                bool resFoc = (bool)Microscope.Types["IMTBContinual"].InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, light, args);
            }
        }
        public override string ToString()
        {
            return name;
        }
    }
    /* The class is a wrapper for the filter wheel. It has two methods, one to get the current position
    of the filter wheel and one to set the position of the filter wheel */
    public class FilterWheel
    {
        static Type filterType;
        static object filterWheel = null;
        static int position;
        public FilterWheel() { }
        public FilterWheel(object o) 
        {
            filterWheel = o;
            filterType = Microscope.Types["IMTBChanger"];
        }
        /// Get the current position of the filter wheel
        /// 
        /// @return The position of the filter wheel.
        public int GetPosition()
        {
            if(Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                return (int)filterType.InvokeMember("get_Position", BindingFlags.InvokeMethod, null, filterWheel, null);
            }
            if (Properties.Settings.Default.PMicroscope)
            {
                int pos;
                Microscope.pMicroscope.GetFilterWheelPosition(out pos);
                return pos;
            }
            return -1;
        }
        
        /// The function takes an integer as an argument and sets the filter wheel position to that
        /// integer
        /// 
        /// @param i The position to set the filter wheel to.
        public void SetPosition(int i)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                object[] args = new object[2];
                args[0] = (short)i;
                args[1] = Activator.CreateInstance(Microscope.Types["MTBCmdSetModes"]);
                bool res = (bool)filterType.InvokeMember("SetPosition", BindingFlags.InvokeMethod, null, filterWheel, args);
                return;
            }
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
        /* Defining an enum. */
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
        public static HXPShutter HXPShutter = null;
        public static LightSource TLHalogen = null;
        public static LightSource RLHalogen = null;
        public static LightSource HXP = null;
        public static FilterWheel TLFilterWheel = null;
        public static FilterWheel RLFilterWheel = null;
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
        /* A property that returns the value of the GetObjective method. */
        public static Objectives.Objective Objective
        {
            get
            {
                return Objectives.GetObjective();
            }
        }
        
        /// The function initializes the microscope and the imaging library
        /// 
        /// @return The return value is an object.
        public static void Initialize()
        {
            if (initialized)
                return;
            int err;
            folder = Properties.Settings.Default.ImagingPath;
            if (Properties.Settings.Default.PMicroscope)
            {
                pMicroscope = new PMicroscope();
                pMicroscope.Initialize();
                bool b = pMicroscope.SetPosition(new Point3D(100, 100, 100));
                Point3D p;
                bool res = pMicroscope.GetPosition3D(out p,false);
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
                st[0] = "MTBTLHalogenLamp";
                var tllamp = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBRLHalogenLamp";
                var rllamp = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBHXPLamp";
                var hxp = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBHXP120Shutter";
                var hxpShut = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBTLFilterChanger1";
                var tlfilter = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                st[0] = "MTBRLFilterChanger1";
                var rlfilter = r.InvokeMember("GetComponent", BindingFlags.InvokeMethod, null, root, st);
                TLFilterWheel = new FilterWheel(tlfilter);
                RLFilterWheel = new FilterWheel(rlfilter);
                Stage = new Stage(sta);
                Focus = new Focus(foc);
                Objectives = new Objectives(objs);
                TLShutter = new TLShutter(tlShutter);
                RLShutter = new RLShutter(rlShutter);
                TLHalogen = new LightSource(tllamp,"TL Halogen");
                RLHalogen = new LightSource(rllamp,"RL Halogen");
                HXP = new LightSource(hxp,"HXP");
                HXPShutter = new HXPShutter(hxpShut);
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

        /// It invokes a method on an object
        /// 
        /// @param Type The type of the object you want to invoke the method on.
        /// @param name The name of the method to invoke.
        /// @param o The object to invoke the method on.
        /// @param args The arguments to pass to the method. This array of arguments must match in number,
        /// order, and type the parameters of the method to be invoked. If there are no parameters, args must be
        /// null.
        /// 
        /// @return The return value of the method.
        public static object Invoke(Type type, string name, object o, object[] args)
        {
            return type.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }

        /// It takes a type name, a method name, an object, and an array of arguments, and returns the result of
        /// invoking the method on the object with the arguments
        /// 
        /// @param type The type of the object you want to invoke the method on.
        /// @param name The name of the method to invoke.
        /// @param o The object to invoke the method on.
        /// @param args The arguments to pass to the method. This array of arguments must match in number,
        /// order, and type the parameters of the method to be invoked. If there are no parameters, args must be
        /// null.
        /// 
        /// @return The return value of the method.
        public static object Invoke(string type, string name, object o, object[] args)
        {
            Type t = Types[type];
            return t.InvokeMember(name, BindingFlags.InvokeMethod, null, o, args);
        }

        /// > Get the property value of the object of the given type and name
        /// 
        /// @param type The type of the object you want to get the property from.
        /// @param name The name of the property you want to get the value of.
        /// @param obj The object you want to get the property from
        /// 
        /// @return The value of the property.
        public static object GetProperty(string type, string name, object obj)
        {
            Type myType = Types[type];
            PropertyInfo p = myType.GetProperty(name);
            return p.GetValue(obj);
        }

        /// It takes a string type and a string name and returns an object that is the enum value of the
        /// type with the name
        /// 
        /// @param type The type of the enum you want to get the value from.
        /// @param name The name of the enum
        /// 
        /// @return The enum value.
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

        /// It checks to see if the stage and focus are calibrated on the lower limit and if not, it
        /// calibrates them
        /// 
        /// @param calibMode "OnLowerLimit"
        public static void CalibrateXYZ(string calibMode)
        {
            if (Properties.Settings.Default.LibPath.Contains("MTB"))
            {
                //We check to see if focus & stage are correctly calibrated on lower limit and perform calibration if necessary
                PointD cur = Stage.GetPosition(true);
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

        
        /// Get the current stage position and focus position, and return a Point3D object containing
        /// the stage position and focus position
        /// 
        /// @return A Point3D object.
        public static Point3D GetPosition()
        {
            PointD p = Stage.GetPosition(false);
            double f = Focus.GetFocus();
            return new Point3D(p.X, p.Y, f);
        }

       
        /// It sets the position of the stage and focus to the values in the Point3D object
        /// 
        /// @param Point3D a class that contains 3 doubles, X, Y, and Z.
        public static void SetPosition(Point3D p)
        {
            Stage.SetPosition(p.X,p.Y);
            Focus.SetFocus(p.Z);
            Microscope.redraw = true;
        }

       /// SetPosition(PointD p) sets the position of the stage to the PointD p
       /// 
       /// @param PointD a class that contains an X and Y coordinate
        public static void SetPosition(PointD p)
        {
            Stage.SetPosition(p.X, p.Y);
            Microscope.redraw = true;
        }
        /// If the shutter is closed, open it
        public static void OpenRL()
        {
            //If shutter is closed we open it.
            if (RLShutter.GetPosition() == 0)
                RLShutter.SetPosition(1);
        }

        
       /// If the shutter is closed, open it
        public static void OpenTL()
        {
            //If shutter is closed we open it.
            if (TLShutter.GetPosition() == 0)
                TLShutter.SetPosition(1);
        }

        /// If the shutter is open, then close it
        public static void CloseRL()
        {
            //If shutter is open then we close it.
            if (RLShutter.GetPosition() == 0)
                RLShutter.SetPosition(1);
        }

        /// If the shutter is open, then we close it
        public static void CloseTL()
        {
            //If shutter is open then we close it.
            if (TLShutter.GetPosition() == 0)
                TLShutter.SetPosition(1);
        }

        /// This function sets the position of the TL shutter to the value of the variable tl
        /// 
        /// @param tl the position of the shutter
        public static void SetTL(uint tl)
        {
            TLShutter.SetPosition((short)tl);
        }

       /// Set the position of the RLShutter to the value of tr
       /// 
       /// @param tr The position of the shutter.
        public static void SetRL(uint tr)
        {
            RLShutter.SetPosition((short)tr);
        }

        /// It returns the position of the TLShutter object
        /// 
        /// @return The position of the shutter.
        public static int GetTL()
        {
            return TLShutter.GetPosition();
        }

        /// GetRL() returns the position of the RLShutter object.
        /// 
        /// @return The position of the RLShutter.
        public static int GetRL()
        {
            return RLShutter.GetPosition();
        }

        /// Move the stage up by a distance of d
        /// 
        /// @param d The distance to move the stage up.
        public static void MoveUp(double d)
        {
            Stage.MoveUp(d);
        }

        /// MoveRight(double d) moves the stage right by d
        /// 
        /// @param d The distance to move the stage in millimeters.
        public static void MoveRight(double d)
        {
            Stage.MoveRight(d);
        }

        /// Move the stage down by the specified amount
        /// 
        /// @param d The distance to move the stage down.
        public static void MoveDown(double d)
        {
            Stage.MoveDown(d);
        }

        /// Move the stage left by the specified distance
        /// 
        /// @param d The distance to move the stage in mm.
        public static void MoveLeft(double d)
        {
            Stage.MoveLeft(d);
        }
        /// Move the field up by the height of the view
        public static void MoveFieldUp()
        {
            Stage.MoveUp(viewSize.Y);
        }
        /// Move the field right by the width of the view
        public static void MoveFieldRight()
        {
            Stage.MoveRight(viewSize.X);
        }
        /// Move the field down by one unit
        public static void MoveFieldDown()
        {
            Stage.MoveDown(viewSize.Y);
        }
       /// Move the field left by the width of the viewport
        public static void MoveFieldLeft()
        {
            Stage.MoveLeft(viewSize.X);
        }
        /// > The function `SetFocus` is a static function that takes a double as a parameter and calls the
        /// static function `SetFocus` on the class `Focus` with the parameter `d`
        /// 
        /// @param d The double value to set the focus to.
        public static void SetFocus(double d)
        {
            Focus.SetFocus(d);
        }

       /// It returns the current focus of the z-axis.
       /// 
       /// @return The focus of the z-axis.
        public static double GetFocus()
        {
            return Focus.GetFocus();
        }
        private static string folder = "";
        /// If the folder is not set, prompt the user to select a folder. 
        /// If the user selects a folder, set the folder to the selected folder. 
        /// If the user does not select a folder, return null. 
        /// If the folder is set, return the folder.
        /// 
        /// @return The folder path.
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
                    Properties.Settings.Default.ImagingPath = fs.SelectedPath;
                    return fs.SelectedPath;
                }
            }
            else
            {
                folder = Properties.Settings.Default.ImagingPath;
                return folder;
            }
        }
        /// If the folder doesn't exist, create it. If the property exists, set it. Set the folder
        /// variable to the folder. Set the ImagingPath property to the folder. Save the settings
        /// 
        /// @param fol the folder to save the images to
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
        /// It takes an image from the camera and saves it to a file
        /// 
        /// @return The image is being returned.
        public static void TakeImage()
        {
            if (Properties.Settings.Default.SimulateCamera && !Properties.Settings.Default.PMicroscope)
            {
                folder = GetFolder();
                BioImage b = MicroscopeSetup.simImage.Copy();
                Point3D p = GetPosition();
                b.Volume.Location = p;
                b.stageSizeX = p.X;
                b.stageSizeY = p.Y;
                b.stageSizeZ = p.Z;
                string file;
                if (folder == "" || folder == null)
                    file = Properties.Settings.Default.ImageName + (ImageCount++) + ".ome.tif";
                else
                    file = folder + "/" + Properties.Settings.Default.ImageName + (ImageCount++) + ".ome.tif";
                b.ID = file;
                b.file = file;
                Images.AddImage(b);
                BioImage.SaveOME(file, b.ID);
                Images.RemoveImage(b);
            }
            else if (Properties.Settings.Default.PMicroscope)
            {
                folder = GetFolder();
                string file;
                if (folder == "" || folder == null)
                    file = Properties.Settings.Default.ImageName + (ImageCount++).ToString();
                else
                    file = folder + "/" + Properties.Settings.Default.ImageName + (ImageCount++);
                pMicroscope.TakeImage(file);
                byte[] bts = File.ReadAllBytes(file);
                int w, h;
                System.Drawing.Imaging.PixelFormat px;
                Enum.TryParse<System.Drawing.Imaging.PixelFormat>(Properties.Settings.Default.PCameraFormat, out px);
                pMicroscope.GetSize(out w, out h);
                BufferInfo bf = new BufferInfo(file, w, h, px, bts, new ZCT(0, 0, 0), 0);
                Statistics.CalcStatistics(bf);
                BioImage bm = new BioImage(file + ".ome.tif");
                bm.Buffers.Add(bf);
                //Set the physical size based on objective view
                RectangleD rec = GetViewRectangle(false);
                bm.physicalSizeX = rec.W / bm.SizeX;
                bm.physicalSizeY = rec.H / bm.SizeY;
                bm.physicalSizeZ = 1;
                double f = Focus.GetFocus();
                bm.bitsPerPixel = bf.BitsPerPixel;
                bm.stageSizeX = rec.X;
                bm.stageSizeY = rec.Y;
                bm.stageSizeZ = f;
                bm.UpdateCoords(1, 1, 1);
                bm.Volume = new VolumeD(new Point3D(bm.stageSizeX, bm.stageSizeY, bm.stageSizeZ), new Point3D(bm.physicalSizeX * bm.SizeX, bm.physicalSizeY * bm.SizeY, bm.physicalSizeZ * bm.SizeZ));
                for (int c = 0; c < bf.RGBChannelsCount; c++)
                {
                    bm.Channels.Add(new Channel(c, bf.BitsPerPixel, bf.RGBChannelsCount));
                }
                //We wait for threshold image statistics calculation
                do
                {
                    Thread.Sleep(50);
                } while (bm.Buffers[bm.Buffers.Count - 1].Stats == null);
                Statistics.ClearCalcBuffer();
                BioImage.AutoThreshold(bm, false);
                if (!imagingStack)
                {   
                    Images.AddImage(bm);
                    BioImage.SaveOME(file + ".ome.tif", bm.ID);
                    if (bm.bitsPerPixel > 8)
                        bm.StackThreshold(true);
                    else
                        bm.StackThreshold(false);
                    App.viewer.AddImage(bm);
                }
                else
                    bi.Buffers.Add(bm.Buffers[0]);
                //We delete the temporary file created by camera.
                File.Delete(file);
                currentImage = bm;
            }
            else
            {
                if (Function.Functions.ContainsKey("TakeImage"))
                {
                    App.imager.PerformFunction(Function.Functions["TakeImage"]);
                }
                else
                {
                    MessageBox.Show("Go to Microscope Setup to set 'TakeImage' function eg. a Button shortcut or GUI recording. Or set camera simulation on & set Camera image.", "Function 'TakeImage' not defined.");
                    return;
                }
            }
        }
        /// This function takes a stack of images from the camera and returns a BioImage object
        /// 
        /// @return A BioImage object.
        public static BioImage TakeImageStack()
        {
            return TakeImageStack(UpperLimit, LowerLimit, fInterVal);
        }
        static FileSystemWatcher watcher = new FileSystemWatcher();
       /// This function is called when the user clicks the "Start" button. It gets the folder path from
       /// the textbox, sets the watcher's path to that folder, and enables the watcher.
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
        static bool imagingStack = false;
        static List<BioImage> images = new List<BioImage>();
        static Dictionary<int,string> locked = new Dictionary<int, string>();
        /// We wait till the new file is no longer written into
        static void LockWait()
        {
            do
            {
                Thread.Sleep(500);
                for (int i = 0; i < locked.Count; i++)
                {
                    //We wait till the new file is no longer written into.
                    do
                    {
                        List<Process> pr = FileUtil.Locking(locked[i]);
                        FileInfo fi = new FileInfo(locked[i]);
                        if (pr.Count == 0 && fi.Length > 0)
                            break;
                        fi = null;
                    } while (true);
                    currentImage = BioImage.OpenOME(locked[i], 0, false, false, 0, 0, 0, 0);
                    if (imagingStack)
                        bi.Buffers.AddRange(currentImage.Buffers);
                    else
                        images.Add(currentImage);
                }
            } while (imagingStack);
        }
        /// It creates a new thread and starts it. The thread will call the LockWait function
        private static void WaitLocking()
        {
            System.Threading.Thread t = new Thread(LockWait);
            t.Start();
        }
        /// If the program is not in the microscope mode, then we will add the file to the locked list
        /// 
        /// @param sender The object that raised the event.
        /// @param FileSystemEventArgs 
        /// 
        /// @return The file path of the image that was just created.
        private static void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            if (!Properties.Settings.Default.PMicroscope)
            {
                //We will only open files with an extension
                if (!Path.HasExtension(e.FullPath))
                    return;
                locked.Add(ImageCount-1,e.FullPath);
            }
        }
        /// It takes an image stack from the microscope and adds it to the viewer. 
        /// 
        /// The function is called from the main thread and it's purpose is to take an image stack from
        /// the microscope and add it to the viewer. 
        /// 
        /// The function is called from the main thread and it's purpose is to take an image stack from
        /// the microscope and add it to the viewer. 
        /// 
        /// The function is called from the main thread and it's purpose is to take an image stack from
        /// the microscope and add it to the viewer. 
        /// 
        /// The function is called from the main thread and it's purpose is to take an image stack from
        /// the microscope and add it to the viewer. 
        /// 
        /// The function is called from the main thread and it's purpose is to take an image stack from
        /// the microscope and add it to the viewer. 
        /// 
        /// The function is called from the main thread and it's purpose is to take an image stack from
        /// the microscope and add it
        /// 
        /// @param UpperLimit The upper limit of the stack
        /// @param LowerLimit The lowest focus position
        /// @param interval The interval between each image.
        /// 
        /// @return A BioImage object.
        public static BioImage TakeImageStack(double UpperLimit, double LowerLimit, double interval)
        {
            imagingStack = true;
            //We start the locking wait loop
            WaitLocking();
            bi = new BioImage(Properties.Settings.Default.ImageName);
            watcher.Path = GetFolder();
            Focus.SetFocus(UpperLimit);
            double d = UpperLimit - LowerLimit;
            double dd = d / interval;
            for (int i = 0; i < dd; i++)
            {
                TakeImage();
                //If this is the last image we don't need to move the focus. 
                if (i < dd)
                    Focus.SetFocus(Focus.GetFocus() - fInterVal);
                Application.DoEvents();
            }
            //If we are using the imaging application to take images we need to add them to the viewer as the filesystemwatcher
            //can't add them due to thread access violation
            if (!Properties.Settings.Default.PMicroscope)
            {
                //First we wait till all the images are loaded
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                } while (bi.Buffers.Count < (int)dd);
            }
            
            bi.UpdateCoords(bi.Buffers.Count, 1, 1);
            for (int c = 0; c < bi.Buffers[0].RGBChannelsCount; c++)
            {
                bi.Channels.Add(new Channel(c, bi.bitsPerPixel, bi.Buffers[c].RGBChannelsCount));
            }
            //Set the physical size based on objective view
            RectangleD rec = GetObjectiveViewRectangle();
            bi.physicalSizeX = rec.W / bi.SizeX;
            bi.physicalSizeY = rec.H / bi.SizeY;
            bi.physicalSizeZ = (UpperLimit - LowerLimit) / bi.SizeZ;
            bi.bitsPerPixel = bi.Buffers[0].BitsPerPixel;
            bi.stageSizeX = rec.X;
            bi.stageSizeY = rec.Y;
            bi.stageSizeZ = UpperLimit;
            bi.Volume = new VolumeD(new Point3D(bi.stageSizeX, bi.stageSizeY, bi.stageSizeZ), new Point3D(bi.physicalSizeX * bi.SizeX, bi.physicalSizeY * bi.SizeY, bi.physicalSizeZ * bi.SizeZ));
            BioImage.AutoThreshold(bi, false);
            if (bi.bitsPerPixel > 8)
                bi.StackThreshold(true);
            else
                bi.StackThreshold(false);
            Images.AddImage(bi);
            imagingStack = false;
            currentImage = bi;
            images.Clear();
            App.viewer.AddImage(currentImage);
            locked.Clear();
            ImageCount = 0;
            return bi;
        }
        /// It takes a number of images in a grid pattern
        /// 
        /// @param width The number of tiles in the x direction
        /// @param height The number of fields to take in the y direction
        public static void TakeTiles(int width, int height)
        {
            bool leftright = true;
            if (Properties.Settings.Default.PMicroscope)
            {
                Point3D p = Microscope.GetPosition();
                //We ensure that the cached position is same as actual position
                Microscope.SetPosition(p);
            }
            for (int y = 0; y < height; y++)
            {
                if (y != 0)
                {
                    Microscope.MoveFieldDown();
                }
                leftright = !leftright;
                Microscope.TakeImage();
                for (int x = 0; x < width - 1; x++)
                {
                    if (leftright)
                    {
                        Microscope.MoveFieldRight();
                    }
                    else
                    {
                        Microscope.MoveFieldLeft();
                    }
                    Microscope.TakeImage();
                }
            }
            //If we are using the imaging application to take images we need to add them to the viewer as the filesystemwatcher
            //can't add them due to thread access violation
            if(!Properties.Settings.Default.PMicroscope)
            {
                //First we wait till all the images are loaded
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                } while (images.Count < width * height);
                for (int i = 0; i < images.Count; i++)
                {
                    App.viewer.AddImage(images[i]);
                }
                images.Clear();
            }
        }
        /// It returns a rectangle that represents the viewable area of the current objective
        /// 
        /// @return A rectangle with the dimensions of the viewport.
        public static RectangleD GetObjectiveViewRectangle()
        {
            Objectives.Objective o = Objectives.GetObjective();
            PointD d = Stage.GetPosition(false);
            return new RectangleD(d.X, d.Y, o.ViewWidth, o.ViewHeight);
        }
        /// If the microscope is in PMicroscope mode, return the PMicroscope rectangle, otherwise return
        /// the stage rectangle
        /// 
        /// @param update Boolean value that determines whether the view rectangle should be updated or
        /// not.
        /// 
        /// @return A rectangle with the dimensions of the viewport.
        public static RectangleD GetViewRectangle(bool update)
        {
            Objectives.Objective o = Objectives.GetObjective();
            if (update)
            {
                PointD d = Stage.GetPosition(false);
                return new RectangleD(d.X, d.Y, viewSize.X, viewSize.Y);
            }
            else
            {
                if(Properties.Settings.Default.PMicroscope)
                {
                    return new RectangleD(PMicroscope.location.X, PMicroscope.location.Y, viewSize.X, viewSize.Y);
                }
                else
                {
                    PointD d = Stage.GetPosition(false);
                    return new RectangleD(d.X, d.Y, viewSize.X, viewSize.Y);
                }
            }
        }
        public static void Close()
        {
            PMicroscope.Stop();
        }
    }

}
