using Ecommerce.Data.Base;
using Ecommerce.Data.IService;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Service
{
    public class AddressService : EntityBaseRepository<Address>, IAddressService
    {
        public AddressService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Address> GetAddressDetail(int userId, string userRole, int id)
        {
            var address = await _context.Addresses.Include(n => n.Customer).FirstOrDefaultAsync(n => n.Id == id && n.CustomerId == userId);

            return address;           
        }

        public async Task<List<Address>> GetAddressesByUserIdAndRoleAsync(int userId, string userRole)
        {
            var addresses = await _context.Addresses.Include(n => n.Customer).ToListAsync();

            if (userRole == "Customer")
            {
                addresses = addresses.Where(n => n.CustomerId == userId).ToList();
            }

            return addresses;
        }
    }
}
