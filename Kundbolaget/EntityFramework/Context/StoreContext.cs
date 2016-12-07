using System.Data.Entity;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Context
{
    public class StoreContext : DbContext
    {
        public DbSet<ProductInfo> ProductsInfoes { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Country> Countries { get; set; } 
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }

        public StoreContext() : base(@"data source =.\SQLEXPRESS; initial catalog=KundBolagetDemo; integrated security=SSPI") {}

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