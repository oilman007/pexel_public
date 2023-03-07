using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Pexel.FileIO
{
    public class CMGReader
    {
        const string remString = "**";
        const string tabString = "\t";
        const string singlSpace = " ";
        const string doubleSpace = "  ";


        public static string ClearLine(string line)
        {
            int index = line.IndexOf(remString);
            if (index != -1)
                line = line.Remove(index);
            line = line.Replace(tabString, singlSpace);
            while (line.Contains(doubleSpace))
                line = line.Replace(doubleSpace, singlSpace);
            line = line.Trim();
            line = line.ToUpper();
            return line;
        }



        public static List<string> KWContains(string file)
        {
            List<string> r = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = ClearLine(line);
                        if (line == string.Empty)
                            continue;
                        if (line[0] != '*')
                            continue;
                        r.Add(line);
                    }
                }
            }
            catch (Exception)
            {
                r.Clear();
                return r;
            }
            return r;
        }




    }
}
