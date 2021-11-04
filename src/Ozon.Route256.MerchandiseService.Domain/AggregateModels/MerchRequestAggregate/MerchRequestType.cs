using Ozon.Route256.MerchandiseService.Domain.BaseModels;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate
{
    public class MerchRequestType : Enumeration
    {
        public static MerchRequestType WelcomePack = new MerchRequestType(10, nameof(WelcomePack));
        public static MerchRequestType ConferenceListenerPack = new MerchRequestType(20, nameof(ConferenceListenerPack));
        public static MerchRequestType ConferenceSpeakerPack = new MerchRequestType(30, nameof(ConferenceSpeakerPack));
        public static MerchRequestType ProbationPeriodEndingPack = new MerchRequestType(40, nameof(ProbationPeriodEndingPack));
        public static MerchRequestType VeteranPack = new MerchRequestType(50, nameof(VeteranPack));
        
        
        public MerchRequestType(int id, string name) : base(id, name)
        {
        }
    }
}