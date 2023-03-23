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
    public partial class PolyFuncForm : Form
    {
        public PolyFuncForm()
        {
            InitializeComponent();
        }

        private void button_run_Click(object sender, EventArgs e)
        {
            Point2D[] points = new Point2D[] {
                new Point2D( 12  ,   7   ),
                new Point2D( 5   ,   11  ),
                new Point2D( 11  ,   12  ),
                new Point2D( 4   ,   10  ),
                new Point2D( 13  ,   7   ),
                new Point2D( 10  ,   8   ),
                new Point2D( 14  ,   8   ),
                new Point2D( 14  ,   6   ),
                new Point2D( 1   ,   4   )
            };
            double r2;
            double[] coef = PolyFunctions.Coef(points, 6, out r2);
            Point2D point = PolyFunctions.GetYBy(2.5f, coef);
        }
    }
}
