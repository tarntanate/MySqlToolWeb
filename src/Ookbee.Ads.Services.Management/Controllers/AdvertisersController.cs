
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.CreateAdvertiser;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.DeleteAdvertiser;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.UpdateAdvertiser;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.GetAdvertiserById;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.GetAdvertiserList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisersController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<AdvertiserDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdvertiserListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<AdvertiserDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdvertiserByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateAdvertiserRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdvertiserCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateAdvertiserRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdvertiserCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdvertiserCommand(id), cancellationToken);
    }
}
