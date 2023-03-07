using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Pexel
{

    [Serializable]
    public class Coord
    {

        public Coord()
        {
            Init();
        }

        public Coord(MapAxes mapAxes, Pillar[,,] pillars)
        {
            Init(mapAxes, pillars);
        }

        public Coord(SpecGrid specGrid, MapAxes mapAxes, CoordSys sys, string file, FileType type)
        {
            Init();
            Read(specGrid, mapAxes, sys, file, type);
        }

        public Coord(int nx, int ny, double xSize, double ySize, 
            double xShift = 0.0, double yShift = 0.0, double xAngle = 0.0, double yAngle = 0.0)
        {
            Init(nx, ny, xSize, ySize, xShift, yShift, xAngle, yAngle);
        }

        public Coord(SpecGrid specGrid, MapAxes mapAxes, CoordSys sys, FileIO.EclFile eclfile)
        {
            Init();
            if (eclfile.GetSingleValuesArray(grdecl_kw_coord, out double[] values))
                Init(specGrid, mapAxes, sys, values);
        }


        //public Pillar[,,] Pillars { set; get; }
        public Pillar[,,] Pillars { set; get; }


        public MapAxes MapAxes { set; get; }




        public void Write(BinaryWriter writer)
        {
            MapAxes.Write(writer);
            int icount = NPillarsX();
            int jcount = NPillarsY();
            int kcount = NRes();
            writer.Write(icount);
            writer.Write(jcount);
            writer.Write(kcount);
            for (int k = 0; k < kcount; ++k)
                for (int j = 0; j < jcount; ++j)
                    for (int i = 0; i < icount; ++i)
                        Pillars[i, j, k].Write(writer);
        }



        public static Coord Read(BinaryReader reader)
        {
            MapAxes ma = MapAxes.Read(reader);
            int icount = reader.ReadInt32();
            int jcount = reader.ReadInt32();
            int kcount = reader.ReadInt32();
            Pillar[,,] ps = new Pillar[icount, jcount, kcount];
            for (int k = 0; k < kcount; ++k)
                for (int j = 0; j < jcount; ++j)
                    for (int i = 0; i < icount; ++i)
                        ps[i, j, k] = Pillar.Read(reader);
            return new Coord(ma, ps);
        }









        public int NPillarsX()
        {
            return Pillars.GetLength(0);
        }




        public int NPillarsY()
        {
            return Pillars.GetLength(1);
        }

        public int NRes()
        {
            return Pillars.GetLength(2);
        }



        void Init(int nx, int ny, double xSize, double ySize, 
                    double xShift = 0.0, double yShift = 0.0, double xAngle = 0.0, double yAngle = 0.0)
        {
            MapAxes = new MapAxes();
            // xSize *= Math.Cos(Math.PI * xAngle / 180);
            // ySize *= Math.Cos(Math.PI * yAngle / 180);
            const double z = 0.0;
            Pillars = new Pillar[nx + 1, ny + 1, 1];
            Parallel.For(0, ny + 1, j =>
            {
                double x = xShift;
                double y = yShift + j * ySize;
                for (int i = 0; i < nx + 1; ++i)
                {
                    Pillars[i, j, 0] = new Pillar(x, y, z, x, y, z);
                    x += xSize;
                }
            });
        }




        void Init()
        {
            Pillars = new Pillar[0, 0, 0];
            MapAxes = new MapAxes();
        }


        void Init(MapAxes mapAxes, Pillar[,,] pillars)
        {
            Pillars = pillars;
            MapAxes = mapAxes;
        }

               



        public bool Specified()
        {
            return NPillarsX() != 0 && NPillarsY() != 0;
        }





        double[,] Matrix(MapAxes mapAxes)
        {
            if (mapAxes.X1 == mapAxes.X2 && mapAxes.X2 == mapAxes.X3 &&
                mapAxes.Y1 == mapAxes.Y2 && mapAxes.Y2 == mapAxes.Y3)
                return new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            // http://htmlbook.ru/blog/matritsa-preobrazovanii
            /*
            x' = ax+cy+tx
            y' = bx+dy+ty
            */
            //double tx = -X2;
            //double ty = -Y2;
            //double a = X1 == X2 ? 1f : X1 - X2 / Math.Abs(X1 - X2);
            //double d = Y3 == Y2 ? 1f : Y3 - Y2 / Math.Abs(Y3 - Y2);
            double tx = 0;
            double ty = 0;
            double a = -1;
            double d = 1;
            double b = 0;// переделать
            double c = 0;// переделать
            /*
            0,0  0,1  0,2            a   b   0
            1,0  1,1  1,2            c   d   0
            2,0  2,1  2,2            tx  ty  1
            */
            return new double[,] { { a, b, 0 }, { c, d, 0 }, { tx, ty, 1 } };
        }



        public Pillar GlobalPillar(int i, int j, int nres)
        {
            const double zTop = 0, zBottom = 10000;
            Point3D top = Pillars[i, j, nres].Point3D(zTop);
            Point3D bottom = Pillars[i, j, nres].Point3D(zBottom);
            Pillar result = new Pillar(GlobalPoint3D(top), GlobalPoint3D(bottom));
            return result;
        }




        Point3D GlobalPoint3D(Point3D local)
        {
            //http://roboforum.ru/wiki/%D0%9F%D1%80%D0%B8%D0%BA%D0%BB%D0%B0%D0%B4%D0%BD%D0%B0%D1%8F_%D0%B3%D0%B5%D0%BE%D0%BC%D0%B5%D1%82%D1%80%D0%B8%D1%8F
            Point3D global = new Point3D();
            double distance = Distance(this.MapAxes.X1, this.MapAxes.Y1, this.MapAxes.X2, this.MapAxes.Y2);
            double cos = (this.MapAxes.Y1 - this.MapAxes.Y2) / distance;
            double sin = (this.MapAxes.X1 - this.MapAxes.X2) / distance;
            global.X = this.MapAxes.X2 + local.X * cos - local.Y * sin;
            global.Y = this.MapAxes.Y2 + local.Y * cos + local.X * sin;
            global.Z = local.Z;
            return global;
        }



        /*
        Point3D GlobalPoint3D(Point3D local)
        {
            //http://roboforum.ru/wiki/%D0%9F%D1%80%D0%B8%D0%BA%D0%BB%D0%B0%D0%B4%D0%BD%D0%B0%D1%8F_%D0%B3%D0%B5%D0%BE%D0%BC%D0%B5%D1%82%D1%80%D0%B8%D1%8F
            Point3D global = new Point3D();
            double cos = (this.MapAxes.Y1 - this.MapAxes.Y2) / Distance(this.MapAxes.X1, this.MapAxes.Y1, this.MapAxes.X2, this.MapAxes.Y2);
            double sin = (this.MapAxes.X3 - this.MapAxes.X2) / Distance(this.MapAxes.X2, this.MapAxes.Y2, this.MapAxes.X3, this.MapAxes.Y3);
            global.X = this.MapAxes.X2 + local.X * cos - local.Y * sin;
            global.Y = this.MapAxes.Y2 + local.Y * cos + local.X * sin;
            global.Z = local.Z;
            return global;
        }
        */



        double Distance(double x1, double y1, double x2, double y2)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;
            double result = Math.Pow(dx * dx + dy * dy, 0.5);
            return result;
        }




        void Init(SpecGrid specGrid, MapAxes mapAxes, CoordSys sys, double[] values)
        {
            if (values.Length == 0) return;
            if (values.Length != (2 * 3 * (specGrid.NX + 1) * (specGrid.NY + 1) * sys.Records.Length)) return;
            Pillars = new Pillar[specGrid.NX + 1, specGrid.NY + 1, sys.Records.Length];
            int n = 0;
            for (int k = 0; k < sys.Records.Length; ++k)
                for (int j = 0; j < specGrid.NY + 1; ++j)
                    for (int i = 0; i < specGrid.NX + 1; ++i)
                    {
                        double xt = values[n++];
                        double yt = values[n++];
                        double zt = values[n++];
                        double xb = values[n++];
                        double yb = values[n++];
                        double zb = values[n++];
                        Pillars[i, j, k] = new Pillar(xt, yt, zt, xb, yb, zb);
                    }            
            MapAxes = mapAxes;
        }



        
        public bool Read(SpecGrid specGrid, MapAxes mapAxes, CoordSys sys, string file,  FileType type)
        {
            switch (type)
            {
                case FileType.GRDECL_ASCII:
                    return ReadFromGRDECL(specGrid, mapAxes, sys, file);
                default:
                    return false;
            }
        }
        



        const string grdecl_kw_coord = "COORD";
        bool ReadFromGRDECL(SpecGrid specGrid, MapAxes mapAxes, CoordSys sys, string file)
        {
            FileIO.EclFile ecl = new FileIO.EclFile(file);
            if (!ecl.GetSingleValuesArray(grdecl_kw_coord, out double[] values))
                return false;
            if (values.Length != (2 * 3 * (specGrid.NX + 1) * (specGrid.NY + 1) * sys.Records.Length))
                return false;
            //Init(specGrid.NX, specGrid.NX, 50f, 50f);
            Pillars = new Pillar[specGrid.NX + 1, specGrid.NY + 1, sys.Records.Length];
            int n = 0;
            for (int k = 0; k < sys.Records.Length; ++k)
                for (int j = 0; j < specGrid.NY + 1; ++j)
                    for (int i = 0; i < specGrid.NX + 1; ++i)
                    {
                        double xt = values[n++];
                        double yt = values[n++];
                        double zt = values[n++];
                        double xb = values[n++];
                        double yb = values[n++];
                        double zb = values[n++];
                        Pillars[i, j, k] = new Pillar(xt, yt, zt, xb, yb, zb);
                    }
            MapAxes = mapAxes;
            return true;
        }


        /*
        public bool FillFromGRDECL(SpecGrid specGrid, string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Pillars = new Pillar[specGrid.NX + 1, specGrid.NY + 1];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (GRDECLReader.ClearLine(line) == grdecl_kw_coord)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = 6 * (specGrid.NX + 1);
                    int n = 0;
                    for (int j = 0; j < specGrid.NY + 1; ++j)
                    {
                        while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null) // add
                        {
                            string[] words = GRDECLReader.ClearWords(line);
                            foreach (string word in words)
                                values.Add(word);
                        }
                        for (int i = 0; i < specGrid.NX + 1; ++i)
                        {
                            double xt = double.Parse(values[n++]);
                            double yt = double.Parse(values[n++]);
                            double zt = double.Parse(values[n++]);
                            double xb = double.Parse(values[n++]);
                            double yb = double.Parse(values[n++]);
                            double zb = double.Parse(values[n++]);
                            Pillars[i, j] = new Pillar(xt, yt, zt, xb, yb, zb);
                        }
                        values.RemoveRange(0, valuesNeeded); // remove
                        n = 0;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
        }
        */



        /*
        const string cmg_kw_coord = "*COORD";
        bool ReadFromCMG(SpecGrid specGrid, CoordSys sys, string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Pillars = new Pillar[specGrid.NX + 1, specGrid.NY + 1, sys.Records.Length];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (FileIO.CMGReader.ClearLine(line) == cmg_kw_coord)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = 6 * (specGrid.NX + 1);
                    int n = 0;
                    for (int j = 0; j < specGrid.NY + 1; ++j)
                    {
                        while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null)// add
                        {
                            line = FileIO.CMGReader.ClearLine(line);
                            if (!string.IsNullOrEmpty(line))
                                foreach (string word in line.Split())
                                    values.Add(word);
                        }
                        for (int i = 0; i < specGrid.NX + 1; ++i)
                        {
                            double xt = Helper.ParseDouble(values[n++]);
                            double yt = Helper.ParseDouble(values[n++]);
                            double zt = Helper.ParseDouble(values[n++]);
                            double xb = Helper.ParseDouble(values[n++]);
                            double yb = Helper.ParseDouble(values[n++]);
                            double zb = Helper.ParseDouble(values[n++]);
                            Pillars[i, j] = new Pillar(xt, yt, zt, xb, yb, zb);
                        }
                        values.RemoveRange(0, valuesNeeded); // remove
                        n = 0;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
        }
        */







        public bool Write(string file, FileType type)
        {
            switch (type)
            {
                /*
                case FileType.CMG_ASCII:
                    return ReadFromCMG(file);
                    */
                case FileType.GRDECL_ASCII:
                    return WriteGRDECL(file);
                default:
                    return false;
            }
        }






        bool WriteGRDECL(string file)
        {
            int nx = NPillarsX(), ny = NPillarsY(), nres = NRes(), n = 0;
            double[] values = new double[6 * nx * ny];
            for (int k = 0; k < nres; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                    {
                        Point3D t = Pillars[i, j, k].Top();
                        Point3D b = Pillars[i, j, k].Bottom();
                        values[n++] = t.X;
                        values[n++] = t.Y;
                        values[n++] = t.Z;
                        values[n++] = b.X;
                        values[n++] = b.Y;
                        values[n++] = b.Z;
                    }
            return FileIO.EclFile.Write(grdecl_kw_coord, file, values, true);
        }



        /*
        bool WriteGRDECL(string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file, true))
                {
                    int nx = NPillarsX(), ny = NPillarsY();
                    // this.MapAxes.Write(file, FileType.GRDECL_ASCII);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_coord);
                    for (int j = 0; j < ny; ++j)
                        for (int i = 0; i < nx; ++i)
                            sw.WriteLine(Pillars[i, j].ToString());
                    sw.WriteLine("/");
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
            return true;
        }
        */








        Pillar[,] LoadPillars(int nx, int ny, string folder)
        {
            Pillar[,] result = new Pillar[nx, ny];
            using (BinaryReader reader = new BinaryReader(File.Open(folder + "\\" + FileName, FileMode.Open)))
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        result[i, j] = Pillar.Read(reader);
            }
            return result;
        }


        string _filename = string.Empty;
        string FileName
        {
            set
            {
                _filename = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_filename))
                    _filename = "co_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                return _filename;
            }
        }







    }
}
