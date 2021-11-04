using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class MerchRequestStatusDoneDomainEventHandler : INotificationHandler<MerchRequestStatusDoneDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;

        public MerchRequestStatusDoneDomainEventHandler(IMerchRequestRepository merchRequestRepository)
        {
            _merchRequestRepository = merchRequestRepository;
        }
        
        public Task Handle(MerchRequestStatusDoneDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}