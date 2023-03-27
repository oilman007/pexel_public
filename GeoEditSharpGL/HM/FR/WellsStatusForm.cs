using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexel.HM.HMFFR
{
    public partial class WellsStatusForm : Form
    {
        public WellsStatusForm()
        {
            InitializeComponent();
        }


        public void UpdateStatus(List<WellFace2D> wells, List<FRWellsLink> iplinks, List<Point3D> piezo_map, List<Point3D> F_map)
        {
            this.dataGridView_wells.Rows.Clear();
            int i = 0;
            double f_sum = 0f, x_sum = 0f, d_sum = 0f, t_x_f_sum = 0f, n_sum = 0f, t_sum = 0f;
            foreach (WellFace2D well in wells)
            {
                if (well.Status == WellStatus.AQUI)// || well.Status == WellStatus.INJE)
                {
                    List<double> tlist = new List<double>();
                    List<double> xlist = new List<double>();
                    List<double> dlist = new List<double>();
                    List<double> flist = new List<double>();
                    foreach (var link in iplinks)
                        if (link.Visible)
                            if (well.Title == link.W1.Title)
                            {
                                double d = link.Distance();
                                double x = link.CoveredValues(piezo_map);
                                double f = link.CoveredValues(F_map);
                                double t = link.HCTime(d, x);
                                tlist.Add(t);
                                xlist.Add(x);
                                dlist.Add(d);
                                flist.Add(f);
                                this.dataGridView_wells.Rows.Add(new object[]
                                {
                                    well.Title,
                                    well.Status.ToString(),
                                    link.W2.Title,
                                    f.ToString(),
                                    x.ToString(),
                                    d.ToString(),
                                    t.ToString()
                                });
                                f_sum += f;
                                x_sum += x;
                                d_sum += d;
                                t_x_f_sum += t * f;
                                n_sum += 1f;
                                t_sum += t;
                            }
                    double xaver = (xlist.Count == 0 ? 0 : xlist.Average());
                    double daver = (dlist.Count == 0 ? 0 : dlist.Average());                    
                    double taver = (tlist.Count == 0 ? 0 : tlist.Average());
                    double faver = (flist.Count == 0 ? 0 : flist.Average());
                    this.dataGridView_wells.Rows.Add(new object[]
                    {
                        well.Title,
                        well.Status.ToString(),
                        "Average",
                        faver.ToString(),
                        xaver.ToString(),
                        daver.ToString(),
                        taver.ToString()
                    });
                    double tw = 0f;
                    if (flist.Count > 0)
                    {
                        double fsum = flist.Sum();
                        if (fsum > 0)
                        {
                            List<double> tflist = new List<double>(flist.Count);
                            for (int j = 0; j < flist.Count; ++j) tflist.Add(tlist[j] * flist[j]);
                            tw = tflist.Sum() / fsum;
                        }
                    }
                    this.dataGridView_wells.Rows.Add(new object[]
                    {
                        well.Title,
                        well.Status.ToString(),
                        "F-Weighted",
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        tw.ToString()
                    });
                }
                else
                {
                    this.dataGridView_wells.Rows.Add(new object[]
                    {
                        well.Title,
                        well.Status.ToString(),
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty
                    });
                }
                this.dataGridView_wells.Rows[i++].Tag = well;
            }
            this.dataGridView_wells.Rows.Add(new object[]
            {
                        "TOTAL",
                        string.Empty,
                        "Average",
                        (n_sum == 0f ? 0f : f_sum / n_sum).ToString(),
                        (n_sum == 0f ? 0f : x_sum / n_sum).ToString(),
                        (n_sum == 0f ? 0f : d_sum / n_sum).ToString(),
                        (n_sum == 0f ? 0f : t_sum / n_sum).ToString()
            });
            this.dataGridView_wells.Rows.Add(new object[]
            {
                        "TOTAL",
                        string.Empty,
                        "F-Weighted",
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        (f_sum == 0f ? 0f : t_x_f_sum / f_sum).ToString()
            });
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }



    }
}
