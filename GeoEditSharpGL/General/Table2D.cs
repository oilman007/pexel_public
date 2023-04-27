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

        public Table2D(double[] x_vals, double[] y_vals, double yDefValue)
        {
            if (x_vals is null || y_vals is null || x_vals.Length != y_vals.Length)
                return;

            Point2D[] points = new Point2D[x_vals.Length];
            for (int i = 0; i < points.Length; i++)
                points[i] = new Point2D(x_vals[i], y_vals[i]);

            YDefValue = yDefValue;
            double[] x_dist = points.Select(v => v.X).Distinct().OrderBy(x => x).ToArray();
            Points = new Point2D[x_dist.Length];
            for (int i = 0; i < x_dist.Length; i++)
            {
                Points[i] = new Point2D();
                Points[i].X = x_dist[i];
                double[] y_temp = points.Where(v => v.X == x_dist[i]).Select(v => v.Y).Where(y => y != yDefValue).ToArray();
                if (y_temp.Length == 0)
                    Points[i].Y = yDefValue;
                else
                    Points[i].Y = y_temp.Average();
            }
        }

        public double YDefValue { set; get; } = -999;
        public Point2D[] Points { set; get; } = Array.Empty<Point2D>();
        //public double[] XValues { set; get; } = Array.Empty<double>();
        //public double[] YValues { set; get; } = Array.Empty<double>();


        public double GetY(double x)
        {
            if (Points.Length == 0)
                return YDefValue;

            if (Points.Length == 1)
                return Points[0].Y;

            bool stop;
            stop = false;
            int iv = 0;
            for (; !stop && iv < Points.Length; ++iv)
                if (x == Points[iv].X)
                    if (Points[iv].Y == YDefValue)
                        break;
                    else
                        return Points[iv].Y;

            List<int> il = new List<int>();
            for (int i = iv; i >= 0 && il.Count < 2; --i)
                if (Points[i].Y != YDefValue)
                    il.Add(i);

            List<int> iu = new List<int>();
            for (int i = iv; i < Points.Length && iu.Count < 2; ++i)
                if (Points[i].Y != YDefValue)
                    iu.Add(i);

            double xl, yl, xu, yu;

            if (il.Count > 0 && iu.Count > 0)
            {
                xl = Points[il[0]].X;
                yl = Points[il[0]].Y;
                xu = Points[iu[0]].X;
                yu = Points[iu[0]].Y;
            }
            else if (il.Count > 1)
            {
                xl = Points[il[1]].X;
                yl = Points[il[1]].Y;
                xu = Points[il[0]].X;
                yu = Points[il[0]].Y;
            }
            else if (iu.Count > 1)
            {
                xl = Points[iu[0]].X;
                yl = Points[iu[0]].Y;
                xu = Points[iu[1]].X;
                yu = Points[iu[1]].Y;
            }
            else if (il.Count == 1)
            {
                return Points[il[0]].Y;
            }
            else if (iu.Count == 1)
            {
                return Points[iu[0]].Y;
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
