namespace Ozon.Route256.MerchandiseService.Domain.BaseModels
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}