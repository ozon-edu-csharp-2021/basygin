using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class MerchRequestItemArgumentNullException : Exception
    {
        public MerchRequestItemArgumentNullException(string message) : base(message)
        {
        }
        
        public MerchRequestItemArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}