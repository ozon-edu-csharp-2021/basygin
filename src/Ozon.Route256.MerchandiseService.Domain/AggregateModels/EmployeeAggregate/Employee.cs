using System;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.EmployeeAggregate
{
    public class Employee : Entity
    {
        private int employeeId;
        private object t;
        private Email email;

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

        public Employee(int employeeId, object t, Email email)
        {
            this.employeeId = employeeId;
            this.t = t;
            this.email = email;
        }

        public Size Size { get; }
        public Email Email { get; }

        private void SetId(int id)
        {
            Id = id;
        }
    }
}