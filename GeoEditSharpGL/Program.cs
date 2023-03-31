using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pexel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Pexel.HM;
using Pexel.HM.FR;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

// https://docs.obfuscar.com/getting-started/configuration.html#table-of-settings
// https://allmnet.tistory.com/entry/NET-%EC%BD%94%EB%93%9C-%EB%82%9C%EB%8F%85%ED%99%94

internal static class NativeMethods
{
    [DllImport("kernel32.dll")]
    internal static extern Boolean AllocConsole();
}


namespace Pexel
{
    static class Program
    {

        static public bool IsUserAdministrator()
        {
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }


        // Wizard
        public const string WizardArg = "WZ";
        public const string HistoryMatchingArg = "HM";
        public const string ResultsViewerArg = "RV";
        public const string CoreyerArg = "CY";
        public const string FDMArg = "FD";


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            /*
            Verification.Renew2(new DateTime(2022, 10, 5));
            if (!Verification.Pass2(out DateTime last_date))
            {
                MessageBox.Show("The licence has expired.\nContact technical support at support@pexel.pro.");
                return;
            }
            */
            
            CultureInfo.DefaultThreadCurrentCulture = Helper.MyCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            
            if (args.Length == 0)
            {
                /*
                if (!IsUserAdministrator())
                    MessageBox.Show("Not Admin!");
                    */

                /*
                double dif = last_date.Subtract(DateTime.Now).TotalDays;
                if (dif < 31)
                {
                    MessageBox.Show($"The licence will expire after {Math.Round(dif, 0)} days.\nContact technical support at support@pexel.pro.");
                }
                */

                // set separator
                //System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(Application.CurrentCulture.Name);
                //culture.NumberFormat.NumberDecimalSeparator = ".";
                //Application.CurrentCulture = culture;

                //Application.Run(new Pexel.MainForm());
                ///Application.Run(new Pexel.MainForm2());
                //Application.Run(new Pexel.TreeForm());
                //Application.Run(new GeoEdit.ManualForm());
                //Application.Run(new Pexel.HMTableForm());
                ///Application.Run(new Pexel.FFR.CycForm());
                //Application.Run(new CudaTestForm());
                //Application.Run(new Pexel.MachineLearning.MLTestForm());
                //Application.Run(new SCAL.NeuroSCALForm());
                ///Application.Run(new HM.WizardForm());
                //Application.Run(new HM.ResultsViewForm());
                //Application.Run(new LauncherForm());
                //Application.Run(new HFileForm());
                //Application.Run(new Pexel.FFR.CombinationsForm());
                //Application.Run(new TestForm());
                ///Application.Run(new HM.Aquifer.AquiferCellsForm());
                //Application.Run(new SCAL.CoreyDBViewForm());
                //Application.Run(new Pexel.HM.IterationResultsTestForm());
                //Application.Run(new SCAL.PicTest());
                //Application.Run(new SCAL.RelPermAnalyzerForm());
                //Application.Run(new Pexel.SCAL.CoreySetForm());
                
                Application.Run(new ResultsViewTreeForm());

                //Application.Run(new UcTreeFormTest());
                //Application.Run(new FRForm());
            }
            else //if (ZipLocker.IsValid())
            {
                if (Helper.ToUpper(args[0]) == ResultsViewerArg)
                {
                    Application.Run(new HM.ResultsViewTreeForm());
                }
                else if (Helper.ToUpper(args[0]) == WizardArg)
                {
                    if (args.Length == 2)
                    {
                        WizardForm wizard = new WizardForm() { DownloaderID = args[1] };
                        Application.Run(wizard);
                    }
                    else
                    {
                        Application.Run(new HM.WizardForm());
                    }
                }
                else if (Helper.ToUpper(args[0]) == HistoryMatchingArg)
                {
                    if (args.Length == 2)
                    {
                        if (HM.HistMatchingInput.Load(args[1], out HM.HistMatchingInput input))
                        {
                            HM.HistMatching hm = new HM.HistMatching(input);
                            hm.Run();
                        }
                    }
                }
            }





        }




    }
}
