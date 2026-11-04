using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET ALL PRODUCTS
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        // GET PRODUCT BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // CREATE PRODUCT
        [HttpPost]
        public async Task<IActionResult> AddProduct(
            [FromBody] ProductCreateDto productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.AddProduct(productDTO);

            return Ok(new
            {
                message = "Product added successfully"
            });
        }

        // UPDATE PRODUCT
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            [FromBody] ProductUpdateDto productDTO)
        {
            try
            {
                await _productService.UpdateProduct(productDTO);

                return Ok(new
                {
                    message = "Product updated successfully"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE PRODUCT
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);

                return Ok(new
                {
                    message = "Product deleted successfully"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}