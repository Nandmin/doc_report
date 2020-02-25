using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Report
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string ugyszam;
            IDataObject iData = Clipboard.GetDataObject();

            if (iData.GetDataPresent(DataFormats.Text))
            {
                ugyszam = (String)iData.GetData(DataFormats.Text);
                label1.Text = "Az " + ugyszam.Substring(0, 10) + "-" + DateTime.Now.Year.ToString() + " azonosítószámú panaszhoz kapcsolódó mellékletek:";
                string forras = @"\\teamweb2\sites\TMEK\manager\Andoc\Input\" + ugyszam;
                if (!Directory.Exists(forras))
                {
                    DialogResult f1_dr03 = MessageBox.Show("A megadott ügyhöz nem érkezett melléklet az ÜPR-bõl!", "Nincs melléklet!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                }
                else
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(forras);
                    FileInfo[] files = dirinfo.GetFiles("*.*");

                    foreach (FileInfo file in files)
                    {
                        listBox1.Items.Add(file.Name);
                    }
                }
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e) //AnDOC mellékletek kiválasztása, megnyitása
        {
            try
            {
                string ugyszam;
                IDataObject iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    ugyszam = (String)iData.GetData(DataFormats.Text);
                    string forras = @"\\teamweb2\sites\TMEK\manager\Andoc\Input\" + ugyszam;
                    string TempDir = Path.GetTempPath();
                    string melleklet = listBox1.SelectedItem.ToString();
                    File.Copy(Path.Combine(forras, melleklet), Path.Combine(TempDir, melleklet), true);
                    System.Diagnostics.Process.Start(TempDir + listBox1.SelectedItem.ToString());
                }
            }
            catch (Exception c)
            {
                MessageBox.Show("A file megnyitása sikertelen volt!" + c.ToString());
            }            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ugyszam;
            string celmappa;
            IDataObject iData = Clipboard.GetDataObject();

            if (iData.GetDataPresent(DataFormats.Text)) //ha a könytár nem létezik, létrehozás és másolás
            {
                ugyszam = (String)iData.GetData(DataFormats.Text);

                string forras = @"\\teamweb2\sites\TMEK\manager\Andoc\Input\" + ugyszam;
                string filename = "*.*";

                try
                {
                    if (FormCode.Drive.ToString() == "D")
                    {
                        celmappa = celmappa = @"D:\Adatszolgáltatás\Vizsgálatok\" + ugyszam;
                    }
                    else
                    {
                        celmappa = celmappa = @"C:\Adatszolgáltatás\Vizsgálatok\" + ugyszam;
                    }
                }
                catch
                {
                    celmappa = celmappa = @"C:\Adatszolgáltatás\Vizsgálatok\" + ugyszam;
                }

                //string celmappa = "@" + FormCode.Drive.ToString() + "\\Adatszolgáltatás\\Vizsgálatok\\" + ugyszam;
                string celfile = Path.Combine(celmappa, filename);

                if (!Directory.Exists(celmappa))
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(celmappa);
                    DirectoryInfo dir = new DirectoryInfo(forras);
                    FileInfo[] files = dir.GetFiles();

                    foreach (FileInfo file in files)
                    {
                        celfile = Path.Combine(celmappa, file.Name);
                        if (!File.Exists(celfile))
                        {
                            file.CopyTo(Path.Combine(celmappa, file.Name), false);
                        }
                        else
                        {
                            DialogResult f1_dr04 = MessageBox.Show("A másolni kívánt file a megadott mappában már létezik!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    DialogResult f1_dr01 =  MessageBox.Show("A mellékletek mentése az alábbi helyre történt: \n\n" + celmappa, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else //ha a könytár létezik, akkor csak másolás
                {
                    DirectoryInfo dir = new DirectoryInfo(forras);
                    FileInfo[] files = dir.GetFiles();

                    foreach (FileInfo file in files)
                    {
                        celfile = Path.Combine(celmappa, file.Name);
                        if (!File.Exists(celfile))
                        {
                            file.CopyTo(Path.Combine(celmappa, file.Name), false);
                        }

                        else
                        {
                           DialogResult f1_d05 = MessageBox.Show("A másolni kívánt file a megadott mappában már létezik!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    //DialogResult f1_dr01 = MessageBox.Show("A mellékletek mentése az alábbi helyre történt: \n\n" + celmappa, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}