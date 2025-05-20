using FoodProducts.Domain.Exceptions;

namespace FoodProducts.Domain.Entities
{
    public class FoodProduct
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = default!;

        public FoodProduct(string name, string description, decimal price, Category category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new FoodProductNameException("El nombre del producto no puede estar vacío.");

            if (price < 0)
                throw new DomainException("El precio no puede ser negativo.");

            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;

            Name = name;
            Description = description;
            Price = price;

            Category = category ?? throw new DomainException("La categoría es obligatoria.");
            CategoryId = category.Id;
        }

        protected FoodProduct() { } // Para EF Core

        // Métodos para modificar estado (encapsulación)

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new FoodProductNameException("El nombre del producto no puede estar vacío.");
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            Description = description ?? string.Empty;
        }

        public void ChangePrice(decimal price)
        {
            if (price < 0)
                throw new DomainException("El precio no puede ser negativo.");
            Price = price;
        }

        public void ChangeCategory(Category category)
        {
            Category = category ?? throw new DomainException("La categoría es obligatoria.");
            CategoryId = category.Id;
        }
    }
}
