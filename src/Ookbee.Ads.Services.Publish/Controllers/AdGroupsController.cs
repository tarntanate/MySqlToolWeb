using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetwork.GroupItem;
using Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Queries.GetAdGroupItemListByKey;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/ad-groups")]
    public class AdGroupsController : ApiController
    {
        [HttpGet("{adGroupId}")]
        public async Task<HttpResult<GroupItemDto>> Get([FromRoute] long adGroupId)
            => await Mediator.Send(new GetGroupItemListByKeyQuery(adGroupId));
    }
}
