namespace Pexel
{
    partial class PropCreateForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_min = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_rand = new System.Windows.Forms.TabPage();
            this.tabPage_const = new System.Windows.Forms.TabPage();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_max = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPage_rand.SuspendLayout();
            this.tabPage_const.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(143, 135);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(62, 135);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 2;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(62, 12);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(145, 20);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.Text = "Prop";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Min";
            // 
            // textBox_min
            // 
            this.textBox_min.Location = new System.Drawing.Point(71, 6);
            this.textBox_min.Name = "textBox_min";
            this.textBox_min.Size = new System.Drawing.Size(100, 20);
            this.textBox_min.TabIndex = 1;
            this.textBox_min.Text = "1";
            this.textBox_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_min.Validating += new System.ComponentModel.CancelEventHandler(this.valueTextBox_Validating);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_const);
            this.tabControl.Controls.Add(this.tabPage_rand);
            this.tabControl.Location = new System.Drawing.Point(12, 38);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(199, 91);
            this.tabControl.TabIndex = 12;
            // 
            // tabPage_rand
            // 
            this.tabPage_rand.Controls.Add(this.textBox_max);
            this.tabPage_rand.Controls.Add(this.label4);
            this.tabPage_rand.Controls.Add(this.textBox_min);
            this.tabPage_rand.Controls.Add(this.label2);
            this.tabPage_rand.Location = new System.Drawing.Point(4, 22);
            this.tabPage_rand.Name = "tabPage_rand";
            this.tabPage_rand.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_rand.Size = new System.Drawing.Size(191, 65);
            this.tabPage_rand.TabIndex = 0;
            this.tabPage_rand.Text = "Random";
            this.tabPage_rand.UseVisualStyleBackColor = true;
            // 
            // tabPage_const
            // 
            this.tabPage_const.Controls.Add(this.textBox_value);
            this.tabPage_const.Controls.Add(this.label3);
            this.tabPage_const.Location = new System.Drawing.Point(4, 22);
            this.tabPage_const.Name = "tabPage_const";
            this.tabPage_const.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_const.Size = new System.Drawing.Size(191, 65);
            this.tabPage_const.TabIndex = 1;
            this.tabPage_const.Text = "Const";
            this.tabPage_const.UseVisualStyleBackColor = true;
            // 
            // textBox_value
            // 
            this.textBox_value.Location = new System.Drawing.Point(70, 19);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(100, 20);
            this.textBox_value.TabIndex = 13;
            this.textBox_value.Text = "1";
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_value.Validating += new System.ComponentModel.CancelEventHandler(this.valueTextBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Max";
            // 
            // textBox_max
            // 
            this.textBox_max.Location = new System.Drawing.Point(71, 32);
            this.textBox_max.Name = "textBox_max";
            this.textBox_max.Size = new System.Drawing.Size(100, 20);
            this.textBox_max.TabIndex = 1;
            this.textBox_max.Text = "10";
            this.textBox_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_max.Validating += new System.ComponentModel.CancelEventHandler(this.valueTextBox_Validating);
            // 
            // CreatePropForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(232, 171);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePropForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Prop";
            this.tabControl.ResumeLayout(false);
            this.tabPage_rand.ResumeLayout(false);
            this.tabPage_rand.PerformLayout();
            this.tabPage_const.ResumeLayout(false);
            this.tabPage_const.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_min;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_const;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage_rand;
        private System.Windows.Forms.TextBox textBox_max;
        private System.Windows.Forms.Label label4;
    }
}