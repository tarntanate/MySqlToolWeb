using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{groupId}/stats")]
    public class GroupsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromRoute(Name = "adGroupId")] long adGroupId,
            [FromQuery(Name = "event")] string statsName,
            [FromQuery(Name = "platform")] string platformName,
            CancellationToken cancellationToken)
        {
            if (Enum.TryParse(statsName, true, out AdStatsType stats))
            {
                if (Enum.TryParse(platformName, true, out Platform platform))
                {
                    await Mediator.Send(new IncrementAdGroupStatsCacheCommand(adGroupId, platform, stats), cancellationToken);
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
