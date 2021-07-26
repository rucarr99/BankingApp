using System;

namespace Services.Exceptions
{
    public class InsufficientMoneyException : Exception
    {
        public InsufficientMoneyException()
        {

        }
        public InsufficientMoneyException(string message) : base(message)
        {

        }
        public InsufficientMoneyException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
