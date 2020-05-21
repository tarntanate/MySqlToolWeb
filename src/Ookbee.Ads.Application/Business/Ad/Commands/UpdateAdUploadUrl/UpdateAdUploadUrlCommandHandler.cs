using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaUrl;
using Ookbee.Ads.Application.Business.UploadUrl.Commands.CommitUploadUrl;
using Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrlById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdUploadUrl
{
    public class UpdateAdUploadUrlCommandHandler : IRequestHandler<UpdateAdUploadUrlCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<UploadUrlDocument> UploadUrlMongoDB { get; }

        public UpdateAdUploadUrlCommandHandler(
            IMediator mediator,
            AdsMongoRepository<UploadUrlDocument> uploadUrlMongoDB)
        {
            Mediator = mediator;
            UploadUrlMongoDB = uploadUrlMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.AdId));
            if (!isExistsAdResult.Ok)
                return isExistsAdResult;

            var uploadUrlResult = await Mediator.Send(new GetUploadUrlByIdQuery(request.UploadFileId));
            Console.WriteLine(uploadUrlResult.Ok);
            if (!uploadUrlResult.Ok)
                return result.Fail(uploadUrlResult.StatusCode, uploadUrlResult.Message);

            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var commitUploadUrlResult = await Mediator.Send(new CommitUploadUrlCommand(
                id: request.UploadFileId,
                bucket: cosConfig.Bucket.Public,
                key: uploadUrlResult.Data.Key
            ));
            if (!commitUploadUrlResult.Ok)
                return commitUploadUrlResult;

            var updateMediaUrlResult = await Mediator.Send(new UpdateMediaUrlCommand(request.MediaFileId, uploadUrlResult.Data.Key));
            if (!updateMediaUrlResult.Ok)
                return result.Fail(updateMediaUrlResult.StatusCode, updateMediaUrlResult.Message);

            return result.Success(true);
        }
    }
}
