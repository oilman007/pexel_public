using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ZedGraph;
using System.IO;
using MathNet.Numerics;
using Pexel.SCAL;
using Pexel.Eclipse;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Collections;
using Pexel.FileIO;
using System.Linq.Expressions;
using DynamicExpresso;
using Pexel.View;
using SharpGL.SceneGraph.Primitives;
using System.Diagnostics;

namespace Pexel.HM
{
    public partial class ResultsViewTreeForm : Form
    {
        public ResultsViewTreeForm()
        {
            InitializeComponent();
            Init();
        }


        ToolStripSeparator separatorUpFileMenu, separatorDownFileMenu;
        const int maxRecentFiles = 5;
        ToolStripMenuItem[] recentFilesFileMenu;


        void Init()
        {
            this.propView2DControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propView2DControl.Location = new System.Drawing.Point(0, 0);
            this.propView2DControl.Name = "PropView2DControl";
            this.propView2DControl.Size = new System.Drawing.Size(608, 463);
            this.propView2DControl.TabIndex = 0;
            this.tabPage_view2d.Controls.Add(this.propView2DControl);
            this.propView2DControl.MsgEvent += Msg;

            InitGraphs();
            resultsViewSettingsForm.ApplyEvent += ResultsViewSettingsForm_ApplyEvent;
            Downloader.MsgHandler += Msg;
            Downloader.TreeView = this.treeView_project;
            Downloader.Start();
            //auto_refrash(true);

            checkBox_log_scale_cp_CheckedChanged(null, null);
            checkBox_totals_cp_CheckedChanged(null, null);

            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            OriginalText = $"{LauncherForm.AppName()} History Matching";
            Text = OriginalText;

            //InitCrossSectNames();
            //IterChangedEvent += createDataTreeForm.IterChahged;
            //createDataTreeForm.IterChahged(-1, -1);

            toolStripButton_add_child.Enabled = false;
            toolStripButton_remove.Enabled = false;
            CreateMenuItems();

            //propView2DControl.SelectionChanged += propView2DControl_SelectionChanged;

            checkedListBox_uparams_SelectedIndexChanged(null, null);
        }


        void CreateMenuItems()
        {
            //
            separatorUpFileMenu = new ToolStripSeparator();
            separatorUpFileMenu.Name = "separatorUp_FileMenu";
            separatorUpFileMenu.Size = new Size(149, 6);
            recentFilesFileMenu = new ToolStripMenuItem[maxRecentFiles];
            for (int i = 0; i < maxRecentFiles; ++i)
            {
                recentFilesFileMenu[i] = new ToolStripMenuItem();
                recentFilesFileMenu[i].Size = new Size(152, 22);
                recentFilesFileMenu[i].Click += new System.EventHandler(recentFilesMenu_Click);
            }
            separatorDownFileMenu = new ToolStripSeparator();
            separatorDownFileMenu.Name = "separator2_FileMenu";
            separatorDownFileMenu.Size = new Size(149, 6);
            UpdateRecentFile();
            //
            exitMenu = new ToolStripMenuItem();
            exitMenu.Name = "Exit_FileMenuItem";
            exitMenu.Size = new Size(152, 22);
            exitMenu.Text = "E&xit";
            exitMenu.Click += new System.EventHandler(exit_Click);
            //
            // File Menu
            //
            fileToolStripMenuItem.DropDownItems.Add(separatorUpFileMenu);
            for (int i = 0; i < maxRecentFiles; ++i)
                fileToolStripMenuItem.DropDownItems.Add(recentFilesFileMenu[i]);
            fileToolStripMenuItem.DropDownItems.Add(separatorDownFileMenu);
            fileToolStripMenuItem.DropDownItems.Add(exitMenu);
        }






        HistMatchingProject CurrProject { set; get; } = new HistMatchingProject();

        string OriginalText = string.Empty;

        //string ProjectFile { set; get; } = string.Empty;


        const string settings_file_ext = ".pxlrv";

        PropView2DControl propView2DControl = new PropView2DControl();

        ResultsViewTreeDataDownloader Downloader = 
            new ResultsViewTreeDataDownloader(Helper.ShowInt(System.Diagnostics.Process.GetCurrentProcess().Id));
        //public Dictionary<string, ResultsViewDataOpt> Data { set; get; } = new Dictionary<string, ResultsViewDataOpt>();


        ResultsViewSettings Settings 
        { 
            set
            {
                this.resultsViewSettingsForm.Settings = value;
            }
            get
            {
                return this.resultsViewSettingsForm.Settings;
            }
        }

        string SettingsFile { set; get; } = string.Empty;

        string[] iter_names = Array.Empty<string>();
        string[] well_names = Array.Empty<string>();
        int[] leyers_selected = Array.Empty<int>();
        int prop_selected = 0;
        DateTime[] dates = Array.Empty<DateTime>();
        string[] satnum_names = Array.Empty<string>();

        int well_selected = -1;
        int dt_selected = -1;
        int satnum_selected = -1;

        ResultsViewSettingsForm resultsViewSettingsForm = new ResultsViewSettingsForm();

        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle_values = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle_wells = new System.Windows.Forms.DataGridViewCellStyle();



        //public delegate void IterChangedHandler(string pxlhm, string id, int iter);
        //public IterChangedHandler IterChangedEvent;



        /*
        const int cs_perm = 0;
        const int cs_permmult = 1;
        const int cs_satnum = 2;
        const int cs_krorw = 3;        
        const int cs_krorwmult = 4;
        const int cs_krwr = 5;
        const int cs_krwrmult = 6;
        const int cs_multpv = 7;
        */

        private ZedGraph.ZedGraphControl zedGraphControl_cs_perm = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_permmult = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_satnum = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_krorw = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_krorwmult = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_krwr = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_krwrmult = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_multpv = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_opt = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_wpt = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_lpt = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_wit = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_opr = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_wpr = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_lpr = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_wir = new ZedGraphControl();
        private ZedGraph.ZedGraphControl zedGraphControl_cs_wct = new ZedGraphControl();


