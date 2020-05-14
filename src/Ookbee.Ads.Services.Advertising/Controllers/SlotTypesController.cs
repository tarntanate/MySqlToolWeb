﻿
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.SlotType;
using Ookbee.Ads.Application.Business.SlotType.Commands.CreateSlotType;
using Ookbee.Ads.Application.Business.SlotType.Commands.DeleteSlotType;
using Ookbee.Ads.Application.Business.SlotType.Commands.UpdateSlotType;
using Ookbee.Ads.Application.Business.SlotType.Queries.GetByIdSlotType;
using Ookbee.Ads.Application.Business.SlotType.Queries.GetListSlotType;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/slot-types")]
    public class SlotTypesController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<SlotTypeDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListSlotTypeCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<SlotTypeDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdSlotTypeCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateSlotTypeCommand request)
            => await Mediator.Send(new CreateSlotTypeCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateSlotTypeCommand request)
            => await Mediator.Send(new UpdateSlotTypeCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteSlotTypeCommand(id));
    }
}
