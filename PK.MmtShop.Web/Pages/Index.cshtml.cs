using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PK.MmtShop.Web.Models;
using PK.MmtShop.Web.Services;

namespace PK.MmtShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        private IProductDataService _productDataService;
        private ICategoryDataService _categoryDataService;

        public IndexModel(ILogger<IndexModel> logger, 
            IProductDataService productDataService, 
            ICategoryDataService categoryDataService)
        {
            _logger = logger;
            _productDataService = productDataService;
            _categoryDataService = categoryDataService;
        }

        public IEnumerable<ProductModel> Products { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }

        public async Task OnGetAsync()
        {
            await GetAllProductsAsync();
        }


        private async Task GetAllProductsAsync()
        {
            Categories = await _categoryDataService.GetAllCategoriiesAsync();
            var products = await _productDataService.GetAllProductsAsync();
            foreach (var product in products)
            {
                var category = Categories.FirstOrDefault(c => c.Id == product.CategoryId);
                product.CategoryName = category.Name;
            }

            Products = products;
        }

    }
}
