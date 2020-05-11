using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.IsExistsByIdBanner
{
    public class IsExistsByIdBannerCommandHandler : IRequestHandler<IsExistsByIdBannerCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<BannerDocument> BannerMongoDB { get; }

        public IsExistsByIdBannerCommandHandler(OokbeeAdsMongoDBRepository<BannerDocument> bannerMongoDB)
        {
            BannerMongoDB = bannerMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdBannerCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await BannerMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
