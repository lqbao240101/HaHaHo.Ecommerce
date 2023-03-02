using Ecommerce.Data.Base;

namespace Ecommerce.Models
{
    public class Address : IEntityBase
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}
