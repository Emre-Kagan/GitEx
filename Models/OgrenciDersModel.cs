using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciKayit.Models
{
    public class OgrenciDersModel
    {
        public string DersAdi { get; set; }
        public string OgrenciNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Yil { get; set; }
        public string Donem { get; set; }
        public Nullable<int> Vize { get; set; }
        public Nullable<int> MazeretVize { get; set; }
        public Nullable<int> Final { get; set; }
        public Nullable<int> Butunleme { get; set; }
        public Nullable<int> TekDers { get; set; }
        public Nullable<int> BasariNotu { get; set; }
        public string HarfNotu { get; set; }

    }
}