using Products.Domain.Exceptions;

namespace Products.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre de la categoría no puede estar vacío.");

            Id = Guid.NewGuid();
            Name = name;
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("El nombre no puede estar vacío");
            Name = newName;
        }

        protected Category() { }
    }


}
