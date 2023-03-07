namespace Pexel.HM.Aquifer
{
    partial class AquiferCellsForm
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
            this.textBox_actnum = new System.Windows.Forms.TextBox();
            this.button_actnum = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_swatinit = new System.Windows.Forms.TextBox();
            this.button_swatinit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_multpv = new System.Windows.Forms.TextBox();
            this.button_multpv = new System.Windows.Forms.Button();
            this.button_run = new System.Windows.Forms.Button();
            this.numericUpDown_nx = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_ny = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_nz = new System.Windows.Forms.NumericUpDown();
            this.textBox_actnum_kw = new System.Windows.Forms.TextBox();
            this.textBox_swatinit_kw = new System.Windows.Forms.TextBox();
            this.textBox_multpv_kw = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_radius = new System.Windows.Forms.NumericUpDown();
            this.textBox_aqreg_kw = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_aqreg = new System.Windows.Forms.TextBox();
            this.button_aqreg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_radius)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "actnum";
            // 
            // textBox_actnum
            // 
            this.textBox_actnum.Location = new System.Drawing.Point(189, 64);
            this.textBox_actnum.Name = "textBox_actnum";
            this.textBox_actnum.Size = new System.Drawing.Size(561, 20);
            this.textBox_actnum.TabIndex = 1;
            this.textBox_actnum.Text = "D:\\Rong_RC569_dec2021\\_adapt\\actnum.map";
            // 
            // button_actnum
            // 
            this.button_actnum.Location = new System.Drawing.Point(756, 62);
            this.button_actnum.Name = "button_actnum";
            this.button_actnum.Size = new System.Drawing.Size(38, 23);
            this.button_actnum.TabIndex = 2;
            this.button_actnum.Text = "...";
            this.button_actnum.UseVisualStyleBackColor = true;
            this.button_actnum.Click += new System.EventHandler(this.button_actnum_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "swatinit";
            // 
            // textBox_swatinit
            // 
            this.textBox_swatinit.Location = new System.Drawing.Point(189, 90);
            this.textBox_swatinit.Name = "textBox_swatinit";
            this.textBox_swatinit.Size = new System.Drawing.Size(561, 20);
            this.textBox_swatinit.TabIndex = 1;
            this.textBox_swatinit.Text = "D:\\Rong_RC569_dec2021\\_adapt\\swatinit.map";
            // 
            // button_swatinit
            // 
            this.button_swatinit.Location = new System.Drawing.Point(756, 88);
            this.button_swatinit.Name = "button_swatinit";
            this.button_swatinit.Size = new System.Drawing.Size(38, 23);
            this.button_swatinit.TabIndex = 2;
            this.button_swatinit.Text = "...";
            this.button_swatinit.UseVisualStyleBackColor = true;
            this.button_swatinit.Click += new System.EventHandler(this.button_swatinit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "multpv";
            // 
            // textBox_multpv
            // 
            this.textBox_multpv.Location = new System.Drawing.Point(189, 137);
            this.textBox_multpv.Name = "textBox_multpv";
            this.textBox_multpv.Size = new System.Drawing.Size(561, 20);
            this.textBox_multpv.TabIndex = 1;
            this.textBox_multpv.Text = "D:\\Rong_RC569_dec2021\\_adapt\\multpv.map";
            // 
            // button_multpv
            // 
            this.button_multpv.Location = new System.Drawing.Point(756, 135);
            this.button_multpv.Name = "button_multpv";
            this.button_multpv.Size = new System.Drawing.Size(38, 23);
            this.button_multpv.TabIndex = 2;
            this.button_multpv.Text = "...";
            this.button_multpv.UseVisualStyleBackColor = true;
            this.button_multpv.Click += new System.EventHandler(this.button_multpv_Click);
            // 
            // button_run
            // 
            this.button_run.Location = new System.Drawing.Point(383, 210);
            this.button_run.Name = "button_run";
            this.button_run.Size = new System.Drawing.Size(75, 32);
            this.button_run.TabIndex = 3;
            this.button_run.Text = "Run";
            this.button_run.UseVisualStyleBackColor = true;
            this.button_run.Click += new System.EventHandler(this.button_run_Click);
            // 
            // numericUpDown_nx
            // 
            this.numericUpDown_nx.Location = new System.Drawing.Point(76, 30);
            this.numericUpDown_nx.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown_nx.Name = "numericUpDown_nx";
            this.numericUpDown_nx.Size = new System.Drawing.Size(59, 20);
            this.numericUpDown_nx.TabIndex = 4;
            this.numericUpDown_nx.Value = new decimal(new int[] {
            165,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "nx";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "ny";
            // 
            // numericUpDown_ny
            // 
            this.numericUpDown_ny.Location = new System.Drawing.Point(141, 30);
            this.numericUpDown_ny.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown_ny.Name = "numericUpDown_ny";
            this.numericUpDown_ny.Size = new System.Drawing.Size(59, 20);
            this.numericUpDown_ny.TabIndex = 4;
            this.numericUpDown_ny.Value = new decimal(new int[] {
            198,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(203, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "nz";
            // 
            // numericUpDown_nz
            // 
            this.numericUpDown_nz.Location = new System.Drawing.Point(206, 30);
            this.numericUpDown_nz.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown_nz.Name = "numericUpDown_nz";
            this.numericUpDown_nz.Size = new System.Drawing.Size(59, 20);
            this.numericUpDown_nz.TabIndex = 4;
            this.numericUpDown_nz.Value = new decimal(new int[] {
            635,
            0,
            0,
            0});
            // 
            // textBox_actnum_kw
            // 
            this.textBox_actnum_kw.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_actnum_kw.Location = new System.Drawing.Point(78, 64);
            this.textBox_actnum_kw.Name = "textBox_actnum_kw";
            this.textBox_actnum_kw.Size = new System.Drawing.Size(105, 20);
            this.textBox_actnum_kw.TabIndex = 1;
            this.textBox_actnum_kw.Text = "ACTNUM";
            // 
            // textBox_swatinit_kw
            // 
            this.textBox_swatinit_kw.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_swatinit_kw.Location = new System.Drawing.Point(78, 90);
            this.textBox_swatinit_kw.Name = "textBox_swatinit_kw";
            this.textBox_swatinit_kw.Size = new System.Drawing.Size(105, 20);
            this.textBox_swatinit_kw.TabIndex = 1;
            this.textBox_swatinit_kw.Text = "SWATINIT";
            // 
            // textBox_multpv_kw
            // 
            this.textBox_multpv_kw.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_multpv_kw.Location = new System.Drawing.Point(78, 137);
            this.textBox_multpv_kw.Name = "textBox_multpv_kw";
            this.textBox_multpv_kw.Size = new System.Drawing.Size(105, 20);
            this.textBox_multpv_kw.TabIndex = 1;
            this.textBox_multpv_kw.Text = "MULTPV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(342, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "radius";
            // 
            // numericUpDown_radius
            // 
            this.numericUpDown_radius.Location = new System.Drawing.Point(345, 30);
            this.numericUpDown_radius.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown_radius.Name = "numericUpDown_radius";
            this.numericUpDown_radius.Size = new System.Drawing.Size(59, 20);
            this.numericUpDown_radius.TabIndex = 4;
            this.numericUpDown_radius.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // textBox_aqreg_kw
            // 
            this.textBox_aqreg_kw.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_aqreg_kw.Location = new System.Drawing.Point(78, 166);
            this.textBox_aqreg_kw.Name = "textBox_aqreg_kw";
            this.textBox_aqreg_kw.Size = new System.Drawing.Size(105, 20);
            this.textBox_aqreg_kw.TabIndex = 1;
            this.textBox_aqreg_kw.Text = "AQREG";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "reg";
            // 
            // textBox_aqreg
            // 
            this.textBox_aqreg.Location = new System.Drawing.Point(189, 166);
            this.textBox_aqreg.Name = "textBox_aqreg";
            this.textBox_aqreg.Size = new System.Drawing.Size(561, 20);
            this.textBox_aqreg.TabIndex = 1;
            this.textBox_aqreg.Text = "D:\\Rong_RC569_dec2021\\_adapt\\aqreg.map";
            // 
            // button_aqreg
            // 
            this.button_aqreg.Location = new System.Drawing.Point(756, 164);
            this.button_aqreg.Name = "button_aqreg";
            this.button_aqreg.Size = new System.Drawing.Size(38, 23);
            this.button_aqreg.TabIndex = 2;
            this.button_aqreg.Text = "...";
            this.button_aqreg.UseVisualStyleBackColor = true;
            this.button_aqreg.Click += new System.EventHandler(this.button_aqreg_Click);
            // 
            // AquiferCellsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 260);
            this.Controls.Add(this.numericUpDown_radius);
            this.Controls.Add(this.numericUpDown_nz);
            this.Controls.Add(this.numericUpDown_ny);
            this.Controls.Add(this.numericUpDown_nx);
            this.Controls.Add(this.button_run);
            this.Controls.Add(this.button_aqreg);
            this.Controls.Add(this.button_multpv);
            this.Controls.Add(this.textBox_aqreg);
            this.Controls.Add(this.button_swatinit);
            this.Controls.Add(this.textBox_multpv);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button_actnum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_swatinit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_aqreg_kw);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_multpv_kw);
            this.Controls.Add(this.textBox_swatinit_kw);
            this.Controls.Add(this.textBox_actnum_kw);
            this.Controls.Add(this.textBox_actnum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "AquiferCellsForm";
            this.Text = "AquiferCellsForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_radius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_actnum;
        private System.Windows.Forms.Button button_actnum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_swatinit;
        private System.Windows.Forms.Button button_swatinit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_multpv;
        private System.Windows.Forms.Button button_multpv;
        private System.Windows.Forms.Button button_run;
        private System.Windows.Forms.NumericUpDown numericUpDown_nx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown_ny;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_nz;
        private System.Windows.Forms.TextBox textBox_actnum_kw;
        private System.Windows.Forms.TextBox textBox_swatinit_kw;
        private System.Windows.Forms.TextBox textBox_multpv_kw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_radius;
        private System.Windows.Forms.TextBox textBox_aqreg_kw;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_aqreg;
        private System.Windows.Forms.Button button_aqreg;
    }
}