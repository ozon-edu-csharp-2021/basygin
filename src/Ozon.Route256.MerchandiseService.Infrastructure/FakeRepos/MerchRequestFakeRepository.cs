﻿using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.FakeRepos
{
    public class MerchRequestFakeRepository : IMerchRequestRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        
        public Task<MerchRequest> GetMerchRequestByIdAsync(long id, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchRequest> GetMerchRequestByEmployeeIdAndMerchTypeAsync(long employeeId, MerchRequestType merchType,
            CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchRequest> GetMerchRequestAwaitsSupplyBySkuOrderByCreatedAsync(Sku sku, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchRequest> CreateMerchRequestAsync(MerchRequest merchRequest, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(MerchRequest merchRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}