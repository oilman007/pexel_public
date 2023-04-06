using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


namespace Pexel
{

    public enum FileType { GRDECL_ASCII, CMG_ASCII, RMSWell }

    public static class Helper
    {

        //private Helper();

        static public CultureInfo MyCulture = new CultureInfo("en-US")
        {
            NumberFormat = new NumberFormatInfo()
            {
                NumberDecimalSeparator = "."
            },
            DateTimeFormat = new DateTimeFormatInfo()
            {
                DateSeparator = ".",
                TimeSeparator = ":",
                LongDatePattern = "dd.MM.yyyy HH:mm:ss",
                ShortDatePattern = "dd.MM.yyyy"
            }
        };


        // long
        public static bool TryParseLong(string number, out long value)
        {
            return long.TryParse(number, NumberStyles.Float, MyCulture, out value);
        }
        public static decimal ParseLong(string number)
        {
            return long.Parse(number, MyCulture.NumberFormat);
        }
        public static string ShowLong(long number)
        {
            return number.ToString(MyCulture.NumberFormat);
        }



        // decimal
        public static bool TryParseDecimal(string number, out decimal value)
        {
            return decimal.TryParse(number, NumberStyles.Float, MyCulture, out value);
        }
        public static decimal ParseDecimal(string number)
        {
            return decimal.Parse(number, MyCulture.NumberFormat);
        }
        public static string ShowDecimal(decimal number)
        {
            return number.ToString(MyCulture.NumberFormat);
        }


        // float
        public static bool TryParseFloat(string number, out float value)
        {
            return float.TryParse(number, NumberStyles.Float, MyCulture, out value);
        }
        public static float ParseFloat(string number)
        {
            return float.Parse(number, MyCulture.NumberFormat);
        }
        public static string ShowFloat(float number)
        {
            return number.ToString(MyCulture.NumberFormat);
        }


        // int
        public static bool TryParseInt(string number, out int value)
        {
            return int.TryParse(number, NumberStyles.Integer, MyCulture, out value);
        }
        public static int ParseInt(string number)
        {
            return int.Parse(number, MyCulture.NumberFormat);
        }
        public static string ShowInt(int number)
        {
            return number.ToString(MyCulture.NumberFormat);
        }



        // double 
        public static bool TryParseDouble(string number, out double value)
        {
            return double.TryParse(number, NumberStyles.Float, MyCulture, out value);
        }
        public static double ParseDouble(string number)
        {
            return double.Parse(number, MyCulture.NumberFormat);
        }
        public static string ShowDouble(double number, int digits = -1)
        {
            if (digits >= 0)
                number = Math.Round(number, digits);
            return number.ToString(MyCulture.NumberFormat);
        }




        // DateTime
        public static string ShowDateTimeShort(DateTime dt)
        {
            return dt.ToString(MyCulture.DateTimeFormat.ShortDatePattern);
        }
        public static string ShowDateTimeLong(DateTime dt)
        {
            return dt.ToString(MyCulture.DateTimeFormat.LongDatePattern);
        }
        public static DateTime ParseDateTime(string dt)
        {
            return DateTime.Parse(dt, MyCulture.DateTimeFormat);
        }



        // string
        public static string ToUpper(string text)
        {
            return text?.ToUpper(MyCulture);
        }

        public static string ToLower(string text)
        {
            return text?.ToLower(MyCulture);
        }

        const string RemString = "--";
        const string TabString = "\t";
        const string SingleSpace = " ";
        const string DoubleSpace = "  ";
        const string Terminator = "/";
        const char Repeator = '*';

        static string ClearWS(string text)
        {
/*            StringBuilder stringBuilder = new StringBuilder(text);
            stringBuilder.*/

            return Regex.Replace(text, @"\s+", " ", RegexOptions.Multiline).Trim();
        }

