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
using System.IO;
using System.Management;
using Pexel.SCAL;
using Google.Protobuf.WellKnownTypes;
using DynamicExpresso;




// Многопоточность в C#: как работать с потоками в C# 
// и как в потоке обращаться к элементам формы
// https://victorz.ru/20160526176




namespace Pexel.HM
{
    public enum WizardStartPage { Intro, Data, Run }

    public partial class WizardForm : Form
    {
        public WizardForm()
        {
            InitializeComponent();
            Init();
        }

        public WizardForm(HistMatchingInput input, WizardStartPage startPage)
        {
            InitializeComponent();
            Init();
            DataFile = input.DataFile;
            InputData = input;
            switch (startPage)
            {
                case WizardStartPage.Intro:
                    break;
                case WizardStartPage.Data:
                    this.Load += this.WizardForm_Load_Data;
                    break;
                case WizardStartPage.Run:
                    this.Load += this.WizardForm_Load_Run;
                    break;
            }
        }


        string OriginalText = string.Empty;



        public string DownloaderID { set; get; } = string.Empty;
        



        void Init()
        {
            //HistMatching = new HistMatching();
            //HistMatching.MsgHandler += new HistMatching.StateHandler(Msg);
            IDs = new List<string>();
            SimulatorType = SimulatorType.Eclipse;
            /*
            richTextBox14.Rtf =   @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang4105{\fonttbl{\f0\fnil\fcharset0 Calibri;}}
                                    {\*\generator Riched20 10.0.17134}\viewkind4\uc1 
                                    {\field{\*\fldinst { HYPERLINK ""http://www.google.com"" }}{\fldrslt {Click here}}}
                                    \pard\sa200\sl276\slmult1\f0\fs22\lang9  for more information.\par
                                    }";
                                    */
            /*
            this.groupBox_permx_min.Text = $"'{HistMatchingInput.PermxMinTitleDef}'";
            this.groupBox_permx_max.Text = $"'{HistMatchingInput.PermxMaxTitleDef}'";
            DataCheckOnly = true;
            this.radioButton_not_use_wlist.Checked = true;
            this.radioButton_permMinValue.Checked = true;
            this.radioButton_permMaxValue.Checked = true;
            this.radioButton_use_grid.Checked = true;
            this.radioButton_not_use_wlist.Checked = true;
            this.radioButton_inje_ending_not_used.Checked = true;
            richTextBox_intro.Rtf = HMRes.Intro;
            numericUpDown_cpu_ValueChanged(null, null);
            checkBox_use_gpu_CheckedChanged(null, null);
            RegionsFile = string.Empty;
            */
            //richTextBox_intro.Rtf = HMRes.Intro;

            // change check state of check box before init input data just for normal init

            SimChanged();
            checkBox_use_wbhph_file_CheckedChanged(null, null);
            checkBox_aquifer_matching_CheckedChanged(null, null);
            checkBox_perm_matching_CheckedChanged(null, null);
            checkBox_relperm_matching_CheckedChanged(null, null);
            checkBox_krorw_matching_CheckedChanged(null, null);
            checkBox_krwr_matching_CheckedChanged(null, null);
            checkBox_fdm_CheckedChanged(null, null);

            checkBox_simulate_before_hm.Checked = true;
            checkBox_simulate_before_hm.Checked = false;

            this.radioButton_use_res_press_file.Checked = true;

            GPUList = LoadGPU();

            InputData = new HistMatchingInput();
            DataCheckOnly = true;

            OriginalText = $"{LauncherForm.AppName()} History Matching Wizard";
            Text = OriginalText;

            this.numericUpDown_perm_min_value.Minimum = (decimal)HistMatching.PERMMIN;

            this.label_max_cpu.Text = $"/ {ProcessorCount}";
            this.numericUpDown_cpu.Maximum = ProcessorCount;

            this.propertyGrid_input_data.SelectedObject = InputData;
        }




        string _prev_json_text = string.Empty;
        bool SaveInputData()
        {
            string text = InputData.JsonText();
            if (string.Equals(_prev_json_text, text))
                return false;
            _prev_json_text = text;
            return InputData.Save(SaveFile);
        }




        string[] GPUList
        {
            set
            {
                this.comboBox_gpu_list.Items.Clear();
                foreach (string item in value)
                    this.comboBox_gpu_list.Items.Add(item);
            }
        }

        public delegate void RunStartedHandler(params string[] pxlhm_file);
        public RunStartedHandler RunStartedEvent;



        HistMatchingInput InputData
        {
            set
            {
                string old_ref = value.DataFile;
                string new_ref = DataFile;

                //OutFolder = value.OutFolder;
                SimulatorType = value.SimulatorType;

                tNavExeFile = !File.Exists(value.tNavExeFile) ? string.Empty : value.tNavExeFile;
                TempestExeFile = !File.Exists(value.TempestExeFile) ? string.Empty : value.TempestExeFile;
                EclipseExeFile = !File.Exists(value.EclipseExeFile) ? string.Empty : value.EclipseExeFile;
                TempestMpiExeFile = !File.Exists(value.TempestMpiExeFile) ? string.Empty : value.TempestMpiExeFile;
                EclipseMpiExeFile = !File.Exists(value.EclipseMpiExeFile) ? string.Empty : value.EclipseMpiExeFile;

                UseUserSumFile = value.UserSummaryFileUsed;
                ResultsSubFolderUsed = value.ResultsSubFolderUsed;
                LastIter = value.LastIter;
                CPU = value.CpuNumber;
                UseGPU = value.GpuUsed;
                //UseMPI = value.UseMPI;
                GPUDevice = value.GpuDevice;
                SimulateBeforeHM = value.SimulateBeforeRun;

                comboBox_id_DropDown(null, null);
                ID = value.ID;
                Iter = value.StartIter;
                Series = value.Series;
                LayerSplitting = value.PermLayerSplitting;

                LiqProdOilTotal = value.LiqProdOilTotal;
                LiqProdWatTotal = value.LiqProdWatTotal;
                LiqProdGasTotal = value.LiqProdGasTotal;
                LiqProdLiqTotal = value.LiqProdLiqTotal;
                GasProdOilTotal = value.GasProdOilTotal;
                GasProdWatTotal = value.GasProdWatTotal;
                GasProdGasTotal = value.GasProdGasTotal;
                GasProdLiqTotal = value.GasProdLiqTotal;
                OilInjeTotal = value.OilInjeTotal;
                WatInjeTotal = value.WatInjeTotal;
                GasInjeTotal = value.GasInjeTotal;
                LiqProdOilRate = value.LiqProdOilRate;
                LiqProdWatRate = value.LiqProdWatRate;
                LiqProdGasRate = value.LiqProdGasRate;
                LiqProdLiqRate = value.LiqProdLiqRate;
                GasProdOilRate = value.GasProdOilRate;
                GasProdWatRate = value.GasProdWatRate;
                GasProdGasRate = value.GasProdGasRate;
                GasProdLiqRate = value.GasProdLiqRate;
                OilInjeRate = value.OilInjeRate;
                WatInjeRate = value.WatInjeRate;
                GasInjeRate = value.GasInjeRate;
                MaxBHPDev = value.WbhpDeviation;
                MaxResPressDev = value.ResPressDeviation;
                LiqProdOilRequested = value.LiqProdOilRequested;
                LiqProdWatRequested = value.LiqProdWatRequested;
                LiqProdGasRequested = value.LiqProdGasRequested;
                LiqProdRequested = value.LiqProdRequested;
                GasProdOilRequested = value.GasProdOilRequested;
                GasProdWatRequested = value.GasProdWatRequested;
                GasProdRequested = value.GasProdRequested;
                GasProdLiqRequested = value.GasProdLiqRequested;
                OilInjeRequested = value.OilInjeRequested;
                WatInjeRequested = value.WatInjeRequested;
                GasInjeRequested = value.GasInjeRequested;
                BHPRequested = value.WbhpRequested;
                RPRequested = value.ResPressRequested;

                MaxGLR = value.WglrDeviation;

                GridFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.GridFile);
                NX = value.NX;
                NY = value.NY;
                NZ = value.NZ;
                DX = value.DX;
                DY = value.DY;
                DZ = value.DZ;
                TopDepth = value.TopDepth;
                UseGridFile = value.GridFileUsed;

                PermxFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.PermFile);
                PermxTitle = value.PermTitle;

