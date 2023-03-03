using Ecommerce.Data.Base;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;

namespace Ecommerce.Data.IService
{
    public interface ICartItemService
    {
        Task<List<CartItem>> GetCartItemsByUserId(string userId);

        Task<CartItem> GetCartItemDetail(string userId, int productId);

        Task AddNewCartItemAsync(string userId, NewCartItemModel data);
        Task UpdateCartItemAsync(UpdateCartItemModel data);
        Task DeleteCartItemAsync(string userId, int productId);
    }
}
