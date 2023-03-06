using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var result = await _orderService.GetOrders(userId);
                return Ok(result);
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Thông tin cần điền không hợp lệ");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var result = await _orderService.GetOrder(userId, id);
                return Ok(result);
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Thông tin cần điền không hợp lệ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(NewOrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            else
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    var result = await _orderService.StoreOrderAsync(order.ProductIds, userId, order.AddressId);

                    return result.StatusCode switch
                    {
                        400 => BadRequest(result),
                        404 => NotFound(result),
                        _ => Ok(result),
                    };
                }
                catch (DbUpdateException e)
                {
                    return BadRequest("Thông tin cần điền không hợp lệ");
                }
            }
        }

        //[HttpPost("{id}")]
        //public async Task<IActionResult> CancelOrder(int id)
        //{
            
        //}
    }
}
