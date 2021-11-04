using System.Linq;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects
{
    public class Size : Enumeration
    {
        public static Size XS = new Size(1, nameof(XS).ToLowerInvariant());
        public static Size S = new Size(2, nameof(S).ToLowerInvariant());
        public static Size M = new Size(3, nameof(M).ToLowerInvariant());
        public static Size L = new Size(4, nameof(L).ToLowerInvariant());
        public static Size XL = new Size(5, nameof(XL).ToLowerInvariant());
        public static Size XXL = new Size(6, nameof(XXL).ToLowerInvariant());
        
        public Size(int id, string name) : base(id, name)
        {
        }
    }
}