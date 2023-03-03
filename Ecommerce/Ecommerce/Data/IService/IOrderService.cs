using Ecommerce.Data.ViewModels;

namespace Ecommerce.Data.IService
{
    public interface IOrderService
    {
        Task<EntityResponseMessage> StoreOrderAsync(List<int> productIds, string userId, int addressId);
    }
}
