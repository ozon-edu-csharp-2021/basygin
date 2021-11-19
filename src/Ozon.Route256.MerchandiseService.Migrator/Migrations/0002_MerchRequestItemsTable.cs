using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class MerchRequestItemsTable : Migration
    {
        public override void Up()
        {
            Create
                .Table("merch_request_items")
                .WithColumn("merch_request_id").AsInt64().ForeignKey("merch_requests", "id")
                .WithColumn("sku").AsInt64()
                .WithColumn("quantity").AsInt32()
                .WithColumn("quantity_issued").AsInt32();

            Create.
                UniqueConstraint().
                OnTable("merch_request_items")
                .Columns(new string[] { "merch_request_id", "sku" });
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_request_items;");
        }
    }
}
