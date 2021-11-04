using System;
using System.Runtime.Serialization;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {
        }
        
        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}