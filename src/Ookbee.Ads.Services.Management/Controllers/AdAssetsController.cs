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
using System.Threading;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/ad-assets")]
    public class AdAssetsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<AdAssetDto>>> GetList([FromQuery] int start, [FromQuery] int length, [FromQuery] long? adId, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdAssetListQuery(start, length, adId), cancellationToken);

        [HttpGet("{id}")]
        public async Task<HttpResult<AdAssetDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdAssetByIdQuery(id), cancellationToken);

        [HttpGet("{id}/upload/url")]
        public async Task<HttpResult<string>> GenereateUploadUrl([FromRoute] long id, [FromQuery] string extension, CancellationToken cancellationToken)
            => await Mediator.Send(new GenerateUploadUrlCommand(id, extension), cancellationToken);

        [HttpPost]
        public async Task<HttpResult<long>> Create([FromBody] CreateAdAssetRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdAssetCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute] long id, [FromBody] UpdateAdAssetRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdAssetCommand(id, request), cancellationToken);

        [HttpPut("{id}/upload/commit")]
        public async Task<HttpResult<bool>> CommitUploadUrl([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new CommitUploadUrlCommand(id), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdAssetCommand(id), cancellationToken);
    }
}
