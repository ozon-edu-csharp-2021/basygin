using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.DomainEvents
{
    public class MerchRequestStatusDoneDomainEventHandler : INotificationHandler<MerchRequestStatusDoneDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;

        public MerchRequestStatusDoneDomainEventHandler(IMerchRequestRepository merchRequestRepository)
        {
            _merchRequestRepository = merchRequestRepository;
        }
        
        public async Task Handle(MerchRequestStatusDoneDomainEvent notification, CancellationToken cancellationToken)
        {
            // необходимо описать логику уведомления сотрудника о том что он может забрать свой мерчпак

            var merchRequest = notification.MerchRequest;
            
            merchRequest.SetIssuedDate(DateTime.Now);

            await _merchRequestRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}