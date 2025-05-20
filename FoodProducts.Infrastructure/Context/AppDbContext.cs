using FoodProducts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodProducts.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
