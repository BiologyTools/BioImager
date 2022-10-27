using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bio
{
    public partial class XMLView : Form
    {
        XmlTreeDisplay tree = null;
        public XMLView(string xml)
        {
            InitializeComponent();
            tree = new XmlTreeDisplay(xml);
            tree.Dock = DockStyle.Fill;
            this.Controls.Add(tree);
            
        }
    }
    public class XmlTreeDisplay : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.TreeView treeXml = new TreeView();

        public XmlTreeDisplay(string xml)
        {
            treeXml.Nodes.Clear();
            this.Controls.Add(treeXml);
            // Load the XML Document
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
                //doc.Load("");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }

            ConvertXmlNodeToTreeNode(doc, treeXml.Nodes);
            treeXml.Nodes[0].ExpandAll();
            treeXml.Dock = DockStyle.Fill;
        }

        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode,
          TreeNodeCollection treeNodes)
        {

            TreeNode newTreeNode = treeNodes.Add(xmlNode.Name);

            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    newTreeNode.Text = "<?" + xmlNode.Name + " " +
                      xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    newTreeNode.Text = "<" + xmlNode.Name + ">";
                    break;
                case XmlNodeType.Attribute:
                    newTreeNode.Text = "ATTRIBUTE: " + xmlNode.Name;
                    break;
                case XmlNodeType.Text:
                case XmlNodeType.CDATA:
                    newTreeNode.Text = xmlNode.Value;
                    break;
                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
            }
        }
    }
}
