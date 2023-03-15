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
    public class Index2D : IEquatable<Index2D>
    {

        public Index2D()
        {
            I = 0;
            J = 0;
        }
        public Index2D(int x, int y)
        {
            I = x;
            J = y;
        }
        public Index2D(Index2D point)
        {
            I = point.I;
            J = point.J;
        }
        public int I { set; get; }
        public int J { set; get; }



        public void Write(BinaryWriter writer)
        {
            writer.Write(I);
            writer.Write(J);
        }

        public static Index2D Read(BinaryReader reader)
        {
            return new Index2D(reader.ReadInt32(), reader.ReadInt32());
        }


        public string ToString()
        {
            return Helper.ShowInt(I) + " " + Helper.ShowInt(J);
        }

        static public Index2D operator +(Index2D p1, Index2D p2)
        {
            return new Index2D((p1.I + p2.I), (p1.J + p2.J));
        }
        static public Index2D operator -(Index2D p1, Index2D p2)
        {
            return new Index2D((p1.I - p2.I), (p1.J - p2.J));
        }
        static public bool operator ==(Index2D p1, Index2D p2)
        {
            return p1.I == p2.I && p1.J == p2.J;
        }
        static public bool operator !=(Index2D p1, Index2D p2)
        {
            return p1.I != p2.I || p1.J != p2.J;
        }

        public override bool Equals(object obj) => this.Equals(obj as Index2D);

        public bool Equals(Index2D p)
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
            return (I == p.I) && (J == p.J);
        }

        public override int GetHashCode() => (I, J).GetHashCode();


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

