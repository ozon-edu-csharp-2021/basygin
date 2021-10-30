using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Ozon.Route256.MerchandiseService.Infrastructure.Middlewares;

namespace Ozon.Route256.MerchandiseService.Infrastructure.StartupFilters
{
    public class InfrastructureStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                app.Map("/ready", builder => builder.UseMiddleware<ReadyMiddleware>());
                app.Map("/live", builder => builder.UseMiddleware<LiveMiddleware>());

                app.UseWhen(context => context.Request.ContentType != "application/grpc", builder =>
                {
                    builder.UseMiddleware<LoggingMiddleware>();
                });
                
                next(app);
            };
        }
    }
}