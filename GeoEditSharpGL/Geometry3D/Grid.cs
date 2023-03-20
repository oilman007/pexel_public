using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace Pexel
{
    [Serializable]
    public class Grid
    {
        public Grid() 
        {
            Init();
        }


        public Grid(string file, FileType type)
        {
            Init();
            Read(file, type);
        }



        public Grid(string title, int nx, int ny, int nz, double xSize, double ySize, double zSize, double depth,
                    double xShift = 0.0, double yShift = 0.0, double xAngle = 0.0, double yAngle = 0.0)
        {
            Init(title, nx, ny, nz, xSize, ySize, zSize, depth, xShift, yShift, xAngle, yAngle);
        }


        public Grid(string title, SpecGrid sg, Coord c, Zcorn z, Actnum a, List<Prop> ps, List<Compdat> ws)
        {
            Init(title, sg, new CoordSys(sg.NZ), c, z, a, ps, ws);
        }




        public void Init()
        {
            SpecGrid = new SpecGrid();
            //MapAxes = new MapAxes();
            Coord = new Coord();
            CoordSys = new CoordSys();
            Zcorn = new Zcorn();
            Actnum = new Actnum();
            Props = new List<Prop>();
            Title = "Grid_" + (Namber++).ToString();
            Compdats = new List<Compdat>();
            CurrProp = null;
            CurrLayers = Array.Empty<int>();
        }


        public DateTime LastWriteTime { set; get; }
        public string Title { set; get; }
        public SpecGrid SpecGrid { set; get; }
        //public MapAxes MapAxes { set; get; }
        public CoordSys CoordSys { set; get; }
        public Coord Coord { set; get; }
        public Zcorn Zcorn { set; get; }
        public Actnum Actnum { set; get; }
        public List<Prop> Props { set; get; }
        public Prop CurrProp { set; get; }
        public List<Compdat> Compdats { set; get; }
        //public Cell[,,] Cells { private set; get; }

        public  int[] CurrLayers { set; get; }

        static long Namber = 1;

        public string Tag { get; } = "Grid";


        public Cell[,,] Cells()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            Cell[,,] result = new Cell[nx, ny, nz];
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    Parallel.For(0, nx, i => result[i, j, k] = LocalCell(i, j, k));
            return result;
        }

        




        public void AddProp(string title, double value)
        {
            this.Props.Add(new Prop(NX(), NY(), NZ(), value, title));
        }






        public bool WriteBinary(string file)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.Create)))
                {
                    SpecGrid.Write(writer);
                    CoordSys.Write(writer);
                    Coord.Write(writer);
                    Zcorn.Write(writer);
                    Actnum.Write(writer);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        static public bool ReadBinary(string file, out Grid grid)
        {
            grid = new Grid();
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {                    
                    grid.LastWriteTime = File.GetLastWriteTime(file);
                    grid.SpecGrid = SpecGrid.Read(reader);
                    grid.CoordSys = CoordSys.Read(reader);
                    grid.Coord = Coord.Read(reader);
                    grid.Zcorn = Zcorn.Read(reader);
                    grid.Actnum = Actnum.Read(reader);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            //writer.Write(Version0);
            //writer.Write(Title);
            SpecGrid.Write(writer);
            Coord.Write(writer);
            Zcorn.Write(writer);
            Actnum.Write(writer);
            //writer.Write(Props.Count);
            //foreach (Prop p in Props)
            //    p.Write(writer);
            //writer.Write(Compdats.Count);
            //foreach (Compdat well in Compdats)
            //    well.Write(writer);
        }


        public static Grid Read(BinaryReader reader)
        {
            //string title = reader.ReadString();
            SpecGrid sg = SpecGrid.Read(reader);
            Coord c = Coord.Read(reader);
            Zcorn z = Zcorn.Read(reader);
            Actnum a = Actnum.Read(reader);
            //int np = reader.ReadInt32();
            //List<Prop> ps = new List<Prop>();
            //for (int p = 0; p < np; ++p)
            //    ps.Add(Prop.Read(reader));
            //int nw = reader.ReadInt32();
            //List<Compdat> ws = new List<Compdat>();
            //for (int w = 0; w < nw; ++w)
            //    ws.Add(Compdat.Read(reader));
            return new Grid(string.Empty, sg, c, z, a, new List<Prop>(), new List<Compdat>());
        }







        void Init(string title, int nx, int ny, int nz, double xSize, double ySize, double zSize, double depth,
                   double xShift = 0.0, double yShift = 0.0, double xAngle = 0.0, double yAngle = 0.0)
        {
            Init();
            Title = title;
            SpecGrid = new Pexel.SpecGrid(nx, ny, nz);
            Coord = new Pexel.Coord(nx, ny, xSize, ySize, xShift, yShift, xAngle, yAngle);
            CoordSys = new CoordSys(nz);
            Zcorn = new Pexel.Zcorn(nx, ny, nz, xSize, ySize, zSize, depth, xAngle, yAngle);
            Actnum = new Pexel.Actnum(nx, ny, nz);
        }



        void Init(string title, SpecGrid sg, CoordSys cs, Coord c, Zcorn z, Actnum a, List<Prop> ps, List<Compdat> ws)
        {
            Init();
            Title = title;
            SpecGrid = sg;
            CoordSys = cs;
            Coord = c;
            Zcorn = z;
            Actnum = a;
            Props = ps;
            Compdats = ws;
        }






        public int NX()
        {
            return Zcorn.NX();
        }

        public int NY()
        {
            return Zcorn.NY();
        }

        public int NZ()
        {
            return Zcorn.NZ();
        }


        public Cell LocalCell(Index3D index)
        {
            return LocalCell(index.I, index.J, index.K);
        }


        public Cell LocalCell(int i, int j, int k)
        {
            //int nres = CoordSys.Number(k);
            //double[,] matrix = MapAxes.Matrix();
            int nres = CoordSys.Number(k);
            return new Cell(Coord.Pillars[i + 0, j + 0, nres].Point3D(Zcorn.Items[i, j, k].Corners[0]),
                            Coord.Pillars[i + 1, j + 0, nres].Point3D(Zcorn.Items[i, j, k].Corners[1]),
                            Coord.Pillars[i + 0, j + 1, nres].Point3D(Zcorn.Items[i, j, k].Corners[2]),
                            Coord.Pillars[i + 1, j + 1, nres].Point3D(Zcorn.Items[i, j, k].Corners[3]),
                            Coord.Pillars[i + 0, j + 0, nres].Point3D(Zcorn.Items[i, j, k].Corners[4]),
                            Coord.Pillars[i + 1, j + 0, nres].Point3D(Zcorn.Items[i, j, k].Corners[5]),
                            Coord.Pillars[i + 0, j + 1, nres].Point3D(Zcorn.Items[i, j, k].Corners[6]),
                            Coord.Pillars[i + 1, j + 1, nres].Point3D(Zcorn.Items[i, j, k].Corners[7]),
                            Actnum.IsAct(i, j, k));
        }



        /*
        public Cell GlobalCell(int i, int j, int k)
        {
            //double[,] matrix = MapAxes.Matrix();
            Cell cell = new Cell();
            cell.Act = Actnum.Values[i, j, k];
            cell.Corners[0] = Coord.GlobalPillar(i + 0, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[0]);
            cell.Corners[1] = Coord.GlobalPillar(i + 1, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[1]);
            cell.Corners[2] = Coord.GlobalPillar(i + 0, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[2]);
            cell.Corners[3] = Coord.GlobalPillar(i + 1, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[3]);
            cell.Corners[4] = Coord.GlobalPillar(i + 0, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[4]);
            cell.Corners[5] = Coord.GlobalPillar(i + 1, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[5]);
            cell.Corners[6] = Coord.GlobalPillar(i + 0, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[6]);
            cell.Corners[7] = Coord.GlobalPillar(i + 1, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[7]);
            return cell;
        }
        */







        public bool Specified()
        {
            return SpecGrid.Specified() && Coord.Specified() && Zcorn.Specified() && Actnum.Specified();
        }

        public bool Specified(out List<string> report)
        {            
            bool SpecGrid_Specified = SpecGrid.Specified();
            bool CoordSys_Specified = CoordSys.Specified(SpecGrid);
            bool Coord_Specified = Coord.Specified();
            bool Zcorn_Specified = Zcorn.Specified();
            bool Actnum_Specified = Actnum.Specified();
            bool Grid_Specified = SpecGrid_Specified && CoordSys_Specified && Coord_Specified && Zcorn_Specified && Actnum_Specified;
            report = new List<string>();
            report.Add($"SpecGrid {(SpecGrid_Specified ? "   " : "not")} specified");
            report.Add($"CoordSys {(CoordSys_Specified ? "   " : "not")} specified");
            report.Add($"Coord    {(Coord_Specified    ? "   " : "not")} specified");
            report.Add($"Zcorn    {(Zcorn_Specified    ? "   " : "not")} specified");
            report.Add($"Actnum   {(Actnum_Specified   ? "   " : "not")} specified");
            report.Add($"Grid     {(Grid_Specified     ? "   " : "not")} specified");
            return Grid_Specified;
        }

        
        public bool Read(string file, FileType type)
        {
            this.LastWriteTime = File.GetLastWriteTime(file);
            SpecGrid = new SpecGrid(file, type);
            MapAxes ma = new MapAxes(file, type);
            CoordSys = new CoordSys(SpecGrid, file, type);
            Coord = new Coord(SpecGrid, ma, CoordSys, file, type);
            Zcorn = new Zcorn(SpecGrid, file, type);
            Actnum = new Actnum(SpecGrid, file, type);
            if (Specified())
            {
                return true;
            }
            Init();
            return false;
        }


        public bool ReadGRDECL(string file, out List<string> report)
        {
            this.LastWriteTime = File.GetLastWriteTime(file);
            FileIO.EclFile eclfile = new FileIO.EclFile(file);
            SpecGrid = new SpecGrid(eclfile);
            MapAxes ma = new MapAxes(eclfile);
            CoordSys = new CoordSys(eclfile);
            Coord = new Coord(SpecGrid, ma, CoordSys, eclfile);
            Zcorn = new Zcorn(SpecGrid, eclfile);
            Actnum = new Actnum(SpecGrid, eclfile);
            if (Specified(out report))
            {
                return true;
            }
            Init();
            return false;
        }



        public bool Write(string file, FileType type)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine(string.Empty);
            }
            return  this.Coord.MapAxes.Write(file, type)    &&
                    this.SpecGrid.Write(file, type)         &&
                    //this.CoordSys.
                    this.Coord.Write(file, type)            &&
                    this.Zcorn.Write(file, type)            &&
                    this.Actnum.Write(file, type);
        }











        public Point3D[,,] CellCenters()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            Point3D[,,] centers = new Point3D[nx, ny, nz];
            for (int k = 0; k < nz; ++k)
                Parallel.For(0, ny, j =>
                {
                    for (int i = 0; i < nx; ++i)
                        centers[i, j, k] = LocalCell(i, j, k).Center;
                });
                        //Parallel.For(0, nx, i => centers[i, j, k] = LocalCell(i, j, k).Center);
            return centers;
        }




        /*
        public double Height()
        {
            if (!Specified())
                return 0f;
            double min = Cells[0, 0, 0].Corners[0].Y;
            double max = Cells[0, 0, 0].Corners[0].Y;
            for (int k = 0; k < NZ(); ++k)
            {
                for (int j = 0; j < NY(); ++j)
                {
                    for (int i = 0; i < NX(); ++i)
                    {
                        for (int c = 0; c < 8; ++c)
                        {
                            if (min > Cells[i, j, k].Corners[c].Y)
                                min = Cells[i, j, k].Corners[c].Y;
                            if (max < Cells[i, j, k].Corners[c].Y)
                                max = Cells[i, j, k].Corners[c].Y;
                        }
                    }
                }
            }
            return max - min;
        }


        public double Width()
        {
            if (!Specified())
                return 0f;
            double min = Cells[0, 0, 0].Corners[0].X;
            double max = Cells[0, 0, 0].Corners[0].X;
            for (int k = 0; k < NZ(); ++k)
            {
                for (int j = 0; j < NY(); ++j)
                {
                    for (int i = 0; i < NX(); ++i)
                    {
                        for (int c = 0; c < 8; ++c)
                        {
                            if (min > Cells[i, j, k].Corners[c].X)
                                min = Cells[i, j, k].Corners[c].X;
                            if (max < Cells[i, j, k].Corners[c].X)
                                max = Cells[i, j, k].Corners[c].X;
                        }
                    }
                }
            }
            return max - min;
        }


        public PointF Location()
        {
            if (!Specified())
                return new PointF(0f, 0f);
            double minX = Cells[0, 0, 0].Corners[0].X;
            double minY = Cells[0, 0, 0].Corners[0].Y;
            for (int k = 0; k < NZ(); ++k)
            {
                for (int j = 0; j < NY(); ++j)
                {
                    for (int i = 0; i < NX(); ++i)
                    {
                        for (int c = 0; c < 8; ++c)
                        {
                            if (minX > Cells[i, j, k].Corners[c].X)
                                minX = Cells[i, j, k].Corners[c].X;
                            if (minY > Cells[i, j, k].Corners[c].Y)
                                minY = Cells[i, j, k].Corners[c].Y;
                        }
                    }
                }
            }
            return new PointF(minX, minY);
        }


        
        const string kw_specgrid = "SPECGRID";
        const string kw_mapaxes = "MAPAXES";
        const string kw_coord = "COORD";
        const string kw_zcorn = "ZCORN";
        const string kw_actnum = "ACTNUM";
         * 
         * */




        /*
        const string remString = "--";
        const string tabString = "\t";
        const string singlSpace = " ";
        const string doubleSpace = "  ";
        List<string> ClearLines(string filename)
        {
            List<string> r = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                int index = line.IndexOf(remString);
                string temp = line;
                if (index != -1)
                    temp = temp.Remove(index);
                temp = temp.Replace(tabString, singlSpace);
                while (temp.Contains(doubleSpace))
                    temp = temp.Replace(doubleSpace, singlSpace);
                temp = temp.Trim();
                temp = temp.ToUpper();
                if (temp.Count() > 0)
                    r.Add(temp);
            }
            return r;
        }


        const string terminator = "/";
        const char repeator = '*';
        List<string> KeyWordData(string kw, List<string> lines)
        {
            List<string> r = new List<string>();
            int i = 0;
            while (lines[i++] != kw)
                if (i == lines.Count)
                    return r;
            for (; i < lines.Count; ++i)
            {
                string[] split = lines[i].Split();
                foreach (string word in split)
                {
                    if (word == terminator)
                        return r;
                    if (word.Contains(repeator))
                        foreach (string value in RepeatedValues(word))
                            r.Add(value);
                    else
                        r.Add(word);
                }
            }
            r.Clear();
            return r;
        }


        const string valbydef = "1*";
        List<string> RepeatedValues(string expression)
        {
            List<string> r = new List<string>();
            string[] split = expression.Split(repeator);
            switch (split.Length)
            {
                case 1:
                    for (int i = int.Parse(split[0]); i > 0; --i)
                        r.Add(valbydef);
                    break;
                case 2:
                    for (int i = int.Parse(split[0]); i > 0; --i)
                        r.Add(split[1]);
                    break;
            }
            return r;
        }


        bool Fill(MapAxes mapAxes, SpecGrid specGrid, Coord coord, Zcorn zcorn, Actnum actnum)
        {
            if (!specGrid.Specified() || !coord.Specified() || !zcorn.Specified() || !actnum.Specified())
                return false;
            Cells = new Cell[specGrid.NX, specGrid.NY, specGrid.NZ];
            for (int k = 0; k < specGrid.NZ; ++k)
                for (int j = 0; j < specGrid.NY; ++j)
                    for (int i = 0; i < specGrid.NX; ++i)
                    {
                        Cell cell = new Cell();
                        cell.Act = actnum.Values[i, j, k];
                        cell.Corners[0] = coord.Pillars[i + 0, j + 0].Point3D(zcorn.Values[i, j, k, 0]);
                        cell.Corners[1] = coord.Pillars[i + 1, j + 0].Point3D(zcorn.Values[i, j, k, 1]);
                        cell.Corners[2] = coord.Pillars[i + 0, j + 1].Point3D(zcorn.Values[i, j, k, 2]);
                        cell.Corners[3] = coord.Pillars[i + 1, j + 1].Point3D(zcorn.Values[i, j, k, 3]);
                        cell.Corners[4] = coord.Pillars[i + 0, j + 0].Point3D(zcorn.Values[i, j, k, 4]);
                        cell.Corners[5] = coord.Pillars[i + 1, j + 0].Point3D(zcorn.Values[i, j, k, 5]);
                        cell.Corners[6] = coord.Pillars[i + 0, j + 1].Point3D(zcorn.Values[i, j, k, 6]);
                        cell.Corners[7] = coord.Pillars[i + 1, j + 1].Point3D(zcorn.Values[i, j, k, 7]);
                        Cells[i, j, k] = cell;
                    }
            return true;
        }
        */


        /*
        public bool ReadCMG(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;

                    const string kw_grid = "*GRID";
                    const string kw_coord = "*COORD";
                    const string kw_zcorn = "*ZCORN";
                    const string kw_null = "*NULL";
                    // grid
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = CMGClearLine(line);
                        if (line == string.Empty)
                            continue;
                        string[] split = line.Split();
                        if (split[0].ToUpper() == kw_grid)
                        {
                            int nx = int.Parse(split[2]);
                            int ny = int.Parse(split[3]);
                            int nz = int.Parse(split[4]);
                            const double cellSize = 0f;
                            Init(nx, ny, nz, cellSize, cellSize, cellSize);
                            break;
                        }
                    }
                    // coord
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (CMGClearLine(line) == kw_coord)
                            break;
                    }
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = CMGClearLine(line);
                        if (line == string.Empty)
                            continue;
                        if (line == kw_zcorn)
                            break;
                        foreach (string word in line.Split())
                        {
                            double value = double.Parse(word);

                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        void GetGridDim(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if(line == string.Empty)
                        continue;
                    string [] split = line.Split();
                    if (split[0] == "*GRID")
                    {
                        int nx = int.Parse(split[2]);
                        int ny = int.Parse(split[3]);
                        int nz = int.Parse(split[4]);
                        const double cellSize = 0f;
                        Init(nx, ny, nz, cellSize, cellSize, cellSize);
                        return;
                    }
                }
            }
        }

        void GetCoord(string file)
        {
        }

        void GetZcorn(string file)
        {
        }

        void GetNull(string file)
        {
        }


        string CMGClearLine(string line)
        {
            const string dblSpace = "  ";
            const string snglSpace = " ";
            const string tab = "\t";
            const string remSymbol = "**";
            int index = line.IndexOf(remSymbol);
            if (index != -1)
                line = line.Remove(index);
            line = line.Trim();
            line = line.Replace(tab, snglSpace);
            while (line.Contains(dblSpace))
                line = line.Replace(dblSpace, snglSpace);
            return line;
        }
         * */

    }
}
