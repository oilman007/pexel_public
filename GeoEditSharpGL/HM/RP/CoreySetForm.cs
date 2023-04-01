using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Pexel.SCAL
{
    public partial class CoreySetForm : Form
    {
        public CoreySetForm()
        {
            InitializeComponent();
            //
            zedGraphControl_swof.GraphPane.Legend.IsVisible = false;
            zedGraphControl_swof.GraphPane.Title.Text = "SWOF";
            zedGraphControl_swof.GraphPane.XAxis.Title.Text = "Sw";
            zedGraphControl_swof.GraphPane.YAxis.Title.Text = "Kr";
            zedGraphControl_swof.GraphPane.XAxis.Scale.Min = 0;
            zedGraphControl_swof.GraphPane.YAxis.Scale.Min = 0;
            zedGraphControl_swof.GraphPane.XAxis.Scale.Max = 1;
            zedGraphControl_swof.GraphPane.YAxis.Scale.Max = 1;
            //
            zedGraphControl_sgof.GraphPane.Legend.IsVisible = false;
            zedGraphControl_sgof.GraphPane.Title.Text = "SGOF";
            zedGraphControl_sgof.GraphPane.XAxis.Title.Text = "Sg";
            zedGraphControl_sgof.GraphPane.YAxis.Title.Text = "Kr";
            zedGraphControl_sgof.GraphPane.XAxis.Scale.Min = 0;
            zedGraphControl_sgof.GraphPane.YAxis.Scale.Min = 0;
            zedGraphControl_sgof.GraphPane.XAxis.Scale.Max = 1;
            zedGraphControl_sgof.GraphPane.YAxis.Scale.Max = 1;
            //
            listBox_tables_SelectedIndexChanged_1(null, null);
            //
            this.propertyGrid_set.SelectedObject = new CoreySet();
        }


        public const string EXT = "COREY";


        CoreySet CoreySet
        {
            set
            {
                this.propertyGrid_set.SelectedObject = value;
                UpdateList();
            }
            get
            {
                return this.propertyGrid_set.SelectedObject as CoreySet;
            }
        }



        void UpdateList()
        {
            this.listBox_tables.Items.Clear();
            int i = 1;
            foreach (CoreyTable table in CoreySet.Tables)
                this.listBox_tables.Items.Add($"Table#{i++}");
        }




        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Corey Files (*.{0})|*.{0}|All Files (*.*)|*.*", EXT);
            dialog.Multiselect = false;
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName) && CoreySet.Load(dialog.FileName, out CoreySet file))
                CoreySet = file;
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("Corey Files (*.{0})|*.{0}|All Files (*.*)|*.*", EXT);
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                CoreySet.Save(saveFileDialog.FileName);
            }
        }


        ExportSetForm exportSetForm = new ExportSetForm();
        private void exportTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportSetForm.CoreySet = this.CoreySet;
            exportSetForm.ShowDialog(this);
        }


        void UpdateAll()
        {
            CoreyTable table = CoreySet.Tables[listBox_tables.SelectedIndex];
            this.propertyGrid_table.SelectedObject = table;
            SymbolType symbol = checkBox_points.Checked ? SymbolType.Plus : SymbolType.None;

            // swof
            double[][] swof = table.SWOF(out int ncol_swof, out int nrow_swof);

            string[] swof_points_names = Array.Empty<string>();
            Point2D[] swof_points = Array.Empty<Point2D>();
            AlignH[] swof_points_aH = Array.Empty<AlignH>();
            AlignV[] swof_points_aV = Array.Empty<AlignV>();
            CoordType[] swof_points_axes = Array.Empty<CoordType>();

            if (this.checkBox_critical_points.Checked)
            {
                table.GetSWOFPoints(out swof_points_names, out swof_points, out swof_points_aH, out swof_points_aV, out swof_points_axes);
            }

            List<string> swof_titles = new List<string>();
            List<double[]> swof_y = new List<double[]>();
            List<Color> swof_colors = new List<Color>();
            List<DashStyle> swof_dashes = new List<DashStyle>();
            List<CoordType> swof_axes = new List<CoordType>();

            if (this.checkBox_kr.Checked)
            {
                swof_titles.AddRange(new List<string> { "Krw", "Kro" });
                swof_y.AddRange(new List<double[]> { swof[1], swof[2] });
                swof_colors.AddRange(new List<Color> { Color.Blue, Color.Red });
                swof_dashes.AddRange(new List<DashStyle> { DashStyle.Solid, DashStyle.Solid });
                swof_axes.AddRange(new List<CoordType> { CoordType.AxisXYScale, CoordType.AxisXYScale });
            }

            if (this.checkBox_pc.Checked)
            {
                swof_titles.Add("Pcow");
                swof_y.Add(swof[3]);
                swof_colors.Add(Color.Green);
                swof_dashes.Add(DashStyle.Solid);
                swof_axes.Add(CoordType.AxisXY2Scale);
            }

            if (this.checkBox_now_min_max.Checked || this.checkBox_nw_min_max.Checked)
            {
                CoreyTable table_min = new CoreyTable(table);
                table_min.NOW = table_min.NOWmin;
                table_min.NW = table_min.NWmin;
                double[][] swof_min = table_min.SWOF(out _, out _);

                CoreyTable table_max = new CoreyTable(table);
                table_max.NOW = table_max.NOWmax;
                table_max.NW = table_max.NWmax;
                double[][] swof_max = table_max.SWOF(out _, out _);

                if (this.checkBox_now_min_max.Checked)
                {
                    swof_titles.Add("NOWmin");
                    swof_y.Add(swof_min[2]);
                    swof_colors.Add(Color.Red);
                    swof_dashes.Add(DashStyle.DashDot);
                    swof_axes.Add(CoordType.AxisXYScale);
                    //
                    swof_titles.Add("NOWmin");
                    swof_y.Add(swof_max[2]);
                    swof_colors.Add(Color.Red);
                    swof_dashes.Add(DashStyle.DashDotDot);
                    swof_axes.Add(CoordType.AxisXYScale);
                }

                if (this.checkBox_nw_min_max.Checked)
                {
                    swof_titles.Add("NOWmin");
                    swof_y.Add(swof_min[1]);
                    swof_colors.Add(Color.Blue);
                    swof_dashes.Add(DashStyle.DashDot);
                    swof_axes.Add(CoordType.AxisXYScale);
                    //
                    swof_titles.Add("NWmin");
                    swof_y.Add(swof_max[1]);
                    swof_colors.Add(Color.Blue);
                    swof_dashes.Add(DashStyle.DashDotDot);
                    swof_axes.Add(CoordType.AxisXYScale);
                }
            }

            UpdateGraph(this.zedGraphControl_swof,
                        swof_titles.ToArray(), swof[0], swof_y.ToArray(), swof_colors.ToArray(), swof_dashes.ToArray(),
                        swof_axes.ToArray(), symbol,
                        swof_points_names, swof_points, swof_points_aH, swof_points_aV, swof_points_axes);

            UpdateTable(dataGridView_swof, swof, ncol_swof, nrow_swof);



            // sgof
            double[][] sgof = table.SGOF(out int ncol_sgof, out int nrow_sgof);

            string[] sgof_points_names = Array.Empty<string>();
            Point2D[] sgof_points = Array.Empty<Point2D>();
            AlignH[] sgof_points_aH = Array.Empty<AlignH>();
            AlignV[] sgof_points_aV = Array.Empty<AlignV>();
            CoordType[] sgof_points_axes = Array.Empty<CoordType>();

            if (this.checkBox_critical_points.Checked)
            {
                table.GetSGOFPoints(out sgof_points_names, out sgof_points, out sgof_points_aH, out sgof_points_aV, out sgof_points_axes);
            }

            List<string> sgof_titles = new List<string>();
            List<double[]> sgof_y = new List<double[]>();
            List<Color> sgof_colors = new List<Color>();
            List<DashStyle> sgof_dashes = new List<DashStyle>();
            List<CoordType> sgof_axes = new List<CoordType>();

            if (this.checkBox_kr.Checked)
            {
                sgof_titles.AddRange(new List<string> { "Krg", "Kro" });
                sgof_y.AddRange(new List<double[]> { sgof[1], sgof[2] });
                sgof_colors.AddRange(new List<Color> { Color.Yellow, Color.Red });
                sgof_dashes.AddRange(new List<DashStyle> { DashStyle.Solid, DashStyle.Solid });
                sgof_axes.AddRange(new List<CoordType> { CoordType.AxisXYScale, CoordType.AxisXYScale });
            }

            if (this.checkBox_pc.Checked)
            {
                sgof_titles.Add("Pcog");
                sgof_y.Add(sgof[3]);
                sgof_colors.Add(Color.Green);
                sgof_dashes.Add(DashStyle.Solid);
                sgof_axes.Add(CoordType.AxisXY2Scale);
            }

            UpdateGraph(this.zedGraphControl_sgof, 
                        sgof_titles.ToArray(), sgof[0], sgof_y.ToArray(), sgof_colors.ToArray(), sgof_dashes.ToArray(),
                        sgof_axes.ToArray(), symbol,
                        sgof_points_names, sgof_points, sgof_points_aH, sgof_points_aV, sgof_points_axes);

            UpdateTable(dataGridView_sgof, sgof, ncol_sgof, nrow_sgof);
        }




        static void UpdateGraph(ZedGraphControl graph, 
                                string[] titles, double[] x, double[][] y, Color[] colors, DashStyle[] dashes, 
                                CoordType[] t_axes, SymbolType symbol,
                                string[] names, Point2D[] points, AlignH[] aH, AlignV[] aV, CoordType[] p_axes)
        {
            graph.GraphPane.CurveList.Clear();
            graph.GraphPane.GraphObjList.Clear();

            graph.GraphPane.YAxisList.Clear();
            if (t_axes.Contains(CoordType.AxisXYScale)) graph.GraphPane.AddYAxis("Kr");
            if (t_axes.Contains(CoordType.AxisXY2Scale)) graph.GraphPane.AddYAxis("Pc");

            for (int t = 0; t < titles.Length; ++t)
            {
                LineItem curve = graph.GraphPane.AddCurve(titles[t], x, y[t], colors[t], symbol);
                curve.Line.Style = dashes[t];
                //curve.Line.Width = 3;
                //curve.Line.DashOn = 50;
                //curve.Line.DashOff = 100;
                curve.YAxisIndex = t_axes[t] == CoordType.AxisXYScale ? 0 : 1;
            }

            const double interval = 0;
            LineItem green_curve = graph.GraphPane.AddCurve("", points.Select(v => v.X).ToArray(),
                                                                points.Select(v => v.Y).ToArray(), 
                                                                Color.Black, SymbolType.Circle);
            green_curve.Line.IsVisible = false;

            for (int i = 0; i < points.Length; ++i)
            {
                TextObj text = new TextObj(names[i], points[i].X + interval, points[i].Y + interval, p_axes[i], aH[i], aV[i]);
                text.FontSpec.FontColor = Color.Black;
                text.ZOrder = ZOrder.A_InFront;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                //text.FontSpec.Size = Settings.CrossPlotFontSpec;
                text.FontSpec.Angle = 0;
                //string lblString = "name";
                //Link lblLink = new Link(lblString, "#", "");
                //text.Link = lblLink;
                graph.GraphPane.GraphObjList.Add(text);
            }

            if (titles.Length > 0)// || names.Length > 0)
            {
                graph.GraphPane.AxisChange();
                //
                graph.GraphPane.XAxis.Scale.Min = 0;
                graph.GraphPane.YAxis.Scale.Min = 0;
                graph.GraphPane.XAxis.Scale.Max = 1;
                graph.GraphPane.YAxis.Scale.Max = 1;
                //
                graph.GraphPane.XAxis.Scale.Min = 0;
                graph.GraphPane.YAxis.Scale.Min = 0;
                graph.GraphPane.XAxis.Scale.Max = 1;
                graph.GraphPane.YAxis.Scale.Max = 1;
            }
            graph.Invalidate();
        }



        static void UpdateTable(DataGridView dataGridView, double[][] table, int ncol, int nrow)
        {
            dataGridView.Rows.Clear();
            for (int r = 0; r < nrow; ++r)
            {
                object[] values = new object[ncol];
                for (int c = 0; c < ncol; ++c)
                    values[c] = Helper.ShowDouble(table[c][r]);
                dataGridView.Rows.Add(values);
            }
        }





        private void propertyGrid_table_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateAll();
        }



        private void CoreySetForm2_FormClosing(object sender, FormClosingEventArgs e)
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

        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            CoreySet.Tables.Add(new CoreyTable());
            UpdateList();
        }



        private void listBox_tables_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox_tables.SelectedIndex > -1)
            {
                UpdateAll();
            }
            UpdateButtons();
        }

        void UpdateButtons()
        {
            this.toolStripButton_remove.Enabled = listBox_tables.SelectedIndex > -1;
            this.checkBox_kr.Enabled = listBox_tables.SelectedIndex > -1;
            this.checkBox_points.Enabled = listBox_tables.SelectedIndex > -1;
            this.checkBox_critical_points.Enabled = listBox_tables.SelectedIndex > -1;
            this.checkBox_pc.Enabled = listBox_tables.SelectedIndex > -1;
            this.checkBox_now_min_max.Enabled = listBox_tables.SelectedIndex > -1;
            this.checkBox_nw_min_max.Enabled = listBox_tables.SelectedIndex > -1;
        }


        private void toolStripButton_remove_Click(object sender, EventArgs e)
        {
            string message = $"Do you really want to remove {listBox_tables.SelectedItem}?";
            if (MessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                CoreySet.Tables.RemoveAt(listBox_tables.SelectedIndex);
                UpdateList();
                UpdateButtons();
            }
        }

        private void checkBox_points_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void checkBox_critical_points_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void checkBox_now_min_max_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void checkBox_nw_min_max_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void checkBox_pc_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void checkBox_kr_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("TEXT Files (*.{0})|*.{0}|All Files (*.*)|*.*", "TXT");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName) && CoreySet.ImportCOREYWO(dialog.FileName, out CoreySet file))
                CoreySet = file;
        }

        private void propertyGrid_set_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            CoreySet = propertyGrid_set.SelectedObject as CoreySet;
        }
    }
}
