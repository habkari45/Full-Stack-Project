using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Repositories.Base;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET ALL
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _unitOfWork.ProductRepository
                .GetAllProductsAsync();

            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty
            });
        }

        // GET BY ID
        public async Task<ProductUpdateDto?> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .GetProductByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductUpdateDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId
            };
        }

        // GET CATEGORIES
        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categories = await _unitOfWork.CategoryRepository
                .GetAllCategoriesAsync();

            return categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        // CREATE
        public async Task AddProduct(ProductCreateDto productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ImageUrl = productDTO.ImageUrl,
                CategoryId = productDTO.CategoryId
            };

            await _unitOfWork.ProductRepository
                .AddProductAsync(product);
        }

        // UPDATE
        public async Task UpdateProduct(ProductUpdateDto productDTO)
        {
            var product = await _unitOfWork.ProductRepository
                .GetProductByIdAsync(productDTO.Id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.ImageUrl = productDTO.ImageUrl;
            product.CategoryId = productDTO.CategoryId;

            await _unitOfWork.ProductRepository
                .UpdateProductAsync(product);
        }

        // DELETE
        public async Task DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .GetProductByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            await _unitOfWork.ProductRepository
                .DeleteProductAsync(id);
        }
    }
}