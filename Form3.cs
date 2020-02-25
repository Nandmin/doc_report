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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public string indokolva = "1";                  
        private static string tevekenyseg;
        private static string indoklas;
        public static string indoklas_kod;
        public static string datechange = "0"; // 0 ha a dátumhoz nem nyúlt, 1 ha megváltoztatta
        public static string kiosztva;
        public static string iktatoszam;
        public static string Temp; // átküldött adatok

        public static string GetUserFullName(string domain, string userName)
        {
            DirectoryEntry userEntry = new DirectoryEntry("WinNT://" + domain + "/" + userName + ",User");
            return (string)userEntry.Properties["fullname"].Value;
        }

        public string Visszakuldes_oka
        {
            get
            {
                //return indoklas;
                return indoklas_kod;
            }
        }

        public DateTime Bekuldesi_hatarido
        {
            get
            {
                if (datechange.ToString() == "1")
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

        public string KiegVissza
        {
            get
            {
                return "0";
            }
        }

        public string Tullepes
        {
            get
            {
                return textBox3.Text;
            }
        }
        

        private void button3_Click(object sender, EventArgs e) // bezárás gomb
        {
            if (datechange.ToString() == "1" && textBox1.Text.Length == 0)
            {
                tevekenyseg = "Felülvizsgálat";
                chkbox_chk(sender, e);
                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), tevekenyseg.ToString(), indoklas.ToString(), dateTimePicker1.Value.ToString().Substring(0, 11) + dateTimePicker2.Text); //, comboBox1.Text, textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
            }

            else if (datechange.ToString() == "0" && textBox1.Text.Length > 0 && Temp.Contains("Nem"))
            {
                tevekenyseg = "Válaszadás (Ügyintézõ)";
                indoklas = textBox1.Text;
                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), tevekenyseg.ToString(), indoklas.ToString());//, dateTimePicker1.Value.ToString().Substring(0, 11)); //, comboBox1.Text, textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
                indoklas = "";
                indokolva = "0";
            }
            else if (textBox1.Text.Length > 0 && Temp.Contains("Igen"))
            {
                tevekenyseg = "Felülvizsgálat (Indoklás)";
                indoklas = textBox1.Text;
                dataGridView1.Rows.Add(GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName), DateTime.Now.ToString(), tevekenyseg.ToString(), indoklas.ToString());// dateTimePicker1.Value.ToString().Substring(0, 11) + dateTimePicker2.Text); //, comboBox1.Text, textBox2.Text);
                dataGridView1.AllowUserToAddRows = false;
                indoklas = "";
            }
            
            dataGridView1.AllowUserToAddRows = false;
            int lastRow = dataGridView1.Rows.Count;

            //if (dataGridView1.Rows[lastRow-1].Cells[1].Value != null)   // a rowindex értéke mindig 1-el kisebb a valóságnál, mert 0-ról indul a számozás
            if (lastRow >= 1)
            {
                string Year = kiosztva.Substring(0, 4);
                string Month = kiosztva.Substring(5, 2);
                string mentes_helye = @"\\teamweb2\sites\TMEK\Manager\Manager_Ftemp\" + Year + "\\" + Month + "\\";

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
                    using (TextWriter tw = new StreamWriter(@mentes_helye + iktatoszam + ".xml"))
                    {
                        xs.Serialize(tw, data);
                        tw.Close();
                    }
                }
                catch
                {
                }
            }

            this.Close();
            // dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = value; //Meghatározott cellába adat írása!!! Mindegyik értéknél 0-val indul a számozás
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
                indokolva = "0";
                button3.Visible = true;
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
                indokolva = "0";
                button3.Visible = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = "08:00";
            datechange = "0";
            
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                Temp = (String)iData.GetData(DataFormats.Text);
                if (Temp.ToString().Contains("Nem"))
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
                    Visszakuldes.Hide();
                    Elfogadas.Hide();
                }
                else if (Temp.ToString().Contains("Igen")) // Vezetõi jogosultság esetén nem lehet indoklást tenni - KIKAPCSOLVA!
                {
                    //textBox1.Enabled = false;
                }
                
                kiosztva = Temp.Substring(Temp.LastIndexOf("/") + 1, 10);
                iktatoszam = Temp.Substring(Temp.LastIndexOf("=") + 1, Temp.LastIndexOf("!") - Temp.LastIndexOf("=")-1);

                if (indoklas == null && datechange == "0")
                {
                    button3.Hide();
                }

                if (FormCode.Fv_velemenyt_var == "0")
                {
                    Elfogadas.Hide();
                    Visszakuldes.Hide();
                }
            
            }

            string Year = kiosztva.Substring(0, 4);
            string Month = kiosztva.Substring(5, 2);
            string mentes_helye = @"\\teamweb2\sites\TMEK\Manager\Manager_Ftemp\" + Year + "\\" + Month + "\\";

            List<List<string>> data = new List<List<string>>();
            XmlSerializer xs = new XmlSerializer(data.GetType());
            //string form_elozmeny = @"D:\\tst.xml";
            if (File.Exists(mentes_helye + iktatoszam + ".xml") == true)
            {
                using (TextReader tr = new StreamReader(mentes_helye + iktatoszam + ".xml"))
                    data = (List<List<string>>)xs.Deserialize(tr);
            }

            foreach (List<string> rowData in data)
                dataGridView1.Rows.Add(rowData.ToArray());
            
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    Temp = (String)iData.GetData(DataFormats.Text);
                    string kiegeszitve = Temp.ToString().Substring(0, Temp.LastIndexOf(";"));
                    if (Temp.ToString().Length > 8 && kiegeszitve.ToString() == "1")             // Itt kerül átalakításra és beírásra a kiegészítõ jelentés beküldési ideje
                    {
                        string Bekuldve = Temp.ToString().Substring(Temp.IndexOf(",")+1, 19);
                        
                        dataGridView1.Rows.Add("Rendszerüzenet", DateTime.Now.ToString(), "Rendszerüzenet", "A vizsgálati jelentés ügyintézõ általi kiegészítése megtörtént: " + Convert.ToDateTime(Bekuldve.ToString()).ToLongDateString() + " " + Convert.ToDateTime(Bekuldve.ToString()).ToLongTimeString(), "");//.ToLongDateString(), "");
                        dataGridView1.AllowUserToAddRows = false;
                    }
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

            datechange = "1";
            button3.Visible = true;

        }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            datechange = "1";
            button3.Visible = true;
        }

        public void chkbox_chk(object sender, EventArgs e)
        {
            indoklas = "";
            indoklas_kod = "";
            if (checkBox1.Checked == true)
            {
                indoklas = indoklas + "," + checkBox1.Text;
                indoklas_kod = indoklas_kod + ",H001";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox2.Checked == true)
            {
                indoklas = indoklas + "," + checkBox2.Text;
                indoklas_kod = indoklas_kod + ",H002";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }
            
            if (checkBox3.Checked == true)
            {
                indoklas = indoklas + "," + checkBox3.Text;
                indoklas_kod = indoklas_kod + ",H003";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox4.Checked == true)
            {
                indoklas = indoklas + "," + checkBox4.Text;
                indoklas_kod = indoklas_kod + ",H004";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox5.Checked == true)
            {
                indoklas = indoklas + "," + checkBox5.Text;
                indoklas_kod = indoklas_kod + ",H005";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox6.Checked == true)
            {
                indoklas = indoklas + "," + checkBox6.Text;
                indoklas_kod = indoklas_kod + ",H006";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (checkBox7.Checked == true)
            {
                indoklas = indoklas + "," + checkBox7.Text;
                indoklas_kod = indoklas_kod + ",H007";
                textBox1.Text = "";
                textBox1.Enabled = false;
            }

            if (indoklas.Length > 1 && indoklas.ToString().Substring(0, 1) == ",")
            {
                    indoklas = indoklas.Remove(0, 1);
                    indoklas_kod = indoklas_kod.Remove(0, 1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Temp.Contains("Igen") && textBox1.Text.Length > 0)
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
            }
            else if (Temp.Contains("Igen") && textBox1.Text.Length == 0)
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
            }
            else if (Temp.Contains("Nem") && textBox1.Text.Length > 0)
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
                button3.Visible = true;
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
    }
}   