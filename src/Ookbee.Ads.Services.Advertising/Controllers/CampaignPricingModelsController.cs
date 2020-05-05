using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.CampaignPricingModel;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.CreateCampaignPricingModel;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.DeleteCampaignPricingModel;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.UpdateCampaignPricingModel;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetByIdCampaignPricingModel;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetListCampaignPricingModel;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/campaigns/pricing-models")]
    public class PricingModelsController : BaseController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignPricingModelDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListCampaignPricingModelCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignPricingModelDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdCampaignPricingModelCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateCampaignPricingModelCommand request)
            => await Mediator.Send(new CreateCampaignPricingModelCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateCampaignPricingModelCommand request)
            => await Mediator.Send(new UpdateCampaignPricingModelCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteCampaignPricingModelCommand(id));
    }
}
