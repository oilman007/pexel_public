using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Pexel
{
    public class Point3D
    {
        public Point3D()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
        }
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D(Point3D point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
        public Point2D ToPoint2D(CoordPlane plane)
        {
            switch(plane)
            {
                case CoordPlane.XY:
                    return new Point2D(X, Y);
                case CoordPlane.XZ:
                    return new Point2D(X, Z);
                default: //CoordPlane.XY:
                    return new Point2D(Y, Z);
            }
        }
        public Point2D Point2D(CoordPlane plane = CoordPlane.XY)
        {
            switch (plane)
            {
                case CoordPlane.XY:
                    return new Point2D(X, Y);
                case CoordPlane.XZ:
                    return new Point2D(X, Z);
                default: //CoordPlane.XY:
                    return new Point2D(Y, Z);
            }
        }
        static public Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D((p1.X + p2.X), (p1.Y + p2.Y), (p1.Z + p2.Z));
        }
        static public Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D((p1.X - p2.X), (p1.Y - p2.Y), (p1.Z - p2.Z));
        }
        static public Point3D operator /(Point3D p1, double div)
        {
            return new Point3D(p1.X / div, p1.Y / div, p1.Z / div);
        }
        static public Point3D operator *(Point3D p1, double mult)
        {
            return new Point3D(p1.X * mult, p1.Y * mult, p1.Z * mult);
        }


        override public string ToString()
        {
            return Helper.ShowDouble(X) + " " + Helper.ShowDouble(Y) + " " + Helper.ShowDouble(Z);
        }


        public bool Use(double[,] matrix)
        {
            if (matrix.GetLength(0) != 3 && matrix.GetLength(1) != 3)
                return false;
            X = matrix[0, 0] * X + matrix[1, 0] * Y + matrix[2, 0];
            Y = matrix[0, 1] * X + matrix[1, 1] * Y + matrix[2, 1];
            Z = matrix[0, 2] * X + matrix[1, 2] * Y + matrix[2, 2];
            return true;
        }



        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }

        public static Point3D Read(BinaryReader reader)
        {
            return new Point3D(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }






        public static List<Point3D> Read(string file, out bool successfully)
        {
            List<Point3D> result = new List<Point3D>();
            try
            {
                const double end = 999;
                string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));
                foreach (string line in lines)
                {
                    string cline = ClearLine(line, "--");
                    if (string.IsNullOrEmpty(cline)) continue;
                    string[] split = cline.Split();
                    if (split.Length != 3) continue;
                    double x = Helper.ParseDouble(split[0]);
                    double y = Helper.ParseDouble(split[1]);
                    double z = Helper.ParseDouble(split[2]);
                    result.Add(new Point3D(x, y, z));
                }
            }
            catch (Exception ex)
            {
                successfully = false;
                return result;
            }
            successfully = true;
            return result;
        }

        static string ClearLine(string line, string remString)
        {
            const string tabString = "\t";
            const string singlSpace = " ";
            const string doubleSpace = "  ";

            int index = line.IndexOf(remString);
            if (index != -1)
                line = line.Remove(index);
            line = line.Replace(tabString, singlSpace);
            while (line.Contains(doubleSpace))
                line = line.Replace(doubleSpace, singlSpace);
            line = line.Trim();
            line = line.ToUpper();

            return line;
        }





    }
}
