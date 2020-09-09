using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache;
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
        public async Task UpdateAdStats([FromRoute] long adId, [FromQuery] string type, [FromQuery] string platform, CancellationToken cancellationToken)
            => await Mediator.Send(new IncrementAdStatsCacheCommand(adId, platform.ToEnum<Platform>(), type.ToEnum<StatsType>()), cancellationToken);
    }
}
