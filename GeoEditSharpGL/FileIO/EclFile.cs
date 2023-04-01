using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Pexel.FileIO
{

    public enum ContentType { KEYWORD, DATA, TERMINATOR, END_OF_FILE }

    public class EclFile
    {
        public EclFile()
        {
            Init();
            Content = new string[0];
        }

        public EclFile(string file)
        {
            Init();
            Content = new string[0];
            Read(file);
        }


        private string[] Content { set; get; }
        private ContentType[][] ContentTypes { set; get; }

        public const string RemString = "--";
        public const string TabString = "\t";
        public const string SingleSpace = " ";
        public const string DoubleSpace = "  ";
        public const string Terminator = "/";
        public const char Repeator = '*';


        System.Globalization.NumberFormatInfo numberformat = null;
        void Init()
        {
            System.Globalization.CultureInfo info = System.Globalization.CultureInfo.InstalledUICulture;
            numberformat = (System.Globalization.NumberFormatInfo)info.NumberFormat.Clone();
            numberformat.NumberDecimalSeparator = ".";
        }


        const int lpt = 100000;
        const long ecl_file_aver_length = 30;
        public bool Read(string file)
        {
            try
            {
                List<string> result = new List<string>();
                string[] lines = Helper.ReadLines(file);
                int nl = lines.Length;
                int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
                Parallel.For(0, nt, t =>
                {
                    int first = t * lpt, last = Math.Min(first + lpt, nl);
                    for (int i = first; i < last; ++i)
                        lines[i] = Helper.ClearLine(lines[i]);
                });
                foreach (string line in lines)
                    if (!string.IsNullOrEmpty(line)) result.Add(line);
                Content = result.ToArray();
            }
            catch (Exception ex)
            {
                return false;
            }
            ContentTypes = GetContentTypes(Content);
            return true;
        }


        static ContentType[][] GetContentTypes(string[] content)
        {
            ContentType[][] result = new ContentType[content.Length][];
            int nl = content.Length;
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, t =>
            {
                int first = t * lpt, last = Math.Min(first + lpt, nl);
                for (int i = first; i < last; ++i)
                    result[i] = GetContentType(content[i]);
            });
            List<ContentType> temp = new List<ContentType>(result[content.Length - 1]);
            temp.Add(ContentType.END_OF_FILE);
            result[content.Length - 1] = temp.ToArray();
            return result;
        }


        static ContentType[] GetContentType(string content)
        {
            if (content == Terminator)
                return new ContentType[] { ContentType.TERMINATOR };
            string[] split = content.Split();
            if (split.Length == 1 && !double.TryParse(content, out _))
                return new ContentType[] { ContentType.KEYWORD };
            if (split.Last() == Terminator)
                return new ContentType[] { ContentType.DATA, ContentType.TERMINATOR };
            return new ContentType[] { ContentType.DATA };
        }



        bool GetLineKW(string kw, out int nline, int start = 0)
        {
            List<int> find = new List<int>();
            int finish = Content.Length;
            int nl = finish - start;
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, (t, loopState) =>
            {
                int first = t * lpt + start;
                int last = Math.Min(first + lpt, finish);
                for (int c = first; c < last; ++c)
                    if (ContentTypes[c].Contains(ContentType.KEYWORD) && Content[c] == kw)
                        lock (find) find.Add(c);
            });
            nline = find.Count != 0 ? find.Min() : start;
            return find.Count != 0;
        }


        bool GetLineType(IEnumerable<ContentType> types, out int nline, int start = 0)
        {
            List<int> find = new List<int>();
            int finish = Content.Length;
            int nl = finish - start;
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, (t, loopState) =>
            {
                int first = t * lpt + start;
                int last = Math.Min(first + lpt, finish);
                for (int c = first; c < last; ++c)
                    if (types.Intersect(ContentTypes[c]).Count() > 0)
                        lock (find) find.Add(c);
            });
            nline = find.Count != 0 ? find.Min() : start;
            return find.Count != 0;
        }



        public bool GetWordsArrays(string kw, out string[][] values, params ContentType[] endings)
        {
            if (Content.Length == 0 || !GetLineKW(kw, out int start) || !GetLineType(endings, out int finish, ++start))
            {
                values = Array.Empty<string[]>();
                return false;
            }
            int nl = ++finish - start;
            string[][] splits = new string[nl][];
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, (t) =>
            {
                int first = t * lpt + start;
                int last = Math.Min(first + lpt, finish);
                for (int c = first; c < last; ++c)
                    splits[c - start] = Split(Content[c]);
            });
            List<string> temp = new List<string>(splits.SelectMany(x => x));
            temp.RemoveAt(temp.Count - 1);
            values = CutByTerminator(temp);
            return true;
        }



        string[][] CutByTerminator(List<string> values)
        {
            List<int> terms = new List<int>();
            int i = 0;
            foreach (string v in values)
            {
                if (v == Terminator) terms.Add(i);
                i++;
            }
            string[][] result = new string[terms.Count][];
            i = 0;
            int s = 0;
            foreach (int t in terms)
            {
                result[i++] = values.Skip(s).Take(t - s).ToArray();
                s = t + 1;
            }
            return result;
        }
             



        bool GetLineEquals(string exp, out int nline, int start = 0)
        {
            List<int> find = new List<int>();
            int finish = Content.Length;
            int nl = finish - start;
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, (t, loopState) =>
            {
                int first = t * lpt + start;
                int last = Math.Min(first + lpt, finish);
                for (int c = first; c < last; ++c)
                    if (Content[c] == exp)
                        lock (find) find.Add(c);
            });
            nline = find.Count != 0 ? find.Min() : start;
            return find.Count != 0;
        }


        bool GetLineContains(string exp, out int nline, int start = 0)
        {
            List<int> find = new List<int>();
            int finish = Content.Length;
            int nl = finish - start;
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, (t, loopState) =>
            {
                int first = t * lpt + start;
                int last = Math.Min(first + lpt, finish);
                for (int c = first; c < last; ++c)
                    if (Content[c].Contains(exp))
                        lock (find) find.Add(c);
            });
            nline = find.Count != 0 ? find.Min() : start;
            return find.Count != 0;
        }



        string[] Split(string clear_line)
        {
            List<string> result = new List<string>();
            string[] words = clear_line.Split();
            foreach (string word in words)
            {
                if (!word.Contains(Repeator)) result.Add(word);
                else
                {
                    string[] split = word.Split(Repeator);
                    int count = Helper.ParseInt(split[0]);
                    string value = (split.Length == 2) ? split[1] : "1*";
                    for (int i = 0; i < count; ++i) result.Add(value);
                }
            }
            return result.ToArray();
        }



        const int vpt = 10000;
        public bool GetSingleValuesArray(string kw, out double[] values)
        {
            try
            {
                if (!GetWordsArray(kw, out string[] words))
                {
                    values = Array.Empty<double>();
                    return false;
                }
                int nv = words.Length;
                int nt = nv / vpt + (nv % vpt > 0 ? 1 : 0);
                double[] result = new double[nv];
                Parallel.For(0, nt, t =>
                {
                    int first = t * vpt, last = Math.Min(first + vpt, nv);
                    for (int i = first; i < last; ++i)
                        result[i] = Helper.ParseDouble(words[i]);
                });
                values = result;
            }
            catch (Exception ex)
            {
                values = Array.Empty<double>();
                return false;
            }
            return true;
        }



        public bool GetWordsArray(string kw, out string[] values)
        {
            if (Content.Length == 0 || !GetLineEquals(kw, out int start) || !GetLineContains(Terminator, out int finish, ++start))
            {
                values = Array.Empty<string>();
                return false;
            }
            int nl = ++finish - start;
            string[][] splits = new string[nl][];
            int nt = nl / lpt + (nl % lpt > 0 ? 1 : 0);
            Parallel.For(0, nt, (t) =>
            {
                int first = t * lpt + start;
                int last = Math.Min(first + lpt, finish);
                for (int c = first; c < last; ++c)
                    splits[c - start] = Split(Content[c]);
            });
            List<string> result = new List<string>();
            foreach (string[] split in splits)
                result.AddRange(split);
            result.RemoveAt(result.Count - 1);
            values = result.ToArray();
            return true;
        }



        const double def_val = -999;
        const int line_size = 80;

        public static bool Write(string kw, string file, double[] values, bool append = false, params string[] comments)
        {
            int n = 0;
            double prev_v = def_val;
            List<string> line_items = new List<string>();
            foreach (double v in values)
            {
                if (n == 0) { prev_v = v; n++; }
                else if (prev_v == v) n++;
                else
                {
                    line_items.Add(LineItem(prev_v, n));
                    prev_v = v;
                    n = 1;
                }
            }
            line_items.Add(LineItem(prev_v, n));
            line_items.Add(SingleSpace);
            line_items.Add(Terminator);
            return Write(kw, file, line_items.ToArray(), append, comments);
        }
        

        static string LineItem(double value, int n)
        {
            return SingleSpace + ((n > 1) ? Helper.ShowInt(n) + Repeator : string.Empty) + Helper.ShowDouble(value);
        }



        static bool Write(string kw, string file, string[] line_items, bool append = false, params string[] comments)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file, append))
                {
                    sw.WriteLine(string.Empty);
                    if (comments.Length > 0)
                    {
                        foreach (string comment in comments)
                            sw.WriteLine(RemString + SingleSpace + comment);
                        sw.WriteLine(string.Empty);
                    }
                    sw.WriteLine(kw);
                    string line = string.Empty;
                    foreach (string item in line_items)
                    {
                        if (line.Length + item.Length >= line_size)
                        {
                            sw.WriteLine(line);
                            line = item;
                        }
                        else line += item;
                    }
                    sw.WriteLine(line);
                }
            }
            catch { return false; }
            return true;
        }







    }
}
