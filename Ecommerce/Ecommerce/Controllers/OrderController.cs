using Ecommerce.Data.IService;
using Ecommerce.Models;
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
                var result = _orderService.GetOrders(userId);
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
                var result = _orderService.GetOrder(userId, id);
                return Ok(result);
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Thông tin cần điền không hợp lệ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(List<int> productIds, int addressId)
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
                    var result = await _orderService.StoreOrderAsync(productIds, User.FindFirstValue(ClaimTypes.NameIdentifier), addressId);
                    return Ok(result);
                }
                catch (DbUpdateException e)
                {
                    return BadRequest("Thông tin cần điền không hợp lệ");
                }
            }
        }
    }
}
