using Pexel.Geometry2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pexel
{
    [Serializable]
    public class Polygon2D : IViewable2D
    {
        public List<Point2D> Points { set; get; } = new List<Point2D>();
        public Color Color { set; get; } = Color.Yellow;


        public string Title { set; get; } = string.Empty;
        public bool Closed { set; get; } = false;
        public double Value { set; get; } = 0;
        public static List<Polygon2D> Read(string file, out bool successfully)
        {
            List<Polygon2D> result = new List<Polygon2D>();
            int i = 0;
            string title = System.IO.Path.GetFileNameWithoutExtension(file);
            result.Add(new Polygon2D() { Title = title + $"_{++i}" });
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
                    if (x == end && y == end && z == end)
                        result.Add(new Polygon2D() { Title = title + $"_{++i}" });
                    else
                        result.Last().Points.Add(new Point2D(x, y));
                }
                result.RemoveAt(result.Count - 1);
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

        public bool Surrounds(Point2D point)
        {
            return PointInPolygon(point, this.Points.ToArray());
        }      

        static bool PointInPolygon(Point2D point, Point2D[] poly)
        {
            //\\// pre proc
            double[] constant = new double[poly.Length];
            double[] multiple = new double[poly.Length];
            int i, j = poly.Length - 1;
            for (i = 0; i < poly.Length; i++)
            {
                if (poly[j].Y == poly[i].Y)
                {
                    constant[i] = poly[i].X;
                    multiple[i] = 0;
                }
                else
                {
                    constant[i] = poly[i].X - (poly[i].Y * poly[j].X) / (poly[j].Y - poly[i].Y) + (poly[i].Y * poly[i].X) / (poly[j].Y - poly[i].Y);
                    multiple[i] = (poly[j].X - poly[i].X) / (poly[j].Y - poly[i].Y);
                }
                j = i;
            }
            //\\// proc
            bool oddNodes = false, current = poly[poly.Length - 1].Y > point.Y, previous;
            for (i = 0; i < poly.Length; i++)
            {
                previous = current;
                current = poly[i].Y > point.Y;
                if (current != previous) oddNodes ^= point.Y * multiple[i] + constant[i] < point.X;
            }
            return oddNodes;
        }



        public void GetBoundaries(out double minX, out double maxX, out double minY, out double maxY)
        {
            if (Points.Count == 0)
            {
                minX = 0;
                minY = 0;
                maxX = 0;
                maxY = 0;
            }
            else
            {
                minX = Points[0].X;
                maxX = Points[0].X;
                minY = Points[0].Y;
                maxY = Points[0].Y;
                for (int i = 1; i < Points.Count; ++i)
                {
                    if (minX > Points[i].X) minX = Points[i].X;
                    else
                    if (maxX < Points[i].X) maxX = Points[i].X;

                    if (minY > Points[i].Y) minY = Points[i].Y;
                    else
                    if (maxY < Points[i].Y) maxY = Points[i].Y;
                }
            }
        }



        public bool HasIntersection(Line2D line)
        {
            Point2D intersection;
            for (int i = 1; i < this.Points.Count; ++i)
                if ((new Line2D(Points[i - 1], Points[i])).Intersect(line, out intersection))
                    return true;
            if (Closed && (Points.Count > 2) && (new Line2D(Points[0], Points[Points.Count - 1])).Intersect(line, out intersection))
                return true;
            return false;
        }



    }
}
