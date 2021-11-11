using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class MerchRequestStatusWaitDomainEventHandler : INotificationHandler<MerchRequestStatusWaitDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;

        public MerchRequestStatusWaitDomainEventHandler(IMerchRequestRepository merchRequestRepository)
        {
            _merchRequestRepository = merchRequestRepository;
        }
        
        public Task Handle(MerchRequestStatusWaitDomainEvent notification, CancellationToken cancellationToken)
        {
            // необходимо описать что делать когда реквест переходит в статус Wait
            
            return Task.CompletedTask;
        }
    }
}