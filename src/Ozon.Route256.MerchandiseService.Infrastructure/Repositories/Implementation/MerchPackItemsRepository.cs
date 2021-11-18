using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Models;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchPackItemsRepository : IMerchPackItemRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchPackItemsRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<IEnumerable<MerchPackItem>> CollectItemsByMerchRequestTypeAndSizeAsync(MerchRequestType merchType, Size size, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT sku, quantity
                FROM merch_pack_items
                WHERE merch_request_type = @MerchRequestType and (size = @Size or size is null);";

            var parameters = new
            {
                MerchRequestType = merchType.Id,
                Size = size.Id
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var merchPackItems = await connection.QueryAsync<MerchPackItemDb>(commandDefinition);

            // Добавление после успешно выполненной операции.
            var result = merchPackItems.Select(x => new MerchPackItem(new Sku(x.Sku), new Quantity(x.Quantity))).ToList();
            //_changeTracker.Track(result);
            return result;
        }
    }
}