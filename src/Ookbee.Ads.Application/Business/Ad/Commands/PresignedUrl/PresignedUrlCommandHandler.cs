using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.PresignedUrl
{
    public class PresignedUrlCommandHandler : IRequestHandler<PresignedUrlCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<UploadUrlDocument> UploadUrlMongoDB { get; }

        public PresignedUrlCommandHandler(
            IMediator mediator,
            AdsMongoRepository<UploadUrlDocument> uploadUrlMongoDB)
        {
            Mediator = mediator;
            UploadUrlMongoDB = uploadUrlMongoDB;
        }

        public async Task<HttpResult<string>> Handle(PresignedUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(request.Id));
            if (!isExistsAdResult.Ok)
                return result.Fail(isExistsAdResult.StatusCode, isExistsAdResult.Message);

            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var signedUrl = await Mediator.Send(new GenerateUploadUrlCommand(
                mapperId: request.Id,
                bucket: cosConfig.Bucket.Private,
                key: $"temp/ad/{request.Id}/{ObjectId.GenerateNewId()}{request.FileExtension}"
            ));
            return signedUrl;
        }
    }
}
