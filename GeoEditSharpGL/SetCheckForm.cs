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
    public partial class SetCheckForm : Form
    {
        public SetCheckForm()
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



        public bool Value
        {
            set
            {
                this.checkBox_use.Checked = value;
            }
            get
            {
                return checkBox_use.Checked;
            }
        }



        public event EventHandler ApplyEvent;

        

    }
}
