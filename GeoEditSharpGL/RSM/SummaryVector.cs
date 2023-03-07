using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.RSM
{


    [Serializable]
    public partial class SummaryVector
    {
        public const string KW_WOPT = "WOPT";
        public const string KW_WLPT = "WLPT";
        public const string KW_WWPT = "WWPT";
        public const string KW_WWIT = "WWIT";
        public const string KW_WOPTH = "WOPTH";
        public const string KW_WLPTH = "WLPTH";
        public const string KW_WWPTH = "WWPTH";
        public const string KW_WWITH = "WWITH";

        public const string KW_WLPRH = "WLPRH";
        public const string KW_WLPR = "WLPR";
        public const string KW_WWIRH = "WWIRH";
        public const string KW_WOPR = "WOPR";
        public const string KW_WOPRH = "WOPRH";
        public const string KW_WWPR = "WWPR";
        public const string KW_WWPRH = "WWPRH";
        public const string KW_WWIR = "WWIR";

        public const string KW_WTHP = "WTHP";
        public const string KW_WTHPH = "WTHPH";
        public const string KW_WBHP = "WBHP";
        public const string KW_WBHPH = "WBHPH";
        public const string KW_WBP9 = "WBP9";

        public const string KW_CLPT = "CLPT";
        public const string KW_COPT = "COPT";
        public const string KW_CWPT = "CWPT";
        public const string KW_CWIT = "CWIT";
        public const string KW_COPR = "COPR";
        public const string KW_CWPR = "CWPR";
        public const string KW_CWIR = "CWIR";
        public const string KW_CLPR = "CLPR";

        public const string KW_FOPT = "FOPT";
        public const string KW_FOPTH = "FOPTH";
        public const string KW_FLPT = "FLPT";
        public const string KW_FLPTH = "FLPTH";
        public const string KW_FWPT = "FWPT";
        public const string KW_FWPTH = "FWPTH";
        public const string KW_FWIT = "FWIT";
        public const string KW_FWITH = "FWITH";

        public const string KW_FOPR = "FOPR";
        public const string KW_FOPRH = "FOPRH";
        public const string KW_FLPR = "FLPR";
        public const string KW_FLPRH = "FLPRH";
        public const string KW_FWIR = "FWIR";
        public const string KW_FWIRH = "FWIRH";



        public SummaryVector() { }


        public SummaryVector(string vectorName, string id, string units, string info, DateTime[] dates, double[] values)
        {
            VectorName = vectorName;
            ID = id;
            Units = units;
            Dates = dates;
            Values = values;
            Info = info;
        }

        public SummaryVector(int n) 
        {
            Dates = new DateTime[n];
            Values = new double[n];
            for (int i = 0; i < n; ++i) Values[i] = 0;
        }

        public DateTime[] Dates { set; get; } = new DateTime[0];
        public double[] Values { set; get; } = new double[0];
        public string Info { set; get; } = string.Empty;
        public string ID { set; get; } = string.Empty;
        public string VectorName { set; get; } = string.Empty;
        public string Units { set; get; } = string.Empty;




        public double Value(DateTime date)
        {
            int count = Dates.Count();
            if (count == 0) return -999f;
            if (count == 1) return Values.First();
            if (date < Dates.First()) return Values.First();
            if (date > Dates.Last()) return Values.Last();
            for (int i = 1; i < count; ++i)
            {
                if (date == Dates[i])
                    return Values[i];
                else if (date < Dates[i])
                    return Value(Values[i - 1], Values[i], Dates[i - 1], Dates[i], date);
            }
            return -999f;
        }



        double Value(double v1, double v2, DateTime d1, DateTime d2, DateTime d)
        {
            double k = (v2 - v1) / (d2.ToOADate() - d1.ToOADate());
            double b = v1 - k * d1.ToOADate();
            double result = k * d.ToOADate() + b;
            return result;
        }





        public SummaryVector SubVector(DateTime firstDate)
        {
            int count = Dates.Count();
            int f;
            for (f = 0; f < count; ++f)
                if (Dates[f] >= firstDate)
                    break;
            DateTime[] subDates = new DateTime[count - f];
            double[] subValues = new double[count - f];
            for (int i = f; i < count; ++i)
            {
                subDates[i - f] = Dates[i];
                subValues[i - f] = Values[i];
            }
            return new SummaryVector(this.VectorName, this.ID, this.Units, this.Info, subDates, subValues);
        }



        public SummaryVector SubVector(DateTime firstDate, DateTime lastDate)
        {
            int count = Dates.Count();
            int f, l;
            for (f = 0; f < count; ++f)
                if (Dates[f] >= firstDate)
                    break;
            for (l = f; l < count; ++l)
                if (Dates[l] >= lastDate)
                    break;
            DateTime[] subDates = new DateTime[l - f + 1];
            double[] subValues = new double[l - f + 1];
            for (int i = f; i <= l; ++i)
            {
                subDates[i - f] = Dates[i];
                subValues[i - f] = Values[i];
            }
            return new SummaryVector(this.VectorName, this.ID, this.Units, this.Info, subDates, subValues);
        }



        public void Exclude(double[] values, out DateTime[] excluded)
        {
            List<DateTime> e = new List<DateTime>();
            List<double> v = new List<double>();
            List<DateTime> d = new List<DateTime>();
            for (int i = 0; i < Values.Length; ++i)
                if(values.Contains(Values[i]))
                {
                    e.Add(Dates[i]);
                }
                else
                {
                    v.Add(Values[i]);
                    d.Add(Dates[i]);
                }
            Values = v.ToArray();
            Dates = d.ToArray();
            excluded = e.ToArray();
        }


        public void Exclude(DateTime[] dates, out double[] excluded)
        {
            List<double> e = new List<double>();
            List<double> v = new List<double>();
            List<DateTime> d = new List<DateTime>();
            for (int i = 0; i < Values.Length; ++i)
                if (dates.Contains(Dates[i]))
                {
                    v.Add(Values[i]);
                }
                else
                {
                    v.Add(Values[i]);
                    d.Add(Dates[i]);
                }
            Values = v.ToArray();
            Dates = d.ToArray();
            excluded = e.ToArray();
        }



        public enum CombineType { add_if_not_exist, replace_if_exist }

        public SummaryVector Combine(SummaryVector other, CombineType type)
        {
            DateTime[] dates = this.Dates.Union(other.Dates).ToArray();
            SummaryVector result = new SummaryVector
            {
                VectorName = this.VectorName,
                ID = this.ID,
                Info = this.Info,
                Units = this.Units,
                Dates = dates,
                Values = new double[dates.Length]
            };

            for (int i = 0; i < dates.Length; ++i)
            {
                DateTime dt = dates[i];
                bool this_contains = this.Dates.Contains(dt);
                bool other_cantains = other.Dates.Contains(dt);
                if (this_contains && other_cantains)
                {
                    switch (type)
                    {
                        case CombineType.add_if_not_exist:
                            result.Values[i] = this.Value(dt);
                            break;
                        case CombineType.replace_if_exist:
                            result.Values[i] = other.Value(dt);
                            break;
                    }
                }
                else if (this_contains)
                {
                    result.Values[i] = this.Value(dt);
                }
                else
                {
                    result.Values[i] = other.Value(dt);
                }
            }
            return result;
        }



    }




}
