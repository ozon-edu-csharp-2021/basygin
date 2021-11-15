using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class InvalidSkuException : Exception
    {
        public InvalidSkuException(string message) : base(message)
        {
        }
        
        public InvalidSkuException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}