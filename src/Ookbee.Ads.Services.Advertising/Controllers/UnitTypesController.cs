
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.UnitType;
using Ookbee.Ads.Application.Business.UnitType.Commands.CreateUnitType;
using Ookbee.Ads.Application.Business.UnitType.Commands.DeleteUnitType;
using Ookbee.Ads.Application.Business.UnitType.Commands.UpdateUnitType;
using Ookbee.Ads.Application.Business.UnitType.Queries.GetByIdUnitType;
using Ookbee.Ads.Application.Business.UnitType.Queries.GetListUnitType;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/campaigns/unit-types")]
    public class UnitTypesController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<UnitTypeDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListUnitTypeCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<UnitTypeDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdUnitTypeCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateUnitTypeCommand request)
            => await Mediator.Send(new CreateUnitTypeCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateUnitTypeCommand request)
            => await Mediator.Send(new UpdateUnitTypeCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteUnitTypeCommand(id));
    }
}
