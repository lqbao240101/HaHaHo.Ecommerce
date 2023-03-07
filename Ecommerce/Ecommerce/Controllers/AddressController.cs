using Ecommerce.Data.IService;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var addresses = await _addressService.GetAddressesByUserId(userId);
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var address = await _addressService.GetAddressDetail(userId, id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(NewAddressModel address)
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
                    await _addressService.AddNewAddressAsync(userId, address);
                    return Ok(address);
                }
                catch (DbUpdateException e)
                {
                    return BadRequest("Thông tin cần điền không hợp lệ");
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var address = await _addressService.GetAddressDetail(userId, id);
            if (address == null)
            {
                return NotFound();
            }

            await _addressService.DeleteAsync(id);
            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAddressModel address)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == address.Id && userId == address.CustomerId)
            {
                await _addressService.UpdateAddressAsync(address);
                return Ok(address);
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
