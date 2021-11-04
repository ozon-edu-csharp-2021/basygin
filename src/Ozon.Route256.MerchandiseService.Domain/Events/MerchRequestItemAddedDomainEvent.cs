using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.Events
{
    public class MerchRequestItemAddedDomainEvent : INotification
    {
        public MerchRequestItem MerchRequestItem { get; }
        
        public MerchRequestItemAddedDomainEvent(MerchRequestItem merchRequestItem)
        {
            MerchRequestItem = merchRequestItem;
        }
    }
}