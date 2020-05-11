
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.MediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Commands.CreateMediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Commands.DeleteMediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetByIdMediaFile;
using Ookbee.Ads.Application.Business.MediaFile.Queries.GetListMediaFile;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/campaigns/banners/[controller]")]
    public class MediaFilesController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<MediaFileDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListMediaFileCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<MediaFileDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdMediaFileCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreateMediaFileCommand request)
            => await Mediator.Send(new CreateMediaFileCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdateMediaFileCommand request)
            => await Mediator.Send(new UpdateMediaFileCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeleteMediaFileCommand(id));
    }
}
