using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Stubs
{
    public class MerchPackItemFakeRepository : IMerchPackItemRepository
    {
        public Task<List<MerchPackItem>> CollectItemsByMerchRequestTypeAndSizeAsync(MerchRequestType merchType, Size size, CancellationToken token)
        {
            var items = new List<MerchPackItem>
            {
                new MerchPackItem(new Sku(1234), new Quantity(2)),
                new MerchPackItem(new Sku(5678), new Quantity(1)),
                new MerchPackItem(new Sku(9531), new Quantity(3))
            };

            return Task.FromResult(items);
        }
    }
}