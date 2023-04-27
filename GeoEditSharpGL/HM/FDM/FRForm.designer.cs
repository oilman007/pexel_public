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
            this.treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.treeColumn_title = new Aga.Controls.Tree.TreeColumn();
            this.treeColumn_visible = new Aga.Controls.Tree.TreeColumn();
            this.treeColumn_used = new Aga.Controls.Tree.TreeColumn();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeCheckBox1 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nodeCheckBox2 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_dates = new System.Windows.Forms.ComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip_main.SuspendLayout();
            this.statusStrip_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
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
            this.statusStrip_main.Location = new System.Drawing.Point(0, 704);
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
            this.splitContainer_main.Panel1.Controls.Add(this.treeViewAdv);
            this.splitContainer_main.Panel1.Controls.Add(this.menuStrip_main);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer_main.Size = new System.Drawing.Size(1380, 704);
            this.splitContainer_main.SplitterDistance = 465;
            this.splitContainer_main.TabIndex = 2;
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
            this.treeViewAdv.Location = new System.Drawing.Point(0, 28);
            this.treeViewAdv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeViewAdv.Model = null;
            this.treeViewAdv.Name = "treeViewAdv";
            this.treeViewAdv.NodeControls.Add(this.nodeTextBox1);
            this.treeViewAdv.NodeControls.Add(this.nodeCheckBox1);
            this.treeViewAdv.NodeControls.Add(this.nodeCheckBox2);
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.Size = new System.Drawing.Size(465, 676);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 704);
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
            // FRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 726);
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
    }
}