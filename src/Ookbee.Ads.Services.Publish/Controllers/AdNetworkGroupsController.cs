using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdUnitCache.Commands.GetAdUnitCacheByGroupId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class AdNetworkGroupsController : ApiController
    {
        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdNetworkGroupListByKey([FromRoute] long groupId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdUnitCacheByGroupIdQuery(groupId), cancellationToken);
            return Content(result.Data, "application/json");
        }
    }
}
