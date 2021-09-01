using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Service.Entities;
using PK.MmtShop.Service.Repositories;
using Xunit;

namespace PK.MmtShop.Service.Test.Fakes
{
    public class ProductFakeRepository : IProductRepository
    {
        private readonly ICategoryRepository catRepo;

        public ProductFakeRepository()
        {
            catRepo = new CategoryFakeRepository();
        }

        public Task<bool> DeleteProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = new ProductDto[]
            {
                new ProductDto() { Id = new Guid("4eff06f1-47c7-4776-97de-46bd66a37bf1"), Description = "Product 1", Name = "P01", Price = 10.10M, CategoryId = 1, Sku = 10001},
                new ProductDto() { Id = new Guid("513d2e59-8d59-4a14-8ee1-7f325149ac68"), Description = "Product 2", Name = "P02", Price = 10.10M, CategoryId = 2, Sku = 20002},
                new ProductDto() { Id = new  Guid("fb1dbdfa-bf16-43aa-a4d6-01d94eacfaf0"), Description = "Product 3", Name = "P03", Price = 10.10M, CategoryId = 1, Sku = 10002},
                new ProductDto() { Id = new Guid("bcdd33f4-8f20-45cd-ad29-119f235bff8b"), Description = "Product 44", Name = "P44", Price = 20.10M, CategoryId = 4, Sku = 40001},
                new ProductDto() { Id = new Guid("400d4d29-7733-4750-95ad-4145367c442d"), Description = "Product 12", Name = "P12", Price = 100.10M, CategoryId = 3 },
                new ProductDto() { Id = new Guid("3967f055-c4b8-4ca0-97e0-001d737f52e6"), Description = "Product 5", Name = "P05", Price = 5.89M, CategoryId = 5 },
                new ProductDto() { Id = new Guid("ef22f1de-d9d8-489a-9de4-2aaf160a7760"), Description = "Product 15", Name = "P15", Price = 20.19M, CategoryId = 5, Sku = 50001}


            };

            return Task.FromResult(products.AsEnumerable());
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsByCategoryAsync(int categoryId)
        {
            var products = GetAllProductsAsync().Result.ToList().Where(p => p.CategoryId == categoryId).ToList();

            return Task.FromResult(products.AsEnumerable());
        }

        public Task<int> GetNextSkuByCategoryAsync(int categoryId)
        {
            var products = GetAllProductsAsync().Result;
            var sku = products.Where(p => p.CategoryId == categoryId)
                .Max(p => p.Sku);
            var catRange = catRepo.GetCategoryRangeByCategoryIdAsync(categoryId).Result;

            var returnSku = (sku == 0) ? catRange.SkuRange + 1 : sku + 1;

            return Task.FromResult(returnSku);
        }

        public Task<ProductDto> GetProductByIdAsync(Guid productId)
        {
            var products = GetAllProductsAsync().Result;
            var item = products.FirstOrDefault(p => p.Id == productId);
            
            return Task.FromResult(item);
        }

        public Task<Product> InsertProductAsync(ProductDto product)
        {
           // TODO:
            return Task.FromResult(new Product());
        }

        public Task<Product> UpdateProductAsync(ProductDto product)
        {
            // TODO:
            return Task.FromResult(new Product());
        }
    }
}
