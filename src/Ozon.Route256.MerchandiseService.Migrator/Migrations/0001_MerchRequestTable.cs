using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Migrator.Migrations
{
    [Migration(1)]
    public class MerchRequestTable : Migration
    {
        public override void Up()
        {
            Create
                .Table("merch_requests")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("type").AsInt32().NotNullable()
                .WithColumn("employee_id").AsInt64().NotNullable()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("size").AsInt32().NotNullable()
                .WithColumn("status").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("issued_at").AsDateTime();
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_requests;");
        }
    }
}
