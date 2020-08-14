using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetwork.Group.Queries.GetAdGroupListByKey;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/ad-groups")]
    public class AdGroupsController : ApiController
    {
        [HttpGet("{adGroupId}")]
        public async Task<ContentResult> Get([FromRoute] long adGroupId)
        {
            var getGroupListByKey = await Mediator.Send(new GetGroupListByKeyQuery(adGroupId));
            return Content(getGroupListByKey.Data, "application/json");
        }
    }
}
