using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Pexel.FileIO
{
    public class CSVFile
    {

       

        public static bool Read(string file, out double[][] values, char separator = ',')
        {
            System.Globalization.NumberFormatInfo numberformat = null;
            System.Globalization.CultureInfo info = System.Globalization.CultureInfo.InstalledUICulture;
            numberformat = (System.Globalization.NumberFormatInfo)info.NumberFormat.Clone();
            numberformat.NumberDecimalSeparator = ".";
            double[][] result = new double[0][];
            try
            {
                string[] lines = System.IO.File.ReadAllLines(file);
                result = new double[lines.Length][];
                Parallel.For(0, lines.Length, i =>
                {
                    string[] line = lines[i].Split(separator);
                    result[i] = new double[line.Length];
                    for (int j = 0; j < line.Length; ++j)
                        result[i][j] = Helper.ParseDouble(line[j]);
                });
            }
            catch (Exception ex)
            {
                values = result;
                return false;
            }
            values = result;
            return true;
        }


        public static bool Write(string file, double[][] values, char separator = ',')
        {
            System.Globalization.NumberFormatInfo numberformat = null;
            System.Globalization.CultureInfo info = System.Globalization.CultureInfo.InstalledUICulture;
            numberformat = (System.Globalization.NumberFormatInfo)info.NumberFormat.Clone();
            numberformat.NumberDecimalSeparator = ".";
            try
            {
                List<string> lines = new List<string>();
                foreach (double[] line in values)
                    lines.Add(string.Join(separator.ToString(), line));
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
