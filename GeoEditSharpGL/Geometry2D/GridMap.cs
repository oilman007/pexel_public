using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Pexel
{
    public class GridMap
    {

        public GridMap()
        {

        }

        public GridMap(Grid grid, double depth)
        {
            Init(grid, depth);
        }


        public bool[,] Act { protected set; get; }
        public double[,] Values { protected set; get; }
        public Point2D[,] Nodes { protected set; get; }
        public int NX { protected set; get; }
        public int NY { protected set; get; }
        public double MinX { protected set; get; }
        public double MinY { protected set; get; }
        public double MaxX { protected set; get; }
        public double MaxY { protected set; get; }
        public string Title { set; get; }
        public double MinValue { set; get; }
        public double MaxValue { set; get; }






        /*
        public double MinX()
        {
            if (NX == 0 || NY == 0) return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            double result = Nodes[perimeterIndexes[0].X, perimeterIndexes[0].Y].X;
            for (int i = 1; i < perimeterIndexes.Length; ++i)
            {
                double value = Nodes[perimeterIndexes[i].X, perimeterIndexes[i].Y].X;
                if (result > value) //(!)
                    result = value;
            }
            return result;
        }



        public double MaxX()
        {
            if (NX == 0 || NY == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Count();
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MaxX(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MaxX(); //(!)
                if (result < value) //(!)
                    result = value;
            }
            return result;
        }



        public double MinY()
        {
            if (NX == 0 || NY == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Count();
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MinY(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MinY(); //(!)
                if (result > value) //(!)
                    result = value;
            }
            return result;
        }



        public double MaxY()
        {
            if (NX == 0 || NY == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Count();
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MaxY(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MaxY(); //(!)
                if (result < value) //(!)
                    result = value;
            }
            return result;
        }
        */



        Point[] PerimeterIndexes()
        {
            List<Point> result = new List<Point>();
            int i, j;
            // i = 0 -> nx-1       j = 0
            for (i = 0, j = 0; i < NX; ++i)
                result.Add(new Point(i, j));
            // i = nx-1            j = 1 -> ny-1
            for (i = NX - 1, j = 1; j < NY; ++j)
                result.Add(new Point(i, j));
            // i = nx-2 -> 0       j = ny-1
            for (i = NX - 2, j = NY - 1; i > -1; --i)
                result.Add(new Point(i, j));
            // i = 0               j = ny-2 -> 1
            for (i = 0, j = NY - 2; j > 0; --j)
                result.Add(new Point(i, j));
            return result.ToArray();
        }



        public static Point UndefIndex { get { return new Point(-1, -1); } }

        public bool Saved
        {
            get
            {
                return true;
            }
            set
            {
            }
        }



        public const double UndefValue = 0f;


        public Point2D Center(int i, int j)
        {
            double x = (Nodes[i + 0, j + 0].X + Nodes[i + 1, j + 0].X + Nodes[i + 1, j + 1].X + Nodes[i + 0, j + 1].X) / 4;
            double y = (Nodes[i + 0, j + 0].Y + Nodes[i + 1, j + 0].Y + Nodes[i + 1, j + 1].Y + Nodes[i + 0, j + 1].Y) / 4;
            return new Point2D(x, y);
        }



        public Point CellIndex(Point2D point)
        {
            for (int j = 0; j < NY; ++j)
                for (int i = 0; i < NX; ++i)
                    if (InsideQuadrangle(Nodes[i + 0, j + 0], Nodes[i + 1, j + 0], Nodes[i + 1, j + 1], Nodes[i + 0, j + 1], point))
                        return new Point(i, j);
            return UndefIndex;
        }

       
        bool InsideQuadrangle(Point2D p0, Point2D p1, Point2D p2, Point2D p3, Point2D p)
        {
            return InsideTriangle(p0, p1, p2, p) || InsideTriangle(p1, p2, p3, p);
        }
        
        /*
            Иногда встречается необходимость определения принадлежности точки какой-нибудь геометрической фигуре. 
            Самой простейшей является треугольник ABC.
            Дано:

            Точка P(P_x,P_y,P_z), треугольник с вершинами: A(A_x,A_y,A_z), B(B_x,B_y,B_z), C(C_x,C_y,C_z).
            методика

            Формируются три треугольника: ABP, ACP, BCP. После вычисляются их площади SABP,SACP,SBCP. 
            После этого сверяется сумма этих площадей с площадью треугольника SABC. Если точка лежит на треугольнике ABC, 
            то треугольники ABP, ACP, BCP будут просто частями треугольника ABC, и сумма их площадей будет равна его площади SABC. 
            Если же точка не принадлежит треугольнику, сумма площадей SABP,SACP,SBCP превысит площадь треугольника ABC.
            Легкое лирическое отступление для тех, кто не помнит, чему равна площадь треугольника: проще всего использовать формулу Герона, 
            которая позволяет найти площадь треугольника, зная только его стороны, оч. удобно
        */

        const double EPS = 1e-6f;
        bool InsideTriangle(Point2D a, Point2D b, Point2D c, Point2D p)
        {
            double AB = Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
            double BC = Math.Sqrt((b.X - c.X) * (b.X - c.X) + (b.Y - c.Y) * (b.Y - c.Y));
            double CA = Math.Sqrt((a.X - c.X) * (a.X - c.X) + (a.Y - c.Y) * (a.Y - c.Y));

            double AP = Math.Sqrt((p.X - a.X) * (p.X - a.X) + (p.Y - a.Y) * (p.Y - a.Y));
            double BP = Math.Sqrt((p.X - b.X) * (p.X - b.X) + (p.Y - b.Y) * (p.Y - b.Y));
            double CP = Math.Sqrt((p.X - c.X) * (p.X - c.X) + (p.Y - c.Y) * (p.Y - c.Y));
            double diff = (TriangleSquare(AP, BP, AB) + TriangleSquare(AP, CP, CA) + TriangleSquare(BP, CP, BC)) - TriangleSquare(AB, BC, CA);

            return Math.Abs(diff) < EPS;
        }


        double TriangleSquare(double a, double b, double c)
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }


        
        void Update()
        {
            NX = Values.GetLength(0);
            NY = Values.GetLength(1);
            UpdateMinMax();
        }

        void UpdateMinMax()
        {
            if (NX == 0 || NY == 0)
            {
                MinX = 0f;
                MinY = 0f;
                MaxX = 0f;
                MaxY = 0f;
                return;
            }
            MinX = Nodes[0, 0].X;
            MinY = Nodes[0, 0].Y;
            MaxX = Nodes[0, 0].X;
            MaxY = Nodes[0, 0].Y;
            foreach(Point i in PerimeterNodesIndexes())
            {
                if (MinX > i.X) MinX = i.X;
                else
                if (MaxX < i.X) MaxX = i.X;
                //
                if (MinY > i.Y) MinY = i.Y;
                else
                if (MaxY < i.Y) MaxY = i.Y;
            }
        }



        Point[] PerimeterNodesIndexes()
        {
            List<Point> result = new List<Point>();
            int i, j, nx = NX + 1, ny = NY + 1;
            // i = 0 -> nx-1       j = 0
            for (i = 0, j = 0; i < nx; ++i)
                result.Add(new Point(i, j));
            // i = nx-1            j = 1 -> ny-1
            for (i = nx - 1, j = 1; j < ny; ++j)
                result.Add(new Point(i, j));
            // i = nx-2 -> 0       j = ny-1
            for (i = nx - 2, j = ny - 1; i > -1; --i)
                result.Add(new Point(i, j));
            // i = 0               j = ny-2 -> 1
            for (i = 0, j = ny - 2; j > 0; --j)
                result.Add(new Point(i, j));
            return result.ToArray();
        }






        // http://alienryderflex.com/polygon/
        //  Globals which should be set before calling these functions:
        //
        //  int    polyCorners  =  how many corners the polygon has (no repeats)
        //  double  polyX[]      =  horizontal coordinates of corners
        //  double  polyY[]      =  vertical coordinates of corners
        //  double  x, y         =  point to be tested
        //
        //  The following global arrays should be allocated before calling these functions:
        //
        //  double  constant[] = storage for precalculated constants (same size as polyX)
        //  double  multiple[] = storage for precalculated multipliers (same size as polyX)
        //
        //  (Globals are used in this example for purposes of speed.  Change as
        //  desired.)
        //
        //  USAGE:
        //  Call precalc_values() to initialize the constant[] and multiple[] arrays,
        //  then call pointInPolygon(x, y) to determine if the point is in the polygon.
        //
        //  The function will return YES if the point x,y is inside the polygon, or
        //  NO if it is not.  If the point is exactly on the edge of the polygon,
        //  then the function may return YES or NO.
        //
        //  Note that division by zero is avoided because the division is protected
        //  by the "if" clause which surrounds it.
        /*
        int polyCorners;
        double[] polyX;
        double[] polyY;
        double x, y;
        double[] constant;
        double[] multiple;
        void precalc_values()
        {
            int i, j = polyCorners - 1;
            for (i = 0; i < polyCorners; i++)
            {
                if (polyY[j] == polyY[i])
                {
                    constant[i] = polyX[i];
                    multiple[i] = 0;
                }
                else {
                    constant[i] = polyX[i] - (polyY[i] * polyX[j]) / (polyY[j] - polyY[i]) + (polyY[i] * polyX[i]) / (polyY[j] - polyY[i]);
                    multiple[i] = (polyX[j] - polyX[i]) / (polyY[j] - polyY[i]);
                }
                j = i;
            }
        }
        
        bool pointInPolygon()
        {
            bool oddNodes = false, current = polyY[polyCorners - 1] > y, previous;
            for (int i = 0; i < polyCorners; i++)
            {
                previous = current;
                current = polyY[i] > y;
                if (current != previous) oddNodes ^= y * multiple[i] + constant[i] < x;
            }
            return oddNodes;
        }
        */


        public bool PointInPolygon(PointF point, PointF[] poly)
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



        TreeNode[] TreeNodes()
        {
            return new TreeNode[] { new TreeNode(Title) };
        }

        public TreeNode TreeNode()
        {
            TreeNode result = new TreeNode(Title) { Tag = this, ToolTipText = this.Tag };
            return result;
        }

        public string Tag { get; } = "Map";





        public void Init(int nx, int ny)
        {
            NX = nx;
            NY = ny;
            Act = new bool[nx, ny];
            Values = new double[nx, ny];
            Nodes = new Point2D[nx + 1, ny + 1];
        }


        public void Init(Grid grid, double depth)
        {
            Init(grid.NX(), grid.NY());
            for (int i = 0; i < NX; ++i)
            {
                Parallel.For(0, NY, j =>
                {
                    Nodes[i + 0, j + 0] = grid.Coord.Pillars[i + 0, j + 0, 0].Point2D(depth);
                    Nodes[i + 1, j + 0] = grid.Coord.Pillars[i + 1, j + 0, 0].Point2D(depth);
                    Nodes[i + 1, j + 1] = grid.Coord.Pillars[i + 1, j + 1, 0].Point2D(depth);
                    Nodes[i + 0, j + 1] = grid.Coord.Pillars[i + 0, j + 1, 0].Point2D(depth);
                });
            }
            Update();
        }










    }
}

