using MediatR;
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
            var bucket = request.Bucket;
            var key = request.Key;
            var result = await CreateMongoDB(bucket, key);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(string bucket, string key)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
                var signUrl = await GenerateSignURL(cosConfig.Bucket.Temp, key);
                var document = new UploadUrlDocument()
                {
                    AppId = cosConfig.AppId,
                    Region = cosConfig.Region,
                    Bucket = bucket,
                    Key = key,
                    SignedUrl = signUrl,
                    CreatedDate = now.DateTime,
                    UpdatedDate = now.DateTime
                };
                await UploadUrlMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

        private async Task<string> GenerateSignURL(string bucket, string key)
        {
            return await Mediator.Send(new GenerateSignURLCommand()
            {
                Bucket = bucket,
                Key = key
            });
        }
    }
}
