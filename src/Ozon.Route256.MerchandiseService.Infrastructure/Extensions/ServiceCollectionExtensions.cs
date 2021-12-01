using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Integrations.StockApi;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Implementation;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Extensions
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных сервисов
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            // тут будут добавляться репозитории
            
            services.AddScoped<IMerchRequestRepository, MerchRequestRepository>();
            services.AddScoped<IMerchPackItemRepository, MerchPackItemsRepository>();
            
            return services;
        }

        /// <summary>
        /// Добавление в DI контейнер внешних сервисов
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureIntegrations(this IServiceCollection services)
        {
            services.AddScoped<IStockApi, StockApi>();

            return services;
        }
    }
}