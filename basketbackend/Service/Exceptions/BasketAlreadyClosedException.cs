namespace basketbackend.Service.Exceptions
{
    public class BasketAlreadyClosedException : GracefulException
    {
        public BasketAlreadyClosedException(string message) : base(message)
        {
        }
    }
}
