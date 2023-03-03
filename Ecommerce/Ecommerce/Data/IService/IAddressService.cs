using Ecommerce.Data.Base;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;

namespace Ecommerce.Data.IService
{
    public interface IAddressService : IEntityBaseRepository<Address> {
        Task<List<Address>> GetAddressesByUserId(string userId);

        Task<Address> GetAddressDetail(string userId, int id);

        Task AddNewAddressAsync(string userId, NewAddressModel data);
        Task UpdateAddressAsync(UpdateAddressModel data);
    }
}
