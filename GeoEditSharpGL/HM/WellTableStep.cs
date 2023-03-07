using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.HM
{
    public class WellTableStep
    {
        public int Period { get; set; } = 0;

        DateTime Date { set; get; }


        // control
        public ControlType Control { set; get; } = ControlType.LRAT;

        // oil
        public double WOT { set; get; } = 0;
        public double WOTH { set; get; } = 0;
        public double[] COT { set; get; } = Array.Empty<double>();
        public double[] COTH { set; get; } = Array.Empty<double>();
        public double WOR { set; get; } = 0;
        public double WORH { set; get; } = 0;


        // gas
        public double WGT { set; get; } = 0;
        public double WGTH { set; get; } = 0;
        public double[] CGT { set; get; } = Array.Empty<double>();
        public double WGR { set; get; } = 0;
        public double WGRH { set; get; } = 0;


        // water
        public double WWT { set; get; } = 0;
        public double WWTH { set; get; } = 0;
        public double[] CWT { set; get; } = Array.Empty<double>();
        public double[] CWTH { set; get; } = Array.Empty<double>();
        public double WWR { set; get; } = 0;
        public double WWRH { set; get; } = 0;


        // press
        public double WBHP { set; get; } = 0;
        public double WBHPH { set; get; } = 0;

        public double WBP9 { set; get; } = 0;
        public double ResPress { set; get; } = 0;



        // matching
        public HistMatchingCriteria Requested { set; get; } = new HistMatchingCriteria();
        public HistMatchingCriteria Calculated { set; get; } = new HistMatchingCriteria();

        // settings
        public WellDataAnalyzerSettings Settings { set; get; } = new WellDataAnalyzerSettings();

    }
}
