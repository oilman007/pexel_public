﻿namespace Pexel.HM.FR
{
    partial class FRForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRForm));
            this.menuStrip_main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.homeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip_main = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_msg = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.treeColumn_title = new Aga.Controls.Tree.TreeColumn();
            this.treeColumn_visible = new Aga.Controls.Tree.TreeColumn();
            this.treeColumn_used = new Aga.Controls.Tree.TreeColumn();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeCheckBox1 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nodeCheckBox2 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button_show_all_regions = new System.Windows.Forms.Button();
            this.button_hide_all_regions = new System.Windows.Forms.Button();
            this.button_show_all_bouns = new System.Windows.Forms.Button();
            this.button_hide_all_bouns = new System.Windows.Forms.Button();
            this.button_show_all_wells = new System.Windows.Forms.Button();
            this.button_hide_all_wells = new System.Windows.Forms.Button();
            this.button_show_all_links = new System.Windows.Forms.Button();
            this.button_hide_all_links = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_dates = new System.Windows.Forms.ComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip_main.SuspendLayout();
            this.statusStrip_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_main
            // 
            this.menuStrip_main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip_main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_main.Name = "menuStrip_main";
            this.menuStrip_main.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip_main.Size = new System.Drawing.Size(465, 28);
            this.menuStrip_main.TabIndex = 0;
            this.menuStrip_main.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem1});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // homeToolStripMenuItem1
            // 
            this.homeToolStripMenuItem1.Name = "homeToolStripMenuItem1";
            this.homeToolStripMenuItem1.Size = new System.Drawing.Size(133, 26);
            this.homeToolStripMenuItem1.Text = "&Home";
            this.homeToolStripMenuItem1.Click += new System.EventHandler(this.homeToolStripMenuItem1_Click);
            // 
            // statusStrip_main
            // 
            this.statusStrip_main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_msg});
            this.statusStrip_main.Location = new System.Drawing.Point(0, 601);
            this.statusStrip_main.Name = "statusStrip_main";
            this.statusStrip_main.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip_main.Size = new System.Drawing.Size(1380, 22);
            this.statusStrip_main.TabIndex = 1;
            this.statusStrip_main.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_msg
            // 
            this.toolStripStatusLabel_msg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripStatusLabel_msg.Name = "toolStripStatusLabel_msg";
            this.toolStripStatusLabel_msg.Size = new System.Drawing.Size(0, 16);
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_main.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer_main.Panel1.Controls.Add(this.menuStrip_main);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer_main.Size = new System.Drawing.Size(1380, 601);
            this.splitContainer_main.SplitterDistance = 465;
            this.splitContainer_main.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewAdv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(465, 573);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeViewAdv
            // 
            this.treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv.Columns.Add(this.treeColumn_title);
            this.treeViewAdv.Columns.Add(this.treeColumn_visible);
            this.treeViewAdv.Columns.Add(this.treeColumn_used);
            this.treeViewAdv.DefaultToolTipProvider = null;
            this.treeViewAdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeViewAdv.Model = null;
            this.treeViewAdv.Name = "treeViewAdv";
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox1);
            this.treeViewAdv.NodeControls.Add(this.nodeCheckBox1);
            this.treeViewAdv.NodeControls.Add(this.nodeCheckBox2);
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.Size = new System.Drawing.Size(465, 243);
            this.treeViewAdv.TabIndex = 0;
            this.treeViewAdv.Text = "treeViewAdv1";
            this.treeViewAdv.UseColumns = true;
            this.treeViewAdv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewAdv_MouseClick);
            // 
            // treeColumn_title
            // 
            this.treeColumn_title.Header = "Title";
            this.treeColumn_title.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumn_title.TooltipText = null;
            this.treeColumn_title.Width = 220;
            // 
            // treeColumn_visible
            // 
            this.treeColumn_visible.Header = "Visible";
            this.treeColumn_visible.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumn_visible.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.treeColumn_visible.TooltipText = null;
            // 
            // treeColumn_used
            // 
            this.treeColumn_used.Header = "Used";
            this.treeColumn_used.SortOrder = System.Windows.Forms.SortOrder.None;
            this.treeColumn_used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.treeColumn_used.TooltipText = null;
            // 
            // nodeTextBox1
            // 
            this.nodeTextBox1.DataPropertyName = "NodeControl0";
            this.nodeTextBox1.IncrementalSearchEnabled = true;
            this.nodeTextBox1.LeftMargin = 3;
            this.nodeTextBox1.ParentColumn = this.treeColumn_title;
            // 
            // nodeCheckBox1
            // 
            this.nodeCheckBox1.DataPropertyName = "NodeControl1";
            this.nodeCheckBox1.EditEnabled = true;
            this.nodeCheckBox1.LeftMargin = 0;
            this.nodeCheckBox1.ParentColumn = this.treeColumn_visible;
            // 
            // nodeCheckBox2
            // 
            this.nodeCheckBox2.DataPropertyName = "NodeControl2";
            this.nodeCheckBox2.EditEnabled = true;
            this.nodeCheckBox2.LeftMargin = 0;
            this.nodeCheckBox2.ParentColumn = this.treeColumn_used;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.button_show_all_regions, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.button_hide_all_regions, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.button_show_all_bouns, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.button_hide_all_bouns, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.button_show_all_wells, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.button_hide_all_wells, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.button_show_all_links, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.button_hide_all_links, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.button1, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.button2, 4, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(465, 326);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // button_show_all_regions
            // 
            this.button_show_all_regions.Location = new System.Drawing.Point(85, 19);
            this.button_show_all_regions.Name = "button_show_all_regions";
            this.button_show_all_regions.Size = new System.Drawing.Size(56, 28);
            this.button_show_all_regions.TabIndex = 0;
            this.button_show_all_regions.Text = "yes";
            this.button_show_all_regions.UseVisualStyleBackColor = true;
            this.button_show_all_regions.Click += new System.EventHandler(this.button_show_all_regions_Click);
            // 
            // button_hide_all_regions
            // 
            this.button_hide_all_regions.Location = new System.Drawing.Point(147, 19);
            this.button_hide_all_regions.Name = "button_hide_all_regions";
            this.button_hide_all_regions.Size = new System.Drawing.Size(56, 28);
            this.button_hide_all_regions.TabIndex = 0;
            this.button_hide_all_regions.Text = "no";
            this.button_hide_all_regions.UseVisualStyleBackColor = true;
            this.button_hide_all_regions.Click += new System.EventHandler(this.button_hide_all_regions_Click);
            // 
            // button_show_all_bouns
            // 
            this.button_show_all_bouns.Location = new System.Drawing.Point(85, 53);
            this.button_show_all_bouns.Name = "button_show_all_bouns";
            this.button_show_all_bouns.Size = new System.Drawing.Size(56, 28);
            this.button_show_all_bouns.TabIndex = 0;
            this.button_show_all_bouns.Text = "yes";
            this.button_show_all_bouns.UseVisualStyleBackColor = true;
            this.button_show_all_bouns.Click += new System.EventHandler(this.button_show_all_bouns_Click);
            // 
            // button_hide_all_bouns
            // 
            this.button_hide_all_bouns.Location = new System.Drawing.Point(147, 53);
            this.button_hide_all_bouns.Name = "button_hide_all_bouns";
            this.button_hide_all_bouns.Size = new System.Drawing.Size(56, 28);
            this.button_hide_all_bouns.TabIndex = 0;
            this.button_hide_all_bouns.Text = "no";
            this.button_hide_all_bouns.UseVisualStyleBackColor = true;
            this.button_hide_all_bouns.Click += new System.EventHandler(this.button_hide_all_bouns_Click);
            // 
            // button_show_all_wells
            // 
            this.button_show_all_wells.Location = new System.Drawing.Point(85, 87);
            this.button_show_all_wells.Name = "button_show_all_wells";
            this.button_show_all_wells.Size = new System.Drawing.Size(56, 28);
            this.button_show_all_wells.TabIndex = 0;
            this.button_show_all_wells.Text = "yes";
            this.button_show_all_wells.UseVisualStyleBackColor = true;
            this.button_show_all_wells.Click += new System.EventHandler(this.button_show_all_wells_Click);
            // 
            // button_hide_all_wells
            // 
            this.button_hide_all_wells.Location = new System.Drawing.Point(147, 87);
            this.button_hide_all_wells.Name = "button_hide_all_wells";
            this.button_hide_all_wells.Size = new System.Drawing.Size(56, 28);
            this.button_hide_all_wells.TabIndex = 0;
            this.button_hide_all_wells.Text = "no";
            this.button_hide_all_wells.UseVisualStyleBackColor = true;
            this.button_hide_all_wells.Click += new System.EventHandler(this.button_hide_all_wells_Click);
            // 
            // button_show_all_links
            // 
            this.button_show_all_links.Location = new System.Drawing.Point(85, 121);
            this.button_show_all_links.Name = "button_show_all_links";
            this.button_show_all_links.Size = new System.Drawing.Size(56, 28);
            this.button_show_all_links.TabIndex = 0;
            this.button_show_all_links.Text = "yes";
            this.button_show_all_links.UseVisualStyleBackColor = true;
            this.button_show_all_links.Click += new System.EventHandler(this.button_show_all_links_Click);
            // 
            // button_hide_all_links
            // 
            this.button_hide_all_links.Location = new System.Drawing.Point(147, 121);
            this.button_hide_all_links.Name = "button_hide_all_links";
            this.button_hide_all_links.Size = new System.Drawing.Size(56, 28);
            this.button_hide_all_links.TabIndex = 0;
            this.button_hide_all_links.Text = "no";
            this.button_hide_all_links.UseVisualStyleBackColor = true;
            this.button_hide_all_links.Click += new System.EventHandler(this.button_hide_all_links_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 601);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Controls.Add(this.comboBox_dates, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(905, 35);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // comboBox_dates
            // 
            this.comboBox_dates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_dates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_dates.FormattingEnabled = true;
            this.comboBox_dates.Location = new System.Drawing.Point(3, 5);
            this.comboBox_dates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_dates.Name = "comboBox_dates";
            this.comboBox_dates.Size = new System.Drawing.Size(799, 24);
            this.comboBox_dates.TabIndex = 0;
            this.comboBox_dates.SelectedIndexChanged += new System.EventHandler(this.comboBox_dates_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "view.png");
            this.imageList1.Images.SetKeyName(1, "hide.png");
            this.imageList1.Images.SetKeyName(2, "deletered.png");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(85, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Visible";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(209, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Used";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Regions";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Boundaries";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Wells";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Links";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "yes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_show_all_regions_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(271, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 28);
            this.button2.TabIndex = 0;
            this.button2.Text = "no";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_show_all_regions_Click);
            // 
            // FRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 623);
            this.Controls.Add(this.splitContainer_main);
            this.Controls.Add(this.statusStrip_main);
            this.MainMenuStrip = this.menuStrip_main;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FRForm";
            this.Text = "FR Analyze";
            this.menuStrip_main.ResumeLayout(false);
            this.menuStrip_main.PerformLayout();
            this.statusStrip_main.ResumeLayout(false);
            this.statusStrip_main.PerformLayout();
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel1.PerformLayout();
            this.splitContainer_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_main;
        private System.Windows.Forms.StatusStrip statusStrip_main;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_msg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox comboBox_dates;
        private System.Windows.Forms.ImageList imageList1;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv;
        private Aga.Controls.Tree.TreeColumn treeColumn_visible;
        private Aga.Controls.Tree.TreeColumn treeColumn_used;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox nodeCheckBox1;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox nodeCheckBox2;
        private Aga.Controls.Tree.TreeColumn treeColumn_title;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button_show_all_regions;
        private System.Windows.Forms.Button button_hide_all_regions;
        private System.Windows.Forms.Button button_show_all_bouns;
        private System.Windows.Forms.Button button_hide_all_bouns;
        private System.Windows.Forms.Button button_show_all_wells;
        private System.Windows.Forms.Button button_hide_all_wells;
        private System.Windows.Forms.Button button_show_all_links;
        private System.Windows.Forms.Button button_hide_all_links;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}