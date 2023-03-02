using Ecommerce.Data.IService;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> GetAll()
        {
            int userId = Int32.Parse(ClaimTypes.NameIdentifier);
            string userRole = ClaimTypes.Role;

            var addresses = await _addressService.GetAddressesByUserIdAndRoleAsync(userId, userRole);
            return Ok(addresses);
        }


    }
}
