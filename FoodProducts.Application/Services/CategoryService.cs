using FoodProducts.Application.DTOs.Categories;
using FoodProducts.Application.Interfaces;
using FoodProducts.Domain.Entities;
using FoodProducts.Domain.Exceptions;
using FoodProducts.Domain.Interfaces;


namespace FoodProducts.Application.Services
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

            // Actualizamos la propiedad con método explícito en la entidad (recomendado)
            existing.ChangeName(dto.Name);

            return await _categoryRepo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(Guid id) =>
            await _categoryRepo.DeleteAsync(id);
    }
}
