using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;


namespace Pexel
{
    public class MapAxes
    {


        public MapAxes()
        {
            Init();
        }



        public MapAxes(string file, FileType type)
        {
            Init();
            Read(file, type);
        }



        public MapAxes(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
        }


        public MapAxes(FileIO.EclFile eclfile)
        {
            Init();
            if (eclfile.GetSingleValuesArray(grdecl_kw_mapaxes, out double[] values))
                Specify(values);
        }




        public double X1 { set; get; }
        public double Y1 { set; get; }
        public double X2 { set; get; }
        public double Y2 { set; get; }
        public double X3 { set; get; }
        public double Y3 { set; get; }



        public void Write(BinaryWriter writer)
        {
            writer.Write(X1);
            writer.Write(Y1);
            writer.Write(X2);
            writer.Write(Y2);
            writer.Write(X3);
            writer.Write(Y3);
        }



        public static MapAxes Read(BinaryReader reader)
        {
            return new MapAxes(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(),
                               reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }





        void Init()
        {
            X1 = 0;
            Y1 = 0;
            X2 = 0;
            Y2 = 0;
            X3 = 0;
            Y3 = 0;
        }




        public void Clear()
        {
            Init();
        }




        void Specify(double[] values)
        {
            const int valuesNeeded = 6;
            if (values.Length != valuesNeeded)
            {
                Init();
                return;
            }
            X1 = values[0];
            Y1 = values[1];
            X2 = values[2];
            Y2 = values[3];
            X3 = values[4];
            Y3 = values[5];
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





        const string grdecl_kw_mapaxes = "MAPAXES";
        bool ReadFromGRDECL(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (Helper.ClearLine(line) == grdecl_kw_mapaxes)
                            break;
                    List<string> values = new List<string>();
                    const int valuesNeeded = 6;
                    while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null)
                    {
                        line = Helper.ClearLine(line);
                        if (!string.IsNullOrEmpty(line))
                            foreach (string word in line.Split())
                                values.Add(word);
                    }
                    X1 = Helper.ParseDouble(values[0]);
                    Y1 = Helper.ParseDouble(values[1]);
                    X2 = Helper.ParseDouble(values[2]);
                    Y2 = Helper.ParseDouble(values[3]);
                    X3 = Helper.ParseDouble(values[4]);
                    Y3 = Helper.ParseDouble(values[5]);
                    return true;
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
        }





        bool ReadFromCMG(string file)
        {
            Init();
            return true;
        }









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
            try
            {
                using (StreamWriter sw = new StreamWriter(file, true))
                {
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_mapaxes);
                    sw.WriteLine(Helper.ShowDouble(X1) + " " + Helper.ShowDouble(Y1) + " " +
                                 Helper.ShowDouble(X2) + " " + Helper.ShowDouble(Y2) + " " +
                                 Helper.ShowDouble(X3) + " " + Helper.ShowDouble(Y3) + " /");
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }








    }
}
