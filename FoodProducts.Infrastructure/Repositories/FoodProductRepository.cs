using FoodProducts.Domain.Entities;
using FoodProducts.Domain.Interfaces;
using FoodProducts.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodProducts.Infrastructure.Repositories
{
    public class FoodProductRepository : IFoodProductRepository
    {
        private readonly AppDbContext _context;

        public FoodProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FoodProduct>> GetAllAsync() =>
            await _context.FoodProducts.Include(p => p.Category).ToListAsync();

        public async Task<FoodProduct?> GetByIdAsync(Guid id) =>
            await _context.FoodProducts.Include(p => p.Category)
                   .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<FoodProduct> CreateAsync(FoodProduct product)
        {
            _context.FoodProducts.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<FoodProduct> UpdateAsync(FoodProduct product)
        {
            _context.FoodProducts.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.FoodProducts.FindAsync(id);
            if (existing == null) return false;

            _context.FoodProducts.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
