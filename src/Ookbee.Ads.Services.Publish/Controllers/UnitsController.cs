using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Cache.AdRedis.Commands.GetAdRedis;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ApiController
    {
        [HttpGet("{unitId}/ad")]
        public async Task<ContentResult> GetAdByUnitId([FromQuery] string platform, [FromRoute] long unitId, [FromQuery] string ookbeeId_query, [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId_header, CancellationToken cancellationToken)
        {
            long? ookbeeId = Convert.ToInt32(ookbeeId_query ?? ookbeeId_header);
            var result = await Mediator.Send(new GetAdRedisQuery(platform, unitId, userId: ookbeeId), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
