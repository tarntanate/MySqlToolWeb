using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Advertising.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandHandler : IRequestHandler<CreateAdvertiserCommand, HttpResult<string>>
    {
        private OokbeeAdsMongoRepository<AdvertiserDocument> AdvertiserMongoRepo { get; }

        public CreateAdvertiserCommandHandler(OokbeeAdsMongoRepository<AdvertiserDocument> advertiserMongoRepo)
        {
            AdvertiserMongoRepo = advertiserMongoRepo;
        }

        public async Task<HttpResult<string>> Handle(CreateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateOnMongo(CreateAdvertiserCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var item = Mapper.Map(request).ToANew<AdvertiserDocument>();
                var now = DateTimeHelper.Now;
                item.CreatedDate = now.DateTime;
                item.UpdatedDate = now.DateTime;
                await AdvertiserMongoRepo.AddAsync(item);
                return result.Success(item.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
