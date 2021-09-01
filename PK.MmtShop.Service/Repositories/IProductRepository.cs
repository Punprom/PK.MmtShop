using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Service.Entities;

namespace PK.MmtShop.Service.Repositories
{
    /// <summary>
    /// Product repository interface contract
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>products data object arrays <see cref="ProductDto"/></returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Gets product by id
        /// </summary>
        /// <param name="productId">product id (guid)</param>
        /// <returns>product data object <see cref="ProductDto"/></returns>
        Task<ProductDto> GetProductByIdAsync(Guid productId);

        /// <summary>
        /// Gets all products by category id
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>product data object arrays <see cref="ProductDto"/></returns>
        Task<IEnumerable<ProductDto>> GetAllProductsByCategoryAsync(int categoryId);

        /// <summary>
        /// Getting next value of sku by category id
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <returns>next sku id by category</returns>
        Task<int> GetNextSkuByCategoryAsync(int categoryId);

        /// <summary>
        /// Insert new product
        /// </summary>
        /// <param name="product">product data object <see cref="ProductDto"/></param>
        /// <returns></returns>
        Task<Product> InsertProductAsync(ProductDto product);

        /// <summary>
        /// Update current product
        /// </summary>
        /// <param name="product">product data object <see cref="ProductDto"/></param>
        /// <returns></returns>
        Task<Product> UpdateProductAsync(ProductDto product);

        /// <summary>
        /// Deleting product from database
        /// </summary>
        /// <param name="productId">product id (guid)</param>
        /// <returns>success yes/no</returns>
        Task<bool> DeleteProductAsync(Guid productId);
    }
}
