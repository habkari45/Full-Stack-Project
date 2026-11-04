using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET ALL CATEGORIES
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            return Ok(categories);
        }

        // GET CATEGORY BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // CREATE CATEGORY
        [HttpPost]
        public async Task<IActionResult> AddCategory(
            [FromBody] CategoryCreateDto categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.AddCategory(categoryDTO);

            return Ok(new
            {
                message = "Category added successfully"
            });
        }

        // UPDATE CATEGORY
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(
            [FromBody] CategoryUpdateDto categoryDTO)
        {
            try
            {
                await _categoryService.UpdateCategory(categoryDTO);

                return Ok(new
                {
                    message = "Category updated successfully"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE CATEGORY
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);

                return Ok(new
                {
                    message = "Category deleted successfully"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}