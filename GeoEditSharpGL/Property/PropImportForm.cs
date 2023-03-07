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
    public partial class PropImportForm : Form
    {
        public PropImportForm()
        {
            InitializeComponent();
            ChoosenKW = new List<string>();
        }

        public List<string> ChoosenKW { set; get; }

        public List<string> KW
        {
            set
            {
                kwCheckedList.Items.Clear();
                foreach (string kw in value)
                {
                    kwCheckedList.Items.Add(kw);
                }
            }
            get
            {
                List<string> r = new List<string>();
                foreach (object item in kwCheckedList.Items)
                {
                    r.Add(item.ToString());
                }
                return r;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            foreach (object item in kwCheckedList.CheckedItems)
            {
                ChoosenKW.Add(item.ToString());
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
