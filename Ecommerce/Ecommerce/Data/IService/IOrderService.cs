using Ecommerce.Data.ViewModels;

namespace Ecommerce.Data.IService
{
    public interface IOrderService
    {
        Task<EntityResponseMessage> StoreOrderAsync(List<int> productIds, string userId, int addressId);
        Task<List<OrderView>> GetOrders(string userId);
        Task<OrderView> GetOrder(string userId, int orderId);

        Task<EntityResponseMessage> CancelOrder(int orderId, string role, string userId);
    }
}
