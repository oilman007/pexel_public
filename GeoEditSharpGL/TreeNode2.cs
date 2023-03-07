using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// https://stackoverflow.com/questions/28644011/how-to-show-multiple-check-boxes-in-a-treeview-in-c


namespace Pexel
{
    public class TreeNode2 : TreeNode
    {
        public string Label { get; set; }
        public bool Check1 { get; set; }
        public bool Check2 { get; set; }

        public new string Text
        {
            get { return Label; }
            set { Label = value; base.Text = ""; }
        }

        public TreeNode2() { }

        public TreeNode2(string text) { Label = text; }

        public TreeNode2(string text, bool check1, bool check2)
        {
            Label = text;
            Check1 = check1; Check2 = check2;
        }

        public TreeNode2(string text, TreeNode2[] children)
        {
            Label = text;
            foreach (TreeNode2 node in children) this.Nodes.Add(node);
        }
    }
}
