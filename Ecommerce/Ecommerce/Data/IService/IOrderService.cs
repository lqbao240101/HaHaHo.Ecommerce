using Ecommerce.Data.ViewModels;

namespace Ecommerce.Data.IService
{
    public interface IOrderService
    {
        Task<EntityResponseMessage> StoreOrderAsync(List<int> productIds, string userId, int addressId);
        List<OrderView> GetOrders(string userId);
        OrderView GetOrder(string userId, int orderId);
    }
}
