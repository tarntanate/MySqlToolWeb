using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{adUnitId}/stats")]
    public class UnitsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromRoute(Name = "adUnitId")] long adUnitId,
            [FromQuery(Name = "event")] string statsName,
            [FromQuery(Name = "platform")] string platformName,
            CancellationToken cancellationToken)
        {
            if (Enum.TryParse(statsName, true, out AdStatsType stats))
            {
                if (Enum.TryParse(platformName, true, out Platform platform))
                {
                    await Mediator.Send(new IncrementAdUnitStatsCacheCommand(adUnitId, platform, stats), cancellationToken);
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
