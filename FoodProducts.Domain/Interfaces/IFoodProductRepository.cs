using FoodProducts.Domain.Entities;

namespace FoodProducts.Domain.Interfaces
{
    public interface IFoodProductRepository
    {
        Task<List<FoodProduct>> GetAllAsync();
        Task<FoodProduct?> GetByIdAsync(Guid id);
        Task<FoodProduct> CreateAsync(FoodProduct product);
        Task<FoodProduct> UpdateAsync(FoodProduct product);
        Task<bool> DeleteAsync(Guid id);
    }
}
