using Ecommerce.Data.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class CartItem
    {
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int quantity { get; set; }
    }
}
