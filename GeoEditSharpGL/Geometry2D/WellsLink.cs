using Pexel.Geometry2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pexel
{
    [Serializable]
    public class WellsLink : IViewable2D
    {
        public WellsLink()
        {
            W1 = new WellFace2D();
            W2 = new WellFace2D();
        }


        public string Title { set; get; } = string.Empty;
        public WellFace2D W1 { set; get; }
        public WellFace2D W2 { set; get; }
        public Color Color { set; get; } = Color.Black;


        public double Distance()
        {
            return Point2D.Distance(W1.Point, W2.Point);
        }



        public static bool Intersect(WellsLink l1, WellsLink l2, out Point2D i, bool exclude_endpoints = true)
        {
            if (!Intersect(l1.W1.Point, l1.W2.Point, l2.W1.Point, l2.W2.Point, out i, 0.001)) 
                return false;
            if (exclude_endpoints)
                return (l1.W1 != l2.W1) && (l1.W1 != l2.W2) &&
                       (l1.W2 != l2.W1) && (l1.W2 != l2.W2);
            else 
                return true;
        }




        public static bool Intersect(Point2D line1V1, Point2D line1V2, Point2D line2V1, Point2D line2V2,
                                       out Point2D intersection, bool exclude_endpoints, double tolerance = 0.001)
        {
            if (!Intersect(line1V1, line1V2, line2V1, line2V2,
                                               out intersection, 0.001))
                return false;
            if (exclude_endpoints)
                return (line1V1 != line2V1) && (line1V1 != line2V2) &&
                       (line1V2 != line2V1) && (line1V2 != line2V2);
            else
                return true;
        }




        //  Returns Point of intersection if do intersect otherwise default Point (null)
        public static bool Intersect(Point2D line1V1, Point2D line1V2, Point2D line2V1, Point2D line2V2, 
                                               out Point2D intersection, double tolerance = 0.001)
        {
            double x1 = line1V1.X, y1 = line1V1.Y;
            double x2 = line1V2.X, y2 = line1V2.Y;

            double x3 = line2V1.X, y3 = line2V1.Y;
            double x4 = line2V2.X, y4 = line2V2.Y;
            
            intersection = null;

            // equations of the form x = c (two vertical lines)
            if (Math.Abs(x1 - x2) < tolerance && Math.Abs(x3 - x4) < tolerance && Math.Abs(x1 - x3) < tolerance)
            {
                return false;
                //throw new Exception("Both lines overlap vertically, ambiguous intersection points.");
            }

            //equations of the form y=c (two horizontal lines)
            if (Math.Abs(y1 - y2) < tolerance && Math.Abs(y3 - y4) < tolerance && Math.Abs(y1 - y3) < tolerance)
            {
                return false;
                //throw new Exception("Both lines overlap horizontally, ambiguous intersection points.");
            }

            //equations of the form x=c (two vertical lines)
            if (Math.Abs(x1 - x2) < tolerance && Math.Abs(x3 - x4) < tolerance)
            {
                return false;
            }

            //equations of the form y=c (two horizontal lines)
            if (Math.Abs(y1 - y2) < tolerance && Math.Abs(y3 - y4) < tolerance)
            {
                return false;
            }

            //general equation of line is y = mx + c where m is the slope
            //assume equation of line 1 as y1 = m1x1 + c1 
            //=> -m1x1 + y1 = c1 ----(1)
            //assume equation of line 2 as y2 = m2x2 + c2
            //=> -m2x2 + y2 = c2 -----(2)
            //if line 1 and 2 intersect then x1=x2=x & y1=y2=y where (x,y) is the intersection point
            //so we will get below two equations 
            //-m1x + y = c1 --------(3)
            //-m2x + y = c2 --------(4)

            double x, y;

            //lineA is vertical x1 = x2
            //slope will be infinity
            //so lets derive another solution
            if (Math.Abs(x1 - x2) < tolerance)
            {
                //compute slope of line 2 (m2) and c2
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                //equation of vertical line is x = c
                //if line 1 and 2 intersect then x1=c1=x
                //subsitute x=x1 in (4) => -m2x1 + y = c2
                // => y = c2 + m2x1 
                x = x1;
                y = c2 + m2 * x1;
            }
            //lineB is vertical x3 = x4
            //slope will be infinity
            //so lets derive another solution
            else if (Math.Abs(x3 - x4) < tolerance)
            {
                //compute slope of line 1 (m1) and c2
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                //equation of vertical line is x = c
                //if line 1 and 2 intersect then x3=c3=x
                //subsitute x=x3 in (3) => -m1x3 + y = c1
                // => y = c1 + m1x3 
                x = x3;
                y = c1 + m1 * x3;
            }
            //lineA & lineB are not vertical 
            //(could be horizontal we can handle it with slope = 0)
            else
            {
                //compute slope of line 1 (m1) and c2
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                //compute slope of line 2 (m2) and c2
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                //solving equations (3) & (4) => x = (c1-c2)/(m2-m1)
                //plugging x value in equation (4) => y = c2 + m2 * x
                x = (c1 - c2) / (m2 - m1);
                y = c2 + m2 * x;

                //verify by plugging intersection point (x, y)
                //in orginal equations (1) & (2) to see if they intersect
                //otherwise x,y values will not be finite and will fail this check
                if (!(Math.Abs(-m1 * x + y - c1) < tolerance
                    && Math.Abs(-m2 * x + y - c2) < tolerance))
                {
                    return false;
                }
            }

            //x,y can intersect outside the line segment since line is infinitely long
            //so finally check if x, y is within both the line segments
            if (IsInsideLine(line1V1, line1V2, x, y) &&
                IsInsideLine(line2V1, line2V2, x, y))
            {
                intersection = new Point2D(x, y);
                return true;
            }

            //return default null (no intersection)
            return false;

        }

        // Returns true if given point(x,y) is inside the given line segment
        private static bool IsInsideLine(Point2D p1, Point2D p2, double x, double y)
        {
            return (x >= p1.X && x <= p2.X || x >= p2.X && x <= p1.X) && 
                   (y >= p1.Y && y <= p2.Y || y >= p2.Y && y <= p1.Y);
        }





        public Point2D Center()
        {
            return (W1.Point + W2.Point) / 2;
        }



        public Line2D Line()
        {
            return new Line2D(W1.Point, W2.Point);
        }



    }
}

