using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Stubs
{
    public class MerchRequestItemFakeRepository : IMerchRequestItemRepository
    {
        public Task CreateMerchRequestItemAsync(MerchRequestItem merchRequestItem, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateMerchRequestItemAsync(MerchRequestItem merchRequestItem, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchRequestItem>> GetItemsAwaitsSupplyBySkuAsync(Sku sku, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}