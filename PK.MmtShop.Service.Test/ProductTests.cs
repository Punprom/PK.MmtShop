using System;
using System.Linq;
using Moq;
using PK.MmtShop.Service.Repositories;
using PK.MmtShop.Service.Test.Fakes;
using Xunit;

namespace PK.MmtShop.Service.Test
{
    public class ProductTests
    {
        private readonly Mock<IProductRepository> _productMokRepo;

        public ProductTests()
        {
            // init the moq product repo
            _productMokRepo = new Mock<IProductRepository>();
        }

        /// <summary>
        /// Test listing all products from repository
        /// </summary>
        [Fact]
        public void Test_getting_all_products()
        {
            _productMokRepo.Setup(x => x.GetAllProductsAsync())
                .Returns(() =>
                {
                    var repo = new ProductFakeRepository();
                    return repo.GetAllProductsAsync();
                });

            var moqRepoObj = _productMokRepo.Object;
            var actualProducts = moqRepoObj.GetAllProductsAsync().Result;

            Assert.NotEmpty(actualProducts);
            
        }

        /// <summary>
        /// Testing if getting product id from product repository ok 
        /// </summary>
        /// <param name="guid">product id</param>
        /// <param name="expectedName">expect product name</param>
        /// <param name="expectedDesc">expected product desc</param>
        /// <param name="expectedCategoryId">expected category id</param>
        /// <param name="expectedSku">expected sku number</param>
        [Theory]
        [InlineData("ef22f1de-d9d8-489a-9de4-2aaf160a7760","P15", "Product 15", 5, 50001)]
        [InlineData("4eff06f1-47c7-4776-97de-46bd66a37bf1", "P01","Product 1", 1, 10001)]
        [InlineData("bcdd33f4-8f20-45cd-ad29-119f235bff8b", "P44", "Product 44", 4, 40001)]
        public void Test_getting_a_product_from_this_products_listing(string guid, string expectedName, string expectedDesc, int expectedCategoryId, int expectedSku)
        {

            var g = new Guid(guid);
            _productMokRepo.Setup(x => x.GetProductByIdAsync(It.IsAny<Guid>()))
                .Returns(() =>
                {
                   var repo = new ProductFakeRepository();
                    return repo.GetProductByIdAsync(g);
                });

            var moqRepoObj = _productMokRepo.Object;
            var actualItem = moqRepoObj.GetProductByIdAsync(g).Result;
            Assert.NotNull(actualItem);

            Assert.Equal(g, actualItem.Id);
            Assert.Equal(expectedName, actualItem.Name);
            Assert.Equal(expectedCategoryId, actualItem.CategoryId);
            Assert.Equal(expectedDesc, actualItem.Description);
            Assert.Equal(expectedSku, actualItem.Sku);
        }

        /// <summary>
        /// Test returns products with category id
        /// </summary>
        /// <param name="categoryId">category id to be search</param>
        /// <param name="expectedQty">expected quantity items return</param>
        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 1)]
        [InlineData(5, 2)]
        [InlineData(3, 1)]
        public void Test_products_by_category_id(int categoryId, int expectedQty)
        {
            _productMokRepo.Setup(x => x.GetAllProductsByCategoryAsync(It.IsAny<int>()))
                .Returns(() =>
                {
                    var repo = new ProductFakeRepository();
                    return repo.GetAllProductsByCategoryAsync(categoryId);
                });
            var moqRepoObj = _productMokRepo.Object;

            var actualItems = moqRepoObj.GetAllProductsByCategoryAsync(categoryId).Result;
            Assert.NotEmpty(actualItems);

            Assert.True(actualItems.All(p => p.CategoryId == categoryId));
            Assert.Equal(expectedQty, actualItems.Count());
        }

        /// <summary>
        /// Test - determine next available next sku number by category id
        /// </summary>
        /// <param name="categoryId">category id to be searched</param>
        /// <param name="expectedNextSku">next available sku number</param>
        [Theory]
        [InlineData(2, 20003)]
        [InlineData(5, 50002)]
        [InlineData(3, 30001)]
        [InlineData(4, 40002)]
        [InlineData(1, 10003)]
        public void Test_getting_next_available_sku_from_category_id(int categoryId, int expectedNextSku)
        {
            _productMokRepo.Setup(x => x.GetNextSkuByCategoryAsync(categoryId))
                .Returns(() =>
                {
                    var repo = new ProductFakeRepository();
                    return repo.GetNextSkuByCategoryAsync(categoryId);

                });
            var moqRepoObj = _productMokRepo.Object;
            var actualNextSku = moqRepoObj.GetNextSkuByCategoryAsync(categoryId).Result;

            Assert.NotEqual(0, actualNextSku);
            Assert.Equal(expectedNextSku, actualNextSku);
        }
        
        

        
        
    }
}
