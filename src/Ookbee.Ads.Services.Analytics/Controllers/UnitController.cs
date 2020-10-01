using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.UpdateAdUnitStatsRedis;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{adUnitId}/stats")]
    public class UnitsController : ApiController
    {
        [HttpGet]
        public async Task<ContentResult> UpdateUnitStats([FromRoute] long adUnitId, [FromQuery] string platform, [FromQuery] string type, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UpdateAdUnitStatsRedisCommand(adUnitId, type.ToEnum<AdStatsType>()), cancellationToken);
            if (result.IsSuccess)
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
