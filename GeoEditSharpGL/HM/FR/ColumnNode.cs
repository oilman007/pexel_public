using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

namespace Pexel.HM.FR
{
    public class ColumnNode : Node
    {
        // This sould make the DataPropertyName specified in the Node Collection.
        public string NodeControl0 = "";
        public bool NodeControl1 = true;
        public bool NodeControl2 = true;
        
        public ColumnNode(string nodeControl0, bool nodeControl1, bool nodeControl2)
        {
            NodeControl0 = nodeControl0;
            NodeControl1 = nodeControl1;
            NodeControl2 = nodeControl2;
        }
    }


}
