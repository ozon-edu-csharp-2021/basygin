using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.HttpModels;
using Ozon.Route256.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;
using Ozon.Route256.MerchandiseService.Infrastructure.Exceptions;
using Ozon.Route256.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace Ozon.Route256.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MerchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> MerchRequest(RequestMerchRequestModel merchRequest, CancellationToken cancellationToken)
        {
            var command = new CreateMerchRequestCommand()
            {
                MerchType = (int) merchRequest.MerchType,
                Email = merchRequest.Email,
                Size = (int) merchRequest.Size,
                EmployeeId = merchRequest.EmployeeId
            };

            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok();
            }
            catch (MerchRequestAlreadyCreatedException _)
            {
                return Conflict();
            }
        }
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<RequestMerchModel>> GetRequestMerchById(long id, CancellationToken cancellationToken)
        {
            var query = new GetMerchRequestQuery()
            {
                MerchRequestId = id
            };
            
            var result = await _mediator.Send(query, cancellationToken);

            if (result is null)
            {
                return NotFound();
            }
            
            var response = new RequestMerchModel()
            {
                Id = result.Id,
                Status = (HttpModels.MerchRequestStatus) result.Status.Id,
                EmployeeId = result.Employee.Id.Value,
                MerchType = (HttpModels.MerchType) result.Type.Id
            };

            return Ok(response);
        }
    }
}