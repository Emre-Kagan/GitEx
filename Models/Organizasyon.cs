//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OgrenciKayit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organizasyon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organizasyon()
        {
            this.Organizasyon1 = new HashSet<Organizasyon>();
            this.Ogrenci = new HashSet<Ogrenci>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Ad { get; set; }
        public string KisaAd { get; set; }
        public bool Aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Organizasyon> Organizasyon1 { get; set; }
        public virtual Organizasyon Organizasyon2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ogrenci> Ogrenci { get; set; }
    }
}
