
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.CreateAdUnitType;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.DeleteAdUnitType;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.UpdateAdUnitType;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeById;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/ad-unit-types")]
    public class AdUnitTypesController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<AdUnitTypeDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdUnitTypeListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<AdUnitTypeDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdUnitTypeByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateAdUnitTypeRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdUnitTypeCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateAdUnitTypeRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdUnitTypeCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdUnitTypeCommand(id), cancellationToken);
    }
}
