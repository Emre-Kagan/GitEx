using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OgrenciKayit.Models
{
    public class TranskriptModel
    {
        public List<OdModel> OgrenciDersList { get; set; }
        public List<YilDonem> YilDonemList { get; set; }
        public OgrTrans OgrTrans { get; set; }

    }
    public class OdModel
    {

        public string DrsAd { get; set; }
        public string DrsKod { get; set; }
        public double DrsKredi { get; set; }
        public double DrsEcts { get; set; }
        public string HrfNotu { get; set; }
        public string Yil { get; set; }
        public string Donem { get; set; }

        

    }
    public class YilDonem
    {
        public string Yil { get; set; }
        public string Donem { get; set; }

    }
    public class OgrTrans
    {
        public string TcKimlikNo { get; set; }
        public string OgrenciNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> KayitlanmaYili { get; set; }

        public int OrganizasyonID { get; set; }

    }

}