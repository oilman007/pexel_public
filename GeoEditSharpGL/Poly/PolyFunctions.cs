using MathNet.Numerics;
using System;
using System.Drawing;
using System.Linq;

namespace Pexel
{
    class PolyFunctions
    {
        public static double[] Coef(Point2D[] points, int n, out double R2)
        {
            var xdata = new double[points.Length];
            var ydata = new double[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                var p = points[i];
                xdata[i] = p.X;
                ydata[i] = p.Y;
            }
            var poly = Polynomial.Fit(xdata, ydata, n);
            var obs = poly.Evaluate(xdata);
            R2 = GoodnessOfFit.RSquared(ydata, obs);

            return poly.Coefficients.Select(c => c).ToArray();
        }

        public static Point2D GetYBy(double x, double[] coef)
        {
            var y = Polynomial.Evaluate(x, Array.ConvertAll(coef, c => c));
            //var y = Polynomial.Evaluate(x, coef);
            return new Point2D(x, y);
        }





        public static double[] Coef(double[] xdata, double[] ydata, int n, out double R2)
        {
            var poly = Polynomial.Fit(xdata, ydata, n);
            var obs = poly.Evaluate(xdata);
            R2 = GoodnessOfFit.RSquared(ydata, obs);
            return poly.Coefficients.Select(c => c).ToArray();
        }


    }
}
