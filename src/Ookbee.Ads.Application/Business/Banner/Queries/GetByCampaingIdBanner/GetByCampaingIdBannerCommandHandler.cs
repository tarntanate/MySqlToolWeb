using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetByCampaingIdBanner
{
    public class GetByCampaingIdBannerCommandHandler : IRequestHandler<GetByCampaingIdBannerCommand, HttpResult<IEnumerable<BannerDto>>>
    {
        private OokbeeAdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public GetByCampaingIdBannerCommandHandler(OokbeeAdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<IEnumerable<BannerDto>>> Handle(GetByCampaingIdBannerCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.CampaingId, request.Start, request.Length);
        }

        private async Task<HttpResult<IEnumerable<BannerDto>>> GetOnMongo(string campaignId, int start, int length)
        {
            var result = new HttpResult<IEnumerable<BannerDto>>();
            var items = await BannerMongoDB.FindAsync(
                sort: Builders<BannerDocument>.Sort.Descending(nameof(BannerDocument.Name)),
                start: start,
                length: length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<BannerDto>>();
            return result.Success(data);
        }
    }
}
