﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOS_DL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TOSEntities : DbContext
    {
        public TOSEntities()
            : base("name=TOSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<M_Brand> M_Brand { get; set; }
        public virtual DbSet<M_Company> M_Company { get; set; }
        public virtual DbSet<M_CompanyBrand> M_CompanyBrand { get; set; }
        public virtual DbSet<M_CompanyShipping> M_CompanyShipping { get; set; }
        public virtual DbSet<M_CompanyTag> M_CompanyTag { get; set; }
        public virtual DbSet<M_Group> M_Group { get; set; }
        public virtual DbSet<M_GroupList> M_GroupList { get; set; }
        public virtual DbSet<M_Item> M_Item { get; set; }
        public virtual DbSet<M_JobTimeable> M_JobTimeable { get; set; }
        public virtual DbSet<M_MultiPorpose> M_MultiPorpose { get; set; }
        public virtual DbSet<M_Price> M_Price { get; set; }
        public virtual DbSet<M_SKU> M_SKU { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<T_Information> T_Information { get; set; }
        public virtual DbSet<T_InformationDestination> T_InformationDestination { get; set; }
        public virtual DbSet<T_Inventory> T_Inventory { get; set; }
        public virtual DbSet<T_OrderDetail> T_OrderDetail { get; set; }
        public virtual DbSet<T_OrderHeader> T_OrderHeader { get; set; }
    }
}
