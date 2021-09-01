using PK.MmtShop.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PK.MmtShop.Service.Repositories
{
    /// <summary>
    /// Category Interface contract
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>returns all category <see cref="PK.MmtShop.Service.Entities.Category"/></returns>
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        /// <summary>
        /// get category by id
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns>Category <see cref="PK.MmtShop.Service.Entities.Category"/></returns>
        Task<CategoryDto> GetCategoryAsync(int categoryId);

        /// <summary>
        /// Getting all category ranges
        /// </summary>
        /// <returns>array of category ranges <see cref="PK.MmtShop.Service.Entities.CategoryRange"/></returns>
        Task<IEnumerable<CategoryRangeDto>> GetAllCategoryRangesAsync();

        /// <summary>
        /// Get category range by category id
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <returns>Category range <see cref="PK.MmtShop.Service.Entities.CategoryRange"/></returns>
        Task<CategoryRangeDto> GetCategoryRangeByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Checks if category id exists
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <returns>exists yes/no</returns>
        Task<bool> IsCategoryIdExistsAsync(int categoryId);
    }
}