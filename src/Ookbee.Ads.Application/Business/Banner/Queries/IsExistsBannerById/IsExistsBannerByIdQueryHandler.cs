using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.IsExistsBannerById
{
    public class IsExistsBannerByIdQueryHandler : IRequestHandler<IsExistsBannerByIdQuery, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public IsExistsBannerByIdQueryHandler(OokbeeAdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsBannerByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await BannerMongoDB.AnyAsync(filter: f => f.Id == id);
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Banner '{id}' doesn't exist.");
        }
    }
}
