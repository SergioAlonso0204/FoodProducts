namespace Products.Domain.Exceptions
{
    public class ProductNameException : DomainException
    {
        public ProductNameException(string message) : base(message)
        {
        }
    }
}
