using System;
using System.Collections.Generic;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate
{
    public class MerchRequestItem : Entity
    {
        public MerchRequestItem(Sku sku, Quantity quantity)
        {
            Sku = sku ?? throw new MerchRequestItemArgumentNullException(nameof(sku));
            Quantity = quantity ?? throw new MerchRequestItemArgumentNullException(nameof(quantity));
            IssuedQuantity = new IssuedQuantity(0);
        }
        
        public MerchRequestItem(Sku sku, Quantity quantity, IssuedQuantity issuedQuantity)
        : this(sku, quantity)
        {
            if (issuedQuantity == null)
            {
                throw new MerchRequestItemArgumentNullException(nameof(issuedQuantity));
            }
            
            SetIssuedQuantity(issuedQuantity);
        }
        
        public Sku Sku { get; }
        public Quantity Quantity { get; private set; }
        
        public IssuedQuantity IssuedQuantity { get; private set; }

        public MerchRequestItemStatus MerchRequestItemStatus
        {
            get
            {   
                if (Equals(Quantity.Value, IssuedQuantity.Value))
                {
                    return MerchRequestItemStatus.Done;
                }
                
                return MerchRequestItemStatus.New;
            }
        }

        public void UpdateRequiredQuantity(int newQuantityValue)
        {
            if (newQuantityValue < 0)
                throw new InvalidQuantityException($"{nameof(newQuantityValue)} value is negative");
            Quantity = new Quantity(newQuantityValue);
        }
        
        // добавление количества выданного товара
        public void IncreaseIssuedQuantity(int valueToIncrease)
        {
            if (valueToIncrease < 0)
                throw new InvalidQuantityException($"{nameof(valueToIncrease)} value is negative");
            
            SetIssuedQuantity(new IssuedQuantity(this.IssuedQuantity.Value + valueToIncrease));
        }

        private void SetIssuedQuantity(IssuedQuantity issuedQuantity)
        {
            if (issuedQuantity == null)
            {
                throw new MerchRequestItemArgumentNullException(nameof(issuedQuantity));
            }

            if (issuedQuantity.Value > Quantity.Value)
            {
                throw new InvalidMerchRequestItemIssuedQuantityException(
                    $"Issued quantity ({issuedQuantity.Value} more than MerchRequestItem quantity ({Quantity.Value})");
            }
            
            IssuedQuantity = issuedQuantity;
        }
    }
}