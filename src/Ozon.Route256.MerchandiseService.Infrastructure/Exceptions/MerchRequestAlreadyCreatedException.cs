using System;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Exceptions
{
    public class MerchRequestAlreadyCreatedException : Exception
    {
        public MerchRequestAlreadyCreatedException(string message) : base(message)
        {
        }
        
        public MerchRequestAlreadyCreatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}