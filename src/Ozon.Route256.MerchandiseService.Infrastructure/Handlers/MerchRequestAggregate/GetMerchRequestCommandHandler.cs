using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;
using Ozon.Route256.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class GetMerchRequestCommandHandler : IRequestHandler<GetMerchRequestQuery, MerchRequest>
    {
        private readonly IMerchPackItemRepository _merchItemRepository;
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IMerchRequestItemRepository _merchRequestItemRepository;
        private readonly IStockApi _stockApi;

        public GetMerchRequestCommandHandler(IStockApi stockApi, IMerchRequestRepository merchRequestRepository,
            IMerchPackItemRepository merchItemRepository, IMerchRequestItemRepository merchRequestItemRepository)
        {
            _stockApi = stockApi;
            _merchRequestRepository = merchRequestRepository;
            _merchItemRepository = merchItemRepository;
            _merchRequestItemRepository = merchRequestItemRepository;
        }

        public async Task<MerchRequest> Handle(GetMerchRequestQuery request, CancellationToken cancellationToken)
        {
            var merchRequest =
                await _merchRequestRepository.GetMerchRequestByIdAsync(new Identifier(request.MerchRequestId), cancellationToken);

            if (merchRequest is null)
            {
                return null;
            }

            // добавить логику на проверку не расширился ли мерчпак
            var merchPackItems = await _merchItemRepository.CollectItemsByMerchRequestTypeAndSizeAsync(
                merchRequest.Type,
                merchRequest.Employee.Size, cancellationToken);

            foreach (var item in merchPackItems)
            {
                var merchRequestItem = merchRequest.Items.FirstOrDefault(x => x.Sku.Equals(item.Sku));

                int quantityToGiveOut = 0;

                // вычисляем количество которое надо заказать
                if (merchRequestItem is null)
                {
                    quantityToGiveOut = item.Quantity.Value;
                }
                else
                {
                    quantityToGiveOut = item.Quantity.Value - merchRequestItem.IssuedQuantity.Value;
                }

                // если надо заказать - заказываем
                if (quantityToGiveOut > 0)
                {
                    var availableQuantity =
                        await _stockApi.GetAvailableQuantityAsync(item.Sku.Value, cancellationToken);

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
                            Sku = item.Sku.Value,
                            Quantity = quantityToGiveOut
                        }, cancellationToken);
                    }
                    
                    if (merchRequestItem is null)
                    {
                        // если это новая позиция в паке - то создаем новый MerchRequestItem
                        merchRequestItem = new MerchRequestItem(new Identifier(merchRequest.Id),
                            new Sku(item.Sku.Value), new Quantity(item.Quantity.Value), new IssuedQuantity(quantityToGiveOut));

                        merchRequest.AddItem(merchRequestItem);

                        await _merchRequestItemRepository.CreateMerchRequestItemAsync(merchRequestItem, cancellationToken);
                    }
                    else
                    {
                        // если не новая позиция - обновляем количества по существующей
                        merchRequestItem.UpdateRequiredQuantity(item.Quantity.Value);
                        merchRequestItem.IncreaseIssuedQuantity(quantityToGiveOut);
                    
                        await _merchRequestItemRepository.UpdateMerchRequestItemAsync(merchRequestItem, cancellationToken);
                    }
                }
            }
            
            return merchRequest;
        }
    }
}