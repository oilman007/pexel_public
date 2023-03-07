using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Pexel
{
    public class Compdat
    {
        public Compdat()
        {
            Title = string.Empty;
            Connections = new List<Index3D>();
            Checked = false;
        }
        public Compdat(string title, List<Index3D> connections, bool check)
        {
            Title = title;
            Connections = connections;
            Checked = check;
        }


        public string Title { set; get; }
        public List<Index3D> Connections { set; get; }
        public bool Checked { set; get; }




        public List<int> Layers()
        {
            List<int> result = new List<int>();
            foreach (Index3D i in Connections) result.Add(i.K);
            result.Sort();
            return result;
        }



        public void Write(BinaryWriter writer)
        {
            writer.Write(Title);
            writer.Write(Connections.Count());
            foreach (Index3D c in Connections)
                c.Write(writer);
            writer.Write(Checked);
        }


        public static Compdat Read(BinaryReader reader)
        {
            string title = reader.ReadString();
            int count = reader.ReadInt32();
            List<Index3D> cs = new List<Index3D>();
            for (int c = 0; c < count; ++c)
                cs.Add(Index3D.Read(reader));
            bool check = reader.ReadBoolean();
            return new Compdat(title, cs, check);
        }








        static public List<Compdat> Read(string file)
        {
            Dictionary<string, Compdat> result = new Dictionary<string, Compdat>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));
                foreach (string line in lines)
                {
                    string cline = ClearLine(line, "--");
                    if (string.IsNullOrEmpty(cline)) continue;
                    string[] split = cline.Split();

                    if (split.Length < 4) continue;

                    string title = split[0];
                    int i = Helper.ParseInt(split[1]) - 1;
                    int j = Helper.ParseInt(split[2]) - 1;
                    int kl = Helper.ParseInt(split[3]) - 1;
                    int ku = (split.Length < 5 ? kl : int.Parse(split[4]) - 1);

                    int k = kl;
                    if (!result.ContainsKey(title))
                        result.Add(title, new Compdat(title, new List<Index3D>() { new Index3D(i, j, k++) }, true));
                    
                    while (k <= ku)
                        result[title].Connections.Add(new Index3D(i, j, k++));
                }
            }
            catch
            {
                return result.Values.ToList();
            }
            return result.Values.ToList();
        }








        static string ClearLine(string line, string remString)
        {
            const string tabString = "\t";
            const string singlSpace = " ";
            const string doubleSpace = "  ";

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


               






    }
}
