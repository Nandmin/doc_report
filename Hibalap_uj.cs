using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Xml.Serialization;
using System.IO;

namespace Report
{
    public partial class Hibalap_uj : Form
    {
        public Hibalap_uj()
        {
            InitializeComponent();
        }
        
        public string indokolva_Hibalap = "1";
        private static string tevekenyseg_Hibalap;
        public static string indoklas_Hibalap;
        public static string indoklas_kod_Hibalap;
        public static string datechange_Hibalap = "0"; // 0 ha a dátumhoz nem nyúlt, 1 ha megváltoztatta
        public static string kiosztva_Hibalap;
        public static string iktatoszam_Hibalap;
        public static string Temp_Hibalap; // átküldött adatok
        //public static string ugytipus;

        public static string GetUserFullName(string domain, string userName)
        {
            DirectoryEntry userEntry = new DirectoryEntry("WinNT://" + domain + "/" + userName + ",User");
            return (string)userEntry.Properties["fullname"].Value;
        }


        public DateTime Bekuldesi_hatarido_Hibalap
        {
            get
            {
                if (datechange_Hibalap.ToString() == "1")
                {
                    if (Convert.ToInt32(dateTimePicker2.Text.Substring(0, 1)) > 0)
                    {
                        return Convert.ToDateTime(dateTimePicker1.Value.ToString().Substring(0, 11) + " " + dateTimePicker2.Text);
                    }
                    else
                    {
                        return Convert.ToDateTime(dateTimePicker1.Value.ToString().Substring(0, 11) + " " + dateTimePicker2.Text.ToString());
                    }
                }
                else
                {
                    return Convert.ToDateTime("1980.01.01.");
                }
            }
        }

        public string Visszakuldes_oka_Hibalap
        {
            get
            {
                //return indoklas;
                return indoklas_kod_Hibalap;
            }
        }

        //public string Visszakuldesi_ok_szoveggel
        //{
        //    get
        //    {
        //        return indoklas_Hibalap;
        //    }
        //}


        public string KiegVissza_Hibalap
        {
            get
            {
                return "0";
            }
        }

        public string Tullepes_Hibalap
        {
            get
            {
                return textBox3.Text;
            }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateTimePicker1.Text) < DateTime.Now.Date)
            {
                MessageBox.Show("Az ügy beküldési határideje nem lehet kisebb, mint a mai nap!\n\nKérem, hogy a helyes értéket megadni szíveskedj!", "Adatrögzítési hiba!");
                dateTimePicker1.Text = DateTime.Now.Date.ToString();
            }

