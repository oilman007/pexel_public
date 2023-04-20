using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;

namespace Pexel
{


    public class Actnum
    {




        public Actnum()
        {
            Init(0, 0, 0, Array.Empty<double>());
        }

        public Actnum(Actnum other)
        {
            int nx = other.NX(), ny = other.NY(), nz = other.NZ();
            Values = new int[nx, ny, nz];
            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] = other.Values[i, j, k];
            });
            NValues = other.NValues;
        }



        public Actnum(SpecGrid specGrid, string file, FileType type)
        {
            Read(specGrid, file, type);
        }



        public Actnum(SpecGrid specGrid, FileIO.EclFile eclfile)
        {
            if (eclfile.GetSingleValuesArray(grdecl_kw_actnum, out double[] values))
                Init(specGrid.NX, specGrid.NY, specGrid.NZ, values.ToArray());
        }




        public Actnum(int nx, int ny, int nz)
        {
            Init(nx, ny, nz, Enumerable.Range(1, nx * ny * nz).Select(x => (double)x).ToArray());
        }



        public Actnum(int nx, int ny, int nz, double[] values)
        {
            Init(nx, ny, nz, values);
        }




        public Actnum(Prop prop)
        {
            Init(prop.NX(), prop.NY(), prop.NZ(), prop.ValuesArray());
        }






        public int[,,] Values { set; get; } = new int[0, 0, 0];

        public int NValues { protected set; get; } = 1;






        public void Write(BinaryWriter writer)
        {
            int icount = NX();
            int jcount = NY();
            int kcount = NZ();
            writer.Write(icount);
            writer.Write(jcount);
            writer.Write(kcount);
            for (int i = 0; i < icount; ++i)
                for (int j = 0; j < jcount; ++j)
                    for (int k = 0; k < kcount; ++k)
                        writer.Write(Values[i, j, k]);
            writer.Write(NValues);
        }



        public static Actnum Read(BinaryReader reader)
        {
            int nx = reader.ReadInt32();
            int ny = reader.ReadInt32();
            int nz = reader.ReadInt32();
            Actnum result = new Actnum(nx, ny, nz);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                    for (int k = 0; k < nz; ++k)
                        result.Values[i, j, k] = reader.ReadInt32();
            result.NValues = reader.ReadInt32();
            return result;
        }







        public bool Specified()
        {
            return NX() != 0 && NY() != 0 && NZ() != 0;
        }







        bool Init(int nx, int ny, int nz, double[] values)
        {
/*            int nx = values.GetLength(0);
            int ny = values.GetLength(1);
            int nz = values.GetLength(2);*/
            if (values.Length == 0)
                return false;
            if (values.Length != nx * ny * nz)
                return false;
            Values = new int[nx, ny, nz]; 
            int n = 0;
            int nact = 0;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] = (values[n++] == 0) ? 0 : ++nact;
            NValues = nact + 1;
            return true;
        }






        public bool Read(SpecGrid specGrid, string file, FileType type)
        {
            switch (type)
            {
                case FileType.GRDECL_ASCII:
                    return ReadFromGRDECL(specGrid, file);
                default:
                    return false;
            }
        }





        public int NX()
        {
            return Values.GetLength(0);
        }

        public int NY()
        {
            return Values.GetLength(1);
        }

        public int NZ()
        {
            return Values.GetLength(2);
        }






        const string grdecl_kw_actnum = "ACTNUM";
        bool ReadFromGRDECL(SpecGrid specGrid, string file)
        {
            FileIO.EclFile ecl = new FileIO.EclFile(file);
            if (!ecl.GetSingleValuesArray(grdecl_kw_actnum, out double[] values))
                return false;
            return Init(specGrid.NX, specGrid.NY, specGrid.NZ, values.ToArray());
        }





        /*
        public bool Fill(int dx, int dy, int dz, List<string> data)
        {
            if (dx * dy * dz != data.Count)
                return false;
            Values = new bool[dx, dy, dz];
            int n = 0;
            for (int k = 0; k < dz; ++k)
                for (int j = 0; j < dy; ++j)
                    for (int i = 0; i < dx; ++i)
                    {
                        int value = int.Parse(data[n++]);
                        Values[i, j, k] = (value == 0) ? false : true;
                    }
            return true;
        }
         * */







        public bool Write(string file, FileType type)
        {
            switch (type)
            {
                /*
                case FileType.CMG_ASCII:
                    return ReadFromCMG(specGrid, file);
                    */
                case FileType.GRDECL_ASCII:
                    return WriteGRDECL(file);
                default:
                    return false;
            }
        }




        bool WriteGRDECL(string file)
        {
            int nx = NX(), ny = NY(), nz = NZ();
            double[] values = new double[nx * ny * nz];
            int n = 0;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        values[n++] = IsAct(i, j, k) ? 1 : 0;
            return FileIO.EclFile.Write(grdecl_kw_actnum, file, values, true);
        }


        public bool IsAct(int i, int j, int k)
        {
            return Values[i, j, k] != 0;
        }




    }
}
