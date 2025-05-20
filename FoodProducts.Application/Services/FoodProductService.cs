using FoodProducts.Application.DTOs.FoodProduct;
using FoodProducts.Application.Interfaces;
using FoodProducts.Domain.Entities;
using FoodProducts.Domain.Exceptions;
using FoodProducts.Domain.Interfaces;

namespace FoodProducts.Application.Services
{
    public class FoodProductService : IFoodProductService
    {
        private readonly IFoodProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public FoodProductService(IFoodProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<List<FoodProductResponseDto>> GetAllAsync()
        {
            var entities = await _productRepo.GetAllAsync();
            return entities.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<FoodProductResponseDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return product is null ? null : MapToResponseDto(product);
        }

        public async Task<FoodProductResponseDto> CreateAsync(FoodProductCreateDto dto)
        {
            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId)
                           ?? throw new DomainException("Categoría no encontrada.");

            var product = new FoodProduct(dto.Name, dto.Description, dto.Price, category);
            var created = await _productRepo.CreateAsync(product);

            return MapToResponseDto(created);
        }

        public async Task<FoodProductResponseDto> UpdateAsync(Guid id, FoodProductUpdateDto dto)
        {
            var existing = await _productRepo.GetByIdAsync(id)
                           ?? throw new DomainException("Producto no encontrado.");

            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId)
                           ?? throw new DomainException("Categoría no encontrada.");

            existing.ChangeName(dto.Name);
            existing.ChangeDescription(dto.Description);
            existing.ChangePrice(dto.Price);
            existing.ChangeCategory(category);

            var updated = await _productRepo.UpdateAsync(existing);
            return MapToResponseDto(updated);
        }

        public async Task<bool> DeleteAsync(Guid id) =>
            await _productRepo.DeleteAsync(id);

        private static FoodProductResponseDto MapToResponseDto(FoodProduct p)
        {
            return new FoodProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            };
        }
    }
}
