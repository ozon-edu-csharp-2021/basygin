using System;
using System.Runtime.Serialization;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    internal class InvalidMerchTypeException : Exception
    {
        public InvalidMerchTypeException(string message) : base(message)
        {
        }

        public InvalidMerchTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}