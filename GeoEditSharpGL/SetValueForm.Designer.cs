namespace Pexel
{
    partial class SetValueForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_apply = new System.Windows.Forms.Button();
            this.radioButton_set = new System.Windows.Forms.RadioButton();
            this.radioButton_mult = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Value";
            // 
            // textBox_value
            // 
            this.textBox_value.Location = new System.Drawing.Point(52, 36);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(197, 20);
            this.textBox_value.TabIndex = 1;
            this.textBox_value.Text = "1";
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_value.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_value_Validating);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(93, 63);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(174, 63);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(12, 63);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 2;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // radioButton_set
            // 
            this.radioButton_set.AutoSize = true;
            this.radioButton_set.Checked = true;
            this.radioButton_set.Location = new System.Drawing.Point(142, 12);
            this.radioButton_set.Name = "radioButton_set";
            this.radioButton_set.Size = new System.Drawing.Size(41, 17);
            this.radioButton_set.TabIndex = 3;
            this.radioButton_set.TabStop = true;
            this.radioButton_set.Text = "Set";
            this.radioButton_set.UseVisualStyleBackColor = true;
            // 
            // radioButton_mult
            // 
            this.radioButton_mult.AutoSize = true;
            this.radioButton_mult.Location = new System.Drawing.Point(189, 12);
            this.radioButton_mult.Name = "radioButton_mult";
            this.radioButton_mult.Size = new System.Drawing.Size(60, 17);
            this.radioButton_mult.TabIndex = 4;
            this.radioButton_mult.Text = "Multiply";
            this.radioButton_mult.UseVisualStyleBackColor = true;
            // 
            // SetValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 98);
            this.Controls.Add(this.radioButton_mult);
            this.Controls.Add(this.radioButton_set);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetValueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SetValueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.RadioButton radioButton_set;
        private System.Windows.Forms.RadioButton radioButton_mult;
    }
}