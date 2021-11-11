using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Events;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
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

            foreach (var merchRequestItem in merchRequest.Items)
            {
                var quantityToGiveOut = merchRequestItem.Quantity.Value - merchRequestItem.IssuedQuantity.Value;

                // если надо заказать - заказываем
                if (quantityToGiveOut > 0)
                {
                    var availableQuantity =
                        await _stockApi.GetAvailableQuantityAsync(merchRequestItem.Sku.Value, cancellationToken);

                    // на складе в остатках есть какое то количество данной позиции
                    if (availableQuantity > 0)
                    {
                        // заказываем максимально возможное количество данной позиции
                        if (availableQuantity < quantityToGiveOut)
                        {
                            quantityToGiveOut = availableQuantity;
                        }

                        await _stockApi.GiveOutItemAsync(new SkuItem()
                        {
                            Sku = merchRequestItem.Sku.Value,
                            Quantity = quantityToGiveOut
                        }, cancellationToken);
                    }
                    
                    // если не новая позиция - обновляем количества по существующей
                    merchRequestItem.IncreaseIssuedQuantity(quantityToGiveOut);
                }
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
    }
}