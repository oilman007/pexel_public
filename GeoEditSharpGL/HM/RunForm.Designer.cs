namespace Pexel.HM
{
    partial class RunForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunForm));
            this.richTextBox_msg = new System.Windows.Forms.RichTextBox();
            this.progressBar_iter = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_time_per_iter = new System.Windows.Forms.TextBox();
            this.textBox_time_spent = new System.Windows.Forms.TextBox();
            this.textBox_time_left = new System.Windows.Forms.TextBox();
            this.label_iter = new System.Windows.Forms.Label();
            this.progressBar_case = new System.Windows.Forms.ProgressBar();
            this.label_case = new System.Windows.Forms.Label();
            this.numericUpDown_last_iter = new System.Windows.Forms.NumericUpDown();
            this.textBox_cur_iter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel_menu = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_menu2 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_cpu = new System.Windows.Forms.NumericUpDown();
            this.label_max_cpu = new System.Windows.Forms.Label();
            this.checkBox_use_gpu = new System.Windows.Forms.CheckBox();
            this.comboBox_gpu_device = new System.Windows.Forms.ComboBox();
            this.button_iter_down = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_last_iter)).BeginInit();
            this.tableLayoutPanel_menu.SuspendLayout();
            this.tableLayoutPanel_menu2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_cpu)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox_msg
            // 
            this.richTextBox_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_msg.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_msg.Location = new System.Drawing.Point(16, 165);
            this.richTextBox_msg.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox_msg.Name = "richTextBox_msg";
            this.richTextBox_msg.ReadOnly = true;
            this.richTextBox_msg.Size = new System.Drawing.Size(1359, 429);
            this.richTextBox_msg.TabIndex = 1;
            this.richTextBox_msg.Text = "";
            this.richTextBox_msg.WordWrap = false;
            // 
            // progressBar_iter
            // 
            this.progressBar_iter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_iter.Location = new System.Drawing.Point(268, 41);
            this.progressBar_iter.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar_iter.Name = "progressBar_iter";
            this.progressBar_iter.Size = new System.Drawing.Size(1087, 18);
            this.progressBar_iter.Step = 1;
            this.progressBar_iter.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Time per iter";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(28, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time spent";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(45, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Time left";
            // 
            // textBox_time_per_iter
            // 
            this.textBox_time_per_iter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_time_per_iter.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_time_per_iter.Location = new System.Drawing.Point(117, 41);
            this.textBox_time_per_iter.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_time_per_iter.Name = "textBox_time_per_iter";
            this.textBox_time_per_iter.ReadOnly = true;
            this.textBox_time_per_iter.Size = new System.Drawing.Size(139, 25);
            this.textBox_time_per_iter.TabIndex = 4;
            this.textBox_time_per_iter.Text = "<unknown>";
            // 
            // textBox_time_spent
            // 
            this.textBox_time_spent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_time_spent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_time_spent.Location = new System.Drawing.Point(117, 74);
            this.textBox_time_spent.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_time_spent.Name = "textBox_time_spent";
            this.textBox_time_spent.ReadOnly = true;
            this.textBox_time_spent.Size = new System.Drawing.Size(139, 25);
            this.textBox_time_spent.TabIndex = 4;
            this.textBox_time_spent.Text = "<unknown>";
            // 
            // textBox_time_left
            // 
            this.textBox_time_left.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_time_left.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_time_left.Location = new System.Drawing.Point(117, 110);
            this.textBox_time_left.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_time_left.Name = "textBox_time_left";
            this.textBox_time_left.ReadOnly = true;
            this.textBox_time_left.Size = new System.Drawing.Size(139, 25);
            this.textBox_time_left.TabIndex = 4;
            this.textBox_time_left.Text = "<unknown>";
            // 
            // label_iter
            // 
            this.label_iter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_iter.AutoSize = true;
            this.label_iter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_iter.Location = new System.Drawing.Point(4, 9);
            this.label_iter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_iter.Name = "label_iter";
            this.label_iter.Size = new System.Drawing.Size(32, 18);
            this.label_iter.TabIndex = 3;
            this.label_iter.Text = "Iter:";
            // 
            // progressBar_case
            // 
            this.progressBar_case.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_case.Location = new System.Drawing.Point(268, 107);
            this.progressBar_case.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar_case.Name = "progressBar_case";
            this.progressBar_case.Size = new System.Drawing.Size(1087, 18);
            this.progressBar_case.Step = 1;
            this.progressBar_case.TabIndex = 2;
            // 
            // label_case
            // 
            this.label_case.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_case.AutoSize = true;
            this.label_case.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_case.Location = new System.Drawing.Point(268, 77);
            this.label_case.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_case.Name = "label_case";
            this.label_case.Size = new System.Drawing.Size(47, 18);
            this.label_case.TabIndex = 3;
            this.label_case.Text = "Case:";
            // 
            // numericUpDown_last_iter
            // 
            this.numericUpDown_last_iter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDown_last_iter.Enabled = false;
            this.numericUpDown_last_iter.Location = new System.Drawing.Point(125, 7);
            this.numericUpDown_last_iter.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_last_iter.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown_last_iter.Name = "numericUpDown_last_iter";
            this.numericUpDown_last_iter.Size = new System.Drawing.Size(61, 22);
            this.numericUpDown_last_iter.TabIndex = 5;
            this.numericUpDown_last_iter.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown_last_iter.ValueChanged += new System.EventHandler(this.numericUpDown_last_iter_ValueChanged);
            // 
            // textBox_cur_iter
            // 
            this.textBox_cur_iter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_cur_iter.Location = new System.Drawing.Point(51, 7);
            this.textBox_cur_iter.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_cur_iter.Name = "textBox_cur_iter";
            this.textBox_cur_iter.ReadOnly = true;
            this.textBox_cur_iter.Size = new System.Drawing.Size(41, 22);
            this.textBox_cur_iter.TabIndex = 6;
            this.textBox_cur_iter.Text = "9999";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(106, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "/";
            // 
            // tableLayoutPanel_menu
            // 
            this.tableLayoutPanel_menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_menu.ColumnCount = 3;
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_menu.Controls.Add(this.label_case, 2, 2);
            this.tableLayoutPanel_menu.Controls.Add(this.progressBar_iter, 2, 1);
            this.tableLayoutPanel_menu.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel_menu.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel_menu.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel_menu.Controls.Add(this.progressBar_case, 2, 3);
            this.tableLayoutPanel_menu.Controls.Add(this.textBox_time_left, 1, 3);
            this.tableLayoutPanel_menu.Controls.Add(this.textBox_time_per_iter, 1, 1);
            this.tableLayoutPanel_menu.Controls.Add(this.textBox_time_spent, 1, 2);
            this.tableLayoutPanel_menu.Controls.Add(this.tableLayoutPanel_menu2, 2, 0);
            this.tableLayoutPanel_menu.Location = new System.Drawing.Point(16, 15);
            this.tableLayoutPanel_menu.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel_menu.Name = "tableLayoutPanel_menu";
            this.tableLayoutPanel_menu.RowCount = 4;
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_menu.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_menu.Size = new System.Drawing.Size(1359, 143);
            this.tableLayoutPanel_menu.TabIndex = 7;
            // 
            // tableLayoutPanel_menu2
            // 
            this.tableLayoutPanel_menu2.ColumnCount = 11;
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 415F));
            this.tableLayoutPanel_menu2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_menu2.Controls.Add(this.label_iter, 0, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.numericUpDown_last_iter, 3, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.textBox_cur_iter, 1, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.label6, 5, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.numericUpDown_cpu, 6, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.label_max_cpu, 7, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.checkBox_use_gpu, 8, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.comboBox_gpu_device, 9, 0);
            this.tableLayoutPanel_menu2.Controls.Add(this.button_iter_down, 4, 0);
            this.tableLayoutPanel_menu2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_menu2.Location = new System.Drawing.Point(264, 0);
            this.tableLayoutPanel_menu2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_menu2.Name = "tableLayoutPanel_menu2";
            this.tableLayoutPanel_menu2.RowCount = 1;
            this.tableLayoutPanel_menu2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_menu2.Size = new System.Drawing.Size(1095, 37);
            this.tableLayoutPanel_menu2.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(273, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 3;
            this.label6.Text = "CPU:";
            // 
            // numericUpDown_cpu
            // 
            this.numericUpDown_cpu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDown_cpu.Enabled = false;
            this.numericUpDown_cpu.Location = new System.Drawing.Point(328, 7);
            this.numericUpDown_cpu.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_cpu.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_cpu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_cpu.Name = "numericUpDown_cpu";
            this.numericUpDown_cpu.Size = new System.Drawing.Size(52, 22);
            this.numericUpDown_cpu.TabIndex = 5;
            this.numericUpDown_cpu.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_cpu.ValueChanged += new System.EventHandler(this.numericUpDown_cpu_ValueChanged);
            // 
            // label_max_cpu
            // 
            this.label_max_cpu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_max_cpu.AutoSize = true;
            this.label_max_cpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_max_cpu.Location = new System.Drawing.Point(388, 10);
            this.label_max_cpu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_max_cpu.Name = "label_max_cpu";
            this.label_max_cpu.Size = new System.Drawing.Size(12, 17);
            this.label_max_cpu.TabIndex = 3;
            this.label_max_cpu.Text = "/";
            // 
            // checkBox_use_gpu
            // 
            this.checkBox_use_gpu.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checkBox_use_gpu.AutoSize = true;
            this.checkBox_use_gpu.Enabled = false;
            this.checkBox_use_gpu.Location = new System.Drawing.Point(496, 8);
            this.checkBox_use_gpu.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_use_gpu.Name = "checkBox_use_gpu";
            this.checkBox_use_gpu.Size = new System.Drawing.Size(86, 20);
            this.checkBox_use_gpu.TabIndex = 7;
            this.checkBox_use_gpu.Text = "Use GPU";
            this.checkBox_use_gpu.UseVisualStyleBackColor = true;
            this.checkBox_use_gpu.CheckedChanged += new System.EventHandler(this.checkBox_use_gpu_CheckedChanged);
            // 
            // comboBox_gpu_device
            // 
            this.tableLayoutPanel_menu2.SetColumnSpan(this.comboBox_gpu_device, 2);
            this.comboBox_gpu_device.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_gpu_device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_gpu_device.Enabled = false;
            this.comboBox_gpu_device.FormattingEnabled = true;
            this.comboBox_gpu_device.Location = new System.Drawing.Point(590, 4);
            this.comboBox_gpu_device.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_gpu_device.Name = "comboBox_gpu_device";
            this.comboBox_gpu_device.Size = new System.Drawing.Size(501, 24);
            this.comboBox_gpu_device.TabIndex = 8;
            this.comboBox_gpu_device.SelectedIndexChanged += new System.EventHandler(this.comboBox_gpu_device_SelectedIndexChanged);
            // 
            // button_iter_down
            // 
            this.button_iter_down.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_iter_down.Enabled = false;
            this.button_iter_down.Image = global::Pexel.Properties.Resources.down_arrow;
            this.button_iter_down.Location = new System.Drawing.Point(194, 6);
            this.button_iter_down.Margin = new System.Windows.Forms.Padding(4);
            this.button_iter_down.Name = "button_iter_down";
            this.button_iter_down.Size = new System.Drawing.Size(27, 25);
            this.button_iter_down.TabIndex = 9;
            this.button_iter_down.UseVisualStyleBackColor = true;
            this.button_iter_down.Click += new System.EventHandler(this.button_iter_down_Click);
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 609);
            this.Controls.Add(this.tableLayoutPanel_menu);
            this.Controls.Add(this.richTextBox_msg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1405, 638);
            this.Name = "RunForm";
            this.Text = "PEXEL History Matching Run";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RunForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RunForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_last_iter)).EndInit();
            this.tableLayoutPanel_menu.ResumeLayout(false);
            this.tableLayoutPanel_menu.PerformLayout();
            this.tableLayoutPanel_menu2.ResumeLayout(false);
            this.tableLayoutPanel_menu2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_cpu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox_msg;
        private System.Windows.Forms.ProgressBar progressBar_iter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_time_per_iter;
        private System.Windows.Forms.TextBox textBox_time_spent;
        private System.Windows.Forms.TextBox textBox_time_left;
        private System.Windows.Forms.Label label_iter;
        private System.Windows.Forms.ProgressBar progressBar_case;
        private System.Windows.Forms.Label label_case;
        private System.Windows.Forms.NumericUpDown numericUpDown_last_iter;
        private System.Windows.Forms.TextBox textBox_cur_iter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_menu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_menu2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_cpu;
        private System.Windows.Forms.Label label_max_cpu;
        private System.Windows.Forms.CheckBox checkBox_use_gpu;
        private System.Windows.Forms.ComboBox comboBox_gpu_device;
        private System.Windows.Forms.Button button_iter_down;
    }
}