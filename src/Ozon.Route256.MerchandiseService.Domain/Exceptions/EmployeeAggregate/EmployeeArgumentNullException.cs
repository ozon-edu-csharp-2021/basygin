using System;

namespace Ozon.Route256.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeArgumentNullException : Exception
    {
        public EmployeeArgumentNullException(string message) : base(message)
        {
        }
        
        public EmployeeArgumentNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}