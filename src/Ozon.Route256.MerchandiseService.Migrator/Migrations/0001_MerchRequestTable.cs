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
                .WithColumn("type").AsInt32()
                .WithColumn("employee_id").AsInt64()
                .WithColumn("email").AsString()
                .WithColumn("size").AsInt32()
                .WithColumn("status").AsInt32()
                .WithColumn("created_at").AsDateTime()
                .WithColumn("issued_at").AsDateTime().Nullable();

            Create
                .Index()
                .OnTable("merch_requests")
                .OnColumn("id").Ascending();

            Create
                .Index()
                .OnTable("merch_requests")
                .OnColumn("type").Ascending()
                .OnColumn("employee_id").Ascending();
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_requests;");
        }
    }
}
