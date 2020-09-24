using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.IncrementAdStatsCache;
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
            var result = await Mediator.Send(new IncrementAdStatsCacheCommand(type.ToEnum<AdStatsType>(), adId), cancellationToken);
            if (result.Ok &&
                result.Data.HasValue())
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
