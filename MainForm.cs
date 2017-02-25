using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace AutomatyczneBackupowanie
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Ustawienia ustawienia = new Ustawienia();
        Rozszerzenia rozszerzenia = new Rozszerzenia();

        delegate void Pocztek();
        delegate void Koniec();
        delegate void Srodek();
        private void button2_Click(object sender, EventArgs e)
        {
            Konfiguracja konfiguracja = new Konfiguracja();
            konfiguracja.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("ustawienia.dat"))
            {
                ustawienia = Serializer.DeserializujUstawienia();
            }
            else
            {
                MessageBox.Show("brak zdefiniowanej konfiguracji!");
                return;
            }
            if(File.Exists("rozszerzenia.dat"))
            {
                rozszerzenia = Serializer.DeserializujRozszerzenia();
            }
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            label2.Visible = true;
            label2.Text = "Obliczam";
            label3.Text = "0";
            label2.Refresh();
            int ilosc = obliczaj(ustawienia.Zrodlo);
            progressBar1.Maximum = ilosc;
            label3.Visible = true;
            label4.Visible = true;
            label5.Text = ilosc.ToString();
            label5.Visible = true;
            label5.Refresh();
            Thread t = new Thread(zacznij);
            t.Start();
        }

        void Poczatkowe()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new Pocztek(Poczatkowe));
            }
            else
            {
                switch (ustawienia.Mod)
                {
                    case Mod.KOPIUJ:
                        {
                            label2.Text = "kopiuję: ";
                            break;
                        }
                    case Mod.PRZENOS:
                        {
                            label2.Text = "przenoszę: ";
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
                label2.Refresh();
            }
        }

        void Koncowe()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Koniec(Koncowe));
            }
            else
            {
                switch (ustawienia.Mod)
                {
                    case Mod.KOPIUJ:
                        {
                            label2.Text = "skopiowano: ";
                            break;
                        }
                    case Mod.PRZENOS:
                        {
                            label2.Text = "przeniesiono: ";
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
                label2.Refresh();
            }
        }

        void Srodkowe()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new Srodek(Srodkowe));
            }
            else
            {
                progressBar1.Value++;
                label3.Text = (int.Parse(label3.Text) + 1).ToString();
                progressBar1.Refresh();
                label3.Refresh();
            }
        }

        void zacznij()
        {
            Poczatkowe();            
            przetwarzaj(ustawienia.Zrodlo);
            Koncowe();
        }

        void przetwarzaj(string path)
        {
            try
            {

                string[] pliki = Directory.GetFiles(path);
                string[] podkatalogi = Directory.GetDirectories(path);
                foreach (string plik in pliki)
                {
                    if (plikSpelniaKryteria(plik))
                    {
                        Srodkowe();
                        try
                        {
                            przetwarzajPlik(plik);
                        }
                        catch
                        {

                        }
                    }
                }
                foreach (string katalog in podkatalogi)
                {
                    try
                    {

                        przetwarzaj(katalog);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
        }

        void przetwarzajPlik(string path)
        {
            string cel = path.Replace(ustawienia.Zrodlo, ustawienia.Cel);
            if(!Directory.GetParent(cel).Exists)
            {
                Directory.GetParent(cel).Create();
            }
            switch (ustawienia.Mod)
            {
                case Mod.KOPIUJ:
                    {
                        dodajdoLogu("kopiuję: " + path + " do: " + cel);
                        if (!File.Exists(cel))
                        {
                            File.Copy(path, cel);
                            dodajdoLogu("skopiowany");
                        }
                        else
                        {
                            dodajdoLogu("nieskopiowany");
                        }
                        return;
                    }
                case Mod.PRZENOS:
                    {
                        dodajdoLogu("przenoszę: " + path + " do: " + cel);
                        if (!File.Exists(cel))
                        {
                            File.Move(path, cel);
                            dodajdoLogu("przeniesiony");
                        }
                        else
                        {
                            dodajdoLogu("nieprzeniesiony");
                        }
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        bool plikSpelniaKryteria(string plik)
        {
            string rozszerzeniePliku;
            try
            {
                rozszerzeniePliku = "." + plik.Split('.').Last().ToLower();
            }
            catch
            {
                return false;
            }
            if(ustawienia.Zdjecia)
            {
                if(rozszerzenia.Zdjecia.Contains(rozszerzeniePliku))
                {
                    return true;
                }
            }
            if (ustawienia.Muzyka)
            {
                if (rozszerzenia.Muzyka.Contains(rozszerzeniePliku))
                {
                    return true;
                }
            }
            if (ustawienia.Filmy)
            {
                if (rozszerzenia.Filmy.Contains(rozszerzeniePliku))
                {
                    return true;
                }
            }
            if (ustawienia.Wlasne)
            {
                if (rozszerzenia.Wlasne.Contains(rozszerzeniePliku))
                {
                    return true;
                }
            }
            return false;
        }

        int obliczaj(string path)
        {
            int wynik = 0;
            try
            {
                string[] pliki = Directory.GetFiles(path);
                string[] podkatalogi = Directory.GetDirectories(path);
                foreach (string plik in pliki)
                {
                    try
                    {
                        if (plikSpelniaKryteria(plik))
                        {
                            wynik++;
                        }
                    }
                    catch
                    {

                    }
                }
                foreach (string katalog in podkatalogi)
                {
                    try
                    {
                        wynik += obliczaj(katalog);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
            return wynik;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(!File.Exists("log.csv"))
            {
                dodajdoLogu("utworzono log");
            }
            label2.Visible = label3.Visible = label4.Visible = label5.Visible = false;
        }

        void dodajdoLogu(string x)
        {
            File.AppendAllText("log.csv", DateTime.Now.ToString() + " :    " + x + "\n");
        }
    }
}
