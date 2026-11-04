using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(
            IProductService productService,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET ALL
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();

            return View(products);
        }

        // GET CATEGORIES
        private async Task LoadCategories()
        {
            var categories = await _productService.GetAllCategories();

            ViewBag.Categories = new SelectList(
                categories,
                "Id",
                "Name");
        }

        // VALIDATE IMAGE
        private void ValidateImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return;
            }

            if (imageFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError(
                    "imageFile",
                    "Image size must not exceed 5 MB.");
            }

            var extension = Path.GetExtension(
                imageFile.FileName).ToLowerInvariant();

            var allowedExtensions = new[]
            {
                ".jpg", ".jpeg", ".png"
            };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(
                    "imageFile",
                    "Only JPG, JPEG and PNG images are allowed.");
            }
        }

        // SAVE IMAGE
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var webRootPath = _webHostEnvironment.WebRootPath
                ?? Path.Combine(
                    _webHostEnvironment.ContentRootPath,
                    "wwwroot");

            var folderPath = Path.Combine(
                webRootPath,
                "images",
                "products");

            Directory.CreateDirectory(folderPath);

            var extension = Path.GetExtension(
                imageFile.FileName).ToLowerInvariant();

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(
                folderPath,
                fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.CreateNew))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/images/products/{fileName}";
        }

        // CREATE GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCategories();

            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(
            ProductCreateDto productDTO,
            IFormFile? imageFile)
        {
            ValidateImage(imageFile);

            var categories = await _productService.GetAllCategories();

            if (!categories.Any(x => x.Id == productDTO.CategoryId))
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Please select a valid category.");
            }

            if (!ModelState.IsValid)
            {
                await LoadCategories();

                return View(productDTO);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                productDTO.ImageUrl = await SaveImage(imageFile);
            }

            await _productService.AddProduct(productDTO);

            return RedirectToAction("Index");
        }

        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var productDTO = await _productService
                .GetProductById(id);

            if (productDTO == null)
            {
                return NotFound();
            }

            await LoadCategories();

            return View(productDTO);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(
            ProductUpdateDto productDTO,
            IFormFile? imageFile)
        {
            var existingProduct = await _productService
                .GetProductById(productDTO.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            // Keep the current image
            productDTO.ImageUrl = existingProduct.ImageUrl;

            ValidateImage(imageFile);

            var categories = await _productService.GetAllCategories();

            if (!categories.Any(x => x.Id == productDTO.CategoryId))
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Please select a valid category.");
            }

            if (!ModelState.IsValid)
            {
                await LoadCategories();

                return View(productDTO);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                productDTO.ImageUrl = await SaveImage(imageFile);
            }

            try
            {
                await _productService.UpdateProduct(productDTO);
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
            var productDTO = await _productService
                .GetProductById(id);

            if (productDTO == null)
            {
                return NotFound();
            }

            return View(productDTO);
        }

        // DELETE POST
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}