using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Ozon.Route256.MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseService.MerchandiseServiceBase
    {
        public override Task<Empty> CreateMerchRequest(MerchRequestCreateModel request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }

        public override Task<GetMerchRequestByEmployeeIdResponse> GetMerchRequestByEmployeeId(GetMerchRequestByEmployeeIdRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetMerchRequestByEmployeeIdResponse()
            {
                MerchRequests = {  }
            });
        }
    }
}