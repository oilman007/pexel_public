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
    public class Point2I : IEquatable<Point2I>
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

        public override bool Equals(object obj) => this.Equals(obj as Point2I);

        public bool Equals(Point2I p)
        {
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode() => (X, Y).GetHashCode();


        /*
        public static bool operator ==(Point2I lhs, Point2I rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Point2I lhs, Point2I rhs) => !(lhs == rhs);
        */
    }


}

