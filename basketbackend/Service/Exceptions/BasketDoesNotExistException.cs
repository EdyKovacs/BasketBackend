namespace basketbackend.Service.Exceptions
{
    public class BasketDoesNotExistException : GracefulException
    {
        public BasketDoesNotExistException(string message) : base(message)
        {
        }
    }
}
