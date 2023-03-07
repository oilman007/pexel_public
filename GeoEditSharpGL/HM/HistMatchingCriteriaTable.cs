using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pexel
{
    public class HistMatchingCriteriaTable
    {
        

        /*
        public bool TryGetCriteria(string wellname, DateTime dt, out HistMatchingCriteria criteria)
        {
            criteria = new HistMatchingCriteria();
            if (!Criteria.ContainsKey(wellname))
                return false;
            if (Criteria[wellname].Keys.Count == 0)
                return false;
            if (Criteria[wellname].ContainsKey(dt))
                criteria = Criteria[wellname][dt];
            else
                criteria = Criteria[wellname].Values.First();
            return true;
        }
        */


        public List<HistMatchingCriteria> Groups { get; set; } = new List<HistMatchingCriteria>();

        public Dictionary<string, HistMatchingCriteria> Wells { set; get; } = new Dictionary<string, HistMatchingCriteria>();







    }
}
