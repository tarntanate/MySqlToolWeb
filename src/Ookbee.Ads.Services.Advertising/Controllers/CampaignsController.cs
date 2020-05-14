
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Banner;
using Ookbee.Ads.Application.Business.Banner.Queries.GetByCampaingIdBanner;
using Ookbee.Ads.Application.Business.Campaign;
using Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign;
using Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetByIdCampaign;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetListCampaign;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListCampaignCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdCampaignCommand(id));

        [HttpGet("{id}/banners")]
        public async Task<HttpResult<IEnumerable<BannerDto>>> GetListMediaFile([FromRoute] string id, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetByCampaingIdBannerCommand(id, start, length));

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
