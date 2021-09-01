using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PK.MmtShop.Service.Entities
{
    /// <summary>
    /// CategoryRange Table: represents Sku range and next sku number
    /// </summary>
    [Table("CategoryRange")]
    public class CategoryRange
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SkuRange { get; set; }
        

        public virtual Category Category { get; set; }

    }
}
