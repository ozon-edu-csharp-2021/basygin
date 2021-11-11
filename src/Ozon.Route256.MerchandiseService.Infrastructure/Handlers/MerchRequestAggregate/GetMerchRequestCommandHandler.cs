using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Commands;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;
using Ozon.Route256.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class GetMerchRequestCommandHandler : IRequestHandler<GetMerchRequestQuery, MerchRequest>
    {
        private readonly IMerchPackItemRepository _merchItemRepository;
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IStockApi _stockApi;
        private readonly IMediator _mediator;

        public GetMerchRequestCommandHandler(IStockApi stockApi, IMerchRequestRepository merchRequestRepository,
            IMerchPackItemRepository merchItemRepository, IMediator mediator)
        {
            _stockApi = stockApi;
            _merchRequestRepository = merchRequestRepository;
            _merchItemRepository = merchItemRepository;
            _mediator = mediator;
        }

        public async Task<MerchRequest> Handle(GetMerchRequestQuery request, CancellationToken cancellationToken)
        {
            var merchRequest =
                await _merchRequestRepository.GetMerchRequestByIdAsync(new Identifier(request.MerchRequestId), cancellationToken);

            if (merchRequest is null)
            {
                return null;
            }

            var merchPackExtended = await _mediator.Send(new MerchPackCheckOnExtendedCommand(merchRequest), cancellationToken);

            if (merchPackExtended)
            {
                merchRequest.SetStatusInWork();
            }

            return merchRequest;
        }
    }
}