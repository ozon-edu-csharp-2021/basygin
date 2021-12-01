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
                .WithColumn("sku").AsInt64()
                .WithColumn("size").AsInt32().Nullable()
                .WithColumn("merch_request_type").AsInt32()
                .WithColumn("quantity").AsInt32();

            Create
                .Index()
                .OnTable("merch_pack_items")
                .OnColumn("size").Ascending()
                .OnColumn("merch_request_type").Ascending();
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_pack_items;");
        }
    }
}
