using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatyczneBackupowanie
{
    [Serializable]
    public class Ustawienia
    {
        public bool Zdjecia;
        public bool Muzyka;
        public bool Filmy;
        public bool Wlasne;
        public string Zrodlo;
        public string Cel;
        public Mod Mod;
    }

    public enum Mod
    {
        KOPIUJ,
        PRZENOS
    }
}
