using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsByPlatformCache
{
    public class CreateAdStatsByPlatformCacheCommandHandler : IRequestHandler<CreateAdStatsByPlatformCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public CreateAdStatsByPlatformCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdStatsByPlatformCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId, request.Platform);
            var hashField = request.StatsType.ToString();
            var hashValue = request.Value;
            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
            return Unit.Value;
        }
    }
}
