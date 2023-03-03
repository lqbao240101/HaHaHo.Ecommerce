using Ecommerce.Data.IService;
using Ecommerce.Data.Service;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService) {
            _addressService = addressService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string userId = ClaimTypes.NameIdentifier;
            string userRole = ClaimTypes.Role;

            var addresses = await _addressService.GetAddressesByUserIdAndRoleAsync(userId);
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            string userId = ClaimTypes.NameIdentifier;
            string userRole = ClaimTypes.Role;

            var address = await _addressService.GetAddressDetail(userId, id);
            if(address == null)
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
            try
            {
                await _addressService.AddNewAddressAsync(address);
                return Ok(address);
            }
            catch (DbUpdateException de)
            {
                return StatusCode(409, de);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = ClaimTypes.NameIdentifier;
            
            var address = _addressService.GetAddressDetail(userId, id);
            if(address == null)
            {
                return NotFound();
            }

            await _addressService.DeleteAsync(id);
            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NewAddressModel address)
        {
            if (id == address.Id)
            {
                string userId = ClaimTypes.NameIdentifier;
                await _addressService.UpdateAddressAsync(userId, address);
                return Ok(address);
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
