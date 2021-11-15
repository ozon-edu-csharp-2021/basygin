using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.FakeRepos
{
    public class MerchPackItemsFakeRepository : IMerchPackItemRepository
    {
        public Task<List<MerchPackItem>> CollectItemsByMerchRequestTypeAndSizeAsync(MerchRequestType merchType, Size size, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}