using Products.Application.DTOs.Categories;
using Products.Domain.Entities;

namespace Products.Application.Interfaces
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
