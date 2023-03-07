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
    public partial class SetNameForm : Form
    {
        public SetNameForm()
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
            Ok(sender, e);
        }

        void Ok(object sender, EventArgs e)
        {
            Apply(sender, e);
            Close();
        }



        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }



        public string Value
        {
            set
            {
                this.textBox_name.Text = value;
            }
            get
            {
                return this.textBox_name.Text;
            }
        }



        public event EventHandler ApplyEvent;

        private void textBox_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) Ok(sender, null);
        }
    }
}
