﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dentplex.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DentplexDBEntities : DbContext
    {
        public DentplexDBEntities()
            : base("name=DentplexDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<SliderItem> SliderItems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGallery> ProductGalleries { get; set; }
    }
}
