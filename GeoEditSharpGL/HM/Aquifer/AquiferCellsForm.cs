using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexel.HM.Aquifer
{
    public partial class AquiferCellsForm : Form
    {
        public AquiferCellsForm()
        {
            InitializeComponent();
        }

        int Radius
        {
            get
            {
                return (int)numericUpDown_radius.Value;
            }
        }

        int NX
        {
            get
            {
                return (int)numericUpDown_nx.Value;
            }
        }

        int NY
        {
            get
            {
                return (int)numericUpDown_ny.Value;
            }
        }

        int NZ
        {
            get
            {
                return (int)numericUpDown_nz.Value;
            }
        }


        string ActnumFile
        {
            get
            {
                return this.textBox_actnum.Text;
            }
            set
            {
                this.textBox_actnum.Text = value;
            }
        }

        string SwatinitFile
        {
            get
            {
                return this.textBox_swatinit.Text;
            }
            set
            {
                this.textBox_swatinit.Text = value;
            }
        }

        string MultpvFile
        {
            get
            {
                return this.textBox_multpv.Text;
            }
            set
            {
                this.textBox_multpv.Text = value;
            }
        }

        string AqregFile
        {
            get
            {
                return this.textBox_aqreg.Text;
            }
            set
            {
                this.textBox_aqreg.Text = value;
            }
        }

        string ActnumKW
        {
            get
            {
                return this.textBox_actnum_kw.Text;
            }
            set
            {
                this.textBox_actnum_kw.Text = value;
            }
        }
        string SwatinitKW
        {
            get
            {
                return this.textBox_swatinit_kw.Text;
            }
            set
            {
                this.textBox_swatinit_kw.Text = value;
            }
        }
        string MultpvKW
        {
            get
            {
                return this.textBox_multpv_kw.Text;
            }
            set
            {
                this.textBox_multpv_kw.Text = value;
            }
        }
        string AqregKW
        {
            get
            {
                return this.textBox_aqreg_kw.Text;
            }
            set
            {
                this.textBox_aqreg_kw.Text = value;
            }
        }


        private void button_run_Click(object sender, EventArgs e)
        {
            int nx = NX;
            int ny = NY;
            int nz = NZ;
            Prop actprop = new Prop(), swatinit = new Prop();
            Task[] tasks = new Task[2];
            tasks[0] = Task.Run(() => { actprop = new Prop(nx, ny, nz, ActnumKW, ActnumFile, FileType.GRDECL_ASCII); });
            tasks[1] = Task.Run(() => { swatinit = new Prop(nx, ny, nz, SwatinitKW, SwatinitFile, FileType.GRDECL_ASCII); });
            Task.WaitAll(tasks);

            Actnum actnum = new Actnum(actprop);

            tasks[0] = Task.Run(() => 
            {
                Prop multpv = AquiferAnalyzer.MultPV(nx, ny, nz, actnum, swatinit, 500, Radius);
                multpv.Write(MultpvKW, MultpvFile, FileType.GRDECL_ASCII); 
            });
            tasks[1] = Task.Run(() => 
            {
                Prop aqreg = AquiferAnalyzer.Regions(nx, ny, nz, actnum);
                aqreg.Write(AqregKW, AqregFile, FileType.GRDECL_ASCII); 
            });
            Task.WaitAll(tasks);

            //Prop aqreg = AquiferAnalyzer.Regions(nx, ny, nz, actnum, swatinit, Radius);
            //aqreg.Write(AqregKW, AqregFile, FileType.GRDECL_ASCII);

            MessageBox.Show("Successfully completed!");
        }


        private void button_actnum_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                ActnumFile = filename;
        }

        private void button_swatinit_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("Property GRDECL Files (*.*)|*.*");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                SwatinitFile = filename;
        }

        private void button_multpv_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*";
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                MultpvFile = filename;
        }

        private void button_aqreg_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "GRDECL File (*.*)|*.*";
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                AqregFile = filename;
        }
    }
}
