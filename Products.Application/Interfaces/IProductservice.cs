using Products.Application.DTOs.Product;

namespace Products.Application.Interfaces
{
    public interface IProductservice
    {
        Task<List<ProductResponseDto>> GetAllAsync();
        Task<ProductResponseDto?> GetByIdAsync(Guid id);
        Task<ProductResponseDto> CreateAsync(ProductCreateDto dto);
        Task<ProductResponseDto> UpdateAsync(Guid id, ProductUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
