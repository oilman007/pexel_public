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
    public partial class PropScaleForm : Form
    {
        public PropScaleForm()
        {
            InitializeComponent();
        }








        void Apply(object sender, EventArgs e)
        {
            if (ApplyEvent != null)
                ApplyEvent(sender, e);
        }



        private void button_apply_Click(object sender, EventArgs e)
        {
            Apply(sender, e);
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



        public PropScale PropScale
        {
            set
            {
                this.textBox_minValue.Text = Helper.ShowDouble(value.Min);
                this.textBox_maxValue.Text = Helper.ShowDouble(value.Max);
                this.checkBox_auto.Checked = value.Auto;
            }
            get
            {
                return new PropScale(Helper.ParseDouble(textBox_minValue.Text), 
                                 		 Helper.ParseDouble(textBox_maxValue.Text),   
                                 		 this.checkBox_auto.Checked);
            }
        }



        public double MaxValue
        {
            set
            {
                this.textBox_maxValue.Text = Helper.ShowDouble(value);
            }
            get
            {
                return Helper.ParseDouble(textBox_maxValue.Text);
            }
        }



        public event EventHandler ApplyEvent;
        public event EventHandler GetFromPropEvent;


        void GetFromProp(object sender, EventArgs e)
        {
            if (GetFromPropEvent != null)
                GetFromPropEvent(sender, e);
        }


        private void button_getMinMax_Click(object sender, EventArgs e)
        {
            GetFromProp(sender, e);
        }










    }
}
