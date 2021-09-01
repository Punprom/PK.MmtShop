using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PK.MmtShop.Service.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PK.MmtShop.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _catRepository;

        public CategoryController(ICategoryRepository repository)
        {
            _catRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoriesResult()
        {
            var list = await _catRepository.GetCategoriesAsync();
            if (!list.Any())
                return NotFound("Category returns empty set!");

            return Ok(list.AsEnumerable());
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _catRepository.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound($"Unable to find category with id: {categoryId}. ");

            return Ok(category);
        }

        [HttpGet("Ranges")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryRanges()
        {
            var ranges = await _catRepository.GetAllCategoryRangesAsync();

            if (!ranges.Any())
                return NotFound("Unable to find any category ranges on the system.");

            return Ok(ranges.ToList().AsEnumerable());

        }

        [HttpGet("Ranges/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryRangeByCategoryId(int categoryId)
        {
            var range = await _catRepository.GetCategoryRangeByCategoryIdAsync(categoryId);

            if (range == null)
                return NotFound($"Unable to find category range with category id: {categoryId}");

            return Ok(range);
        }
    }
}
