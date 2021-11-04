using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message)
        {
        }
        
        public InvalidQuantityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}