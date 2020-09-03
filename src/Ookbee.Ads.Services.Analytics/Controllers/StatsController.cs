using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdAssetsStatsCache.Commands.IncrementAdAssetsStatsCache;
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
            if (Enum.TryParse(statsName, true, out AdStats stats))
            {
                await Mediator.Send(new IncrementAdAssetsStatsCacheCommand(adId, stats), cancellationToken);
            }
        }
    }
}
