using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models.Context
{
    public class StoreContext : DbContext
    {
        public DbSet<ProductInfo> Products { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public StoreContext() : base("name=KundBolaget") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasOptional(x => x.ParentCompany)
                .WithMany(x => x.SubCompanies)
                .HasForeignKey(x => x.ParentCompanyId);
            modelBuilder.Entity<Company>()
                .HasRequired(x => x.Address)
                .WithMany(x => x.Companies)
                .HasForeignKey(x => x.AddressId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasRequired(x => x.DeliveryAddress)
                .WithMany(x => x.DeliveryCompanies)
                .HasForeignKey(x => x.DeliveryAddressId)
                .WillCascadeOnDelete(false);
        }
    }
}