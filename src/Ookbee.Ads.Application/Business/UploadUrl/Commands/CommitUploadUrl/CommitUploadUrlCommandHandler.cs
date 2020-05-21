using MediatR;
using Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrlById;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure;
using System;
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
            Console.WriteLine(uploadUrlResult.Ok);
            if (!uploadUrlResult.Ok)
                return result.Fail(uploadUrlResult.StatusCode, uploadUrlResult.Message);

            var uploadUrlData = uploadUrlResult.Data;
            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var copyObjectResult = await Mediator.Send(new CopyObjectCommand()
            {
                SourceAppid = uploadUrlData.AppId,
                SourceRegion = uploadUrlData.Region,
                SourceBucket = cosConfig.Bucket.Private,
                SourceKey = $"temp/{uploadUrlData.Key}",
                DestinationBucket = request.Bucket,
                DestinationKey = request.Key
            });
            if (!copyObjectResult.Ok)
            {
                if (copyObjectResult.StatusCode == HttpStatusCode.NotFound)
                    return result.Fail(404, "File not found.");
                return result.Fail(copyObjectResult.StatusCode, copyObjectResult.Message);
            }
            return result.Success(true);
        }
    }
}
