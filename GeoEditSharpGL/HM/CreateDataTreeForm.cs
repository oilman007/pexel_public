using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using Pexel.Eclipse;

namespace Pexel.HM
{
    public partial class CreateDataTreeForm : Form
    {
        public CreateDataTreeForm()
        {
            InitializeComponent();
        }

        //public delegate void CreateDataHandler(string id, string iter, string data);
        //public CreateDataHandler CreateDataEvent;

        //public delegate void CreateDataHandler(int id, int iter);
        //public CreateDataHandler CreateDataEvent;

        public delegate void StateHandler(string message);
        public StateHandler MsgEvent;

        public delegate void DataCreatedHandler(params string[] pxlhm_file);
        public DataCreatedHandler DataCreatedEvent;


        private void button_run_Click(object sender, EventArgs e)
        {
            string message = $"Are you sure you want to save the '{this.textBox_odata.Text}'?";
            string files = string.Empty;
            if (this.textBox_odata.BackColor == ErrorColor) files += $"\n{this.textBox_odata.Text}";
            if (this.textBox_pxlhm.BackColor == ErrorColor) files += $"\n{this.textBox_pxlhm.Text}";
            if (this.textBox_perm.BackColor == ErrorColor) files += $"\n{this.textBox_perm.Text}";
            if (this.textBox_rp_table.BackColor == ErrorColor) files += $"\n{this.textBox_rp_table.Text}";
            if (this.textBox_corey.BackColor == ErrorColor) files += $"\n{this.textBox_corey.Text}";
            if (this.textBox_multpv.BackColor == ErrorColor) files += $"\n{this.textBox_multpv.Text}";
            if (this.textBox_krorw.BackColor == ErrorColor) files += $"\n{this.textBox_krorw.Text}";
            if (this.textBox_krwr.BackColor == ErrorColor) files += $"\n{this.textBox_krwr.Text}";
            if (!string.IsNullOrEmpty(files)) message += "\nThe following file(s) will be replaced:" + files;

            if (MessageBox.Show(message, this.Text, MessageBoxButtons.YesNoCancel) != DialogResult.Yes) return;

            try
            {
                Input.Save(this.textBox_pxlhm.Text);
                HistMatchingInput.Load(this.textBox_pxlhm.Text, out HistMatchingInput histMatchingInput);
                histMatchingInput.DataFile = this.textBox_odata.Text;
                histMatchingInput.ParentCase = this.textBox_idata.Text;
                histMatchingInput.ParentID = this.textBox_id.Text;
                histMatchingInput.ParentIter = this.textBox_iter.Text;
                MsgEvent?.Invoke($"Writing '{this.textBox_odata.Text}'");
                File.WriteAllText(this.textBox_odata.Text, NewDataFileContent);
                if (!string.IsNullOrEmpty(this.textBox_perm.Text))
                {
                    MsgEvent?.Invoke($"Writing '{this.textBox_perm.Text}'");
                    CurIter.Props.Items[HistMatching.PermTitle].GetProp(CurGrid.Actnum).Write(Input.PermTitle, this.textBox_perm.Text, HistMatching.FILETYPE);
                    histMatchingInput.PermFile = this.textBox_perm.Text;
                }
                if (!string.IsNullOrEmpty(this.textBox_multpv.Text))
                {
                    MsgEvent?.Invoke($"Writing '{this.textBox_multpv.Text}'");
                    CurIter.Props.Items[HistMatching.MultpvTitle].GetProp(CurGrid.Actnum).Write(Input.MultpvTitle, this.textBox_multpv.Text, HistMatching.FILETYPE);
                    histMatchingInput.MultpvFile = this.textBox_multpv.Text;
                }
                if (!string.IsNullOrEmpty(this.textBox_krorw.Text))
                {
                    MsgEvent?.Invoke($"Writing '{this.textBox_krorw.Text}'");
                    CurIter.Props.Items[HistMatching.KrorwTitle].GetProp(CurGrid.Actnum).Write(Input.KrorwTitle, this.textBox_krorw.Text, HistMatching.FILETYPE);
                    histMatchingInput.KrorwFile = this.textBox_krorw.Text;
                }
                if (!string.IsNullOrEmpty(this.textBox_krwr.Text))
                {
                    MsgEvent?.Invoke($"Writing '{this.textBox_krwr.Text}'");
                    CurIter.Props.Items[HistMatching.KrwrTitle].GetProp(CurGrid.Actnum).Write(Input.KrwrTitle, this.textBox_krwr.Text, HistMatching.FILETYPE);
                    histMatchingInput.KrwrFile = this.textBox_krwr.Text;
                }
                if (!string.IsNullOrEmpty(this.textBox_rp_table.Text))
                {
                    MsgEvent?.Invoke($"Writing '{this.textBox_rp_table.Text}'");
                    CurIter.CoreySet.Export(this.textBox_rp_table.Text);
                    MsgEvent?.Invoke($"Writing '{this.textBox_corey.Text}'");
                    CurIter.CoreySet.Save(this.textBox_corey.Text);
                    histMatchingInput.RelPermTableFile = this.textBox_rp_table.Text;
                    histMatchingInput.CoreyInputFile = this.textBox_corey.Text;
                }
                MsgEvent?.Invoke($"Writing '{this.textBox_pxlhm.Text}'");
                histMatchingInput.Save(this.textBox_pxlhm.Text);
                MessageBox.Show($"'{this.textBox_odata.Text}' completed successfully!");
                DataCreatedEvent?.Invoke(this.textBox_pxlhm.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error! '{this.textBox_odata.Text}' not compiled!\nError:\n{ex.Message}");
            }
        }


        //Dictionary<string, ResultsViewDataOpt> Data = new Dictionary<string, ResultsViewDataOpt>();
        HistMatchingInput Input = new HistMatchingInput();
        string NewDataFileContent = string.Empty;
        Iteration CurIter = new Iteration();
        Grid CurGrid = new Grid();

        private void textBox_sufix_TextChanged(object sender, EventArgs e)
        {
            UpdateTitles();
        }


        Color PermitColor = Color.GreenYellow;
        Color ErrorColor = Color.OrangeRed;


        void UpdateTitles()
        {
            string sufix = this.textBox_sufix.Text;
            HistMatching.CreateNewDataFile(  Input, sufix,
                                                out string newDataFile,
                                                out NewDataFileContent,
                                                out string newRsmFile,
                                                out string newPermFile,
                                                out string newRelPermTableFile,
                                                out string newCoreyInputFile,
                                                out string newMultpvFile,
                                                out string newKrorwFile,
                                                out string newKrwrFile,
                                                out List<MsgLine> log);

            this.textBox_odata.Text = newDataFile;
            this.textBox_pxlhm.Text = Path.ChangeExtension(newDataFile, HistMatching.EXT);
            this.textBox_perm.Text = newPermFile;
            this.textBox_rp_table.Text = newRelPermTableFile;
            this.textBox_corey.Text = newCoreyInputFile;
            this.textBox_multpv.Text = newMultpvFile;
            this.textBox_krorw.Text = newKrorwFile;
            this.textBox_krwr.Text = newKrwrFile;

            this.textBox_odata.BackColor = !File.Exists(this.textBox_odata.Text) ? PermitColor : ErrorColor;
            this.textBox_pxlhm.BackColor = !File.Exists(this.textBox_pxlhm.Text) ? PermitColor : ErrorColor;
            this.textBox_perm.BackColor = !Input.PermMatching || !File.Exists(this.textBox_perm.Text) ? PermitColor : ErrorColor;
            this.textBox_rp_table.BackColor = !Input.RelPermMatching || !File.Exists(this.textBox_rp_table.Text) ? PermitColor : ErrorColor;
            this.textBox_corey.BackColor = !Input.RelPermMatching || !File.Exists(this.textBox_corey.Text) ? PermitColor : ErrorColor;
            this.textBox_multpv.BackColor = !Input.AquiferMatching || !File.Exists(this.textBox_multpv.Text) ? PermitColor : ErrorColor;
            this.textBox_krorw.BackColor = !Input.KrorwMatching || !File.Exists(this.textBox_krorw.Text) ? PermitColor : ErrorColor;
            this.textBox_krwr.BackColor = !Input.KrwrMatching || !File.Exists(this.textBox_krwr.Text) ? PermitColor : ErrorColor;
        }



        //string _pxlhm;


        public void UpdateData(Grid grid, Iteration iter, string pxlhm, string folder, string iter_number)
        {
            //_pxlhm = pxlhm;
            if (iter == null)
            {
                this.textBox_sufix.Enabled = false;
                this.button_run.Enabled = false;
                this.textBox_idata.Text = string.Empty;
                this.textBox_id.Text = string.Empty;
                this.textBox_iter.Text = string.Empty;
            }
            else
            {
                this.textBox_sufix.Enabled = true;
                this.button_run.Enabled = true;
                this.textBox_idata.Text = Path.GetFileNameWithoutExtension(Path.GetFileName(pxlhm));
                this.textBox_id.Text = HistMatching.GetID(folder);
                this.textBox_iter.Text = iter_number;
                CurIter = iter;
                CurGrid= grid;
                HistMatchingInput.Load(pxlhm, out HistMatchingInput input);
                input.SetNewPath(pxlhm);
                Input = input;
            }
        }



        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }

    }
}
