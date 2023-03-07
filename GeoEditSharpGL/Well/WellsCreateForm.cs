using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pexel
{
    public partial class WellsCreateForm : Form
    {
        public WellsCreateForm()
        {
            InitializeComponent();
        }


        private void WellsCreateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }




        public int Type
        {
            get { return this.comboBox_type.SelectedIndex; }
            set { this.comboBox_type.SelectedIndex = value; }
        }

        public decimal Distanse
        {
            get { return this.numericUpDown_distance.Value; }
            set { this.numericUpDown_distance.Value = value; }
        }

        public decimal XWidht
        {
            get { return this.numericUpDown_x_width.Value; }
            set { this.numericUpDown_x_width.Value = value; }
        }

        public decimal YWidht
        {
            get { return this.numericUpDown_y_width.Value; }
            set { this.numericUpDown_y_width.Value = value; }
        }

        public decimal Azimuth
        {
            get { return this.numericUpDown_azimuth.Value; }
            set { this.numericUpDown_azimuth.Value = value; }
        }


        const int SINGLE = 0;
        const int TRIANGULAR = 1;
        const int QUADRATIC = 2;
        const int SPOT_5 = 3;
        const int SPOT_7 = 4;
        const int SPOT_9 = 5;
        const int ROW_1 = 6;
        const int ROW_3 = 7;
        const int ROW_5 = 8;


        public const double PROD_STATE = 0;
        public const double INJE_STATE = 1;

        public List<Point3D> WellsIntersect(Point2D pos)
        {
            if (Type == SINGLE) return new List<Point3D> { new Point3D(pos.X, pos.Y, PROD_STATE) };
            List<Point3D> result = new List<Point3D>();

            bool quadrat = (Type == QUADRATIC || Type == SPOT_9);

            double x_min = -(double)XWidht / 2;
            double x_max = -x_min;
            double x_step = (double)Distanse;

            double y_min = -(double)YWidht / 2;
            double y_max = -y_min;
            double y_step = quadrat ? x_step : 0.8660254037844386f * x_step;

            double angle = (double)Azimuth * Math.PI / 180;
            double cos = Math.Cos(angle), sin = Math.Sin(angle);

            int row_denom, col_denom, col_0, col_1;

            switch (Type)
            {
                case SINGLE: row_denom = 1; col_denom = 1; col_0 = 1; col_1 = 1;
                    break;
                case TRIANGULAR: row_denom = 1; col_denom = int.MaxValue; col_0 = 1; col_1 = 1;
                    break;
                case QUADRATIC: row_denom = 1; col_denom = int.MaxValue; col_0 = 1; col_1 = 1;
                    break;
                case SPOT_5: row_denom = 2; col_denom = 1; col_0 = 0; col_1 = int.MaxValue;
                    break;
                case SPOT_7: row_denom = 1; col_denom = 3; col_0 = 1; col_1 = 2;
                    break;
                case SPOT_9: row_denom = 1; col_denom = 4; col_0 = 1; col_1 = 3;
                    break;
                case ROW_1: row_denom = 2; col_denom = 1; col_0 = 0; col_1 = int.MaxValue;
                    break;
                case ROW_3: row_denom = 5; col_denom = 1; col_0 = 0; col_1 = int.MaxValue;
                    break;
                case ROW_5: row_denom = 7; col_denom = 1; col_0 = 0; col_1 = int.MaxValue;
                    break;
                default: row_denom = 1; col_denom = 1; col_0 = 1; col_1 = 1;
                    break;
            }

            int row = 0;
            for (double y = y_min; y <= y_max; y += y_step)
            {
                int row_order = row % 2;

                int col = (row_order == 1) ? col_0 : col_1;

                double shift = (quadrat ? 0 : x_step / 2 * row_order);
                for (double x = x_min + shift; x <= x_max; x += x_step)
                {
                    double state = (col % col_denom != 0 || row % row_denom != 0) ? PROD_STATE : INJE_STATE;
                    result.Add(new Point3D(pos.X + x * cos - y * sin, pos.Y + y * cos + x * sin, state));
                    col++;
                }
                row++;
            }
            
            return result;
        }





        /*
        public List<Point2D> WellsIntersect(Point2D pos)
        {
            if (Type == SINGLE) return new List<Point2D> { pos };
            List<Point2D> result = new List<Point2D>();

            double x_min = pos.X - XWidht / 2;
            double x_max = pos.X + XWidht / 2;
            double x_step = Distanse;

            double y_min = pos.Y - YWidht / 2;
            double y_max = pos.Y + YWidht / 2;
            double y_step = (Type == QUADRATIC ? Distanse : Distanse / 1.4142135623731);

            double angle = Azimuth * Math.PI / 180;

            for (double y = y_min; y <= y_max; y += y_step)
                for (double x = x_min + (Type == QUADRATIC ? 0 : x_step / 2 * y % 2); x <= x_max; x += x_step)
                {
                    double cos = Math.Cos(angle), sin = Math.Sin(angle);
                    result.Add(new Point2D(x * cos - y * sin, -(x * sin - y * cos)));
                }

            return result;
        }
        */

    }
}
