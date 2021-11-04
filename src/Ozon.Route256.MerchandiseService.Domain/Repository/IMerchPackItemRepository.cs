using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;

namespace Ozon.Route256.MerchandiseService.Domain.Repository
{
    public interface IMerchPackItemRepository
    {
        Task<List<MerchPackItem>> CollectItemsByMerchRequestTypeAndSizeAsync(MerchRequestType merchType, Size size, CancellationToken token);
    }
}