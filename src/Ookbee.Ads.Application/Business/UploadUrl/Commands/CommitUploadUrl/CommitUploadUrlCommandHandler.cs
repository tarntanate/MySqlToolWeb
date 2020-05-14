using MediatR;
using Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl;
using Ookbee.Ads.Application.Business.UploadUrl.Queries.GetByIdUploadUrl;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure;
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

            var uploadUrlResult = await Mediator.Send(new GetByIdUploadUrlCommand(request.Id));
            if (!uploadUrlResult.Ok)
                return result.Fail(uploadUrlResult.StatusCode, uploadUrlResult.Message);

            var uploadUrlData = uploadUrlResult.Data;
            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var copyObjectCommand = new CopyObjectCommand()
            {
                SourceAppid = uploadUrlData.AppId,
                SourceRegion = uploadUrlData.Region,
                SourceBucket = cosConfig.Bucket.Temp,
                SourceKey = uploadUrlData.Key,
                DestinationBucket = uploadUrlData.DestinationBucket,
                DestinationKey = uploadUrlData.Key
            };

            var isSuccessCopyObject = await Mediator.Send(copyObjectCommand);
            if (!isSuccessCopyObject)
                return result.Fail(500);

            var UpdateMediaUrlResult = await Mediator.Send(new UpdateMediaUrlCommand(uploadUrlData.Id, uploadUrlData.SignUrl));
            if (!UpdateMediaUrlResult.Ok)
                return UpdateMediaUrlResult;

            return result.Success(true);
        }
    }
}
