using MediatR;
using Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrlById;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.DeleteObject;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.CommitUploadUrl
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

            var uploadUrlResult = await Mediator.Send(new GetUploadUrlByIdQuery(request.Id));
            if (!uploadUrlResult.Ok)
                return result.Fail(uploadUrlResult.StatusCode, uploadUrlResult.Message);

            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var copyObjectResult = await Mediator.Send(new CopyObjectCommand()
            {
                SourceAppid = uploadUrlResult.Data.AppId,
                SourceRegion = uploadUrlResult.Data.Region,
                SourceBucket = uploadUrlResult.Data.SourceBucket,
                SourceKey = uploadUrlResult.Data.SourceKey,
                DestinationBucket = uploadUrlResult.Data.DestinationBucket,
                DestinationKey = uploadUrlResult.Data.DestinationKey
            });
            if (!copyObjectResult.Ok && copyObjectResult.StatusCode != HttpStatusCode.NoContent)
            {
                if (copyObjectResult.StatusCode == HttpStatusCode.NotFound)
                    return result.Fail(404, "Can't move file, File not found.");
                return result.Fail(copyObjectResult.StatusCode, copyObjectResult.Message);
            }

            var deleteObjectResult = await Mediator.Send(new DeleteObjectCommand()
            {
                Bucket = uploadUrlResult.Data.SourceBucket,
                Key = uploadUrlResult.Data.SourceKey
            });
            if (!deleteObjectResult.Ok && deleteObjectResult.StatusCode != HttpStatusCode.NoContent)
            {
                if (deleteObjectResult.StatusCode == HttpStatusCode.NotFound)
                    return result.Fail(404, "Can't delete file, File not found.");
                return result.Fail(deleteObjectResult.StatusCode, deleteObjectResult.Message);
            }

            return result.Success(true);
        }
    }
}
