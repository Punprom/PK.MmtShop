using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PK.MmtShop.Service.Entities
{
    /// <summary>
    /// Category Table: Category table representation
    /// </summary>
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        public virtual ICollection<CategoryRange> CategoryRanges { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
