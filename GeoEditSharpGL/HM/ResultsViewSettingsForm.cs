using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexel.HM
{
    public partial class ResultsViewSettingsForm : Form
    {
        public ResultsViewSettingsForm()
        {
            InitializeComponent();
            this.comboBox_cross_plot_font.Items.Clear();
            for (float v = 7f; v <= 50f; v += 1f)
                this.comboBox_cross_plot_font.Items.Add(v);
            this.comboBox_cross_plot_font.SelectedIndex = 0;
            //
            this.comboBox_graph_font.Items.Clear();
            for (float v = 7f; v <= 50f; v += 1f)
                this.comboBox_graph_font.Items.Add(v);
            this.comboBox_graph_font.SelectedIndex = 0;
        }

        public double OilRateDeltaPercent 
        { 
            set
            {
                this.numericUpDown_opr.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_opr.Value;
            }
        }

        public double LiqRateDeltaPercent
        {
            set
            {
                this.numericUpDown_lpr.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_lpr.Value;
            }
        }

        public double OilTotalDeltaPercent
        {
            set
            {
                this.numericUpDown_opt.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_opt.Value;
            }
        }
        public double LiqTotalDeltaPercent
        {
            set
            {
                this.numericUpDown_lpt.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_lpt.Value;
            }
        }

        public double InjeRateDeltaPercent
        {
            set
            {
                this.numericUpDown_wir.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_wir.Value;
            }
        }

        public double InjeTotalDeltaPercent
        {
            set
            {
                this.numericUpDown_wit.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_wit.Value;
            }
        }

        public double WBHPDeltaPercent
        {
            set
            {
                this.numericUpDown_bhp.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_bhp.Value;
            }
        }

        public double ResPressDeltaPercent
        {
            set
            {
                this.numericUpDown_res_press.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_res_press.Value;
            }
        }

        public double TargetWellsPercent
        {
            set
            {
                this.numericUpDown_targ_wells.Value = (decimal)value;
            }
            get
            {
                return (double)this.numericUpDown_targ_wells.Value;
            }
        }


        public string WellSufix
        {
            set
            {
                this.textBox_ending.Text = value;
            }
            get
            {
                return this.textBox_ending.Text;
            }
        }

        public Color OilRateColor
        {
            set
            {
                this.button_opr_color.BackColor = value;
            }
            get
            {
                return this.button_opr_color.BackColor;
            }
        }

        public Color OilTotalColor
        {
            set
            {
                this.button_opt_color.BackColor = value;
            }
            get
            {
                return this.button_opt_color.BackColor;
            }
        }

        public Color LiqRateColor
        {
            set
            {
                this.button_lpr_color.BackColor = value;
            }
            get
            {
                return this.button_lpr_color.BackColor;
            }
        }

        public Color LiqTotalColor
        {
            set
            {
                this.button_lpt_color.BackColor = value;
            }
            get
            {
                return this.button_lpt_color.BackColor;
            }
        }

        public Color InjeRateColor
        {
            set
            {
                this.button_wir_color.BackColor = value;
            }
            get
            {
                return this.button_wir_color.BackColor;
            }
        }

        public Color InjeTotalColor
        {
            set
            {
                this.button_wir_color.BackColor = value;
            }
            get
            {
                return this.button_wit_color.BackColor;
            }
        }

        public Color WBHPColor
        {
            set
            {
                this.button_bhp_color.BackColor = value;
            }
            get
            {
                return this.button_bhp_color.BackColor;
            }
        }

        public Color WBP9Color
        {
            set
            {
                this.button_bp9_color.BackColor = value;
            }
            get
            {
                return this.button_bp9_color.BackColor;
            }
        }

        public Color HMDevColor { set; get; } = Color.Orange;

        public int DecimalPlaces
        {
            set
            {
                this.numericUpDown_decimal_places.Value = (int)value;
            }
            get
            {
                return (int)this.numericUpDown_decimal_places.Value;
            }
        }


        public bool ShowWellNamesCP
        {
            get => this.checkBox_show_wellnames_cp.Checked;
            set => this.checkBox_show_wellnames_cp.Checked = value;
        }

        public float CrossPlotFontSpec
        {
            get => Helper.ParseFloat(this.comboBox_cross_plot_font.SelectedItem.ToString());
            set => this.comboBox_cross_plot_font.SelectedItem = value; 
        }

        public float GraphFontSpec
        {
            get => Helper.ParseFloat(this.comboBox_graph_font.SelectedItem.ToString());
            set => this.comboBox_graph_font.SelectedItem = value;
        }



        void Apply(object sender, EventArgs e)
        {
            ApplyEvent?.Invoke(sender, e);
        }
        

        void Ok(object sender, EventArgs e)
        {
            Apply(sender, e);
            Close();
        }
               
        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        public event EventHandler ApplyEvent;


        public ResultsViewSettings Settings
        {
            set
            {
                OilRateDeltaPercent = value.OPRDeltaPercent;
                LiqRateDeltaPercent = value.LPRDeltaPercent;
                OilTotalDeltaPercent = value.OPTDeltaPercent;
                LiqTotalDeltaPercent = value.LPTDeltaPercent;
                InjeRateDeltaPercent = value.WIRDeltaPercent;
                InjeTotalDeltaPercent = value.WITDeltaPercent;
                WBHPDeltaPercent = value.WBHPDeltaPercent;
                ResPressDeltaPercent = value.ResPressDeltaPercent;
                TargetWellsPercent = value.TargetWellsPercent;
                WellSufix = value.WellSufix;
                OilRateColor = value.OPRColor;
                OilTotalColor = value.OPTColor;
                LiqRateColor = value.LPRColor;
                LiqTotalColor = value.LPTColor;
                InjeRateColor = value.WIRColor;
                InjeTotalColor = value.WITColor;
                WBHPColor = value.WBHPColor;
                WBP9Color = value.WBP9Color;
                HMDevColor = value.HMDevColor;
                DecimalPlaces = value.DecimalPlaces;
                ShowWellNamesCP = value.ShowWellNamesCP;
                CrossPlotFontSpec = value.CrossPlotFontSpec;
                GraphFontSpec = value.GraphFontSpec;
            }
            get
            {
                return new ResultsViewSettings
                {
                    OPRDeltaPercent = OilRateDeltaPercent,
                    LPRDeltaPercent = LiqRateDeltaPercent,
                    OPTDeltaPercent = OilTotalDeltaPercent,
                    LPTDeltaPercent = LiqTotalDeltaPercent,
                    WIRDeltaPercent = InjeRateDeltaPercent,
                    WITDeltaPercent = InjeTotalDeltaPercent,
                    WBHPDeltaPercent = WBHPDeltaPercent,
                    ResPressDeltaPercent = ResPressDeltaPercent,
                    TargetWellsPercent = TargetWellsPercent,
                    WellSufix = WellSufix,
                    OPRColor = OilRateColor,
                    OPTColor = OilTotalColor,
                    LPRColor = LiqRateColor,
                    LPTColor = LiqTotalColor,
                    WIRColor = InjeRateColor,
                    WITColor = InjeTotalColor,
                    WBHPColor = WBHPColor,
                    WBP9Color = WBP9Color,
                    HMDevColor = HMDevColor,
                    DecimalPlaces = DecimalPlaces,
                    ShowWellNamesCP = ShowWellNamesCP,
                    CrossPlotFontSpec = CrossPlotFontSpec,
                    GraphFontSpec= GraphFontSpec,
                };
            }
        }




        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }


        ColorDialog colorDialog = new ColorDialog();
        private void button_color_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            colorDialog.Color = button.BackColor;
            colorDialog.ShowDialog();
            button.BackColor = colorDialog.Color;
        }
    }
}
