﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge;
using BioLib;

namespace BioImager
{
    public partial class NodeView : Form
    {
        public class Node
        {
            public TreeNode node;
            public enum DataType
            {
                image,
                buf,
                roi,
                text
            }
            private DataType type;
            public DataType Type
            {
                get { return type; }
                set { type = value; }
            }
            private object obj;
            public object Object
            {
                get { return obj; }
                set { obj = value; }
            }
            public Node(object data,DataType typ)
            {
                type = typ;
                obj = data;
                node = new TreeNode();
                node.Tag = this;
                node.Text = obj.ToString();
                node.ForeColor = System.Drawing.Color.White;
            }
            public string Text
            {
                get { return node.Text; }
                set { node.Text = value; }
            }
        }
        static bool setup = false;
        public NodeView(string[] args)
        {
            List<string> sts = new List<string>();
            sts.AddRange(args);
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("-setup"))
                {
                    setup = true;
                    sts.RemoveAt(i);
                }
            }
            App.nodeView = this;
            InitializeComponent();
            InitNodes();
            Init();
            if (args.Length > 0)
            {
                App.tabsView = new TabsView(sts.ToArray());
                App.tabsView.Show();
            }
            else
            {
                App.tabsView = new TabsView();
                App.tabsView.Show();
            }
            //timer.Start();
        }

        private static void Init()
        {
            if (!Properties.Settings.Default.Setup || setup)
                new Setup().ShowDialog();
            App.Initialize();
            Filters.Init();
            App.imager.Show();
            App.runner = new Scripting();
            App.recorder = new Recorder();
        }
        public void UpdateOverlay()
        {
            if (App.viewer != null)
                App.viewer.UpdateOverlay();
        }
        public void InitNodes()
        {
            treeView.Nodes.Clear();
            TreeNode images = new TreeNode();
            images.Text = "BioImages";
            images.ForeColor = System.Drawing.Color.White;
            foreach (BioImage item in Images.images)
            {
                //TreeNode node = new TreeNode();
                Node tree = new Node(item, Node.DataType.image);

                Node implanes = new Node(item, Node.DataType.text);
                implanes.Text = "Planes";

                foreach (AForge.Bitmap buf in item.Buffers)
                {
                    Node plane = new Node(buf, Node.DataType.buf);
                    plane.Text = buf.ID + ", " + buf.Coordinate.ToString();
                   
                    implanes.node.Nodes.Add(plane.node);
                }
                tree.node.Nodes.Add(implanes.node);

                Node rois = new Node(item, Node.DataType.text);
                rois.Text = "ROI";

                foreach (ROI an in item.Annotations)
                {
                    Node roi = new Node(an, Node.DataType.roi);
                    rois.node.Nodes.Add(roi.node);
                }
                tree.node.Nodes.Add(rois.node);
                images.Nodes.Add(tree.node);
            }
            treeView.Nodes.Add(images);
        }

        public void UpdateNodes()
        {
            if (Images.images.Count != treeView.Nodes[0].Nodes.Count)
            {
                //If image count is not same as node count we refresh whole tree.
                InitNodes();
                return;
            }
            TreeNode images = treeView.Nodes[0];
            foreach (TreeNode item in images.Nodes)
            {
                //TreeNode node = new TreeNode();
                Node node = (Node)item.Tag;
                BioImage im = (BioImage)node.Object;

                TreeNode rois = node.node.Nodes[1];
                if(im.Annotations.Count != rois.Nodes.Count)
                {
                    //If ROI count is not same as node count we refresh annotations.
                    rois.Nodes.Clear();
                    foreach (ROI an in im.Annotations)
                    {
                        Node roi = new Node(an, Node.DataType.roi);
                        rois.Nodes.Add(roi.node);
                    }
                }
                else
                for (int i = 0; i < im.Annotations.Count; i++)
                {
                    TreeNode roi = rois.Nodes[i];
                    Node n = (Node)roi.Tag;  
                    ROI an = (ROI)n.Object;
                    roi.Text = an.ToString();
                }
            }
        }

        public void Exit()
        {
            this.Close();
            Application.Exit();
            Application.ExitThread();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            UpdateNodes();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null)
                return;
            Node node = (Node)treeView.SelectedNode.Tag;
            if (node == null)
                return;
            if(node.Type == Node.DataType.roi)
            {
                ROI an = (ROI)node.Object;
                Node nod = (Node)treeView.SelectedNode.Parent.Tag;
                BioImage im = (BioImage)nod.Object;
                im.Annotations.Remove(an);
            }
            if (node.Type == Node.DataType.image)
            {
                BioImage im = (BioImage)node.Object;
                Images.RemoveImage(im);
                im.Dispose();
            }
            UpdateNodes();
            UpdateOverlay();
        }

        private void setTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node node = (Node)treeView.SelectedNode.Tag;
            if (node.Type == Node.DataType.roi)
            {
                ROI an = (ROI)node.Object;
                Node nod = (Node)treeView.SelectedNode.Parent.Tag;
                BioImage im = (BioImage)nod.Object;
                TextInput input = new TextInput(an.Text);
                if (input.ShowDialog() != DialogResult.OK)
                    return;
                an.Text = input.TextValue;
                an.family = input.Font.FontFamily.ToString();
                an.strokeWidth = input.Font.Size;
                an.strokeColor = AForge.Color.FromArgb(input.color.R, input.color.G, input.color.B);
            }
            UpdateNodes();
            UpdateOverlay();
        }

        private void setIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node node = (Node)treeView.SelectedNode.Tag;
            if (node.Type == Node.DataType.roi)
            {
                ROI an = (ROI)node.Object;
                Node nod = (Node)treeView.SelectedNode.Parent.Tag;
                BioImage im = (BioImage)nod.Object;
                TextInput input = new TextInput(an.id);
                if (input.ShowDialog() != DialogResult.OK)
                    return;
                an.id = input.TextValue;
            }
            UpdateNodes();
            UpdateOverlay();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void newTabsViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabsView v = new TabsView();
            v.Show();
        }

        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            Node node = (Node)treeView.SelectedNode.Tag;
            if (node != null)
                if (node.Type == Node.DataType.buf)
                {
                    setIDToolStripMenuItem.Visible = false;
                    setTextToolStripMenuItem.Visible = false;
                    AForge.Bitmap buf = (AForge.Bitmap)node.Object;
                    App.viewer.SetCoordinate(buf.Coordinate.Z, buf.Coordinate.C, buf.Coordinate.T);
                    App.viewer.GoToImage();
                }
                else
                if (node.Type == Node.DataType.roi)
                {
                    setIDToolStripMenuItem.Visible = true;
                    setTextToolStripMenuItem.Visible = true;
                    ROI an = (ROI)node.Object;
                    string name = node.node.Parent.Parent.Text;
                    ImageView v = TabsView.SelectedViewer;
                    if (v != null)
                        v.SetCoordinate(an.coord.Z, an.coord.C, an.coord.T);
                }
        }
    }
}
