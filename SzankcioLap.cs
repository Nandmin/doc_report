using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Data.Linq;
using System.Windows.Forms;

namespace Report
{
    public partial class SzankcioLap : Form
    {
        public SzankcioLap()
        {
            InitializeComponent();
        }

        public string book;
        public XmlDocument doc_sap;     // SAP adatok
        public XmlDocument doc;         // Postalista elérhetõsége
        public XmlNodeList postalista;  // postalista változó
        public string chkbox1 = "Nem";
        public string TIG;              // Területi igazgatóság azonosító - SAP betöltéshez
        public string eredmeny;
        

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SzankcioLap_Load(object sender, EventArgs e)
        {
            
            XmlDocument doc = new XmlDocument(); // Ez máûködik, 
            //doc.Load(@"C:\Teszt_adat\Postalista.xml");//Server.MapPath("regis.xml"));
            doc.Load(@"\\teamweb2\sites\TMEK\manager\Connections\Postalista.xml");
            
            XmlNodeList postalista;
            XmlNode root = doc.DocumentElement;
            postalista = root.SelectNodes("descendant::Postalista[contains(Igazgatóság, 'Igazgatóság')]");//Nyugat-magyarországi Területi Igazgatóság']");

            foreach (XmlNode posta in postalista)
            {
                comboBox1.Items.Add(posta.FirstChild.InnerText);
            }

            if (comboBox2.Text == "")
            {
                checkBox1.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void sap_betolto()
        {
            if (textBox3.Text == "Központi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                doc_sap.Load(@"C:\Teszt_adat\SAP\SAP_KTIG.xml");
                TIG = "SAP_KTIG";
            }
            else if (textBox3.Text == "Nyugat-magyarországi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                doc_sap.Load(@"C:\Teszt_adat\SAP\SAP_NYMTIG.xml");
                TIG = "SAP_NYMTIG";
            }
            else if (textBox3.Text == "Kelet-magyarországi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                doc_sap.Load(@"C:\Teszt_adat\SAP\SAP_KMTIG.xml");
                TIG = "SAP_KMTIG";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // combox2 feltöltése a kiválasztott posta dolgozóinak nevével
        {
            XmlDocument doc = new XmlDocument();
            //doc.Load(@"C:\Teszt_adat\Postalista.xml");
            doc.Load(@"\\teamweb2\sites\TMEK\manager\Connections\Postalista.xml");
            postalista = doc.DocumentElement.SelectNodes("descendant::Postalista[Név='" + comboBox1.Text + "']");
            foreach (XmlNode posta in postalista)
            {
                textBox3.Text = posta.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                textBox4.Text = posta.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
            }


            if (textBox3.Text == "Központi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                //doc_sap.Load(@"C:\Teszt_adat\SAP\SAP_KTIG.xml");
                doc_sap.Load(@"\\teamweb2\sites\TMEK\manager\Connections\SAP_KTIG.xml");
                TIG = "SAP_KTIG";

                XmlNodeList sapTIG = doc_sap.DocumentElement.SelectNodes("descendant::" + TIG + "[PostaNev='" + comboBox1.Text + "']");
                comboBox2.Items.Clear();
                textBox1.Clear();
                textBox2.Clear();
                //textBox4.Clear();

                foreach (XmlNode sap in sapTIG)
                {
                    comboBox2.Items.Add(sap.FirstChild.NextSibling.NextSibling.NextSibling.InnerText);
                }
            }
            else if (textBox3.Text == "Nyugat-magyarországi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                //doc_sap.Load(@"C:\Teszt_adat\SAP\SAP_NYMTIG.xml");
                doc_sap.Load(@"\\teamweb2\sites\TMEK\manager\Connections\SAP_NYMTIG.xml");
                TIG = "SAP_NYMTIG";

                XmlNodeList sapTIG = doc_sap.DocumentElement.SelectNodes("descendant::" + TIG + "[PostaNev='" + comboBox1.Text + "']");
                comboBox2.Items.Clear();
                textBox1.Clear();
                textBox2.Clear();
                //textBox4.Clear();

                foreach (XmlNode sap in sapTIG)
                {
                    comboBox2.Items.Add(sap.FirstChild.NextSibling.NextSibling.NextSibling.InnerText);
                }
            }
            else if (textBox3.Text == "Kelet-magyarországi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                //doc_sap.Load(@"C:\Teszt_adat\SAP\SAP_KMTIG.xml");
                doc_sap.Load(@"\\teamweb2\sites\TMEK\manager\Connections\SAP_KMTIG.xml");
                TIG = "SAP_KMTIG";

                XmlNodeList sapTIG = doc_sap.DocumentElement.SelectNodes("descendant::" + TIG + "[PostaNev='" + comboBox1.Text + "']");
                comboBox2.Items.Clear();
                textBox1.Clear();
                textBox2.Clear();
                

                foreach (XmlNode sap in sapTIG)
                {
                    comboBox2.Items.Add(sap.FirstChild.NextSibling.NextSibling.NextSibling.InnerText);
                }
            }

            if (comboBox1.Text.Contains("irendeltség"))
            {
                comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
            }
            else
            {
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
            }

            if (comboBox1.Text != "" && textBox2.Text == "") // comboBox2.Text == "")
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }

            textBox5.Text = comboBox2.Items.Count.ToString();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;

            if (comboBox2.Text == "")
            {
                button3.Visible = false;
                button3.Enabled = false;
            }
            else
            {
                button3.Visible = true;
                button3.Enabled = true;
            }

            if (textBox3.Text == "Központi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                doc_sap.Load(@"\\teamweb2\sites\TMEK\manager\Connections\SAP_KTIG.xml");
                TIG = "SAP_KTIG";
                XmlNodeList munkavallaloSAP = doc_sap.DocumentElement.SelectNodes("descendant::" + TIG + "[PostaNev='" + comboBox1.Text + "']" + "[DolgozoNev='" + comboBox2.Text + "']" + "[SZTSZ!='" + textBox1.Text + "']");

                foreach (XmlNode mv_sap in munkavallaloSAP)
                {
                    textBox1.Text = mv_sap.FirstChild.InnerText.ToString();
                    textBox2.Text = mv_sap.LastChild.InnerText;
                }
            }
            else if (textBox3.Text == "Nyugat-magyarországi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                //doc_sap.Load(@"C:\Teszt_adat\SAP\SAP_NYMTIG.xml");
                doc_sap.Load(@"\\teamweb2\sites\TMEK\manager\Connections\SAP_NYMTIG.xml");
                TIG = "SAP_NYMTIG";
                XmlNodeList munkavallaloSAP = doc_sap.DocumentElement.SelectNodes("descendant::" + TIG + "[PostaNev='" + comboBox1.Text + "']" + "[DolgozoNev='" + comboBox2.Text + "']" + "[SZTSZ!='" + textBox1.Text + "']");

                foreach (XmlNode mv_sap in munkavallaloSAP)
                {
                    textBox1.Text = mv_sap.FirstChild.InnerText.ToString();
                    textBox2.Text = mv_sap.LastChild.InnerText;
                }
            }
            else if (textBox3.Text == "Kelet-magyarországi Területi Igazgatóság")
            {
                XmlDocument doc_sap = new XmlDocument();
                doc_sap.Load(@"\\teamweb2\sites\TMEK\manager\Connections\SAP_KMTIG.xml");
                TIG = "SAP_KMTIG";
                XmlNodeList munkavallaloSAP = doc_sap.DocumentElement.SelectNodes("descendant::" + TIG + "[PostaNev='" + comboBox1.Text + "']" + "[DolgozoNev='" + comboBox2.Text + "']" + "[SZTSZ!='" + textBox1.Text + "']");
                
                foreach (XmlNode mv_sap in munkavallaloSAP)
                {
                    textBox1.Text = mv_sap.FirstChild.InnerText.ToString();
                    textBox2.Text = mv_sap.LastChild.InnerText;
                }
            }
            
        }


        private void button3_Click(object sender, EventArgs e)  // Adat hozzáadása
        {
            if (textBox1.Text.Length < 8)
            {
                MessageBox.Show("Hibás SAP azonosító!");
            }
            else
            {

                if (dataGridView1.Rows.Count > 1)   // dupla rögzítés ellenõrzése
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; ++i)
                    {
                        if (dataGridView1.Rows[i].Cells[3].Value.ToString() == textBox1.Text)
                        {
                            MessageBox.Show("Erre a munkavállalóra vonatkozóan már rögzítésre került szankcionálási javaslat!");
                            eredmeny = "";
                            break; // kilépés a ciklusból
                        }
                        else
                        {
                            eredmeny = "OK";
                        }
                    }

                    if (eredmeny == "OK")
                    {
                        dataGridView1.Rows.Add(comboBox1.Text, comboBox2.Text, chkbox1, textBox1.Text, textBox2.Text);
                        checkBox1.Checked = false;
                        comboBox2.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        eredmeny = "";
                    }
                }

                else
                {
                    dataGridView1.Rows.Add(comboBox1.Text, comboBox2.Text, chkbox1, textBox1.Text, textBox2.Text);
                    checkBox1.Checked = false;
                    comboBox2.Text = "";
                    textBox1.Text = "";
                    textBox2.Text = "";
                }

                if (comboBox2.DropDownStyle == ComboBoxStyle.DropDown)
                {
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                chkbox1 = "Igen";
            }
            else
            {
                chkbox1 = "Nem";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Contains("vezetõ"))
            {
                checkBox1.Enabled = true;
            }
            else
            {
                checkBox1.Enabled = false;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            button3.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
        }
        
    }
}