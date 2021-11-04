using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class MerchRequestIssuedDateException : Exception
    {
        public MerchRequestIssuedDateException(string message) : base(message)
        {
        }
        
        public MerchRequestIssuedDateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}