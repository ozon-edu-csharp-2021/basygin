using System;
using System.Collections.Generic;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate
{
    public class Employee : ValueObject
    {
        public Employee(Identifier id, Size size, Email email)
        {
            Id = id ?? throw new EmployeeArgumentNullException(nameof(id));
            Size = size ?? throw new EmployeeArgumentNullException(nameof(size));
            Email = email ?? throw new EmployeeArgumentNullException(nameof(email));
        }
        
        public Identifier Id { get; }
        public Size Size { get; }
        public Email Email { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Size;
            yield return Email;
        }
    }
}