namespace Pexel
{
    partial class LauncherForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
            this.button_hm = new System.Windows.Forms.Button();
            this.button_results = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_hm_table = new System.Windows.Forms.Button();
            this.button_corey = new System.Windows.Forms.Button();
            this.button_hfile = new System.Windows.Forms.Button();
            this.button_tree = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_hm
            // 
            this.button_hm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_hm.Location = new System.Drawing.Point(132, 3);
            this.button_hm.Name = "button_hm";
            this.button_hm.Size = new System.Drawing.Size(123, 85);
            this.button_hm.TabIndex = 0;
            this.button_hm.Text = "History Matching";
            this.button_hm.UseVisualStyleBackColor = true;
            this.button_hm.Click += new System.EventHandler(this.button_hm_Click);
            // 
            // button_results
            // 
            this.button_results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_results.Location = new System.Drawing.Point(261, 3);
            this.button_results.Name = "button_results";
            this.button_results.Size = new System.Drawing.Size(123, 85);
            this.button_results.TabIndex = 2;
            this.button_results.Text = "Results Viewer";
            this.button_results.UseVisualStyleBackColor = true;
            this.button_results.Click += new System.EventHandler(this.button_results_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66708F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66708F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66708F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66542F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.button_tree, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_hm, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_results, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_hm_table, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_hfile, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_corey, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 91);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // button_hm_table
            // 
            this.button_hm_table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_hm_table.Location = new System.Drawing.Point(390, 3);
            this.button_hm_table.Name = "button_hm_table";
            this.button_hm_table.Size = new System.Drawing.Size(123, 85);
            this.button_hm_table.TabIndex = 2;
            this.button_hm_table.Text = "HM Table Maker";
            this.button_hm_table.UseVisualStyleBackColor = true;
            this.button_hm_table.Click += new System.EventHandler(this.button_hm_table_Click);
            // 
            // button_corey
            // 
            this.button_corey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_corey.Location = new System.Drawing.Point(519, 3);
            this.button_corey.Name = "button_corey";
            this.button_corey.Size = new System.Drawing.Size(123, 85);
            this.button_corey.TabIndex = 2;
            this.button_corey.Text = "Corey";
            this.button_corey.UseVisualStyleBackColor = true;
            this.button_corey.Click += new System.EventHandler(this.button_corey_Click);
            // 
            // button_hfile
            // 
            this.button_hfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_hfile.Location = new System.Drawing.Point(648, 3);
            this.button_hfile.Name = "button_hfile";
            this.button_hfile.Size = new System.Drawing.Size(124, 85);
            this.button_hfile.TabIndex = 2;
            this.button_hfile.Text = "HFile";
            this.button_hfile.UseVisualStyleBackColor = true;
            this.button_hfile.Click += new System.EventHandler(this.button_hfile_Click);
            // 
            // button_tree
            // 
            this.button_tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_tree.Location = new System.Drawing.Point(3, 3);
            this.button_tree.Name = "button_tree";
            this.button_tree.Size = new System.Drawing.Size(123, 85);
            this.button_tree.TabIndex = 2;
            this.button_tree.Text = "Project Tree";
            this.button_tree.UseVisualStyleBackColor = true;
            this.button_tree.Click += new System.EventHandler(this.button_tree_Click);
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 120);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 135);
            this.Name = "LauncherForm";
            this.Text = "PEXEL Launcher";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_hm;
        private System.Windows.Forms.Button button_results;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_hm_table;
        private System.Windows.Forms.Button button_corey;
        private System.Windows.Forms.Button button_hfile;
        private System.Windows.Forms.Button button_tree;
    }
}