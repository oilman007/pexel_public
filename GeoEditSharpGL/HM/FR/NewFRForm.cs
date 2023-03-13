using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public delegate void UpdateProjectHandler(FRProject project);
        public UpdateProjectHandler UpdateProjectEvent;


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


        int GridFileType { set; get; }



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
            Grid grid;
            if (GridFileType == 1)
                Grid.ReadBinary(GridFile, out grid);
            else
                grid = new Grid(GridFile, FileType.GRDECL_ASCII);
            //Prop aqcell = new Prop();
            //aqcell.Read(grid.NX(), grid.NY(), grid.NZ(), "AQCELL", AqcellFile, HistMatching.FILETYPE);
            IterationResult rsm = new IterationResult(RsmFile, out List<string> msgs);
            Prop regions = Aquifer.AquiferAnalyzer.Regions(grid.NX(), grid.NY(), grid.NZ(), grid.Actnum);
            FRProject project = new FRProject(grid, regions, rsm);
            UpdateProjectEvent?.Invoke(project);
        }

        private void button_grid_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("PXLBIN Files (*.{0})|*.{0}|GRDECL Files (*.*)|*.*", "BIN");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            GridFileType = dialog.FilterIndex;
            if (!string.IsNullOrEmpty(filename))
            {
                GridFile = filename;
            }
        }

        private void button_aqcell_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                AqcellFile = filename;
            }
        }

        private void button_rsm_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("RSM Files (*.{0})|*.{0}|All Files (*.*)|*.*", "RSM");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                RsmFile = filename;
            }
        }












    }
}
