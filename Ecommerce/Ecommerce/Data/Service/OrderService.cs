using AutoMapper;
using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
using Ecommerce.Helper;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EntityResponseMessage> StoreOrderAsync(List<int> productIds, string userId, int addressId)
        {
            List<CartItem> cartItems = new List<CartItem>();

            foreach (var productId in productIds)
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(n => n.ProductId == productId && n.CustomerId == userId);

                if (cartItem != null)
                {
                    if (cartItem.Quantity <= cartItem.Product.Quantity)
                        cartItems.Add(cartItem);
                    else
                    {
                        return new EntityResponseMessage
                        {
                            Message = $"Sản phẩm {cartItem.Product.ProductName} không đủ số lượng yêu cầu",
                            IsSuccess = false
                        };
                    }
                }
                else
                {
                    return new EntityResponseMessage
                    {
                        Message = $"Không tồn tại sản phẩm có id {productId} trong giỏ hàng",
                        IsSuccess = false
                    };
                }
            }

            var address = await _context.Addresses.FindAsync(addressId);

            if (address != null)
            {
                var order = new Order()
                {
                    CustomerId = userId,
                    DateCreated = DateTime.Now,
                    Status = "Chờ xác nhận",
                    Street = address.Street,
                    Ward = address.Ward,
                    District = address.District,
                    City = address.City
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                foreach (var cartItem in cartItems)
                {
                    var orderdetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        PercentSale = cartItem.Product.PercentSale,
                        Price = cartItem.Product.Price,
                        Total = (cartItem.Quantity * cartItem.Product.Price) * (decimal)(100.0 - cartItem.Product.PercentSale) / 100
                    };

                    await _context.OrderDetails.AddAsync(orderdetail);
                }

                await _context.SaveChangesAsync();
                return new EntityResponseMessage
                {
                    Message = $"Đơn hàng của bạn đã tạo thành công",
                    IsSuccess = true
                };
            }
            else
            {
                return new EntityResponseMessage
                {
                    Message = $"Id của địa chỉ không tồn tại",
                    IsSuccess = false
                };
            }
        }

        public List<OrderView> GetOrders(string userId)
        {
            var orders = _mapper.Map<List<OrderView>>(_context.Orders.Where(o => o.CustomerId == userId).ToList());
            return orders;
        }

        

        public OrderView GetOrder(string userId, int orderId)
        {
            var order = _mapper.Map<OrderView>(_context.Orders.FirstOrDefault(o => o.CustomerId == userId && o.Id == orderId));
            return order;
        }

    }
}
