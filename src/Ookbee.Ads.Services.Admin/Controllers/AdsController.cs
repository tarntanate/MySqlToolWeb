
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Commands.CreateAd;
using Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd;
using Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaignId;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.GetSignedUrl;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Admin.Controllers
{
    [ApiController]
    [Route("api/campaigns/{campaignId}/[controller]")]
    public class AdsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdDto>>> GetList([FromRoute] string campaignId, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdByCampaignIdQuery(campaignId, start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdDto>> GetById([FromRoute] string campaignId, [FromRoute] string id)
            => await Mediator.Send(new GetAdByIdQuery(campaignId, id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromRoute] string campaignId, [FromBody] CreateAdCommand request)
            => await Mediator.Send(new CreateAdCommand(campaignId, request));

        [HttpPost("{id}/signed-url")]
        public async Task<HttpResult<string>> GetSignedUrlById([FromRoute] string campaignId, [FromRoute] string id, [FromBody] GetSignedUrlQuery request)
            => await Mediator.Send(new GetSignedUrlQuery(campaignId, id, request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] string campaignId, [FromRoute] string id, [FromBody] UpdateAdCommand request)
            => await Mediator.Send(new UpdateAdCommand(campaignId, id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] string campaignId, [FromRoute] string id)
            => await Mediator.Send(new DeleteAdCommand(campaignId, id));
    }
}
