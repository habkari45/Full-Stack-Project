using WebApplication3.Application.DTOs;

namespace WebApplication3.Application.Services.Base
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();

        Task<CategoryUpdateDto?> GetCategoryById(int id);

        Task AddCategory(CategoryCreateDto categoryDTO);

        Task UpdateCategory(CategoryUpdateDto categoryDTO);

        Task DeleteCategory(int id);
    }
}