        void InitGraphs()
        {
            //\\// parameters
            //
            zedGraphControl_gr_op.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_op.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_op.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_op.GraphPane.AddYAxis($"Oil prod rate");
            zedGraphControl_gr_op.GraphPane.AddYAxis($"Oil prod total");
            //zedGraphControl_gr_op.Font = new Font()
            //
            zedGraphControl_gr_lp.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_lp.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_lp.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_lp.GraphPane.AddYAxis($"Liquid prod rate");
            zedGraphControl_gr_lp.GraphPane.AddYAxis($"Liquid prod total");
            //
            zedGraphControl_gr_wp.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_wp.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_wp.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_wp.GraphPane.AddYAxis($"Water prod rate");
            zedGraphControl_gr_wp.GraphPane.AddYAxis($"Water prod total");
            //
            zedGraphControl_gr_wi.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_wi.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_wi.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_wi.GraphPane.AddYAxis($"Water inje rate");
            zedGraphControl_gr_wi.GraphPane.AddYAxis($"Water inje total");
            //
            zedGraphControl_gr_gp.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_gp.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_gp.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_gp.GraphPane.AddYAxis($"Gas prod rate");
            zedGraphControl_gr_gp.GraphPane.AddYAxis($"Gas prod total");
            //
            zedGraphControl_gr_gi.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_gi.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_gi.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_gi.GraphPane.AddYAxis($"Gas inje rate");
            zedGraphControl_gr_gi.GraphPane.AddYAxis($"Gas inje total");
            //
            zedGraphControl_gr_press.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_press.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_press.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_press.GraphPane.AddYAxis($"Press");
            //
            zedGraphControl_gr_wct_gor.GraphPane.Title.IsVisible = false;
            zedGraphControl_gr_wct_gor.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl_gr_wct_gor.GraphPane.YAxisList.Clear();
            zedGraphControl_gr_wct_gor.GraphPane.AddYAxis($"WCT");
            zedGraphControl_gr_wct_gor.GraphPane.AddYAxis($"GOR");
            //\\// cross-plots
            // totals
            zedGraphControl_wopt_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopt_cp.GraphPane.Title.Text = "Oil production total";
            zedGraphControl_wopt_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wopt_cp.GraphPane.YAxis.Title.Text = "Model";
            //
            zedGraphControl_wwpt_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpt_cp.GraphPane.Title.Text = "Water production total";
            zedGraphControl_wwpt_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wwpt_cp.GraphPane.YAxis.Title.Text = "Model";
            //
            zedGraphControl_wgpt_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpt_cp.GraphPane.Title.Text = "Gas production total";
            zedGraphControl_wgpt_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wgpt_cp.GraphPane.YAxis.Title.Text = "Model";
            //
            zedGraphControl_wlpt_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpt_cp.GraphPane.Title.Text = "Liquid production total";
            zedGraphControl_wlpt_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wlpt_cp.GraphPane.YAxis.Title.Text = "Model";
            //
            //zedGraphControl_woit_cp.GraphPane.Legend.IsVisible = false;
            //zedGraphControl_woit_cp.GraphPane.Title.Text = "Oil injection total";
            //zedGraphControl_woit_cp.GraphPane.XAxis.Title.Text = "Fact";
            //zedGraphControl_woit_cp.GraphPane.YAxis.Title.Text = "Model";
            //
            zedGraphControl_wwit_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwit_cp.GraphPane.Title.Text = "Water injection total";
            zedGraphControl_wwit_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wwit_cp.GraphPane.YAxis.Title.Text = "Model";
            //
            zedGraphControl_wgit_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgit_cp.GraphPane.Title.Text = "Gas injection total";
            zedGraphControl_wgit_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wgit_cp.GraphPane.YAxis.Title.Text = "Model";
            // rates
            zedGraphControl_wopr_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopr_cp.GraphPane.Title.Text = "Oil production rate";
            zedGraphControl_wopr_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wopr_cp.GraphPane.YAxis.Title.Text = "Model";
            //
            zedGraphControl_wwpr_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpr_cp.GraphPane.Title.Text = "Water production rate";
            zedGraphControl_wwpr_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wwpr_cp.GraphPane.YAxis.Title.Text = "Model";
            //                 
            zedGraphControl_wgpr_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpr_cp.GraphPane.Title.Text = "Gas production rate";
            zedGraphControl_wgpr_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wgpr_cp.GraphPane.YAxis.Title.Text = "Model";
            //                 
            zedGraphControl_wlpr_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpr_cp.GraphPane.Title.Text = "Liquid production rate";
            zedGraphControl_wlpr_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wlpr_cp.GraphPane.YAxis.Title.Text = "Model";
            //                 
            //zedGraphControl_woir_cp.GraphPane.Legend.IsVisible = false;
            //zedGraphControl_woir_cp.GraphPane.Title.Text = "Oil injection rate";
            //zedGraphControl_woir_cp.GraphPane.XAxis.Title.Text = "Fact";
            //zedGraphControl_woir_cp.GraphPane.YAxis.Title.Text = "Model";
            //                 
            zedGraphControl_wwir_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwir_cp.GraphPane.Title.Text = "Water injection rate";
            zedGraphControl_wwir_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wwir_cp.GraphPane.YAxis.Title.Text = "Model";
            //                 
            zedGraphControl_wgir_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgir_cp.GraphPane.Title.Text = "Gas injection rate";
            zedGraphControl_wgir_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wgir_cp.GraphPane.YAxis.Title.Text = "Model";
            // WBHP
            zedGraphControl_wbhp_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wbhp_cp.GraphPane.Title.Text = "Bottom hole pressure (all observed)";
            zedGraphControl_wbhp_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wbhp_cp.GraphPane.YAxis.Title.Text = "Model";
            // WBP9
            zedGraphControl_wbp9_cp.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wbp9_cp.GraphPane.Title.Text = "Reservoir pressure (all observed)";
            zedGraphControl_wbp9_cp.GraphPane.XAxis.Title.Text = "Fact";
            zedGraphControl_wbp9_cp.GraphPane.YAxis.Title.Text = "Model";
            //\\// deltas
            // totals
            zedGraphControl_wopt_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopt_delta.GraphPane.Title.Text = "Oil production total";
            zedGraphControl_wopt_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wopt_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwpt_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpt_delta.GraphPane.Title.Text = "Water production total";
            zedGraphControl_wwpt_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wwpt_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgpt_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpt_delta.GraphPane.Title.Text = "Gas production total";
            zedGraphControl_wgpt_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wgpt_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wlpt_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpt_delta.GraphPane.Title.Text = "Liquid production total";
            zedGraphControl_wlpt_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wlpt_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwit_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwit_delta.GraphPane.Title.Text = "Water injection total";
            zedGraphControl_wwit_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wwit_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgit_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgit_delta.GraphPane.Title.Text = "Gas injection total";
            zedGraphControl_wgit_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wgit_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            // rates
            zedGraphControl_wopr_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopr_delta.GraphPane.Title.Text = "Oil production rate";
            zedGraphControl_wopr_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wopr_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwpr_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpr_delta.GraphPane.Title.Text = "Water production rate";
            zedGraphControl_wwpr_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wwpr_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgpr_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpr_delta.GraphPane.Title.Text = "Gas production rate";
            zedGraphControl_wgpr_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wgpr_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wlpr_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpr_delta.GraphPane.Title.Text = "Liquid production rate";
            zedGraphControl_wlpr_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wlpr_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwir_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwir_delta.GraphPane.Title.Text = "Water injection rate";
            zedGraphControl_wwir_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wwir_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgir_delta.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgir_delta.GraphPane.Title.Text = "Gas injection rate";
            zedGraphControl_wgir_delta.GraphPane.XAxis.Title.Text = "Delta, %";
            zedGraphControl_wgir_delta.GraphPane.YAxis.Title.Text = "Iter";
            //
            //zedGraphControl_wbhp_dev.GraphPane.Legend.IsVisible = false;
            //zedGraphControl_wbhp_dev.GraphPane.Title.Text = "Well Bottome Hole Press";
            //zedGraphControl_wbhp_dev.GraphPane.XAxis.Title.Text = "R2, %";
            //zedGraphControl_wbhp_dev.GraphPane.YAxis.Title.Text = "Iter";
            // st dev
/*            zedGraphControl_wopt_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopt_stdev.GraphPane.Title.Text = "Oil production total";
            zedGraphControl_wopt_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wopt_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwpt_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpt_stdev.GraphPane.Title.Text = "Water production total";
            zedGraphControl_wwpt_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwpt_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgpt_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpt_stdev.GraphPane.Title.Text = "Gas production total";
            zedGraphControl_wgpt_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgpt_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wlpt_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpt_stdev.GraphPane.Title.Text = "Liquid production total";
            zedGraphControl_wlpt_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wlpt_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwit_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwit_stdev.GraphPane.Title.Text = "Water injection total";
            zedGraphControl_wwit_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwit_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgit_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgit_stdev.GraphPane.Title.Text = "Gas injection total";
            zedGraphControl_wgit_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgit_stdev.GraphPane.YAxis.Title.Text = "Iter";
            // rates
            zedGraphControl_wopr_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopr_stdev.GraphPane.Title.Text = "Oil production rate";
            zedGraphControl_wopr_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wopr_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwpr_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpr_stdev.GraphPane.Title.Text = "Water production rate";
            zedGraphControl_wwpr_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwpr_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgpr_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpr_stdev.GraphPane.Title.Text = "Gas production rate";
            zedGraphControl_wgpr_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgpr_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wlpr_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpr_stdev.GraphPane.Title.Text = "Liquid production rate";
            zedGraphControl_wlpr_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wlpr_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwir_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwir_stdev.GraphPane.Title.Text = "Water injection rate";
            zedGraphControl_wwir_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwir_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgir_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgir_stdev.GraphPane.Title.Text = "Gas injection rate";
            zedGraphControl_wgir_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgir_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wbhp_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wbhp_stdev.GraphPane.Title.Text = "Well Bottome Hole Press";
            zedGraphControl_wbhp_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wbhp_stdev.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wbp9_stdev.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wbp9_stdev.GraphPane.Title.Text = "Well Res Press";
            zedGraphControl_wbp9_stdev.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wbp9_stdev.GraphPane.YAxis.Title.Text = "Iter";*/
            // cross sect
            zedGraphControl_cs_perm.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_perm.GraphPane.Title.Text = "Permeability";
            zedGraphControl_cs_perm.GraphPane.XAxis.Title.Text = "mD";
            zedGraphControl_cs_perm.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_perm.GraphPane.XAxis.Type = AxisType.Log;
            zedGraphControl_cs_perm.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //
            zedGraphControl_cs_permmult.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_permmult.GraphPane.Title.Text = "Perm mult";
            zedGraphControl_cs_permmult.GraphPane.XAxis.Title.Text = "frac.";
            zedGraphControl_cs_permmult.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_permmult.GraphPane.XAxis.Type = AxisType.Log;
            zedGraphControl_cs_permmult.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //
            zedGraphControl_cs_satnum.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_satnum.GraphPane.Title.Text = "SATNUM";
            zedGraphControl_cs_satnum.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_satnum.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_satnum.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_satnum.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //
            zedGraphControl_cs_krorw.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_krorw.GraphPane.Title.Text = "Krorw";
            zedGraphControl_cs_krorw.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_krorw.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_krorw.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_krorw.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                               
            zedGraphControl_cs_krorwmult.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_krorwmult.GraphPane.Title.Text = "Krorw mult";
            zedGraphControl_cs_krorwmult.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_krorwmult.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_krorwmult.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_krorwmult.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                                         
            zedGraphControl_cs_krwr.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_krwr.GraphPane.Title.Text = "Krwr";
            zedGraphControl_cs_krwr.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_krwr.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_krwr.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_krwr.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                            
            zedGraphControl_cs_krwrmult.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_krwrmult.GraphPane.Title.Text = "Krwr mult";
            zedGraphControl_cs_krwrmult.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_krwrmult.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_krwrmult.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_krwrmult.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                                  
            zedGraphControl_cs_multpv.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_multpv.GraphPane.Title.Text = "MultPV";
            zedGraphControl_cs_multpv.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_multpv.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_multpv.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_multpv.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                           
            zedGraphControl_cs_opt.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_opt.GraphPane.Title.Text = "Oil Prod Total";
            zedGraphControl_cs_opt.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_opt.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_opt.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_opt.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                                              
            zedGraphControl_cs_wpt.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_wpt.GraphPane.Title.Text = "Water Prod Total";
            zedGraphControl_cs_wpt.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_wpt.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_wpt.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_wpt.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                                             
            zedGraphControl_cs_lpt.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_lpt.GraphPane.Title.Text = "Liquid Prod Total";
            zedGraphControl_cs_lpt.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_lpt.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_lpt.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_lpt.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                               
            zedGraphControl_cs_wit.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_wit.GraphPane.Title.Text = "Water Inje Total";
            zedGraphControl_cs_wit.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_wit.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_wit.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_wit.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                                              
            zedGraphControl_cs_opr.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_opr.GraphPane.Title.Text = "Oil Prod Rate";
            zedGraphControl_cs_opr.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_opr.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_opr.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_opr.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                              
            zedGraphControl_cs_wpr.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_wpr.GraphPane.Title.Text = "Water Prod Rate";
            zedGraphControl_cs_wpr.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_wpr.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_wpr.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_wpr.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                          
            zedGraphControl_cs_lpr.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_lpr.GraphPane.Title.Text = "Liquis Prod Rate";
            zedGraphControl_cs_lpr.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_lpr.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_lpr.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_lpr.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                  
            zedGraphControl_cs_wir.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_wir.GraphPane.Title.Text = "Water Inje Rate";
            zedGraphControl_cs_wir.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_wir.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_wir.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_wir.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                                     
            zedGraphControl_cs_wct.GraphPane.Legend.IsVisible = false;
            zedGraphControl_cs_wct.GraphPane.Title.Text = "Water Cut";
            zedGraphControl_cs_wct.GraphPane.XAxis.Title.Text = "";
            zedGraphControl_cs_wct.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl_cs_wct.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl_cs_wct.IsShowPointValues = true;
            //zedGraphControl_cs_perm.PointValueFormat = "0.000";
            //zedGraphControl_cs_perm.PointDateFormat = "d";
            //                                                                             
            // analyse
            //
            zedGraphControl_wopt_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopt_r2.GraphPane.Title.Text = "Oil production total";
            zedGraphControl_wopt_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wopt_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwpt_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpt_r2.GraphPane.Title.Text = "Water production total";
            zedGraphControl_wwpt_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwpt_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgpt_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpt_r2.GraphPane.Title.Text = "Gas production total";
            zedGraphControl_wgpt_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgpt_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wlpt_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpt_r2.GraphPane.Title.Text = "Liquid production total";
            zedGraphControl_wlpt_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wlpt_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwit_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwit_r2.GraphPane.Title.Text = "Water injection total";
            zedGraphControl_wwit_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwit_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgit_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgit_r2.GraphPane.Title.Text = "Gas injection total";
            zedGraphControl_wgit_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgit_r2.GraphPane.YAxis.Title.Text = "Iter"; 
            // rates
            zedGraphControl_wopr_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wopr_r2.GraphPane.Title.Text = "Oil production rate";
            zedGraphControl_wopr_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wopr_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwpr_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwpr_r2.GraphPane.Title.Text = "Water production rate";
            zedGraphControl_wwpr_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwpr_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgpr_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgpr_r2.GraphPane.Title.Text = "Gas production rate";
            zedGraphControl_wgpr_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgpr_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wlpr_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wlpr_r2.GraphPane.Title.Text = "Liquid production rate";
            zedGraphControl_wlpr_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wlpr_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wwir_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wwir_r2.GraphPane.Title.Text = "Water injection rate";
            zedGraphControl_wwir_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wwir_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wgir_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wgir_r2.GraphPane.Title.Text = "Gas injection rate";
            zedGraphControl_wgir_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wgir_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wbhp_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wbhp_r2.GraphPane.Title.Text = "Well Bottome Hole Press";
            zedGraphControl_wbhp_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wbhp_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            zedGraphControl_wbp9_r2.GraphPane.Legend.IsVisible = false;
            zedGraphControl_wbp9_r2.GraphPane.Title.Text = "Well Res Press";
            zedGraphControl_wbp9_r2.GraphPane.XAxis.Title.Text = "R2";
            zedGraphControl_wbp9_r2.GraphPane.YAxis.Title.Text = "Iter";
            //
            //zedGraphControl_lpt_vs_wct.GraphPane.Legend.IsVisible = false;
            zedGraphControl_lpt_vs_wct.GraphPane.Title.IsVisible = false;
            zedGraphControl_lpt_vs_wct.GraphPane.XAxis.Title.Text = "LPT_scaled";
            zedGraphControl_lpt_vs_wct.GraphPane.YAxis.Title.Text = "WCT";
            zedGraphControl_lpt_vs_wct.GraphPane.XAxis.Scale.Min = RelPermAnalyzerResults.Settings.XMin;
            zedGraphControl_lpt_vs_wct.GraphPane.XAxis.Scale.Max = RelPermAnalyzerResults.Settings.XMax;
            zedGraphControl_lpt_vs_wct.GraphPane.YAxis.Scale.Min = RelPermAnalyzerResults.Settings.YMin;
            zedGraphControl_lpt_vs_wct.GraphPane.YAxis.Scale.Max = RelPermAnalyzerResults.Settings.YMax;
            //
            //zedGraphControl_distancies.GraphPane.Legend.IsVisible = false;
            zedGraphControl_distancies.GraphPane.Title.IsVisible = false;
            zedGraphControl_distancies.GraphPane.XAxis.Title.Text = "Distancies, frac.";
            zedGraphControl_distancies.GraphPane.YAxis.Title.Text = "Point";
            ////////
            dataGridViewCellStyle_wells.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle_wells.Format = $"F0";
            dataGridViewCellStyle_wells.NullValue = null;
            //
            dataGridViewCellStyle_values.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle_values.Format = $"F{Settings.DecimalPlaces}";
            dataGridViewCellStyle_values.NullValue = null;
            //Column_totals_wellname.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wopth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wopt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wopt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wlpth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wlpt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wlpt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwpth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwpt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwpt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwith.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwit.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwit_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgpth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgpt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgpt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgith.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgit.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgit_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_woprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wopr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wopr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wlprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wlpr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wlpr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwpr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwpr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwirh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwir.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wwir_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgpr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgpr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgirh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgir.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_totals_wgir_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            //Column_wells_wellname.DefaultCellStyle = dataGridViewCellStyle;
            Column_wells_wopth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wopt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wopt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wlpth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wlpt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wlpt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwpth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwpt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwpt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwith.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwit.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwit_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgpth.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgpt.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgpt_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgith.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgit.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgit_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_woprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wopr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wopr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wlprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wlpr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wlpr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwirh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwir.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwir_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwpr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wwpr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgprh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgpr.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgpr_delta.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgirh.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgir.DefaultCellStyle = dataGridViewCellStyle_values;
            Column_wells_wgir_delta.DefaultCellStyle = dataGridViewCellStyle_values;

            Column_totals_wopth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wopt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wopt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wlpth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wlpt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wlpt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwpth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwpt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwpt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwith.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwit_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgpth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgpt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgpt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgith.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgit_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_woprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wopr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wopr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wlprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wlpr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wlpr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwpr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwpr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwirh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwir.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wwir_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgpr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgpr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgirh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgir.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_totals_wgir_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //Column_wells_wellname.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wopth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wopt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wopt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wlpth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wlpt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wlpt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwpth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwpt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwpt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwith.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwit_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgpth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgpt.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgpt_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgith.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgit_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_woprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wopr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wopr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wlprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wlpr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wlpr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwirh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwir.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwir_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwpr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wwpr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgprh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgpr.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgpr_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgirh.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgir.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column_wells_wgir_delta.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }



        static PointPairList PointPairList(DateTime[] dates, RSM.RsmVector vector)
        {
            PointPairList result = new PointPairList();
            if (dates.Length == vector.Values.Length)
                for (int i = 0; i < dates.Length; ++i)
                    result.Add(new XDate(dates[i]), vector.Values[i]);
            return result;
        }




        static void RedrawGrGraph(  ZedGraphControl zedGraphControl,
                                    DateTime[] dates,
                                    RSM.RsmVector rh, RSM.RsmVector r,
                                    RSM.RsmVector th, RSM.RsmVector t,
                                    string rh_title, string r_title,
                                    string th_title, string t_title,
                                    Color r_color, Color t_color )
        {
            GraphPane graphPane = zedGraphControl.GraphPane;
            graphPane.CurveList.Clear();
            PointPairList rhPointList = PointPairList(dates, rh);
            PointPairList rPointList = PointPairList(dates, r);
            PointPairList thPointList = PointPairList(dates, th);
            PointPairList tPointList = PointPairList(dates, t);

            LineItem rh_curve = graphPane.AddCurve(rh_title, rhPointList, r_color, SymbolType.Circle);
            LineItem r_curve = graphPane.AddCurve(r_title, rPointList, r_color, SymbolType.None);
            rh_curve.Line.IsVisible = false;
            rh_curve.YAxisIndex = 0;
            r_curve.YAxisIndex = 0;

            LineItem th_curve = graphPane.AddCurve(th_title, thPointList, t_color, SymbolType.Circle);
            LineItem t_curve = graphPane.AddCurve(t_title, tPointList, t_color, SymbolType.None);
            th_curve.Line.IsVisible = false;
            th_curve.YAxisIndex = 1;
            t_curve.YAxisIndex = 1;

            graphPane.XAxis.Type = AxisType.Date;
            zedGraphControl.GraphPane.AxisChange();
            zedGraphControl.Invalidate();
        }





        void RedrawRatesGraph( 
                              DateTime[] dates,
                              RSM.RsmVector woprh, RSM.RsmVector wopr,
                              RSM.RsmVector wwprh, RSM.RsmVector wwpr,
                              RSM.RsmVector wgprh, RSM.RsmVector wgpr,
                              RSM.RsmVector wlprh, RSM.RsmVector wlpr,
                              RSM.RsmVector woirh, RSM.RsmVector woir,
                              RSM.RsmVector wwirh, RSM.RsmVector wwir,
                              RSM.RsmVector wgirh, RSM.RsmVector wgir
                              )
        {
            GraphPane graphPane = zedGraphControl_gr_wp.GraphPane;
            graphPane.CurveList.Clear();
            PointPairList woprhPointList = PointPairList(dates, woprh);
            PointPairList woprPointList = PointPairList(dates, wopr);
            PointPairList wwprhPointList = PointPairList(dates, wwprh);
            PointPairList wwprPointList = PointPairList(dates, wwpr);
            PointPairList wgprhPointList = PointPairList(dates, wgprh);
            PointPairList wgprPointList = PointPairList(dates, wgpr);
            PointPairList wlprhPointList = PointPairList(dates, wlprh);
            PointPairList wlprPointList = PointPairList(dates, wlpr);
            PointPairList woirhPointList = PointPairList(dates, woirh);
            PointPairList woirPointList = PointPairList(dates, woir);
            PointPairList wwirhPointList = PointPairList(dates, wwirh);
            PointPairList wwirPointList = PointPairList(dates, wwir);
            PointPairList wgirhPointList = PointPairList(dates, wgirh);
            PointPairList wgirPointList = PointPairList(dates, wgir);

            AddCurves(graphPane, woprhPointList, woprPointList, "WOPRH", "WOPR", Settings.OPRColor);
            AddCurves(graphPane, wwprhPointList, wwprPointList, "WWPRH", "WWPR", Settings.WPRColor);
            AddCurves(graphPane, wgprhPointList, wgprPointList, "WGPRH", "WGPR", Settings.GPRColor, 1);
            AddCurves(graphPane, wlprhPointList, wlprPointList, "WLPRH", "WLPR", Settings.LPRColor);

            AddCurves(graphPane, woirhPointList, woirPointList, "WOIRH", "WOIR", Settings.OIRColor);
            AddCurves(graphPane, wwirhPointList, wwirPointList, "WWIRH", "WWIR", Settings.WIRColor);
            AddCurves(graphPane, wgirhPointList, wgirPointList, "WGIRH", "WGIR", Settings.GIRColor, 1);

            graphPane.XAxis.Type = AxisType.Date;
            zedGraphControl_gr_wp.GraphPane.AxisChange();
            zedGraphControl_gr_wp.Invalidate();
        }


        void AddCurves( GraphPane graphPane,
                        PointPairList hist, PointPairList model,
                        string hist_title, string model_title, Color color, int axis=0)
        {
            LineItem hist_curve = graphPane.AddCurve(hist_title, hist, color, SymbolType.Circle);
            LineItem model_curve = graphPane.AddCurve(model_title, model, color, SymbolType.None);
            hist_curve.Line.IsVisible = false;
            hist_curve.YAxisIndex = axis;
            model_curve.YAxisIndex = axis;
        }




        void RedrawTotalsGraph(
                              DateTime[] dates,
                              RSM.RsmVector woprh, RSM.RsmVector wopr,
                              RSM.RsmVector wwprh, RSM.RsmVector wwpr,
                              RSM.RsmVector wgprh, RSM.RsmVector wgpr,
                              RSM.RsmVector wlprh, RSM.RsmVector wlpr,
                              RSM.RsmVector woirh, RSM.RsmVector woir,
                              RSM.RsmVector wwirh, RSM.RsmVector wwir,
                              RSM.RsmVector wgirh, RSM.RsmVector wgir
                              )
        {
            GraphPane graphPane = this.zedGraphControl_gr_op.GraphPane;
            graphPane.CurveList.Clear();
            PointPairList woprhPointList = PointPairList(dates, woprh);
            PointPairList woprPointList = PointPairList(dates, wopr);
            PointPairList wwprhPointList = PointPairList(dates, wwprh);
            PointPairList wwprPointList = PointPairList(dates, wwpr);
            PointPairList wgprhPointList = PointPairList(dates, wgprh);
            PointPairList wgprPointList = PointPairList(dates, wgpr);
            PointPairList wlprhPointList = PointPairList(dates, wlprh);
            PointPairList wlprPointList = PointPairList(dates, wlpr);
            PointPairList woirhPointList = PointPairList(dates, woirh);
            PointPairList woirPointList = PointPairList(dates, woir);
            PointPairList wwirhPointList = PointPairList(dates, wwirh);
            PointPairList wwirPointList = PointPairList(dates, wwir);
            PointPairList wgirhPointList = PointPairList(dates, wgirh);
            PointPairList wgirPointList = PointPairList(dates, wgir);

            AddCurves(graphPane, woprhPointList, woprPointList, "WOPTH", "WOPT", Settings.OPTColor);
            AddCurves(graphPane, wwprhPointList, wwprPointList, "WWPTH", "WWPT", Settings.WPTColor);
            AddCurves(graphPane, wgprhPointList, wgprPointList, "WGPTH", "WGPT", Settings.GPTColor, 1);
            AddCurves(graphPane, wlprhPointList, wlprPointList, "WLPTH", "WLPT", Settings.LPTColor);

            AddCurves(graphPane, woirhPointList, woirPointList, "WOITH", "WOIT", Settings.OITColor);
            AddCurves(graphPane, wwirhPointList, wwirPointList, "WWITH", "WWIT", Settings.WITColor);
            AddCurves(graphPane, wgirhPointList, wgirPointList, "WGITH", "WGIT", Settings.GITColor, 1);

            graphPane.XAxis.Type = AxisType.Date;
            zedGraphControl_gr_op.GraphPane.AxisChange();
            zedGraphControl_gr_op.Invalidate();
        }



        void RedrawPressGraph(DateTime[] dates,
                              RSM.RsmVector wbhph, RSM.RsmVector wbhp,
                              RSM.RsmVector wbp9h, RSM.RsmVector wbp9)
        {
            GraphPane graphPane = zedGraphControl_gr_press.GraphPane;
            graphPane.CurveList.Clear();
            PointPairList wbhphPointList = PointPairList(dates, wbhph);
            PointPairList wbhpPointList = PointPairList(dates, wbhp);
            PointPairList wbp9hPointList = PointPairList(dates, wbp9h);
            PointPairList wbp9PointList = PointPairList(dates, wbp9);
            LineItem wbhph_curve = graphPane.AddCurve("WBHPH", wbhphPointList, Settings.WBHPColor, SymbolType.Circle);
            LineItem wbhp_curve = graphPane.AddCurve("WBHP", wbhpPointList, Settings.WBHPColor, SymbolType.None);
            wbhph_curve.Line.IsVisible = false;
            wbhph_curve.YAxisIndex = 0;
            wbhp_curve.YAxisIndex = 0;
            LineItem wbp9h_curve = graphPane.AddCurve("WBP9H", wbp9hPointList, Settings.WBP9Color, SymbolType.Circle);
            LineItem wbp9_curve = graphPane.AddCurve("WBP9", wbp9PointList, Settings.WBP9Color, SymbolType.None);
            wbp9h_curve.Line.IsVisible = false;
            wbp9h_curve.YAxisIndex = 0;
            wbp9_curve.YAxisIndex = 0;
            graphPane.XAxis.Type = AxisType.Date;
            zedGraphControl_gr_press.GraphPane.AxisChange();
            zedGraphControl_gr_press.Invalidate();
        }





        void UpdateCrossPlotTag(ref ZedGraphControl graphControl)
        {
            CrossPlotTag crossPlotTag = (CrossPlotTag)graphControl.Tag;
            GraphPane graphPane = graphControl.GraphPane;

            double max = Math.Max(graphPane.XAxis.Scale.Max, graphPane.YAxis.Scale.Max);
            double min = Math.Min(graphPane.XAxis.Scale.Max, graphPane.YAxis.Scale.Max);

            crossPlotTag.MidLine = new LineItem("", new double[] { 0, max }, new double[] { 0, max }, Color.Green, SymbolType.None);
            double y = max * (1 - crossPlotTag.Delta / 100);
            crossPlotTag.MinLine = new LineItem("", new double[] { 0, max }, new double[] { 0, y }, Color.Red, SymbolType.None);
            double x = max / (1 + crossPlotTag.Delta / 100);
            crossPlotTag.MaxLine = new LineItem("", new double[] { 0, x }, new double[] { 0, max }, Color.Red, SymbolType.None);

            crossPlotTag.MinLineText = new TextObj($"-{crossPlotTag.Delta}%", max, y, CoordType.AxisXYScale, AlignH.Right, AlignV.Bottom);
            crossPlotTag.MinLineText.FontSpec.FontColor = Color.Red;
            crossPlotTag.MinLineText.ZOrder = ZOrder.A_InFront;
            crossPlotTag.MinLineText.FontSpec.Border.IsVisible = false;
            crossPlotTag.MinLineText.FontSpec.Fill.IsVisible = false;
            crossPlotTag.MinLineText.FontSpec.Size = Settings.CrossPlotFontSpec;
            crossPlotTag.MinLineText.FontSpec.Angle = 0;

            crossPlotTag.MaxLineText = new TextObj($"+{crossPlotTag.Delta}%", x, max, CoordType.AxisXYScale, AlignH.Left, AlignV.Top);
            crossPlotTag.MaxLineText.FontSpec.FontColor = Color.Red;
            crossPlotTag.MaxLineText.ZOrder = ZOrder.A_InFront;
            crossPlotTag.MaxLineText.FontSpec.Border.IsVisible = false;
            crossPlotTag.MaxLineText.FontSpec.Fill.IsVisible = false;
            crossPlotTag.MaxLineText.FontSpec.Size = Settings.CrossPlotFontSpec;
            crossPlotTag.MaxLineText.FontSpec.Angle = 0;

            crossPlotTag.MidLine.Symbol.IsVisible = false;
            crossPlotTag.MinLine.Symbol.IsVisible = false;
            crossPlotTag.MaxLine.Symbol.IsVisible = false;

            graphControl.Tag = crossPlotTag;
        }



        void RedrawCrossPlot2(ZedGraphControl graphControl, PointPairList points, List<string> wellnames, double delta = 10)
        {
            GraphPane graphPane = graphControl.GraphPane;
            graphPane.CurveList.Clear();
            graphPane.GraphObjList.Clear();

            PointPairList green_points = new PointPairList(), red_points = new PointPairList();
            foreach (PointPair point in points)
            {
                if (point.X != 0 && Math.Abs(point.Y - point.X) / point.X > delta / 100)
                    red_points.Add(point);
                else
                    green_points.Add(point);
            }

            LineItem green_curve = graphPane.AddCurve("", green_points, Color.Green, SymbolType.Circle);
            LineItem red_curve = graphPane.AddCurve("", red_points, Color.Red, SymbolType.Circle);
            green_curve.Line.IsVisible = false;
            red_curve.Line.IsVisible = false;
            graphControl.GraphPane.AxisChange();

            double max = Math.Max(graphPane.XAxis.Scale.Max, graphPane.YAxis.Scale.Max);
            graphPane.XAxis.Scale.Max = max;
            graphPane.YAxis.Scale.Max = max;

            CrossPlotTag crossPlotTag = new CrossPlotTag() { Delta = delta };
            graphPane.CurveList.Add(crossPlotTag.MidLine);
            graphPane.CurveList.Add(crossPlotTag.MinLine);
            graphPane.CurveList.Add(crossPlotTag.MaxLine);
            graphPane.GraphObjList.Add(crossPlotTag.MinLineText);
            graphPane.GraphObjList.Add(crossPlotTag.MaxLineText);

            graphControl.Tag = crossPlotTag;
            UpdateCrossPlotTag(ref graphControl);

            if (Settings.ShowWellNamesCP)
            {
                double interval = Math.Abs(max - 0) / 100;
                //double interval = 1;
                for (int i = 0; i < points.Count; ++i)
                {
                    PointPair pt = points[i];
                    TextObj text = new TextObj(wellnames[i], pt.X + interval, pt.Y + interval,
                                               CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                    text.FontSpec.FontColor = Color.Black;
                    text.ZOrder = ZOrder.A_InFront;
                    text.FontSpec.Border.IsVisible = false;
                    text.FontSpec.Fill.IsVisible = false;
                    text.FontSpec.Size = Settings.CrossPlotFontSpec;
                    text.FontSpec.Angle = 0;
                    //string lblString = "name";
                    //Link lblLink = new Link(lblString, "#", "");
                    //text.Link = lblLink;
                    graphPane.GraphObjList.Add(text);
                }
            }

            graphControl.GraphPane.AxisChange();
            graphControl.Invalidate();
        }





        /*
        static double Variance(ZedGraph.PointPairList pairs)
        {
            double result = 0;
            if (pairs is null)
                return result;
            List<double> values = new List<double>();
            for (int i = 0; i < pairs.Count; ++i)
                if (pairs[i].X != 0)
                    values.Add(pairs[i].Y - pairs[i].X);
            if (values.Count == 0) 
                return result;
            double average = values.Average();
            foreach (double value in values)
                result += Math.Pow(value - average, 2);
            result /= values.Count;
            return result;
        }
        static double StDev(ZedGraph.PointPairList pairs)
        {
            return Math.Sqrt(Variance(pairs));
        }
        */


        static double R2(double[] fact, double[] model)
        {
            List<double> model_list = new List<double>();
            List<double> fact_list = new List<double>();
            for (int i = 0; i < fact.Length; ++i)
                if (fact[i] != 0)
                {
                    model_list.Add(model[i]);
                    fact_list.Add(fact[i]);
                }
            double R2 = GoodnessOfFit.RSquared(model_list, fact_list);
            return Math.Round(R2, 3);
        }

        /*
        static double VariancePercent(ZedGraph.PointPairList pairs)
        {
            double result = 0;
            if (pairs is null)
                return result;
            List<double> values = new List<double>();
            for (int i = 0; i < pairs.Count; ++i)
                if (pairs[i].X != 0)
                    values.Add((pairs[i].Y - pairs[i].X) / pairs[i].X * 100);
            if (values.Count == 0)
                return result;
            foreach (double value in values)
                result += Math.Pow(value, 2);
            result /= values.Count;
            return result;
        }

        static double StDevPercent(ZedGraph.PointPairList pairs)
        {
            return Math.Sqrt(VariancePercent(pairs));
        }
        */




        void RedrawCrossPlot(ZedGraphControl graphControl, PointPairList points, List<string> wellnames, double delta)
        {
            GraphPane graphPane = graphControl.GraphPane;
            graphPane.CurveList.Clear();
            graphPane.GraphObjList.Clear();

            const double min = 0;
            double max = 0;
            PointPairList green_points = new PointPairList(), red_points = new PointPairList();
            foreach (PointPair point in points)
            {
                if (max < point.X) max = point.X;
                if (max < point.Y) max = point.Y;

                if (point.X != 0 && Math.Abs(point.Y - point.X) / point.X > delta / 100)
                    red_points.Add(point);
                else
                    green_points.Add(point);
            }
            max = Math.Max(1, max * 1.1);

            LineItem green_curve = graphPane.AddCurve("", green_points, Color.Green, SymbolType.Circle);
            LineItem red_curve = graphPane.AddCurve("", red_points, Color.Red, SymbolType.Circle);
            green_curve.Line.IsVisible = false;
            red_curve.Line.IsVisible = false;

            graphControl.GraphPane.AxisChange();

            //double max = Math.Max(graphPane.XAxis.Scale.Max, graphPane.YAxis.Scale.Max);
            graphPane.XAxis.Scale.Max = max;
            graphPane.YAxis.Scale.Max = max;

            LineItem mid_line = graphPane.AddCurve("", new double[] { min, max }, new double[] { min, max }, Color.Green);
            double y = max * (1 - delta / 100);
            LineItem min_line = graphPane.AddCurve("", new double[] { min, max }, new double[] { min, y }, Color.Red);
            double x = max / (1 + delta / 100);
            LineItem max_line = graphPane.AddCurve("", new double[] { min, x }, new double[] { min, max }, Color.Red);

            const float dev_font_spec = 12;
            TextObj min_dev_text = new TextObj($"-{delta}%", max, y, CoordType.AxisXYScale, AlignH.Right, AlignV.Bottom);
            min_dev_text.FontSpec.FontColor = Color.Red;
            min_dev_text.ZOrder = ZOrder.A_InFront;
            min_dev_text.FontSpec.Border.IsVisible = false;
            min_dev_text.FontSpec.Fill.IsVisible = false;
            min_dev_text.FontSpec.Size = dev_font_spec;
            min_dev_text.FontSpec.Angle = 0;
            graphPane.GraphObjList.Add(min_dev_text);
            TextObj max_dev_text = new TextObj($"+{delta}%", x, max, CoordType.AxisXYScale, AlignH.Left, AlignV.Top);
            max_dev_text.FontSpec.FontColor = Color.Red;
            max_dev_text.ZOrder = ZOrder.A_InFront;
            max_dev_text.FontSpec.Border.IsVisible = false;
            max_dev_text.FontSpec.Fill.IsVisible = false;
            max_dev_text.FontSpec.Size = dev_font_spec;
            max_dev_text.FontSpec.Angle = 0;
            graphPane.GraphObjList.Add(max_dev_text);

            mid_line.Symbol.IsVisible = false;
            min_line.Symbol.IsVisible = false;
            max_line.Symbol.IsVisible = false;
            //myLine.GetRange(out other, out other, out minY, out maxY, false, false, myPane);

            if (Settings.ShowWellNamesCP)
            {
                double interval = Math.Abs(max - min) / 100;
                //double interval = 1;
                for (int i = 0; i < points.Count; ++i)
                {
                    PointPair pt = points[i];
                    TextObj text = new TextObj(wellnames[i], pt.X + interval, pt.Y + interval,
                                               CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                    text.FontSpec.FontColor = Color.Black;
                    text.ZOrder = ZOrder.A_InFront;
                    text.FontSpec.Border.IsVisible = false;
                    text.FontSpec.Fill.IsVisible = false;
                    text.FontSpec.Size = Settings.CrossPlotFontSpec;
                    text.FontSpec.Angle = 0;
                    //string lblString = "name";
                    //Link lblLink = new Link(lblString, "#", "");
                    //text.Link = lblLink;
                    graphPane.GraphObjList.Add(text);
                }
            }

            // st dev
            double stdev = R2(points.Select(v => v.X).ToArray(), points.Select(v => v.Y).ToArray());
            TextObj st_dev_text = new TextObj($"R2 = {Helper.ShowDouble(stdev)}", 0.05, 0.05, CoordType.ChartFraction, AlignH.Left, AlignV.Center);
            st_dev_text.FontSpec.FontColor = Color.Black;
            st_dev_text.ZOrder = ZOrder.A_InFront;
            st_dev_text.FontSpec.Border.IsVisible = false;
            st_dev_text.FontSpec.Fill.IsVisible = false;
            st_dev_text.FontSpec.Size = Settings.CrossPlotFontSpec * 1.5f;
            st_dev_text.FontSpec.Angle = 0;
            //string lblString = "name";
            //Link lblLink = new Link(lblString, "#", "");
            //text.Link = lblLink;
            graphPane.GraphObjList.Add(st_dev_text);

            graphControl.GraphPane.AxisChange();
            graphControl.Invalidate();
        }


        static void RedrawHMBar(ZedGraphControl graphControl, string[] names, params (string, double[], Color)[] values)
        {
            GraphPane graphPane = graphControl.GraphPane;
            graphPane.CurveList.Clear();
            foreach (var val in values)
                graphPane.AddBar(val.Item1, val.Item2, null, val.Item3);
            graphPane.YAxis.MajorTic.IsBetweenLabels = false;
            graphPane.YAxis.Scale.TextLabels = names; //  HistMatching.GetIterNames(iter_names)
            graphPane.YAxis.Type = AxisType.Text;
            graphPane.YAxis.Scale.IsReverse = true;
            graphPane.BarSettings.Base = BarBase.Y;
            graphPane.BarSettings.Type = BarType.SortedOverlay;
            //graphPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            graphControl.GraphPane.AxisChange();
            graphControl.Invalidate();
            //graphControl.Refresh();
        }




        void RedrawPermBar2(ZedGraphControl graphControl, string[] names, double[] min, double[] values, double[] max, Color color)
        {
            GraphPane graphPane = graphControl.GraphPane;
            graphPane.CurveList.Clear();
            graphPane.GraphObjList.Clear();

            BarItem myBar_min = graphPane.AddBar("", min, null, color);
            BarItem myBar_val = graphPane.AddBar("", values, null, color);
            BarItem myBar_max = graphPane.AddBar("", max, null, color);
            
            for (int i = 0; i < names.Length; ++i)
            {
                //myBar_min.AddPoint(min[i], i);
                TextObj text_min = new TextObj("min", min[i], i + 1,
                                            CoordType.AxisXYScale, AlignH.Left, AlignV.Top);
                text_min.FontSpec.FontColor = Color.Black;
                text_min.ZOrder = ZOrder.A_InFront;
                text_min.FontSpec.Border.IsVisible = false;
                text_min.FontSpec.Fill.IsVisible = false;
                text_min.FontSpec.Size = 12;
                text_min.FontSpec.Angle = 0;
                graphPane.GraphObjList.Add(text_min);
                //
                //myBar_val.AddPoint(values[i], i);
                TextObj text_val = new TextObj("value", values[i], i + 1,
                                            CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                text_val.FontSpec.FontColor = Color.Black;
                text_val.ZOrder = ZOrder.A_InFront;
                text_val.FontSpec.Border.IsVisible = false;
                text_val.FontSpec.Fill.IsVisible = false;
                text_val.FontSpec.Size = 12;
                text_val.FontSpec.Angle = 0;
                graphPane.GraphObjList.Add(text_val);
                //
                //myBar_max.AddPoint(max[i], i);
                TextObj text_max = new TextObj("max", max[i], i + 1,
                            CoordType.AxisXYScale, AlignH.Left, AlignV.Bottom);
                text_max.FontSpec.FontColor = Color.Black;
                text_max.ZOrder = ZOrder.A_InFront;
                text_max.FontSpec.Border.IsVisible = false;
                text_max.FontSpec.Fill.IsVisible = false;
                text_max.FontSpec.Size = 12;
                text_max.FontSpec.Angle = 0;
                graphPane.GraphObjList.Add(text_max);
            }
            
            graphPane.YAxis.MajorTic.IsBetweenLabels = false;
            graphPane.YAxis.Scale.TextLabels = names; //  HistMatching.GetIterNames(iter_names)
            graphPane.YAxis.Type = AxisType.Text;
            graphPane.YAxis.Scale.IsReverse = true;
            graphPane.BarSettings.Base = BarBase.Y;
            graphPane.BarSettings.Type = BarType.SortedOverlay;
            //graphPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            graphControl.GraphPane.AxisChange();
            graphControl.Invalidate();
            //graphControl.Refresh();
        }





        static void RedrawPermBar(ZedGraphControl graphControl, string[] names, double[] min, double[] values, double[] max, Color color, bool show_min_max)
        {
            GraphPane graphPane = graphControl.GraphPane;
            graphPane.CurveList.Clear();            
            graphPane.GraphObjList.Clear();

            BarItem myBar_val = graphPane.AddBar("", values, null, color);

            if (show_min_max)
            {
                LineItem min_line = graphPane.AddCurve("", min, null, Color.Blue, SymbolType.None);
                LineItem max_line = graphPane.AddCurve("", max, null, Color.Red, SymbolType.None);
                //min_line.Symbol.IsVisible = false;
                //max_line.Symbol.IsVisible = false;

                /*
                for (int i = 0; i < names.Length; ++i)
                {
                    //myBar_min.AddPoint(min[i], i);
                    TextObj text_min = new TextObj("min", min[i], i + 1,
                                                CoordType.AxisXYScale, AlignH.Left, AlignV.Top);
                    text_min.FontSpec.FontColor = Color.Black;
                    text_min.ZOrder = ZOrder.A_InFront;
                    text_min.FontSpec.Border.IsVisible = false;
                    text_min.FontSpec.Fill.IsVisible = false;
                    text_min.FontSpec.Size = 12;
                    text_min.FontSpec.Angle = 0;
                    graphPane.GraphObjList.Add(text_min);
                    //
                    //myBar_val.AddPoint(values[i], i);
                    TextObj text_val = new TextObj("value", values[i], i + 1,
                                                CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                    text_val.FontSpec.FontColor = Color.Black;
                    text_val.ZOrder = ZOrder.A_InFront;
                    text_val.FontSpec.Border.IsVisible = false;
                    text_val.FontSpec.Fill.IsVisible = false;
                    text_val.FontSpec.Size = 12;
                    text_val.FontSpec.Angle = 0;
                    graphPane.GraphObjList.Add(text_val);
                    //
                    //myBar_max.AddPoint(max[i], i);
                    TextObj text_max = new TextObj("max", max[i], i + 1,
                                CoordType.AxisXYScale, AlignH.Left, AlignV.Bottom);
                    text_max.FontSpec.FontColor = Color.Black;
                    text_max.ZOrder = ZOrder.A_InFront;
                    text_max.FontSpec.Border.IsVisible = false;
                    text_max.FontSpec.Fill.IsVisible = false;
                    text_max.FontSpec.Size = 12;
                    text_max.FontSpec.Angle = 0;
                    graphPane.GraphObjList.Add(text_max);
                }
                */
            }

            graphPane.YAxis.MajorTic.IsBetweenLabels = false;
            graphPane.YAxis.Scale.TextLabels = names; //  HistMatching.GetIterNames(iter_names)
            graphPane.YAxis.Type = AxisType.Text;
            graphPane.YAxis.Scale.IsReverse = true;
            graphPane.BarSettings.Base = BarBase.Y;
            graphPane.BarSettings.Type = BarType.SortedOverlay;
            //graphPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            graphControl.GraphPane.AxisChange();
            graphControl.Invalidate();
            //graphControl.Refresh();
        }




        void UpdateWellGraphs(IterationResult result, DataTable res_press, string[] wellnames)
        {
            RSM.RsmVector woprh = result.WOPRH(wellnames);
            RSM.RsmVector wopr = result.WOPR(wellnames);
            RSM.RsmVector wwprh = result.WWPRH(wellnames);
            RSM.RsmVector wwpr = result.WWPR(wellnames);
            RSM.RsmVector wgprh = result.WGPRH(wellnames);
            RSM.RsmVector wgpr = result.WGPR(wellnames);
            RSM.RsmVector wlprh = result.WLPRH(wellnames);
            RSM.RsmVector wlpr = result.WLPR(wellnames);
            //			
            RSM.RsmVector wopth = result.WOPTH(wellnames);
            RSM.RsmVector wopt = result.WOPT(wellnames);
            RSM.RsmVector wwpth = result.WWPTH(wellnames);
            RSM.RsmVector wwpt = result.WWPT(wellnames);
            RSM.RsmVector wgpth = result.WGPTH(wellnames);
            RSM.RsmVector wgpt = result.WGPT(wellnames);
            RSM.RsmVector wlpth = result.WLPTH(wellnames);
            RSM.RsmVector wlpt = result.WLPT(wellnames);
            //			
            RSM.RsmVector woirh = result.WOIRH(wellnames);
            RSM.RsmVector woir = result.WOIR(wellnames);
            RSM.RsmVector wwirh = result.WWIRH(wellnames);
            RSM.RsmVector wwir = result.WWIR(wellnames);
            RSM.RsmVector wgirh = result.WGIRH(wellnames);
            RSM.RsmVector wgir = result.WGIR(wellnames);
            //			
            RSM.RsmVector woith = result.WOITH(wellnames);
            RSM.RsmVector woit = result.WOIT(wellnames);
            RSM.RsmVector wwith = result.WWITH(wellnames);
            RSM.RsmVector wwit = result.WWIT(wellnames);
            RSM.RsmVector wgith = result.WGITH(wellnames);
            RSM.RsmVector wgit = result.WGIT(wellnames);
            //
            RSM.RsmVector wbhph = new RSM.RsmVector();
            RSM.RsmVector wbhp = new RSM.RsmVector();
            RSM.RsmVector wbp9h = new RSM.RsmVector();
            RSM.RsmVector wbp9 = new RSM.RsmVector();
            //
            RSM.RsmVector wcth = wwprh.Combine(wlprh, CombineType.DIVIDE);
            RSM.RsmVector wct = wwpr.Combine(wlpr, CombineType.DIVIDE);
            RSM.RsmVector gorh = wgprh.Combine(woprh, CombineType.DIVIDE);
            RSM.RsmVector gor = wgpr.Combine(wopr, CombineType.DIVIDE);
            //
            if (wellnames.Length == 1)
            {
                wbhph = result.WBHPH(wellnames);
                wbhp = result.WBHP(wellnames);
                wbp9h = res_press.Vector(result.Dates, wellnames);
                wbp9 = result.WBP9(wellnames);
            }
            //
            RedrawGrGraph(zedGraphControl_gr_op, result.Dates, woprh, wopr, wopth, wopt, "OPRH", "OPR", "OPTH", "OPT", Settings.OPRColor, Settings.OPTColor);
            RedrawGrGraph(zedGraphControl_gr_lp, result.Dates, wlprh, wlpr, wlpth, wlpt, "LPRH", "LPR", "LPTH", "LPT", Settings.LPRColor, Settings.LPTColor);
            RedrawGrGraph(zedGraphControl_gr_wp, result.Dates, wwprh, wwpr, wwpth, wwpt, "WPRH", "WPR", "WPTH", "WPT", Settings.WPRColor, Settings.WPTColor);
            RedrawGrGraph(zedGraphControl_gr_wi, result.Dates, wwirh, wwir, wwith, wwit, "WIRH", "WIR", "WITH", "WIT", Settings.WIRColor, Settings.WITColor);
            RedrawGrGraph(zedGraphControl_gr_gp, result.Dates, wgprh, wgpr, wgpth, wgpt, "GPRH", "GPR", "GPTH", "GPT", Settings.GPRColor, Settings.GPTColor);
            RedrawGrGraph(zedGraphControl_gr_gi, result.Dates, wgirh, wgir, wgith, wgit, "GIRH", "GIR", "GITH", "GIT", Settings.GIRColor, Settings.GITColor);
            RedrawGrGraph(zedGraphControl_gr_wct_gor, result.Dates, wcth, wct, gorh, gor, "WCTH", "WCT", "GORH", "GOR", Settings.WCTColor, Settings.GORColor);
            RedrawPressGraph(result.Dates, wbhph, wbhp, wbp9h, wbp9);
        }




        void UpdateCrossPlots(IterationResult result, DataTable res_press)
        {
            PointPairList wopr_pl = new PointPairList();
            PointPairList wwpr_pl = new PointPairList();
            PointPairList wgpr_pl = new PointPairList();
            PointPairList wlpr_pl = new PointPairList();
            PointPairList woir_pl = new PointPairList();
            PointPairList wwir_pl = new PointPairList();
            PointPairList wgir_pl = new PointPairList();
            //
            PointPairList wopt_pl = new PointPairList();
            PointPairList wwpt_pl = new PointPairList();
            PointPairList wgpt_pl = new PointPairList();
            PointPairList wlpt_pl = new PointPairList();
            PointPairList woit_pl = new PointPairList();
            PointPairList wwit_pl = new PointPairList();
            PointPairList wgit_pl = new PointPairList();
            //
            PointPairList wbhp_pl = new PointPairList();
            PointPairList wbp9_pl = new PointPairList();
            //
            List<string> wopr_wellnames = new List<string>();
            List<string> wwpr_wellnames = new List<string>();
            List<string> wgpr_wellnames = new List<string>();
            List<string> wlpr_wellnames = new List<string>();
            List<string> woir_wellnames = new List<string>();
            List<string> wwir_wellnames = new List<string>();
            List<string> wgir_wellnames = new List<string>();
            List<string> wopt_wellnames = new List<string>();
            List<string> wwpt_wellnames = new List<string>();
            List<string> wgpt_wellnames = new List<string>();
            List<string> wlpt_wellnames = new List<string>();
            List<string> woit_wellnames = new List<string>();
            List<string> wwit_wellnames = new List<string>();
            List<string> wgit_wellnames = new List<string>();
            List<string> wbhp_wellnames = new List<string>();
            List<string> wbp9_wellnames = new List<string>();
            //
            foreach (string wellname in well_names)
            {
                RSM.RsmVector woprh = result.WOPRH(wellname);
                RSM.RsmVector wopr = result.WOPR(wellname);
                RSM.RsmVector wwprh = result.WWPRH(wellname);
                RSM.RsmVector wwpr = result.WWPR(wellname);
                RSM.RsmVector wgprh = result.WGPRH(wellname);
                RSM.RsmVector wgpr = result.WGPR(wellname);
                RSM.RsmVector wlprh = result.WLPRH(wellname);
                RSM.RsmVector wlpr = result.WLPR(wellname);
                //
                //RSM.RsmVector woirh = result.WOIRH(wellname);
                //RSM.RsmVector woir = result.WOIR(wellname);
                RSM.RsmVector wwirh = result.WWIRH(wellname);
                RSM.RsmVector wwir = result.WWIR(wellname);
                RSM.RsmVector wgirh = result.WGIRH(wellname);
                RSM.RsmVector wgir = result.WGIR(wellname);
                //
                RSM.RsmVector wopth = result.WOPTH(wellname);
                RSM.RsmVector wopt = result.WOPT(wellname);
                RSM.RsmVector wwpth = result.WWPTH(wellname);
                RSM.RsmVector wwpt = result.WWPT(wellname);
                RSM.RsmVector wgpth = result.WGPTH(wellname);
                RSM.RsmVector wgpt = result.WGPT(wellname);
                RSM.RsmVector wlpth = result.WLPTH(wellname);
                RSM.RsmVector wlpt = result.WLPT(wellname);
                //
                //RSM.RsmVector woith = result.WOITH(wellname);
                //RSM.RsmVector woit = result.WOIT(wellname);
                RSM.RsmVector wwith = result.WWITH(wellname);
                RSM.RsmVector wwit = result.WWIT(wellname);
                RSM.RsmVector wgith = result.WGITH(wellname);
                RSM.RsmVector wgit = result.WGIT(wellname);
                //
                RSM.RsmVector wbhph = result.WBHPH(wellname);
                RSM.RsmVector wbhp = result.WBHP(wellname);
                //
                RSM.RsmVector wbp9h = res_press.Vector(result.Dates, wellname);
                RSM.RsmVector wbp9 = result.WBP9(wellname);
                //
                if (wopth.Values.Last() > 0)
                {
                    wopt_wellnames.Add(wellname);
                    wopr_wellnames.Add(wellname);
                    wopt_pl.Add(wopth.Values.Last(), wopt.Values.Last());
                    wopr_pl.Add(woprh.Values.Last(), wopr.Values.Last());
                }
                if (wwpth.Values.Last() > 0)
                {
                    wwpt_wellnames.Add(wellname);
                    wwpr_wellnames.Add(wellname);
                    wwpt_pl.Add(wwpth.Values.Last(), wwpt.Values.Last());
                    wwpr_pl.Add(wwprh.Values.Last(), wwpr.Values.Last());
                }
                if (wgpth.Values.Last() > 0)
                {
                    wgpt_wellnames.Add(wellname);
                    wgpr_wellnames.Add(wellname);
                    wgpt_pl.Add(wgpth.Values.Last(), wgpt.Values.Last());
                    wgpr_pl.Add(wgprh.Values.Last(), wgpr.Values.Last());
                }
                if (wlpth.Values.Last() > 0)
                {
                    wlpt_wellnames.Add(wellname);
                    wlpr_wellnames.Add(wellname);
                    wlpt_pl.Add(wlpth.Values.Last(), wlpt.Values.Last());
                    wlpr_pl.Add(wlprh.Values.Last(), wlpr.Values.Last());
                }
                /*
                if (woith.Values.Last() > 0)
                {
                    woit_pl.Add(woith.Values.Last(), woit.Values.Last());
                    woir_pl.Add(woirh.Values.Last(), woir.Values.Last());
                }
                */
                if (wwith.Values.Last() > 0)
                {
                    wwit_wellnames.Add(wellname);
                    wwir_wellnames.Add(wellname);
                    wwit_pl.Add(wwith.Values.Last(), wwit.Values.Last());
                    wwir_pl.Add(wwirh.Values.Last(), wwir.Values.Last());
                }
                if (wgith.Values.Last() > 0)
                {
                    wgit_wellnames.Add(wellname);
                    wgir_wellnames.Add(wellname);
                    wgit_pl.Add(wgith.Values.Last(), wgit.Values.Last());
                    wgir_pl.Add(wgirh.Values.Last(), wgir.Values.Last());
                }
                //
                for (int i = 0; i < wbhph.Values.Length; ++i)
                    if (wbhph.Values[i] > 0 && wbhp.Values[i] > 0)
                    {
                        wbhp_pl.Add(wbhph.Values[i], wbhp.Values[i]);
                        wbhp_wellnames.Add(wellname);
                    }
                //
                for (int i = 0; i < wbp9h.Values.Length; ++i)
                    if (wbp9h.Values[i] > 0 && wbp9.Values[i] > 0)
                    {
                        wbp9_pl.Add(wbp9h.Values[i], wbp9.Values[i]);
                        wbp9_wellnames.Add(wellname);
                    }
            }
            //
            RedrawCrossPlot(this.zedGraphControl_wopt_cp, wopt_pl, wopt_wellnames, Settings.OPTDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wwpt_cp, wwpt_pl, wwpt_wellnames, Settings.WPTDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wgpt_cp, wgpt_pl, wgpt_wellnames, Settings.GPTDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wlpt_cp, wlpt_pl, wlpt_wellnames, Settings.LPTDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wwit_cp, wwit_pl, wwit_wellnames, Settings.WITDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wgit_cp, wgit_pl, wgit_wellnames, Settings.GITDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wopr_cp, wopr_pl, wopr_wellnames, Settings.OPRDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wwpr_cp, wwpr_pl, wwpr_wellnames, Settings.WPRDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wgpr_cp, wgpr_pl, wgpr_wellnames, Settings.GPRDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wlpr_cp, wlpr_pl, wlpr_wellnames, Settings.LPRDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wwir_cp, wwir_pl, wwir_wellnames, Settings.WIRDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wgir_cp, wgir_pl, wgir_wellnames, Settings.GIRDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wbhp_cp, wbhp_pl, wbhp_wellnames, Settings.WBHPDeltaPercent);
            RedrawCrossPlot(this.zedGraphControl_wbp9_cp, wbp9_pl, wbp9_wellnames, Settings.ResPressDeltaPercent);
        }



        void UpdateHMAnalisyseGraphs(Iteration[] iterations, string[] wellnames)
        {
            List<double> wopt_deltas = new List<double>();
            List<double> wwpt_deltas = new List<double>();
            List<double> wgpt_deltas = new List<double>();
            List<double> wlpt_deltas = new List<double>();
            List<double> woit_deltas = new List<double>();
            List<double> wwit_deltas = new List<double>();
            List<double> wgit_deltas = new List<double>();

            List<double> wopr_devs = new List<double>();
            List<double> wwpr_devs = new List<double>();
            List<double> wgpr_devs = new List<double>();
            List<double> wlpr_devs = new List<double>();
            List<double> woir_devs = new List<double>();
            List<double> wwir_devs = new List<double>();
            List<double> wgir_devs = new List<double>();

            List<double> wbhp_devs = new List<double>();
            List<double> hm_devs = new List<double>();
            //
            foreach (Iteration iter in iterations) // не учитывать скважины с 0 дебитом/приемистостью
            {
                RSM.RsmVector woprh = iter.Result.WOPRH(wellnames);
                RSM.RsmVector wopr = iter.Result.WOPR(wellnames);
                RSM.RsmVector wwprh = iter.Result.WWPRH(wellnames);
                RSM.RsmVector wwpr = iter.Result.WWPR(wellnames);
                RSM.RsmVector wgprh = iter.Result.WGPRH(wellnames);
                RSM.RsmVector wgpr = iter.Result.WGPR(wellnames);
                RSM.RsmVector wlprh = iter.Result.WLPRH(wellnames);
                RSM.RsmVector wlpr = iter.Result.WLPR(wellnames);
                //			
                RSM.RsmVector woirh = iter.Result.WOIRH(wellnames);
                RSM.RsmVector woir = iter.Result.WOIR(wellnames);
                RSM.RsmVector wwirh = iter.Result.WWIRH(wellnames);
                RSM.RsmVector wwir = iter.Result.WWIR(wellnames);
                RSM.RsmVector wgirh = iter.Result.WGIRH(wellnames);
                RSM.RsmVector wgir = iter.Result.WGIR(wellnames);
                //
                RSM.RsmVector wopth = iter.Result.WOPTH(wellnames);
                RSM.RsmVector wopt = iter.Result.WOPT(wellnames);
                RSM.RsmVector wwpth = iter.Result.WWPTH(wellnames);
                RSM.RsmVector wwpt = iter.Result.WWPT(wellnames);
                RSM.RsmVector wgpth = iter.Result.WGPTH(wellnames);
                RSM.RsmVector wgpt = iter.Result.WGPT(wellnames);
                RSM.RsmVector wlpth = iter.Result.WLPTH(wellnames);
                RSM.RsmVector wlpt = iter.Result.WLPT(wellnames);
                //			
                RSM.RsmVector woith = iter.Result.WOITH(wellnames);
                RSM.RsmVector woit = iter.Result.WOIT(wellnames);
                RSM.RsmVector wwith = iter.Result.WWITH(wellnames);
                RSM.RsmVector wwit = iter.Result.WWIT(wellnames);
                RSM.RsmVector wgith = iter.Result.WGITH(wellnames);
                RSM.RsmVector wgit = iter.Result.WGIT(wellnames);
                //
                RSM.RsmVector wbhph = iter.Result.WBHPH(wellnames);
                RSM.RsmVector wbhp = iter.Result.WBHP(wellnames);
                //
                GetDeltaPercent(wopth, wopt, ref wopt_deltas);
                GetDeltaPercent(wwpth, wwpt, ref wwpt_deltas);
                GetDeltaPercent(wgpth, wgpt, ref wgpt_deltas);
                GetDeltaPercent(wlpth, wlpt, ref wlpt_deltas);
                GetDeltaPercent(woith, woit, ref woit_deltas);
                GetDeltaPercent(wwith, wwit, ref wwit_deltas);
                GetDeltaPercent(wgith, wgit, ref wgit_deltas);
                //
                GetDeltaPercent(wopr, woprh, ref wopr_devs);
                GetDeltaPercent(wwpr, wwprh, ref wwpr_devs);
                GetDeltaPercent(wgpr, wgprh, ref wgpr_devs);
                GetDeltaPercent(wlpr, wlprh, ref wlpr_devs);
                GetDeltaPercent(woir, woirh, ref woir_devs);
                GetDeltaPercent(wwir, wwirh, ref wwir_devs);
                GetDeltaPercent(wgir, wgirh, ref wgir_devs);
                //
                //double hm_dev = HMDeviation(wlpt_delta, wopt_delta, wwit_delta, wlpr_dev, wopr_dev, wwir_dev);
                //hm_devs.Add(hm_dev);
            }
            //
            string[] names = iterations.Select(v => v.Title).ToArray();
            RedrawHMBar(this.zedGraphControl_wopt_delta, names, ("", wopt_deltas.ToArray(), Settings.OPTColor));
            RedrawHMBar(this.zedGraphControl_wwpt_delta, names, ("", wwpt_deltas.ToArray(), Settings.WPTColor));
            RedrawHMBar(this.zedGraphControl_wgpt_delta, names, ("", wgpt_deltas.ToArray(), Settings.GPTColor));
            RedrawHMBar(this.zedGraphControl_wlpt_delta, names, ("", wlpt_deltas.ToArray(), Settings.LPTColor));
            RedrawHMBar(this.zedGraphControl_wwit_delta, names, ("", wwit_deltas.ToArray(), Settings.WITColor));
            RedrawHMBar(this.zedGraphControl_wgit_delta, names, ("", wgit_deltas.ToArray(), Settings.GITColor));

            RedrawHMBar(this.zedGraphControl_wopr_delta, names, ("", wopr_devs.ToArray(), Settings.OPRColor));
            RedrawHMBar(this.zedGraphControl_wwpr_delta, names, ("", wwpr_devs.ToArray(), Settings.WPRColor));
            RedrawHMBar(this.zedGraphControl_wgpr_delta, names, ("", wgpr_devs.ToArray(), Settings.GPRColor));
            RedrawHMBar(this.zedGraphControl_wlpr_delta, names, ("", wlpr_devs.ToArray(), Settings.LPRColor));
            RedrawHMBar(this.zedGraphControl_wwir_delta, names, ("", wwir_devs.ToArray(), Settings.WIRColor));
            RedrawHMBar(this.zedGraphControl_wgir_delta, names, ("", wgir_devs.ToArray(), Settings.GIRColor));

            //RedrawHMBar(this.zedGraphControl_wbhp_delta, names, wbhp_devs.ToArray(), Settings.WBHPColor);
            //RedrawHMBar(this.zedGraphControl_hm_dev, hm_devs, HMDev);
        }



/*
        void UpdateStDevsGraphs(IterationOpt[] iterations, string[] wellnames, DataTable res_press)
        {
            List<double> wopt_stdevs = new List<double>();
            List<double> wwpt_stdevs = new List<double>();
            List<double> wgpt_stdevs = new List<double>();
            List<double> wlpt_stdevs = new List<double>();
            List<double> wwit_stdevs = new List<double>();
            List<double> wgit_stdevs = new List<double>();

            List<double> wopr_stdevs = new List<double>();
            List<double> wwpr_stdevs = new List<double>();
            List<double> wgpr_stdevs = new List<double>();
            List<double> wlpr_stdevs = new List<double>();
            List<double> wwir_stdevs = new List<double>();
            List<double> wgir_stdevs = new List<double>();

            List<double> wbhp_stdevs = new List<double>();
            List<double> wbp9_stdevs = new List<double>();
            //
            foreach (IterationOpt iter in iterations) // не учитывать скважины с 0 дебитом/приемистостью
            {
                RSM.RsmVector woprh = iter.Result.WOPRH(wellnames);
                RSM.RsmVector wopr = iter.Result.WOPR(wellnames);
                RSM.RsmVector wwprh = iter.Result.WWPRH(wellnames);
                RSM.RsmVector wwpr = iter.Result.WWPR(wellnames);
                RSM.RsmVector wgprh = iter.Result.WGPRH(wellnames);
                RSM.RsmVector wgpr = iter.Result.WGPR(wellnames);
                RSM.RsmVector wlprh = iter.Result.WLPRH(wellnames);
                RSM.RsmVector wlpr = iter.Result.WLPR(wellnames);
                //			
                RSM.RsmVector wwirh = iter.Result.WWIRH(wellnames);
                RSM.RsmVector wwir = iter.Result.WWIR(wellnames);
                RSM.RsmVector wgirh = iter.Result.WGIRH(wellnames);
                RSM.RsmVector wgir = iter.Result.WGIR(wellnames);
                //
                RSM.RsmVector wopth = iter.Result.WOPTH(wellnames);
                RSM.RsmVector wopt = iter.Result.WOPT(wellnames);
                RSM.RsmVector wwpth = iter.Result.WWPTH(wellnames);
                RSM.RsmVector wwpt = iter.Result.WWPT(wellnames);
                RSM.RsmVector wgpth = iter.Result.WGPTH(wellnames);
                RSM.RsmVector wgpt = iter.Result.WGPT(wellnames);
                RSM.RsmVector wlpth = iter.Result.WLPTH(wellnames);
                RSM.RsmVector wlpt = iter.Result.WLPT(wellnames);
                //			
                RSM.RsmVector wwith = iter.Result.WWITH(wellnames);
                RSM.RsmVector wwit = iter.Result.WWIT(wellnames);
                RSM.RsmVector wgith = iter.Result.WGITH(wellnames);
                RSM.RsmVector wgit = iter.Result.WGIT(wellnames);
                //
                RSM.RsmVector wbhph = iter.Result.WBHPH(wellnames);
                RSM.RsmVector wbhp = iter.Result.WBHP(wellnames);
                //
                RSM.RsmVector wbp9h = res_press.Vector(iter.Result.Dates, wellnames);
                RSM.RsmVector wbp9 = iter.Result.WBP9(wellnames);
                //
                wopt_stdevs.Add(R2(wopth.Values, wopt.Values));
                wwpt_stdevs.Add(R2(wwpth.Values, wwpt.Values));
                wgpt_stdevs.Add(R2(wgpth.Values, wgpt.Values));
                wlpt_stdevs.Add(R2(wlpth.Values, wlpt.Values));
                wwit_stdevs.Add(R2(wwith.Values, wwit.Values));
                wgit_stdevs.Add(R2(wgith.Values, wgit.Values));
                wopr_stdevs.Add(R2(woprh.Values, wopr.Values));
                wwpr_stdevs.Add(R2(wwprh.Values, wwpr.Values));
                wgpr_stdevs.Add(R2(wgprh.Values, wgpr.Values));
                wlpr_stdevs.Add(R2(wlprh.Values, wlpr.Values));
                wwir_stdevs.Add(R2(wwirh.Values, wwir.Values));
                wgir_stdevs.Add(R2(wgirh.Values, wgir.Values));
                wbhp_stdevs.Add(R2(wbhph.Values, wbhp.Values));
                wbp9_stdevs.Add(R2(wbp9h.Values, wbp9.Values));
            }
            //
            string[] names = iterations.Select(v => v.Title).ToArray();
            RedrawHMBar(this.zedGraphControl_wopt_stdev, names, ("", wopt_stdevs.ToArray(), Settings.OPTColor));
            RedrawHMBar(this.zedGraphControl_wwpt_stdev, names, ("", wwpt_stdevs.ToArray(), Settings.WPTColor));
            RedrawHMBar(this.zedGraphControl_wgpt_stdev, names, ("", wgpt_stdevs.ToArray(), Settings.GPTColor));
            RedrawHMBar(this.zedGraphControl_wlpt_stdev, names, ("", wlpt_stdevs.ToArray(), Settings.LPTColor));
            RedrawHMBar(this.zedGraphControl_wwit_stdev, names, ("", wwit_stdevs.ToArray(), Settings.WITColor));
            RedrawHMBar(this.zedGraphControl_wgit_stdev, names, ("", wgit_stdevs.ToArray(), Settings.GITColor));
            RedrawHMBar(this.zedGraphControl_wopr_stdev, names, ("", wopr_stdevs.ToArray(), Settings.OPRColor));
            RedrawHMBar(this.zedGraphControl_wwpr_stdev, names, ("", wwpr_stdevs.ToArray(), Settings.WPRColor));
            RedrawHMBar(this.zedGraphControl_wgpr_stdev, names, ("", wgpr_stdevs.ToArray(), Settings.GPRColor));
            RedrawHMBar(this.zedGraphControl_wlpr_stdev, names, ("", wlpr_stdevs.ToArray(), Settings.LPRColor));
            RedrawHMBar(this.zedGraphControl_wwir_stdev, names, ("", wwir_stdevs.ToArray(), Settings.WIRColor));
            RedrawHMBar(this.zedGraphControl_wgir_stdev, names, ("", wgir_stdevs.ToArray(), Settings.GIRColor));
            RedrawHMBar(this.zedGraphControl_wbhp_stdev, names, ("", wbhp_stdevs.ToArray(), Settings.WBHPColor));
            RedrawHMBar(this.zedGraphControl_wbp9_stdev, names, ("", wbp9_stdevs.ToArray(), Settings.WBP9Color));
        }
*/

        void GetDeltaPercent(RSM.RsmVector hist, RSM.RsmVector model, ref List<double> deltas)
        {
            double hist_last = hist.Values.Last();
            double model_delta = hist_last == 0 ? 0 : (model.Values.Last() - hist_last) / hist_last * 100;
            deltas.Add(model_delta);
        }

        void GetDelta(RSM.RsmVector hist, RSM.RsmVector model, ref List<double> deltas)
        {
            double hist_last = hist.Values.Last();
            double model_delta = hist_last == 0 ? 0 : (model.Values.Last() - hist_last);
            deltas.Add(model_delta);
        }


        /*
        void UpdateCrossSectGraphs(Iteration iter, Prop perm_base, Prop perm_min, Prop perm_max, string[] wellnames, DateTime dt, bool show_min_max)
        {
            bool totals = this.checkBox_totals_cp.Checked;

            string[] names = Array.Empty<string>();
            double[] perm_values = Array.Empty<double>();
            double[] perm_min_values = Array.Empty<double>();
            double[] perm_max_values = Array.Empty<double>();
            double[] mult_values = Array.Empty<double>();
            double[] mult_cum_values = Array.Empty<double>();
            double[] opt_values = Array.Empty<double>();
            double[] lpt_values = Array.Empty<double>();
            double[] wit_values = Array.Empty<double>();
            double[] wct_values = Array.Empty<double>();
            //
            if (wellnames.Length == 1)
            {
                CellValue[] cop = totals ? iter.Result.COPT(wellnames[0], dt) : iter.Result.COPR(wellnames[0], dt);
                CellValue[] cwp = totals ? iter.Result.CWPT(wellnames[0], dt) : iter.Result.CWPR(wellnames[0], dt);
                CellValue[] cwi = totals ? iter.Result.CWIT(wellnames[0], dt) : iter.Result.CWIR(wellnames[0], dt);

                int length = cop.Length;
                Index3D[] indexes = new Index3D[length];

                names = new string[length];
                perm_values = new double[length];
                perm_min_values = new double[length];
                perm_max_values = new double[length];
                mult_values = new double[length];
                mult_cum_values = new double[length];
                opt_values = new double[length];
                lpt_values = new double[length];
                wit_values = new double[length];
                wct_values = new double[length];

                for (int i = 0; i < length; i++)
                {
                    indexes[i] = cop[i].Index;
                    names[i] = (new Index3D(indexes[i].I + 1, indexes[i].J + 1, indexes[i].K + 1)).ToString();
                    perm_values[i] = iter.Perm.Values[indexes[i].I, indexes[i].J, indexes[i].K];
                    perm_min_values[i] = perm_min.Values[indexes[i].I, indexes[i].J, indexes[i].K];
                    perm_max_values[i] = perm_max.Values[indexes[i].I, indexes[i].J, indexes[i].K];
                    mult_values[i] = iter.PermMult.Values[indexes[i].I, indexes[i].J, indexes[i].K];
                    mult_cum_values[i] = perm_values[i] / Helper.Bound(perm_min_values[i], perm_base.Values[indexes[i].I, indexes[i].J, indexes[i].K], perm_max_values[i]);
                    opt_values[i] = cop[i].Value;
                    lpt_values[i] = cop[i].Value + cwp[i].Value;
                    wit_values[i] = cwi[i].Value;
                    wct_values[i] = lpt_values[i] == 0 ? 0 : cwp[i].Value / lpt_values[i];
                }
            }
            //
            RedrawPermBar(this.zedGraphControl_perm_cs, names, perm_min_values, perm_values, perm_max_values, Settings.WBHPColor, show_min_max);
            RedrawHMBar(this.zedGraphControl_mult_cs, names, mult_values, Settings.WBP9Color);
            RedrawHMBar(this.zedGraphControl_mult_cum_cs, names, mult_cum_values, Settings.WBP9Color);
            RedrawHMBar(this.zedGraphControl_opt_cs, names, opt_values, totals ? Settings.OPTColor : Settings.OPRColor);
            RedrawHMBar(this.zedGraphControl_lpt_cs, names, lpt_values, totals ? Settings.LPTColor : Settings.LPRColor);
            RedrawHMBar(this.zedGraphControl_wit_cs, names, wit_values, totals ? Settings.WITColor : Settings.WIRColor);
            RedrawHMBar(this.zedGraphControl_wct_cs, names, wct_values, Settings.WCTColor);
        }
        */


        /*
        const int cs_perm = 0;
        const int cs_permmult = 1;
        const int cs_satnum = 2;
        const int cs_krorw = 3;
        const int cs_krorwmult = 4;
        const int cs_krwr = 5;
        const int cs_krwrmult = 6;
        const int cs_multpv = 7;
        const int cs_opt = 8;
        const int cs_wpt = 9;
        const int cs_lpt = 10;
        const int cs_wit = 11;
        const int cs_opr = 12;
        const int cs_wpr = 13;
        const int cs_lpr = 14;
        const int cs_wir = 15;
        const int cs_wct = 16;
        */

        /*
        string cs_name_perm = "Perm";
        string cs_name_permmult = "PermMult";
        string cs_name_satnum = "SATNUM";
        string cs_name_krorw = "Krorw";
        string cs_name_krorwmult = "KrorwMult";
        string cs_name_krwr = "Krwr";
        string cs_name_krwrmult = "KrwrMult";
        string cs_name_multpv = "MultPV";
        */
        string cs_name_opt = "OPT";
        string cs_name_wpt = "WPT";
        string cs_name_lpt = "LPT";
        string cs_name_wit = "WIT";
        string cs_name_opr = "OPR";
        string cs_name_wpr = "WPR";
        string cs_name_lpr = "LPR";
        string cs_name_wir = "WIR";
        string cs_name_wct = "WCT";


        void InitCrossSectNames(params string[] names)
        {
            this.checkedListBox_cross_sect.Items.Clear();
            foreach (string name in names)
                this.checkedListBox_cross_sect.Items.Add(name);
            this.checkedListBox_cross_sect.Items.Remove(HistMatching.PermMinTitle);
            this.checkedListBox_cross_sect.Items.Remove(HistMatching.PermMaxTitle);
            this.checkedListBox_cross_sect.Items.Remove(HistMatching.KrorwMinTitle);
            this.checkedListBox_cross_sect.Items.Remove(HistMatching.KrorwMaxTitle);
            this.checkedListBox_cross_sect.Items.Remove(HistMatching.KrwrMinTitle);
            this.checkedListBox_cross_sect.Items.Remove(HistMatching.KrwrMaxTitle);

            this.checkedListBox_cross_sect.Items.AddRange(new object[] {
                /*
                HistMatching.PermTitle     ,
                HistMatching.PermMultTitle ,
                HistMatching.SatnumTitle   ,
                HistMatching.KrorwTitle    ,
                HistMatching.KrorwMultTitle,
                HistMatching.KrwrTitle     ,
                HistMatching.KrwrMultTitle ,
                HistMatching.MultpvTitle   ,
                */
                cs_name_opt      ,
                cs_name_wpt      ,
                cs_name_lpt      ,
                cs_name_wit      ,
                cs_name_opr      ,
                cs_name_wpr      ,
                cs_name_lpr      ,
                cs_name_wir      ,
                cs_name_wct
                });
        }


        List<string> cross_sect_exceptions = new List<string>() 
        { 
            HistMatching.MultpvTitle, HistMatching.MultpvMinTitle, HistMatching.MultpvMaxTitle,
            HistMatching.AqcellTitle, HistMatching.AqregTitle
        };


        string[] cs_checked_items = new string[0];

        static string[] GetCheckedItems(CheckedListBox checkedListBox)
        {
            List<string> result = new List<string>();
            foreach (var v in checkedListBox.CheckedItems) result.Add(v.ToString());
            return result.ToArray();
        }



        List<string> prev_titles = new List<string>();
        void UpdateCrossSectGraphs2(IterationSet set, Iteration iter, string wellname, DateTime dt, bool show_min_max)
        {
            if (wellname == kw_field)
                return;

            List<string> new_titles = new List<string>();
            if (set.Grid.Specified())
            {
                new_titles.AddRange(set.Props.Items.Keys);
                new_titles.AddRange(iter.Props.Items.Keys);
            }

            new_titles = new_titles.Except(cross_sect_exceptions).ToList();

            if (!Enumerable.SequenceEqual(new_titles, prev_titles))
                InitCrossSectNames(new_titles.ToArray());

            prev_titles = new_titles;

            string[] checked_items = GetCheckedItems(checkedListBox_cross_sect);
            int nchecked = checked_items.Length;

            if (!Enumerable.SequenceEqual(checked_items, cs_checked_items))
            {
                cs_checked_items = checked_items;

                tableLayoutPanel_cross_sect.SuspendLayout();

                tableLayoutPanel_cross_sect.Controls.Clear();

                tableLayoutPanel_cross_sect.RowStyles.Clear();
                tableLayoutPanel_cross_sect.RowCount = 1;
                tableLayoutPanel_cross_sect.RowStyles.Add(new RowStyle());

                tableLayoutPanel_cross_sect.ColumnStyles.Clear();
                tableLayoutPanel_cross_sect.ColumnCount = nchecked;
                float size_percent = 100F / nchecked;
                for (int i = 0; i < nchecked; i++)
                    tableLayoutPanel_cross_sect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, size_percent));

                // satnum mult
                if (checked_items.Contains(HistMatching.SatnumTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_satnum, Array.IndexOf(checked_items, HistMatching.SatnumTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_satnum);
                }

                // perm
                if (checked_items.Contains(HistMatching.PermTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_perm, Array.IndexOf(checked_items, HistMatching.PermTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_perm);
                }

                // krorw
                if (checked_items.Contains(HistMatching.KrorwTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_krorw, Array.IndexOf(checked_items, HistMatching.KrorwTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_krorw);
                }

                // krwr
                if (checked_items.Contains(HistMatching.KrwrTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_krwr, Array.IndexOf(checked_items, HistMatching.KrwrTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_krwr);
                }

                // perm mult
                if (checked_items.Contains(HistMatching.PermMultTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_permmult, Array.IndexOf(checked_items, HistMatching.PermMultTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_permmult);
                }

                // krorw mult
                if (checked_items.Contains(HistMatching.KrorwMultTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_krorwmult, Array.IndexOf(checked_items, HistMatching.KrorwMultTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_krorwmult);
                }

                // krwr mult
                if (checked_items.Contains(HistMatching.KrwrMultTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_krwrmult, Array.IndexOf(checked_items, HistMatching.KrwrMultTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_krwrmult);
                }

                // multpv mult
                if (checked_items.Contains(HistMatching.MultpvTitle))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_multpv, Array.IndexOf(checked_items, HistMatching.MultpvTitle), 0);
                    InitZedGraphControl(zedGraphControl_cs_multpv);
                }

                // opt
                if (checked_items.Contains(cs_name_opt))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_opt, Array.IndexOf(checked_items, cs_name_opt), 0);
                    InitZedGraphControl(zedGraphControl_cs_opt);
                }

                // wpt
                if (checked_items.Contains(cs_name_wpt))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_wpt, Array.IndexOf(checked_items, cs_name_wpt), 0);
                    InitZedGraphControl(zedGraphControl_cs_wpt);
                }

                // lpt
                if (checked_items.Contains(cs_name_lpt))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_lpt, Array.IndexOf(checked_items, cs_name_lpt), 0);
                    InitZedGraphControl(zedGraphControl_cs_lpt);
                }

                // wit
                if (checked_items.Contains(cs_name_wit))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_wit, Array.IndexOf(checked_items, cs_name_wit), 0);
                    InitZedGraphControl(zedGraphControl_cs_wit);
                }

                // opr
                if (checked_items.Contains(cs_name_opr))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_opr, Array.IndexOf(checked_items, cs_name_opr), 0);
                    InitZedGraphControl(zedGraphControl_cs_opr);
                }

