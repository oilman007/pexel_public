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
            checkBox_aqcell_CheckedChanged(null, null);
            checkBox_aqreg_CheckedChanged(null, null);
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



        bool AqcellUsed
        {
            set
            {
                this.checkBox_aqcell.Checked = value;
            }
            get
            {
                return this.checkBox_aqcell.Checked;
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



        bool AqregUsed
        {
            set
            {
                this.checkBox_aqreg.Checked = value;
            }
            get
            {
                return this.checkBox_aqreg.Checked;
            }
        }

        string AqregFile
        {
            set
            {
                this.textBox_aqreg.Text = value;
            }
            get
            {
                return this.textBox_aqreg.Text;
            }
        }


        private void button_run_Click(object sender, EventArgs e)
        {
            Grid grid;
            if (GridFileType == 1)
                Grid.ReadBinary(GridFile, out grid);
            else
                grid = new Grid(GridFile, FileType.GRDECL_ASCII);
            //
            Prop aqcell = new Prop();
            if (AqcellUsed)
                aqcell.Read(grid.NX(), grid.NY(), grid.NZ(), "AQCELL", AqcellFile, HistMatching.FILETYPE);
            else
                aqcell = new Prop(grid.NX(), grid.NY(), grid.NZ(), 0, "AQCELL");
            //
            Prop aqreg = new Prop();
            if (AqregUsed)
                aqreg.Read(grid.NX(), grid.NY(), grid.NZ(), "AQREG", AqregFile, HistMatching.FILETYPE);
            else
                aqreg = Aquifer.AquiferAnalyzer.Regions(grid.NX(), grid.NY(), grid.NZ(), grid.Actnum);
            //
            IterationResult rsm = new IterationResult(RsmFile, out List<string> msgs);
            //
            FRProject project = new FRProject(grid, aqreg, aqcell, rsm);
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


        private void checkBox_aqcell_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox_aqcell.Enabled = this.checkBox_aqcell.Checked;
            this.button_aqcell.Enabled = this.checkBox_aqcell.Checked;
        }

        private void checkBox_aqreg_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox_aqreg.Enabled = this.checkBox_aqreg.Checked;
            this.button_aqreg.Enabled = this.checkBox_aqreg.Checked;
        }

        private void button_aqreg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                AqregFile = filename;
            }
        }
    }
}
