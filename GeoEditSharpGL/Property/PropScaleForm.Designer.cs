namespace Pexel
{
    partial class PropScaleForm
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
            this.textBox_minValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_maxValue = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_getFromProp = new System.Windows.Forms.Button();
            this.checkBox_auto = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Min Value";
            // 
            // textBox_minValue
            // 
            this.textBox_minValue.Location = new System.Drawing.Point(77, 6);
            this.textBox_minValue.Name = "textBox_minValue";
            this.textBox_minValue.Size = new System.Drawing.Size(111, 20);
            this.textBox_minValue.TabIndex = 1;
            this.textBox_minValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Max Value";
            // 
            // textBox_maxValue
            // 
            this.textBox_maxValue.Location = new System.Drawing.Point(77, 32);
            this.textBox_maxValue.Name = "textBox_maxValue";
            this.textBox_maxValue.Size = new System.Drawing.Size(111, 20);
            this.textBox_maxValue.TabIndex = 1;
            this.textBox_maxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(121, 91);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(202, 91);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // button_getFromProp
            // 
            this.button_getFromProp.Location = new System.Drawing.Point(202, 12);
            this.button_getFromProp.Name = "button_getFromProp";
            this.button_getFromProp.Size = new System.Drawing.Size(75, 36);
            this.button_getFromProp.TabIndex = 2;
            this.button_getFromProp.Text = "Get From Prop";
            this.button_getFromProp.UseVisualStyleBackColor = true;
            this.button_getFromProp.Click += new System.EventHandler(this.button_getMinMax_Click);
            // 
            // checkBox_auto
            // 
            this.checkBox_auto.AutoSize = true;
            this.checkBox_auto.Location = new System.Drawing.Point(77, 58);
            this.checkBox_auto.Name = "checkBox_auto";
            this.checkBox_auto.Size = new System.Drawing.Size(86, 17);
            this.checkBox_auto.TabIndex = 3;
            this.checkBox_auto.Text = "Auto Update";
            this.checkBox_auto.UseVisualStyleBackColor = true;
            // 
            // ScalePropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 123);
            this.Controls.Add(this.checkBox_auto);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_getFromProp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_maxValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_minValue);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScalePropForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ScalePropForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_minValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_maxValue;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_getFromProp;
        private System.Windows.Forms.CheckBox checkBox_auto;
    }
}