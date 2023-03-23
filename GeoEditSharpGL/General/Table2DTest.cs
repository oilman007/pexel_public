using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexel.General
{
    public partial class Table2DTest : Form
    {
        public Table2DTest()
        {
            InitializeComponent();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2)
            {
                double[] xval = new double[this.dataGridView.Rows.Count - 1];
                double[] yval = new double[this.dataGridView.Rows.Count - 1];
                for (int r = 0; r < this.dataGridView.Rows.Count - 1; r++)
                {
                    if (this.dataGridView.Rows[r].Cells[0].Value is null ||
                        this.dataGridView.Rows[r].Cells[1].Value is null ||
                        !Helper.TryParseDouble(this.dataGridView.Rows[r].Cells[0].Value.ToString(), out xval[r]) ||
                        !Helper.TryParseDouble(this.dataGridView.Rows[r].Cells[1].Value.ToString(), out yval[r]))
                        return;
                }
                Table2D table = new Table2D(xval, yval, -999);
                for (int r = 0; r < this.dataGridView.Rows.Count - 1; r++)
                {
                    this.dataGridView.Rows[r].Cells[2].Value = table.GetY(xval[r]);
                }

            }
        }
    }
}
