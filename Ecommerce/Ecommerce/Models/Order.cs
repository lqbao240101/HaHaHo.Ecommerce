using Ecommerce.Data.Base;
using Ecommerce.Data.Enums;

namespace Ecommerce.Models
{
    public class Order : IEntityBase
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public OrderStatus Status { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
