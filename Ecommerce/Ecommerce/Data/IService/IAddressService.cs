using Ecommerce.Data.Base;
using Ecommerce.Models;

namespace Ecommerce.Data.IService
{
    public interface IAddressService : IEntityBaseRepository<Address> {
        Task<List<Address>> GetAddressesByUserIdAndRoleAsync(int userId, string userRole);

        Task<Address> GetAddressDetail(int userId, string userRole, int id);
    }
}
