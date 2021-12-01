using Microsoft.Extensions.Options;
using Npgsql;
using Ozon.Route256.MerchandiseService.Infrastructure.Configuration;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Infrastructure
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory<NpgsqlConnection>
    {
        private readonly DatabaseConnectionOptions _options;
        private  NpgsqlConnection _connection;
        public NpgsqlConnectionFactory(IOptions<DatabaseConnectionOptions> options)
        {
            _options = options.Value;
        }


        public async Task<NpgsqlConnection> CreateConnection(CancellationToken token)
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new NpgsqlConnection(_options.ConnectionString);
            await _connection.OpenAsync(token);
            _connection.StateChange += (o, e) =>
            {
                if (e.CurrentState == ConnectionState.Closed)
                {
                    _connection = null;
                }
            };
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}