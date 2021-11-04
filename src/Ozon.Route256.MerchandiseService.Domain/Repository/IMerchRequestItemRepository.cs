using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;

namespace Ozon.Route256.MerchandiseService.Domain.Repository
{
    public interface IMerchRequestItemRepository
    {
        Task CreateMerchRequestItemAsync(MerchRequestItem merchRequestItem, CancellationToken cancellationToken);
        
        Task UpdateMerchRequestItemAsync(MerchRequestItem merchRequestItem, CancellationToken cancellationToken);
        
        Task<List<MerchRequestItem>> GetItemsAwaitsSupplyBySkuAsync(Sku sku, CancellationToken cancellationToken);
    }
}