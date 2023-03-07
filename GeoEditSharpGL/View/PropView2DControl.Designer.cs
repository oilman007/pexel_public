namespace Pexel.View
{
    partial class PropView2DControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Modifiers");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropView2DControl));
            this.listBox_layers = new System.Windows.Forms.ListBox();
            this.treeView_mods = new System.Windows.Forms.TreeView();
            this.dataGridView_mods = new System.Windows.Forms.DataGridView();
            this.listBox_props = new System.Windows.Forms.ListBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.toolStrip_view = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox_cellValue = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox_index = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox_xy = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_focus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_gridLines = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_scale = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_prop_export = new System.Windows.Forms.ToolStripButton();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.toolStrip_mods = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_mod_add = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_mod_rename = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_mod_remove = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_mods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.toolStrip_view.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.toolStrip_mods.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_layers
            // 
            this.listBox_layers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_layers.FormattingEnabled = true;
            this.listBox_layers.ItemHeight = 16;
            this.listBox_layers.Location = new System.Drawing.Point(0, 0);
            this.listBox_layers.Margin = new System.Windows.Forms.Padding(4);
            this.listBox_layers.Name = "listBox_layers";
            this.listBox_layers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_layers.Size = new System.Drawing.Size(130, 638);
            this.listBox_layers.TabIndex = 0;
            this.listBox_layers.SelectedIndexChanged += new System.EventHandler(this.layersListBox_SelectedIndexChanged);
            // 
            // treeView_mods
            // 
            this.treeView_mods.CheckBoxes = true;
            this.treeView_mods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_mods.Location = new System.Drawing.Point(0, 0);
            this.treeView_mods.Name = "treeView_mods";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Modifiers";
            this.treeView_mods.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView_mods.Size = new System.Drawing.Size(130, 638);
            this.treeView_mods.TabIndex = 0;
            // 
            // dataGridView_mods
            // 
            this.dataGridView_mods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_mods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_mods.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_mods.Name = "dataGridView_mods";
            this.dataGridView_mods.RowHeadersWidth = 51;
            this.dataGridView_mods.RowTemplate.Height = 24;
            this.dataGridView_mods.Size = new System.Drawing.Size(261, 638);
            this.dataGridView_mods.TabIndex = 0;
            // 
            // listBox_props
            // 
            this.listBox_props.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_props.FormattingEnabled = true;
            this.listBox_props.ItemHeight = 16;
            this.listBox_props.Location = new System.Drawing.Point(0, 0);
            this.listBox_props.Name = "listBox_props";
            this.listBox_props.Size = new System.Drawing.Size(123, 638);
            this.listBox_props.TabIndex = 0;
            this.listBox_props.SelectedIndexChanged += new System.EventHandler(this.listBox_props_SelectedIndexChanged);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer6);
            this.splitContainer5.Panel1.Controls.Add(this.toolStrip_view);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer7);
            this.splitContainer5.Panel2.Controls.Add(this.toolStrip_mods);
            this.splitContainer5.Size = new System.Drawing.Size(1275, 669);
            this.splitContainer5.SplitterDistance = 876;
            this.splitContainer5.TabIndex = 4;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 31);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.listBox_props);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer6.Size = new System.Drawing.Size(876, 638);
            this.splitContainer6.SplitterDistance = 123;
            this.splitContainer6.TabIndex = 3;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.listBox_layers);
            this.splitContainer8.Size = new System.Drawing.Size(749, 638);
            this.splitContainer8.SplitterDistance = 130;
            this.splitContainer8.TabIndex = 0;
            // 
            // toolStrip_view
            // 
            this.toolStrip_view.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_view.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox_cellValue,
            this.toolStripTextBox_index,
            this.toolStripTextBox_xy,
            this.toolStripButton_focus,
            this.toolStripButton_gridLines,
            this.toolStripButton_scale,
            this.toolStripButton_prop_export});
            this.toolStrip_view.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_view.Name = "toolStrip_view";
            this.toolStrip_view.Size = new System.Drawing.Size(876, 31);
            this.toolStrip_view.TabIndex = 2;
            this.toolStrip_view.Text = "toolStrip1";
            // 
            // toolStripTextBox_cellValue
            // 
            this.toolStripTextBox_cellValue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_cellValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_cellValue.Name = "toolStripTextBox_cellValue";
            this.toolStripTextBox_cellValue.ReadOnly = true;
            this.toolStripTextBox_cellValue.Size = new System.Drawing.Size(132, 31);
            // 
            // toolStripTextBox_index
            // 
            this.toolStripTextBox_index.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_index.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_index.Name = "toolStripTextBox_index";
            this.toolStripTextBox_index.ReadOnly = true;
            this.toolStripTextBox_index.Size = new System.Drawing.Size(132, 31);
            // 
            // toolStripTextBox_xy
            // 
            this.toolStripTextBox_xy.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_xy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_xy.Name = "toolStripTextBox_xy";
            this.toolStripTextBox_xy.ReadOnly = true;
            this.toolStripTextBox_xy.Size = new System.Drawing.Size(265, 31);
            // 
            // toolStripButton_focus
            // 
            this.toolStripButton_focus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_focus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_focus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_focus.Image")));
            this.toolStripButton_focus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_focus.Name = "toolStripButton_focus";
            this.toolStripButton_focus.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton_focus.Text = "Home";
            this.toolStripButton_focus.Click += new System.EventHandler(this.homeToolStripButton_Click);
            // 
            // toolStripButton_gridLines
            // 
            this.toolStripButton_gridLines.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_gridLines.CheckOnClick = true;
            this.toolStripButton_gridLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_gridLines.Image = global::Pexel.Properties.Resources._3x3_grid;
            this.toolStripButton_gridLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_gridLines.Name = "toolStripButton_gridLines";
            this.toolStripButton_gridLines.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton_gridLines.Text = "Show/Hide Grid Lines";
            // 
            // toolStripButton_scale
            // 
            this.toolStripButton_scale.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_scale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_scale.Image = global::Pexel.Properties.Resources.scale;
            this.toolStripButton_scale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_scale.Name = "toolStripButton_scale";
            this.toolStripButton_scale.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton_scale.Text = "Scale";
            this.toolStripButton_scale.Click += new System.EventHandler(this.toolStripButton_propScale_Click);
            // 
            // toolStripButton_prop_export
            // 
            this.toolStripButton_prop_export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_prop_export.Image = global::Pexel.Properties.Resources._237009_file_document__arrow_move_export_128;
            this.toolStripButton_prop_export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_prop_export.Name = "toolStripButton_prop_export";
            this.toolStripButton_prop_export.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton_prop_export.Text = "Property Export";
            this.toolStripButton_prop_export.Click += new System.EventHandler(this.toolStripButton_prop_export_Click);
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 31);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.treeView_mods);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.dataGridView_mods);
            this.splitContainer7.Size = new System.Drawing.Size(395, 638);
            this.splitContainer7.SplitterDistance = 130;
            this.splitContainer7.TabIndex = 3;
            // 
            // toolStrip_mods
            // 
            this.toolStrip_mods.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_mods.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_mod_add,
            this.toolStripButton_mod_rename,
            this.toolStripButton_mod_remove});
            this.toolStrip_mods.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_mods.Name = "toolStrip_mods";
            this.toolStrip_mods.Size = new System.Drawing.Size(395, 31);
            this.toolStrip_mods.TabIndex = 2;
            this.toolStrip_mods.Text = "toolStrip1";
            // 
            // toolStripButton_mod_add
            // 
            this.toolStripButton_mod_add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_mod_add.Image = global::Pexel.Properties.Resources.Add;
            this.toolStripButton_mod_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_mod_add.Name = "toolStripButton_mod_add";
            this.toolStripButton_mod_add.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton_mod_add.Text = "toolStripButton_add_mod";
            this.toolStripButton_mod_add.Click += new System.EventHandler(this.toolStripButton_mod_add_Click);
            // 
            // toolStripButton_mod_rename
            // 
            this.toolStripButton_mod_rename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_mod_rename.Image = global::Pexel.Properties.Resources.Rename_icon;
            this.toolStripButton_mod_rename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_mod_rename.Name = "toolStripButton_mod_rename";
            this.toolStripButton_mod_rename.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton_mod_rename.Text = "toolStripButton_rename_mod";
            // 
            // toolStripButton_mod_remove
            // 
            this.toolStripButton_mod_remove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_mod_remove.Image = global::Pexel.Properties.Resources.deletered;
            this.toolStripButton_mod_remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_mod_remove.Name = "toolStripButton_mod_remove";
            this.toolStripButton_mod_remove.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton_mod_remove.Text = "toolStripButton_remove_mod";
            // 
            // PropView2DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer5);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PropView2DControl";
            this.Size = new System.Drawing.Size(1275, 669);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_mods)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.toolStrip_view.ResumeLayout(false);
            this.toolStrip_view.PerformLayout();
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.toolStrip_mods.ResumeLayout(false);
            this.toolStrip_mods.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox_layers;
        private System.Windows.Forms.ListBox listBox_props;
        private System.Windows.Forms.TreeView treeView_mods;
        private System.Windows.Forms.DataGridView dataGridView_mods;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.ToolStrip toolStrip_view;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_cellValue;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_index;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_xy;
        private System.Windows.Forms.ToolStripButton toolStripButton_focus;
        private System.Windows.Forms.ToolStripButton toolStripButton_gridLines;
        private System.Windows.Forms.ToolStripButton toolStripButton_scale;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.ToolStrip toolStrip_mods;
        private System.Windows.Forms.ToolStripButton toolStripButton_mod_add;
        private System.Windows.Forms.ToolStripButton toolStripButton_mod_rename;
        private System.Windows.Forms.ToolStripButton toolStripButton_mod_remove;
        private System.Windows.Forms.ToolStripButton toolStripButton_prop_export;
    }
}
