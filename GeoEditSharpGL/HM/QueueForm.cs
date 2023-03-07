using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Pexel.HM
{
    public partial class QueueForm : Form
    {
        public QueueForm()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = $"PEXEL History Matching Project Files (*{HistMatching.EXT})|*{HistMatching.EXT}";
            dialog.Multiselect = true;
            dialog.ShowDialog();
            foreach(string file in dialog.FileNames)
                AddNewCase(file);
        }

        const int col_file = 1;
        string CaseFile(int i)
        {
            return this.dataGridView_cases.Rows[i].Cells[col_file].Value.ToString();
        }

        void AddNewCase(string file)
        {
            this.dataGridView_cases.Rows.Add(new object[] { string.Empty, file, string.Empty });
        }
        bool ConfirmRun()
        {
            string title = "Start running";
            string message = "Do you want to start running?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            return result == DialogResult.Yes;
        }

        private void button_run_Click(object sender, EventArgs e)
        {
            if (ConfirmRun()) 
                Run();
        }



        void Run()
        {
            for (int i = 0; i < this.dataGridView_cases.Rows.Count; ++i)
            {
                string file = CaseFile(i);
                HistMatchingInput input;
                if (HistMatchingInput.Load(file, out input))
                {
                    HistMatching hm = new HistMatching(input);
                    hm.Run();
                }
            }
        }



    }
}
