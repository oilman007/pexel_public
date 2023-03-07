namespace Pexel.SCAL
{
    partial class CoreySetForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.listBox_tables = new System.Windows.Forms.ListBox();
            this.propertyGrid_set = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_add = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_remove = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid_table = new System.Windows.Forms.PropertyGrid();
            this.checkBox_kr = new System.Windows.Forms.CheckBox();
            this.checkBox_pc = new System.Windows.Forms.CheckBox();
            this.checkBox_nw_min_max = new System.Windows.Forms.CheckBox();
            this.checkBox_now_min_max = new System.Windows.Forms.CheckBox();
            this.checkBox_critical_points = new System.Windows.Forms.CheckBox();
            this.checkBox_points = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphControl_swof = new ZedGraph.ZedGraphControl();
            this.zedGraphControl_sgof = new ZedGraph.ZedGraphControl();
            this.dataGridView_swof = new System.Windows.Forms.DataGridView();
            this.ColumnSw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnKrw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnKro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPcow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_sgof = new System.Windows.Forms.DataGridView();
            this.ColumnSg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnKrg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPcog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_swof)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_sgof)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1232, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportTableToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.importToolStripMenuItem.Text = "&Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportTableToolStripMenuItem
            // 
            this.exportTableToolStripMenuItem.Name = "exportTableToolStripMenuItem";
            this.exportTableToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exportTableToolStripMenuItem.Text = "&Export...";
            this.exportTableToolStripMenuItem.Click += new System.EventHandler(this.exportTablesToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1232, 626);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(248, 626);
            this.splitContainer2.SplitterDistance = 84;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 27);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.listBox_tables);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.propertyGrid_set);
            this.splitContainer4.Size = new System.Drawing.Size(84, 599);
            this.splitContainer4.SplitterDistance = 303;
            this.splitContainer4.TabIndex = 2;
            // 
            // listBox_tables
            // 
            this.listBox_tables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_tables.FormattingEnabled = true;
            this.listBox_tables.ItemHeight = 16;
            this.listBox_tables.Location = new System.Drawing.Point(0, 0);
            this.listBox_tables.Name = "listBox_tables";
            this.listBox_tables.Size = new System.Drawing.Size(84, 303);
            this.listBox_tables.TabIndex = 2;
            this.listBox_tables.SelectedIndexChanged += new System.EventHandler(this.listBox_tables_SelectedIndexChanged_1);
            // 
            // propertyGrid_set
            // 
            this.propertyGrid_set.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid_set.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid_set.Name = "propertyGrid_set";
            this.propertyGrid_set.Size = new System.Drawing.Size(84, 292);
            this.propertyGrid_set.TabIndex = 0;
            this.propertyGrid_set.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_set_PropertyValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_add,
            this.toolStripButton_remove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(84, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_add
            // 
            this.toolStripButton_add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_add.Image = global::Pexel.Properties.Resources.Add;
            this.toolStripButton_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_add.Name = "toolStripButton_add";
            this.toolStripButton_add.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_add.Text = "toolStripButton1";
            this.toolStripButton_add.Click += new System.EventHandler(this.toolStripButton_add_Click);
            // 
            // toolStripButton_remove
            // 
            this.toolStripButton_remove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_remove.Image = global::Pexel.Properties.Resources.deletered;
            this.toolStripButton_remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_remove.Name = "toolStripButton_remove";
            this.toolStripButton_remove.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_remove.Text = "toolStripButton1";
            this.toolStripButton_remove.Click += new System.EventHandler(this.toolStripButton_remove_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid_table, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_kr, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_pc, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_nw_min_max, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_now_min_max, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_critical_points, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_points, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(160, 626);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // propertyGrid_table
            // 
            this.propertyGrid_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid_table.Location = new System.Drawing.Point(3, 159);
            this.propertyGrid_table.Name = "propertyGrid_table";
            this.propertyGrid_table.Size = new System.Drawing.Size(154, 464);
            this.propertyGrid_table.TabIndex = 0;
            this.propertyGrid_table.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_table_PropertyValueChanged);
            // 
            // checkBox_kr
            // 
            this.checkBox_kr.AutoSize = true;
            this.checkBox_kr.Location = new System.Drawing.Point(3, 3);
            this.checkBox_kr.Name = "checkBox_kr";
            this.checkBox_kr.Size = new System.Drawing.Size(41, 20);
            this.checkBox_kr.TabIndex = 1;
            this.checkBox_kr.Text = "Kr";
            this.checkBox_kr.UseVisualStyleBackColor = true;
            this.checkBox_kr.CheckedChanged += new System.EventHandler(this.checkBox_kr_CheckedChanged);
            // 
            // checkBox_pc
            // 
            this.checkBox_pc.AutoSize = true;
            this.checkBox_pc.Location = new System.Drawing.Point(3, 29);
            this.checkBox_pc.Name = "checkBox_pc";
            this.checkBox_pc.Size = new System.Drawing.Size(119, 20);
            this.checkBox_pc.TabIndex = 1;
            this.checkBox_pc.Text = "Capillary press";
            this.checkBox_pc.UseVisualStyleBackColor = true;
            this.checkBox_pc.CheckedChanged += new System.EventHandler(this.checkBox_pc_CheckedChanged);
            // 
            // checkBox_nw_min_max
            // 
            this.checkBox_nw_min_max.AutoSize = true;
            this.checkBox_nw_min_max.Location = new System.Drawing.Point(3, 133);
            this.checkBox_nw_min_max.Name = "checkBox_nw_min_max";
            this.checkBox_nw_min_max.Size = new System.Drawing.Size(105, 20);
            this.checkBox_nw_min_max.TabIndex = 1;
            this.checkBox_nw_min_max.Text = "NW min/max";
            this.checkBox_nw_min_max.UseVisualStyleBackColor = true;
            this.checkBox_nw_min_max.CheckedChanged += new System.EventHandler(this.checkBox_nw_min_max_CheckedChanged);
            // 
            // checkBox_now_min_max
            // 
            this.checkBox_now_min_max.AutoSize = true;
            this.checkBox_now_min_max.Location = new System.Drawing.Point(3, 107);
            this.checkBox_now_min_max.Name = "checkBox_now_min_max";
            this.checkBox_now_min_max.Size = new System.Drawing.Size(115, 20);
            this.checkBox_now_min_max.TabIndex = 1;
            this.checkBox_now_min_max.Text = "NOW min/max";
            this.checkBox_now_min_max.UseVisualStyleBackColor = true;
            this.checkBox_now_min_max.CheckedChanged += new System.EventHandler(this.checkBox_now_min_max_CheckedChanged);
            // 
            // checkBox_critical_points
            // 
            this.checkBox_critical_points.AutoSize = true;
            this.checkBox_critical_points.Location = new System.Drawing.Point(3, 81);
            this.checkBox_critical_points.Name = "checkBox_critical_points";
            this.checkBox_critical_points.Size = new System.Drawing.Size(108, 20);
            this.checkBox_critical_points.TabIndex = 1;
            this.checkBox_critical_points.Text = "Critical points";
            this.checkBox_critical_points.UseVisualStyleBackColor = true;
            this.checkBox_critical_points.CheckedChanged += new System.EventHandler(this.checkBox_critical_points_CheckedChanged);
            // 
            // checkBox_points
            // 
            this.checkBox_points.AutoSize = true;
            this.checkBox_points.Location = new System.Drawing.Point(3, 55);
            this.checkBox_points.Name = "checkBox_points";
            this.checkBox_points.Size = new System.Drawing.Size(66, 20);
            this.checkBox_points.TabIndex = 1;
            this.checkBox_points.Text = "Points";
            this.checkBox_points.UseVisualStyleBackColor = true;
            this.checkBox_points.CheckedChanged += new System.EventHandler(this.checkBox_points_CheckedChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.zedGraphControl_swof, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.zedGraphControl_sgof, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.dataGridView_swof, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.dataGridView_sgof, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(980, 626);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // zedGraphControl_swof
            // 
            this.zedGraphControl_swof.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl_swof.Location = new System.Drawing.Point(4, 4);
            this.zedGraphControl_swof.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl_swof.Name = "zedGraphControl_swof";
            this.zedGraphControl_swof.ScrollGrace = 0D;
            this.zedGraphControl_swof.ScrollMaxX = 0D;
            this.zedGraphControl_swof.ScrollMaxY = 0D;
            this.zedGraphControl_swof.ScrollMaxY2 = 0D;
            this.zedGraphControl_swof.ScrollMinX = 0D;
            this.zedGraphControl_swof.ScrollMinY = 0D;
            this.zedGraphControl_swof.ScrollMinY2 = 0D;
            this.zedGraphControl_swof.Size = new System.Drawing.Size(482, 305);
            this.zedGraphControl_swof.TabIndex = 0;
            this.zedGraphControl_swof.UseExtendedPrintDialog = true;
            // 
            // zedGraphControl_sgof
            // 
            this.zedGraphControl_sgof.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl_sgof.Location = new System.Drawing.Point(4, 317);
            this.zedGraphControl_sgof.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl_sgof.Name = "zedGraphControl_sgof";
            this.zedGraphControl_sgof.ScrollGrace = 0D;
            this.zedGraphControl_sgof.ScrollMaxX = 0D;
            this.zedGraphControl_sgof.ScrollMaxY = 0D;
            this.zedGraphControl_sgof.ScrollMaxY2 = 0D;
            this.zedGraphControl_sgof.ScrollMinX = 0D;
            this.zedGraphControl_sgof.ScrollMinY = 0D;
            this.zedGraphControl_sgof.ScrollMinY2 = 0D;
            this.zedGraphControl_sgof.Size = new System.Drawing.Size(482, 305);
            this.zedGraphControl_sgof.TabIndex = 0;
            this.zedGraphControl_sgof.UseExtendedPrintDialog = true;
            // 
            // dataGridView_swof
            // 
            this.dataGridView_swof.AllowUserToAddRows = false;
            this.dataGridView_swof.AllowUserToDeleteRows = false;
            this.dataGridView_swof.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_swof.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSw,
            this.ColumnKrw,
            this.ColumnKro,
            this.ColumnPcow});
            this.dataGridView_swof.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_swof.Location = new System.Drawing.Point(493, 3);
            this.dataGridView_swof.Name = "dataGridView_swof";
            this.dataGridView_swof.RowHeadersWidth = 51;
            this.dataGridView_swof.RowTemplate.Height = 24;
            this.dataGridView_swof.Size = new System.Drawing.Size(484, 307);
            this.dataGridView_swof.TabIndex = 1;
            // 
            // ColumnSw
            // 
            this.ColumnSw.HeaderText = "Sw";
            this.ColumnSw.MinimumWidth = 6;
            this.ColumnSw.Name = "ColumnSw";
            this.ColumnSw.Width = 125;
            // 
            // ColumnKrw
            // 
            this.ColumnKrw.HeaderText = "Krw";
            this.ColumnKrw.MinimumWidth = 6;
            this.ColumnKrw.Name = "ColumnKrw";
            this.ColumnKrw.Width = 125;
            // 
            // ColumnKro
            // 
            this.ColumnKro.HeaderText = "Kro";
            this.ColumnKro.MinimumWidth = 6;
            this.ColumnKro.Name = "ColumnKro";
            this.ColumnKro.Width = 125;
            // 
            // ColumnPcow
            // 
            this.ColumnPcow.HeaderText = "Pcow";
            this.ColumnPcow.MinimumWidth = 6;
            this.ColumnPcow.Name = "ColumnPcow";
            this.ColumnPcow.Width = 125;
            // 
            // dataGridView_sgof
            // 
            this.dataGridView_sgof.AllowUserToAddRows = false;
            this.dataGridView_sgof.AllowUserToDeleteRows = false;
            this.dataGridView_sgof.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_sgof.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSg,
            this.ColumnKrg,
            this.dataGridViewTextBoxColumn1,
            this.ColumnPcog});
            this.dataGridView_sgof.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_sgof.Location = new System.Drawing.Point(493, 316);
            this.dataGridView_sgof.Name = "dataGridView_sgof";
            this.dataGridView_sgof.RowHeadersWidth = 51;
            this.dataGridView_sgof.RowTemplate.Height = 24;
            this.dataGridView_sgof.Size = new System.Drawing.Size(484, 307);
            this.dataGridView_sgof.TabIndex = 1;
            // 
            // ColumnSg
            // 
            this.ColumnSg.HeaderText = "Sg";
            this.ColumnSg.MinimumWidth = 6;
            this.ColumnSg.Name = "ColumnSg";
            this.ColumnSg.ReadOnly = true;
            this.ColumnSg.Width = 125;
            // 
            // ColumnKrg
            // 
            this.ColumnKrg.HeaderText = "Krg";
            this.ColumnKrg.MinimumWidth = 6;
            this.ColumnKrg.Name = "ColumnKrg";
            this.ColumnKrg.ReadOnly = true;
            this.ColumnKrg.Width = 125;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Kro";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // ColumnPcog
            // 
            this.ColumnPcog.HeaderText = "Pcog";
            this.ColumnPcog.MinimumWidth = 6;
            this.ColumnPcog.Name = "ColumnPcog";
            this.ColumnPcog.ReadOnly = true;
            this.ColumnPcog.Width = 125;
            // 
            // CoreySetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 654);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "CoreySetForm";
            this.Text = "Corey Edit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CoreySetForm2_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_swof)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_sgof)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTableToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid propertyGrid_table;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private ZedGraph.ZedGraphControl zedGraphControl_swof;
        private ZedGraph.ZedGraphControl zedGraphControl_sgof;
        private System.Windows.Forms.DataGridView dataGridView_swof;
        private System.Windows.Forms.DataGridView dataGridView_sgof;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSw;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKrw;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKro;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPcow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKrg;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPcog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_add;
        private System.Windows.Forms.ToolStripButton toolStripButton_remove;
        private System.Windows.Forms.ListBox listBox_tables;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBox_points;
        private System.Windows.Forms.CheckBox checkBox_critical_points;
        private System.Windows.Forms.CheckBox checkBox_pc;
        private System.Windows.Forms.CheckBox checkBox_now_min_max;
        private System.Windows.Forms.CheckBox checkBox_nw_min_max;
        private System.Windows.Forms.CheckBox checkBox_kr;
        private System.Windows.Forms.PropertyGrid propertyGrid_set;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
    }
}