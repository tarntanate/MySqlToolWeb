using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetworkItem.Queries.GetAdNetworkItemByUnitId
{
    public class GetAdNetworkItemByUnitIdQueryHandler : IRequestHandler<GetAdNetworkItemByUnitIdQuery, HttpResult<string>>
    {
        private IDatabase AdsRedis { get; }

        public GetAdNetworkItemByUnitIdQueryHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<string>> Handle(GetAdNetworkItemByUnitIdQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var redisKey = CacheKey.AssetByAd(request.AdUnitId);
            var redisValue = await AdsRedis.StringGetAsync(redisKey);

            if (redisValue.HasValue)
                return result.Success(redisValue);

            return result.Fail(404, $"Data not found.");
        }
    }
}
