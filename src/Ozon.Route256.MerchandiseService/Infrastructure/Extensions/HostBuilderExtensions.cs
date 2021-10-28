using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ozon.Route256.MerchandiseService.Infrastructure.Filters;
using Ozon.Route256.MerchandiseService.Infrastructure.Interceptors;
using Ozon.Route256.MerchandiseService.Infrastructure.StartupFilters;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, InfrastructureStartupFilter>();

                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());;
                
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Ozon.Route256.MerchandiseService", Version = "v1"});
                
                    options.CustomSchemaIds(x => x.FullName);
                
                    var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                    options.IncludeXmlComments(xmlFilePath);
                });
            });
            return builder;
        }
    }
}