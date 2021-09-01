using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PK.MmtShop.Domain.Dtos
{
    public class CategoryRangeDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int SkuRange { get; set; }
    }
}
