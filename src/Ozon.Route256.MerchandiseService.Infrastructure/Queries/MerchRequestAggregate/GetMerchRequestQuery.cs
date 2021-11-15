using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate
{
    /// <summary>
    /// Получить информацию о запросе на мерч
    /// </summary>
    public class GetMerchRequestQuery : IRequest<MerchRequest>
    {
        /// <summary>
        /// Идентификатор товарной позиции
        /// </summary>
        public long MerchRequestId { get; set; }
    }
}