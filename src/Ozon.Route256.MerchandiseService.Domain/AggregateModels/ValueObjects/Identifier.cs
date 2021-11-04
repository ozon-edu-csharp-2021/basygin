using System.Collections.Generic;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects
{
    public class Identifier : ValueObject
    {
        public long Value { get; private set; }

        public Identifier(long value)
        {
            SetValue(value);
        }

        private void SetValue(long value)
        {
            if (value <= 0)
            {
                throw new InvalidIdentifierException("MerchRequestId is too less than 1");
            }

            Value = value;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}