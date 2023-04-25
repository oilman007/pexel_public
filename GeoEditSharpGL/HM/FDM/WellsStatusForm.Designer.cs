namespace Pexel.HM.HMFFR
{
    partial class WellsStatusForm
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
            this.dataGridView_wells = new System.Windows.Forms.DataGridView();
            this.ColumnWell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPiezo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnHCTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_wells)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_wells
            // 
            this.dataGridView_wells.AllowUserToAddRows = false;
            this.dataGridView_wells.AllowUserToDeleteRows = false;
            this.dataGridView_wells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_wells.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnWell,
            this.ColumnStatus,
            this.ColumnLink,
            this.ColumnF,
            this.ColumnPiezo,
            this.ColumnDistance,
            this.ColumnHCTime});
            this.dataGridView_wells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_wells.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_wells.Name = "dataGridView_wells";
            this.dataGridView_wells.Size = new System.Drawing.Size(748, 495);
            this.dataGridView_wells.TabIndex = 0;
            // 
            // ColumnWell
            // 
            this.ColumnWell.HeaderText = "Well";
            this.ColumnWell.Name = "ColumnWell";
            this.ColumnWell.ReadOnly = true;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            // 
            // ColumnLink
            // 
            this.ColumnLink.HeaderText = "Link";
            this.ColumnLink.Name = "ColumnLink";
            this.ColumnLink.ReadOnly = true;
            // 
            // ColumnF
            // 
            this.ColumnF.HeaderText = "F";
            this.ColumnF.Name = "ColumnF";
            this.ColumnF.ReadOnly = true;
            // 
            // ColumnPiezo
            // 
            this.ColumnPiezo.HeaderText = "Piezo";
            this.ColumnPiezo.Name = "ColumnPiezo";
            this.ColumnPiezo.ReadOnly = true;
            // 
            // ColumnDistance
            // 
            this.ColumnDistance.HeaderText = "Distance";
            this.ColumnDistance.Name = "ColumnDistance";
            this.ColumnDistance.ReadOnly = true;
            // 
            // ColumnHCTime
            // 
            this.ColumnHCTime.HeaderText = "HCTime";
            this.ColumnHCTime.Name = "ColumnHCTime";
            this.ColumnHCTime.ReadOnly = true;
            // 
            // WellsStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 495);
            this.Controls.Add(this.dataGridView_wells);
            this.Name = "WellsStatusForm";
            this.Text = "WellsStatusForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_wells)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_wells;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWell;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPiezo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnHCTime;
    }
}