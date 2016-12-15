using System.Data.Entity;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Context
{
    public class StoreContext : DbContext
    {
        public virtual DbSet<ProductInfo> ProductsInfoes { get; set; }
        public virtual DbSet<Container> Containers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ContactPerson> ContactPersons { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<ProductStock> ProductStocks { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Volume> Volumes { get; set; }

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
            modelBuilder.Entity<ProductStock>()
                .HasRequired(x => x.ProductInfo)
                .WithMany(x => x.ProductStocks);
        }
    }
}