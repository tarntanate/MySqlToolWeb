
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdGroup;
using Ookbee.Ads.Application.Business.AdGroup.Commands.CreateAdGroup;
using Ookbee.Ads.Application.Business.AdGroup.Commands.DeleteAdGroup;
using Ookbee.Ads.Application.Business.AdGroup.Commands.UpdateAdGroup;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupById;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager.Controllers
{
    [ApiController]
    [Route("api/ad-groups")]
    public class AdGroupsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdGroupDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<AdGroupDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdGroupRequest request, CancellationToken cancellationToken)
                => await Mediator.Send(new CreateAdGroupCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdGroupRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdGroupCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdGroupCommand(id), cancellationToken);
    }
}
