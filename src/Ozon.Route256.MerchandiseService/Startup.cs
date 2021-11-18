using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.GrpcServices;
using Ozon.Route256.MerchandiseService.Infrastructure.Configuration;
using Ozon.Route256.MerchandiseService.Infrastructure.Extensions;
using Ozon.Route256.MerchandiseService.Infrastructure.Interceptors;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Infrastructure;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;

namespace Ozon.Route256.MerchandiseService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());

            services.AddInfrastructureIntegrations();
            services.AddInfrastructureServices();
            AddDatabaseComponents(services);
            services.AddInfrastructureRepositories();
        }

        private void AddDatabaseComponents(IServiceCollection services)
        {
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchandiseGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}