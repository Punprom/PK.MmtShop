using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Service.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PK.MmtShop.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ILogger<ProductController> logger)
        {
            _productRepository = productRepository
                                 ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository
                                  ?? throw new ArgumentNullException(nameof(categoryRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Getting all products
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            _logger.LogInformation("Listing all products");

            var list = await _productRepository.GetAllProductsAsync();

            if (!list.Any())
            {
                var msg = "No products found on the database.";
                _logger.LogWarning(msg);

                return NotFound(msg);
            }

            return Ok(list.AsEnumerable());
        }

        /// <summary>
        /// Getting product by id
        /// </summary>
        /// <param name="productId">product id (guid in string)</param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(string productId)
        {
            Guid g = Guid.Empty;
            var msg = string.Empty;
            var isOk = Guid.TryParse(productId, out g);

            if (!isOk)
            {
                msg = $"Failed to get product by id: {productId}";
                _logger.LogWarning(msg);

                return BadRequest(msg);
            }

            var product = await _productRepository.GetProductByIdAsync(g);
            if (product == null)
            {
                msg = $"Product not found by id: {productId}";
                _logger.LogWarning(msg);

                return NotFound(msg);
            }

            return Ok(product);
        }

        /// <summary>
        /// Getting all products with search category id
        /// </summary>
        /// <param name="categoryId">category id to be searched</param>
        /// <returns></returns>
        [HttpGet("Categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var msg = string.Empty;
            var isExists = await _categoryRepository.IsCategoryIdExistsAsync(categoryId);
            if (!isExists)
            {
                msg = $"Category id: {categoryId} does not exists.";
                _logger.LogWarning(msg);

                return BadRequest(msg);
            }

            var products = await _productRepository.GetAllProductsByCategoryAsync(categoryId);
            if (!products.Any())
            {
                msg = $"Unable to find any products with category id: {categoryId}.";
                _logger.LogWarning(msg);

                return NotFound(msg);
            }

            return Ok(products);
        }

        /// <summary>
        /// Getting next sku by category id
        /// </summary>
        /// <param name="categoryId">category id to be search</param>
        /// <returns></returns>
        [HttpGet("NextSku/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNextSku(int categoryId)
        {
            var msg = string.Empty;

            try
            {
                var isExists = await _categoryRepository.IsCategoryIdExistsAsync(categoryId);
                if (!isExists)
                {
                    msg = $"Category id: {categoryId} does not exists.";
                    _logger.LogWarning(msg);

                    return NotFound(msg);
                }

                var nextSku = await _productRepository.GetNextSkuByCategoryAsync(categoryId);
                
                return Ok(nextSku);

            }
            catch (Exception ex)
            {
                msg = $"Encountered error attempt to get next sku, message: {ex.Message}";
                _logger.LogError(msg, ex);

                return BadRequest(msg);
            }
            
        }


        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertNewProduct([FromBody] ProductDto product)
        {
            try
            {
                var entity = await _productRepository.InsertProductAsync(product);

                return Ok(entity);
            }
            catch (Exception ex)
            {
                var msg = $"Error in inserting new product, message: {ex.Message}";
                _logger.LogError(msg, ex);

                return BadRequest(msg);
            }
            
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductDto product)
        {
           if (product == null)
               return BadRequest();
            
           try
           {
               var updated = await _productRepository.UpdateProductAsync(product);

               return Ok(updated);
           }
           catch (Exception ex)
           {
               var msg = $"Error in updating product sku: {product.Sku}, message: {ex.Message}";
               _logger.LogError(msg, ex);

               return BadRequest(msg);
                
           }

        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductAsync(string productId)
        {
            var msg = string.Empty;

            try
            {
                Guid g = Guid.Empty;
                if (!Guid.TryParse(productId, out g))
                {
                    msg = $"Invalid product id, {productId}.";
                    _logger.LogWarning(msg);

                    return BadRequest(msg);
                }

                var delitem = await _productRepository.GetProductByIdAsync(g);
                if (delitem == null)
                    return NotFound($"Deleting Product with id: {productId} to found.");
                await _productRepository.DeleteProductAsync(delitem.Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurs message: {ex.Message}");

                return BadRequest(msg);
            }
        }
    }
}
