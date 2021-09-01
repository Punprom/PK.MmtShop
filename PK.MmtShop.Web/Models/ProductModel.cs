using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PK.MmtShop.Web.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public int Sku { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
