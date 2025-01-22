namespace ECommerceBackend.Application.Exceptions
{
    public class CartUpdateFailedException : Exception
    {
        public CartUpdateFailedException() : base("There is no Cart for updated!")
        {
        }

        public CartUpdateFailedException(string? message) : base(message)
        {
        }

        public CartUpdateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
