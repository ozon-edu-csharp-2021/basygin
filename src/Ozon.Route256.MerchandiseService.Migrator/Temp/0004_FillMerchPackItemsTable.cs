using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Migrator.Migrations
{
    [Migration(4)]
    public class FillMerchPackItemsTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            #region Welcome pack
            // Welcome pack without size
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210000, merch_request_type = 10, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210001, merch_request_type = 10, quantity = 1 });

            // Welcome pack XS
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210010, size = 1, merch_request_type = 10, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210011, size = 1, merch_request_type = 10, quantity = 1 });

            // Welcome pack S
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210020, size = 2, merch_request_type = 10, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210021, size = 2, merch_request_type = 10, quantity = 1 });

            // Welcome pack M
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210030, size = 3, merch_request_type = 10, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210031, size = 3, merch_request_type = 10, quantity = 1 });

            // Welcome pack L
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210040, size = 4, merch_request_type = 10, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210041, size = 4, merch_request_type = 10, quantity = 1 });

            // Welcome pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210050, size = 5, merch_request_type = 10, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210051, size = 5, merch_request_type = 10, quantity = 1 });

            // Welcome pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210060, size = 6, merch_request_type = 10, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 210061, size = 6, merch_request_type = 10, quantity = 1 });
            #endregion

            #region Conference Listener Pack
            // Conference Listener Pack without size
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220000, merch_request_type = 20, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220001, merch_request_type = 20, quantity = 1 });

            // Conference Listener Pack XS
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220010, size = 1, merch_request_type = 20, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220011, size = 1, merch_request_type = 20, quantity = 1 });

            // Conference Listener Pack S
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220020, size = 2, merch_request_type = 20, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220021, size = 2, merch_request_type = 20, quantity = 1 });

            // Conference Listener Pack M
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220030, size = 3, merch_request_type = 20, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220031, size = 3, merch_request_type = 20, quantity = 1 });

            // Conference Listener Pack L
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220040, size = 4, merch_request_type = 20, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220041, size = 4, merch_request_type = 20, quantity = 1 });

            // Conference Listener Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220050, size = 5, merch_request_type = 20, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220051, size = 5, merch_request_type = 20, quantity = 1 });

            // Conference Listener Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220060, size = 6, merch_request_type = 20, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 220061, size = 6, merch_request_type = 20, quantity = 1 });
            #endregion

            #region Conference Listener Pack
            // Conference Speaker Pack without size
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230000, merch_request_type = 30, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230001, merch_request_type = 30, quantity = 1 });

            // Conference Speaker Pack XS
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230010, size = 1, merch_request_type = 30, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230011, size = 1, merch_request_type = 30, quantity = 1 });

            // Conference Speaker Pack S
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230020, size = 2, merch_request_type = 30, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230021, size = 2, merch_request_type = 30, quantity = 1 });

            // Conference Speaker Pack M
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230030, size = 3, merch_request_type = 30, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230031, size = 3, merch_request_type = 30, quantity = 1 });

            // Conference Speaker Pack L
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230040, size = 4, merch_request_type = 30, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230041, size = 4, merch_request_type = 30, quantity = 1 });

            // Conference Speaker Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230050, size = 5, merch_request_type = 30, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230051, size = 5, merch_request_type = 30, quantity = 1 });

            // Conference Speaker Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230060, size = 6, merch_request_type = 30, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 230061, size = 6, merch_request_type = 30, quantity = 1 });
            #endregion

            #region Probation Period Ending Pack
            // Probation Period Ending Pack without size
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240000, merch_request_type = 40, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240001, merch_request_type = 40, quantity = 1 });

            // Probation Period Ending Pack XS
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240010, size = 1, merch_request_type = 40, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240011, size = 1, merch_request_type = 40, quantity = 1 });

            // Probation Period Ending Pack S
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240020, size = 2, merch_request_type = 40, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240021, size = 2, merch_request_type = 40, quantity = 1 });

            // Probation Period Ending Pack M
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240030, size = 3, merch_request_type = 40, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240031, size = 3, merch_request_type = 40, quantity = 1 });

            // Probation Period Ending Pack L
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240040, size = 4, merch_request_type = 40, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240041, size = 4, merch_request_type = 40, quantity = 1 });

            // Probation Period Ending Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240050, size = 5, merch_request_type = 40, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240051, size = 5, merch_request_type = 40, quantity = 1 });

            // Probation Period Ending Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240060, size = 6, merch_request_type = 40, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 240061, size = 6, merch_request_type = 40, quantity = 1 });
            #endregion

            #region Veteran pack
            // Veteran Pack without size
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250000, merch_request_type = 50, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250001, merch_request_type = 50, quantity = 1 });

            // Veteran Pack XS
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250010, size = 1, merch_request_type = 50, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250011, size = 1, merch_request_type = 50, quantity = 1 });

            // Veteran Pack S
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250020, size = 2, merch_request_type = 50, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250021, size = 2, merch_request_type = 50, quantity = 1 });

            // Veteran Pack M
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250030, size = 3, merch_request_type = 50, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250031, size = 3, merch_request_type = 50, quantity = 1 });

            // Veteran Pack L
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250040, size = 4, merch_request_type = 50, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250041, size = 4, merch_request_type = 50, quantity = 1 });

            // Veteran Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250050, size = 5, merch_request_type = 50, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250051, size = 5, merch_request_type = 50, quantity = 1 });

            // Veteran Pack XL
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250060, size = 6, merch_request_type = 50, quantity = 2 });
            Insert.IntoTable("merch_pack_items").Row(new { sku = 250061, size = 6, merch_request_type = 50, quantity = 1 });
            #endregion
        }
    }
}
