using Ozon.Route256.MerchandiseService.Domain.BaseModels;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate
{
    public class MerchRequestItemStatus : Enumeration
    {
        public static MerchRequestItemStatus New = new MerchRequestItemStatus(1, nameof(New));
        public static MerchRequestItemStatus Done = new MerchRequestItemStatus(2, nameof(Done));
        
        public MerchRequestItemStatus(int id, string name) : base(id, name)
        {
        }
    }
}