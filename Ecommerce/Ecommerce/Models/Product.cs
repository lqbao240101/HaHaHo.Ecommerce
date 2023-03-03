using Ecommerce.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    [Table("Products")]
    [Index(nameof(ProductName), IsUnique = true)]
    public class Product : IEntityBase
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double PercentSale { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
