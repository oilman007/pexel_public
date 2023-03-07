using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pexel.HM
{
    public class DataTable
    {


        public DataTable() { Data = new Dictionary<string, Dictionary<DateTime, double>>(); }

        //        wellname            date       values
        //                                 (0=WBHPH, 1=WBP9H)
        Dictionary<string, Dictionary<DateTime, double>> Data { set; get; } = new Dictionary<string, Dictionary<DateTime, double>>();


        public bool Contains(string wellname) { return Data.ContainsKey(wellname); }

        public List<string> WellNames() { return Data.Keys.ToList(); }
        public DateTime LastWriteTime { set; get; }


        public RSM.SummaryVector Vector(string wellname)
        {
            RSM.SummaryVector result = new RSM.SummaryVector();
            if (Data.ContainsKey(wellname))
            {
                Dictionary<DateTime, double> temp = Data[wellname];
                result.Dates = new DateTime[temp.Count];
                result.Values = new double[temp.Count];
                int i = 0;
                foreach (KeyValuePair<DateTime, double> pair in temp)
                {
                    result.Dates[i] = pair.Key;
                    result.Values[i] = pair.Value;
                    i++;
                }
            }
            return result;
        }



        public RSM.RsmVector Vector(DateTime[] dates_requested, params string[] wellnames)
        {
            RSM.RsmVector result = new RSM.RsmVector(string.Empty, string.Empty, string.Empty, string.Empty, new double[dates_requested.Length]);
            for (int i = 0; i < wellnames.Length; ++i)
            {
                RSM.SummaryVector vector = Vector(wellnames[i]);
                List<DateTime> dates_list = vector.Dates.ToList();
                for (int j = 0; j < dates_requested.Length; ++j)
                {
                    int index = dates_list.IndexOf(dates_requested[j]);
                    if (index > -1)
                        result.Values[j] += vector.Values[index];
                }
            }
            return result;
        }



        public bool Read(string file, out List<string> problem_lines)
        {
            problem_lines = new List<string>();
            LastWriteTime = System.IO.File.GetLastWriteTime(file);
            try
            {
                string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));
                foreach (string line in lines)
                {
                    string[] items = Helper.ClearWords(line);
                    if (items.Length == 0)
                    {
                    }
                    else if (items.Length != 3 && items.Length != 4)
                    {
                        problem_lines.Add(line);
                    }
                    else
                    {
                        string well = items[0];
                        double hours = 0;
                        if (!DateTime.TryParse(items[1], out DateTime dt) || !Helper.TryParseDouble(items[2], out double value) || 
                            (items.Length != 4 ? false : !Helper.TryParseDouble(items[3], out hours)))
                        {
                            problem_lines.Add(line);
                        }
                        else
                        {
                            dt = dt.AddMinutes(Math.Round(hours * 60, 0));
                            if (Data.ContainsKey(well)) Data[well].Add(dt, value);
                            else Data.Add(well, new Dictionary<DateTime, double> { { dt, value } });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                problem_lines.Add(ex.Message);
                return false;
            }
            return true;
        }




        public bool Write(string file)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var wellitem in Data)
                {
                    string well = wellitem.Key;
                    foreach (var dateitem in wellitem.Value)
                    {
                        string date = Helper.ShowDateTimeShort(dateitem.Key);
                        string value = Helper.ShowDouble(dateitem.Value);
                        lines.Add($"{well}\t{date}\t{value}");
                    }
                }
                System.IO.File.WriteAllLines(file, lines.ToArray());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }







    }
}
