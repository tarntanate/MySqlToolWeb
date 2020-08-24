using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdUnitListByGroup;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class AdNetworkGroupsController : ApiController
    {
        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdNetworkGroupListByKey([FromRoute] long groupId)
        {
            var result = await Mediator.Send(new GetAdUnitListByGroupQuery(groupId));
            return Content(result.Data, "application/json");
        }
    }
}
