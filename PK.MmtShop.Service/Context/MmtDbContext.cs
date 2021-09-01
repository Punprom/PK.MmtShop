using Microsoft.EntityFrameworkCore;
using PK.MmtShop.Service.Entities;
using PK.MmtShop.Service.Extensions;

namespace PK.MmtShop.Service.Context
{
    public class MmtDbContext: DbContext 
    {
        public MmtDbContext(DbContextOptions<MmtDbContext> options) 
            :base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryRange> CategoryRanges { get; set; }


        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(t => t.Created)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            // set product sku to be unique index
            modelBuilder.Entity<Product>()
                .HasIndex(t => t.Sku)
                .IsUnique();

            // seeding categories & category ranges
            modelBuilder.InitiateCategoryItems();
            modelBuilder.InitiateCategoryRanging();

            // seeding products
            modelBuilder.GenerateInitialProducts();

            base.OnModelCreating(modelBuilder);
        }
    }
}
