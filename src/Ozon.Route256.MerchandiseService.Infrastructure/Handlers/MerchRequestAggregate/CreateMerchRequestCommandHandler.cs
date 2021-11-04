using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;
using Ozon.Route256.MerchandiseService.Infrastructure.Exceptions;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class CreateMerchRequestCommandHandler : IRequestHandler<CreateMerchRequestCommand, long>
    {
        private readonly IMerchPackItemRepository _merchItemRepository;
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IStockApi _stockApi;

        public CreateMerchRequestCommandHandler(IMerchPackItemRepository merchItemRepository,
            IMerchRequestRepository merchRequestRepository, IStockApi stockApi)
        {
            _merchItemRepository = merchItemRepository;
            _merchRequestRepository = merchRequestRepository;
            _stockApi = stockApi;
        }

        public async Task<long> Handle(CreateMerchRequestCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
                new Identifier(request.EmployeeId),
                Enumeration.GetAll<Size>().FirstOrDefault(it => it.Id.Equals(request.Size)),
                new Email(request.Email));

            var requestType = Enumeration.GetAll<MerchRequestType>()
                .FirstOrDefault(it => it.Id.Equals(request.MerchType));

            // проверяем выдавался ли ранее данный мерчпак сотруднику
            var exitstingMerchRequest =
                await _merchRequestRepository.GetMerchRequestByEmployeeIdAndMerchTypeAsync(employee.Id, requestType,
                    cancellationToken);

            if (exitstingMerchRequest is not null)
            {
                throw new MerchRequestAlreadyCreatedException(
                    $"Merch request is with type {exitstingMerchRequest.Type} for employee with id {exitstingMerchRequest.Employee.Id} already created");
            }

            // собираем набор мерча
            var merchPackItems =
                await _merchItemRepository.CollectItemsByMerchRequestTypeAndSizeAsync(requestType, employee.Size,
                    cancellationToken);

            var merchRequest = new MerchRequest(requestType, employee, DateTime.Now);

            var merchRequestId = await _merchRequestRepository.CreateMerchRequestAsync(merchRequest, cancellationToken);

            foreach (var item in merchPackItems)
            {
                var availableQuantity = await _stockApi.GetAvailableQuantityAsync(item.Sku.Value, cancellationToken);

                int quantityToGiveOut = 0;

                if (availableQuantity > 0)
                {
                    quantityToGiveOut = availableQuantity >= item.Quantity.Value
                        ? item.Quantity.Value
                        : availableQuantity;

                    await _stockApi.GiveOutItemAsync(new SkuItem()
                    {
                        Sku = item.Sku.Value,
                        Quantity = quantityToGiveOut
                    }, cancellationToken);
                }

                merchRequest.AddItem(new MerchRequestItem(new Identifier(merchRequest.Id), new Sku(item.Sku.Value),
                    new Quantity(item.Quantity.Value), new IssuedQuantity(quantityToGiveOut)));
            }

            await _merchRequestRepository.AddMerchRequestItemsAsync(merchRequest.Items, cancellationToken);

            return merchRequest.Id;
        }
    }
}