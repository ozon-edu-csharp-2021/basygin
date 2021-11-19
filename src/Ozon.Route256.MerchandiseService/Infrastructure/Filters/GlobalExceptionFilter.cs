using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = new
            {
                ExceptionType = context.Exception.GetType().Name,
                Message = context.Exception.ToString()
            };
            var jsonResult = new JsonResult(exception)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ContentType = "application/json"
            };
            context.Result = jsonResult;
        }
    }
}