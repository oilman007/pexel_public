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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }


        private void linkLabel_mail_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel_mail.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("mailto:aubakirov-artur@yandex.ru");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