        public static string ClearLine(string line, bool slash_is_comment = true)
        {
            //StringBuilder sb = new StringBuilder(line);
            int index = line.IndexOf(RemString, StringComparison.OrdinalIgnoreCase);
            if (index != -1)
                line = line.Remove(index);

            if (slash_is_comment)
            {
                index = line.IndexOf(Terminator, StringComparison.OrdinalIgnoreCase);
                if (index != -1 && index + 1 < line.Length)
                    line = line.Remove(index + 1);
            }

            line = line.Replace(TabString, SingleSpace);
            while (line.Contains(DoubleSpace))
                line = line.Replace(DoubleSpace, SingleSpace);
            line = line.Trim();
            
            //line = ClearWS(line);
            line = ToUpper(line);
            return line;
        }
               
        public static string[] ClearLines(string filename)
        {
            List<string> result = new List<string>();
            string[] lines = ReadLines(filename);
            const int lpt = 10000;
            int nl = lines.Length;
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, t =>
            {
                int first = t * lpt, last = Math.Min(first + lpt, nl);
                for (int i = first; i < last; ++i)
                    lines[i] = ClearLine(lines[i]);
            });
            foreach (string line in lines)
                if (!string.IsNullOrEmpty(line)) result.Add(line);
            return result.ToArray();
        }
                             
        public static string[] ReadLines(string file)
        {
            List<string> lines = new List<string>();
            using (FileStream s = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    while (sr.Peek() >= 0)
                    {
                        lines.Add(sr.ReadLine());
                    }
                }
            }
            return lines.ToArray();
        }

        public static string[] ClearWords(string line)
        {
            string cline = ClearLine(line);
            if (string.IsNullOrEmpty(cline))
                return Array.Empty<string>();
            string[] clearwords = cline.Split();
            List<string> result = new List<string>();
            foreach (string word in clearwords)
            {
                if (!word.Contains(Repeator))
                    result.Add(word);
                else
                {
                    string[] split = word.Split(Repeator);
                    if (split.Length == 1)
                        result.Add(word);
                    else
                    {
                        int count = ParseInt(split[0]);
                        for (int i = 0; i < count; ++i)
                            result.Add(split[1]);
                    }
                }
            }
            return result.ToArray();
        }
               
        public static string[] KWContains(string file)
        {
            List<string> r = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        /*
                        line = ClearWords(line);
                        if (line == string.Empty)
                            continue;
                            */
                        string[] words = ClearWords(line);
                        if (words.Length != 1)
                            continue;
                        if (words[0] == Terminator)
                            continue;
                        r.Add(line);
                    }
                }
            }
            catch
            {
                r.Clear();
                return r.ToArray();
            }
            return r.ToArray();         
        }




        public static double Bound(double min, double value, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }


        public static decimal Bound(decimal min, decimal value, decimal max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }


        public static int Bound(int min, int value, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }






        public static string GetRefFolder(string base_file, string other_file)
        {
            string[] base_split = base_file.Split('\\');
            string[] other_split = other_file.Split('\\');

            int base_count = base_split.Length - 1;
            int other_count = other_split.Length;
            int count = Math.Min(base_count, other_count);

            if (count <= 0)
                return string.Empty;

            int n = 0;
            for (; n < count; ++n)
                if (base_split[n].ToUpper() != other_split[n].ToUpper())
                    if (n == 0)
                        return other_file;
                    else
                        break;

            string result = string.Empty;
            for (int i = n; i < base_count; ++i)
                result += "\\..";
            for (int i = n; i < other_count; ++i)
                result += "\\" + other_split[i];

            return result;
        }


        public static string GetNewPath(string old_ref_file, string new_ref_file, string old_path)
        {
            try
            {
                string new_folder = GetRefFolder(old_ref_file, old_path);
                string new_path = Path.GetDirectoryName(new_ref_file) + new_folder;
                return Path.GetFullPath(new_path);
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string GetNewPathIfExists(string old_ref_file, string new_ref_file, string old_path)
        {
            string new_path = GetNewPath(old_ref_file, new_ref_file, old_path);
            return File.Exists(new_path) ? new_path : string.Empty;
        }



    }
}
