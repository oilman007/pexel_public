using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pexel.HM
{


    public class CellValue
    {
        public CellValue() { Index.I = 0; Index.J = 0; Index.K = 0; Value = 0; }
        public CellValue(int i, int j, int k, double value, string comment = "") { Index.I = i; Index.J = j; Index.K = k; Value = value; Comment = comment; }
        public CellValue(Index3D index, double value, string comment = "") { Index = index; Value = value; Comment = comment; }

        public double Value { set; get; }
        public string Comment { set; get; }

        public Index3D Index { set; get; } = new Index3D();

        public static List<CellValue> Values(string file)
        {
            string[] lines = Helper.ClearLines(file);
            List<CellValue> result = new List<CellValue>();
            foreach(string line in lines)
            {
                string[] split = line.Split();
                if (split.Length == 4)
                {
                    int i = Helper.ParseInt(split[0]) - 1;
                    int j = Helper.ParseInt(split[1]) - 1;
                    int k = Helper.ParseInt(split[2]) - 1;
                    double value = Helper.ParseDouble(split[3]);
                    result.Add(new CellValue(i, j, k, value));
                }
                else if (split.Length == 5)
                {
                    int i  = Helper.ParseInt(split[0]) - 1;
                    int j  = Helper.ParseInt(split[1]) - 1;
                    int k1 = Helper.ParseInt(split[2]) - 1;
                    int k2 = Helper.ParseInt(split[3]) - 1;
                    double value = Helper.ParseDouble(split[4]);
                    for (int k = k1; k <= k2; ++k)
                        result.Add(new CellValue(i, j, k, value));
                }
            }
            return result;
        }


        public static Dictionary<int, CellValue> KValues(string file)
        {
            Dictionary<int, CellValue> result = new Dictionary<int, CellValue>();
            foreach (CellValue value in Values(file)) result.Add(value.Index.K, value);
            return result;
        }



        public string Log()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.string.format?view=netcore-3.1
            return string.Format("{4} {0,6} {1,6} {2,6} {3,20}", (Index.I + 1), (Index.J + 1), (Index.K + 1), Value, Comment);
        }



    }

}
