using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Pexel.HM;

namespace Pexel
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();
            this.Text = $"{AppName()} Launcher";
        }


        public static string AppName()
        {
            /*
            Version ver = System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed ?
                          System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion :
                          System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            */
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return $"{System.Reflection.Assembly.GetEntryAssembly().GetName().Name}v{ver.Major}.{ver.Minor}.{ver.Revision}";
            //return $"{System.Reflection.Assembly.GetEntryAssembly().GetName().Name} {ver.Major}.{ver.Minor}r{ver.Revision}";
        }


        private void button_hm_Click(object sender, EventArgs e)
        {
#if DEBUG
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HM.WizardForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
#else
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HM.WizardForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
            //Start(LauncherForm.HistoryMatchingWizardArg);
#endif
        }



        private void button_queue_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
#if DEBUG
                Application.Run(new HM.QueueForm());
#else
                try
                {
                    Application.Run(new HM.QueueForm());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
#endif
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();

        }



        private void button_results_Click(object sender, EventArgs e)
        {
#if DEBUG
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HM.ResultsViewTreeForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
#else
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HM.ResultsViewTreeForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
            //Start(LauncherForm.ResultsViewerArg);
#endif
        }




        public const string HistoryMatchingWizardArg = "-HM";
        public const string ResultsViewerArg = "-RV";
        public static void Start(params string[] args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            string line = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string line = $@"{AppContext.BaseDirectory}\PEXEL.EXE";
            foreach (string arg in args) line += " " + arg;

            cmd.StandardInput.WriteLine(line);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            //cmd.WaitForExit();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        private void getFullVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HM.UnlockForm unlockForm = new HM.UnlockForm();
            Cursor.Current = Cursors.Default;
            unlockForm.ShowDialog();
        }


        private void button_hm_table_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HMTableForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void button_corey_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new Pexel.SCAL.CoreySetForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void button_hfile_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HFileForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void button_tree_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new ResultsViewTreeForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }
    }
}
