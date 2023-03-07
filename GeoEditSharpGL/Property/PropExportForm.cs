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
    public partial class PropExportForm : Form
    {
        public PropExportForm()
        {
            InitializeComponent();
            OK = false;
        }

        public bool OK { set; get; }


        public string PropName
        {
            set { this.textBox_prop_name.Text = value; }
            get { return Helper.ClearLine(this.textBox_prop_name.Text); }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            OK = true;
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
