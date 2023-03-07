using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Simulator
{
    public class tNavigatorRunner : SimulatorRunner
    {
        public bool UseUserSumFile { set; get; }
        public string tNavigatorEXEfile { set; get; }
        public int CPU { set; get; }
        public bool UseGPU { set; get; }
        public int GPUDevice { set; get; }


        override protected List<Tuple<string, string>> GetLines(string data_file)
        {
            List<Tuple<string, string>> lines = new List<Tuple<string, string>>();

            if (UseUserSumFile)
                CreateUserSumFile(data_file, HM.IterationResult.ExpectedSUMMARY());
            string filename = QQ(tNavigatorEXEfile);
            string arguments = $"--cpu-num={CPU} {(UseGPU ? $"--use-gpu --gpu-device={GPUDevice} " : "")}--no-gui --ecl-rsm  {QQ(data_file)}";

            lines.Add(new Tuple<string, string>(filename, arguments));
            return lines;
        }


    }
}
