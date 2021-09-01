using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Service.Entities;
using PK.MmtShop.Service.Repositories;

namespace PK.MmtShop.Service.Test.Fakes
{
    public class CategoryFakeRepository : ICategoryRepository
    {
        public Task<IEnumerable<CategoryRangeDto>> GetAllCategoryRangesAsync()
        {
            var items = new CategoryRangeDto[]
            {
                new CategoryRangeDto() { Id = 1, CategoryId = 1, SkuRange = 10000 },
                new CategoryRangeDto() { Id = 2, CategoryId = 2, SkuRange = 20000 },
                new CategoryRangeDto() { Id = 3, CategoryId = 3, SkuRange = 30000 },
                new CategoryRangeDto() { Id = 4, CategoryId = 4, SkuRange = 40000 },
                new CategoryRangeDto() { Id = 5, CategoryId = 5, SkuRange = 50000 }
            };;

            return Task.FromResult(items.AsEnumerable());
        }

        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var items = new CategoryDto[]
            {
                new CategoryDto() { Id = 1, Name = "Home" },
                new CategoryDto() { Id = 2, Name = "Garden"},
                new CategoryDto() { Id = 3, Name = "Electronics" },
                new CategoryDto() { Id = 4, Name = "Fitness"},
                new CategoryDto() { Id = 5, Name = "Toys"}
            };

            return Task.FromResult(items.AsEnumerable());
        }

        public Task<CategoryDto> GetCategoryAsync(int categoryId)
        {
            var items = GetCategoriesAsync().Result;
            var item = items.FirstOrDefault(c => c.Id == categoryId);

            return Task.FromResult(item);
        }

        public Task<CategoryRangeDto> GetCategoryRangeByCategoryIdAsync(int categoryId)
        {
            var categoryRanges = GetAllCategoryRangesAsync().Result;
            var catRange = categoryRanges.FirstOrDefault(cr => cr.CategoryId == categoryId);

            return Task.FromResult(catRange);
        }

        public Task<bool> IsCategoryIdExistsAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
