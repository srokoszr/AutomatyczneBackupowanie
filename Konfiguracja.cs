using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutomatyczneBackupowanie
{
    public partial class Konfiguracja : Form
    {
        public Konfiguracja()
        {
            InitializeComponent();
        }

        Ustawienia ustawienia = new Ustawienia();

        private void Konfiguracja_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            if (File.Exists("ustawienia.dat"))
            {
                ustawienia = Serializer.DeserializujUstawienia();
                checkBox1.Checked = ustawienia.Zdjecia;
                checkBox2.Checked = ustawienia.Muzyka;
                checkBox3.Checked = ustawienia.Filmy;
                checkBox4.Checked = ustawienia.Wlasne;
                textBox1.Text = ustawienia.Zrodlo;
                textBox2.Text = ustawienia.Cel;
                radioButton1.Checked = ustawienia.Mod == Mod.KOPIUJ;
                radioButton2.Checked = ustawienia.Mod == Mod.PRZENOS;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ustawienia.Zdjecia = checkBox1.Checked;
            ustawienia.Muzyka = checkBox2.Checked;
            ustawienia.Filmy = checkBox3.Checked;
            ustawienia.Wlasne = checkBox4.Checked;
            ustawienia.Zrodlo = textBox1.Text;
            ustawienia.Cel = textBox2.Text;
            ustawienia.Mod = radioButton1.Checked ? Mod.KOPIUJ : Mod.PRZENOS;
            if(!textBox1.Text.Contains(":\\") || !textBox2.Text.Contains(":\\"))
            {
                MessageBox.Show("Niewłasciwe formaty ściezek, mają zawierać ':\'");
                return;
            }
            if(textBox1.Text.Contains(textBox2.Text) || textBox2.Text.Contains(textBox1.Text))
            {
                MessageBox.Show("Wykryto groźne zależności rekurencyjne!");
                return;
            }
            Serializer.Serializuj(ustawienia);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KonfiguracjaRozszerzenia konfiguracjaRozszerzenia = new KonfiguracjaRozszerzenia();
            konfiguracjaRozszerzenia.ShowDialog();
        }

        string przegladaj()
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            string wynik = folderBrowserDialog1.SelectedPath;
            return wynik;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = przegladaj();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = przegladaj();
        }
    }
}
