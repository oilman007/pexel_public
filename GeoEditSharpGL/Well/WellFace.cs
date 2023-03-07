using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace Pexel
{
    public class WellFace
    {
        public WellFace()
        {
            Title       = string.Empty;
            Trajectory  = new List<Point3D>();
            Checked = false;
        }
        public WellFace(string title, List<Point3D> trajectory, bool check)
        {
            Title = title;
            Trajectory = trajectory;
            Checked = check;
        }
        public string Title { set; get; }
        public List<Point3D> Trajectory { set; get; }
        public bool Checked { set; get; }

        public double MinX()
        {
            double result = Trajectory[0].X;
            foreach (Point3D point in Trajectory)
                if (result > point.X)
                    result = point.X;
            return result;
        }
        public double MaxX()
        {
            double result = Trajectory[0].X;
            foreach (Point3D point in Trajectory)
                if (result < point.X)
                    result = point.X;
            return result;
        }
        public double MinY()
        {
            double result = Trajectory[0].Y;
            foreach (Point3D point in Trajectory)
                if (result > point.Y)
                    result = point.Y;
            return result;
        }
        public double MaxY()
        {
            double result = Trajectory[0].Y;
            foreach (Point3D point in Trajectory)
                if (result < point.Y)
                    result = point.Y;
            return result;
        }
    }
}
