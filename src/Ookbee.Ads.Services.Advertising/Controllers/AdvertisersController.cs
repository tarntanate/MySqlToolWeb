
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Advertiser;
using Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser;
using Ookbee.Ads.Application.Business.Advertiser.Commands.DeleteAdvertiser;
using Ookbee.Ads.Application.Business.Advertiser.Commands.UpdateAdvertiser;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisersController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdvertiserDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdvertiserListQuery(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdvertiserDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetAdvertiserByIdQuery(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateAdvertiserCommand request)
            => await Mediator.Send(new CreateAdvertiserCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateAdvertiserCommand request)
            => await Mediator.Send(new UpdateAdvertiserCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteAdvertiserCommand(id));
    }
}
