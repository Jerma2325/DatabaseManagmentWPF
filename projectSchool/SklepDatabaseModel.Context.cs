﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace projectSchool
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SklepDatabaseEntities : DbContext
    {
        public SklepDatabaseEntities()
            : base("name=SklepDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Pracownik> Pracowniks { get; set; }
        public virtual DbSet<Produkt> Produkts { get; set; }
        public virtual DbSet<Sklep> Skleps { get; set; }
        public virtual DbSet<Wynik> Wyniks { get; set; }
    }
}
