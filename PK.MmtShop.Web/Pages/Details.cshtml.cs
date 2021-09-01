using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PK.MmtShop.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PK.MmtShop.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private ICategoryDataService _categoryDataService;

        public DetailsModel(ICategoryDataService categoryDataService)
        {
            _categoryDataService = categoryDataService
                                   ?? throw new ArgumentNullException(nameof(categoryDataService));
        }

        public SelectList CategorySelection { get; set; }
        
        public async Task OnGetAsync(string productId)
        {
            CategorySelection = await DoCategoryListAsync();

        }


        private async Task<SelectList> DoCategoryListAsync()
        {
            var categories = await _categoryDataService.GetAllCategoriiesAsync();

            var sekectItems = new List<SelectListItem>();
            categories.ToList().ForEach(item =>
            {
                sekectItems.Add(new SelectListItem(text:item.Name, value: item.Id.ToString()));
            });

            return new SelectList(sekectItems);
        }

    }
}
