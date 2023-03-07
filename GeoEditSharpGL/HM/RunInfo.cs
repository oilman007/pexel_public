using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.HM
{
    public class RunInfo
    {
        public delegate void RunInfoHandler(RunInfo info);

        public string ID { set; get; }
        public DateTime Start { set; get; }
        public TimeSpan TimePerIter { set; get; }
        public int FirstIter { set; get; }
        public int LastIter { set; get; }
        public int CurIter { set; get; }
    }
}
