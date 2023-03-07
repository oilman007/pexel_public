using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace Pexel.RSM
{

    public enum RsmType { Eclipse, Tempest, tNavigator }
    public enum RsmVectorType { Unknown, Field, Group, Well, Connection, Region, Block, Aquifer }


    public class Rsm
    {
        public Rsm()
        {
            Clear();
        }


        public Rsm(string filename)
        {
            Clear();
            Read(filename);
        }



        void Clear()
        {
            NumberFormatInit();
            Title = string.Empty;
            Dates = Array.Empty<DateTime>();
            Vectors = Array.Empty<RsmVector>();
        }


        public string Title { set; get; }
        public DateTime[] Dates { set; get; } = Array.Empty<DateTime>();
        public RsmVector[] Vectors { set; get; } = Array.Empty<RsmVector>();
        public DateTime LastWriteTime { set; get; }
        
        public List<string> VectorNames()
        {
            List<string> result = new List<string>();
            foreach (RsmVector v in Vectors) if (!result.Contains(v.Title)) result.Add(v.Title);
            return result;
        }


        public Rsm SubRsm(string vectorName)
        {
            Rsm result = new Rsm();
            result.Title = this.Title;
            result.Dates = this.Dates;
            List<RsmVector> temp = new List<RsmVector>();
            foreach (RsmVector vector in Vectors)
                if (vector.Title == vectorName)
                    temp.Add(vector);
            result.Vectors = temp.ToArray();
            return result;
        }





        public SummaryVector SummaryVector(string vectorName, string ID)
        {
            foreach (RsmVector vector in Vectors)
                if (vector.Title == vectorName && vector.ID == ID)
                    return new SummaryVector(vector.Title, vector.ID, vector.Units, vector.Info, Dates.ToArray(), vector.Values.ToArray());
            return new SummaryVector();
        }



        public List<SummaryVector> NameVectors(string vectorName)
        {
            List<SummaryVector> result = new List<SummaryVector>();
            foreach (RsmVector vector in Vectors)
                if (vector.Title == vectorName)
                    result.Add(new SummaryVector(vector.Title, vector.ID, vector.Units, vector.Info, Dates.ToArray(), vector.Values.ToArray()));
            return result; 
        }

        public List<SummaryVector> NameVectors(string vectorName, DateTime firstDate, DateTime lastDate)
        {
            List<SummaryVector> result = new List<SummaryVector>();
            foreach (RsmVector vector in Vectors)
                if (vector.Title == vectorName)
                    result.Add((new SummaryVector(vector.Title, vector.ID, vector.Units, vector.Info, Dates.ToArray(), vector.Values.ToArray())).
                                                    SubVector(firstDate, lastDate));
            return result;
        }


        public List<SummaryVector> IDVectors(string ID)
        {
            List<SummaryVector> result = new List<SummaryVector>();
            foreach (RsmVector vector in Vectors)
                if (vector.ID == ID)
                    result.Add(new SummaryVector(vector.Title, vector.ID, vector.Units, vector.Info, Dates.ToArray(), vector.Values.ToArray()));
            return result;
        }


        public List<SummaryVector> IDVectors(string ID, DateTime firstDate, DateTime lastDate)
        {
            List<SummaryVector> result = new List<SummaryVector>();
            foreach (RsmVector vector in Vectors)
                if (vector.ID == ID)
                    result.Add((new SummaryVector(vector.Title, vector.ID, vector.Units, vector.Info, Dates.ToArray(), vector.Values.ToArray())).
                                                    SubVector(firstDate,lastDate));
            return result;
        }




        const int columnLength = 13;
        const int startPos = 1;
        const int lineLenght = 131;
        List<string> TempestColumns(string line)
        {
            line = line.PadRight(lineLenght);
            List<string> r = new List<string>();
            for (int i = startPos; i  < line.Length; i += columnLength)
                r.Add(Helper.ToUpper(line.Substring(i , columnLength).Trim()));
            return r;
        }


        const char separator = '\t';
        List<string> EclipseColumns(string line)
        {
            string[] split = line.Split(separator);
            List<string> result = new List<string>();
            int count = split.Count();
            for (int i = 1; i < count; ++i)
                result.Add(Helper.ToUpper(split[i].Trim()));
            return result;
        }



        static System.Globalization.NumberFormatInfo numberformat = null;
        void NumberFormatInit()
        {
            System.Globalization.CultureInfo info = System.Globalization.CultureInfo.InstalledUICulture;
            numberformat = (System.Globalization.NumberFormatInfo)info.NumberFormat.Clone();
            numberformat.NumberDecimalSeparator = ".";
            //double duration = double.Parse("0.125", numberformat);
        }


        const long rsm_line_length = 130;
        public bool Read(string filename)
        {
            try
            {
                Clear();
                List<DateTime> dates = new List<DateTime>();
                List<RsmVector> vectors = new List<RsmVector>();
                // start
                this.LastWriteTime = File.GetLastWriteTime(filename);
                string[] lines = Helper.ReadLines(filename);
                const string border = " -----";
                List<int> borders = new List<int>();
                for (int i = 0; i < lines.Length; ++i)
                    if (lines[i].Length >= border.Length && lines[i].Substring(0, border.Length) == border) borders.Add(i);
                int b = 0, bpt = 3, nt = borders.Count / bpt;
                int[,] indexes = new int[nt, 4];
                borders.Add(lines.Length + 1);
                for (int i = 0; i < nt; ++i)
                {
                    indexes[i, 0] = borders[b++] + 1; // SUMMARY OF RUN
                    indexes[i, 1] = borders[b++] + 1; // Header
                    indexes[i, 2] = borders[b++] + 1; // data first line
                    indexes[i, 3] = borders[b]   - 1; // data last + 1 line
                }
                // Title
                const string summary = " SUMMARY OF RUN";
                Title = lines[indexes[0, 0]].Substring(summary.Length).Trim();
                // Dates
                DateTime prev_dt = new DateTime();
                double prev_years = 0;
                for (int l = indexes[0, 2]; l < indexes[0, 3]; ++l)
                {
                    List<string> columns = TempestColumns(lines[l]);
                    DateTime dt = Helper.ParseDateTime(columns[0]);
                    double years = Helper.ParseDouble(columns[1]);
                    if (prev_dt != dt)
                    {
                        prev_dt = dt;
                        prev_years = years;
                    }
                    else
                    {
                        const double days_per_year = 365;
                        dt = dt.AddMinutes(Math.Round((years - prev_years) * days_per_year * 24 * 60, 0));
                    }
                    dates.Add(dt);
                }
                // Values
                Parallel.For(0, nt, t =>
                //for(int t = 0; t<nt; ++t)
                {
                    // header
                    int l = indexes[t, 1];
                    List<string> columns = TempestColumns(lines[l++]);
                    List<string> temp = new List<string>();
                    for (int i = 1; i < columns.Count; ++i)
                    {
                        if (string.IsNullOrEmpty(columns[i])) continue;
                        temp.Add(columns[i]);
                    }
                    int ncol = temp.Count;
                    RsmVector[] result = new RsmVector[ncol];
                    for (int i = 0; i < temp.Count; ++i)
                    {
                        result[i] = new RsmVector();
                        result[i].Title = temp[i];
                        result[i].Values = new double[dates.Count];
                    }
                    // units
                    columns = TempestColumns(lines[l++]);
                    for (int i = 0; i < ncol; ++i) result[i].Units = columns[i + 1];
                    // power
                    const double def_power = 1.0f;
                    List<double> powers = new List<double>();
                    for (int i = 0; i < ncol; ++i) powers.Add(def_power);
                    const string power_str = "*10**";
                    if (string.IsNullOrEmpty(lines[l].Trim()))
                    {
                        l++;
                    }
                    else if (lines[l].Contains(power_str)) // else if ?????
                    {
                        columns = TempestColumns(lines[l++]);
                        for (int i = 0; i < ncol; ++i)
                            if (!string.IsNullOrEmpty(columns[i + 1]) && columns[i + 1].Substring(0, power_str.Length) == power_str)
                                powers[i] = Helper.ParseDouble(columns[i + 1].Substring(power_str.Length));
                    }
                    // ID
                    columns = TempestColumns(lines[l++]);
                    for (int i = 0; i < ncol; ++i) result[i].ID = columns[i + 1];
                    // info
                    columns = TempestColumns(lines[l]);
                    for (int i = 0; i < ncol; ++i) result[i].Info = columns[i + 1];
                    // data
                    int d = 0;
                    for (l = indexes[t, 2]; l < indexes[t, 3]; ++l, ++d)
                    {
                        columns = TempestColumns(lines[l]);
                        for (int i = 0; i < ncol; ++i)
                        {
                            // исправляю косяки в RSM
                            if (columns[i + 1].Last() == 'E')
                                columns[i + 1] = columns[i + 1].Substring(0, columns[i + 1].Length - 1);
                            if (columns[i + 1].Last() == '-' ||
                                columns[i + 1].Last() == '+')
                                columns[i + 1] = columns[i + 1].Substring(0, columns[i + 1].Length - 2);
                            double value = Helper.ParseDouble(columns[i + 1]);
                            //
                            double power = powers[i];
                            if (power != def_power) value *= Math.Pow(10.0, power);
                            result[i].Values[d] = value;
                        }
                    }
                    lock (vectors) { vectors.AddRange(result); }
                });
                // finish
                Dates = dates.ToArray();
                Vectors = vectors.ToArray();
            }
            catch (Exception ex)
            {
                Clear();
                return false;
            }
            return true;
        }



    }



    



}
