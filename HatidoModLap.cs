using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Report
{
    public partial class HatidoModLap : Form
    {
        public HatidoModLap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Határidõ módosítások", "");
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
                "<Field Name='Indoklas'>" + richTextBox1.Text + "</Field>" +
                "<Field Name='Eredeti_hatarido'>" + FormCode.Vizsgalti_hatarido + "</Field>" +
                "<Field Name='Modositott_hatarido'>" + FormCode.Vizsgatlati_hatarido_uj + "</Field></Method>";

            try
            {
                listService.UpdateListItems(strListID, batchElement);
            }

            catch
            {
                MessageBox.Show(e.ToString());
            }
            
            Close();

        }

        private void HatidoModLap_Load(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Trim().Length < 25)
            {
                button1.Hide();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Trim().Length > 25)
            {
                button1.Show();
            }
            else
            {
                button1.Hide();
            }
        }
    }
}