namespace Products.Application.DTOs.Categories
{
    public class CategoryUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
