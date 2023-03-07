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


    

    public partial class SetValueForm : Form
    {
        public SetValueForm()
        {
            InitializeComponent();
        }








        void Apply(object sender, EventArgs e)
        {
            if (ApplyEvent != null)
                ApplyEvent(sender, e);
        }

        
        private void button_ok_Click(object sender, EventArgs e)
        {
            Apply(sender, e);
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        
        public double Value
        {
            set
            {
                this.textBox_value.Text = Helper.ShowDouble(value);
            }
            get
            {
                return Helper.ParseDouble(this.textBox_value.Text);
            }
        }


        
        public bool IsMult
        {
            get
            {
                return this.radioButton_mult.Checked;
            }
        }


        public event EventHandler ApplyEvent;
        
        

        bool IsValidValue(string txtValue)
        {
            double value;
            return Helper.TryParseDouble(txtValue, out value);
            //return (double.TryParse(txtValue, out value) && value >= 0f);
        }

        private void textBox_value_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_value.Text);
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            Apply(sender, e);
        }
    }
}
