using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions
{
    public class MerchPackItemArgumentNullException : Exception
    {
        public MerchPackItemArgumentNullException(string message) : base(message)
        {
        }
        
        public MerchPackItemArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}