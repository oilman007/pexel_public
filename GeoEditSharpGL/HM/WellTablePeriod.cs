using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.HM
{
    public class WellTablePeriod
    {
        public WellTablePeriod() { }
        public DateTime[] Dates { set; get; } = Array.Empty<DateTime>();
        public WellTableStep[] Steps { set; get; } = Array.Empty<WellTableStep>();


    }
}
