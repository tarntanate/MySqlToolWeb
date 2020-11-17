using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.GetAdUnitByGroupIdCache;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherIdRedis;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ApiController
    {
        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdUnitByGroupId([FromQuery] string platform, [FromRoute] long groupId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdUnitByGroupIdCacheQuery(platform, groupId), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }

        [HttpGet("ids")]
        public async Task<ContentResult> GetAdGroupIdListByPubliser([FromHeader(Name = "Ookbee-App-Language")] string lange, [FromQuery] string publisher, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdGroupIdListByPublisherIdRedisQuery(publisher, lange), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
