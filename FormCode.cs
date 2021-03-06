using Microsoft.Office.InfoPath;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using mshtml;
using System.Net.Mail;
//using System.Net.Mime;
using System.Net;
using System.Collections;


namespace Report
{
    public partial class FormCode
    {
        public static string csoport;
        public static string posta;
        public static XPathNavigator Drive;
        public static string ugyintezo;
        public static string targy;
        public static string iktatoszam;
        public static string Vizsgalti_hatarido;        // Jelenlegi vizsgálati határidő
        public static string Vizsgatlati_hatarido_uj;   // Az új vizsgálati határidő a módosítást követően
        public static int szamlalo = 1;                 // mező230 módosításánál szükséges chk érték
        public static string Fv_tevekenyseg;            // fv_service meghívásához kell, hogy a user milyen tevékenységet végez
        public static string Mtevekenyseg;              // Melleklet nézet megnyitásával kapcsolatos tevékenység
        public static string AndocLap;                  // AnDoc adatlap megtekintésével kapcsolatos információk
        public DateTime belep;                          // Melléklet vagy Andoc adatap megnyitás időpontja
        public DateTime kilep;                          // Melléklet vagy Andoc adatap elhagyás időpontja
        public string ido_raforditas;                   // Melléklet vagy Andoc adatapon töltött idő
        public static string Fv_velemenyt_var;          // Felülvizsgálati vélemény volt-e vagy sem
        public static string tempIktato;
        public static string ugytipus;
        public static string iktato_chk_eredmeny;       // Az iktatószám ellenőrzést követően vagy továbbengedi(0), vagy törli a rögzített értékeket (1)
        public static int counter = 1;                  // Iktató_chk-nál kell, hogy csak egyszer fusson le
        public static string PID_bejovo;
        public static string instrukcio;
        public static string vizsg_elozmeny;            // visszaélés vizsgálatnál az előzmény
        public static string vizsg_nev;                 // visszaélés vizsgálatnál az elkövető neve
        public static string vizsg_munkakor;            // visszaélés vizsgálatnál az elkövető munkaköre
        public string cimzettek;                        // Outlook emil küldése ezeknek az embereknek - a cimzettek_begyujtese és az email_kuldes használja
        public XPathNavigator iratjegyzekbe;
        public string level;
        public static DateTime kiosztva;                // ügy kiosztásának napja
        public static DateTime jelentesleadas;          // jelentés leadásának a napja (fix érték, nem változhat)

        public static XPathNodeIterator UserLista;      // Panaszadatok átemelése a Feladatlap form-ra
        public static XPathNodeIterator Panaszlista;
        public static XPathNodeIterator CimzettLista;
        public static XPathNodeIterator TargyLista;
        public static XPathNodeIterator PID;
        public static XPathNodeIterator P_cimzett;
        public static XPathNodeIterator P_targy;
        public static XPathNodeIterator P_vizsgalo;
        public static XPathNodeIterator P_leiras;
        public static XPathNodeIterator P_hatarido;
        public static XPathNodeIterator P_bejelent_datum;
        public static XPathNodeIterator P_UPR_datum;
        public static XPathNodeIterator P_felvevo_posta;
        public static XPathNodeIterator P_anonim;
        public static XPathNodeIterator P_posta;

        public static XPathNodeIterator P_PID_ugyfel;
        public static XPathNodeIterator P_ugyfel_neve;
        public static XPathNodeIterator P_ugyfel_cime;
        public static XPathNodeIterator P_ugyfel_tipus;

        public static XPathNodeIterator T_PID;
        public static XPathNodeIterator T_termek;
        public static XPathNodeIterator T_termekid;
        public static XPathNodeIterator T_felvevo;
        public static XPathNodeIterator T_rendposta;
        public static XPathNodeIterator T_felvdate;
        public static XPathNodeIterator T_suly;
        public static XPathNodeIterator T_dij;
        public static XPathNodeIterator T_kszolg;

        public static XPathNodeIterator postaadatok;

        public int webTeszt_state;      // 0: hibás, nincs kérdés, nem nyílik meg 1: rendben, teszt megnyílik
        public static XPathNavigator kerdesID1;
        public static XPathNavigator kerdesID2;
        public static XPathNavigator kerdesID3;
        public static string kerdes1;
        public static string kerdes2;
        public static string kerdes3;
        public static string k1V1;
        public static string k1V2;
        public static string k1V3;
        public static string k1_helyesValasz;
        public static string k2V1;
        public static string k2V2;
        public static string k2V3;
        public static string k2_helyesValasz;
        public static string k3V1;
        public static string k3V2;
        public static string k3V3;
        public static string k3_heyesValasz;

        public static XPathNodeIterator hetvege;
        public static XPathNodeIterator targy_data;     // a Tárgy felsorolást tartalmazó lista forrása
        //public static string[,] kod32_adatok;
        public static XPathNodeIterator kod32_adatok;

        public void InternalStartup()
        {
            ((ButtonEvent)EventManager.ControlEvents["CTRL41_25"]).Clicked += new ClickedEventHandler(CTRL41_25_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL43_25"]).Clicked += new ClickedEventHandler(CTRL43_25_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL187_8"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //PDF készít
            ((ButtonEvent)EventManager.ControlEvents["CTRL52_17"]).Clicked += new ClickedEventHandler(CTRL52_17_Clicked); //Munkáltató PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL55_21"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); // TIG PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL33_102"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Biztonság Főig PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL33_17"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Biztonság terület PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL28_50"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Egyéb levél PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL24_87"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél Hálózat PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL21_6"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél Jogi igazgatóság PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL31_21"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél Jogi Szolgáltató PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL37_82"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél Logisztika PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL26_33"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél PEK PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL24_7"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Ügyféllevél PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL25_41"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél ÜGK PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL25_57"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél Vigh PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL26_61"]).Clicked += new ClickedEventHandler(CTRL187_8_Clicked); //Levél Vig PDF
            ((ButtonEvent)EventManager.ControlEvents["CTRL9_83"]).Clicked += new ClickedEventHandler(CTRL9_83_Clicked); //Egyéb mellékletek mentése
            ((ButtonEvent)EventManager.ControlEvents["CTRL44"]).Clicked += new ClickedEventHandler(CTRL44_Clicked); //Elfogadás és PÜK-be küldés ha kell
            ((ButtonEvent)EventManager.ControlEvents["CTRL31_8"]).Clicked += new ClickedEventHandler(CTRL44_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL39"]).Clicked += new ClickedEventHandler(CTRL44_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL41"]).Clicked += new ClickedEventHandler(CTRL44_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL87_8"]).Clicked += new ClickedEventHandler(CTRL44_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL156_8"]).Clicked += new ClickedEventHandler(CTRL44_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL116_8"]).Clicked += new ClickedEventHandler(CTRL44_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL115_8"]).Clicked += new ClickedEventHandler(CTRL44_Clicked);
            EventManager.XmlEvents["/my:sajátMezők/my:Ügyirat_adatai/my:Vizsgálat_állapota"].Changed += new XmlChangedEventHandler(Vizsgálat_állapota_Changed);

