namespace ECommerceBackend.Application.Exceptions
{
    public class CreateOrUpdatePaymentIntentErrorException : Exception
    {
        public CreateOrUpdatePaymentIntentErrorException() : base("CreateOrUpdatePaymentIntent ERROR!!!")
        {
        }

        public CreateOrUpdatePaymentIntentErrorException(string? message) : base(message)
        {
        }

        public CreateOrUpdatePaymentIntentErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
