namespace FoodProducts.Domain.Exceptions
{
    public class FoodProductNameException : DomainException
    {
        public FoodProductNameException(string message) : base(message)
        {
        }
    }
}
