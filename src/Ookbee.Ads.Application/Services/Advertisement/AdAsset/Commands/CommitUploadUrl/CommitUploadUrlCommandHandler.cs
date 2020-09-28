using MediatR;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.DeleteObject;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Settings;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommandHandler : IRequestHandler<CommitUploadUrlCommand, Response<bool>>
    {
        private readonly IMediator Mediator;

        public CommitUploadUrlCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Response<bool>> Handle(CommitUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            var adAssetResult = await Mediator.Send(new GetAdAssetByIdQuery(request.Id), cancellationToken);
            if (!adAssetResult.IsSuccess)
                return result.Status(adAssetResult.StatusCode, adAssetResult.Message);

            var adAsset = adAssetResult.Data;
            var cos = GlobalVar.AppSettings.Tencent.Cos;
            var bucket = cos.Bucket.Private;
            var key = adAsset.AssetPath;

            var copyObjectResult = await CopyObject(cos, bucket, key, cancellationToken);
            if (!copyObjectResult.IsSuccess)
                return result.Status(copyObjectResult.StatusCode, copyObjectResult.Message);

            var deleteObjectResult = await DeleteObject(bucket, key, cancellationToken);
            if (!deleteObjectResult.IsSuccess)
                return result.Status(deleteObjectResult.StatusCode, deleteObjectResult.Message);

            return result.OK(true);
        }

        private async Task<Response<bool>> CopyObject(CosSettings cos, string bucket, string key, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            var copyObjectCommand = new CopyObjectCommand()
            {
                SourceAppid = cos.AppId,
                SourceRegion = cos.Region,
                SourceBucket = cos.Bucket.Private,
                SourceKey = $"temp{key}",
                DestinationBucket = cos.Bucket.Public,
                DestinationKey = key,
            };
            var copyObjectResult = await Mediator.Send(copyObjectCommand, cancellationToken);
            if (!copyObjectResult.IsSuccess && copyObjectResult.StatusCode != HttpStatusCode.NoContent)
            {
                if (copyObjectResult.StatusCode == HttpStatusCode.NotFound)
                    return result.NotFound("File not found.");
                return result.Status(copyObjectResult.StatusCode, copyObjectResult.Message);
            }

            return result.OK(true);
        }

        private async Task<Response<bool>> DeleteObject(string bucket, string key, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            var deleteObjectCommand = new DeleteObjectCommand()
            {
                Bucket = bucket,
                Key = $"temp{key}",
            };
            var deleteObjectResult = await Mediator.Send(deleteObjectCommand, cancellationToken);
            if (!deleteObjectResult.IsSuccess && deleteObjectResult.StatusCode != HttpStatusCode.NoContent)
                return result.Status(deleteObjectResult.StatusCode, deleteObjectResult.Message);

            return result.OK(true);
        }
    }
}
