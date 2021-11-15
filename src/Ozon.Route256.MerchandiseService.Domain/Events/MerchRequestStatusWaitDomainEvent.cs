using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.Events
{
    public class MerchRequestStatusWaitDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }
        
        public MerchRequestStatusWaitDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }
    }
}