using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ozon.Route256.MerchandiseService.HttpModels;

namespace Ozon.Route256.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> MerchRequest(RequestMerchRequestModel merchRequest, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("{id:Guid}")]
        public Task<ActionResult<RequestMerchModel>> GetRequestMerchById([FromQuery] Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}