using Microsoft.EntityFrameworkCore;
using PK.MmtShop.Service.Entities;

namespace PK.MmtShop.Service.Extensions
{
    /// <summary>
    /// MMTDbContext ModelBuilder Extension
    /// </summary>
    public static class CategoryModelBuilderExtender
    {
        /// <summary>
        /// Initiate the category items 
        /// </summary>
        /// <param name="builder">ModelBuilder extension <see cref="ModelBuilder"/>.</param>
        public static void InitiateCategoryItems(this ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(new Category[]
            {
                new Category() { Id = 1, Name = "Home" },
                new Category() { Id = 2, Name = "Garden"},
                new Category() { Id = 3, Name = "Electronics" },
                new Category() { Id = 4, Name = "Fitness"},
                new Category() { Id = 5, Name = "Toys"}
            });
        }

        /// <summary>
        /// Initiate category range items
        /// </summary>
        /// <param name="builder">ModelBuilder extension <see cref="ModelBuilder"/></param>
        public static void InitiateCategoryRanging(this ModelBuilder builder)
        {
            builder.Entity<CategoryRange>().HasData(new CategoryRange[]
            {
                new CategoryRange() { Id = 1, CategoryId = 1, SkuRange = 10000 },
                new CategoryRange() { Id = 2, CategoryId = 2, SkuRange = 20000 },
                new CategoryRange() { Id = 3, CategoryId = 3, SkuRange = 30000 },
                new CategoryRange() { Id = 4, CategoryId = 4, SkuRange = 40000 },
                new CategoryRange() { Id = 5, CategoryId = 5, SkuRange = 50000 }
            });
        }
    }
}
