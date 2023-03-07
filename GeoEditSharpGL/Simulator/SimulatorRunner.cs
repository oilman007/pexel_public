using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace Pexel.Simulator
{

    public enum SimulatorType { Eclipse, CMG, Tempest, tNavigator }


    public class SimulatorRunner
    {

        public Process RunProcess { set; get; } = new Process();
        protected List<string> FilesToDelete { set; get; } = new List<string>();

        public delegate void SimulatorRunnerHandler(string data_file);
        public SimulatorRunnerHandler RunStarted;
        public SimulatorRunnerHandler RunFinished;




        public void KillRunProcess(object o, EventArgs e)
        {
            try { RunProcess?.Kill(); } catch { }
        }



        public bool SimulateCase(string data_file)
        {
            try
            {
                FilesToDelete = new List<string>();
                List<Tuple<string, string>> lines = GetLines(data_file);
                RunStarted?.Invoke(data_file);
                foreach (Tuple<string, string> line in lines)
                {
                    ProcessStartInfo info = new ProcessStartInfo(line.Item1, line.Item2)
                    {
                        WorkingDirectory = Path.GetDirectoryName(data_file),
                        UseShellExecute = true,
                        //if (System.Environment.OSVersion.Version.Major >= 6) { info.Verb = "runas"; }
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    using (RunProcess = Process.Start(info)) { RunProcess.WaitForExit(); }
                }
#if !DEBUG
                foreach (string file in FilesToDelete) if (File.Exists(file)) File.Delete(file);
#endif
            }
            catch
            {
                RunFinished?.Invoke(data_file);
                return false;
            }
            RunFinished?.Invoke(data_file);
            return true;
        }


        protected static string QQ(string input)
        {
            return "\"" + input + "\"";
        }


        protected static bool CreateUserSumFile(string dataFile, string[] user_sum_file)
        {
            try
            {
                string dir = $@"{Path.GetDirectoryName(dataFile)}\USER";
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                string title = Path.GetFileNameWithoutExtension(dataFile);
                string file = $@"{dir}\{title}.sum";
                File.WriteAllLines(file, user_sum_file);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        protected static bool FixTprj(string tprj_folder, string project_title)
        {
            string[] lines = new string[]
            {
                $"roff-asc",
                $"#Creator: {LauncherForm.AppName()}#",
                $"",
                $"tag filedata",
                $"  int     byteswaptest 1",
                $"  char    filetype \"Tempest Project\"",
                $"  char    creationDate \"{DateTime.Now}\"",
                $"endtag",
                $"",
                $"tag version",
                $"  int     major 1",
                $"  int     minor 0",
                $"endtag",
                $"",
                $"tag project",
                $"  char    projectname \"{project_title}\"",
                $"  char    simulator \"MORE\"",
                $"endtag",
                $"",
                $"tag case",
                $"  char    simdisplayname \"{project_title}\"",
                $"  char    inputpath \"{project_title}\"",
                $"  char    outputpath \"{project_title}\"",
                $"  bool    loadarray 1",
                $"  bool    loadsummary 1",
                $"  bool    loadbase 0",
                $"endtag",
                $"",
                $"tag eof",
                $"endtag"
            };

            try
            {
                System.IO.Directory.CreateDirectory(tprj_folder);
                string project_file = tprj_folder + "\\project.tempest";
                if (File.Exists(project_file))
                    File.Delete(project_file);
                using (StreamWriter sw = File.CreateText(project_file))
                {
                    foreach (string line in lines)
                        sw.WriteLine(line);
                }
            }
            catch (Exception Ex)
            {
                return false;
            }

            return true;
        }


        virtual protected List<Tuple<string, string>> GetLines(string DataFile)
        {
            return new List<Tuple<string, string>>();
        }


    }
}
