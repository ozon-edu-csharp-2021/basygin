using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class MerchRequestItemAlreadyExistException : Exception
    {
        public MerchRequestItemAlreadyExistException(string message) : base(message)
        {
        }
        
        public MerchRequestItemAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}