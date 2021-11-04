using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Commands.SupplyShip;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers
{
    public class SupplyShippedEventHandler : IRequestHandler<SupplyShipCommand>
    {
        private readonly IMerchRequestItemRepository _merchRequestItemRepository;
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IStockApi _stockApi;

        public SupplyShippedEventHandler(IMerchRequestItemRepository merchRequestItemRepository, IMerchRequestRepository merchRequestRepository, IStockApi stockApi)
        {
            _merchRequestItemRepository = merchRequestItemRepository;
            _merchRequestRepository = merchRequestRepository;
            _stockApi = stockApi;
        }

        public async Task<Unit> Handle(SupplyShipCommand request, CancellationToken cancellationToken)
        {
            // merchRequestList - список реквестов по которым необходимо будет отправить уведомления для того чтоб сотрудник забрал мерч
            var merchRequestList = new List<MerchRequest>();
            
            foreach (var supplyItem in request.Items)
            {
                var awaitsSupplyItems =
                    await _merchRequestItemRepository.GetItemsAwaitsSupplyBySkuAsync(new Sku(supplyItem.SkuId),
                        cancellationToken);

                if (!awaitsSupplyItems.Any())
                {
                    continue;
                }

                foreach (var awaitItem in awaitsSupplyItems)
                {
                    var quantityToGiveOut = awaitItem.Quantity.Value - awaitItem.IssuedQuantity.Value;
                    
                    var availableQuantity =
                        await _stockApi.GetAvailableQuantityAsync(awaitItem.Sku.Value, cancellationToken);

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
                            Sku = awaitItem.Sku.Value,
                            Quantity = quantityToGiveOut
                        }, cancellationToken);
    
                        awaitItem.IncreaseIssuedQuantity(quantityToGiveOut);
                        
                        await _merchRequestItemRepository.UpdateMerchRequestItemAsync(awaitItem, cancellationToken);
                        
                        // пополняем список мерч реквестов для нотификации
                        if (!merchRequestList.Exists(x => x.Id == awaitItem.MerchRequestId.Value))
                        {
                            var merchRequest =
                                await _merchRequestRepository.GetMerchRequestByIdAsync(awaitItem.MerchRequestId,
                                    cancellationToken);
                            
                            merchRequestList.Add(merchRequest);
                        }
                    }
                }
            }
            
            return Unit.Value;
        }
    }
}