            if (Convert.ToDateTime(dateTimePicker1.Text) != DateTime.Now.Date)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
            }

            datechange_Hibalap = "1";
            button1.Visible = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            datechange_Hibalap = "1";
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)      // Bezár gomb
        {
            if (datechange_Hibalap.ToString() == "1" && textBox1.Text.Length == 0)
            {
                tevekenyseg_Hibalap = "Felülvizsgálat";
                chkbox_chk(sender, e);
                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), tevekenyseg_Hibalap.ToString(), indoklas_Hibalap.ToString(), dateTimePicker1.Value.ToShortDateString().Substring(0, 11) + dateTimePicker2.Text); //, comboBox1.Text, textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
            }

            else if (datechange_Hibalap.ToString() == "0" && textBox1.Text.Length > 0 && Temp_Hibalap.Contains("Nem"))
            {
                tevekenyseg_Hibalap = "Válaszadás (Ügyintézõ)";
                indoklas_Hibalap = textBox1.Text;
                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), tevekenyseg_Hibalap.ToString(), indoklas_Hibalap.ToString());//, dateTimePicker1.Value.ToString().Substring(0, 11)); //, comboBox1.Text, textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
                indoklas_Hibalap = "";
                indokolva_Hibalap = "0";
            }
            else if (textBox1.Text.Length > 0 && Temp_Hibalap.Contains("Igen"))
            {
                tevekenyseg_Hibalap = "Felülvizsgálat (Indoklás)";
                indoklas_Hibalap = textBox1.Text;
                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), tevekenyseg_Hibalap.ToString(), indoklas_Hibalap.ToString());// dateTimePicker1.Value.ToString().Substring(0, 11) + dateTimePicker2.Text); //, comboBox1.Text, textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
                indoklas_Hibalap = "";
            }

            dataGridView1.AllowUserToAddRows = false;
            int lastRow = dataGridView1.Rows.Count;

            //if (dataGridView1.Rows[lastRow-1].Cells[1].Value != null)   // a rowindex értéke mindig 1-el kisebb a valóságnál, mert 0-ról indul a számozás
            if (lastRow >= 1)
            {
                string Year = kiosztva_Hibalap.Substring(0, 4);
                string Month = kiosztva_Hibalap.Substring(5, 2);
                string mentes_helye = @"\\teamweb2\sites\TMEK\Manager\Manager_Ftemp\" + Year + "\\" + Month + "\\";
                //string mentes_helye = @"C:\Teszt_adat\" + Year + "\\" + Month + "\\"; // TESZT IDEJÉRE!!!!

                try
                {
                    if (!System.IO.Directory.Exists(mentes_helye))
                    {
                        System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(mentes_helye);
                    }

                    List<List<string>> data = new List<List<string>>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        List<string> rowData = new List<string>();
                        foreach (DataGridViewCell cell in row.Cells)
                            rowData.Add(cell.FormattedValue.ToString());
                        data.Add(rowData);
                    }

                    XmlSerializer xs = new XmlSerializer(data.GetType());
                    using (TextWriter tw = new StreamWriter(@mentes_helye + iktatoszam_Hibalap + ".xml"))
                    {
                        xs.Serialize(tw, data);
                        tw.Close();
                    }
                }
                catch
                {
                }
            }
           
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = "08:00:00";
            datechange_Hibalap = "0";

            if (FormCode.kiosztva >= Convert.ToDateTime("2020.01.01"))
            {
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPage4);
                //checkBox6.Visible = false;
                //checkBox5.Text = "tst";
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage5);
                tabControl1.TabPages.Remove(tabPage6);
                tabControl1.TabPages.Remove(tabPage7);
                tabControl1.TabPages.Remove(tabPage8);
            }

            
            if (GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName).Contains("Németh András"))
            {
            }
            else
            {
                button2.Visible = false;
            }
            try
            {
                IDataObject iData_Hibalap = Clipboard.GetDataObject();
                if (iData_Hibalap.GetDataPresent(DataFormats.Text))
                {
                    Temp_Hibalap = (String)iData_Hibalap.GetData(DataFormats.Text);
                    if (Temp_Hibalap.ToString().Contains("Nem"))
                    {
                        dateTimePicker1.Enabled = false;
                        dateTimePicker2.Enabled = false;
                        checkBox1.Enabled = false;
                        checkBox2.Enabled = false;
                        checkBox3.Enabled = false;
                        checkBox4.Enabled = false;
                        checkBox5.Enabled = false;
                        checkBox6.Enabled = false;
                        checkBox7.Enabled = false;
                        checkBox8.Enabled = false;
                        checkBox9.Enabled = false;
                        checkBox10.Enabled = false;
                        checkBox11.Enabled = false;
                        checkBox12.Enabled = false;
                        checkBox13.Enabled = false;
                        checkBox14.Enabled = false;
                        checkBox15.Enabled = false;
                        checkBox16.Enabled = false;
                        checkBox17.Enabled = false;
                        checkBox18.Enabled = false;

                        checkBox19.Enabled = false;
                        checkBox20.Enabled = false;
                        checkBox21.Enabled = false;
                        checkBox22.Enabled = false;
                        checkBox25.Enabled = false;
                        checkBox26.Enabled = false;
                        checkBox24.Enabled = false;
                        checkBox23.Enabled = false;
                        checkBox36.Enabled = false;
                        checkBox37.Enabled = false;
                        checkBox38.Enabled = false;
                        checkBox39.Enabled = false;
                        checkBox34.Enabled = false;
                        checkBox33.Enabled = false;
                        checkBox32.Enabled = false;
                        checkBox35.Enabled = false;
                        checkBox31.Enabled = false;
                        checkBox27.Enabled = false;
                        checkBox30.Enabled = false;
                        checkBox29.Enabled = false;
                        checkBox28.Enabled = false;
                        checkBox40.Enabled = false;
                        checkBox41.Enabled = false;
                        checkBox42.Enabled = false;

                        Visszakuldes.Hide();
                        Elfogadas.Hide();
                    }
                    else if (Temp_Hibalap.ToString().Contains("Igen")) // Vezetõi jogosultság esetén nem lehet indoklást tenni - KIKAPCSOLVA!
                    {
                        //textBox1.Enabled = false;
                    }

                    kiosztva_Hibalap = Temp_Hibalap.Substring(Temp_Hibalap.LastIndexOf("/") + 1, 10);
                    iktatoszam_Hibalap = Temp_Hibalap.Substring(Temp_Hibalap.LastIndexOf("=") + 1, Temp_Hibalap.LastIndexOf("!") - Temp_Hibalap.LastIndexOf("=") - 1);

                    if (indoklas_Hibalap == null && datechange_Hibalap == "0")
                    {
                        button1.Hide();
                    }

                    if (FormCode.Fv_velemenyt_var == "0")
                    {
                        Elfogadas.Hide();
                        Visszakuldes.Hide();
                    }
                }

                string Year = kiosztva_Hibalap.Substring(0, 4);
                string Month = kiosztva_Hibalap.Substring(5, 2);
                string mentes_helye = @"\\teamweb2\sites\TMEK\Manager\Manager_Ftemp\" + Year + "\\" + Month + "\\";
                //string mentes_helye = @"C:\Teszt_adat\" + Year + "\\" + Month + "\\"; // TESZT IDEJÉRE!!!!
            
                List<List<string>> data = new List<List<string>>();
                XmlSerializer xs = new XmlSerializer(data.GetType());
                //string form_elozmeny = @"D:\\tst.xml";
                if (File.Exists(mentes_helye + iktatoszam_Hibalap + ".xml") == true)
                {
                    using (TextReader tr = new StreamReader(mentes_helye + iktatoszam_Hibalap + ".xml"))
                        data = (List<List<string>>)xs.Deserialize(tr);
                }
                else
                {
                    DialogResult dr007 = MessageBox.Show("Az elõzmények betöltése nem sikerült!", "Adatbetöltési hiba!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                foreach (List<string> rowData in data)
                    dataGridView1.Rows.Add(rowData.ToArray());

                if (iData_Hibalap.GetDataPresent(DataFormats.Text))
                {
                    Temp_Hibalap = (String)iData_Hibalap.GetData(DataFormats.Text);
                    string kiegeszitve_Hibalap = Temp_Hibalap.ToString().Substring(0, Temp_Hibalap.LastIndexOf(";"));
                    if (Temp_Hibalap.ToString().Length > 8 && kiegeszitve_Hibalap.ToString() == "1")             // Itt kerül átalakításra és beírásra a kiegészítõ jelentés beküldési ideje
                    {
                        string Bekuldve = Temp_Hibalap.ToString().Substring(Temp_Hibalap.IndexOf(",") + 1, 20);
                        
                        dataGridView1.Rows.Add("Rendszerüzenet", DateTime.Now.ToString(), "Rendszerüzenet", "A vizsgálati jelentés ügyintézõ általi kiegészítése megtörtént: " + Convert.ToDateTime(Bekuldve.ToString()).ToLongDateString() + " " + Convert.ToDateTime(Bekuldve.ToString()).ToLongTimeString(), "");//.ToLongDateString(), "");
                        dataGridView1.AllowUserToAddRows = false;
                    }
                }
            }
            catch
            {
                DialogResult dr006 = MessageBox.Show("Az adatlap megnyitása során hiba lépett fel, errõl a képernyõfotó megküldésével tájékoztasd a rendszergazdát!\n\nÜzenet:" + 
                    Temp_Hibalap + "\n" + kiosztva_Hibalap + "\n" + iktatoszam_Hibalap + 
                    "\n\nKérem továbbá, hogy a küldendõ e-mail-be a visszaküldés okát is megküldeni szíveskedj!", "Adatbetöltési hiba!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            int lastrow = dataGridView1.Rows.Count-2;
            //MessageBox.Show(dataGridView1.Rows[lastrow].Cells[2].Value.ToString());

            if (dataGridView1.Rows.Count == 1 || dataGridView1.Rows[lastrow].Cells[2].Value.ToString() != "Válaszadás (Ügyintézõ)")
            {
                Elfogadas.Hide();
                Visszakuldes.Hide();
            }
        }

        public void chkbox_chk(object sender, EventArgs e)
        {
            indoklas_Hibalap = "";
            indoklas_kod_Hibalap = "";
            if (checkBox1.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox1.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H015";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox2.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox2.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H016";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox3.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox3.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H017";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox4.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox4.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H018";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox5.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox5.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H019";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox6.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox6.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H020";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox7.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox7.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H008";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox8.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox8.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H009";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox9.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox9.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H010";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox10.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox10.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H011";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox11.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox11.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H012";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox12.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox12.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H013";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox13.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox13.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H014";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox14.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox14.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H021";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox15.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox15.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H022";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox16.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox16.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H023";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox17.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox17.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H024";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox18.Checked == true && FormCode.ugytipus != "Panasz vizsgálat")  // CSAK technológiai vizsgálatnál hazsnálható!
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox18.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H025";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }


            if (checkBox19.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox19.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H026";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox20.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox20.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H027";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox21.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox21.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H028";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox22.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox22.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H029";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox25.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox25.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H030";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox26.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox26.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H031";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox24.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox24.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H032";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox23.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox23.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H033";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox36.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox36.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H034";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox37.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox37.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H035";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox38.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox38.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H036";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox39.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox39.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H037";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox34.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox34.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H038";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox33.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox33.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H039";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox32.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox32.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H040";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox35.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox35.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H041";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox31.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox31.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H042";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox27.Checked == true && FormCode.ugytipus == "Panasz vizsgálat")//
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox27.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H043";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox30.Checked == true && FormCode.ugytipus != "Panasz vizsgálat")//
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox30.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H044";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox29.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox29.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H045";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox28.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox28.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H046";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox40.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox40.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H047";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox41.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox41.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H048";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox42.Checked == true)
            {
                indoklas_Hibalap = indoklas_Hibalap + "\n" + checkBox42.Text;
                indoklas_kod_Hibalap = indoklas_kod_Hibalap + ",H049";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }


            if (indoklas_kod_Hibalap.Length > 0 && indoklas_kod_Hibalap.ToString().Substring(0, 1) == ",")
            {
                //indoklas_Hibalap = indoklas_Hibalap.Remove(0, 1);
                indoklas_kod_Hibalap = indoklas_kod_Hibalap.Remove(0, 1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Temp_Hibalap.Contains("Igen") && textBox1.Text.Length > 0)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox11.Enabled = false;
                checkBox12.Enabled = false;
                checkBox13.Enabled = false;
                checkBox14.Enabled = false;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                checkBox17.Enabled = false;
                checkBox18.Enabled = false;

                checkBox19.Enabled = false;
                checkBox20.Enabled = false;
                checkBox21.Enabled = false;
                checkBox22.Enabled = false;
                checkBox25.Enabled = false;
                checkBox26.Enabled = false;
                checkBox24.Enabled = false;
                checkBox23.Enabled = false;
                checkBox36.Enabled = false;
                checkBox37.Enabled = false;
                checkBox38.Enabled = false;
                checkBox39.Enabled = false;
                checkBox34.Enabled = false;
                checkBox33.Enabled = false;
                checkBox32.Enabled = false;
                checkBox35.Enabled = false;
                checkBox31.Enabled = false;
                checkBox27.Enabled = false;
                checkBox30.Enabled = false;
                checkBox29.Enabled = false;
                checkBox28.Enabled = false;
                checkBox40.Enabled = false;
                checkBox41.Enabled = false;
                checkBox42.Enabled = false;


            }
            else if (Temp_Hibalap.Contains("Igen") && textBox1.Text.Length == 0)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
                checkBox8.Enabled = true;
                checkBox9.Enabled = true;
                checkBox10.Enabled = true;
                checkBox11.Enabled = true;
                checkBox12.Enabled = true;
                checkBox13.Enabled = true;
                checkBox14.Enabled = true;
                checkBox15.Enabled = true;
                checkBox16.Enabled = true;
                checkBox17.Enabled = true;
                checkBox18.Enabled = true;

                checkBox19.Enabled = true;
                checkBox20.Enabled = true;
                checkBox21.Enabled = true;
                checkBox22.Enabled = true;
                checkBox25.Enabled = true;
                checkBox26.Enabled = true;
                checkBox24.Enabled = true;
                checkBox23.Enabled = true;
                checkBox36.Enabled = true;
                checkBox37.Enabled = true;
                checkBox38.Enabled = true;
                checkBox39.Enabled = true;
                checkBox34.Enabled = true;
                checkBox33.Enabled = true;
                checkBox32.Enabled = true;
                checkBox35.Enabled = true;
                checkBox31.Enabled = true;
                checkBox27.Enabled = true;
                checkBox30.Enabled = true;
                checkBox29.Enabled = true;
                checkBox28.Enabled = true;
                checkBox40.Enabled = true;
                checkBox41.Enabled = true;
                checkBox42.Enabled = true;

            }
            else if (Temp_Hibalap.Contains("Nem") && textBox1.Text.Length > 0)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox11.Enabled = false;
                checkBox12.Enabled = false;
                checkBox13.Enabled = false;
                checkBox14.Enabled = false;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                checkBox17.Enabled = false;
                checkBox18.Enabled = false;

                checkBox19.Enabled = false;
                checkBox20.Enabled = false;
                checkBox21.Enabled = false;
                checkBox22.Enabled = false;
                checkBox25.Enabled = false;
                checkBox26.Enabled = false;
                checkBox24.Enabled = false;
                checkBox23.Enabled = false;
                checkBox36.Enabled = false;
                checkBox37.Enabled = false;
                checkBox38.Enabled = false;
                checkBox39.Enabled = false;
                checkBox34.Enabled = false;
                checkBox33.Enabled = false;
                checkBox32.Enabled = false;
                checkBox35.Enabled = false;
                checkBox31.Enabled = false;
                checkBox27.Enabled = false;
                checkBox30.Enabled = false;
                checkBox29.Enabled = false;
                checkBox28.Enabled = false;
                checkBox40.Enabled = false;
                checkBox41.Enabled = false;
                checkBox42.Enabled = false;


                button1.Visible = true;
            }

            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox11.Enabled = false;
                checkBox12.Enabled = false;
                checkBox13.Enabled = false;
                checkBox14.Enabled = false;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                checkBox17.Enabled = false;
                checkBox18.Enabled = false;

                checkBox19.Enabled = false;
                checkBox20.Enabled = false;
                checkBox21.Enabled = false;
                checkBox22.Enabled = false;
                checkBox25.Enabled = false;
                checkBox26.Enabled = false;
                checkBox24.Enabled = false;
                checkBox23.Enabled = false;
                checkBox36.Enabled = false;
                checkBox37.Enabled = false;
                checkBox38.Enabled = false;
                checkBox39.Enabled = false;
                checkBox34.Enabled = false;
                checkBox33.Enabled = false;
                checkBox32.Enabled = false;
                checkBox35.Enabled = false;
                checkBox31.Enabled = false;
                checkBox27.Enabled = false;
                checkBox30.Enabled = false;
                checkBox29.Enabled = false;
                checkBox28.Enabled = false;
                checkBox40.Enabled = false;
                checkBox41.Enabled = false;
                checkBox42.Enabled = false;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked == true && FormCode.ugytipus != "Panasz vizsgálat")
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }

            if (checkBox18.Checked == true && FormCode.ugytipus == "Panasz vizsgálat")
            {
                DialogResult dr18 = MessageBox.Show("Ez az érték panasz vizsgálat esetén nem választható!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                checkBox18.Checked = false;
            }
        }

        private void Elfogadas_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show
                ("Biztos, hogy elfogadod az ügyintézõ válaszát? \n\n Az elfogadási mûvelet a késõbbiekben nem módosíható, nem visszavonható!", "Figyelem!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                textBox2.Text = "A határidõ túllépéssel kapcsolatos indoklást elfogadom!";// +DateTime.Now.ToString() + " " + GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName);
                int lastRow = dataGridView1.Rows.Count;

                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), "Véleményezés", textBox2.Text);//, dateTimePicker1.Value.ToString().Substring(0, 11)); //, comboBox1.Text, textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
                textBox3.Text = "Nem";
                indokolva_Hibalap = "0";
                button1.Visible = true;
                Elfogadas.Hide();
                Visszakuldes.Hide();
            }
        }

        private void Visszakuldes_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Biztos, hogy visszautasítod az ügyintézõ indokását? \n\n A mûvelet a késõbbiekben nem módosíható, nem visszavonható!", "Figyelem!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                textBox2.Text = "A határidõ túllépéssel kapcsolatos indoklást nem fogadom el!";// +DateTime.Now.ToString() + " " + GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName);
                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), "Véleményezés", textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
                textBox3.Text = "Igen";
                indokolva_Hibalap = "0";
                button1.Visible = true;
                Elfogadas.Hide();
                Visszakuldes.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e) // Bezárás csak nekem
        {
            Close();
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox25.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox26.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox34.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox33.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox32.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox31.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e) // Panasz
        {
            if (checkBox30.Checked == true && FormCode.ugytipus != "Panasz vizsgálat")
            {
                DialogResult dr30 = MessageBox.Show("Ez az érték nem panasz vizsgálat esetén nem választható!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                checkBox30.Checked = false;
            }
                

            if (checkBox30.Checked == true && FormCode.ugytipus == "Panasz vizsgálat")
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox29.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox28.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e) // Technológia
        {
            if (checkBox27.Checked == true && FormCode.ugytipus != "Panasz vizsgálat")
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }

            if (checkBox27.Checked == true && FormCode.ugytipus == "Panasz vizsgálat")
            {
                DialogResult dr27 = MessageBox.Show("Ez az érték panasz vizsgálat esetén nem választható!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                checkBox27.Checked = false;
            }
        }

        private void checkBox35_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox35.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox36.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox37.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox38_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox38.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox39_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox39.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox40_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox40.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox41.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox42.Checked == true)
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
        }

        

    }
}