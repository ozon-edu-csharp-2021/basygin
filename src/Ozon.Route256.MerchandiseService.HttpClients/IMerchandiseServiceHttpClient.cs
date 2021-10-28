using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.HttpModels;

namespace Ozon.Route256.MerchandiseService.HttpClients
{
    public interface IMerchandiseServiceHttpClient
    {
        Task CreateMerchRequestAsync(MerchRequestCreateModel merchRequestCreateModel, CancellationToken token);
        
        Task<List<MerchRequestModel>> GetMerchRequestsByEmployeeIdAsync(long employeeId, CancellationToken token);
    }
}