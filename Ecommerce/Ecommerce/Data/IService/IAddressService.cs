using Ecommerce.Data.Base;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;

namespace Ecommerce.Data.IService
{
    public interface IAddressService : IEntityBaseRepository<Address> {
        Task<List<Address>> GetAddressesByUserIdAndRoleAsync(string userId);

        Task<Address> GetAddressDetail(string userId, int id);

        Task<Address> AddNewAddressAsync(NewAddressModel data);
        Task UpdateAddressAsync(string customerId, NewAddressModel data);
    }
}
