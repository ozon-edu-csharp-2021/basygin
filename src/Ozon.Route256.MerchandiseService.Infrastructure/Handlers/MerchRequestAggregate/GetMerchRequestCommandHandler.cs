using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Commands;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;
using Ozon.Route256.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class GetMerchRequestCommandHandler : IRequestHandler<GetMerchRequestQuery, MerchRequest>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public GetMerchRequestCommandHandler(IMerchRequestRepository merchRequestRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _merchRequestRepository = merchRequestRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<MerchRequest> Handle(GetMerchRequestQuery request, CancellationToken cancellationToken)
        {
            var merchRequest =
                await _merchRequestRepository.GetMerchRequestByIdAsync(request.MerchRequestId, cancellationToken);

            if (merchRequest is null)
            {
                return null;
            }

            var merchPackExtended = await _mediator.Send(new MerchPackCheckOnExtendedCommand(merchRequest), cancellationToken);

            if (merchPackExtended)
            {
                merchRequest.SetStatusInWork();
            }

            await _unitOfWork.StartTransaction(cancellationToken);

            await _merchRequestRepository.Update(merchRequest, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return merchRequest;
        }
    }
}