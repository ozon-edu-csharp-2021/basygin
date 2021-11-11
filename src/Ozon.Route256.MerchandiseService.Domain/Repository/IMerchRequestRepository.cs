﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;

namespace Ozon.Route256.MerchandiseService.Domain.Repository
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<MerchRequest> GetMerchRequestByIdAsync(Identifier id, CancellationToken token);
        
        Task<MerchRequest> GetMerchRequestByEmployeeIdAndMerchTypeAsync(Identifier employeeId, MerchRequestType merchType, CancellationToken token);
        
        Task<MerchRequest> GetMerchRequestAwaitsSupplyBySkuOrderByCreatedAsync(Sku sku, CancellationToken cancellationToken);
    }
}