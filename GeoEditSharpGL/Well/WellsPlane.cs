using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace Pexel
{
    public class WellsPlane
    {
        public WellsPlane()
        {
            Faces = new List<WellFace>();
        }


        /*
            Parallel.For(0, ny, j =>
            {
                lock(new object())
                {
                }
            });
        */

        public WellsPlane(Grid grid, int[] layers)
        {
            Faces = new List<WellFace>();
            int nx = grid.NX(), ny = grid.NY(), nz = grid.NZ(), wcount = grid.Compdats.Count();
            List<int> list = layers.ToList();
            //for (int w = 0; w < wcount; ++w)
            Parallel.For(0, wcount, w =>
            {
                //if (grid.Wells[w].Checked)
                //{
                    List<Point3D> intersections = new List<Point3D>();
                    foreach (Index3D con in grid.Compdats[w].Connections)
                        if (list.Contains(con.K)) intersections.Add(grid.LocalCell(con.I, con.J, con.K).MiddleTopFace.Center());
                    if (intersections.Count() > 0)
                        lock (Faces) { Faces.Add(new WellFace(grid.Compdats[w].Title, intersections, grid.Compdats[w].Checked)); }
                //}
            });
        }

        public List<WellFace> Faces { set; get; }


        public string WellNames(Point2D center, double radius)
        {
            List<string> result = new List<string>();
            //Parallel.ForEach(Faces, face =>
            foreach (WellFace face in Faces)
            {
                //foreach (Point3D t in face.Trajectory)
                Parallel.ForEach(face.Trajectory, (t, state) =>
                {
                    double dx = t.X - center.X;
                    double dy = t.Y - center.Y;
                    double d = Math.Pow(dx * dx + dy * dy, 0.5);
                    if (d <= radius)
                    {
                        result.Add(face.Title);
                        //break;
                        state.Break();
                    }
                });
            }//);
            return result.Count == 0 ? center.ToString(1) : string.Join("~", result);
        }

        public double MinX()
        {
            double result = Faces[0].MinX();
            foreach (WellFace face in Faces)
                if (result > face.MinX())
                    result = face.MinX();
            return result;
        }
        public double MaxX()
        {
            double result = Faces[0].MaxX();
            foreach (WellFace face in Faces)
                if (result < face.MaxX())
                    result = face.MaxX();
            return result;
        }
        public double MinY()
        {
            double result = Faces[0].MinY();
            foreach (WellFace face in Faces)
                if (result > face.MinY())
                    result = face.MinY();
            return result;
        }
        public double MaxY()
        {
            double result = Faces[0].MaxY();
            foreach (WellFace face in Faces)
                if (result < face.MaxY())
                    result = face.MaxY();
            return result;
        }


    }
}
