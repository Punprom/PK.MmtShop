using Moq;
using PK.MmtShop.Service.Repositories;
using PK.MmtShop.Service.Test.Fakes;
using System.Linq;
using Xunit;

namespace PK.MmtShop.Service.Test
{
    public class CategoryTests
    {
        private readonly Mock<ICategoryRepository> categoryRepo;
        
        public CategoryTests()
        {
            // init category repo
            categoryRepo = new Mock<ICategoryRepository>();
            
        }

        /// <summary>
        /// Test all category from repository
        /// </summary>
        /// <param name="expectedId">Category id/param>
        /// <param name="expectedName">expected category name</param>
        [Theory]
        [InlineData(1, "Home")]
        [InlineData(2, "Garden")]
        [InlineData(3, "Electronics")]
        [InlineData(4, "Fitness")]
        [InlineData(5, "Toys")]
        public void Test_getting_all_category_from_repository(int expectedId, string expectedName)
        {
            SetupAllCategories();

            var moqRepoObj = categoryRepo.Object;
            var actualResults = moqRepoObj.GetCategoriesAsync().Result;

            var actual = actualResults.FirstOrDefault(c => c.Id == expectedId);
            
            Assert.NotNull(actual);
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedName, actual.Name);
            
        }

        /// <summary>
        /// Test - getting all the sku range by category id
        /// </summary>
        /// <param name="expectedCategoryId">expected category id</param>
        /// <param name="expectSkuRange">expected category sku range</param>
        [Theory]
        [InlineData(1, 10000)]
        [InlineData(2, 20000)]
        [InlineData(3, 30000)]
        [InlineData(4, 40000)]
        [InlineData(5, 50000)]
        public void Test_getting_all_category_ranges(int expectedCategoryId, int expectSkuRange)
        {
           SetupCategoryRanges();

           var moqRepoObj = categoryRepo.Object;
           var actualItems = moqRepoObj.GetAllCategoryRangesAsync();
           var actualItem = actualItems.Result.FirstOrDefault(cr => cr.CategoryId == expectedCategoryId);

           Assert.NotNull(actualItem);
           Assert.Equal(expectedCategoryId, actualItem.CategoryId);
           Assert.Equal(expectSkuRange, actualItem.SkuRange);

        }

        /// <summary>
        /// Test - getting sku range from category id
        /// </summary>
        /// <param name="categoryId">category id to be search</param>
        /// <param name="expectedRange">expected category sku range</param>
        [Theory]
        [InlineData(2, 20000)]
        [InlineData(1, 10000)]
        [InlineData(5, 50000)]
        [InlineData(3, 30000)]
        public void Test_getting_category_range_by_category_id(int categoryId, int expectedRange)
        {
            SetupCategoryRanges();
            var moqRepoObj = categoryRepo.Object;
            var actualCategoryRanges = moqRepoObj.GetAllCategoryRangesAsync().Result;

            var actualCategoryRange = actualCategoryRanges.FirstOrDefault(cr => cr.CategoryId == categoryId);

            Assert.NotNull(actualCategoryRange);
            Assert.Equal(categoryId, actualCategoryRange.CategoryId);
            Assert.Equal(expectedRange, actualCategoryRange.SkuRange);

        }


        private void SetupAllCategories()
        {
            categoryRepo.Setup(x => x.GetCategoriesAsync()).Returns(
                () =>
                {
                    var repo = new CategoryFakeRepository();
                    return repo.GetCategoriesAsync();
                }
            );

        }

        private void SetupCategoryRanges()
        {
            categoryRepo.Setup(x => x.GetAllCategoryRangesAsync()).Returns(
                    () =>
                    {
                        var repo = new CategoryFakeRepository();
                        return repo.GetAllCategoryRangesAsync();
                    }
            );
        }


    }
}
