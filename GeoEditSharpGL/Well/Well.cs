using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace Pexel
{
    [Serializable]
    public class Well
    {
        public Well()
        {
            Clear();
        }


        public Well(string title, List<InclPoint> trajectory, List<WellEvent> events, bool check = true)
        {
            Clear();
            Title = title;
            Trajectory = trajectory;
            Events = events;
            Checked = check;
        }




        public string Title { set; get; }
        public List<InclPoint> Trajectory { set; get; }
        public List<WellEvent> Events { set; get; }
        public bool Checked { set; get; }

        static long Namber = 1;





        public void Write(BinaryWriter writer)
        {
            writer.Write(Title);
            writer.Write(Trajectory.Count());
            foreach (InclPoint p in Trajectory) p.Write(writer);
            writer.Write(Events.Count());
            foreach(WellEvent e in Events) e.Write(writer);
            writer.Write(Checked);
        }


        public static Well Read(BinaryReader reader)
        {
            string title = reader.ReadString();
            int pcount = reader.ReadInt32();
            List<InclPoint> ps = new List<InclPoint>();
            for (int p = 0; p < pcount; ++p) ps.Add(InclPoint.Read(reader));
            int ecount = reader.ReadInt32();
            List<WellEvent> es = new List<WellEvent>();
            for (int e = 0; e < ecount; ++e) es.Add(WellEvent.Read(reader));
            bool check = reader.ReadBoolean();
            return new Well(title, ps, es, check);
        }




        public void Clear()
        {
            Title = "Well_" + (Namber++).ToString();
            Trajectory = new List<InclPoint>();
            Events = new List<WellEvent>();
            Checked = false;
        }



        public Point2D XY(double tvd)
        {
            for (int i = 1; i < Trajectory.Count(); ++i)
            {
                if (Trajectory[i].TVD >= tvd)
                {
                    InclPoint t = Trajectory[i - 1];
                    InclPoint b = Trajectory[i];
                    Pillar pillar = new Pillar(t.X, t.Y, t.TVD, b.X, b.Y, b.TVD);
                    return pillar.Point2D(tvd);
                }
            }
            return new Point2D();
        }


        public List<Point3D> Intersections(CellFace cellface)
        {
            List<Point3D> result = new List<Point3D>();
            for (int i = 1; i < Trajectory.Count(); ++i)
            {
                Point3D inter;
                if (cellface.Intersect(Trajectory[i - 1].XYZ(), Trajectory[i].XYZ(), out inter)) result.Add(inter);
            }
            return result;
        }



        public Point2D[] Area()
        {
            double x_min = Trajectory[0].X, x_max = Trajectory[0].X, y_min = Trajectory[0].Y, y_max = Trajectory[0].Y;
            int count = Trajectory.Count();
            for (int i = 1; i < count; ++i)
            {
                if (x_min > Trajectory[i].X) x_min = Trajectory[i].X;
                else
                if (x_max < Trajectory[i].X) x_max = Trajectory[i].X;
                if (y_min > Trajectory[i].Y) y_min = Trajectory[i].Y;
                else
                if (y_max < Trajectory[i].Y) y_max = Trajectory[i].Y;
            }
            return new Point2D[]
            {
                new Point2D(x_min, y_min),
                new Point2D(x_max, y_min),
                new Point2D(x_max, y_max),
                new Point2D(x_min, y_max)
            };
        }



        /*
        public List<Point3D> Intersections(Cell cell)
        {
            List<Point3D> result = new List<Point3D>();
            CellFace xyNear = cell.Face(CoordPlane.XY, FaceDistance.Near);
            CellFace xyFar = cell.Face(CoordPlane.XY, FaceDistance.Far);
            bool exit = true;
            foreach (Point2D p in Area())
                if (xyNear.Contain(p, CoordPlane.XY) || xyFar.Contain(p, CoordPlane.XY)) { exit = false; break; }
            if (exit) return result;            
            result.AddRange(Intersections(cell.Face(CoordPlane.XY, FaceDistance.Near)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.XZ, FaceDistance.Near)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.YZ, FaceDistance.Near)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.YZ, FaceDistance.Far)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.XZ, FaceDistance.Far)));
            result.AddRange(Intersections(cell.Face(CoordPlane.XY, FaceDistance.Far)));
            return result;
        }
        */




        static public List<Well> Read(  string file,
                                        int n_col_X,
                                        int n_col_Y,
                                        int n_col_tvd,
                                        int n_col_md,
                                        string prefix = "",
                                        string ending = "/"  )
        {
            List<Well> result = new List<Well>();
            try
            {
                prefix = Helper.ToUpper(prefix).Trim();
                ending = Helper.ToUpper(ending).Trim();
                string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));
                List<string> words = new List<string>();
                foreach (string line in lines)
                {
                    string cline = Helper.ClearLine(line);
                    if (string.IsNullOrEmpty(cline)) continue;
                    if (!string.IsNullOrEmpty(prefix)) cline = Helper.ClearLine(cline.Replace(prefix, string.Empty));
                    string[] split = cline.Split();
                    foreach (string word in split) words.Add(word);
                }
                int count = words.Count;
                for (int i = 0; i < count; i++)
                {
                    string title = words[i++];
                    List<InclPoint> points = new List<InclPoint>();
                    const int n_col = 4;
                    for (; words[i] != ending; i += n_col)
                    {
                        double x     = Helper.ParseDouble(words[i + n_col_X     ]);
                        double y     = Helper.ParseDouble(words[i + n_col_Y     ]);
                        double tvd   = Helper.ParseDouble(words[i + n_col_tvd   ]);
                        double md    = Helper.ParseDouble(words[i + n_col_md    ]);
                        points.Add(new InclPoint(x, y, tvd, md));
                    }
                    result.Add(new Well(title, points, new List<WellEvent>(), true));
                }
            }
            catch
            {
                return result;
            }
            return result;
        }





        



        /*
        public bool Read(string file, FileType type)
        {
            try
            {
                switch (type)
                {
                    case FileType.RMSWell:
                        {
                            string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));
                            string[] split = ClearLine(lines[2]).Split();
                            Title = split[0];
                            Top = new Point3D(Helper.ParseDouble(split[1]), Helper.ParseDouble(split[2]), Helper.ParseDouble(split[3]));
                            Incl.Clear();
                            int count = lines.Count();
                            for (int i = 7; i < count; ++i)
                            {
                                split = ClearLine(lines[i]).Split();
                                double x = Helper.ParseDouble(split[0]);
                                double y = Helper.ParseDouble(split[1]);
                                double tvd = Helper.ParseDouble(split[2]);
                                double md = Helper.ParseDouble(split[3]);
                                Incl.Add(new InclPoint(x, y, tvd, md));
                            }
                        }
                        break;
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        */



    }
}
