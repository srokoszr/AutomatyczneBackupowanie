using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatyczneBackupowanie
{
    [Serializable]
    class Rozszerzenia
    {
        public string[] Zdjecia;
        public string[] Muzyka;
        public string[] Filmy;
        public string[] Wlasne;

        public Rozszerzenia()
        {
            Zdjecia = new string[]
            {
                ".bmp",
                ".fpx",
                ".gif",
                ".ico",
                ".img",
                ".jp2",
                ".jpg",
                ".pcd",
                ".pgf",
                ".png",
                ".ras",
                ".raw",
                ".tif",
                ".tiff",
                ".xcf",
                ".svg"
            };

            Muzyka = new string[]
            {
                ".3gp",
                ".aac",
                ".flac",
                ".m4p",
                ".mp3",
                ".wav",
                ".wma"
            };

            Filmy = new string[]
            {
                ".3gp",
                ".asf",
                ".avi",
                ".dv",
                ".dvd",
                ".flv",
                ".m2ts",
                ".mkv",
                ".mov",
                ".mp4",
                ".mpg",
                ".ogg",
                ".smv",
                ".svcd",
                ".ts",
                ".wmv",
                ".vcd"
            };

            Wlasne = new string[] {  };
        }
    }
}
