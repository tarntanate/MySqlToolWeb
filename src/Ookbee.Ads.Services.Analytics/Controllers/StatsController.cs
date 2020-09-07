using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.IncrementAdStatsCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/stats")]
    public class StatsController : ApiController
    {
        [HttpGet]
        public async Task Get(
            [FromQuery(Name = "adId")] long adId,
            [FromQuery(Name = "unitId")] long unitId,
            [FromQuery(Name = "groupId")] long groupId,
            [FromQuery(Name = "event")] string statsName,
            [FromQuery(Name = "platform")] string platformName,
            CancellationToken cancellationToken)
        {
            if (Enum.TryParse(statsName, true, out AdStatsType stats))
            {
                if (Enum.TryParse(platformName, true, out Platform platform))
                {
                    await Mediator.Send(new IncrementAdStatsCacheCommand(adId, platform, stats), cancellationToken);
                }
            }
        }
    }
}
