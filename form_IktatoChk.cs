using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Report
{
    public partial class form_IktatoChk : Form
    {
        public form_IktatoChk()
        {
            InitializeComponent();
        }

        private void form_IktatoChk_Load(object sender, EventArgs e)
        {
            label2.Text = FormCode.iktatoszam.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCode.iktato_chk_eredmeny = "0";
            this.Close();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            FormCode.iktato_chk_eredmeny = "1";
            this.Close();
        }
    }
}