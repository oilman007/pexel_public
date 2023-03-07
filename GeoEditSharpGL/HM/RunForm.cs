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
using System.IO;

namespace Pexel.HM
{
    public partial class RunForm : Form
    {
        public RunForm()
        {
            InitializeComponent();
            this.progressBar_case.Minimum = 0;
            this.progressBar_case.Maximum = 100;
            this.progressBar_case.Step = 1;
            this.Text = $"{LauncherForm.AppName()} History Matching Run";
        }


        string _datafile = string.Empty;
        public string DataFile
        { 
            set
            {
                _datafile = value;
                this.Text += " | " + Path.ChangeExtension(value, null);
            }
            get
            {
                return _datafile;
            }
        }

        public void CloseMe(object o, EventArgs args)
        {
            this.Close();
        }

        private void RunForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AbortRunEvent?.Invoke(this, EventArgs.Empty);
        }

        // delegate
        //public delegate void MsgHandler(string message, bool append);


        public EventHandler StopRunEvent;
        public EventHandler AbortRunEvent;
        public EventHandler CloseEvent;


        public delegate void IterChangedHandler(int lastIter);
        public IterChangedHandler LastIterChangedEvent;

        public delegate void CPUNumberChangedHandler(int cpu_number);
        public CPUNumberChangedHandler CPUNumberChangedEvent;

        public delegate void GPUChangedHandler(bool use_gpu, int gpu_device);
        public GPUChangedHandler GPUChangedEvent;


        public void IDChanged(string id)
        {
            this?.BeginInvoke(new Action(() =>
            {
                this.Text += " | " + id;
            }));
        }



        public void ShowMsg(DateTime dt, HMMessageType messageType, params string[] messages)
        {
            try
            {
                richTextBox_msg?.BeginInvoke(new Action(() =>
                {
                    foreach (string message in messages)
                    {
                        string text = HistMatching.DisplayLine(dt, message) + Environment.NewLine;
                        switch (messageType)
                        {
                            case HMMessageType.Comment:
                                richTextBox_msg?.AppendText(text);
                                break;
                            case HMMessageType.Message:
                                richTextBox_msg?.AppendText(text);
                                break;
                            case HMMessageType.Warning:
                                richTextBox_msg.SelectionStart = richTextBox_msg.TextLength;
                                richTextBox_msg.SelectionLength = 0;
                                richTextBox_msg.SelectionBackColor = Color.Yellow;
                                richTextBox_msg?.AppendText(text);
                                //richTextBox_msg.SelectionColor = richTextBox_msg.ForeColor;
                                break;
                            case HMMessageType.Error:
                                richTextBox_msg.SelectionStart = richTextBox_msg.TextLength;
                                richTextBox_msg.SelectionLength = 0;
                                richTextBox_msg.SelectionBackColor = Color.Red;
                                richTextBox_msg?.AppendText(text);
                                //richTextBox_msg.SelectionColor = richTextBox_msg.ForeColor;
                                break;
                        }
                    }
                    richTextBox_msg?.Select(this.richTextBox_msg.Text.Length - 1, 0);
                    richTextBox_msg?.ScrollToCaret();
                }));
            }
            catch { }
        }



        bool run_finished = false;
        public void RunFinished(object o, EventArgs args)
        {
            run_finished = true;
            case_percent?.Abort();
            SetCasePercent(100);
            /*
            this.button_stop?.BeginInvoke(new Action(() =>
            {
                this.button_stop.Enabled = false;
            }));
            */
            this.numericUpDown_last_iter?.BeginInvoke(new Action(() =>
            {
                numericUpDown_last_iter.Enabled = false;
            }));
            this.button_iter_down?.BeginInvoke(new Action(() =>
            {
                button_iter_down.Enabled = false;
            }));
            this.numericUpDown_cpu?.BeginInvoke(new Action(() =>
            {
                numericUpDown_cpu.Enabled = false;
            }));
            this.checkBox_use_gpu?.BeginInvoke(new Action(() =>
            {
                checkBox_use_gpu.Enabled = false;
            }));
            this.comboBox_gpu_device?.BeginInvoke(new Action(() =>
            {
                comboBox_gpu_device.Enabled = false;
            }));
        }


