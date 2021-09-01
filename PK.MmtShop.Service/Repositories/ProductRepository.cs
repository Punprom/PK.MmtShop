using PK.MmtShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PK.MmtShop.Service.Context;
using PK.MmtShop.Service.Entities;

namespace PK.MmtShop.Service.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly MmtDbContext _context;
        public ProductRepository(MmtDbContext dbContext,
            IMapper mapper)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>products data object arrays <see cref="ProductDto"/></returns>
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var list = new List<ProductDto>();
            var results = await _context.Products.ToListAsync();

            results.ForEach(item => list.Add(_mapper.Map<ProductDto>(item)));

            return list.AsEnumerable();
        }

        /// <summary>
        /// Gets all products by category id
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>product data object arrays <see cref="ProductDto"/></returns>
        public async Task<IEnumerable<ProductDto>> GetAllProductsByCategoryAsync(int categoryId)
        {
            var list = new List<ProductDto>();
            var results = await _context.Products.Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            results.ForEach(item => list.Add(_mapper.Map<ProductDto>(item)));
            
            return list.AsEnumerable();
        }

        /// <summary>
        /// Getting next value of sku by category id
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <returns>next sku id by category</returns>
        public async Task<int> GetNextSkuByCategoryAsync(int categoryId)
        {
            // finding root sku range from category
            var catRange = await _context.CategoryRanges.FirstOrDefaultAsync(cr => cr.CategoryId == categoryId);
            // if fail.. bad request!
            if (catRange == null)
                throw new ArgumentOutOfRangeException($"Invalid category id: {categoryId}");

            var sku = -1;
            // gets products with this category and if not found set sku range by category id
            var groupedProducts = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
            
            sku = !groupedProducts.Any() ? catRange.SkuRange : groupedProducts.Max(p => p.Sku);
            // working out next sku no.
            var returnSku = (sku == 0) ? catRange.SkuRange + 1 : sku + 1;

            return returnSku;

        }

        /// <summary>
        /// Gets product by id
        /// </summary>
        /// <param name="productId">product id (guid)</param>
        /// <returns>product data object <see cref="ProductDto"/></returns>
        public async Task<ProductDto> GetProductByIdAsync(Guid productId)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (result == null)
                return null;

            var product = _mapper.Map<ProductDto>(result);

            return product;
        }

        /// <summary>
        /// Insert new product
        /// </summary>
        /// <param name="product">product data object <see cref="ProductDto"/></param>
        /// <returns></returns>
        public async Task<Product> InsertProductAsync(ProductDto product)
        {
            try
            {
                var isExists = _context.Products.Any(p => p.Sku == product.Sku);
                if (isExists)
                    throw new AccessViolationException("Product sku already exists.");
                
                var entity = _mapper.Map<Product>(product);
                entity.Created = DateTime.Now;
                
                var entry = await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entry.Entity;

            }
            catch (Exception ex)
            {
                var msg = $"Encountered exception on product creation, message:{ex.Message}";
                throw new Exception(msg, ex);
            }
        }

        /// <summary>
        /// Update current product
        /// </summary>
        /// <param name="product">product data object <see cref="ProductDto"/></param>
        /// <returns></returns>
        public async Task<Product> UpdateProductAsync(ProductDto product)
        {
            try
            {
                var isExists = _context.Products.Any(p => p.Sku == product.Sku);
                if (!isExists)
                    throw new AccessViolationException("Product sku does not exists on the system.");   
                
                var entity = _mapper.Map<Product>(product);
                entity.LastModified = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                var msg = $"Encountered exception on update product, message:{ex.Message}";
                throw new Exception(msg, ex);
            }
        }

        /// <summary>
        /// Deleting product from database
        /// </summary>
        /// <param name="productId">product id (guid)</param>
        /// <returns>success yes/no</returns>
        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            try
            {
                var deletionEntity = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (deletionEntity!=null)
                {
                    _context.Entry(deletionEntity).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                var msg = $"Encountered exception on product deletion, message:{ex.Message}";
                throw new Exception(msg, ex);
            }
        }
    }
}
