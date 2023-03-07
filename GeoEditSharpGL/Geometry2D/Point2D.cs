using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Pexel
{
    [Serializable]
    public class Point2D
    {
        public Point2D()
        {
            X = 0.0;
            Y = 0.0;
        }
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point2D(Point2D point)
        {
            X = point.X;
            Y = point.Y;
        }
        public double X { set; get; }
        public double Y { set; get; }



        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }

        public static Point2D Read(BinaryReader reader)
        {
            return new Point2D(reader.ReadDouble(), reader.ReadDouble());
        }


        public string ToString(int digits = -1)
        {
            if (digits < 0)
                return Helper.ShowDouble(X) + " " + Helper.ShowDouble(Y);
            else
                return Helper.ShowDouble(Math.Round(X, digits)) + " " + Helper.ShowDouble(Math.Round(Y, digits));
        }

        static public Point2D operator +(Point2D p1, Point2D p2)
        {
            return new Point2D((p1.X + p2.X), (p1.Y + p2.Y));
        }
        static public Point2D operator -(Point2D p1, Point2D p2)
        {
            return new Point2D((p1.X - p2.X), (p1.Y - p2.Y));
        }
        static public Point2D operator /(Point2D p1, double div)
        {
            return new Point2D(p1.X / div, p1.Y / div);
        }
        static public Point2D operator *(Point2D p1, double mult)
        {
            return new Point2D(p1.X * mult, p1.Y * mult);
        }
        static public bool operator ==(Point2D p1, Point2D p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        static public bool operator !=(Point2D p1, Point2D p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y;
        }


        public static Point2D Average(params Point2D[] points)
        {
            if (points.Length == 0) return null;
            double x = 0, y = 0;
            foreach (Point2D p in points)
            {
                x += p.X;
                y += p.Y;
            }
            return new Point2D(x / points.Length, 
                               y / points.Length);
        }





    }
}
