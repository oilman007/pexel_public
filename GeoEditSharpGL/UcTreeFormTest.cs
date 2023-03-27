using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexel
{
    public partial class UcTreeFormTest : Form
    {

        public UcTreeFormTest()
        {
            InitializeComponent();



            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true) { Tag = "some tag" });
            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes.Add(new TreeNode2("new one", true, true));

            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));
            ucTreeView1.Nodes[0].Nodes.Add(new TreeNode2("new one", true, true));


            treeView1.Nodes.Add("one");
            treeView1.Nodes.Add("two");
            treeView1.Nodes.Add("three");

        }



        private void ucTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = ucTreeView1.SelectedNode;
        }


        //TreeNode node;
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {              
            //node = e.Node;
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            //TreeNode node = treeView1.GetNodeAt(MousePosition.X, MousePosition.Y);
            if(node is null) return;
            if (!((TreeNode2)node).Check2)
                node.SelectedImageIndex = 2;
            else if (!((TreeNode2)node).Check1)
                node.SelectedImageIndex = 1;
            else
                node.SelectedImageIndex = 0;
            node.ImageIndex = node.SelectedImageIndex;
        }

        private void ucTreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            /*
            if (!((TreeNode2)e.Node).Check2)
                e.Node.SelectedImageIndex = 3;
            else if(!((TreeNode2)e.Node).Check1)
                e.Node.SelectedImageIndex = 2;
            else
                e.Node.SelectedImageIndex = 1;
            e.Node.ImageIndex = e.Node.SelectedImageIndex;
            */
        }

        private void ucTreeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            /*
            if (!((TreeNode2)e.Node).Check2)
            {
                e.Node.SelectedImageIndex = 3;
            }
            else if (!((TreeNode2)e.Node).Check1)
            {
                e.Node.SelectedImageIndex = 2;
            }
            else
            {
                e.Node.SelectedImageIndex = 1;
            }
            e.Node.ImageIndex = e.Node.SelectedImageIndex;
            */

        }


        private void ucTreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            //node = TreeNode2 as treeView1.GetNodeAt(e.X, e.Y);
        }



        TreeNode node;
        private void ucTreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            node = e.Node;
        }

        private void ucTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
    }
}
