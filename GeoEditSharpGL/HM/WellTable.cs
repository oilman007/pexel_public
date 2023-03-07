using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.HM
{
    public class WellTable
    {
        public string Title { set; get; } = string.Empty;
        
        public DateTime[] Dates { set; get; } = Array.Empty<DateTime>();
        
        public WellTableStep[] Steps { set; get; } = Array.Empty<WellTableStep>();

        public int[][] ProdPeriods { set; get; }

        public int[][] InjePeriods { set; get; }

        public Index3D[] Cells { set; get; } = Array.Empty<Index3D>();

        public int[][] KrCellGroups { set; get; }

        public List<string> Messages { private set; get; } = new List<string>();

        public bool NoHistory { private set; get; } = false; // to func
    }
}
