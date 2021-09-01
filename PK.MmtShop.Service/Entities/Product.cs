using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PK.MmtShop.Service.Entities
{
    /// <summary>
    /// Product Table : Product table representation
    /// </summary>
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Sku { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastModified { get; set; }

        public virtual Category ProductCategory { get; set; } 
    }
}
