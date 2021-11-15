using System;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.EmployeeAggregate
{
    public class Employee : Entity
    {
        public Employee(int id, Size size, Email email)
        {
            Size = size;
            Email = email;
            
            if (id <= 0)
            {
                throw new Exception();
            }
            
            SetId(id);
        }
        
        public Size Size { get; }
        public Email Email { get; }

        private void SetId(int id)
        {
            Id = id;
        }
    }
}