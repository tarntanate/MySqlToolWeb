using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.UploadUrl.Commands.CommitUploadUrl;
using Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/upload-urls")]
    public class UploadUrlController : ApiController
    {
        [HttpPost]
        public async Task<HttpResult<string>> GenerateUploadUrl()
            => await Mediator.Send(new GenerateUploadUrlCommand());

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> CommitSignUrlComman([FromRoute]string id)
            => await Mediator.Send(new CommitUploadUrlCommand(id));
    }
}
