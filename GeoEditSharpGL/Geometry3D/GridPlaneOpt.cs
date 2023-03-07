using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;


namespace Pexel
{
    public class GridPlaneOpt
    {



        public CellFace[,] Faces { set; get; }
        public bool[,] Act { set; get; }
        public double[,] Values { set; get; }
        public static Point UndefIndex { get { return new Point(-1, -1); } }
        public Grid Grid { set; get; }
        public ActProp Prop { set; get; }
        public string ArrayTitle { set; get; }
        public int[] Layers { set; get; }
        public int NX { get; protected set; }
        public int NY { get; protected set; }

        public const double UndefValue = 0;








        public GridPlaneOpt(Grid grid, ActProp prop, string array_title, int[] layers)
        {
            Grid = grid;
            Prop = prop;
            ArrayTitle = array_title;
            Layers = layers;
            NX = grid.NX();
            NY = grid.NY();
            Faces = new CellFace[NX, NY];
            Act = new bool[NX, NY];
            Values = new double[NX, NY];
            UpdateCells();
            UpdateValues();
        }



        public void UpdateCells()
        {
            for (int i = 0; i < NX; ++i)
            //Parallel.For(0, nx, i =>
            {
                //for (int j = 0; j < ny; ++j)
                Parallel.For(0, NY, j =>
                {
                    // faces
                    List<CellFace> faces = new List<CellFace>();
                    foreach (int k in Layers)
                        faces.Add(Grid.LocalCell(i, j, k).MiddleTopFace);
                    Faces[i, j] = AverageCellFace(faces);
                    // act
                    foreach (int k in Layers)
                        if (Grid.Actnum.IsAct(i, j, k) == true)
                        {
                            Act[i, j] = true;
                            break;
                        }
                });
            }
        }



        public void UpdateValues()
        {
            for (int i = 0; i < NX; ++i)
            //Parallel.For(0, nx, i =>
            {
                //for (int j = 0; j < ny; ++j)
                Parallel.For(0, NY, j =>
                {
                    // values
                    Values[i, j] = 0;
                    double weight = 0;
                    foreach (int k in Layers)
                        if (Grid.Actnum.IsAct(i, j, k) == true)
                        {
                            Values[i, j] += Prop.GetValue(i, j, k, Grid.Actnum);// * cell.Volume;
                            weight += 1;// cell.Volume;
                        }
                    if (weight == 0)
                        Values[i, j] = Prop.Values[0];
                    else
                        Values[i, j] /= weight;
                });
            }//);
        }




        CellFace AverageCellFace(List<CellFace> faces)
        {
            if (faces.Count == 0) return new CellFace();
            CellFace summ = faces[0];
            for (int i = 1; i < faces.Count; ++i)
                summ += faces[i];
            return summ / faces.Count;
        }





        public Point Index(Point2D point)
        {
            for (int j = 0; j < NY; ++j)
                for (int i = 0; i < NX; ++i)
                    if (Faces[i, j].Contain(point, CoordPlane.XY))
                        return new Point(i, j);
            return UndefIndex;
        }









        public double MinX()
        {
            if (NX == 0 || NY == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Length;
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MinX(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MinX(); //(!)
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
            int count = perimeterIndexes.Length;
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
            int count = perimeterIndexes.Length;
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
            int count = perimeterIndexes.Length;
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MaxY(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MaxY(); //(!)
                if (result < value) //(!)
                    result = value;
            }
            return result;
        }




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





        int DirFlip(int i, int n, bool flip)
        {
            if (flip)
                return n - 1 - i;
            else
                return i;
        }





    }
}
