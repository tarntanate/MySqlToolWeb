﻿
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdUnit;
using Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit;
using Ookbee.Ads.Application.Business.AdUnit.Commands.DeleteAdUnit;
using Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager.Controllers
{
    [ApiController]
    [Route("api/ad-units")]
    public class AdUnitsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdUnitDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] int adUnitTypeId, [FromQuery] int publisherId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdUnitListQuery(start, length, adUnitTypeId, publisherId), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<AdUnitDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdUnitByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdUnitRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdUnitCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdUnitRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdUnitCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdUnitCommand(id), cancellationToken);
    }
}
