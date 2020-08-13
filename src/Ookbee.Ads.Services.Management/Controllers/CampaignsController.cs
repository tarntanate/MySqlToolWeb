
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Campaign;
using Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList;
using Ookbee.Ads.Application.Business.CampaignCost.Commands.CreateCampaignCost;
using Ookbee.Ads.Application.Business.CampaignCost.Commands.UpdateCampaignCost;
using Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression;
using Ookbee.Ads.Application.Business.CampaignImpression.Commands.UpdateCampaignImpression;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Threading;

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

        [HttpPost("cost")]
        public async Task<HttpResult<long>> CreateCampaignCost([FromBody] CreateCampaignCostRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateCampaignCostCommand(request), cancellationToken);

        [HttpPost("impression")]
        public async Task<HttpResult<long>> CreateCampaignImpression([FromBody] CreateCampaignImpressionRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateCampaignImpressionCommand(request), cancellationToken);

        [HttpPut("{id}/cost")]
        public async Task<HttpResult<bool>> UpdateCampaignCost([FromRoute] long id, [FromBody] UpdateCampaignCostRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateCampaignCostCommand(id, request), cancellationToken);

        [HttpPut("{id}/impression")]
        public async Task<HttpResult<bool>> UpdateCampaignImpression([FromRoute] long id, [FromBody] UpdateCampaignImpressionRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateCampaignImpressionCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteCampaignCommand(id), cancellationToken);
    }
}