                // wpr
                if (checked_items.Contains(cs_name_wpr))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_wpr, Array.IndexOf(checked_items, cs_name_wpr), 0);
                    InitZedGraphControl(zedGraphControl_cs_wpr);
                }

                // lpr
                if (checked_items.Contains(cs_name_lpr))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_lpr, Array.IndexOf(checked_items, cs_name_lpr), 0);
                    InitZedGraphControl(zedGraphControl_cs_lpr);
                }

                // wir
                if (checked_items.Contains(cs_name_wir))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_wir, Array.IndexOf(checked_items, cs_name_wir), 0);
                    InitZedGraphControl(zedGraphControl_cs_wir);
                }

                // wct
                if (checked_items.Contains(cs_name_wct))
                {
                    tableLayoutPanel_cross_sect.Controls.Add(zedGraphControl_cs_wct, Array.IndexOf(checked_items, cs_name_wct), 0);
                    InitZedGraphControl(zedGraphControl_cs_wct);
                }

                tableLayoutPanel_cross_sect.ResumeLayout();
            }

            Index3D[] cells = iter.Result.Cells(wellname);
            int length = cells.Length;
            string[] names = new string[length];
            for (int i = 0; i < length; i++)
                names[i] = (new Index3D(cells[i].I + 1, cells[i].J + 1, cells[i].K + 1)).ToString();


            // satnum mult
            if (checked_items.Contains(HistMatching.SatnumTitle))
            {
                RedrawCSBar(names, cells, set.Props.Items[HistMatching.SatnumTitle], set.Grid.Actnum, zedGraphControl_cs_satnum, Color.Black);
            }

            // perm
            if (checked_items.Contains(HistMatching.PermTitle))
            {
                RedrawCSBar(names, cells, 
                    set.Props.Items[HistMatching.PermMinTitle], iter.Props.Items[HistMatching.PermTitle], set.Props.Items[HistMatching.PermMaxTitle], set.Grid.Actnum,
                    zedGraphControl_cs_perm, Color.Black, show_min_max);
            }

            // krorw
            if (checked_items.Contains(HistMatching.KrorwTitle))
            {
                RedrawCSBar(names, cells, 
                    set.Props.Items[HistMatching.KrorwMinTitle], iter.Props.Items[HistMatching.KrorwTitle], set.Props.Items[HistMatching.KrorwMaxTitle], set.Grid.Actnum,
                    zedGraphControl_cs_krorw, Color.Black, show_min_max);
            }

            // krwr
            if (checked_items.Contains(HistMatching.KrwrTitle))
            {
                RedrawCSBar(names, cells,
                    set.Props.Items[HistMatching.KrwrMinTitle], iter.Props.Items[HistMatching.KrwrTitle], set.Props.Items[HistMatching.KrwrMaxTitle], set.Grid.Actnum,
                    zedGraphControl_cs_krwr, Color.Black, show_min_max);
            }

            // perm mult
            if (checked_items.Contains(HistMatching.PermMultTitle))
            {
                RedrawCSBar(names, cells, iter.Props.Items[HistMatching.PermMultTitle], set.Grid.Actnum, zedGraphControl_cs_permmult, Color.Black);
            }

            // krorw mult
            if (checked_items.Contains(HistMatching.KrorwMultTitle))
            {
                RedrawCSBar(names, cells, iter.Props.Items[HistMatching.KrorwMultTitle], set.Grid.Actnum, zedGraphControl_cs_krorwmult, Color.Black);
            }

            // krwr mult
            if (checked_items.Contains(HistMatching.KrwrMultTitle))
            {
                RedrawCSBar(names, cells, iter.Props.Items[HistMatching.KrwrMultTitle], set.Grid.Actnum, zedGraphControl_cs_krwrmult, Color.Black);
            }

            // multpv mult
            if (checked_items.Contains(HistMatching.MultpvTitle))
            {
                RedrawCSBar(names, cells, iter.Props.Items[HistMatching.MultpvTitle], set.Grid.Actnum, zedGraphControl_cs_multpv, Color.Black);
            }

            // opt
            if (checked_items.Contains(cs_name_opt))
            {
                RedrawCSBar(names, iter.Result.COPT(wellname, dt), zedGraphControl_cs_opt, Color.Black);
            }

            // wpt
            if (checked_items.Contains(cs_name_wpt))
            {
                RedrawCSBar(names, iter.Result.CWPT(wellname, dt), zedGraphControl_cs_wpt, Color.Black);
            }

            // lpt
            if (checked_items.Contains(cs_name_lpt))
            {
                CellValue[] opt = iter.Result.COPT(wellname, dt);
                CellValue[] wpt = iter.Result.CWPT(wellname, dt);
                CellValue[] lpt = new CellValue[opt.Length];
                for (int i = 0; i < lpt.Length; i++)
                    lpt[i] = new CellValue(opt[i].Index, opt[i].Value + wpt[i].Value);
                RedrawCSBar(names, lpt, zedGraphControl_cs_lpt, Color.Black);
            }

            // wit
            if (checked_items.Contains(cs_name_wit))
            {
                RedrawCSBar(names, iter.Result.CWIT(wellname, dt), zedGraphControl_cs_wit, Color.Black);
            }

            // opr
            if (checked_items.Contains(cs_name_opr))
            {
                RedrawCSBar(names, iter.Result.COPR(wellname, dt), zedGraphControl_cs_opr, Color.Black);
            }

            // wpr
            if (checked_items.Contains(cs_name_wpr))
            {
                RedrawCSBar(names, iter.Result.CWPR(wellname, dt), zedGraphControl_cs_wpr, Color.Black);
            }

            // lpr
            if (checked_items.Contains(cs_name_lpr))
            {
                CellValue[] opr = iter.Result.COPR(wellname, dt);
                CellValue[] wpr = iter.Result.CWPR(wellname, dt);
                CellValue[] lpr = new CellValue[opr.Length];
                for (int i = 0; i < lpr.Length; i++)
                    lpr[i] = new CellValue(opr[i].Index, opr[i].Value + wpr[i].Value);
                RedrawCSBar(names, lpr, zedGraphControl_cs_lpr, Color.Black);
            }

            // wir
            if (checked_items.Contains(cs_name_wir))
            {
                RedrawCSBar(names, iter.Result.CWIR(wellname, dt), zedGraphControl_cs_wir, Color.Black);
            }

            // wct
            if (checked_items.Contains(cs_name_wct))
            {
                CellValue[] opr = iter.Result.COPR(wellname, dt);
                CellValue[] wpr = iter.Result.CWPR(wellname, dt);
                CellValue[] wct = new CellValue[opr.Length];
                for (int i = 0; i < wct.Length; i++)
                    wct[i] = new CellValue(opr[i].Index, opr[i].Value + wpr[i].Value == 0 ? 0 : wpr[i].Value / (opr[i].Value + wpr[i].Value));
                RedrawCSBar(names, wct, zedGraphControl_cs_wct, Color.Black);
            }
        }




        static void InitZedGraphControl(ZedGraphControl zedGraphControl)
        {
            zedGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            zedGraphControl.Location = new System.Drawing.Point(5, 5);
            zedGraphControl.Margin = new System.Windows.Forms.Padding(5);
            zedGraphControl.Name = "zedGraphControl_mult_cs";
            zedGraphControl.ScrollGrace = 0D;
            zedGraphControl.ScrollMaxX = 0D;
            zedGraphControl.ScrollMaxY = 0D;
            zedGraphControl.ScrollMaxY2 = 0D;
            zedGraphControl.ScrollMinX = 0D;
            zedGraphControl.ScrollMinY = 0D;
            zedGraphControl.ScrollMinY2 = 0D;
            zedGraphControl.Size = new System.Drawing.Size(10, 20);
            zedGraphControl.TabIndex = 5;
            zedGraphControl.UseExtendedPrintDialog = true;
        }



        static void RedrawCSBar(string[] names, Index3D[] cells, ActProp min, ActProp array, ActProp max, Actnum actnum,
                                ZedGraphControl graphControl, Color color, bool show_min_max)
        {
            int length = cells.Length;
            double[] values = new double[length];
            double[] min_values = new double[length];
            double[] max_values = new double[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = array.GetValue(cells[i], actnum);
                min_values[i] = min.GetValue(cells[i], actnum);
                max_values[i] = max.GetValue(cells[i], actnum);
            }
            RedrawPermBar(graphControl, names, min_values, values, max_values, color, show_min_max);
        }



        static void RedrawCSBar(string[] names, Index3D[] cells, ActProp array, Actnum actnum, 
                                ZedGraphControl graphControl, Color color)
        {
            int length = cells.Length;
            double[] values = new double[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = array.GetValue(cells[i], actnum);
            }
            RedrawHMBar(graphControl, names, ("", values, color));
        }

        static void RedrawCSBar(string[] names, CellValue[] cell_values, ZedGraphControl graphControl, Color color)
        {
            int length = cell_values.Length;
            double[] values = new double[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = cell_values[i].Value;
            }
            RedrawHMBar(graphControl, names, ("", values, color));
        }



        static void AddGraphControl(TableLayoutPanel panel, int column_number, string[] names, Index3D[] cells,  Prop min, Prop array, Prop max, ZedGraphControl graphControl, Color color, bool show_min_max)
        {
            int length = cells.Length;
            double[] values = new double[length];
            double[] min_values = new double[length];
            double[] max_values = new double[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = array.Value(cells[i]);
                min_values[i] = min.Value(cells[i]);
                max_values[i] = max.Value(cells[i]);
            }
            RedrawPermBar(graphControl, names, min_values, values, max_values, color, show_min_max);
            panel.Controls.Add(graphControl, column_number, 0);
            InitZedGraphControl(graphControl);
        }


        static void AddGraphControl(TableLayoutPanel panel, int column_number, string[] names, Index3D[] cells,  Prop array, ZedGraphControl graphControl, Color color)
        {
            int length = cells.Length;
            double[] values = new double[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = array.Value(cells[i]);
            }
            RedrawHMBar(graphControl, names, ("", values, color));
            panel.Controls.Add(graphControl, column_number, 0);
            InitZedGraphControl(graphControl);
        }

        static void AddGraphControl(TableLayoutPanel panel, int column_number, string[] names, CellValue[] cell_values, ZedGraphControl graphControl, Color color)
        {
            int length = cell_values.Length;
            double[] values = new double[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = cell_values[i].Value;
            }
            RedrawHMBar(graphControl, names, ("", values, color));
            panel.Controls.Add(graphControl, column_number, 0);
            InitZedGraphControl(graphControl);
        }



        void UpdateAnalyseGraphs(Iteration[] iterations, DataTable res_press)
        {
            double[] wopt_r2 = new double[iterations.Length];
            double[] wwpt_r2 = new double[iterations.Length];
            double[] wgpt_r2 = new double[iterations.Length];
            double[] wlpt_r2 = new double[iterations.Length];
            double[] wwit_r2 = new double[iterations.Length];
            double[] wgit_r2 = new double[iterations.Length];
            double[] wopr_r2 = new double[iterations.Length];
            double[] wwpr_r2 = new double[iterations.Length];
            double[] wgpr_r2 = new double[iterations.Length];
            double[] wlpr_r2 = new double[iterations.Length];
            double[] wwir_r2 = new double[iterations.Length];
            double[] wgir_r2 = new double[iterations.Length];
            double[] wbhp_r2 = new double[iterations.Length];
            double[] wbp9_r2 = new double[iterations.Length];

            for (int i = 0; i < iterations.Length; ++i)
            {
                UpdatePointPairLists(iterations[i].Result, res_press,
                                        out PointPairList wopr_pl,
                                        out PointPairList wwpr_pl,
                                        out PointPairList wgpr_pl,
                                        out PointPairList wlpr_pl,
                                        out PointPairList woir_pl,
                                        out PointPairList wwir_pl,
                                        out PointPairList wgir_pl,
                                        out PointPairList wopt_pl,
                                        out PointPairList wwpt_pl,
                                        out PointPairList wgpt_pl,
                                        out PointPairList wlpt_pl,
                                        out PointPairList woit_pl,
                                        out PointPairList wwit_pl,
                                        out PointPairList wgit_pl,
                                        out PointPairList wbhp_pl,
                                        out PointPairList wbp9_pl,
                                        out List<string> result_wellnames,
                                        out List<string> wbhp_wellnames,
                                        out List<string> wbp9_wellnames);

                wopt_r2[i] = R2(wopt_pl.Select(v => v.X).ToArray(), wopt_pl.Select(v => v.Y).ToArray());
                wwpt_r2[i] = R2(wwpt_pl.Select(v => v.X).ToArray(), wwpt_pl.Select(v => v.Y).ToArray());
                wgpt_r2[i] = R2(wgpt_pl.Select(v => v.X).ToArray(), wgpt_pl.Select(v => v.Y).ToArray());
                wlpt_r2[i] = R2(wlpt_pl.Select(v => v.X).ToArray(), wlpt_pl.Select(v => v.Y).ToArray());
                wwit_r2[i] = R2(wwit_pl.Select(v => v.X).ToArray(), wwit_pl.Select(v => v.Y).ToArray());
                wgit_r2[i] = R2(wgit_pl.Select(v => v.X).ToArray(), wgit_pl.Select(v => v.Y).ToArray());
                wopr_r2[i] = R2(wopr_pl.Select(v => v.X).ToArray(), wopr_pl.Select(v => v.Y).ToArray());
                wwpr_r2[i] = R2(wwpr_pl.Select(v => v.X).ToArray(), wwpr_pl.Select(v => v.Y).ToArray());
                wgpr_r2[i] = R2(wgpr_pl.Select(v => v.X).ToArray(), wgpr_pl.Select(v => v.Y).ToArray());
                wlpr_r2[i] = R2(wlpr_pl.Select(v => v.X).ToArray(), wlpr_pl.Select(v => v.Y).ToArray());
                wwir_r2[i] = R2(wwir_pl.Select(v => v.X).ToArray(), wwir_pl.Select(v => v.Y).ToArray());
                wgir_r2[i] = R2(wgir_pl.Select(v => v.X).ToArray(), wgir_pl.Select(v => v.Y).ToArray());
                wbhp_r2[i] = R2(wbhp_pl.Select(v => v.X).ToArray(), wbhp_pl.Select(v => v.Y).ToArray());
                wbp9_r2[i] = R2(wbp9_pl.Select(v => v.X).ToArray(), wbp9_pl.Select(v => v.Y).ToArray());
            }

            string[] names = iterations.Select(v => v.Title).ToArray();
            RedrawHMBar(this.zedGraphControl_wopt_r2, names, ("", wopt_r2.ToArray(), Settings.OPTColor));
            RedrawHMBar(this.zedGraphControl_wwpt_r2, names, ("", wwpt_r2.ToArray(), Settings.WPTColor));
            RedrawHMBar(this.zedGraphControl_wgpt_r2, names, ("", wgpt_r2.ToArray(), Settings.GPTColor));
            RedrawHMBar(this.zedGraphControl_wlpt_r2, names, ("", wlpt_r2.ToArray(), Settings.LPTColor));
            RedrawHMBar(this.zedGraphControl_wwit_r2, names, ("", wwit_r2.ToArray(), Settings.WITColor));
            RedrawHMBar(this.zedGraphControl_wgit_r2, names, ("", wgit_r2.ToArray(), Settings.GITColor));
            RedrawHMBar(this.zedGraphControl_wopr_r2, names, ("", wopr_r2.ToArray(), Settings.OPRColor));
            RedrawHMBar(this.zedGraphControl_wwpr_r2, names, ("", wwpr_r2.ToArray(), Settings.WPRColor));
            RedrawHMBar(this.zedGraphControl_wgpr_r2, names, ("", wgpr_r2.ToArray(), Settings.GPRColor));
            RedrawHMBar(this.zedGraphControl_wlpr_r2, names, ("", wlpr_r2.ToArray(), Settings.LPRColor));
            RedrawHMBar(this.zedGraphControl_wwir_r2, names, ("", wwir_r2.ToArray(), Settings.WIRColor));
            RedrawHMBar(this.zedGraphControl_wgir_r2, names, ("", wgir_r2.ToArray(), Settings.GIRColor));
            RedrawHMBar(this.zedGraphControl_wbhp_r2, names, ("", wbhp_r2.ToArray(), Settings.WBHPColor));
            RedrawHMBar(this.zedGraphControl_wbp9_r2, names, ("", wbp9_r2.ToArray(), Settings.WBP9Color));
        }



        void UpdatePointPairLists(IterationResult result, DataTable res_press,
                                    out PointPairList wopr_pl,
                                    out PointPairList wwpr_pl,
                                    out PointPairList wgpr_pl,
                                    out PointPairList wlpr_pl,
                                    out PointPairList woir_pl,
                                    out PointPairList wwir_pl,
                                    out PointPairList wgir_pl,
                                    out PointPairList wopt_pl,
                                    out PointPairList wwpt_pl,
                                    out PointPairList wgpt_pl,
                                    out PointPairList wlpt_pl,
                                    out PointPairList woit_pl,
                                    out PointPairList wwit_pl,
                                    out PointPairList wgit_pl,
                                    out PointPairList wbhp_pl,
                                    out PointPairList wbp9_pl,
                                    out List<string> result_wellnames,
                                    out List<string> wbhp_wellnames,
                                    out List<string> wbp9_wellnames)
        {
            wopr_pl = new PointPairList();
            wwpr_pl = new PointPairList();
            wgpr_pl = new PointPairList();
            wlpr_pl = new PointPairList();
            woir_pl = new PointPairList();
            wwir_pl = new PointPairList();
            wgir_pl = new PointPairList();
            wopt_pl = new PointPairList();
            wwpt_pl = new PointPairList();
            wgpt_pl = new PointPairList();
            wlpt_pl = new PointPairList();
            woit_pl = new PointPairList();
            wwit_pl = new PointPairList();
            wgit_pl = new PointPairList();
            wbhp_pl = new PointPairList();
            wbp9_pl = new PointPairList();
            result_wellnames = new List<string>();
            wbhp_wellnames = new List<string>();
            wbp9_wellnames = new List<string>();
            //
            foreach (string wellname in well_names)
            {
                result_wellnames.Add(wellname);
                //
                RSM.RsmVector woprh = result.WOPRH(wellname);
                RSM.RsmVector wopr = result.WOPR(wellname);
                RSM.RsmVector wwprh = result.WWPRH(wellname);
                RSM.RsmVector wwpr = result.WWPR(wellname);
                RSM.RsmVector wgprh = result.WGPRH(wellname);
                RSM.RsmVector wgpr = result.WGPR(wellname);
                RSM.RsmVector wlprh = result.WLPRH(wellname);
                RSM.RsmVector wlpr = result.WLPR(wellname);
                //
                RSM.RsmVector woirh = result.WOIRH(wellname);
                RSM.RsmVector woir = result.WOIR(wellname);
                RSM.RsmVector wwirh = result.WWIRH(wellname);
                RSM.RsmVector wwir = result.WWIR(wellname);
                RSM.RsmVector wgirh = result.WGIRH(wellname);
                RSM.RsmVector wgir = result.WGIR(wellname);
                //
                RSM.RsmVector wopth = result.WOPTH(wellname);
                RSM.RsmVector wopt = result.WOPT(wellname);
                RSM.RsmVector wwpth = result.WWPTH(wellname);
                RSM.RsmVector wwpt = result.WWPT(wellname);
                RSM.RsmVector wgpth = result.WGPTH(wellname);
                RSM.RsmVector wgpt = result.WGPT(wellname);
                RSM.RsmVector wlpth = result.WLPTH(wellname);
                RSM.RsmVector wlpt = result.WLPT(wellname);
                //
                RSM.RsmVector woith = result.WOITH(wellname);
                RSM.RsmVector woit = result.WOIT(wellname);
                RSM.RsmVector wwith = result.WWITH(wellname);
                RSM.RsmVector wwit = result.WWIT(wellname);
                RSM.RsmVector wgith = result.WGITH(wellname);
                RSM.RsmVector wgit = result.WGIT(wellname);
                //
                RSM.RsmVector wbhph = result.WBHPH(wellname);
                RSM.RsmVector wbhp = result.WBHP(wellname);
                //
                RSM.RsmVector wbp9h = res_press.Vector(result.Dates, wellname);
                RSM.RsmVector wbp9 = result.WBP9(wellname);
                //
                if (wopth.Values.Last() > 0)
                {
                    wopt_pl.Add(wopth.Values.Last(), wopt.Values.Last());
                    wopr_pl.Add(woprh.Values.Last(), wopr.Values.Last());
                }
                if (wwpth.Values.Last() > 0)
                {
                    wwpt_pl.Add(wwpth.Values.Last(), wwpt.Values.Last());
                    wwpr_pl.Add(wwprh.Values.Last(), wwpr.Values.Last());
                }
                if (wgpth.Values.Last() > 0)
                {
                    wgpt_pl.Add(wgpth.Values.Last(), wgpt.Values.Last());
                    wgpr_pl.Add(wgprh.Values.Last(), wgpr.Values.Last());
                }
                if (wlpth.Values.Last() > 0)
                {
                    wlpt_pl.Add(wlpth.Values.Last(), wlpt.Values.Last());
                    wlpr_pl.Add(wlprh.Values.Last(), wlpr.Values.Last());
                }
                if (woith.Values.Last() > 0)
                {
                    woit_pl.Add(woith.Values.Last(), woit.Values.Last());
                    woir_pl.Add(woirh.Values.Last(), woir.Values.Last());
                }
                if (wwith.Values.Last() > 0)
                {
                    wwit_pl.Add(wwith.Values.Last(), wwit.Values.Last());
                    wwir_pl.Add(wwirh.Values.Last(), wwir.Values.Last());
                }
                if (wgith.Values.Last() > 0)
                {
                    wgit_pl.Add(wgith.Values.Last(), wgit.Values.Last());
                    wgir_pl.Add(wgirh.Values.Last(), wgir.Values.Last());
                }
                //
                for (int i = 0; i < wbhph.Values.Length; ++i)
                    if (wbhph.Values[i] > 0 && wbhp.Values[i] > 0)
                    {
                        wbhp_pl.Add(wbhph.Values[i], wbhp.Values[i]);
                        wbhp_wellnames.Add(wellname);
                    }
                //
                for (int i = 0; i < wbp9h.Values.Length; ++i)
                    if (wbp9h.Values[i] > 0 && wbp9.Values[i] > 0)
                    {
                        wbp9_pl.Add(wbp9h.Values[i], wbp9.Values[i]);
                        wbp9_wellnames.Add(wellname);
                    }
            }
        }



        double HMDeviation(double wlpt_delta, double wopt_delta, double wwit_delta,
                           double wlpr_dev, double wopr_dev, double wwir_dev)
        {
            const double wt = 0.7 / 3, wr = 0.3 / 3;
            return Math.Abs(wlpt_delta) * wt + Math.Abs(wopt_delta) * wt + Math.Abs(wwit_delta) * wt + wlpr_dev * wr + wopr_dev * wr + wwir_dev * wr;
        }


        static double Round(double value)
        {
            return value;
        }


        void UpdateTable(IterationResult result)
        {
            //if (niter < 0) return;
            double wopr_d = Settings.OPRDeltaPercent;
            double wlpr_d = Settings.LPRDeltaPercent;
            double wwir_d = Settings.WIRDeltaPercent;
            double wopt_d = Settings.OPTDeltaPercent;
            double wlpt_d = Settings.LPTDeltaPercent;
            double wwit_d = Settings.WITDeltaPercent;
            double wells_targ = Settings.TargetWellsPercent;

            Dictionary<string, double[]> rows = new Dictionary<string, double[]>();

            const int v_wopth = 0;
            const int v_wopt = 1;
            const int v_wopt_d = 2;
            const int v_wlpth = 3;
            const int v_wlpt = 4;
            const int v_wlpt_d = 5;
            const int v_wwpth = 6;
            const int v_wwpt = 7;
            const int v_wwpt_d = 8;
            const int v_wwith = 9;
            const int v_wwit = 10;
            const int v_wwit_d = 11;
            const int v_wgpth = 12;
            const int v_wgpt = 13;
            const int v_wgpt_d = 14;
            const int v_wgith = 15;
            const int v_wgit = 16;
            const int v_wgit_d = 17;
            const int v_woprh = 18;
            const int v_wopr = 19;
            const int v_wopr_d = 20;
            const int v_wlprh = 21;
            const int v_wlpr = 22;
            const int v_wlpr_d = 23;
            const int v_wwprh = 24;
            const int v_wwpr = 25;
            const int v_wwpr_d = 26;
            const int v_wwirh = 27;
            const int v_wwir = 28;
            const int v_wwir_d = 29;
            const int v_wgprh = 30;
            const int v_wgpr = 31;
            const int v_wgpr_d = 32;
            const int v_wgirh = 33;
            const int v_wgir = 34;
            const int v_wgir_d = 35;
            const int v_count = 36;

            string[] wellnames = result.WellNames.ToArray();

            double[] wopth_list = new double[wellnames.Length];
            double[] woprh_list = new double[wellnames.Length];

            this.dataGridView_hm_table_wells.Rows.Clear();
            this.dataGridView_hm_table_wells.Rows.Add(wellnames.Length);

            //Parallel.For(0, wellnames.Length, w =>
            for (int w = 0; w < wellnames.Length; ++w)
            {
                double wopt = result.WOPT(wellnames[w]).Values.Last();
                double wopth = result.WOPTH(wellnames[w]).Values.Last();
                double wlpt = result.WLPT(wellnames[w]).Values.Last();
                double wlpth = result.WLPTH(wellnames[w]).Values.Last();
                double wwpt = result.WWPT(wellnames[w]).Values.Last();
                double wwpth = result.WWPTH(wellnames[w]).Values.Last();
                double wwit = result.WWIT(wellnames[w]).Values.Last();
                double wwith = result.WWITH(wellnames[w]).Values.Last();
                double wgpt = result.WGPT(wellnames[w]).Values.Last();
                double wgpth = result.WGPTH(wellnames[w]).Values.Last();
                double wgit = result.WGIT(wellnames[w]).Values.Last();
                double wgith = result.WGITH(wellnames[w]).Values.Last();

                double wopr = result.WOPR(wellnames[w]).Values.Last();
                double woprh = result.WOPRH(wellnames[w]).Values.Last();
                double wlpr = result.WLPR(wellnames[w]).Values.Last();
                double wlprh = result.WLPRH(wellnames[w]).Values.Last();
                double wwpr = result.WWPR(wellnames[w]).Values.Last();
                double wwprh = result.WWPRH(wellnames[w]).Values.Last();
                double wwir = result.WWIR(wellnames[w]).Values.Last();
                double wwirh = result.WWIRH(wellnames[w]).Values.Last();
                double wgpr = result.WGPR(wellnames[w]).Values.Last();
                double wgprh = result.WGPRH(wellnames[w]).Values.Last();
                double wgir = result.WGIR(wellnames[w]).Values.Last();
                double wgirh = result.WGIRH(wellnames[w]).Values.Last();

                double[] temp = new double[v_count];
                temp[v_wopth] = wopth / 1000;
                temp[v_wopt] = wopt / 1000;
                temp[v_wopt_d] = (wopth == 0) ? 0 : Math.Abs(wopt - wopth) / wopth * 100;
                temp[v_wlpth] = wlpth / 1000;
                temp[v_wlpt] = wlpt / 1000;
                temp[v_wlpt_d] = (wlpth == 0) ? 0 : Math.Abs(wlpt - wlpth) / wlpth * 100;
                temp[v_wwpth] = wwpth / 1000;
                temp[v_wwpt] = wwpt / 1000;
                temp[v_wwpt_d] = (wwpth == 0) ? 0 : Math.Abs(wwpt - wwpth) / wwpth * 100;
                temp[v_wwith] = wwith / 1000;
                temp[v_wwit] = wwit / 1000;
                temp[v_wwit_d] = (wwith == 0) ? 0 : Math.Abs(wwit - wwith) / wwith * 100;
                temp[v_wgpth] = wgpth / 1000000;
                temp[v_wgpt] = wgpt / 1000000;
                temp[v_wgpt_d] = (wgpth == 0) ? 0 : Math.Abs(wgpt - wgpth) / wgpth * 100;
                temp[v_wgith] = wgith / 1000000;
                temp[v_wgit] = wgit / 1000000;
                temp[v_wgit_d] = (wgith == 0) ? 0 : Math.Abs(wgit - wgith) / wgith * 100;
                temp[v_woprh] = woprh;
                temp[v_wopr] = wopr;
                temp[v_wopr_d] = (woprh == 0) ? 0 : Math.Abs(wopr - woprh) / woprh * 100;
                temp[v_wlprh] = wlprh;
                temp[v_wlpr] = wlpr;
                temp[v_wlpr_d] = (wlprh == 0) ? 0 : Math.Abs(wlpr - wlprh) / wlprh * 100;
                temp[v_wwprh] = wwprh;
                temp[v_wwpr] = wwpr;
                temp[v_wwpr_d] = (wwprh == 0) ? 0 : Math.Abs(wwpr - wwprh) / wwprh * 100;
                temp[v_wwirh] = wwirh;
                temp[v_wwir] = wwir;
                temp[v_wwir_d] = (wwirh == 0) ? 0 : Math.Abs(wwir - wwirh) / wwirh * 100;
                temp[v_wgprh] = wgprh;
                temp[v_wgpr] = wgpr;
                temp[v_wgpr_d] = (wgprh == 0) ? 0 : Math.Abs(wgpr - wgprh) / wgprh * 100;
                temp[v_wgirh] = wgirh;
                temp[v_wgir] = wgir;
                temp[v_wgir_d] = (wgirh == 0) ? 0 : Math.Abs(wgir - wgirh) / wgirh * 100;

                wopth_list[w] = temp[v_wopth];
                woprh_list[w] = temp[v_woprh];

                string clear_name = wellnames[w];
                if (Settings.WellSufix.Length > 0 && wellnames[w].Substring(Math.Max(0, wellnames[w].Length - Settings.WellSufix.Length)) == Settings.WellSufix)
                    clear_name = wellnames[w].Substring(0, wellnames[w].Length - Settings.WellSufix.Length);

                lock (rows)
                {
                    if (rows.ContainsKey(clear_name))
                    {
                        double[] temp2 = rows[clear_name];
                        for (int i = 0; i < v_count; ++i) temp[i] += temp2[i];
                        rows[clear_name] = temp;
                    }
                    else
                        rows.Add(clear_name, temp);
                }
            }//);

            const int c_wellname = 0;
            const int c_wopth = 1;
            const int c_wopt = 2;
            const int c_wopt_d = 3;
            const int c_wlpth = 4;
            const int c_wlpt = 5;
            const int c_wlpt_d = 6;
            const int c_wwpth = 7;
            const int c_wwpt = 8;
            const int c_wwpt_d = 9;
            const int c_wwith = 10;
            const int c_wwit = 11;
            const int c_wwit_d = 12;
            const int c_wgpth = 13;
            const int c_wgpt = 14;
            const int c_wgpt_d = 15;
            const int c_wgith = 16;
            const int c_wgit = 17;
            const int c_wgit_d = 18;
            const int c_woprh = 19;
            const int c_wopr = 20;
            const int c_wopr_d = 21;
            const int c_wlprh = 22;
            const int c_wlpr = 23;
            const int c_wlpr_d = 24;
            const int c_wwprh = 25;
            const int c_wwpr = 26;
            const int c_wwpr_d = 27;
            const int c_wwirh = 28;
            const int c_wwir = 29;
            const int c_wwir_d = 30;
            const int c_wgprh = 31;
            const int c_wgpr = 32;
            const int c_wgpr_d = 33;
            const int c_wgirh = 34;
            const int c_wgir = 35;
            const int c_wgir_d = 36;

            double[] totals_wells_all = new double[v_count];
            double[] totals_wells_target = new double[v_count];
            for (int i = 0; i < v_count; ++i) { totals_wells_all[i] = 0; totals_wells_target[i] = 0; }

            int r_first = 0;
            //Parallel.For(0, wellnames.Length, w =>
            for (int w = 0; w < wellnames.Length; ++w)
            {
                this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wellname].Value = wellnames[w];
                double[] values = rows[wellnames[w]];
                if (values[v_wlpth] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopth].Value = Round(values[v_wopth]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt].Value = Round(values[v_wopt]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt_d].Value = Round(DeltaPercent(values[v_wopth], values[v_wopt]));
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpth].Value = Round(values[v_wlpth]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt].Value = Round(values[v_wlpt]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt_d].Value = Round(DeltaPercent(values[v_wlpth], values[v_wlpt]));
                    //\\//\\
                    if (values[v_wopth] > 0)
                    {
                        totals_wells_all[v_wopth]++;
                        if (values[v_wopt_d] <= wopt_d) totals_wells_all[v_wopt]++;
                        else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt_d].Style.ForeColor = Color.Red;
                    }
                    totals_wells_all[v_wlpth]++;
                    if (values[v_wlpt_d] <= wlpt_d) totals_wells_all[v_wlpt]++;
                    else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt_d].Style.ForeColor = Color.Red;
                    if (IsTarget(values[v_wopth], wopth_list, wells_targ))
                    {
                        totals_wells_target[v_wopth]++;
                        if (values[v_wopt_d] <= wopt_d) totals_wells_target[v_wopt]++;
                        totals_wells_target[v_wlpth]++;
                        if (values[v_wlpt_d] <= wlpt_d) totals_wells_target[v_wlpt]++;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopth].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt_d].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpth].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt_d].Style.BackColor = Color.GreenYellow;
                    }
                }
                if (values[v_wwith] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwith].Value = Round(values[v_wwith]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwit].Value = Round(values[v_wwit]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwit_d].Value = Round(DeltaPercent(values[v_wwith], values[v_wwit]));
                    //\\//\\
                    totals_wells_all[v_wwith]++;
                    totals_wells_target[v_wwith]++;
                    if (values[v_wwit_d] <= wwit_d)
                    {
                        totals_wells_all[v_wwit]++;
                        totals_wells_target[v_wwit]++;
                    }
                }
                if (values[v_wlprh] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_woprh].Value = Round(values[v_woprh]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr].Value = Round(values[v_wopr]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr_d].Value = Round(DeltaPercent(values[v_woprh], values[v_wopr]));
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlprh].Value = Round(values[v_wlprh]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr].Value = Round(values[v_wlpr]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr_d].Value = Round(DeltaPercent(values[v_wlprh], values[v_wlpr]));
                    //\\//\\
                    if (values[v_woprh] > 0)
                    {
                        totals_wells_all[v_woprh]++;
                        if (values[v_wopr_d] <= wopr_d) totals_wells_all[v_wopr]++;
                        else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr_d].Style.ForeColor = Color.Red;
                    }
                    totals_wells_all[v_wlprh]++;
                    if (values[v_wlpr_d] <= wlpr_d) totals_wells_all[v_wlpr]++;
                    else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr_d].Style.ForeColor = Color.Red;
                    if (IsTarget(values[v_woprh], woprh_list, wells_targ))
                    {
                        totals_wells_target[v_woprh]++;
                        if (values[v_wopr_d] <= wopr_d) totals_wells_target[v_wopr]++;
                        totals_wells_target[v_wlprh]++;
                        if (values[v_wlpr_d] <= wlpr_d) totals_wells_target[v_wlpr]++;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_woprh].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr_d].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlprh].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr_d].Style.BackColor = Color.GreenYellow;
                    }
                }
                if (values[v_wwirh] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwirh].Value = Round(values[v_wwirh]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwir].Value = Round(values[v_wwir]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwir_d].Value = Round(DeltaPercent(values[v_wwirh], values[v_wwir]));
                    //\\//\\
                    totals_wells_all[v_wwirh]++;
                    totals_wells_target[v_wwirh]++;
                    if (values[v_wwir_d] <= wwir_d)
                    {
                        totals_wells_all[v_wwir]++;
                        totals_wells_target[v_wwir]++;
                    }
                    else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwir_d].Style.ForeColor = Color.Red;
                }
            }//);

            this.dataGridView_hm_table_total.Rows.Clear();
            this.dataGridView_hm_table_total.Rows.Add(3);

            double foprh = result.WOPRH(wellnames).Values.Last();
            double fopr = result.WOPR(wellnames).Values.Last();
            double flprh = result.WLPRH(wellnames).Values.Last();
            double flpr = result.WLPR(wellnames).Values.Last();
            //
            double fopth = result.WOPTH(wellnames).Values.Last();
            double fopt = result.WOPT(wellnames).Values.Last();
            double flpth = result.WLPTH(wellnames).Values.Last();
            double flpt = result.WLPT(wellnames).Values.Last();
            //
            double fwirh = result.WWIRH(wellnames).Values.Last();
            double fwir = result.WWIR(wellnames).Values.Last();
            double fwith = result.WWITH(wellnames).Values.Last();
            double fwit = result.WWIT(wellnames).Values.Last();
            
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wellname].Value = "Итого";
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopth].Value = Round(fopth) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopt].Value = Round(fopt) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopt_d].Value = Round(DeltaPercent(fopth, fopt));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpth].Value = Round(flpth) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpt].Value = Round(flpt) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpt_d].Value = Round(DeltaPercent(flpth, flpt));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwith].Value = Round(fwith) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwit].Value = Round(fwit) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwit_d].Value = Round(DeltaPercent(fwith, fwit));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_woprh].Value = Round(foprh);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopr].Value = Round(fopr);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopr_d].Value = Round(DeltaPercent(foprh, fopr));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlprh].Value = Round(flprh);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpr].Value = Round(flpr);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpr_d].Value = Round(DeltaPercent(flprh, flpr));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwirh].Value = Round(fwirh);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwir].Value = Round(fwir);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwir_d].Value = Round(DeltaPercent(fwirh, fwir));

            this.dataGridView_hm_table_total.Rows[1].Cells[c_wellname].Value = "Весь фонд";
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopth].Value = totals_wells_all[v_wopth];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopt].Value = totals_wells_all[v_wopt];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopt_d].Value = Round(DivPercent(totals_wells_all[v_wopth], totals_wells_all[v_wopt]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpth].Value = totals_wells_all[v_wlpth];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpt].Value = totals_wells_all[v_wlpt];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpt_d].Value = Round(DivPercent(totals_wells_all[v_wlpth], totals_wells_all[v_wlpt]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwith].Value = totals_wells_all[v_wwith];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwit].Value = totals_wells_all[v_wwit];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwit_d].Value = Round(DivPercent(totals_wells_all[v_wwith], totals_wells_all[v_wwit]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_woprh].Value = totals_wells_all[v_woprh];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopr].Value = totals_wells_all[v_wopr];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopr_d].Value = Round(DivPercent(totals_wells_all[v_woprh], totals_wells_all[v_wopr]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlprh].Value = totals_wells_all[v_wlprh];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpr].Value = totals_wells_all[v_wlpr];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpr_d].Value = Round(DivPercent(totals_wells_all[v_wlprh], totals_wells_all[v_wlpr]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwirh].Value = totals_wells_all[v_wwirh];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwir].Value = totals_wells_all[v_wwir];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwir_d].Value = Round(DivPercent(totals_wells_all[v_wwirh], totals_wells_all[v_wwir]));

            this.dataGridView_hm_table_total.Rows[2].Cells[c_wellname].Value = "Целевой фонд";
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopth].Value = totals_wells_target[v_wopth];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopt].Value = totals_wells_target[v_wopt];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopt_d].Value = Round(DivPercent(totals_wells_target[v_wopth], totals_wells_target[v_wopt]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpth].Value = totals_wells_target[v_wlpth];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpt].Value = totals_wells_target[v_wlpt];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpt_d].Value = Round(DivPercent(totals_wells_target[v_wlpth], totals_wells_target[v_wlpt]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwith].Value = totals_wells_target[v_wwith];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwit].Value = totals_wells_target[v_wwit];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwit_d].Value = Round(DivPercent(totals_wells_target[v_wwith], totals_wells_target[v_wwit]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_woprh].Value = totals_wells_target[v_woprh];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopr].Value = totals_wells_target[v_wopr];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopr_d].Value = Round(DivPercent(totals_wells_target[v_woprh], totals_wells_target[v_wopr]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlprh].Value = totals_wells_target[v_wlprh];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpr].Value = totals_wells_target[v_wlpr];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpr_d].Value = Round(DivPercent(totals_wells_target[v_wlprh], totals_wells_target[v_wlpr]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwirh].Value = totals_wells_target[v_wwirh];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwir].Value = totals_wells_target[v_wwir];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwir_d].Value = Round(DivPercent(totals_wells_target[v_wwirh], totals_wells_target[v_wwir]));

            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwith].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwit].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_woprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwirh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwir].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwith].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwit].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_woprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwirh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwir].Style = dataGridViewCellStyle_wells;

            this.dataGridView_hm_table_wells.Sort(this.Column_wells_wopth, ListSortDirection.Descending);
        }



        bool IsTarget(double value, double[] values, double percent)
        {
            if (values.Length == 0) return false;
            double sum_pecent = values.Sum() * percent / 100;
            values = values.OrderBy(x => x).Reverse().ToArray();
            double sum = 0;
            foreach (double v in values)
            {
                sum += v;
                if (sum >= sum_pecent)
                    if (value >= v) return true;
                    else return false;
            }
            return false;
        }


        static double DeltaPercent(double hist, double fact)
        {
            return (hist == 0) ? 0 : (fact - hist) / hist * 100;
        }

        static double DivPercent(double hist, double fact)
        {
            return (hist == 0) ? 0 : fact / hist * 100;
        }


        void UpdateView2D(Grid grid, Dictionary<string, ActProp> set_props, Dictionary<string, ActProp> iter_props, IterationResult result)
        {
            if (grid.Specified() && set_props.Count > 0 && iter_props.Count > 0)
            {
                grid.Compdats = result.Compdats.Values.ToList();
                List<ActProp> temp = new List<ActProp>();
                temp.AddRange(set_props.Values);
                temp.AddRange(iter_props.Values);
                propView2DControl.UpdateData(grid, temp.ToArray());
                propView2DControl.SelectedLayers = leyers_selected;
                propView2DControl.SelectedProp = prop_selected;
            }
            else
            {
                propView2DControl.Clear();
            }
        }


        void Msg(string msg)
        {
            try
            {
                this.richTextBox_log?.BeginInvoke(new System.Action(() =>
                {
                    string text = HistMatching.DisplayLine(DateTime.Now, msg) + Environment.NewLine;
                    richTextBox_log?.AppendText(text);
                    richTextBox_log?.Select(richTextBox_log.Text.Length - 1, 0);
                    richTextBox_log?.ScrollToCaret();
                }));
            }
            catch { }
        }



        const string kw_field = "FIELD";
        void UpdateAll()
        {
            Iteration iter = Downloader.Data[_results_pxlhm_file].Sets[_results_id_folder].Iterations[_results_iter_number];
            well_names = iter.Result.WellNames.ToArray();

            UpdateWells(this.listBox_wg_wells, well_names, well_selected);
            UpdateWells(this.listBox_ha_wells, well_names, well_selected);
            //UpdateWells(this.listBox_sdi_wells, well_names, well_selected);
            UpdateWells(this.listBox_cs_wells, well_names, well_selected);
            UpdateDates();
            UpdateSatnum();
            UpdateAllGraphs();
        }



        static void UpdateWells(ListBox listBox, string[] well_names, int well_selected)
        {
            listBox.BeginInvoke(new System.Action(() =>
            {
                listBox.Items.Clear();
                if (well_names.Length > 0)
                {
                    listBox.Items.Add(kw_field);
                    foreach (string name in well_names)
                        listBox.Items.Add(name);
                    listBox.SelectedIndex = Helper.Bound(0, well_selected, listBox.Items.Count - 1);
                }
            }));
        }



        void UpdateDates()
        {
            comboBox_dates_cp.BeginInvoke(new System.Action(() =>
            {
                this.comboBox_dates_cp.Items.Clear();
                Iteration iter = Downloader.Data[_results_pxlhm_file].Sets[_results_id_folder].Iterations[_results_iter_number];
                dates = iter.Result.Dates;

                if (dates.Length > 0)
                {
                    foreach (DateTime dt in dates)
                        this.comboBox_dates_cp.Items.Add(Helper.ShowDateTimeShort(dt));
                    this.comboBox_dates_cp.SelectedIndex = Helper.Bound(0, dt_selected, this.comboBox_dates_cp.Items.Count - 1);
                }
            }));
            trackBar_dates_cp.BeginInvoke(new System.Action(() =>
            {
                this.trackBar_dates_cp.Maximum = dates.Length - 1;
            }));       
        }



        void UpdateSatnum()
        {
            satnum_names = Array.Empty<string>();
            this.listBox_satnum.BeginInvoke(new System.Action(() =>
            {
                this.listBox_satnum.Items.Clear();
                Iteration iter = Downloader.Data[_results_pxlhm_file].Sets[_results_id_folder].Iterations[_results_iter_number];
                int nsat = iter.CoreySet.Tables.Count;
                satnum_names = new string[nsat];
                if (nsat > 0)
                {
                    for (int i = 0; i < nsat; ++i)
                    {
                        string name = $"SATNUM#{i + 1}";
                        satnum_names[i] = name;
                        this.listBox_satnum.Items.Add(name);
                    }
                    this.listBox_satnum.SelectedIndex = Helper.Bound(0, satnum_selected, this.listBox_satnum.Items.Count - 1);
                }
            }));
        }



        static IEnumerable<System.Windows.Forms.Control> CollectControls(System.Windows.Forms.Control.ControlCollection collection)
        {
            foreach (System.Windows.Forms.Control control in collection)
            {
                yield return control;
                foreach (var child in CollectControls(control.Controls))
                    yield return child;
            }
        }


        void SetGraphFont()
        {
            foreach (var control in CollectControls(this.Controls).Where(v => v is ZedGraphControl).Select(v => v as ZedGraphControl))
            {
                control.Font = new Font(control.Font.FontFamily, Settings.GraphFontSpec);
                //control.GraphPane.fo
            }
        }


        void UpdateAllGraphs()
        {
            //SetGraphFont();

            if (well_selected < 0 || comboBox_dates_cp.SelectedIndex < 0)
                return;

            this.listBox_wg_wells.SelectedIndex = well_selected;
            this.listBox_ha_wells.SelectedIndex = well_selected;
            //this.listBox_sdi_wells.SelectedIndex = well_selected;
            this.listBox_cs_wells.SelectedIndex = well_selected;

            string wellname = well_selected == 0 ? kw_field : well_names[well_selected - 1];
            string[] wlist = (wellname == kw_field) ? well_names : new string[] { wellname };            

            DateTime dt = dates[comboBox_dates_cp.SelectedIndex];

            ResultsViewDataOpt view_data = Downloader.Data[_results_pxlhm_file];
            IterationSet set = view_data.Sets[_results_id_folder];
            Iteration[] iterations = set.Iterations.Values.ToArray();
            Iteration iteration = set.Iterations[_results_iter_number];
            IterationResult iter_result = iteration.Result;
            Dictionary<string, ActProp> iter_props = iteration.Props.Items;
            Dictionary<string, ActProp> set_props = set.Props.Items;
            bool show_min_max = this.checkBox_min_max.Checked;

            if (tabControl.SelectedTab == tabPage_well_graphs)
            {
                UpdateWellGraphs(iter_result, set.ResPressTable, wlist);
            }
            else if (tabControl.SelectedTab == tabPage_cross_plots)
            {
                UpdateCrossPlots(iter_result, set.ResPressTable);
            }
            else if (tabControl.SelectedTab == tabPage_hm_analyse)
            {
                UpdateHMAnalisyseGraphs(iterations, wlist);
            }
            /*
            else if (tabControl.SelectedTab == tabPage_stdev_item)
            {
                UpdateStDevsGraphs(iterations, wlist, set.ResPressTable);
            }
            */
            else if (tabControl.SelectedTab == tabPage_table)
            {
                UpdateTable(iter_result);
                //UpdateTable2(iteration);
            }
            else if (tabControl.SelectedTab == tabPage_view2d)
            {
                UpdateView2D(set.Grid, set_props, iter_props, iter_result);
            }
            else if (tabControl.SelectedTab == this.tabPage_cross_sect)
            {
                UpdateCrossSectGraphs2(set, iteration, wellname, dt, show_min_max);
            }
            else if (tabControl.SelectedTab == this.tabPage_r2_iter)
            {
                UpdateAnalyseGraphs(iterations, set.ResPressTable);
            }
            else if (tabControl.SelectedTab == this.tabPage_relperm)
            {
                Iteration prev_iteration = null;
                int iter_number = Helper.ParseInt(_results_iter_number);
                if (iter_number > 0)
                    prev_iteration = set.Iterations[HistMatching.ShowIter(iter_number - 1)];
                if (set_props.ContainsKey(HistMatching.SatnumTitle))
                    UpdateRelPermGraphs(iteration, prev_iteration, set_props[HistMatching.SatnumTitle], set.Grid.Actnum);
            }
            else if (tabControl.SelectedTab == this.tabPage_info)
            {
                this.propertyGrid_info.SelectedObject = iteration.InputData;
            }
            else if (tabControl.SelectedTab == this.tabPage_uparam)
            {
                UpdateUparamsGraphs(iterations, set.ResPressTable);
            }
        }




        private void importRSMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("PEXEL HM Project Files (*.{0})|*.{0}|All Files (*.*)|*.*", HistMatchingProject.Identifier);
            dialog.Multiselect = false;
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
                Open(dialog.FileName);
            */
            Open();
        }


        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = string.Format("PEXEL HM Project Files (*.{0})|*.{0}|All Files (*.*)|*.*", HistMatchingProject.Identifier);
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ProjectFile = dialog.FileName;
                Save();
            }
            */
            Save();
        }


        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }



        private void clearAllDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }



        void Clear()
        {
            Downloader.Data = new Dictionary<string, ResultsViewDataOpt>();
            iter_names = Array.Empty<string>();
            well_names = Array.Empty<string>();
            dates = Array.Empty<DateTime>();
            leyers_selected = Array.Empty<int>();
            well_selected = -1;
            dt_selected = -1;
         }




        private void listBox_wells_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (sender as ListBox);
            if (well_selected == listBox.SelectedIndex)
                return;
            well_selected = listBox.SelectedIndex;
            UpdateAllGraphs();
        }

        private void comboBox_dates_cp_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt_selected = comboBox_dates_cp.SelectedIndex;
            trackBar_dates_cp.Value = comboBox_dates_cp.SelectedIndex;
            UpdateAllGraphs();
        }

        private void trackBar_dates_cp_Scroll(object sender, EventArgs e)
        {
            this.comboBox_dates_cp.SelectedIndex = trackBar_dates_cp.Value;
        }


        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = @"Do you really want to close the Results Viewer?";
            if (e.CloseReason == CloseReason.UserClosing &&
                MessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            /*
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
            */
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAllGraphs();
        }

        private void toolStripButton_refrash_Click(object sender, EventArgs e)
        {
            //Refrash(false);
        }


        void ShowState(string msg)
        {
            this.toolStripStatusLabel.Text = msg;
        }



        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resultsViewSettingsForm.Settings = this.Settings;
            resultsViewSettingsForm.Show(this);
        }

        private void ResultsViewSettingsForm_ApplyEvent(object sender, EventArgs e)
        {
            Downloader.Data[_results_pxlhm_file].Settings = this.Settings;
            this.Settings.Save(SettingsFile); // _results_pxlhm_file
            UpdateAllGraphs();
        }

        private void zedGraphControl_cp_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            //UpdateCrossPlotTag(ref sender);
        }


        private void checkBox_log_scale_cp_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl_cs_perm.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            zedGraphControl_cs_permmult.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            zedGraphControl_cs_satnum.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            zedGraphControl_cs_krorw.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            zedGraphControl_cs_krorwmult.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            zedGraphControl_cs_krwr.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            zedGraphControl_cs_krwrmult.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            zedGraphControl_cs_multpv.GraphPane.XAxis.Type = checkBox_log_scale_cp.Checked ? AxisType.Log : AxisType.Linear;
            //
            zedGraphControl_cs_perm.GraphPane.AxisChange();
            zedGraphControl_cs_permmult.GraphPane.AxisChange();
            zedGraphControl_cs_satnum.GraphPane.AxisChange();
            zedGraphControl_cs_krorw.GraphPane.AxisChange();
            zedGraphControl_cs_krorwmult.GraphPane.AxisChange();
            zedGraphControl_cs_krwr.GraphPane.AxisChange();
            zedGraphControl_cs_krwrmult.GraphPane.AxisChange();
            zedGraphControl_cs_multpv.GraphPane.AxisChange();
            //
            zedGraphControl_cs_perm.Invalidate();
            zedGraphControl_cs_permmult.Invalidate();
            zedGraphControl_cs_satnum.Invalidate();
            zedGraphControl_cs_krorw.Invalidate();
            zedGraphControl_cs_krorwmult.Invalidate();
            zedGraphControl_cs_krwr.Invalidate();
            zedGraphControl_cs_krwrmult.Invalidate();
            zedGraphControl_cs_multpv.Invalidate();
        }



        private void checkBox_totals_cp_CheckedChanged(object sender, EventArgs e)
        {
            /*
            zedGraphControl_opt_cs.GraphPane.Title.Text = checkBox_totals_cp.Checked ? "Oil Prod Total" : "Oil Prod Rate";
            zedGraphControl_lpt_cs.GraphPane.Title.Text = checkBox_totals_cp.Checked ? "Liq Prod Total" : "Liq Prod Rate";
            zedGraphControl_wct_cs.GraphPane.Title.Text = checkBox_totals_cp.Checked ? "Cum Watercut" : "Cur Watercut";
            zedGraphControl_wit_cs.GraphPane.Title.Text = checkBox_totals_cp.Checked ? "Wat Inje Total" : "Wat Inje Rate";
            //
            UpdateAllGraphs();
            */
        }

        private void checkBox_min_max_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAllGraphs();
        }





        Color base_color = Color.Black;
        Color case_color = Color.Red;

        void UpdateRelPermGraphs(Iteration iteration, Iteration prev_iteration, ActProp satnum, Actnum actnum)
        {
            if (satnum_selected < 0)
                return;
            
            int digits = 6;
            zedGraphControl_lpt_vs_wct.GraphPane.CurveList.Clear();
            zedGraphControl_distancies.GraphPane.CurveList.Clear();
            zedGraphControl_swof.GraphPane.CurveList.Clear();
            this.pictureBox_relperm_points.Image = null;
            this.pictureBox_relperm_points.Invalidate();

            if (prev_iteration != null && prev_iteration.CoreySet.Tables.Count > 0)
            {
                prev_iteration.Result.GetRegData_fix(satnum, actnum, 
                                                    Enumerable.Range(1, prev_iteration.CoreySet.Tables.Count).ToArray(), 
                                                    out double[][] opths, out double[][] wpths, out double[][] opts, out double[][] wpts);
                //
                CoreyTable selectedTable = prev_iteration.CoreySet.Tables[satnum_selected];
                RelPermAnalyzerResults analyzer = new RelPermAnalyzerResults(selectedTable, opths[satnum_selected], wpths[satnum_selected], opts[satnum_selected], wpts[satnum_selected]);
                //
                this.textBox_aver_value.Text = Helper.ShowDouble(analyzer.AverDistanceGraph, digits);
                this.textBox_poly_value.Text = Helper.ShowDouble(analyzer.PolyDistanceGraph, digits);
                this.textBox_aver_mult.Text = Helper.ShowDouble(RelPermAnalyzerResults.AverMult, digits);
                this.textBox_poly_mult.Text = Helper.ShowDouble(RelPermAnalyzerResults.PolyMult, digits);
                this.textBox_aver_result.Text = Helper.ShowDouble(analyzer.AverDistanceResult, digits);
                this.textBox_poly_result.Text = Helper.ShowDouble(analyzer.PolyDistanceResult, digits);
                this.textBox_now_cur.Text = Helper.ShowDouble(analyzer.NOW, digits);
                this.textBox_nw_cur.Text = Helper.ShowDouble(analyzer.NW, digits);
                this.textBox_now_prev.Text = Helper.ShowDouble(prev_iteration.CoreySet.Tables[satnum_selected].NOW, digits);
                this.textBox_nw_prev.Text = Helper.ShowDouble(prev_iteration.CoreySet.Tables[satnum_selected].NW, digits);
                //
                this.pictureBox_relperm_points.Image = analyzer.Curves(base_color, case_color);
                //
                zedGraphControl_lpt_vs_wct.GraphPane.AddCurve("Hist", analyzer.LPTH_scaled, analyzer.WCTH, base_color, SymbolType.None);
                zedGraphControl_lpt_vs_wct.GraphPane.AddCurve("Model", analyzer.LPT_scaled, analyzer.WCT, case_color, SymbolType.None);
                zedGraphControl_lpt_vs_wct.GraphPane.AxisChange();
                //
                analyzer.NextTable.UpdateSWOFGraph(ref zedGraphControl_swof);
                //
                zedGraphControl_distancies.GraphPane.AddBar("", analyzer.DistanciesGraph, null, Color.Black);
                zedGraphControl_distancies.GraphPane.YAxis.MajorTic.IsBetweenLabels = false;
                zedGraphControl_distancies.GraphPane.AddCurve(string.Empty, analyzer.AverDistanciesGraph, null, Color.Red, SymbolType.None);
                zedGraphControl_distancies.GraphPane.AddCurve(string.Empty, analyzer.PolyDistanciesGraph, null, Color.Blue, SymbolType.None);
                zedGraphControl_distancies.GraphPane.BarSettings.Base = BarBase.Y;
                zedGraphControl_distancies.GraphPane.BarSettings.Type = BarType.SortedOverlay;
                zedGraphControl_distancies.GraphPane.AxisChange();        
            }
            else if (iteration != null && iteration.CoreySet.Tables.Count > 0)
            {
                this.textBox_aver_value.Text = string.Empty;
                this.textBox_poly_value.Text = string.Empty;
                this.textBox_aver_mult.Text = string.Empty;
                this.textBox_poly_mult.Text = string.Empty;
                this.textBox_aver_result.Text = string.Empty;
                this.textBox_poly_result.Text = string.Empty;
                this.textBox_now_cur.Text = Helper.ShowDouble(iteration.CoreySet.Tables[satnum_selected].NOW, digits);
                this.textBox_nw_cur.Text = Helper.ShowDouble(iteration.CoreySet.Tables[satnum_selected].NW, digits);
                this.textBox_now_prev.Text = string.Empty;
                this.textBox_nw_prev.Text = string.Empty;
            }
            zedGraphControl_swof.Invalidate();
            zedGraphControl_lpt_vs_wct.Invalidate();
            zedGraphControl_distancies.Invalidate();

        }



        private void listBox_satnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            satnum_selected = listBox_satnum.SelectedIndex;
            UpdateAllGraphs();
        }

        private void checkedListBox_cross_sect_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateAllGraphs();
        }

        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("PEXEL HM Project Files (*.{0})|*.{0}|All Files (*.*)|*.*", HistMatching.Identifier);
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileNames.Length > 0)
                Downloader.AddFiles(dialog.FileNames);
        }






        string _results_pxlhm_file;
        string _results_id_folder;
        string _results_iter_number;

        string _selected_pxlhm_file;
        string _selected_id_folder;

        TreeNode prev_selected = new TreeNode();

        private void treeView_project_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolStripButton_add_child.Enabled = false;
            toolStripButton_remove.Enabled = false;

            if (treeView_project.SelectedNode == null) 
                return;

            var tag = treeView_project.SelectedNode.Tag as Tuple<int, string>;
            if (tag.Item1 == ResultsViewTreeDataDownloader.CS_TAG_KEY)
            {
                toolStripButton_remove.Enabled = true;
                _selected_pxlhm_file = tag.Item2;
            }
            else if (tag.Item1 == ResultsViewTreeDataDownloader.ID_TAG_KEY)
            {
                var ptag = treeView_project.SelectedNode.Parent.Tag as Tuple<int, string>;
                _selected_pxlhm_file = ptag.Item2;
                _selected_id_folder = tag.Item2;
            }
            else if (tag.Item1 == ResultsViewTreeDataDownloader.IT_TAG_KEY)
            {
                this.toolStripButton_add_child.Enabled = true;
                //
                prev_selected.ForeColor = SystemColors.WindowText;
                treeView_project.SelectedNode.ForeColor = Color.Red;
                prev_selected = treeView_project.SelectedNode;
                //
                var ptag = treeView_project.SelectedNode.Parent.Tag as Tuple<int, string>;
                var pptag = treeView_project.SelectedNode.Parent.Parent.Tag as Tuple<int, string>;
                _results_pxlhm_file = pptag.Item2;
                _results_id_folder = ptag.Item2;
                _results_iter_number = tag.Item2;
                //
                leyers_selected = propView2DControl.SelectedLayers;
                prop_selected = propView2DControl.SelectedProp;
                //
                ResultsViewDataOpt view_data = Downloader.Data[_results_pxlhm_file];
                this.Settings = view_data.Settings;
                this.SettingsFile = view_data.SettingsFile;
                //
                UpdateAll();
            }
        }



        private void toolStripButton_wizard_Click(object sender, EventArgs e)
        {
#if !DEBUG
            ProcessStartInfo info = 
                new ProcessStartInfo(System.Reflection.Assembly.GetEntryAssembly().Location, string.Join(" ", Program.WizardArg, Downloader.ID)); 
            Process.Start(info);
#else
            Thread thread = new Thread(new ThreadStart(() =>
            {
                WizardForm wizard = new WizardForm() { DownloaderID = Downloader.ID };
                //wizard.RunStartedEvent += Downloader.AddFiles;
                Application.Run(wizard);
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
#endif
        }




        private void toolStripButton_add_child_Click(object sender, EventArgs e)
        {
            CreateDataDialog();
        }


        void CreateDataDialog()
        {
            CreateDataTreeForm createDataTreeForm = new CreateDataTreeForm();
            createDataTreeForm.UpdateData(Downloader.Data[_results_pxlhm_file].Sets[_results_id_folder].Grid,
                                          Downloader.Data[_results_pxlhm_file].Sets[_results_id_folder].Iterations[_results_iter_number],
                                          _results_pxlhm_file, _results_id_folder, _results_iter_number);
            createDataTreeForm.DataCreatedEvent += Downloader.AddFiles;
            createDataTreeForm.MsgEvent += Msg;
            createDataTreeForm.ShowDialog(this);
        }



        private void toolStripButton_remove_Click(object sender, EventArgs e)
        {
            string message = $"Do you really want to remove the run  '{_selected_pxlhm_file}' from tree view?";
            if (MessageBox.Show(message, this.Text, MessageBoxButtons.YesNoCancel) != DialogResult.Yes) 
                return;
            Downloader.RemoveFiles(_selected_pxlhm_file);
        }

        private void hFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HFileForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void coreyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new Pexel.SCAL.CoreySetForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void tableMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HMTableForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }












        void UpdateTable2(Iteration iteration)
        {
            HistMatchingInput input = iteration.InputData;
            ShowColumn( input.LiqProdRequested || input.OilInjeRequested,
                        input.LiqProdRequested || input.WatInjeRequested,
                        input.GasProdRequested || input.GasInjeRequested,
                        input.LiqProdRequested);

            IterationResult result = iteration.Result;
            //if (niter < 0) return;
            double wopr_d = Settings.OPRDeltaPercent;
            double wlpr_d = Settings.LPRDeltaPercent;
            double wwir_d = Settings.WIRDeltaPercent;
            double wopt_d = Settings.OPTDeltaPercent;
            double wlpt_d = Settings.LPTDeltaPercent;
            double wwit_d = Settings.WITDeltaPercent;
            double wells_targ = Settings.TargetWellsPercent;

            Dictionary<string, double[]> rows = new Dictionary<string, double[]>();

            const int v_wopth = 0;
            const int v_wopt = 1;
            const int v_wopt_d = 2;
            const int v_wlpth = 3;
            const int v_wlpt = 4;
            const int v_wlpt_d = 5;
            const int v_wwpth = 6;
            const int v_wwpt = 7;
            const int v_wwpt_d = 8;
            const int v_wwith = 9;
            const int v_wwit = 10;
            const int v_wwit_d = 11;
            const int v_wgpth = 12;
            const int v_wgpt = 13;
            const int v_wgpt_d = 14;
            const int v_wgith = 15;
            const int v_wgit = 16;
            const int v_wgit_d = 17;
            const int v_woprh = 18;
            const int v_wopr = 19;
            const int v_wopr_d = 20;
            const int v_wlprh = 21;
            const int v_wlpr = 22;
            const int v_wlpr_d = 23;
            const int v_wwprh = 24;
            const int v_wwpr = 25;
            const int v_wwpr_d = 26;
            const int v_wwirh = 27;
            const int v_wwir = 28;
            const int v_wwir_d = 29;
            const int v_wgprh = 30;
            const int v_wgpr = 31;
            const int v_wgpr_d = 32;
            const int v_wgirh = 33;
            const int v_wgir = 34;
            const int v_wgir_d = 35;
            const int v_count = 36;

            string[] wellnames = result.WellNames.ToArray();

            double[] wopth_list = new double[wellnames.Length];
            double[] woprh_list = new double[wellnames.Length];

            this.dataGridView_hm_table_wells.Rows.Clear();
            this.dataGridView_hm_table_wells.Rows.Add(wellnames.Length);

            for (int w = 0; w < wellnames.Length; ++w)
            {
                double wopt = result.WOPT(wellnames[w]).Values.Last();
                double wopth = result.WOPTH(wellnames[w]).Values.Last();
                double wlpt = result.WLPT(wellnames[w]).Values.Last();
                double wlpth = result.WLPTH(wellnames[w]).Values.Last();
                double wwpt = result.WWPT(wellnames[w]).Values.Last();
                double wwpth = result.WWPTH(wellnames[w]).Values.Last();
                double wwit = result.WWIT(wellnames[w]).Values.Last();
                double wwith = result.WWITH(wellnames[w]).Values.Last();
                double wgpt = result.WGPT(wellnames[w]).Values.Last();
                double wgpth = result.WGPTH(wellnames[w]).Values.Last();
                double wgit = result.WGIT(wellnames[w]).Values.Last();
                double wgith = result.WGITH(wellnames[w]).Values.Last();

                double wopr = result.WOPR(wellnames[w]).Values.Last();
                double woprh = result.WOPRH(wellnames[w]).Values.Last();
                double wlpr = result.WLPR(wellnames[w]).Values.Last();
                double wlprh = result.WLPRH(wellnames[w]).Values.Last();
                double wwpr = result.WWPR(wellnames[w]).Values.Last();
                double wwprh = result.WWPRH(wellnames[w]).Values.Last();
                double wwir = result.WWIR(wellnames[w]).Values.Last();
                double wwirh = result.WWIRH(wellnames[w]).Values.Last();
                double wgpr = result.WGPR(wellnames[w]).Values.Last();
                double wgprh = result.WGPRH(wellnames[w]).Values.Last();
                double wgir = result.WGIR(wellnames[w]).Values.Last();
                double wgirh = result.WGIRH(wellnames[w]).Values.Last();

                double[] temp = new double[v_count];
                temp[v_wopth] = wopth / 1000;
                temp[v_wopt] = wopt / 1000;
                temp[v_wopt_d] = (wopth == 0) ? 0 : Math.Abs(wopt - wopth) / wopth * 100;
                temp[v_wlpth] = wlpth / 1000;
                temp[v_wlpt] = wlpt / 1000;
                temp[v_wlpt_d] = (wlpth == 0) ? 0 : Math.Abs(wlpt - wlpth) / wlpth * 100;
                temp[v_wwpth] = wwpth / 1000;
                temp[v_wwpt] = wwpt / 1000;
                temp[v_wwpt_d] = (wwpth == 0) ? 0 : Math.Abs(wwpt - wwpth) / wwpth * 100;
                temp[v_wwith] = wwith / 1000;
                temp[v_wwit] = wwit / 1000;
                temp[v_wwit_d] = (wwith == 0) ? 0 : Math.Abs(wwit - wwith) / wwith * 100;
                temp[v_wgpth] = wgpth / 1000000;
                temp[v_wgpt] = wgpt / 1000000;
                temp[v_wgpt_d] = (wgpth == 0) ? 0 : Math.Abs(wgpt - wgpth) / wgpth * 100;
                temp[v_wgith] = wgith / 1000000;
                temp[v_wgit] = wgit / 1000000;
                temp[v_wgit_d] = (wgith == 0) ? 0 : Math.Abs(wgit - wgith) / wgith * 100;
                temp[v_woprh] = woprh;
                temp[v_wopr] = wopr;
                temp[v_wopr_d] = (woprh == 0) ? 0 : Math.Abs(wopr - woprh) / woprh * 100;
                temp[v_wlprh] = wlprh;
                temp[v_wlpr] = wlpr;
                temp[v_wlpr_d] = (wlprh == 0) ? 0 : Math.Abs(wlpr - wlprh) / wlprh * 100;
                temp[v_wwprh] = wwprh;
                temp[v_wwpr] = wwpr;
                temp[v_wwpr_d] = (wwprh == 0) ? 0 : Math.Abs(wwpr - wwprh) / wwprh * 100;
                temp[v_wwirh] = wwirh;
                temp[v_wwir] = wwir;
                temp[v_wwir_d] = (wwirh == 0) ? 0 : Math.Abs(wwir - wwirh) / wwirh * 100;
                temp[v_wgprh] = wgprh;
                temp[v_wgpr] = wgpr;
                temp[v_wgpr_d] = (wgprh == 0) ? 0 : Math.Abs(wgpr - wgprh) / wgprh * 100;
                temp[v_wgirh] = wgirh;
                temp[v_wgir] = wgir;
                temp[v_wgir_d] = (wgirh == 0) ? 0 : Math.Abs(wgir - wgirh) / wgirh * 100;

                wopth_list[w] = temp[v_wopth];
                woprh_list[w] = temp[v_woprh];

                string clear_name = wellnames[w];
                if (Settings.WellSufix.Length > 0 && wellnames[w].Substring(Math.Max(0, wellnames[w].Length - Settings.WellSufix.Length)) == Settings.WellSufix)
                    clear_name = wellnames[w].Substring(0, wellnames[w].Length - Settings.WellSufix.Length);

                lock (rows)
                {
                    if (rows.ContainsKey(clear_name))
                    {
                        double[] temp2 = rows[clear_name];
                        for (int i = 0; i < v_count; ++i) temp[i] += temp2[i];
                        rows[clear_name] = temp;
                    }
                    else
                        rows.Add(clear_name, temp);
                }
            }

            int j = 0;
            int c_wellname = j++;
            int c_wopth = Column_totals_wopth.Displayed ? j++ : v_count - 1;
            int c_wopt = Column_totals_wopt.Displayed ? j++ : v_count - 1;
            int c_wopt_d = Column_totals_wopt_delta.Displayed ? j++ : v_count - 1;
            int c_wlpth = Column_totals_wlpth.Displayed ? j++ : v_count - 1;
            int c_wlpt = Column_totals_wlpt.Displayed ? j++ : v_count - 1;
            int c_wlpt_d = Column_totals_wlpt_delta.Displayed ? j++ : v_count - 1;
            int c_wwpth = Column_totals_wwpth.Displayed ? j++ : v_count - 1;
            int c_wwpt = Column_totals_wwpt.Displayed ? j++ : v_count - 1;
            int c_wwpt_d = Column_totals_wwpt_delta.Displayed ? j++ : v_count - 1;
            int c_wwith = Column_totals_wwith.Displayed ? j++ : v_count - 1;
            int c_wwit = Column_totals_wwit.Displayed ? j++ : v_count - 1;
            int c_wwit_d = Column_totals_wwit_delta.Displayed ? j++ : v_count - 1;
            int c_wgpth = Column_totals_wgpth.Displayed ? j++ : v_count - 1;
            int c_wgpt = Column_totals_wgpt.Displayed ? j++ : v_count - 1;
            int c_wgpt_d = Column_totals_wgpt_delta.Displayed ? j++ : v_count - 1;
            int c_wgith = Column_totals_wgith.Displayed ? j++ : v_count - 1;
            int c_wgit = Column_totals_wgit.Displayed ? j++ : v_count - 1;
            int c_wgit_d = Column_totals_wgit_delta.Displayed ? j++ : v_count - 1;
            int c_woprh = Column_totals_woprh.Displayed ? j++ : v_count - 1;
            int c_wopr = Column_totals_wopr.Displayed ? j++ : v_count - 1;
            int c_wopr_d = Column_totals_wopr_delta.Displayed ? j++ : v_count - 1;
            int c_wlprh = Column_totals_wlprh.Displayed ? j++ : v_count - 1;
            int c_wlpr = Column_totals_wlpr.Displayed ? j++ : v_count - 1;
            int c_wlpr_d = Column_totals_wlpr_delta.Displayed ? j++ : v_count - 1;
            int c_wwprh = Column_totals_wwprh.Displayed ? j++ : v_count - 1;
            int c_wwpr = Column_totals_wwpr.Displayed ? j++ : v_count - 1;
            int c_wwpr_d = Column_totals_wwpr_delta.Displayed ? j++ : v_count - 1;
            int c_wwirh = Column_totals_wwirh.Displayed ? j++ : v_count - 1;
            int c_wwir = Column_totals_wwir.Displayed ? j++ : v_count - 1;
            int c_wwir_d = Column_totals_wwir_delta.Displayed ? j++ : v_count - 1;
            int c_wgprh = Column_totals_wgprh.Displayed ? j++ : v_count - 1;
            int c_wgpr = Column_totals_wgpr.Displayed ? j++ : v_count - 1;
            int c_wgpr_d = Column_totals_wgpr_delta.Displayed ? j++ : v_count - 1;
            int c_wgirh = Column_totals_wgirh.Displayed ? j++ : v_count - 1;
            int c_wgir = Column_totals_wgir.Displayed ? j++ : v_count - 1;
            int c_wgir_d = Column_totals_wgir_delta.Displayed ? j++ : v_count - 1;

            double[] totals_wells_all = new double[v_count];
            double[] totals_wells_target = new double[v_count];
            for (int i = 0; i < v_count; ++i) { totals_wells_all[i] = 0; totals_wells_target[i] = 0; }

            int r_first = 0;
            for (int w = 0; w < wellnames.Length; ++w)
            {
                this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wellname].Value = wellnames[w];
                double[] values = rows[wellnames[w]];
                if (values[v_wlpth] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopth].Value = Round(values[v_wopth]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt].Value = Round(values[v_wopt]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt_d].Value = Round(DeltaPercent(values[v_wopth], values[v_wopt]));
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpth].Value = Round(values[v_wlpth]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt].Value = Round(values[v_wlpt]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt_d].Value = Round(DeltaPercent(values[v_wlpth], values[v_wlpt]));
                    //\\//\\
                    if (values[v_wopth] > 0)
                    {
                        totals_wells_all[v_wopth]++;
                        if (values[v_wopt_d] <= wopt_d) totals_wells_all[v_wopt]++;
                        else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt_d].Style.ForeColor = Color.Red;
                    }
                    totals_wells_all[v_wlpth]++;
                    if (values[v_wlpt_d] <= wlpt_d) totals_wells_all[v_wlpt]++;
                    else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt_d].Style.ForeColor = Color.Red;
                    if (IsTarget(values[v_wopth], wopth_list, wells_targ))
                    {
                        totals_wells_target[v_wopth]++;
                        if (values[v_wopt_d] <= wopt_d) totals_wells_target[v_wopt]++;
                        totals_wells_target[v_wlpth]++;
                        if (values[v_wlpt_d] <= wlpt_d) totals_wells_target[v_wlpt]++;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopth].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopt_d].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpth].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpt_d].Style.BackColor = Color.GreenYellow;
                    }
                }
                if (values[v_wwith] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwith].Value = Round(values[v_wwith]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwit].Value = Round(values[v_wwit]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwit_d].Value = Round(DeltaPercent(values[v_wwith], values[v_wwit]));
                    //\\//\\
                    totals_wells_all[v_wwith]++;
                    totals_wells_target[v_wwith]++;
                    if (values[v_wwit_d] <= wwit_d)
                    {
                        totals_wells_all[v_wwit]++;
                        totals_wells_target[v_wwit]++;
                    }
                }
                if (values[v_wlprh] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_woprh].Value = Round(values[v_woprh]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr].Value = Round(values[v_wopr]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr_d].Value = Round(DeltaPercent(values[v_woprh], values[v_wopr]));
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlprh].Value = Round(values[v_wlprh]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr].Value = Round(values[v_wlpr]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr_d].Value = Round(DeltaPercent(values[v_wlprh], values[v_wlpr]));
                    //\\//\\
                    if (values[v_woprh] > 0)
                    {
                        totals_wells_all[v_woprh]++;
                        if (values[v_wopr_d] <= wopr_d) totals_wells_all[v_wopr]++;
                        else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr_d].Style.ForeColor = Color.Red;
                    }
                    totals_wells_all[v_wlprh]++;
                    if (values[v_wlpr_d] <= wlpr_d) totals_wells_all[v_wlpr]++;
                    else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr_d].Style.ForeColor = Color.Red;
                    if (IsTarget(values[v_woprh], woprh_list, wells_targ))
                    {
                        totals_wells_target[v_woprh]++;
                        if (values[v_wopr_d] <= wopr_d) totals_wells_target[v_wopr]++;
                        totals_wells_target[v_wlprh]++;
                        if (values[v_wlpr_d] <= wlpr_d) totals_wells_target[v_wlpr]++;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_woprh].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wopr_d].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlprh].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr].Style.BackColor = Color.GreenYellow;
                        this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wlpr_d].Style.BackColor = Color.GreenYellow;
                    }
                }
                if (values[v_wwirh] > 0)
                {
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwirh].Value = Round(values[v_wwirh]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwir].Value = Round(values[v_wwir]);
                    this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwir_d].Value = Round(DeltaPercent(values[v_wwirh], values[v_wwir]));
                    //\\//\\
                    totals_wells_all[v_wwirh]++;
                    totals_wells_target[v_wwirh]++;
                    if (values[v_wwir_d] <= wwir_d)
                    {
                        totals_wells_all[v_wwir]++;
                        totals_wells_target[v_wwir]++;
                    }
                    else this.dataGridView_hm_table_wells.Rows[r_first + w].Cells[c_wwir_d].Style.ForeColor = Color.Red;
                }
            }

            this.dataGridView_hm_table_total.Rows.Clear();
            this.dataGridView_hm_table_total.Rows.Add(3);

            double foprh = result.WOPRH(wellnames).Values.Last();
            double fopr = result.WOPR(wellnames).Values.Last();
            double flprh = result.WLPRH(wellnames).Values.Last();
            double flpr = result.WLPR(wellnames).Values.Last();
            //
            double fopth = result.WOPTH(wellnames).Values.Last();
            double fopt = result.WOPT(wellnames).Values.Last();
            double flpth = result.WLPTH(wellnames).Values.Last();
            double flpt = result.WLPT(wellnames).Values.Last();
            //
            double fwirh = result.WWIRH(wellnames).Values.Last();
            double fwir = result.WWIR(wellnames).Values.Last();
            double fwith = result.WWITH(wellnames).Values.Last();
            double fwit = result.WWIT(wellnames).Values.Last();

            this.dataGridView_hm_table_total.Rows[0].Cells[c_wellname].Value = "Итого";
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopth].Value = Round(fopth) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopt].Value = Round(fopt) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopt_d].Value = Round(DeltaPercent(fopth, fopt));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpth].Value = Round(flpth) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpt].Value = Round(flpt) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpt_d].Value = Round(DeltaPercent(flpth, flpt));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwith].Value = Round(fwith) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwit].Value = Round(fwit) / 1000;
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwit_d].Value = Round(DeltaPercent(fwith, fwit));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_woprh].Value = Round(foprh);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopr].Value = Round(fopr);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wopr_d].Value = Round(DeltaPercent(foprh, fopr));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlprh].Value = Round(flprh);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpr].Value = Round(flpr);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wlpr_d].Value = Round(DeltaPercent(flprh, flpr));
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwirh].Value = Round(fwirh);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwir].Value = Round(fwir);
            this.dataGridView_hm_table_total.Rows[0].Cells[c_wwir_d].Value = Round(DeltaPercent(fwirh, fwir));

            this.dataGridView_hm_table_total.Rows[1].Cells[c_wellname].Value = "Весь фонд";
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopth].Value = totals_wells_all[v_wopth];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopt].Value = totals_wells_all[v_wopt];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopt_d].Value = Round(DivPercent(totals_wells_all[v_wopth], totals_wells_all[v_wopt]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpth].Value = totals_wells_all[v_wlpth];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpt].Value = totals_wells_all[v_wlpt];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpt_d].Value = Round(DivPercent(totals_wells_all[v_wlpth], totals_wells_all[v_wlpt]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwith].Value = totals_wells_all[v_wwith];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwit].Value = totals_wells_all[v_wwit];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwit_d].Value = Round(DivPercent(totals_wells_all[v_wwith], totals_wells_all[v_wwit]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_woprh].Value = totals_wells_all[v_woprh];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopr].Value = totals_wells_all[v_wopr];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopr_d].Value = Round(DivPercent(totals_wells_all[v_woprh], totals_wells_all[v_wopr]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlprh].Value = totals_wells_all[v_wlprh];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpr].Value = totals_wells_all[v_wlpr];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpr_d].Value = Round(DivPercent(totals_wells_all[v_wlprh], totals_wells_all[v_wlpr]));
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwirh].Value = totals_wells_all[v_wwirh];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwir].Value = totals_wells_all[v_wwir];
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwir_d].Value = Round(DivPercent(totals_wells_all[v_wwirh], totals_wells_all[v_wwir]));

            this.dataGridView_hm_table_total.Rows[2].Cells[c_wellname].Value = "Целевой фонд";
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopth].Value = totals_wells_target[v_wopth];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopt].Value = totals_wells_target[v_wopt];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopt_d].Value = Round(DivPercent(totals_wells_target[v_wopth], totals_wells_target[v_wopt]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpth].Value = totals_wells_target[v_wlpth];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpt].Value = totals_wells_target[v_wlpt];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpt_d].Value = Round(DivPercent(totals_wells_target[v_wlpth], totals_wells_target[v_wlpt]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwith].Value = totals_wells_target[v_wwith];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwit].Value = totals_wells_target[v_wwit];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwit_d].Value = Round(DivPercent(totals_wells_target[v_wwith], totals_wells_target[v_wwit]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_woprh].Value = totals_wells_target[v_woprh];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopr].Value = totals_wells_target[v_wopr];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopr_d].Value = Round(DivPercent(totals_wells_target[v_woprh], totals_wells_target[v_wopr]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlprh].Value = totals_wells_target[v_wlprh];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpr].Value = totals_wells_target[v_wlpr];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpr_d].Value = Round(DivPercent(totals_wells_target[v_wlprh], totals_wells_target[v_wlpr]));
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwirh].Value = totals_wells_target[v_wwirh];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwir].Value = totals_wells_target[v_wwir];
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwir_d].Value = Round(DivPercent(totals_wells_target[v_wwirh], totals_wells_target[v_wwir]));

            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwith].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwit].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_woprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wopr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wlpr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwirh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[1].Cells[c_wwir].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpth].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpt].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwith].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwit].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_woprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wopr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlprh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wlpr].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwirh].Style = dataGridViewCellStyle_wells;
            this.dataGridView_hm_table_total.Rows[2].Cells[c_wwir].Style = dataGridViewCellStyle_wells;

            this.dataGridView_hm_table_wells.Sort(this.Column_wells_wopth, ListSortDirection.Descending);
        }



        void ShowColumn(bool oil, bool water, bool gas, bool liq)
        {
            //Column_totals_wellname.Visible = true;
            Column_totals_wopth.Visible = oil;
            Column_totals_wopt.Visible = oil;
            Column_totals_wopt_delta.Visible = oil;
            Column_totals_wlpth.Visible = liq;
            Column_totals_wlpt.Visible = liq;
            Column_totals_wlpt_delta.Visible = liq;
            Column_totals_wwpth.Visible = water;
            Column_totals_wwpt.Visible = water;
            Column_totals_wwpt_delta.Visible = water;
            Column_totals_wwith.Visible = water;
            Column_totals_wwit.Visible = water;
            Column_totals_wwit_delta.Visible = water;
            Column_totals_wgpth.Visible = gas;
            Column_totals_wgpt.Visible = gas;
            Column_totals_wgpt_delta.Visible = gas;
            Column_totals_wgith.Visible = gas;
            Column_totals_wgit.Visible = gas;
            Column_totals_wgit_delta.Visible = gas;
            Column_totals_woprh.Visible = oil;
            Column_totals_wopr.Visible = oil;
            Column_totals_wopr_delta.Visible = oil;
            Column_totals_wlprh.Visible = liq;
            Column_totals_wlpr.Visible = liq;
            Column_totals_wlpr_delta.Visible = liq;
            Column_totals_wwprh.Visible = water;
            Column_totals_wwpr.Visible = water;
            Column_totals_wwpr_delta.Visible = water;
            Column_totals_wwirh.Visible = water;
            Column_totals_wwir.Visible = water;
            Column_totals_wwir_delta.Visible = water;
            Column_totals_wgprh.Visible = gas;
            Column_totals_wgpr.Visible = gas;
            Column_totals_wgpr_delta.Visible = gas;
            Column_totals_wgirh.Visible = gas;
            Column_totals_wgir.Visible = gas;
            Column_totals_wgir_delta.Visible = gas;
            //
            //Column_wells_wellname.Visible = Column_totals_wellname.Visible;
            Column_wells_wopth.Visible = Column_totals_wopth.Visible;
            Column_wells_wopt.Visible = Column_totals_wopt.Visible;
            Column_wells_wopt_delta.Visible = Column_totals_wopt_delta.Visible;
            Column_wells_wlpth.Visible = Column_totals_wlpth.Visible;
            Column_wells_wlpt.Visible = Column_totals_wlpt.Visible;
            Column_wells_wlpt_delta.Visible = Column_totals_wlpt_delta.Visible;
            Column_wells_wwpth.Visible = Column_totals_wwpth.Visible;
            Column_wells_wwpt.Visible = Column_totals_wwpt.Visible;
            Column_wells_wwpt_delta.Visible = Column_totals_wwpt_delta.Visible;
            Column_wells_wwith.Visible = Column_totals_wwith.Visible;
            Column_wells_wwit.Visible = Column_totals_wwit.Visible;
            Column_wells_wwit_delta.Visible = Column_totals_wwit_delta.Visible;
            Column_wells_wgpth.Visible = Column_totals_wgpth.Visible;
            Column_wells_wgpt.Visible = Column_totals_wgpt.Visible;
            Column_wells_wgpt_delta.Visible = Column_totals_wgpt_delta.Visible;
            Column_wells_wgith.Visible = Column_totals_wgith.Visible;
            Column_wells_wgit.Visible = Column_totals_wgit.Visible;
            Column_wells_wgit_delta.Visible = Column_totals_wgit_delta.Visible;
            Column_wells_woprh.Visible = Column_totals_woprh.Visible;
            Column_wells_wopr.Visible = Column_totals_wopr.Visible;
            Column_wells_wopr_delta.Visible = Column_totals_wopr_delta.Visible;
            Column_wells_wlprh.Visible = Column_totals_wlprh.Visible;
            Column_wells_wlpr.Visible = Column_totals_wlpr.Visible;
            Column_wells_wlpr_delta.Visible = Column_totals_wlpr_delta.Visible;
            Column_wells_wwprh.Visible = Column_totals_wwprh.Visible;
            Column_wells_wwpr.Visible = Column_totals_wwpr.Visible;
            Column_wells_wwpr_delta.Visible = Column_totals_wwpr_delta.Visible;
            Column_wells_wwirh.Visible = Column_totals_wwirh.Visible;
            Column_wells_wwir.Visible = Column_totals_wwir.Visible;
            Column_wells_wwir_delta.Visible = Column_totals_wwir_delta.Visible;
            Column_wells_wgprh.Visible = Column_totals_wgprh.Visible;
            Column_wells_wgpr.Visible = Column_totals_wgpr.Visible;
            Column_wells_wgpr_delta.Visible = Column_totals_wgpr_delta.Visible;
            Column_wells_wgirh.Visible = Column_totals_wgirh.Visible;
            Column_wells_wgir.Visible = Column_totals_wgir.Visible;
            Column_wells_wgir_delta.Visible = Column_totals_wgir_delta.Visible;
        }









        void New()
        {
            if (OkToContinue())
            {
                UpdateProject(new HistMatchingProject());
                SetCurrentFile(string.Empty);
            }
        }




        void UpdateProject(HistMatchingProject newProject)
        {
            Clear();
            CurrProject = newProject;
            Downloader.RemoveFiles(newProject.PXLHMFiles);
            Downloader.AddFiles(newProject.PXLHMFiles);
        }



        void Open()
        {
            if (OkToContinue())
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = string.Format("Project Files (*.{0})|*.{0}", HistMatchingProject.Identifier);
                dialog.Multiselect = false;
                dialog.ShowDialog();
                string filename = dialog.FileName;
                if (!string.IsNullOrEmpty(filename))
                    LoadFile(filename);
            }
        }




        bool Save()
        {
            if (string.IsNullOrEmpty(curfile))
                return SaveAs();
            else
                return SaveFile(curfile);
        }




        bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = string.Format("Project Files (*.{0})|*.{0}", HistMatchingProject.Identifier);
            dialog.DefaultExt = HistMatchingProject.Identifier;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (string.IsNullOrEmpty(filename))
                return false;
            return SaveFile(filename);
        }


        void OpenRecentFile(string filename)
        {
            if (OkToContinue())
            {
                LoadFile(filename);
            }
        }


        bool modified = false;

        bool OkToContinue()
        {
            if (modified)
            {
                string caption = "Project Closing";
                string message = "The Project has been modified.\n" + "Do you want to save your changes?";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    return Save();
                else if (result == DialogResult.Cancel)
                    return false;
            }
            return true;
        }


        bool LoadFile(string filename)
        {
            if (!ReadFile(filename))
                return false;
            SetCurrentFile(filename);
            return true;
        }


        bool SaveFile(string filename)
        {
            if (!WriteFile(filename))
                return false;
            SetCurrentFile(filename);
            return true;
        }


        void SetCurrentFile(string filename)
        {
            curfile = filename;
            string shownName = OriginalText + " | " + "Unknown";
            if (!string.IsNullOrEmpty(curfile))
            {
                shownName = OriginalText + " | " + Path.ChangeExtension(filename, null);
                recentFiles.Remove(curfile);
                //
                recentFiles.Reverse();
                recentFiles.Add(curfile);
                recentFiles.Reverse();
                //
                UpdateRecentFile();
            }
            this.Text = shownName;
        }


        void UpdateRecentFile()
        {
            List<string> temp = new List<string>();
            foreach (string filename in recentFiles)
                if (File.Exists(filename))
                    temp.Add(filename);
            recentFiles = temp;
            for (int i = 0; i < maxRecentFiles; ++i)
            {
                if (i < recentFiles.Count)
                {
                    recentFilesFileMenu[i].Name = recentFiles[i];
                    recentFilesFileMenu[i].Text = "&" + (i + 1).ToString() + " " + StrippedName(recentFiles[i]);
                    recentFilesFileMenu[i].Visible = true;
                }
                else
                    recentFilesFileMenu[i].Visible = false;
            }
            separatorDownFileMenu.Visible = (recentFiles.Count != 0);
        }


        string StrippedName(string fullFileName)
        {
            FileInfo fileinfo = new FileInfo(fullFileName);
            return fileinfo.Name;
        }





 


        bool WriteFile(string filename)
        {
            //Cursor.Current = Cursors.WaitCursor;
            HistMatchingProject project = new HistMatchingProject() { PXLHMFiles = Downloader.Data.Keys.ToArray() };
            bool ok = project.Save(filename);
            if (ok)
                Msg($"Project file '{filename}' successfully saved!");
            else
                Msg($"Project file '{filename}' saving error!");
            //Cursor.Current = Cursors.Default;
            return ok;
        }




        bool ReadFile(string filename)
        {
            //Cursor.Current = Cursors.WaitCursor;
            bool ok = HistMatchingProject.Load(filename, out HistMatchingProject project);
            if (ok)
            {
                Msg($"Project file '{filename}' successfully read!");
                UpdateProject(project);
            }
            else
            {
                Msg($"Project file '{filename}' reading error!");
            }
            //Cursor.Current = Cursors.Default;
            return ok;
        }





        List<string> recentFiles = new List<string>();
        string curfile;


        void recentFilesMenu_Click(object sender, EventArgs e)
        {
            string filename = ((ToolStripMenuItem)sender).Name;
            OpenRecentFile(filename);
        }

        void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        ToolStripMenuItem exitMenu;

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }


        private void treeView_project_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {            
            var tag = treeView_project.SelectedNode.Tag as Tuple<int, string>;
            //var tag = e.Node.Tag as Tuple<int, string>;

            if (tag.Item1 == ResultsViewTreeDataDownloader.CS_TAG_KEY)
            {
                ThreadStart ts = new ThreadStart(() =>
                {
                    HistMatchingInput input = Downloader.Data[_selected_pxlhm_file].InputData;
                    WizardForm wizard = new WizardForm(input, WizardStartPage.Data);
                    wizard.RunStartedEvent += Downloader.AddFiles;
                    Application.Run(wizard);
                });
                Thread thread = new Thread(ts);
                thread.ApartmentState = ApartmentState.STA;
                thread.Start();
            }
            else if (tag.Item1 == ResultsViewTreeDataDownloader.ID_TAG_KEY)
            {
                ThreadStart ts = new ThreadStart(() =>
                {
                    HistMatchingInput input = Downloader.Data[_selected_pxlhm_file].Sets[_selected_id_folder].Iterations.Last().Value.InputData;
                    WizardForm wizard = new WizardForm(input, WizardStartPage.Run);
                    wizard.RunStartedEvent += Downloader.AddFiles;
                    Application.Run(wizard);
                });
                Thread thread = new Thread(ts);
                thread.ApartmentState = ApartmentState.STA;
                thread.Start();
            }
            else if (tag.Item1 == ResultsViewTreeDataDownloader.IT_TAG_KEY)
            {
                CreateDataDialog();
            }
        }


        List<UparamExpression> expressions = new List<UparamExpression>();
        int nparam = 1;
        private void toolStripButton_add_param_Click(object sender, EventArgs e)
        {
            string title = $"param_{nparam++}";
            this.checkedListBox_uparams.Items.Add(title);
            expressions.Add(new UparamExpression() { Checked = true, Title = title, Text = string.Empty });
        }

        private void richTextBox_uparam_expression_TextChanged(object sender, EventArgs e)
        {
            expressions[checkedListBox_uparams.SelectedIndex].Text = this.richTextBox_expression.Text;
        }

        private void checkedListBox_uparams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(checkedListBox_uparams.SelectedIndex >= 0)
                this.richTextBox_expression.Text = expressions[checkedListBox_uparams.SelectedIndex].Text;
            this.listBox_kw.Enabled = checkedListBox_uparams.SelectedIndex >= 0;
            this.richTextBox_expression.Enabled = checkedListBox_uparams.SelectedIndex >= 0;
        }

        private void checkedListBox_uparams_MouseUp(object sender, MouseEventArgs e)
        {
            int i = checkedListBox_uparams.SelectedIndex;
            if (i >= 0)
            {
                expressions[i].Checked = checkedListBox_uparams.CheckedIndices.Contains(i);
                UpdateAllGraphs();
            }
        }



        void UpdateUparamsGraphs(Iteration[] iterations, DataTable res_press)
        {
            List<(string, double[], Color)> bars = new List<(string, double[], Color)>();
            foreach (UparamExpression expression in expressions.Where(v => v.Checked))
            {
                double[] x_values = new double[iterations.Length];
                for (int i = 0; i < iterations.Length; ++i)
                {
                    HistMatchingKeyValues[] kv = iterations[i].Result.GetKeyValues(Settings, res_press);                   
                    x_values[i] = Evaluete(expression.Text, kv.SelectMany(v => v.GetParameters()).ToArray());
                }
                bars.Add((expression.Title, x_values, Color.Black));
            }
            string[] names = iterations.Select(v => v.Title).ToArray();
            RedrawHMBar(this.zedGraphControl_uparams, names, bars.ToArray());
        }

        private void listBox_kw_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox_kw.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                richTextBox_expression.Select(richTextBox_expression.SelectionStart, 0);
                richTextBox_expression.SelectedText = " " + listBox_kw.SelectedItem.ToString() + " ";
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //resultsViewSettingsForm.Settings = this.Settings;
            resultsViewSettingsForm.Show(this);
        }

        private void fRAnalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HM.FR.FRForm());
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
        }

        private void ResultsViewTreeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Downloader.Stop();
            Downloader = null;
        }




        // https://github.com/dynamicexpresso/DynamicExpresso
        public double Evaluete(string expression, Parameter[] parameters)
        {
            Func<double, double, double> pow = (x, y) => Math.Pow(x, y);
            var target = new Interpreter();
            target.SetFunction("POW", pow);
            double result = 0;
            try
            {
                result = target.Eval<double>(expression, parameters);
            }
            catch (Exception ex)
            {
                //MsgEvent?.Invoke(ex.Message);
            }
            return result;
        }






        /*protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 515)
            { *//* WM_LBUTTONDBLCLK *//*
            }
            else
                base.DefWndProc(ref m);
        }
*/



    }
}
