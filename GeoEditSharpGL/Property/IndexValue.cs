using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pexel
{
    class IndexValue
    {
        public int I { set; get; }
        public int J { set; get; }
        public int K { set; get; }
        public double Value { set; get; }
        public IndexValue() { I = 0; J = 0; K = 0; Value = 0; }
        public IndexValue(int i, int j, int k, double value) { I = i; J = j; K = k; Value = value; }

    }
}
