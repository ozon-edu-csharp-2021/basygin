using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.Events
{
    public class MerchRequestStatusInWorkDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }
        
        public MerchRequestStatusInWorkDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }
    }
}