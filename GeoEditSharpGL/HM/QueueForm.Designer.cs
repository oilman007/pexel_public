namespace Pexel.HM
{
    partial class QueueForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueueForm));
            this.button_add = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            this.button_remove_all = new System.Windows.Forms.Button();
            this.button_run = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.dataGridView_cases = new System.Windows.Forms.DataGridView();
            this.ColumnN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox_use_gpu = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_cases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_add
            // 
            this.button_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add.Location = new System.Drawing.Point(788, 12);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(78, 23);
            this.button_add.TabIndex = 1;
            this.button_add.Text = "&Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_remove
            // 
            this.button_remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_remove.Location = new System.Drawing.Point(788, 41);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(78, 23);
            this.button_remove.TabIndex = 1;
            this.button_remove.Text = "R&emove";
            this.button_remove.UseVisualStyleBackColor = true;
            // 
            // button_remove_all
            // 
            this.button_remove_all.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_remove_all.Location = new System.Drawing.Point(788, 70);
            this.button_remove_all.Name = "button_remove_all";
            this.button_remove_all.Size = new System.Drawing.Size(78, 23);
            this.button_remove_all.TabIndex = 1;
            this.button_remove_all.Text = "Remove &All";
            this.button_remove_all.UseVisualStyleBackColor = true;
            // 
            // button_run
            // 
            this.button_run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_run.Location = new System.Drawing.Point(788, 385);
            this.button_run.Name = "button_run";
            this.button_run.Size = new System.Drawing.Size(78, 23);
            this.button_run.TabIndex = 1;
            this.button_run.Text = "&Run";
            this.button_run.UseVisualStyleBackColor = true;
            this.button_run.Click += new System.EventHandler(this.button_run_Click);
            // 
            // button_stop
            // 
            this.button_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_stop.Location = new System.Drawing.Point(788, 414);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(78, 23);
            this.button_stop.TabIndex = 1;
            this.button_stop.Text = "&Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            // 
            // dataGridView_cases
            // 
            this.dataGridView_cases.AllowUserToAddRows = false;
            this.dataGridView_cases.AllowUserToDeleteRows = false;
            this.dataGridView_cases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_cases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnN,
            this.ColumnCase,
            this.ColumnStatus});
            this.dataGridView_cases.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_cases.Name = "dataGridView_cases";
            this.dataGridView_cases.Size = new System.Drawing.Size(770, 331);
            this.dataGridView_cases.TabIndex = 2;
            // 
            // ColumnN
            // 
            this.ColumnN.FillWeight = 30F;
            this.ColumnN.HeaderText = "#";
            this.ColumnN.Name = "ColumnN";
            this.ColumnN.Width = 30;
            // 
            // ColumnCase
            // 
            this.ColumnCase.FillWeight = 500F;
            this.ColumnCase.HeaderText = "Case";
            this.ColumnCase.Name = "ColumnCase";
            this.ColumnCase.Width = 500;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.Name = "ColumnStatus";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(68, 349);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 3;
            // 
            // checkBox_use_gpu
            // 
            this.checkBox_use_gpu.AutoSize = true;
            this.checkBox_use_gpu.Location = new System.Drawing.Point(210, 350);
            this.checkBox_use_gpu.Name = "checkBox_use_gpu";
            this.checkBox_use_gpu.Size = new System.Drawing.Size(80, 17);
            this.checkBox_use_gpu.TabIndex = 4;
            this.checkBox_use_gpu.Text = "checkBox1";
            this.checkBox_use_gpu.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // QueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 449);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_use_gpu);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.dataGridView_cases);
            this.Controls.Add(this.button_remove_all);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_run);
            this.Controls.Add(this.button_add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueueForm";
            this.Text = "PEXEL Queue";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_cases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.Button button_remove_all;
        private System.Windows.Forms.Button button_run;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.DataGridView dataGridView_cases;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCase;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBox_use_gpu;
        private System.Windows.Forms.Label label1;
    }
}