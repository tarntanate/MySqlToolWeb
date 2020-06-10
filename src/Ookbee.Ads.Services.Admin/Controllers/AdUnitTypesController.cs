
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdUnitType;
using Ookbee.Ads.Application.Business.AdUnitType.Commands.CreateAdUnitType;
using Ookbee.Ads.Application.Business.AdUnitType.Commands.DeleteAdUnitType;
using Ookbee.Ads.Application.Business.AdUnitType.Commands.UpdateAdUnitType;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeById;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Admin.Controllers
{
    [ApiController]
    [Route("api/ad-unit-types")]
    public class AdUnitTypesController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdUnitTypeDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdUnitTypeListQuery(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdUnitTypeDto>> GetById([FromRoute]long id)
            => await Mediator.Send(new GetAdUnitTypeByIdQuery(id));

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody]CreateAdUnitTypeCommand request)
            => await Mediator.Send(new CreateAdUnitTypeCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]long id, [FromBody]UpdateAdUnitTypeCommand request)
            => await Mediator.Send(new UpdateAdUnitTypeCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]long id)
            => await Mediator.Send(new DeleteAdUnitTypeCommand(id));
    }
}
