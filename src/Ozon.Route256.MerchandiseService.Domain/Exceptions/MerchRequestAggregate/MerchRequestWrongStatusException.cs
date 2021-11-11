using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class MerchRequestWrongStatusException : Exception
    {
        public MerchRequestWrongStatusException(string message) : base(message)
        {
        }
        
        public MerchRequestWrongStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}