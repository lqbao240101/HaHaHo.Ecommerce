using Ecommerce.Data.IService;
using Ecommerce.Data.Service;
using Ecommerce.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(NewCartItemModel data)
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
                    await _cartItemService.AddNewCartItemAsync(userId, data);
                    return Ok(data);
                }
                catch (DbUpdateException e)
                {
                    return BadRequest("Thông tin cần điền không hợp lệ");
                }
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItem = await _cartItemService.GetCartItemsByUserId(userId);
            return Ok(cartItem);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Detail(int productId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItem = await _cartItemService.GetCartItemDetail(userId, productId);
            if (cartItem == null)
            {
                return NotFound();
            }

            return Ok(cartItem);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(int productId, UpdateCartItemModel cartItem)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (productId == cartItem.ProductId && userId == cartItem.CustomerId)
            {
                await _cartItemService.UpdateCartItemAsync(cartItem);
                return Ok(cartItem);
            }
            else
            {
                return NotFound(productId);
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            //string userId = ClaimTypes.NameIdentifier;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItem = await _cartItemService.GetCartItemDetail(userId, productId);
            if (cartItem == null)
            {
                return NotFound();
            }

            await _cartItemService.DeleteCartItemAsync(userId, productId);
            return Ok(productId);
        }
    }
}
