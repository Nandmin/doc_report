using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Report
{
    public partial class Feladatlap_fast : Form
    {
        public int i;
        public int j;
        public int k;
        public int l;
        public int m;
        public int n;
        public int p;
        public int r;
        public int s;
        public int t;
        public int u;
        public string eredmeny;
        public XmlDocument doc;         // Postalista elérhetõsége
        public XmlNodeList postalista;  // postalista változó
        public string TIG;
        public static string beadvanyos;    // az átalakított beadványos értéke, amit majd az InfoPath-nak küld
        
        public string ugyintezo;
        public string ugytipus;

        public string[] posta_neve;
        public string[] posta_szeaz;
        public string[] posta_TIG;
        public string[] posta_besorolas;



        public Feladatlap_fast()
        {
            InitializeComponent();
            
        }

        private void Feladatlap_fast_Load(object sender, EventArgs e)
        {
            if (checkBox5.Checked == false)
            {
                groupBox10.Visible = false;
                groupBox11.Location = new Point(8, 23);
            }
            
            panasz_tabla_feltoltese();

            //TabControl1.TabPages(1).Enabled = False
            //tabControl1.TabPages.Remove(tabPage2);

            textBox1.Text = FormCode.csoport;
            comboBox2.Text = FormCode.ugyintezo;

            foreach (object nevsor in FormCode.UserLista)
            {
                comboBox1.Items.Add(nevsor);
            }

            foreach (object panaszok in FormCode.Panaszlista)
            {
                comboBox2.Items.Add(panaszok);
            }

            foreach (object targy_lista in FormCode.targy_data)
            {
                comboBox5.Items.Add(targy_lista.ToString());
            }
            

            if (textBox1.Text == "EVO")
            {
                textBox1.Text = "Szolgáltató és Operatív Egységek Ellenõrzési Osztály";
                TIG = "Igazgatóság";
            }
            else if (textBox1.Text == "Csoport1")
            {
                textBox1.Text = "Központi Ellenõrzési és Vizsgálati Csoport";
                TIG = "Központi Területi Igazgatóság";
            }
            else if (textBox1.Text == "Csoport2")
            {
                textBox1.Text = "Nyugat-magyarországi Ellenõrzési és Vizsgálati Csoport";
                TIG = "Nyugat-magyarországi Területi Igazgatóság";
            }
            else if (textBox1.Text == "Csoport3")
            {
                textBox1.Text = "Kelet-magyarországi Ellenõrzési és Vizsgálati Csoport";
                TIG = "Kelet-magyarországi Területi Igazgatóság";
            }
            else if (textBox1.Text == "MKCS")
            {
                textBox1.Text = "Módszertani és Kiemelt Ügyek Ellenõrzési Csoport";
                TIG = "Igazgatóság";
            }

            postalista_aktivalas();
            datagridcombo_betoltes();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (DataGridViewRow cimzettek in dataGridView1.Rows)//[i].Cells[0])
                {
                    //MessageBox.Show(cimzettek.ToString());
                    if (comboBox2.Text == cimzettek.Cells[0].Value.ToString())
                    {
                        textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();                                            // Panasz címzett
                        textBox15.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();                                           // Panasz tárgya
                        textBox16.Text = Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value.ToString()).ToShortDateString();   // Bejelentés dátum
                        textBox17.Text = Convert.ToDateTime(dataGridView1.Rows[i].Cells[4].Value.ToString()).ToShortDateString();   // ÜPR date
                        textBox18.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();                                           // Anonim panasz
                        textBox19.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();                                           // Felvevõ posta
                        textBox20.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();                                           // Határidõ
                        textBox21.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();                                           // Panasz leírása
                        comboBox4.Text = "Helyszíni vizsgálat";
                        comboBox6.Text = "Panasz vizsgálat";
                        comboBox7.Text = "Ügyfélszolgálati Igazgatóság";
                        textBox9.Text = comboBox2.Text;
                        dateTimePicker3.Text = textBox20.Text;
                        
                        //dgrid2_torlese();
                        ugyfeladatok_tabla_feltoltese();
                        termektabla_feltoltese();

                        if (textBox18.Text == "Igen")
                        {
                            comboBox5.Text = "Anonim panasz";
                        }
                        else
                        {
                            try
                            {
                                if (dataGridView2.Rows[0].Cells[0].Value.ToString().Length > 0)
                                {
                                    comboBox5.Text = dataGridView2.Rows[0].Cells[0].Value.ToString() + " panasza";
                                }
                                else
                                {
                                    comboBox5.Text = "Anonim panasz";
                                }
                            }
                            catch
                            {
                                comboBox5.Text = "Anonim panasz";
                            }
                        }

                    }
                    if (i < dataGridView1.Rows.Count - 1)
                    {
                        ++i;
                    }
                }

                if (comboBox2.Text.Contains("Igazgatóság"))
                {

                }
                else
                {
                    comboBox3.Text = textBox19.Text;
                }

                
            }
            catch
            {

            }
            
        }

        private void panasz_tabla_feltoltese()
        {
            i = 0;
            j = 0;
            k = 0;
            l = 0;
            m = 0;
            n = 0;
            p = 0;
            r = 0;
            
            //List<string> P_pid = new List<string>();

            foreach (object panaszid in FormCode.PID)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = panaszid;
                //P_pid.Add(panaszid.ToString());
            }

            label28.Text = dataGridView1.Rows.Count.ToString();

            foreach (object cimzett in FormCode.P_cimzett)
            {
                dataGridView1.Rows[i].Cells[1].Value = cimzett;
                ++i;
            }

            foreach (object p_targy in FormCode.P_targy)
            {
                dataGridView1.Rows[j].Cells[2].Value = p_targy;
                ++j;
            }

            foreach (object p_leir in FormCode.P_leiras)
            {
                dataGridView1.Rows[l].Cells[6].Value = p_leir;
                ++l;
            }


            foreach (object be_datum in FormCode.P_bejelent_datum)
            {
                dataGridView1.Rows[k].Cells[3].Value = be_datum;
                ++k;
            }

                foreach (object hatarido in FormCode.P_hatarido)
                {
                    dataGridView1.Rows[m].Cells[7].Value = Convert.ToDateTime(hatarido.ToString()).AddDays(-2).ToShortDateString();

                    foreach (object datum_chk in FormCode.hetvege)
                    {
                        if (Convert.ToDateTime(datum_chk.ToString()).ToShortDateString() == dataGridView1.Rows[m].Cells[7].Value.ToString())
                        {
                            dataGridView1.Rows[m].Cells[7].Value = Convert.ToDateTime(dataGridView1.Rows[m].Cells[7].Value.ToString()).AddDays(-2).ToShortDateString();
                        }
                    }

                    ++m;
                }

            foreach (object uprDate in FormCode.P_UPR_datum)
            {
                dataGridView1.Rows[n].Cells[4].Value = uprDate;
                ++n;
            }

            foreach (object anonim in FormCode.P_anonim)
            {
                dataGridView1.Rows[p].Cells[5].Value = anonim;
                ++p;
            }

            foreach (object felvPosta in FormCode.P_posta)
            {
                dataGridView1.Rows[r].Cells[8].Value = felvPosta;
                ++r;
            }

            
        }

        private void ugyfeladatok_tabla_feltoltese()
        {
            dgrid2_torlese();
            
            s = 0;
            t = 0;
            u = 0;

            foreach (object ugyel_neve in FormCode.P_ugyfel_neve)
            {
                int index2 = dataGridView2.Rows.Add();
                dataGridView2.Rows[index2].Cells[0].Value = ugyel_neve;
            }

            foreach (object ugyfel_cime in FormCode.P_ugyfel_cime)
            {
                dataGridView2.Rows[s].Cells[1].Value = ugyfel_cime;
                ++s;
            }

            foreach (object ugyel_tipusa in FormCode.P_ugyfel_tipus)
            {
                dataGridView2.Rows[t].Cells[2].Value = ugyel_tipusa;
                ++t;
            }

            foreach (object pid_ugyfel in FormCode.P_PID_ugyfel)
            {
                dataGridView2.Rows[u].Cells[3].Value = pid_ugyfel;
                ++u;
            }

            ////PID_ugyfel grid2-ben a P számokat tartalmazza, nem látható módban

            try
            {
                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    if (dataGridView2.Rows[i].Cells[3].Value.ToString() != comboBox2.Text)
                    {
                        dataGridView2.Rows.RemoveAt(i);
                        --i;
                    }
                }
            }
            catch
            {
            }
        }

        private void termektabla_feltoltese()
        {
            i = 0;
            j = 0;
            k = 0;
            l = 0;
            m = 0;
            n = 0;
            p = 0;
            r = 0;

            foreach (object termekPID in FormCode.T_PID)
            {
                int index3 = dataGridView3.Rows.Add();
                dataGridView3.Rows[index3].Cells[0].Value = termekPID;
            }

            foreach (object termekID in FormCode.T_termekid)
            {
                dataGridView3.Rows[i].Cells[1].Value = termekID;
                ++i;
            }

            foreach (object t_termek in FormCode.T_termek)
            {
                dataGridView3.Rows[j].Cells[2].Value = t_termek;
                ++j;
            }

            foreach (object felvposta in FormCode.T_felvevo)
            {
                dataGridView3.Rows[k].Cells[3].Value = felvposta;
                ++k;
            }

            foreach (object rendposta in FormCode.T_rendposta)
            {
                dataGridView3.Rows[l].Cells[4].Value = rendposta;
                ++l;
            }

            foreach (object suly in FormCode.T_suly)
            {
                dataGridView3.Rows[n].Cells[6].Value = suly;
                ++n;
            }

            foreach (object f_dij in FormCode.T_dij)
            {
                dataGridView3.Rows[p].Cells[7].Value = f_dij;
                ++p;
            }

            foreach (object kszolgok in FormCode.T_kszolg)
            {
                dataGridView3.Rows[r].Cells[8].Value = kszolgok;
                ++r;
            }

            try
            {
                foreach (object feladas_datum in FormCode.T_felvdate)
                {
                    //MessageBox.Show(feladas_datum.ToString());
                    if (feladas_datum.ToString().Length > 0)
                    {
                        dataGridView3.Rows[m].Cells[5].Value = Convert.ToDateTime(feladas_datum.ToString()).ToShortDateString();
                        ++m;
                    }
                    else
                    {
                        ++m;
                    }
                    
                }
            }
            catch
            {
                //MessageBox.Show("hiba: " + m);
            }

            try
            {
                for (int x = 0; x < dataGridView3.Rows.Count; ++x)
                {
                    if (dataGridView3.Rows[x].Cells[0].Value.ToString() != comboBox2.Text)
                    {
                        dataGridView3.Rows.RemoveAt(x);
                        --x;
                    }
                }
            }
            catch
            {

            }

        }


        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            if (textBox18.Text == "false")
            {
                textBox18.Text = "Nem";
            }
            else if (textBox18.Text == "true")
            {
                textBox18.Text = "Igen";
            }
        }

        private void postalista_aktivalas()
        {

            XmlDocument doc = new XmlDocument(); // Ez máûködik, 
            doc.Load(@"\\teamweb2\sites\TMEK\manager\Connections\Postalista.xml");

            //XmlNodeList postalista;
            XmlNode root = doc.DocumentElement;
            postalista = root.SelectNodes("descendant::Postalista[contains(Igazgatóság, '" + TIG + "')]"); //'Igazgatóság')]");//Nyugat-magyarországi Területi Igazgatóság']");

            posta_neve = new string[postalista.Count];
            posta_besorolas = new string[postalista.Count];
            posta_TIG = new string[postalista.Count];

            int i = 0;
            foreach (XmlNode posta in postalista)
            {
                comboBox3.Items.Add(posta.FirstChild.InnerText);// NextSibling.InnerText.ToString());
                posta_neve[i] = posta.FirstChild.InnerText;
                posta_besorolas[i] = posta.FirstChild.NextSibling.NextSibling.InnerText;
                posta_TIG[i] = posta.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                ++i;
            }

            //foreach (object posta in FormCode.postaadatok)
            //{
            //    comboBox3.Items.Add(posta.ToString());
            //}
            
        }

        public void dgrid2_torlese()        // datagrid2 minden adatát törli
        {
            for (int j = 0; j < dataGridView2.Rows.Count; ++j)
            {
                    dataGridView2.Rows.RemoveAt(j);
                    --j;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                Clipboard.Clear();
                Clipboard.SetText(comboBox2.Text);
                Form1 form = new Form1();
                form.ShowDialog();
            }
            else
            {
                DialogResult drF01 = MessageBox.Show("Nincs kiválasztott panasz, így mellékleteket sem lehet megtekinteni!", "Adathiány!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            // tabControl1.TabPages.Add(tabPage2);  // ------------- TABPAGE HOZZÁADÁSA ------------
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < posta_neve.Length; ++i)
            {
                if (posta_neve[i].ToString() == comboBox3.Text)
                {
                    textBox4.Text = posta_TIG[i].ToString();
                    textBox5.Text = posta_besorolas[i].ToString();
                }

            }
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"\\teamweb2\sites\TMEK\manager\Connections\Postalista.xml");

            //XmlNode root = doc.DocumentElement;
            //postalista = root.SelectNodes("descendant::Postalista[contains(Igazgatóság, '" + TIG + "')]"); //'Igazgatóság')]");//Nyugat-magyarországi Területi Igazgatóság']");

            //foreach (XmlNode ter_ig in postalista)
            //{
            //    //MessageBox.Show(ter_ig.FirstChild);
            //    if (ter_ig.FirstChild.InnerText == comboBox3.Text)
            //    {
            //        textBox4.Text = ter_ig.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
            //        textBox5.Text = ter_ig.FirstChild.NextSibling.NextSibling.InnerText;
            //    }
            //}

           
        }


        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comboBeadvanyos = comboBox7.Text;

            switch (comboBeadvanyos)
            {
                case "SZMEO - TECS":
                    beadvanyos = "TECS";
                    break;
                case "Területi Igazgatóság":
                    beadvanyos = "TIG";
                    break;
                case "Hálózati Igazgatóság":
                    beadvanyos = "HIg";
                    break;
                case "EVO - EVCS":
                    beadvanyos = "TMECS";
                    break;
                case "Hatóság":
                    beadvanyos = "HATOSAG";
                    break;
                case "Hiánylat":
                    beadvanyos = "HIANYLAT";
                    break;
                case "Kártérítés":
                    beadvanyos = "KARTERITES";
                    break;
                case "Jogi személy":
                    beadvanyos = "JOGI";
                    break;
                case "Magánfél":
                    beadvanyos = "MAGAN";
                    break;
                case "Nyugdíjfolyósító Fõigazgatóság":
                    beadvanyos = "NYUFIG";
                    break;
                case "Posta Elszámoló Központ":
                    beadvanyos = "PEK";
                    break;
                case "Posta":
                    beadvanyos = "POSTA";
                    break;
                case "Ügyfélszolgálati Igazgatóság":
                    beadvanyos = "KÜSZI";
                    break;
                case "Egyéb":
                    beadvanyos = "EGYEB";
                    break;
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            foreach (object datum_chk in FormCode.hetvege)
            {
                if (Convert.ToDateTime(datum_chk.ToString()).ToShortDateString() == dateTimePicker3.Text)
                {
                    dateTimePicker3.Text = Convert.ToDateTime(dateTimePicker3.Text).AddDays(-1).ToShortDateString();
                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)      // ----------------- ---- EZT MAJD VISSZA KELL KACCCSOLNI!!!!!!! --------------------------- \\
        {
            //if (comboBox1.Text != "Németh András")
            //{
            //    tabControl1.TabPages.Remove(tabPage2);
            //    MessageBox.Show("A kiosztás még nem fejezõdött be, emiatt a nézetváltás nem lehetséges!");
            //}
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = Form3.GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName).ToString();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            string state = "Zárva";
            
            if (checkBox5.Checked == true)
            {
                if (state == "Zárva" && groupBox10.Location.Y < 100)
                {
                    DialogResult dr_fl02 = MessageBox.Show("Jelen módosítás, csak a vizsgálat kiosztásától számított 30 napot meghaladó új vizsgálati határidõ kérése esetén használható!\n\nFolytassuk?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr_fl02 == DialogResult.Yes)
                    {
                        for (int i = 23; i <= 285; i++)  // Ez lejjebb csúsztatja az iktatószámos részt
                        {
                            groupBox11.Location = new Point(8, i);
                            if (i == 23)
                            {
                                groupBox10.Visible = true;
                            }
                            System.Threading.Thread.Sleep(25);
                            ++i;
                        }

                        state = "Nyitva";
                    }
                    else if (dr_fl02 == DialogResult.No)  //Rájött, hogy 30 napnál rövidebb új határidõt kér...
                    {
                        checkBox5.Checked = false;
                        state = "Zárva";
                    }
                }
            }
            else  // HA késõbb jött rá, hogy mégse 30 napon túli kérése lesz...
            {
                if (state == "Zárva" && checkBox5.Checked == false && checkBox5.Location.Y < 100)
                {
                    for (int i = 285; i > 22; i--)  // Ez feljebb csúsztatja az iktatószámos részt
                    {
                        groupBox11.Location = new Point(8, i);
                        System.Threading.Thread.Sleep(25);
                    }

                    groupBox10.Visible = false;
                }
            }
        }

        public void datagridcombo_betoltes()
        {
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();

            foreach (object posta in FormCode.postaadatok) // posta_neve)
            {
                cmb.Items.Add(posta.ToString());
            }
            

            cmb.HeaderText = "Posta";
            cmb.MaxDropDownItems = 25;
            cmb.Width = 250;
            dataGridView4.Columns.Add(cmb);
        }

        private void btn_f6_bezar_Click(object sender, EventArgs e)
        {
            if (comboBox6.Text == "Panasz vizsgálat")
            {
                if (FormCode.PID_bejovo == null)
                {
                    FormCode.ugyintezo = comboBox1.Text;
                    FormCode.ugytipus = comboBox6.Text;
                    FormCode.PID_bejovo = comboBox2.Text;
                    FormCode.posta = comboBox3.Text;
                }
            }
            else
            {
                FormCode.ugyintezo = comboBox1.Text;
                FormCode.ugytipus = comboBox6.Text;
                FormCode.posta = comboBox3.Text;
                FormCode.targy = comboBox5.Text;
                FormCode.Vizsgalti_hatarido = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            }
            this.Close();

        }

        
    }
}