using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Repositories.Models
{
    internal class MerchRequestDb
    {
        public long Id { get; set; }
        public int Type { get; set; }
        public int Size { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? IssuedAt { get; set; }
    }
}
