
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.CreateAdGroup;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.DeleteAdGroup;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.UpdateAdGroup;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupById;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/ad-groups")]
    public class AdGroupsController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<AdGroupDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? adUnitTypeId, [FromQuery] long? publisherId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupListQuery(start, length, adUnitTypeId, publisherId), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<AdGroupDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateAdGroupRequest request, CancellationToken cancellationToken)
                => await Mediator.Send(new CreateAdGroupCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateAdGroupRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdGroupCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdGroupCommand(id), cancellationToken);
    }
}
