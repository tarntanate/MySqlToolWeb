using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommandHandler : IRequestHandler<GenerateUploadUrlCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<UploadUrlDocument> UploadUrlMongoDB { get; }

        public GenerateUploadUrlCommandHandler(
            IMediator mediator,
            AdsMongoRepository<UploadUrlDocument> uploadUrlMongoDB)
        {
            Mediator = mediator;
            UploadUrlMongoDB = uploadUrlMongoDB;
        }

        public async Task<HttpResult<string>> Handle(GenerateUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(GenerateUploadUrlCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
                var signUrl = await Mediator.Send(new GenerateSignURLCommand()
                {
                    Bucket = request.Bucket,
                    Key = request.Key
                }); ;
                var document = new UploadUrlDocument()
                {
                    MapperId = request.MapperId,
                    AppId = cosConfig.AppId,
                    Region = cosConfig.Region,
                    Bucket = request.Bucket,
                    Key = request.Key,
                    SignedUrl = signUrl,
                    CreatedDate = now.DateTime,
                    UpdatedDate = now.DateTime
                };
                await UploadUrlMongoDB.AddAsync(document);
                return result.Success(signUrl);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
