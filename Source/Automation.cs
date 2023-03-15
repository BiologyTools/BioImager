using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using Gma.System.MouseKeyHook;
using System.Threading;
using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;
using System.Drawing;

namespace Bio
{
    public class Automation
    {
        public static InputSimulator input;
        public static Dictionary<string, Recording> Recordings = new Dictionary<string, Recording>();
        public static Dictionary<string, Recording> Properties = new Dictionary<string, Recording>();
        /* It's a class that represents an action that can be performed on an automation element */
        public class Action
        {
            private KeyEventArgs key;
            private KeyPressEventArgs keyPress;
            private string automationID;
            private string name;
            private string className;
            private string process;
            private string title;
            private int index;
            private System.Drawing.Point point;
            private long ticks;
            private MouseEventArgs mouse;
            private MouseButtons mouseButton;
            private ValueType autoValue;
            public enum Type
            {
                mousedown,
                mouseup,
                mousemove,
                wheeldown,
                wheelup,
                keyup,
                keydown,
                keypress,
            }
            public enum ValueType
            {
                Name,
                ValuePattern,
                SelectionPattern,
                TextPattern,
                TogglePattern,
                Image,
            }

            private Type type;
            public Type ActionType
            {
                get { return type; }
                set { type = value; }
            }
            public Keys Key
            {
                get
                {
                    if (key != null)
                        return key.KeyCode;
                    else
                    if (keyPress != null)
                        return (Keys)keyPress.KeyChar;
                    else
                        return Keys.None;
                }
            }
            public MouseButtons Button
            {
                get { return mouseButton; }
                set { mouseButton = value; }
            }
            public System.Drawing.Point Point
            {
                get { return point; }
                set { point = value; }
            }
            public string AutomationID
            {
                get { return automationID; }
                set { automationID = value; }
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string ClassName
            {
                get { return className; }
                set { className = value; }
            }
            public string ProcessName
            {
                get { return process; }
                set { process = value; }
            }
            public string Title
            {
                get { return title; }
                set { title = value; }
            }
            public int Index
            {
                get { return index; }
                set { index = value; }
            }
            public ValueType Value
            {
                get { return autoValue; }
                set { autoValue = value; }
            }
            public Action(Type t, int proc, KeyEventArgs e)
            {
                type = t;
                key = e;
                AutomationElement el = Element;
                name = el.Current.Name;
                automationID = el.Current.AutomationId;
                className = el.Current.ClassName;
                Process p = Process.GetProcessById(proc);
                ProcessName = p.ProcessName;
                title = Win32.GetActiveWindowTitle();
            }
            public Action(Type t, int proc, KeyPressEventArgs e)
            {
                type = t;
                keyPress = e;
                AutomationElement el = Element;
                name = el.Current.Name;
                automationID = el.Current.AutomationId;
                className = el.Current.ClassName;
                Process p = Process.GetProcessById(proc);
                ProcessName = p.ProcessName;
                title = Win32.GetActiveWindowTitle();
            }
            public Action(Type t, int proc, MouseEventArgs e)
            {
                type = t;
                mouse = e;
                AutomationElement el = Element;
                name = el.Current.Name;
                automationID = el.Current.AutomationId;
                className = el.Current.ClassName;
                mouseButton = e.Button;
                point = new Point((int)(e.Location.X - el.Current.BoundingRectangle.X), (int)(e.Location.Y - el.Current.BoundingRectangle.Y));
                Process p = Process.GetProcessById(proc);
                title = p.MainWindowTitle;
                ProcessName = p.ProcessName;
                Index = AutomationHelpers.GetIndex(ProcessName, Title, AutomationID, Name, ClassName, e.Location);
            }
            public Action(Type t, string process, string title, int index, string id, string name, string classname, MouseEventArgs mo)
            {
                type = t;
                this.automationID = id;
                this.process = process;
                this.name = name;
                this.className = classname;
                this.mouseButton = mo.Button;
                this.title = title;
                this.index = index;
                type = t;
                point = mo.Location;
                mouse = mo;
            }
            public Action(Type t, string process, string title, int index, string id, string name, string classname, KeyEventArgs ke)
            {
                type = t;
                key = ke;
                this.automationID = id;
                this.name = name;
                this.process = process;
                this.className = classname;
                this.index = index;
                this.title = title;
                type = t;
            }
            public Action(Type t, string process, string title, int index, string id, string name, string classname, KeyPressEventArgs ke)
            {
                type = t;
                keyPress = ke;
                this.automationID = id;
                this.process = process;
                this.name = name;
                this.className = classname;
                this.index = index;
                this.title = title;
                type = t;
            }
            public Action(Type t, string process, string title, int index, string id, string name, string classname)
            {
                this.automationID = id;
                this.name = name;
                this.className = classname;
                this.automationID = id;
                this.process = process;
                this.name = name;
                this.className = classname;
                this.index = index;
                this.title = title;
                type = t;
            }
            public void Perform()
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                AutomationElement el = AutomationHelpers.GetElementByProcess(ProcessName, Title, AutomationID, Name, ClassName, Index);
                if (type == Type.mousedown)
                {
                    Cursor.Position = new Point((int)el.Current.BoundingRectangle.X + Point.X, (int)el.Current.BoundingRectangle.Y + Point.Y);
                    if (mouse.Button == MouseButtons.Left)
                        MouseOperations.LeftClick();

                    if (mouse.Button == MouseButtons.Right)
                        MouseOperations.RightClick();
                }
                else if (type == Type.mouseup)
                {
                    Cursor.Position = new Point((int)el.Current.BoundingRectangle.X + Point.X, (int)el.Current.BoundingRectangle.Y + Point.Y);
                    if (mouse.Button == MouseButtons.Left)
                        MouseOperations.LeftClick();
                    if (mouse.Button == MouseButtons.Right)
                        MouseOperations.RightClick();
                }
                else if (type == Type.keydown)
                {
                    Win32.SetFocus(ProcessName);
                    input.Keyboard.KeyDown((VirtualKeyCode)key.KeyValue);
                }
                else if (type == Type.keyup)
                {
                    Win32.SetFocus(ProcessName);
                    input.Keyboard.KeyUp((VirtualKeyCode)key.KeyValue);
                }
                else if (type == Type.keypress)
                {
                    Win32.SetFocus(ProcessName);
                    SendKeys.Send(keyPress.KeyChar.ToString());
                }
                else if (type == Type.wheelup)
                {
                    Win32.SetFocus(ProcessName);
                    //input.Mouse.VerticalScroll(1);
                    Win32.MouseWheelUp();
                }
                else if (type == Type.wheeldown)
                {
                    Win32.SetFocus(ProcessName);
                    //input.Mouse.VerticalScroll(-1);
                    Win32.MouseWheelDown();
                }
                stopwatch.Stop();
                ticks = stopwatch.ElapsedMilliseconds;
                stopwatch = null;
            }
            public override string ToString()
            {
                if(type == Type.keyup || type == Type.keydown)
                    return Name + ", " + AutomationID + ", " + ClassName + ", " + ActionType + ", " + Index + ", " + key.KeyValue;
                else
                if (type == Type.keypress)
                    return Name + ", " + AutomationID + ", " + ClassName + ", " + ActionType + ", " + Index + ", " + keyPress.KeyChar;
                else
                if (type == Type.mouseup || type == Type.mousedown)
                    return Name + ", " + AutomationID + ", " + ClassName + ", " + ActionType + ", " + Index + ", " + point.ToString();
                else
                    return Name + ", " + AutomationID + ", " + ClassName + ", " + ActionType + ", " + Index + ", " + mouse.Delta;
            }
        }
        /* It's a class that represents a recording of a series of actions that can be performed on a
        UI element. */
        public class Recording
        {
            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            private string file;
            public string File
            {
                get { return file; }
                set { file = value; }
            }
            private List<Action> list = new List<Action>();
            public List<Action> List
            {
                get { return list; }
                set { list = value; }
            }
            public Recording()
            {
            }
            public Recording(string fil,List<Action> l)
            {
                list = l;
                file = fil;
                Name = System.IO.Path.GetFileNameWithoutExtension(fil);
            }        
            public void Run()
            {
                foreach (Action a in list)
                {
                    a.Perform();
                }
            }
            public object Get()
            {
                if(list.Count > 1)
                for (int i = 0; i < list.Count; i++)
                {
                    Action ac = list[i];
                    if (i == list.Count - 1)
                        continue;
                    ac.Perform();
                }
                Action la = list.Last();
                AutomationElement ae = AutomationHelpers.GetElementByProcess(la.ProcessName, la.Title, la.AutomationID, la.Name, la.ClassName, la.Index);
                if (la.Value == Action.ValueType.Name)
                    return ae.Current.Name;
                else if (la.Value == Action.ValueType.SelectionPattern)
                    return AutomationHelpers.GetSelection(ae);
                else if (la.Value == Action.ValueType.TextPattern)
                    return AutomationHelpers.GetText(ae);
                else if (la.Value == Action.ValueType.ValuePattern)
                    return AutomationHelpers.GetValue(ae);
                else if (la.Value == Action.ValueType.TogglePattern)
                    return AutomationHelpers.GetToggle(ae);
                else
                    return AutomationHelpers.GetImage(ae);
            }
            public void Set(string val)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Action ac = list[i];
                    if (i == list.Count - 1)
                        continue;
                    ac.Perform();
                }
                Action la = list.Last();
                AutomationElement ae = AutomationHelpers.GetElementByProcess(la.ProcessName, la.Title, la.AutomationID, la.Name, la.ClassName, la.Index);
                AutomationHelpers.SetValue(ae, val);
            }
            public override string ToString()
            {
                return Name;
            }
        }

        /* A getter method that returns the value of the recording variable. */
        public static bool IsRecording
        {
            get { return recording; }
        }

        private IKeyboardMouseEvents m_GlobalHook;
        private static bool recording = false;
        private static bool move = false;
        public static Recording rec = new Recording();
       /* Creating a global hook for the mouse and keyboard. */
        public Automation()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.MouseUpExt += GlobalHookMouseUpExt;
            m_GlobalHook.MouseMoveExt += GlobalHookMouseMoveExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
            m_GlobalHook.KeyUp += GlobalHookKeyUp;
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
            m_GlobalHook.MouseWheel += GlobalHookMouseWheel; 
            input = new InputSimulator();
        }

        //This represent the current element under the mouse.
       /* Getting the element that the mouse is currently over. */
        public static AutomationElement Element
        {
            get
            {
                try
                {
                    return AutomationElement.FromPoint(new System.Windows.Point(Cursor.Position.X, Cursor.Position.Y));
                }
                catch (Exception)
                {

                }
                return null;
            }
        }
        /// It starts recording.
        public static void StartRecording()
        {
            recording = true;
            rec = new Recording();
        }
        /// It stops the recording and adds it to the list of recordings
        public static void StopRecording()
        {
            recording=false;
            rec.Name = "Recording" + (Recordings.Count + 1);
            Recordings.Add(rec.Name,rec);
        }
        /// > Start recording the properties of the current object
        public static void StartPropertyRecording()
        {
            recording = true;
            rec = new Recording();
        }
        /// Stop recording the property and add it to the list of properties
        public static void StopPropertyRecording()
        {
            recording = false;
            rec.Name = "Property" + (Properties.Count+1);
            Properties.Add(rec.Name,rec);
        }
        /// It takes a string as an argument, and returns an object
        /// 
        /// @param prop The name of the property to get.
        /// 
        /// @return The value of the property.
        public static object GetProperty(string prop)
        {
            Recorder.AddLine("Automation.GetProperty(" + '"' + prop + '"' + ");");
            return Properties[prop].Get();
        }
       /// It takes a property name and a value, and sets the property to the value
       /// 
       /// @param prop The name of the property you want to set.
       /// @param val The value to set the property to.
        public static void SetProperty(string prop, string val)
        {
            Recording rec = (Recording)Properties[prop];
            rec.Set(val);
            Recorder.AddLine("Automation.SetProperty(" + '"' + prop + '"' + "," + '"' + val + '"' + ");");
        }
        /// If the dictionary contains the key, then update the value. Otherwise, add the key and value
        /// to the dictionary
        /// 
        /// @param Recording 
        public static void AddProperty(Recording rec)
        {
            if (Properties.ContainsKey(rec.Name))
                Properties[rec.Name] = rec;
            else
                Properties.Add(rec.Name, rec);
        }
        /// Runs a recording from the Properties collection
        /// 
        /// @param prop The name of the recording you want to run.
        public static void RunRecording(string prop)
        {
            Recording rec = (Recording)Properties[prop];
            rec.Run();
            Recorder.AddLine("Automation.RunRecording(" + '"' + prop + '"' + ");");
        }
        /// If the user is recording, and the mouse is clicked, then add a new action to the list of
        /// actions
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventExtArgs
        /// https://github.com/gmamaladze/globalmousekeyhook/blob/master/MouseKeyHook/MouseEventExtArgs.cs
        /// 
        /// @return The return value is the AutomationElement that is under the mouse pointer.
        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (!recording)
                return;
            AutomationElement el = Element;
            if (el == null)
                return;
            rec.List.Add(new Action(Action.Type.mousedown, el.Current.ProcessId, e));
        }
       /// If the user is recording, then get the current element and add a new action to the list of
       /// actions
       /// 
       /// @param sender The object that raised the event.
       /// @param MouseEventExtArgs
       /// https://github.com/gmamaladze/globalmousekeyhook/blob/master/MouseKeyHook/MouseEventExtArgs.cs
       /// 
       /// @return The AutomationElement is being returned.
        private void GlobalHookMouseUpExt(object sender, MouseEventExtArgs e)
        {
            if (!recording)
                return;
            AutomationElement el = Element;
            if (el == null)
                return;
            rec.List.Add(new Action(Action.Type.mouseup, el.Current.ProcessId, e));
        }
        /// If the mouse wheel is being used, and the mouse is over a window, then add the action to the
        /// list of actions
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs
        /// https://msdn.microsoft.com/en-us/library/system.windows.forms.mouseeventargs(v=vs.110).aspx
        /// 
        /// @return The return value is the AutomationElement that is currently under the mouse.
        private void GlobalHookMouseWheel(object sender, MouseEventArgs e)
        {
            if (!recording)
                return;
            AutomationElement el = Element;
            if (el == null)
                return;
            if (e.Delta > 0)
                rec.List.Add(new Action(Action.Type.wheelup, el.Current.ProcessId, e));
            else
                rec.List.Add(new Action(Action.Type.wheeldown, el.Current.ProcessId, e));
        }
       /// If the mouse is moving, and we're recording, and we have an element, then add a mousemove
       /// action to the list of actions
       /// 
       /// @param sender The object that raised the event.
       /// @param MouseEventExtArgs
       /// https://github.com/gmamaladze/globalmousekeyhook/blob/master/MouseKeyHook/MouseEventExtArgs.cs
       /// 
       /// @return The return value is the current AutomationElement.
        private void GlobalHookMouseMoveExt(object sender, MouseEventExtArgs e)
        {
            if (!move)
                return;
            if (!recording)
                return;
            AutomationElement el = Element;
            if (el == null)
                return;
            rec.List.Add(new Action(Action.Type.mousemove, el.Current.ProcessId, e));

        }
        /// If the user is recording, and the element is not null, then add a new action to the list of
        /// actions
        /// 
        /// @param sender The object that raised the event.
        /// @param KeyPressEventArgs
        /// https://msdn.microsoft.com/en-us/library/system.windows.forms.keypresseventargs(v=vs.110).aspx
        /// 
        /// @return The current process ID.
        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!recording)
                return;
            AutomationElement el = Element;
            if (el == null)
                return;
            rec.List.Add(new Action(Action.Type.keypress, el.Current.ProcessId, e));
        }
        /// If the user is recording, and the user has selected an element, then add a new action to the
        /// list of actions
        /// 
        /// @param sender The object that raised the event.
        /// @param KeyEventArgs
        /// https://msdn.microsoft.com/en-us/library/system.windows.input.keyeventargs(v=vs.110).aspx
        /// 
        /// @return The AutomationElement is being returned.
        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            if (!recording)
                return;
            AutomationElement el = Element;
            if (el == null)
                return;
            rec.List.Add(new Action(Action.Type.keyup, el.Current.ProcessId, e));
        }
        /// If the user is recording, and the user presses a key, then add a new action to the list of
        /// actions
        /// 
        /// @param sender The object that raised the event.
        /// @param KeyEventArgs
        /// https://msdn.microsoft.com/en-us/library/system.windows.input.keyeventargs(v=vs.110).aspx
        /// 
        /// @return The AutomationElement is being returned.
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            if (!recording)
                return;
            AutomationElement el = Element;
            if (el == null)
                return;
            rec.List.Add(new Action(Action.Type.keydown, el.Current.ProcessId, e));
        }

        /* It's a class that helps you automate windows applications */
        public static class AutomationHelpers
        {
            public static List<AutomationElement> GetChildren(AutomationElement parent)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                AutomationElementCollection collection = parent.FindAll(TreeScope.Children, Condition.TrueCondition);

                if (collection != null)
                {
                    List<AutomationElement> result = new List<AutomationElement>(collection.Cast<AutomationElement>());
                    return result;
                }
                else
                {
                    // some error occured
                    return null;
                }
            }
            public static List<AutomationElement> GetAllChildren(AutomationElement parent)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }
                try
                {
                    AutomationElementCollection collection = parent.FindAll(TreeScope.Subtree, Condition.TrueCondition);
                    if (collection != null)
                    {
                        List<AutomationElement> result = new List<AutomationElement>(collection.Cast<AutomationElement>());
                        return result;
                    }
                    else
                    {
                        // some error occured
                        return null;
                    }
                }
                catch (Exception )
                {
                    return null;
                }
            }
            public static List<AutomationElement> GetName(AutomationElement parent, string name)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                AutomationElementCollection collection = parent.FindAll(TreeScope.Subtree, Condition.TrueCondition);

                if (collection != null)
                {
                    List<AutomationElement> result = new List<AutomationElement>();
                    foreach (AutomationElement item in collection)
                    {
                        if (item.Current.Name == name)
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
                else
                {
                    // some error occured
                    return null;
                }
            }
            public static List<AutomationElement> GetAutomationID(AutomationElement parent, string id)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                AutomationElementCollection collection = parent.FindAll(TreeScope.Subtree, Condition.TrueCondition);

                if (collection != null)
                {
                    List<AutomationElement> result = new List<AutomationElement>();
                    foreach (AutomationElement item in collection)
                    {
                        if (item.Current.AutomationId == id)
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
                else
                {
                    // some error occured
                    return null;
                }
            }
            public static List<AutomationElement> GetClassName(AutomationElement parent, string name)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                AutomationElementCollection collection = parent.FindAll(TreeScope.Subtree, Condition.TrueCondition);

                if (collection != null)
                {
                    List<AutomationElement> result = new List<AutomationElement>();
                    foreach (AutomationElement item in collection)
                    {
                        if (item.Current.ClassName == name)
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
                else
                {
                    // some error occured
                    return null;
                }
            }
            public static AutomationElement GetElementByProcess(string process, string title, string id, string name, string classname, int index)
            {
                Process[] procs = Process.GetProcessesByName(process);
                string ti = title;
                if (ti == "")
                    ti = procs[0].MainWindowTitle;
                AutomationElement root = AutomationElement.RootElement;
                AutomationElementCollection prs = root.FindAll(TreeScope.Children, Condition.TrueCondition);
                AutomationElement window = null;
                List<AutomationElement> ats = new List<AutomationElement>();
                foreach (AutomationElement item in prs)
                {
                    try
                    {
                        if (item.Current.Name.Contains(ti))
                        {
                            window = item;
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                if (window == null)
                    throw new ArgumentException("Window " + title + " not found.");
                AutomationElementCollection items = window.FindAll(TreeScope.Subtree, Condition.TrueCondition);
                List<AutomationElement> result = new List<AutomationElement>();
                foreach (AutomationElement item in items)
                {
                    try
                    {
                        int[] r = item.GetRuntimeId();
                        if (item.Current.ClassName == classname && item.Current.AutomationId == id && item.Current.Name == name)
                        {
                            result.Add(item);
                        }
                    }
                    catch (Exception)
                    {

                    }
                        
                }
                if (index == -1)
                    return result[0];
                return result[index];
            }
            public static int GetIndex(string process, string title, string id, string name, string classname, Point p)
            {
                //For elements which have the same name, tittle, & id we also find the index of the item.
                Process[] procs = Process.GetProcessesByName(process);
                string ti = title;
                if (ti == "")
                    ti = procs[0].MainWindowTitle;
                AutomationElement root = AutomationElement.RootElement;
                AutomationElementCollection prs = root.FindAll(TreeScope.Children, Condition.TrueCondition);
                AutomationElement window = null;
                List<AutomationElement> ats = new List<AutomationElement>();
                foreach (AutomationElement item in prs)
                {
                    try
                    {
                        if (item.Current.Name.Contains(title))
                        {
                            window = item;
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                if (window == null)
                    return 0;
                AutomationElementCollection items = null;
                try
                {
                    items = window.FindAll(TreeScope.Subtree, Condition.TrueCondition);
                }
                catch (Exception)
                {

                }
                List<AutomationElement> result = new List<AutomationElement>();
                int index = 0;
                if(items != null)
                foreach (AutomationElement item in items)
                {
                    try
                    {
                        int[] r = item.GetRuntimeId();
                        if (item.Current.ClassName == classname && item.Current.AutomationId == id && item.Current.Name == name)
                        {
                            result.Add(item);
                        }
                        if (item.Current.BoundingRectangle.IntersectsWith(new System.Windows.Rect(p.X, p.Y, 1, 1)) && result.Count > 0)
                        {
                            index = result.Count-1;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                return index;
            }
            public static int[] StringToRuntimeID(string s)
            {
                int[] r = new int[3];
                string[] sts = s.Split(',');
                for (int i = 0; i < 3; i++)
                {
                    r[i] = int.Parse(sts[i]);
                }
                return r;
            }
            public static List<AutomationElement> GetElement(string id, string name)
            {
                AutomationElement parent = AutomationElement.RootElement;

                AutomationElementCollection collection = parent.FindAll(TreeScope.Subtree, Condition.TrueCondition);

                if (collection != null)
                {
                    List<AutomationElement> result = new List<AutomationElement>();
                    foreach (AutomationElement item in collection)
                    {
                        if (item.Current.AutomationId == id && item.Current.Name == name)
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
                else
                {
                    // some error occured
                    return null;
                }
            }
            public static List<AutomationElement> GetLocalizedControlType(AutomationElement parent, TreeScope scope, string type)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }
                AutomationElementCollection collection = parent.FindAll(scope, Condition.TrueCondition);
                if (collection != null)
                {
                    List<AutomationElement> result = new List<AutomationElement>();
                    foreach (AutomationElement item in collection)
                    {
                        if (item.Current.LocalizedControlType == type)
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
                else
                {
                    // some error occured
                    return null;
                }
            }
            public static List<AutomationElement> GetRawChildren(AutomationElement parent)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                List<AutomationElement> result = new List<AutomationElement>();

                // the predefined tree walker wich iterates through controls
                TreeWalker tw = TreeWalker.RawViewWalker;
                AutomationElement child = tw.GetFirstChild(parent);

                while (child != null)
                {
                    result.Add(child);
                    child = tw.GetNextSibling(child);
                }

                return result;
            }
            public static List<AutomationElement> GetRawAutomationID(AutomationElement parent, string auto)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                List<AutomationElement> all = new List<AutomationElement>();
                List<AutomationElement> result = new List<AutomationElement>();

                // the predefined tree walker wich iterates through controls
                TreeWalker tw = TreeWalker.RawViewWalker;
                AutomationElement child = tw.GetFirstChild(parent);

                while (child != null)
                {
                    all.Add(child);
                    child = tw.GetNextSibling(child);
                }
                foreach (AutomationElement item in all)
                {
                    if (item.Current.AutomationId == auto)
                    {
                        result.Add(item);
                    }
                }

                return result;
            }
            public static List<AutomationElement> GetRawLocalizedControlType(AutomationElement parent, string auto)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                List<AutomationElement> all = new List<AutomationElement>();
                List<AutomationElement> result = new List<AutomationElement>();

                // the predefined tree walker wich iterates through controls
                TreeWalker tw = TreeWalker.RawViewWalker;
                AutomationElement child = tw.GetFirstChild(parent);

                while (child != null)
                {
                    all.Add(child);
                    child = tw.GetNextSibling(child);
                }
                foreach (AutomationElement item in all)
                {
                    if (item.Current.LocalizedControlType == auto)
                    {
                        result.Add(item);
                    }
                }

                return result;
            }
            public static List<AutomationElement> GetRaw(AutomationElement parent)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }

                List<AutomationElement> result = new List<AutomationElement>();

                // the predefined tree walker wich iterates through controls
                TreeWalker tw = TreeWalker.RawViewWalker;
                AutomationElement child = tw.GetFirstChild(parent);

                while (child != null)
                {
                    result.Add(child);
                    child = tw.GetNextSibling(child);
                }

                return result;
            }
            public static string GetValue(AutomationElement control)
            {
                object patternProvider;
                if (control.TryGetCurrentPattern(ValuePattern.Pattern, out patternProvider))
                {
                    ValuePattern valuePatternProvider = patternProvider as ValuePattern;
                    return valuePatternProvider.Current.Value;
                }
                return null;
            }
            public static string GetSelection(AutomationElement control)
            {
                object patternProvider;
                if (control.TryGetCurrentPattern(SelectionPattern.Pattern, out patternProvider))
                {
                    SelectionPattern selPatternProvider = patternProvider as SelectionPattern;
                    AutomationElement[] sel = selPatternProvider.Current.GetSelection();
                    return sel[0].Current.Name;
                }
                return null;
            }
            public static string GetText(AutomationElement control)
            {
                object patternProvider;
                if (control.TryGetCurrentPattern(TextPattern.Pattern, out patternProvider))
                {
                    TextPattern textPatternProvider = patternProvider as TextPattern;
                    return textPatternProvider.DocumentRange.GetText(-1).TrimEnd('\r');
                }
                return null;
            }
            public static void SetValue(AutomationElement targetControl, string value)
            {
                // Validate arguments / initial setup
                if (value == null)
                    throw new ArgumentNullException(
                        "String parameter must not be null.");

                if (targetControl == null)
                    throw new ArgumentNullException(
                        "AutomationElement parameter must not be null");

                // A series of basic checks prior to attempting an insertion.
                //
                // Check #1: Is control enabled?
                // An alternative to testing for static or read-only controls 
                // is to filter using 
                // PropertyCondition(AutomationElement.IsEnabledProperty, true) 
                // and exclude all read-only text controls from the collection.
                if (!targetControl.Current.IsEnabled)
                {
                    throw new InvalidOperationException(
                        "The control is not enabled.\n\n");
                }

                // Check #2: Are there styles that prohibit us 
                //           from sending text to this control?
                if (!targetControl.Current.IsKeyboardFocusable)
                {
                    throw new InvalidOperationException(
                        "The control is not focusable.\n\n");
                }

                // Once you have an instance of an AutomationElement,  
                // check if it supports the ValuePattern pattern.
                object valuePattern = null;

                if (!targetControl.TryGetCurrentPattern(
                    ValuePattern.Pattern, out valuePattern))
                {
                    // Elements that support TextPattern 
                    // do not support ValuePattern and TextPattern
                    // does not support setting the text of 
                    // multi-line edit or document controls.
                    // For this reason, text input must be simulated.
                }
                // Control supports the ValuePattern pattern so we can 
                // use the SetValue method to insert content.
                else
                {
                    if (((ValuePattern)valuePattern).Current.IsReadOnly)
                    {
                        throw new InvalidOperationException(
                            "The control is read-only.");
                    }
                    else
                    {
                        ((ValuePattern)valuePattern).SetValue(value);
                        //We send enter key to update the value now that the text field has been set.
                        input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                    }
                }
            }
            public static int GetCount(AutomationElement parent)
            {
                if (parent == null)
                {
                    // null parameter
                    throw new ArgumentException();
                }
                int i = 0;
                // the predefined tree walker wich iterates through controls
                TreeWalker tw = TreeWalker.RawViewWalker;
                AutomationElement child = tw.GetFirstChild(parent);
                while (child != null)
                {
                    child = tw.GetNextSibling(child);
                    i++;
                }
                return i;
            }
            public static bool GetToggle(AutomationElement element)
            {
                if (element == null)
                {
                    return false;
                }

                Object objPattern;
                TogglePattern togPattern;
                if (true == element.TryGetCurrentPattern(TogglePattern.Pattern, out objPattern))
                {
                    togPattern = objPattern as TogglePattern;
                    return togPattern.Current.ToggleState == ToggleState.On;
                }
                return false;
            }
            public static Bitmap GetImage(AutomationElement el)
            {
                System.Drawing.Graphics g;
                System.Windows.Rect r = el.Current.BoundingRectangle;
                Bitmap b = new Bitmap((int)r.Width, (int)r.Height);
                g = System.Drawing.Graphics.FromImage(b);
                g.CopyFromScreen(new Point((int)r.X,(int)r.Y), new Point(0, 0),new Size(b.Width,b.Height));
                return b;
            }

        }
        
    }

}
