
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdSlot;
using Ookbee.Ads.Application.Business.AdSlot.Commands.CreateAdSlot;
using Ookbee.Ads.Application.Business.AdSlot.Commands.DeleteAdSlot;
using Ookbee.Ads.Application.Business.AdSlot.Commands.UpdateAdSlot;
using Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById;
using Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.AdSlots.Services.AdSlotvertising.Controllers
{
    [ApiController]
    [Route("api/ad-slots")]
    public class AdSlotSlotsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdSlotDto>>> GetList([FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdSlotListQuery(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdSlotDto>> GetById([FromRoute] string id)
            => await Mediator.Send(new GetAdSlotByIdQuery(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody] CreateAdSlotCommand request)
            => await Mediator.Send(new CreateAdSlotCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] string id, [FromBody] UpdateAdSlotCommand request)
            => await Mediator.Send(new UpdateAdSlotCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] string id)
            => await Mediator.Send(new DeleteAdSlotCommand(id));
    }
}
