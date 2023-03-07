using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.HM
{
    public class PointValue
    {
        public PointValue() { Point.X = 0; Point.Y = 0; Point.Z = 0; Value = 0; }
        public PointValue(double x, double y, double z, double value) { Point.X = x; Point.Y = y; Point.Z = z; Value = value; }
        public PointValue(Point3D point, double value) { Point = point; Value = value; }

       public double Value { set; get; }

        public double X { set { Point.X = value; } get { return Point.X; } }
        public double Y { set { Point.Y = value; } get { return Point.Y; } }
        public double Z { set { Point.Z = value; } get { return Point.Z; } }

        Point3D Point { set; get; } = new Point3D();


    }
}
