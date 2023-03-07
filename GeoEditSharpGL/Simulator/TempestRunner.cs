using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pexel.Simulator
{
    public class TempestRunner : SimulatorRunner
    {
        public string MoredBatfile { set; get; }
        public int CPU { set; get; }



        override protected List<Tuple<string, string>> GetLines(string data_file)
        {
            List<Tuple<string, string>> lines = new List<Tuple<string, string>>();

            string filename = QQ(MoredBatfile);
            string arguments = string.Join(" ", $"-np {CPU}", QQ(data_file));
            lines.Add(new Tuple<string, string>(filename, arguments));

            string project_title = Path.GetFileNameWithoutExtension(data_file);
            string crf_file = Path.ChangeExtension(data_file, ".crf");
            string tprj_folder = Path.ChangeExtension(data_file, ".tprj");
            using (StreamWriter writer = new System.IO.StreamWriter(crf_file))
            {
                string dir = Path.GetDirectoryName(data_file);
                writer.WriteLine($"LoadSimProject({QQ(tprj_folder)})");
                writer.WriteLine($"ExportEclRsmEx({QQ(project_title)}, {QQ("ALL")}, {QQ("dd-MMM-yyyy")}, {QQ(dir)}, {QQ(project_title)})");
                writer.Close();
            }
            lines.Add(new Tuple<string, string>("tempest", $"-batch -crf {QQ(crf_file)}"));

            FilesToDelete.Add(crf_file);

            //if (!TprjContainsOutputpath(project_file))
            FixTprj(tprj_folder, project_title);

            lines.Add(new Tuple<string, string>(filename, arguments));
            return lines;
        }
    }
}
