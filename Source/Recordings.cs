using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bio
{
    public partial class Recordings : Form
    {
        public Recordings()
        {
            InitializeComponent();
            if (!Directory.Exists("Recordings"))
                Directory.CreateDirectory("Recordings");
            foreach (string file in Directory.GetFiles("Recordings"))
            {
                if (file.EndsWith("reco"))
                    OpenRecording(file);
                else if (file.EndsWith("pro"))
                    OpenProperty(file);
            }
            foreach (Automation.Action.ValueType val in (Automation.Action.ValueType[])Enum.GetValues(typeof(Automation.Action.ValueType)))
            {
                propBox.Items.Add(val);
            }

        }
        public void InitElements()
        {
            view.Nodes.Clear();
            foreach (Automation.Recording rec in Automation.Recordings.Values)
            {
                TreeNode tr = new TreeNode();
                Node n = new Node(rec, tr, Node.Type.recording);
                tr.Tag = n;
                tr.Text = rec.Name;
                foreach (Automation.Action item in rec.List)
                {
                    try
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = item.ToString();

                        Node no = new Node(item, tn, Node.Type.action);
                        no.recording = rec;
                        tn.Tag = no;
                        tr.Nodes.Add(tn);
                    }
                    catch (Exception)
                    {

                    }
                }
                view.Nodes.Add(tr);
            }

            propView.Nodes.Clear();
            foreach (Automation.Recording rec in Automation.Properties.Values)
            {
                TreeNode tr = new TreeNode();
                Node n = new Node(rec, tr, Node.Type.recording);
                tr.Tag = n;
                tr.Text = rec.Name;
                foreach (Automation.Action item in rec.List)
                {
                    try
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = item.ToString();

                        Node no = new Node(item, tn, Node.Type.action);
                        no.recording = rec;
                        tn.Tag = no;
                        tr.Nodes.Add(tn);
                    }
                    catch (Exception)
                    {

                    }
                }
                propView.Nodes.Add(tr);
            }
        }
        public void UpdateElements()
        {
            foreach (TreeNode rec in view.Nodes)
            {
                rec.Nodes.Clear();
                Node n = (Node)rec.Tag;
                foreach (Automation.Action item in n.recording.List)
                {
                    try
                    {
                        TreeNode tr = new TreeNode();
                        tr.Text = item.ToString();
                        Node no = new Node(item, rec, Node.Type.action);
                        no.recording = n.recording;
                        tr.Tag = no;
                        rec.Nodes.Add(tr);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            foreach (TreeNode rec in propView.Nodes)
            {
                rec.Nodes.Clear();
                Node n = (Node)rec.Tag;
                foreach (Automation.Action item in n.recording.List)
                {
                    try
                    {
                        TreeNode tr = new TreeNode();
                        tr.Text = item.ToString();
                        Node no = new Node(item, rec, Node.Type.action);
                        no.recording = n.recording;
                        tr.Tag = no;
                        rec.Nodes.Add(tr);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        public class Node
        {
            public Automation.Recording recording;
            public Automation.Action action;
            public Type type;
            public enum Type
            {
                recording,
                action
            }

            public TreeNode node;
            public Node(Automation.Action el, TreeNode n, Type t)
            {
                action = el;
                node = n;
                type = t;
                recording = new Automation.Recording();
                recording.List.Add(el);
                //items = Automation.AutomationHelpers.GetAllChildren(el.element);
            }
            public Node(Automation.Recording rec, TreeNode n, Type t)
            {
                recording = rec;
                node = n;
                type = t;
                //items = Automation.AutomationHelpers.GetAllChildren(el.element);
            }
            public override string ToString()
            {
                return action.Name + ", " + action.AutomationID + ", " + action.ClassName.ToString() + ", " + action.ActionType;
            }
        }

        public static void Perform(string act)
        {
            Automation.Recording rec = (Automation.Recording)Automation.Recordings[act];
            rec.Run();
            Recorder.AddLine("Recordings.Perform(" + rec.Name + ");");
        }
        public static object GetProperty(Automation.Action.ValueType automation, string pro)
        {
            Automation.Recording rec = null;
            if (Automation.Properties.ContainsKey(pro))
                rec = (Automation.Recording)Automation.Properties[pro];
            else
                return null;
            Recorder.AddLine("Recordings.Get(" + rec.Name + ");");
            return rec.Get();
        }

        private void view_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void startBut_Click(object sender, EventArgs e)
        {
            TextInput ti = new TextInput("Property" + propBox.Items.Count);
            ti.Text = "Set Property Name";
            if (ti.ShowDialog() != DialogResult.OK)
                return;
            recordStatusLabel.Text = "Recording: Started";
            if (Automation.IsRecording)
                return;
            Automation.StartRecording();
        }

        private void stopBut_Click(object sender, EventArgs e)
        {
            recordStatusLabel.Text = "Recording: Stopped";
            if (!Automation.IsRecording)
                return;
            Automation.StopRecording();
            if (Automation.Recordings.Count > 0)
            {
                //actions = Automation.Recordings[0].List;
                InitElements();
            }
        }

        private void playBut_Click(object sender, EventArgs e)
        {
            if (view.SelectedNode == null)
                return;
            Node n = (Node)view.SelectedNode.Tag;
            if (n.type == Node.Type.action)
                return; ;
            n.recording.Run();
        }

        private void view_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void Elements_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void performToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tr = view.SelectedNode;
            Node no = (Node)tr.Tag;
            if (no.type == Node.Type.action)
                no.action.Perform();
            else
                no.recording.Run();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveRecDialog.ShowDialog() != DialogResult.OK)
                return;
            SaveRecording(saveRecDialog.FileName);
        }
        private void SaveRecording(string file)
        {
            Automation.Recording n;
            if (view.SelectedNode == null)
                return;
            else
            {
                n = ((Node)view.SelectedNode.Tag).recording;
            }
            string s = Path.GetFileNameWithoutExtension(saveRecDialog.FileName);
            n.Name = s;
            string j = JsonConvert.SerializeObject(n.List, Formatting.None);
            File.WriteAllText(file, j);
            InitElements();
        }
        private void SaveProperty(string file)
        {
            Automation.Recording n;
            if (propView.SelectedNode == null)
                return;
            else
            {
                n = ((Node)propView.SelectedNode.Tag).recording;
            }
            string s = Path.GetFileNameWithoutExtension(savePropDialog.FileName);
            n.Name = s;

            string j = JsonConvert.SerializeObject(n.List, Formatting.None);
            File.WriteAllText(file, j);
            InitElements();
        }
        private Automation.Recording OpenRec(string file)
        {
            string st = File.ReadAllText(file);
            Automation.Recording rec = new Automation.Recording();
            rec.Name = Path.GetFileNameWithoutExtension(file);
            rec.File = file;
            Newtonsoft.Json.Linq.JArray ar = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(st);
            for (int i = 0; i < ar.Count; i++)
            {
                Automation.Action.Type t = (Automation.Action.Type)Enum.Parse(typeof(Automation.Action.Type), ar[i].ElementAt(0).First.ToString());
                if (t == Automation.Action.Type.keydown || t == Automation.Action.Type.keyup)
                {
                    Keys k = (Keys)int.Parse(ar[i].ElementAt(2).First.ToString());
                    KeyEventArgs kea = new KeyEventArgs(k);
                    int ind = int.Parse(ar[i].ElementAt(9).First.ToString());
                    Automation.Action ac = new Automation.Action(t, ar[i].ElementAt(8).First.ToString(), ar[i].ElementAt(9).First.ToString(), ind, ar[i].ElementAt(3).First.ToString(), ar[i].ElementAt(4).First.ToString(), ar[i].ElementAt(5).First.ToString(), kea);
                    rec.List.Add(ac);
                }
                if (t == Automation.Action.Type.mousedown || t == Automation.Action.Type.mouseup)
                {
                    MouseButtons mb = (MouseButtons)int.Parse(ar[i].ElementAt(2).First.ToString());
                    string ps = ar[i].ElementAt(3).First.ToString();
                    string xs = ps.Substring(0, ps.IndexOf(','));
                    string ys = ps.Substring(ps.IndexOf(',') + 1, ps.Length - (ps.IndexOf(',') + 1));
                    int ind = int.Parse(ar[i].ElementAt(9).First.ToString());
                    System.Drawing.Point po = new Point(int.Parse(xs), int.Parse(ys));
                    MouseEventArgs mo = new MouseEventArgs(mb, 1, po.X, po.Y, 0);
                    Automation.Action ac = new Automation.Action(t, ar[i].ElementAt(7).First.ToString(), ar[i].ElementAt(8).First.ToString(), ind, ar[i].ElementAt(4).First.ToString(), ar[i].ElementAt(5).First.ToString(), ar[i].ElementAt(6).First.ToString(), mo);
                    rec.List.Add(ac);
                }
            }
            return rec;
        }
        public void OpenRecording(string file)
        {
            Automation.Recording rec = OpenRec(file);
            Automation.Recordings.Add(rec.Name, rec);
            InitElements();
        }
        public void OpenProperty(string file)
        {
            Automation.Recording rec = OpenRec(file);
            Automation.Properties.Add(rec.Name, rec);
            InitElements();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openRecDialog.InitialDirectory = Application.StartupPath + "\\Recordings";
            if (openRecDialog.ShowDialog() != DialogResult.OK)
                return;
            OpenRecording(openRecDialog.FileName);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node n = (Node)view.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
            {
                Automation.Recordings.Remove(n.recording.Name);
                File.Delete(n.recording.File);
                InitElements();
            }
            else if (n.type == Node.Type.action)
            {
                n.recording.List.Remove(n.action);
                UpdateElements();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitElements();
        }

        private void startPropBut_Click(object sender, EventArgs e)
        {
            TextInput ti = new TextInput("Property" + propBox.Items.Count);
            ti.Text = "Set Property Name";
            ti.TopMost = true;
            if (ti.ShowDialog() != DialogResult.OK)
                return;
            propRecStatusLabel.Text = "Property Recording: Started";
            Automation.StartPropertyRecording();
        }

        private void stopPropBut_Click(object sender, EventArgs e)
        {
            propRecStatusLabel.Text = "Property Recording: Stopped";
            if (!Automation.IsRecording)
                return;
            Automation.StopPropertyRecording();
            if (Automation.Properties.Count > 0)
            {
                //actions = Automation.Recordings[0].List;
                InitElements();
            }
        }

        private void getPropBut_Click(object sender, EventArgs e)
        {
            if (propView.SelectedNode == null)
                return;
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.action)
                return;
            if (n.recording.List.Last().Value == Automation.Action.ValueType.Image)
                Clipboard.SetImage((Bitmap)Automation.GetProperty(n.recording.Name));
            else
            {
                if (n.recording.List.Last().Value == Automation.Action.ValueType.TogglePattern)
                {
                    bool b = (bool)Automation.GetProperty(n.recording.Name);
                    MessageBox.Show(b.ToString());
                }
                else
                {
                    string s = (string)Automation.GetProperty(n.recording.Name);
                    MessageBox.Show(s);
                }
            }
        }
        private void setPropBut_Click(object sender, EventArgs e)
        {
            TextInput ti = new TextInput("Property" + propBox.Items.Count);
            ti.Text = "Set Text To Set";
            ti.TopMost = true;
            if (propView.SelectedNode == null)
                return;
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.action)
                return;
            Automation.SetProperty(n.recording.Name, ti.TextValue);
        }
        private void saveSelectedValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePropDialog.InitialDirectory = Application.StartupPath + "\\Recordings";
            if (savePropDialog.ShowDialog() != DialogResult.OK)
                return;
            SaveProperty(savePropDialog.FileName);
        }

        private void openPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openPropDialog.InitialDirectory = Application.StartupPath + "\\Recordings";
            if (openPropDialog.ShowDialog() != DialogResult.OK)
                return;
            OpenProperty(openPropDialog.FileName);
        }

        private void getMenuItem_Click(object sender, EventArgs e)
        {
            getPropBut.PerformClick();
        }

        private void deletePropMenuItem_Click(object sender, EventArgs e)
        {
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
            {
                Automation.Properties.Remove(n.recording.Name);
                if (n.recording.File != null)
                    File.Delete(n.recording.File);
                InitElements();
            }
            else if (n.type == Node.Type.action)
            {
                n.recording.List.Remove(n.action);
                UpdateElements();
            }
        }


        private void propView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (propView.SelectedNode == null)
                return;
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
                return;
            propBox.SelectedIndex = (int)n.action.Value;
        }

        private void propBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (propView.SelectedNode == null)
                return;
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
                return;
            n.action.Value = (Automation.Action.ValueType)propBox.SelectedItem;
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (view.SelectedNode == null)
                return;
            Node n = (Node)view.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
                return;
            int oldindex = n.recording.List.IndexOf(n.action);
            n.recording.List.RemoveAt(oldindex);
            int newindex = oldindex - 1;
            if (newindex > oldindex) newindex--;
            // the actual index could have shifted due to the removal
            n.recording.List.Insert(newindex, n.action);
            UpdateElements();
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (view.SelectedNode == null)
                return;
            Node n = (Node)view.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
                return;
            int oldindex = n.recording.List.IndexOf(n.action);
            n.recording.List.RemoveAt(oldindex);
            int newindex = oldindex - 1;
            // the actual index could have shifted due to the removal
            n.recording.List.Insert(newindex, n.action);
            UpdateElements();
        }

        private void moveUpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (propView.SelectedNode == null)
                return;
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
                return;
            int oldindex = n.recording.List.IndexOf(n.action);
            n.recording.List.RemoveAt(oldindex);
            int newindex = oldindex - 1;
            // the actual index could have shifted due to the removal
            n.recording.List.Insert(newindex, n.action);
            UpdateElements();
        }

        private void moveDownToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (propView.SelectedNode == null)
                return;
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
                return;
            int oldindex = n.recording.List.IndexOf(n.action);
            n.recording.List.RemoveAt(oldindex);
            int newindex = (oldindex) + 1;
            // the actual index could have shifted due to the removal
            n.recording.List.Insert(newindex, n.action);
            UpdateElements();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (propView.SelectedNode == null)
                return;
            Node n = (Node)propView.SelectedNode.Tag;
            if (n.type == Node.Type.recording)
                return;
            n.action.Value = (Automation.Action.ValueType)propBox.SelectedItem;
        }

        private void topMostBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = topMostBox.Checked;
        }

        private void startPropBut_Click_1(object sender, EventArgs e)
        {

        }
    }
}