            //((ButtonEvent)EventManager.ControlEvents["CTRL315_8"]).Clicked += new ClickedEventHandler(CTRL315_8_Clicked); //PÜK adatlap megnyitása
            ((ButtonEvent)EventManager.ControlEvents["CTRL65_148"]).Clicked += new ClickedEventHandler(CTRL44_Clicked); // Report_BE elfogadás gomb
            EventManager.XmlEvents["/my:sajátMezők/my:BackGround/my:Logba_Esemeny"].Changed += new XmlChangedEventHandler(Logba_Esemeny_Changed);
            ((ButtonEvent)EventManager.ControlEvents["CTRL5_147"]).Clicked += new ClickedEventHandler(CTRL5_147_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL6_147"]).Clicked += new ClickedEventHandler(CTRL6_147_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["Pmelleklet"]).Clicked += new ClickedEventHandler(Pmelleklet_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL79_136"]).Clicked += new ClickedEventHandler(CTRL79_136_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL105_86"]).Clicked += new ClickedEventHandler(CTRL105_86_Clicked); //Bejövő panaszadatokba átvétel írása

            EventManager.XmlEvents["/my:sajátMezők/my:Mellékletek/my:UtolsoMOD"].Changed += new XmlChangedEventHandler(UtolsoMOD_Changed);
            EventManager.XmlEvents["/my:sajátMezők/my:Egyeb_melleklet/my:UtolsoMOD_EgyebM"].Changed += new XmlChangedEventHandler(UtolsoMOD_EgyebM_Changed);
            ((ButtonEvent)EventManager.ControlEvents["Egyetertes"]).Clicked += new ClickedEventHandler(Egyetertes_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["Visszakuldes"]).Clicked += new ClickedEventHandler(Visszakuldes_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["Fv_Elfogadas"]).Clicked += new ClickedEventHandler(Fv_Elfogadas_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["Fv_Visszavonas"]).Clicked += new ClickedEventHandler(Fv_Visszavonas_Clicked);
            EventManager.FormEvents.Loading += new LoadingEventHandler(FormEvents_Loading);
            EventManager.XmlEvents["/my:sajátMezők/my:Panasz_alapadat/my:Panasz_roviden"].Changed += new XmlChangedEventHandler(Panasz_roviden_Changed);
            ((ButtonEvent)EventManager.ControlEvents["Felulvizsgalati_adatlap"]).Clicked += new ClickedEventHandler(Felulvizsgalati_adatlap_Clicked);
            EventManager.XmlEvents["/my:sajátMezők/my:Elfogadás_lépései/my:mező230"].Changed += new XmlChangedEventHandler(mező230_Changed);
            EventManager.XmlEvents["/my:sajátMezők/my:BackGround/my:Bekuldesi_hatarido"].Changed += new XmlChangedEventHandler(Bekuldesi_hatarido_Changed);
            EventManager.XmlEvents["/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő"].Changed += new XmlChangedEventHandler(Vizsgálati_határidő_Changed);
            ((ButtonEvent)EventManager.ControlEvents["Mellekletek"]).Clicked += new ClickedEventHandler(Mellekletek_Clicked);
            EventManager.XmlEvents["/my:sajátMezők/my:Info2Groups/my:MB_Start"].Changed += new XmlChangedEventHandler(MB_Start_Changed);
            ((ButtonEvent)EventManager.ControlEvents["btn_Hianylatnak"]).Clicked += new ClickedEventHandler(btn_Hianylatnak_Clicked);
            EventManager.XmlEvents["/my:sajátMezők/my:Ügyirat_adatai/my:Iktato_rogzitve"].Changed += new XmlChangedEventHandler(Iktato_rogzitve_Changed);
            EventManager.XmlEvents["/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria"].Changing += new XmlChangingEventHandler(RegNum_Havaria_Changing);
            EventManager.XmlEvents["/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria"].Validating += new XmlValidatingEventHandler(RegNum_Havaria_Validating);
            //((ButtonEvent)EventManager.ControlEvents["btn_sulyos_szabalytalansag"]).Clicked += new ClickedEventHandler(btn_sulyos_szabalytalansag_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL21_113"]).Clicked += new ClickedEventHandler(CTRL21_113_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL22_113"]).Clicked += new ClickedEventHandler(CTRL22_113_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_szankcio"]).Clicked += new ClickedEventHandler(btn_szankcio_Clicked);
            //((ButtonEvent)EventManager.ControlEvents["btn_panaszOK"]).Clicked += new ClickedEventHandler(btn_panaszOK_Clicked);
            //((ButtonEvent)EventManager.ControlEvents["btn_kiosztas"]).Clicked += new ClickedEventHandler(btn_kiosztas_Clicked);
            //((ButtonEvent)EventManager.ControlEvents["btn_tst"]).Clicked += new ClickedEventHandler(btn_tst_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_startLap_Flap"]).Clicked += new ClickedEventHandler(btn_startLap_Flap_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["CTRL112_74"]).Clicked += new ClickedEventHandler(CTRL112_74_Clicked);
            EventManager.XmlEvents["/my:sajátMezők/my:BackGround/my:Chk_visszaeles"].Changed += new XmlChangedEventHandler(Chk_visszaeles_Changed);
            ((ButtonEvent)EventManager.ControlEvents["btn_emailKuldes"]).Clicked += new ClickedEventHandler(btn_emailKuldes_Clicked);
            ((ButtonEvent)EventManager.ControlEvents["btn_joker"]).Clicked += new ClickedEventHandler(btn_joker_Clicked);
        }

        public void CTRL41_25_Clicked(object sender, ClickedEventArgs e) // Mellékletek mentése
        {
            
            XmlNamespaceManager ns = this.NamespaceManager;
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator xnAttNode = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet/my:mező207", ns);
            
            //Repeating table-be lévő file-k mentése
            XPathNavigator domNav = MainDataSource.CreateNavigator();
            XPathNodeIterator rows = domNav.Select("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet", NamespaceManager);
            XPathNavigator celmappa = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager);

            while (rows.MoveNext())
            {
                string theAttachmentField = rows.Current.SelectSingleNode("my:mező207", NamespaceManager).Value;

                //Repating table vége

                //string theAttachmentField1 = xnAttNode.Value;
                if (theAttachmentField.Length > 0)
                {
                   string path = @celmappa.ToString();
                    try
                        {
                            System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                        }

                    catch (System.Exception c)
                        {
                            MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt:");
                        } 
                    InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                    myDecoder.SaveAttachment(path.ToString());
                 }
              }
              MessageBox.Show("A mellékletek az alábbi helyre kerültek elmentésre: " + celmappa);
        }

        public void CTRL43_25_Clicked(object sender, ClickedEventArgs e) //Mellékletek PÜK-be mentése
        {
            XmlNamespaceManager ns = this.NamespaceManager;
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator xnAttNode = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet/my:mező207", ns);

            //Repeating table-be lévő file-k mentése
            XPathNavigator domNav = MainDataSource.CreateNavigator();
            XPathNodeIterator rows = domNav.Select("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet", NamespaceManager);
            XPathNavigator celmappa = xnMain.SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Pmappa", NamespaceManager); 
            //celmappa = @"\\teamweb2\sites\tesztek\TST_Dok"; // CSAK A TESZT IDEJÉN!!!


            while (rows.MoveNext())
            {
                string mellekletTipusa = rows.Current.SelectSingleNode("my:mező205", NamespaceManager).Value; 
                string theAttachmentField = rows.Current.SelectSingleNode("my:mező207", NamespaceManager).Value;

                //Repating table vége

                //string theAttachmentField1 = xnAttNode.Value;
                if (theAttachmentField.Length > 0 && 
                    mellekletTipusa != "Jegyzőkönyv" &&
                    mellekletTipusa != "Nyilatkozat" &&
                    mellekletTipusa != "Tájékoztató" &&
                    mellekletTipusa != "Kárvállalási nyilatkozat" &&
                    mellekletTipusa != "Ellenőrzési könyvi bejegyzés")
                {
                    // string path = @"d:\MyDir";
                    string path = @celmappa.ToString();
                    try
                    {

                    }
                    catch (System.Exception c)
                    {
                        MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt: ");
                    }

                    InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                    myDecoder.SaveAttachment(path.ToString());

                }
            }
            MessageBox.Show("A mellékletek PÜK-be mentése megtörtént!");
        }

        public void CTRL187_8_Clicked(object sender, ClickedEventArgs e) //PDF készítés
        {
            string fileName;
            XPathNavigator nameNode; // File nevét tartalmazó mező
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator Pdfcel = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager); // FONTOS!!! NEM KIHAGYNI!! Ez határozza meg a mentés helyét!!
            XPathNavigator currentUser = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Current_User", NamespaceManager);
            nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
            
            if (currentUser.ToString() == "Németh András")
            {
                //ViewInfos.SwitchView("Nyomtatási: Report_2019"); //Nyomtatási: Report_2019
                fileName = nameNode.Value + ".pdf";

                string path = @Pdfcel.ToString();
                try
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                }
                catch (System.Exception c)

                {
                    MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt: ");
                }

                this.CurrentView.Export(Pdfcel.ToString() + fileName, ExportFormat.Pdf);           
                

                MessageBox.Show("A mellékletek az alábbi helyre kerültek elmentésre: " + Pdfcel);
                //ViewInfos.SwitchView("Report_2019"); //Nyomtatási: Report_2019
            }
            else
            {
            
                fileName = nameNode.Value + ".pdf";

                string path = @Pdfcel.ToString();
                try
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                }
                catch (System.Exception c)

                {
                    MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt: ");
                }

                this.CurrentView.Export(Pdfcel.ToString() + fileName, ExportFormat.Pdf);           

                MessageBox.Show("A mellékletek az alábbi helyre kerültek elmentésre: " + Pdfcel);
            }
        }

        public void CTRL52_17_Clicked(object sender, ClickedEventArgs e)    // Munkáltatói levél - pdf készítés
        {
            //CTRL187_8_Clicked(sender, e);

            string fileName;
            XPathNavigator nameNode; // File nevét tartalmazó mező
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator Pdfcel = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager); // FONTOS!!! NEM KIHAGYNI!! Ez határozza meg a mentés helyét!!

            nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
            fileName = nameNode.Value + ".pdf";

            string path = @Pdfcel.ToString();
            try
            {
                System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                this.CurrentView.Export(Pdfcel.ToString() + fileName, ExportFormat.Pdf);
            }
            catch (System.Exception c)
            {
                MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt: ");
            }

        }


        public void CTRL9_83_Clicked(object sender, ClickedEventArgs e)
        {
            XmlNamespaceManager ns = this.NamespaceManager;
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator xnAttNode = xnMain.SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81/my:mező256", ns);

            //Repeating table-be lévő file-k mentése
            XPathNavigator domNav = MainDataSource.CreateNavigator();
            XPathNodeIterator rows = domNav.Select("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81", NamespaceManager);
            XPathNavigator celmappa = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager); // Ez határozza meg a mentés helyét!!

            while (rows.MoveNext())
            {
                string theAttachmentField = rows.Current.SelectSingleNode("my:mező256", NamespaceManager).Value;

                //Repating table vége

                //string theAttachmentField1 = xnAttNode.Value;
                if (theAttachmentField.Length > 0)
                {
                    
                    string path = @celmappa.ToString();
                    try
                    {
                        System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);

                    }
                    catch (System.Exception c)
                    {
                        MessageBox.Show("A mentési folyamat megszakadt: {0}", c.ToString());
                    }

                    InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                    myDecoder.SaveAttachment(path.ToString());

                }
            }
            MessageBox.Show("A mellékletek az alábbi helyre kerültek elmentésre: " + celmappa);
        }

        public void CTRL44_Clicked(object sender, ClickedEventArgs e) // Jelentés elfogadása, szükség esetén mellékletek PÜK-be vagy Freud-nak küldése
        {
            string fileName;
            XPathNavigator nameNode; // File nevét tartalmazó mező            
            XPathNavigator Plink; // Az ügy közvetlen PÜK linkje
            XPathNavigator Tipus;

            Tipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager);
            Plink = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:P_Link", NamespaceManager);

            if (Tipus.ToString() == "Panasz vizsgálat")
            {
                XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
                XPathNavigator PanaszJelentesHelye = xnMain.SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Pmappa", NamespaceManager); // FONTOS!!! NEM KIHAGYNI!! Ez határozza meg ÉLESBEN a mentés helyét!!
                //string PanaszJelentesHelye = @"\\teamweb2\sites\TMEK\tesztek\TST_Dok\"; //Teszt idejére
                string ugyszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_bejovo", NamespaceManager).Value;
                string Mellekletek = @"\\teamweb2\sites\TMEK\Manager\Andoc\Input\" + ugyszam.ToString(); //ugyszam.Substring(0, 10) + "-" + ugyszam.Substring(ugyszam.LastIndexOf("/") + 1, 4); //Bejövő mellékletek helye
                
                nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
                fileName = nameNode.Value + ".pdf";

                string path = @PanaszJelentesHelye.ToString();
                try
                {
                    if (!System.IO.Directory.Exists(path))
                    {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                    }
                }
                catch (System.Exception c)
                {
                    MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt:");
                }

                this.CurrentView.Export(PanaszJelentesHelye.ToString() + fileName, ExportFormat.Pdf);

                // ------------------------------------------------ Panasz adatok SPlistába küldése webservice-vel ------------------------------------------ \\

                XPathNavigator Pszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolódó_ügyirat", NamespaceManager);
                XPathNavigator Reklamacio_targy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Report_adatlap/my:Rekl_targy", NamespaceManager);
                XPathNavigator Reklamacio_felmerulese = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Report_adatlap/my:Rkl_felmerules", NamespaceManager);
                XPathNavigator Pbejovo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_bejovo", NamespaceManager);
                XPathNavigator Elfogadta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:Elfogadta", NamespaceManager);
                XPathNavigator Elfogadas_nap = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:Closed", NamespaceManager);
                XPathNavigator Ugyintezo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgalo", NamespaceManager);
                XPathNavigator Panasz_adat;
                    
                if (Pbejovo.ToString().Length != 0)
                    {
                        Panasz_adat = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:AnDOC_adatlap/my:AnDOC_Panasz_adatok", NamespaceManager);
                    }
                    else
                    {
                        Panasz_adat = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Report_adatlap/my:Panasz_ok", NamespaceManager);
                    }

                int figyelt = Panasz_adat.ToString().IndexOf("|");

                if (Panasz_adat.ToString().Substring(figyelt + 1, 1) == "|")        // Ha nincs küldeményazonosító, akkor helyette egy - ad át az Andoc-nak, null érték helyett
                {
                    Panasz_adat.SetValue(Panasz_adat.ToString().Insert(figyelt + 1, "-"));
                }


                        sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();

                        listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
                        System.Xml.XmlNode ndListView = listService.GetListAndView("AnDOC_Kimeno_panaszadatok", "");
                        string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
                        string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
                        batchElement.SetAttribute("OnError", "Continue");
                        batchElement.SetAttribute("ListVersion", "1");
                        batchElement.SetAttribute("ViewName", strViewID);

                        batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + Pszam.Value + "</Field>" + 
                            "<Field Name='Ugyintezo'>" + Ugyintezo.Value + "</Field>" +
                            "<Field Name='Reklamacio_targya'>" + Reklamacio_targy.Value + "</Field>" + 
                            "<Field Name='Reklamacio_felmerulese'>" + Reklamacio_felmerulese.Value + "</Field>" +
                            "<Field Name='Panasz_adat'>" + Panasz_adat.Value + "</Field>" + 
                            "<Field Name='Elfogadta'>" + Elfogadta.Value + "</Field>" + 
                            "<Field Name='Elfogadas_napja'>" + Elfogadas_nap.Value + "</Field>" + 
                            "<Field Name='Megjegyzes'></Field>" + 
                            "<Field Name='Hiba_oka'></Field></Method>";

                        try
                        {
                            listService.UpdateListItems(strListID, batchElement);
                        }
                        catch
                        {
                            MessageBox.Show(e.ToString(), "Az adatátadási folyamat az alábbi hibával megszakadt:");
                        }
                    

                // --------------------------------------------- Webservice vége ------------------------------------------------------------------------------- \\

                        panasz_okok_kiiratasa();        // A KIJELÖLT LISTÁBA KÜLDI AZ ADATOKAT

                try
                {
                    if (Pbejovo.ToString().Length != 0)
                    {
                        Directory.Delete(Mellekletek, true);    //Bejövő panaszokhoz kapcsolódó mellékletek törlése
                    }
                }

                catch
                {
                    //MessageBox.Show("Nem volt beérkezett melléklet!");
                }

                CTRL43_25_Clicked(sender, e);           // Mellékletek PÜK-be küldésének meghívása
                //Andoc_TST_melleklet(sender, e);         // RKR panasz kezelése, mintha AndoC-nak adnánk át KIKAPCSOLVA, TÖRÖLHETŐ!!!

                {
                    XPathNavigator PanaszOka = xnMain.SelectSingleNode("/my:sajátMezők/my:Report_adatlap/my:Panasz_ok", NamespaceManager);
                    XPathNavigator Elbiralas = xnMain.SelectSingleNode("/my:sajátMezők/my:Report_adatlap/my:Elbiralas", NamespaceManager);
                    XPathNavigator BefSzolg = xnMain.SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:BefSzolg", NamespaceManager);
                    XPathNavigator JelentesCimzett = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Jelentes_cimzett", NamespaceManager);
                    XmlNamespaceManager ns = this.NamespaceManager;
                    XPathNavigator domNav = MainDataSource.CreateNavigator();
                    XPathNodeIterator rows = domNav.Select("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet", NamespaceManager);
                    
                    XPathNavigator RegNum = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
                    XPathNavigator iktato; //= xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager); !!!!!!!!!!
                    
                    XPathNavigator Erkezett = xnMain.SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Érkezett", NamespaceManager);
                    //XPathNavigator nameNode;
                    //string fileName;

                    if (RegNum.ToString().Length > 0)
                    {
                        iktato = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
                    }
                    else
                    {
                        iktato = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager);
                    }

                    //if (PanaszOka.ToString() == "Ügyfelekkel szemben nem megfelelő hangnem, stílus alkalmazása (munkavállaló)." && Elbiralas.ToString() == "Jogos")
                    //    this.CurrentView.Export(@"\\teamweb2\sites\TMEK\manager\Dokumentumok\SzolgMag\" + fileName, ExportFormat.Pdf);

                    //if (PanaszOka.ToString() == "Ügyfelekkel szemben nem megfelelő hangnem, stílus alkalmazása (vezető)." && Elbiralas.ToString() == "Jogos")
                    //    this.CurrentView.Export(@"\\teamweb2\sites\TMEK\manager\Dokumentumok\SzolgMag\" + fileName, ExportFormat.Pdf);

                    //if (PanaszOka.ToString() == "Várakozási idő") //&& Elbiralas.ToString() == "Jogos")
                    //    this.CurrentView.Export(@"\\teamweb2\sites\TMEK\manager\Dokumentumok\Varakozas\" + fileName, ExportFormat.Pdf);

                    if (BefSzolg.ToString() == "1" && JelentesCimzett.ToString() != "Csoportvezető")
                    {
                        //Repeating table-be lévő file-k mentése                          
                        nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
                        fileName = nameNode.Value + ".pdf";
                        
                        string TW_Freud = @"\\teamweb2\sites\TMEK\manager\Dokumentumok\BefSzolg\";
                        string path_F = TW_Freud + "\\" + iktato + "_" + Erkezett.ToString().Substring(0,4) + "\\";
                        //string path = Path.Combine(TW_Freud, iktato.ToString());
                        //string path = string.Concat(TW_Freud, iktato).ToString();
                        //string[] path = {@"c:\My Music", "2013", "media", "banner"};
                        
                        while (rows.MoveNext())
                        {
                            string theAttachmentField = rows.Current.SelectSingleNode("my:mező207", NamespaceManager).Value;

                            //Repating table vége
                            if (theAttachmentField.Length > 0)
                            {
                                try
                                {
                                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path_F);
                                }
                                catch (System.Exception c)
                                {
                                    MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt:");
                                }

                                InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                                myDecoder.SaveAttachment(path_F.ToString());
                            }
                        }
                        this.CurrentView.Export(path_F.ToString() + fileName, ExportFormat.Pdf);
                        
                    }
                            //MessageBox.Show("A mellékletek az alábbi helyre kerültek elmentésre: " + path);                                        
                                        
                            // this.CurrentView.Export(@"\\teamweb2\sites\TMEK\manager\Dokumentumok\BefSzolg\" + fileName, ExportFormat.Pdf); ' Original BEFSZOLG FREUD
                }
                //---------------------------
                MessageBox.Show("A panaszhoz kapcsolódó vizsgálati jelentés és mellékleteinek ÜPR-be történő mentése megtörtént!");
                               
                //System.Diagnostics.Process.Start(@PanaszJelentesHelye.ToString()); // TESZTELÉS IDEJÉRE!!!!
                
                //if (Pbejovo.ToString().Length == 0)      //Andoc-nál már ne nyíljon meg.
                //{
                //    System.Diagnostics.Process.Start(@Plink.ToString()); // PÜK adatlap megnyitása
                //}
            }
            else
            {
                //MessageBox.Show("Ez nem panasz");

                XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
                XPathNavigator Pdfcel = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager); // FONTOS!!! NEM KIHAGYNI!! Ez határozza meg a mentés helyét!!
                XPathNavigator BefSzolg = xnMain.SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:BefSzolg", NamespaceManager);
                XPathNavigator JelentesCimzett = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Jelentes_cimzett", NamespaceManager);
                XPathNavigator Erkezett = xnMain.SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Érkezett", NamespaceManager);

                nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
                fileName = nameNode.Value + ".pdf";

                string path = @Pdfcel.ToString();
                try
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                }
                catch (System.Exception c)
                {
                    MessageBox.Show(c.ToString(), "A folyamat az alábbi hibával megszakadt:");
                }

                this.CurrentView.Export(Pdfcel.ToString() + fileName, ExportFormat.Pdf);        // Itt készül a pdf a jelentésből

                if (BefSzolg.ToString() == "1" && JelentesCimzett.ToString() != "Csoportvezető")
                        {
                            XmlNamespaceManager ns = this.NamespaceManager;
                            XPathNavigator domNav = MainDataSource.CreateNavigator();
                            XPathNodeIterator rows = domNav.Select("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet", NamespaceManager);
                            //string fileName;
                            //XPathNavigator nameNode;
                            
                            XPathNavigator RegNum = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
                            XPathNavigator iktato;// = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);

                            if (RegNum.ToString().Length > 0)
                            {
                                iktato = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
                            }
                            else
                            {
                                iktato = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager);
                            }

                            //Repeating table-be lévő file-k mentése
                            nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
                            fileName = nameNode.Value + ".pdf";
                    
                            string TW_Freud = @"\\teamweb2\sites\TMEK\manager\Dokumentumok\Befszolg\";
                            string path3 = TW_Freud + "\\" + iktato + "_" + Erkezett.ToString().Substring(0,4) + "\\";
                    
                            while (rows.MoveNext())
                            {
                                string theAttachmentField = rows.Current.SelectSingleNode("my:mező207", NamespaceManager).Value;

                                //Repating table vége
                                if (theAttachmentField.Length > 0)
                                {
                                    try
                                    {
                                        System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path3);
                                    }
                                    catch (System.Exception c)
                                    {
                                        MessageBox.Show(c.ToString(), "A mentési folyamat az alábbi hibával megszakadt:");
                                    }
                                    
                                    InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                                    myDecoder.SaveAttachment(path3.ToString());
                                }
                            }
                            this.CurrentView.Export(path3.ToString() + fileName, ExportFormat.Pdf);
                            //MessageBox.Show("A mellékletek az alábbi helyre kerültek elmentésre: " + path);

                            //-------------------
                            //this.CurrentView.Export(@"\\teamweb2\sites\TMEK\manager\Dokumentumok\BefSzolg\" + fileName, ExportFormat.Pdf); //Freud mentés
                        }
                MessageBox.Show("A mellékletek az alábbi helyre kerültek elmentésre: " + Pdfcel);

            }
        }

        public void Vizsgálat_állapota_Changed(object sender, XmlEventArgs e) // Lezárás és irattári záradék beállítása
        {
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator Zarhato = xnMain.SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Zárható", NamespaceManager);

            if (Zarhato.ToString() == "Igen")
            {
                string myNamespace = NamespaceManager.LookupNamespace("my");
                using (XmlWriter writer = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:TÜSZI", NamespaceManager).AppendChild())
                {
                    writer.WriteStartElement("TÜSZI_Átadás", myNamespace);
                    writer.WriteElementString("TÜSZI-nek_átadva", myNamespace, "Cell 1");
                    writer.WriteElementString("Átadás_oka", myNamespace, "Cell 2");
                    writer.WriteEndElement();
                    writer.Close();
                }

                XPathNavigator domNav = MainDataSource.CreateNavigator();
                XPathNavigator itemNav = domNav.SelectSingleNode("/my:sajátMezők/my:TÜSZI/my:TÜSZI_Átadás[1]", NamespaceManager);

                // Sortörlés
                if (itemNav != null)
                    itemNav.DeleteSelf();
            }
        }

        

        public void CTRL65_148_Clicked(object sender, ClickedEventArgs e)
        {
            // CTRL44 meghívásával indul az elfogadási folyamat, InternalStartUp-ban definiálva
        }

        public void Logba_Esemeny_Changed(object sender, XmlEventArgs e)  //----------------- LOG (Eseménynapló) adatok létrehozása -----------------------\\
        {
            XPathNavigator Log = this.MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
            XPathNavigator User = this.MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Current_User", NamespaceManager);

            XPathNavigator domNav = MainDataSource.CreateNavigator();
            XPathNavigator itemNav = domNav.SelectSingleNode("/my:sajátMezők/my:Log/my:LogData/my:Log_Date[1]", NamespaceManager);
            XPathNavigator Ugy_tipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager);
            string Vezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Vezeto", NamespaceManager).Value;
            string JelentesLeadva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:mező229", NamespaceManager).Value; // Ügyintéző ekkor adta le
            XPathNodeIterator RepReadOnly = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:ReportReadOnly", NamespaceManager);
            int RepReadOnly_sorok = RepReadOnly.Count;
            string Elfogadott = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report[" + RepReadOnly_sorok + "]/my:ReportReadOnly", NamespaceManager).Value;

            if (Vezeto.ToString() == "Igen" && JelentesLeadva.ToString() != "" && Elfogadott.ToString() == "")// && Ugy_tipus.ToString() == "Panasz vizsgálat")
            {
                if (Log.ToString() == "Mellékletek nézet megnyitása" | Log.ToString() == "Andoc adatlap megnyitása")
                {
                    string belep_temp = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    belep = Convert.ToDateTime(belep_temp);
                    //MessageBox.Show(belep.ToString());
                    if (Log.ToString().Contains("Melléklet"))
                    {
                        Mtevekenyseg = "Mellékletek megtekintése";
                    }
                    else
                    {
                        Mtevekenyseg = "Andoc adatlap megtekintése";
                    }
                    
                }

                else if (Log.ToString() == "Mellékletek nézet elhagyása" | Log.ToString() == "Andoc adatlap elhagyása")
                {
                    string kilep_temp = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    kilep = Convert.ToDateTime(kilep_temp);
                    //MessageBox.Show(belep.ToString() + "\n\n" + kilep.ToString());

                    TimeSpan ido = kilep - belep;
                    double ora = ido.Hours;
                    double perc = ido.Minutes;
                    double mp = ido.Seconds;

                    string idoraforditas_temp = ora + ":" + perc + ":" + mp;

                    ido_raforditas = (Convert.ToDateTime(idoraforditas_temp).ToString("H:mm:ss"));
                    Mellekletek_figyelese(sender, e);
                }
                
            }

            // Sortörlés
            if (itemNav == null)
                itemNav.DeleteSelf();

            string myNamespace = NamespaceManager.LookupNamespace("my");
            using (XmlWriter writer = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Log", NamespaceManager).AppendChild())
            {
                writer.WriteStartElement("LogData", myNamespace);
                writer.WriteElementString("Log_Date", myNamespace, DateTime.Now.ToString());
                writer.WriteElementString("Log_Name", myNamespace, User.ToString());
                writer.WriteElementString("Log_Esemeny", myNamespace, Log.ToString());
                writer.WriteEndElement();
                writer.Close();
            }
            
        }

        public void CTRL5_147_Clicked(object sender, ClickedEventArgs e) //----------------- LOG (Eseménynapló) adatok PDF-be mentése -----------------------\\
        {
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator Pdfcel = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager); // FONTOS!!! NEM KIHAGYNI!! Ez határozza meg a mentés helyét!!
            string fileName;
            XPathNavigator nameNode;

            nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
            fileName = nameNode.Value + ".pdf";

            string path = @Pdfcel.ToString();
            try
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                }
            catch (System.Exception c)
                {
                    MessageBox.Show(c.ToString(), "A mentési folyamat megszakadt:");
                }

            this.CurrentView.Export(Pdfcel.ToString() + fileName, ExportFormat.Pdf);
        }

        public void CTRL6_147_Clicked(object sender, ClickedEventArgs e)    //LOG (Eseménynapló) adatok MHT-be mentése
        {
            XPathNavigator xnMain = this.MainDataSource.CreateNavigator();
            XPathNavigator Pdfcel = xnMain.SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager); // FONTOS!!! NEM KIHAGYNI!! Ez határozza meg a mentés helyét!!
            string fileName;
            XPathNavigator nameNode;

            nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
            fileName = nameNode.Value + ".mht";

            string path = @Pdfcel.ToString();
            try
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                }
            catch (System.Exception c)
                {
                    MessageBox.Show(c.ToString(), "A mentési folyamat megszakadt: ");
                }

            this.CurrentView.Export(Pdfcel.ToString() + fileName, ExportFormat.Mht);
        }

        public void Pmelleklet_Clicked(object sender, ClickedEventArgs e) //AnDOC mellékletek megnyitása form-ban
        {
            XPathNavigator ertek = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_bejovo", NamespaceManager);

            string P_szam = (ertek.Value);//.Substring(0, 10) + "-" + (ertek.Value).Substring((ertek.Value).LastIndexOf("/") + 1, 4);
            Clipboard.Clear();
            Clipboard.SetText(P_szam.ToString());
            Form1 form = new Form1();
            form.ShowDialog();

        }

        public void CTRL79_136_Clicked(object sender, ClickedEventArgs e) // ------------------ Adatok Form2-be és visszaküldése ----------------------- \\ Adatmódosító lap
        {
            XPathNavigator kpcs = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Karterites", NamespaceManager);
            XPathNavigator hiánylat = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Hiánylattal_kapcsolatos_vizsgálat", NamespaceManager);
            XPathNavigator TEO_T = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező271", NamespaceManager);
            XPathNavigator TEO_I = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező272", NamespaceManager); // értéke 0 v. 1
            XPathNavigator MKCS_T = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező481", NamespaceManager);
            XPathNavigator MKCS_I = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező482", NamespaceManager); // értéke 0 v.1
            XPathNavigator Jelentes_cimzett = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Jelentes_cimzett", NamespaceManager);
            string Iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value; // ebből képződik az iktatószám
            XPathNavigator regnum1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager); //ide fel kell venni egy IP szabályt, ami átrakja az értéket a RegNumba ----> de már nem abba a mezőbe írja az adatot
            XPathNavigator kpvez = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Központvezető", NamespaceManager);
            XPathNavigator ov = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Osztályvezető", NamespaceManager);
            XPathNavigator csv = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Csoportvezető", NamespaceManager);
            XPathNavigator VIP = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:VIP", NamespaceManager);
            XPathNavigator BefSzolg = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:BefSzolg", NamespaceManager);
            XPathNavigator Ugy_tipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager);
            XPathNavigator TMECS = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager);
            XPathNavigator cs1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező332", NamespaceManager);
            XPathNavigator cs2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező333", NamespaceManager);
            XPathNavigator cs3 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező334", NamespaceManager);
            XPathNavigator cs4 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező352", NamespaceManager);
            XPathNavigator cs5 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező353", NamespaceManager);
            XPathNavigator cs6 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező354", NamespaceManager);
            XPathNavigator sikkaszt1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Visszaélés_összege", NamespaceManager);
            XPathNavigator sikkaszt_kar = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Kárösszeg", NamespaceManager);
            XPathNavigator KapcsoltUgyintezo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolatos_ügyintéző", NamespaceManager);


            string adat = Ugy_tipus.Value + "/" + sikkaszt1.Value + "," + sikkaszt_kar.Value + "/" + TMECS.Value + ",VIP" + VIP.Value + ",Befszolg" + BefSzolg.Value + ",KPCS" + kpcs.Value + ",Hiánylat" + hiánylat.Value +
                ",TEO_T" + TEO_T.Value + ",TEO_I" + TEO_I.Value + ",MKCS_T" + MKCS_T.Value + ",MKCS_I" + MKCS_I.Value + "," + cs1.Value + "," + cs2.Value +
                "," + cs3.Value + "," + cs4.Value + "," + cs5.Value + "," + cs6.Value + "," + Jelentes_cimzett.Value + "," + Iktatoszam.Substring(0, Iktatoszam.LastIndexOf("/")) + "," + KapcsoltUgyintezo.ToString() + "!";
            Clipboard.Clear();
            Clipboard.SetText(adat.ToString());

            Form2 form = new Form2();
            form.ShowDialog();

            XPathNavigator node = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:JelCimzett2", NamespaceManager);
            XPathNavigator node2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
            XPathNavigator Kozpontvezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Központvezető", NamespaceManager);
            XPathNavigator Ovezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Osztályvezető", NamespaceManager);
            XPathNavigator Csoportvezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Csoportvezető", NamespaceManager);
            
            if (node.ToString() != form.ComboBoxValue)
            {
                node.SetValue(form.ComboBoxValue);
                if (form.ComboBoxValue.ToString() == "Központvezető")
                {
                    Kozpontvezeto.SetValue("true");
                }
                else if (form.ComboBoxValue.ToString() == "Osztályvezető")
                {
                    Ovezeto.SetValue("true"); // Fell kell venni egy technikai mezőt, amibe írok és az írja tovább a célmezőt
                }
                else if (form.ComboBoxValue.ToString() == "Csoportvezető")
                {
                    Csoportvezeto.SetValue("true");
                }
            }
            
            if (regnum1.ToString() != form.IktatoValue)
            {
                if (form.IktatoValue.Length > 8) //gyűjtőszámos iktatószám esetén
                {
                    regnum1.SetValue(form.IktatoValue);
                }
                else
                {
                    regnum1.SetValue(form.IktatoValue.Substring(0, form.IktatoValue.LastIndexOf("-")));
                }
                    
            }
            
            if (kpcs.ToString() != form.Chk1)
            {
                kpcs.SetValue(form.Chk1);
            }

            if (hiánylat.ToString() != form.Chk2)
            {
                hiánylat.SetValue(form.Chk2);
            }
            
            if (TEO_T.ToString() != form.Chk3)
            {
                TEO_T.SetValue(form.Chk3);
            }

            if (TEO_I.ToString() != form.Chk5)
            {
                TEO_I.SetValue(form.Chk5);
            }
            
            if (MKCS_T.ToString() != form.Chk4)
            {
                MKCS_T.SetValue(form.Chk4);
            }
            
            if (MKCS_I.ToString() != form.Chk6)
            {
                MKCS_I.SetValue(form.Chk6);
            }
            
            if (VIP.ToString() != form.Chk7)
            {
                VIP.SetValue(form.Chk7);
            }
           
            if (BefSzolg.ToString() != form.Chk8)
            {
                BefSzolg.SetValue(form.Chk8);
            }
           
            if (cs1.ToString() != form.Chk9)
            {
                cs1.SetValue(form.Chk9);
            }
            if (cs2.ToString() != form.Chk10)
            {
                cs2.SetValue(form.Chk10);
            }
            if (cs3.ToString() != form.Chk11)
            {
                cs3.SetValue(form.Chk11);
            }
            if (cs4.ToString() != form.Chk12)
            {
                cs4.SetValue(form.Chk12);
            }
            if (cs5.ToString() != form.Chk13)
            {
                cs5.SetValue(form.Chk13);
            }
            if (cs6.ToString() != form.Chk14)
            {
                cs6.SetValue(form.Chk14);
            }
            if (sikkaszt1.ToString() != form.Sikitas)
            {
                sikkaszt1.SetValue(form.Sikitas);
            }
            if (sikkaszt_kar.ToString() != form.Karertek)
            {
                sikkaszt_kar.SetValue(form.Karertek);
            }
            if (KapcsoltUgyintezo.ToString() != form.KapcsolatosUgyintezo)
            {
                KapcsoltUgyintezo.SetValue(form.KapcsolatosUgyintezo);
            }
        
        }

        public void CTRL105_86_Clicked(object sender, ClickedEventArgs e) //----------- Bejövő AnDOC panaszadat update, hogy eltűntjön az átvételre várók közül ----------- \\
        {
            XPathNavigator UgyTipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager);
            XPathNavigator ID = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_SP_ID", NamespaceManager);
            XPathNavigator Pbejovo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_bejovo", NamespaceManager);
            string TMECS_SZEAZ = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS_SZEAZ", NamespaceManager).Value;
            XPathNavigator instrukcio = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Instrukciók", NamespaceManager);

            if (UgyTipus.ToString() == "Panasz vizsgálat" && Pbejovo.ToString().Length != 0)
            {
                sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
                listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
                System.Xml.XmlNode ndListView = listService.GetListAndView("AnDOC_Bejovo_panaszadatok", "");
                string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
                string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
                batchElement.SetAttribute("OnError", "Continue");
                batchElement.SetAttribute("ListVersion", "1");
                batchElement.SetAttribute("ViewName", strViewID);

                DataConnections["AnDOC_Input"].Execute();           //Panaszok ismételt lekérdezése, hogy mindig az aktuális érték kerüljön updatelésre
                string Pmegjegyzes = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_megjegyzes", NamespaceManager).Value;

                //MessageBox.Show(Pmegjegyzes.ToString());
                string Megjegyzes = Pmegjegyzes + TMECS_SZEAZ;

                batchElement.InnerXml = "<Method ID='1' Cmd='Update'>" + "<Field Name='ID'>" + ID.Value + "</Field>" + "<Field Name='Megjegyzes'>" + Megjegyzes.ToString() + "</Field></Method>";
                

                if (instrukcio.Value.Length > 0)
                {
                    instrukcio_kiiratas();
                }

                try
                {
                    FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                    fc.Execute();                                                                   // adatok Sharepoint-ba küldése

                    listService.UpdateListItems(strListID, batchElement);
                    DialogResult dr02 = MessageBox.Show("Az adatmentés és ÜPR-nek történő visszajelzés sikeresen megtörtént!" + "\n" + "\n" + "Az adatlap az 'OK' gombra történő kattintást követően bezáródik!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch
                {
                    DialogResult dr03 = MessageBox.Show("Az adatmentés vagy ÜPR-nek történő visszajelzés sikertelen volt!" + "\n" + "\n" + "Amennyiben a kiosztás volt sikeretelen, kérem, hogy azt ismételten megpróbálni szíveskedj."
                        + "\n" + "Ha a kiosztás sikeres volt, akkor a problémát e-mail-en jelezd a Manager rendszergazda részére!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                this.Application.Quit();
            }
            else
            {
                //XPathNavigator instrukcio = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Instrukciók", NamespaceManager);
                
                if (instrukcio.Value.Length > 0)
                {
                    instrukcio_kiiratas();
                }

                try
                {
                    FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                    fc.Execute();                                                                   // adatok Sharepoint-ba küldése

                    DialogResult dr01 = MessageBox.Show("A mentés sikeresen megtörtént!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    DialogResult dr02 = MessageBox.Show("A mentési kísérlet sikertelen volt, kérem ismételd meg a kiosztást!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void UtolsoMOD_Changed(object sender, XmlEventArgs e) //Melléklet elnevezés figyelése. Ha a file neve "."-ra végződik, automatikusan törli
        {
            XPathNavigator LastMod = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:UtolsoMOD", NamespaceManager);
            if (LastMod.ToString().Length > 0)
            {
                string i = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:UtolsoMOD", NamespaceManager).Value;
                string attachedFile = string.Empty;
                attachedFile = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:mező207", NamespaceManager).Value;
                Encoding _encoding = Encoding.Unicode;

                using (MemoryStream _memoryStream = new MemoryStream(Convert.FromBase64String(attachedFile)))
                {
                    BinaryReader _theReader = new BinaryReader(_memoryStream);
                    byte[] _headerData = _theReader.ReadBytes(16);

                    int _fileSize = (int)_theReader.ReadUInt32(); 
                    int _attachmentNameLength = (int)_theReader.ReadUInt32() * 2;
                    byte[] _fileNameBytes = _theReader.ReadBytes(_attachmentNameLength);

                    string fileName = _encoding.GetString(_fileNameBytes, 0, _attachmentNameLength - 2);
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:Mellekelt_file_neve", NamespaceManager).SetValue(fileName.ToString());

                    if (fileName.ToString().Contains(".."))
                    {
                        MessageBox.Show("Adatrögzítési probléma: Hibás filename!" + "\n" + "\n" +
                            "A file elnevezése nem végződhet írásjelre (.,;?!), ezért a hibás elnevezésű állomány törlésre került!");
                        MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:mező207", NamespaceManager).SetValue(""); //Melléklet törlése
                        MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:Mellekelt_file_neve", NamespaceManager).SetValue(""); //Mellékelt file neve
                    }

                    else // Ha ugyanolyan elnevezésű mellékletet talál, akkor az aktuálisat törli
                    {
                        XPathNodeIterator sor = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet/my:mező207", NamespaceManager);
                        int dml = sor.Count;
                        int counter = 0;

                        for (int z = 1; z < Convert.ToInt32(i); ++z)
                        {
                            string akt_filename1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + z + "]/my:Mellekelt_file_neve", NamespaceManager).Value;
                            if (akt_filename1.ToString() == fileName.ToString() && fileName.ToString().Length > 0)
                            {
                                if (counter == 0)
                                {
                                    ++counter;
                                    MessageBox.Show("Ezt a file-t korábban már mellékletként feltöltötted (" + z.ToString() + ".számú melléklet), ezért az aktuálisan mellékelt állomány törlésre került!");
                                }
                                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:mező207", NamespaceManager).SetValue(""); //Melléklet törlése
                                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:Mellekelt_file_neve", NamespaceManager).SetValue(""); //Mellékelt file neve
                            }
                        }

                        for (int x = Convert.ToInt32(i) + 1; x <= dml; ++x)
                        {
                            string akt_filename2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + x + "]/my:Mellekelt_file_neve", NamespaceManager).Value;
                            if (akt_filename2.ToString() == fileName.ToString() && fileName.ToString().Length > 0)
                            {
                                if (counter == 0)
                                {
                                    ++counter;
                                    MessageBox.Show("Ezt a file-t korábban már mellékletként feltöltötted (" + x.ToString() + ".számú melléklet), ezért az aktuálisan mellékelt állomány törlésre került!");
                                }
                                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:mező207", NamespaceManager).SetValue(""); //Melléklet törlése
                                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet[" + i + "]/my:Mellekelt_file_neve", NamespaceManager).SetValue(""); //Mellékelt file neve
                            }
                        }
                    }
                }
            }
        }

        public void UtolsoMOD_EgyebM_Changed(object sender, XmlEventArgs e)
        {
            XPathNavigator LastMod_egyeb = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:UtolsoMOD_EgyebM", NamespaceManager);
            if (LastMod_egyeb.ToString().Length > 0)
            {
                string i = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:UtolsoMOD_EgyebM", NamespaceManager).Value;
                string attachedFile = string.Empty;
                attachedFile = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81[" + i + "]/my:mező256", NamespaceManager).Value;
                Encoding _encoding = Encoding.Unicode;

                using (MemoryStream _memoryStream = new MemoryStream(Convert.FromBase64String(attachedFile)))
                {
                    BinaryReader _theReader = new BinaryReader(_memoryStream);
                    byte[] _headerData = _theReader.ReadBytes(16);

                    int _fileSize = (int)_theReader.ReadUInt32(); 
                    int _attachmentNameLength = (int)_theReader.ReadUInt32() * 2;
                    byte[] _fileNameBytes = _theReader.ReadBytes(_attachmentNameLength);

                    string fileName = _encoding.GetString(_fileNameBytes, 0, _attachmentNameLength - 2);
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81[" + i + "]/my:Egyeb_melleklet_neve", NamespaceManager).SetValue(fileName.ToString());

                    if (fileName.ToString().Contains(".."))
                    {
                        MessageBox.Show("Adatrögzítési probléma: Hibás filename!" + "\n" + "\n" + 
                            "A file elnevezése nem végződhet írásjelre (.,;?!), ezért a hibás elnevezésű állomány törlésre került!");
                        MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81[" + i + "]/my:mező256", NamespaceManager).SetValue(""); //Melléklet törlése
                        MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81[" + i + "]/my:Egyeb_melleklet_neve", NamespaceManager).SetValue(""); //Mellékelt file neve
                    }
                    else // Ha ugyanolyan elnevezésű mellékletet talál, akkor az aktuálisat törli
                    {
                        XPathNodeIterator sor = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81/my:mező256", NamespaceManager);
                        int dml = sor.Count;

                        int j = 1;
                        while (j < dml)
                        {
                            string akt_filename = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81[" + j + "]/my:Egyeb_melleklet_neve", NamespaceManager).Value;
                            if (akt_filename.ToString() == fileName.ToString() && fileName.ToString().Length > 0)
                            {
                                MessageBox.Show("Ezt a file-t korábban már az 'Alapadatok' között mellékletként csatoltad, (" + j.ToString() +
                                    ".számú melléklet), ezért most törlésre került!");
                                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81[" + i + "]/my:mező256", NamespaceManager).SetValue(""); //Melléklet törlése
                                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81[" + i + "]/my:Egyeb_melleklet_neve", NamespaceManager).SetValue(""); //Mellékelt file neve
                            }
                            ++j;
                        }
                    }
                }
            }
        }
                

        public void fv_service(object sender, EventArgs e)      //Felülvizsgálati adatok írása a "Manager - Felülvizsgálati log" listába
        {
            XPathNavigator Tmecs = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager);
            XPathNavigator Iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager);
            XPathNavigator Targy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Tárgy", NamespaceManager);
            XPathNavigator Posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager);
            XPathNodeIterator sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Uj_elfogadas_fazis/my:csoport131/my:mező500", NamespaceManager);
            int sorszam = sorok.Count;
            string Megjegyzes = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Uj_elfogadas_fazis/my:csoport131[" + sorszam + "]/my:mező500", NamespaceManager).Value;
            string TargyPosta = Targy.Value + " (" + Posta.Value + ")";
                
            if (Fv_tevekenyseg.ToString() == "Megnyitás")   ///Megnyitáskor az utolsó észrevételt ne tegye a logba
                {
                    Megjegyzes = String.Empty;
                }

            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Felülvizsgálati log", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            if (Fv_tevekenyseg != "Visszaküldés")
            {
                batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + Iktatoszam.Value + "</Field>" +
                    "<Field Name='Csoport'>" + Tmecs.Value + "</Field>" +
                    "<Field Name='Targy'>" + TargyPosta.ToString() + "</Field>" +
                    "<Field Name='Tevekenyseg'>" + Fv_tevekenyseg.ToString() + "</Field>" +
                    "<Field Name='HibaKod'>" + Hibalap_uj.indoklas_kod_Hibalap + "</Field>" +
                    "<Field Name='Megjegyzes'>" + Megjegyzes.ToString() + "</Field></Method>";
            }
            else
            {
                batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + Iktatoszam.Value + "</Field>" +
                    "<Field Name='Csoport'>" + Tmecs.Value + "</Field>" +
                    "<Field Name='Targy'>" + TargyPosta.ToString() + "</Field>" +
                    "<Field Name='Tevekenyseg'>" + Fv_tevekenyseg.ToString() + "</Field>" +
                    "<Field Name='HibaKod'>" + Hibalap_uj.indoklas_kod_Hibalap + "</Field>" +
                    "<Field Name='HibaOK'>" + Hibalap_uj.indoklas_Hibalap + "</Field>" +
                    "<Field Name='Megjegyzes'>" + Megjegyzes.ToString() + "</Field></Method>";
            }

            try
            {
                listService.UpdateListItems(strListID, batchElement);
            }

            catch
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Egyetertes_Clicked(object sender, ClickedEventArgs e)
        {

            string chkMelleklet = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:CHK_Melleklet", NamespaceManager).Value;
            string chkAlapinfo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:CHK_Egyeb_Melleklet", NamespaceManager).Value;
            ugytipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager).Value;

            if (chkAlapinfo == "1" && ugytipus != "Panasz vizsgálat" && chkMelleklet == "1" ||
                chkAlapinfo == "0" && ugytipus == "Panasz vizsgálat" && chkMelleklet == "1" ||
                chkAlapinfo == "1" && ugytipus == "Panasz vizsgálat" && chkMelleklet == "1")
            {
                Fv_tevekenyseg = "Egyetértés";
                fv_service(sender, e);
            }
        }

        public void Visszakuldes_Clicked(object sender, ClickedEventArgs e)
        {
            Fv_tevekenyseg = "Visszaküldés";
            string TMECS = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager).Value;

            if (TMECS.Substring(TMECS.Length-1, 1) == "4" || TMECS.Substring(TMECS.Length-1, 1) == "5" || TMECS.Substring(TMECS.Length-1, 1) == "6") //TIG esetében nem indul el
            {
                
            }
            else
            {
                XPathNavigator JelentesLeadas = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Jelentésleadás", NamespaceManager);

                if (Convert.ToDateTime(JelentesLeadas.ToString()) > Convert.ToDateTime("2018.02.28"))
                {
                    //MessageBox.Show("Nagyobb"); indul az új folyamat
                    hibalap_betoltes();
                }
                else
                {
                    //MessageBox.Show("kisebb");
                    Felulvizsgalati_adatlap(sender, e); //itt a régi folyamat indul
                }
            }

            TM_visszakuldes();  //EVO 2019-es TM feladathoz kapcsolódó adat külön táblába gyűjtése, mivel a LOG-ban 3 hónap után törlésre kerülne, helyhiány miatt
            fv_service(sender, e);
        }

        public void TM_visszakuldes()
        {
            string user = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Current_User", NamespaceManager).Value;
            XPathNavigator Tmecs = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager);
            XPathNavigator Iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager);
            XPathNavigator Targy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Tárgy", NamespaceManager);
            XPathNavigator Posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager);
            string TargyPosta = Targy.Value + " (" + Posta.Value + ")";

            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - VisszaküldésLOG", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + Iktatoszam.Value + "</Field>" +
                "<Field Name='Csoport'>" + Tmecs.Value + "</Field>" +
                "<Field Name='Ugyintezo'>" + ugyintezo + "</Field>" +
                "<Field Name='Targy'>" + TargyPosta.ToString() + "</Field>" +
                "<Field Name='Felulvizsgalta'>" + user + "</Field>" +
                "<Field Name='HibaOK'>" + Hibalap_uj.indoklas_kod_Hibalap + "</Field></Method>";//"</Field>" +
                //"<Field Name='Megjegyzes'>" + Megjegyzes.ToString() + "</Field></Method>";

            try
            {
                listService.UpdateListItems(strListID, batchElement);
            }

            catch
            {
                MessageBox.Show("Adatküldési hiba, értesítsd a rendszergazdát!\n\nHibakód: TW01\n\nA hiba a munkamenetet nem befolyásolja, folytathatod a munkád.");
            }
        }

        public void Fv_Elfogadas_Clicked(object sender, ClickedEventArgs e)
        {
            string sulyosSzabalytalansag = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Sulyos_szabalytalan", NamespaceManager).Value;
            string chk_start = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Jelentes_SulyosHiba_CHK", NamespaceManager).Value;
            string sulyosHiba_beosztas_CHK = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Sulyoshiba_beosztas_CHK", NamespaceManager).Value;
            string ugy_tipusa = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager).Value;
            XPathNavigator MB = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:MB_elrendelve", NamespaceManager);
            XPathNavigator MB_volt = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:MB_volt", NamespaceManager);


            //XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);


            if (MB.ToString().Length > 0 && MB_volt.ToString().Length == 0)
            {
                //MB még nem volt
            }
            else
            {
                string chkMelleklet = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:CHK_Melleklet", NamespaceManager).Value;
                string chkAlapinfo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:CHK_Egyeb_Melleklet", NamespaceManager).Value;
                ugytipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager).Value;

                if (chkAlapinfo == "1" && ugytipus != "Panasz vizsgálat" && chkMelleklet == "1" ||
                    chkAlapinfo == "0" && ugytipus == "Panasz vizsgálat" && chkMelleklet == "1" ||
                    chkAlapinfo == "1" && ugytipus == "Panasz vizsgálat" && chkMelleklet == "1")
                {
                    
                    // Mentési folyamat megszakítása, figyelmeztetés az adatok ellenőrzésére
                    if (Convert.ToInt32(chk_start) <= 2 && Convert.ToInt32(sulyosHiba_beosztas_CHK) > 0 && sulyosSzabalytalansag == "Nem")
                    {
                        // MessageBox.Show
                        //("Az ügyintéző vezető beosztású munkavállalóval kapcsolatban szankcionálási javaslatot fogalmazott meg, azonban a \"Statisztikai lapon\" nem jelölt súlyos kezelői szabálytalanságot! \n\n " +
                        //"Kérem, hogy a berögzített adatot felülvizsgálni, szükség esetén módosítani szíveskedj!", "FIGYELEM!");
                        //Logba.SetValue("Sulyos chk lefutott");

                        DialogResult dr7 = MessageBox.Show("Az elfogadási folyamat hibára futott!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    else //if (Convert.ToInt32(chk_start) >= 3 || Convert.ToInt32(chk_start) == 1)
                    {
                        
                        System.Threading.Thread.Sleep(3000); // 3 mp szünet, hogy előtt minden lefusson

                        string voltTullepes = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Tullepes_chk", NamespaceManager).Value; // Ha 0 nem, ha 1 akkor igen
                        Fv_tevekenyseg = "Elfogadás";
                        fv_service(sender, e);
                        CTRL44_Clicked(sender, e);
                        hianylati_adatok_mentese();

                        if (csoport.Substring(csoport.Length - 1, 1) == "4" || csoport.Substring(csoport.Length - 1, 1) == "5" || csoport.Substring(csoport.Length - 1, 1) == "6") //TIG esetében nem indul el
                        {
                        }
                        else
                        {
                            string jelentes_cimzett = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Jelentes_cimzett", NamespaceManager).Value;
                            try
                            {
                                if (voltTullepes == "1")// && csoport != "MKCS" && jelentes_cimzett != "Csoportvezető")
                                {
                                    Form4 form4 = new Form4();
                                    form4.ShowDialog();
                                }
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show("A határidő túllépés értékelése során hiba lépett fel!" + ex.InnerException, "Kritikus hiba!");
                            }
                        }
                        //Logba.SetValue("Kód lefutott");

                        if (ugy_tipusa == "Panasz vizsgálat")
                        {
                            FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                            fc.Execute();                                                                   // adatok Sharepoint-ba küldése
                            
                        }
                    }
                    //MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Jelentes_SulyosHiba_CHK", NamespaceManager).SetValue("3");
                }
            } 
        }

        public void Fv_Visszavonas_Clicked(object sender, ClickedEventArgs e)
        {
            Fv_tevekenyseg = "Visszavonás";
            fv_service(sender, e);
        }

        public void FormEvents_Loading(object sender, LoadingEventArgs e)
        {
            csoport = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager).Value;
            ugyintezo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:gpContactSelector/my:Person/my:DisplayName", NamespaceManager).Value;
            targy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Tárgy", NamespaceManager).Value;
            iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value;
            Vizsgalti_hatarido = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő", NamespaceManager).Value;

            string currentUser = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Current_User", NamespaceManager).Value;
            string Vezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Vezeto", NamespaceManager).Value;
            string JelentesLeadva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:mező229", NamespaceManager).Value; // Ügyintéző ekkor adta le
            XPathNodeIterator RepReadOnly = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:ReportReadOnly", NamespaceManager);
            int RepReadOnly_sorok = RepReadOnly.Count;
            string Elfogadott = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:Closed", NamespaceManager).Value; // /my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report[" + RepReadOnly_sorok + "]/my:ReportReadOnly", NamespaceManager).Value;
            
            szamlalo = 1;

                if (Vezeto.ToString() == "Igen" && JelentesLeadva.ToString() != "" && Elfogadott.ToString() == "")
                {
                    Fv_tevekenyseg = "Megnyitás";
                    fv_service(sender, e);
                }

                if (System.Environment.OSVersion.ToString().Contains("6.1")) //Ellenőrzi, hogy Win10 vagy Win7, majd beállítja a mentés helyét 6.1 --> Win7, 6.2 --> Win10
                {
                    
                }
                else
                {
                    Drive = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Drive", NamespaceManager);
                    Drive.SetValue("C");
                }

                //if (currentUser == "Németh András")     // Ez indítja a webteszteket
                if (Vezeto == "Nem" && csoport != "MKCS" && Elfogadott.Length == 0)
                {
                    //MessageBox.Show("vezető: " + Vezeto + "\n\ncsoport: " + csoport);
                    webTesztek();
                }
        }

        public void Andoc_TST_melleklet(object sender, EventArgs e) // RKR ügyek mellékleteinek úgy kezelése, mintha az AnDOC-nak adnánk át. CSAK TESZT JELLEGŰ!!!
        {
            XPathNavigator xnAttNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet/my:mező207", NamespaceManager);
            XPathNodeIterator rows = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet", NamespaceManager);
            string ugyszam = "MPR-" + MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:mező388", NamespaceManager).Value + "-2016";
            string celmappa = @"\\teamweb2\sites\TMEK\manager\AnDoc\Output\" + ugyszam.ToString() + "\\";

            while (rows.MoveNext())
            {
                string theAttachmentField = rows.Current.SelectSingleNode("my:mező207", NamespaceManager).Value;
                if (theAttachmentField.Length > 0)
                {
                    string path = @celmappa.ToString();
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(celmappa);
                    }
                    catch (System.Exception c)
                    {
                        MessageBox.Show(c.ToString());
                    }
                    
                    InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                    myDecoder.SaveAttachment(path.ToString());
                }
            }

            string pszam = "P" + MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:mező388", NamespaceManager).Value;
            string pkapcs = Convert.ToString(MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolódó_ügyirat", NamespaceManager).Value); ;
            string ev = pkapcs.Substring(pkapcs.LastIndexOf("/")+1, 4);
            string PanaszHelye = @"\\kasappsfile02\reklamacio\" + ev.ToString() + "\\" + pszam.ToString();
            string iktato = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum",NamespaceManager).Value;
            string fileName = "Vizsgálati_jelentés_" + iktato.ToString() + "_" + ev.ToString() + ".pdf";
            
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(celmappa);
            }
            catch (System.Exception c)
            {
                MessageBox.Show(c.ToString());
            }

            File.Copy(Path.Combine(PanaszHelye, fileName), Path.Combine(celmappa, fileName), true);

        }

        public void Panasz_roviden_Changed(object sender, XmlEventArgs e)       // HA az interfészen nem, vagy rossz határidő jön át, akkor a leírásba be lehet írni a vizsgálati határidőt
        {
            //string panaszLeiras = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_roviden", NamespaceManager).Value;
            //XPathNavigator panaszHatarIdo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő", NamespaceManager);
            //string phat_original = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő", NamespaceManager).Value;
            //string pbejovo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_bejovo", NamespaceManager).Value;
            //string chk_PhatarIdo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Chk_PhatarIdo", NamespaceManager).Value;
            //string date;
            //DateTime ChkDate = DateTime.Today.AddDays(5);

            //if (pbejovo.Length > 0 && Convert.ToDateTime(panaszHatarIdo.ValueAsDateTime.ToShortDateString()) > ChkDate)
            //{
            //    if (panaszLeiras.ToString().Length > 11)
            //    {
            //        date = panaszLeiras.ToString().Substring(panaszLeiras.ToString().Length - 11, 11);

            //        string year = date.Substring(0, 4);
            //        string month = date.Substring(5, 2);
            //        string day = date.Substring(8, 2);
                    
            //        try
            //        {
            //            if (panaszHatarIdo.MoveToAttribute("nil", NamespaceManager.LookupNamespace("xsi")))
            //            {
            //                panaszHatarIdo.DeleteSelf();
            //            }

            //            if (date.Contains("201"))
            //            {
            //                panaszHatarIdo.SetValue(year + "-" + month + "-" + day);
            //            }
            //            else
            //            {
            //                //DateTime NewDate = DateTime.Today.AddDays(11); // 11 nap alatt kell elvégezni a panasz vizsgálatot
            //                //string NewYear = NewDate.ToShortDateString().Substring(0, 4);
            //                //string NewMonth = NewDate.ToShortDateString().Substring(5, 2);
            //                //string NewDay = NewDate.ToShortDateString().Substring(8, 2);
            //                ////MessageBox.Show(DateTime.Today.AddDays(-3).ToString());
            //                //if (Convert.ToDateTime(chk_PhatarIdo) > NewDate)
            //                //{
            //                //    panaszHatarIdo.SetValue(NewYear + "-" + NewMonth + "-" + NewDay);
            //                //}
            //                //else
            //                //{
            //                //    panaszHatarIdo.SetValue(chk_PhatarIdo.ToString());
            //                //}
            //            }
            //        }
            //        catch
            //        {
            //        }
            //    }
            //}
        }

        public void Felulvizsgalati_adatlap(object sender, EventArgs e)     // Ez a régi adadtlap, már nincs használatban, csak a 2018.02.28. előtti vizsgálatoknál jelenhet meg.
        {
            string kuldes;
            string vezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Vezeto", NamespaceManager).Value;
            XPathNavigator bekuldve = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Kieg_bekuldve", NamespaceManager);
            XPathNavigator kiegSzam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Kieg_szam", NamespaceManager);
            XPathNavigator tullepes = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Határidőtúllépés", NamespaceManager);
            string kiosztva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Kiosztva", NamespaceManager).Value;
            string iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value;
            XPathNavigator indokolva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Indokolva", NamespaceManager);
            XPathNavigator fv_valasz = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Fv_velemenyt_var", NamespaceManager);
            XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
            Fv_velemenyt_var = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Fv_velemenyt_var", NamespaceManager).Value;

            if (bekuldve.ToString().Length == 0)
            {
                kuldes = kiegSzam.ToString() + ";" + vezeto.ToString() + "," + bekuldve.ToString() + "/" + kiosztva + "=" + iktatoszam.Substring(0, iktatoszam.LastIndexOf("/")) + "!";
            }
            else
            {
                kuldes = kiegSzam.ToString() + ";" + vezeto.ToString() + "," + bekuldve.ToString().Substring(bekuldve.ToString().Length - 19, 19) + "/" + kiosztva + "=" + iktatoszam.Substring(0, iktatoszam.LastIndexOf("/")) + "!";
            }
            
            Clipboard.Clear();
            Clipboard.SetText(kuldes.ToString());
            Form3 form3 = new Form3();
            form3.ShowDialog();

            XPathNavigator visszakuldes_oka = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Visszakuldes_oka", NamespaceManager);
            XPathNavigator bekuldesi_hatarido = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Bekuldesi_hatarido", NamespaceManager);

            if (visszakuldes_oka.ToString() != form3.Visszakuldes_oka && form3.Visszakuldes_oka != null)
            {
                if (visszakuldes_oka.ToString().Length == 0)
                {
                    visszakuldes_oka.SetValue(form3.Visszakuldes_oka);
                }
                else
                {
                    visszakuldes_oka.SetValue(visszakuldes_oka.ToString() + "," + form3.Visszakuldes_oka);
                }
            }

            if (bekuldesi_hatarido.Value != form3.Bekuldesi_hatarido.ToString() && form3.Bekuldesi_hatarido > Convert.ToDateTime("2016.11.01"))
            {
                if (bekuldesi_hatarido.ToString().Length > 0)
                {
                    bekuldesi_hatarido.SetValue(form3.Bekuldesi_hatarido.ToString());//bekuldesi_hatarido.ToString() + "," + form3.Bekuldesi_hatarido.ToString());//.Substring(0,11));
                }
                else
                {
                    bekuldesi_hatarido.SetValue(form3.Bekuldesi_hatarido.ToString());//.Substring(0,11));
                }
            }

            if (kiegSzam.ToString() != form3.KiegVissza.ToString())
            {
                kiegSzam.SetValue(form3.KiegVissza.ToString());
            }

            if (tullepes.ToString() != "Igen")
            {
                tullepes.SetValue(form3.Tullepes.ToString());
            }
            else
            { 
            }

            if (form3.indokolva != "1" && vezeto == "Nem")  //Művelet Log-ba írása
            {
                indokolva.SetValue(form3.KiegVissza.ToString());
                Logba.SetValue("Határidő túllépés miatti indoklás írása");

            }
            else if (form3.indokolva != "1" && vezeto == "Igen")    //Művelet Log-ba írása
            {   
                fv_valasz.SetValue(form3.KiegVissza.ToString());
                Logba.SetValue("Határidő túllépés indoklásának véleményezése");
            }

            if (Form3.GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName) == "Vass Kálmán")
            {
                FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                fc.Execute();                                                                   // adatok Sharepoint-ba küldése
            }
        }

        public void hibalap_betoltes()
        {
            string kuldes;
            string vezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Vezeto", NamespaceManager).Value;
            XPathNavigator bekuldve = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Kieg_bekuldve", NamespaceManager);
            XPathNavigator kiegSzam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Kieg_szam", NamespaceManager);
            XPathNavigator tullepes = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Határidőtúllépés", NamespaceManager);
            kiosztva = Convert.ToDateTime(MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Kiosztva", NamespaceManager).ToString().Substring(0,10));
            string iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value;
            XPathNavigator indokolva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Indokolva", NamespaceManager);
            XPathNavigator fv_valasz = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Fv_velemenyt_var", NamespaceManager);
            XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
            string Fv_velemenyt_var = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Fv_velemenyt_var", NamespaceManager).Value;
            ugytipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager).Value;
            jelentesleadas = Convert.ToDateTime(MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Jelentésleadás", NamespaceManager).ToString().Substring(0, 10));

            //try
            //{
                if (bekuldve.ToString().Length == 0)
                {
                    kuldes = kiegSzam.ToString() + ";" + vezeto.ToString() + "," + bekuldve.ToString() + "/" + kiosztva.ToString().Trim() + "=" + iktatoszam.Substring(0, iktatoszam.LastIndexOf("/")) + "!";

                }
                else
                {
                    kuldes = kiegSzam.ToString() + ";" + vezeto.ToString() + "," + bekuldve.ToString().Substring(bekuldve.ToString().Length - 19, 19) + "/" + kiosztva.ToString().Trim() + "=" + iktatoszam.Substring(0, iktatoszam.LastIndexOf("/")) + "!";
                }
            

            Clipboard.Clear();
            Clipboard.SetText(kuldes.ToString());
            Hibalap_uj HibaLap_Form = new Hibalap_uj();
            HibaLap_Form.ShowDialog();

            //}
            //catch
            //{
            //    DialogResult dr_005 = MessageBox.Show(" bemeneti adatok képzésénél hiba keletkezett, a 'Felülvizsgálati adatlap' betöltése csak részlegesen fog megtörténni!", "Adathiba!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


            XPathNavigator Temp_visszakuldes_oka = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Temp_visszakuldes_oka", NamespaceManager);
            XPathNavigator visszakuldes_oka = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Visszakuldes_oka", NamespaceManager);
            XPathNavigator bekuldesi_hatarido = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Bekuldesi_hatarido", NamespaceManager);

            if (visszakuldes_oka.ToString() != HibaLap_Form.Visszakuldes_oka_Hibalap && HibaLap_Form.Visszakuldes_oka_Hibalap != null)
            {
                if (visszakuldes_oka.ToString().Length == 0)
                {
                    visszakuldes_oka.SetValue(HibaLap_Form.Visszakuldes_oka_Hibalap);
                    Temp_visszakuldes_oka.SetValue(HibaLap_Form.Visszakuldes_oka_Hibalap);
                }
                else
                {
                    visszakuldes_oka.SetValue(visszakuldes_oka.ToString() + "," + HibaLap_Form.Visszakuldes_oka_Hibalap);
                    Temp_visszakuldes_oka.SetValue(HibaLap_Form.Visszakuldes_oka_Hibalap);
                }
            }

            if (bekuldesi_hatarido.Value != HibaLap_Form.Bekuldesi_hatarido_Hibalap.ToString() && HibaLap_Form.Bekuldesi_hatarido_Hibalap > Convert.ToDateTime("2016.11.01"))
            {
                if (bekuldesi_hatarido.ToString().Length > 0)
                {
                    bekuldesi_hatarido.SetValue(HibaLap_Form.Bekuldesi_hatarido_Hibalap.ToString());//bekuldesi_hatarido.ToString() + "," + form1.Bekuldesi_hatarido.ToString());//.Substring(0,11));
                }
                else
                {
                    bekuldesi_hatarido.SetValue(HibaLap_Form.Bekuldesi_hatarido_Hibalap.ToString());//.Substring(0,11));
                }
            }

            if (kiegSzam.ToString() != HibaLap_Form.KiegVissza_Hibalap.ToString())
            {
                kiegSzam.SetValue(HibaLap_Form.KiegVissza_Hibalap.ToString());
            }

            if (tullepes.ToString() != "Igen")
            {
                tullepes.SetValue(HibaLap_Form.Tullepes_Hibalap.ToString());
            }
            else
            {
            }

            if (HibaLap_Form.indokolva_Hibalap != "1" && vezeto == "Nem")  //Művelet Log-ba írása
            {
                indokolva.SetValue(HibaLap_Form.KiegVissza_Hibalap.ToString());
                Logba.SetValue("Határidő túllépés miatti indoklás írása");

            }
            else if (HibaLap_Form.indokolva_Hibalap != "1" && vezeto == "Igen")    //Művelet Log-ba írása
            {
                fv_valasz.SetValue(HibaLap_Form.KiegVissza_Hibalap.ToString());
                Logba.SetValue("Határidő túllépés indoklásának véleményezése");
            }

            if (Hibalap_uj.GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName) == "Vass Kálmán")
            {
                FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                fc.Execute();                                                                   // adatok Sharepoint-ba küldése
            }

        }


        public void Felulvizsgalati_adatlap_Clicked(object sender, ClickedEventArgs e)
        {
            XPathNavigator JelentesLeadas = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Jelentésleadás", NamespaceManager);

            if (Convert.ToDateTime(JelentesLeadas.ToString()) > Convert.ToDateTime("2018.02.28"))
            {
                //MessageBox.Show("Nagyobb"); indul az új folyamat
                hibalap_betoltes();
            }
            else
            {
                //MessageBox.Show("kisebb");
                Felulvizsgalati_adatlap(sender, e); //itt a régi folyamat indul
            }
            
            //Felulvizsgalati_adatlap(sender, e);
        }

       
        public void mező230_Changed(object sender, XmlEventArgs e)
        {
            string jelentesLeadas = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:mező230", NamespaceManager).Value;
            string visszaAdta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:Nem_fogadta_el", NamespaceManager).Value;
            string csv = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:Egyetértő_csoportvezető", NamespaceManager).Value;
            XPathNavigator kiegSzam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Kieg_szam", NamespaceManager);
            XPathNavigator bekuldve = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Kieg_bekuldve", NamespaceManager);
            string kuldes;
            string vezeto = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Vezeto", NamespaceManager).Value;
            XPathNavigator tullepes = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Határidőtúllépés", NamespaceManager);
            string kiosztva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Kiosztva", NamespaceManager).Value;
            string iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value;
            string bekuldesi_hatarido = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Bekuldesi_hatarido", NamespaceManager).Value;
            XPathNavigator indokolva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Indokolva", NamespaceManager);
            XPathNavigator fv_indokra_var = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Fv_velemenyt_var", NamespaceManager);
            XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
            XPathNavigator tullepes_chk = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Tullepes_chk", NamespaceManager);

            // --------------------------------------- Beküldési dátum gyűjtése külön táblába ------------------------------------------\\
            string ugyintezo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:UserData/my:Current_User", NamespaceManager).Value;
            string csoport = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager).Value;
            string targy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Tárgy", NamespaceManager).Value;
            string vizsgalati_hatarido = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő", NamespaceManager).Value;
            string modositott_hatarido = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Határidőmódosítás/my:Határidőmód_TMECS", NamespaceManager).Value;
            string hatarido1;
            string Posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager).Value;

            if (bekuldesi_hatarido.Length > 0)
            {
                hatarido1 = bekuldesi_hatarido;
            }
            else
            {
                if (modositott_hatarido.ToString().Length > 0) // + Trim bekerült a tostring után. Ha így sem lesz jó, akkor Win10 esetében 12 karakterre kell állítani
                {
                    hatarido1 = Convert.ToDateTime(modositott_hatarido.Trim()).ToString().Trim().Substring(0, 11) + " 23:59:59"; ;
                }
                else
                {
                    hatarido1 = Convert.ToDateTime(vizsgalati_hatarido.Trim()).ToString().Trim().Substring(0, 11) + " 23:59:59";
                }
            }

            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Felülvizsgálatra", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + iktatoszam.ToString() + "</Field>" +
                "<Field Name='Ugyintezo'>" + ugyintezo.ToString() + "</Field>" +
                "<Field Name='Csoport'>" + csoport.ToString() + "</Field>" +
                "<Field Name='Bekuldesi_hatarido'>" + hatarido1.ToString() + "</Field>" +
                "<Field Name='Targy'>" + targy.ToString() + " (" + Posta + ")" + "</Field></Method>";

            try
            {
                listService.UpdateListItems(strListID, batchElement);
            }

            catch
            {
                MessageBox.Show(e.ToString());
            }
            // --------------------------------------- Beküldési dátum gyűjtése vége ------------------------------------------\\

            if (csoport.Substring(csoport.Length - 1, 1) == "4" || csoport.Substring(csoport.Length - 1, 1) == "5" || csoport.Substring(csoport.Length - 1, 1) == "6") //TIG esetében nem indul el
            {
                //TIG esetén további utasításig csak a beküldési adadot gyűtjük!
            }
            else
            {
                if (bekuldesi_hatarido.Length > 0 && jelentesLeadas.Length > 0 && visszaAdta.Length > 0 && csv.Length == 0)// && szamlalo == 1)
                {
                    kiegSzam.SetValue(szamlalo.ToString());
                    bekuldve.SetValue(jelentesLeadas);

                    string bekuldte = Convert.ToDateTime(jelentesLeadas).ToShortDateString() + " " + Convert.ToDateTime(jelentesLeadas).ToLongTimeString();
                    string hatarido = Convert.ToDateTime(bekuldesi_hatarido).ToShortDateString() + " " + Convert.ToDateTime(bekuldesi_hatarido).ToLongTimeString();

                    Form3 form3 = new Form3();
                    if (bekuldve.ToString().Length == 0)
                    {
                        kuldes = kiegSzam.ToString() + ";" + vezeto.ToString() + "," + bekuldte.ToString() + "/" + kiosztva + "=" + iktatoszam.Substring(0, iktatoszam.LastIndexOf("/")) + "!";
                    }
                    else
                    {
                        kuldes = kiegSzam.ToString() + ";" + vezeto.ToString() + "," + bekuldte.ToString().Substring(jelentesLeadas.ToString().Length - 19, 19) + "/" + kiosztva + "=" + iktatoszam.Substring(0, iktatoszam.LastIndexOf("/")) + "!";
                    }

                    Clipboard.Clear();
                    Clipboard.SetText(kuldes.ToString());

                    if (DateTime.Parse(jelentesLeadas) > DateTime.Parse(bekuldesi_hatarido))
                    {
                        tullepes_chk.SetValue(szamlalo.ToString());
                        MessageBox.Show("A vizsgálati jelentés módosítását a megadott határidőn túl fejezted be!\n\nKérem, hogy a határidőtúllépés okát indokolni szíveskedj");
                        form3.ShowDialog();

                        
                        indokolva.SetValue(szamlalo.ToString());
                        fv_indokra_var.SetValue(szamlalo.ToString());

                        if (kiegSzam.ToString() != form3.KiegVissza.ToString())
                        {
                            kiegSzam.SetValue(form3.KiegVissza.ToString());
                        }
                    }

                    if (form3.indokolva != "1" && vezeto == "Nem")
                    {
                        indokolva.SetValue(form3.KiegVissza.ToString());
                        Logba.SetValue("Határidő túllépés miatti indoklás írása");
                    }
                    else if (form3.indokolva != "1" && vezeto == "Igen")
                    {
                        fv_indokra_var.SetValue(form3.KiegVissza.ToString());
                        Logba.SetValue("Határidő túllépés indoklásának véleményezése");
                    }
                }
            }
        }

        public void Bekuldesi_hatarido_Changed(object sender, XmlEventArgs e) // A felülvizsgáló észrevételének és a beküldési határidő egy mezőbe összevonása
        {
            string Bekuldesi_hatarido = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Bekuldesi_hatarido", NamespaceManager).Value;
            XPathNodeIterator sor = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Uj_elfogadas_fazis/my:csoport131/my:mező500", NamespaceManager);
            int sorokszama = sor.Count;

            if (Bekuldesi_hatarido.Length > 0 && csoport.Substring(csoport.Length - 1, 1) != "4" || csoport.Substring(csoport.Length - 1, 1) != "5" || csoport.Substring(csoport.Length - 1, 1) != "6")
            {
                XPathNavigator Velemeny = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Uj_elfogadas_fazis/my:csoport131[" + sorokszama + "]/my:mező500", NamespaceManager);
                string ujVelemeny = Velemeny + "\n\n"+"Beküldési határidő: " + Bekuldesi_hatarido;
                Velemeny.SetValue(ujVelemeny);
            }
        }


        public void Vizsgálati_határidő_Changed(object sender, XmlEventArgs e)  //Ha módosítja a vizsgálati határidőt, akkor indokolnia kell
        {
                Vizsgatlati_hatarido_uj = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő", NamespaceManager).Value;
                string ugy_tipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager).Value;
                string kire_var = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:Kire_vár", NamespaceManager).Value;
                iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value;

                if (Vizsgatlati_hatarido_uj.Length > 0 && szamlalo == 1 && targy != null && ugy_tipus != "Panasz vizsgálat" && kire_var.Length > 0)
                {
                    ++szamlalo;
                    HatidoModLap form5 = new HatidoModLap();
                    form5.ShowDialog();
                }
        }

        public void Mellekletek_Clicked(object sender, ClickedEventArgs e)
        {
            
        }

        public void Mellekletek_figyelese(object sender, EventArgs e) // Plog listába kiírja ha a mellékletek vagy andoc adatlapot vki megnézi/becsukja
        {
            XPathNavigator Targy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Tárgy", NamespaceManager);
            XPathNavigator Posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager);
            string KapcsoltIrat = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolódó_ügyirat", NamespaceManager).Value;
            string TargyPosta = Targy.Value + " (" + Posta.Value + ")";

            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Plog", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + iktatoszam.ToString() + "</Field>" +
                "<Field Name='Csoport'>" + csoport.ToString() + "</Field>" +
                "<Field Name='Targy'>" + TargyPosta.ToString() + "</Field>" +
                "<Field Name='Kapcsolt_ugyirat'>" + KapcsoltIrat + "</Field>" +
                "<Field Name='Felhasznalo_neve'>" + Form3.GetUserFullName(System.Environment.UserDomainName, System.Environment.UserName) + "</Field>" +
                "<Field Name='Idotartam'>" + ido_raforditas.ToString() + "</Field>" +
                "<Field Name='Tevekenyseg'>" + Mtevekenyseg + "</Field></Method>";

            try
            {
                listService.UpdateListItems(strListID, batchElement);
            }

            catch
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void MB_start(object sender, EventArgs e) //MB esetén az adatokat egy külön listába írja, abból megy az e-mail értesítés az érintetteknek
        {
            string posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager).Value;
            XPathNodeIterator MB_szemelyek = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Info2Groups/my:MB/my:csoport88/my:MB_Persons", NamespaceManager);
            int MB_sorok = MB_szemelyek.Count;
            string MB_masik_cimzett = null;
            string MB_szoveg = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:mező279", NamespaceManager).Value;
            string MB_elrendelte = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:MB_elrendelő", NamespaceManager).Value;
            string MB_volt = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:MB_volt", NamespaceManager).Value;
            string TargyPosta = targy + " (" + posta + ")";

            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - MB", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            for (int i = 1; i <= MB_sorok; ++i)
            {
                string MB_cimzett = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:MB/my:csoport88[" + i + "]/my:MB_Persons", NamespaceManager).Value;
                for (int j = 1; j <= MB_sorok; ++j)
                {
                    if (j != i && MB_sorok > 1)
                    {
                        string MB_tovabbi_cimzett_akt = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:MB/my:csoport88[" + j + "]/my:MB_Persons", NamespaceManager).Value;
                        MB_masik_cimzett = MB_masik_cimzett + ", " + MB_tovabbi_cimzett_akt + ", ";
                    }
                }

                        
                if (MB_masik_cimzett != null)// ------------ Ha az utolsó karakter , akkor azt eltünteti ------------------------ \\
                {
                    if (MB_masik_cimzett.Substring(MB_masik_cimzett.Length - 2, 1) == ",")
                    {
                        MB_masik_cimzett = MB_masik_cimzett.Substring(0, MB_masik_cimzett.Length - 2);
                    }

                    if (MB_masik_cimzett.Substring(0, 1) == "," || MB_masik_cimzett.Substring(1, 1) == " ") // ha az első karakter ,
                    {
                        MB_masik_cimzett = MB_masik_cimzett.Substring(2, MB_masik_cimzett.Length -2);
                    }

                    if (MB_masik_cimzett.Contains(", ,"))
                    {
                        MB_masik_cimzett = MB_masik_cimzett.Replace(", , ", ", ");
                    }
                }                           // ------------ Ha az utolsó karakter , akkor azt eltünteti vége -------------------- \\

                        if (MB_masik_cimzett != null && MB_masik_cimzett.Length > 2)
                        {
                            batchElement.InnerXml = "<Method ID='4' Cmd='New'>" +
                                "<Field Name='Title'>" + iktatoszam.ToString() + "</Field>" +
                                "<Field Name='Csoport'>" + csoport.ToString() + "</Field>" +
                                "<Field Name='Targy'>" + TargyPosta.ToString() + "</Field>" +
                                "<Field Name='Ugyintezo'>" + ugyintezo + "</Field>" +
                                "<Field Name='MB_cimzett'>" + MB_cimzett + "</Field>" +
                                "<Field Name='MB_tovabbi_cimzett'>" + MB_masik_cimzett + "</Field>" +
                                "<Field Name='MB_szoveg'>" + MB_szoveg + "</Field></Method>";

                            MB_masik_cimzett = null;
                        }

                        else if (MB_sorok == 1)
                        {
                            batchElement.InnerXml = "<Method ID='4' Cmd='New'>" +
                                "<Field Name='Title'>" + iktatoszam.ToString() + "</Field>" +
                                "<Field Name='Csoport'>" + csoport.ToString() + "</Field>" +
                                "<Field Name='Targy'>" + TargyPosta.ToString() + "</Field>" +
                                "<Field Name='Ugyintezo'>" + ugyintezo + "</Field>" +
                                "<Field Name='MB_cimzett'>" + MB_cimzett + "</Field>" +
                                "<Field Name='MB_szoveg'>" + MB_szoveg + "</Field></Method>";
                        }

                    try
                    {
                        listService.UpdateListItems(strListID, batchElement);
                    }

                    catch
                    {
                        MessageBox.Show(e.ToString());
                    }
            }

        }

        public void MB_Start_Changed(object sender, XmlEventArgs e) //MB folyamat indítása, adatok táblába kiírása
        {
            XPathNavigator MB_Start = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Info2Groups/my:MB_Start", NamespaceManager);

            if (MB_Start.ToString() == "1")
            {
                MB_start(sender, e);
                MB_Start.SetValue("0");
            }
        }

        public void hianylati_adatok_mentese() //Hiánylati ügyek mentése TW felületre: vizsgálati jelentés + mellékletek
        {
            string hianylatiUgy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Hiánylattal_kapcsolatos_vizsgálat", NamespaceManager).Value;
            
            if (hianylatiUgy == "Igen")
            {
                XPathNavigator nameNode;
                string fileName;
                XPathNodeIterator rows = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Mellékletek/my:csoport60/my:Melléklet", NamespaceManager);
                XPathNavigator RegNum = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
                XPathNavigator iktato;
                XPathNavigator Erkezett = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Érkezett", NamespaceManager);
                
                if (RegNum.ToString().Length > 0)
                {
                    iktato = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
                }
                else
                {
                    iktato = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager);
                }

                nameNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:FileName", NamespaceManager);
                fileName = nameNode.Value + ".pdf";
                string TW_Hianylat = @"\\teamweb2\sites\TMEK\manager\Dokumentumok\Hianylat\";
                string path_Hianylat = TW_Hianylat + "\\" + iktato + "_" + Erkezett.ToString().Substring(0, 4) + "\\";

                while (rows.MoveNext())
                {
                    string theAttachmentField = rows.Current.SelectSingleNode("my:mező207", NamespaceManager).Value;

                    if (theAttachmentField.Length > 0)
                    {
                        try
                        {
                            System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path_Hianylat);
                        }
                        catch (System.Exception c)
                        {
                            MessageBox.Show("A mentési folyamat megszakadt: ", c.ToString());
                        }

                        InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                        myDecoder.SaveAttachment(path_Hianylat.ToString());
                    }
                }
                this.CurrentView.Export(path_Hianylat.ToString() + fileName, ExportFormat.Pdf);
                egyeb_mellekletek_hianylatnak();    // Ez küldi ki az egyéb mellékleteket

                // ------------------------------- Link írása egy listába --------------------------------- \\

                XPathNavigator Targy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Tárgy", NamespaceManager);
                XPathNavigator Posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager);
                string KapcsoltIrat = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolódó_ügyirat", NamespaceManager).Value;
                string TargyPosta = Targy.Value + " (" + Posta.Value + ")";

                sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
                listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
                System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Hiánylati ügyek", "");
                string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
                string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
                batchElement.SetAttribute("OnError", "Continue");
                batchElement.SetAttribute("ListVersion", "1");
                batchElement.SetAttribute("ViewName", strViewID);

                batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + "<Field Name='Title'>" + iktato + "</Field>" +
                    "<Field Name='Link'>" + "http://teamweb2/sites/TMEK/Manager/Dokumentumok/Hianylat/" + iktato + "_" + Erkezett.ToString().Substring(0, 4) + "</Field></Method>";

                try
                {
                    listService.UpdateListItems(strListID, batchElement);
                }

                catch
                {
                    //MessageBox.Show(e.ToString());
                }
            }
        }

        public void egyeb_mellekletek_hianylatnak()
        {
            XPathNavigator xnAttNode = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81/my:mező256", NamespaceManager);

            //Repeating table-be lévő file-k mentése
            XPathNodeIterator rows = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Egyeb_melleklet/my:csoport80/my:csoport81", NamespaceManager);
            XPathNavigator celmappa = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager); // Ez határozza meg a mentés helyét!!
            XPathNavigator RegNum = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
            XPathNavigator iktato;
            XPathNavigator Erkezett = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Érkezett", NamespaceManager);

            if (RegNum.ToString().Length > 0)
            {
                iktato = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum", NamespaceManager);
            }
            else
            {
                iktato = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager);
            }

            string TW_Hianylat = @"\\teamweb2\sites\TMEK\manager\Dokumentumok\Hianylat\";
            string path_Hianylat = TW_Hianylat + "\\" + iktato + "_" + Erkezett.ToString().Substring(0, 4) + "\\";

            while (rows.MoveNext())
            {
                string theAttachmentField = rows.Current.SelectSingleNode("my:mező256", NamespaceManager).Value;

                //Repating table vége
                
                if (theAttachmentField.Length > 0)
                {
                    string path = @path_Hianylat;
                    try
                    {
                        System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path_Hianylat);
                    }
                    catch (System.Exception c)
                    {
                        MessageBox.Show("A mentési folyamat megszakadt: ", c.ToString());
                    }

                    InfoPathAttachmentEncoding.InfoPathAttachmentDecoder myDecoder = new InfoPathAttachmentEncoding.InfoPathAttachmentDecoder(theAttachmentField);
                    myDecoder.SaveAttachment(path.ToString());
                }
            }
        }

        public void btn_Hianylatnak_Clicked(object sender, ClickedEventArgs e)
        {
            hianylati_adatok_mentese();
            MessageBox.Show("A vizsgálati jelentés és mellékleteinek PEK-nek történő átadása megtörtént!");
        }

        public void iktatoszamIrasa()
        {
            XPathNavigator iktatoRogzitve = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktato_rogzitve", NamespaceManager);
            XPathNavigator RegNum = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager);
            XPathNavigator iktato = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager);
            XPathNavigator Posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager);
            //XPathNavigator RegNumDate = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNumDate", NamespaceManager);
            XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
            string TargyPosta = targy + " (" + Posta.Value + ")";
                        
            if (iktatoRogzitve.ToString() == "0")                
            {
                sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
                listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
                System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Iktatószámok", "");
                string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
                string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
                batchElement.SetAttribute("OnError", "Continue");
                batchElement.SetAttribute("ListVersion", "1");
                batchElement.SetAttribute("ViewName", strViewID);

                batchElement.InnerXml = "<Method ID='4' Cmd='New'>" + 
                    "<Field Name='Title'>" + iktato + "</Field>" +
                    "<Field Name='Ugyintezo'>" + ugyintezo + "</Field>" +
                    "<Field Name='Bekuldve'>" + DateTime.Now.ToString() + "</Field>" +
                    "<Field Name='Targy'>" + TargyPosta + "</Field></Method>";

                try
                {
                    listService.UpdateListItems(strListID, batchElement);
                    //RegNumDate.SetValue(DateTime.Now.ToShortDateString());
                    //RegNumDate.SetValue(XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd"));
                    //DateTime curDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                    //RegNumDate.SetValue(curDate.GetDateTimeFormats().GetValue(5).ToString());
                    Logba.SetValue("Iktatószám rögzítése (automatikus mentés)");
                    iktatoRogzitve.SetValue("");
                    tempIktato = RegNum.ToString();
                    try
                    {
                        FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                        fc.Execute();                                                                   // adatok Sharepoint-ba küldése
                        DialogResult dr_ikt = MessageBox.Show("Az iktatószám automatikus mentése megtörtént!", "Figyelem:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        DialogResult dr_ikt2 = MessageBox.Show("Az iktatószám mentése sikertelen volt, ellenőrizd a hálózati kapcsolatod!", "Figyelem:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }

                catch
                {
                    //MessageBox.Show(e.ToString());
                }
            }

            else if (iktatoRogzitve.ToString() == "1")
            {
                string masikUgyintezo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Ugyintezo", NamespaceManager).Value;
                string masikTargy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Targy", NamespaceManager).Value;
                string masikDatum = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Datum", NamespaceManager).Value;

                MessageBox.Show("Hibás iktatószám!\n\n" + "Ezt az iktatószámot valaki már korábban rögzítette: \n\n Ügyintéző: " + masikUgyintezo + 
                    "\n Tárgy: \t  " + masikTargy + 
                    "\n Rögzítve: \t  " + Convert.ToDateTime(masikDatum).ToShortDateString().ToString() + " " + Convert.ToDateTime(masikDatum).ToLongTimeString().ToString() +
                    "\n\n Kérem, hogy helyes iktatószámot megadni szíveskedj!", "Figyelem: Adatrögzítési hiba!");
                
               //RegNum.DeleteSelf();
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager).SetValue("");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).SetValue("");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Ugyintezo", NamespaceManager).SetValue("");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Targy", NamespaceManager).SetValue("");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Datum", NamespaceManager).SetValue("");
                iktatoRogzitve.SetValue("");
                counter = 1;
            }
        }

        public void Iktato_rogzitve_Changed(object sender, XmlEventArgs e)
        {
            iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value;
            form_IktatoChk form5 = new form_IktatoChk();
            
            if (iktatoszam.Length > 0 && counter == 1)
            {
                form5.ShowDialog();

                if (iktato_chk_eredmeny.ToString() == "0")
                {
                    iktato_chk_eredmeny = "";
                    ++counter;
                    iktatoszamIrasa();
                }
                else if (iktato_chk_eredmeny.ToString() == "1")
                {
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager).SetValue("");
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).SetValue("");
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Ugyintezo", NamespaceManager).SetValue("");
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Targy", NamespaceManager).SetValue("");
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IkatoChk_Datum", NamespaceManager).SetValue("");
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktato_rogzitve", NamespaceManager).SetValue("");
                    iktato_chk_eredmeny = "";
                    counter = 1;
                }
            }

            //DataConnections["Manager - Iktatószámok"].Execute();
            //DataSource ds = this.DataSources["Manager - Iktatószámok"];
            //MessageBox.Show(ds.CreateNavigator().InnerXml.ToString());
            
        }

        public void RegNum_Havaria_Changing(object sender, XmlChangingEventArgs e)
        {
            XPathNavigator RegNumHavaria = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:RegNum_Havaria", NamespaceManager);
            XPathNavigator IktatoID = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:IktatoID", NamespaceManager);
            XPathNavigator iktato = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager);

            if (RegNumHavaria.ToString().Length > 0 && iktato.ToString().Length > 0 && IktatoID.ToString().Length > 0)
            {
                //DialogResult kerdes = MessageBox.Show("Valóban módosítani szeretnéd az iktatószámot?", "Figyelem! Adatmódosítást kezdeményeztél!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //if (kerdes == DialogResult.Yes)
                //{
                sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();
                listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
                System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Iktatószámok", "");
                string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
                string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
                batchElement.SetAttribute("OnError", "Continue");
                batchElement.SetAttribute("ListVersion", "1");
                batchElement.SetAttribute("ViewName", strViewID);

                batchElement.InnerXml = "<Method ID='3' Cmd='Delete'><Field Name='ID'>" + IktatoID + "</Field></Method>";
                //batchElement.InnerXml = "<Method ID='2' Cmd='Update'><Field Name='ID'>" + IktatoID + "</Field>" + "<Field Name='Title'>" + iktato + "</Field></Method>";
                //batchElement.InnerXml = "<Method ID='1' Cmd='Update'> + <Field Name='ID'>" + IktatoID + "</Field>" + "<Field Name='Title'>" + iktato + "</Field></Method>";

                try
                {
                    listService.UpdateListItems(strListID, batchElement);
                }

                catch
                {
                    MessageBox.Show(e.ToString(), "Nem található a PID értéke!");
                }
                //}
                //else
                //{
                //    MessageBox.Show(Convert.ToInt32(tempIktato).ToString());
                //    RegNumHavaria.SetValue(Convert.ToInt32(tempIktato).ToString());
                //}
            }
        }

        public void RegNum_Havaria_Validating(object sender, XmlValidatingEventArgs e)
        {
            
        }

        public void sulyos_szabalytalansag()        // A súlyos szabálytalanságok kódját egy mezőbe fűzi és a felesleges ,-től megtisztítja
        {
            XPathNodeIterator nodes = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Sulyos_hibak/my:csoport137", this.NamespaceManager);
            string súlyoshibaKódok = string.Empty;

            foreach (XPathNavigator node in nodes)
            {
                súlyoshibaKódok = súlyoshibaKódok + "," + node.SelectSingleNode("my:Hiba_Hibakod", this.NamespaceManager).Value; //mezők összefűzése v2: első mezőtől az utolsóig                
            }
            
            if (súlyoshibaKódok.Substring(0, 1) == ",")       // ha az első karakter , akkor azt eltünteti
            {
                súlyoshibaKódok = súlyoshibaKódok.Remove(0, 1);
                XPathNavigator súlyoshibaKódok_tisztított = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Sulyos_hiba", NamespaceManager);
                súlyoshibaKódok_tisztított.SetValue(súlyoshibaKódok.ToString());
            }

        }

        //public void btn_sulyos_szabalytalansag_Clicked(object sender, ClickedEventArgs e)
        //{
        //    sulyos_szabalytalansag();
        //}

        public void CTRL21_113_Clicked(object sender, ClickedEventArgs e) // StatLap-ról a Vizsgálati jelentésre ugráskor
        {
            string elfogadta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Elfogadás_lépései/my:Elfogadta", NamespaceManager).Value;
            
            if (elfogadta.Length < 1)
            {
                sulyos_szabalytalansag();
            }
        }

        public void CTRL22_113_Clicked(object sender, ClickedEventArgs e) //  StatLap-ról a Feladatlapra ugráskor
        {
            CTRL21_113_Clicked(sender, e);
        }

        public void btn_szankcio_Clicked(object sender, ClickedEventArgs e)
        {
            SzankcioLap szankciolap = new SzankcioLap();
            szankciolap.ShowDialog();

            XPathNodeIterator tabla_sorok_start = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla/my:Nev", NamespaceManager);
            int tabla_sor_db = tabla_sorok_start.Count;
            
            if (szankciolap.dataGridView1.Rows[0].Cells[0].Value != null)
            {
                int db = szankciolap.dataGridView1.Rows.Count;
                int sor;
                string chkSor = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla/my:Nev", NamespaceManager).Value;

                for (int i = 1; i < db; ++i)
                {
                    if (tabla_sor_db == 1 && chkSor.Length == 0 && i == 1)
                    {
                        sor = 1;
                    }
                    else
                    {
                        string myNamespace = NamespaceManager.LookupNamespace("my");
                        using (XmlWriter writer = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap", NamespaceManager).AppendChild())
                        {
                            writer.WriteStartElement("AdatTabla", myNamespace);
                            writer.WriteElementString("Nev", myNamespace, "");
                            writer.WriteElementString("Munkakor", myNamespace, "");
                            writer.WriteElementString("SAP", myNamespace, "");
                            writer.WriteElementString("SulyostElkovet", myNamespace, "0");
                            writer.WriteElementString("Javaslat", myNamespace, "Saját hatáskörben történő intézkedés");
                            writer.WriteElementString("Munkaltato_dontes", myNamespace, "");
                            writer.WriteElementString("Munkaltato_Q04", myNamespace, "");
                            writer.WriteElementString("Konkret_szankcio", myNamespace, "");
                            writer.WriteEndElement();
                            writer.Close();
                        }

                        tabla_sorok_start = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla/my:Nev", NamespaceManager);
                        sor = tabla_sorok_start.Count;
                    }

                    if (szankciolap.dataGridView1.Rows[i - 1].Cells[0].Value != null)
                    {

                        XPathNavigator tabla_nev = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla[" + sor + "]/my:Nev", NamespaceManager);
                        XPathNavigator tabla_SAP = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla[" + sor + "]/my:SAP", NamespaceManager);
                        XPathNavigator tabla_munkakor = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla[" + sor + "]/my:Munkakor", NamespaceManager);
                        XPathNavigator tabla_chk = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla[" + sor + "]/my:SulyostElkovet", NamespaceManager);
                        //XPathNavigator tabla_tig = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla[" + sor + "]/my:mező5", NamespaceManager);

                        tabla_nev.SetValue(szankciolap.dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                        tabla_SAP.SetValue(szankciolap.dataGridView1.Rows[i - 1].Cells[3].Value.ToString());
                        tabla_munkakor.SetValue(szankciolap.dataGridView1.Rows[i - 1].Cells[4].Value.ToString());

                        if (szankciolap.dataGridView1.Rows[i - 1].Cells[2].Value.ToString() == "Igen")
                        {
                            tabla_chk.SetValue("1");
                        }
                        else
                        {
                            tabla_chk.SetValue("0");
                        }

                        //tabla_tig.SetValue(szankciolap.dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                    }
                }
                XPathNodeIterator tabla = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla/my:Nev", NamespaceManager);
                int tabla_sorok = tabla.Count;

                int k = 1;
                while (k <= tabla_sorok)
                {
                    XPathNavigator itemNav = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Felelősségrevonás/my:SzankcioLap/my:AdatTabla[" + tabla_sorok + "]", NamespaceManager);

                    //Sortörlés
                    if (itemNav == null)
                    {
                        itemNav.DeleteSelf();
                    }
                    ++k;
                }
            }

        }

        public void panasz_okok_kiiratasa()     // A KIJELÖLT LISTÁBA KÜLDI AZ ADATOKAT
        {
            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();

            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - PanaszOk", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);


            XPathNodeIterator termek = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata", NamespaceManager);
            int termek_db = termek.Count;

            XPathNodeIterator posta = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata/my:Posta_adatok/my:Posta_adat", NamespaceManager);
            int posta_db = posta.Count;

            XPathNodeIterator panasz_ok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata[" + termek_db + "]/my:Posta_adatok/my:Posta_adat[" + posta_db + "]/my:Erdekeltsegi_adatok/my:Erdekeltsegi_adat/my:AnDOC_Pkategoria", NamespaceManager);
            int panaszok_db = panasz_ok.Count;

            XPathNavigator PID = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_bejovo", NamespaceManager);
            XPathNavigator csoport = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager); // ez már definiálva van
            XPathNavigator kiosztva = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Kiosztva", NamespaceManager);
            XPathNavigator Kiemelt_ugy = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:VIP", NamespaceManager);
            XPathNavigator szankcio = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Felelősségrevont_DB", NamespaceManager);
            XPathNavigator felelossegrevonas = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Felelősségrevont_DB", NamespaceManager);
            XPathNavigator kiosztPosta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager);
            string VIP;
            string felelossegrevonas_volt;
            int kulonbozo = 1;

            if (Kiemelt_ugy.Value == "0")
            {
                VIP = "Nem";
            }
            else
            {
                VIP = "Igen";
            }

            if (Convert.ToInt32(felelossegrevonas.Value) > 0)
            {
                felelossegrevonas_volt = "Igen";
            }
            else
            {
                felelossegrevonas_volt = "Nem";
            }


            for (int i = 1; i <= termek_db; i++)
            {
                for (int j = 1; j <= posta_db; j++)
                {
                    XPathNodeIterator tabla_okok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata[" + i + "]/my:Posta_adatok/my:Posta_adat[" + j + "]/my:Erdekeltsegi_adatok/my:Erdekeltsegi_adat/my:AnDOC_Pkategoria", NamespaceManager);
                    int akt_tabla_sorok = tabla_okok.Count;

                    for (int k = 1; k <= akt_tabla_sorok; k++)
                    {
                        XPathNavigator termek_neve = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata[" + i + "]/my:Termek_neve", NamespaceManager);
                        XPathNavigator Erdekelt_posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata[" + i + "]/my:Posta_adatok/my:Posta_adat[" + j + "]/my:AnDOC_Postalista", NamespaceManager);
                        //XPathNavigator TIG = MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager);
                        XPathNavigator Kategoria = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata[" + i + "]/my:Posta_adatok/my:Posta_adat[" + j + "]/my:Erdekeltsegi_adatok/my:Erdekeltsegi_adat[" + k + "]/my:AnDOC_Pkategoria", NamespaceManager);
                        XPathNavigator PanaszOka = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata[" + i + "]/my:Posta_adatok/my:Posta_adat[" + j + "]/my:Erdekeltsegi_adatok/my:Erdekeltsegi_adat[" + k + "]/my:AnDOC_PanaszOka", NamespaceManager);
                        XPathNavigator Elbiralas = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:AnDOC_adatlap/my:Termek_adatok/my:Termek_adata[" + i + "]/my:Posta_adatok/my:Posta_adat[" + j + "]/my:Erdekeltsegi_adatok/my:Erdekeltsegi_adat[" + k + "]/my:AnDOC_Minosit", NamespaceManager);

                        batchElement.InnerXml = "<Method ID='4' Cmd='New'>" +
                            "<Field Name='Title'>" + PID.Value + "</Field>" +
                            "<Field Name='Kulonbozo'>" + kulonbozo.ToString() + "</Field>" +
                            "<Field Name='Csoport'>" + csoport.Value + "</Field>" +
                            "<Field Name='Kiosztva'>" + kiosztva.Value + "</Field>" +
                            "<Field Name='Kiemelt_ugy'>" + VIP + "</Field>" +
                            "<Field Name='Erdekelt_posta'>" + Erdekelt_posta.Value + "</Field>" +
                            //"<Field Name='TIG'>" + TIG.Value + "</Field>" +
                            "<Field Name='Kategoria'>" + Kategoria.Value + "</Field>" +
                            "<Field Name='Panasz_Oka'>" + PanaszOka.Value + "</Field>" +
                            "<Field Name='Termek'>" + termek_neve.Value + "</Field>" +
                            "<Field Name='Felelossegrevonas_volt'>" + felelossegrevonas_volt + "</Field>" +
                            "<Field Name='Kioszt_posta'>" + kiosztPosta.Value + "</Field>" +
                            "<Field Name='Elbiralas'>" + Elbiralas.Value + "</Field></Method>";

                        kulonbozo = 0;

                        try
                        {
                            listService.UpdateListItems(strListID, batchElement);
                        }
                        catch
                        {
                            DialogResult dr04 = MessageBox.Show("A panasz ok-ok adatátadási folyamata hibával megszakadt, értesítsd a rendszergazdát!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        //MessageBox.Show(Erdekelt_posta.ToString() + "\nPanaszoka: " + PanaszOka.ToString());
                    }
                }
            }
        }


        public void instrukcio_kiiratas()
        {
            sitesWebServiceLists.Lists listService = new sitesWebServiceLists.Lists();

            listService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            listService.Url = "http://teamweb2/sites/TMEK/Manager/_vti_bin/Lists.asmx";
            System.Xml.XmlNode ndListView = listService.GetListAndView("Manager - Instrukciok", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            XPathNavigator csoport = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS",NamespaceManager);
            XPathNavigator name = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:gpContactSelector/my:Person/my:DisplayName",NamespaceManager);
            XPathNavigator keszult = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Created",NamespaceManager);
            XPathNavigator instrukcio = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Instrukciók",NamespaceManager);

            string id = string.Concat(csoport.ToString(), "_", name.ToString(), "_", keszult.ToString(), ".xml").Replace(':', '_');


                        batchElement.InnerXml = "<Method ID='4' Cmd='New'>" +
                            "<Field Name='Title'>" + id + "</Field>" +
                            "<Field Name='Istrukcio'>" + instrukcio.Value + "</Field></Method>";


                        try
                        {
                            listService.UpdateListItems(strListID, batchElement);
                            FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                            fc.Execute();                                                                   // adatok Sharepoint-ba küldése
                          
                            DialogResult dr01 = MessageBox.Show("A mentés sikeresen megtörtént!", "Figyelem!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            DialogResult dr002 = MessageBox.Show("A mentés sikertelen volt, végezd el újra a kiosztást!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        //MessageBox.Show(Erdekelt_posta.ToString() + "\nPanaszoka: " + PanaszOka.ToString());
                                    
        }


        public void btn_startLap_Flap_Clicked(object sender, ClickedEventArgs e)        // Windows Form-ra épülő nézet és adatok megjelenítése - fejlesztés alatt...
        {
            DialogResult dresult01 = MessageBox.Show("Az új Feladatlapot jelentítsük meg?", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dresult01 == DialogResult.Yes)
            {
                csoport = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS_2019", NamespaceManager).Value;
                ugyintezo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgalo", NamespaceManager).Value;
                ugytipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager).Value;
                string csoportSzeaz = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS_SZEAZ", NamespaceManager).Value;
                string mezo470 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:mező470", NamespaceManager).Value;


                this.DataConnections["UserData"].Execute();
                XPathNavigator nav = this.DataSources["UserData"].CreateNavigator();
                UserLista = nav.Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:NAME[../d:Sector = '" + csoport + "' and ../d:Status = 'Aktiv']", NamespaceManager);

                
                //DataConnections["AnDOC_Input"].Execute();
                DataConnections["AnDOC - Ügyféladatok"].Execute();
                DataConnections["AnDOC - Termékadatok"].Execute();
                DataConnections["AnDOC_Input"].Execute();
                DataConnections["Manager - Tárgy felsorolás"].Execute();
                DataConnections["Code32"].Execute();
                DataConnections["Postalista"].Execute();
                //DataConnections["Hetvegek"].Execute();

                Panaszlista = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Title[not(contains(./d:Megjegyzes, '"+ csoportSzeaz +"')) and contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "')]", NamespaceManager);
                CimzettLista = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Reklamacio_cimzettje[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "')]", NamespaceManager);
                TargyLista = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Reklamacio_targya[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "')]", NamespaceManager);
                ///dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Title[not(contains(../d:Megjegyzes, xdXDocument:get-DOM()/my:sajátMezők/my:Alapadatok/my:TMECS_SZEAZ)) and contains(../d:Vizsgalo_szervezet, xdXDocument:get-DOM()/my:sajátMezők/my:Alapadatok/my:TMECS_SZEAZ)]

                postaadatok = DataSources["Postalista"].CreateNavigator().Select("/dataroot/Postalista/Név[contains(../Igazgatóság, '" + mezo470 + "')]", NamespaceManager);

                PID = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Title[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_cimzett = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Reklamacio_cimzettje[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_targy = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Reklamacio_targya[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_vizsgalo = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Vizsgalo_szervezet[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_leiras = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Reklamacio_rovid_leirasa[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_hatarido = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Vizsgalati_hatarido[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_bejelent_datum = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Bejelentes_idopontja[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_UPR_datum = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:RKR_rogzites_datum[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_felvevo_posta = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Felvevo_szervezeti_egyseg[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_anonim = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Anonim_panasz[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);
                P_posta = DataSources["AnDOC_Input"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Posta_neve[contains(../d:Vizsgalo_szervezet, '" + csoportSzeaz + "') and ../d:Megjegyzes = '']", NamespaceManager);

                P_PID_ugyfel = DataSources["AnDOC - Ügyféladatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Title", NamespaceManager);
                P_ugyfel_neve = DataSources["AnDOC - Ügyféladatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Ugyfel_neve", NamespaceManager);
                P_ugyfel_cime = DataSources["AnDOC - Ügyféladatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Ugyfel_cime", NamespaceManager);
                P_ugyfel_tipus = DataSources["AnDOC - Ügyféladatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Ugyfel_tipusa", NamespaceManager);

                T_PID = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Title", NamespaceManager);
                T_termek = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Termek", NamespaceManager);
                T_termekid = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Kuldemenyazonosito", NamespaceManager);
                T_felvevo = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Felvevo_posta", NamespaceManager);
                T_rendposta = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Rendeltetesi_posta", NamespaceManager);
                T_felvdate = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Feladas_napja", NamespaceManager);
                T_suly = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Kuldemeny_sulya", NamespaceManager);
                T_dij = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Feladas_dija", NamespaceManager);
                T_kszolg = DataSources["AnDOC - Termékadatok"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Igenybevett_szolgaltatasok", NamespaceManager);

                targy_data = DataSources["Manager - Tárgy felsorolás"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Title[../d:Ervenyes = 'Igen']", NamespaceManager);
                hetvege = DataSources["Hetvegek"].CreateNavigator().Select("/dataroot/Hetvegek/Nap", NamespaceManager);
                kod32_adatok = DataSources["Code32"].CreateNavigator().Select("dataroot/Code32", NamespaceManager);
                

                // /dfs:myFields/dfs:dataFields/d:SharePointListItem_RW[d:Ervenyes = "Igen"]
                // /dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:Title[../d:Ervenyes = "Igen"]
                // /dataroot/Hetvegek/Nap //Hétvége figyelése
                // /dataroot/Code32
                Kod_32s_lista_letrehozasa();

                Feladatlap_fast form6 = new Feladatlap_fast();
                form6.ShowDialog();

                IP_nak_visszakuldott_adatok();
            }
            else
            {
                string nezet = "Feladatlap_2020";
                ViewInfos.SwitchView(nezet);
            }


        }

        public void CTRL112_74_Clicked(object sender, ClickedEventArgs e)
        {
            btn_startLap_Flap_Clicked(sender, e);
        }

        public void Kod_32s_lista_letrehozasa()
        {
            //XPathNodeIterator fo_kod = DataSources["Code32"].CreateNavigator().Select("dataroot/Code32/Kód", NamespaceManager);
            //XPathNodeIterator fo_kod_neve = DataSources["Code32"].CreateNavigator().Select("dataroot/Code32/Vizsgálat_besorolása", NamespaceManager);
            //XPathNodeIterator alkod = DataSources["Code32"].CreateNavigator().Select("dataroot/Code32/Alkód", NamespaceManager);
            //XPathNodeIterator alkod_neve = DataSources["Code32"].CreateNavigator().Select("dataroot/Code32/Vizsgálati_tevékenység", NamespaceManager);

            //int kodlista_db = 0;

            //foreach (object lista_db in fo_kod)
            //{
            //    ++kodlista_db;
            //}

            //string [,] kod32_adatok = new string[kodlista_db, 4];

            //for (int i = 0; i < kodlista_db; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {

            //        kod32_adatok[i, j] = fo_kod.Current..Current.f.InnerXml; //j.ToString();
            //        MessageBox.Show(kod32_adatok[i, j].ToString());
            //    }
            //}
            //MessageBox.Show(kod32_adatok.ToString());

            //kod32_adatok = DataSources["Code32"].CreateNavigator().Select("dataroot/Code32/Kód", NamespaceManager);
            //string vmi;
            //foreach (XmlNode TSTLISTA in kod32_adatok)
            //{
            //    vmi = TSTLISTA.FirstChild.InnerText;
            //    MessageBox.Show(vmi.ToString());
            //}

            //XmlNode tst = kod32_adatok.Current.Value;
            //XmlNodeList adat1 = XmlNode.

        }

        public void IP_nak_visszakuldott_adatok()
        {
            Feladatlap_fast form6 = new Feladatlap_fast();

            if (ugytipus == "Panasz vizsgálat")
            {
                //XPathNavigator IP_PID = MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Panasz_alapadat/my:Panasz_bejovo", NamespaceManager).SetValue(PID_bejovo);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgalo", NamespaceManager).SetValue(ugyintezo);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager).SetValue(posta);
                //MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő", NamespaceManager).SetValue(form6.dateTimePicker3.Text);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Instrukciók", NamespaceManager).SetValue(form6.textBox13.Text);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolatos_ügyintéző", NamespaceManager).SetValue(form6.comboBox8.Text);
                //MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager).SetValue();
                //MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager).SetValue();
                //MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager).SetValue();
            }
            else
            {
                
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgalo", NamespaceManager).SetValue(ugyintezo);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager).SetValue(posta);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgálati_határidő", NamespaceManager).SetValue(Vizsgalti_hatarido); //6tól ki van kapcsolva
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Instrukciók", NamespaceManager).SetValue(form6.textBox13.Text);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolatos_ügyintéző", NamespaceManager).SetValue(form6.comboBox8.Text);
                //MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager).SetValue();
                //MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager).SetValue();
                //MainDataSource.CreateNavigator().SelectSingleNode("", NamespaceManager).SetValue();
            }


        }

        //public void visszaeles_vizsgalat()
        //{
            
        //}

        public void Chk_visszaeles_Changed(object sender, XmlEventArgs e)   // Egyszerűsített vizsgálati jelentés BF-nek átadott visszaélésről \\
        {
            string visszaeles = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Chk_visszaeles", NamespaceManager).Value;

            if (visszaeles == "1")
            {
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Chk_visszaeles", NamespaceManager).SetValue("2");
                Visszaeleshez form7 = new Visszaeleshez();
                form7.ShowDialog();

                ugytipus = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Ügy_típusa", NamespaceManager).Value;
                string pszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Kapcsolódó_ügyirat", NamespaceManager).Value;

                if (ugytipus == "Panasz vizsgálat")
                {
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:BE_VizsgCel", NamespaceManager).SetValue("Az " + pszam + " számú panasz okainak feltárása, felelősök behatárolása.");
                }
                else
                {
                    MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:BE_VizsgCel", NamespaceManager).SetValue("Ellenőrzés során feltárt szabálytalanság okainak feltárása, felelősök behatárolása.");
                }

                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:BE_VizsgModszer", NamespaceManager).SetValue("A vizsgálat során a kapcsolódó adatok elemzése, okiratok felülvizsgálata, valamint az érdekelt munkavállaló jegyzőkönyvi nyilatkoztatása történt meg.");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:BE_VizsgTerjedelem", NamespaceManager).SetValue("A vizsgálat a jogsértés beismertetése érdekében a kapcsolódó okiratok, készletek felülvizsgálatára terjedt ki.");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:A_vizsgálat_tárgya/my:Vizsgálat_tárgya", NamespaceManager).SetValue(vizsg_elozmeny);
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:A_vizsgálat_tárgya/my:BE_Szabalyozasi_hatter", NamespaceManager).SetValue("BEK 9. sz. melléklet.");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Megállapítások/my:BE_Megállapítások/my:csoport127/my:mező495", NamespaceManager).SetValue("Az érdekelt munkavállaló - " + vizsg_nev + " " + vizsg_munkakor + " - a vizsgálat során a jogsértést jegyzőkönyvi nyilatkozatában elismerte.");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Megállapítások/my:BE_Megállapítások/my:csoport127/my:mező496", NamespaceManager).SetValue("Nyilatkozat eredménye");
                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Jelentés/my:Vizsgálati_jelentés/my:Report/my:Munkavállalók_felelőssége/my:Felelősség", NamespaceManager).SetValue(String.Concat(vizsg_elozmeny, System.Environment.NewLine, "Az érdekelt munkavállaló a vizsgálat során a jogsértést jegyzőkönyvi nyilatkozatában elismerte.", System.Environment.NewLine, "Tekintettel arra, hogy az összeg a szabályozásban rögzített felelősségi köröknek megfelelő összeghatárt meghaladta, az ügy további vizsgálatra a Biztonsági Főigazgatóság munkatársa részére „Átadási jegyzőkönyv” kitöltése mellett átadásra került.")); //vizsg_elozmeny + System.Environment.NewLine + "\r\n\nAz érdekelt munkavállaló a vizsgálat során a jogsértést jegyzőkönyvi nyilatkozatában elismerte." + "\r\n\n" + "Tekintettel arra, hogy az összeg a szabályozásban rögzített felelősségi köröknek megfelelő összeghatárt meghaladta, az ügy további vizsgálatra a Biztonsági Főigazgatóság munkatársa részére „Átadási jegyzőkönyv” kitöltése mellett átadásra került.");
                
            }

        }

        private string ReadSignature()      // aláírás kigyűjtése az Outlook-ból
        {
            string appDataDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Signatures";
            string signature = string.Empty;
            DirectoryInfo diInfo = new DirectoryInfo(appDataDir);

            if (diInfo.Exists)
            {
                FileInfo[] fiSignature = diInfo.GetFiles("*.htm");

                if (fiSignature.Length > 0)
                {
                    StreamReader sr = new StreamReader(fiSignature[0].FullName, System.Text.Encoding.Default);
                    signature = sr.ReadToEnd();

                    if (!string.IsNullOrEmpty(signature))
                    {
                        string fileName = fiSignature[0].Name.Replace(fiSignature[0].Extension, string.Empty);
                        signature = signature.Replace(fileName + "_files/", appDataDir + "/" + fileName + "_files/");
                    }
                }
            }
            return signature;
        }

        public void email_kuldes()
        {
            XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
            Logba.SetValue("E-mail küldés kezdeményezése");

            try
            {
                cimzettek_begyujtese(); 
                ReadSignature();

                Microsoft.Office.Interop.Outlook.Application outlookapp = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mailItem = (Microsoft.Office.Interop.Outlook.MailItem)outlookapp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                int helyzet = 1;
                string iktatoszam = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Ügyirat_adatai/my:Iktatószám", NamespaceManager).Value;//.Replace("/", "_");
                string mappa = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Mellékletek/my:mező476", NamespaceManager).Value; //@"C:\Adatszolgáltatás\Vizsgálatok\" + iktatoszam + "\\";
                string posta = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Posta_neve", NamespaceManager).Value;
                //string cimzettek = cimzettek;// "Csorba-Papp Ildikó; Papp Éva; Németh András; Filó Norbert";

                try
                {
                        string[] fileLista = Directory.GetFiles(mappa);
                    

                    mailItem.Subject = "Intézkedés kérése_" + posta + " (" + iktatoszam + ")";// posta neve_iktatószám";
                    mailItem.To = cimzettek;
                    mailItem.BodyFormat = OlBodyFormat.olFormatHTML;
                    mailItem.HTMLBody = "<h3 style=font-family:Arial;><strong>Tisztelt Vezető Asszony / Úr!</strong></h3><p></p><p style=font-family:Arial;>Tájékoztatom, hogy a Működésellenőrzési Központ munkatársa vizsgálatot folytatott " + posta + "-t érintően.</p><p style=font-family:Arial;>A vizsgálat eredményét, és javaslatainkat a csatolt vizsgálati jelentés és mellékletei, valamint csatolt levelünk tartalmazzák, melyek alapján kérem szíves intézkedését!</p><p style=font-family:Arial;>Intézkedését köszönöm!</p><p>&nbsp;</p>" + ReadSignature();
                    
                    mailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceNormal;
                    mailItem.Display(false);

                    foreach (string lista in fileLista)
                    {
                        helyzet++;
                        string név = lista.Remove(0, lista.LastIndexOf("\\") + 1);
                        mailItem.Attachments.Add(lista, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, helyzet, név);
                    }
                }
                catch (System.Exception Ex)
                {
                    Logba.SetValue("E-mail küldés - hiányzó állományok miatt - megszakítva");
                    DialogResult dr_hiba02 = MessageBox.Show("Az érintett ügyhöz nem találhatók csatolható állományok!\n\nKérem, hogy a vizsgálati jelentés, a mellékletek, valamint a szükséges levelekből PDF állomány elkészítését elvégezni szíveskedj!", "Adatküldési hiba: Hiányzó állományok!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (System.Exception ex)
            {
                Logba.SetValue("E-mail küldés - egyéb ok miatt - megszakítva");
                MessageBox.Show(ex.ToString(), "Hiba!");
            }
        }
                   

        public void cimzettek_begyujtese()  // A levelek címzetjeit összegyűjti, hogy majd az Outlook-nak át tudja adni
        {
            cimzettek = "";
            int munkaltato_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport6/my:Levél_Munkáltató/my:csoport17/my:csoport18/my:mező29", NamespaceManager).Count;
            int TIG_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport10/my:Levél_TerIg/my:csoport19/my:csoport20/my:mező37", NamespaceManager).Count;          //40
            int logisztika_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Logisztika/my:Levél_logisztika/my:csoport73/my:csoport74/my:mező236", NamespaceManager).Count; //39
            int egyeb_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport31/my:Levél_Egyebek/my:csoport33/my:csoport34/my:mező80", NamespaceManager).Count;      //83
            int TBO_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Levél_BiztonságTerület/my:csoport15/my:csoport16/my:mező24", NamespaceManager).Count;            //27
            int BF_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Level_Biztonsag/my:csoport90/my:csoport91/my:csoport92/my:mező337", NamespaceManager).Count;      //340
            int Jog_ter_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport5/my:Levél_Jog1/my:csoport23/my:csoport24/my:mező49", NamespaceManager).Count;        //52
            int Jog_ig_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Levél_Jog/my:csoport21/my:csoport22/my:mező42", NamespaceManager).Count;                      //45
            int PEK_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport9/my:Levél_PEK/my:csoport27/my:csoport28/my:mező64", NamespaceManager).Count;             //67
            int HI_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:Hálózat/my:Levél_Hálózat/my:csoport77/my:csoport78/my:mező244", NamespaceManager).Count;          //247
            int BE_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport48/my:Levél_EMFO/my:csoport50/my:Levél_Janicsák/my:mező185", NamespaceManager).Count;      //188
            int VIG_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport54/my:Levél_VIG/my:csoport56/my:csoport57/my:mező194", NamespaceManager).Count;           //197
            int UI_sorok = MainDataSource.CreateNavigator().Select("/my:sajátMezők/my:csoport12/my:Levél_ÜSZK/my:csoport25/my:csoport26/my:mező56", NamespaceManager).Count;            //56

            XPathNavigator munkaltatoi_level;
            XPathNavigator munkaltatoi_lev_date;
            XPathNavigator TIG_levél;
            XPathNavigator TIG_lev_date;
            XPathNavigator Logisztika_level;
            XPathNavigator Logisztika_lev_date;
            XPathNavigator Egyeb_level;
            XPathNavigator Egyeb_lev_date;
            XPathNavigator TBO_level;
            XPathNavigator TBO_lev_date;
            XPathNavigator BF_level;
            XPathNavigator BF_lev_date;
            XPathNavigator Jog_terulet_level;
            XPathNavigator Jog_terulet_lev_date;
            XPathNavigator Jog_level;
            XPathNavigator Jog_lev_date;
            XPathNavigator PEK_level;
            XPathNavigator PEK_lev_date;
            XPathNavigator HI_level;
            XPathNavigator HI_lev_date;
            XPathNavigator BE_level;
            XPathNavigator BE_lev_date;
            XPathNavigator VIG_level;
            XPathNavigator VIG_lev_date;
            XPathNavigator UI_level;
            XPathNavigator UI_lev_date;

            try
            {

                for (int i = 1; i <= munkaltato_sorok; i++)
                {
                    munkaltatoi_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport6/my:Levél_Munkáltató[" + i + "]/my:csoport17/my:csoport18/my:mező29", NamespaceManager);
                    munkaltatoi_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport6/my:Levél_Munkáltató[" + i + "]/my:csoport17/my:csoport18/my:mező32", NamespaceManager);

                    if (munkaltatoi_lev_date.ToString().Length > 0 && munkaltatoi_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", munkaltatoi_level.ToString());

                        level = munkaltatoi_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport6/my:Levél_Munkáltató[" + i + "]/my:csoport17/my:csoport18/my:mező311", NamespaceManager);
                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int j = 1; j <= TIG_sorok; j++)
                {
                    TIG_levél = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport10/my:Levél_TerIg[" + j + "]/my:csoport19/my:csoport20/my:mező37", NamespaceManager);
                    TIG_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport10/my:Levél_TerIg[" + j + "]/my:csoport19/my:csoport20/my:mező40", NamespaceManager);

                    if (TIG_lev_date.ToString().Length > 0 && TIG_levél.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", TIG_levél.ToString());

                        level = TIG_levél.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport10/my:Levél_TerIg[" + j + "]/my:csoport19/my:csoport20/my:mező312", NamespaceManager);
                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int k = 1; k <= egyeb_sorok; k++)
                {
                    Egyeb_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport31/my:Levél_Egyebek[" + k + "]/my:csoport33/my:csoport34/my:mező80", NamespaceManager);
                    Egyeb_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport31/my:Levél_Egyebek[" + k + "]/my:csoport33/my:csoport34/my:mező83", NamespaceManager);

                    if (Egyeb_lev_date.ToString().Length > 0 && Egyeb_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", Egyeb_level.ToString());

                        level = Egyeb_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport31/my:Levél_Egyebek[" + k + "]/my:csoport33/my:csoport34/my:mező324", NamespaceManager);
                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int l = 1; l <= logisztika_sorok; l++)
                {
                    Logisztika_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Logisztika/my:Levél_logisztika[" + l + "]/my:csoport73/my:csoport74/my:mező236", NamespaceManager);
                    Logisztika_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Logisztika/my:Levél_logisztika[" + l + "]/my:csoport73/my:csoport74/my:mező239", NamespaceManager);

                    if (Logisztika_lev_date.ToString().Length > 0 && Logisztika_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", Logisztika_level.ToString());

                        level = Logisztika_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Logisztika/my:Levél_logisztika[" + l + "]/my:csoport73/my:csoport74/my:mező316", NamespaceManager);
                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int m = 1; m <= TBO_sorok; m++)
                {
                    TBO_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Levél_BiztonságTerület[" + m + "]/my:csoport15/my:csoport16/my:mező24", NamespaceManager);
                    TBO_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Levél_BiztonságTerület[" + m + "]/my:csoport15/my:csoport16/my:mező27", NamespaceManager);

                    if (TBO_lev_date.ToString().Length > 0 && TBO_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", TBO_level.ToString());
                        level = TBO_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Levél_BiztonságTerület[" + m + "]/my:csoport15/my:csoport16/my:mező314", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int n = 1; n <= BF_sorok; n++)
                {
                    BF_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Level_Biztonsag/my:csoport90[" + n + "]/my:csoport91/my:csoport92/my:mező337", NamespaceManager);
                    BF_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Level_Biztonsag/my:csoport90[" + n + "]/my:csoport91/my:csoport92/my:mező340", NamespaceManager);

                    if (BF_lev_date.ToString().Length > 0 && BF_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", BF_level.ToString());
                        level = BF_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Level_Biztonsag/my:csoport90[" + n + "]/my:csoport91/my:csoport92/my:mező341", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int o = 1; o <= Jog_ter_sorok; o++)
                {
                    Jog_terulet_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport5/my:Levél_Jog1[" + o + "]/my:csoport23/my:csoport24/my:mező49", NamespaceManager);
                    Jog_terulet_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport5/my:Levél_Jog1[" + o + "]/my:csoport23/my:csoport24/my:mező52", NamespaceManager);

                    if (Jog_terulet_lev_date.ToString().Length > 0 && Jog_terulet_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", Jog_terulet_level.ToString());
                        level = Jog_terulet_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport5/my:Levél_Jog1[" + o + "]/my:csoport23/my:csoport24/my:mező320", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int p = 1; p <= Jog_ig_sorok; p++)
                {
                    Jog_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Levél_Jog/my:csoport21/my:csoport22/my:mező42", NamespaceManager);
                    Jog_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Levél_Jog/my:csoport21/my:csoport22/my:mező45", NamespaceManager);

                    if (Jog_lev_date.ToString().Length > 0 && Jog_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", Jog_level.ToString());
                        level = Jog_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Levél_Jog/my:csoport21/my:csoport22/my:mező319", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int q = 1; q <= HI_sorok; q++)
                {
                    HI_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Hálózat/my:Levél_Hálózat/my:csoport77/my:csoport78/my:mező244", NamespaceManager);
                    HI_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Hálózat/my:Levél_Hálózat/my:csoport77/my:csoport78/my:mező247", NamespaceManager);

                    if (HI_lev_date.ToString().Length > 0 && HI_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", HI_level.ToString());
                        level = HI_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("my:sajátMezők/my:Hálózat/my:Levél_Hálózat/my:csoport77/my:csoport78/my:mező315", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int r = 1; r <= PEK_sorok; r++)
                {
                    PEK_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport9/my:Levél_PEK[" + r + "]/my:csoport27/my:csoport28/my:mező64", NamespaceManager);
                    PEK_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport9/my:Levél_PEK[" + r + "]/my:csoport27/my:csoport28/my:mező67", NamespaceManager);

                    if (PEK_lev_date.ToString().Length >0 && PEK_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", PEK_level.ToString());
                        level = PEK_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport9/my:Levél_PEK[" + r + "]/my:csoport27/my:csoport28/my:mező322", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int s = 1; s <= BE_sorok; s++)
                {
                    BE_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport48/my:Levél_EMFO[" + s + "]/my:csoport50/my:Levél_Janicsák/my:mező185", NamespaceManager);
                    BE_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport48/my:Levél_EMFO[" + s + "]/my:csoport50/my:Levél_Janicsák/my:mező188", NamespaceManager);

                    if (BE_lev_date.ToString().Length > 0 && BE_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", BE_level.ToString());
                        level = BE_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport48/my:Levél_EMFO[" + s + "]/my:csoport50/my:Levél_Janicsák/my:mező318", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int t = 1; t <= VIG_sorok; t++)
                {
                    VIG_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport54/my:Levél_VIG[" + t + "]/my:csoport56/my:csoport57/my:mező194", NamespaceManager);
                    VIG_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport54/my:Levél_VIG[" + t + "]/my:csoport56/my:csoport57/my:mező197", NamespaceManager);

                    if (VIG_lev_date.ToString().Length > 0 && VIG_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", VIG_level.ToString());
                        level = VIG_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport54/my:Levél_VIG[" + t + "]/my:csoport56/my:csoport57/my:mező317", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }

                for (int u = 1; u < UI_sorok; u++)
                {
                    UI_level = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport12/my:Levél_ÜSZK[" + u + "]/my:csoport25/my:csoport26/my:mező56", NamespaceManager);
                    UI_lev_date = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport12/my:Levél_ÜSZK[" + u + "]/my:csoport25/my:csoport26/my:mező59", NamespaceManager);

                    if (UI_lev_date.ToString().Length > 0 && UI_level.ToString().Length > 0)
                    {
                        cimzettek = string.Concat(cimzettek, "; ", UI_level.ToString());
                        level = UI_level.ToString();
                        iratjegyzekbe = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:csoport54/my:Levél_VIG[" + u + "]/my:csoport56/my:csoport57/my:mező321", NamespaceManager);

                        if (iratjegyzekbe.ToString().Length == 0)
                        {
                            iratjegyekbe_iras();
                        }
                    }
                }


                if (cimzettek.Substring(0, 1) == ";")
                {
                    cimzettek = cimzettek.Remove(0, 2);
                }

                try
                {
                    XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
                    Logba.SetValue("Automatikus mentés (E-mailküldés után)");

                    FileSubmitConnection fc = DataConnections["UpLoad"] as FileSubmitConnection;    // adatok Sharepoint-ba küldéshez deklaráció
                    fc.Execute();                                                                   // adsatok SharePoint-ba mentése
                    DialogResult dr_m01 = MessageBox.Show("Az automatikus mentés sikeresen megtörtént", "Sikeres adatmentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    DialogResult dr_m02 = MessageBox.Show("Az automatikus mentés nem sikerült!", "Sikertelen adatmentés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (System.Exception Ex)
            {
               DialogResult dr_hiba_emil = MessageBox.Show(Ex.InnerException.ToString(), "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void iratjegyekbe_iras() // ------------------------- DateTimePicker vezérlőbe így lehet dátumot írni!!!! ----------------------- \\
        {
            // az iratjegyzekbe public xpathnavigatorként root-ban definiálva
            if (iratjegyzekbe.MoveToAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance"))
                iratjegyzekbe.DeleteSelf();
            iratjegyzekbe.SetValue(Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"));

            XPathNavigator Logba = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager);
            Logba.SetValue("Levélküldés dátumának automatikus bejegyzése (" + level + ")");

        }

        public void btn_emailKuldes_Clicked(object sender, ClickedEventArgs e)
        {
            email_kuldes();
            //cimzettek_begyujtese();
            //MessageBox.Show(cimzettek);
        }

        public void webTesztek()        // Szakmai felkészültség mérése, adatbázisból véletlenszerűen kiválasztott 3 db kérdéssel
        {
            DataConnections["Manager_KerdesLista"].Execute();
            webTesztek_kerdesek_betoltese();

            if (webTeszt_state == 1)        //  Csak akkor indul el, ha talál betölthető kérdéssort
            {
                kerdes1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:K1", NamespaceManager).Value;
                k1V1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k1V1", NamespaceManager).Value;
                k1V2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k1V2", NamespaceManager).Value;
                k1V3 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k1V3", NamespaceManager).Value;
                k1_helyesValasz = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k1_helyes", NamespaceManager).Value;

                kerdes2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:K2", NamespaceManager).Value;
                k2V1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k2V1", NamespaceManager).Value;
                k2V2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k2V2", NamespaceManager).Value;
                k2V3 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k2V3", NamespaceManager).Value;
                k2_helyesValasz = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k2_helyes", NamespaceManager).Value;

                kerdes3 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:K3", NamespaceManager).Value;
                k3V1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k3V1", NamespaceManager).Value;
                k3V2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k3V2", NamespaceManager).Value;
                k3V3 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k3V3", NamespaceManager).Value;
                k3_heyesValasz = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:k3_helyes", NamespaceManager).Value;

                DialogResult dr_webteszt_mesage = MessageBox.Show("Mielőtt nekiállnál a vizsgálati jelentéssel kapcsoatos feladatok elvégzéséhez, néhány kérdésre választ kell adnod!\n\nAz 'OK' gomb megnyomását követően 3 kérdést fogsz látni, kérdésenként legfeljebb 3 válaszlehetőséggel.\n\nAz egyes kérdések között a tesztlap alján található 'Előző' / 'Következő' gombokra történő kattintással lehet navigálni.\n\nA kérdések megválaszolására 3 perc áll rendelkezésedre, melynek letelte után az alkalmazás automatikusan bezárja a tesztlapot.\n\nTermészetesen a 'Befejezés' gombra történő kattintással a válaszadást hamarabb is befejezheted!\n\nVezetői döntés értelmében, a válaszokról negyedéves értékelés készül!", "Figyelem!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ugyintezo = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:Vizsgalo", NamespaceManager).Value;
                csoport = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:Alapadatok/my:TMECS", NamespaceManager).Value;

                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager).SetValue("WebTeszt indítása");

                WebTeszt webTeszt = new WebTeszt();
                webTeszt.ShowDialog();

                MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:BackGround/my:Logba_Esemeny", NamespaceManager).SetValue("WebTeszt befejezése");
            }
        }

        public void webTesztek_kerdesek_betoltese()
        {
            XPathNodeIterator kerdesIdLista = DataSources["Manager_KerdesLista"].CreateNavigator().Select("/dfs:myFields/dfs:dataFields/d:SharePointListItem_RW/d:ID[../d:Valaszthato = string(true())]", NamespaceManager);
            kerdesID1 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:kerdesID_1", NamespaceManager);
            kerdesID2 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:kerdesID_2", NamespaceManager);
            kerdesID3 = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:kerdesID_3", NamespaceManager);
            XPathNavigator lefutott = MainDataSource.CreateNavigator().SelectSingleNode("/my:sajátMezők/my:WebTesztek/my:Futtatva", NamespaceManager);
            string toltheto;

            
            ArrayList idNumbers = new ArrayList();

            foreach (object kerdesID in kerdesIdLista)      // Egy tömbbe rendezi a kérdesek egyedi azonosítóját
            {
                idNumbers.Add(Convert.ToInt32(kerdesID.ToString()));
            }

            if (idNumbers.Count > 0)
            {
                webTeszt_state = 1;
                idNumbers.Insert(0, 1000);

                int[] betoltendoKerdesek = new int[idNumbers.Count + 1];

                try
                {
                    toltheto = "Igen";
                    for (int i = 0; i < idNumbers.Count - 1; i++)
                    {
                        Random veletlemSzam = new Random();
                        int elemID = veletlemSzam.Next(0, idNumbers.Count + 1); // véletlen szám, a tömb egyik eleme, melynek értéke lesz majd a kérdés sorszáma

                        for (int j = 0; j < betoltendoKerdesek.Length; )
                        {
                            if (betoltendoKerdesek[j] != elemID)
                            {
                                toltheto = "Igen";
                                ++j;
                            }
                            else
                            {
                                toltheto = "Nem";
                                elemID = veletlemSzam.Next(0, idNumbers.Count + 1);
                                j = 0;
                            }
                        }

                        if (toltheto == "Igen")
                        {
                            betoltendoKerdesek[i] = elemID;
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    kerdesID1.SetValue(idNumbers[betoltendoKerdesek[0]].ToString());
                    kerdesID2.SetValue(idNumbers[betoltendoKerdesek[1]].ToString());
                    kerdesID3.SetValue(idNumbers[betoltendoKerdesek[2]].ToString());
                    //lefutott.SetValue(futtatva.ToString());
                }
                catch
                {
                    webTesztek_kerdesek_betoltese();
                }
            }
            else
            {
                DialogResult dr_webteszt_02 = MessageBox.Show("Nem találtam betölthető kérdéssort, ezért a tesztírás elmarad!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                webTeszt_state = 0;
            }
        }

        public void pdf_keszites()
        {

        }

        public void btn_joker_Clicked(object sender, ClickedEventArgs e)
        {
            webTesztek();
        }
    }     
}
