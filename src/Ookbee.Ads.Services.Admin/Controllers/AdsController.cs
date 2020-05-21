using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Commands.CreateAd;
using Ookbee.Ads.Application.Business.Ad.Commands.CreateAdUploadUrl;
using Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd;
using Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd;
using Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdUploadUrl;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaignId;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdDto>>> GetList([FromQuery] string campaignId, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdByCampaignIdQuery(campaignId, start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdDto>> GetById([FromRoute] string id)
            => await Mediator.Send(new GetAdByIdQuery(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody] CreateAdCommand request)
            => await Mediator.Send(new CreateAdCommand(request));

        [HttpPost("{id}/signed-url")]
        public async Task<HttpResult<AdUploadUrlDto>> SignedUrl([FromRoute] string id, [FromBody] CreateAdUploadUrlCommand request)
            => await Mediator.Send(new CreateAdUploadUrlCommand(id, request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] string id, [FromBody] UpdateAdCommand request)
            => await Mediator.Send(new UpdateAdCommand(id, request));

        [HttpPut("{id}/signed-url")]
        public async Task<HttpResult<bool>> CommitUrl([FromRoute] string id, [FromBody] UpdateAdUploadUrlCommand request)
            => await Mediator.Send(new UpdateAdUploadUrlCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] string campaignId, [FromRoute] string id)
            => await Mediator.Send(new DeleteAdCommand(id));
    }
}
