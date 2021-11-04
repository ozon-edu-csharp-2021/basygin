using System;

namespace Ozon.Route256.MerchandiseService.HttpModels
{
    public class RequestMerchModel
    {
        public long Id { get; set; }
        
        public long EmployeeId { get; set; }
        
        public MerchType MerchType { get; set; }
        
        public MerchRequestStatus Status { get; set; }
    }
}