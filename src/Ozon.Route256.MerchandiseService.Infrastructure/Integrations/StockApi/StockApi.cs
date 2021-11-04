using System.Threading;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi
{
    public class StockApi : IStockApi
    {
        public async Task<bool> GiveOutItemAsync(SkuItem item, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }

        public async Task<int> GetAvailableQuantityAsync(long skuId, CancellationToken cancellationToken)
        {
            return await Task.FromResult(4);
        }
    }
}