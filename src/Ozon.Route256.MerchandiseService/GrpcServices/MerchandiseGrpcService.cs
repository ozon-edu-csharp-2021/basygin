using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Ozon.Route256.MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseService.MerchandiseServiceBase
    {
        public override Task<Empty> RequestMerch(RequestMerchRequest request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }

        public override Task<GetRequestMerchByIdResponse> GetRequestMerchById(GetRequestMerchByIdRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetRequestMerchByIdResponse()
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId = 123,
                MerchType = MerchType.VeteranPack
            });
        }
    }
}