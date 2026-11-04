using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Repositories.Base;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET ALL
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

        // GET BY ID
        public async Task<CategoryUpdateDto?> GetCategoryById(int id)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetCategoryByIdAsync(id);

            if (category == null)
            {
                return null;
            }

            return new CategoryUpdateDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        // CREATE
        public async Task AddCategory(CategoryCreateDto categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name
            };

            await _unitOfWork.CategoryRepository
                .AddCategoryAsync(category);
        }

        // UPDATE
        public async Task UpdateCategory(CategoryUpdateDto categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetCategoryByIdAsync(categoryDTO.Id);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            category.Name = categoryDTO.Name;

            await _unitOfWork.CategoryRepository
                .UpdateCategoryAsync(category);
        }

        // DELETE
        public async Task DeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetCategoryByIdAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            await _unitOfWork.CategoryRepository
                .DeleteCategoryAsync(id);
        }
    }
}