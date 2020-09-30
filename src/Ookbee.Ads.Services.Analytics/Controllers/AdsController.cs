using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.IncrementAdStatsCache;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.UpdateAdStatsRedis;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{adId}/stats")]
    public class AdsController : ApiController
    {
        [HttpGet]
        public async Task<ContentResult> UpdateAdStats([FromRoute] long adId, [FromQuery] string type, [FromQuery] string platform, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UpdateAdStatsRedisCommand(adId, type.ToEnum<AdStatsType>(), 1), cancellationToken);
            if (result.IsSuccess &&
                result.Data.HasValue())
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
