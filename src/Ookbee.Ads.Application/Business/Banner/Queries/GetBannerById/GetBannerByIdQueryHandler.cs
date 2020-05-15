using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerById
{
    public class GetBannerByIdQueryHandler : IRequestHandler<GetBannerByIdQuery, HttpResult<BannerDto>>
    {
        private AdsMongoRepository<BannerDocument> BannerMongoDB { get; }

        public GetBannerByIdQueryHandler(AdsMongoRepository<BannerDocument> bannerMongoDB)
        {
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<BannerDto>> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<BannerDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<BannerDto>();
            var item = await BannerMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"Banner '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<BannerDto>();
            return result.Success(data);
        }
    }
}
