
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
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager .Controllers
{
    [ApiController]
    [Route("api/ad-units")]
    public class AdUnitsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdUnitDto>>> GetList([FromQuery] int adUnitTypeId, [FromQuery] int publisherId, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdUnitListQuery(adUnitTypeId, publisherId, start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdUnitDto>> GetById([FromRoute] long id)
            => await Mediator.Send(new GetAdUnitByIdQuery(id));

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdUnitCommand request)
            => await Mediator.Send(new CreateAdUnitCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdUnitCommand request)
            => await Mediator.Send(new UpdateAdUnitCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id)
            => await Mediator.Send(new DeleteAdUnitCommand(id));
    }
}
