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
    public partial class HFileForm : Form
    {
        public HFileForm()
        {
            InitializeComponent();
        }

        string RSMFile { set { this.textBox_rsm.Text = value; } get { return this.textBox_rsm.Text; } }
        
        //RsmType RsmType { get { return this.radioButton_tNav.Checked ? RsmType.tNavigator : RsmType.Eclipse; } }
        double WEFAC { get { return Helper.ParseDouble(this.textBox_wefac.Text); } }

        private void button_rsm_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = String.Format("RSM File (*.{0})|*.{0}|All Files (*.*)|*.*", "RSM");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (!string.IsNullOrEmpty(filename))
                RSMFile = filename;
        }

        private void button_run_Click(object sender, EventArgs e)
        {
            HM.IterationResult result = new HM.IterationResult(RSMFile, out _);
            string hfile = NoExt(RSMFile) + ".hfil";
            string efile = NoExt(RSMFile) + ".efil";
            string wbhpfile = NoExt(RSMFile) + ".wbhp";
            string wbp9file = NoExt(RSMFile) + ".wbp9";
            if (result.WriteHfile(hfile, efile, wbhpfile, wbp9file, WEFAC))
                MessageBox.Show($"Complited sucesfully!\n{hfile}\n{efile}\n{wbhpfile}\n{wbp9file}");
            else
                MessageBox.Show("Operation error. Check RSM content.");
        }
        
        string NoExt(string file)
        {
            string[] split = file.Split('.');
            int n = split.Length;
            if (n > 1) return split[n - 2];
            return split.First();
        }


        private void HFileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }



    }
}
