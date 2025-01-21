namespace ECommerceBackend.Application.Exceptions
{
    public class ProductGetFailedException : Exception
    {
        public ProductGetFailedException() : base("There is no product!")
        {
        }

        public ProductGetFailedException(string? message) : base(message)
        {
        }

        public ProductGetFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
