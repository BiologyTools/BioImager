using CSScriptLib;
using AForge;
namespace BioImager
{
    public partial class Scripting : Form
    {
        public static string log;
        public CodeView view = null;
        public static string ImageJPath = Properties.Settings.Default.ImageJPath;

        public static void LogLine(string s)
        {
            log += s + Environment.NewLine;
        }
        public static Dictionary<string, Script> Scripts = new Dictionary<string, Script>();
        public class Script
        {
            public string name;
            public string file;
            public string scriptString;
            public dynamic script;
            public object obj;
            public string output = "";
            public bool done = false;
            public static List<string> usings = new List<string>();
            public Exception ex = null;
            public Thread thread;
            public ScriptType type = ScriptType.script;
            /* Creating a new script object. */
            public Script(string file, string scriptStr)
            {
                name = Path.GetFileName(file);
                scriptString = scriptStr;
                if (file.EndsWith(".txt") || file.EndsWith(".ijm"))
                    type = ScriptType.imagej;
            }
            /* Reading the file and storing the file name, file path, and file contents in the
            variables name, file, and scriptString. */
            public Script(string file)
            {
                name = Path.GetFileName(file);
                scriptString = File.ReadAllText(file);
                this.file = file;
                if (file.EndsWith(".txt") || file.EndsWith(".ijm"))
                    type = ScriptType.imagej;
            }

            /// It creates a new thread and starts it. 
            /// 
            /// The thread is started by calling the RunScript function. 
            public static void Run(Script rn)
            {
                scriptName = rn.name;
                Thread t = new Thread(new ThreadStart(RunScript));
                t.Start();
            }
            private static string scriptName = "";
            private static string str = "";
            /// It runs a script in a separate thread
            private static void RunScript()
            {
                Script rn = Scripts[scriptName];
                rn.ex = null;
                if (rn.type == ScriptType.imagej)
                {
                    try
                    {
                        rn.done = false;
                        ImageJ.RunString(rn.scriptString, "", false);
                        rn.done = true;
                    }
                    catch (Exception e)
                    {
                        rn.ex = e;
                    }
                }
                else
                {
                    try
                    {
                        rn.done = false;
                        rn.script = CSScript.Evaluator.LoadCode(rn.scriptString);
                        rn.obj = rn.script.Load();
                        rn.output = rn.obj.ToString();
                        rn.done = true;
                    }
                    catch (Exception e)
                    {
                        rn.ex = e;
                    }
                }
            }
            /// It takes a string, adds a bunch of using statements, wraps the string in a class, and
            /// then compiles and runs the class
            /// 
            /// @param st The string to be executed
            /// 
            /// @return The return value of the last statement in the script.
            public static object RunString(string st)
            {
                try
                {
                    string loader =
                  @"//css_reference " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + @".dll
                    using System;
                    using System.Windows.Forms;
                    using System.Drawing;
                    using System.Threading;
                    using Bio;";
                    foreach (string s in usings)
                    {
                        loader += usings + Environment.NewLine;
                    }
                    loader += @"
                    public class Loader
                    {
                        public object Load()
                        {" +
                            st + @";
                            return true;
                        }
                    }";
                    dynamic script = CSScript.Evaluator.LoadCode(loader);
                    return script.Load();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, e.Source);
                    return e;
                }
            }
            /// It creates a new thread and starts it
            public void Run()
            {
                if (!Scripts.ContainsKey(name))
                    Scripts.Add(name, this);
                scriptName = this.name;
                thread = new Thread(new ThreadStart(RunScript));
                thread.Start();
            }
           /// The function stops the thread
            public void Stop()
            {
                if (thread != null)
                    thread.Abort();
            }
            /// If the thread is not null, return the name and the thread state. Otherwise, return the
           /// name
           /// 
           /// @return The name of the thread and the state of the thread.
            public override string ToString()
            {
                if (thread != null)
                {
                    return name.ToString() + ", " + thread.ThreadState.ToString();
                }
                else
                    return name.ToString();
            }
        }
        public class State
        {
            public Event type;
            /// > This function returns a new State object with the type set to Event.Up, the point set
            /// to the point passed in, and the buttons set to the buttons passed in
            /// 
            /// @param PointD A point with double precision.
            /// @param MouseButtons The mouse button that was pressed.
            /// 
            /// @return A new State object is being returned.
            public static State GetUp(PointD pf, MouseButtons mb)
            {
                State st = new State();
                st.type = Event.Up;
                st.p = pf;
                st.buts = mb;
                return st;
            }
            /// > This function returns a new State object with the type set to Event.Down, the point
            /// set to the point passed in, and the buttons set to the buttons passed in
            /// 
            /// @param PointD A point with double precision.
            /// @param MouseButtons The mouse button that was pressed.
            /// 
            /// @return A new State object is being returned.
            public static State GetDown(PointD pf, MouseButtons mb)
            {
                State st = new State();
                st.type = Event.Down;
                st.p = pf;
                st.buts = mb;
                return st;
            }
            /// > This function returns a state object that represents a mouse move event
            /// 
            /// @param PointD A point with double precision.
            /// @param MouseButtons The mouse buttons that are pressed.
            /// 
            /// @return A new State object with the type, p, and buts fields set to the values passed
            /// in.
            public static State GetMove(PointD pf, MouseButtons mb)
            {
                State st = new State();
                st.type = Event.Move;
                st.p = pf;
                st.buts = mb;
                return st;
            }
            /// It returns a new State object with the type set to None, the point set to a new PointD
            /// object, and the buttons set to None
            /// 
            /// @return A new State object with the type set to None, the point set to a new PointD
            /// object, and the buttons set to None.
            public static State GetNone()
            {
                State st = new State();
                st.type = Event.None;
                st.p = new PointD();
                st.buts = MouseButtons.None;
                return st;
            }

