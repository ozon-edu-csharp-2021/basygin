using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Commands
{
    public class MerchPackCheckOnExtendedCommand : IRequest<bool>
    {
        public MerchPackCheckOnExtendedCommand(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }

        public MerchRequest MerchRequest { get; private set; }
    }
}