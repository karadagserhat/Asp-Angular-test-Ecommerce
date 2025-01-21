namespace ECommerceBackend.Application.Exceptions
{
    public class ProductUpdateFailedException : Exception
    {
        public ProductUpdateFailedException() : base("There is no product for updated!")
        {
        }

        public ProductUpdateFailedException(string? message) : base(message)
        {
        }

        public ProductUpdateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