            public PointD p;
            public MouseButtons buts;
            public bool processed = false;
            public override string ToString()
            {
                return type.ToString() + " ,(" + p.X.ToString() + ", " + p.Y.ToString() + "), " + buts.ToString();
            }

        }
        public enum Event
        {
            Down,
            Up,
            Move,
            None
        }
        public enum ScriptType
        {
            tool,
            script,
            imagej
        }
        private static State state;
        /// It returns the state of the game.
        /// 
        /// @return The state of the game.
        public static State GetState()
        {
            return state;
        }
        /// If the state is null, return. If the state is not null, set the state to the new state. If
        /// the state is not null and the new state is the same as the old state, set the processed flag
        /// to true
        /// 
        /// @param State This is the state of the game. It contains the position of the player, the type
        /// of the player, and whether the state has been processed or not.
        /// 
        /// @return The state of the current object.
        public static void UpdateState(State s)
        {
            if (s == null)
                return;
            if (state == null)
                state = s;
            if (s.p.X == state.p.X && s.p.Y == state.p.Y && s.type == state.type)
            {
                state.processed = true;
            }
            else
                state = s;
        }
        /// It reads all the files in the Scripts and Tools folders and adds them to a listview
        public void RefreshItems()
        {
            Scripts.Clear();
            string st = Application.StartupPath;
            foreach (string file in Directory.GetFiles(st + "/Scripts"))
            {
                if (!Scripts.ContainsKey(Path.GetFileName(file)))
                {
                    //This is a script file.
                    Script sc = new Script(file, File.ReadAllText(file));
                    ListViewItem lv = new ListViewItem();
                    lv.Tag = sc;
                    lv.Text = sc.ToString();
                    scriptView.Items.Add(lv);
                    Scripts.Add(lv.Text, sc);
                }
            }
            foreach (string file in Directory.GetFiles(st + "/Tools"))
            {
                if (file.EndsWith(".cs"))
                {
                    if (!Scripts.ContainsKey(Path.GetFileName(file)))
                    {
                        //This is a script file.
                        Script sc = new Script(file, File.ReadAllText(file));
                        sc.type = ScriptType.tool;
                        ListViewItem lv = new ListViewItem();
                        lv.Tag = sc;
                        lv.Text = sc.ToString();
                        scriptView.Items.Add(lv);
                        Scripts.Add(lv.Text, sc);
                    }
                }
            }
        }
        /// It updates the status of the script and the log
        public void RefreshStatus()
        {
            errorView.Clear();
            foreach (ListViewItem item in scriptView.SelectedItems)
            {
                Script s = (Script)item.Tag;
                //We update item text to show Script status.
                item.Text = s.ToString();
                outputBox.Text = s.output;
                if (s.ex != null)
                {
                    string[] sps = s.ex.Message.Split('>');
                    //ListViewItem it = new ListViewItem(s.ex.ToString());
                    for (int i = 1; i < sps.Length; i++)
                    {
                        ListViewItem er = new ListViewItem(sps[i]);
                        er.Tag = s.ex;
                        errorView.Items.Add(er);
                    }

                }
            }
            foreach (ListViewItem item in scriptView.SelectedItems)
            {
                Script s = (Script)item.Tag;
                //We update item text to show Script status.
                item.Text = s.ToString();
            }
            logBox.Text = log;
            //We scroll to end of text so we see latest log output.
            if (logBox.SelectionStart != logBox.Text.Length)
            {
                logBox.SelectionStart = logBox.Text.Length;
                logBox.ScrollToCaret();
            }
        }
        private CodeView codeview;
        private RichTextBox textBox;

