using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel
{
    public class Table2D
    {
        public Table2D()
        {
        }

        public Table2D(double[] xValues, double[] yValues, double yDefValue)
        {
            YDefValue = yDefValue;
            XValues = xValues;
            YValues = yValues;
        }

        public double YDefValue { set; get; } = -999;
        public double[] XValues { set; get; } = Array.Empty<double>();
        public double[] YValues { set; get; } = Array.Empty<double>();


        public double GetY(double x)
        {
            if (XValues.Length == 0 || YValues.Length == 0 || XValues.Length != YValues.Length)
                return YDefValue;

            if (YValues.Length == 1)
                return YValues[0];

            bool stop;
            stop = false;
            int iv = 0;
            for (; !stop && iv < XValues.Length; ++iv)
                if (x == XValues[iv])
                    if (YValues[iv] == YDefValue)
                        break;
                    else
                        return YValues[iv];

            List<int> il = new List<int>();
            for (int i = iv; i >= 0 && il.Count < 2; --i)
                if (YValues[i] != YDefValue)
                    il.Add(i);

            List<int> iu = new List<int>();
            for (int i = iv; i < XValues.Length && iu.Count < 2; ++i)
                if (YValues[i] != YDefValue)
                    iu.Add(i);

            double xl, yl, xu, yu;

            if (il.Count > 0 && iu.Count > 0)
            {
                xl = XValues[il[0]];
                yl = YValues[il[0]];
                xu = XValues[iu[0]];
                yu = YValues[iu[0]];
            }
            else if (il.Count > 1)
            {
                xl = XValues[il[1]];
                yl = YValues[il[1]];
                xu = XValues[il[0]];
                yu = YValues[il[0]];
            }
            else if (iu.Count > 1)
            {
                xl = XValues[iu[0]];
                yl = YValues[iu[0]];
                xu = XValues[iu[1]];
                yu = YValues[iu[1]];
            }
            else if (il.Count == 1)
            {
                return YValues[il[0]];
            }
            else if (iu.Count == 1)
            {
                return YValues[iu[0]];
            }
            else
            {
                return YDefValue;
            }

            double k = (yu - yl) / (xu - xl);
            double b = yl - k * xl;
            double r = k * x + b;

            return r;
        }


    }
}
