
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.AdAsset;
using Ookbee.Ads.Application.Business.AdAsset.Commands.CreateAdAsset;
using Ookbee.Ads.Application.Business.AdAsset.Commands.DeleteAdAsset;
using Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetList;
using Ookbee.Ads.Application.Business.AdAsset.Commands.CommitUploadUrl;
using Ookbee.Ads.Application.Business.AdAsset.Commands.GenerateUploadUrl;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Admin.Controllers
{
    [ApiController]
    [Route("api/ad-assets")]
    public class AdAssetsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdAssetDto>>> GetList([FromQuery] int adUnitTypeId, [FromQuery] int publisherId, [FromQuery] int start, [FromQuery] int length)
            => await Mediator.Send(new GetAdAssetListQuery(adUnitTypeId, publisherId, start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<AdAssetDto>> GetById([FromRoute] long id)
            => await Mediator.Send(new GetAdAssetByIdQuery(id));

        [HttpGet("{id}/upload/url")]
        public async Task<HttpResult<string>> GenereateUploadUrl([FromRoute] long id, [FromQuery] string extension)
            => await Mediator.Send(new GenerateUploadUrlCommand(id, extension));

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdAssetCommand request)
            => await Mediator.Send(new CreateAdAssetCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdAssetCommand request)
            => await Mediator.Send(new UpdateAdAssetCommand(id, request));

        [HttpPut("{id}/upload/commit")]
        public async Task<HttpResult<bool>> CommitUploadUrl([FromRoute] long id)
            => await Mediator.Send(new CommitUploadUrlCommand(id));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id)
            => await Mediator.Send(new DeleteAdAssetCommand(id));
    }
}
