using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Pexel.Eclipse;
using System.ComponentModel;
using System.Runtime.Serialization.Json;
using ZedGraph;

//using System.Text.Json;


public enum AquiferRegions { Single, Array, Auto }
public enum AquiferCells { MultPV, Array, Auto }
public enum AquiferValues { MultPV, Init }
public enum ControlType { UNDEF, ORAT, WRAT, LRAT, GRAT, AUTO, CUSTOM }


namespace Pexel.HM
{
    // https://www.c-sharpcorner.com/article/using-property-grid-in-C-Sharp/
    [DefaultPropertyAttribute("Hist Matching Input")]
    public class HistMatchingInput
    {
        public HistMatchingInput() { }

        const string parent_category_attribute      = "0. Parent";
        const string data_category_attribute        = "1. Data";
        const string grid_category_attribute        = "2. Grid";
        const string perm_category_attribute        = "3. Permeability array";
        const string relperm_category_attribute     = "4. Corey curves";
        const string kr_category_attribute          = "5. Kr";
        const string aqui_category_attribute        = "6. Aquifer";
        const string criteria_category_attribute    = "7. Matching creteria";
        const string run_category_attribute         = "8. Run";


        [CategoryAttribute(data_category_attribute), DescriptionAttribute("Title of the Data file"), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string DataFile { set; get; } = string.Empty;
        //public string OutFolder { set; get; } = string.Empty;


        [CategoryAttribute(data_category_attribute), DescriptionAttribute("Simulator EXE file path"), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string SimulatorExeFile
        {
            get
            {
                switch (this.SimulatorType)
                {
                    case SimulatorType.Eclipse: return EclipseExeFile;
                    case SimulatorType.Tempest: return TempestExeFile;
                    case SimulatorType.tNavigator: return tNavExeFile;
                    default: return string.Empty;
                }
            }
        }

        [CategoryAttribute(data_category_attribute), DescriptionAttribute("Mpi EXE file path"), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string MpiExeFile
        {
            get
            {
                switch (this.SimulatorType)
                {
                    case SimulatorType.Eclipse: return EclipseMpiExeFile;
                    case SimulatorType.Tempest: return TempestMpiExeFile;
                    default: return string.Empty;
                }
            }
        }

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string tNavExeFile { set; get; } = string.Empty;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string TempestExeFile { set; get; } = string.Empty;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string EclipseExeFile { set; get; } = string.Empty;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string TempestMpiExeFile { set; get; } = string.Empty;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string EclipseMpiExeFile { set; get; } = string.Empty;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public SimulatorType SimulatorType { set; get; } = SimulatorType.tNavigator;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool UserSummaryFileUsed { set; get; } = true;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool ResultsSubFolderUsed { set; get; } = false;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public int CpuNumber { set; get; } = 1;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool GpuUsed { set; get; } = false;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool MpiUsed { set; get; } = false;

        [CategoryAttribute(data_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public int GpuDevice { set; get; } = 0;


        // run
        [CategoryAttribute(run_category_attribute), DescriptionAttribute(""), BrowsableAttribute(false), ReadOnlyAttribute(true)]
        public bool SimulateBeforeRun { set; get; } = false;

        [CategoryAttribute(run_category_attribute), DescriptionAttribute(""), BrowsableAttribute(false), ReadOnlyAttribute(true)]
        public string ID { set; get; } = string.Empty;

        [CategoryAttribute(run_category_attribute), DescriptionAttribute(""), BrowsableAttribute(false), ReadOnlyAttribute(true)]
        public int StartIter { set; get; } = 0;

        [CategoryAttribute(run_category_attribute), DescriptionAttribute(""), BrowsableAttribute(false), ReadOnlyAttribute(true)]
        public int LastIter { set; get; } = 1;

        [CategoryAttribute(run_category_attribute), DescriptionAttribute(""), BrowsableAttribute(false), ReadOnlyAttribute(true)]
        public string Series { set; get; } = string.Empty;

        [CategoryAttribute(run_category_attribute), DescriptionAttribute(""), BrowsableAttribute(false), ReadOnlyAttribute(true)]
        public bool DataCheckOnly { set; get; } = true;

        [CategoryAttribute(run_category_attribute), DescriptionAttribute(""), BrowsableAttribute(false), ReadOnlyAttribute(true)]
        public DateTime LastWriteTime { set; get; } = UndefDateTime;


        static readonly public DateTime UndefDateTime = new DateTime(2000, 1, 1);


        // grid
        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string GridFile { set; get; } = string.Empty;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public int NX { set; get; } = 1;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public int NY { set; get; } = 1;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public int NZ { set; get; } = 1;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double DX { set; get; } = 50;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double DY { set; get; } = 50;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double DZ { set; get; } = 0.5;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double TopDepth { set; get; } = 2000;

        [CategoryAttribute(grid_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool GridFileUsed { set; get; } = false;



        // matching
        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdOilTotal { set; get; } = 3;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdWatTotal { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdGasTotal { set; get; } = 5;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdLiqTotal { set; get; } = 0;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdOilTotal { set; get; } = 5;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdWatTotal { set; get; } = 5;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdGasTotal { set; get; } = 3;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdLiqTotal { set; get; } = 5;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double OilInjeTotal { set; get; } = 0;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double WatInjeTotal { set; get; } = 0;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasInjeTotal { set; get; } = 0;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdOilRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdWatRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdGasRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double LiqProdLiqRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdOilRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdWatRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdGasRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasProdLiqRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double OilInjeRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double WatInjeRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double GasInjeRate { set; get; } = 10;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double WbhpDeviation { set; get; } = 20;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double ResPressDeviation { set; get; } = 20;


        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool LiqProdOilRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool LiqProdWatRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool LiqProdGasRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool LiqProdRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool GasProdOilRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool GasProdWatRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool GasProdRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool GasProdLiqRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool OilInjeRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool WatInjeRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool GasInjeRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool WbhpRequested { set; get; }

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool ResPressRequested { set; get; }


        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double WglrDeviation { set; get; } = 10000;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public ControlType Control { set; get; } = ControlType.LRAT;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string ControlFile { set; get; } = string.Empty;

        //DataTable WBHPHTable { set; get; } = new DataTable();
        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string ResPressTableFile { set; get; } = string.Empty;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool WbhphFileUsed { set; get; } = false;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string WbhphFile { set; get; } = string.Empty;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public CombineType WbhphCombineType { set; get; } = CombineType.ADD_IF_NOT_EXIST;


        // targ wells
        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool TargetWellsFileUsed { set; get; } = false;

        [CategoryAttribute(criteria_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string TargetWellsFile { set; get; } = string.Empty;


        // aquifer
        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool AquiferMatching { set; get; } = false;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public AquiferRegions AquiferRegionsSettings { set; get; }

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public AquiferCells AquiferCellsSettings { set; get; }

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public AquiferValues AquiferValuesSettings { set; get; }

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string MultpvTitle { set; get; } = "MULTPV";

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string MultpvFile { set; get; } = string.Empty;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string SwatinitTitle { set; get; } = "SWATINIT";

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string SwatinitFile { set; get; } = string.Empty;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string ActnumTitle { set; get; } = "ACTNUM";

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string ActnumFile { set; get; } = string.Empty;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string AquiferRegionsTitle { set; get; } = "AQREG";

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string AquiferRegionsFile { set; get; } = string.Empty;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string AquiferCellsTitle { set; get; } = "AQCELL";

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string AquiferCellsFile { set; get; } = string.Empty;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double MultpvStartValue { set; get; } = 1;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool ResPressFileUsed { set; get; } = true;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public int AquiferCellsMinDistance { set; get; } = 1;


        // multpv min max
        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool MultpvMinFileUsed { set; get; } = false;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool MultpvMaxFileUsed { set; get; } = false;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string MultpvMinTitle { set; get; } = "MULTPVMIN";

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string MultpvMaxTitle { set; get; } = "MULTPVMAX";

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double MultpvMinValue { set; get; } = HistMatching.MULTPVMIN;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double MultpvMaxValue { set; get; } = HistMatching.MULTPVMAX;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string MultpvMinFile { set; get; } = string.Empty;

        [CategoryAttribute(aqui_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string MultpvMaxFile { set; get; } = string.Empty;



        // perm
        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool PermMatching { set; get; } = false;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermFile { set; get; } = string.Empty;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermTitle { set; get; } = "PERMX";


        // perm min max
        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool PermMinFileUsed { set; get; } = false;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool PermMaxFileUsed { set; get; } = false;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermMinTitle { set; get; } = "PERMXMIN";

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermMaxTitle { set; get; } = "PERMXMAX";

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double PermMinValue { set; get; } = HistMatching.PERMMIN;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double PermMaxValue { set; get; } = 9999;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermMinFile { set; get; } = string.Empty;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermMaxFile { set; get; } = string.Empty;


        // perm regions
        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool PermRegionsFileUsed { set; get; } = false;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermRegionsFile { set; get; } = string.Empty;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string PermRegionsTitle { set; get; } = "ZONES";

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool PermLayerSplitting { set; get; } = true;



        // fdm
        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool FDModel { set; get; } = false;

        [CategoryAttribute(perm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string FDModelFile { set; get; } = string.Empty;



        // relperm
        [CategoryAttribute(relperm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool RelPermMatching { set; get; } = false;

        [CategoryAttribute(relperm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool SatnumFileUsed { set; get; } = false;

        [CategoryAttribute(relperm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string SatnumTitle { set; get; } = "SATNUM";

        [CategoryAttribute(relperm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string SatnumFile { set; get; } = string.Empty;

        [CategoryAttribute(relperm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string RelPermTableFile { set; get; } = string.Empty;

        [CategoryAttribute(relperm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string CoreyInputFile { set; get; } = string.Empty;


        // KRORW
        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrorwMatching { set; get; } = false;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrorwFile { set; get; } = string.Empty;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrorwTitle { set; get; } = "KRORW";

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrorwMinFileUsed { set; get; } = false;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrorwMaxFileUsed { set; get; } = false;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrorwMinTitle { set; get; } = "KRORWMIN";

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrorwMaxTitle { set; get; } = "KRORWMAX";

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double KrorwMinValue { set; get; } = HistMatching.KRORWMIN;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double KrorwMaxValue { set; get; } = HistMatching.KRORWMAX;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrorwMinFile { set; get; } = string.Empty;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrorwMaxFile { set; get; } = string.Empty;



        // KRWR
        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrwrMatching { set; get; } = false;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrwrFile { set; get; } = string.Empty;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrwrTitle { set; get; } = "KRWR";

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrwrMinFileUsed { set; get; } = false;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrwrMaxFileUsed { set; get; } = false;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrwrMinTitle { set; get; } = "KRWRMIN";

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrwrMaxTitle { set; get; } = "KRWRMAX";

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double KrwrMinValue { set; get; } = HistMatching.KRWRMIN;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public double KrwrMaxValue { set; get; } = HistMatching.KRWRMAX;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrwrMinFile { set; get; } = string.Empty;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrwrMaxFile { set; get; } = string.Empty;




        // Kr regions
        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrRegionsFileUsed { set; get; } = false;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrRegionsFile { set; get; } = string.Empty;

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string KrRegionsTitle { set; get; } = "ZONES";

        [CategoryAttribute(kr_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public bool KrLayerSplitting { set; get; } = true;






        // parent
        [CategoryAttribute(parent_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string ParentCase { set; get; } = string.Empty;

        [CategoryAttribute(parent_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string ParentID { set; get; } = string.Empty;

        [CategoryAttribute(parent_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(true)]
        public string ParentIter { set; get; } = string.Empty;





        public HMDeviations Deviations()
        {
            HMDeviations result = new HMDeviations()
            {
                UseLiqProdOilTotal = this.LiqProdOilRequested,
                UseLiqProdWatTotal = this.LiqProdWatRequested,
                UseLiqProdGasTotal = this.LiqProdGasRequested,
                UseLiqProdLiqTotal = this.LiqProdRequested,
                UseGasProdOilTotal = this.GasProdOilRequested,
                UseGasProdWatTotal = this.GasProdWatRequested,
                UseGasProdGasTotal = this.GasProdRequested,
                UseGasProdLiqTotal = this.GasProdLiqRequested,
                UseOilInjeTotal = this.OilInjeRequested,
                UseWatInjeTotal = this.WatInjeRequested,
                UseGasInjeTotal = this.GasInjeRequested,
                UseLiqProdOilRate = this.LiqProdOilRequested,
                UseLiqProdWatRate = this.LiqProdWatRequested,
                UseLiqProdGasRate = this.LiqProdGasRequested,
                UseLiqProdLiqRate = this.LiqProdRequested,
                UseGasProdOilRate = this.GasProdOilRequested,
                UseGasProdWatRate = this.GasProdWatRequested,
                UseGasProdGasRate = this.GasProdRequested,
                UseGasProdLiqRate = this.GasProdLiqRequested,
                UseOilInjeRate = this.OilInjeRequested,
                UseWatInjeRate = this.WatInjeRequested,
                UseGasInjeRate = this.GasInjeRequested,
                UseMaxBHPDev = this.WbhpRequested,
                UseMaxResPressDev = this.ResPressRequested,
                LiqProdOilTotal = this.LiqProdOilTotal,
                LiqProdWatTotal = this.LiqProdWatTotal,
                LiqProdGasTotal = this.LiqProdGasTotal,
                LiqProdLiqTotal = this.LiqProdLiqTotal,
                GasProdOilTotal = this.GasProdOilTotal,
                GasProdWatTotal = this.GasProdWatTotal,
                GasProdGasTotal = this.GasProdGasTotal,
                GasProdLiqTotal = this.GasProdLiqTotal,
                OilInjeTotal = this.OilInjeTotal,
                WatInjeTotal = this.WatInjeTotal,
                GasInjeTotal = this.GasInjeTotal,
                LiqProdOilRate = this.LiqProdOilRate,
                LiqProdWatRate = this.LiqProdWatRate,
                LiqProdGasRate = this.LiqProdGasRate,
                LiqProdLiqRate = this.LiqProdLiqRate,
                GasProdOilRate = this.GasProdOilRate,
                GasProdWatRate = this.GasProdWatRate,
                GasProdGasRate = this.GasProdGasRate,
                GasProdLiqRate = this.GasProdLiqRate,
                OilInjeRate = this.OilInjeRate,
                WatInjeRate = this.WatInjeRate,
                GasInjeRate = this.GasInjeRate,
                MaxBHPDev = this.WbhpDeviation,
                MaxResPressDev = this.ResPressDeviation
            };
            return result;
        }







        static public bool Load(string filename, out HistMatchingInput result)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
                HistMatchingInput temp = new HistMatchingInput();
                using (StreamReader r = new StreamReader(filename))
                {
                    string json = r.ReadToEnd();
                    temp = JsonConvert.DeserializeAnonymousType(json, temp, settings);
                }
                temp.LastWriteTime = File.GetLastWriteTime(filename);
                result = temp;

                //string jsonString = File.ReadAllText(filename);
                //HistMatchingInput weatherForecast = JsonSerializer.Deserialize<HistMatchingInput>(jsonString);
            }
            catch (Exception ex)
            {
                result = new HistMatchingInput();
                return false;
            }
            return true;
        }




        public bool Save(string filename)
        {
            try
            {
                LastWriteTime = DateTime.Now;
                using (StreamWriter file = File.CreateText(filename))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.Auto
                    };
                    serializer.Serialize(file, this);
                }
                //string result = JsonSerializer.Serialize(this);
                //File.WriteAllText(filename, result);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



        public string JsonText()
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(HistMatchingInput));
                MemoryStream msObj = new MemoryStream();
                js.WriteObject(msObj, this);
                msObj.Position = 0;
                StreamReader sr = new StreamReader(msObj);
                // "{\"Description\":\"Share Knowledge\",\"Name\":\"C-sharpcorner\"}"
                string json = sr.ReadToEnd();
                sr.Close();
                msObj.Close();
                return json;

                /*
                MemoryStream msObj = new MemoryStream();
                JsonSerializer serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
                serializer.Serialize(msObj, this);
                */
                //JsonSerializerOptions options = new JsonSerializerOptions();
                //options.
                //string result = JsonSerializer.Serialize(this);
                //return result;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }



        public Grid GetGrid(out List<string> report)
        {
            report = new List<string>();
            if (!GridFileUsed)
                return new Grid("GRID", NX, NY, NZ, 50, 50, 50, 2000);
            Grid result = new Grid();
            result.ReadGRDECL(GridFile, out report);
            return result;
        }



        public Prop GetPermxMin(int nx, int ny, int nz)
        {
            if (!PermMinFileUsed)
                return new Prop(nx, ny, nz, PermMinValue);
            return new Prop(nx, ny, nz, PermMinTitle, PermMinFile, HistMatching.FILETYPE);
        }



        public Prop GetPermxMax(int nx, int ny, int nz)
        {
            if (!PermMaxFileUsed)
                return new Prop(nx, ny, nz, PermMaxValue);
            return new Prop(nx, ny, nz, PermMaxTitle, PermMaxFile, HistMatching.FILETYPE);
        }




        public void SetNewPath(string new_file_file)
        {
            GridFile = Helper.GetNewPath(DataFile, new_file_file, GridFile);
            PermFile = Helper.GetNewPath(DataFile, new_file_file, PermFile);
            PermMinFile = Helper.GetNewPath(DataFile, new_file_file, PermMinFile);
            PermMaxFile = Helper.GetNewPath(DataFile, new_file_file, PermMaxFile);
            PermRegionsFile = Helper.GetNewPath(DataFile, new_file_file, PermRegionsFile);
            TargetWellsFile = Helper.GetNewPath(DataFile, new_file_file, TargetWellsFile);
            MultpvFile = Helper.GetNewPath(DataFile, new_file_file, MultpvFile);
            SwatinitFile = Helper.GetNewPath(DataFile, new_file_file, SwatinitFile);
            ActnumFile = Helper.GetNewPath(DataFile, new_file_file, ActnumFile);
            AquiferRegionsFile = Helper.GetNewPath(DataFile, new_file_file, AquiferRegionsFile);
            AquiferCellsFile = Helper.GetNewPath(DataFile, new_file_file, AquiferCellsFile);
            ResPressTableFile = Helper.GetNewPath(DataFile, new_file_file, ResPressTableFile);
            ControlFile = Helper.GetNewPath(DataFile, new_file_file, ControlFile);
            WbhphFile = Helper.GetNewPath(DataFile, new_file_file, WbhphFile);
            SatnumFile = Helper.GetNewPath(DataFile, new_file_file, SatnumFile);
            RelPermTableFile = Helper.GetNewPath(DataFile, new_file_file, RelPermTableFile);
            CoreyInputFile = Helper.GetNewPath(DataFile, new_file_file, CoreyInputFile);
            KrorwFile = Helper.GetNewPath(DataFile, new_file_file, KrorwFile);
            KrorwMinFile = Helper.GetNewPath(DataFile, new_file_file, KrorwMinFile);
            KrorwMaxFile = Helper.GetNewPath(DataFile, new_file_file, KrorwMaxFile);
            KrwrFile = Helper.GetNewPath(DataFile, new_file_file, KrwrFile);
            KrwrMinFile = Helper.GetNewPath(DataFile, new_file_file, KrwrMinFile);
            KrwrMaxFile = Helper.GetNewPath(DataFile, new_file_file, KrwrMaxFile);
            KrRegionsFile = Helper.GetNewPath(DataFile, new_file_file, KrRegionsFile);
            //
            DataFile = Helper.GetNewPath(DataFile, new_file_file, DataFile);
        }



    }
}
