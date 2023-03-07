namespace Pexel
{
    partial class HFileForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_rsm = new System.Windows.Forms.TextBox();
            this.button_rsm = new System.Windows.Forms.Button();
            this.button_run = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_wefac = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RSM";
            // 
            // textBox_rsm
            // 
            this.textBox_rsm.Location = new System.Drawing.Point(68, 10);
            this.textBox_rsm.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_rsm.Name = "textBox_rsm";
            this.textBox_rsm.Size = new System.Drawing.Size(408, 20);
            this.textBox_rsm.TabIndex = 1;
            // 
            // button_rsm
            // 
            this.button_rsm.Location = new System.Drawing.Point(480, 10);
            this.button_rsm.Margin = new System.Windows.Forms.Padding(2);
            this.button_rsm.Name = "button_rsm";
            this.button_rsm.Size = new System.Drawing.Size(32, 19);
            this.button_rsm.TabIndex = 2;
            this.button_rsm.Text = "...";
            this.button_rsm.UseVisualStyleBackColor = true;
            this.button_rsm.Click += new System.EventHandler(this.button_rsm_Click);
            // 
            // button_run
            // 
            this.button_run.Location = new System.Drawing.Point(439, 37);
            this.button_run.Margin = new System.Windows.Forms.Padding(2);
            this.button_run.Name = "button_run";
            this.button_run.Size = new System.Drawing.Size(73, 38);
            this.button_run.TabIndex = 2;
            this.button_run.Text = "&Run";
            this.button_run.UseVisualStyleBackColor = true;
            this.button_run.Click += new System.EventHandler(this.button_run_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "WEFAC";
            // 
            // textBox_wefac
            // 
            this.textBox_wefac.Location = new System.Drawing.Point(68, 34);
            this.textBox_wefac.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_wefac.Name = "textBox_wefac";
            this.textBox_wefac.Size = new System.Drawing.Size(120, 20);
            this.textBox_wefac.TabIndex = 1;
            this.textBox_wefac.Text = "0.95";
            // 
            // HFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 87);
            this.Controls.Add(this.button_run);
            this.Controls.Add(this.button_rsm);
            this.Controls.Add(this.textBox_wefac);
            this.Controls.Add(this.textBox_rsm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HFileForm";
            this.Text = "HFileForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HFileForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_rsm;
        private System.Windows.Forms.Button button_rsm;
        private System.Windows.Forms.Button button_run;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_wefac;
    }
}