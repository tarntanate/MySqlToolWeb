using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertising.Advertiser.ViewModels;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Advertising.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.GetAdvertiser
{
    public class GetAdvertiserCommandHandler : IRequestHandler<GetAdvertiserCommand, HttpResult<AdvertiserViewModel>>
    {
        private OokbeeAdsMongoRepository<AdvertiserDocument> AdvertiserMongoRepo { get; }

        public GetAdvertiserCommandHandler(OokbeeAdsMongoRepository<AdvertiserDocument> advertiserMongoRepo)
        {
            AdvertiserMongoRepo = advertiserMongoRepo;
        }

        public async Task<HttpResult<AdvertiserViewModel>> Handle(GetAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<AdvertiserViewModel>> GetOnMongo(GetAdvertiserCommand request)
        {
            var result = new HttpResult<AdvertiserViewModel>();
            var item = await AdvertiserMongoRepo.FirstOrDefaultAsync(filter: f => f.Id == request.Id);
            if (item == null)
                return result.Fail(404, $"This Advertiser doesn't exist.");
            var data = Mapper.Map(item).ToANew<AdvertiserViewModel>();
            return result.Success(data);
        }
    }
}
