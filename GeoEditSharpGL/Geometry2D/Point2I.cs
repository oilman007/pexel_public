using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Pexel
{
    public class Point2I
    {

        public Point2I()
        {
            X = 0;
            Y = 0;
        }
        public Point2I(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point2I(Point2I point)
        {
            X = point.X;
            Y = point.Y;
        }
        public int X { set; get; }
        public int Y { set; get; }



        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }

        public static Point2I Read(BinaryReader reader)
        {
            return new Point2I(reader.ReadInt32(), reader.ReadInt32());
        }


        public string ToString()
        {
            return Helper.ShowInt(X) + " " + Helper.ShowInt(Y);
        }

        static public Point2I operator +(Point2I p1, Point2I p2)
        {
            return new Point2I((p1.X + p2.X), (p1.Y + p2.Y));
        }
        static public Point2I operator -(Point2I p1, Point2I p2)
        {
            return new Point2I((p1.X - p2.X), (p1.Y - p2.Y));
        }
        static public bool operator ==(Point2I p1, Point2I p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        static public bool operator !=(Point2I p1, Point2I p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y;
        }




    }
}
