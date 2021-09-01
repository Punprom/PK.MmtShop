using PK.MmtShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PK.MmtShop.Service.Context;
using IMapper = AutoMapper.IMapper;

namespace PK.MmtShop.Service.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MmtDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        private readonly IMapper _mapper;

        public CategoryRepository(MmtDbContext dbContext,
            IMapper mapper,
            ILogger<CategoryRepository> logging)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logging ?? throw new ArgumentNullException(nameof(logging));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CategoryRangeDto>> GetAllCategoryRangesAsync()
        {
            var list = new List<CategoryRangeDto>();

            _logger.LogInformation("Getting all category ranges.");
            var results = await _context.CategoryRanges.OrderBy(cr => cr.CategoryId).ToListAsync();

            _logger.LogInformation("Converts entities to dtos.");
            results.ForEach(item => { list.Add(_mapper.Map<CategoryRangeDto>(item)); });

            return list.AsEnumerable();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var list = new List<CategoryDto>();
            _logger.LogInformation("List all categories.");
            var results = await _context.Categories.OrderBy(c => c.Id).ToListAsync();

            _logger.LogInformation("Converting entities to dtos.");
            results.ForEach(item => { list.Add(_mapper.Map<CategoryDto>(item)); });

            return list.AsEnumerable();
        }

        public async Task<CategoryDto> GetCategoryAsync(int categoryId)
        {
            _logger.LogInformation($"List category: {categoryId}.");
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (result == null)
            {
                _logger.LogWarning($"Unable to find any category with this id: {categoryId}!");
                return null;
            }

            _logger.LogInformation("Found, returning as dto");
            var dto = _mapper.Map<CategoryDto>(result);

            return dto;
        }

        public async Task<CategoryRangeDto> GetCategoryRangeByCategoryIdAsync(int categoryId)
        {
            _logger.LogInformation($"List of category range by category id: {categoryId}.");
            var rangeResult = await _context.CategoryRanges.FirstOrDefaultAsync(cr => cr.CategoryId == categoryId);

            if ( rangeResult == null)
            {
                _logger.LogWarning($"Unable to find any category range with this id: {categoryId}!");
                return null;
            }

            _logger.LogInformation("Found, returning category range as dto");
            var dto = _mapper.Map<CategoryRangeDto>(rangeResult);

            return dto;
            
        }

        /// <summary>
        /// Checks if category id exists
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <returns>exists yes/no</returns>
        public async Task<bool> IsCategoryIdExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }
    }
}
