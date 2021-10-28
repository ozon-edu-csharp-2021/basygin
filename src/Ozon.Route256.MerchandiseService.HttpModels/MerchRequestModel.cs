using System;

namespace Ozon.Route256.MerchandiseService.HttpModels
{
    public class MerchRequestModel
    {
        public Guid Id { get; set; }
        
        public long EmployeeId { get; set; }
        
        public MerchType MerchType { get; set; }
    }
}