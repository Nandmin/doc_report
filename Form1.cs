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
                label1.Text = "Az " + ugyszam.Substring(0, 10) + "-" + DateTime.Now.Year.ToString() + " azonos�t�sz�m� panaszhoz kapcsol�d� mell�kletek:";
                string forras = @"\\teamweb2\sites\TMEK\manager\Andoc\Input\" + ugyszam;
                if (!Directory.Exists(forras))
                {
                    DialogResult f1_dr03 = MessageBox.Show("A megadott �gyh�z nem �rkezett mell�klet az �PR-b�l!", "Nincs mell�klet!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void listBox1_DoubleClick(object sender, EventArgs e) //AnDOC mell�kletek kiv�laszt�sa, megnyit�sa
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
                MessageBox.Show("A file megnyit�sa sikertelen volt!" + c.ToString());
            }            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ugyszam;
            string celmappa;
            IDataObject iData = Clipboard.GetDataObject();

            if (iData.GetDataPresent(DataFormats.Text)) //ha a k�nyt�r nem l�tezik, l�trehoz�s �s m�sol�s
            {
                ugyszam = (String)iData.GetData(DataFormats.Text);

                string forras = @"\\teamweb2\sites\TMEK\manager\Andoc\Input\" + ugyszam;
                string filename = "*.*";

                try
                {
                    if (FormCode.Drive.ToString() == "D")
                    {
                        celmappa = celmappa = @"D:\Adatszolg�ltat�s\Vizsg�latok\" + ugyszam;
                    }
                    else
                    {
                        celmappa = celmappa = @"C:\Adatszolg�ltat�s\Vizsg�latok\" + ugyszam;
                    }
                }
                catch
                {
                    celmappa = celmappa = @"C:\Adatszolg�ltat�s\Vizsg�latok\" + ugyszam;
                }

                //string celmappa = "@" + FormCode.Drive.ToString() + "\\Adatszolg�ltat�s\\Vizsg�latok\\" + ugyszam;
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
                            DialogResult f1_dr04 = MessageBox.Show("A m�solni k�v�nt file a megadott mapp�ban m�r l�tezik!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    DialogResult f1_dr01 =  MessageBox.Show("A mell�kletek ment�se az al�bbi helyre t�rt�nt: \n\n" + celmappa, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else //ha a k�nyt�r l�tezik, akkor csak m�sol�s
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
                           DialogResult f1_d05 = MessageBox.Show("A m�solni k�v�nt file a megadott mapp�ban m�r l�tezik!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    //DialogResult f1_dr01 = MessageBox.Show("A mell�kletek ment�se az al�bbi helyre t�rt�nt: \n\n" + celmappa, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}