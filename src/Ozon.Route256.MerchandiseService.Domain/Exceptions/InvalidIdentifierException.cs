using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class InvalidIdentifierException : Exception
    {
        public InvalidIdentifierException(string message) : base(message)
        {
        }
        
        public InvalidIdentifierException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}