using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AutomatyczneBackupowanie
{
    class Serializer
    {

        public static void Serializuj(Ustawienia u)
        {
            FileStream fs = new FileStream("ustawienia.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, u);
            fs.Close();
        }
        public static void Serializuj(Rozszerzenia r)
        {
            FileStream fs = new FileStream("rozszerzenia.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, r);
            fs.Close();
        }

        public static Ustawienia DeserializujUstawienia()
        {
            FileStream fs = new FileStream("ustawienia.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Ustawienia wynik = (Ustawienia)bf.Deserialize(fs);
            fs.Close();
            return wynik;
        }

        public static Rozszerzenia DeserializujRozszerzenia()
        {
            FileStream fs = new FileStream("rozszerzenia.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Rozszerzenia wynik = (Rozszerzenia)bf.Deserialize(fs);
            fs.Close();
            return wynik;
        }
    }
}
