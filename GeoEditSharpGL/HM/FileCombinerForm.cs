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

namespace Pexel.HM
{
    public partial class FileCombinerForm : Form
    {
        public FileCombinerForm()
        {
            InitializeComponent();
            UpdateState();
        }


        public delegate void CopyHandler(string gridfile);
        public CopyHandler GridFileHandler;


        private void button_add_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = string.Format("TXT Files (*.*)|*.*");
            dialog.Multiselect = true;
            dialog.ShowDialog();
            foreach (string file in dialog.FileNames)
                this.listBox_input_files.Items.Add(file);
            UpdateState();
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            this.listBox_input_files.Items.RemoveAt(this.listBox_input_files.SelectedIndex);
            UpdateState();
        }

        private void button_remove_all_Click(object sender, EventArgs e)
        {
            this.listBox_input_files.Items.Clear();
            UpdateState();
        }




        public void MoveItem(int direction)
        {
            // Checking selected item
            if (listBox_input_files.SelectedItem == null || listBox_input_files.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = listBox_input_files.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= listBox_input_files.Items.Count)
                return; // Index out of range - nothing to do

            object selected = listBox_input_files.SelectedItem;

            // Removing removable element
            listBox_input_files.Items.Remove(selected);
            // Insert it in new position
            listBox_input_files.Items.Insert(newIndex, selected);
            // Restore selection
            listBox_input_files.SetSelected(newIndex, true);
        }
        private void button_up_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            MoveItem(+1);
        }

        private void button_output_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            //dialog.Filter = string.Format("TXT Files (*.*)|*.*");
            //dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialog.FileName))
                this.textBox_output_file.Text = dialog.FileName;
        }


        void ShowMsg(string msg)
        {
            this.statusStrip?.BeginInvoke(new Action(() =>
            {
                this.toolStripStatusLabel.Text = msg;
            }));
        }


        private void button_run_Click(object sender, EventArgs e)
        {
            Task t = Task.Run(() => Run());
        }

        void Run()
        {
            try
            {
                this.button_run?.BeginInvoke(new Action(() =>
                {
                    button_run.Enabled = false;
                }));

                string[][] files = new string[this.listBox_input_files.Items.Count][];
                int total = 0;
                for (int i = 0; i < this.listBox_input_files.Items.Count; ++i)
                {
                    string file = this.listBox_input_files.Items[i].ToString();
                    ShowMsg($"Reading '{file}'");
                    files[i] = File.ReadAllLines(file);
                    total += files[i].Length;
                }

                string[] outfile = new string[total];
                ShowMsg($"Spliting files...");
                for (int i = 0, p = 0; i < this.listBox_input_files.Items.Count; p += files[i++].Length)
                {
                    files[i].CopyTo(outfile, p);
                }

                ShowMsg($"Writing '{this.textBox_output_file.Text}'");
                File.WriteAllLines(this.textBox_output_file.Text, outfile);

                ShowMsg($"Сompleted successfully");
            }
            catch (Exception ex)
            {
                ShowMsg($"Operation error");
            }
            finally
            {
                GC.Collect();
                this.button_run?.BeginInvoke(new Action(() =>
                {
                    button_run.Enabled = true;
                }));
            }
        }




        void UpdateState()
        {
            button_run.Enabled = !string.IsNullOrEmpty(this.textBox_output_file.Text) && (this.listBox_input_files.Items.Count > 0);
            button_down.Enabled = (this.listBox_input_files.Items.Count > 1) &&
                                  (this.listBox_input_files.SelectedIndex > -1) &&
                                  (this.listBox_input_files.SelectedIndex != this.listBox_input_files.Items.Count - 1);
            button_up.Enabled = (this.listBox_input_files.Items.Count > 1) &&
                                (this.listBox_input_files.SelectedIndex > -1) &&
                                (this.listBox_input_files.SelectedIndex != 0);
            button_copy.Enabled = !string.IsNullOrEmpty(this.textBox_output_file.Text);
        }

        private void textBox_output_file_TextChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void listBox_input_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void button_copy_Click(object sender, EventArgs e)
        {
            GridFileHandler?.Invoke(this.textBox_output_file.Text);
        }
    }
}
