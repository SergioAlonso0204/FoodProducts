using Products.Application.DTOs.Product;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Domain.Exceptions;
using Products.Domain.Interfaces;

namespace Products.Application.Services
{
    public class Productservice : IProductservice
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public Productservice(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<List<ProductResponseDto>> GetAllAsync()
        {
            var entities = await _productRepo.GetAllAsync();
            return entities.Select(p => MapToResponseDto(p)).ToList();
        }

        public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return product is null ? null : MapToResponseDto(product);
        }

        public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
        {
            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId)
                           ?? throw new DomainException("Categoría no encontrada.");

            var product = new Product(dto.Name, dto.Description, dto.Price, category);
            var created = await _productRepo.CreateAsync(product);

            return MapToResponseDto(created);
        }

        public async Task<ProductResponseDto> UpdateAsync(Guid id, ProductUpdateDto dto)
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

        private static ProductResponseDto MapToResponseDto(Product p)
        {
            return new ProductResponseDto
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
