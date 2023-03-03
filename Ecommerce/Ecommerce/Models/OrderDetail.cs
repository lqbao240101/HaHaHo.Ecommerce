namespace Ecommerce.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public double PercentSale { get; set; }
        public decimal Total { get; set; }
    }
}
