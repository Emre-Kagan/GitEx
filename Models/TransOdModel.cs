using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciKayit.Models
{
    public class TransOdModel
    {
        public int OgrenciID { get; set; }
        public List<Dersler> Dersler { get; set; }
        public List<Ogrenci> Ogrenciler { get; set; }
        public List<OgrenciDers> OgrDers { get; set; }


    }
}