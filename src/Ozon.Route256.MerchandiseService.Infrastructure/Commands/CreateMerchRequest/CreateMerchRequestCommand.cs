using MediatR;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public class CreateMerchRequestCommand: IRequest<long>
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; set; }
        
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