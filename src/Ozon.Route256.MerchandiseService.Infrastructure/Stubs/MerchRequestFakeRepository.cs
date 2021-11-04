using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Stubs
{
    public class MerchRequestFakeRepository : IMerchRequestRepository
    {
        public Task<long> CreateMerchRequestAsync(MerchRequest request, CancellationToken token)
        {
            return Task.FromResult<long>(123);
        }

        public async Task AddMerchRequestItemsAsync(List<MerchRequestItem> request, CancellationToken token)
        {
            await Task.CompletedTask;
        }

        public async Task<MerchRequest> GetMerchRequestByIdAsync(Identifier id, CancellationToken token)
        {
            MerchRequest merchRequest = null;
            
            if (id.Value == 1000)
            {
                var items = new List<MerchRequestItem>
                {
                    new MerchRequestItem(id, new Sku(1234), new Quantity(2), new IssuedQuantity(2)),
                    new MerchRequestItem(id, new Sku(5678), new Quantity(1), new IssuedQuantity(1)),
                    new MerchRequestItem(id, new Sku(9531), new Quantity(3), new IssuedQuantity(3))
                };
            
                merchRequest = new MerchRequest(
                    MerchRequestType.WelcomePack,
                    new Employee(new Identifier(123), Size.M, new Email("email@email.com")), DateTime.Now);

                foreach (var item in items)
                {
                    merchRequest.AddItem(item);
                }
            }

            return await Task.FromResult(merchRequest);
        }

        public async Task<MerchRequest> GetMerchRequestByEmployeeIdAndMerchTypeAsync(Identifier employeeId,
            MerchRequestType merchType,
            CancellationToken token)
        {
            MerchRequest merchRequest = null;
            
            if (employeeId.Value == 1000)
            {
                var items = new List<MerchRequestItem>
                {
                    new MerchRequestItem(new Identifier(123), new Sku(1234), new Quantity(2), new IssuedQuantity(2)),
                    new MerchRequestItem(new Identifier(123),new Sku(5678), new Quantity(1), new IssuedQuantity(1)),
                    new MerchRequestItem(new Identifier(123),new Sku(9531), new Quantity(3), new IssuedQuantity(3))
                };
            
                merchRequest = new MerchRequest(
                    merchType,
                    new Employee(employeeId, Size.M, new Email("email@email.com")), DateTime.Now);
            
                foreach (var item in items)
                {
                    merchRequest.AddItem(item);
                }
            }
            
            return await Task.FromResult(merchRequest);
        }
    }
}