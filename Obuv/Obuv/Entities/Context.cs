using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Obuv.Entities
{
    public partial class ContextT : DbContext
    {
        public ContextT()
            : base("name=ContextT")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<StaffRole> StaffRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.productCategory);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Manufacturer)
                .HasForeignKey(e => e.productManufacturer);

            modelBuilder.Entity<Product>()
                .Property(e => e.productCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Provider>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Provider)
                .HasForeignKey(e => e.productProvider);

            modelBuilder.Entity<StaffRole>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.StaffRole)
                .HasForeignKey(e => e.userRole)
                .WillCascadeOnDelete(false);
        }
    }
}
