﻿//------------------------------------------------------------------------------
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
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Web.UI.WebControls.WebParts;

    public partial class DBOgrenciEntities1 : DbContext
    {
        public DBOgrenciEntities1()
            : base("name=DBOgrenciEntities1")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            // modelBuilder.Entity<OgrenciDers>()
            //.HasOptional(a => a.Ogrenci)
            //.WithMany(c => c.OgrenciDers)
            //.HasForeignKey(a => a.OgrenciID)
            //.WillCascadeOnDelete(true);

            // modelBuilder.Entity<Ogrenci>()
            //.HasMany(c => c.OgrenciDers)
            //.WithOptional(a => a.Ogrenci)
            //.WillCascadeOnDelete(true);







            throw new UnintentionalCodeFirstException();





            //modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();







        }

        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<Dersler> Dersler { get; set; }
        public virtual DbSet<Organizasyon> Organizasyon { get; set; }
        public virtual DbSet<OgrenciDers> OgrenciDers { get; set; }
    }
}
