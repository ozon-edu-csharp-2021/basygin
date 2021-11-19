using System.Collections.Generic;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects
{
    public class IssuedQuantity : ValueObject
    {
        public IssuedQuantity(int value)
        {
            SetIssuedQuantity(value);
        }

        public int Value { get; private set; }

        private void SetIssuedQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new InvalidQuantityException("Quantity is less or equal to zero");
            }

            Value = quantity;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}