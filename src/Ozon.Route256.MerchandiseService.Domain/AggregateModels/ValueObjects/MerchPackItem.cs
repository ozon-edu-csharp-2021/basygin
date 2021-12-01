using System;
using System.Collections.Generic;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects
{
    public class MerchPackItem : Entity
    {
        public MerchPackItem(Sku sku, Quantity quantity)
        {
            Sku = sku ?? throw new MerchPackItemArgumentNullException(nameof(sku));
            Quantity = quantity ?? throw new MerchPackItemArgumentNullException(nameof(quantity));
        }

        public Sku Sku { get; }
        
        public Quantity Quantity { get; }
    }
}