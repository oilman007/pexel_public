using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexel.SCAL
{
    public partial class ExportSetForm : Form
    {
        public ExportSetForm()
        {
            InitializeComponent();
            this.comboBox_tables.SelectedIndex = 0;
        }


        public CoreySet CoreySet { set; get; } = new CoreySet();


        private void textBox_tables_TextChanged(object sender, EventArgs e)
        {
            this.textBox_corey.Text = 
                this.checkBox_export_corey.Checked ? Path.ChangeExtension(textBox_tables.Text, CoreySetForm.EXT) : string.Empty;
        }

        private void button_file_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = string.Format("TXT Files (*.{0})|*.{0}|All Files (*.*)|*.*", "TXT");
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_tables.Text = saveFileDialog.FileName;
            }
        }


        private void button_export_Click(object sender, EventArgs e)
        {
            try
            {
                CoreySet.SWOF = this.comboBox_tables.SelectedIndex != 2;
                CoreySet.SGOF = this.comboBox_tables.SelectedIndex != 1;
                string text = string.Empty;
                if (CoreySet.Export(this.textBox_tables.Text))
                    text += $"File '{this.textBox_tables.Text}' exported successfully!";
                else
                    text += $"File '{this.textBox_tables.Text}' export error!";
                CoreySet.Export(this.textBox_tables.Text);
                if (this.checkBox_export_corey.Checked)
                {
                    if (CoreySet.Save(this.textBox_corey.Text))
                        text += Environment.NewLine + $"File '{this.textBox_corey.Text}' exported successfully!";
                    else
                        text += Environment.NewLine + $"File '{this.textBox_corey.Text}' export error!";
                }
                MessageBox.Show(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void ExportSetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }








    }
}
