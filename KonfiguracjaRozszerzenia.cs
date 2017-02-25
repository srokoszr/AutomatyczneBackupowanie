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
    public partial class KonfiguracjaRozszerzenia : Form
    {
        public KonfiguracjaRozszerzenia()
        {
            InitializeComponent();
        }

        Rozszerzenia rozszerzenia;

        private void button1_Click(object sender, EventArgs e)
        {
            rozszerzenia.Zdjecia = richTextBox1.Text.TrimEnd(new char[] { '\n' }).ToLower().Split('\n');
            rozszerzenia.Muzyka = richTextBox2.Text.TrimEnd(new char[] { '\n' }).ToLower().Split('\n');
            rozszerzenia.Filmy = richTextBox3.Text.TrimEnd(new char[] { '\n' }).ToLower().Split('\n');
            rozszerzenia.Wlasne = richTextBox4.Text.TrimEnd(new char[] { '\n' }).ToLower().Split('\n');
            Serializer.Serializuj(rozszerzenia);
            this.Close();
        }

        private void KonfiguracjaRozszerzenia_Load(object sender, EventArgs e)
        {
            if (File.Exists("rozszerzenia.dat"))
            {
                rozszerzenia = Serializer.DeserializujRozszerzenia();
            }
            else
            {
                rozszerzenia = new Rozszerzenia();
            }
            foreach(string linia in rozszerzenia.Zdjecia)
            {
                richTextBox1.AppendText(linia + "\n");
            }
            foreach (string linia in rozszerzenia.Muzyka)
            {
                richTextBox2.AppendText(linia + "\n");
            }
            foreach (string linia in rozszerzenia.Filmy)
            {
                richTextBox3.AppendText(linia + "\n");
            }
            foreach (string linia in rozszerzenia.Wlasne)
            {
                richTextBox4.AppendText(linia + "\n");
            }
        }
    }
}
