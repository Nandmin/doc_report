using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Report
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Mentés és adat SharePoint listába küldése
        {
            DialogResult dialogResult = MessageBox.Show
                ("Szeretnél más munkatársra vonatkozóan is véleményt rögzíteni?  \n\n FIGYELEM: A jelenleg rögzített adatok az 'Igen' vagy a 'Nem' gombok megnyomását követõen már nem módosíthatók!"
                , "Figyelem!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                comboBox1.Text = null;
                comboBox2.Text = null;
                richTextBox1.Text = "";
                sharepoint_kuldes(sender, e);
            }

            else if (dialogResult == DialogResult.No)
            {
                sharepoint_kuldes(sender, e);
                Close();
            }
            
            else if (dialogResult == DialogResult.Cancel)
            {
            }
        }

        public void sharepoint_kuldes(object sender, EventArgs e)
        {
            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Hibaközlés értékelés", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);
            
            batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + FormCode.iktatoszam + "</Field>" +
                "<Field Name='Ugyintezo'>" + FormCode.ugyintezo + "</Field>" +
                "<Field Name='Csoport'>" + FormCode.csoport + "</Field>" +
                "<Field Name='Targy'>" + FormCode.targy + "</Field>" +
                "<Field Name='Felulvizsgalo'>" + comboBox1.Text + "</Field>" +
                "<Field Name='Velemeny'>" + comboBox2.Text + "</Field>" +
                "<Field Name='Velemenyezo'>" + Form3.GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName) + "</Field>" +
                "<Field Name='Indoklas'>" + richTextBox1.Text + "</Field></Method>";

            try
            {
                listService.UpdateListItems(strListID, batchElement);
            }

            catch
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e) //Hibaközlõ megnyitása
        {
            if(Convert.ToDateTime(FormCode.Vizsgatlati_hatarido_uj.ToString()) > Convert.ToDateTime("2018.03.01"))
            {
                Hibalap_uj hibalap = new Hibalap_uj();
                hibalap.ShowDialog();
            }
            else
            {
                Form3 form3 = new Form3();
                form3.ShowDialog();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}