namespace ECommerceBackend.Application.Exceptions
{
    public class CartDeleteFailedException : Exception
    {
        public CartDeleteFailedException() : base("There is no Cart for Deleted!")
        {
        }

        public CartDeleteFailedException(string? message) : base(message)
        {
        }

        public CartDeleteFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
