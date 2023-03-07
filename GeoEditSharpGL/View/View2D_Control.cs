using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Pexel
{
    public partial class View2D_Control : UserControl
    {
        private ToolStrip toolStrip_view;
        private ToolStripTextBox toolStripTextBox_value;
        private ToolStripTextBox toolStripTextBox_index;
        private ToolStripButton toolStripButton_focus;
        private ToolStripButton toolStripButton_grid_lines;
        private ToolStripButton toolStripButton_scale;
        public View2D View2D { set; get; } = new View2D();
        private ToolStripButton toolStripButton_map_mode;
        private StatusStrip statusStrip;

        public View2D_Control()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View2D_Control));
            this.toolStrip_view = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox_value = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox_index = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_focus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_grid_lines = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_scale = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_map_mode = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip_view.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_view
            // 
            this.toolStrip_view.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_view.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox_value,
            this.toolStripTextBox_index,
            this.toolStripButton_focus,
            this.toolStripButton_grid_lines,
            this.toolStripButton_scale,
            this.toolStripButton_map_mode});
            this.toolStrip_view.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_view.Name = "toolStrip_view";
            this.toolStrip_view.Size = new System.Drawing.Size(514, 27);
            this.toolStrip_view.TabIndex = 1;
            this.toolStrip_view.Text = "toolStrip1";
            // 
            // toolStripTextBox_value
            // 
            this.toolStripTextBox_value.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_value.Name = "toolStripTextBox_value";
            this.toolStripTextBox_value.ReadOnly = true;
            this.toolStripTextBox_value.Size = new System.Drawing.Size(132, 27);
            // 
            // toolStripTextBox_index
            // 
            this.toolStripTextBox_index.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_index.Name = "toolStripTextBox_index";
            this.toolStripTextBox_index.ReadOnly = true;
            this.toolStripTextBox_index.Size = new System.Drawing.Size(132, 27);
            // 
            // toolStripButton_focus
            // 
            this.toolStripButton_focus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_focus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_focus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_focus.Image")));
            this.toolStripButton_focus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_focus.Name = "toolStripButton_focus";
            this.toolStripButton_focus.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_focus.Text = "Home";
            this.toolStripButton_focus.Click += new System.EventHandler(this.toolStripButton_focus_Click);
            // 
            // toolStripButton_grid_lines
            // 
            this.toolStripButton_grid_lines.CheckOnClick = true;
            this.toolStripButton_grid_lines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_grid_lines.Image = global::Pexel.Properties.Resources._3x3_grid;
            this.toolStripButton_grid_lines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_grid_lines.Name = "toolStripButton_grid_lines";
            this.toolStripButton_grid_lines.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_grid_lines.Text = "Show/Hide Grid Lines";
            this.toolStripButton_grid_lines.Click += new System.EventHandler(this.toolStripButton_grid_lines_Click);
            // 
            // toolStripButton_scale
            // 
            this.toolStripButton_scale.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_scale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_scale.Image = global::Pexel.Properties.Resources.scale;
            this.toolStripButton_scale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_scale.Name = "toolStripButton_scale";
            this.toolStripButton_scale.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_scale.Text = "Scale";
            // 
            // toolStripButton_map_mode
            // 
            this.toolStripButton_map_mode.CheckOnClick = true;
            this.toolStripButton_map_mode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_map_mode.Image = global::Pexel.Properties.Resources.map3;
            this.toolStripButton_map_mode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_map_mode.Name = "toolStripButton_map_mode";
            this.toolStripButton_map_mode.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton_map_mode.Text = "toolStripButton1";
            this.toolStripButton_map_mode.Click += new System.EventHandler(this.toolStripButton_map_mode_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 536);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(514, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // richTextBox1
            // 
            this.View2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.View2D.Location = new System.Drawing.Point(0, 27);
            this.View2D.Name = "richTextBox1";
            this.View2D.Size = new System.Drawing.Size(514, 509);
            this.View2D.TabIndex = 3;
            this.View2D.Text = "";
            // 
            // View2D_Control
            // 
            this.Controls.Add(this.View2D);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip_view);
            this.Name = "View2D_Control";
            this.Size = new System.Drawing.Size(514, 558);
            this.toolStrip_view.ResumeLayout(false);
            this.toolStrip_view.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void View2d_PositionChanged(object sender, EventArgs e)
        {
            toolStripTextBox_index.Text = "I:" + Helper.ShowInt(View2D.CurrCellIndex.X + 1) + " J:" + Helper.ShowInt(View2D.CurrCellIndex.Y + 1);
            toolStripTextBox_value.Text = Helper.ShowDouble(View2D.Map.Values[View2D.CurrCellIndex.X, View2D.CurrCellIndex.Y]);
        }

        double CurrValue
        {
            set
            {
                this.toolStripTextBox_value.Text = Helper.ShowDouble(value);
            }
            get
            {
                return Helper.ParseDouble(toolStripTextBox_value.Text);
            }
        }

        private void View2d_MouseLeave(object sender, EventArgs e)
        {
            this.toolStripTextBox_index.Text = string.Empty;
            this.toolStripTextBox_value.Text = string.Empty;
            this.statusStrip.Text = string.Empty;

        }

        private void toolStripButton_grid_lines_Click(object sender, EventArgs e)
        {
            View2D.ShowGridLines = this.toolStripButton_grid_lines.Checked;
        }

        private void toolStripButton_map_mode_Click(object sender, EventArgs e)
        {
            View2D.MapMode = this.toolStripButton_map_mode.Checked;
        }

        private void toolStripButton_focus_Click(object sender, EventArgs e)
        {
            View2D.HomePosition();
        }
    }
}
