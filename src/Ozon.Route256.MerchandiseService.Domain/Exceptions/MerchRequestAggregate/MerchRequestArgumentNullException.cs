using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class MerchRequestArgumentNullException : Exception
    {
        public MerchRequestArgumentNullException(string message) : base(message)
        {
        }
        
        public MerchRequestArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}