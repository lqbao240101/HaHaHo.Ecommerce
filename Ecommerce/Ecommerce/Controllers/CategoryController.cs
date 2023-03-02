using Ecommerce.Data.IService;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = await _categoryService.GetAllAsync();
            return Ok(allCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([Bind("CategoryName, Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _categoryService.AddAsync(category);
                return Ok(category);
            }
            catch (DbUpdateException de)
            {
                return StatusCode(409, de);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                await _categoryService.DeleteAsync(id);
                return Ok(category);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [Bind("Id,CategoryName, Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id == category.Id)
            {
                try
                {
                    await _categoryService.UpdateAsync(id, category);
                    return Ok(category);
                }
                catch (DbUpdateException de)
                {
                    return StatusCode(409, de);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
