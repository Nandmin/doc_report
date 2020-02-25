using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Report
{
    public partial class WebTeszt : Form
    {
        public WebTeszt()
        {
            InitializeComponent();
        }

        private int timeLeft;
        public int kerdes_sorszam;
        public string k1;
        public string k1_ID;
        public string k1_v1;
        public string k1_v2;
        public string k1_v3;
        public string k1_helyes;
        public string k1_megoldas;
        public string k2;
        public string k2_ID;
        public string k2_v1;
        public string k2_v2;
        public string k2_v3;
        public string k2_helyes;
        public string k2_megoldas;
        public string k3;
        public string k3_ID;
        public string k3_v1;
        public string k3_v2;
        public string k3_v3;
        public string k3_helyes;
        public string k3_megoldas;

        public string displayName;  //ez csak a teszt miatt kell
        public string csoport;

        private void WebTeszt_Load(object sender, EventArgs e)
        {
            kerdesek_betoltese();

            textTimer.Text = "00:03:00";
            kerdes_sorszam = 1;
            kerdesek_valtasa();

            if (textTimer.Text == "00:00:00")
            {
                
            }
            else
            {

                string[] totalSeconds = textTimer.Text.Split(':');
                int hours = Convert.ToInt32(totalSeconds[0]); // saját (minutes indul 0-ról)
                int minutes = Convert.ToInt32(totalSeconds[1]);
                int seconds = Convert.ToInt32(totalSeconds[2]);
                timeLeft = (hours * 3600) + (minutes * 60) + seconds;
                progressBar1.Maximum = timeLeft;

                timer2.Tick += new EventHandler(timer2_Tick);
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;

                Object timespan = TimeSpan.FromSeconds(timeLeft);
                textTimer.Text = timespan.ToString();// timespan.ToString(@"mm\:ss");
                progressBar1.Increment(1);
            }
            else
            {
                timer2.Stop();
                //SystemSounds.Question.Play();
                lejartIdo();
                
                Close();
            }
        }

        private void kerdesek_betoltese()
        {
            k1 = FormCode.kerdes1;
            k1_ID = FormCode.kerdesID1.ToString();
            k1_v1 = FormCode.k1V1;
            k1_v2 = FormCode.k1V2;
            k1_v3 = FormCode.k1V3;
            k1_helyes = FormCode.k1_helyesValasz;
            
            k2 = FormCode.kerdes2;
            k2_ID = FormCode.kerdesID2.ToString();
            k2_v1 = FormCode.k2V1;
            k2_v2 = FormCode.k2V2;
            k2_v3 = FormCode.k2V3;
            k2_helyes = FormCode.k2_helyesValasz;
            
            k3 = FormCode.kerdes3;
            k3_ID = FormCode.kerdesID3.ToString();
            k3_v1 = FormCode.k3V1;
            k3_v2 = FormCode.k3V2;
            k3_v3 = FormCode.k3V3;
            k3_helyes = FormCode.k3_heyesValasz;
        }

        private void kerdesek_valtasa()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;

            if (kerdes_sorszam == 1)
            {
                label1.Text = k1;
                label2.Text = k1_v1;
                label3.Text = k1_v2;
                label4.Text = k1_v3;

                if (k1_megoldas == "A")
                {
                    checkBox1.Checked = true;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (k1_megoldas == "B")
                {
                    checkBox2.Checked = true;
                    checkBox1.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (k1_megoldas == "C")
                {
                    checkBox3.Checked = true;
                    checkBox2.Checked = false;
                    checkBox1.Checked = false;
                }
            }
            else if (kerdes_sorszam == 2)
            {
                label1.Text = k2;
                label2.Text = k2_v1;
                label3.Text = k2_v2;
                label4.Text = k2_v3;

                if (k2_megoldas == "A")
                {
                    checkBox1.Checked = true;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (k2_megoldas == "B")
                {
                    checkBox2.Checked = true;
                    checkBox1.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (k2_megoldas == "C")
                {
                    checkBox3.Checked = true;
                    checkBox2.Checked = false;
                    checkBox1.Checked = false;
                }
            }
            else
            {
                label1.Text = k3;
                label2.Text = k3_v1;
                label3.Text = k3_v2;
                label4.Text = k3_v3;

                if (k3_megoldas == "A")
                {
                    checkBox1.Checked = true;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (k3_megoldas == "B")
                {
                    checkBox2.Checked = true;
                    checkBox1.Checked = false;
                    checkBox3.Checked = false;
                }
                else if (k3_megoldas == "C")
                {
                    checkBox3.Checked = true;
                    checkBox2.Checked = false;
                    checkBox1.Checked = false;
                }
            }

            if (label4.Text.Length == 0)
            {
                checkBox3.Enabled = false;
                label4.Hide();
                panel3.Hide();
            }
            else
            {
                checkBox3.Enabled = true;
                label4.Visible = true;
                panel3.Visible = true;
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (kerdes_sorszam == 1)
            {
                kerdes_sorszam = 2;
            }
            else if (kerdes_sorszam == 2)
            {
                kerdes_sorszam = 3;
            }

            kerdesek_valtasa();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (kerdes_sorszam == 2)
            {
                kerdes_sorszam = 1;
            }
            else if (kerdes_sorszam == 3)
            {
                kerdes_sorszam = 2;
            }

            kerdesek_valtasa();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (kerdes_sorszam == 1 && checkBox1.Checked == true)
            {
                k1_megoldas = "A";
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }

            if (kerdes_sorszam == 2 && checkBox1.Checked == true)
            {
                k2_megoldas = "A";
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }

            if (kerdes_sorszam == 3 && checkBox1.Checked == true)
            {
                k3_megoldas = "A";
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (kerdes_sorszam == 1 && checkBox2.Checked == true)
            {
                k1_megoldas = "B";
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }

            if (kerdes_sorszam == 2 && checkBox2.Checked == true)
            {
                k2_megoldas = "B";
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }

            if (kerdes_sorszam == 3 && checkBox2.Checked == true)
            {
                k3_megoldas = "B";
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                if (kerdes_sorszam == 1)
                {
                    k1_megoldas = "C";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                }

                if (kerdes_sorszam == 2)
                {
                    k2_megoldas = "C";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                }

                if (kerdes_sorszam == 3)
                {
                    k3_megoldas = "C";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                }
            }
        }

        private void lejartIdo()
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            btn_next.Enabled = false;
            btn_prev.Enabled = false;
            
            sharePointbaIras();
            DialogResult dr_web02 = MessageBox.Show("A válaszadásra fordítható idõ lejárt!\n\nAz adatok mentése sikeresen megtörtént.", "Lejárt az idõ!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Close();
        }

        private void sharePointbaIras()
        {
            displayName = "Németh András";
            csoport = "EVO";

            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();

            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager_WebTesztek", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            try // nem szép....de ez volt az egyszerûbb megoldás
            {
                batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + displayName + "</Field>" +
                    "<Field Name='Csoport'>" + csoport + "</Field>" +
                    "<Field Name='KerdesID'>" + k1_ID + "</Field>" +
                    "<Field Name='Adott_valasz'>" + k1_megoldas + "</Field>" +
                    "<Field Name='Helyes_valasz'>" + k1_helyes + "</Field></Method>";

                try
                {
                    listService.UpdateListItems(strListID, batchElement);
                }
                catch
                {
                    //DialogResult dr04 = MessageBox.Show("Az eredmények mentési folyamata hibával megszakadt, értesítsd a rendszergazdát!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + displayName + "</Field>" +
                    "<Field Name='Csoport'>" + csoport + "</Field>" +
                    "<Field Name='KerdesID'>" + k2_ID + "</Field>" +
                    "<Field Name='Adott_valasz'>" + k2_megoldas + "</Field>" +
                    "<Field Name='Helyes_valasz'>" + k2_helyes + "</Field></Method>";

                try
                {
                    listService.UpdateListItems(strListID, batchElement);
                }
                catch
                {
                    //DialogResult dr04 = MessageBox.Show("Az eredmények mentési folyamata hibával megszakadt, értesítsd a rendszergazdát!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + displayName + "</Field>" +
                    "<Field Name='Csoport'>" + csoport + "</Field>" +
                    "<Field Name='KerdesID'>" + k3_ID + "</Field>" +
                    "<Field Name='Adott_valasz'>" + k3_megoldas + "</Field>" +
                    "<Field Name='Helyes_valasz'>" + k3_helyes + "</Field></Method>";

                try
                {
                    listService.UpdateListItems(strListID, batchElement);
                }
                catch
                {
                    
                }
            }
            catch
            {
                DialogResult dr04 = MessageBox.Show("Az eredmények mentési folyamata hibával megszakadt, értesítsd a rendszergazdát!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            DialogResult dr_webStop_message = MessageBox.Show("Biztos, hogy befejezed?\n\nAz 'Igen' gombra történõ kattintást követõen nincs lehetõség a válaszok módosítására!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr_webStop_message == DialogResult.Yes)
            {
                timer2.Stop();
                timeLeft = 0;
                lejartIdo();
            }
        }
    }
}