        public void RunStarted(RunInfo info)
        {
            run_finished = false;
            /*
            this.button_stop?.BeginInvoke(new Action(() =>
            {
                this.button_stop.Enabled = true;
            }));

            this.button_result_viewer?.BeginInvoke(new Action(() =>
            {
                button_result_viewer.Enabled = true;
            }));
            */
            this.numericUpDown_last_iter?.BeginInvoke(new Action(() =>
            {
                numericUpDown_last_iter.Enabled = true;
            }));
            this.button_iter_down?.BeginInvoke(new Action(() =>
            {
                button_iter_down.Enabled = true;
            }));
            this.numericUpDown_cpu?.BeginInvoke(new Action(() =>
            {
                numericUpDown_cpu.Enabled = true;
            }));
            this.checkBox_use_gpu?.BeginInvoke(new Action(() =>
            {
                checkBox_use_gpu.Enabled = true;
            }));
            this.comboBox_gpu_device?.BeginInvoke(new Action(() =>
            {
                comboBox_gpu_device.Enabled = true;
            }));

            /*
            this.textBox_cur_iter?.BeginInvoke(new Action(() =>
            {
                textBox_cur_iter.Text = $"{Math.Min(info.CurIter, info.LastIter)}";
            }));
            */
            this.numericUpDown_last_iter?.BeginInvoke(new Action(() =>
            {
                numericUpDown_last_iter.Value = info.LastIter;
                numericUpDown_last_iter.Minimum = info.LastIter;
            }));
        }



        void SetIter(RunInfo info)
        {
            this.progressBar_iter?.BeginInvoke(new Action(() =>
            {
                this.progressBar_iter.Minimum = info.FirstIter;
                this.progressBar_iter.Maximum = info.LastIter + 1;
                this.progressBar_iter.Value = info.CurIter;
            }));
            /*
            this.label_iter?.BeginInvoke(new Action(() =>
            {
                label_iter.Text = "Iter: " + Math.Min(info.CurIter, info.LastIter) + "/" + info.LastIter;
            }));
            */
            this.textBox_cur_iter?.BeginInvoke(new Action(() =>
            {
                textBox_cur_iter.Text = $"{Math.Min(info.CurIter, info.LastIter)}";
            }));
            this.numericUpDown_last_iter?.BeginInvoke(new Action(() =>
            {
                numericUpDown_last_iter.Value = info.LastIter;
                numericUpDown_last_iter.Minimum = Math.Min(info.CurIter, info.LastIter);
            }));
        }

        void SetCasePercent(int i)
        {
            this.progressBar_case?.BeginInvoke(new Action(() =>
            {
                this.progressBar_case.Value = i;
            }));
            this.label_case?.BeginInvoke(new Action(() =>
            {
                label_case.Text = $"Case: {i}%";
            }));
        }

        const string format = @"dd\ hh\:mm\:ss";
        //const string format = @"hh\:mm\:ss";
        const string unknown = "<unknown>";

        Thread case_percent;
        public void ShowRunInfo(RunInfo info)
        {
            SetIter(info);

            this.textBox_time_per_iter?.BeginInvoke(new Action(() =>
            {
                string text = unknown;
                if (info.TimePerIter != TimeSpan.Zero)
                    text = info.TimePerIter.ToString(format);
                this.textBox_time_per_iter.Text = text;
            }));

            this.textBox_time_spent?.BeginInvoke(new Action(() =>
            {
                string text = unknown;
                if (info.TimePerIter != TimeSpan.Zero)
                    text = TimeSpan.FromSeconds(info.TimePerIter.TotalSeconds * (info.CurIter - info.FirstIter)).ToString(format);
                this.textBox_time_spent.Text = text;
            }));

            this.textBox_time_left?.BeginInvoke(new Action(() =>
            {
                string text = unknown;
                if (info.TimePerIter != TimeSpan.Zero)
                    text = TimeSpan.FromSeconds(info.TimePerIter.TotalSeconds * (info.LastIter - info.CurIter + 1)).ToString(format);
                this.textBox_time_left.Text = text;
            }));

            case_percent?.Abort();
            case_percent = new Thread(new ThreadStart(() =>
            {
                if (info.TimePerIter.Seconds > 0)
                {
                    int delay = ((int)info.TimePerIter.TotalSeconds) * 10; // 10 = 1000 / 100 * 1.0
                    for (int i = 0; i < 100; ++i)
                    {
                        Thread.Sleep(delay);
                        SetCasePercent(i);
                    }
                }
                else
                {
                    SetCasePercent(0);
                }
            }));
            case_percent.Start();
        }


