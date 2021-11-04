using System.Collections.Generic;
using MediatR;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Commands.SupplyShip
{
    public class SupplyShipCommand : IRequest
    {
        /// <summary>
        /// Идентификатор поставки.
        /// </summary>
        public long SupplyId { get; set; }

        /// <summary>
        /// Коллекция товаров в поставке.
        /// </summary>
        public ICollection<SupplyShippedItem> Items { get; set; }
    }
    
    public class SupplyShippedItem
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long SkuId { get; set; }

        /// <summary>
        /// Количество товаров.
        /// </summary>
        public long Quantity { get; set; }
    }
}