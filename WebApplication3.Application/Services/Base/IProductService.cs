using WebApplication3.Application.DTOs;

namespace WebApplication3.Application.Services.Base
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();

        Task<ProductUpdateDto?> GetProductById(int id);

        Task AddProduct(ProductCreateDto productDTO);

        Task UpdateProduct(ProductUpdateDto productDTO);

        Task DeleteProduct(int id);

        Task<IEnumerable<CategoryDto>> GetAllCategories();
    }
}