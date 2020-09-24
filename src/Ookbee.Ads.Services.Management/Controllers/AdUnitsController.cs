
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.CreateAdUnit;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.DeleteAdUnit;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.UpdateAdUnit;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/ad-units")]
    public class AdUnitsController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<AdUnitDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? adGroupId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdUnitListQuery(start, length, adGroupId), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<AdUnitDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdUnitByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateAdUnitRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdUnitCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateAdUnitRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdUnitCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdUnitCommand(id), cancellationToken);
    }
}
