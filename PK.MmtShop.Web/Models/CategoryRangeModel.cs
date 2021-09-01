using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PK.MmtShop.Web.Models
{
    public class CategoryRangeModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int SkuRange { get; set; }
    }
}
