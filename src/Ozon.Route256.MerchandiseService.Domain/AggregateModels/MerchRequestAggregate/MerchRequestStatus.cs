using Ozon.Route256.MerchandiseService.Domain.BaseModels;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate
{
    public class MerchRequestStatus : Enumeration
    {
        public static MerchRequestStatus New = new MerchRequestStatus(1, nameof(New));
        public static MerchRequestStatus InWork = new MerchRequestStatus(2, nameof(InWork));
        public static MerchRequestStatus Wait = new MerchRequestStatus(3, nameof(Wait));
        public static MerchRequestStatus Done = new MerchRequestStatus(4, nameof(Done));
        
        public MerchRequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}