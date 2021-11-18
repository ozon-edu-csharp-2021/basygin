using System;
using System.Collections.Generic;
using System.Linq;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate
{
    /// <summary>
    /// Запрос на выдачу мерча
    /// </summary>
    public class MerchRequest : Entity, IAggregateRoot
    {
        public MerchRequest(MerchRequestType type, Employee employee, DateTime createdAt, DateTime? issuedAt = null)
        {
            Type = type;
            Employee = employee ?? throw new MerchRequestItemArgumentNullException(nameof(employee));
            CreatedAt = createdAt;

            if (issuedAt.HasValue)
            {
                SetIssuedDate(issuedAt.Value);
            }
            
            Items = new List<MerchRequestItem>();
            Status = MerchRequestStatus.New;
        }
        
        public MerchRequestType Type { get; }
        public Employee Employee { get; }

        public MerchRequestStatus Status { get; private set; }

        public DateTime CreatedAt { get; }
        public DateTime? IssuedAt { get; internal set; }
        public List<MerchRequestItem> Items { get; private set; }

        public void SetIssuedDate(DateTime issuedDate)
        {
            if (CreatedAt > issuedDate)
            {
                throw new MerchRequestIssuedDateException("Issued date is less then created date");
            }

            if (!Equals(Status, MerchRequestStatus.Done))
            {
                throw new MerchRequestWrongStatusException($"Unable to set issued date for merch request in status {Status.Name}");
            }
            
            IssuedAt = issuedDate;
        }

        public void AddItem(MerchRequestItem merchRequestItem)
        {
            if (!Equals(Status, MerchRequestStatus.InWork) && Equals(Status, MerchRequestStatus.New))
            {
                throw new MerchRequestWrongStatusException(
                    $"Unable to add new item for merch request in status {Status.Name}");
            }
            
            if (merchRequestItem == null) throw new MerchRequestItemArgumentNullException(nameof(merchRequestItem));
            
            if (Items.Exists(x => x.Sku == merchRequestItem.Sku))
            {
                throw new MerchRequestItemAlreadyExistException(
                    $"Item with sku = {merchRequestItem.Sku} already exist");
            }
            
            Items.Add(merchRequestItem);
        }

        public void SetStatusInWork()
        {
            if (!Status.Equals(MerchRequestStatus.New))
            {
                ThrowMerchRequestStatusException(MerchRequestStatus.InWork);
            }
            
            Status = MerchRequestStatus.InWork;
            
            AddDomainEvent(new MerchRequestStatusInWorkDomainEvent(this));
        }
        
        public void SetStatusWait()
        {
            if (!Status.Equals(MerchRequestStatus.InWork))
            {
                ThrowMerchRequestStatusException(MerchRequestStatus.Wait);
            }

            if (!Items.Exists(x => Equals(x.MerchRequestItemStatus, MerchRequestItemStatus.New)))
            {
                ThrowMerchRequestStatusException(MerchRequestStatus.Wait);
            }
            
            Status = MerchRequestStatus.Wait;
            
            AddDomainEvent(new MerchRequestStatusWaitDomainEvent(this));
        }
        
        public void SetStatusDone()
        {
            if (!Status.Equals(MerchRequestStatus.InWork))
            {
                ThrowMerchRequestStatusException(MerchRequestStatus.Done);
            }
            
            if (Items.Exists(x => Equals(x.MerchRequestItemStatus, MerchRequestItemStatus.New)))
            {
                ThrowMerchRequestStatusException(MerchRequestStatus.Done);
            }

            Status = MerchRequestStatus.Done;
            
            AddDomainEvent(new MerchRequestStatusDoneDomainEvent(this));
        }

        private void ThrowMerchRequestStatusException(MerchRequestStatus statusToChange)
        {
            throw new MerchRequestWrongStatusException($"Is not possible to change the merch request status from {Status.Name} to {statusToChange.Name}.");
        }

        public void SetId(long id)
        {
            Id = (int)id;
        }

        public void SetMerchRequestItems(List<MerchRequestItem> merchRequestItems)
        {
            Items = merchRequestItems;
        }
    }
}