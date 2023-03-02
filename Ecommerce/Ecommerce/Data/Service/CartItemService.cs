using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;

namespace Ecommerce.Data.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly ApplicationDbContext _context;
        public CartItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<CartItem> Create(NewCartItemModel data)
        {
            return null;
        }
    }
}
