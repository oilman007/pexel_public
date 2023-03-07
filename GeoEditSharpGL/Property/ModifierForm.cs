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

    public enum SelectedLayers { None, All, Selected }


    public partial class ModifierForm : Form
    {
        public ModifierForm()
        {
            InitializeComponent();
        }

        private void ModifiersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }



        public string Title
        {
            set
            {
                this.textBox_title.Text = value;
            }
            get
            {
                return this.textBox_title.Text;
            }
        }


        public bool AutoTitle
        {
            set
            {
                checkBox_autoTitle.Checked = value;
            }
            get
            {
                return checkBox_autoTitle.Checked;
            }
        }


        public double Radius
        {
            set
            {
                this.textBox_radius.Text = value.ToString();
            }
            get
            {
                double result;
                if (Helper.TryParseDouble(this.textBox_radius.Text, out result))
                    return result;
                else return 0;
            }
        }


        public double Value
        {
            set
            {
                this.textBox_value.Text = value.ToString();
            }
            get
            {
                double result;
                if (Helper.TryParseDouble(this.textBox_value.Text, out result))
                    return result;
                else return 1;
            }
        }



        public SelectedLayers Layers
        {
            get
            {
                if (this.radioButton_layers_all.Checked)
                    return SelectedLayers.All;
                if (this.radioButton_layers_selected.Checked)
                    return SelectedLayers.Selected;
                return SelectedLayers.None;
            }
        }

            
        




        bool IsValid(string txtvalue)
        {
            double value;
            return Helper.TryParseDouble(txtvalue, out value);
            //return valid && value > 0;
        }





        private void textBox_radius_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValid(textBox_radius.Text.ToString());
        }

        private void textBox_value_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValid(textBox_value.Text.ToString());
        }












    }
}
