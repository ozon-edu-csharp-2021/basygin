using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.Route256.MerchandiseService.Domain.Events;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Handlers
{
    public class SupplyShippedEventHandler : INotificationHandler<SupplyShippedEvent>
    {
        public Task Handle(SupplyShippedEvent notification, CancellationToken cancellationToken)
        {
            // необходимо описать логику получения поставки
            
            return Task.CompletedTask;
        }
    }
}