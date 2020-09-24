
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.CreateAdNetwork;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.DeleteAdNetwork;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.UpdateAdNetwork;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkById;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/ad-networks")]
    public class AdNetworkController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<AdNetworkDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? adUnitId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdNetworkListQuery(start, length, adUnitId), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<AdNetworkDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdNetworkByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateAdNetworkRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdNetworkCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateAdNetworkRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdNetworkCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdNetworkCommand(id), cancellationToken);
    }
}
