using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.GetAdByUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class UnitsController : ApiController
    {
        [HttpGet("{unitId}/ad")]
        public async Task<ContentResult> GetAdAssetByUnitId([FromQuery] string platform, [FromRoute] long unitId, [FromQuery] long? ookbeeId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdByUnitIdQuery(platform, unitId, ookbeeId), cancellationToken);
            if (result.IsSuccess && result.Data.HasValue())
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
