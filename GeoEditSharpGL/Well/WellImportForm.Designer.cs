namespace Pexel
{
    partial class WellImportForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_prefix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_ending = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.textBox_md = new System.Windows.Forms.TextBox();
            this.textBox_tvd = new System.Windows.Forms.TextBox();
            this.textBox_y = new System.Windows.Forms.TextBox();
            this.textBox_x = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X Column";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y Column";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "TVD Column";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "MD Column";
            // 
            // textBox_prefix
            // 
            this.textBox_prefix.Location = new System.Drawing.Point(101, 12);
            this.textBox_prefix.Name = "textBox_prefix";
            this.textBox_prefix.Size = new System.Drawing.Size(171, 20);
            this.textBox_prefix.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Wellname Prefix";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(110, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "End Incl";
            // 
            // textBox_ending
            // 
            this.textBox_ending.Location = new System.Drawing.Point(83, 108);
            this.textBox_ending.Name = "textBox_ending";
            this.textBox_ending.Size = new System.Drawing.Size(100, 20);
            this.textBox_ending.TabIndex = 1;
            this.textBox_ending.Text = "/";
            this.textBox_ending.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(116, 151);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(197, 151);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // textBox_md
            // 
            this.textBox_md.Location = new System.Drawing.Point(210, 60);
            this.textBox_md.Name = "textBox_md";
            this.textBox_md.Size = new System.Drawing.Size(38, 20);
            this.textBox_md.TabIndex = 1;
            this.textBox_md.Text = "4";
            this.textBox_md.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_md.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_md_Validating);
            // 
            // textBox_tvd
            // 
            this.textBox_tvd.Location = new System.Drawing.Point(145, 60);
            this.textBox_tvd.Name = "textBox_tvd";
            this.textBox_tvd.Size = new System.Drawing.Size(38, 20);
            this.textBox_tvd.TabIndex = 1;
            this.textBox_tvd.Text = "3";
            this.textBox_tvd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_tvd.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_tvd_Validating);
            // 
            // textBox_y
            // 
            this.textBox_y.Location = new System.Drawing.Point(81, 60);
            this.textBox_y.Name = "textBox_y";
            this.textBox_y.Size = new System.Drawing.Size(38, 20);
            this.textBox_y.TabIndex = 1;
            this.textBox_y.Text = "2";
            this.textBox_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_y.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_y_Validating);
            // 
            // textBox_x
            // 
            this.textBox_x.Location = new System.Drawing.Point(19, 60);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(38, 20);
            this.textBox_x.TabIndex = 1;
            this.textBox_x.Text = "1";
            this.textBox_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_x.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_x_Validating);
            // 
            // WellImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 184);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_md);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_tvd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_ending);
            this.Controls.Add(this.textBox_prefix);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_x);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WellImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WellImportForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WellImportForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_prefix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_ending;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox textBox_md;
        private System.Windows.Forms.TextBox textBox_tvd;
        private System.Windows.Forms.TextBox textBox_y;
        private System.Windows.Forms.TextBox textBox_x;
    }
}