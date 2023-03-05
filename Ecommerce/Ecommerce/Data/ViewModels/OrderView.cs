namespace Ecommerce.Data.ViewModels
{
    public class OrderView
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string CustomerId { get; set; }
    }
}
