using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Pexel
{

    [Serializable]
    public class SpecGrid
    {
        public SpecGrid()
        {
            Init();
        }
        public SpecGrid(int nx, int ny, int nz, int numRes = 1, char coordType = CartesianCoordType)
        {
            Init();
            NX = nx;
            NY = ny;
            NZ = nz;
            NumRes = numRes;
            CoordType = coordType;
        }
        public SpecGrid(string file, FileType type)
        {
            Init();
            Read(file, type);
        }
        public SpecGrid(FileIO.EclFile eclfile)
        {
            Init();
            string[] values;
            if (eclfile.GetWordsArray(grdecl_kw_specgrid, out values))
                Specify(values);
        }




        public int NX { set; get; }
        public int NY { set; get; }
        public int NZ { set; get; }
        public int NumRes { set; get; }
        public char CoordType { set; get; }






        const int defNX = 0;
        const int defNY = 0;
        const int defNZ = 0;
        const int defNumRes = 0;
        public const char DefCoordType = 'U';
        public const char RadialCoordType = 'T';
        public const char CartesianCoordType = 'F';






        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            //writer.Write(Version0);
            writer.Write(NX);
            writer.Write(NY);
            writer.Write(NZ);
            writer.Write(NumRes);
            writer.Write(CoordType);
        }


        public static SpecGrid Read(BinaryReader reader)
        {
            int nx = reader.ReadInt32();
            int ny = reader.ReadInt32();
            int nz = reader.ReadInt32();
            int numRes = reader.ReadInt32();
            char coordType = reader.ReadChar();
            return new SpecGrid(nx, ny, nz, numRes, coordType);
        }
        







        void Init()
        {
            NX = defNX;
            NY = defNY;
            NZ = defNZ;
            NumRes = defNumRes;
            CoordType = DefCoordType;
        }




        public void Clear()
        {
            Init();
        }



        public bool Specified()
        {
            return NX != defNX &&
                   NY != defNY &&
                   NZ != defNZ &&
                   NumRes != defNumRes &&
                   CoordType != DefCoordType;
        }




        void Specify(string[] values)
        {
            if (values.Length != 5)
            {
                Init();
                return;
            }
            NX = Helper.ParseInt(values[0]);
            NY = Helper.ParseInt(values[1]);
            NZ = Helper.ParseInt(values[2]);
            NumRes = Helper.ParseInt(values[3]);
            CoordType = char.Parse(values[4]);
        }





        public bool Read(string file, FileType type)
        {
            switch (type)
            {
                case FileType.CMG_ASCII:
                    return ReadFromCMG(file);
                case FileType.GRDECL_ASCII:
                    return ReadFromGRDECL(file);
                default:
                    return false;
            }
        }

        



        const string grdecl_kw_specgrid = "SPECGRID";
        bool ReadFromGRDECL (string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (Helper.ClearLine(line) == grdecl_kw_specgrid)
                        {
                            line = sr.ReadLine();
                            line = Helper.ClearLine(line);
                            string[] split = line.Split();
                            NX = Helper.ParseInt(split[0]);
                            NY = Helper.ParseInt(split[1]);
                            NZ = Helper.ParseInt(split[2]);
                            NumRes = Helper.ParseInt(split[3]);
                            CoordType = char.Parse(split[4]);
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
            return false;
        }





        const string cmg_kw_specgrid = "*GRID";
        bool ReadFromCMG(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] split = FileIO.CMGReader.ClearLine(line).Split();
                        if (split.Count() != 0 && split[0] == cmg_kw_specgrid)
                        {
                            NX = Helper.ParseInt(split[2]);
                            NY = Helper.ParseInt(split[3]);
                            NZ = Helper.ParseInt(split[4]);
                            NumRes = 1;
                            CoordType = CartesianCoordType;
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
            return false;
        }





        public bool Write(string file, FileType type)
        {
            switch (type)
            {
                /*
                case FileType.CMG_ASCII:
                    return WriteFromCMG(file);
                */
                case FileType.GRDECL_ASCII:
                    return WriteGRDECL(file);
                default:
                    return false;
            }
        }




        bool WriteGRDECL(string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file, true))
                {
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_specgrid);
                    sw.WriteLine(Helper.ShowInt(NX) + " " + Helper.ShowInt(NY) + " " + Helper.ShowInt(NZ) + " 1 F /");
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("COORDSYS");
                    sw.WriteLine("1 " + NZ + " /");
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
            return true;
        }






    }
}
