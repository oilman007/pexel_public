using Pexel.Geometry2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Pexel
{
    //[Serializable]
    public class Triangle2D : IViewable2D
    {
        public Triangle2D()
        {
            Corners[0] = new Point2D();
            Corners[1] = new Point2D();
            Corners[2] = new Point2D();
        }

        public Triangle2D(Point2D p1, Point2D p2, Point2D p3)
        {
            Corners[0] = p1;
            Corners[1] = p2;
            Corners[2] = p3;
        }

        public Triangle2D(DelaunayVoronoi.Triangle triangle)
        {
            Corners[0] = new Point2D(triangle.Vertices[0].X, triangle.Vertices[0].Y);
            Corners[1] = new Point2D(triangle.Vertices[1].X, triangle.Vertices[1].Y);
            Corners[2] = new Point2D(triangle.Vertices[2].X, triangle.Vertices[2].Y);
        }

        public Point2D[] Corners { get; set; } = new Point2D[3];

        public Color Color { set; get; } = Color.Black;

        public Point2D Center()
        {
            return new Point2D((Corners[0].X + Corners[1].X + Corners[2].X) / 3, (Corners[0].Y + Corners[1].Y + Corners[2].Y) / 3);
        }
        public double Square()
        {
            double a = Distance(Corners[0], Corners[1]);
            double b = Distance(Corners[1], Corners[2]);
            double c = Distance(Corners[2], Corners[0]);
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static double Square(List<Triangle2D> triangles)
        {
            double result = 0;
            foreach (Triangle2D triangle in triangles) if (triangle.Visible) result += triangle.Square();
            return result;
        }
        double Distance(Point2D p1, Point2D p2  )
        {
            double x = p1.X - p2.X;
            double y = p1.Y - p2.Y;
            return Math.Sqrt(x * x + y * y);
        }


        public bool Contains(Point2D p)
        {
            return InsideTriangle(Corners[0], Corners[1], Corners[2], p);
        }

        public static bool InsideTriangle(Point2D p0, Point2D p1, Point2D p2, Point2D p)
        {
            bool cp0 = CrossProduct(p0, p1, p) < 0.0;
            bool cp1 = CrossProduct(p1, p2, p) < 0.0;
            bool cp2 = CrossProduct(p2, p0, p) < 0.0;
            return cp0 == cp1 && cp1 == cp2 && cp2 == cp0;
        }

        static double CrossProduct(Point2D p0, Point2D p1, Point2D p)
        {
            return (p.X - p1.X) * (p1.Y - p0.Y) - (p.Y - p1.Y) * (p1.X - p0.X);
        }
    }
}
