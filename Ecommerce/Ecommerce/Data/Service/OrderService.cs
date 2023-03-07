using AutoMapper;
using Ecommerce.Data.Enums;
using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
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
            Dictionary<CartItem, Product> list = new();

            foreach (var productId in productIds)
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(n => n.ProductId == productId && n.CustomerId == userId);

                if (cartItem != null)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(n => n.Id == cartItem.ProductId);

                    if (product != null)
                    {
                        if (cartItem.Quantity <= product.Quantity)
                            list.Add(cartItem, product);
                        else
                        {
                            return new EntityResponseMessage
                            {
                                Message = $"Sản phẩm {product.ProductName} không đủ số lượng yêu cầu",
                                IsSuccess = false,
                                StatusCode = 400
                            };
                        }
                    }
                    else
                    {
                        return new EntityResponseMessage
                        {
                            Message = $"Sản phẩm có id {productId} không tồn tại",
                            IsSuccess = false,
                            StatusCode = 404
                        };
                    }
                }
                else
                {
                    return new EntityResponseMessage
                    {
                        Message = $"Không tồn tại sản phẩm có id {productId} trong giỏ hàng",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }
            }

            var address = await _context.Addresses.FindAsync(addressId);

            if (address != null)
            {
                return await CreateOrder(list, address, userId);
            }
            else
            {
                return new EntityResponseMessage
                {
                    Message = $"Id của địa chỉ không tồn tại",
                    IsSuccess = false,
                    StatusCode = 404
                };
            }
        }
        public async Task<EntityResponseMessage> CreateOrder(Dictionary<CartItem, Product> list, Address address, string userId)
        {
            var order = new Order()
            {
                CustomerId = userId,
                DateCreated = DateTime.Now,
                Status = OrderStatus.Pending,
                Street = address.Street,
                Ward = address.Ward,
                District = address.District,
                City = address.City
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var cartItem in list)
            {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.Key.ProductId,
                        Quantity = cartItem.Key.Quantity,
                        PercentSale = cartItem.Value.PercentSale,
                        Price = cartItem.Value.Price,
                        Total = (cartItem.Key.Quantity * cartItem.Value.Price) * (decimal)(100.0 - cartItem.Value.PercentSale) / 100
                    };

                    cartItem.Value.Quantity -= orderDetail.Quantity;

                    await _context.OrderDetails.AddAsync(orderDetail);
            }

            _context.CartItems.RemoveRange(list.Keys);
            await _context.SaveChangesAsync();

            return new EntityResponseMessage
            {
                Message = $"Đơn hàng của bạn đã tạo thành công",
                IsSuccess = true
            };
        }
        public async Task<List<OrderView>> GetOrders(string userId)
        {
            var orders = _mapper.Map<List<OrderView>>(await _context.Orders.Include(n => n.OrderDetails).Where(o => o.CustomerId == userId).ToListAsync());
            return orders;
        }

        public async Task<OrderView> GetOrder(string userId, int orderId)
        {
            var order = _mapper.Map<OrderView>(
                await _context.Orders.Include(n => n.OrderDetails)
                .FirstOrDefaultAsync(o => o.CustomerId == userId && o.Id == orderId));
            return order;
        }

        public async Task<EntityResponseMessage> CancelOrder(int id, string role, string userId)
        {
            Order? order = null;
            if (role.Equals("Admin") || role.Equals("SuperAdmin"))
            {
                order = await _context.Orders.FirstOrDefaultAsync(n => n.Id == id);
            }
            else
            if (role.Equals("User"))
            {
                order = await _context.Orders.FirstOrDefaultAsync(n => n.Id == id && n.CustomerId == userId);
            }

            if (order != null)
            {
                if (order.Status == OrderStatus.Pending)
                {
                    order.Status = OrderStatus.Canceled;

                    var listOrderDetail = await _context.OrderDetails.Where(o => o.OrderId == id).ToListAsync();

                    foreach (var orderDetail in listOrderDetail)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == orderDetail.ProductId);

                        if (product != null)
                        {
                            product.Quantity += orderDetail.Quantity;
                        }
                    }

                    await _context.SaveChangesAsync();

                    return new EntityResponseMessage
                    {
                        Message = $"Đơn hàng đã bị hủy",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new EntityResponseMessage
                    {
                        Message = $"Hủy đơn hàng không thành công do trạng thái không hợp lệ",
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }
            }
            else
            {
                return new EntityResponseMessage
                {
                    Message = $"Không tìm thấy đơn hàng có mã {id}",
                    IsSuccess = false,
                    StatusCode = 404
                };
            }
        }
    }
}