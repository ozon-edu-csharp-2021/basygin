using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Domain.BaseModels
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask StartTransaction(CancellationToken token);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}