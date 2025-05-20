using FoodProducts.Application.DTOs.FoodProduct;

namespace FoodProducts.Application.Interfaces
{
    public interface IFoodProductService
    {
        Task<List<FoodProductResponseDto>> GetAllAsync();
        Task<FoodProductResponseDto?> GetByIdAsync(Guid id);
        Task<FoodProductResponseDto> CreateAsync(FoodProductCreateDto dto);
        Task<FoodProductResponseDto> UpdateAsync(Guid id, FoodProductUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