        /* Creating a new directory called Scripts and Tools. */
        public Scripting()
        {
            if (!Directory.Exists(Application.StartupPath + "//" + "Scripts"))
                Directory.CreateDirectory(Application.StartupPath + "//" + "Scripts");
            if (!Directory.Exists(Application.StartupPath + "//" + "Tools"))
                Directory.CreateDirectory(Application.StartupPath + "//" + "Tools");
            InitializeComponent();
            scriptView.MultiSelect = false;
            RefreshItems();
            timer.Start();
            codeview = new CodeView();
            codeview.Dock = DockStyle.Fill;
            scriptLabel.Text = "NewScript.cs";
            //splitContainer.Dock = DockStyle.Fill;
            textBox = codeview.TextBox;
            splitContainer.Panel1.Controls.Add(codeview);
        }
        /// It runs a script file
        /// 
        /// @param file The file path of the script to run.
        public void RunScriptFile(string file)
        {
            Script sc = new Script(file);
            Scripts.Add(sc.name, sc);
            RefreshItems();
            RunByName(sc.name);
        }
        /// It creates a new script object, adds it to the dictionary, and then runs it
        /// 
        /// @param file The file path to the script.
        public static void RunScript(string file)
        {
            Script sc = new Script(file);
            Scripts.Add(sc.name, sc);
            RunByName(sc.name);
        }
        public static void RunScript(Script sc)
        {
            if(!Scripts.ContainsKey(sc.name))
            Scripts.Add(sc.name, sc);
            RunByName(sc.name);
        }
        /// It runs a string as a Lua script
        /// 
        /// @param st The string to run.
        public static void RunString(string st)
        {
            Script.RunString(st);
        }
        /// It runs the script that is selected in the listview
        /// 
        /// @return The output of the script.
        public void Run()
        {
            log = "";
            outputBox.Text = "";
            logBox.Text = "";
            if (scriptView.SelectedItems.Count == 0)
                return;
            foreach (ListViewItem item in scriptView.SelectedItems)
            {
                //We run this script
                Script sc = (Script)item.Tag;
                sc.scriptString = textBox.Text;
                sc.output = "";
                sc.Run();
                outputBox.Text = sc.output;
                logBox.Text = log;
            }
        }
        /// We stop the script
        /// 
        /// @return The script is being returned.
        public void Stop()
        {
            if (scriptView.SelectedItems.Count == 0)
                return;
            foreach (ListViewItem item in scriptView.SelectedItems)
            {
                //We run this script
                Script sc = (Script)item.Tag;
                sc.Stop();
            }
        }
        /// RunByName(string name)
        /// 
        /// @param name The name of the script.
        public static void RunByName(string name)
        {
            Scripts[name].Run();
        }

        /// If the user double clicks on the script view, run the script
        /// 
        /// @param sender The object that raised the event.
        /// @param MouseEventArgs The event data.
        private void scriptView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Run();
        }

        /// When the user clicks the "Run" menu item, the Run() function is called.
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }

        /// It opens the folder where the scripts are stored
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void openScriptFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\Scripts";
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        /// RefreshItems() refreshes the list of items in the listbox
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshItems();
            RefreshStatus();
        }

        /// If the window is not minimized, then refresh the status
