using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pexel
{
    public partial class GridCreateForm : Form
    {
        public GridCreateForm()
        {
            InitializeComponent();
            this.textBox_zSize.Text = (0.5).ToString();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (AddGrid != null)
                AddGrid(sender, e);
            this.Close();
        }




        public event EventHandler AddGrid;





        public int NX
        {
            set { textBox_nx.Text = value.ToString(); }
            get { return Helper.ParseInt(textBox_nx.Text); }
        }
        public int NY
        {
            set { textBox_ny.Text = value.ToString(); }
            get { return Helper.ParseInt(textBox_ny.Text); }
        }
        public int NZ
        {
            set { textBox_nz.Text = value.ToString(); }
            get { return Helper.ParseInt(textBox_nz.Text); }
        }

        public double XSize
        {
            set { textBox_xSize.Text = value.ToString(); }
            get { return Helper.ParseDouble(textBox_xSize.Text); }
        }
        public double YSize
        {
            set { textBox_ySize.Text = value.ToString(); }
            get { return Helper.ParseDouble(textBox_ySize.Text); }
        }
        public double ZSize
        {
            set { textBox_zSize.Text = value.ToString(); }
            get { return Helper.ParseDouble(textBox_zSize.Text); }
        }
        public double Depth
        {
            set { this.textBox_depth.Text = value.ToString(); }
            get { return Helper.ParseDouble(this.textBox_depth.Text); }
        }
        public string Title
        {
            set { this.textBox_title.Text = value; }
            get { return this.textBox_title.Text; }
        }
        public double XShift
        {
            set { this.textBox_xShift.Text = value.ToString(); }
            get { return Helper.ParseDouble(this.textBox_xShift.Text); }
        }
        public double YShift
        {
            set { this.textBox_yShift.Text = value.ToString(); }
            get { return Helper.ParseDouble(this.textBox_yShift.Text); }
        }
        public double XAngle
        {
            set { this.textBox_xAngle.Text = value.ToString(); }
            get { return Helper.ParseDouble(this.textBox_xAngle.Text); }
        }
        public double YAngle
        {
            set { this.textBox_yAngle.Text = value.ToString(); }
            get { return Helper.ParseDouble(this.textBox_yAngle.Text); }
        }









        private void CreateGridForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }








        bool IsNatural(string txtvalue)
        {
            int value;
            return Helper.TryParseInt(txtvalue, out value) && value > 0;
        }

        bool IsLength(string txtvalue)
        {
            double value;
            return Helper.TryParseDouble(txtvalue, out value) && value > 0.0;
        }

        bool IsCoord(string txtvalue)
        {
            double value;
            return Helper.TryParseDouble(txtvalue, out value);
        }
        
        bool IsAngle(string txtvalue)
        {
            double v;
            return Helper.TryParseDouble(txtvalue, out v) && (-365.0 <= v) && (v <= 360.0);
        }





        private void nTextBox_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsNatural(((TextBox)sender).Text.ToString());
        }
        private void cellSizeTextBox_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsLength(((TextBox)sender).Text.ToString());
        }
        private void coordTextBox_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsCoord(((TextBox)sender).Text.ToString());
        }
        private void angleTextBox_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsAngle(((TextBox)sender).Text.ToString());
        }






    }
}
