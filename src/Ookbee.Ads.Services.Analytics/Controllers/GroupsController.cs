using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache;
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
        public async Task<ContentResult> UpdateGroupStats([FromRoute] long adGroupId, [FromQuery] string platform, [FromQuery] string type, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IncrementAdGroupStatsCacheCommand(type.ToEnum<StatsType>(), adGroupId), cancellationToken);
            if (result.Ok &&
                result.Data.HasValue())
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
