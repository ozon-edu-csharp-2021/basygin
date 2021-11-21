using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Models
{
    internal class MerchRequestItemDb
    {
        public long Sku { get; set; }
        public int Quantity { get; set; }
        public int QuantityIssued { get; set; }
    }
}
