
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Commands.CreateAd;
using Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd;
using Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.Ad.Queries.GetSignedUrl;
using Ookbee.Ads.Application.Business.MediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByAdId;
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
        public async Task<HttpResult<IEnumerable<AdDto>>> GetList([FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdListQuery(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdDto>> GetById([FromRoute] string id)
            => await Mediator.Send(new GetAdByIdQuery(id));

        [HttpGet("{id}/media-files")]
        public async Task<HttpResult<IEnumerable<MediaFileDto>>> GetMediaFileList([FromRoute] string id, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetMediaFileByAdIdQuery(id, start, length));

        [HttpGet("signed-url")]
        public async Task<HttpResult<string>> GetSignedUrlById([FromRoute] string id)
            => await Mediator.Send(new GetSignedUrlQuery());

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody] CreateAdCommand request)
            => await Mediator.Send(new CreateAdCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] string id, [FromBody] UpdateAdCommand request)
            => await Mediator.Send(new UpdateAdCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] string id)
            => await Mediator.Send(new DeleteAdCommand(id));
    }
}
