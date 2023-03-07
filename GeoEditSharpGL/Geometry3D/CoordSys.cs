using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Pexel
{
    public partial class CoordSys
    {
        public CoordSys()
        {
            Init();
        }


        public CoordSys(FileIO.EclFile eclfile)
        {
            Init();
            if (eclfile.GetWordsArrays(grdecl_kw_coordsys, out string[][] values, 
                                       FileIO.ContentType.KEYWORD, FileIO.ContentType.END_OF_FILE))
                Init(values);
        }


        public CoordSys(SpecGrid specGrid, string file, FileType type)
        {
            Init();
            Fill(specGrid, file, type);
        }


        public CoordSys(int nz)
        {
            Init(nz);
        }

        public CoordSysRecord[] Records { set; get; } = Array.Empty<CoordSysRecord>();



        public bool Fill(SpecGrid specGrid, string file, FileType type)
        {
            switch (type)
            {
                case FileType.GRDECL_ASCII:
                    return FillFromGRDECL(specGrid, file);
                default:
                    return false;
            }
        }




        const string grdecl_kw_coordsys = "COORDSYS";
        public bool FillFromGRDECL(SpecGrid specGrid, string file)
        {
            //List<string>[] values = GRDECL_IO.Array(file, grdecl_kw_coordsys, specGrid.NumRes);
            List<CoordSysRecord> records = new List<CoordSysRecord>();
            List<string>[] values = new List<string>[0];
            if (values.Length != specGrid.NumRes)
                return false;
            foreach (List<string> record in values)
                if (record.Count < 2)
                    return false;
                else
                {
                    int lb = Helper.ParseInt(record[0]) - 1;
                    int ub = Helper.ParseInt(record[1]) - 1;
                    if (record.Count == 2)
                        records.Add(new CoordSysRecord(lb, ub));
                    else
                        records.Add(new CoordSysRecord(lb, ub, record[2]));
                }
            Records = records.ToArray();
            return true;
        }








        public bool Specified(SpecGrid sg)
        {
            return sg.NumRes == Records.Length;
        }


        void Init()
        {
            Records = Array.Empty<CoordSysRecord>();
        }

        void Init(int nz)
        {
            Records = new CoordSysRecord[] { new CoordSysRecord(0, nz - 1) };
        }



        void Init(string[][] values)
        {
            List<CoordSysRecord> temp = new List<CoordSysRecord>();
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Length == 2)
                    temp.Add(new CoordSysRecord(Helper.ParseInt(values[i][0]) - 1, Helper.ParseInt(values[i][1]) - 1));
                else if (values[i].Length == 3)
                    temp.Add(new CoordSysRecord(Helper.ParseInt(values[i][0]) - 1, Helper.ParseInt(values[i][1]) - 1, values[i][2]));
            }
            Records = temp.ToArray();
        }




        public int Number(int k)
        {
            int i = 0;
            foreach (CoordSysRecord r in Records)
                if (r.LBound <= k && k <= r.UBound)
                    return i;
                else ++i;
            return 0;
        }






        public void Write(BinaryWriter writer)
        {
            writer.Write(this.Records.Length);
            foreach (CoordSysRecord r in Records)
                r.Write(writer);
        }



        public static CoordSys Read(BinaryReader reader)
        {
            int n = reader.ReadInt32();
            CoordSys result = new CoordSys() { Records = new CoordSysRecord[n] };
            for (int i = 0; i < n; i++)
                result.Records[i] = CoordSysRecord.Read(reader);
            return result;
        }



    }













    public class CoordSysRecord
    {
        public CoordSysRecord()
        {
            LBound = 0;
            UBound = int.MaxValue;
            CompletionOfCircle = "INCOMP";
        }

        public CoordSysRecord(int lb, int ub, string coc = "INCOMP")
        {
            LBound = lb;
            UBound = ub;
            CompletionOfCircle = coc;
        }


        public int LBound { set; get; }
        public int UBound { set; get; }
        public string CompletionOfCircle { set; get; }



        public void Write(BinaryWriter writer)
        {
            writer.Write(LBound);
            writer.Write(UBound);
            writer.Write(CompletionOfCircle);
        }


        public static CoordSysRecord Read(BinaryReader reader)
        {
            return new CoordSysRecord(reader.ReadInt32(), reader.ReadInt32(), reader.ReadString());
        }



    }














}
