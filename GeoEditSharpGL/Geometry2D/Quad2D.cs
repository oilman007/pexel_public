using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pexel
{

    /*
     *  1--2
     *  |  |
     *  0--3
     * */

    public class Quad2D
    {
        public Point2D[] Corners { get; set; } = new Point2D[4];
        public bool Checked { set; get; } = true;
        public Color Color { set; get; } = Color.Black;
        public string Title { set; get; } = string.Empty;
        public Point2D Center()
        {
            return new Point2D((Corners[0].X + Corners[1].X + Corners[2].X + Corners[3].X) / 4, 
                               (Corners[0].Y + Corners[1].Y + Corners[2].Y + Corners[3].Y) / 4);
        }
        public double Square() // TODO
        {
            return 0f;
        }
        public static double Square(List<Quad2D> quads)
        {
            double result = 0f;
            foreach (Quad2D item in quads) if (item.Checked) result += item.Square();
            return result;
        }

        public bool Contains(Point2D p)
        {
            return InsideTriangle(Corners[0], Corners[1], Corners[3], p) ||
                   InsideTriangle(Corners[2], Corners[1], Corners[3], p) ;
        }

        static bool InsideTriangle(Point2D p0, Point2D p1, Point2D p2, Point2D p)
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
