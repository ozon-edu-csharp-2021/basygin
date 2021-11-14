using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.DomainEvents
{
    public class MerchRequestStatusInWorkDomainEventHandler : INotificationHandler<MerchRequestStatusInWorkDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IStockApi _stockApi;

        public MerchRequestStatusInWorkDomainEventHandler(IMerchRequestRepository merchRequestRepository, IStockApi stockApi)
        {
            _merchRequestRepository = merchRequestRepository;
            _stockApi = stockApi;
        }
        
        public async Task Handle(MerchRequestStatusInWorkDomainEvent notification, CancellationToken cancellationToken)
        {
            var merchRequest = notification.MerchRequest;

            var itemsForIssue = merchRequest.Items.Where(x => Equals(x.MerchRequestItemStatus, MerchRequestItemStatus.New));

            foreach (var merchRequestItem in itemsForIssue)
            {
                await IssueMerchRequestItem(merchRequestItem, cancellationToken);
            }
            
            if (!merchRequest.Items.Exists(x => x.Quantity.Value > x.IssuedQuantity.Value))
            {
                merchRequest.SetStatusDone();
            }
            else
            {
                merchRequest.SetStatusWait();
            }
            
            await _merchRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task IssueMerchRequestItem(MerchRequestItem merchRequestItem, CancellationToken cancellationToken)
        {
            var possibleQuantity = await CalculatePossibleQuantityForIssuing(merchRequestItem, cancellationToken);

            if (possibleQuantity > 0)
            {
                await _stockApi.GiveOutItemAsync(new SkuItem()
                {
                    Sku = merchRequestItem.Sku.Value,
                    Quantity = possibleQuantity
                }, cancellationToken);
                
                merchRequestItem.IncreaseIssuedQuantity(possibleQuantity);
            }
        }

        private async Task<int> CalculatePossibleQuantityForIssuing(MerchRequestItem merchRequestItem, CancellationToken cancellationToken)
        {
            var quantityToGiveOut = merchRequestItem.Quantity.Value - merchRequestItem.IssuedQuantity.Value;
            
            var availableQuantity =
                await _stockApi.GetAvailableQuantityAsync(merchRequestItem.Sku.Value, cancellationToken);

            if (availableQuantity == 0)
            {
                return 0;
            }
            
            if (availableQuantity < quantityToGiveOut)
            {
                quantityToGiveOut = availableQuantity;
            }

            return quantityToGiveOut;
        }
    }
}