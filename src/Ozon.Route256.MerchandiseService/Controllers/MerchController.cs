using System;
using System.Collections.Generic;
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
        public Task<IActionResult> CreateMerchRequest(MerchRequestCreateModel createModel, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public Task<ActionResult<List<MerchRequestModel>>> GetMerchRequestByEmployeeId([FromQuery] long EmployeeId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}