using MediatR;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.DeleteObject;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommandHandler : IRequestHandler<CommitUploadUrlCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }

        public CommitUploadUrlCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<HttpResult<bool>> Handle(CommitUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var adAssetResult = await Mediator.Send(new GetAdAssetByIdQuery(request.Id));
            if (!adAssetResult.Ok)
                return result.Fail(adAssetResult.StatusCode, adAssetResult.Message);

            var adAsset = adAssetResult.Data;
            var cos = GlobalVar.AppSettings.Tencent.Cos;
            var bucket = cos.Bucket.Private;
            var key = adAsset.AssetPath;

            var copyObjectResult = await CopyObject(cos, bucket, key);
            if (!copyObjectResult.Ok)
                return result.Fail(copyObjectResult.StatusCode, copyObjectResult.Message);

            var deleteObjectResult = await DeleteObject(bucket, key);
            if (!deleteObjectResult.Ok)
                return result.Fail(deleteObjectResult.StatusCode, deleteObjectResult.Message);

            return result.Success(true);
        }

        private async Task<HttpResult<bool>> CopyObject(CosSettings cos, string bucket, string key)
        {
            var result = new HttpResult<bool>();

            var copyObjectCommand = new CopyObjectCommand()
            {
                SourceAppid = cos.AppId,
                SourceRegion = cos.Region,
                SourceBucket = cos.Bucket.Private,
                SourceKey = $"temp{key}",
                DestinationBucket = cos.Bucket.Public,
                DestinationKey = key,
            };
            var copyObjectResult = await Mediator.Send(copyObjectCommand);
            if (!copyObjectResult.Ok && copyObjectResult.StatusCode != HttpStatusCode.NoContent)
            {
                if (copyObjectResult.StatusCode == HttpStatusCode.NotFound)
                    return result.Fail(404, "File not found.");
                return result.Fail(copyObjectResult.StatusCode, copyObjectResult.Message);
            }

            return result.Success(true);
        }

        private async Task<HttpResult<bool>> DeleteObject(string bucket, string key)
        {
            var result = new HttpResult<bool>();

            var deleteObjectCommand = new DeleteObjectCommand()
            {
                Bucket = bucket,
                Key = $"temp{key}",
            };
            var deleteObjectResult = await Mediator.Send(deleteObjectCommand);
            if (!deleteObjectResult.Ok && deleteObjectResult.StatusCode != HttpStatusCode.NoContent)
                return result.Fail(deleteObjectResult.StatusCode, deleteObjectResult.Message);

            return result.Success(true);
        }
    }
}
