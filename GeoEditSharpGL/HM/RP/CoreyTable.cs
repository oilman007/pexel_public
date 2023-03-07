using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlTypes;
using System.Drawing;
using ZedGraph;
using System.ComponentModel;
using System.Windows.Forms;
using Pexel.FileIO;

namespace Pexel.SCAL
{

    [DefaultPropertyAttribute("Corey Table Input")]
    public class CoreyTable
    {
        public CoreyTable() { }

        public CoreyTable(double[] coreywo) 
        {
            this.SetCOREYWO(coreywo);
        }

        public CoreyTable(CoreyTable other)
        {
            SWL = other.SWL;
            SWCR = other.SWCR;
            SOWCR = other.SOWCR;
            SGL = other.SGL;
            NW = other.NW;
            NOW = other.NOW;
            NPW = other.NPW;
            KRWR = other.KRWR;
            KRWU = other.KRWU;
            KRORW = other.KRORW;
            KROLW = other.KROLW;
            PCW = other.PCW;
            SGCR = other.SGCR;
            SOGCR = other.SOGCR;
            NG = other.NG;
            NOG = other.NOG;
            NPOG = other.NPOG;
            KRGR = other.KRGR;
            KRGU = other.KRGU;
            KRORG = other.KRORG;
            KROLG = other.KROLG;
            PCG = other.PCG;
            SWU = other.SWU;
            SGU = other.SGU;
            Step = other.Step;
            NOWmin = other.NOWmin;
            NOWmax = other.NOWmax;
            NWmin   = other.NWmin;
            NWmax = other.NWmax;
        }

        public CoreyTable(CoreyInput input) 
        {
            SWL = input.SWL;
            SWCR = input.SWCR;
            SOWCR = input.SOWCR;
            SGL = input.SGL;
            NW = input.NW;
            NOW = input.NOW;
            NPW = input.NPW;
            KRWR = input.KRWR;
            KRWU = input.KRWU;
            KRORW = input.KRORW;
            KROLW = input.KROLW;
            PCW = input.PCW;
            SGCR = input.SGCR;
            SOGCR = input.SOGCR;
            NG = input.NG;
            NOG = input.NOG;
            NPOG = input.NPOG;
            KRGR = input.KRGR;
            KRGU = input.KRGU;
            KRORG = input.KRORG;
            KROLG = input.KROLG;
            PCG = input.PCG;
            SWU = input.SWU;
            SGU = input.SGU;
        }

        const string swof_category_attribute = "SWOF";
        const string sgof_category_attribute = "SGOF";
        const string hm_category_attribute = "History Matching";
        const string common_category_attribute = "Common";


        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SWL { set; get; } = 0.18;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SWCR { set; get; } = 0.2;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SOWCR { set; get; } = 0.25;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SGL { set; get; } = 0.05;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NW { set; get; } = 1.5;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NOW { set; get; } = 1.5;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NPW { set; get; } = 5;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KRWR { set; get; } = 0.6;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KRWU { set; get; } = 1;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KRORW { set; get; } = 0.7;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KROLW { set; get; } = 1;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double PCW { set; get; } = 2;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SGCR { set; get; } = 0.1;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SOGCR { set; get; } = 0.3;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NG { set; get; } = 2;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NOG { set; get; } = 4;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NPOG { set; get; } = 1;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KRGR { set; get; } = 0.6;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KRGU { set; get; } = 0.9;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KRORG { set; get; } = 0.7;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double KROLG { set; get; } = 1;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double PCG { set; get; } = 0;

