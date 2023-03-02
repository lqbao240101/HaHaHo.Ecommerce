using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
