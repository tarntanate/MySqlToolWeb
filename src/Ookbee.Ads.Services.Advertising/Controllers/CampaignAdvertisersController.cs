
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.CampaignAdvertiser;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.CreateCampaignAdvertiser;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.DeleteCampaignAdvertiser;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.UpdateCampaignAdvertiser;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetByIdCampaignAdvertiser;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetListCampaignAdvertiser;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/campaigns/[controller]")]
    public class AdvertisersController : BaseController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<CampaignAdvertiserDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListCampaignAdvertiserCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<CampaignAdvertiserDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdCampaignAdvertiserCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateCampaignAdvertiserCommand request)
            => await Mediator.Send(new CreateCampaignAdvertiserCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<string>> Update([FromRoute]string id, [FromBody]UpdateCampaignAdvertiserCommand request)
            => await Mediator.Send(new UpdateCampaignAdvertiserCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteCampaignAdvertiserCommand(id));
    }
}
