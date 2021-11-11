using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Commands;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;
using Ozon.Route256.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class MerchPackCheckOnExtendedCommandHandler : IRequestHandler<MerchPackCheckOnExtendedCommand, bool>
    {
        private readonly IMerchPackItemRepository _merchItemRepository;

        public MerchPackCheckOnExtendedCommandHandler(IStockApi stockApi, IMerchRequestRepository merchRequestRepository,
            IMerchPackItemRepository merchItemRepository)
        {
            _merchItemRepository = merchItemRepository;
        }

        public async Task<bool> Handle(MerchPackCheckOnExtendedCommand request, CancellationToken cancellationToken)
        {
            var merchRequest = request.MerchRequest;
            
            var merchPackItems =
                await _merchItemRepository.CollectItemsByMerchRequestTypeAndSizeAsync(merchRequest.Type, merchRequest.Employee.Size,
                    cancellationToken);
            var result = false;
            foreach (var item in merchPackItems)
            {
                var merchRequestItem = merchRequest.Items.FirstOrDefault(x => x.Sku.Equals(item.Sku));
                
                if (merchRequestItem is null)
                {
                    merchRequestItem = new MerchRequestItem(
                        new Sku(item.Sku.Value), new Quantity(item.Quantity.Value));

                    merchRequest.AddItem(merchRequestItem);
                    
                    result = true;
                }
                else if (merchRequestItem.Quantity.Value < item.Quantity.Value)
                {
                    merchRequestItem.UpdateRequiredQuantity(item.Quantity.Value);
                    result = true;
                }
            }
            
            return result;
        }
    }
}