                PermxMinTitle = value.PermMinTitle;
                PermxMaxTitle = value.PermMaxTitle;
                PermxMinValue = value.PermMinValue;
                PermxMaxValue = value.PermMaxValue;
                PermxMinFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.PermMinFile);
                PermxMaxFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.PermMaxFile);
                UsePermxMinFile = value.PermMinFileUsed;
                UsePermxMaxFile = value.PermMaxFileUsed;
                FDModel = value.FDModel;
                FDModelFile = value.FDModelFile;

                RegionsFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.PermRegionsFile);
                RegionsTitle = value.PermRegionsTitle;
                UseRegionsFile = value.PermRegionsFileUsed;

                TargetWellsFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.TargetWellsFile);
                UseTargetWellsFile = value.TargetWellsFileUsed;

                AquiferMatching = value.AquiferMatching;
                AquiferRegSettings = value.AquiferRegionsSettings;
                AquiferCellSettings = value.AquiferCellsSettings;
                AquiferValuesSettings = value.AquiferValuesSettings;
                MultpvTitle = value.MultpvTitle;
                MultpvFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.MultpvFile);
                MinAquiferCellsDistance = value.AquiferCellsMinDistance;
                SwatinitTitle = value.SwatinitTitle;
                SwatinitFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.SwatinitFile);
                ActnumTitle = value.ActnumTitle;
                ActnumFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.ActnumFile);
                AquiferRegTitle = value.AquiferRegionsTitle;
                AquiferRegFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.AquiferRegionsFile);
                AquiferCellTitle = value.AquiferCellsTitle;
                AquiferCellFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.AquiferCellsFile);
                MultpvStart = value.MultpvStartValue;
                MultpvMinFileUsed = value.MultpvMinFileUsed;
                MultpvMaxFileUsed = value.MultpvMaxFileUsed;
                MultpvMinTitle = value.MultpvMinTitle;
                MultpvMaxTitle = value.MultpvMaxTitle;
                MultpvMinValue = value.MultpvMinValue;
                MultpvMaxValue = value.MultpvMaxValue;
                MultpvMinFile = value.MultpvMinFile;
                MultpvMaxFile = value.MultpvMaxFile;
                ResPressTableFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.ResPressTableFile);
                UseResPressFile = value.ResPressFileUsed;
                Control = value.Control;
                ControlFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.ControlFile);
                DataCheckOnly = value.DataCheckOnly;

                WBHPHCombineType = value.WbhphCombineType;
                WBHPHFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.WbhphFile);
                UseWBHPHFile = value.WbhphFileUsed;

                PermMatching = value.PermMatching;
                RelPermMatching = value.RelPermMatching;

                UseSatnumFile = value.SatnumFileUsed;
                SatnumTitle = value.SatnumTitle;
                SatnumFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.SatnumFile);
                RelPermTableFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.RelPermTableFile);
                CoreyInputFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.CoreyInputFile);

                KrorwMatching = value.KrorwMatching;
                KrorwFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.KrorwFile);
                KrorwTitle = value.KrorwTitle;
                UseKrorwMinFile = value.KrorwMinFileUsed;
                UseKrorwMaxFile = value.KrorwMaxFileUsed;
                KrorwMinTitle = value.KrorwMinTitle;
                KrorwMaxTitle = value.KrorwMaxTitle;
                KrorwMinValue = value.KrorwMinValue;
                KrorwMaxValue = value.KrorwMaxValue;
                KrorwMinFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.KrorwMinFile);
                KrorwMaxFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.KrorwMaxFile);
                KrwrMatching = value.KrwrMatching;
                KrwrFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.KrwrFile);
                KrwrTitle = value.KrwrTitle;
                UseKrwrMinFile = value.KrwrMinFileUsed;
                UseKrwrMaxFile = value.KrwrMaxFileUsed;
                KrwrMinTitle = value.KrwrMinTitle;
                KrwrMaxTitle = value.KrwrMaxTitle;
                KrwrMinValue = value.KrwrMinValue;
                KrwrMaxValue = value.KrwrMaxValue;
                KrwrMinFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.KrwrMinFile);
                KrwrMaxFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.KrwrMaxFile);
                UseKrRegionsFile = value.KrRegionsFileUsed;
                KrRegionsFile = Helper.GetNewPathIfExists(old_ref, new_ref, value.KrRegionsFile);
                KrRegionsTitle = value.KrRegionsTitle;
                KrLayerSplitting = value.KrLayerSplitting;

                ParentCase = value.ParentCase;
                ParentID = value.ParentID;
                ParentIter = value.ParentIter;

                DataFile = value.DataFile;
            }
            get
            {
                HistMatchingInput result = new HistMatchingInput
                {
                    DataFile = this.DataFile,
                    //OutFolder = this.OutFolder,
                    tNavExeFile = this.tNavExeFile,
                    TempestExeFile = this.TempestExeFile,
                    EclipseExeFile = this.EclipseExeFile,
                    TempestMpiExeFile = this.TempestMpiExeFile,
                    EclipseMpiExeFile = this.EclipseMpiExeFile,
                    SimulatorType = this.SimulatorType,
                    UserSummaryFileUsed = this.UseUserSumFile,
                    ResultsSubFolderUsed = this.ResultsSubFolderUsed,
                    LastIter = this.LastIter,
                    CpuNumber = this.CPU,
                    GpuUsed = this.UseGPU,
                    MpiUsed = this.UseMPI,
                    GpuDevice = this.GPUDevice,
                    SimulateBeforeRun = this.SimulateBeforeHM,
                    ID = this.ID,
                    StartIter = this.Iter,
                    Series = this.Series,
                    PermLayerSplitting = this.LayerSplitting,
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
                    WbhpDeviation = this.MaxBHPDev,
                    ResPressDeviation = this.MaxResPressDev,
                    LiqProdOilRequested = this.LiqProdOilRequested,
                    LiqProdWatRequested = this.LiqProdWatRequested,
                    LiqProdGasRequested = this.LiqProdGasRequested,
                    LiqProdRequested = this.LiqProdRequested,
                    GasProdOilRequested = this.GasProdOilRequested,
                    GasProdWatRequested = this.GasProdWatRequested,
                    GasProdRequested = this.GasProdRequested,
                    GasProdLiqRequested = this.GasProdLiqRequested,
                    OilInjeRequested = this.OilInjeRequested,
                    WatInjeRequested = this.WatInjeRequested,
                    GasInjeRequested = this.GasInjeRequested,
                    WbhpRequested = this.BHPRequested,
                    ResPressRequested = this.RPRequested,

                    WglrDeviation = this.MaxGLR,
                    GridFile = this.GridFile,
                    NX = this.NX,
                    NY = this.NY,
                    NZ = this.NZ,
                    DX = this.DX,
                    DY = this.DY,
                    DZ = this.DZ,
                    TopDepth = this.TopDepth,
                    GridFileUsed = this.UseGridFile,
                    PermFile = this.PermxFile,
                    PermTitle = this.PermxTitle,
                    PermMinFileUsed = this.UsePermxMinFile,
                    PermMaxFileUsed = this.UsePermxMaxFile,
                    PermMinTitle = this.PermxMinTitle,
                    PermMaxTitle = this.PermxMaxTitle,
                    PermMinValue = this.PermxMinValue,
                    PermMaxValue = this.PermxMaxValue,
                    PermMinFile = this.PermxMinFile,
                    PermMaxFile = this.PermxMaxFile,
                    PermRegionsFileUsed = this.UseRegionsFile,
                    PermRegionsFile = this.RegionsFile,
                    PermRegionsTitle = this.RegionsTitle,
                    FDModel = this.FDModel,
                    FDModelFile = this.FDModelFile,
                    TargetWellsFileUsed = this.UseTargetWellsFile,
                    TargetWellsFile = this.TargetWellsFile,

                    AquiferMatching = this.AquiferMatching,
                    AquiferRegionsSettings = this.AquiferRegSettings,
                    AquiferCellsSettings = this.AquiferCellSettings,
                    AquiferValuesSettings = this.AquiferValuesSettings,
                    AquiferCellsMinDistance = this.MinAquiferCellsDistance,
                    MultpvTitle = this.MultpvTitle,
                    MultpvFile = this.MultpvFile,
                    SwatinitTitle = this.SwatinitTitle,
                    SwatinitFile = this.SwatinitFile,
                    ActnumTitle = this.ActnumTitle,
                    ActnumFile = this.ActnumFile,
                    AquiferRegionsTitle = this.AquiferRegTitle,
                    AquiferRegionsFile = this.AquiferRegFile,
                    AquiferCellsTitle = this.AquiferCellTitle,
                    AquiferCellsFile = this.AquiferCellFile,
                    MultpvStartValue = this.MultpvStart,
                    MultpvMinFileUsed = this.MultpvMinFileUsed,
                    MultpvMaxFileUsed = this.MultpvMaxFileUsed,
                    MultpvMinTitle = this.MultpvMinTitle,
                    MultpvMaxTitle = this.MultpvMaxTitle,
                    MultpvMinValue = this.MultpvMinValue,
                    MultpvMaxValue = this.MultpvMaxValue,
                    MultpvMinFile = this.MultpvMinFile,
                    MultpvMaxFile = this.MultpvMaxFile,
                    ResPressTableFile = this.ResPressTableFile,
                    ResPressFileUsed = this.UseResPressFile,
                    Control = this.Control,
                    ControlFile = this.ControlFile,
                    DataCheckOnly = this.DataCheckOnly,

                    WbhphCombineType = this.WBHPHCombineType,
                    WbhphFile = this.WBHPHFile,
                    WbhphFileUsed = this.UseWBHPHFile,

                    PermMatching = this.PermMatching,
                    RelPermMatching = this.RelPermMatching,

                    SatnumFileUsed = this.UseSatnumFile,
                    SatnumTitle = this.SatnumTitle,
                    SatnumFile = this.SatnumFile,
                    RelPermTableFile = this.RelPermTableFile,
                    CoreyInputFile = this.CoreyInputFile,

                    KrorwMatching = this.KrorwMatching,
                    KrorwFile = this.KrorwFile,
                    KrorwTitle = this.KrorwTitle,
                    KrorwMinFileUsed = this.UseKrorwMinFile,
                    KrorwMaxFileUsed = this.UseKrorwMaxFile,
                    KrorwMinTitle = this.KrorwMinTitle,
                    KrorwMaxTitle = this.KrorwMaxTitle,
                    KrorwMinValue = this.KrorwMinValue,
                    KrorwMaxValue = this.KrorwMaxValue,
                    KrorwMinFile = this.KrorwMinFile,
                    KrorwMaxFile = this.KrorwMaxFile,
                    KrwrMatching = this.KrwrMatching,
                    KrwrFile = this.KrwrFile,
                    KrwrTitle = this.KrwrTitle,
                    KrwrMinFileUsed = this.UseKrwrMinFile,
                    KrwrMaxFileUsed = this.UseKrwrMaxFile,
                    KrwrMinTitle = this.KrwrMinTitle,
                    KrwrMaxTitle = this.KrwrMaxTitle,
                    KrwrMinValue = this.KrwrMinValue,
                    KrwrMaxValue = this.KrwrMaxValue,
                    KrwrMinFile = this.KrwrMinFile,
                    KrwrMaxFile = this.KrwrMaxFile,

                    KrRegionsFileUsed = this.UseKrRegionsFile,
                    KrRegionsFile = this.KrRegionsFile,
                    KrRegionsTitle = this.KrRegionsTitle,
                    KrLayerSplitting = this.KrLayerSplitting,

                    ParentCase = this.ParentCase,
                    ParentID = this.ParentID,
                    ParentIter = this.ParentIter
                };
                return result;
            }
        }

        string Series { set; get; } = string.Empty;

        public string ParentCase { set; get; } = string.Empty;
        public string ParentID { set; get; } = string.Empty;
        public string ParentIter { set; get; } = string.Empty;





        public bool FDModel
        {
            set
            {
                this.checkBox_fdm.Checked = value;
            }
            get
            {
                return this.checkBox_fdm.Checked;
            }
        }


        public string FDModelFile 
        { 
            set
            {
                this.textBox_fdm.Text = value;
            }
            get
            {
                return this.textBox_fdm.Text;
            }
        }






        public bool ResultsSubFolderUsed 
        { 
            set
            {
                this.checkBox_results_sub_folder.Checked = value;
            }
            get
            {
                return this.checkBox_results_sub_folder.Checked;
            }
        }


        public bool MultpvMinFileUsed 
        { 
            set
            {
                this.radioButton_multpv_min_array.Checked = value;
                this.radioButton_multpv_min_value.Checked = !value;                
            }
            get
            {
                return this.radioButton_multpv_min_array.Checked;
            }
        }

        public bool MultpvMaxFileUsed
        {
            set
            {
                this.radioButton_multpv_max_array.Checked = value;
                this.radioButton_multpv_max_value.Checked = !value;
            }
            get
            {
                return this.radioButton_multpv_max_array.Checked;
            }
        }

        public string MultpvMinTitle 
        { 
            set
            {
                this.textBox_multpv_min_title.Text = value;
            }
            get
            {
                return this.textBox_multpv_min_title.Text;
            }
        }

        public string MultpvMaxTitle
        {
            set
            {
                this.textBox_multpv_max_title.Text = value;
            }
            get
            {
                return this.textBox_multpv_max_title.Text;
            }
        }

        public double MultpvMinValue 
        { 
            set
            {
                this.numericUpDown_multpv_min_value.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_multpv_min_value.Value;
            }
        }

        public double MultpvMaxValue
        {
            set
            {
                this.numericUpDown_multpv_max_value.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_multpv_max_value.Value;
            }
        }

        public string MultpvMinFile
        { 
            set
            {
                this.textBox_multpv_min_file.Text = value;
            }
            get
            {
                return this.textBox_multpv_min_file.Text;
            }
        }

        public string MultpvMaxFile
        {
            set
            {
                this.textBox_multpv_max_file.Text = value;
            }
            get
            {
                return this.textBox_multpv_max_file.Text;
            }
        }



        // Kr regions
        public bool UseKrRegionsFile 
        { 
            set
            {
                this.radioButton_use_kr_array.Checked = value;
                this.radioButton_use_kr_single_region.Checked = !value;
            }
            get
            {
                return this.radioButton_use_kr_array.Checked;
            }
        }

        public string KrRegionsFile
        {
            set
            {
                this.textBox_kr_regions_file.Text = value;
            }
            get
            {
                return this.textBox_kr_regions_file.Text;
            }
        }
            
        public string KrRegionsTitle
        {
            set
            {
                this.textBox_kr_regions_title.Text = value;
            }
            get
            {
                return this.textBox_kr_regions_title.Text;
            }
        }

        public bool KrLayerSplitting 
        { 
            set
            {
                this.radioButton_kr_2D.Checked = value;
                this.radioButton_kr_3D.Checked = !value;
            }
            get
            {
                return this.radioButton_kr_2D.Checked;
            }
        }



        // KRORW
        public bool KrorwMatching 
        { 
            set
            {
                this.checkBox_krorw_matching.Checked = value;
            }
            get
            {
                return this.checkBox_krorw_matching.Checked;
            }
        }

        public string KrorwFile 
        { 
            set
            {
                this.textBox_krorw_file.Text = value;
            }
            get
            {
                return this.textBox_krorw_file.Text;
            }
        }
            
        public string KrorwTitle
        { 
            set
            {
                this.textBox_krorw_title.Text = value;
            }
            get
            {
                return this.textBox_krorw_title.Text;
            }
        }
            
        public bool UseKrorwMinFile 
        { 
            set
            {
                this.radioButton_krorw_min_array.Checked = value;
                this.radioButton_krorw_min_value.Checked = !value;
            }
            get
            {
                return this.radioButton_krorw_min_array.Checked;
            }
        }

        public bool UseKrorwMaxFile
        {
            set
            {
                this.radioButton_krorw_max_array.Checked = value;
                this.radioButton_krorw_max_value.Checked = !value;
            }
            get
            {
                return this.radioButton_krorw_max_array.Checked;
            }
        }

        public string KrorwMinTitle 
        { 
            set
            {
                this.textBox_krorw_min_array_title.Text = value;
            }
            get
            {
                return this.textBox_krorw_min_array_title.Text;
            }
        }

        public string KrorwMaxTitle
        {
            set
            {
                this.textBox_krorw_max_array_title.Text = value;
            }
            get
            {
                return this.textBox_krorw_max_array_title.Text;
            }
        }

        public double KrorwMinValue 
        { 
            set
            {
                this.numericUpDown_krorw_min_value.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_krorw_min_value.Value;
            }
        } 

        public double KrorwMaxValue
        {
            set
            {
                this.numericUpDown_krorw_max_value.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_krorw_max_value.Value;
            }
        }

        public string KrorwMinFile 
        {
            set
            {
                this.textBox_krorw_min_array_file.Text = value;
            }
            get
            {
                return this.textBox_krorw_min_array_file.Text;
            }
        }
            
        public string KrorwMaxFile
        {
            set
            {
                this.textBox_krorw_max_array_file.Text = value;
            }
            get
            {
                return this.textBox_krorw_max_array_file.Text;
            }
        }



        // KRWR
        public bool KrwrMatching
        {
            set
            {
                this.checkBox_krwr_matching.Checked = value;
            }
            get
            {
                return this.checkBox_krwr_matching.Checked;
            }
        }

        public string KrwrFile
        {
            set
            {
                this.textBox_krwr_file.Text = value;
            }
            get
            {
                return this.textBox_krwr_file.Text;
            }
        }

        public string KrwrTitle
        {
            set
            {
                this.textBox_krwr_title.Text = value;
            }
            get
            {
                return this.textBox_krwr_title.Text;
            }
        }

        public bool UseKrwrMinFile
        {
            set
            {
                this.radioButton_krwr_min_array.Checked = value;
                this.radioButton_krwr_min_value.Checked = !value;
            }
            get
            {
                return this.radioButton_krwr_min_array.Checked;
            }
        }

        public bool UseKrwrMaxFile
        {
            set
            {
                this.radioButton_krwr_max_array.Checked = value;
                this.radioButton_krwr_max_value.Checked = !value;
            }
            get
            {
                return this.radioButton_krwr_max_array.Checked;
            }
        }

        public string KrwrMinTitle
        {
            set
            {
                this.textBox_krwr_min_array_title.Text = value;
            }
            get
            {
                return this.textBox_krwr_min_array_title.Text;
            }
        }

        public string KrwrMaxTitle
        {
            set
            {
                this.textBox_krwr_max_array_title.Text = value;
            }
            get
            {
                return this.textBox_krwr_max_array_title.Text;
            }
        }

        public double KrwrMinValue
        {
            set
            {
                this.numericUpDown_krwr_min_value.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_krwr_min_value.Value;
            }
        }

        public double KrwrMaxValue
        {
            set
            {
                this.numericUpDown_krwr_max_value.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_krwr_max_value.Value;
            }
        }

        public string KrwrMinFile
        {
            set
            {
                this.textBox_krwr_min_array_file.Text = value;
            }
            get
            {
                return this.textBox_krwr_min_array_file.Text;
            }
        }

        public string KrwrMaxFile
        {
            set
            {
                this.textBox_krwr_max_array_file.Text = value;
            }
            get
            {
                return this.textBox_krwr_max_array_file.Text;
            }
        }



        // relperm
        public bool RelPermMatching
        {
            set
            {
                this.checkBox_relperm_matching.Checked = value;
            }
            get
            {
                return this.checkBox_relperm_matching.Checked;
            }
        }

        public bool UseSatnumFile
        {
            set
            {
                this.radioButton_use_satnum_array.Checked = value;
                this.radioButton_use_single_satnum.Checked = !value;
            }
            get
            {
                return this.radioButton_use_satnum_array.Checked;
            }
        }

        public string SatnumTitle
        {
            set
            {
                this.textBox_satnum_title.Text = value;
            }
            get
            {
                return this.textBox_satnum_title.Text;
            }
        }

        public string SatnumFile
        {
            set
            {
                this.textBox_satnum_file.Text = value;
            }
            get
            {
                return this.textBox_satnum_file.Text;
            }
        }

        public string RelPermTableFile
        {
            set
            {
                this.textBox_swof_sgof_file.Text = value;
            }
            get
            {
                return this.textBox_swof_sgof_file.Text;
            }
        }

        public string CoreyInputFile
        {
            set
            {
                this.textBox_corey_file.Text = value;
            }
            get
            {
                return this.textBox_corey_file.Text;
            }
        }






        string ResPressTableFile
        {
            set
            {
                this.textBox_res_press_file.Text = value;
            }
            get
            {
                return this.textBox_res_press_file.Text;
            }
        }




        bool UseWBHPHFile
        {
            set
            {
                this.checkBox_use_wbhph_file.Checked = value;
            }
            get
            {
                return this.checkBox_use_wbhph_file.Checked;
            }
        }

        string WBHPHFile
        {
            set
            {
                this.textBox_add_wbhph.Text = value;
            }
            get
            {
                return this.textBox_add_wbhph.Text;
            }
        }

        CombineType WBHPHCombineType
        {
            set
            {
                switch (value)
                {
                    case CombineType.ADD_IF_NOT_EXIST:
                        this.radioButton_wbhph_add.Checked = true;
                        this.radioButton_wbhph_replace.Checked = false;
                        break;
                    default:
                        this.radioButton_wbhph_add.Checked = false;
                        this.radioButton_wbhph_replace.Checked = true;
                        break;
                }
            }
            get
            {
                if (this.radioButton_wbhph_add.Checked)
                    return CombineType.ADD_IF_NOT_EXIST;
                else
                    return CombineType.REPLACE_IF_EXIST;
            }
        }


        public bool PermMatching
        {
            set
            {
                this.checkBox_perm_matching.Checked = value;
            }
            get
            {
                return this.checkBox_perm_matching.Checked;
            }
        }



        bool AquiferMatching
        {
            set
            {
                this.checkBox_aquifer_matching.Checked = value;
            }
            get
            {
                return this.checkBox_aquifer_matching.Checked;
            }
        }

        AquiferRegions AquiferRegSettings
        {
            set
            {
                switch (value)
                {
                    case AquiferRegions.Array:
                        this.radioButton_aqreg_via_array.Checked = true;
                        this.radioButton_aqreg_auto.Checked = false;
                        this.radioButton_aqreg_single.Checked = false;
                        break;
                    case AquiferRegions.Auto:
                        this.radioButton_aqreg_via_array.Checked = false;
                        this.radioButton_aqreg_auto.Checked = true;
                        this.radioButton_aqreg_single.Checked = false;
                        break;
                    case AquiferRegions.Single:
                        this.radioButton_aqreg_via_array.Checked = false;
                        this.radioButton_aqreg_auto.Checked = false;
                        this.radioButton_aqreg_single.Checked = true;
                        break;
                }
            }
            get
            {
                if (this.radioButton_aqreg_via_array.Checked)
                    return AquiferRegions.Array;
                else if (this.radioButton_aqreg_auto.Checked)
                    return AquiferRegions.Auto;
                else
                    return AquiferRegions.Single;
            }
        }

        AquiferCells AquiferCellSettings
        {
            set
            {
                switch (value)
                {
                    case AquiferCells.Array:
                        this.radioButton_aqcell_via_array.Checked = true;
                        this.radioButton_aqcell_auto.Checked = false;                        
                        this.radioButton_aqcell_via_multpv.Checked = false;
                        break;
                    case AquiferCells.Auto:
                        this.radioButton_aqcell_via_array.Checked = false;
                        this.radioButton_aqcell_auto.Checked = true;
                        this.radioButton_aqcell_via_multpv.Checked = false;
                        break;
                    case AquiferCells.MultPV:
                        this.radioButton_aqcell_via_array.Checked = false;
                        this.radioButton_aqcell_auto.Checked = false;
                        this.radioButton_aqcell_via_multpv.Checked = true;
                        break;
                }
            }
            get
            {
                if (this.radioButton_aqcell_via_array.Checked)
                    return AquiferCells.Array;
                else if (this.radioButton_aqcell_auto.Checked)
                    return AquiferCells.Auto;
                else
                    return AquiferCells.MultPV;
            }
        }


        AquiferValues AquiferValuesSettings
        {
            set
            {
                switch (value)
                {
                    case AquiferValues.MultPV:
                        this.radioButton_multpv_via_array.Checked = true;
                        this.radioButton_multpv_start_value.Checked = false;
                        break;
                    case AquiferValues.Init:
                        this.radioButton_multpv_via_array.Checked = false;
                        this.radioButton_multpv_start_value.Checked = true;
                        break;
                }
            }
            get
            {
                if (this.radioButton_multpv_via_array.Checked)
                    return AquiferValues.MultPV;
                else
                    return AquiferValues.Init;
            }
        }


        int MinAquiferCellsDistance
        {
            set
            {
                this.numericUpDown_aquifer_d.Value = value;
            }
            get
            {
                return (int)this.numericUpDown_aquifer_d.Value;
            }
        }


        string MultpvTitle
        {
            set
            {
                this.textBox_multpv_title.Text = value;
            }
            get
            {
                return this.textBox_multpv_title.Text;
            }
        }

        string MultpvFile
        {
            set
            {
                this.textBox_multpv_file.Text = value;
            }
            get
            {
                return this.textBox_multpv_file.Text;
            }
        }

        string SwatinitTitle
        {
            set
            {
                this.textBox_swat_title.Text = value;
            }
            get
            {
                return this.textBox_swat_title.Text;
            }
        }

        string SwatinitFile
        {
            set
            {
                this.textBox_swat_file.Text = value;
            }
            get
            {
                return this.textBox_swat_file.Text;
            }
        }

        string ActnumTitle
        {
            set
            {
                this.textBox_actnum_title.Text = value;
            }
            get
            {
                return this.textBox_actnum_title.Text;
            }
        }

        string ActnumFile
        {
            set
            {
                this.textBox_actnum_file.Text = value;
            }
            get
            {
                return this.textBox_actnum_file.Text;
            }
        }

        string AquiferRegTitle
        {
            set
            {
                this.textBox_aqreg_title.Text = value;
            }
            get
            {
                return this.textBox_aqreg_title.Text;
            }
        }

        string AquiferRegFile
        {
            set
            {
                this.textBox_aqreg_file.Text = value;
            }
            get
            {
                return this.textBox_aqreg_file.Text;
            }
        }

        string AquiferCellTitle
        {
            set
            {
                this.textBox_aqcell_title.Text = value;
            }
            get
            {
                return this.textBox_aqcell_title.Text;
            }
        }
        string AquiferCellFile
        {
            set
            {
                this.textBox_aqcell_file.Text = value;
            }
            get
            {
                return this.textBox_aqcell_file.Text;
            }
        }

        double MultpvStart
        {
            set
            {
                this.numericUpDown_multpv_start_value.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_multpv_start_value.Value;
            }
        }

                       

        int GPUDevice
        {
            set
            {
                this.comboBox_gpu_list.SelectedIndex = Math.Min(value, this.comboBox_gpu_list.Items.Count - 1);
            }
            get
            {
                return this.comboBox_gpu_list.SelectedIndex;
            }
        }


        int NX
        {
            set
            {
                this.numericUpDown_nx.Value = Helper.Bound(this.numericUpDown_nx.Minimum, value, this.numericUpDown_nx.Maximum);
            }
            get
            {
                return (int)this.numericUpDown_nx.Value;
            }
        }
        int NY
        {
            set
            {
                this.numericUpDown_ny.Value = Helper.Bound(this.numericUpDown_ny.Minimum, value, this.numericUpDown_ny.Maximum);
            }
            get
            {
                return (int)this.numericUpDown_ny.Value;
            }
        }
        int NZ
        {
            set
            {
                this.numericUpDown_nz.Value = Helper.Bound(this.numericUpDown_nz.Minimum, value, this.numericUpDown_nz.Maximum);
            }
            get
            {
                return (int)this.numericUpDown_nz.Value;
            }
        }


        double DX
        {
            set
            {
                this.numericUpDown_dx.Value = Helper.Bound(this.numericUpDown_dx.Minimum, (decimal)value, this.numericUpDown_dx.Maximum);
            }
            get
            {
                return (double)this.numericUpDown_dx.Value;
            }
        }
        double DY
        {
            set
            {
                this.numericUpDown_dy.Value = Helper.Bound(this.numericUpDown_dy.Minimum, (decimal)value, this.numericUpDown_dy.Maximum);
            }
            get
            {
                return (double)this.numericUpDown_dy.Value;
            }
        }
        double DZ
        {
            set
            {
                this.numericUpDown_dz.Value = Helper.Bound(this.numericUpDown_dz.Minimum, (decimal)value, this.numericUpDown_dz.Maximum);
            }
            get
            {
                return (double)this.numericUpDown_dz.Value;
            }
        }

        double TopDepth
        {
            set
            {
                this.numericUpDown_top_depth.Value = Helper.Bound(this.numericUpDown_top_depth.Minimum, (decimal)value, this.numericUpDown_top_depth.Maximum);
            }
            get
            {
                return (double)this.numericUpDown_top_depth.Value;
            }
        }



        bool UseGridFile
        {
            get
            {
                return this.radioButton_use_grid.Checked;
            }
            set
            {
                this.radioButton_use_grid.Checked = value;
                this.radioButton_use_dim.Checked = !value;
            }
        }

        bool UsePermxMinFile
        {
            set
            {
                this.radioButton_perm_min_file.Checked = value;
                this.radioButton_perm_min_value.Checked = !value;
            }
            get
            {
                return this.radioButton_perm_min_file.Checked;
            }
        }

        bool UsePermxMaxFile
        {
            set
            {
                this.radioButton_perm_max_file.Checked = value;
                this.radioButton_perm_max_value.Checked = !value;
            }
            get
            {
                return this.radioButton_perm_max_file.Checked;
            }
        }

        string PermxTitle
        {
            set
            {
                this.textBox_perm_title.Text = value;
            }
            get
            {
                return this.textBox_perm_title.Text;
            }
        }



        bool UseRegionsFile
        {
            set
            {
                this.radioButton_use_perm_regions_array.Checked = value;
                this.radioButton_use_perm_single_region.Checked = !value;
            }
            get
            {
                return radioButton_use_perm_regions_array.Checked;
            }
        }


        string RegionsTitle
        {
            set
            {
                this.textBox_perm_regions_title.Text = value;
            }
            get
            {
                return this.textBox_perm_regions_title.Text;
            }
        }


        string RegionsFile
        {
            set
            {
                this.textBox_perm_regions_file.Text = value;
            }
            get
            {
                return this.textBox_perm_regions_file.Text;
            }
        }


        bool LayerSplitting
        {
            set
            {
                this.radioButton_perm_2D.Checked = value;
                this.radioButton_perm_3D.Checked = !value;
            }
            get
            {
                return this.radioButton_perm_2D.Checked;
            }
        }

        SimulatorType SimulatorType
        {
            get
            {
                if (this.radioButton_ecl.Checked)
                    return SimulatorType.Eclipse;
                else
                if (this.radioButton_tnav.Checked)
                    return SimulatorType.tNavigator;
                else
                    return SimulatorType.Tempest;
            }
            set
            {
                switch (value)
                {
                    case SimulatorType.Eclipse:
                        this.radioButton_ecl.Checked = true;
                        this.radioButton_tnav.Checked = false;
                        this.radioButton_tempest.Checked = false;
                        break;
                    case SimulatorType.tNavigator:
                        this.radioButton_ecl.Checked = false;
                        this.radioButton_tnav.Checked = true;
                        this.radioButton_tempest.Checked = false;
                        break;
                    case SimulatorType.Tempest:
                        this.radioButton_ecl.Checked = false;
                        this.radioButton_tnav.Checked = false;
                        this.radioButton_tempest.Checked = true;
                        break;
                }
                SimChanged();
            }
        }


        bool UseUserSumFile
        {
            get
            {
                return this.checkBox_create_user_sum_file.Checked;
            }
            set { this.checkBox_create_user_sum_file.Checked = value; }
        }


        bool DataCheckOnly
        {
            get
            {
                return this.radioButton_data_check.Checked;
            }
            set
            {
                this.radioButton_data_check.Checked = value;
                this.radioButton_history_matching.Checked = !value;
            }
        }




        string PermxMinTitle
        {
            get
            {
                return this.textBox_perm_min_title.Text;
            }
            set
            {
                this.textBox_perm_min_title.Text = value;
            }
        }

        string PermxMaxTitle
        {
            get
            {
                return this.textBox_perm_max_title.Text;
            }
            set
            {
                this.textBox_perm_max_title.Text = value;
            }
        }


        string PermxMinFile
        {
            get
            {
                return this.textBox_perm_min_file.Text;
            }
            set
            {
                this.textBox_perm_min_file.Text = value;
            }
        }


        string PermxMaxFile
        {
            get
            {
                return this.textBox_perm_max_file.Text;
            }
            set
            {
                this.textBox_perm_max_file.Text = value;
            }
        }


        double PermxMinValue
        {
            get
            {
                return (double)this.numericUpDown_perm_min_value.Value;
            }
            set
            {
                this.numericUpDown_perm_min_value.Value = (decimal)value;
            }
        }

        double PermxMaxValue
        {
            get
            {
                return (double)this.numericUpDown_perm_max_value.Value;
            }
            set
            {
                this.numericUpDown_perm_max_value.Value = (decimal)value;
            }
        }



        bool UseGPU
        {
            get
            {
                return this.checkBox_use_gpu.Checked;
            }
            set
            {
                this.checkBox_use_gpu.Checked = value;
            }
        }


        bool UseMPI
        {
            get
            {
                return this.textBox_mpi_exe_file.Enabled;
            }
        }

        string DataFile
        {
            get
            {
                return this.textBox_data_file.Text;
            }
            set
            {
                this.textBox_data_file.Text = value;
            }
        }

        string OutFolder
        {
            get
            {
                return this.textBox_out_folder.Text;
            }
            set
            {
                this.textBox_out_folder.Text = value;
            }
        }


        //string WBHPHTableFile { get { return this.textBox_wbhph_file.Text; } set { this.textBox_wbhph_file.Text = value; this.textBox_wbhph_file_clone.Text = value; } }
        //string WBP9HTableFile { set; get; } = string.Empty;
        int LastIter
        {
            get
            {
                return (int)this.numericUpDown_last_iter.Value;
            }
            set
            {
                this.numericUpDown_last_iter.Value = value;
            }
        }


        string PermxFile
        {
            get
            {
                return this.textBox_perm_file.Text;
            }
            set
            {
                this.textBox_perm_file.Text = value;
            }
        }



        string tNavExeFile { set; get; } = string.Empty;
        string TempestExeFile { set; get; } = string.Empty;
        string EclipseExeFile { set; get; } = string.Empty;
        string TempestMpiExeFile { set; get; } = string.Empty;
        string EclipseMpiExeFile { set; get; } = string.Empty;



        string SimulatorEXEfile
        {
            get
            {
                return this.textBox_sim_exe_file.Text;
            }
            set
            {
                this.textBox_sim_exe_file.Text = value;
            }
        }


        string MpiEXEfile
        {
            get
            {
                return this.textBox_mpi_exe_file.Text;
            }
            set
            {
                this.textBox_mpi_exe_file.Text = value;
            }
        }


        int CPU
        {
            get
            {
                return (int)this.numericUpDown_cpu.Value;
            }
            set
            {
                this.numericUpDown_cpu.Value = Helper.Bound(this.numericUpDown_cpu.Minimum, value, this.numericUpDown_cpu.Maximum);
            }
        }


        string GridFile
        {
            get
            {
                return this.textBox_grid_file.Text;
            }
            set
            {
                this.textBox_grid_file.Text = value;
            }
        }


        bool SimulateBeforeHM
        {
            get
            {
                return this.checkBox_simulate_before_hm.Checked;
            }
            set
            {
                this.checkBox_simulate_before_hm.Checked = value;
            }
        }

        string ID
        {
            get { return this.comboBox_id.SelectedItem.ToString() == def_id ? string.Empty : this.comboBox_id.SelectedItem.ToString(); }
            set { this.comboBox_id.SelectedItem = string.IsNullOrEmpty(value) ? def_id : this.comboBox_id.SelectedItem = value; }
        }

        int Iter
        {
            get { return Helper.ParseInt(this.comboBox_iter.SelectedItem.ToString()); }
            set { this.comboBox_iter.SelectedItem = HistMatching.ShowIter(value); } 
        }


        bool UseTargetWellsFile
        {
            get
            {
                return radioButton_use_wlist.Checked;
            }
            set
            {
                radioButton_use_wlist.Checked = value;
                radioButton_not_use_wlist.Checked = !value;
            }
        }


        string TargetWellsFile
        {
            get
            {
                return this.textBox_wlist.Text;
            }
            set
            {
                this.textBox_wlist.Text = value;
            }
        }


        string ControlFile
        {
            get
            {
                return this.textBox_control_custom_file.Text;
            }
            set
            {
                this.textBox_control_custom_file.Text = value;
            }
        }


        ControlType Control
        {
            get
            {
                if (this.radioButton_control_lrat.Checked)
                    return ControlType.LRAT;
                if (this.radioButton_control_grat.Checked)
                    return ControlType.GRAT;
                if (this.radioButton_control_orat.Checked)
                    return ControlType.ORAT;
                if (this.radioButton_control_auto.Checked)
                    return ControlType.AUTO;
                return ControlType.CUSTOM;
            }
            set
            {
                switch (value)
                {
                    case ControlType.LRAT:
                        this.radioButton_control_lrat.Checked = true;
                        this.radioButton_control_grat.Checked = false;
                        this.radioButton_control_orat.Checked = false;
                        this.radioButton_control_auto.Checked = false;
                        this.radioButton_control_custom.Checked = false;
                        break;
                    case ControlType.GRAT:
                        this.radioButton_control_lrat.Checked = false;
                        this.radioButton_control_grat.Checked = true;
                        this.radioButton_control_orat.Checked = false;
                        this.radioButton_control_auto.Checked = false;
                        this.radioButton_control_custom.Checked = false;
                        break;
                    case ControlType.ORAT:
                        this.radioButton_control_lrat.Checked = false;
                        this.radioButton_control_grat.Checked = false;
                        this.radioButton_control_orat.Checked = true;
                        this.radioButton_control_auto.Checked = false;
                        this.radioButton_control_custom.Checked = false;
                        break;
                    case ControlType.AUTO:
                        this.radioButton_control_lrat.Checked = false;
                        this.radioButton_control_grat.Checked = false;
                        this.radioButton_control_orat.Checked = false;
                        this.radioButton_control_auto.Checked = true;
                        this.radioButton_control_custom.Checked = false;
                        break;
                    case ControlType.CUSTOM:
                        this.radioButton_control_lrat.Checked = false;
                        this.radioButton_control_grat.Checked = false;
                        this.radioButton_control_orat.Checked = false;
                        this.radioButton_control_auto.Checked = false;
                        this.radioButton_control_custom.Checked = true;
                        break;
                }
            }
        }



        double LiqProdOilTotal { set { this.deviationsControl.LiqProdOilTotal = value; } get { return this.deviationsControl.LiqProdOilTotal; } }
        double LiqProdWatTotal { set { this.deviationsControl.LiqProdWatTotal = value; } get { return this.deviationsControl.LiqProdWatTotal; } }
        double LiqProdGasTotal { set { this.deviationsControl.LiqProdGasTotal = value; } get { return this.deviationsControl.LiqProdGasTotal; } }
        double LiqProdLiqTotal { set { this.deviationsControl.LiqProdLiqTotal = value; } get { return this.deviationsControl.LiqProdLiqTotal; } }
        double GasProdOilTotal { set { this.deviationsControl.GasProdOilTotal = value; } get { return this.deviationsControl.GasProdOilTotal; } }
        double GasProdWatTotal { set { this.deviationsControl.GasProdWatTotal = value; } get { return this.deviationsControl.GasProdWatTotal; } }
        double GasProdGasTotal { set { this.deviationsControl.GasProdGasTotal = value; } get { return this.deviationsControl.GasProdGasTotal; } }
        double GasProdLiqTotal { set { this.deviationsControl.GasProdLiqTotal = value; } get { return this.deviationsControl.GasProdLiqTotal; } }
        double OilInjeTotal { set { this.deviationsControl.OilInjeTotal = value; } get { return this.deviationsControl.OilInjeTotal; } }
        double WatInjeTotal { set { this.deviationsControl.WatInjeTotal = value; } get { return this.deviationsControl.WatInjeTotal; } }
        double GasInjeTotal { set { this.deviationsControl.GasInjeTotal = value; } get { return this.deviationsControl.GasInjeTotal; } }
        double LiqProdOilRate { set { this.deviationsControl.LiqProdOilRate = value; } get { return this.deviationsControl.LiqProdOilRate; } }
        double LiqProdWatRate { set { this.deviationsControl.LiqProdWatRate = value; } get { return this.deviationsControl.LiqProdWatRate; } }
        double LiqProdGasRate { set { this.deviationsControl.LiqProdGasRate = value; } get { return this.deviationsControl.LiqProdGasRate; } }
        double LiqProdLiqRate { set { this.deviationsControl.LiqProdLiqRate = value; } get { return this.deviationsControl.LiqProdLiqRate; } }
        double GasProdOilRate { set { this.deviationsControl.GasProdOilRate = value; } get { return this.deviationsControl.GasProdOilRate; } }
        double GasProdWatRate { set { this.deviationsControl.GasProdWatRate = value; } get { return this.deviationsControl.GasProdWatRate; } }
        double GasProdGasRate { set { this.deviationsControl.GasProdGasRate = value; } get { return this.deviationsControl.GasProdGasRate; } }
        double GasProdLiqRate { set { this.deviationsControl.GasProdLiqRate = value; } get { return this.deviationsControl.GasProdLiqRate; } }
        double OilInjeRate { set { this.deviationsControl.OilInjeRate = value; } get { return this.deviationsControl.OilInjeRate; } }
        double WatInjeRate { set { this.deviationsControl.WatInjeRate = value; } get { return this.deviationsControl.WatInjeRate; } }
        double GasInjeRate { set { this.deviationsControl.GasInjeRate = value; } get { return this.deviationsControl.GasInjeRate; } }
        double MaxBHPDev { set { this.deviationsControl.BHP = value; } get { return this.deviationsControl.BHP; } }
        double MaxResPressDev { set { this.deviationsControl.RP = value; } get { return this.deviationsControl.RP; } }

        bool LiqProdOilRequested { set { this.deviationsControl.LiqProdOilRequested = value; } get { return this.deviationsControl.LiqProdOilRequested; } }
        bool LiqProdWatRequested { set { this.deviationsControl.LiqProdWatRequested = value; } get { return this.deviationsControl.LiqProdWatRequested; } }
        bool LiqProdGasRequested { set { this.deviationsControl.LiqProdGasRequested = value; } get { return this.deviationsControl.LiqProdGasRequested; } }
        bool LiqProdRequested { set { this.deviationsControl.LiqProdRequested = value; } get { return this.deviationsControl.LiqProdRequested; } }
        bool GasProdOilRequested { set { this.deviationsControl.GasProdOilRequested = value; } get { return this.deviationsControl.GasProdOilRequested; } }
        bool GasProdWatRequested { set { this.deviationsControl.GasProdWatRequested = value; } get { return this.deviationsControl.GasProdWatRequested; } }
        bool GasProdRequested { set { this.deviationsControl.GasProdRequested = value; } get { return this.deviationsControl.GasProdRequested; } }
        bool GasProdLiqRequested { set { this.deviationsControl.GasProdLiqRequested = value; } get { return this.deviationsControl.GasProdLiqRequested; } }
        bool OilInjeRequested { set { this.deviationsControl.OilInjeRequested = value; } get { return this.deviationsControl.OilInjeRequested; } }
        bool WatInjeRequested { set { this.deviationsControl.WatInjeRequested = value; } get { return this.deviationsControl.WatInjeRequested; } }
        bool GasInjeRequested { set { this.deviationsControl.GasInjeRequested = value; } get { return this.deviationsControl.GasInjeRequested; } }
        bool BHPRequested { set { this.deviationsControl.BHPRequested = value; } get { return this.deviationsControl.BHPRequested; } }
        bool RPRequested { set { this.deviationsControl.RPRequested = value; } get { return this.deviationsControl.RPRequested; } }






        public double MaxGLR
        {
            set
            {
                this.numericUpDown_glr.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_glr.Value;
            }
        }




        bool ConfirmRun(string datafile)
        {
            string title = "Start running";
            string message = "Do you want to start running?";
            /*
            if (show_note)
            {
                string text = "\n\nNote:\n";
                text += "The Command Prompt window will now appear. ";
                text += $"This indicates that the case '{datafile}' is running with the required settings via the command line. ";
                text += "Do not close this window. It will not damage your data or computer. ";
                text += "It can be minimised if necessary. It will disappear automatically when the run is completed.";
                message += text;
            }
            */
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            return result == DialogResult.Yes;
        }


        /*
        CancellationTokenSource run_task_token;
        void run_task_abort(object o, EventArgs args)
        {
            //run_task_token?.Cancel();
        }
        */

        /*
        Thread run_thread;
        void run_thread_abort(object o, EventArgs args)
        {
            run_thread?.Abort();
        }
        */



        private void button_run_Click(object sender, EventArgs e)
        {
            SaveInputData();
            try
            {
                if (!ConfirmRun(DataFile)) return;
                string save_file = SaveFile; // для разрыва связи с элементом контроля
                //RunStartedEvent?.Invoke(save_file);
                ResultsViewTreeDataDownloader.PushCases(DownloaderID, save_file);

                CancellationTokenSource run_task_token = new CancellationTokenSource();
                HistMatchingInput input = InputData;

                HistMatching histMatching = new HistMatching(input);

                // should be here, otherwise at window 7 runForm became hided 
                RunForm runForm = histMatching.RunForm();

                Task run_task = new Task(() =>
                {
                    /*
                    Thread runForm_thread = new Thread(() =>
                    {
                        Application.Run(runForm);
                        run_task_token?.Cancel();
                    })
                    {
                        ApartmentState = ApartmentState.STA
                    };
                    runForm_thread.Start();
                    */
                    
                    Task runForm_task = new Task(() =>
                    {
                        //Application.Run(runForm);
                        runForm.ShowDialog();
                        run_task_token?.Cancel();
                    });
                    runForm_task.Start();
                    

                    Thread.Sleep(50);

                    if (DataCheckOnly)
                    {
                        histMatching.AllDataDefined();
                    }
                    else
                    {
                        if (input.StartIter == 0 && input.SimulateBeforeRun)
                        {
                            histMatching.SimulatorRunner.SimulateCase(input.DataFile);
                        }
                        histMatching.Run();
                    }
                    Task.WaitAll(runForm_task);
                    //runForm_thread.Join();
                }, run_task_token.Token);

                run_task.Start();

                this.Hide();

                Task.WaitAll(run_task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Show();
            }
        }




        /*        
        private void button_run_Click(object sender, EventArgs e)
        {
            SaveInputData();
            try
            {
                if (!ConfirmRun(DataFile)) return;
                string save_file = SaveFile; // для разрыва связи с элементом контроля
                RunStartedEvent?.Invoke(save_file);

                this.Hide();

                HistMatchingInput input = InputData;
                HistMatching histMatching = new HistMatching(input);
                RunForm runForm = histMatching.RunForm();
                runForm.AbortRunEvent += run_task_abort; // NOTE: 2

                run_task_token = new CancellationTokenSource();

                Task runForm_task = new Task(() =>
                {
                    runForm.ShowDialog();
                });
                runForm_task.Start();
                
                Thread.Sleep(50);

        
                Task run_task = new Task(() =>
                {
                    if (DataCheckOnly)
                    {
                        histMatching.AllDataDefined();
                    }
                    else
                    {
                        if (input.StartIter == 0 && input.SimulateBeforeRun)
                        {
                            histMatching.SimulatorRunner.SimulateCase(input.DataFile);
                        }
                        histMatching.Run();
                    }
                }, run_task_token.Token);
                run_task.Start();


                Task.WaitAll(run_task, runForm_task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Show();
            }
        }
        */




        /*
        private void button_run_Click(object sender, EventArgs e)
        {
            SaveInputData();
            try
            {
                if (!ConfirmRun(DataFile)) return;
                string save_file = SaveFile; // для разрыва связи с элементом контроля
                RunStartedEvent?.Invoke(save_file);

                this.Hide();

                HistMatchingInput input = InputData;
                HistMatching histMatching = new HistMatching(input);
                RunForm runForm = histMatching.RunForm();
                runForm.AbortRunEvent += run_thread_abort; // NOTE: 2

                Thread runForm_thread = new Thread(new ThreadStart(() => { runForm.ShowDialog(); }));
                runForm_thread.Start();
                Thread.Sleep(10);

                run_thread = new Thread(new ThreadStart(() =>
                {
                    if (DataCheckOnly)
                    {
                        histMatching.AllDataDefined();
                    }
                    else
                    {
                        if (input.StartIter == 0 && input.SimulateBeforeRun)
                        {
                            histMatching.SimulatorRunner.SimulateCase(input.DataFile);
                        }
                        histMatching.Run();
                    }
                }));
                run_thread.Start();

                run_thread.Join();
                runForm_thread.Join();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Show();
            }
        }
        */






        public EventHandler EndRunEvent;


        /*
        CancellationTokenSource sim_task_token;
        void sim_task_abort(object o, EventArgs args)
        {
            sim_task_token?.Cancel();
        }
        */

        private void button_sim_case_Click(object sender, EventArgs e)
        {
            HistMatchingInput input = InputData;

            if (ConfirmRun(input.DataFile))
            {
                HistMatching histMatching = new HistMatching(input);
                RunForm simForm = histMatching.RunForm();
                //simForm.AbortRunEvent += sim_task_abort; // NOTE: 2

                CancellationTokenSource sim_task_token = new CancellationTokenSource();

                Task run_task = new Task(() =>
                {
                    Task runForm_task = new Task(() =>
                    {
                        simForm.ShowDialog();
                        sim_task_token?.Cancel();
                    });
                    runForm_task.Start();

                    Thread.Sleep(50);

                    histMatching.SimulatorRunner.SimulateCase(input.DataFile);
                    //simForm?.BeginInvoke(new Action(()=> simForm.Dispose()));
                    Task.WaitAll(runForm_task);
                }, sim_task_token.Token);
                run_task.Start();
            }
        }








        /*
        private void button_sim_case_Click(object sender, EventArgs e)
        {
            HistMatchingInput input = InputData;

            if (ConfirmRun(input.DataFile))
            {
                HistMatching histMatching = new HistMatching(input);
                RunForm runForm = histMatching.RunForm();
                runForm.AbortRunEvent += run_task_abort; // NOTE: 2
                //runForm.UseGPUDevice = false;
                Thread runForm_thread = new Thread(new ThreadStart(() => { runForm.ShowDialog(); }));
                runForm_thread.Start();

                Thread run_thread2 = new Thread(new ThreadStart(() =>
                {
                    histMatching.SimulatorRunner.SimulateCase(input.DataFile);
                    //runForm.Close();
                    runForm_thread.Abort();
                }));
                Thread.Sleep(10);
                run_thread2.Start();
                //run_thread.Join();
                //runForm_thread.Join();
            }
        }
        */




        string SaveFile
        {
            get
            {
                if (string.IsNullOrEmpty(DataFile))
                    return string.Empty;
                return Path.ChangeExtension(DataFile, HistMatching.EXT);
            }
        }


        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }


        private void button_data_file_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("DATA Files (*.{0})|*.{0}|All Files (*.*)|*.*", "DAT?");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                DataFile = filename;
                HistMatchingInput.Load(Path.ChangeExtension(filename, HistMatching.EXT), out HistMatchingInput temp);
                //temp.DataFile = filename;
                InputData = temp;
                DataFile = filename;
                SaveInputData();
            }
        }

        private void button_grid_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Grid GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_grid_file.Text = filename;
        }

        private void button_permx_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_perm_file.Text = filename;
        }

        private void button_results_folder_Click(object sender, EventArgs e)
        {
            /*
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Open Results Folder";
            if (dialog.ShowDialog() == DialogResult.OK)
                ResultsFolder = dialog.SelectedPath;
                */
        }

        private void button_wbhph_file_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("WPHPH Table File (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                WBHPHTableFile = filename;
                */
        }


        void SimChanged()
        {
            this.button_copy_sum.Visible = SimulatorType != SimulatorType.Tempest;
            this.checkBox_create_user_sum_file.Visible = SimulatorType == SimulatorType.tNavigator;
            switch (SimulatorType)
            {
                case SimulatorType.Eclipse:
                    {
                        // gpu/mpi
                        this.checkBox_use_gpu.Visible = false;
                        this.comboBox_gpu_list.Visible = false;
                        // exe/mpi
                        this.label_sim_exe_file.Text = "e*.exe";
                        this.label_mpi_exe_file.Text = "e*_ilmpi.exe";                        
                        bool use_mpi = CPU > 1;
                        this.textBox_mpi_exe_file.Enabled = use_mpi;
                        this.label_mpi_exe_file.Enabled = use_mpi;
                        this.button_mpi_exe_file.Enabled = use_mpi;
                        this.textBox_sim_exe_file.Enabled = true;
                        this.label_sim_exe_file.Enabled = true;
                        this.button_sim_exe_file.Enabled = true;
                        // result
                        this.textBox_sim_exe_file.Text = EclipseExeFile;
                        this.textBox_mpi_exe_file.Text = EclipseMpiExeFile;
                        //
                        this.checkBox_results_sub_folder.Visible = false;
                    }
                    break;
                case SimulatorType.Tempest:
                    {
                        // gpu/mpi
                        this.checkBox_use_gpu.Visible = false;
                        this.comboBox_gpu_list.Visible = false;
                        // exe/mpi
                        this.label_sim_exe_file.Text = "mored.exe"; // TODO
                        this.label_mpi_exe_file.Text = "mpirun.exe"; // TODO
                        bool use_mpi = true;
                        this.textBox_mpi_exe_file.Enabled = use_mpi;
                        this.label_mpi_exe_file.Enabled = use_mpi;
                        this.button_mpi_exe_file.Enabled = use_mpi;
                        this.textBox_sim_exe_file.Enabled = true;
                        this.label_sim_exe_file.Enabled = true;
                        this.button_sim_exe_file.Enabled = true;
                        // result
                        this.textBox_sim_exe_file.Text = TempestExeFile;
                        this.textBox_mpi_exe_file.Text = TempestMpiExeFile;
                        //
                        this.checkBox_results_sub_folder.Visible = false;
                    }
                    break;
                case SimulatorType.tNavigator:
                    {
                        // gpu/mpi
                        this.checkBox_use_gpu.Visible = true;
                        this.comboBox_gpu_list.Visible = UseGPU;
                        // exe/mpi
                        this.label_sim_exe_file.Text = "tNav*.exe";
                        this.label_mpi_exe_file.Text = "MPI*.exe";
                        bool use_mpi = false;
                        this.textBox_mpi_exe_file.Enabled = use_mpi;
                        this.label_mpi_exe_file.Enabled = use_mpi;
                        this.button_mpi_exe_file.Enabled = use_mpi;
                        this.textBox_sim_exe_file.Enabled = true;
                        this.label_sim_exe_file.Enabled = true;
                        this.button_sim_exe_file.Enabled = true;
                        // result
                        this.textBox_sim_exe_file.Text = tNavExeFile;
                        this.textBox_mpi_exe_file.Text = not_used;
                        //
                        this.checkBox_results_sub_folder.Visible = true;
                    }
                    break;
            }
        }


        void UpdateSimExe()
        {
            switch (SimulatorType)
            {
                case SimulatorType.Eclipse:
                    EclipseExeFile = this.textBox_sim_exe_file.Text;
                    break;
                case SimulatorType.Tempest:
                    TempestExeFile = this.textBox_sim_exe_file.Text;
                    break;
                case SimulatorType.tNavigator:
                    tNavExeFile = this.textBox_sim_exe_file.Text;
                    break;
            }
        }

        void UpdateMpiExe()
        {
            switch (SimulatorType)
            {
                case SimulatorType.Eclipse:
                    EclipseMpiExeFile = this.textBox_mpi_exe_file.Text;
                    break;
                case SimulatorType.Tempest:
                    TempestMpiExeFile = this.textBox_mpi_exe_file.Text;
                    break;
            }
        }


        private void textBox_sim_exe_file_TextChanged(object sender, EventArgs e)
        {
            UpdateSimExe();
        }

        private void textBox_mpi_exe_file_TextChanged(object sender, EventArgs e)
        {
            UpdateMpiExe();
        }


        private void radioButton_sim_CheckedChanged(object sender, EventArgs e)
        {
            ChangeOutFolder();
            SimChanged();
        }
        
        private void numericUpDown_cpu_ValueChanged(object sender, EventArgs e)
        {
            ChangeOutFolder();
            SimChanged();
        }

        private void checkBox_use_gpu_CheckedChanged(object sender, EventArgs e)
        {
            ChangeOutFolder();
            SimChanged();
        }
        
        private void button_tNav_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = $"{this.label_sim_exe_file.Text} (*.EXE)|*.EXE|All Files (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_sim_exe_file.Text = filename;
        }

        private void button_target_wells_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Targer Wells List File (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_wlist.Text = filename;
        }

        private void button_open_wbp9h_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("WBP9H Table File (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                WBP9HTableFile = filename;
            */
        }

        List<string> IDs
        {
            set
            {
                this.comboBox_id.Items.Clear();
                this.comboBox_id.Items.Add(def_id);
                foreach (string id in value)
                    this.comboBox_id.Items.Add(id);
                this.comboBox_id.SelectedItem = def_id;
                Iters = new List<string>();
            }
            get
            {
                List<string> result = new List<string>();
                result.Add(string.Empty);
                for (int i = 1; i < this.comboBox_id.Items.Count; ++i)
                    result.Add(this.comboBox_id.Items[i].ToString());
                return result;
            }
        }


        List<string> Iters
        {
            set
            {
                List<string> items = new List<string>();
                items.Add(HistMatching.ShowIter(0));
                items.AddRange(value);
                this.comboBox_iter.Items.Clear();
                foreach (string iter in items.Distinct())
                    this.comboBox_iter.Items.Add(iter);
                this.comboBox_iter.SelectedIndex = 0;
            }
            get
            {
                List<string> result = new List<string>();
                result.Add(string.Empty);
                for (int i = 1; i < this.comboBox_iter.Items.Count; ++i)
                    result.Add(this.comboBox_iter.Items[i].ToString());
                return result;
            }
        }


        const string def_id = "<new>";
        const string def_iter = "<continue>";

        private void button_cancel_Click(object sender, EventArgs e)
        {
            SaveInputData();
            Close();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            SaveInputData();
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            SaveInputData();
        }

        private void radioButton_data_check_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBox_id.Enabled = !this.radioButton_data_check.Checked;
            this.comboBox_iter.Enabled = !this.radioButton_data_check.Checked;
            this.label_id.Enabled = !this.radioButton_data_check.Checked;
            this.label_iter.Enabled = !this.radioButton_data_check.Checked;

            this.numericUpDown_last_iter.Enabled = this.radioButton_history_matching.Checked;
            this.checkBox_simulate_before_hm.Enabled = this.radioButton_history_matching.Checked;
            this.label_last_iter.Enabled = this.radioButton_history_matching.Checked;
        }


        private void button_summary_kw_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(string.Join("\n", HM.IterationResult.ExpectedSUMMARY()));
            MessageBox.Show("SUMMARY section copied to the clipboard.");
        }

        private void comboBox_id_DropDown(object sender, EventArgs e)
        {
            string[] folders = HM.HistMatching.GetExistingIDFolders(DataFile);
            string[] ids = HM.HistMatching.GetExistingIDs(folders);
            IDs = ids.ToList();
            //Iter = string.Empty;
        }

        private void comboBox_iter_DropDown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                Iters = HM.HistMatching.ExistingItersNames(DataFile, ID);
            }
        }

        private void WizardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveInputData();
        }

        private void radioButton_not_use_well_list_CheckedChanged(object sender, EventArgs e)
        {
            UseWellNames(false);
        }

        private void radioButton_use_well_list_CheckedChanged(object sender, EventArgs e)
        {
            UseWellNames(true);
        }

        void UseWellNames(bool use)
        {
            this.textBox_wlist.Enabled = use;
            this.button_wlist.Enabled = use;
            this.richTextBox_wlist.Enabled = use;
        }




        private void radioButton_not_use_wlist_CheckedChanged(object sender, EventArgs e)
        {
            UseWlist(false);
        }

        private void radioButton_use_wlist_CheckedChanged(object sender, EventArgs e)
        {
            UseWlist(true);
        }

        void UseWlist(bool use)
        {
            this.richTextBox_wlist.Enabled = use;
            this.textBox_wlist.Enabled = use;
            this.button_wlist.Enabled = use;
        }



        private void radioButton_use_grid_CheckedChanged(object sender, EventArgs e)
        {
            GridChahged();
        }

        private void radioButton_use_dim_CheckedChanged(object sender, EventArgs e)
        {
            GridChahged();
        }


        private void numericUpDown_dx_ValueChanged(object sender, EventArgs e)
        {
            GridChahged();
        }

        private void numericUpDown_dy_ValueChanged(object sender, EventArgs e)
        {
            GridChahged();
        }

        private void numericUpDown_dz_ValueChanged(object sender, EventArgs e)
        {
            GridChahged();
        }

        private void textBox_grid_file_TextChanged(object sender, EventArgs e)
        {
            GridChahged();
        }

        void GridChahged()
        {
            bool use_dim = !UseGridFile;
            this.tableLayoutPanel_dim.Enabled = use_dim;
            this.button_grid_file.Enabled = !use_dim;
            this.textBox_grid_file.Enabled = !use_dim;
            this.richTextBox_grid_file.Enabled = !use_dim;
        }


        private void radioButton_permMinValue_CheckedChanged(object sender, EventArgs e)
        {
            PermxMinChahged();
        }

        private void radioButton_permMinArray_CheckedChanged(object sender, EventArgs e)
        {
            PermxMinChahged();
        }


        private void numericUpDown_perm_min_value_ValueChanged(object sender, EventArgs e)
        {
            PermxMinChahged();
        }


        private void textBox_permMinArray_TextChanged(object sender, EventArgs e)
        {
            PermxMinChahged();
        }

        void PermxMinChahged()
        {
            bool usevalue = !this.UsePermxMinFile;
            this.richTextBox_perm_min_note.Enabled = !usevalue;
            this.numericUpDown_perm_min_value.Enabled = usevalue;
            this.button_perm_min_file.Enabled = !usevalue;
            this.textBox_perm_min_file.Enabled = !usevalue;
            this.textBox_perm_min_title.Enabled = !usevalue;
        }




        private void radioButton_permMaxValue_CheckedChanged(object sender, EventArgs e)
        {
            PermxMaxChahged();
        }

        private void radioButton_permMaxArray_CheckedChanged(object sender, EventArgs e)
        {
            PermxMaxChahged();
        }


        private void numericUpDown_perm_max_value_ValueChanged(object sender, EventArgs e)
        {
            PermxMaxChahged();
        }


        private void textBox_permMaxArray_TextChanged(object sender, EventArgs e)
        {
            PermxMaxChahged();
        }

        void PermxMaxChahged()
        {
            bool usevalue = !this.UsePermxMaxFile;
            this.richTextBox_perm_max_note.Enabled = !usevalue;
            this.numericUpDown_perm_max_value.Enabled = usevalue;
            this.button_perm_max_file.Enabled = !usevalue;
            this.textBox_perm_max_file.Enabled = !usevalue;
            this.textBox_perm_max_title.Enabled = !usevalue;
        }

        private void button_permMinArray_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_perm_min_file.Text = filename;
        }

        private void button_permMaxArray_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_perm_max_file.Text = filename;
        }

        private void textBox_data_file_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox_data_file.Text))
                this.Text = OriginalText;
            else
                this.Text = OriginalText + " | " + Path.ChangeExtension(this.textBox_data_file.Text, null);
            ChangeOutFolder();
        }

        void ChangeOutFolder()
        {
            switch (SimulatorType)
            {
                case SimulatorType.Tempest:
                    this.richTextBox_out_folder.Text =
                        "NOTE1: So that Tempest simulator results are saved in the same directory as the DATA-file, the keyword \"OPEN [ALL]\" must be removed from the DATA-file.\n" +
                        "NOTE2: The RATE keyword in the RECUrent section must contain WELL and CRAT (for example: \"RATE 1 month EXACT WELL CRAT /\").";
                    this.richTextBox_out_folder.SelectionStart = 0;
                    this.richTextBox_out_folder.SelectionLength = this.richTextBox_out_folder.Text.Length;
                    this.richTextBox_out_folder.SelectionColor = Color.Red;
                    break;
                case SimulatorType.Eclipse:
                    this.richTextBox_out_folder.Text =
                        "NOTE: Copy the SUMMARY section keywords to the DATA-file and complete it (see comments inside).";
                    this.richTextBox_out_folder.SelectionStart = 0;
                    this.richTextBox_out_folder.SelectionLength = this.richTextBox_out_folder.Text.Length;
                    this.richTextBox_out_folder.SelectionColor = Color.Red;
                    break;
                default:
                    this.richTextBox_out_folder.Text =
                        "NOTE: For tNavigator the required SUMMARY file will be automatically created in the USER folder.";
                    this.richTextBox_out_folder.SelectionStart = 0;
                    this.richTextBox_out_folder.SelectionLength = this.richTextBox_out_folder.Text.Length;
                    this.richTextBox_out_folder.SelectionColor = Color.Black;
                    break;
            }
            OutFolder = HistMatching.GetRsmFolder(DataFile, SimulatorType, ResultsSubFolderUsed);
        }




        private void radioButton_use_single_region_CheckedChanged(object sender, EventArgs e)
        {
            UseRegionsChanged();
        }

        private void radioButton_use_regions_array_CheckedChanged(object sender, EventArgs e)
        {
            UseRegionsChanged();
        }

        const string not_used = "<not used>";
        const string used = "<used>";
        void UseRegionsChanged()
        {
            bool usefile = this.UseRegionsFile;
            //this.tableLayoutPanel_regions.Enabled = usefile;
            this.textBox_perm_regions_file.Enabled = usefile;
            this.button_perm_regions_file.Enabled = usefile;
            this.textBox_perm_regions_title.Enabled = usefile;
            this.richTextBox_regions.Enabled = usefile;
        }


        private void button_regions_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_perm_regions_file.Text = filename;
        }

        /*
        private void AddHyperlinkText(string linkURL, string linkName,
              string TextBeforeLink, string TextAfterLink)
        {
            Paragraph para = new Paragraph();
            para.Margin = new Thickness(0); // remove indent between paragraphs

            Hyperlink link = new Hyperlink();
            link.IsEnabled = true;
            link.Inlines.Add(linkName);
            link.NavigateUri = new Uri(linkURL);
            link.RequestNavigate += (sender, args) => Process.Start(args.Uri.ToString());

            para.Inlines.Add(new Run("[" + DateTime.Now.ToLongTimeString() + "]: "));
            para.Inlines.Add(TextBeforeLink);
            para.Inlines.Add(link);
            para.Inlines.Add(new Run(TextAfterLink));

            rtb.Document.Blocks.Add(para);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddHyperlinkText("http://www.google.com", "http://www.google.com",
                   "Please visit ", ". Thank you! Some veeeeeeeeeery looooooong text.");
        }
        */




        public static int ProcessorCount
        {
            get
            {
                return Environment.ProcessorCount;
            }
        }



        public static string[] LoadGPU()
        {
            List<string> result = new List<string>();
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                int i = 0;
                foreach (ManagementObject obj in searcher.Get())
                {
                    string item = string.Empty;
                    //item += obj["DeviceID"];
                    item += $"{i++}-";
                    item += obj["Name"];
                    /*
                    Response.Write("AdapterRAM  -  " + obj["AdapterRAM"] + "</br>");
                    Response.Write("AdapterDACType  -  " + obj["AdapterDACType"] + "</br>");
                    Response.Write("Monochrome  -  " + obj["Monochrome"] + "</br>");
                    Response.Write("InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"] + "</br>");
                    Response.Write("DriverVersion  -  " + obj["DriverVersion"] + "</br>");
                    Response.Write("VideoProcessor  -  " + obj["VideoProcessor"] + "</br>");
                    Response.Write("VideoArchitecture  -  " + obj["VideoArchitecture"] + "</br>");
                    Response.Write("VideoMemoryType  -  " + obj["VideoMemoryType"] + "</br>");
                    */
                    result.Add(item);
                }
            }
            return result.ToArray();
        }

        void SetGridFile(string gridfile)
        {
            UseGridFile = true;
            GridFile = gridfile;
        }


        private void button_split_files_Click(object sender, EventArgs e)
        {
            FileCombinerForm splitFilesForm = new FileCombinerForm();
            splitFilesForm.GridFileHandler += SetGridFile;
            splitFilesForm.Show();
        }

        private void WizardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = @"Do you really want to close the Wizard?";
            if (e.CloseReason == CloseReason.UserClosing &&
                MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void button_mpi_exe_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = $"{this.label_mpi_exe_file.Text} (*.EXE)|*.EXE|All Files (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_mpi_exe_file.Text = filename;
        }


        private void button_out_folder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Open Out Folder";
            if (dialog.ShowDialog() == DialogResult.OK)
                OutFolder = dialog.SelectedPath;
        }



        void AqMatchingInterface(bool matching)
        {
            //this.tableLayoutPanel_multpv.Enabled = matching;
            //this.tableLayoutPanel_res_press.Enabled = matching;            
            this.groupBox_aqreg.Enabled = matching;
            this.groupBox_aqcells.Enabled = matching;
            this.groupBox_aqval.Enabled = matching;
            this.groupBox_aq_input.Enabled = matching;
            this.tabControl_multpv_min_max.Enabled = matching;
        }



        private void button_aquifer_regions_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_aqreg_file.Text = filename;
        }

        private void button_aquifer_array_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_multpv_file.Text = filename;
        }

        private void button_res_press_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Press Table File (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_res_press_file.Text = filename;
        }

        private void comboBox_iter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkBox_simulate_before_hm.Visible = Iter == 0;
            this.numericUpDown_last_iter.Minimum = comboBox_iter.SelectedIndex + 1;
        }

        private void comboBox_id_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //this.comboBox_iter.Enabled = !string.IsNullOrEmpty(ID);
            if (!string.IsNullOrEmpty(ID))
            {
                Iters = HM.HistMatching.ExistingItersNames(DataFile, ID);
            }
        }

        private void button_swat_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_swat_file.Text = filename;
        }

        private void button_actnum_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_actnum_file.Text = filename;
        }

        private void radioButton_aqreg_auto_CheckedChanged(object sender, EventArgs e)
        {
            AqRegCell();
        }

        private void radioButton_aqreg_manually_CheckedChanged(object sender, EventArgs e)
        {
            AqRegCell();
        }

        private void radioButton_aqreg_single_CheckedChanged(object sender, EventArgs e)
        {
            AqRegCell();
        }

        private void radioButton_aqcell_auto_CheckedChanged(object sender, EventArgs e)
        {
            AqRegCell();
        }

        private void radioButton_aqcell_manually_CheckedChanged(object sender, EventArgs e)
        {
            AqRegCell();
        }

        private void radioButton_aqcell_via_multpv_CheckedChanged(object sender, EventArgs e)
        {
            AqRegCell();
        }


        void AqRegCell()
        {
            this.tableLayoutPanel_swat.Enabled = this.radioButton_aqcell_auto.Checked;
            this.tableLayoutPanel_actnum.Enabled = this.radioButton_aqcell_auto.Checked || this.radioButton_aqreg_auto.Checked;
            this.tableLayoutPanel_aqcell.Enabled = this.radioButton_aqcell_via_array.Checked;
            this.tableLayoutPanel_aqreg.Enabled = this.radioButton_aqreg_via_array.Checked;
            this.labe_aquifer_d.Enabled = this.radioButton_aqcell_auto.Checked;
            this.numericUpDown_aquifer_d.Enabled = this.radioButton_aqcell_auto.Checked;
        }




        private void button_aqcell_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_aqcell_file.Text = filename;
        }

        private void radioButton_use_thph_as_res_press_CheckedChanged(object sender, EventArgs e)
        {
            ResPressFile(false);
        }

        private void radioButton_use_res_press_file_CheckedChanged(object sender, EventArgs e)
        {
            ResPressFile(true);
        }

        void ResPressFile(bool use)
        {
            this.button_res_press_file.Enabled = use;
            this.textBox_res_press_file.Enabled = use;
        }

        private void radioButton_multpv_via_array_CheckedChanged(object sender, EventArgs e)
        {
            AqVal();
        }

        private void radioButton_multpv_start_value_CheckedChanged(object sender, EventArgs e)
        {
            AqVal();
        }

        void AqVal()
        {
            this.numericUpDown_multpv_start_value.Enabled = this.radioButton_multpv_start_value.Checked;
            //this.numericUpDown_multpv_max_value.Enabled = false;
        }


        bool UseResPressFile
        {
            set
            {
                this.radioButton_use_res_press_file.Checked = value;
                this.radioButton_use_thph_as_res_press.Checked = !value;
            }
            get
            {
                return this.radioButton_use_res_press_file.Checked;
            }
        }


        private void radioButton_control_lrat_CheckedChanged(object sender, EventArgs e)
        {
            ControlChanged();
        }

        private void radioButton_control_grat_CheckedChanged(object sender, EventArgs e)
        {
            ControlChanged();
        }

        private void radioButton_control_auto_CheckedChanged(object sender, EventArgs e)
        {
            ControlChanged();
        }

        private void radioButton_control_custom_CheckedChanged(object sender, EventArgs e)
        {
            ControlChanged();
        }


        void ControlChanged()
        {
            this.label_glr.Enabled = this.radioButton_control_auto.Checked;
            this.numericUpDown_glr.Enabled = this.radioButton_control_auto.Checked;
            this.textBox_control_custom_file.Enabled = this.radioButton_control_custom.Checked;
            this.button_control_custom_file.Enabled = this.radioButton_control_custom.Checked;
        }


        private void button_control_custom_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Wells Control List File (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_control_custom_file.Text = filename;
        }

        private void button_add_wbhph_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("WBHPH Table File (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_add_wbhph.Text = filename;
        }

        private void checkBox_use_wbhph_file_CheckedChanged(object sender, EventArgs e)
        {
            this.button_add_wbhph.Enabled = checkBox_use_wbhph_file.Checked;
            this.radioButton_wbhph_add.Enabled = checkBox_use_wbhph_file.Checked;
            this.radioButton_wbhph_replace.Enabled = checkBox_use_wbhph_file.Checked;
            this.textBox_add_wbhph.Enabled = checkBox_use_wbhph_file.Checked;
        }

        private void checkBox_aquifer_matching_CheckedChanged(object sender, EventArgs e)
        {
            AqMatchingInterface(checkBox_aquifer_matching.Checked);
        }

        private void checkBox_perm_matching_CheckedChanged(object sender, EventArgs e)
        {
            PermMatchingInterface(checkBox_perm_matching.Checked);
        }

        private void checkBox_relperm_matching_CheckedChanged(object sender, EventArgs e)
        {
            RelPermMatchingInterface(checkBox_relperm_matching.Checked);
        }


        void PermMatchingInterface(bool matching)
        {
            this.tabControl_perm_min_max.Enabled = matching;
            this.richTextBox_perm_intro.Enabled = matching;
            this.tableLayoutPanel_perm_file.Enabled = matching;
            this.richTextBox_perm_file_notes.Enabled = matching;
            this.groupBox_perm_inter_type.Enabled = matching;
            this.groupBox_perm_inter_reg.Enabled = matching;
        }

        void RelPermMatchingInterface(bool matching)
        {
            this.groupBox_relperm_tables.Enabled = matching;
            this.groupBox_satnum.Enabled = matching;
        }

        private void button_swof_sgof_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("TXT Files (*.{0})|*.{0}|All Files (*.*)|*.*", "TXT");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                this.textBox_swof_sgof_file.Text = filename;
            }
        }

        private void button_satnum_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_satnum_file.Text = filename;
        }

        private void textBox_swof_sgof_file_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_swof_sgof_file.Text))
            {
                this.textBox_corey_file.Text = string.Empty;
                this.textBox_corey_file.BackColor = Color.White;
            }
            else
            {
                this.textBox_corey_file.Text = Path.ChangeExtension(textBox_swof_sgof_file.Text, SCAL.CoreySetForm.EXT);
                bool corey_file_exists = File.Exists(this.textBox_corey_file.Text);
                this.textBox_corey_file.BackColor = corey_file_exists ? Color.GreenYellow : Color.OrangeRed;
                UpdateCoreyMinMaxTable();
            }
        }


        CoreySet coreySet = new CoreySet();
        void UpdateCoreyMinMaxTable()
        {
            bool corey_file_exists = File.Exists(this.textBox_corey_file.Text);
            this.listBox_corey_table.Items.Clear();
            if (corey_file_exists)
            {
                CoreySet.Load(this.textBox_corey_file.Text, out coreySet);
                for (int i = 0; i < coreySet.Tables.Count; ++i)
                    this.listBox_corey_table.Items.Add($"Table#{i + 1}");
                if (coreySet.Tables.Count > 0)
                    this.listBox_corey_table.SelectedIndex = 0;
            }
        }


        private void radioButton_use_single_satnum_CheckedChanged(object sender, EventArgs e)
        {
            UseSatnumFileChanged(false);
        }

        private void radioButton_use_satnum_array_CheckedChanged(object sender, EventArgs e)
        {
            UseSatnumFileChanged(true);
        }

        void UseSatnumFileChanged(bool use)
        {
            this.button_satnum_file.Enabled = use;
            this.textBox_satnum_title.Enabled = use;
            this.textBox_satnum_file.Enabled = use;
        }


        void KrMatchingInterface(bool matching)
        {
            this.groupBox_kr_inter_type.Enabled = matching;
            this.groupBox_kr_regions.Enabled = matching;
        }


        void KrorwMatchingInterface(bool matching)
        {
            this.tableLayoutPanel_krorw_array.Enabled = matching;
            this.richTextBox_krorw_notes.Enabled = matching;
            this.groupBox_krorw_min.Enabled = matching;
            this.groupBox_krorw_max.Enabled = matching;
        }

        void KrwrMatchingInterface(bool matching)
        {
            this.tableLayoutPanel_krwr_array.Enabled = matching;
            this.richTextBox_krwr_notes.Enabled = matching;
            this.groupBox_krwr_min.Enabled = matching;
            this.groupBox_krwr_max.Enabled = matching;
        }

        private void checkBox_krwr_matching_CheckedChanged(object sender, EventArgs e)
        {
            KrwrMatchingInterface(checkBox_krwr_matching.Checked);
            KrMatchingInterface(checkBox_krwr_matching.Checked || checkBox_krorw_matching.Checked);
        }

        private void checkBox_krorw_matching_CheckedChanged(object sender, EventArgs e)
        {
            KrorwMatchingInterface(checkBox_krorw_matching.Checked);
            KrMatchingInterface(checkBox_krwr_matching.Checked || checkBox_krorw_matching.Checked);
        }



        void KrorwMinChahged(bool usevalue)
        {
            this.richTextBox_krorw_min_notes.Enabled = !usevalue;
            this.numericUpDown_krorw_min_value.Enabled = usevalue;
            this.button_krorw_min_array_file.Enabled = !usevalue;
            this.textBox_krorw_min_array_file.Enabled = !usevalue;
            this.textBox_krorw_min_array_title.Enabled = !usevalue;
        }


        void KrorwMaxChahged(bool usevalue)
        {
            this.richTextBox_krorw_max_notes.Enabled = !usevalue;
            this.numericUpDown_krorw_max_value.Enabled = usevalue;
            this.button_krorw_max_array_file.Enabled = !usevalue;
            this.textBox_krorw_max_array_file.Enabled = !usevalue;
            this.textBox_krorw_max_array_title.Enabled = !usevalue;
        }



        void UseKrRegionsChanged(bool usevalue)
        {
            this.textBox_kr_regions_file.Enabled = !usevalue;
            this.button_kr_regions_file.Enabled = !usevalue;
            this.textBox_kr_regions_title.Enabled = !usevalue;
            this.richTextBox_kr_regions_notes.Enabled = !usevalue;
        }



        private void radioButton_krorw_min_value_CheckedChanged(object sender, EventArgs e)
        {
            KrorwMinChahged(radioButton_krorw_min_value.Checked);
        }

        private void radioButton_krorw_max_value_CheckedChanged(object sender, EventArgs e)
        {
            KrorwMaxChahged(radioButton_krorw_max_value.Checked);
        }

        private void radioButton_use_kr_single_region_CheckedChanged(object sender, EventArgs e)
        {
            UseKrRegionsChanged(radioButton_use_kr_single_region.Checked);
        }





        private void radioButton_krwr_min_value_CheckedChanged(object sender, EventArgs e)
        {
            KrwrMinChahged(radioButton_krwr_min_value.Checked);
        }

        private void radioButton_krwr_max_value_CheckedChanged(object sender, EventArgs e)
        {
            KrwrMaxChahged(radioButton_krwr_max_value.Checked);
        }


        void KrwrMinChahged(bool usevalue)
        {
            this.richTextBox_krwr_min_notes.Enabled = !usevalue;
            this.numericUpDown_krwr_min_value.Enabled = usevalue;
            this.button_krwr_min_array_file.Enabled = !usevalue;
            this.textBox_krwr_min_array_file.Enabled = !usevalue;
            this.textBox_krwr_min_array_title.Enabled = !usevalue;
        }


        void KrwrMaxChahged(bool usevalue)
        {
            this.richTextBox_krwr_max_notes.Enabled = !usevalue;
            this.numericUpDown_krwr_max_value.Enabled = usevalue;
            this.button_krwr_max_array_file.Enabled = !usevalue;
            this.textBox_krwr_max_array_file.Enabled = !usevalue;
            this.textBox_krwr_max_array_title.Enabled = !usevalue;
        }

        private void button_krorw_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_krorw_file.Text = filename;
        }

        private void button_krorw_min_array_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_krorw_min_array_file.Text = filename;
        }

        private void button_krorw_max_array_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_krorw_max_array_file.Text = filename;
        }

        private void button_kr_regions_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_kr_regions_file.Text = filename;
        }

        private void button_krwr_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_krwr_file.Text = filename;
        }

        private void button_krwr_min_array_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_krwr_min_array_file.Text = filename;
        }

        private void button_krwr_max_array_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_krwr_max_array_file.Text = filename;
        }

        private void radioButton_multpv_max_value_CheckedChanged(object sender, EventArgs e)
        {
            MultpvMaxChahged();
        }

        private void radioButton_multpv_max_array_CheckedChanged(object sender, EventArgs e)
        {
            MultpvMaxChahged();
        }



        void MultpvMinChahged()
        {
            bool usevalue = !MultpvMinFileUsed;
            this.richTextBox_multpv_min_notes.Enabled = !usevalue;
            this.numericUpDown_perm_min_value.Enabled = usevalue;
            this.button_multpv_min_file.Enabled = !usevalue;
            this.textBox_multpv_min_file.Enabled = !usevalue;
            this.textBox_multpv_min_title.Enabled = !usevalue;
        }


        void MultpvMaxChahged()
        {
            bool usevalue = !MultpvMaxFileUsed;
            this.richTextBox_multpv_max_notes.Enabled = !usevalue;
            this.numericUpDown_perm_max_value.Enabled = usevalue;
            this.button_multpv_max_file.Enabled = !usevalue;
            this.textBox_multpv_max_file.Enabled = !usevalue;
            this.textBox_multpv_max_title.Enabled = !usevalue;
        }

        private void radioButton_multpv_min_value_CheckedChanged(object sender, EventArgs e)
        {
            MultpvMinChahged();
        }

        private void radioButton_multpv_min_array_CheckedChanged(object sender, EventArgs e)
        {
            MultpvMinChahged();
        }

        private void button_multpv_min_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_multpv_min_file.Text = filename;
        }

        private void button_multpv_max_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_multpv_max_file.Text = filename;
        }


        int[] _prevIndices = Array.Empty<int>();

        static int[] ToArray(ListBox.SelectedIndexCollection collection)
        {
            int[] result = new int[collection.Count];
            for (int i = 0; i < collection.Count; i++) { result[i] = collection[i]; }
            return result;
        }

        private void listBox_corey_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_corey_table.SelectedIndices.Count > 0)
            {
                int i = listBox_corey_table.SelectedIndices[0];
                this.numericUpDown_now_min.Value = (decimal)coreySet.Tables[i].NOWmin;
                this.numericUpDown_nw_min.Value = (decimal)coreySet.Tables[i].NWmin;
                this.numericUpDown_now_max.Value = (decimal)coreySet.Tables[i].NOWmax;
                this.numericUpDown_nw_max.Value = (decimal)coreySet.Tables[i].NWmax;
            }
        }

        private void wizardPage4_corey_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            coreySet.Save(this.textBox_corey_file.Text);
        }

        private void numericUpDown_now_min_ValueChanged(object sender, EventArgs e)
        {
            foreach(int i in listBox_corey_table.SelectedIndices)
                coreySet.Tables[i].NOWmin = (double)this.numericUpDown_now_min.Value;
        }

        private void numericUpDown_nw_min_ValueChanged(object sender, EventArgs e)
        {
            foreach (int i in listBox_corey_table.SelectedIndices)
                coreySet.Tables[i].NWmin = (double)this.numericUpDown_nw_min.Value;
        }

        private void numericUpDown_now_max_ValueChanged(object sender, EventArgs e)
        {
            foreach (int i in listBox_corey_table.SelectedIndices)
                coreySet.Tables[i].NOWmax = (double)this.numericUpDown_now_max.Value;
        }

        private void numericUpDown_nw_max_ValueChanged(object sender, EventArgs e)
        {
            foreach (int i in listBox_corey_table.SelectedIndices)
                coreySet.Tables[i].NWmax = (double)this.numericUpDown_nw_max.Value;
        }

        private void WizardForm_Load_Data(object sender, EventArgs e)
        {
            wizardPageContainer.NextPage(this.wizardPage1_data, true);
        }

        private void WizardForm_Load_Run(object sender, EventArgs e)
        {
            wizardPageContainer.NextPage(this.wizardPage8_run, true);
        }

        private void wizardPage8_run_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            this.propertyGrid_input_data.SelectedObject = InputData;
        }

        private void checkBox_results_sub_folder_CheckedChanged(object sender, EventArgs e)
        {
            ChangeOutFolder();
        }

        private void checkBox_fdm_CheckedChanged(object sender, EventArgs e)
        {
            this.button_fdm.Enabled = this.checkBox_fdm.Checked;
            this.textBox_fdm.Enabled = this.checkBox_fdm.Checked;
        }

        private void button_fdm_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Flow Direction Model Files (*.{0})|*.{0}|All Files (*.*)|*.*", Pexel.HM.FR.FDModel.EXT);
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                this.textBox_fdm.Text = filename;
        }
    }
}
