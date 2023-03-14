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


        TreeNode node;
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {              
            node = e.Node;
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            if(node is null) return;
            node.SelectedImageIndex = node.SelectedImageIndex == 0 ? 1 : 0;
            node.ImageIndex = node.SelectedImageIndex;
        }
    }
}
