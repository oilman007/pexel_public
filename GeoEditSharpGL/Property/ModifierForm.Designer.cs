namespace Pexel
{
    partial class ModifierForm
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
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.valueLabel = new System.Windows.Forms.Label();
            this.textBox_radius = new System.Windows.Forms.TextBox();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.textBox_title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton_layers_all = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_layers_selected = new System.Windows.Forms.RadioButton();
            this.checkBox_autoTitle = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_value
            // 
            this.textBox_value.Location = new System.Drawing.Point(59, 64);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(110, 20);
            this.textBox_value.TabIndex = 3;
            this.textBox_value.Text = "1";
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_value.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_value_Validating);
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(10, 67);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(48, 13);
            this.valueLabel.TabIndex = 5;
            this.valueLabel.Text = "Multiplier";
            // 
            // textBox_radius
            // 
            this.textBox_radius.Location = new System.Drawing.Point(59, 38);
            this.textBox_radius.Name = "textBox_radius";
            this.textBox_radius.Size = new System.Drawing.Size(110, 20);
            this.textBox_radius.TabIndex = 2;
            this.textBox_radius.Text = "0";
            this.textBox_radius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_radius.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_radius_Validating);
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(10, 15);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(27, 13);
            this.radiusLabel.TabIndex = 4;
            this.radiusLabel.Text = "Title";
            // 
            // textBox_title
            // 
            this.textBox_title.Location = new System.Drawing.Point(59, 12);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Size = new System.Drawing.Size(110, 20);
            this.textBox_title.TabIndex = 1;
            this.textBox_title.Text = "mod_1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Radius";
            // 
            // radioButton_layers_all
            // 
            this.radioButton_layers_all.AutoSize = true;
            this.radioButton_layers_all.Location = new System.Drawing.Point(9, 19);
            this.radioButton_layers_all.Name = "radioButton_layers_all";
            this.radioButton_layers_all.Size = new System.Drawing.Size(36, 17);
            this.radioButton_layers_all.TabIndex = 6;
            this.radioButton_layers_all.TabStop = true;
            this.radioButton_layers_all.Text = "All";
            this.radioButton_layers_all.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_layers_selected);
            this.groupBox1.Controls.Add(this.radioButton_layers_all);
            this.groupBox1.Location = new System.Drawing.Point(13, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 47);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layers";
            // 
            // radioButton_layers_selected
            // 
            this.radioButton_layers_selected.AutoSize = true;
            this.radioButton_layers_selected.Checked = true;
            this.radioButton_layers_selected.Location = new System.Drawing.Point(83, 19);
            this.radioButton_layers_selected.Name = "radioButton_layers_selected";
            this.radioButton_layers_selected.Size = new System.Drawing.Size(67, 17);
            this.radioButton_layers_selected.TabIndex = 6;
            this.radioButton_layers_selected.TabStop = true;
            this.radioButton_layers_selected.Text = "Selected";
            this.radioButton_layers_selected.UseVisualStyleBackColor = true;
            // 
            // checkBox_autoTitle
            // 
            this.checkBox_autoTitle.AutoSize = true;
            this.checkBox_autoTitle.Checked = true;
            this.checkBox_autoTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_autoTitle.Location = new System.Drawing.Point(175, 14);
            this.checkBox_autoTitle.Name = "checkBox_autoTitle";
            this.checkBox_autoTitle.Size = new System.Drawing.Size(48, 17);
            this.checkBox_autoTitle.TabIndex = 8;
            this.checkBox_autoTitle.Text = "Auto";
            this.checkBox_autoTitle.UseVisualStyleBackColor = true;
            // 
            // ModifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 149);
            this.Controls.Add(this.checkBox_autoTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.textBox_title);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.textBox_radius);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radiusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ModifierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModifierForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModifiersForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.TextBox textBox_radius;
        private System.Windows.Forms.TextBox textBox_title;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_layers_all;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_layers_selected;
        private System.Windows.Forms.CheckBox checkBox_autoTitle;
    }
}