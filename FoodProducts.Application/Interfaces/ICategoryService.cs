using FoodProducts.Application.DTOs.Categories;
using FoodProducts.Domain.Entities;

namespace FoodProducts.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> CreateAsync(CategoryCreateDto dto);
        Task<Category> UpdateAsync(CategoryUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
