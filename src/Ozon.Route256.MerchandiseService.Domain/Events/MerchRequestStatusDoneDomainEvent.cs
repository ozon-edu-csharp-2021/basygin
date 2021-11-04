using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.Events
{
    public class MerchRequestStatusDoneDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }
        
        public MerchRequestStatusDoneDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }
    }
}