using System;

namespace basketbackend.Service.Exceptions
{
    public class BasketExistsException : ApplicationException
    {
        public BasketExistsException(string message) : base(message)
        {
        }
    }
}
