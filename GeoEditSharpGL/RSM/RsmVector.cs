using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum CombineType { ADD_IF_NOT_EXIST, REPLACE_IF_EXIST, MULTIPLY, DIVIDE }


namespace Pexel.RSM
{
    [Serializable]
    public class RsmVector
    {
        public RsmVector() { }

        public RsmVector(int n, double v) 
        {
            Values = new double[n];
            for (int i = 0; i < n; ++i) Values[i] = v;
        }

        public RsmVector(string title, string id, string units, string info, double[] values)
        {
            Title = title;
            ID = id;
            Units = units;
            Info = info;
            Values = values;
        }

        public double[] Values { set; get; } = Array.Empty<double>();
        public string Title { set; get; } = string.Empty;
        public string ID { set; get; } = string.Empty;
        public string Info { set; get; } = string.Empty;
        public string Units { set; get; } = string.Empty;





        public RsmVectorType MyType
        {
            get
            {
                RsmVectorType result = RsmVectorType.Unknown;
                if (Title.Length != 0)
                    switch (Title[0])
                    {
                        case 'F': result = RsmVectorType.Field; break;
                        case 'G': result = RsmVectorType.Group; break;
                        case 'W': result = RsmVectorType.Well; break;
                        case 'C': result = RsmVectorType.Connection; break;
                        case 'R': result = RsmVectorType.Region; break;
                        case 'B': result = RsmVectorType.Block; break;
                        case 'A': result = RsmVectorType.Aquifer; break;
                    }
                return result;
            }
        }







        public RsmVector Combine(RsmVector other, CombineType type)
        {
            const double empty = 0;
            RsmVector result = new RsmVector
            {
                Title = this.Title,
                ID = this.ID,
                Info = this.Info,
                Units = this.Units,
                Values = new double[this.Values.Length]
            };

            for (int i = 0; i < this.Values.Length; ++i)
                result.Values[i] = this.Values[i];

            for (int i = 0; i < this.Values.Length; ++i)
            {
                bool this_contains = this.Values[i] != empty;
                bool other_cantains = other.Values[i] != empty;

                switch (type)
                {
                    case CombineType.ADD_IF_NOT_EXIST:
                        if (this_contains && other_cantains)
                            result.Values[i] = this.Values[i];
                        else if (this_contains)
                            result.Values[i] = this.Values[i];
                        else
                            result.Values[i] = other.Values[i];
                        break;
                    case CombineType.REPLACE_IF_EXIST:
                        if (this_contains && other_cantains)
                            result.Values[i] = other.Values[i];
                        else if (this_contains)
                            result.Values[i] = this.Values[i];
                        else
                            result.Values[i] = other.Values[i];
                        break;
                    case CombineType.MULTIPLY:
                            result.Values[i] *= other.Values[i];
                        break;
                    case CombineType.DIVIDE:
                        result.Values[i] = other.Values[i] == 0 ? 0 : result.Values[i] / other.Values[i];
                        break;
                }
            }

            return result;
        }







    }








}
