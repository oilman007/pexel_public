using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pexel.Simulator
{
    public class EclipseRunner : SimulatorRunner
    {
        public bool UseMPI { set; get; }
        public string MpiEXEfile { set; get; }
        public string EclipseEXEfile { set; get; }
        public int CPU { set; get; }



        override protected List<Tuple<string, string>> GetLines(string data_file)
        {
            List<Tuple<string, string>> lines = new List<Tuple<string, string>>();

            //string mpi = UseMPI ? $"  mpiexec -localonly -np {CPU}  " : string.Empty;
            /*
            string mpi = UseMPI ? $" -localonly -np {CPU} " : string.Empty;
            string filename = QQ(UseMPI ? MpiEXEfile : EclipseEXEfile);
            string arguments = mpi + QQ(Path.ChangeExtension(DataFile, null));
            */

            string filename = EclipseEXEfile;
            string arguments = $"-localonly -np {CPU} {QQ(MpiEXEfile)} {QQ(Path.ChangeExtension(data_file, null))}";

            lines.Add(new Tuple<string, string>(filename, arguments));
            return lines;
        }
    }
}
