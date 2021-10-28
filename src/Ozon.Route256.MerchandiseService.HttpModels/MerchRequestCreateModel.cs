using System;

namespace Ozon.Route256.MerchandiseService.HttpModels
{
    public class MerchRequestCreateModel
    {
        public long EmployeeId { get; set; }
        
        public MerchType MerchType { get; set; }
    }
}