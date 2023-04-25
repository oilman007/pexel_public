namespace Pexel
{
    partial class GridCreateForm
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
            this.textBox_nx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_ny = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_nz = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_xSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ySize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_zSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.textBox_depth = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_yShift = new System.Windows.Forms.TextBox();
            this.textBox_xShift = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_yAngle = new System.Windows.Forms.TextBox();
            this.textBox_xAngle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_nx
            // 
            this.textBox_nx.Location = new System.Drawing.Point(91, 13);
            this.textBox_nx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_nx.MaxLength = 4;
            this.textBox_nx.Name = "textBox_nx";
            this.textBox_nx.Size = new System.Drawing.Size(132, 22);
            this.textBox_nx.TabIndex = 2;
            this.textBox_nx.Text = "40";
            this.textBox_nx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_nx.Validating += new System.ComponentModel.CancelEventHandler(this.nTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "NX";
            // 
            // textBox_ny
            // 
            this.textBox_ny.Location = new System.Drawing.Point(91, 43);
            this.textBox_ny.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_ny.MaxLength = 4;
            this.textBox_ny.Name = "textBox_ny";
            this.textBox_ny.Size = new System.Drawing.Size(132, 22);
            this.textBox_ny.TabIndex = 3;
            this.textBox_ny.Text = "30";
            this.textBox_ny.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_ny.Validating += new System.ComponentModel.CancelEventHandler(this.nTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "NY";
            // 
            // textBox_nz
            // 
            this.textBox_nz.Location = new System.Drawing.Point(91, 75);
            this.textBox_nz.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_nz.MaxLength = 4;
            this.textBox_nz.Name = "textBox_nz";
            this.textBox_nz.Size = new System.Drawing.Size(132, 22);
            this.textBox_nz.TabIndex = 4;
            this.textBox_nz.Text = "20";
            this.textBox_nz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_nz.Validating += new System.ComponentModel.CancelEventHandler(this.nTextBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "NZ";
            // 
            // textBox_xSize
            // 
            this.textBox_xSize.Location = new System.Drawing.Point(91, 107);
            this.textBox_xSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_xSize.Name = "textBox_xSize";
            this.textBox_xSize.Size = new System.Drawing.Size(132, 22);
            this.textBox_xSize.TabIndex = 5;
            this.textBox_xSize.Text = "100";
            this.textBox_xSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_xSize.Validating += new System.ComponentModel.CancelEventHandler(this.cellSizeTextBox_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 111);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "X-Size, m";
            // 
            // textBox_ySize
            // 
            this.textBox_ySize.Location = new System.Drawing.Point(91, 139);
            this.textBox_ySize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_ySize.Name = "textBox_ySize";
            this.textBox_ySize.Size = new System.Drawing.Size(132, 22);
            this.textBox_ySize.TabIndex = 6;
            this.textBox_ySize.Text = "100";
            this.textBox_ySize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_ySize.Validating += new System.ComponentModel.CancelEventHandler(this.cellSizeTextBox_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 143);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Y-Size, m";
            // 
            // textBox_zSize
            // 
            this.textBox_zSize.Location = new System.Drawing.Point(91, 171);
            this.textBox_zSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_zSize.Name = "textBox_zSize";
            this.textBox_zSize.Size = new System.Drawing.Size(132, 22);
            this.textBox_zSize.TabIndex = 7;
            this.textBox_zSize.Text = "0.5";
            this.textBox_zSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_zSize.Validating += new System.ComponentModel.CancelEventHandler(this.cellSizeTextBox_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 175);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Z-Size, m";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(18, 387);
            this.addButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 28);
            this.addButton.TabIndex = 13;
            this.addButton.Text = "Save...";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(126, 387);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // textBox_depth
            // 
            this.textBox_depth.Location = new System.Drawing.Point(91, 269);
            this.textBox_depth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_depth.Name = "textBox_depth";
            this.textBox_depth.Size = new System.Drawing.Size(132, 22);
            this.textBox_depth.TabIndex = 10;
            this.textBox_depth.Text = "2000";
            this.textBox_depth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_depth.Validating += new System.ComponentModel.CancelEventHandler(this.coordTextBox_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 207);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "X-Shift, m";
            // 
            // textBox_yShift
            // 
            this.textBox_yShift.Location = new System.Drawing.Point(91, 237);
            this.textBox_yShift.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_yShift.Name = "textBox_yShift";
            this.textBox_yShift.Size = new System.Drawing.Size(132, 22);
            this.textBox_yShift.TabIndex = 9;
            this.textBox_yShift.Text = "0";
            this.textBox_yShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_yShift.Validating += new System.ComponentModel.CancelEventHandler(this.coordTextBox_Validating);
            // 
            // textBox_xShift
            // 
            this.textBox_xShift.Location = new System.Drawing.Point(91, 203);
            this.textBox_xShift.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_xShift.Name = "textBox_xShift";
            this.textBox_xShift.Size = new System.Drawing.Size(132, 22);
            this.textBox_xShift.TabIndex = 8;
            this.textBox_xShift.Text = "0";
            this.textBox_xShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_xShift.Validating += new System.ComponentModel.CancelEventHandler(this.coordTextBox_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 240);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Y-Shift, m";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 272);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 16);
            this.label10.TabIndex = 1;
            this.label10.Text = "Depth, m";
            // 
            // textBox_yAngle
            // 
            this.textBox_yAngle.Location = new System.Drawing.Point(91, 334);
            this.textBox_yAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_yAngle.Name = "textBox_yAngle";
            this.textBox_yAngle.Size = new System.Drawing.Size(132, 22);
            this.textBox_yAngle.TabIndex = 12;
            this.textBox_yAngle.Text = "0";
            this.textBox_yAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_yAngle.Validating += new System.ComponentModel.CancelEventHandler(this.angleTextBox_Validating);
            // 
            // textBox_xAngle
            // 
            this.textBox_xAngle.Location = new System.Drawing.Point(91, 301);
            this.textBox_xAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_xAngle.Name = "textBox_xAngle";
            this.textBox_xAngle.Size = new System.Drawing.Size(132, 22);
            this.textBox_xAngle.TabIndex = 11;
            this.textBox_xAngle.Text = "0";
            this.textBox_xAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_xAngle.Validating += new System.ComponentModel.CancelEventHandler(this.angleTextBox_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 304);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 16);
            this.label11.TabIndex = 1;
            this.label11.Text = "X angle, °";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 337);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 16);
            this.label12.TabIndex = 1;
            this.label12.Text = "Y angle, °";
            // 
            // GridCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(246, 435);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_nz);
            this.Controls.Add(this.textBox_xAngle);
            this.Controls.Add(this.textBox_yAngle);
            this.Controls.Add(this.textBox_xShift);
            this.Controls.Add(this.textBox_yShift);
            this.Controls.Add(this.textBox_depth);
            this.Controls.Add(this.textBox_zSize);
            this.Controls.Add(this.textBox_ySize);
            this.Controls.Add(this.textBox_xSize);
            this.Controls.Add(this.textBox_ny);
            this.Controls.Add(this.textBox_nx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Grid";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateGridForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_nx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_ny;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_nz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_xSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ySize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_zSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox textBox_depth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_yShift;
        private System.Windows.Forms.TextBox textBox_xShift;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_yAngle;
        private System.Windows.Forms.TextBox textBox_xAngle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}