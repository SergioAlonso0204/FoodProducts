using Products.Application.DTOs.Categories;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Domain.Exceptions;
using Products.Domain.Interfaces;


namespace Products.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _categoryRepo.GetAllAsync();

        public async Task<Category?> GetByIdAsync(Guid id) =>
            await _categoryRepo.GetByIdAsync(id);

        public async Task<Category> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category(dto.Name);
            return await _categoryRepo.CreateAsync(category);
        }

        public async Task<Category> UpdateAsync(CategoryUpdateDto dto)
        {
            var existing = await _categoryRepo.GetByIdAsync(dto.Id)
                ?? throw new DomainException("Categoría no encontrada.");

            existing.ChangeName(dto.Name);

            return await _categoryRepo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(Guid id) =>
            await _categoryRepo.DeleteAsync(id);
    }
}
