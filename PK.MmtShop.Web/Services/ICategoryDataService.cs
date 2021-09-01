using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Web.Models;

namespace PK.MmtShop.Web.Services
{
    /// <summary>
    /// ICategoryDataService interface - Category data service contract
    /// </summary>
    public interface ICategoryDataService
    {
        Task<IEnumerable<CategoryModel>> GetAllCategoriiesAsync();

        Task<CategoryModel> GetCategoryAsync(int categoryId);

        Task<IEnumerable<CategoryRangeModel>> GetAllCategoryRanges();

        Task<CategoryRangeModel> GetCategoryRange(int categoryId);

    }
}
