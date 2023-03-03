using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly ApplicationDbContext _context;
        public CartItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddNewCartItemAsync(string userId, NewCartItemModel data)
        {
            var currentCartItem = await _context.CartItems.FirstOrDefaultAsync(n => n.CustomerId == userId && n.ProductId == data.ProductId);

            if (currentCartItem == null)
            {
                var newCartItem = new CartItem()
                {
                    CustomerId = userId,
                    ProductId = data.ProductId,
                    Quantity = data.quantity
                };

                await _context.CartItems.AddAsync(newCartItem);
            }
            else
            {
                currentCartItem.Quantity += data.quantity;
            }

            await _context.SaveChangesAsync();

        }

        public async Task DeleteCartItemAsync(string userId, int productId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(n => n.CustomerId == userId && n.ProductId == productId);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem> GetCartItemDetail(string userId, int productId)
        {
            var cartItem = await _context.CartItems.Include(n => n.Product).FirstOrDefaultAsync(n => n.ProductId == productId && n.CustomerId == userId);

            return cartItem;
        }

        public async Task<List<CartItem>> GetCartItemsByUserId(string userId)
        {
            var cartItem = await _context.CartItems.Include(n => n.Product).Where(n => n.CustomerId == userId).ToListAsync();

            return cartItem;
        }

        public async Task UpdateCartItemAsync(UpdateCartItemModel data)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(n => n.CustomerId == data.CustomerId && n.ProductId == data.ProductId);

            if (cartItem != null)
            {
                cartItem.Quantity = data.quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
