using AgileObjects.AgileMapper;
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
    public class GenerateUploadUrlCommandHandler : IRequestHandler<GenerateUploadUrlCommand, HttpResult<UploadUrlDto>>
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

        public async Task<HttpResult<UploadUrlDto>> Handle(GenerateUploadUrlCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<UploadUrlDto>> CreateMongoDB(GenerateUploadUrlCommand request)
        {
            var result = new HttpResult<UploadUrlDto>();
            var now = MechineDateTime.Now;
            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var generateSignUrlResult = await Mediator.Send(new GenerateSignURLCommand()
            {
                Bucket = request.Bucket,
                Key = $"temp/{request.Key}"
            }); ;
            var document = new UploadUrlDocument()
            {
                MapperId = request.MapperId,
                MapperType = request.MapperType,
                AppId = cosConfig.AppId,
                Region = cosConfig.Region,
                Bucket = request.Bucket,
                Key = request.Key,
                SignedUrl = generateSignUrlResult.Data,
                CreatedDate = now.DateTime,
                UpdatedDate = now.DateTime
            };
            await UploadUrlMongoDB.AddAsync(document);
            var data = Mapper.Map(document).ToANew<UploadUrlDto>();
            return result.Success(data);
        }
    }
}
