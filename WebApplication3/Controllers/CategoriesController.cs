using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET ALL
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService
                .GetAllCategories();

            return View(categories);
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(
            CategoryCreateDto categoryDTO)
        {
            if (string.IsNullOrWhiteSpace(categoryDTO.Name))
            {
                ModelState.AddModelError(
                    "Name",
                    "Category name is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(categoryDTO);
            }

            await _categoryService.AddCategory(categoryDTO);

            return RedirectToAction("Index");
        }

        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDTO = await _categoryService
                .GetCategoryById(id);

            if (categoryDTO == null)
            {
                return NotFound();
            }

            return View(categoryDTO);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(
            CategoryUpdateDto categoryDTO)
        {
            if (string.IsNullOrWhiteSpace(categoryDTO.Name))
            {
                ModelState.AddModelError(
                    "Name",
                    "Category name is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(categoryDTO);
            }

            try
            {
                await _categoryService.UpdateCategory(categoryDTO);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        // DELETE GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDTO = await _categoryService
                .GetCategoryById(id);

            if (categoryDTO == null)
            {
                return NotFound();
            }

            return View(categoryDTO);
        }

        // DELETE POST
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] =
                    "Cannot delete this category because it contains products.";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}