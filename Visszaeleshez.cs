using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Report
{
    public partial class Visszaeleshez : Form
    {
        public Visszaeleshez()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            DialogResult dr01 = MessageBox.Show("A megadott adatok helyesek?\n\nAz 'Igen' gombra t�rt�n� kattint�st k�vet�n nincs lehet�s�g az adatokban t�rt�n� m�dos�t�sra!", "Figyelem!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr01 == DialogResult.Yes)
            {
                FormCode.vizsg_elozmeny = textBox1.Text;
                FormCode.vizsg_nev = textBox2.Text;
                FormCode.vizsg_munkakor = textBox3.Text;
                Close();
            }
            else
            {

            }
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{

        //}
    }
}