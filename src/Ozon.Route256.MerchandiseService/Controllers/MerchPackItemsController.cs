using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ozon.Route256.MerchandiseService.HttpModels;

namespace Ozon.Route256.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merchpackitems")]
    [Produces("application/json")]
    public class MerchPackItemsController
    {
        [HttpPut]
        public Task<IActionResult> UpdateMerchPackItemRequest(MerchPackItemModel merchPackItemModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public Task<IActionResult> AddMerchPackItemRequest(MerchPackItemModel merchPackItemModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        // ... добавление удаление обновление итемов из пака
    }
}