using Ecommerce.Data.Enums;
using Ecommerce.Models;

namespace Ecommerce.Data.ViewModels
{
    public class OrderView
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public OrderStatus Status { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string CustomerId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
