
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

namespace Ookbee.Ads.Services.Manager .Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignDto>>> GetList([FromQuery] int advertiserId, [FromQuery] string pricingModel, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetCampaignListQuery(advertiserId, pricingModel, start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignDto>> GetById([FromRoute] long id)
            => await Mediator.Send(new GetCampaignByIdQuery(id));

        [HttpPost("cost")]
        public async Task<HttpResult<long>> CreateCampaignCost([FromBody] CreateCampaignCostCommand request)
            => await Mediator.Send(new CreateCampaignCostCommand(request));

        [HttpPost("impression")]
        public async Task<HttpResult<long>> CreateCampaignImpression([FromBody] CreateCampaignImpressionCommand request)
            => await Mediator.Send(new CreateCampaignImpressionCommand(request));

        [HttpPut("{id}/cost")]
        public async Task<HttpResult<bool>> UpdateCampaignCost([FromRoute] long id, [FromBody] UpdateCampaignCostCommand request)
            => await Mediator.Send(new UpdateCampaignCostCommand(id, request));

        [HttpPut("{id}/impression")]
        public async Task<HttpResult<bool>> UpdateCampaignImpression([FromRoute] long id, [FromBody] UpdateCampaignImpressionCommand request)
            => await Mediator.Send(new UpdateCampaignImpressionCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id)
            => await Mediator.Send(new DeleteCampaignCommand(id));
    }
}
