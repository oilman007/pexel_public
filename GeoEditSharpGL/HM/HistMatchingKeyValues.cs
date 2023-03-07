using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynamicExpresso;


namespace Pexel.HM
{
    public class HistMatchingKeyValues
    {
        public string ParamPrefix { set; get; } = "";

        public double OPT_DP { set; get; }
        public double WPT_DP { set; get; }
        public double GPT_DP { set; get; }
        public double LPT_DP { set; get; }
        public double WIT_DP { set; get; }
        public double GIT_DP { set; get; }
        public double OPR_DP { set; get; }
        public double WPR_DP { set; get; }
        public double GPR_DP { set; get; }
        public double LPR_DP { set; get; }
        public double WIR_DP { set; get; }
        public double GIR_DP { set; get; }
        //public double BHP_DP { set; get; }
        //public double BP9_DP { set; get; }



        public double OPT_R2 { set; get; }
        public double WPT_R2 { set; get; }
        public double GPT_R2 { set; get; }
        public double LPT_R2 { set; get; }
        public double WIT_R2 { set; get; }
        public double GIT_R2 { set; get; }
        public double OPR_R2 { set; get; }
        public double WPR_R2 { set; get; }
        public double GPR_R2 { set; get; }
        public double LPR_R2 { set; get; }
        public double WIR_R2 { set; get; }
        public double GIR_R2 { set; get; }
        public double BHP_R2 { set; get; }
        public double BP9_R2 { set; get; }




        
        public Parameter[] GetParameters()
        {
            return new Parameter[]
            {
                new Parameter($"{ParamPrefix}OPT_DP", typeof(double), OPT_DP),
                new Parameter($"{ParamPrefix}WPT_DP", typeof(double), WPT_DP),
                new Parameter($"{ParamPrefix}GPT_DP", typeof(double), GPT_DP),
                new Parameter($"{ParamPrefix}LPT_DP", typeof(double), LPT_DP),
                new Parameter($"{ParamPrefix}WIT_DP", typeof(double), WIT_DP),
                new Parameter($"{ParamPrefix}GIT_DP", typeof(double), GIT_DP),
                new Parameter($"{ParamPrefix}OPR_DP", typeof(double), OPR_DP),
                new Parameter($"{ParamPrefix}WPR_DP", typeof(double), WPR_DP),
                new Parameter($"{ParamPrefix}GPR_DP", typeof(double), GPR_DP),
                new Parameter($"{ParamPrefix}LPR_DP", typeof(double), LPR_DP),
                new Parameter($"{ParamPrefix}WIR_DP", typeof(double), WIR_DP),
                new Parameter($"{ParamPrefix}GIR_DP", typeof(double), GIR_DP),
                new Parameter($"{ParamPrefix}OPT_R2", typeof(double), OPT_R2),
                new Parameter($"{ParamPrefix}WPT_R2", typeof(double), WPT_R2),
                new Parameter($"{ParamPrefix}GPT_R2", typeof(double), GPT_R2),
                new Parameter($"{ParamPrefix}LPT_R2", typeof(double), LPT_R2),
                new Parameter($"{ParamPrefix}WIT_R2", typeof(double), WIT_R2),
                new Parameter($"{ParamPrefix}GIT_R2", typeof(double), GIT_R2),
                new Parameter($"{ParamPrefix}OPR_R2", typeof(double), OPR_R2),
                new Parameter($"{ParamPrefix}WPR_R2", typeof(double), WPR_R2),
                new Parameter($"{ParamPrefix}GPR_R2", typeof(double), GPR_R2),
                new Parameter($"{ParamPrefix}LPR_R2", typeof(double), LPR_R2),
                new Parameter($"{ParamPrefix}WIR_R2", typeof(double), WIR_R2),
                new Parameter($"{ParamPrefix}GIR_R2", typeof(double), GIR_R2),
                new Parameter($"{ParamPrefix}BHP_R2", typeof(double), BHP_R2),
                new Parameter($"{ParamPrefix}BP9_R2", typeof(double), BP9_R2),
            };
        }







    }
}
