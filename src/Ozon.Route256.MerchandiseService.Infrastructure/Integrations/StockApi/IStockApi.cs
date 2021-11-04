using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi
{
    /// <summary>
    /// Временная интеграция, пока не готов сервис stock-api
    /// </summary>
    public interface IStockApi
    {
        Task<bool> GiveOutItemAsync(SkuItem item, CancellationToken cancellationToken);
        
        Task<int> GetAvailableQuantityAsync(long skuId, CancellationToken cancellationToken);
    }
}