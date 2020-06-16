using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Commands.CreateAd;
using Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd;
using Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? adUnitId, [FromQuery] long? campaignId)
            => await Mediator.Send(new GetAdListQuery(start, length, adUnitId, campaignId));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdDto>> GetById([FromRoute] long id)
            => await Mediator.Send(new GetAdByIdQuery(id));

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdCommand request)
            => await Mediator.Send(new CreateAdCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdCommand request)
            => await Mediator.Send(new UpdateAdCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id)
            => await Mediator.Send(new DeleteAdCommand(id));
    }
}
