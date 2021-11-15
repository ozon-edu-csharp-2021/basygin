using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class InvalidMerchRequestItemIssuedQuantityException : Exception
    {
        public InvalidMerchRequestItemIssuedQuantityException(string message) : base(message)
        {
        }
        
        public InvalidMerchRequestItemIssuedQuantityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}