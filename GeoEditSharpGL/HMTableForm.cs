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
    public partial class HMTableForm : Form
    {
        public HMTableForm()
        {
            InitializeComponent();
        }



        string RSMFile { set { this.textBox_rsm.Text = value; } get { return this.textBox_rsm.Text; } }
        string IWSufix { set { this.textBox_suffix.Text = value; } get { return this.textBox_suffix.Text; } }
        //RsmType RsmType { get { return this.radioButton_tNav.Checked ? RsmType.tNavigator : RsmType.Eclipse; } }

        private void button_rsm_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = String.Format("RSM File (*.{0})|*.{0}|All Files (*.*)|*.*", "RSM");
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename != null && filename != string.Empty)
                RSMFile = filename;
        }

        private void button_run_Click(object sender, EventArgs e)
        {
            HM.IterationResult result = new HM.IterationResult(RSMFile, out _);
            /*
            if(!result.SUMMARYCompleted())
            {
                MessageBox.Show("RSM file not contains all nessusary vectors. Abort operation.");
                return;
            }
            */
            /*
            string ID = string.Format("{0}-{1,2:00}-{2,2:00}-{3,2:00}{4,2:00}",
                            DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);

            string file = System.IO.Path.ChangeExtension(RSMFile, null) + "_" + ID + ".xlsx";
            if (result.WriteTable(file, IWSufix))
                MessageBox.Show($"Complited sucesfully!\n{file}");
            else
                MessageBox.Show("Abort operation.");
            */
        }


        private void HMTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            e.Cancel = true;
            this.Hide();
            this.Parent = null;            
        }

        private void button_summary_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(string.Join("\n", HM.IterationResult.ExpectedSUMMARY()));
            MessageBox.Show("SUMMARY section copied to the clipboard.");
        }

        private void button_but_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("call $eclipse  \"C:\\data_file_with_no_extention\"");
            MessageBox.Show("BAT-file example copied to the clipboard.");
        }

        private void button_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "1. Run application.\n" +
                "2. Press application \"SUMMARY...\" button and add SUMMARY section to the your data-file.\n" +
                "3. Run your case with RSM output option.\n" +
                "  *Easy way is run case via BAT-file.\n" +
                "   Press application \"BAT-file...\" button and paste it to blank txt-file.\n" +
                "   Save as BAT-file and make double click.\n" +
                "   Black window will be opened. It means run has started.\n" +
                "   When black window will be closed it means run finished.\n" +
                "4. Specify suffix in \"Ignore Suffix\" field to join totals for separated prod and inje wells (example: 101 and 101-IW).\n" +
                "5. Add RMS-file to application and press application \"Run\" button.\n" +                
                "6. After about 1 minut Excel-file with results will be opened."
                );
        }

        private void button_tNav_bat_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("\"C:\\tNav_exe_file_place\\tNavigator2018.exe\" --cpu-num=24 --no-gui --ecl-rsm  \"C:\\data_file_with_no_extention\"");
            MessageBox.Show("BAT-file example copied to the clipboard.");
        }
    }
}
