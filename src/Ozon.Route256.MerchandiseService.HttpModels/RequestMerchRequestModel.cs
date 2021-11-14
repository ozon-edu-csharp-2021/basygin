namespace Ozon.Route256.MerchandiseService.HttpModels
{
    public class RequestMerchRequestModel
    {
        public int EmployeeId { get; set; }
        
        public string Email { get; set; }
        
        public Size Size { get; set; }
        
        public MerchType MerchType { get; set; }
    }
}