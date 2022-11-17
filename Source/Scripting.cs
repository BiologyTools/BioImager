using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using CSScripting;
using csscript;
using CSScriptLib;

namespace Bio
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
            public Script(string file, string scriptStr)
            {
                name = Path.GetFileName(file);
                scriptString = scriptStr;
                if (file.EndsWith(".txt") || file.EndsWith(".ijm"))
                    type = ScriptType.imagej;
            }
            public Script(string file)
            {
                name = Path.GetFileName(file);
                scriptString = File.ReadAllText(file);
                this.file = file;
                if (file.EndsWith(".txt") || file.EndsWith(".ijm"))
                    type = ScriptType.imagej;
            }
            public static void Run(Script rn)
            {
                scriptName = rn.name;
                Thread t = new Thread(new ThreadStart(RunScript));
                t.Start();
            }
            private static string scriptName = "";
            private static string str = "";
            private static void RunScript()
            {
                Script rn = Scripts[scriptName];
                rn.ex = null;
                if (rn.type == ScriptType.imagej)
                {
                    try
                    {
                        rn.done = false;
                        ImageJ.RunString(rn.scriptString,"", false);
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
            public void Run()
            {
                if (!Scripts.ContainsKey(name))
                    Scripts.Add(name, this);
                scriptName = this.name;
                thread = new Thread(new ThreadStart(RunScript));
                thread.Start();
            }
            public void Stop()
            {
                if(thread!=null)
                thread.Abort();
            }
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
            public static State GetUp(PointD pf,MouseButtons mb)
            {
                State st = new State();
                st.type = Event.Up;
                st.p = pf;
                st.buts = mb;
                return st;
            }
            public static State GetDown(PointD pf, MouseButtons mb)
            {
                State st = new State();
                st.type = Event.Down;
                st.p = pf;
                st.buts = mb;
                return st;
            }
            public static State GetMove(PointD pf, MouseButtons mb)
            {
                State st = new State();
                st.type = Event.Move;
                st.p = pf;
                st.buts = mb;
                return st;
            }
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
        public static State GetState()
        {
            return state;
        }
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
        public void RunScriptFile(string file)
        {
            Script sc = new Script(file);
            Scripts.Add(sc.name,sc);
            RefreshItems();
            RunByName(sc.name);
        }
        public static void RunScript(string file)
        {
            Script sc = new Script(file);
            Scripts.Add(sc.name, sc);
            RunByName(sc.name);
        }
        public static void RunString(string st)
        {
            Script.RunString(st);
        }
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
        public static void RunByName(string name)
        {
            Scripts[name].Run();
        }

        private void scriptView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Run();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void openScriptFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\Scripts";
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshItems();
            RefreshStatus();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                RefreshStatus();
            }
        }

        private void ScriptRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void scriptView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (scriptView.SelectedItems.Count == 0)
                return;
            ListViewItem item = scriptView.SelectedItems[0];
            Script s = (Script)item.Tag;
            textBox.Text = s.scriptString;
            scriptLabel.Text = s.name;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (scriptLabel.Text.EndsWith(".ijm"))
            {
                ImageJ.RunString(textBox.Text, ImageView.SelectedImage.ID, headlessBox.Checked);
            }
            else
                Run();
        }

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

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Scripts";
            saveFileDialog.FileName = scriptLabel.Text;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            scriptLabel.Text = Path.GetFileName(saveFileDialog.FileName);
            File.WriteAllText(saveFileDialog.FileName, textBox.Text);
        }

        private void stopBut_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostBox.Checked;
        }

        private void Scripting_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                saveButton.PerformClick();
            }
        }

        private void errorBox_SelectionChanged(object sender, EventArgs e)
        {
            Script sc = (Script)scriptView.SelectedItems[0].Tag;
            Exception ex = sc.ex;
        }

        private void errorView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Exception ex = (Exception)errorView.SelectedItems[0].Tag;
            string exs = ex.Message.Substring(ex.Message.IndexOf('('), ex.Message.IndexOf(')'));
            string ls = exs.Substring(1, exs.IndexOf(',')-1);
            int line = int.Parse(ls);
            string c = exs.Substring(exs.IndexOf(',') + 1, exs.IndexOf(")") - exs.IndexOf(',') - 1);
            int cr = int.Parse(c);
            //textBox.SelectionLength = cr;
            textBox.SelectionStart = line;
            textBox.Focus();
        }
    }
}
