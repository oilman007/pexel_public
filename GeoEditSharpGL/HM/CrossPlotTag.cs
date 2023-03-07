using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace Pexel.HM
{
    class CrossPlotTag
    {

        public double Delta { set; get; }
        public LineItem MidLine { set; get; } 
        public LineItem MinLine { set; get; } 
        public LineItem MaxLine { set; get; } 

        public TextObj MinLineText { set; get; }
        public TextObj MaxLineText { set; get; }

    }
}
