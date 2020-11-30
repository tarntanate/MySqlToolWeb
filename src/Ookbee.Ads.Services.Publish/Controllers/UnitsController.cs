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
        public async Task<ContentResult> GetAdByUnitId(
            [FromQuery] string platform, 
            [FromRoute] long unitId, 
            [FromHeader(Name = "Ookbee-Account-Id")] string userId, 
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId, 
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdRedisQuery(platform, unitId, userId ?? deviceId), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
