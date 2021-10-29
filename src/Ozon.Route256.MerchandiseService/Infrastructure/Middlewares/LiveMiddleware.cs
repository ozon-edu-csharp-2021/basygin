using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Middlewares
{
    public class LiveMiddleware
    {
        public LiveMiddleware(RequestDelegate next)
        {
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
        }
    }
}