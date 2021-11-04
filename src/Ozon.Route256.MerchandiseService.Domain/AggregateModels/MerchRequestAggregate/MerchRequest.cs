using System;
using System.Collections.Generic;
using System.Linq;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate
{
    /// <summary>
    /// Запрос на выдачу мерча
    /// </summary>
    public class MerchRequest : Entity
    {
        public MerchRequest(MerchRequestType type, Employee employee, DateTime createdAt)
        {
            Type = type ?? throw new MerchRequestItemArgumentNullException(nameof(type));
            Employee = employee ?? throw new MerchRequestItemArgumentNullException(nameof(employee));
            CreatedAt = createdAt;
            Items = new List<MerchRequestItem>();
            
            AddDomainEvent(new MerchRequestCreatedDomainEvent(this));
        }
        
        public MerchRequestType Type { get; }
        public Employee Employee { get; }

        public MerchRequestStatus Status
        {
            get
            {
                if (!Items.Any())
                {
                    return MerchRequestStatus.New;
                }

                if (Items.Exists(x => Equals(x.MerchRequestItemStatus, MerchRequestItemStatus.New)))
                {
                    return MerchRequestStatus.Wait;
                }
                
                return MerchRequestStatus.Done;
            }
        }

        public DateTime CreatedAt { get; }
        public DateTime? IssuedAt { get; internal set; }
        public List<MerchRequestItem> Items { get; internal set; }

        public void SetIssued(DateTime issuedDate)
        {
            if (CreatedAt > issuedDate)
            {
                throw new MerchRequestIssuedDateException("Issued date is less then created date");
            }

            IssuedAt = issuedDate;
        }

        public void AddItem(MerchRequestItem merchRequestItem)
        {
            if (merchRequestItem == null) throw new MerchRequestItemArgumentNullException(nameof(merchRequestItem));
            
            if (Items.Exists(x => x.Sku == merchRequestItem.Sku))
            {
                throw new MerchRequestItemAlreadyExistException(
                    $"Item with sku = {merchRequestItem.Sku} already exist");
            }
            
            Items.Add(merchRequestItem);
            
            AddDomainEvent(new MerchRequestItemAddedDomainEvent(merchRequestItem));

            if (Equals(Status, MerchRequestStatus.Done))
            {
                AddDomainEvent(new MerchRequestStatusDoneDomainEvent(this));
            }
        }
    }
}