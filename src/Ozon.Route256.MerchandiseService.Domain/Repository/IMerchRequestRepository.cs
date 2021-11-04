using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;

namespace Ozon.Route256.MerchandiseService.Domain.Repository
{
    public interface IMerchRequestRepository
    {
        Task<long> CreateMerchRequestAsync(MerchRequest request, CancellationToken token);
        
        Task AddMerchRequestItemsAsync(List<MerchRequestItem> request, CancellationToken token);
        
        Task<MerchRequest> GetMerchRequestByIdAsync(Identifier id, CancellationToken token);
        
        Task<MerchRequest> GetMerchRequestByEmployeeIdAndMerchTypeAsync(Identifier employeeId, MerchRequestType merchType, CancellationToken token);
    }
}