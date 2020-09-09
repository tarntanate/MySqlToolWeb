
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.DeleteCampaign;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.UpdateCampaign;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? advertiserId, [FromQuery] string pricingModel, CancellationToken cancellationToken)
            => await Mediator.Send(new GetCampaignListQuery(start, length, advertiserId, pricingModel), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetCampaignByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> CreateCampaignImpression([FromBody] CreateCampaignRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateCampaignCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> UpdateCampaignCost([FromRoute] long id, [FromBody] UpdateCampaignRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateCampaignCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteCampaignCommand(id), cancellationToken);
    }
}