/// 
/// @param sender The object that raised the event.
/// @param EventArgs The event arguments.
        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                RefreshStatus();
            }
        }

        /// If the user tries to close the form, cancel the close event and minimize the form instead
        /// 
        /// @param sender The object that raised the event.
        /// @param FormClosingEventArgs The event arguments for the FormClosing event.
        private void ScriptRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        /// If the user selects a script from the list, the text box will display the script's text and
        /// the script label will display the script's name
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The scriptString and name of the selected script.
        private void scriptView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (scriptView.SelectedItems.Count == 0)
                return;
            ListViewItem item = scriptView.SelectedItems[0];
            Script s = (Script)item.Tag;
            textBox.Text = s.scriptString;
            scriptLabel.Text = s.name;
        }

        /// If the script is an ImageJ macro, run it in ImageJ, otherwise run it in the C# environment
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        private void runButton_Click(object sender, EventArgs e)
        {
            if (scriptLabel.Text.EndsWith(".ijm"))
            {
                ImageJ.RunString(textBox.Text, ImageView.SelectedImage.ID, headlessBox.Checked);
            }
            else
                Run();
        }

        /// It opens a file dialog, reads the file, and adds it to a listview
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The event arguments.
        /// 
        /// @return The script is being returned.
        private void scriptLoadBut_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Application.StartupPath + "\\Scripts";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Script script = new Script(openFileDialog.FileName, File.ReadAllText(openFileDialog.FileName));
            script.name = Path.GetFileName(openFileDialog.FileName);
            textBox.Text = script.scriptString;
            scriptLabel.Text = Path.GetFileName(openFileDialog.FileName);
            ListViewItem item = new ListViewItem();
            item.Tag = script;
            item.Text = script.name;
            scriptView.Items.Add(item);
        }

        /// If the user clicks the save button, then the save file dialog will open in the scripts
        /// folder, and the name of the file will be the name of the script. If the user clicks OK, then
        /// the name of the script will be the name of the file. If the user clicks cancel, then the
        /// function will return. The text of the file will be the text in the text box
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        /// 
        /// @return The file name of the file that was saved.
        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Scripts";
            saveFileDialog.FileName = scriptLabel.Text;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            scriptLabel.Text = Path.GetFileName(saveFileDialog.FileName);
            File.WriteAllText(saveFileDialog.FileName, textBox.Text);
        }

        /// It stops the timer and resets the timer to 0
/// 
/// @param sender The object that raised the event.
/// @param EventArgs The EventArgs class is the base class for classes that contain event data.
        private void stopBut_Click(object sender, EventArgs e)
        {
            Stop();
        }

        /// If the topMostBox checkbox is checked, then the form will be topmost
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs The EventArgs class is the base class for classes containing event data.
        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostBox.Checked;
        }

        /// If the user presses the Ctrl key and the S key at the same time, the
        /// saveButton.PerformClick() function is called
        /// 
        /// @param sender The object that raised the event.
        /// @param KeyEventArgs The event arguments that are passed to the event handler.
        private void Scripting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                saveButton.PerformClick();
            }
        }

        /// When the user selects a script in the errorBox, the exception that was thrown by that script
       /// is stored in the variable ex
       /// 
       /// @param sender The object that raised the event.
       /// @param EventArgs 
        private void errorBox_SelectionChanged(object sender, EventArgs e)
        {
            Script sc = (Script)scriptView.SelectedItems[0].Tag;
            Exception ex = sc.ex;
        }

        /// It takes the exception message, parses it, and then selects the line and column of the error
        /// in the textbox
        /// 
        /// @param sender The object that raised the event.
        /// @param EventArgs e
        private void errorView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Exception ex = (Exception)errorView.SelectedItems[0].Tag;
            string exs = ex.Message.Substring(ex.Message.IndexOf('('), ex.Message.IndexOf(')'));
            string ls = exs.Substring(1, exs.IndexOf(',') - 1);
            int line = int.Parse(ls);
            string c = exs.Substring(exs.IndexOf(',') + 1, exs.IndexOf(")") - exs.IndexOf(',') - 1);
            int cr = int.Parse(c);
            //textBox.SelectionLength = cr;
            textBox.SelectionStart = line;
            textBox.Focus();
        }
    }
}
