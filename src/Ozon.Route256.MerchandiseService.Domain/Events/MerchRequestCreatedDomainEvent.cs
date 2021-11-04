using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.Events
{
    public class MerchRequestCreatedDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }
        
        public MerchRequestCreatedDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }
    }
}