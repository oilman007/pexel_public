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


    public enum CreatePropType { Const, Random }


    public partial class PropCreateForm : Form
    {
        public PropCreateForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (AddProp != null)
                AddProp(sender, e);
            this.Close();
        }
        

        public event EventHandler AddProp;


        
        public double Value
        {
            set
            {
                textBox_value.Text = Helper.ShowDouble(value);
            }
            get
            {
                return Helper.ParseDouble(textBox_value.Text);
            }
        }

        public double Min
        {
            set
            {
                textBox_min.Text = Helper.ShowDouble(value);
            }
            get
            {
                return Helper.ParseDouble(textBox_min.Text);
            }
        }


        public double Max
        {
            set
            {
                textBox_max.Text = value.ToString();
            }
            get
            {
                return Helper.ParseDouble(textBox_max.Text);
            }
        }


        public string Title
        {
            set
            {
                titleTextBox.Text = value;
            }
            get
            {
                return titleTextBox.Text;
            }
        }




        public CreatePropType CreatePropType
        {
            get { return this.tabControl.SelectedIndex == 0 ? CreatePropType.Const : CreatePropType.Random; }
            set { if (value == CreatePropType.Const) this.tabControl.SelectedIndex = 0; else this.tabControl.SelectedIndex = 1; }
        }



        bool IsValue(string txtvalue)
        {
            double value;
            return Helper.TryParseDouble(txtvalue, out value); // && value > 0.0;
        }

        private void valueTextBox_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValue(((TextBox)sender).Text);
        }
    }
}
