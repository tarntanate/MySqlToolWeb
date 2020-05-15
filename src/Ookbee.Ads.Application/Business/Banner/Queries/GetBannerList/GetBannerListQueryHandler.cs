using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerList
{
    public class GetBannerListQueryHandler : IRequestHandler<GetBannerListQuery, HttpResult<IEnumerable<BannerDto>>>
    {
        private OokbeeAdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public GetBannerListQueryHandler(OokbeeAdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<IEnumerable<BannerDto>>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<BannerDto>>> GetListMongoDB(GetBannerListQuery request)
        {
            var result = new HttpResult<IEnumerable<BannerDto>>();
            var items = await BannerMongoDB.FindAsync(
                sort: Builders<BannerDocument>.Sort.Descending(nameof(BannerDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<BannerDto>>();
            return result.Success(data);
        }
    }
}
