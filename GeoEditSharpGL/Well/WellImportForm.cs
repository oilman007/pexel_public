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
    public partial class WellImportForm : Form
    {
        public WellImportForm()
        {
            InitializeComponent();
        }


        void Apply(object sender, EventArgs e)
        {
            //Result = true;
            if (ApplyEvent != null)
                ApplyEvent(sender, e);
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            Result = true;
            Apply(sender, e);
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Result = false;
            Close();
        }


        public event EventHandler ApplyEvent;
        public bool Result { set; get; }



        bool IsValidValue(string txtValue)
        {
            int value;
            return Helper.TryParseInt(txtValue, out value) && 1 <= value && value <= 4;
        }



        public string Prefix
        {
            get
            {
                return this.textBox_prefix.Text;
            }
        }

        public string Ending
        {
            get
            {
                return this.textBox_ending.Text;
            }
        }
        
        public int X
        {
            get
            {
                return Helper.ParseInt(this.textBox_x.Text) - 1;
            }
        }

        public int Y
        {
            get
            {
                return Helper.ParseInt(this.textBox_y.Text) - 1;
            }
        }

        public int TVD
        {
            get
            {
                return Helper.ParseInt(this.textBox_tvd.Text) - 1;
            }
        }

        public int MD
        {
            get
            {
                return Helper.ParseInt(this.textBox_md.Text) - 1;
            }
        }

        private void textBox_x_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_x.Text.ToString());
        }

        private void textBox_y_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_y.Text.ToString());
        }

        private void textBox_tvd_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_tvd.Text.ToString());
        }

        private void textBox_md_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_md.Text.ToString());
        }

        private void WellImportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Result = false;
        }
    }
}
