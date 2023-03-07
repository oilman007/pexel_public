namespace Pexel.SCAL
{
    partial class ExportSetForm
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
            this.comboBox_tables = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_export_corey = new System.Windows.Forms.CheckBox();
            this.textBox_tables = new System.Windows.Forms.TextBox();
            this.textBox_corey = new System.Windows.Forms.TextBox();
            this.button_file = new System.Windows.Forms.Button();
            this.button_export = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_tables
            // 
            this.comboBox_tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tables.FormattingEnabled = true;
            this.comboBox_tables.Items.AddRange(new object[] {
            "SWOF+SGOF",
            "SWOF",
            "SGOF"});
            this.comboBox_tables.Location = new System.Drawing.Point(12, 12);
            this.comboBox_tables.Name = "comboBox_tables";
            this.comboBox_tables.Size = new System.Drawing.Size(238, 24);
            this.comboBox_tables.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_export_corey, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_tables, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_corey, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_file, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 61);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(837, 71);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tebles File";
            // 
            // checkBox_export_corey
            // 
            this.checkBox_export_corey.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_export_corey.AutoSize = true;
            this.checkBox_export_corey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_export_corey.Location = new System.Drawing.Point(3, 43);
            this.checkBox_export_corey.Name = "checkBox_export_corey";
            this.checkBox_export_corey.Size = new System.Drawing.Size(85, 20);
            this.checkBox_export_corey.TabIndex = 1;
            this.checkBox_export_corey.Text = "Corey file";
            this.checkBox_export_corey.UseVisualStyleBackColor = true;
            this.checkBox_export_corey.CheckedChanged += new System.EventHandler(this.textBox_tables_TextChanged);
            // 
            // textBox_tables
            // 
            this.textBox_tables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_tables.Location = new System.Drawing.Point(94, 6);
            this.textBox_tables.Name = "textBox_tables";
            this.textBox_tables.Size = new System.Drawing.Size(691, 22);
            this.textBox_tables.TabIndex = 2;
            this.textBox_tables.TextChanged += new System.EventHandler(this.textBox_tables_TextChanged);
            // 
            // textBox_corey
            // 
            this.textBox_corey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_corey.Location = new System.Drawing.Point(94, 42);
            this.textBox_corey.Name = "textBox_corey";
            this.textBox_corey.ReadOnly = true;
            this.textBox_corey.Size = new System.Drawing.Size(691, 22);
            this.textBox_corey.TabIndex = 2;
            // 
            // button_file
            // 
            this.button_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_file.Location = new System.Drawing.Point(791, 6);
            this.button_file.Name = "button_file";
            this.button_file.Size = new System.Drawing.Size(43, 23);
            this.button_file.TabIndex = 3;
            this.button_file.Text = "...";
            this.button_file.UseVisualStyleBackColor = true;
            this.button_file.Click += new System.EventHandler(this.button_file_Click);
            // 
            // button_export
            // 
            this.button_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_export.Location = new System.Drawing.Point(767, 144);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(82, 38);
            this.button_export.TabIndex = 3;
            this.button_export.Text = "Export";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // ExportSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 194);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.comboBox_tables);
            this.Controls.Add(this.button_export);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(9999, 241);
            this.MinimumSize = new System.Drawing.Size(600, 241);
            this.Name = "ExportSetForm";
            this.Text = "ExportSetForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportSetForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_tables;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_export_corey;
        private System.Windows.Forms.TextBox textBox_tables;
        private System.Windows.Forms.TextBox textBox_corey;
        private System.Windows.Forms.Button button_file;
        private System.Windows.Forms.Button button_export;
    }
}