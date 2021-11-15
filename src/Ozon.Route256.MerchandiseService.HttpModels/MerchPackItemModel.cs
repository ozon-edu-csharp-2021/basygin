namespace Ozon.Route256.MerchandiseService.HttpModels
{
    public class MerchPackItemModel
    {
        public MerchType MerchType { get; set; }
        
        public Size Size { get; set; }
        
        public long Sku { get; set; }
        
        public int Quantity { get; set; }
    }
}