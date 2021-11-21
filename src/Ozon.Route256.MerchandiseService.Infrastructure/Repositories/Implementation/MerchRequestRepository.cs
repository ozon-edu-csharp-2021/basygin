using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Repository;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Interfaces;
using Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Models;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchRequestRepository : IMerchRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<MerchRequest> CreateMerchRequestAsync(MerchRequest merchRequest, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO public.merch_requests(type, employee_id, email, size, status, created_at)
	                VALUES (@type, @employeeId, @email, @size, @status, @createdAt);
                RETURNING merch_requests.id;";

            var merchRequestCommandParameters = new
            {
                type = merchRequest.Type.Id,
                employeeId = merchRequest.Employee.Id,
                email = merchRequest.Employee.Email.Value,
                size = merchRequest.Employee.Size.Id,
                status = merchRequest.Status.Id,
                createdAt = merchRequest.CreatedAt
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: merchRequestCommandParameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchRequestId = await connection.ExecuteScalarAsync<long>(commandDefinition);

            merchRequest.SetId(merchRequestId);

            _changeTracker.Track(merchRequest);
            return merchRequest;
        }

        public IUnitOfWork UnitOfWork { get; }
        
        public async Task<MerchRequest> GetMerchRequestByIdAsync(long id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT id, type, employee_id as EmployeeId, email, size, status, created_at as CreatedAt, issued_at as IssuedAt
                FROM merch_requests
                WHERE id = @merchRequestId;";

            var parameters = new
            {
                merchRequestId = id
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var merchRequestsDb = await connection.QueryAsync<MerchRequestDb>(commandDefinition);


            if (!merchRequestsDb.Any())
            {
                return null;
            }

            var merchRequestDb = merchRequestsDb.First();

            var merchRequest = new MerchRequest(
                MerchRequestType.Parse(merchRequestDb.Type),
                new Employee(
                    merchRequestDb.EmployeeId,
                    Size.Parse(merchRequestDb.Size),
                    new Email(merchRequestDb.Email)
                    ),
                merchRequestDb.CreatedAt,
                merchRequestDb.IssuedAt
                );

            merchRequest.SetId(merchRequestDb.Id);

            var merchRequestItems = await GetMerchRequestItems(merchRequest.Id, cancellationToken);

            merchRequest.SetMerchRequestItems(merchRequestItems);

            // Добавление после успешно выполненной операции.
            _changeTracker.Track(merchRequest);
            return merchRequest;
        }

        public async Task<MerchRequest> GetMerchRequestByEmployeeIdAndMerchTypeAsync(long employeeId, MerchRequestType merchType,
            CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT id, type, employee_id as EmployeeId, email, size, status, created_at as CreatedAt, issued_at as IssuedAt
                FROM merch_requests
                WHERE type = @MerchRequestType and employee_id = @EmployeeId;";

            var parameters = new
            {
                MerchRequestType = merchType.Id,
                EmployeeId = employeeId
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var merchRequestsDb = await connection.QueryAsync<MerchRequestDb>(commandDefinition);


            if (!merchRequestsDb.Any())
            {
                return null;
            }

            var merchRequestDb = merchRequestsDb.First();

            var merchRequest = new MerchRequest(
                MerchRequestType.Parse(merchRequestDb.Type),
                new Employee(
                    merchRequestDb.EmployeeId,
                    Size.Parse(merchRequestDb.Size),
                    new Email(merchRequestDb.Email)
                    ),
                merchRequestDb.CreatedAt,
                merchRequestDb.IssuedAt
                );

            merchRequest.SetId(merchRequestDb.Id);

            var merchRequestItems = await GetMerchRequestItems(merchRequest.Id, cancellationToken);

            merchRequest.SetMerchRequestItems(merchRequestItems);

            // Добавление после успешно выполненной операции.
            _changeTracker.Track(merchRequest);
            return merchRequest;
        }

        public Task<MerchRequest> GetMerchRequestAwaitsSupplyBySkuOrderByCreatedAsync(Sku sku, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private async Task<List<MerchRequestItem>> GetMerchRequestItems(long id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT sku, quantity, quantity_issued
                FROM merch_request_items
                WHERE merch_request_id = @merchRequestId;";

            var parameters = new
            {
                merchRequestId = id
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var merchRequestItemsDb = await connection.QueryAsync<MerchRequestItemDb>(commandDefinition);

            var result = merchRequestItemsDb.Select(x => new MerchRequestItem(
                new Sku(x.Sku),
                new Quantity(x.Quantity),
                new IssuedQuantity(x.QuantityIssued)
                )
            ).ToList();

            foreach (var stockItem in result)
            {
                _changeTracker.Track(stockItem);
            }

            return result;
        }

        public async Task Update(MerchRequest merchRequest, CancellationToken cancellationToken)
        {
            const string sql = @"
                UPDATE merch_requests
                SET
                    status = @status,
                    issued_at = @issuedAt
                WHERE id = @merchRequestId;";

            var parameters = new
            {
                merchRequestId = merchRequest.Id,
                status = merchRequest.Status.Id,
                issuedAt = merchRequest.IssuedAt
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);

            _changeTracker.Track(merchRequest);

            foreach (var item in merchRequest.Items)
            {
                await UpdateMerchRequestItem(merchRequest.Id, item, cancellationToken);
            }
        }

        private async Task UpdateMerchRequestItem(long merchRequestId, MerchRequestItem item, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO public.merch_request_items(
	                merch_request_id, sku, quantity, quantity_issued)
	                VALUES (@merchRequestId, @sku, @quantity, @quantityIssued)
                  ON CONFLICT (merch_request_id, sku)
                  DO UPDATE SET
                    quantity = EXCLUDED.quantity,
                    quantity_issued = EXCLUDED.quantity_issued;";

            var parameters = new
            {
                merchRequestId = merchRequestId,
                sku = item.Sku.Value,
                quantity = item.Quantity.Value,
                quantityIssued = item.IssuedQuantity.Value
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);

            _changeTracker.Track(item);
        }
    }
}