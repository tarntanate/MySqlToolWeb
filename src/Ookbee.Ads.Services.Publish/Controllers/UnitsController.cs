using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.GetAdByUnitId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class UnitsController : ApiController
    {
        [HttpGet("{unitId}/ad")]
        public async Task<ContentResult> GetAdAssetByUnitId([FromRoute] long unitId, [FromQuery] string platform, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdByUnitIdQuery(unitId, platform), cancellationToken);
            if (result.Ok)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
