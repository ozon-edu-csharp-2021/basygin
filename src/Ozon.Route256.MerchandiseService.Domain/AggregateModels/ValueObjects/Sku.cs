using System.Collections.Generic;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects
{
    public class Sku : ValueObject
    {
        public long Value { get; private set; }
        
        public Sku(long sku)
        {
            SetSku(sku);
        }
        
        private void SetSku(long sku)
        {
            if (sku < 0)
            {
                throw new InvalidSkuException("Sku is negative");
            }
            
            if (sku == 0)
            {
                throw new InvalidSkuException("Sku is zero");
            }

            Value = sku;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}