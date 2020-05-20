using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.MediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Commands.CreateMediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Commands.DeleteMediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileById;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Admin.Controllers
{
    [ApiController]
    [Route("api/ads/{adId}/media-files")]
    public class MediaFilesController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<MediaFileDto>>> GetList([FromRoute] string adId, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetMediaFileListQuery(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<MediaFileDto>> GetById([FromRoute] string adId, [FromRoute] string id)
            => await Mediator.Send(new GetMediaFileByIdQuery(adId, id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromRoute] string adId, [FromBody] CreateMediaFileCommand request)
            => await Mediator.Send(new CreateMediaFileCommand(adId, request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] string adId, [FromRoute] string id, [FromBody] UpdateMediaFileCommand request)
            => await Mediator.Send(new UpdateMediaFileCommand(adId, id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] string adId, [FromRoute] string id)
            => await Mediator.Send(new DeleteMediaFileCommand(adId, id));
    }
}
