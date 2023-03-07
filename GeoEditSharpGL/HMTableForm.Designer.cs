namespace Pexel
{
    partial class HMTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMTableForm));
            this.button_run = new System.Windows.Forms.Button();
            this.button_rsm = new System.Windows.Forms.Button();
            this.textBox_rsm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_suffix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_summary = new System.Windows.Forms.Button();
            this.button_ecl_bat = new System.Windows.Forms.Button();
            this.button_help = new System.Windows.Forms.Button();
            this.button_tNav_bat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_run
            // 
            this.button_run.Location = new System.Drawing.Point(451, 41);
            this.button_run.Margin = new System.Windows.Forms.Padding(2);
            this.button_run.Name = "button_run";
            this.button_run.Size = new System.Drawing.Size(73, 50);
            this.button_run.TabIndex = 9;
            this.button_run.Text = "&Run";
            this.button_run.UseVisualStyleBackColor = true;
            this.button_run.Click += new System.EventHandler(this.button_run_Click);
            // 
            // button_rsm
            // 
            this.button_rsm.Location = new System.Drawing.Point(487, 11);
            this.button_rsm.Margin = new System.Windows.Forms.Padding(2);
            this.button_rsm.Name = "button_rsm";
            this.button_rsm.Size = new System.Drawing.Size(37, 20);
            this.button_rsm.TabIndex = 10;
            this.button_rsm.Text = "...";
            this.button_rsm.UseVisualStyleBackColor = true;
            this.button_rsm.Click += new System.EventHandler(this.button_rsm_Click);
            // 
            // textBox_rsm
            // 
            this.textBox_rsm.Location = new System.Drawing.Point(81, 11);
            this.textBox_rsm.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_rsm.Name = "textBox_rsm";
            this.textBox_rsm.Size = new System.Drawing.Size(402, 20);
            this.textBox_rsm.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "RSM";
            // 
            // textBox_suffix
            // 
            this.textBox_suffix.Location = new System.Drawing.Point(81, 41);
            this.textBox_suffix.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_suffix.Name = "textBox_suffix";
            this.textBox_suffix.Size = new System.Drawing.Size(154, 20);
            this.textBox_suffix.TabIndex = 8;
            this.textBox_suffix.Text = "-IW";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ignore Suffix";
            // 
            // button_summary
            // 
            this.button_summary.Location = new System.Drawing.Point(91, 68);
            this.button_summary.Margin = new System.Windows.Forms.Padding(2);
            this.button_summary.Name = "button_summary";
            this.button_summary.Size = new System.Drawing.Size(144, 23);
            this.button_summary.TabIndex = 9;
            this.button_summary.Text = "&SUMMARY to Clipboard";
            this.button_summary.UseVisualStyleBackColor = true;
            this.button_summary.Click += new System.EventHandler(this.button_summary_Click);
            // 
            // button_ecl_bat
            // 
            this.button_ecl_bat.Location = new System.Drawing.Point(250, 41);
            this.button_ecl_bat.Margin = new System.Windows.Forms.Padding(2);
            this.button_ecl_bat.Name = "button_ecl_bat";
            this.button_ecl_bat.Size = new System.Drawing.Size(144, 23);
            this.button_ecl_bat.TabIndex = 9;
            this.button_ecl_bat.Text = "Ecl &BAT-file to Clipboard";
            this.button_ecl_bat.UseVisualStyleBackColor = true;
            this.button_ecl_bat.Click += new System.EventHandler(this.button_but_Click);
            // 
            // button_help
            // 
            this.button_help.Location = new System.Drawing.Point(398, 41);
            this.button_help.Margin = new System.Windows.Forms.Padding(2);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(35, 50);
            this.button_help.TabIndex = 9;
            this.button_help.Text = "?";
            this.button_help.UseVisualStyleBackColor = true;
            this.button_help.Click += new System.EventHandler(this.button_help_Click);
            // 
            // button_tNav_bat
            // 
            this.button_tNav_bat.Location = new System.Drawing.Point(250, 68);
            this.button_tNav_bat.Margin = new System.Windows.Forms.Padding(2);
            this.button_tNav_bat.Name = "button_tNav_bat";
            this.button_tNav_bat.Size = new System.Drawing.Size(144, 23);
            this.button_tNav_bat.TabIndex = 9;
            this.button_tNav_bat.Text = "tNav &BAT-file to Clipboard";
            this.button_tNav_bat.UseVisualStyleBackColor = true;
            this.button_tNav_bat.Click += new System.EventHandler(this.button_tNav_bat_Click);
            // 
            // HMTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 100);
            this.Controls.Add(this.button_tNav_bat);
            this.Controls.Add(this.button_ecl_bat);
            this.Controls.Add(this.button_summary);
            this.Controls.Add(this.button_help);
            this.Controls.Add(this.button_run);
            this.Controls.Add(this.button_rsm);
            this.Controls.Add(this.textBox_suffix);
            this.Controls.Add(this.textBox_rsm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HMTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HM Quality";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HMTableForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_run;
        private System.Windows.Forms.Button button_rsm;
        private System.Windows.Forms.TextBox textBox_rsm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_suffix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_summary;
        private System.Windows.Forms.Button button_ecl_bat;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.Button button_tNav_bat;
    }
}