        private void button_stop_Click(object sender, EventArgs e)
        {
            string title = "Stop RUN";
            string message =
                    "The history matching process will be finished after the simulator finished the current iteraton.\n" +
                    "Do you want to stop running?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                StopRunEvent?.Invoke(this, e);
                //button_stop.Enabled = false;
                numericUpDown_last_iter.Value = Helper.ParseDecimal(textBox_cur_iter.Text);
                numericUpDown_last_iter.Enabled = false;
            }
        }

        private void RunForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (run_finished) return;
            string title = "Abort RUN";
            string message =
                    "If you close the window, the process will be interrupted and the results of the current iteration will not be saved.\n" +
                    "Do you want to abort running?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }

        private void numericUpDown_last_iter_ValueChanged(object sender, EventArgs e)
        {
            LastIterChangedEvent?.Invoke((int)this.numericUpDown_last_iter.Value);
        }



        /*
        private void button_result_viewer_Click(object sender, EventArgs e)
        {
            string filename = Path.ChangeExtension(DataFile, HistMatching.EXT);
#if DEBUG
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HM.ResultsViewTreeForm(filename));
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
#else
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Application.Run(new HM.ResultsViewForm(filename));
            }));
            thread.ApartmentState = ApartmentState.STA;
            thread.Start();
            //LauncherForm.Start(LauncherForm.ResultsViewerArg, filename);
#endif
        }
        */

        public void CleanMsg(object o, EventArgs args)
        {
            richTextBox_msg?.BeginInvoke(new Action(() =>
            {
                this.richTextBox_msg.Clear();
            }));
        }


        private void numericUpDown_cpu_ValueChanged(object sender, EventArgs e)
        {
            int cpu = (int)this.numericUpDown_cpu.Value;
            CPUNumberChangedEvent?.Invoke(cpu);
        }

        private void checkBox_use_gpu_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBox_gpu_device.Visible = checkBox_use_gpu.Checked;
            GPUChanged();
        }

        private void comboBox_gpu_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            GPUChanged();
        }

        void GPUChanged()
        {
            bool use_gpu = this.checkBox_use_gpu.Checked;
            int gpu_device = this.comboBox_gpu_device.SelectedIndex;
            GPUChangedEvent?.Invoke(use_gpu, gpu_device);
        }


        public string[] GPUList
        {
            set
            {
                this.comboBox_gpu_device.Items.Clear();
                foreach (string item in value)
                    this.comboBox_gpu_device.Items.Add(item);
            }
        }


        public void SetGPUDevice(bool support, bool use, int device)
        {
            this.comboBox_gpu_device.SelectedIndex = device;
            //this.comboBox_gpu_device.Visible = use;
            //this.checkBox_use_gpu.Visible = use;
            this.checkBox_use_gpu.Checked = use;

            this.checkBox_use_gpu.Visible = support;
            this.comboBox_gpu_device.Visible = support && use;

            //checkBox_use_gpu_CheckedChanged(null, null);
        }


        public int MaxCPUNumber
        {
            set
            {
                this.label_max_cpu.Text = $"/ {Helper.ShowInt(value)}";
                this.numericUpDown_cpu.Maximum = value;
            }
        }

        public int CPUNumber
        {
            set
            {
                this.numericUpDown_cpu.Value = value;
            }
        }

        private void button_iter_down_Click(object sender, EventArgs e)
        {
            this.numericUpDown_last_iter.Value = this.numericUpDown_last_iter.Minimum;
        }




        BackgroundWorker iter_prepare_bw = new BackgroundWorker();
        public void IterPrepareStart(object o, EventArgs args)
        {
            this.progressBar_case?.BeginInvoke(new Action(() =>
            {
                progressBar_case.Style = ProgressBarStyle.Marquee;
                progressBar_case.MarqueeAnimationSpeed = 75;

                /*
                iter_prepare_bw = new BackgroundWorker();
                iter_prepare_bw.WorkerSupportsCancellation = true;
                iter_prepare_bw.DoWork += bw_DoWork;
                iter_prepare_bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                iter_prepare_bw.RunWorkerAsync();
                */
            }));
        }

        public void IterPrepareEnd(object o, EventArgs args)
        {
            //iter_prepare_bw?.CancelAsync();
            this.progressBar_case?.BeginInvoke(new Action(() =>
            {
                progressBar_case.MarqueeAnimationSpeed = 0;
                progressBar_case.Style = ProgressBarStyle.Blocks;
                //progressBar_case.Value = progressBar_case.Minimum;
            }));
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // INSERT TIME CONSUMING OPERATIONS HERE
            // THAT DON'T REPORT PROGRESS
            Thread.Sleep(1000000000);
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }



    }
}
