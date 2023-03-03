using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var allProducts = await _productService.GetAllAsync();
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([Bind("ProductName, Description, Price, PercentSale, ProductImg, CategoryId, Quantity")] NewProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            else
            {
                try
                {
                    await _productService.AddNewProductAsync(product);
                    return Ok(product);
                }
                catch (DbUpdateException e)
                {
                    return BadRequest("Thông tin cần điền không hợp lệ");
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NewProductModel product)
        {
            if (id == product.Id)
            {
                await _productService.UpdateProductAsync(product);
                return Ok(product);
            }
            else
            {
                return NotFound(id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                await _productService.DeleteAsync(id);
                return Ok("Đã xóa");
            }
        }
    }
}
