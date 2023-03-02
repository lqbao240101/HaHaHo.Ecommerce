﻿using Ecommerce.Data.IService;
using Ecommerce.Models;
using HAHAHO.ShopHuongDuong.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allProducts = await _productService.GetAllAsync(n => n.Category);
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
        public async Task<IActionResult> Create([Bind("ProductName, Description, Price, PercentSale, ProductImg, CategoryId, Quantity")] CreateProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            else
            {
                try { 
                await _productService.AddNewProductAsync(product);
                return Ok(product);
                } catch (DbUpdateException e)
                {
                    return BadRequest("CategoryId không tồn tại");
                }
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(int productId, Product product)
        {
            if (productId == product.Id)
            {
                await _productService.UpdateAsync(productId, product);
                return Ok(product);
            }
            else
            {
                return NotFound(productId);
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
