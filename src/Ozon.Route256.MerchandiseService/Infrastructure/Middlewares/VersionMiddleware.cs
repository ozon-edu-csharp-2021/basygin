using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var versionModel = new
            {
                Version = Assembly.GetEntryAssembly().GetName().Version?.ToString() ?? "no version",
                ServiceName = Assembly.GetEntryAssembly().GetName().Name
            };
            
            // сделать глобальной настройкой для всего сервиса
            var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var versionModelJson = JsonSerializer.Serialize(versionModel, serializerOptions);
            
            await context.Response.WriteAsync(versionModelJson);
        }
    }
}