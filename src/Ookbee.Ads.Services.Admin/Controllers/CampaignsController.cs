
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaignId;
using Ookbee.Ads.Application.Business.Campaign;
using Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign;
using Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetCampaignListCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetCampaignByIdQuery(id));

        [HttpGet("{id}/banners")]
        public async Task<HttpResult<IEnumerable<AdDto>>> GetAdList([FromRoute] string id, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdByCampaignIdQuery(id, start, length));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateCampaignCommand request)
            => await Mediator.Send(new CreateCampaignCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateCampaignCommand request)
            => await Mediator.Send(new UpdateCampaignCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteCampaignCommand(id));
    }
}
