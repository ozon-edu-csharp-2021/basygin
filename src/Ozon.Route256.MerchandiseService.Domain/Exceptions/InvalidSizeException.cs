using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions
{
    public class InvalidSizeException : Exception
    {
        public InvalidSizeException(string message) : base(message)
        {
        }
        
        public InvalidSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}