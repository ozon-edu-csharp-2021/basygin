using System.Collections.Generic;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects
{
    public class Quantity : ValueObject
    {
        public Quantity(int value)
        {
            SetQuantity(value);
        }

        public int Value { get; private set; }

        private void SetQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new InvalidQuantityException("Quantity is negative");
            }
            
            if (quantity == 0)
            {
                throw new InvalidQuantityException("Quantity is zero");
            }

            Value = quantity;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}