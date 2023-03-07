namespace Pexel
{
    partial class WellsCreateForm
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
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_distance = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_x_width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_y_width = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_azimuth = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_distance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_azimuth)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_type
            // 
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "single",
            "triangular",
            "quadratic",
            "5-spot",
            "7-spot",
            "9-spot",
            "1-row",
            "3-row",
            "5-row"});
            this.comboBox_type.Location = new System.Drawing.Point(67, 6);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(125, 21);
            this.comboBox_type.TabIndex = 3;
            this.comboBox_type.Text = "single";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Distance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "X-width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y-width";
            // 
            // numericUpDown_distance
            // 
            this.numericUpDown_distance.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_distance.Location = new System.Drawing.Point(67, 34);
            this.numericUpDown_distance.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown_distance.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_distance.Name = "numericUpDown_distance";
            this.numericUpDown_distance.Size = new System.Drawing.Size(125, 20);
            this.numericUpDown_distance.TabIndex = 5;
            this.numericUpDown_distance.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // numericUpDown_x_width
            // 
            this.numericUpDown_x_width.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_x_width.Location = new System.Drawing.Point(67, 60);
            this.numericUpDown_x_width.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown_x_width.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_x_width.Name = "numericUpDown_x_width";
            this.numericUpDown_x_width.Size = new System.Drawing.Size(125, 20);
            this.numericUpDown_x_width.TabIndex = 5;
            this.numericUpDown_x_width.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // numericUpDown_y_width
            // 
            this.numericUpDown_y_width.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_y_width.Location = new System.Drawing.Point(67, 86);
            this.numericUpDown_y_width.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown_y_width.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_y_width.Name = "numericUpDown_y_width";
            this.numericUpDown_y_width.Size = new System.Drawing.Size(125, 20);
            this.numericUpDown_y_width.TabIndex = 5;
            this.numericUpDown_y_width.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Azimuth";
            // 
            // numericUpDown_azimuth
            // 
            this.numericUpDown_azimuth.Location = new System.Drawing.Point(67, 112);
            this.numericUpDown_azimuth.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown_azimuth.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDown_azimuth.Name = "numericUpDown_azimuth";
            this.numericUpDown_azimuth.Size = new System.Drawing.Size(125, 20);
            this.numericUpDown_azimuth.TabIndex = 5;
            // 
            // WellsCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 150);
            this.Controls.Add(this.numericUpDown_azimuth);
            this.Controls.Add(this.numericUpDown_y_width);
            this.Controls.Add(this.numericUpDown_x_width);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown_distance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_type);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WellsCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateWellsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WellsCreateForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_distance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_azimuth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown_distance;
        private System.Windows.Forms.NumericUpDown numericUpDown_x_width;
        private System.Windows.Forms.NumericUpDown numericUpDown_y_width;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown_azimuth;
        private System.Windows.Forms.Label label1;
    }
}