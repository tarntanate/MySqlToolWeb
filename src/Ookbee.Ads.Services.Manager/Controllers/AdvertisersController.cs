
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
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisersController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdvertiserDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdvertiserListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<AdvertiserDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdvertiserByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdvertiserRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdvertiserCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdvertiserRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdvertiserCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdvertiserCommand(id), cancellationToken);
    }
}
