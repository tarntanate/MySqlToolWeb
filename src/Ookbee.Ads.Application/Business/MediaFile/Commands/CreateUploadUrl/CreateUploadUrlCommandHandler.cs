using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateUploadUrl
{
    public class CreateUploadUrlCommandHandler : IRequestHandler<CreateUploadUrlCommand, HttpResult<SignedUrlDto>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<MediaFileDocument> MediaFileMongoDB { get; }

        public CreateUploadUrlCommandHandler(
            IMediator mediator,
            AdsMongoRepository<MediaFileDocument> mediaFileMongoDB)
        {
            Mediator = mediator;
            MediaFileMongoDB = mediaFileMongoDB;
        }

        public async Task<HttpResult<SignedUrlDto>> Handle(CreateUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<SignedUrlDto>();
            try
            {
                var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
                var uploadUrlResult = await Mediator.Send(new GenerateUploadUrlCommand(
                    mapperId: request.Id,
                    mapperType: "ad",
                    bucket: cosConfig.Bucket.Public,
                    key: $"ads/{request.Id}/mediafiles/{request.Id}{request.FileExtension}"
                ));
                if (!uploadUrlResult.Ok)
                    return result.Fail(uploadUrlResult.StatusCode, uploadUrlResult.Message);

                var data = Mapper.Map(uploadUrlResult.Data).ToANew<SignedUrlDto>();
                return result.Success(data);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
