using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.CreateAd;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.DeleteAd;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAd;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAdStatus;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ApiController
    {
        [HttpGet]
        public async Task<object> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? adUnitId, [FromQuery] long? campaignId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdListQuery(start, length, adUnitId, campaignId), cancellationToken);

        [HttpGet("{id}")]
        public async Task<object> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<object> Create([FromBody] CreateAdRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<object> Update([FromRoute] long id, [FromBody] UpdateAdRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdCommand(id, request), cancellationToken);

        [HttpPut("status/{id}")]
        public async Task<object> UpdateAdStatus([FromRoute] long id, [FromBody] UpdateAdStatusRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdStatusCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<object> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdCommand(id), cancellationToken);
    }
}
