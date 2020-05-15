
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.Banner;
using Ookbee.Ads.Application.Business.Banner.Commands.CreateBanner;
using Ookbee.Ads.Application.Business.Banner.Commands.DeleteBanner;
using Ookbee.Ads.Application.Business.Banner.Commands.UpdateBanner;
using Ookbee.Ads.Application.Business.Banner.Queries.GetBannerById;
using Ookbee.Ads.Application.Business.Banner.Queries.GetBannerList;
using Ookbee.Ads.Application.Business.Banner.Queries.GetSignedUrl;
using Ookbee.Ads.Application.Business.MediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByBannerId;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannersController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<BannerDto>>> GetList([FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetBannerListQuery(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<BannerDto>> GetById([FromRoute] string id)
            => await Mediator.Send(new GetBannerByIdQuery(id));

        [HttpGet("{id}/media-files")]
        public async Task<HttpResult<IEnumerable<MediaFileDto>>> GetMediaFileList([FromRoute] string id, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetMediaFileByBannerIdQuery(id, start, length));

        [HttpGet("signed-url")]
        public async Task<HttpResult<string>> GetSignedUrlById([FromRoute] string id)
            => await Mediator.Send(new GetSignedUrlQuery());

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody] CreateBannerCommand request)
            => await Mediator.Send(new CreateBannerCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] string id, [FromBody] UpdateBannerCommand request)
            => await Mediator.Send(new UpdateBannerCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] string id)
            => await Mediator.Send(new DeleteBannerCommand(id));
    }
}
