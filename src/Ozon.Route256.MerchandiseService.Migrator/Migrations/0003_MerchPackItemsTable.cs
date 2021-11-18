using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Migrator.Migrations
{
    [Migration(3)]
    public class MerchPackItemsTable : Migration
    {
        public override void Up()
        {
            Create
                .Table("merch_pack_items")
                .WithColumn("sku").AsInt64().NotNullable()
                .WithColumn("size").AsInt32().Nullable()
                .WithColumn("merch_request_type").AsInt32().NotNullable()
                .WithColumn("quantity").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_pack_items;");
        }
    }
}
