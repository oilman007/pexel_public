using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexel.HM.FR
{
    public partial class NewFRForm : Form
    {
        public NewFRForm()
        {
            InitializeComponent();
        }


        string GridFile 
        { 
            set
            {
                this.textBox_grid.Text = value;
            }
            get
            {
                return this.textBox_grid.Text;
            }
        }


        string RsmFile
        {
            set
            {
                this.textBox_rsm.Text = value;
            }
            get
            {
                return this.textBox_rsm.Text;
            }
        }


        string AqcellFile
        {
            set
            {
                this.textBox_aqcell.Text = value;
            }
            get
            {
                return this.textBox_aqcell.Text;
            }
        }


        private void button_run_Click(object sender, EventArgs e)
        {
            Grid.ReadBinary(GridFile, out Grid grid);
            Prop aqcell = new Prop();
            aqcell.Read(grid.NX(), grid.NY(), grid.NZ(), "AQCELL", AqcellFile, HistMatching.FILETYPE);
            IterationResult rsm = new IterationResult(RsmFile, out List<string> msgs);

            // define regions
            Prop regions = Aquifer.AquiferAnalyzer.Regions(grid.NX(), grid.NY(), grid.NZ(), grid.Actnum);

            


            // for each region define wells



            // for each region-wells define map
            // for each map-wells define periods
            // for each period define fr analizys
        }








    }
}
