using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class AdNetworkGroupsController : ApiController
    {
        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdNetworkGroupListByKey([FromRoute] long groupId, [FromQuery] string platform, CancellationToken cancellationToken)
        {
            if (Enum.TryParse(platform, true, out Platform platformx))
            {
                var result = await Mediator.Send(new GetAdUnitCacheByGroupIdQuery(groupId, platformx), cancellationToken);
                return Content(result.Data, "application/json");
            }
            return new ContentResult() { StatusCode = 400 };
        }
    }
}