        [CategoryAttribute(swof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SWU { set; get; } = 1;

        [CategoryAttribute(sgof_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double SGU { set; get; } = 1;

        [CategoryAttribute(common_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double Step { set; get; } = 0.02;

        [CategoryAttribute(hm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NOWmin { set; get; } = 0.1;

        [CategoryAttribute(hm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NOWmax { set; get; } = 10;

        [CategoryAttribute(hm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NWmin { set; get; } = 0.1;

        [CategoryAttribute(hm_category_attribute), DescriptionAttribute(""), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public double NWmax { set; get; } = 10;


        public double SWN(double sw)
        {
            double result = (sw - SWCR) / (1 - SWCR - SOWCR - SGL);
            return result;
        }

        public double KRW(double sw)
        {
            double result;
            if (sw <= SWCR)
                result = 0;
            else if (sw <= 1 - SOWCR - SGL)
                result = KRWR * Math.Pow(SWN(sw), NW);
            else
                result = KRWU - (KRWU - KRWR) * (SWU - sw) / (SOWCR + SGL + SWU - 1);
            return result;
        }

        public double KROW(double sw)
        {
            double result;
            if (sw <= SWL)
                result = KROLW; // TODO: check!
            else if (sw < SWCR)
                result = KRORW + (KROLW - KRORW) * (SWCR - sw) / (SWCR - SWL);
            else if (sw < 1 - SOWCR - SGL)
                result = KRORW * Math.Pow(1 - SWN(sw), NOW);
            else
                result = 0;
            return result;
        }

        public double PCOW(double sw)
        {
            double result;
            double SPCO = 1 - SOWCR - SGL;
            if (sw < SPCO)
                result = PCW * Math.Pow((SPCO - sw) / (SPCO - SWCR), NPW);
            else
                result = 0;
            return result;
        }

        public double SGN(double sg)
        {
            double result = (sg - SGCR) / (1 - SGCR - SOGCR - SWL);
            return result;
        }

        public double KRG(double sg)
        {
            double result;
            if (sg <= SGCR)
                result = 0;
            else if (sg <= 1 - SOGCR - SWL)
                result = KRGR * Math.Pow(SGN(sg), NG);
            else
                result = KRGU - (KRGU - KRGR) * (SGU - sg) / (SOGCR + SWL + SGU - 1);
            return result;
        }

        public double KROG(double sg)
        {
            double result;
            if (sg <= SGL)
                result = KROLG; // TODO: check!
            else if (sg < SGCR)
                result = KRORG + (KROLG - KRORG) * (SGCR - sg) / (SGCR - SGL);
            else if (sg < 1 - SOGCR - SWL)
                result = KRORG * Math.Pow(1 - SGN(sg), NOG);
            else
                result = 0;
            return result;
        }

        public double PCOG(double sg)
        {
            double result;
            double SPCG = 1 - SOGCR - SWL;
            if (sg < SGCR)
                result = 0;
            else
                result = PCG * (1 - SOGCR - SWL) * Math.Pow((sg - SPCG) / (1 - SGCR - SOGCR - SWL), NPOG);
            return result;
        }


        public bool Export(string file, bool swof = true, bool sgof = true, List<string> comments = null)
        {
            const int w = 20;
            try
            {
                List<string> result = new List<string>();
                if (comments != null && comments.Count != 0) result.AddRange(comments);
                if (swof)
                {
                    result.Add("");
                    result.Add("SWOF");
                    double[] swpoints = SWPoints();
                    foreach (double sw in swpoints)
                    {
                        string line = $"{sw,w}\t{KRW(sw),w}\t{KROW(sw),w}\t{PCOW(sw),w}";
                        result.Add(line);
                    }
                    result.Add("/");
                }
                if (sgof)
                {
                    result.Add("");
                    result.Add("SGOF");
                    double[] sgpoints = SGPoints();
                    foreach (double sg in sgpoints)
                    {
                        string line = $"{sg,w}\t{KRG(sg),w}\t{KROG(sg),w}\t{PCOG(sg),w}";
                        result.Add(line);
                    }
                    result.Add("/");
                }
                System.IO.File.WriteAllLines(file, result.ToArray());
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        double[] SWPoints(int round = 6)
        {
            List<double> result = new List<double>();
            for (double s = 0; s <= 1; s += Step) result.Add(Math.Round(s, round));
            if (!result.Contains(SWL)) result.Add(SWL);
            if (!result.Contains(SWCR)) result.Add(SWCR);
            if (!result.Contains(1 - SOWCR - SGL)) result.Add(1 - SOWCR - SGL);
            if (!result.Contains(1)) result.Add(1);
            result.RemoveAll(item => item < SWL);
            result.Sort();
            return result.ToArray();
        }

        double[] SGPoints(int round = 6)
        {
            List<double> result = new List<double>();
            for (double s = 0; s <= 1; s += Step) result.Add(Math.Round(s, round));
            if (!result.Contains(SGL)) result.Add(SGL);
            if (!result.Contains(SGCR)) result.Add(SGCR);
            if (!result.Contains(1 - SOGCR - SWL)) result.Add(1 - SOGCR - SWL);
            if (!result.Contains(1)) result.Add(1);
            result.RemoveAll(item => item < SGL);
            result.RemoveAll(item => item > 1 - SOGCR - SWL);
            result.Sort();
            return result.ToArray();
        }







        public double[][] SWOF(out int ncol, out int nrow)
        {
            try
            {
                double[] swpoints = SWPoints();
                double[][] result = new double[4][];
                for (int i = 0; i < 4; i++)
                    result[i] = new double[swpoints.Length];
                for (int i = 0; i < swpoints.Length; i++)
                {
                    result[0][i] = swpoints[i];
                    result[1][i] = KRW(swpoints[i]);
                    result[2][i] = KROW(swpoints[i]);
                    result[3][i] = PCOW(swpoints[i]);
                }
                ncol = 4;
                nrow = swpoints.Length;
                return result;
            }
            catch (Exception ex)
            {
                nrow = 0;
                ncol = 4;
                return new double[4][];
            }
        }



        public double[][] SGOF(out int ncol, out int nrow)
        {
            try
            {
                double[] sgpoints = SGPoints();
                double[][] result = new double[4][];
                for (int i = 0; i < 4; i++)
                    result[i] = new double[sgpoints.Length];
                for (int i = 0; i < sgpoints.Length; i++)
                {
                    result[0][i] = sgpoints[i];
                    result[1][i] = KRG(sgpoints[i]);
                    result[2][i] = KROG(sgpoints[i]);
                    result[3][i] = PCOG(sgpoints[i]);
                }
                ncol = 4;
                nrow = sgpoints.Length;
                return result;
            }
            catch (Exception ex)
            {
                nrow = 0;
                ncol = 4;
                return new double[4][];
            }
        }





        public void UpdateSWOFGraph(ref ZedGraphControl graphControl)
        {
            try
            {
                graphControl.GraphPane.Legend.IsVisible = false;
                graphControl.GraphPane.Title.Text = "SWOF";
                graphControl.GraphPane.XAxis.Title.Text = "Sw";
                graphControl.GraphPane.YAxis.Title.Text = "Kr";
                graphControl.GraphPane.CurveList.Clear();
                double[][] swof = SWOF(out int ncol, out int nrow);
                LineItem krw_curve = graphControl.GraphPane.AddCurve("Krw", swof[0], swof[1], Color.Blue);
                LineItem kw_curve = graphControl.GraphPane.AddCurve("Kro", swof[0], swof[2], Color.Red);
                //zedGraphControl_swof.GraphPane.AxisChange();
                //graphControl.Invalidate();
            }
            catch { }
        }



        /*
        void UpdateSWOFTable(double[][] swof, int ncol, int nrow)
        {
            this.dataGridView_swof.Rows.Clear();
            for (int r = 0; r < nrow; ++r)
            {
                object[] values = new object[ncol];
                for (int c = 0; c < ncol; ++c)
                    values[c] = Helper.ShowDouble(swof[c][r]);
                this.dataGridView_swof.Rows.Add(values);
            }
        }
        */


        public void GetSWOFPoints(out string[] names, out Point2D[] points, out AlignH[] aH, out AlignV[] aV, out CoordType[] axes)
        {
            names = new string[]
            {
                "SWL",
                "SWCR",
                "1-SOWCR-SGL",
                "SWU",
                "PCOW",
                "KRORW",
                "KRWR",
                "KROLW",
                "KRWU"
            };
            points = new Point2D[]
            {
                new Point2D(SWL, 0),
                new Point2D(SWCR, 0),
                new Point2D(1 - SOWCR - SGL, 0),
                new Point2D(SWU, 0),
                new Point2D(SWCR, PCW),
                new Point2D(SWCR, KRORW),
                new Point2D(1 - SOWCR - SGL, KRWR),
                new Point2D(SWL, KROLW),
                new Point2D(SWU, KRWU)
            };
            aH = new AlignH[]
            {
                AlignH.Left,
                AlignH.Left,
                AlignH.Right,
                AlignH.Right,
                AlignH.Left,
                AlignH.Right,
                AlignH.Left,
                AlignH.Left,
                AlignH.Right
            };
            aV = new AlignV[]
            {
                AlignV.Bottom,
                AlignV.Top,
                AlignV.Bottom,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top
            };
            axes = new CoordType[]
            {
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXY2Scale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
            };
        }


        public void GetSGOFPoints(out string[] names, out Point2D[] points, out AlignH[] aH, out AlignV[] aV, out CoordType[] axes)
        {
            names = new string[]
            {
                "SGL",
                "SGCR",
                "1-SOGCR-SWL",
                "SGU",
                "PCOG",
                "KRORG",
                "KRGR",
                "KROLG",
                "KRGU"
            };
            points = new Point2D[]
            {
                new Point2D(SGL, 0),
                new Point2D(SGCR, 0),
                new Point2D(1 - SOGCR - SWL, 0),
                new Point2D(SGU, 0),
                new Point2D(1 - SOGCR - SWL, PCG),
                new Point2D(SGCR, KRORG),
                new Point2D(1 - SOGCR - SWL, KRGR),
                new Point2D(SGL, KROLG),
                new Point2D(SGU, KRGU)
            };
            aH = new AlignH[]
            {
                AlignH.Left,
                AlignH.Left,
                AlignH.Right,
                AlignH.Right,
                AlignH.Left,
                AlignH.Right,
                AlignH.Left,
                AlignH.Left,
                AlignH.Right
            };
            aV = new AlignV[]
            {
                AlignV.Bottom,
                AlignV.Top,
                AlignV.Bottom,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top,
                AlignV.Top
            };
            axes = new CoordType[]
            {
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXY2Scale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
                CoordType.AxisXYScale,
            };
        }



        void SetCOREYWO(double[] values)
        {
            SWL = values[0];
            SWU = values[1];
            SWCR = values[2];
            SOWCR = values[3];
            KROLW = values[4];
            KRORW = values[5];
            KRWR = values[6];
            KRWU = values[7];
            PCW = values[8];
            NOW = values[9];
            NW = values[10];
            NPW = values[11];
        }


        public double[] COREYWO()
        {
            return new double[] { SWL, SWU, SWCR, SOWCR, KROLW, KRORW, KRWR, KRWU, PCW, NOW, NW, NPW, 1 - SOWCR - SGL };
        }




        void SetCOREYGO(double[] values)
        {
            SGL = values[0];
            SGU = values[1];
            SGCR = values[2];
            SOGCR = values[3];
            KROLG = values[4];
            KRORG = values[5];
            KRGR = values[6];
            KRGU = values[7];
            PCG = values[8];
            NOG = values[9];
            NG = values[10];
            NPOG = values[11];
            SGCR = values[12];
        }



        public double[] COREYGO()
        {
            return new double[] { SGL, SGU, SGCR, SOGCR, KROLG, KRORG, KRGR, KRGU, PCG, NOG, NG, NPOG, SGCR };
        }







    }
}
