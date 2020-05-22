using System;
using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAdUploadUrl
{
    public class CreateAdUploadUrlCommandHandler : IRequestHandler<CreateAdUploadUrlCommand, HttpResult<AdUploadUrlDto>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<UploadUrlDocument> UploadUrlMongoDB { get; }

        public CreateAdUploadUrlCommandHandler(
            IMediator mediator,
            AdsMongoRepository<UploadUrlDocument> uploadUrlMongoDB)
        {
            Mediator = mediator;
            UploadUrlMongoDB = uploadUrlMongoDB;
        }

        public async Task<HttpResult<AdUploadUrlDto>> Handle(CreateAdUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<AdUploadUrlDto>();
            try
            {
                var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
                var uploadUrlResult = await Mediator.Send(new GenerateUploadUrlCommand(
                    mapperId: request.Id,
                    mapperType: "ad",
                    bucket: cosConfig.Bucket.Public,
                    key: $"ad/{request.Id}/{ObjectId.GenerateNewId()}{request.FileExtension}"
                ));
                if (!uploadUrlResult.Ok)
                    return result.Fail(uploadUrlResult.StatusCode, uploadUrlResult.Message);

                var data = Mapper.Map(uploadUrlResult.Data).ToANew<AdUploadUrlDto>();
                return result.Success(data);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
