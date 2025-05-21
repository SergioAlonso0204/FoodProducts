using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _context.Products.Include(p => p.Category).ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id) =>
            await _context.Products.Include(p => p.Category)
                   .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return false;

            _context.Products.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
