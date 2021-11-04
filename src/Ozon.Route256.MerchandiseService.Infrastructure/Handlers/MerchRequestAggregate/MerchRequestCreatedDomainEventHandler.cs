using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Exceptions;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class MerchRequestCreatedDomainEventHandler : INotificationHandler<MerchRequestCreatedDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;

        public MerchRequestCreatedDomainEventHandler(IMerchRequestRepository merchRequestRepository)
        {
            _merchRequestRepository = merchRequestRepository;
        }
        
        public async Task Handle(MerchRequestCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var merchRequest = notification.MerchRequest;
            
            // проверяем выдавался ли ранее данный мерчпак сотруднику
            var exitstingMerchRequest =
                await _merchRequestRepository.GetMerchRequestByEmployeeIdAndMerchTypeAsync(merchRequest.Employee.Id, merchRequest.Type,
                    cancellationToken);

            if (exitstingMerchRequest is not null)
            {
                throw new MerchRequestAlreadyCreatedException(
                    $"Merch request is with type {exitstingMerchRequest.Type} for employee with id {exitstingMerchRequest.Employee.Id} already created");
            }
            
            await _merchRequestRepository.CreateMerchRequestAsync(merchRequest, cancellationToken);
        }
    }
}