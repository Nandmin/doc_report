using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Report
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public string ComboBoxValue
        {
            get
            {
                return comboBox1.Text;
            }
        }

        public string IktatoValue
        {
            get
            {
                return maskedTextBox1.Text;
            }
        }


        public string Chk1 // k�rvizsg�lati �gyek
        {
            get
            {
                if (checkBox1.Checked == true)
                {
                    string chkbox1 = "1";
                    {
                        return chkbox1.ToString();
                    }
                }
                else
                {
                    string chkbox1 = "0";
                    {
                        return chkbox1.ToString();
                    }
                }
            }
        }

        public string Chk2  // k�vetel�skezel�si �gyek
        {
            get
            {
                if (checkBox2.Checked == true)
                {
                    string chkbox2 = "Igen";
                    {
                        return chkbox2.ToString();
                    }
                }
                else
                {
                    string chkbox2 = "Nem";
                    {
                        return chkbox2.ToString();
                    }
                }
            }
        }

        public string Chk3 // TEO t�j�koztat�s �gyek
        {
            get
            {
                if (checkBox3.Checked == true)
                {
                    string chkbox3 = "1";
                    {
                        return chkbox3.ToString();
                    }
                }
                else
                {
                    string chkbox3 = "0";
                    {
                        return chkbox3.ToString();
                    }
                }
            }
        }

        public string Chk4 // MKCS t�j�koztat�s �gyek
        {
            get
            {
                if (checkBox4.Checked == true)
                {
                    string chkbox4 = "1";
                    {
                        return chkbox4.ToString();
                    }
                }
                else
                {
                    string chkbox4 = "0";
                    {
                        return chkbox4.ToString();
                    }
                }
            }
        }

        public string Chk5 // TEO int�zked�s �gyek
        {
            get
            {
                if (checkBox5.Checked == true)
                {
                    string chkbox5 = "1";
                    {
                        return chkbox5.ToString();
                    }
                }
                else
                {
                    string chkbox5 = "0";
                    {
                        return chkbox5.ToString();
                    }
                }
            }
        }

        public string Chk6 // MKCS int�zked�s �gyek
        {
            get
            {
                if (checkBox6.Checked == true)
                {
                    string chkbox6 = "1";
                    {
                        return chkbox6.ToString();
                    }
                }
                else
                {
                    string chkbox6 = "0";
                    {
                        return chkbox6.ToString();
                    }
                }
            }
        }

        public string Chk7 // VIP �gyek
        {
            get
            {
                if (checkBox7.Checked == true)
                {
                    string chkbox7 = "1";
                    {
                        return chkbox7.ToString();
                    }
                }
                else
                {
                    string chkbox7 = "0";
                    {
                        return chkbox7.ToString();
                    }
                }
            }
        }

        public string Chk8 // Befszolg �gyek
        {
            get
            {
                if (checkBox8.Checked == true)
                {
                    string chkbox8 = "1";
                    {
                        return chkbox8.ToString();
                    }
                }
                else
                {
                    string chkbox8 = "0";
                    {
                        return chkbox8.ToString();
                    }
                }
            }
        }

        public string Chk9 // Csoport1 t�j�koztat�s
        {
            get
            {
                if (checkBox9.Checked == true)
                {
                    string chkbox9 = "Csoport1;";
                    {
                        return chkbox9.ToString();
                    }
                }
                else
                {
                    string chkbox9 = "";
                    {
                        return chkbox9.ToString();
                    }
                }
            }
        }

        public string Chk10 // Csoport2 t�j�koztat�s
        {
            get
            {
                if (checkBox10.Checked == true)
                {
                    string chkbox10 = "Csoport2;";
                    {
                        return chkbox10.ToString();
                    }
                }
                else
                {
                    string chkbox10 = "";
                    {
                        return chkbox10.ToString();
                    }
                }
            }
        }

        public string Chk11 // Csoport3 t�j�koztat�s
        {
            get
            {
                if (checkBox11.Checked == true)
                {
                    string chkbox11 = "Csoport3;";
                    {
                        return chkbox11.ToString();
                    }
                }
                else
                {
                    string chkbox11 = "";
                    {
                        return chkbox11.ToString();
                    }
                }
            }
        }

        public string Chk12 // Csoport4 t�j�koztat�s
        {
            get
            {
                if (checkBox12.Checked == true)
                {
                    string chkbox12 = "Csoport4;";
                    {
                        return chkbox12.ToString();
                    }
                }
                else
                {
                    string chkbox12 = "";
                    {
                        return chkbox12.ToString();
                    }
                }
            }
        }

        public string Chk13 // Csoport5 t�j�koztat�s
        {
            get
            {
                if (checkBox13.Checked == true)
                {
                    string chkbox13 = "Csoport5;";
                    {
                        return chkbox13.ToString();
                    }
                }
                else
                {
                    string chkbox13 = "";
                    {
                        return chkbox13.ToString();
                    }
                }
            }
        }

        public string Chk14// Csoport6 t�j�koztat�s
        {
            get
            {
                if (checkBox14.Checked == true)
                {
                    string chkbox14 = "Csoport6;";
                    {
                        return chkbox14.ToString();
                    }
                }
                else
                {
                    string chkbox14 = "";
                    {
                        return chkbox14.ToString();
                    }
                }
            }
        }

        public string Sikitas // Vissza�l�s �sszege
        {
            get
            {
                //int ertek = Convert.ToInt32(textBox2.Text); // sz�veg form�tum� sz�m sz�m form�tumm� konvert�l�sa (a public string helyett int kell)
                return maskedTextBox2.Text.Trim(); //ertek;    
            }
        }

        public string Karertek
        {
            get
            {
                return maskedTextBox3.Text.Trim();
            }
        }

        public string KapcsolatosUgyintezo
        {
            get
            {
                return comboBox2.Text;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            
            if (iData.GetDataPresent(DataFormats.Text))
            {
                string bejovo_adat = (String)iData.GetData(DataFormats.Text);
                string bejovo_TMECS = bejovo_adat.Substring(bejovo_adat.LastIndexOf("/"), 10);
                if (bejovo_adat.ToString().Length != 0)// == "Igen")
                {
                    //l�ssuk az eredm�nyt, mi ment �t 
                    string ugy_tipus = bejovo_adat.Substring(0, bejovo_adat.IndexOf("/"));
                    string sikitas = bejovo_adat.Substring(bejovo_adat.IndexOf("/")+1, (bejovo_adat.LastIndexOf("/") - bejovo_adat.IndexOf("/"))-1);
                    string visszaeles = sikitas.Substring(0, sikitas.IndexOf(","));
                    string karosszeg = sikitas.Remove(0, sikitas.IndexOf(",")+1);
                    

                    if (ugy_tipus != "Panasz vizsg�lat" || bejovo_TMECS.Contains("Csoport4") || bejovo_TMECS.Contains("Csoport5") 
                        || bejovo_TMECS.Contains("Csoport6")) //bejovo_adat.Contains("Csoport4") || bejovo_adat.Contains("Csoport5") 
                        //|| bejovo_adat.Contains("Csoport6"))  // VIP nem v�laszthat�, ha nem panaszr�l besz�l�nk
                        {
                            checkBox7.Enabled = false; //VIP jel�l�s
                        }

                        if (bejovo_TMECS.Contains("Csoport4") || bejovo_TMECS.Contains("Csoport5") || bejovo_TMECS.Contains("Csoport6")) //bejovo_adat.Contains("Csoport4") || bejovo_adat.Contains("Csoport5")
                            //|| bejovo_adat.Contains("Csoport6"))  // VIP �s BefSzolg nem v�laszthat�, ha nem panaszr�l besz�l�nk
                        {
                            checkBox8.Enabled = false; //BefSzolg jel�l�s
                        }  

                    //textBox1.Text = bejovo_adat.Substring(bejovo_adat.LastIndexOf(",1")+1, 7);
                    //maskedTextBox1.Text = bejovo_adat.Substring(bejovo_adat.LastIndexOf(",1") + 1, 7); //eredeti
                    maskedTextBox1.Text = bejovo_adat.Substring(bejovo_adat.LastIndexOf(",1") + 1, (bejovo_adat.LastIndexOf(",") - bejovo_adat.LastIndexOf(",1")));
                    textBox1.Text = bejovo_adat.ToString();
                    maskedTextBox2.Text = visszaeles;
                    maskedTextBox3.Text = karosszeg;
                    comboBox2.Text = bejovo_adat.Substring(bejovo_adat.LastIndexOf(",")+1, (bejovo_adat.LastIndexOf("!") - bejovo_adat.LastIndexOf(",")-1));
                        if (bejovo_adat.Contains("Hi�nylatIgen"))
                            {
                            checkBox2.Checked = true;
                            }
                            else
                            {
                                checkBox2.Checked = false;
                            }

                            if (bejovo_adat.Contains("KPCS1"))
                            {
                                checkBox1.Checked = true;
                            }
                            else
                            {
                                checkBox1.Checked = false;
                            }

                            if (bejovo_adat.Contains("TEO_I1"))
                            {
                                checkBox5.Checked = true;
                            }
                            else
                            {
                                checkBox5.Checked = false;
                            }

                            if (bejovo_adat.Contains("TEO_T1"))
                            {
                                checkBox3.Checked = true;
                            }
                            else
                            {
                                checkBox3.Checked = false;
                            }

                            if (bejovo_adat.Contains("MKCS_I1"))
                            {
                                checkBox6.Checked = true;
                            }
                            else
                            {
                                checkBox6.Checked = false;
                            }

                            if (bejovo_adat.Contains("MKCS_T1"))
                            {
                                checkBox4.Checked = true;
                            }
                            else
                            {
                                checkBox4.Checked = false;
                            }

                            if ((bejovo_adat.Contains("K�zpontvezet�") && bejovo_TMECS.Contains("Csoport1")) ||
                                (bejovo_adat.Contains("K�zpontvezet�") && bejovo_TMECS.Contains("Csoport2"))||
                                (bejovo_adat.Contains("K�zpontvezet�") && bejovo_TMECS.Contains("Csoport3")))
                            {
                                comboBox1.Text = "K�zpontvezet�";
                            }

                            if ((bejovo_adat.Contains("K�zpontvezet�") && bejovo_TMECS.Contains("Csoport4")) ||
                                (bejovo_adat.Contains("K�zpontvezet�") && bejovo_TMECS.Contains("Csoport5")) ||
                                (bejovo_adat.Contains("K�zpontvezet�") && bejovo_TMECS.Contains("Csoport6")))
                            {
                                comboBox1.Text = "Ter�leti igazgat�";
                            }

                            if (bejovo_adat.Contains("Oszt�lyvezet�"))
                            {
                                comboBox1.Text = "Oszt�lyvezet�";
                            }

                            if (bejovo_adat.Contains("Csoportvezet�"))
                            {
                                comboBox1.Text = "Csoportvezet�";
                            }
                            
                            if (bejovo_adat.Contains("Csoport1;"))
                            {
                                checkBox9.Checked = true;
                            }
                            else
                            {
                                checkBox9.Checked = false;
                            }

                            if (bejovo_adat.Contains("Csoport2;"))
                            {
                                checkBox10.Checked = true;
                            }
                            else
                            {
                                checkBox10.Checked = false;
                            }
                                
                            if (bejovo_adat.Contains("Csoport3;"))
                            {
                                checkBox11.Checked = true;
                            }
                            else
                            {
                                checkBox11.Checked = false;
                            }
                                
                            if (bejovo_adat.Contains("Csoport4;"))
                            {
                                checkBox12.Checked = true;
                            }
                            else
                            {
                                checkBox12.Checked = false;
                            }
                            
                            if (bejovo_adat.Contains("Csoport5;"))
                            {
                                checkBox13.Checked = true;
                            }
                            else
                            {
                                checkBox13.Checked = false;
                            }
                            
                            if (bejovo_adat.Contains("Csoport6;"))
                            {
                                checkBox14.Checked = true;
                            }
                            else
                            {
                                checkBox14.Checked = false;
                            }

                            if (bejovo_adat.Contains("Befszolg1"))
                            {
                                checkBox8.Checked = true;
                            }
                            else
                            {
                                checkBox8.Checked = false;
                            }

                            if (bejovo_adat.Contains("VIP1"))
                            {
                                checkBox7.Checked = true;
                            }
                            else
                            {
                                checkBox7.Checked = false;
                            }
                }
            }
        }


        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{
           
        //}
        
        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            int chk = 1200000;
            if(Convert.ToInt32(maskedTextBox1.Text.Substring(0,6)) < Convert.ToInt32(chk.ToString().Substring(0,6)))
            {
                MessageBox.Show("Hib�s iktat�sz�m!" + "\n" + "\n" + "K�rem, hogy a helyes iktat�sz�mot megadni sz�veskedj!");
                e.Cancel = true;
            }
        }
        
        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            string bejovo_adat = textBox1.Text;
            string bejovo_TMECS = bejovo_adat.Substring(bejovo_adat.LastIndexOf("/"), 10);
            if ((comboBox1.Text == "Ter�leti igazgat�" && bejovo_TMECS.Contains("Csoport1")) || // bejovo_adat.Contains("Csoport1")) ||
                //(comboBox1.Text == "Csoportvezet�" && bejovo_TMECS.Contains("Csoport1")) ||
                //(comboBox1.Text == "Csoportvezet�" && bejovo_TMECS.Contains("Csoport2")) || 
                (comboBox1.Text == "Ter�leti igazgat�" && bejovo_TMECS.Contains("Csoport2")) || //bejovo_adat.Contains("Csoport2")) ||
                //(comboBox1.Text == "Csoportvezet�" && bejovo_TMECS.Contains("Csoport3")) ||
                //(comboBox1.Text == "Csoportvezet�" && bejovo_TMECS.Contains("MKCS")) ||
                (comboBox1.Text == "Ter�leti igazgat�" && bejovo_TMECS.Contains("Csoport3"))) // bejovo_adat.Contains("Csoport3")))
                {
                    MessageBox.Show("Ez az �rt�k itt nem v�laszthat�!" + "\n" + "\n" + "K�rem, hogy m�sik jelent�s c�mzettet megadni sz�veskedj!");
                    e.Cancel = true;
                }

                if ((comboBox1.Text == "K�zpontvezet�" && bejovo_TMECS.Contains("Csoport4")) || //bejovo_adat.Contains("Csoport4")) ||
                    (comboBox1.Text == "K�zpontvezet�" && bejovo_TMECS.Contains("Csoport5")) || //bejovo_adat.Contains("Csoport5")) ||
                    (comboBox1.Text == "K�zpontvezet�" && bejovo_TMECS.Contains("Csoport6")) || //bejovo_adat.Contains("Csoport6")) ||
                    (comboBox1.Text == "Oszt�lyvezet�" && bejovo_TMECS.Contains("Csoport4")) || //bejovo_adat.Contains("Csoport4")) ||
                    (comboBox1.Text == "Oszt�lyvezet�" && bejovo_TMECS.Contains("Csoport5")) || //bejovo_adat.Contains("Csoport5")) ||
                    (comboBox1.Text == "Oszt�lyvezet�" && bejovo_TMECS.Contains("Csoport6"))) //bejovo_adat.Contains("Csoport6")))
                {
                    MessageBox.Show("Ez az �rt�k itt nem v�laszthat�!" + "\n" + "\n" + "K�rem, hogy m�sik jelent�s c�mzettet megadni sz�veskedj!");
                    e.Cancel = true;
                }
        }

        private void maskedTextBox2_Validating(object sender, CancelEventArgs e)
        {
            if (maskedTextBox2.Text.Contains(" "))
            {
                MessageBox.Show("A mez� nem megengedett karaktert tartalmaz!");
                e.Cancel = true;
            }
        }

        private void maskedTextBox3_Validating(object sender, CancelEventArgs e)
        {
            if (maskedTextBox3.Text.Contains(" "))
            {
                MessageBox.Show("A mez� nem megengedett karaktert tartalmaz!");
                e.Cancel = true;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(checkBox2.CheckState.ToString());
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox6.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            { 
                checkBox5.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox3.Checked = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox4.Checked = false;
            }
        }

        //private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        
    }
}