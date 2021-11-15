using MediatR;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Commands
{
    public class CreateMerchRequestCommand: IRequest<MerchRequest>
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// Email сотрудника
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Размер одежды сотрудника
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// Тип набора
        /// </summary>
        public int MerchType { get; set; }
    }
}