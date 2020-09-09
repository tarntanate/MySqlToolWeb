using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{groupId}/stats")]
    public class GroupsController : ApiController
    {
        [HttpGet]
        public async Task UpdateGroupStats([FromRoute] long adGroupId, [FromQuery] string platform, [FromQuery] string type, CancellationToken cancellationToken)
            => await Mediator.Send(new IncrementAdGroupStatsCacheCommand(adGroupId, platform.ToEnum<Platform>(), type.ToEnum<StatsType>()), cancellationToken);
    }
}
